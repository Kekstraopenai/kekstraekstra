using System;
using Common.Game;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Interface;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x0200054B RID: 1355
	internal class GameCameraEffects
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x00114CFB File Offset: 0x00112EFB
		public float MortarSpyglassEffect
		{
			get
			{
				return this.{25841}.CurrentSoftValueSmoothstep;
			}
		}

		// Token: 0x06001ED5 RID: 7893 RVA: 0x00114E24 File Offset: 0x00113024
		public void Update(ref FrameTime {25832}, Ship {25833})
		{
			this.{25844} = (this.{25844} ?? new SoftTrigger(0f, 1f, 2f));
			this.{25844}.Evalute(ref {25832}, {22913}.CurrentInstance != null);
			if (!Global.Settings.SpyglassAnimation)
			{
				this.{25844}.SetValue(false);
			}
			this.FovOffset = this.{25844}.CurrentSoftValueSmoothstep * 3f + this.{25840}.CurrentSoftValueSmoothstep * 3f;
			this.PositionOffset = default(Vector3);
			this.{25839}.Evalute(ref {25832}, {25833}.FirstController.LinearStateCode >= 2);
			this.{25840}.Evalute(ref {25832}, {25833}.FirstController.LinearStateCode == 3 || {19086}.CurrentMenu != null);
			this.{25841}.Evalute(ref {25832}, Global.Settings.kb_Mortar_ModifierKey.IsDown && GameScene.GameHasInputFocus && Global.Game.SceneGame.MouseState == 0);
			this.ZoomOffset = this.{25839}.CurrentSoftValueSmoothstep * 0.8f + this.{25840}.CurrentSoftValueSmoothstep * 1f - 0.5f - this.{25841}.CurrentSoftValueSmoothstep * 3f * (1f - Geometry.Saturate(Global.Camera.ZoomFactor));
			this.ZoomOffset += this.{25843}.CurrentSoftValueSmoothstep * 1f;
			this.PositionOffset.Y = this.PositionOffset.Y + this.{25841}.CurrentSoftValueSmoothstep * (4.5f + 3f * Geometry.Saturate(Global.Camera.ZoomFactor));
			this.{25848}.mass = 11f;
			this.{25849}.mass = 11f;
			this.{25848}.damping = 50f;
			this.{25849}.damping = 50f;
			this.{25848}.stiffness = 150f;
			this.{25849}.stiffness = 150f;
			if (Vector2.Distance(new Vector2(this.{25848}.CurrentPosition, this.{25849}.CurrentPosition), Global.Player.Position) > 10f || Global.Player.DebugEnabled)
			{
				this.{25848}.CurrentPosition = Global.Player.Position.X;
				this.{25849}.CurrentPosition = Global.Player.Position.Y;
			}
			float {11803} = {25832}.secElapsed * (1f + Math.Abs(Global.Player.NowSpeed) / 10f);
			this.{25848}.Update({11803}, Global.Player.Position.X);
			this.{25849}.Update({11803}, Global.Player.Position.Y);
			this.PositionOffset.X = this.PositionOffset.X + (this.{25848}.CurrentPosition - Global.Player.Position.X);
			this.PositionOffset.Z = this.PositionOffset.Z + (this.{25849}.CurrentPosition - Global.Player.Position.Y);
			if (Global.Settings.ForceCameraEffect)
			{
				if (this.{25845} > 0f)
				{
					float num = (float)(Global.Game.GameTotalTimeSec * 20.0 % 100.0);
					float x = HashHelper.NoiseCurve(num);
					float y = HashHelper.NoiseCurve(12221.177f + num);
					float z = HashHelper.NoiseCurve(-3852.46f - num);
					Vector3 value = new Vector3(x, y, z);
					value.Normalize();
					this.PositionOffset += value * 0.15f * this.{25845} * (0.3f + 0.7f * (1f - Global.Camera.ZoomFactor));
				}
				for (int i = 0; i < this.{25846}.Size; i++)
				{
					GameCameraEffects.LazyCameraDeform[] array = this.{25846}.Array;
					int num2 = i;
					array[num2].TtlSec = array[num2].TtlSec + {25832}.secElapsed * this.{25846}.Array[i].Speed;
					if (this.{25846}.Array[i].TtlSec > 1f)
					{
						this.{25846}.FastRemoveAt(i);
						i--;
					}
					else
					{
						this.PositionOffset += this.{25846}.Array[i].RandomVector3 * this.{25846}.Array[i].Amount;
					}
				}
			}
			else
			{
				this.{25846}.Clear();
			}
			Vector2 normal = {25833}.Normal;
			Vector2 {11452};
			Geometry.RotateVector2Fast(ref normal, 1.5707964f, out {11452});
			this.PositionOffset += {11452}.X0Y() * this.{25847} * 1.2f;
			Geometry.Evalute(ref this.{25847}, {25833}.physicsBody.TiltAxisControl, {25832}.secElapsed * Math.Abs(this.{25847} - {25833}.physicsBody.TiltAxisControl));
			this.PositionOffset.Y = this.PositionOffset.Y + 1f * (1f - Geometry.InverseLerp(0.47f, 0.83f, Global.Camera.ZoomFactor));
			{25832}.EvaluteTimerSec(ref this.{25845});
			SoftTrigger softTrigger = this.{25842};
			{22409} currentInstance = {22409}.CurrentInstance;
			softTrigger.Evalute(ref {25832}, currentInstance != null && currentInstance.Type == ShipDesignCategory.BowFigure);
			if (this.{25842}.CurrentSoftValue > 0f)
			{
				this.PositionOffset += Global.Player.Normal.X0Y() * Global.Player.UsedShip.StaticInfo.CorpusHalfLength * 0.8f * this.{25842}.CurrentSoftValueSmoothstep * Geometry.InverseLerp(-0.9f, -0.5f, -Vector2.Dot(Global.Player.Normal, Engine.GS.Camera.Direction.XZNormal()));
			}
			this.{25854} = (this.{25854} ?? new SoftTrigger(0f, 1f, 2.5f));
			this.{25854}.Evalute(ref {25832}, {17745}.CurrentInstance != null && !{17745}.CurrentInstance.IsClosedByHand);
			this.PositionOffset.Y = this.PositionOffset.Y - this.{25854}.CurrentSoftValueSmoothstep * 0.2f;
			bool flag = Global.Player.GetBattleTimer == 0f && Global.Player.IsRunningMarchingMode && Global.Player.UsedShip.HpFactor > 0.9f && Global.Game.MusicManager.IsPlaying;
			bool {11796} = false;
			if (flag)
			{
				this.{25851} += {25832}.secElapsed;
				if (this.{25851} > 10f)
				{
					{11796} = true;
				}
			}
			else if (this.{25850}.CurrentSoftValue > 0.5f)
			{
				this.{25851} = -1800f;
			}
			else if (this.{25851} < 0f)
			{
				this.{25851} += {25832}.secElapsed;
			}
			this.{25850}.Evalute(ref {25832}, {11796});
			for (int j = 0; j < this.{25855}.Size; j++)
			{
				Vector3? vector = this.{25855}[j].Update(ref {25832});
				if (vector != null)
				{
					this.PositionOffset += vector.Value;
				}
				else
				{
					this.{25855}.FastRemoveAt(j);
					j--;
				}
			}
		}

		// Token: 0x06001ED6 RID: 7894 RVA: 0x001155FC File Offset: 0x001137FC
		public void StopEffects()
		{
			this.{25839}.Reset();
			this.{25840}.Reset();
			this.PositionOffset = Vector3.Zero;
			this.{25846}.Clear();
			this.{25855}.Clear();
		}

		// Token: 0x06001ED7 RID: 7895 RVA: 0x00115635 File Offset: 0x00113835
		public void RunFocusEffect(CameraFocusEffect {25834})
		{
			this.{25855}.Add({25834});
		}

		// Token: 0x06001ED8 RID: 7896 RVA: 0x00115644 File Offset: 0x00113844
		public void OnReceveDamage(DamageID {25835}, float {25836})
		{
			float num = 0f;
			if ({25835} == DamageID.Collision)
			{
				if ({25836} > 100f)
				{
					for (int i = 0; i < 2; i++)
					{
						Tlist<GameCameraEffects.LazyCameraDeform> tlist = this.{25846};
						GameCameraEffects.LazyCameraDeform lazyCameraDeform = new GameCameraEffects.LazyCameraDeform(Rand.NextVector3(-1f, 1f) * 0.45f, 0.75f + 0.5f * (float)i);
						tlist.Add(lazyCameraDeform);
					}
				}
			}
			else if (({25835} == DamageID.PowderKeg || {25835} == DamageID.MortarShot) && {25836} > 30f)
			{
				float num2 = ({25835} == DamageID.PowderKeg) ? 0.6f : 0.2f;
				num = num2;
				for (int j = 0; j < 2; j++)
				{
					Tlist<GameCameraEffects.LazyCameraDeform> tlist2 = this.{25846};
					GameCameraEffects.LazyCameraDeform lazyCameraDeform = new GameCameraEffects.LazyCameraDeform(Rand.NextVector3(-1f, 1f) * num2, 0.75f + 0.5f * (float)j);
					tlist2.Add(lazyCameraDeform);
				}
			}
			else if ({25835} == DamageID.CannonBall || {25835} == DamageID.FalkonetBall)
			{
				num = 0.1f;
			}
			this.{25845} = Math.Min(this.{25845} + num, 0.5f);
		}

		// Token: 0x06001ED9 RID: 7897 RVA: 0x00115740 File Offset: 0x00113940
		public void OnWeaponSingleShot(bool {25837})
		{
			Tlist<GameCameraEffects.LazyCameraDeform> tlist = this.{25846};
			GameCameraEffects.LazyCameraDeform lazyCameraDeform = new GameCameraEffects.LazyCameraDeform(-Global.Camera.Direction * 0.08f * (1.2f - Global.Camera.ZoomFactor), 4f);
			tlist.Add(lazyCameraDeform);
			this.{25845} = Math.Min(this.{25845} + ({25837} ? 0.5f : 0.11f), 0.5f);
		}

		// Token: 0x06001EDA RID: 7898 RVA: 0x00003100 File Offset: 0x00001300
		public void OnSailingSpeedChange(int {25838})
		{
		}

		// Token: 0x04001E69 RID: 7785
		public Vector3 PositionOffset;

		// Token: 0x04001E6A RID: 7786
		public float ZoomOffset;

		// Token: 0x04001E6B RID: 7787
		public float FovOffset;

		// Token: 0x04001E6C RID: 7788
		private SoftTrigger {25839} = new SoftTrigger(0f, 1f, 1.2f);

		// Token: 0x04001E6D RID: 7789
		private SoftTrigger {25840} = new SoftTrigger(0f, 1f, 0.6f);

		// Token: 0x04001E6E RID: 7790
		private SoftTrigger {25841} = new SoftTrigger(0f, 1f, 1.2f);

		// Token: 0x04001E6F RID: 7791
		private SoftTrigger {25842} = new SoftTrigger(0f, 1f, 1.2f);

		// Token: 0x04001E70 RID: 7792
		private SoftTrigger {25843} = new SoftTrigger(0f, 1f, 1.5f);

		// Token: 0x04001E71 RID: 7793
		private SoftTrigger {25844};

		// Token: 0x04001E72 RID: 7794
		private float {25845};

		// Token: 0x04001E73 RID: 7795
		private Tlist<GameCameraEffects.LazyCameraDeform> {25846} = new Tlist<GameCameraEffects.LazyCameraDeform>();

		// Token: 0x04001E74 RID: 7796
		private float {25847};

		// Token: 0x04001E75 RID: 7797
		private Spring {25848} = new Spring(0f);

		// Token: 0x04001E76 RID: 7798
		private Spring {25849} = new Spring(0f);

		// Token: 0x04001E77 RID: 7799
		private SoftTrigger {25850} = new SoftTrigger(0f, 1f, 0.05f);

		// Token: 0x04001E78 RID: 7800
		private float {25851};

		// Token: 0x04001E79 RID: 7801
		private SoftTrigger {25852} = new SoftTrigger(0f, 1f, 0.3f);

		// Token: 0x04001E7A RID: 7802
		private SoftTrigger {25853} = new SoftTrigger(0f, 1f, 0.3f);

		// Token: 0x04001E7B RID: 7803
		private SoftTrigger {25854};

		// Token: 0x04001E7C RID: 7804
		private Tlist<CameraFocusEffect> {25855} = new Tlist<CameraFocusEffect>();

		// Token: 0x0200054C RID: 1356
		private struct LazyCameraDeform
		{
			// Token: 0x170002E6 RID: 742
			// (get) Token: 0x06001EDB RID: 7899 RVA: 0x001157B9 File Offset: 0x001139B9
			public float Amount
			{
				get
				{
					return 20f * this.TtlSec * MathF.Pow(1f - this.TtlSec, 7f);
				}
			}

			// Token: 0x06001EDC RID: 7900 RVA: 0x001157DE File Offset: 0x001139DE
			public LazyCameraDeform(Vector3 {25858}, float {25859} = 1f)
			{
				this.Speed = {25859};
				this.TtlSec = 0f;
				this.RandomVector3 = {25858};
			}

			// Token: 0x04001E7D RID: 7805
			public Vector3 RandomVector3;

			// Token: 0x04001E7E RID: 7806
			public float TtlSec;

			// Token: 0x04001E7F RID: 7807
			public float Speed;
		}
	}
}
