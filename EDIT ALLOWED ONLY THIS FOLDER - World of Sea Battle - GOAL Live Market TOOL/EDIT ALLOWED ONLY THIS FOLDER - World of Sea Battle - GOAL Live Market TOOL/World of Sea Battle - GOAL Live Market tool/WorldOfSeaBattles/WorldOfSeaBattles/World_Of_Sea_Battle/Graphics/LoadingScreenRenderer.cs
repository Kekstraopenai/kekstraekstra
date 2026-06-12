using System;
using System.Threading;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets;
using TheraEngine.Components;
using TheraEngine.Core;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using WorldOfSeaBattles;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x02000447 RID: 1095
	internal sealed class LoadingScreenRenderer
	{
		// Token: 0x060017A0 RID: 6048 RVA: 0x000CABEB File Offset: 0x000C8DEB
		public LoadingScreenRenderer()
		{
			this.{23382} = false;
		}

		// Token: 0x060017A1 RID: 6049 RVA: 0x000CABFC File Offset: 0x000C8DFC
		public void Initialize(ContentManager {23379})
		{
			this.{23380} = {23379}.Load<Texture2D>(CalendarEvents.IsHalloween ? "Textures\\scld\\Hellowen_25" : (CalendarEvents.IsNewYear ? "Textures\\scld\\MainScreenEventNewYear" : (Rand.Chanse(50f) ? "Textures\\scld\\MainScreen" : "Textures\\scld\\MainScreen2")));
			this.{23381} = {23379}.Load<Texture2D>(PathContent.dir_textures + "scld\\LoadingPallete");
			this.{23382} = true;
			this.{23385} = Local.Current("loading_screen_" + Rand.RangeInt(1, 11).ToString());
			this.{23386} = Fonts.Philosopher_24.Measure(Local.LoadingScreenRenderer_0);
			this.{23387} = Fonts.Philosopher_16.Measure(this.{23385});
			if (false)
			{
				this.{23388} = new VideoRenderer("Video\\logo", {23379}, Global.Settings.MusicVolume);
				this.{23388}.BoundsMode = VideoRenderer.Bounds.ScreenSizeMax;
				try
				{
					this.{23388}.Begin();
				}
				catch (Exception)
				{
					this.{23388} = null;
				}
			}
			int num = (int)((float)this.{23380}.Height / LoadingScreenRenderer.c_loadScreenSize.Y);
			if (this.{23380}.Width > this.{23380}.Height)
			{
				this.{23389} = this.{23380}.Bounds;
				return;
			}
			this.{23389} = new Marker(0f, (float)(Global.Settings.LaunchCount % num) * LoadingScreenRenderer.c_loadScreenSize.Y, ref LoadingScreenRenderer.c_loadScreenSize).ToRect();
		}

		// Token: 0x060017A2 RID: 6050 RVA: 0x000CAD84 File Offset: 0x000C8F84
		public void Upload()
		{
			this.{23382} = false;
			this.{23380}.Dispose();
			this.{23380} = null;
			this.{23381}.Dispose();
			this.{23381} = null;
			VideoRenderer videoRenderer = this.{23388};
			if (videoRenderer == null)
			{
				return;
			}
			videoRenderer.Dispose();
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x000CADC4 File Offset: 0x000C8FC4
		public void TryDraw()
		{
			if (this.{23382})
			{
				if (this.{23388} != null && this.{23388}.IsPlay)
				{
					Engine.GS.ResetRenderTargets();
					Engine.GS.ClearRenderTarget(Color.Black);
					Engine.GS.Begin2D(true);
					this.{23388}.RenderFrame();
					if (!Global.Settings.IsFirstLaunch)
					{
						Engine.GS.SetFont(Fonts.Philosopher_18);
						Device gs = Engine.GS;
						string load_screen_skip = Local.load_screen_skip;
						Vector2 vector = Engine.GS.UIArea.WidthHeight() - new Vector2(300f, 50f);
						Color color = Color.White * 0.5f;
						gs.DrawString(load_screen_skip, vector, color);
					}
					Engine.GS.End2D();
					if (InputHelper.IsClick(Keys.Space) || InputHelper.IsClick(Keys.Enter))
					{
						this.{23388}.End();
						return;
					}
					return;
				}
				else
				{
					LoadingScreenRenderer.waitEndVideo.Set();
					this.{23384} += Global.Game.GameTime.ElapsedDrawReal;
					float num = (this.{23384} == 0f) ? 1f : ((this.{23384} < 1000f) ? ((this.{23384} < 500f) ? (this.{23384} / 500f) : ((this.{23384} < 500f) ? 1f : (1f - (this.{23384} - 500f - 0f) / 500f))) : 0f);
					float num2 = (this.{23384} > 500f) ? (1f - num) : 0f;
					try
					{
						Engine.GS.ResetRenderTargets();
						Engine.GS.ClearRenderTarget(Color.Black);
						Engine.GS.Begin2D(true);
						Engine.GS.SetTexture(this.{23380});
						Vector2 vector2 = Engine.GS.UIArea.WidthHeight() / LoadingScreenRenderer.c_loadScreenSize;
						Vector2 vector;
						Rectangle rectangle;
						if (CalendarEvents.IsNewYear)
						{
							float num3 = Math.Max(vector2.X, vector2.Y);
							Device gs2 = Engine.GS;
							Vector2 {11591} = Engine.GS.UIArea.WidthHeight();
							float {11535} = 0f;
							float {11536} = 0f;
							vector = LoadingScreenRenderer.c_loadScreenSize * num3;
							rectangle = Marker.FromCentrScreen({11591}, new Marker({11535}, {11536}, ref vector).ToRect()).Offset(0f, ((float)Engine.GS.UIArea.Height - LoadingScreenRenderer.c_loadScreenSize.Y * num3) / 2f).ToRect();
							gs2.Draw(this.{23389}, rectangle);
						}
						else
						{
							float scaleFactor = Math.Min(vector2.X, vector2.Y) * 0.87f;
							Device gs3 = Engine.GS;
							Vector2 {11591}2 = Engine.GS.UIArea.WidthHeight();
							float {11535}2 = 0f;
							float {11536}2 = 0f;
							vector = LoadingScreenRenderer.c_loadScreenSize * scaleFactor;
							rectangle = Marker.FromCentrScreen({11591}2, new Marker({11535}2, {11536}2, ref vector).ToRect()).ToRect();
							gs3.Draw(this.{23389}, rectangle);
						}
						new Vector2(114f, (float)(Engine.GS.UIArea.Height - 90));
						Engine.GS.SetFont(Fonts.Philosopher_16);
						Device gs4 = Engine.GS;
						string {14599} = this.{23385};
						vector = new Vector2((float)(Engine.GS.UIArea.Width / 2) - this.{23387}.X / 2f, (float)(Engine.GS.UIArea.Height - 38));
						Color color = Color.Cyan * 0.2f;
						gs4.DrawString({14599}, vector, color);
						Device gs5 = Engine.GS;
						string {14599}2 = this.{23385};
						vector = new Vector2((float)(Engine.GS.UIArea.Width / 2) - this.{23387}.X / 2f, (float)(Engine.GS.UIArea.Height - 40));
						color = Color.White;
						gs5.DrawString({14599}2, vector, color);
						Engine.GS.SetFont(Fonts.Arial_9);
						Device gs6 = Engine.GS;
						string {14599}3 = "... " + ContentManager.CurrentLoadingAsset;
						vector = new Vector2(5f);
						color = Color.Gray * 0.5f;
						gs6.DrawString({14599}3, vector, color);
						if (num != 0f)
						{
							float num4 = this.{23384} / 3000f;
							Engine.GS.SetTexture(this.{23381});
							Device gs7 = Engine.GS;
							rectangle = Engine.GS.UIArea;
							color = Color.Black * (1f - num2 * num2);
							gs7.Draw(LoadingScreenRenderer.c_black, rectangle, color);
						}
						Engine.GS.SetTexture(this.{23381});
						Device gs8 = Engine.GS;
						rectangle = this.{23381}.Bounds;
						vector = new Vector2((float)(Engine.GS.UIArea.Width / 2), (float)(Engine.GS.UIArea.Height - this.{23381}.Height - 2));
						Vector2 vector3 = this.{23381}.Bounds.HalfWidthHeight();
						float {14558} = (float)Global.Game.GameTotalTimeSec * 0.5f;
						float {14559} = 1.25f;
						color = Color.Black;
						gs8.Draw(rectangle, vector, vector3, {14558}, {14559}, color);
						Device gs9 = Engine.GS;
						rectangle = this.{23381}.Bounds;
						vector = new Vector2((float)(Engine.GS.UIArea.Width / 2), (float)(Engine.GS.UIArea.Height - this.{23381}.Height));
						vector3 = this.{23381}.Bounds.HalfWidthHeight();
						float {14558}2 = (float)Global.Game.GameTotalTimeSec * 0.5f;
						float {14559}2 = 1.25f;
						color = new Color(1f, 1f, 1f, 0.5f);
						gs9.Draw(rectangle, vector, vector3, {14558}2, {14559}2, color);
						if (LoadingScreenRenderer.LoadingCompleted)
						{
							this.{23383} += Global.Game.GameTime.ElapsedDrawReal;
							Device gs10 = Engine.GS;
							rectangle = Engine.GS.UIArea;
							color = Color.Black * (this.{23383} / 1000f);
							gs10.Draw(LoadingScreenRenderer.c_black, rectangle, color);
						}
						if (this.{23390} != null)
						{
							Device gs11 = Engine.GS;
							Texture2D {14570} = this.{23390};
							rectangle = this.{23390}.Bounds;
							Rectangle rectangle2 = this.{23390}.Bounds;
							color = Color.White;
							gs11.DrawCustomTexture({14570}, rectangle, rectangle2, color);
							Device gs12 = Engine.GS;
							Texture2D {14570}2 = this.{23390};
							rectangle = this.{23390}.Bounds;
							rectangle2 = new Rectangle(this.{23390}.Bounds.Width, 0, this.{23390}.Bounds.Width / 2, this.{23390}.Bounds.Height / 2);
							color = Color.White;
							gs12.DrawCustomTexture({14570}2, rectangle, rectangle2, color);
							Device gs13 = Engine.GS;
							Texture2D {14570}3 = this.{23390};
							rectangle = this.{23390}.Bounds;
							rectangle2 = new Rectangle(this.{23390}.Bounds.Width + this.{23390}.Bounds.Width / 2, 0, this.{23390}.Bounds.Width / 4, this.{23390}.Bounds.Height / 4);
							color = Color.White;
							gs13.DrawCustomTexture({14570}3, rectangle, rectangle2, color);
							Device gs14 = Engine.GS;
							Texture2D {14570}4 = this.{23390};
							rectangle = this.{23390}.Bounds;
							rectangle2 = new Rectangle(0, this.{23390}.Bounds.Height, this.{23390}.Bounds.Width * 2, this.{23390}.Bounds.Height * 2);
							color = Color.White;
							gs14.DrawCustomTexture({14570}4, rectangle, rectangle2, color);
						}
						Engine.GS.End2D();
						return;
					}
					catch (InvalidOperationException)
					{
						Engine.GS.ResetRenderTargets();
						Engine.GS.ClearRenderTarget(Color.Black);
						return;
					}
				}
			}
			Engine.GS.ResetRenderTargets();
			Engine.GS.ClearRenderTarget(Color.Black);
		}

		// Token: 0x040015FC RID: 5628
		public const bool QuickLoad = false;

		// Token: 0x040015FD RID: 5629
		public static bool LoadingCompleted = false;

		// Token: 0x040015FE RID: 5630
		public static ManualResetEventSlim waitEndVideo = new ManualResetEventSlim(false);

		// Token: 0x040015FF RID: 5631
		private static readonly Vector2 c_loadScreenSize = new Vector2(1600f, 900f);

		// Token: 0x04001600 RID: 5632
		private static readonly Rectangle c_black = new Rectangle(108, 109, 1, 1);

		// Token: 0x04001601 RID: 5633
		private Texture2D {23380};

		// Token: 0x04001602 RID: 5634
		private Texture2D {23381};

		// Token: 0x04001603 RID: 5635
		private bool {23382};

		// Token: 0x04001604 RID: 5636
		private float {23383};

		// Token: 0x04001605 RID: 5637
		private float {23384};

		// Token: 0x04001606 RID: 5638
		private string {23385};

		// Token: 0x04001607 RID: 5639
		private Vector2 {23386};

		// Token: 0x04001608 RID: 5640
		private Vector2 {23387};

		// Token: 0x04001609 RID: 5641
		private VideoRenderer {23388};

		// Token: 0x0400160A RID: 5642
		private Rectangle {23389};

		// Token: 0x0400160B RID: 5643
		private Texture2D {23390};
	}
}
