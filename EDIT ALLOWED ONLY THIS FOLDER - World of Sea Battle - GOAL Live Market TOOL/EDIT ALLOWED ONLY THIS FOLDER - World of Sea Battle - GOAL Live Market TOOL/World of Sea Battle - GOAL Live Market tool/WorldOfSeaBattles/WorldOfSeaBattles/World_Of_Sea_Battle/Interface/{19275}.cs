using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001D1 RID: 465
	internal sealed class {19275} : {18027}
	{
		// Token: 0x06000A74 RID: 2676 RVA: 0x00053DE6 File Offset: 0x00051FE6
		public void ExternalUpdate(PbsBuildingStatus {19287})
		{
			{19275}.provider.BuildingHold = {19287};
			base.ReloadTable();
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x00053DF9 File Offset: 0x00051FF9
		public void ExternalUpdate(DropBonusInfo {19288})
		{
			if ({19275}.provider.BuildingHold != null)
			{
				return;
			}
			{19275}.provider.State = {19288};
			base.ReloadTable();
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x00053E1C File Offset: 0x0005201C
		private {19275}(DropBonusInfo {19289}, bool {19290}, bool {19291}, int {19292}, string {19293}, PbsBuildingStatus {19294}, bool {19295})
		{
			{19275}.Provider provider = new {19275}.Provider({19291}, {19292} != -1);
			provider.State = {19289};
			provider.dropUID = {19292};
			provider.BuildingHold = {19294};
			{19275}.provider = provider;
			base..ctor(provider, true, null, {19293}, 0f, {19294} == null);
			base.Pos = base.Pos.SetHeight((float){19275}.back.Height);
			if ({19290})
			{
				this.TexturePath = Rectangle.Empty;
			}
			Global.Player.ResetSpeedAndRotation();
			{19275}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				if ({19294} == null)
				{
					Session.EducState_LootedShipsCount++;
					EducationHelper.OnShipWasLooted();
				}
				{19275}.CurrentInstance = null;
			};
			this.DropUID = {19292};
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(base.Pos.WH.X / 2f, 204f), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				new Form(Vector2.Zero, {19275}.c_mass, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				}
			});
			stackForm.AddItem(new UiControl[]
			{
				new LiveLabel(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.7f, delegate(LiveLabel {19380})
				{
					{19380}.BasicColor = (((float)((int){19275}.provider.State.Mass) > Global.Player.UsedShipPlayer.FreeCapacity) ? Color.Orange : Color.Wheat) * 0.7f;
					if (!{19290})
					{
						return " " + ((int){19275}.provider.State.Mass).ToString();
					}
					return " ";
				}, 100)
			});
			if ({19295})
			{
				stackForm.AddSpace(10f);
				stackForm.AddItem(new UiControl[]
				{
					new Form(new Marker(0f, 0f, 24f, 24f), CommonAtlas.goldIconSingle32, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					}
				});
				stackForm.AddItem(new UiControl[]
				{
					new LiveLabel(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.9f, delegate()
					{
						int num = {19289}.Resources.ResourceInfo.Sum((GSILocalEnumerablePair<ResourceInfo> {19379}) => {19379}.Info.MediumCost.Value * {19379}.Count) + {19289}.GoldBonus;
						if (num > 0)
						{
							string text = num.ToString();
							if (!{19289}.Ammo.IsEmpty || !{19289}.PowderKegs.IsEmpty || !{19289}.Cannons.IsEmpty || !{19289}.Crew.IsEmpty)
							{
								text += "+";
							}
							return text;
						}
						return "";
					}, 100)
				});
			}
			stackForm.Pos = stackForm.Pos.Offset(-stackForm.Pos.WH.X / 2f, 0f);
			base.AddChild(stackForm);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00054050 File Offset: 0x00052250
		public {19275}(PbsBuildingStatus {19296}, bool {19297}) : this(default(DropBonusInfo), true, true, -1, Local.storage_guild, {19296}, false)
		{
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					Marker marker = new Marker((float)(64 + i * 58), (float)(205 + j * 58), 53f, 53f);
					Marker marker2 = base.Pos;
					Form form = new Form(marker.Offset(marker2.XY), CommonAtlas.whitePixel, Color.Black * 0.16862746f, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						RenderToDepthMap = false
					};
					base.AddChild(form);
					form.MoveToBackLevel();
				}
			}
			for (int k = 0; k < 6; k++)
			{
				for (int l = 0; l < 3; l++)
				{
					Marker marker2 = new Marker((float)(64 + k * 58), (float)(395 + l * 58), 53f, 53f);
					Marker marker = base.Pos;
					Form form2 = new Form(marker2.Offset(marker.XY), CommonAtlas.whitePixel, Color.Black * 0.16862746f, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						RenderToDepthMap = false
					};
					base.AddChild(form2);
					form2.MoveToBackLevel();
				}
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x000541A4 File Offset: 0x000523A4
		public {19275}(int {19298}, DropBonusInfo {19299}) : this({19299}, false, false, {19298}, Local.DropInteropUi_1, null, true)
		{
			GameScene.IncreaseGameInput();
			base.EvRemoveFromContainer += delegate()
			{
				GameScene.DecreaseGameInput();
			};
			this.{19303} = DropRules.Default;
			ClientDrop dropByUid = Global.Game.WorldInstance.GetDropByUid(this.DropUID);
			if (dropByUid != null)
			{
				this.{19303} = dropByUid.Rule;
				this.{19304} = dropByUid.RulesData;
			}
			Marker marker = new Marker(38f, 322f, 396f, 25f);
			Marker marker2 = base.Pos;
			this.{19302} = new Form(marker.Offset(marker2.XY), CommonAtlas.whitePixel, ((this.{19303} == DropRules.OnlyEngagedOwnPart || this.{19303} == DropRules.OnlyEngagedOwnPartWithoutCombat) ? new Color(192, 103, 96, 255) : new Color(132, 133, 96, 255)) * 0.2f, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Form form = this.{19302};
			marker2 = this.{19302}.Pos;
			form.AddChild(new Label(marker2.Center, Fonts.Philosopher_16, new Color(132, 163, 96, 255), "[" + Global.Settings.kb_Action.KeyToString + ((this.{19303} == DropRules.OnlyEngagedOwnPart || this.{19303} == DropRules.OnlyEngagedOwnPartWithoutCombat) ? Local.DropInteropUi_2_B : Local.DropInteropUi_2_A), PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			base.AddChild(this.{19302});
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 2; j++)
				{
					marker2 = new Marker((float)(64 + i * 58), (float)(205 + j * 58), 53f, 53f);
					marker = base.Pos;
					Form form2 = new Form(marker2.Offset(marker.XY), CommonAtlas.whitePixel, Color.Black * 0.16862746f, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						RenderToDepthMap = false
					};
					base.AddChild(form2);
					form2.MoveToBackLevel();
				}
			}
			for (int k = 0; k < 6; k++)
			{
				for (int l = 0; l < 3; l++)
				{
					marker = new Marker((float)(64 + k * 58), (float)(395 + l * 58), 53f, 53f);
					marker2 = base.Pos;
					Form form3 = new Form(marker.Offset(marker2.XY), CommonAtlas.whitePixel, Color.Black * 0.16862746f, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						RenderToDepthMap = false
					};
					base.AddChild(form3);
					form3.MoveToBackLevel();
				}
			}
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0005446C File Offset: 0x0005266C
		protected override void UserUpdate(ref FrameTime {19300})
		{
			if (this.DropUID != -1)
			{
				ClientDrop dropByUid = Global.Game.WorldInstance.GetDropByUid(this.DropUID);
				if (dropByUid == null || !dropByUid.IsInsideInteropZone())
				{
					base.BlockAndClose();
				}
			}
			if (this.{19301} > 0f)
			{
				float num = {19275}.provider.State.Resources.ComputeMass<ResourceInfo>() + {19275}.provider.State.Ammo.ComputeMass<CannonBallInfo>() + (float)({19275}.provider.State.GoldBonus * 2);
				this.{19301} -= {19300}.secElapsed / MathHelper.Lerp(0.6f, 2f, Math.Min(1f, num / 1000f));
				if (this.{19301} < 0f)
				{
					(({18027}.IProvider){19275}.provider).TryMoveAllToShip();
					base.BlockAndClose();
					this.{19301} = 0f;
				}
			}
			if (Global.Settings.kb_Action.IsClick && this.{19302} != null)
			{
				if ({19275}.provider.State.IsEmpty)
				{
					base.BlockAndClose();
				}
				else if (this.{19301} == 0f)
				{
					this.{19301} = 1f;
					this.{19302}.ClearAllChild();
					this.{19302}.AddChild(new Label(this.{19302}.Pos.Center, Fonts.Philosopher_14, new Color(132, 163, 96, 255), Local.DropInteropUi_3, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
				}
			}
			base.UserUpdate(ref {19300});
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000545FE File Offset: 0x000527FE
		protected override void UserBackRender()
		{
			base.UserBackRender();
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x00054608 File Offset: 0x00052808
		protected override void UserFrontRender()
		{
			Device gs = Engine.GS;
			Rectangle rectangle = new Rectangle(2303, 1981, 119, 160);
			Rectangle rectangle2 = base.Pos.ToRect();
			gs.Draw(rectangle, rectangle2);
			base.UserFrontRender();
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00054650 File Offset: 0x00052850
		public override void UserMiddleRender()
		{
			base.UserMiddleRender();
			if (this.{19301} > 0f)
			{
				float num = 1f - this.{19301};
				Marker marker = new Marker(39f, 345f, 395f, 5f);
				marker = marker.SetWidth(marker.WH.X * num);
				marker = marker.SetX(marker.XY.X + 395f * (1f - num) * 0.5f);
				Device gs = Engine.GS;
				Rectangle rectangle = marker.Offset(base.Pos.XY).ToRect();
				Color color = Color.Lerp(new Color(41, 84, 32), new Color(66, 84, 128), num);
				gs.Draw(CommonAtlas.whitePixel, rectangle, color);
			}
		}

		// Token: 0x04000973 RID: 2419
		public static readonly Rectangle back = new Rectangle(1826, 1503, 476, 639);

		// Token: 0x04000974 RID: 2420
		public static {19275} CurrentInstance;

		// Token: 0x04000975 RID: 2421
		private static {19275}.Provider provider;

		// Token: 0x04000976 RID: 2422
		private float {19301};

		// Token: 0x04000977 RID: 2423
		private Form {19302};

		// Token: 0x04000978 RID: 2424
		private DropRules {19303};

		// Token: 0x04000979 RID: 2425
		private Tlist<DropTransferPacket.Partition> {19304};

		// Token: 0x0400097A RID: 2426
		public static readonly Rectangle c_mass = new Rectangle(2258, 887, 12, 13);

		// Token: 0x0400097B RID: 2427
		public int DropUID;

		// Token: 0x020001D2 RID: 466
		private class Provider : {18027}.IProvider
		{
			// Token: 0x06000A7E RID: 2686 RVA: 0x00054762 File Offset: 0x00052962
			public Provider(bool {19307}, bool {19308})
			{
				this.{19324} = {19307};
				this.{19325} = {19308};
			}

			// Token: 0x06000A7F RID: 2687 RVA: 0x00054778 File Offset: 0x00052978
			IEnumerable<{18027}.ItemBind> {18027}.IProvider.EnumerateItemsAtShip()
			{
				{19275}.Provider.<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtShip>d__6 <World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtShip>d__ = new {19275}.Provider.<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtShip>d__6(-2);
				<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtShip>d__.<>4__this = this;
				return <World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtShip>d__;
			}

			// Token: 0x06000A80 RID: 2688 RVA: 0x00054788 File Offset: 0x00052988
			IEnumerable<{18027}.ItemBind> {18027}.IProvider.EnumerateItemsAtRight()
			{
				{19275}.Provider.<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtRight>d__7 <World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtRight>d__ = new {19275}.Provider.<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtRight>d__7(-2);
				<World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtRight>d__.<>4__this = this;
				return <World_Of_Sea_Battle-Interface-HoldsUiCommon-IProvider-EnumerateItemsAtRight>d__;
			}

			// Token: 0x06000A81 RID: 2689 RVA: 0x00054798 File Offset: 0x00052998
			void {18027}.IProvider.{19309}()
			{
				if (this.State.IsEmpty)
				{
					return;
				}
				if (this.BuildingHold != null)
				{
					Global.Network.Send(new OnBuildingHoldOperatingMsg(this.BuildingHold.uIDDynamicServerBuild, this.BuildingHold.HoldResources, new GSI(), false));
				}
				else if ({19275}.CurrentInstance != null && {19275}.CurrentInstance.{19303} == DropRules.OnlyEngagedOwnPart && {19275}.CurrentInstance.{19304}.Size >= 2 && EducationHelper.EnableLootCombat)
				{
					bool flag = {19275}.CurrentInstance.{19304}.FirstOrDefault((DropTransferPacket.Partition {19333}) => {19333}.PlayerUID == Global.Player.uID).DamageFactor > 0.1f;
					new {17312}(Local.drop_ui_1, new Action<int>(this.{19322}), new {17443}[]
					{
						new {17443}(Local.drop_ui_2, "", {17312}.cIconShield, false, 10f),
						new {17443}(Session.Account.WorldFlag.IsPeaceMode() ? Local.drop_ui_3_noPeace : Local.drop_ui_3, flag ? Local.drop_ui_3_tt_a : Local.drop_ui_3_tt_b, {17312}.cIconPirateFlag, !flag, 0f)
					});
				}
				else
				{
					Global.Network.Send(new OnGetDropMsg(this.dropUID, null, DropOperation.TryToRemove, this.State, 0f, DropModel.DebrisWithBox, false, false, false, false));
				}
				{19275} currentInstance = {19275}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.LockToReloadCall();
			}

			// Token: 0x06000A82 RID: 2690 RVA: 0x00054928 File Offset: 0x00052B28
			private void {19310}(OnGetDropMsg {19311}, bool {19312})
			{
				if (this.BuildingHold == null)
				{
					if (!{19312})
					{
						throw new InvalidOperationException();
					}
					Global.Network.Send({19311});
				}
				else
				{
					Global.Network.Send(new OnBuildingHoldOperatingMsg(this.BuildingHold.uIDDynamicServerBuild, {19311}.ActualState.Resources, {19311}.ActualState.Ammo, !{19312}));
				}
				{19275} currentInstance = {19275}.CurrentInstance;
				if (currentInstance == null)
				{
					return;
				}
				currentInstance.LockToReloadCall();
			}

			// Token: 0x06000A83 RID: 2691 RVA: 0x00017097 File Offset: 0x00015297
			UiControl {18027}.IProvider.{19313}({18027} {19314}, Marker {19315}, float {19316})
			{
				return null;
			}

			// Token: 0x06000A84 RID: 2692 RVA: 0x000549A0 File Offset: 0x00052BA0
			[CompilerGenerated]
			private int {19317}()
			{
				return this.State.GoldBonus;
			}

			// Token: 0x06000A85 RID: 2693 RVA: 0x000549B0 File Offset: 0x00052BB0
			[CompilerGenerated]
			private void {19318}(int {19319})
			{
				this.{19310}(new OnGetDropMsg(this.dropUID, null, DropOperation.TryToRemove, new DropBonusInfo({19319}, new GSI(), new GSI()), 0f, DropModel.DebrisWithBox, false, false, false, false), true);
			}

			// Token: 0x06000A86 RID: 2694 RVA: 0x000549EC File Offset: 0x00052BEC
			[CompilerGenerated]
			private void {19320}(int {19321})
			{
				this.{19310}(new OnGetDropMsg(this.dropUID, null, DropOperation.TryToRemove, new DropBonusInfo(0, new GSI(), new GSI())
				{
					PowerupItemIDindex = this.State.PowerupItemIDindex
				}, 0f, DropModel.DebrisWithBox, false, false, false, false), true);
			}

			// Token: 0x06000A87 RID: 2695 RVA: 0x00054A3C File Offset: 0x00052C3C
			[CompilerGenerated]
			private void {19322}(int {19323})
			{
				Global.Network.Send(new OnGetDropMsg(this.dropUID, null, DropOperation.TryToRemove, this.State, 0f, DropModel.DebrisWithBox, false, false, false, false)
				{
					CombatMode = ({19323} == 1)
				});
			}

			// Token: 0x0400097C RID: 2428
			public DropBonusInfo State;

			// Token: 0x0400097D RID: 2429
			public PbsBuildingStatus BuildingHold;

			// Token: 0x0400097E RID: 2430
			public int dropUID;

			// Token: 0x0400097F RID: 2431
			private bool {19324};

			// Token: 0x04000980 RID: 2432
			private bool {19325};
		}
	}
}
