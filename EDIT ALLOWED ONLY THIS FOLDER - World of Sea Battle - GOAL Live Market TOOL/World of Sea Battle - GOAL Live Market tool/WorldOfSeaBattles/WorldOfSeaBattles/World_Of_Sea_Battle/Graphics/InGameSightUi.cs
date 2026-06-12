using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;
using TheraEngine.Grphics.Device;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Interface;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x02000456 RID: 1110
	internal class InGameSightUi : IUpdateableObject
	{
		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x000D0377 File Offset: 0x000CE577
		public static Color GetBlueLight
		{
			get
			{
				return new Color(89, 199, 255, 255) * 0.12941177f;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x000D0399 File Offset: 0x000CE599
		public static Color GetDarkCyanLight
		{
			get
			{
				return new Color(23, 170, 119, 255) * 0.12941177f;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06001827 RID: 6183 RVA: 0x000D03B8 File Offset: 0x000CE5B8
		public static Color GetDimGray
		{
			get
			{
				return new Color(146, 164, 168, 255) * 0.12941177f;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x000D03DD File Offset: 0x000CE5DD
		public static Color SightColor
		{
			get
			{
				return Color.Lerp(Color.Gray, Color.WhiteSmoke, Global.Game.StaticSystem.GetSkyShader.DayOrNight) * 0.6f;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06001829 RID: 6185 RVA: 0x000D040C File Offset: 0x000CE60C
		public static Color SightColorOpaque
		{
			get
			{
				return Color.Lerp(Color.Gray, Color.WhiteSmoke, Global.Game.StaticSystem.GetSkyShader.DayOrNight);
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x000D0431 File Offset: 0x000CE631
		public float MarchingModeEffect
		{
			get
			{
				return 1f - this.{23548}.CurrentSoftValue;
			}
		}

		// Token: 0x0600182B RID: 6187 RVA: 0x000D0444 File Offset: 0x000CE644
		public InGameSightUi()
		{
			InGameSightUi.CannonSights = new CannonsController();
			InGameSightUi.PowderKegSights = new PowderKegController();
			InGameSightUi.FalkonetSights = new FalkonetsController();
			InGameSightUi.MortarSights = new MortarController();
			Tlist<IInGameSightUI> tlist = new Tlist<IInGameSightUI>();
			IInGameSightUI cannonSights = InGameSightUi.CannonSights;
			tlist.Add(cannonSights);
			IInGameSightUI powderKegSights = InGameSightUi.PowderKegSights;
			tlist.Add(powderKegSights);
			IInGameSightUI falkonetSights = InGameSightUi.FalkonetSights;
			tlist.Add(falkonetSights);
			IInGameSightUI mortarSights = InGameSightUi.MortarSights;
			tlist.Add(mortarSights);
			this.{23546} = tlist;
			this.{23548} = new SoftTrigger(0f, 0.6f, 2f);
			this.{23550} = new TargetInfoRenderer();
		}

		// Token: 0x0600182C RID: 6188 RVA: 0x000D04F4 File Offset: 0x000CE6F4
		public void Update(ref FrameTime {23543})
		{
			bool flag = GameScene.GameHasInputFocus && !Global.Game.IsMouseVisible && !Global.Player.IsDestroyed && !Global.Player.IsPortEntry;
			bool flag2 = false;
			bool flag3 = Global.Player.UsedShip.AllowPowderKegsAnySpeed && !Session.SelectedPowderKegsInfo.isFirebrand;
			if (flag && Global.Settings.kb_ThrowPowderKeg.IsClick && !flag3)
			{
				Global.Player.ResetSpeedTo2();
				flag2 = true;
			}
			bool flag4 = true;
			if (!flag2)
			{
				flag4 = (!Global.Player.IsMarchingModeWithLag && this.{23548}.CurrentSoftValue == 0f);
			}
			else
			{
				this.{23548}.Reset();
			}
			for (int i = 0; i < this.{23546}.Size; i++)
			{
				this.{23546}.Array[i].Update(ref {23543}, flag && ((this.{23546}.Array[i] is PowderKegController && flag3) || flag4));
			}
			InGameSightUi.interfacesAutoHideTimer += {23543}.msElapsed;
			if (InGameSightUi.interfacesAutoHideTimer > 15000f)
			{
				this.{23547} = 1f - MathHelper.Clamp((InGameSightUi.interfacesAutoHideTimer - 15000f) / 1000f, 0f, 1f);
			}
			else
			{
				this.{23547} = 1f;
			}
			if (Global.Settings.NoHideSight)
			{
				this.{23547} = 1f;
			}
			SoftTrigger softTrigger = this.{23548};
			bool {11796};
			if (!Global.Player.IsMarchingModeWithLag && EducationHelper.ShowSights && {18139}.CurrentInstance == null)
			{
				ClientDrop nearDrop = ClientDrop.nearDrop;
				{11796} = (nearDrop != null && nearDrop.IsBeingLooted);
			}
			else
			{
				{11796} = true;
			}
			softTrigger.Evalute(ref {23543}, {11796});
			{23543}.EvaluteTimerMs(ref this.{23549});
			WhaleHarpoonController.Update({23543});
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x000D06BC File Offset: 0x000CE8BC
		public void Render2D()
		{
			if (Global.Player.IsDestroyed || Global.Render.UiMode == InterfaceMode.Off)
			{
				return;
			}
			if (!Global.Camera.IsSpyglass)
			{
				this.DrawInformationAboutLastTarget(false);
			}
			float num = 0.6f + 0.4f * (1f - this.{23548}.CurrentSoftValue);
			InGameSightUi.CannonSights.Render2DAnyMode(num);
			InGameSightUi.FalkonetSights.Render2DAnyMode(num);
			InGameSightUi.MortarSights.Render2DAnyMode(num);
			if (this.{23549} > 0f)
			{
				float num2 = this.{23549} / 700f;
				Device gs = Engine.GS;
				Vector2 vector = Engine.GS.UIArea.HalfWidthHeightInt();
				Vector2 vector2 = InGameSightUi.c_skull.WidthHeight() / 2f;
				float {14558} = 0f;
				float {14559} = 0.8f * (2f - num2);
				Color color = Color.White * num2 * 0.7f;
				gs.Draw(InGameSightUi.c_skull, vector, vector2, {14558}, {14559}, color);
			}
			float num3 = this.{23547} * (1f - this.{23548}.CurrentSoftValue / 0.6f);
			foreach (IInGameSightUI inGameSightUI in ((IEnumerable<IInGameSightUI>)this.{23546}))
			{
				if (inGameSightUI is PowderKegController && Global.Player.UsedShip.AllowPowderKegsAnySpeed)
				{
					inGameSightUI.Render2D(1f);
				}
				else if (num3 > 0f)
				{
					inGameSightUI.Render2D(num3);
				}
			}
			if (num3 == 0f && !Global.Player.UsedShip.AllowPowderKegsAnySpeed)
			{
				InGameSightUi.PowderKegSights.ToolTipVisibility = 0f;
			}
			WhaleHarpoonController.DrawSight2D(num3);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x000D0878 File Offset: 0x000CEA78
		public void Render3D()
		{
			if (Global.Player.IsDestroyed || Global.Render.UiMode == InterfaceMode.Off)
			{
				return;
			}
			float num = this.{23547} * (1f - this.{23548}.CurrentSoftValue / 0.6f);
			foreach (IInGameSightUI inGameSightUI in ((IEnumerable<IInGameSightUI>)this.{23546}))
			{
				if (inGameSightUI is PowderKegController && Global.Player.UsedShip.AllowPowderKegsAnySpeed)
				{
					inGameSightUI.Render3D(1f);
				}
				else if (num > 0f)
				{
					inGameSightUI.Render3D(num);
				}
			}
			if (num > 0f)
			{
				InGameSightUi.CannonSights.Render3DProjected(num);
			}
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x000D0940 File Offset: 0x000CEB40
		public void Render3DForCannonsEquip()
		{
			InGameSightUi.CannonSights.Render3DProjected(1f);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x000D0951 File Offset: 0x000CEB51
		public void DrawInformationAboutLastTarget(bool {23544})
		{
			if ({18139}.CurrentInstance != null)
			{
				return;
			}
			Engine.GS.SetTexture(AtlasGameGui.Texture);
			this.{23550}.Draw({23544});
			Engine.GS.SetTexture(AtlasObjs.Texture);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x000D0985 File Offset: 0x000CEB85
		public void OnGun(int {23545})
		{
			InGameSightUi.CannonSights.OnGun({23545});
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x000D0994 File Offset: 0x000CEB94
		public void Reset()
		{
			for (int i = 0; i < this.{23546}.Size; i++)
			{
				this.{23546}.Array[i].Reset();
			}
			this.{23547} = 1f;
			InGameSightUi.interfacesAutoHideTimer = 0f;
			this.{23548}.Reset();
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x000D09E9 File Offset: 0x000CEBE9
		public static void OnAction()
		{
			InGameSightUi.interfacesAutoHideTimer = 0f;
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x000D09F5 File Offset: 0x000CEBF5
		public void OnKill()
		{
			this.{23549} = 700f;
		}

		// Token: 0x04001698 RID: 5784
		public static InGameSightUi CurrentInstance;

		// Token: 0x04001699 RID: 5785
		public static CannonsController CannonSights;

		// Token: 0x0400169A RID: 5786
		public static PowderKegController PowderKegSights;

		// Token: 0x0400169B RID: 5787
		public static FalkonetsController FalkonetSights;

		// Token: 0x0400169C RID: 5788
		public static MortarController MortarSights;

		// Token: 0x0400169D RID: 5789
		private static readonly Rectangle c_skull = new Rectangle(611, 203, 68, 70);

		// Token: 0x0400169E RID: 5790
		private Tlist<IInGameSightUI> {23546};

		// Token: 0x0400169F RID: 5791
		private float {23547} = 1f;

		// Token: 0x040016A0 RID: 5792
		private static float interfacesAutoHideTimer;

		// Token: 0x040016A1 RID: 5793
		private SoftTrigger {23548};

		// Token: 0x040016A2 RID: 5794
		private float {23549};

		// Token: 0x040016A3 RID: 5795
		private TargetInfoRenderer {23550};
	}
}
