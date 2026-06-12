using System;
using System.Collections.Generic;
using System.IO;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Collections;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000507 RID: 1287
	internal class VisualHelper
	{
		// Token: 0x06001CC8 RID: 7368 RVA: 0x00107CDC File Offset: 0x00105EDC
		public static void UpdateUniqueInventory()
		{
			if (!Global.Network.IsCacheSynchronized)
			{
				return;
			}
			List<int> list = new List<int>();
			foreach (KeyValuePair<uint, ObservableTlist<int>> keyValuePair in LocalContent.GuildDesigns)
			{
				foreach (int item in ((IEnumerable<int>)keyValuePair.Value))
				{
					list.Add(item);
				}
			}
			foreach (KeyValuePair<FractionID, ValueTuple<int, int>> keyValuePair2 in LocalContent.Geraldics)
			{
				if (keyValuePair2.Value.Item1 > 0)
				{
					list.Add(keyValuePair2.Value.Item1);
				}
				if (keyValuePair2.Value.Item2 > 0)
				{
					list.Add(keyValuePair2.Value.Item2);
				}
			}
			VisualHelper.designsAllow.Clear();
			if (Session.Guild != null)
			{
				if (ShipDesignInfo.ServerIdParam == "ru1")
				{
					ObservableTlist<int> observableTlist;
					LocalContent.GuildDesigns.TryGetValue(Session.Guild.GuildID, out observableTlist);
					if (observableTlist != null)
					{
						VisualHelper.designsAllow.Add(observableTlist);
					}
				}
				if (!Session.Guild.IsFlotilia)
				{
					ShipDesignInfo shipDesignInfo;
					ShipDesignInfo shipDesignInfo2;
					LocalContent.GetFractionGeraldics(Session.Guild.Fraction, out shipDesignInfo, out shipDesignInfo2);
					if (shipDesignInfo != null)
					{
						Tlist<int> tlist = VisualHelper.designsAllow;
						int num = (int)shipDesignInfo.ID;
						tlist.Add(num);
					}
					if (shipDesignInfo2 != null)
					{
						Tlist<int> tlist2 = VisualHelper.designsAllow;
						int num = (int)shipDesignInfo2.ID;
						tlist2.Add(num);
					}
				}
			}
			foreach (PublicDesignInfo publicDesignInfo in ((IEnumerable<PublicDesignInfo>)Session.PlayerPublicDesigns))
			{
				list.Add(publicDesignInfo.ID + 10000);
				Tlist<int> tlist3 = VisualHelper.designsAllow;
				int num = publicDesignInfo.ID + 10000;
				tlist3.Add(num);
			}
			foreach (int num2 in list)
			{
				if (VisualHelper.designsAllow.IndexOf(num2) == -1 && (ShipDesignInfo.IsPublicDesign(num2) || !ShipDesignInfo.Resolve(num2).InShop))
				{
					int count = Session.Account.DesingElementsAtStorage.GetCount(num2);
					if (count > 0)
					{
						Session.Account.DesingElementsAtStorage.AddOrRemove(num2, -count);
					}
					foreach (PlayerShipDynamicInfo playerShipDynamicInfo in Session.Account.Shipyard.List)
					{
						if (playerShipDynamicInfo.RemoveDesignElementById(num2) > 0 && playerShipDynamicInfo == Global.Player.UsedShipPlayer)
						{
							Global.Player.Client.UpdateModel();
						}
					}
				}
			}
			foreach (int {2393} in ((IEnumerable<int>)VisualHelper.designsAllow))
			{
				ShipDesignInfo shipDesignInfo3 = ShipDesignInfo.Resolve({2393});
				int count2 = Session.Account.DesingElementsAtStorage.GetCount((int)shipDesignInfo3.ID);
				if (count2 > 3)
				{
					Session.Account.DesingElementsAtStorage.AddOrRemove((int)shipDesignInfo3.ID, -(count2 - 2));
				}
				if (count2 == 0)
				{
					Session.Account.DesingElementsAtStorage.AddOrRemove((int)shipDesignInfo3.ID, 1);
				}
			}
		}

		// Token: 0x06001CC9 RID: 7369 RVA: 0x0010808C File Offset: 0x0010628C
		public static Texture2D LoadTexture2DFromBytes(byte[] {25365}, int {25366} = 2147483647, bool {25367} = false)
		{
			Texture2D result;
			try
			{
				using (MemoryStream memoryStream = new MemoryStream({25365}))
				{
					Texture2D texture2D = Texture2D.FromStream(Global.Game.GraphicsDevice, memoryStream);
					if (texture2D.Width > {25366} || texture2D.Height > {25366})
					{
						float val = (float){25366} / (float)texture2D.Width;
						float val2 = (float){25366} / (float)texture2D.Height;
						float num = Math.Min(1f, Math.Min(val, val2));
						int width = (int)((float)texture2D.Width * num);
						int height = (int)((float)texture2D.Height * num);
						texture2D.Dispose();
						texture2D = Texture2D.FromStream(Global.Game.GraphicsDevice, memoryStream, width, height, true);
					}
					result = texture2D;
				}
			}
			catch (Exception {25356})
			{
				Helpers.SendError({25356}, "LoadTexture2DFromBytes", false, false);
				result = ({25367} ? null : Engine.FillerTexture);
			}
			return result;
		}

		// Token: 0x06001CCA RID: 7370 RVA: 0x0010816C File Offset: 0x0010636C
		public static bool FileToJpeg(string {25368}, out byte[] {25369}, out Texture2D {25370}, out bool {25371}, int {25372})
		{
			bool result;
			using (FileStream fileStream = File.OpenRead({25368}))
			{
				{25369} = null;
				{25370} = null;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					for (int i = 128; i > 64; i -= 8)
					{
						memoryStream.Position = 0L;
						Texture2D texture2D = {25370};
						if (texture2D != null)
						{
							texture2D.Dispose();
						}
						{25370} = Texture2D.FromStream(Engine.GS.graphicsDevice, fileStream, i, i, true);
						{25370}.SaveAsJpeg(memoryStream, {25370}.Width, {25370}.Height, 90);
						if (memoryStream.Position < (long){25372})
						{
							{25369} = new byte[memoryStream.Position];
							memoryStream.Seek(memoryStream.Position, SeekOrigin.End);
							memoryStream.Position = 0L;
							memoryStream.Read({25369}, 0, {25369}.Length);
							break;
						}
					}
					memoryStream.Dispose();
					if ({25369} != null)
					{
						{25371} = false;
						result = true;
					}
					else
					{
						{25371} = true;
						result = false;
					}
				}
			}
			return result;
		}

		// Token: 0x06001CCB RID: 7371 RVA: 0x00108270 File Offset: 0x00106470
		public static float GetRelativeScale(Vector2 {25373}, Vector2 {25374})
		{
			float num = {25374}.X / {25373}.X;
			float num2 = {25374}.Y / {25373}.Y;
			if (num >= num2)
			{
				return num2;
			}
			return num;
		}

		// Token: 0x04001C7E RID: 7294
		private static Tlist<ValueTuple<Vector3, float>> tempList1 = new Tlist<ValueTuple<Vector3, float>>();

		// Token: 0x04001C7F RID: 7295
		private static Tlist<ValueTuple<Vector3, float>> tempList2 = new Tlist<ValueTuple<Vector3, float>>();

		// Token: 0x04001C80 RID: 7296
		private static Tlist<int> designsAllow = new Tlist<int>();
	}
}
