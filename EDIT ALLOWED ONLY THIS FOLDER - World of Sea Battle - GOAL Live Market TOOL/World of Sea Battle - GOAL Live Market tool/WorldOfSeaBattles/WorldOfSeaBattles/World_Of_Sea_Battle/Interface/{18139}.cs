using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Graphics.Shaders;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200010B RID: 267
	internal sealed class {18139} : CustomUi
	{
		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x00030D4C File Offset: 0x0002EF4C
		private int CountOfUsedProtection
		{
			get
			{
				return this.{18168}.MyProtectionSetted.Count(new Func<byte, bool>(this.{18154}));
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000648 RID: 1608 RVA: 0x00030D6A File Offset: 0x0002EF6A
		public int OpponentUid
		{
			get
			{
				return this.{18167};
			}
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00030D72 File Offset: 0x0002EF72
		public static bool IsInBoardingUi(int {18141})
		{
			return {18139}.CurrentInstance != null && ({18139}.CurrentInstance.{18167} == {18141} || Global.Player.uID == {18141});
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00030D9C File Offset: 0x0002EF9C
		public {18139}(Ship {18142}) : base(false)
		{
			{18139} <>4__this = this;
			Global.Camera.CameraEffects.RunFocusEffect(new CameraFocusEffect(delegate()
			{
				if ({18139}.CurrentInstance != null && {18142}.InstanceAlive)
				{
					return new Vector3?(({18142}.Position + Global.Player.Position).X0Y() / 2f);
				}
				return null;
			}, 0.5f, 0f));
			{18139}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{18139}.CurrentInstance = null;
			};
			this.{18167} = {18142}.uID;
			this.{18168} = new BoardingCombatInstance(Global.Player);
			this.{18169} = new BoardingCombatInstance({18142});
			this.{18174} = Global.Player.UsedShipPlayer.CraftFrom.BoardingProtectedGroupsLimit;
			BoardingCombatInstance.Configure(Global.Player, this.{18168}, this.{18169});
			BoardingCombatInstance.Configure({18142}, this.{18169}, this.{18168});
			this.{18170} = new Dictionary<byte, CheckboxControl>(500);
			this.{18171} = new Dictionary<byte, CheckboxControl>(500);
			using (IEnumerator<BoardingCombatVisualGroup> enumerator = ((IEnumerable<BoardingCombatVisualGroup>)this.{18168}.VisualGroups).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BoardingCombatVisualGroup item = enumerator.Current;
					CheckboxControl checkboxControl = new CheckboxControl(Vector2.Zero, {18139}.c_protectChecked, {18139}.c_protectUnchecked, this.{18168}.MyProtectionSetted.Contains(item.VisualGroupIndex), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					this.{18171}.Add(item.VisualGroupIndex, checkboxControl);
					checkboxControl.EvCheck += delegate(CheckboxCheckedEventArgs {18183})
					{
						if (<>4__this.{18173})
						{
							return;
						}
						if ({18183}.NewValue && <>4__this.CountOfUsedProtection >= <>4__this.{18174})
						{
							{18183}.UndoCheck = true;
							return;
						}
						Tlist<byte> tlist = <>4__this.{18168}.MyProtectionSetted.Clone();
						if ({18183}.NewValue)
						{
							tlist.AddIfNotContains(item.VisualGroupIndex);
						}
						else
						{
							tlist.Remove(item.VisualGroupIndex);
						}
						OnBoardingResightingOrProtectMsg onBoardingResightingOrProtectMsg = new OnBoardingResightingOrProtectMsg(null, tlist.ToArray());
						<>4__this.{18168}.Resight(onBoardingResightingOrProtectMsg);
						Global.Network.Send(onBoardingResightingOrProtectMsg);
						if (<>4__this.{18178})
						{
							<>4__this.{18179} = 3000f;
						}
						Marker pos = {18183}.Sender.Pos;
						new UiMarkerAnimation({18183}.Sender, {18183}.Sender.Pos.Scale(1.2f), 100f);
						new UiMarkerAnimation({18183}.Sender, pos, 100f);
					};
					base.AddChild(checkboxControl);
				}
			}
			using (IEnumerator<BoardingCombatVisualGroup> enumerator = ((IEnumerable<BoardingCombatVisualGroup>)this.{18169}.VisualGroups).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					BoardingCombatVisualGroup item = enumerator.Current;
					CheckboxControl checkboxControl2 = new CheckboxControl(Vector2.Zero, {18139}.c_focusChecked, {18139}.c_focusUnchecked, this.{18168}.EnemyTargetsSetted.Contains(item.VisualGroupIndex), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					this.{18170}.Add(item.VisualGroupIndex, checkboxControl2);
					checkboxControl2.EvCheck += delegate(CheckboxCheckedEventArgs {18184})
					{
						if (<>4__this.{18173})
						{
							return;
						}
						Tlist<byte> tlist = <>4__this.{18168}.EnemyTargetsSetted.Clone();
						if ({18184}.NewValue)
						{
							tlist.AddIfNotContains(item.VisualGroupIndex);
						}
						else
						{
							tlist.Remove(item.VisualGroupIndex);
						}
						OnBoardingResightingOrProtectMsg onBoardingResightingOrProtectMsg = new OnBoardingResightingOrProtectMsg(tlist.ToArray(), null);
						<>4__this.{18168}.Resight(onBoardingResightingOrProtectMsg);
						Global.Network.Send(onBoardingResightingOrProtectMsg);
						if (<>4__this.{18178})
						{
							<>4__this.{18179} = 3000f;
						}
						Marker pos = {18184}.Sender.Pos;
						new UiMarkerAnimation({18184}.Sender, {18184}.Sender.Pos.Scale(1.2f), 100f);
						new UiMarkerAnimation({18184}.Sender, pos, 100f);
					};
					base.AddChild(checkboxControl2);
				}
			}
			this.{18145}(true);
			Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID(this.{18167});
			this.{18175} = new {18187}(Global.Player, this.{18168}, Global.Player.Client.Scene.CrewInstancesByPlaces);
			this.{18176} = new {18187}(shipFromUID, this.{18169}, (shipFromUID == null) ? null : ((IClientShip)shipFromUID).GetClient.Scene.CrewInstancesByPlaces);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x000310A0 File Offset: 0x0002F2A0
		public void SwitchToWaitActionMode(bool {18143})
		{
			{18945}.CloseExisted();
			this.{18172} = {18945}.TryShowNotif(({18143} ? Local.BattleResultsWindow_0 : Local.BattleResultsWindow_1) + "...", null, new float?((float)10000));
			base.RemoveWithThis(new UiControl[]
			{
				this.{18172}
			});
			new UiOpacityAnimation(this, 0f, 500f);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00031108 File Offset: 0x0002F308
		public void EnemyStatusChange(OnBoardingResightingOrProtectMsg {18144})
		{
			this.{18169}.Resight({18144});
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x00031118 File Offset: 0x0002F318
		private void {18145}(bool {18146})
		{
			this.{18177} = {18146};
			{18945}.CloseExisted();
			this.{18172} = {18945}.TryShowNotif({18146} ? Local.boardingPreparation : Local.boardingCombat, "", new float?({18146} ? 4100f : WosbBoarding.DurationMs));
			base.RemoveWithThis(new UiControl[]
			{
				this.{18172}
			});
			this.{18172}.MoveToBackLevel();
			if ({18146})
			{
				this.{18172}.OnTimeout += this.{18156};
			}
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000311A0 File Offset: 0x0002F3A0
		protected override void UserUpdate(ref FrameTime {18147})
		{
			if ({18147}.EvaluteTimerMs2(ref this.{18179}))
			{
				Global.Settings.BoardingEducationStatus++;
			}
			foreach (KeyValuePair<byte, CheckboxControl> keyValuePair in this.{18170})
			{
				keyValuePair.Value.IsVisible = !this.{18177};
			}
			foreach (KeyValuePair<byte, CheckboxControl> keyValuePair2 in this.{18171})
			{
				keyValuePair2.Value.IsVisible = !this.{18177};
			}
			if (this.{18168}.EnemyTargetsSetted.Size > 0 && this.{18168}.EnemyTargetsSetted.All(new Func<byte, bool>(this.{18157})))
			{
				BoardingCombatVisualGroup group = this.{18169}.VisualGroups.FirstOrDefault((BoardingCombatVisualGroup {18180}) => {18180}.SummaryHp != 0f);
				if (group != null)
				{
					KeyValuePair<byte, CheckboxControl> keyValuePair3 = this.{18170}.FirstOrDefault((KeyValuePair<byte, CheckboxControl> {18186}) => {18186}.Key == group.VisualGroupIndex);
					if (keyValuePair3.Value != null)
					{
						keyValuePair3.Value.IsChecked = true;
					}
				}
			}
			foreach (KeyValuePair<byte, CheckboxControl> keyValuePair4 in this.{18170})
			{
				bool flag = this.{18168}.EnemyTargetsSetted.Contains(keyValuePair4.Key);
				if (flag != keyValuePair4.Value.IsChecked)
				{
					this.{18173} = true;
					keyValuePair4.Value.IsChecked = flag;
					this.{18173} = false;
				}
			}
			this.{18175}.UpdateHpBar(ref {18147});
			this.{18176}.UpdateHpBar(ref {18147});
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000313B0 File Offset: 0x0002F5B0
		protected override void UserBackRender()
		{
			{18139}.sortedRenderQuery.Clear();
			this.{18175}.PrepareDraw({18139}.sortedRenderQuery);
			this.{18176}.PrepareDraw({18139}.sortedRenderQuery);
			{18139}.sortedRenderQuery.SortTop(({18187}.VisualGroupConnection {18181}) => Vector3.Distance({18181}.ComputedPosition3D, Engine.GS.Camera.Position));
			foreach ({18187}.VisualGroupConnection visualGroupConnection in ((IEnumerable<{18187}.VisualGroupConnection>){18139}.sortedRenderQuery))
			{
				visualGroupConnection.Draw((visualGroupConnection.Parent == this.{18175}) ? HealthBarStyle.Lime : HealthBarStyle.Red, (visualGroupConnection.Parent == this.{18175}) ? this.{18171} : this.{18170});
			}
			int countOfUsedProtection = this.CountOfUsedProtection;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted<int>(countOfUsedProtection);
			defaultInterpolatedStringHandler.AppendLiteral("/");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.{18174});
			string text = defaultInterpolatedStringHandler.ToStringAndClear();
			float num = (float)countOfUsedProtection / (float)this.{18174};
			Engine.GS.SetFont(Fonts.Arial_12);
			foreach (KeyValuePair<byte, CheckboxControl> keyValuePair in this.{18171})
			{
				if (keyValuePair.Value.IsVisible)
				{
					if (keyValuePair.Value.IsChecked)
					{
						Device gs = Engine.GS;
						string {14590} = text;
						Vector2 vector = new Vector2(keyValuePair.Value.Pos.Center.X, keyValuePair.Value.Pos.End.Y + 10f);
						Color color = Color.Lerp(Color.Lime, Color.White, 0.5f);
						gs.DrawStringCenteredShadow({14590}, vector, color, 0.8f);
						keyValuePair.Value.Opacity = 1f;
					}
					else
					{
						keyValuePair.Value.Opacity = ((num == 1f) ? 0f : (1f - num * 0.4f));
					}
				}
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x000315DC File Offset: 0x0002F7DC
		public void ApplyAndDisplayChanges(OnBoardingRealTimeChangesMsg {18148})
		{
			int totalItemsCount = Global.Player.UsedShip.Crew.HurtedCrew.GetTotalItemsCount();
			int num = this.{18168}.CommonApplyDamage({18148}.myCrewActions.DamageAndKills, Global.Player.UsedShip.Crew, null, {18148}.serverRandomValue);
			int totalItemsCount2 = Global.Player.UsedShip.Crew.HurtedCrew.GetTotalItemsCount();
			Session.DeadCrewCounter += num - Math.Max(0, totalItemsCount2 - totalItemsCount);
			Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID(this.{18167});
			if (shipFromUID != null)
			{
				this.{18169}.CommonApplyDamage({18148}.enemyCrewActions.DamageAndKills, shipFromUID.UsedShip.Crew, null, {18148}.serverRandomValue);
				foreach (BoardingCombatBatllePass boardingCombatBatllePass in ((IEnumerable<BoardingCombatBatllePass>){18148}.myCrewActions.DamageAndKills))
				{
					this.{18149}((int)boardingCombatBatllePass.SourceVisualGroup, (int)boardingCombatBatllePass.TatgetVisualGroup, shipFromUID, Global.Player);
				}
				foreach (BoardingCombatBatllePass boardingCombatBatllePass2 in ((IEnumerable<BoardingCombatBatllePass>){18148}.enemyCrewActions.DamageAndKills))
				{
					this.{18149}((int)boardingCombatBatllePass2.SourceVisualGroup, (int)boardingCombatBatllePass2.TatgetVisualGroup, Global.Player, shipFromUID);
				}
			}
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00031760 File Offset: 0x0002F960
		private void {18149}(int {18150}, int {18151}, Ship {18152}, Ship {18153})
		{
			if ({18150} < this.{18175}.orderedPositions.OrderedNonNullPlaces.Size && {18151} < this.{18176}.orderedPositions.OrderedNonNullPlaces.Size)
			{
				UnitScene crewOrNull = this.{18175}.orderedPositions.OrderedNonNullPlaces.Array[{18150}].CrewOrNull;
				UnitScene crewOrNull2 = this.{18176}.orderedPositions.OrderedNonNullPlaces.Array[{18151}].CrewOrNull;
				if (crewOrNull != null && crewOrNull2 != null && crewOrNull.Role == UnitRole.Musketeer && !crewOrNull.IsDeath)
				{
					Vector3 {11453} = crewOrNull2.WorldBodyCenter - crewOrNull.WorldBodyCenter;
					crewOrNull.GunEffect({11453}.XZNormal(), {18152});
				}
			}
			CrewConnector.Approximator approximator = ({18153} == Global.Player) ? this.{18175}.orderedPositions : this.{18176}.orderedPositions;
			if ({18151} < approximator.OrderedNonNullPlaces.Size)
			{
				UnitScene crewOrNull3 = approximator.OrderedNonNullPlaces.Array[{18151}].CrewOrNull;
				if (crewOrNull3 != null && !crewOrNull3.IsDeath)
				{
					FXEngine.HitCrewBoardingDamage(crewOrNull3.WorldBodyCenter);
				}
			}
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00031870 File Offset: 0x0002FA70
		protected override void UserFrontRender()
		{
			if (this.{18179} == 0f)
			{
				if (Global.Settings.BoardingEducationStatus == 0)
				{
					{18187}.VisualGroupConnection visualGroupConnection = this.{18176}.groupsVisualizerMy.LastOrDefault(new Func<{18187}.VisualGroupConnection, bool>(this.{18161}));
					if (visualGroupConnection != null)
					{
						this.{18178} = true;
						{18139}.<UserFrontRender>g__DrawToolTip|41_0(visualGroupConnection.ComputedPosition3D, Local.boarding_education_1);
					}
				}
				if (Global.Settings.BoardingEducationStatus == 1)
				{
					{18187}.VisualGroupConnection visualGroupConnection2 = this.{18175}.groupsVisualizerMy.LastOrDefault(new Func<{18187}.VisualGroupConnection, bool>(this.{18163}));
					if (visualGroupConnection2 != null)
					{
						this.{18178} = true;
						{18139}.<UserFrontRender>g__DrawToolTip|41_0(visualGroupConnection2.ComputedPosition3D, Local.boarding_education_2);
					}
				}
				if (Global.Settings.BoardingEducationStatus == 2)
				{
					{18187}.VisualGroupConnection visualGroupConnection3 = this.{18176}.groupsVisualizerMy.LastOrDefault(new Func<{18187}.VisualGroupConnection, bool>(this.{18165}));
					if (visualGroupConnection3 != null)
					{
						this.{18178} = true;
						{18139}.<UserFrontRender>g__DrawToolTip|41_0(visualGroupConnection3.ComputedPosition3D, Local.boarding_education_3);
					}
				}
			}
			Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID(this.{18167});
			Color color;
			Vector2 vector;
			if (this.{18172} != null && shipFromUID != null)
			{
				float opcaity = base.GetOpcaity();
				float num = 60f * this.{18172}.scale;
				Vector2 value = new Vector2((float)(Engine.GS.UIArea.Width / 2 - {18139}.c_progressStateBarRight.Width / 2), this.{18172}.Pos.XY.Y + num);
				Device gs = Engine.GS;
				color = Color.White * opcaity;
				gs.Draw({18139}.c_progressStateBarRight, value, color);
				float num2 = (float)Global.Player.UsedShip.Crew.Count;
				float num3 = (float)shipFromUID.UsedShip.Crew.Count;
				float num4 = num3 - num2;
				float num5 = (num4 < 0f) ? (num4 / (num2 / 1.5f)) : ((num4 > 0f) ? (num4 / (num3 / 1.5f)) : 0f);
				float num6 = 1f - Geometry.Saturate(num5 * 0.5f + 0.5f);
				Rectangle rectangle = {18139}.c_progressStateBarLeft;
				rectangle.Width = (int)((float)rectangle.Width * num6);
				Device gs2 = Engine.GS;
				color = Color.White * opcaity;
				gs2.Draw(rectangle, value, color);
				Device gs3 = Engine.GS;
				string {14599} = num2.ToString();
				vector = value + new Vector2(-25f, -1f);
				color = new Color(167, 255, 133);
				gs3.DrawString({14599}, vector, color);
				Device gs4 = Engine.GS;
				string {14599}2 = num3.ToString();
				vector = value + new Vector2(5f + (float){18139}.c_progressStateBarLeft.Width, -1f);
				color = new Color(255, 103, 106);
				gs4.DrawString({14599}2, vector, color);
			}
			Vector2 value2 = new Vector2((float)(Engine.GS.UIArea.Width / 2 - {18139}.c_tt_back.Width / 2), (float)(Engine.GS.UIArea.Height - {18139}.c_tt_back.Height - 7));
			Engine.GS.Draw({18139}.c_tt_back, value2);
			Engine.GS.SetFont(Fonts.Philosopher_14);
			Device gs5 = Engine.GS;
			string boarding_tt = Local.boarding_tt1;
			vector = value2 + new Vector2(65f, 11f);
			color = new Color(255, 189, 165);
			gs5.DrawString(boarding_tt, vector, color);
			Device gs6 = Engine.GS;
			string boarding_tt2 = Local.boarding_tt2;
			vector = value2 + new Vector2(233f, 11f);
			color = new Color(209, 255, 211);
			gs6.DrawString(boarding_tt2, vector, color);
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x00031C22 File Offset: 0x0002FE22
		public void Close()
		{
			base.AllowMouseInput = false;
			if (base.Opacity != 0f)
			{
				new UiOpacityAnimation(this, 1f, 0f, 300f);
			}
			new UiRemoveAction(this);
			{18139}.CurrentInstance = null;
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x00031DA4 File Offset: 0x0002FFA4
		[CompilerGenerated]
		private bool {18154}(byte {18155})
		{
			BoardingCombatVisualGroup boardingCombatVisualGroup = this.{18168}.VisualGroups.FirstOrDefault((BoardingCombatVisualGroup {18182}) => {18182}.VisualGroupIndex == {18155});
			return ((boardingCombatVisualGroup != null) ? boardingCombatVisualGroup.HealthFactor : 0f) > 0f;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00031DF1 File Offset: 0x0002FFF1
		[CompilerGenerated]
		private void {18156}()
		{
			this.{18145}(false);
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00031DFC File Offset: 0x0002FFFC
		[CompilerGenerated]
		private bool {18157}(byte {18158})
		{
			return this.{18169}.VisualGroups.Find((BoardingCombatVisualGroup {18185}) => {18185}.VisualGroupIndex == {18158}).SummaryHp == 0f;
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00031E40 File Offset: 0x00030040
		[CompilerGenerated]
		internal static void <UserFrontRender>g__DrawToolTip|41_0(Vector3 {18159}, string {18160})
		{
			string[] array = {18160}.Split('$', StringSplitOptions.None);
			Vector2 vector = Global.Camera.GetProjection({18159}) + new Vector2(10f, (float)(-(float)(40 + array.Length * 15)));
			Device gs = Engine.GS;
			Rectangle rectangle = new Rectangle(2377, 71, 201, 135);
			Rectangle rectangle2 = new Marker(vector.X, vector.Y, 310f, (float)(20 + array.Length * 15)).ToRect();
			gs.Draw(rectangle, rectangle2);
			Engine.GS.SetFont(Fonts.Philosopher_14);
			foreach (string text in array)
			{
				Device gs2 = Engine.GS;
				string {14599} = text;
				Vector2 vector2 = vector + new Vector2(8f, 5f);
				Color white = Color.White;
				gs2.DrawString({14599}, vector2, white);
				vector.Y += 15f;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00031F36 File Offset: 0x00030136
		[CompilerGenerated]
		private bool {18161}({18187}.VisualGroupConnection {18162})
		{
			return this.{18168}.EnemyTargetsSetted.Contains({18162}.Group.VisualGroupIndex);
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00031F53 File Offset: 0x00030153
		[CompilerGenerated]
		private bool {18163}({18187}.VisualGroupConnection {18164})
		{
			return !this.{18168}.MyProtectionSetted.Contains({18164}.Group.VisualGroupIndex);
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00031F73 File Offset: 0x00030173
		[CompilerGenerated]
		private bool {18165}({18187}.VisualGroupConnection {18166})
		{
			return !this.{18168}.EnemyTargetsSetted.Contains({18166}.Group.VisualGroupIndex);
		}

		// Token: 0x0400059D RID: 1437
		public static readonly Rectangle c_tt_back = new Rectangle(2579, 106, 367, 44);

		// Token: 0x0400059E RID: 1438
		public static readonly Rectangle c_focusUnchecked = new Rectangle(1660, 64, 44, 45);

		// Token: 0x0400059F RID: 1439
		public static readonly Rectangle c_focusChecked = new Rectangle(1705, 64, 44, 45);

		// Token: 0x040005A0 RID: 1440
		public static readonly Rectangle c_protectUnchecked = new Rectangle(1660, 172, 44, 45);

		// Token: 0x040005A1 RID: 1441
		public static readonly Rectangle c_protectChecked = new Rectangle(1705, 172, 44, 45);

		// Token: 0x040005A2 RID: 1442
		public static readonly Rectangle c_musketIcon = new Rectangle(1662, 112, 65, 28);

		// Token: 0x040005A3 RID: 1443
		public static readonly Rectangle c_musketIconReloading = new Rectangle(1732, 111, 65, 28);

		// Token: 0x040005A4 RID: 1444
		public static readonly Rectangle c_warriorIcon = new Rectangle(1662, 141, 65, 28);

		// Token: 0x040005A5 RID: 1445
		public static readonly Rectangle c_warriorIconReloading = new Rectangle(1732, 141, 65, 28);

		// Token: 0x040005A6 RID: 1446
		public static readonly Rectangle c_sailorIcon = new Rectangle(1582, 179, 76, 35);

		// Token: 0x040005A7 RID: 1447
		public static readonly Rectangle c_progressStateBarLeft = new Rectangle(1211, 140, 337, 14);

		// Token: 0x040005A8 RID: 1448
		public static readonly Rectangle c_progressStateBarRight = new Rectangle(1211, 125, 337, 14);

		// Token: 0x040005A9 RID: 1449
		public static readonly Rectangle c_progressStatePoint = new Rectangle(1423, 74, 49, 49);

		// Token: 0x040005AA RID: 1450
		public static {18139} CurrentInstance;

		// Token: 0x040005AB RID: 1451
		public static Tlist<{18187}.VisualGroupConnection> sortedRenderQuery = new Tlist<{18187}.VisualGroupConnection>(100);

		// Token: 0x040005AC RID: 1452
		private int {18167};

		// Token: 0x040005AD RID: 1453
		private BoardingCombatInstance {18168};

		// Token: 0x040005AE RID: 1454
		private BoardingCombatInstance {18169};

		// Token: 0x040005AF RID: 1455
		private Dictionary<byte, CheckboxControl> {18170};

		// Token: 0x040005B0 RID: 1456
		private Dictionary<byte, CheckboxControl> {18171};

		// Token: 0x040005B1 RID: 1457
		private {18945} {18172};

		// Token: 0x040005B2 RID: 1458
		private bool {18173};

		// Token: 0x040005B3 RID: 1459
		private readonly int {18174};

		// Token: 0x040005B4 RID: 1460
		private {18187} {18175};

		// Token: 0x040005B5 RID: 1461
		private {18187} {18176};

		// Token: 0x040005B6 RID: 1462
		private bool {18177};

		// Token: 0x040005B7 RID: 1463
		private bool {18178};

		// Token: 0x040005B8 RID: 1464
		private float {18179};
	}
}
