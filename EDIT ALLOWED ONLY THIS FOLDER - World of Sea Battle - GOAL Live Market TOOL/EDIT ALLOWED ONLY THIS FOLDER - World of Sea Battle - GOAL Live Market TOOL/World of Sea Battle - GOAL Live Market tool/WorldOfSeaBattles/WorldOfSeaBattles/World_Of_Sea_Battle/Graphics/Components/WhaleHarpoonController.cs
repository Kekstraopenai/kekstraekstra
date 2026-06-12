using System;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics.Effects;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000495 RID: 1173
	internal static class WhaleHarpoonController
	{
		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x000E4817 File Offset: 0x000E2A17
		public static int MaxHarpoonShots
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000E481C File Offset: 0x000E2A1C
		public static void Click(ClientDrop {24295})
		{
			if (WhaleHarpoonController.microCooldown > 0f)
			{
				return;
			}
			WhaleHarpoonController.microCooldown = 300f;
			WhaleHarpoonController.timeSinceLastUse = 0f;
			bool takeItem = Session.WhaleHarpoonShots == 0;
			if (takeItem)
			{
				Session.WhaleHarpoonShots = WhaleHarpoonController.MaxHarpoonShots;
				Session.WReload.BeginReloadingWhaleHooks(Global.Player);
			}
			Session.WhaleHarpoonShots--;
			new WhaleHarpoonFSEffect(Global.Player, {24295}, delegate(bool {24298})
			{
				Global.Network.Send(new OnWhaleHarpoonAttackMsg(Global.Player.uID, {24298} ? {24295}.uID : 0, takeItem));
				if (takeItem)
				{
					GSI resourcesOfHold = Global.Player.ResourcesOfHold;
					int num = resourcesOfHold[36];
					resourcesOfHold[36] = num - 1;
				}
			});
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000E48AF File Offset: 0x000E2AAF
		public static void Update(FrameTime {24296})
		{
			{24296}.EvaluteTimerMs(ref WhaleHarpoonController.microCooldown);
			WhaleHarpoonController.timeSinceLastUse += {24296}.secElapsed;
			if (WhaleHarpoonController.timeSinceLastUse > 40f)
			{
				Session.WhaleHarpoonShots = WhaleHarpoonController.MaxHarpoonShots;
			}
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000E48E4 File Offset: 0x000E2AE4
		public static void DrawSight()
		{
			if (!Global.Game.InteractiveWorldSystem.InteropIsWhaleHarpoon || Global.Game.InteractiveWorldSystem.WhaleNoHarpoonProblem || Session.WReload.BoardingHookReloadingLeftSec > 0f)
			{
				return;
			}
			Global.Render.ItemsShader.RenderWaterDecal(OtherTextures.WhaleHarpoonSight.Tex, Global.Player.Position, new Vector4((0.5f + FXEngine.isNotDark * 0.5f) * 0.66f), 30f, MathF.Atan2(Engine.GS.Camera.Direction.Z, Engine.GS.Camera.Direction.X) + 1.5707964f);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000E499C File Offset: 0x000E2B9C
		public static void DrawSight2D(float {24297})
		{
			if (!Global.Game.InteractiveWorldSystem.InteropIsWhaleHarpoon)
			{
				return;
			}
			Vector3 position3D = Global.Player.Position3D;
			Vector2 vector = Engine.GS.Camera.Direction.XZ;
			Vector3 vector2 = position3D + vector.X0Y * 10f + Vector3.Up * 2f;
			if (Engine.GS.Camera.IsVisible(vector2, 1f))
			{
				Vector2 projectionSmoothed = Engine.GS.Camera.GetProjectionSmoothed(ref vector2);
				if (Session.WReload.BoardingHookReloadingLeftSec > 0f)
				{
					CannonsController.DrawSightCircle(projectionSmoothed, 0.35f, Color.White * {24297}, 1f - Session.WReload.BoardingHookReloadingLeftSec / Session.WReload.BoardingHookReloadingMaxSec, true);
				}
				else
				{
					CannonsController.DrawSightCircle(projectionSmoothed, 0.35f, Color.White * {24297}, 0f, false);
				}
				Vector2 value = projectionSmoothed + new Vector2(30f, -10f);
				Engine.GS.SetFont(Fonts.Philosopher_14Bold);
				Device gs = Engine.GS;
				string {14602} = Global.Player.ResourcesOfHold[36].ToString() + " " + Gameplay.ItemsInfo.FromID(36).Name.ToLower();
				Color lightCyan = Color.LightCyan;
				gs.DrawStringShadowed({14602}, value, lightCyan);
				if (Session.WReload.BoardingHookReloadingLeftSec == 0f)
				{
					for (int i = 0; i < Session.WhaleHarpoonShots + 1; i++)
					{
						Device gs2 = Engine.GS;
						Rectangle rectangle = new Rectangle(205, 176, 18, 18);
						vector = value + new Vector2((float)(i * 12), 22f);
						Rectangle rectangle2 = new Marker(ref vector, 11f, 11f).ToRect();
						gs2.Draw(rectangle, rectangle2);
					}
				}
			}
		}

		// Token: 0x04001814 RID: 6164
		private static float microCooldown;

		// Token: 0x04001815 RID: 6165
		private static float timeSinceLastUse;
	}
}
