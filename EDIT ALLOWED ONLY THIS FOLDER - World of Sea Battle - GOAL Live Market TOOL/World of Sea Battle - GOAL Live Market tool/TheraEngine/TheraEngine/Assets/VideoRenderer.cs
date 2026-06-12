using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using TheraEngine.Core;
using TheraEngine.Grphics.Device;

namespace TheraEngine.Assets
{
	// Token: 0x0200016C RID: 364
	public class VideoRenderer : DisposableObject
	{
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x0002F4EF File Offset: 0x0002D6EF
		public bool IsPlay
		{
			get
			{
				return this.{15267}.State == MediaState.Playing;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x0002F4FF File Offset: 0x0002D6FF
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x0002F50C File Offset: 0x0002D70C
		public float Speed
		{
			get
			{
				return this.{15266}.Speed;
			}
			set
			{
				this.{15266}.Speed = value;
			}
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x0002F51C File Offset: 0x0002D71C
		public VideoRenderer(string {15263}, ContentManager {15264}, float {15265})
		{
			this.{15266} = {15264}.Load<Video>({15263});
			this.{15267} = new VideoPlayer();
			this.Duration = this.{15266}.Duration;
			this.Resolution = new Vector2((float)this.{15266}.Width * 1.4f, (float)this.{15266}.Height);
			this.{15268} = new Rectangle(0, 0, this.{15266}.Width, this.{15266}.Height);
			this.{15270} = {15265};
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x0002F5AC File Offset: 0x0002D7AC
		public void RenderFrame()
		{
			if (!this.{15269})
			{
				throw new InvalidOperationException();
			}
			Texture2D texture = this.{15267}.GetTexture();
			Engine.GS.SetTexture(texture);
			switch (this.BoundsMode)
			{
			case VideoRenderer.Bounds.ScreenSizeMin:
			case VideoRenderer.Bounds.ScreenSizeMax:
			{
				Vector2 vector = Engine.GS.UIArea.WidthHeight();
				float num = (this.BoundsMode == VideoRenderer.Bounds.ScreenSizeMin) ? Math.Min(vector.X / this.Resolution.X, vector.Y / this.Resolution.Y) : Math.Max(vector.X / this.Resolution.X, vector.Y / this.Resolution.Y);
				Vector2 vector2;
				vector2.X = vector.X * 0.5f;
				vector2.Y = vector.Y * 0.5f;
				vector.X = this.Resolution.X * num;
				vector.Y = this.Resolution.Y * num;
				Vector2 vector3;
				vector3.X = vector2.X - vector.X * 0.5f;
				vector3.Y = vector2.Y - vector.Y * 0.5f;
				Device gs = Engine.GS;
				Rectangle rectangle = new Rectangle((int)vector3.X, (int)vector3.Y, (int)vector.X, (int)vector.Y);
				gs.Draw(this.{15268}, rectangle);
				break;
			}
			case VideoRenderer.Bounds.ScreenSizeStretch:
			{
				Vector2 vector = Engine.GS.UIArea.WidthHeight();
				Device gs2 = Engine.GS;
				Rectangle rectangle = new Rectangle(0, 0, (int)vector.X, (int)vector.Y);
				gs2.Draw(this.{15268}, rectangle);
				break;
			}
			case VideoRenderer.Bounds.CustomBound:
				Engine.GS.Draw(this.{15268}, this.CustomBounds);
				break;
			default:
				throw new NotSupportedException();
			}
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x060009BD RID: 2493 RVA: 0x0002F78D File Offset: 0x0002D98D
		public void Begin()
		{
			if (this.{15269})
			{
				throw new InvalidOperationException();
			}
			this.{15267}.Volume = this.{15270};
			this.{15267}.Play(this.{15266});
			this.{15269} = true;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x0002F7C6 File Offset: 0x0002D9C6
		public void End()
		{
			if (!this.{15269})
			{
				throw new InvalidOperationException();
			}
			this.{15267}.Stop();
			this.{15269} = false;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x0002F7E8 File Offset: 0x0002D9E8
		public void Resume()
		{
			if (this.{15269})
			{
				throw new InvalidOperationException();
			}
			this.{15267}.Resume();
			this.{15269} = true;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x0002F80A File Offset: 0x0002DA0A
		public void Pause()
		{
			if (!this.{15269})
			{
				throw new InvalidOperationException();
			}
			this.{15267}.Pause();
			this.{15269} = false;
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0002F82C File Offset: 0x0002DA2C
		public override void Dispose()
		{
			VideoPlayer videoPlayer = this.{15267};
			if (videoPlayer != null)
			{
				videoPlayer.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x040006A4 RID: 1700
		public VideoRenderer.Bounds BoundsMode;

		// Token: 0x040006A5 RID: 1701
		public Rectangle CustomBounds;

		// Token: 0x040006A6 RID: 1702
		public readonly Vector2 Resolution;

		// Token: 0x040006A7 RID: 1703
		public readonly TimeSpan Duration;

		// Token: 0x040006A8 RID: 1704
		private Video {15266};

		// Token: 0x040006A9 RID: 1705
		private VideoPlayer {15267};

		// Token: 0x040006AA RID: 1706
		private Rectangle {15268};

		// Token: 0x040006AB RID: 1707
		private bool {15269};

		// Token: 0x040006AC RID: 1708
		private float {15270};

		// Token: 0x0200016D RID: 365
		public enum Bounds
		{
			// Token: 0x040006AE RID: 1710
			ScreenSizeMin,
			// Token: 0x040006AF RID: 1711
			ScreenSizeMax,
			// Token: 0x040006B0 RID: 1712
			ScreenSizeStretch,
			// Token: 0x040006B1 RID: 1713
			CustomBound
		}
	}
}
