using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TheraEngine.Interface
{
	// Token: 0x02000096 RID: 150
	public class TextureHost : CustomUi
	{
		// Token: 0x06000382 RID: 898 RVA: 0x00013C9D File Offset: 0x00011E9D
		public TextureHost(Texture2D {12836}, bool {12837} = false) : base({12837})
		{
			this.{12842} = {12836};
			this.AnimatedFocus = false;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00013CB4 File Offset: 0x00011EB4
		public TextureHost(Texture2D {12838}, Marker {12839}) : base({12839}, Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp, Color.White, false)
		{
			this.{12842} = {12838};
			this.AnimatedFocus = false;
		}

		// Token: 0x06000384 RID: 900 RVA: 0x00013CD8 File Offset: 0x00011ED8
		protected override void UserBackRender()
		{
			this.{12843} = (Engine.GS.CurrentTexture != this.{12842});
			if (this.{12843})
			{
				Engine.GS.SetTexture(this.{12842});
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x00013D0D File Offset: 0x00011F0D
		protected override void UserFrontRender()
		{
			if (this.{12843})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0000C282 File Offset: 0x0000A482
		protected override void UserUpdate(ref FrameTime {12840})
		{
		}

		// Token: 0x06000387 RID: 903 RVA: 0x00013D21 File Offset: 0x00011F21
		public new TextureHost AddChild(UiControl {12841})
		{
			base.AddChild({12841});
			return this;
		}

		// Token: 0x040002EE RID: 750
		private Texture2D {12842};

		// Token: 0x040002EF RID: 751
		private bool {12843};
	}
}
