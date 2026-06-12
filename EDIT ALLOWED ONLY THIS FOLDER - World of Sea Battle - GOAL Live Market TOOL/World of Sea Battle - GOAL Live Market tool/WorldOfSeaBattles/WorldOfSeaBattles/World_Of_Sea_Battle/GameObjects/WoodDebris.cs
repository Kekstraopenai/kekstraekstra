using System;
using Common;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Animations;
using TheraEngine.Components;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using UWPhysics.SimulationEntities;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000066 RID: 102
	internal sealed class WoodDebris : IPoolObject
	{
		// Token: 0x06000329 RID: 809 RVA: 0x0001AF90 File Offset: 0x00019190
		public WoodDebris()
		{
			this.{17045} = new ModelTransformedScene(LocalContent.Loaded.AllDebris[HitMaterialEffect.Wood][0], new Transform3D(Vector3.Zero, Vector3.Zero, new Vector3(0.3f)));
			this.{17045}.Transform.Scales = new Vector3(0.3f);
			this.{17047} = new GravityOnlyEntity(this.{17045});
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001B004 File Offset: 0x00019204
		public void ClearResources()
		{
			this.{17046} = null;
			this.{17049} = false;
			this.{17050} = 0f;
			this.{17051} = 0f;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001B02C File Offset: 0x0001922C
		public void Initialize(ref Vector3 {17037}, ref Vector3 {17038}, HitMaterialEffect {17039})
		{
			if ({17039} == HitMaterialEffect.Crew)
			{
				{17039} = HitMaterialEffect.Wood;
			}
			this.{17048} = {17039};
			this.{17047}.Stop();
			UWModel[] array = LocalContent.Loaded.AllDebris[{17039}];
			this.{17045}.SetModelData(0, array[Rand.RangeInt(0, array.Length)]);
			this.{17045}.Transform.Scales = Rand.NextVector3(0.85f, 1.4f) * 0.3f * 1.5f;
			this.{17045}.Transform.Translation = {17038};
			this.{17045}.Transform.RotatesAll = new Vector3(Rand.Angle(), Rand.Angle(), Rand.Angle());
			{17037} += Rand.NextVector3(-1f, 1f) * (({17039} == HitMaterialEffect.Sailes) ? 0.8f : 0.4f);
			{17037}.Normalize();
			{17037}.Y = Rand.Range(0.4f, 0.9f);
			Vector3.Multiply(ref {17037}, 18f, out {17037});
			this.{17046} = new TransformRigidAnimation(Rand.Range(2000f, 4000f), this.{17045}.Transform, {17038} + {17037}, new Vector3(Rand.Angle() + (float)(3 * Rand.Sign()), Rand.Angle() + (float)(3 * Rand.Sign()), Rand.Angle() + (float)(3 * Rand.Sign())), this.{17045}.Transform.Scales);
			this.{17045}.VisibleTestType = ModelSceneVisibleTest.Disable;
			this.{17052} = Rand.Chanse(6f);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001B1D4 File Offset: 0x000193D4
		public void Update(ref FrameTime {17040}, bool {17041}, out bool {17042}, int {17043})
		{
			{17042} = false;
			if (!this.{17049})
			{
				FrameTime frameTime = {17040} * ((this.{17048} == HitMaterialEffect.Sailes) ? 0.75f : ((this.{17048} == HitMaterialEffect.Stone) ? 0.85f : 0.8f));
				this.{17047}.Update(ref frameTime);
				this.{17046}.Update(ref frameTime);
			}
			float num = ((this.{17051} == 0f) ? 0f : (this.{17050} / this.{17051})) * 0.6f;
			float num2 = (Global.Player == null) ? 0f : (CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, this.{17045}.Transform.Translation.X, this.{17045}.Transform.Translation.Z) + 0.15f - num);
			float num3 = Vector3.DistanceSquared(Engine.GS.Camera.Position, this.{17045}.Transform.Translation);
			if (this.{17045}.Transform.Translation.Y < num2 || this.{17049})
			{
				this.{17045}.Transform.Translation.Y = num2;
				if (!this.{17049})
				{
					FXEngine.WaterParticleVolumteric(this.{17045}.Transform.Translation.XZ(), 1.3f, Vector3.Up, Color.White * 0.7f, 0.2f, new float?(this.{17045}.Transform.Translation.Y));
					if (Rand.Chanse(90f - MathHelper.Min((float){17043} / 500f * 20f, 80f)) && num3 < 35721f)
					{
						this.{17049} = true;
						this.{17051} = Rand.Range(20f, 40f);
					}
					else
					{
						{17042} = true;
					}
				}
			}
			if (this.{17049})
			{
				this.{17050} += {17040}.secElapsed;
			}
			if ((this.{17049} && this.{17050} > this.{17051}) || num3 > 44100f || (this.{17049} && num3 > 25600f))
			{
				{17042} = true;
			}
			if (this.{17052} && this.{17050} < 10f && Rand.Chanse({17040}.msElapsed / 16f * 30f))
			{
				FXEngine.SampleFlameAndSmoke(this.{17045}.Transform.Translation, 1f, false, Rand.Chanse(20f), false, null, 1f);
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001B470 File Offset: 0x00019670
		public void Render(InstancedModelRenderer {17044})
		{
			Matrix matrix;
			this.{17045}.Transform.CreateWorldMatrix(out matrix);
			{17044}.AddToCache(this.{17045}.GetModels[0].Model, ref matrix);
		}

		// Token: 0x0400027E RID: 638
		private ModelTransformedScene {17045};

		// Token: 0x0400027F RID: 639
		private TransformRigidAnimation {17046};

		// Token: 0x04000280 RID: 640
		private GravityOnlyEntity {17047};

		// Token: 0x04000281 RID: 641
		private HitMaterialEffect {17048};

		// Token: 0x04000282 RID: 642
		private bool {17049};

		// Token: 0x04000283 RID: 643
		private float {17050};

		// Token: 0x04000284 RID: 644
		private float {17051};

		// Token: 0x04000285 RID: 645
		private bool {17052};
	}
}
