using System;
using System.Runtime.CompilerServices;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Scene.ParticleSystem;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004A8 RID: 1192
	internal sealed class MendingpackSFEffect : ShipEffect
	{
		// Token: 0x06001A20 RID: 6688 RVA: 0x000E8E3C File Offset: 0x000E703C
		public MendingpackSFEffect(Ship {24508}, Color {24509}) : base({24508})
		{
			this.{24515} = {24509} * 0.5f;
			this.{24512} = 2500f;
			this.{24514} = new Vector3(0f, {24508}.UsedShip.StaticInfo.BSRadius * 0.22f, 0f);
			this.{24513} = FXEngine.CreateParticleSystemMendingKit({24508}, {24508}.Position3D + this.{24514}, {24509}, MendingpackSFEffect.itemPath);
			base.EndLife += this.{24511};
		}

		// Token: 0x06001A21 RID: 6689 RVA: 0x000E8ECC File Offset: 0x000E70CC
		protected override bool Update(ref FrameTime {24510})
		{
			this.{24512} -= {24510}.msElapsed;
			this.{24513}.Center = this.ship.Position3D + this.{24514};
			this.{24513}.ParticleEffect.Array[0].SingleColor = this.{24515}.ToVector4();
			return this.{24512} < 1f;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x00003100 File Offset: 0x00001300
		public override void Render3D()
		{
		}

		// Token: 0x06001A24 RID: 6692 RVA: 0x000E8F68 File Offset: 0x000E7168
		[CompilerGenerated]
		private void {24511}()
		{
			this.{24513}.Remove();
			this.{24513} = null;
		}

		// Token: 0x04001883 RID: 6275
		private const float c_ttl = 2500f;

		// Token: 0x04001884 RID: 6276
		private static readonly RandomParticleTextureSet itemPath = new RandomParticleTextureSet(new Rectangle[]
		{
			new Rectangle(2800, 1, 20, 20)
		});

		// Token: 0x04001885 RID: 6277
		private float {24512};

		// Token: 0x04001886 RID: 6278
		private ParticleSystem3D {24513};

		// Token: 0x04001887 RID: 6279
		private Vector3 {24514};

		// Token: 0x04001888 RID: 6280
		private Color {24515};
	}
}
