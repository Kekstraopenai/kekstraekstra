using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using World_Of_Sea_Battle.Components.Client;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000B2 RID: 178
	internal sealed class {17539} : CustomUi
	{
		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x00023C93 File Offset: 0x00021E93
		public static {17539} CurrentInstance
		{
			get
			{
				return {17539}.currentInstance;
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x00023C9C File Offset: 0x00021E9C
		public {17539}() : base(false)
		{
			if ({17539}.currentInstance != null)
			{
				throw new InvalidOperationException();
			}
			{17539}.currentInstance = this;
			Global.Game.SceneGame.IncreaseMouse();
			GameScene.IncreaseGameInput();
			base.EvRemoveFromContainer += delegate()
			{
				{17539}.currentInstance = null;
				Global.Game.SceneGame.DecreaseMouse();
				GameScene.DecreaseGameInput();
			};
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x00023D08 File Offset: 0x00021F08
		protected override void UserUpdate(ref FrameTime {17540})
		{
			if (Global.Settings.kb_SendGroupCommand.IsDown && !InputHelper.NowMouseState.LeftPressed)
			{
				this.{17546} = null;
				for (int i = 0; i < this.{17545}.Count; i++)
				{
					Rectangle rectangle = this.{17545}[i];
					Vector2 mouseToUI = Engine.GS.MouseToUI;
					if (rectangle.Intersects(new Marker(ref mouseToUI, 0f, 0f).Border(5f).ToRect()))
					{
						this.{17546} = new GroupCommandId?({17539}.c_groupCommands[i]);
						return;
					}
				}
				return;
			}
			if (this.{17546} == null && InputHelper.NowMouseState.LeftPressed)
			{
				return;
			}
			if (this.{17546} != null)
			{
				Ship shipInSight = Global.Game.InterestPoints.ShipInSight;
				NetworkManager network = Global.Network;
				GroupCommandId value = this.{17546}.Value;
				int uID = Global.Player.uID;
				string empty = string.Empty;
				int {9266} = (shipInSight != null) ? shipInSight.uID : -1;
				IClientShip clientShip = (IClientShip)shipInSight;
				network.Send(new OnSendGroupCommandMsg(value, uID, empty, {9266}, ((clientShip != null) ? clientShip.GetClient.GetName2() : null) ?? ""));
			}
			base.RemoveFromContainer();
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x00023E4C File Offset: 0x0002204C
		protected override void UserFrontRender()
		{
			Device gs = Engine.GS;
			Vector2 vector = Engine.GS.UIArea.HalfWidthHeightInt();
			Vector2 vector2 = new Vector2(50f);
			float {14558} = 0f;
			float {14559} = 8.5f;
			Color color = Color.White;
			gs.Draw({17539}.c_backShadow, vector, vector2, {14558}, {14559}, color);
			Engine.GS.SetFont(Fonts.Philosopher_14Bold);
			Rectangle rectangle = new Rectangle(3034, 0, 70, 60);
			Rectangle rectangle2 = new Rectangle(2963, 0, 70, 60);
			{17539}.<UserFrontRender>g__DrawTextWithIcon|10_0(rectangle, Local.gc_hint1, -20);
			{17539}.<UserFrontRender>g__DrawTextWithIcon|10_0(rectangle2, Local.gc_hint2, 20);
			Engine.GS.SetFont(Fonts.Philosopher_14);
			bool flag = Session.LastMinimapAndGroupUpdate.allies.Any((AllyStateTransfer {17547}) => {17547}.IsOneGroup && {17547}.ShipClass == byte.MaxValue && {17547}.uID != -1);
			int num = 0;
			this.{17545}.Clear();
			foreach (GroupCommandId groupCommandId in {17539}.c_groupCommands)
			{
				GroupCommandInfo groupCommandInfo = new GroupCommandInfo(groupCommandId);
				string displayName = groupCommandInfo.DisplayName;
				Ship ship = groupCommandInfo.HasTarget ? {17539}.GetValidTarget(groupCommandId) : null;
				IClientShip clientShip = (IClientShip)ship;
				string text = (clientShip != null) ? clientShip.GetClient.GetName2() : null;
				string text2 = "";
				Color value = Color.Gray;
				float scale = 1f;
				if (groupCommandInfo.NpcCommand && !flag)
				{
					scale = 0.5f;
				}
				if (!groupCommandInfo.AbleToUse(Global.Player))
				{
					text2 = Local.not_available;
				}
				else if (groupCommandInfo.HasTarget)
				{
					if (ship == null)
					{
						text2 = Local.HoldsUiCommon_no_target;
					}
					else if (!groupCommandInfo.IsTargetClose(Global.Player, ship))
					{
						text2 = Local.HoldsUiCommon_too_far;
					}
					else
					{
						text2 = text;
						value = Color.Orange;
					}
				}
				int num2 = {17539}.c_groupCommands.Length + 1;
				if (num == num2 / 2)
				{
					num++;
				}
				Vector2 vector3 = Geometry.SubstructRotate(6.2831855f * (float)num / (float)num2 - 1.5707964f, 290f);
				if (vector3.X * vector3.X < 0.01f)
				{
					vector3.Y *= 1.1f;
				}
				Vector2 value2 = Engine.GS.UIArea.HalfWidthHeight() + vector3;
				Geometry.IntVector(ref value2);
				Vector2 vector4 = Fonts.Philosopher_14.Measure(displayName);
				Vector2 vector5 = Fonts.Philosopher_14.Measure(text2);
				GroupCommandId? groupCommandId2 = this.{17546};
				GroupCommandId groupCommandId3 = groupCommandId;
				Color value3 = (groupCommandId2.GetValueOrDefault() == groupCommandId3 & groupCommandId2 != null) ? (InputHelper.NowMouseState.LeftPressed ? Color.Gold : Color.SkyBlue) : Color.White;
				value3 *= scale;
				float num3 = vector4.X + vector5.X;
				Rectangle rectangle3 = (num3 > 150f) ? new Rectangle(2963, 62, 213, 38) : new Rectangle(2963, 101, 190, 38);
				Rectangle rectangle4 = groupCommandInfo.NpcCommand ? rectangle2 : rectangle;
				Vector2 value4;
				if (vector3.X * vector3.X < 0.1f)
				{
					value4 = value2 - new Vector2(num3 * 0.5f, vector4.Y * 0.5f);
				}
				else if (vector3.X > 0f)
				{
					value4 = value2 - new Vector2(num3 * 0.2f, vector4.Y * 0.5f);
				}
				else
				{
					value4 = value2 - new Vector2(num3 * 0.8f, vector4.Y * 0.5f);
				}
				List<Rectangle> list = this.{17545};
				Marker marker = new Marker(ref value4, ref rectangle3).ScaleSize(2f);
				vector = new Vector2(num3 / 2f, 10f);
				marker = marker.Offset(vector);
				vector2 = -rectangle3.WidthHeight();
				list.Add(marker.Offset(vector2).Border(-20f, -10f).ToRect());
				Device gs2 = Engine.GS;
				vector = value4 + new Vector2(num3 / 2f, 10f);
				vector2 = new Vector2((float)(rectangle3.Width / 2), (float)(rectangle3.Height / 2));
				float {14558}2 = 0f;
				float {14559}2 = 2f;
				color = Color.White * scale;
				gs2.Draw(rectangle3, vector, vector2, {14558}2, {14559}2, color);
				groupCommandId2 = this.{17546};
				groupCommandId3 = groupCommandId;
				if (groupCommandId2.GetValueOrDefault() == groupCommandId3 & groupCommandId2 != null)
				{
					Device gs3 = Engine.GS;
					Rectangle rectangle5 = new Rectangle(2963, 140, 217, 38);
					vector = value4 + new Vector2(num3 / 2f, 10f);
					vector2 = new Vector2((float)(rectangle3.Width / 2), (float)(rectangle3.Height / 2));
					float {14558}3 = 0f;
					float {14559}3 = 2f;
					color = Color.White * scale;
					gs3.Draw(rectangle5, vector, vector2, {14558}3, {14559}3, color);
				}
				Engine.GS.DrawStringShadowed(displayName, value4, value3);
				if (text2.Length > 0)
				{
					Device gs4 = Engine.GS;
					string {14602} = text2;
					vector = value4 + new Vector2(vector4.X, 0f);
					color = value * scale;
					gs4.DrawStringShadowed({14602}, vector, color);
				}
				Device gs5 = Engine.GS;
				vector = value4 + new Vector2(num3 / 2f - (float)rectangle3.Width + 20f, -34f);
				color = Color.White * scale;
				gs5.Draw(rectangle4, vector, color);
				num++;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x000243E4 File Offset: 0x000225E4
		private static Ship GetValidTarget(GroupCommandId {17541})
		{
			Ship shipInSight = Global.Game.InterestPoints.ShipInSight;
			if (shipInSight == null)
			{
				return null;
			}
			if ({17541} != GroupCommandId.InviteGroup)
			{
				ShipOtherPlayer shipOtherPlayer = shipInSight as ShipOtherPlayer;
				if (shipOtherPlayer == null || !shipOtherPlayer.MakeTransparentForMe)
				{
					ShipNpc shipNpc = shipInSight as ShipNpc;
					if (shipNpc == null || !shipNpc.MakeTransparentForMe)
					{
						return shipInSight;
					}
				}
				return null;
			}
			if (shipInSight is Npc)
			{
				return null;
			}
			return shipInSight;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x00024490 File Offset: 0x00022690
		[CompilerGenerated]
		internal static void <UserFrontRender>g__DrawTextWithIcon|10_0(Rectangle {17542}, string {17543}, int {17544})
		{
			Vector2 vector = Fonts.Philosopher_14Bold.Measure({17543});
			Vector2 value = Engine.GS.UIArea.HalfWidthHeight() + new Vector2(0f, (float){17544});
			float num = 0.8f;
			Device gs = Engine.GS;
			Vector2 vector2 = value + new Vector2((float)(-(float){17542}.Width) * num / 2f - vector.X / 2f, 0f);
			Vector2 vector3 = new Vector2(35f, 30f);
			gs.Draw({17542}, vector2, vector3, 0f, num);
			Device gs2 = Engine.GS;
			Color white = Color.White;
			gs2.DrawStringCenteredShadow({17543}, value, white, 0.8f);
		}

		// Token: 0x040003D0 RID: 976
		private static readonly Rectangle c_backShadow = new Rectangle(2862, 0, 100, 100);

		// Token: 0x040003D1 RID: 977
		private static readonly GroupCommandId[] c_groupCommands = (from {17548} in (GroupCommandId[])Enum.GetValues(typeof(GroupCommandId))
		where {17548} != GroupCommandId.Special_EnteringPassingMap
		select {17548}).ToArray<GroupCommandId>();

		// Token: 0x040003D2 RID: 978
		private static {17539} currentInstance;

		// Token: 0x040003D3 RID: 979
		private readonly List<Rectangle> {17545} = new List<Rectangle>();

		// Token: 0x040003D4 RID: 980
		private GroupCommandId? {17546};
	}
}
