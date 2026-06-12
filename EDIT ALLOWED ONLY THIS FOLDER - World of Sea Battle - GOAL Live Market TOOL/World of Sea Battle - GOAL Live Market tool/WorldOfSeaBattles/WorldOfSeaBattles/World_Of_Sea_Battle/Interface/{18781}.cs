using System;
using System.Runtime.CompilerServices;
using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using TheraEngine;
using TheraEngine.Assets;
using TheraEngine.Assets.Audio;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000185 RID: 389
	internal sealed class {18781} : CustomUi
	{
		// Token: 0x060008D9 RID: 2265 RVA: 0x000445DC File Offset: 0x000427DC
		[NullableContext(2)]
		public static {18781} TryCreateWosbIntro()
		{
			if (Session.Account.Analytics.CohortMetricsFlags == "intro")
			{
				string {18789} = (LocaleInfo.Current.Id == Locale.Ru) ? "Video\\intro_rus" : "Video\\intro_en";
				{18781} {18781} = new {18781}("Video\\Intro_Video", {18789}, true, true, true, true);
				if (!{18781}.failed)
				{
					return {18781};
				}
			}
			return null;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00044638 File Offset: 0x00042838
		public {18781}(string {18788}, [Nullable(2)] string {18789}, bool {18790}, bool {18791}, bool {18792}, bool {18793} = true) : base(true)
		{
			{18781}.CurrentInstance = this;
			this.{18798} = {18790};
			this.{18800} = {18791};
			this.{18801} = {18792};
			this.{18799} = {18793};
			GameScene.IncreaseGameInput();
			base.EvRemoveFromContainer += this.{18795};
			try
			{
				this.{18796} = new VideoRenderer({18788}, Global.Game.Content, Global.Settings.VideoVolume);
				this.{18796}.Speed = 1.05f;
				this.{18796}.BoundsMode = VideoRenderer.Bounds.ScreenSizeStretch;
				this.{18796}.Begin();
				if ({18789} != null)
				{
					this.{18797} = new Music(Global.Game.Content.Load<Song>({18789}));
					this.{18797}.Play(1f);
				}
			}
			catch (Exception {25356})
			{
				Helpers.SendError({25356}, "VideoPlayerUi", false, false);
				this.failed = true;
				base.RemoveFromContainer();
			}
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x0004472C File Offset: 0x0004292C
		protected override void UserFrontRender()
		{
			float num = 1.5f;
			if (this.{18796} != null && this.{18796}.IsPlay && (!this.{18800} || this.{18802} > num / 2f))
			{
				if (this.{18799})
				{
					Global.Render.DenoiseEffect.Parameters["TexelSize"].SetValue(Vector2.One / this.{18796}.Resolution);
					Global.Render.DenoiseEffect.Parameters["Radius"].SetValue(14f);
					Global.Render.DenoiseEffect.Parameters["LumaBias"].SetValue(0.06f);
					Global.Render.DenoiseEffect.Parameters["Blend"].SetValue(0.9f);
					EffectParameter effectParameter = Global.Render.DenoiseEffect.Parameters["Time"];
					int num2 = this.{18806};
					this.{18806} = num2 + 1;
					effectParameter.SetValue(num2);
					Global.Render.DenoiseEffect.Parameters["InvScreenSize"].SetValue(Vector2.One / Engine.GS.UIArea.WidthHeight());
					Engine.GS.End2D();
					Engine.GS.Begin2D(true, Global.Render.DenoiseEffect, null);
					this.{18796}.RenderFrame();
					Engine.GS.End2D();
					Engine.GS.Begin2D(true);
				}
				else
				{
					this.{18796}.RenderFrame();
				}
				if (this.{18798})
				{
					Engine.GS.SetFont(Fonts.Philosopher_18);
					Device gs = Engine.GS;
					string load_screen_skip = Local.load_screen_skip;
					Vector2 vector = Engine.GS.UIArea.WidthHeight() - new Vector2(300f, 50f);
					Color color = Color.White * 0.5f;
					gs.DrawString(load_screen_skip, vector, color);
				}
			}
			if (this.{18800})
			{
				float num3 = this.{18802} / num;
				if (num3 >= 1f)
				{
					this.{18800} = false;
				}
				else
				{
					float scale = num3 * (1f - num3) * 4f;
					Device gs2 = Engine.GS;
					Texture2D tex = CommonAtlas.Texture.Tex;
					Rectangle uiarea = Engine.GS.UIArea;
					Color color = Color.Black * scale;
					gs2.DrawCustomTexture(tex, CommonAtlas.whitePixel, uiarea, color);
				}
			}
			if (!this.{18796}.IsPlay)
			{
				float num4 = (this.{18804} + num / 2f) / num;
				if (num4 >= 1f)
				{
					this.{18801} = false;
					return;
				}
				float scale2 = num4 * (1f - num4) * 4f;
				Device gs3 = Engine.GS;
				Texture2D tex2 = CommonAtlas.Texture.Tex;
				Rectangle uiarea = Engine.GS.UIArea;
				Color color = Color.Black * scale2;
				gs3.DrawCustomTexture(tex2, CommonAtlas.whitePixel, uiarea, color);
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00044A14 File Offset: 0x00042C14
		protected override void UserUpdate(ref FrameTime {18794})
		{
			if (!Global.Game.IsActive)
			{
				if (this.{18796}.IsPlay)
				{
					this.{18796}.Pause();
					this.{18805} = true;
				}
				return;
			}
			if (this.{18805})
			{
				this.{18796}.Resume();
				this.{18805} = false;
			}
			if (!this.{18796}.IsPlay)
			{
				this.{18804} += {18794}.secElapsed;
			}
			if (!this.{18796}.IsPlay && !this.{18801})
			{
				base.RemoveFromContainer();
			}
			else if ((this.{18798} && InputHelper.IsClick(Keys.Space)) || InputHelper.IsClick(Keys.Enter))
			{
				this.{18796}.End();
				Music music = this.{18797};
				if (music != null)
				{
					music.Stop();
				}
				base.RemoveFromContainer();
			}
			this.{18802} += {18794}.secElapsed;
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00044AF3 File Offset: 0x00042CF3
		[CompilerGenerated]
		private void {18795}()
		{
			VideoRenderer videoRenderer = this.{18796};
			if (videoRenderer != null)
			{
				videoRenderer.Dispose();
			}
			Music music = this.{18797};
			if (music != null)
			{
				music.Dispose();
			}
			{18781}.CurrentInstance = null;
			GameScene.DecreaseGameInput();
		}

		// Token: 0x040007D7 RID: 2007
		public static {18781} CurrentInstance;

		// Token: 0x040007D8 RID: 2008
		private readonly VideoRenderer {18796};

		// Token: 0x040007D9 RID: 2009
		private readonly Music {18797};

		// Token: 0x040007DA RID: 2010
		private readonly bool {18798};

		// Token: 0x040007DB RID: 2011
		private readonly bool {18799};

		// Token: 0x040007DC RID: 2012
		private bool {18800};

		// Token: 0x040007DD RID: 2013
		private bool {18801};

		// Token: 0x040007DE RID: 2014
		private float {18802};

		// Token: 0x040007DF RID: 2015
		private float {18803};

		// Token: 0x040007E0 RID: 2016
		private float {18804};

		// Token: 0x040007E1 RID: 2017
		private bool {18805};

		// Token: 0x040007E2 RID: 2018
		protected bool failed;

		// Token: 0x040007E3 RID: 2019
		private int {18806};
	}
}
