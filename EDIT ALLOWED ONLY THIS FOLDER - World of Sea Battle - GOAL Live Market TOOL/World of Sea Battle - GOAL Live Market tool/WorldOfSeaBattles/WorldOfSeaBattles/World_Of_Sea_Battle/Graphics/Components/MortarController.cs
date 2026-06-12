using System;
using System.Linq;
using Common;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.ProcedureGeneration.Generation3D;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000490 RID: 1168
	internal class MortarController : IInGameSightUI
	{
		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600199B RID: 6555 RVA: 0x000E34E1 File Offset: 0x000E16E1
		public float SpecialMortarPreparationLevel
		{
			get
			{
				return this.{24274};
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600199C RID: 6556 RVA: 0x000E34E9 File Offset: 0x000E16E9
		public bool IsActive
		{
			get
			{
				return this.{24268};
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600199D RID: 6557 RVA: 0x000E34F4 File Offset: 0x000E16F4
		public bool ShouldUsePowderKegs
		{
			get
			{
				CannonGameInstance cannonGameInstance = Global.Player.UsedShip.Mortars.Iterate().FirstOrDefault<CannonGameInstance>();
				return cannonGameInstance != null && cannonGameInstance.Info.Feature == CannonFeature.PowderKegMortar;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x0600199E RID: 6558 RVA: 0x000E3530 File Offset: 0x000E1730
		public bool HasAmmo
		{
			get
			{
				if (!this.ShouldUsePowderKegs)
				{
					return Global.Player.UsedShipPlayer.BallsOfHold[(int)Session.SelectedMortarBalls.ID] > 0;
				}
				return Global.Player.UsedShipPlayer.PowderKegsOfHold[MortarShot.PowderKegMortarType] > 0;
			}
		}

		// Token: 0x0600199F RID: 6559 RVA: 0x000E3584 File Offset: 0x000E1784
		public MortarController()
		{
			MortarController.areaSightGeometry2 = RingAreaGenerator.Begin_VertexPositionColorTexture(16, -1f, 0f, 1f, Color.Transparent, Color.White, Color.Transparent, 1f, AtlasObjs.whitepixel_1px, AtlasObjs.Texture.Size);
		}

		// Token: 0x060019A0 RID: 6560 RVA: 0x000E35E0 File Offset: 0x000E17E0
		public void Update(ref FrameTime {24256}, bool {24257})
		{
			bool flag = {19548}.CurrentInstance != null && {19548}.CurrentInstance.MortarSightByPowerupItem;
			bool flag2 = !flag && Vector2.Dot(Global.Camera.Direction.XZNormal(), Global.Player.FastNormal) < 0f;
			CannonBallInfo selectedMortarBalls = Session.SelectedMortarBalls;
			ValueTuple<float, float, float, float, float, float, float> mortarsAverageParameters = Global.Player.UsedShip.GetMortarsAverageParameters(selectedMortarBalls);
			if (Global.Player.FirstController.LinearStateCode == 0)
			{
				this.{24274} = Math.Min(1f, this.{24274} + {24256}.msElapsed / mortarsAverageParameters.Item4);
			}
			else
			{
				this.{24274} = Math.Max(0f, this.{24274} - {24256}.secElapsed / 1.5f);
			}
			if ((!Global.Player.UsedShip.StaticInfo.FetchMortars(!flag2, Global.Player.UsedShipPlayer).Any<ValueTuple<CannonLocationInfo, CannonGameInfo>>() && !flag) || Global.Game.GetCurrentSceneName != GameSceneName.Game)
			{
				this.{24268} = false;
				this.{24269} = 0f;
				this.{24273} = 10f;
				return;
			}
			this.{24273} = mortarsAverageParameters.Item5;
			this.{24270} = MathHelper.Clamp((CannonsController.GetViewNormal(InGameSightUi.CannonSights.FollowDir).Y + (Global.Camera.IsSpyglass ? 0.05f : 0.3f)) * 9f, 0f, 1f);
			this.{24270} = MathHelper.Lerp(-Global.Player.UsedShip.MortarDeadzoneDecrease, 1f + Global.Player.UsedShip.MortarMaxDistanceBonus / (mortarsAverageParameters.Item2 - mortarsAverageParameters.Item1), this.{24270});
			this.{24268} = ({24257} && Global.Settings.kb_Mortar_ModifierKey.IsDown);
			if (this.{24268})
			{
				InGameSightUi.OnAction();
			}
			if (this.{24268})
			{
				Geometry.Evalute(ref this.{24269}, this.{24273}, {24256}.secElapsed * 60f);
			}
			else
			{
				Geometry.Evalute(ref this.{24269}, 0f, {24256}.secElapsed * 25f);
			}
			this.{24272} = this.{24271};
			float num = (Global.Player.UsedShipPlayer.Upgrades.Effects[ShipBonusEffect.BMortarAngleBonus] != 0f) ? 0.45f : 0.17f;
			float rotation = Global.Player.Rotation;
			Vector2 vector = InGameSightUi.CannonSights.FinalAttackNormal.XZ();
			if (flag2)
			{
				vector *= -1f;
			}
			float num2 = -MathF.Atan2(vector.X, vector.Y) - rotation + 1.5707964f;
			Geometry.AxisNorm(ref num2);
			if (num2 < -num)
			{
				num2 = -num;
			}
			if (num2 > num)
			{
				num2 = num;
			}
			this.{24271} = Geometry.SubstructRotate(num2 + rotation, 1f);
			if (flag2)
			{
				this.{24271} *= -1f;
			}
			if (flag)
			{
				Session.MortarShotParamPowerupItem = new Vector3(this.{24271}, this.{24270});
			}
			else if ((flag2 ? (Session.WReload.BackMortarReloadingLeftSec == 0f) : (Session.WReload.RearMortarReloadingLeftSec == 0f)) && Global.Settings.kb_Mortar_ModifierKey.IsDown && InputHelper.LeftWasClicked && {24257})
			{
				if (Global.Player.UsedShip.Mortars.Iterate().Any((CannonGameInstance {24276}) => {24276}.Info.MortarNeedsPreparation) && this.{24274} != 1f)
				{
					if (Global.Player.FirstController.LinearStateCode != 0)
					{
						{19994}.Me({19988}.Info, Local.specific_mortar_tt, Array.Empty<object>());
					}
					else
					{
						{19994}.Me({19988}.Info, Local.specific_mortar_tt2, Array.Empty<object>());
					}
				}
				else
				{
					float num3 = 1f - this.{24275};
					Vector2 vector2 = this.{24271};
					if (num3 > 0f)
					{
						float num4 = this.{24273} * num3;
						float num5 = MathHelper.Lerp(mortarsAverageParameters.Item1, mortarsAverageParameters.Item2, this.{24270});
						float num6 = MathF.Atan(num4 / num5) * 2f;
						vector2 = Geometry.RotateVector2(vector2, Rand.Range(-num6, num6));
						vector2 = vector2.Normal() * (vector2.Length() + Rand.Range(-num4, num4) * 0.25f / mortarsAverageParameters.Item2);
					}
					Global.Player.MortarGun(new Vector3(vector2, this.{24270}), flag2);
				}
			}
			this.{24275} -= {24256}.secElapsed * (float)Math.Abs((Global.Player.FirstController.AxisStateCode == 0 && Vector2.DistanceSquared(this.{24272}, this.{24271}) < 5E-05f) ? 0 : 1);
			this.{24275} += {24256}.secElapsed / mortarsAverageParameters.Item6 * Global.Player.UsedShip.MortarAimingSpeed;
			this.{24275} = Geometry.Saturate(this.{24275});
			if (Global.Player.IsMarchingMode)
			{
				this.{24275} = Math.Min(this.{24275}, 0.5f);
			}
		}

		// Token: 0x060019A1 RID: 6561 RVA: 0x00003100 File Offset: 0x00001300
		public void Render2DAnyMode(float {24258})
		{
		}

		// Token: 0x060019A2 RID: 6562 RVA: 0x00003100 File Offset: 0x00001300
		public void Render2D(float {24259})
		{
		}

		// Token: 0x060019A3 RID: 6563 RVA: 0x000E3B0C File Offset: 0x000E1D0C
		public void Render3D(float {24260})
		{
			if ((this.{24268} || this.{24269} != 0f) && this.{24273} > 0f)
			{
				bool isBack = Vector2.Dot(this.{24271}, Global.Player.FastNormal) < 0f;
				float num = (from {24278} in Global.Player.UsedShip.StaticInfo.MortarPorts
				where {24278}.Side == CannonLocation.InBack == isBack
				select {24278}).Average((CannonLocationInfo {24277}) => {24277}.Position.X);
				Vector2 vector = Functions.MortarShotEndPosition(Global.Player.Position + Global.Player.Normal * (num * Global.Player.Transform.MiddleScale), new Vector3(this.{24271}, this.{24270}), Global.Player, Session.SelectedMortarBalls);
				float num2 = Geometry.Saturate(this.{24269} / this.{24273});
				float num3 = Vector2.Distance(vector, Global.Player.Position);
				ValueTuple<float, float> valueTuple = new ValueTuple<float, float>(Global.Render.ItemsShader.fogNearDistance, Global.Render.ItemsShader.fogFarDistance);
				Global.Render.ItemsShader.ManualSetFog(1000f, 1000f);
				Global.Render.ItemsShader.RenderCircle(new Vector3(vector.X, -100f, vector.Y), Math.Max(0.01f, this.{24269} - num3 / 50f), Math.Max(0.02f, this.{24269}), InGameSightUi.SightColorOpaque * num2, GPUCircleType.SoftCircle, null);
				float num4 = (1.8f - this.{24275}) * this.{24273};
				if (num4 > 1.01f * this.{24273})
				{
					Global.Render.ItemsShader.RenderCircle(new Vector3(vector.X, -100f, vector.Y), Math.Max(0.01f, num4 - 2f), Math.Max(0.02f, num4), InGameSightUi.SightColorOpaque.Multiply(Color.OrangeRed) * num2 * 0.66f, GPUCircleType.SoftCircle, null);
				}
				Global.Render.ItemsShader.ManualSetFog(valueTuple.Item1, valueTuple.Item2);
				LocalContent.Loaded.SightLine.Transform.Translation = new Vector3(Global.Player.Position3D.X, 1f, Global.Player.Position3D.Z);
				LocalContent.Loaded.SightLine.Transform.Scales = new Vector3(0.33f, 1f, 1f);
				LocalContent.Loaded.SightLine.Transform.Yaw = MathF.Atan2(this.{24271}.Y, this.{24271}.X) + 1.5707964f;
				VolumetricSightShader volumeSightRender = InGameSightUi.CannonSights.VolumeSightRender;
				volumeSightRender.SetTexture(AtlasObjs.Texture.Tex);
				volumeSightRender.DrawEffect(Global.Camera.ViewMultiplyProjection, LocalContent.Loaded.SightLine.Transform.CreateWorldMatrix(), new Vector2(num3, num3 / 5f), 0f, (InGameSightUi.SightColor * 1.6666666f).ToVector4() * ({24260} * num2));
				LocalContent.Loaded.SightLine.GetModels[0].Model.OptimizedRenderAllBuffers();
			}
		}

		// Token: 0x060019A4 RID: 6564 RVA: 0x000E3E90 File Offset: 0x000E2090
		public static void AreaSightHelper(Vector2 {24261}, float {24262}, float {24263})
		{
			MortarController.areaSightGeometry.SetUV(new Rectangle(1767, 0, 223, 222), AtlasObjs.Texture.Size);
			float num = CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, {24261}.X, {24261}.Y);
			Global.Render.ItemsShader.SetForRender(new Transform3D(new Vector3({24261}.X, num + 0.5f, {24261}.Y), new Vector3(0f, (float)(Global.Game.GameTotalTimeSec / 2.0 % 6.2831854820251465), 0f), new Vector3({24262})).CreateWorldMatrix(), Vector4.One * 0.7f * {24263});
			Global.Render.ItemsShader.BeginPass(true, false);
			MortarController.areaSightGeometry.Render();
			Global.Render.ItemsShader.SetForRender(new Transform3D(new Vector3({24261}.X, num + 0.5f, {24261}.Y), new Vector3(0f, -(float)(Global.Game.GameTotalTimeSec / 2.0 % 6.2831854820251465), 0f), new Vector3({24262} * 0.8f)).CreateWorldMatrix(), Vector4.One * 0.7f * {24263});
			Global.Render.ItemsShader.BeginPass(true, false);
			MortarController.areaSightGeometry.Render();
			Color orangeRed = Color.OrangeRed;
			MortarController.AreaSightHelperHeight({24261}, {24262}, {24263}, orangeRed);
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x000E4028 File Offset: 0x000E2228
		public static void AreaSightHelperHeight(Vector2 {24264}, float {24265}, float {24266}, in Color {24267})
		{
			float num = Geometry.Saturate((Vector2.Distance(Global.Player.Position, {24264}) - 20f) / 50f);
			if (num > 0f)
			{
				float y = CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, {24264}.X, {24264}.Y);
				num *= 0.5f;
				ParticlesAndStaticMesh itemsShader = Global.Render.ItemsShader;
				Matrix {15449} = new Transform3D(new Vector3({24264}.X, y, {24264}.Y), new Vector3(0f, 0f, 0f), new Vector3({24265} * 0.8f, 1f, {24265} * 0.8f)).CreateWorldMatrix();
				Color color = {24267};
				itemsShader.SetForRender({15449}, color.ToVector4() * num * {24266});
				Global.Render.ItemsShader.BeginPass(true, false);
				MortarController.areaSightGeometry2.Render();
			}
		}

		// Token: 0x060019A6 RID: 6566 RVA: 0x000E4117 File Offset: 0x000E2317
		public void Reset()
		{
			this.{24268} = false;
			this.{24269} = 0f;
		}

		// Token: 0x040017FD RID: 6141
		private bool {24268};

		// Token: 0x040017FE RID: 6142
		private float {24269};

		// Token: 0x040017FF RID: 6143
		private float {24270};

		// Token: 0x04001800 RID: 6144
		private Vector2 {24271};

		// Token: 0x04001801 RID: 6145
		private Vector2 {24272};

		// Token: 0x04001802 RID: 6146
		private static BillboardParent_VPCT areaSightGeometry = BillboardParent_VPCT.CreatePlane(1f, 1f, 0f);

		// Token: 0x04001803 RID: 6147
		private static UserMesh areaSightGeometry2;

		// Token: 0x04001804 RID: 6148
		private float {24273};

		// Token: 0x04001805 RID: 6149
		private float {24274} = 1f;

		// Token: 0x04001806 RID: 6150
		private float {24275};
	}
}
