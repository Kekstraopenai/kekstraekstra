using System;
using Microsoft.Xna.Framework;
using TheraEngine.Components.Architecture;
using TheraEngine.Grphics.Device;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x0200019B RID: 411
	public class Sprite : IUpdateableObject
	{
		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x00034824 File Offset: 0x00032A24
		public Rectangle CurrentFrame
		{
			get
			{
				return this.{15878}[this.{15881}];
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000A75 RID: 2677 RVA: 0x00034837 File Offset: 0x00032A37
		public float CurrentCycleTtl
		{
			get
			{
				return this.{15882};
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000A76 RID: 2678 RVA: 0x0003483F File Offset: 0x00032A3F
		public float AllTtl
		{
			get
			{
				return this.{15883};
			}
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x00034848 File Offset: 0x00032A48
		public Sprite(Rectangle {15870}, int {15871}, float {15872}, int {15873} = 0)
		{
			this.{15878} = new Rectangle[{15871}];
			this.{15879} = {15872};
			this.{15880} = (float){15871};
			this.SpriteColor = Color.White;
			for (int i = 0; i < {15871}; i++)
			{
				this.{15878}[i] = new Rectangle({15870}.X + {15870}.Width * i + {15873} * i, {15870}.Y, {15870}.Width, {15870}.Height);
			}
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x000348C4 File Offset: 0x00032AC4
		public void Update(ref FrameTime {15874})
		{
			this.{15883} += {15874}.msElapsed;
			this.{15882} += {15874}.msElapsed;
			if (this.{15882} > this.{15879})
			{
				if (!this.IsLoop)
				{
					this.{15882} = this.{15879};
					this.{15881} = (int)this.{15880} - 1;
					return;
				}
				this.{15882} -= this.{15879};
			}
			this.{15881} = Math.Min((int)this.{15880} - 1, (int)(this.{15882} / this.{15879} * this.{15880}));
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x00034967 File Offset: 0x00032B67
		public void SetStart()
		{
			this.{15882} = 0f;
			this.{15883} = 0f;
			this.{15881} = 0;
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x00034986 File Offset: 0x00032B86
		public void InterpolateFrame(float {15875})
		{
			this.SetStart();
			this.{15881} = (int)Math.Max(0.0, Math.Min((double)(this.{15880} - 1f), Math.Round((double)({15875} * this.{15880}))));
		}

		// Token: 0x06000A7B RID: 2683 RVA: 0x000349C4 File Offset: 0x00032BC4
		public void Render(Vector2 {15876})
		{
			Device gs = Engine.GS;
			Rectangle currentFrame = this.CurrentFrame;
			gs.Draw(currentFrame, {15876}, this.SpriteColor);
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x000349EC File Offset: 0x00032BEC
		public void Render(Marker {15877})
		{
			Device gs = Engine.GS;
			Rectangle currentFrame = this.CurrentFrame;
			Rectangle rectangle = {15877}.ToRect();
			gs.Draw(currentFrame, rectangle, this.SpriteColor);
		}

		// Token: 0x040007FE RID: 2046
		public bool IsLoop;

		// Token: 0x040007FF RID: 2047
		public Color SpriteColor;

		// Token: 0x04000800 RID: 2048
		private readonly Rectangle[] {15878};

		// Token: 0x04000801 RID: 2049
		private readonly float {15879};

		// Token: 0x04000802 RID: 2050
		private readonly float {15880};

		// Token: 0x04000803 RID: 2051
		private int {15881};

		// Token: 0x04000804 RID: 2052
		private float {15882};

		// Token: 0x04000805 RID: 2053
		private float {15883};
	}
}
