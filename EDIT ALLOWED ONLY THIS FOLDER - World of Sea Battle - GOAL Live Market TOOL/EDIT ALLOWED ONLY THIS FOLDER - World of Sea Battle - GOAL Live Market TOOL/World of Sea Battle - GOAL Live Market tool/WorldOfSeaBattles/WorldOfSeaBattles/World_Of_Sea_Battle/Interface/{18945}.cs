using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200019E RID: 414
	internal sealed class {18945} : CustomUi
	{
		// Token: 0x06000961 RID: 2401 RVA: 0x00049419 File Offset: 0x00047619
		public static void CloseExisted()
		{
			if ({18945}.current != null)
			{
				{18945}.current.RemoveFromContainer();
			}
			{18945}.current = null;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x00049434 File Offset: 0x00047634
		public static {18945} TryShowNotif(string {18954}, string {18955} = null, float? {18956} = null)
		{
			if ({18945}.current != null)
			{
				{19994}.Me({19988}.Info, {18954}, Array.Empty<object>());
				return null;
			}
			{18945}.current = new {18945}({18954}, {18955}, {18956}.GetValueOrDefault(), false, false, false, null, null);
			return {18945}.current;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x00049480 File Offset: 0x00047680
		public static {18945} TryShowAcceptingMode(string {18957}, string {18958}, float {18959}, Action {18960}, bool {18961} = true)
		{
			{18945} {18945} = {18945}.current;
			if ({18945} != null)
			{
				{18945}.RemoveFromContainer();
			}
			{18945}.current = new {18945}({18957}, {18958}, {18959}, {18961}, {18961}, true, null, null);
			{18945}.current.OnTimeout += {18960};
			return {18945}.current;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000964 RID: 2404 RVA: 0x000494CC File Offset: 0x000476CC
		// (remove) Token: 0x06000965 RID: 2405 RVA: 0x00049504 File Offset: 0x00047704
		public event Action OnTimeout
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{18978};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{18978}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{18978};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{18978}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x00049539 File Offset: 0x00047739
		private static float fw
		{
			get
			{
				return (float)Engine.GS.UIArea.Width;
			}
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0004954C File Offset: 0x0004774C
		private {18945}() : base(new Marker(-Math.Max(0f, {18945}.fw - (float)Engine.GS.UIArea.Width) * 0.5f, 128f, {18945}.fw, 150f * {18945}.fw / 1920f), {18945}.c_main, PositionAlignment.Both, PositionAlignment.LeftUp, Color.White * 0.7f, false)
		{
			base.RenderToDepthMap = false;
			base.EvRemoveFromContainer += delegate()
			{
				{18945}.current = null;
			};
			new UiOpacityAnimation(this, 0f, 1f, 500f);
			this.scale = base.Pos.WH.Y / 150f;
			base.AddChild(new Form(new Vector2((float)(Engine.GS.UIArea.Width / 2 - AtlasGameGui.venselNew.Width / 2), base.Pos.XY.Y - 70f), AtlasGameGui.venselNew, PositionAlignment.Center, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			});
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x00049670 File Offset: 0x00047870
		private {18945}(string {18964}, string {18965}, float {18966}, bool {18967}, bool {18968}, bool {18969} = false, Texture2D {18970} = null, Rectangle? {18971} = null) : this()
		{
			this.{18975} = ({18966} != 0f);
			this.{18973} = (this.{18975} ? {18966} : 5000f);
			this.{18974} = {18967};
			this.{18976} = {18968};
			this.{18977} = {18969};
			bool flag = !string.IsNullOrEmpty({18965});
			Label label = new Label(new Vector2((float)(Engine.GS.UIArea.Width / 2), base.Pos.XY.Y + (float)(flag ? 36 : 46) * this.scale), Fonts.Philosopher_24, Color.White, {18964}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center();
			label.Shadowed = true;
			base.AddChild(label);
			if (flag)
			{
				Label {13204} = new Label(new Vector2((float)(Engine.GS.UIArea.Width / 2), label.Pos.XY.Y + 45f), Fonts.Philosopher_14, Color.White * 0.7f, {18965}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center();
				base.AddChild({13204});
			}
			if ({18971} != null)
			{
				if (this.{18975})
				{
					throw new ArgumentException();
				}
				Marker {13271} = Marker.FromCentrScreen(base.Pos.WH, new Vector2(53f * this.scale, 53f * this.scale)).Offset(base.Pos.XY);
				base.AddChild(new Image({13271}, {18970}, {18971}.Value, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x000497F8 File Offset: 0x000479F8
		protected override void UserUpdate(ref FrameTime {18972})
		{
			if (!{18972}.EvaluteTimerMs2(ref this.{18973}))
			{
				if (this.{18973} < 1000f)
				{
					base.Opacity = this.{18973} / 1000f;
				}
				if (this.{18974} && this.{18973} < 1500f)
				{
					Global.Render.PostProcess.GradientAnimationBegin(1500f, false, true);
					this.{18974} = false;
				}
				if (this.{18977})
				{
					if (InputHelper.NowInputState.DownKeys.Count((Keys {18980}) => ({18980} >= Keys.A && {18980} <= Keys.Z) || {18980} == Keys.Escape || {18980} == Keys.Space || {18980} == Keys.LeftAlt || {18980} == Keys.RightAlt) > 0)
					{
						base.RemoveFromContainer();
						return;
					}
				}
				return;
			}
			if (this.{18976})
			{
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Bell, 0.03f, 1f);
			}
			base.RemoveFromContainer();
			Action action = this.{18978};
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x000498E0 File Offset: 0x00047AE0
		protected override void UserBackRender()
		{
			if (this.{18979} = (Engine.GS.CurrentTexture != AtlasGameGui.Texture.Tex))
			{
				Engine.GS.SetTexture(AtlasGameGui.Texture);
			}
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x00049920 File Offset: 0x00047B20
		protected override void UserFrontRender()
		{
			if (this.{18975})
			{
				float x = this.{18973} % 1000f / 1000f;
				string text = Math.Ceiling((double)(this.{18973} / 1000f)).ToString();
				Engine.GS.SetFont((this.scale < 0.6f) ? Fonts.Philosopher_24 : Fonts.Philosopher_36);
				Device gs = Engine.GS;
				string {14610} = text;
				Vector2 vector = new Vector2((float)(Engine.GS.UIArea.Width / 2), base.Pos.XY.Y + 108f * this.scale);
				Color color = Color.Lerp(Color.SkyBlue, Color.White, MathF.Sqrt(x));
				gs.DrawStringCentered({14610}, vector, color);
			}
			if (this.{18979})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x0400086E RID: 2158
		private static readonly Rectangle c_main = new Rectangle(2240, 1900, 938, 75);

		// Token: 0x0400086F RID: 2159
		private static {18945} current;

		// Token: 0x04000870 RID: 2160
		private float {18973};

		// Token: 0x04000871 RID: 2161
		private bool {18974};

		// Token: 0x04000872 RID: 2162
		private bool {18975};

		// Token: 0x04000873 RID: 2163
		private bool {18976};

		// Token: 0x04000874 RID: 2164
		private bool {18977};

		// Token: 0x04000875 RID: 2165
		[CompilerGenerated]
		private Action {18978};

		// Token: 0x04000876 RID: 2166
		public float scale;

		// Token: 0x04000877 RID: 2167
		private bool {18979};
	}
}
