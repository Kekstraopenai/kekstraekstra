using System;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Data;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000116 RID: 278
	internal class {18214} : {17956}
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x000329DC File Offset: 0x00030BDC
		public void Sync(CapturedShipIntance {18216})
		{
			this.CurrentCapturedShip.AmmoInHold.Clean();
			this.CurrentCapturedShip.AmmoInHold.Add({18216}.AmmoInHold);
			this.CurrentCapturedShip.ResourcesInHold.Clean();
			this.CurrentCapturedShip.ResourcesInHold.Add({18216}.ResourcesInHold);
			{17956}.CurrentInstance.Reload();
			{18214}.waitChanges = false;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00032A48 File Offset: 0x00030C48
		private static string GetBanner(CapturedShipIntance {18217}, bool {18218})
		{
			if (Global.Player.IsPortEntry)
			{
				return null;
			}
			Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID({18217}.LastServerUid);
			if (shipFromUID == null || Vector2.Distance(Global.Player.Position, shipFromUID.Position) > (float)({18218} ? 20 : 18))
			{
				return Local.too_far;
			}
			return null;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00032AA4 File Offset: 0x00030CA4
		private static {17956}.Storage GetLeftStorage()
		{
			if (Session.Game.IsInPortOrIsleWithStorage)
			{
				return new {17956}.Storage(Local.storage_d, Session.Account.NearPortStorage, Session.Account.CBallsAtStorage, true, Local.to_hold);
			}
			return new {17956}.Storage(Local.hold, Global.Player.ResourcesOfHold, Global.Player.UsedShipPlayer.BallsOfHold, true, Local.HoldsUiCommon_3);
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00032B10 File Offset: 0x00030D10
		public {18214}(CapturedShipIntance {18219}) : base({18214}.GetBanner({18219}, false), {18214}.GetLeftStorage(), new {17956}.Storage(Local.ship + "NPS", {18219}.ResourcesInHold, {18219}.AmmoInHold, true, Local.HoldsUiCommon_3), delegate({17956}.Movement {18230})
		{
			{18214}.HandleMovement({18230}, {18219});
		})
		{
			{18214} <>4__this = this;
			{18214}.waitChanges = false;
			this.CurrentCapturedShip = {18219};
			if (Global.Player.IsPortEntry)
			{
				Global.Game.ScenePort.MakeAccSync();
			}
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(base.Pos.WH.X * 0.75f, 60f), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Form(Vector2.Zero, {19275}.c_mass, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				}
			});
			stackForm.AddItem(new UiControl[]
			{
				new LiveLabel(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.7f, delegate(LiveLabel {18231})
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
					defaultInterpolatedStringHandler.AppendFormatted<float>({18219}.CurrentHoldLoad);
					defaultInterpolatedStringHandler.AppendLiteral("/");
					defaultInterpolatedStringHandler.AppendFormatted<float>({18219}.MaxHoldLoad(Session.Account.WorldFlag));
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}, 100)
			});
			stackForm.Pos = stackForm.Pos.Offset(-stackForm.Pos.WH.X / 2f, 0f);
			base.AddChild(stackForm);
			base.AddChild(new Label(base.Pos.XY + new Vector2(base.Pos.WH.X * 0.75f, 65f), Fonts.Arial_10, Color.Wheat * 0.7f, Local.captured_ship_hold_tt, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			if (!Session.Game.IsInPortOrIsleWithStorage)
			{
				StackForm stackForm2 = new StackForm(base.Pos.XY + new Vector2(base.Pos.WH.X * 0.25f, 60f), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm2.AddItem(new UiControl[]
				{
					new Form(Vector2.Zero, {19275}.c_mass, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					}
				});
				StackForm stackForm3 = stackForm2;
				UiControl[] array = new UiControl[1];
				array[0] = new LiveLabel(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.7f, delegate(LiveLabel {18228})
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
					defaultInterpolatedStringHandler.AppendFormatted<float>(Global.Player.UsedShipPlayer.GetItemsMass());
					defaultInterpolatedStringHandler.AppendLiteral("/");
					defaultInterpolatedStringHandler.AppendFormatted<float>(Global.Player.UsedShipPlayer.Capacity);
					return defaultInterpolatedStringHandler.ToStringAndClear();
				}, 100);
				stackForm3.AddItem(array);
				stackForm2.Pos = stackForm2.Pos.Offset(-stackForm2.Pos.WH.X / 2f, 0f);
				base.AddChild(stackForm2);
			}
			base.EvRemoveFromContainer += delegate()
			{
				Global.Network.Send(new OnNpcAsCaperActionMsg(<>4__this.CurrentCapturedShip.LastServerUid, OnNpcAsCaperActionMsg.Type.LetGo, null));
			};
			if (Session.Account.WorldFlag == OpenWorldFlag.Peaceful && !this.hasBanner)
			{
				base.TooltipBannerHelper(Local.captured_ship_hold_peaceful_flag_tt);
			}
			if (!this.hasBanner)
			{
				base.AddChild(new Button(new Vector2(base.Pos.Center.X - 15f, base.Pos.XY.Y + 24f), {17745}.c_moveToLeft, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {18232})
				{
					{18214}.MoveAllFromNps(<>4__this.CurrentCapturedShip);
				}));
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x00032E6C File Offset: 0x0003106C
		protected override void UserUpdate(ref FrameTime {18220})
		{
			if (!this.hasBanner && !string.IsNullOrEmpty({18214}.GetBanner(this.CurrentCapturedShip, true)))
			{
				this.BlockAndClose();
			}
			base.UserUpdate(ref {18220});
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x00032E98 File Offset: 0x00031098
		private static void HandleMovement({17956}.Movement {18221}, CapturedShipIntance {18222})
		{
			if ({18214}.waitChanges)
			{
				return;
			}
			ResourceInfo resourceInfo = {18221}.ID as ResourceInfo;
			if (resourceInfo != null && resourceInfo.CantTransferWithPeacefulFlag)
			{
				{19994}.Me({19988}.InfoRed, Local.pearl_move_prohibited(resourceInfo.Name), Array.Empty<object>());
				return;
			}
			OnNpcAsCaperActionMsg.Type type;
			if ({18221}.From == {17956}.StorageId.Left)
			{
				type = (({18221}.ID.getType == StorageAssetEnum.Ammo) ? OnNpcAsCaperActionMsg.Type.MoveAmmoTo : OnNpcAsCaperActionMsg.Type.MoveResTo);
			}
			else
			{
				type = (({18221}.ID.getType == StorageAssetEnum.Ammo) ? OnNpcAsCaperActionMsg.Type.MoveAmmoFrom : OnNpcAsCaperActionMsg.Type.MoveResFrom);
			}
			if (type == OnNpcAsCaperActionMsg.Type.MoveAmmoTo || type == OnNpcAsCaperActionMsg.Type.MoveResTo)
			{
				float num = {18222}.CurrentHoldLoad + {18221}.ID.getStorageMass * (float){18221}.Count;
				if (num > {18222}.MaxHoldLoad(Session.Account.WorldFlag))
				{
					new {17312}(Local.overload_warning((int)num, (int){18222}.MaxHoldLoad(Session.Account.WorldFlag)));
					return;
				}
			}
			{18214} {18214} = {17956}.CurrentInstance as {18214};
			if ({18214} != null)
			{
				{18214}.waitChanges = true;
				{18214}.{18224}(new OnNpcAsCaperActionMsg({18222}.LastServerUid, type, new GSI().Exs((int){18221}.ID.ID, {18221}.Count)), {18221}.ID.getStorageMass * (float){18221}.Count);
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x00032FC4 File Offset: 0x000311C4
		private static void MoveAllFromNps(CapturedShipIntance {18223})
		{
			if ({18214}.waitChanges)
			{
				return;
			}
			{18214}.waitChanges = true;
			{18214} {18214} = {17956}.CurrentInstance as {18214};
			if ({18214} != null)
			{
				{18214}.{18224}(new OnNpcAsCaperActionMsg({18223}.LastServerUid, OnNpcAsCaperActionMsg.Type.MoveResFrom, {18223}.ResourcesInHold), 0f);
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0003300A File Offset: 0x0003120A
		private void {18224}(OnNpcAsCaperActionMsg {18225}, float {18226})
		{
			Global.Network.Send({18225});
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0003301C File Offset: 0x0003121C
		public static void OpenDestroyNpsDialog(AllyStateTransfer {18227})
		{
			string {17133} = "";
			string destroy_captured_ship_ = Local.destroy_captured_ship_1;
			string commonItemCraftUi_ = Local.CommonItemCraftUi_0;
			Action<int> {17136} = delegate(int {18229})
			{
				if ({18229} == 0)
				{
					Global.Network.Send(new OnNpcAsCaperActionMsg({18227}.uID, OnNpcAsCaperActionMsg.Type.DestroyForResources, null));
				}
			};
			bool {17137} = true;
			NpcInfo {4075} = Gameplay.NpcsInfo.FromID({18227}.CapturedShipInfoId);
			Decorator game = Session.Game;
			new {17107}({17133}, destroy_captured_ship_, commonItemCraftUi_, {17136}, {17137}, new CraftingRecipe(WosbNpcs.NpcToDestroyResources({4075}, game)), new string[]
			{
				Local.to_continue,
				Local.undo
			});
		}

		// Token: 0x040005DF RID: 1503
		public CapturedShipIntance CurrentCapturedShip;

		// Token: 0x040005E0 RID: 1504
		private static bool waitChanges;
	}
}
