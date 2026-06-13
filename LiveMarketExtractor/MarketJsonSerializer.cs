using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace LiveMarketExtractor
{
    /// <summary>
    /// Handles JSON serialization and export of market data
    /// </summary>
    public class MarketJsonSerializer
    {
        private readonly JsonSerializerSettings _jsonSettings;

        public MarketJsonSerializer()
        {
            _jsonSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                DateFormatString = "yyyy-MM-dd HH:mm:ss UTC",
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        #region Port Markets

        /// <summary>
        /// Serializes port market data to JSON
        /// </summary>
        public string SerializePortMarkets(Dictionary<string, MarketDataExtractor.PortMarketData> portMarkets)
        {
            try
            {
                var exportData = new
                {
                    extractedAt = DateTime.UtcNow,
                    dataType = "PORT_MARKETS",
                    ports = portMarkets.Select(kvp => new
                    {
                        portName = kvp.Value.PortName,
                        portID = kvp.Value.PortID,
                        extractedAt = kvp.Value.ExtractedAt,
                        items = kvp.Value.Items.Select(itemKvp => new
                        {
                            itemID = itemKvp.Value.ItemID,
                            itemName = itemKvp.Value.ItemName,
                            buyPrice = itemKvp.Value.BuyPrice,
                            sellPrice = itemKvp.Value.SellPrice,
                            availableCount = itemKvp.Value.AvailableCount,
                            isLiveTrading = itemKvp.Value.IsLiveTrading,
                            lastUpdated = itemKvp.Value.LastUpdated
                        }).ToList()
                    }).ToList()
                };

                return JsonConvert.SerializeObject(exportData, _jsonSettings);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = $"Serialization failed: {ex.Message}" });
            }
        }

        /// <summary>
        /// Exports port market data as CSV
        /// </summary>
        public string ExportPortMarketsAsCSV(Dictionary<string, MarketDataExtractor.PortMarketData> portMarkets)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Port Name,Port ID,Item Name,Item ID,Buy Price,Sell Price,Available Count,Is Live Trading,Last Updated");

            foreach (var port in portMarkets.Values)
            {
                foreach (var item in port.Items.Values)
                {
                    sb.AppendLine($"\"{port.PortName}\",{port.PortID},\"{item.ItemName}\",{item.ItemID},{item.BuyPrice},{item.SellPrice},{item.AvailableCount},{item.IsLiveTrading},\"{item.LastUpdated:yyyy-MM-dd HH:mm:ss}\"");
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Auction Markets

        /// <summary>
        /// Serializes auction market data to JSON
        /// </summary>
        public string SerializeAuctionMarkets(Dictionary<string, MarketDataExtractor.AuctionMarketData> auctionMarkets)
        {
            try
            {
                var exportData = new
                {
                    extractedAt = DateTime.UtcNow,
                    dataType = "AUCTION_MARKETS",
                    ports = auctionMarkets.Select(kvp => new
                    {
                        portName = kvp.Value.PortName,
                        portID = kvp.Value.PortID,
                        extractedAt = kvp.Value.ExtractedAt,
                        buyOrders = kvp.Value.BuyOrders.Select(orderKvp => new
                        {
                            itemName = orderKvp.Key,
                            orders = orderKvp.Value.Select(order => new
                            {
                                orderID = order.OrderID,
                                quantity = order.Quantity,
                                pricePerUnit = order.Price,
                                totalValue = order.TotalValue,
                                sellerSID = order.SellerSID,
                                remainingLifetime = order.RemainingLifetime,
                                createdAt = order.CreatedAt,
                                partialSellAllowed = order.IsPartialSellAllowed
                            }).ToList()
                        }).ToList(),
                        sellOrders = kvp.Value.SellOrders.Select(orderKvp => new
                        {
                            itemName = orderKvp.Key,
                            orders = orderKvp.Value.Select(order => new
                            {
                                orderID = order.OrderID,
                                quantity = order.Quantity,
                                pricePerUnit = order.Price,
                                totalValue = order.TotalValue,
                                sellerSID = order.SellerSID,
                                remainingLifetime = order.RemainingLifetime,
                                createdAt = order.CreatedAt,
                                partialSellAllowed = order.IsPartialSellAllowed
                            }).ToList()
                        }).ToList()
                    }).ToList()
                };

                return JsonConvert.SerializeObject(exportData, _jsonSettings);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = $"Serialization failed: {ex.Message}" });
            }
        }

        /// <summary>
        /// Exports auction market data as CSV
        /// Format: Port, Item, Order Type (BUY/SELL), Quantity, Price Per Unit, Total Value, Remaining Lifetime
        /// </summary>
        public string ExportAuctionMarketsAsCSV(Dictionary<string, MarketDataExtractor.AuctionMarketData> auctionMarkets)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Port Name,Port ID,Item Name,Order Type,Quantity,Price Per Unit,Total Value,Seller SID,Remaining Lifetime (sec),Created At,Partial Sell Allowed");

            foreach (var port in auctionMarkets.Values)
            {
                // Buy Orders (Bids)
                foreach (var itemOrders in port.BuyOrders)
                {
                    foreach (var order in itemOrders.Value)
                    {
                        sb.AppendLine($"\"{port.PortName}\",{port.PortID},\"{itemOrders.Key}\",BUY,{order.Quantity},{order.Price},{order.TotalValue},{order.SellerSID},{order.RemainingLifetime:F2},\"{order.CreatedAt:yyyy-MM-dd HH:mm:ss}\",{order.IsPartialSellAllowed}");
                    }
                }

                // Sell Orders (Asks)
                foreach (var itemOrders in port.SellOrders)
                {
                    foreach (var order in itemOrders.Value)
                    {
                        sb.AppendLine($"\"{port.PortName}\",{port.PortID},\"{itemOrders.Key}\",SELL,{order.Quantity},{order.Price},{order.TotalValue},{order.SellerSID},{order.RemainingLifetime:F2},\"{order.CreatedAt:yyyy-MM-dd HH:mm:ss}\",{order.IsPartialSellAllowed}");
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// Creates a market summary with best prices
        /// </summary>
        public string GenerateMarketSummary(
            Dictionary<string, MarketDataExtractor.PortMarketData> portMarkets,
            Dictionary<string, MarketDataExtractor.AuctionMarketData> auctionMarkets)
        {
            try
            {
                var summary = new
                {
                    generatedAt = DateTime.UtcNow,
                    summary = new
                    {
                        totalPorts = portMarkets.Count + auctionMarkets.Count,
                        portMarketStats = new
                        {
                            totalItems = portMarkets.Values.Sum(p => p.Items.Count),
                            tradingPorts = portMarkets.Values.Count(p => p.Items.Values.Any(i => i.IsLiveTrading))
                        },
                        auctionStats = new
                        {
                            totalActiveBuyOrders = auctionMarkets.Values.Sum(a => a.BuyOrders.Values.Sum(o => o.Count)),
                            totalActiveSellOrders = auctionMarkets.Values.Sum(a => a.SellOrders.Values.Sum(o => o.Count)),
                            uniqueItemsForSale = auctionMarkets.Values.Sum(a => a.SellOrders.Count)
                        }
                    }
                };

                return JsonConvert.SerializeObject(summary, _jsonSettings);
            }
            catch (Exception ex)
            {
                return JsonConvert.SerializeObject(new { error = $"Summary generation failed: {ex.Message}" });
            }
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Parses JSON market data back to objects
        /// </summary>
        public T Deserialize<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json, _jsonSettings);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to deserialize JSON: {ex.Message}", ex);
            }
        }

        #endregion
    }
}
