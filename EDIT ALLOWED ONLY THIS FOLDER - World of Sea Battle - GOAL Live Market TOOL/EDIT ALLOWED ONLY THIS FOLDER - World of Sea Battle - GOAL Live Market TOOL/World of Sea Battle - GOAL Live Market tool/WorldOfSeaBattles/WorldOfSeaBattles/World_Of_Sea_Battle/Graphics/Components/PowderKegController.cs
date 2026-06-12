using System;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Helpers;
using TheraEngine.Input;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000493 RID: 1171
	internal class PowderKegController : IInGameSightUI
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060019AE RID: 6574 RVA: 0x000E4174 File Offset: 0x000E2374
		public float ConvertedTimig
		{
			get
			{
				if (Session.SelectedPowderKegsInfo.IsInvisibleMine)
				{
					return (float)(120000 + ((Session.Account.CaptainSkills[PDynamicAccountBonus.BAllowPowderKegLongTimer] > 0) ? 60000 : 0));
				}
				if (this.{24287} < 1000f)
				{
					return 4000f;
				}
				if (this.{24287} < 10000f)
				{
					return 3000f + this.{24287} * 1.6f;
				}
				if (this.{24287} < 14000f)
				{
					return 20000f;
				}
				if (Session.Account.CaptainSkills[PDynamicAccountBonus.BAllowPowderKegLongTimer] <= 0)
				{
					return 20000f;
				}
				if (this.{24287} < 15500f)
				{
					return 60000f;
				}
				return (float)(Global.Player.MapInfo.IsWorldmap ? ((Session.SelectedPowderKegsInfo.ID == 1) ? 2 : 3) : 2) * 60000f;
			}
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060019AF RID: 6575 RVA: 0x000E424E File Offset: 0x000E244E
		private bool isClick
		{
			get
			{
				return (Global.Settings.RightMouseAction == RightMouseKeyAction.PowderKegThrow && InputHelper.RightWasClicked) || Global.Settings.kb_ThrowPowderKeg.IsClick;
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060019B0 RID: 6576 RVA: 0x000E4275 File Offset: 0x000E2475
		private bool isDown
		{
			get
			{
				return (Global.Settings.RightMouseAction == RightMouseKeyAction.PowderKegThrow && InputHelper.NowMouseState.RightPressed) || Global.Settings.kb_ThrowPowderKeg.IsDown;
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060019B1 RID: 6577 RVA: 0x000E42A1 File Offset: 0x000E24A1
		private bool isRelease
		{
			get
			{
				return (Global.Settings.RightMouseAction == RightMouseKeyAction.PowderKegThrow && InputHelper.RightWasReleased) || Global.Settings.kb_ThrowPowderKeg.IsRelease;
			}
		}

		// Token: 0x060019B3 RID: 6579 RVA: 0x000E42C8 File Offset: 0x000E24C8
		public Ship FirebrandTarget(out bool {24279})
		{
			Ship shipInSight = Global.Game.InterestPoints.ShipInSight;
			if (shipInSight == null || ((IClientShip)shipInSight).GetClient.StatusColor == HealthBarStyle.Lime)
			{
				{24279} = false;
				return null;
			}
			{24279} = (Vector2.Distance(shipInSight.Position, Global.Player.Position) < 50f);
			if (!{24279})
			{
				return shipInSight;
			}
			return null;
		}

		// Token: 0x060019B4 RID: 6580 RVA: 0x000E4328 File Offset: 0x000E2528
		public void Update(ref FrameTime {24280}, bool {24281})
		{
			if (Global.Settings.kb_ds_Forward.IsDown && !Global.Player.UsedShip.AllowPowderKegsAnySpeed && Global.Settings.kb_ThrowPowderKeg.IsDown)
			{
				this.{24289} = true;
			}
			{24281} &= !this.{24289};
			if ({24281} && this.isClick)
			{
				this.{24287} = 0f;
			}
			this.{24284} = ({24281} && this.isDown);
			if (this.{24284})
			{
				this.{24285} = 1f;
				InGameSightUi.OnAction();
				this.{24287} += {24280}.msElapsed * 6f;
				if (this.{24287} > 19000f)
				{
					this.{24287} = 0f;
				}
			}
			else
			{
				this.{24285} -= {24280}.secElapsed * 0.7f;
			}
			PowderKegInfo selectedPowderKegsInfo = Session.SelectedPowderKegsInfo;
			if (this.{24284})
			{
				Geometry.Evalute(ref this.{24286}, 1f, {24280}.secElapsed * 5f);
			}
			else
			{
				Geometry.Evalute(ref this.{24286}, 0f, {24280}.secElapsed * 5f);
			}
			this.AllowToRunFirebrand = false;
			this.{24288} = null;
			if (Session.SelectedPowderKegsInfo.isFirebrand)
			{
				bool flag;
				Ship ship = this.FirebrandTarget(out flag);
				if (ship != null)
				{
					this.{24288} = new Vector2?((ship.Position - Global.Player.Position).Normal());
					this.AllowToRunFirebrand = (Math.Abs(Vector2.Dot(this.{24288}.Value, Global.Player.FastNormal)) < 0.94f);
				}
			}
			if (Session.WReload.PowderKegsReloadingLeftSec == 0f && this.isRelease && {24281} && (!selectedPowderKegsInfo.isFirebrand || this.AllowToRunFirebrand))
			{
				Global.Player.RunPowderKeg(this.ConvertedTimig);
			}
			if (!Global.Settings.kb_ThrowPowderKeg.IsDown)
			{
				this.{24289} = false;
			}
		}

		// Token: 0x060019B5 RID: 6581 RVA: 0x000E4525 File Offset: 0x000E2725
		public void Render2D(float {24282})
		{
			this.ToolTipVisibility = this.{24285} * {24282};
			float powderKegsReloadingLeftSec = Session.WReload.PowderKegsReloadingLeftSec;
		}

		// Token: 0x060019B6 RID: 6582 RVA: 0x000E4548 File Offset: 0x000E2748
		public void Render3D(float {24283})
		{
			if (this.{24284} || this.{24286} != 0f)
			{
				if (Session.SelectedPowderKegsInfo.isFirebrand)
				{
					if (this.{24288} != null)
					{
						Vector2 value = this.{24288}.Value;
						float scale = 0.5f * this.{24286};
						Global.Render.ItemsShader.RenderSector(new Vector3(Global.Player.Position.X, -100f, Global.Player.Position.Y), 0.1f, 80f, MathF.Atan2(value.Y, value.X), 0.06283186f, Color.DarkGray * scale, Color.Orange * 0.6f * scale, true);
						return;
					}
				}
				else
				{
					PowderKegInfo selectedPowderKegsInfo = Session.SelectedPowderKegsInfo;
					foreach (Vector2 vector in Global.Player.GetPowderkegThrowPosition(selectedPowderKegsInfo))
					{
						float num = selectedPowderKegsInfo.TriggerRadius * (1f + Global.Player.UsedShip.PowderKegIncreaseTriggerRadiusBonus);
						Global.Render.ItemsShader.RenderCircle(new Vector3(vector.X, -100f, vector.Y), Math.Max(0.01f, num * this.{24286}), Math.Max(0.02f, num * this.{24286} + 1f), InGameSightUi.SightColorOpaque * this.{24286} * 0.4f, GPUCircleType.SoftCircle, null);
						Global.Render.ItemsShader.RenderCircle(new Vector3(vector.X, -100f, vector.Y), Math.Max(0.01f, (selectedPowderKegsInfo.DamageRadius - 1f) * (1.5f - 0.5f * this.{24286})), Math.Max(0.02f, (selectedPowderKegsInfo.DamageRadius - 1f) * (1.5f - 0.5f * this.{24286}) + 1f), InGameSightUi.GetBlueLight * this.{24286} * 1.1f, GPUCircleType.SoftCircle, null);
					}
				}
			}
		}

		// Token: 0x060019B7 RID: 6583 RVA: 0x000E477A File Offset: 0x000E297A
		public void Reset()
		{
			this.{24284} = false;
			this.{24286} = 0f;
			this.{24289} = false;
		}

		// Token: 0x0400180B RID: 6155
		private const int maxTime = 19000;

		// Token: 0x0400180C RID: 6156
		private bool {24284};

		// Token: 0x0400180D RID: 6157
		private float {24285};

		// Token: 0x0400180E RID: 6158
		private float {24286};

		// Token: 0x0400180F RID: 6159
		private float {24287};

		// Token: 0x04001810 RID: 6160
		private Vector2? {24288};

		// Token: 0x04001811 RID: 6161
		private bool {24289};

		// Token: 0x04001812 RID: 6162
		public float ToolTipVisibility;

		// Token: 0x04001813 RID: 6163
		public bool AllowToRunFirebrand;
	}
}
