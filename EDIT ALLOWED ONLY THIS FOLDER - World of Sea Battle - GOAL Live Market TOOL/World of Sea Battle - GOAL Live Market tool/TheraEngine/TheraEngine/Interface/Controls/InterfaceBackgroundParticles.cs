using System;
using Microsoft.Xna.Framework;
using TheraEngine.Components.Architecture;
using TheraEngine.Helpers;
using TheraEngine.Scene.ParticleSystem;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000D9 RID: 217
	public class InterfaceBackgroundParticles : UiControl
	{
		// Token: 0x060005F1 RID: 1521 RVA: 0x0001EC5C File Offset: 0x0001CE5C
		public InterfaceBackgroundParticles(Marker {14164}, ParticlesBackgroundPattern {14165}, ParticlesBackgroundCount {14166}, float {14167}, float {14168}, float {14169}, ParticleManager2D {14170}, params Rectangle[] {14171}) : base({14164}, PositionAlignment.Both, Color.White)
		{
			this.{14174} = {14171};
			this.{14175} = new ParticlesLayerRender(500);
			if ({14165} == ParticlesBackgroundPattern.SparksToUp)
			{
				this.{14176} = new ParticlePattern2D
				{
					DepthEffect = true,
					LifeTime = new Range1D(2000f, 2000f),
					OriginOffset = new Range1D(0f, 0.1f),
					RandomOffsetX = new Range1D(0f, 0f),
					RandomOffsetY = new Range1D(0f, 0f),
					RandomVelocityFactor = 0f,
					RotationVelocity = new Range1D(-0.15f, 0.15f),
					Size = new Range1D({14167}, {14168}),
					SizeSpeed = new Range1D(0f, 0f),
					StartRotationAngle = new Range1D(0f, 0f),
					TimeToStartLife = new Range1D(300f, 300f),
					TexturePath = {14171}[0]
				};
				this.{14177} = new ParticleSystem2D(new Vector2({14164}.Center.X, {14164}.XY.Y + {14164}.WH.Y), 0f, this.{14176}, Color.White, new Range1D(-25f * {14169}, 25f * {14169}), new Range1D(-40f * {14169}, -400f * {14169}), {14170});
				this.{14177}.CountPerSecound = (float)(({14166} == ParticlesBackgroundCount.Many) ? 90 : 30);
				this.{14177}.OutputLayer = this.{14175};
				this.{14177}.Unbind();
				base.UseScissor = true;
				this.{14178} = Engine.GS.MouseToUI;
				this.MouseShiftEffect = true;
				return;
			}
			throw new NotSupportedException();
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x0001EE4C File Offset: 0x0001D04C
		internal override void Update(ref FrameTime {14172}, ref int {14173})
		{
			this.{14177}.Centr = new Vector2(base.Pos.Center.X, base.Pos.XY.Y + base.Pos.WH.Y);
			this.{14176}.TexturePath = this.{14174}[Rand.RangeInt(0, this.{14174}.Length)];
			this.{14177}.ParticlePattern.RandomOffsetX = new Range1D(-base.Pos.WH.X * 0.5f, base.Pos.WH.X * 0.5f);
			((IUpdateableObject)this.{14177}).Update(ref {14172});
			this.{14175}.BoundCheck(base.Pos);
			this.{14175}.Update(ref {14172});
			if (this.MouseShiftEffect)
			{
				float num = Math.Min(Math.Abs(this.{14178}.X - Engine.GS.MouseToUI.X), 20f) * (float)Math.Sign(this.{14178}.X - Engine.GS.MouseToUI.X);
				this.{14175}.Transform(new Vector2(num * 0.25f, 0f), Vector2.One);
			}
			this.{14178} = Engine.GS.MouseToUI;
			base.Update(ref {14172}, ref {14173});
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001EFBC File Offset: 0x0001D1BC
		internal override void Render()
		{
			base.Render();
			if (base.IsVisible)
			{
				float opcaity = base.GetOpcaity();
				if (opcaity != 0f)
				{
					this.{14175}.Render(opcaity);
				}
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x0001EFF2 File Offset: 0x0001D1F2
		protected internal override void MarkerChanged()
		{
			base.MarkerChanged();
			base.UseScissor = true;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0001F001 File Offset: 0x0001D201
		protected override void CleanResources()
		{
			this.{14177}.Remove();
			base.CleanResources();
		}

		// Token: 0x04000465 RID: 1125
		public bool MouseShiftEffect;

		// Token: 0x04000466 RID: 1126
		private Rectangle[] {14174};

		// Token: 0x04000467 RID: 1127
		private ParticlesLayerRender {14175};

		// Token: 0x04000468 RID: 1128
		private ParticlePattern2D {14176};

		// Token: 0x04000469 RID: 1129
		private ParticleSystem2D {14177};

		// Token: 0x0400046A RID: 1130
		private Vector2 {14178};
	}
}
