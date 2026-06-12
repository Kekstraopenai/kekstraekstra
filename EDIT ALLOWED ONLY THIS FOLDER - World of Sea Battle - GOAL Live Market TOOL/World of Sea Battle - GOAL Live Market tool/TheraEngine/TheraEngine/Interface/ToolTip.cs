using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;

namespace TheraEngine.Interface
{
	// Token: 0x02000083 RID: 131
	public sealed class ToolTip
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00011D5F File Offset: 0x0000FF5F
		public static bool IsActiveAny
		{
			get
			{
				return ToolTip.showFlags != 0;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00011D69 File Offset: 0x0000FF69
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00011D71 File Offset: 0x0000FF71
		public float TimeToShow
		{
			get
			{
				return this.{12691};
			}
			set
			{
				this.{12691} = MathHelper.Clamp(value, 0f, this.{12692});
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600032F RID: 815 RVA: 0x00011D8A File Offset: 0x0000FF8A
		// (set) Token: 0x06000330 RID: 816 RVA: 0x00011D92 File Offset: 0x0000FF92
		public float TimeToHide
		{
			get
			{
				return this.{12692};
			}
			set
			{
				this.{12692} = MathHelper.Clamp(value, this.{12691}, 300000f);
			}
		}

		// Token: 0x06000331 RID: 817 RVA: 0x00011DAB File Offset: 0x0000FFAB
		public ToolTip(Func<UiControl, ToolTipState> {12675}) : this(50f, float.MaxValue, null)
		{
			this.PromisedContent = {12675};
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00011DC5 File Offset: 0x0000FFC5
		public ToolTip(ToolTipState {12676}) : this(50f, float.MaxValue, {12676})
		{
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00011DD8 File Offset: 0x0000FFD8
		public ToolTip(float {12677}, float {12678}, ToolTipState {12679} = null)
		{
			this.CurrentContent = {12679};
			this.TexturePath = ToolTip.Texture;
			this.TimeToHide = {12678};
			this.TimeToShow = {12677};
			this.{12697} = this.{12691};
			this.{12694} = false;
			this.{12693} = false;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00011E30 File Offset: 0x00010030
		public ToolTip AddBackgroundEffect(ToolTipBackgroundEffect {12680}, Color {12681})
		{
			if ({12680} == ToolTipBackgroundEffect.None)
			{
				throw new ArgumentException();
			}
			this.{12698} = {12680};
			this.{12699} = {12681};
			return this;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00011E4A File Offset: 0x0001004A
		public void CloseIfIsOpen()
		{
			if (this.{12694})
			{
				this.{12688}();
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00011E5C File Offset: 0x0001005C
		public void RefreshIfIsOpen()
		{
			if (this.{12694})
			{
				UiControl {12687} = this.{12695};
				this.{12688}();
				this.{12686}({12687});
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00011E88 File Offset: 0x00010088
		internal void Update(float {12682}, UiControl {12683})
		{
			if ({12683}.InputMode != MouseInputMode.NoFocus && Engine.Game.IsMouseVisible)
			{
				if (!this.{12694})
				{
					if (this.{12693} || !this.{12684}({12683}))
					{
						this.{12697} = this.{12691};
						return;
					}
					this.{12697} -= {12682};
					if (this.{12697} < 1f)
					{
						this.{12686}({12683});
						return;
					}
				}
				else
				{
					Vector2 vector = this.FixedShowPosition ?? (this.{12696}.Pos.XY + Engine.GS.MouseToUI - Engine.GS.MouseToUIPrev);
					Marker marker = new Marker(ref vector, ref this.{12696}.Pos.WH);
					marker.XY.Y = Math.Max(0f, Math.Min(marker.XY.Y, (float)Engine.GS.UIArea.Height - marker.WH.Y));
					this.{12696}.Pos = marker;
					this.{12697} -= {12682};
					if (this.{12697} < 1f)
					{
						this.{12688}();
						return;
					}
				}
			}
			else
			{
				this.{12693} = false;
				if (this.{12694})
				{
					this.{12688}();
					return;
				}
				this.{12697} = this.{12691};
			}
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00011FF0 File Offset: 0x000101F0
		private bool {12684}(UiControl {12685})
		{
			for (int i = 0; i < {12685}.GetChildren.Size; i++)
			{
				UiControl uiControl = {12685}.GetChildren.Array[i];
				if (uiControl.ToolTip != null && uiControl.ToolTip.{12694})
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0001203C File Offset: 0x0001023C
		private void {12686}(UiControl {12687})
		{
			if (this.PromisedContent != null)
			{
				this.CurrentContent = this.PromisedContent({12687});
			}
			Action updateElementFunc = this.CurrentContent.updateElementFunc;
			if (updateElementFunc != null)
			{
				updateElementFunc();
			}
			this.{12693} = true;
			TextBlockControl textBlockControl = this.CurrentContent.builder.Create(Vector2.Zero);
			Vector2 wh = textBlockControl.Pos.WH;
			if (this.CurrentContent.addElements != null)
			{
				for (int i = 0; i < this.CurrentContent.addElements.Size; i++)
				{
					UiControl uiControl = this.CurrentContent.addElements.Array[i];
					wh.X = Math.Max(wh.X, uiControl.Pos.XY.X + uiControl.Pos.WH.X);
					wh.Y = Math.Max(wh.Y, uiControl.Pos.XY.Y + uiControl.Pos.WH.Y);
				}
			}
			Vector2 vector;
			vector.X = 50f;
			vector.Y = 50f;
			Marker marker = this.{12689}(wh);
			textBlockControl.Pos = marker;
			Vector2 vector2 = marker.XY - vector;
			Vector2 vector3 = marker.WH + vector * 2f;
			this.{12696} = new UnionFormControl(new Marker(ref vector2, ref vector3), this.TexturePath, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{12696}.overrideTexture = ToolTip.TextureObj;
			if (this.{12698} == ToolTipBackgroundEffect.LightedCorner)
			{
				this.{12696}.AddChild(new UiControl[]
				{
					new Form(this.{12696}.Pos.XY, ToolTip.LightedCornerTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						BasicColor = this.{12699},
						overrideTexture = ToolTip.TextureObj
					}
				});
			}
			if (this.CurrentContent.requiredBackgroundHeaderEffect)
			{
				this.{12696}.AddChild(new UiControl[]
				{
					new Form(new Marker(marker.XY.X, marker.XY.Y - 10f, marker.WH.X, (float)ToolTip.HeaderLineTexture.Height), ToolTip.HeaderLineTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false,
						overrideTexture = ToolTip.TextureObj,
						BasicColor = Color.White * 0.7f
					}
				});
			}
			if (this.CurrentContent.addElements != null)
			{
				for (int j = 0; j < this.CurrentContent.addElements.Size; j++)
				{
					UiControl uiControl2 = this.CurrentContent.addElements.Array[j];
					Marker pos = this.CurrentContent.addElements.Array[j].Pos;
					vector2 = this.{12696}.Pos.XY + vector;
					uiControl2.Pos = pos.Offset(vector2);
					this.CurrentContent.addElements.Array[j].RenderToDepthMap = false;
					this.{12696}.AddChild(new UiControl[]
					{
						this.CurrentContent.addElements.Array[j]
					});
				}
				this.CurrentContent.addElements.Clear();
			}
			if (this.CurrentContent.addElements == null && this.CurrentContent.builder.CurrentLocalPoint.Y == 0f)
			{
				this.{12696}.AddChildPos(textBlockControl, PositionAlignment.Center, PositionAlignment.Center, 10f, 0f, false);
			}
			else
			{
				this.{12696}.AddChild(new UiControl[]
				{
					textBlockControl
				});
			}
			this.{12696}.RenderToDepthMap = false;
			this.{12696}.AnimatedFocus = false;
			textBlockControl.RenderToDepthMap = false;
			this.{12694} = true;
			this.{12695} = {12687};
			this.{12697} = this.{12692};
			ToolTip.showFlags++;
			if ({12687}.GetParent != null && {12687}.GetParent.ToolTip != null && {12687}.GetParent.ToolTip.{12694})
			{
				{12687}.GetParent.ToolTip.{12688}();
				{12687}.GetParent.ToolTip.{12693} = false;
			}
			new UiOpacityAnimation(this.{12696}, 0f, this.Opacity, 200f);
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001248A File Offset: 0x0001068A
		private void {12688}()
		{
			this.{12694} = false;
			this.{12695} = null;
			this.{12696}.RemoveFromContainer();
			this.{12696} = null;
			this.{12697} = this.{12691};
			ToolTip.showFlags--;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00011E4A File Offset: 0x0001004A
		internal void Shutdown()
		{
			if (this.{12694})
			{
				this.{12688}();
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x000124C4 File Offset: 0x000106C4
		private Marker {12689}(Vector2 {12690})
		{
			Vector2 vector = this.FixedShowPosition ?? (Engine.GS.MouseToUI + new Vector2(10f));
			Marker marker = new Marker(ref vector, ref {12690});
			if (marker.XY.X + marker.WH.X + 15f > (float)Engine.GS.UIArea.Width)
			{
				marker.XY.X = marker.XY.X - (marker.WH.X + 10f);
			}
			if (marker.XY.Y + marker.WH.Y + 50f > (float)Engine.GS.UIArea.Height)
			{
				marker.XY.Y = marker.XY.Y - (marker.WH.Y + 10f);
			}
			marker.XY.Y = Math.Max(0f, Math.Min(marker.XY.Y, (float)Engine.GS.UIArea.Height - marker.WH.Y));
			if (marker.WH.X % 2f == 1f)
			{
				marker.WH.X = marker.WH.X + 1f;
			}
			if (marker.WH.Y % 2f == 1f)
			{
				marker.WH.Y = marker.WH.Y + 1f;
			}
			marker.XY += new Vector2(10f);
			return marker;
		}

		// Token: 0x04000290 RID: 656
		public object Tag;

		// Token: 0x04000291 RID: 657
		private static int showFlags;

		// Token: 0x04000292 RID: 658
		public static ExpandoTexturePath Texture;

		// Token: 0x04000293 RID: 659
		public static Rectangle LightedCornerTexture;

		// Token: 0x04000294 RID: 660
		public static Rectangle HeaderLineTexture;

		// Token: 0x04000295 RID: 661
		public static Texture2D TextureObj;

		// Token: 0x04000296 RID: 662
		public ExpandoTexturePath TexturePath;

		// Token: 0x04000297 RID: 663
		public Vector2? FixedShowPosition;

		// Token: 0x04000298 RID: 664
		public Func<UiControl, ToolTipState> PromisedContent;

		// Token: 0x04000299 RID: 665
		public ToolTipState CurrentContent;

		// Token: 0x0400029A RID: 666
		public float Opacity = 1f;

		// Token: 0x0400029B RID: 667
		private float {12691};

		// Token: 0x0400029C RID: 668
		private float {12692};

		// Token: 0x0400029D RID: 669
		private bool {12693};

		// Token: 0x0400029E RID: 670
		private bool {12694};

		// Token: 0x0400029F RID: 671
		private UiControl {12695};

		// Token: 0x040002A0 RID: 672
		private UnionFormControl {12696};

		// Token: 0x040002A1 RID: 673
		private float {12697};

		// Token: 0x040002A2 RID: 674
		private ToolTipBackgroundEffect {12698};

		// Token: 0x040002A3 RID: 675
		private Color {12699};
	}
}
