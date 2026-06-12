using System;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x0200003A RID: 58
	internal sealed class BoardingHookRenderer
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000BECA File Offset: 0x0000A0CA
		public int SourceShipUid
		{
			get
			{
				return this.{16559};
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public BoardingHookRenderer(Ship {16549}, Vector3 {16550}, int {16551}, Vector3 {16552})
		{
			Vector2 vector = {16552}.XZNormal();
			this.FalkonetBallUid = {16551};
			this.{16559} = {16549}.uID;
			this.{16560} = MathF.Atan2(vector.Y, vector.X);
			this.{16558} = new ModelTransformedScene
			{
				VisibleTestType = ModelSceneVisibleTest.Disable
			};
			this.{16558}.AddObject(new ModelRenderer(LocalContent.Loaded.BoardingHook[0])
			{
				LocalTransformOrNull = new Transform3D(({16550} - {16549}.Position3D) / 0.3f, new Vector3(0f, this.{16560} + 3.1415927f, 0f), new Vector3(1f))
			}, true);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000BFAB File Offset: 0x0000A1AB
		public BoardingHookRenderer(Ship {16553}, ClientCannonBall {16554}) : this({16553}, {16554}.StartPosition, {16554}.uID, {16554}.StartMomentNormal)
		{
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000BFC8 File Offset: 0x0000A1C8
		public void ConnectTarget(Ship {16555}, Ship {16556})
		{
			this.{16561} = {16556}.uID;
			Vector2 vector = {16555}.Position - {16556}.Position;
			this.{16563} = vector.Length();
			this.{16562} = MathF.Atan2(vector.Y, vector.X);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000C018 File Offset: 0x0000A218
		public bool Update(ref FrameTime {16557})
		{
			Ship shipFromUID = Global.Game.WorldInstance.GetShipFromUID(this.{16559});
			if (shipFromUID == null || shipFromUID.IsDestroyed)
			{
				return true;
			}
			ClientCannonBall falkonetBall = Global.Game.WorldInstance.GetFalkonetBall(this.FalkonetBallUid);
			if (falkonetBall != null)
			{
				float x = Vector2.Distance(falkonetBall.StartPosition.XZ(), falkonetBall.Sphere.XZ()) + 0.5f;
				this.{16564} = x;
				float roll = MathF.Atan2(falkonetBall.Sphere.Y - falkonetBall.StartPosition.Y, x);
				this.{16558}.GetModels[0].LocalTransformOrNull.Roll = roll;
			}
			else
			{
				if (this.{16565} == null)
				{
					this.{16565} = new float?(this.{16558}.GetModels[0].LocalTransformOrNull.Roll);
				}
				if (this.{16561} == -1)
				{
					this.{16566} -= {16557}.msElapsed;
					if (this.{16566} < 0f)
					{
						return true;
					}
				}
			}
			if (this.{16561} != -1)
			{
				Ship shipFromUID2 = Global.Game.WorldInstance.GetShipFromUID(this.{16561});
				if (shipFromUID2 == null || shipFromUID2.IsDestroyed)
				{
					return true;
				}
				Vector2 vector = shipFromUID.Position - shipFromUID2.Position;
				float num = MathF.Atan2(vector.Y, vector.X);
				this.{16558}.GetModels[0].LocalTransformOrNull.Scales.X = 1f * vector.Length() / this.{16563};
				this.{16558}.GetModels[0].LocalTransformOrNull.Yaw = this.{16560} + Geometry.AxisNormFast(num - this.{16562}) + 3.1415927f;
				if (this.{16565} != null)
				{
					this.{16558}.GetModels[0].LocalTransformOrNull.Roll = -(shipFromUID.Position3D.Y - shipFromUID2.Position3D.Y) / (3f + vector.Length()) + this.{16565}.Value;
				}
			}
			this.{16558}.Transform.Translation = shipFromUID.Position3D;
			this.{16558}.Transform.Scales = new Vector3(0.3f);
			return false;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000C275 File Offset: 0x0000A475
		public void Render3D()
		{
			if (this.{16566} >= 750f)
			{
				Global.Render.CommonShader.RenderObject(this.{16558}, true, 1f, true, this.{16564} / 0.3f, false);
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000C2AD File Offset: 0x0000A4AD
		public void Render3DTransparent()
		{
			if (this.{16566} < 750f)
			{
				Global.Render.CommonShader.RenderObject(this.{16558}, true, this.{16566} / 750f, false, this.{16564} / 0.3f, false);
			}
		}

		// Token: 0x04000137 RID: 311
		private const float c_scale = 1f;

		// Token: 0x04000138 RID: 312
		private ModelTransformedScene {16558};

		// Token: 0x04000139 RID: 313
		private int {16559};

		// Token: 0x0400013A RID: 314
		private float {16560};

		// Token: 0x0400013B RID: 315
		public int FalkonetBallUid;

		// Token: 0x0400013C RID: 316
		private int {16561} = -1;

		// Token: 0x0400013D RID: 317
		private float {16562};

		// Token: 0x0400013E RID: 318
		private float {16563};

		// Token: 0x0400013F RID: 319
		private float {16564} = 0.0001f;

		// Token: 0x04000140 RID: 320
		private float? {16565};

		// Token: 0x04000141 RID: 321
		private float {16566} = 750f;
	}
}
