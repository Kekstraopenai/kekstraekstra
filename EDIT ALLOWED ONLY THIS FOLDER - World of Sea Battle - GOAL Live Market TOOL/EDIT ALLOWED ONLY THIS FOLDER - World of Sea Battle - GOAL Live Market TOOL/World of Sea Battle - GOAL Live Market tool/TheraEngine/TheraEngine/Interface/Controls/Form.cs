using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000A7 RID: 167
	public class Form : UiControl
	{
		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000447 RID: 1095 RVA: 0x00016DAC File Offset: 0x00014FAC
		// (remove) Token: 0x06000448 RID: 1096 RVA: 0x00016DE4 File Offset: 0x00014FE4
		public event Func<Vector2, bool> EvIntegerDrop
		{
			[CompilerGenerated]
			add
			{
				Func<Vector2, bool> func = this.{13208};
				Func<Vector2, bool> func2;
				do
				{
					func2 = func;
					Func<Vector2, bool> value2 = (Func<Vector2, bool>)Delegate.Combine(func2, value);
					func = Interlocked.CompareExchange<Func<Vector2, bool>>(ref this.{13208}, value2, func2);
				}
				while (func != func2);
			}
			[CompilerGenerated]
			remove
			{
				Func<Vector2, bool> func = this.{13208};
				Func<Vector2, bool> func2;
				do
				{
					func2 = func;
					Func<Vector2, bool> value2 = (Func<Vector2, bool>)Delegate.Remove(func2, value);
					func = Interlocked.CompareExchange<Func<Vector2, bool>>(ref this.{13208}, value2, func2);
				}
				while (func != func2);
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000449 RID: 1097 RVA: 0x00016E1C File Offset: 0x0001501C
		// (remove) Token: 0x0600044A RID: 1098 RVA: 0x00016E54 File Offset: 0x00015054
		public event Action EvDrop
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{13209};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13209}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{13209};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{13209}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600044B RID: 1099 RVA: 0x00016E89 File Offset: 0x00015089
		public Form(Marker {13184}, Rectangle {13185}, Color {13186}, PositionAlignment {13187} = PositionAlignment.LeftUp, PositionAlignment {13188} = PositionAlignment.LeftUp) : base({13184}, {13187}, {13188}, {13186}, false)
		{
			this.TexturePath = {13185};
			this.textureAddColor = 1f;
			this.AnimatedFocus = true;
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x00016EB4 File Offset: 0x000150B4
		public Form(Vector2 {13189}, Rectangle {13190}, PositionAlignment {13191} = PositionAlignment.LeftUp, PositionAlignment {13192} = PositionAlignment.LeftUp)
		{
			Vector2 vector = new Vector2((float){13190}.Width, (float){13190}.Height);
			this..ctor(new Marker(ref {13189}, ref vector), {13190}, Color.White, {13191}, {13192});
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00016EED File Offset: 0x000150ED
		public Form(Marker {13193}, Rectangle {13194}, PositionAlignment {13195} = PositionAlignment.LeftUp, PositionAlignment {13196} = PositionAlignment.LeftUp) : this({13193}, {13194}, Color.White, {13195}, {13196})
		{
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00016EFF File Offset: 0x000150FF
		public Form(Marker {13197}, PositionAlignment {13198} = PositionAlignment.LeftUp, PositionAlignment {13199} = PositionAlignment.LeftUp) : this({13197}, Rectangle.Empty, Color.White, {13198}, {13199})
		{
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00016F14 File Offset: 0x00015114
		internal override void Update(ref FrameTime {13200}, ref int {13201})
		{
			base.Update(ref {13200}, ref {13201});
			if (this.AllowDragDrop && ((base.RenderToDepthMap && base.InputMode == MouseInputMode.Down) || (this.{13211} && InputHelper.NowMouseState.LeftPressed)))
			{
				Vector2 vector = Engine.GS.MouseToUI - Engine.GS.MouseToUIPrev;
				Vector2.Add(ref this.{13210}, ref vector, out this.{13210});
				if (this.{13211})
				{
					Vector2 vector2;
					vector2.X = (float)((int)this.{13210}.X);
					this.{13210}.X = this.{13210}.X - vector2.X;
					vector2.Y = (float)((int)this.{13210}.Y);
					this.{13210}.Y = this.{13210}.Y - vector2.Y;
					if (this.{13208} == null)
					{
						base.Pos = base.Pos.Offset(vector2);
					}
					else if (this.{13208}(vector2))
					{
						base.Pos = base.Pos.Offset(vector2);
					}
				}
				else if (this.{13210}.Length() > 3f)
				{
					this.{13210} = Vector2.Zero;
					this.{13211} = true;
				}
			}
			else
			{
				if (this.{13211} && this.{13209} != null)
				{
					Action action = this.{13209};
					if (action != null)
					{
						action();
					}
				}
				this.{13211} = false;
				this.{13210} = Vector2.Zero;
			}
			if (this.AnimatedFocus)
			{
				this.textureAddColor = ((base.InputMode == MouseInputMode.NoFocus && this.AnimatedFocus) ? 0.7411f : 1f);
				this.ownExtraBrightness = this.{13212} / 370f * 0.3f;
				if (this.{13212} > 1f)
				{
					this.{13212} = Math.Max(1f, this.{13212} - {13200}.msElapsed);
				}
				if (base.InputMode == MouseInputMode.Focused && this.{13212} == 0f)
				{
					this.{13212} = 370f;
				}
				if (base.InputMode != MouseInputMode.Focused && this.{13212} == 1f)
				{
					this.{13212} = 0f;
					return;
				}
			}
			else
			{
				this.textureAddColor = 1f;
				this.ownExtraBrightness = 0f;
			}
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x00017150 File Offset: 0x00015350
		internal override void Render()
		{
			if (this.overrideTexture != null)
			{
				Device gs = Engine.GS;
				Texture2D {14570} = this.overrideTexture;
				Rectangle rectangle = base.Pos.ToRect();
				Color color = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor * this.textureAddColor);
				gs.DrawCustomTexture({14570}, this.TexturePath, rectangle, color);
			}
			else if (this.TexturePath.Width > 0)
			{
				Device gs2 = Engine.GS;
				Rectangle rectangle = base.Pos.ToRect();
				Color color = UiControl.ComputeColor(base.GetBrightness(), base.GetOpcaity(), this.BasicColor * this.textureAddColor);
				gs2.Draw(this.TexturePath, rectangle, color);
			}
			base.Render();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0001720D File Offset: 0x0001540D
		public new void AddChild(params UiControl[] {13202})
		{
			base.AddChild({13202});
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00017216 File Offset: 0x00015416
		public void AddChild(IEnumerable<UiControl> {13203})
		{
			base.AddChild({13203}.ToArray<UiControl>());
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x0001680A File Offset: 0x00014A0A
		public new void AddChild(UiControl {13204})
		{
			base.AddChild({13204});
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x00017224 File Offset: 0x00015424
		public Form AddChild(Rectangle {13205}, Marker {13206})
		{
			Form form = new Form({13206}, {13205}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			this.AddChild(form);
			return form;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001724A File Offset: 0x0001544A
		public void RemoveChild(UiControl {13207})
		{
			base.RemoveAt({13207}, true);
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x00013C4B File Offset: 0x00011E4B
		public new void ClearAllChild()
		{
			base.ClearAllChild();
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000DD52 File Offset: 0x0000BF52
		protected override bool AllowResize
		{
			get
			{
				return true;
			}
		}

		// Token: 0x04000353 RID: 851
		private const float c_addColor_NoFocus = 0.7411f;

		// Token: 0x04000354 RID: 852
		private const float c_addColor_Focus = 1f;

		// Token: 0x04000355 RID: 853
		private const float c_brightnessEffectDurationMs = 370f;

		// Token: 0x04000356 RID: 854
		[CompilerGenerated]
		private Func<Vector2, bool> {13208};

		// Token: 0x04000357 RID: 855
		[CompilerGenerated]
		private Action {13209};

		// Token: 0x04000358 RID: 856
		internal Texture2D overrideTexture;

		// Token: 0x04000359 RID: 857
		public Rectangle TexturePath;

		// Token: 0x0400035A RID: 858
		public bool AnimatedFocus;

		// Token: 0x0400035B RID: 859
		public bool AllowDragDrop;

		// Token: 0x0400035C RID: 860
		protected float textureAddColor;

		// Token: 0x0400035D RID: 861
		private Vector2 {13210};

		// Token: 0x0400035E RID: 862
		private bool {13211};

		// Token: 0x0400035F RID: 863
		private float {13212};
	}
}
