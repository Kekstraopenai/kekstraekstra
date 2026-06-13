using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Game;
using Common.Resources;
using TheraEngine.Collections;
using World_Of_Sea_Battle.Core;

namespace LiveMarketExtractor
{
    /// <summary>
    /// Extracts live market data from game client memory
    /// Provides both static trading (port prices) and auction (player-to-player) data
    /// </summary>
    public class MarketDataExtractor
    {
        private PortTradePage_SyncProtected _tradePageInstance;

        public MarketDataExtractor()
        {
            _tradePageInstance = PortTradePage_SyncProtected.CurrentInstance;
        }

        #region Static Market (Port Trading)

        /// <summary>
        /// Extracts all static market data (NPC trading) from current port
        /// Structure: Port -> Item -> Buy/Sell Prices
        /// </summary>
        public Dictionary<string, PortMarketData> ExtractPortMarkets()
        {
            var result = new Dictionary<string, PortMarketData>();

            if (_tradePageInstance == null || Global.Player?.NearPort == null)
                return result;

            try
            {
                var port = Global.Player.NearPort;
                var portKey = $"{port.PortName} (ID: {port.PortID})";

                var marketData = new PortMarketData
                {
                    PortName = port.PortName,
                    PortID = port.PortID,
                    Items = new Dictionary<string, ResourcePriceData>()
                };

                // Extract all resources with prices
                foreach (ResourceInfo resourceInfo in (IEnumerable<ResourceInfo>)Gameplay.ItemsInfo)
                {
                    if (resourceInfo == null || resourceInfo.IsTradingItem || resourceInfo.ID == 8)
                        continue;

                    // Check if this port has this item for sale
                    int shopPrice = port.ShopResources[(int)resourceInfo.ID];
                    int sellPrice = port.SellResources[(int)resourceInfo.ID];
                    int liveCount = port.LiveTrading.GetCount((int)resourceInfo.ID);

                    // Get live trading prices if available
                    float liveBuyPrice = 0f;
                    float liveSellPrice = 0f;

                    if (liveCount > 0)
                    {
                        // If port has live trading, get dynamic prices
                        var liveTrading = new PortLiveTrading(port.LiveTrading.Clone(), port.PortID);
                        liveTrading.NowPrice((int)resourceInfo.ID, Session.EventActionsPipeline, out liveBuyPrice, out liveSellPrice);
                    }

                    // Determine final prices
                    float buyPrice = shopPrice > 0 ? shopPrice : liveBuyPrice;
                    float sellPrice_Final = sellPrice > 0 ? sellPrice : liveSellPrice;

                    marketData.Items[resourceInfo.Name] = new ResourcePriceData
                    {
                        ItemID = (int)resourceInfo.ID,
                        ItemName = resourceInfo.Name,
                        BuyPrice = (int)buyPrice,
                        SellPrice = (int)sellPrice_Final,
                        AvailableCount = liveCount,
                        IsLiveTrading = liveCount > 0,
                        LastUpdated = DateTime.UtcNow
                    };
                }

                result[portKey] = marketData;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting port markets: {ex.Message}");
            }

            return result;
        }

        #endregion

        #region Auction Market (Player-to-Player)

        /// <summary>
        /// Extracts auction data (player-to-player trading)
        /// Structure: Port -> Item -> Buy Orders (bids) and Sell Orders (asks)
        /// </summary>
        public Dictionary<string, AuctionMarketData> ExtractAuctionMarkets()
        {
            var result = new Dictionary<string, AuctionMarketData>();

            if (_tradePageInstance == null)
                return result;

            try
            {
                // Access the market cache: LinkedDictionrary<int, TradeOrderCommon>
                // Key = PortID, Value = List of orders for that port
                var allOrders = ExtractOrdersByPort();

                foreach (var portOrders in allOrders)
                {
                    int portID = portOrders.Key;
                    var orders = portOrders.Value;

                    if (orders == null || orders.Count == 0)
                        continue;

                    var port = GetPortByID(portID);
                    if (port == null)
                        continue;

                    var portKey = $"{port.PortName} (ID: {portID})";
                    var auctionData = new AuctionMarketData
                    {
                        PortName = port.PortName,
                        PortID = portID,
                        BuyOrders = new Dictionary<string, List<AuctionOrder>>(),
                        SellOrders = new Dictionary<string, List<AuctionOrder>>()
                    };

                    // Separate buy and sell orders by item
                    foreach (var order in orders)
                    {
                        if (order.Mode == TradeOrderMode.Holding)
                            continue; // Skip holding orders

                        string itemKey = order.ItemInfo?.getName ?? "Unknown";
                        var auctionOrder = new AuctionOrder
                        {
                            OrderID = order.OrderServerID,
                            ItemName = itemKey,
                            Quantity = order.CurrentCount,
                            Price = (int)order.Price.CostPerUnit,
                            SellerSID = order.OwnerSID,
                            RemainingLifetime = order.RemainingLifetime,
                            CreatedAt = DateTime.UtcNow.AddSeconds(-order.RemainingLifetime),
                            IsPartialSellAllowed = order.Mode == TradeOrderMode.AllowSellPartial
                        };

                        if (order.Mode == TradeOrderMode.Shop)
                        {
                            // Seller is asking (wants to sell)
                            if (!auctionData.SellOrders.ContainsKey(itemKey))
                                auctionData.SellOrders[itemKey] = new List<AuctionOrder>();
                            auctionData.SellOrders[itemKey].Add(auctionOrder);
                        }
                        else if (order.Mode == TradeOrderMode.AllowSellPartial || order.Mode == TradeOrderMode.DisallowSellPartial)
                        {
                            // Buyer is bidding (wants to buy)
                            if (!auctionData.BuyOrders.ContainsKey(itemKey))
                                auctionData.BuyOrders[itemKey] = new List<AuctionOrder>();
                            auctionData.BuyOrders[itemKey].Add(auctionOrder);
                        }
                    }

                    // Sort orders by price
                    foreach (var kvp in auctionData.SellOrders)
                        kvp.Value.Sort((a, b) => a.Price.CompareTo(b.Price)); // Lowest price first

                    foreach (var kvp in auctionData.BuyOrders)
                        kvp.Value.Sort((a, b) => b.Price.CompareTo(a.Price)); // Highest price first

                    result[portKey] = auctionData;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting auction markets: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// Extracts orders organized by port ID
        /// </summary>
        private Dictionary<int, List<TradeOrderCommon>> ExtractOrdersByPort()
        {
            var result = new Dictionary<int, List<TradeOrderCommon>>();

            try
            {
                if (_tradePageInstance == null)
                    return result;

                // Access internal field: ppOrdersByPort (LinkedDictionrary<int, TradeOrderCommon>)
                // Through reflection or direct memory access would be needed here
                // For now, we'll use the public methods available

                var allPortsOrders = new Dictionary<int, List<TradeOrderCommon>>();

                // Get current port orders
                if (Global.Player?.NearPort != null)
                {
                    int nearPortID = Global.Player.NearPort.PortID;
                    allPortsOrders[nearPortID] = new List<TradeOrderCommon>();
                }

                return allPortsOrders;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting orders by port: {ex.Message}");
            }

            return result;
        }

        #endregion

        #region Helpers

        private IslePortInfo GetPortByID(int portID)
        {
            try
            {
                return Gameplay.PortsInfo.FirstOrDefault(p => p.PortID == portID);
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region Data Models

        public class PortMarketData
        {
            public string PortName { get; set; }
            public int PortID { get; set; }
            public Dictionary<string, ResourcePriceData> Items { get; set; } = new();
            public DateTime ExtractedAt { get; set; } = DateTime.UtcNow;
        }

        public class ResourcePriceData
        {
            public int ItemID { get; set; }
            public string ItemName { get; set; }
            public int BuyPrice { get; set; }      // Price to buy from NPC
            public int SellPrice { get; set; }     // Price to sell to NPC
            public int AvailableCount { get; set; }
            public bool IsLiveTrading { get; set; }
            public DateTime LastUpdated { get; set; }
        }

        public class AuctionMarketData
        {
            public string PortName { get; set; }
            public int PortID { get; set; }
            public Dictionary<string, List<AuctionOrder>> BuyOrders { get; set; } = new();  // Items buyers want
            public Dictionary<string, List<AuctionOrder>> SellOrders { get; set; } = new(); // Items sellers offer
            public DateTime ExtractedAt { get; set; } = DateTime.UtcNow;
        }

        public class AuctionOrder
        {
            public int OrderID { get; set; }
            public string ItemName { get; set; }
            public int Quantity { get; set; }
            public int Price { get; set; }          // Price per unit
            public uint SellerSID { get; set; }
            public float RemainingLifetime { get; set; }
            public DateTime CreatedAt { get; set; }
            public bool IsPartialSellAllowed { get; set; }

            public int TotalValue => Price * Quantity;
        }

        #endregion
    }
}
