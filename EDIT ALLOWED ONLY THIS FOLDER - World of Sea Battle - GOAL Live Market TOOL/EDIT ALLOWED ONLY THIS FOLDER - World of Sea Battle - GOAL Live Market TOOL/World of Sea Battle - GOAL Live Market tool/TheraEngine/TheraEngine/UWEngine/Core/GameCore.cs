using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using SDL2;
using TheraEngine;
using TheraEngine.Assets.Audio;
using TheraEngine.Assets.Graphics;
using TheraEngine.Assets.Script;
using TheraEngine.Components.Architecture;
using TheraEngine.Core;
using TheraEngine.Graphics;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface;

namespace UWEngine.Core
{
	// Token: 0x02000008 RID: 8
	public abstract class GameCore : Game
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021B7 File Offset: 0x000003B7
		public Vector2 WindowSize
		{
			get
			{
				return base.Window.ClientBounds.WidthHeight();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021C9 File Offset: 0x000003C9
		public Rectangle XScreenRectangle
		{
			get
			{
				return new Rectangle(0, 0, base.Window.ClientBounds.Width, base.Window.ClientBounds.Height);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021F2 File Offset: 0x000003F2
		public Rectangle AdaptiveUiRect
		{
			get
			{
				return this.adaptieUiRect;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021FA File Offset: 0x000003FA
		public float GetAspectRatio
		{
			get
			{
				return this.WindowSize.X / this.WindowSize.Y;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002213 File Offset: 0x00000413
		public Vector2 XScreenIntCenter
		{
			get
			{
				return new Vector2((float)((int)this.WindowSize.X / 2), (float)((int)this.WindowSize.Y / 2));
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000012 RID: 18 RVA: 0x00002238 File Offset: 0x00000438
		// (remove) Token: 0x06000013 RID: 19 RVA: 0x00002270 File Offset: 0x00000470
		public event Action<Vector2> EvSizeChanged;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000014 RID: 20 RVA: 0x000022A8 File Offset: 0x000004A8
		// (remove) Token: 0x06000015 RID: 21 RVA: 0x000022E0 File Offset: 0x000004E0
		public event Action<Vector2> EvUiRectangleSizeChanged;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002315 File Offset: 0x00000515
		// (set) Token: 0x06000017 RID: 23 RVA: 0x0000231D File Offset: 0x0000051D
		public float AdaptiveUiExtraScale
		{
			get
			{
				return this.adaptiveUiScale;
			}
			set
			{
				this.adaptiveUiScale = value;
				this.UpdateAdavtiveUi();
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000018 RID: 24 RVA: 0x0000232C File Offset: 0x0000052C
		public InterfaceManager GetInterfaceManager
		{
			get
			{
				return this.interfaceManager;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002334 File Offset: 0x00000534
		public ScriptManager GetScriptManager
		{
			get
			{
				return this.scriptManager;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000233C File Offset: 0x0000053C
		public GraphicsDeviceManager GetGraphicsManager
		{
			get
			{
				return this.graphicsManager;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002344 File Offset: 0x00000544
		public double GameTotalTimeMs
		{
			get
			{
				return this.GameTime.TotalGameTimeMs;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002351 File Offset: 0x00000551
		public double GameTotalTimeSec
		{
			get
			{
				return this.GameTime.TotalGameTimeMs / 1000.0;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002368 File Offset: 0x00000568
		public GameMusicEngine MusicManager
		{
			get
			{
				return this.musicManager;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002370 File Offset: 0x00000570
		public GameCore()
		{
			this.graphicsManager = new GraphicsDeviceManager(this);
			this.GameTime = new GameTimeEngine();
			base.Content = new HeapLoader(base.Content.ServiceProvider);
			this.Content = new ContentManager((HeapLoader)base.Content);
			Engine.Game = this;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002400 File Offset: 0x00000600
		protected void InitializeCore(string contentRootDir, bool isFullScreen, bool verticalSync, string tittle)
		{
			Engine.PlatformTools.Configure();
			base.Content.RootDirectory = contentRootDir;
			base.Window.Title = tittle;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
			{
				GameWindow window = base.Window;
				window.Title += " (Linux)";
			}
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				GameWindow window2 = base.Window;
				window2.Title += " (OSX)";
			}
			this.GraphicsSystem = new Device(base.GraphicsDevice);
			Engine.GS = this.GraphicsSystem;
			ScreenPlaneRenderer.Initialize();
			this.monitorWidth = Engine.MonitorWidth;
			this.monitorHeight = Engine.MonitorHeight;
			this.isInFullScreen = isFullScreen;
			base.Window.AllowUserResizing = true;
			this.graphicsManager.PreferredBackBufferFormat = SurfaceFormat.Rgba64;
			this.graphicsManager.PreferredDepthStencilFormat = DepthFormat.None;
			this.graphicsManager.SynchronizeWithVerticalRetrace = verticalSync;
			Mouse.WindowHandle = base.Window.Handle;
			this.scriptManager = new ScriptManager();
			this.interfaceManager = new InterfaceManager();
			this.musicManager = new GameMusicEngine();
			this.updateableComponents = new UpdateableObjCollection();
			this.updateableComponents.Add(this.scriptManager);
			this.updateableComponents.Add(this.musicManager);
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000254B File Offset: 0x0000074B
		protected virtual float OnTimeSpeed()
		{
			return 1f;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002554 File Offset: 0x00000754
		protected override void Update(GameTime gameTime)
		{
			base.Update(gameTime);
			this.GameTime.BeginUpdate(gameTime.ElapsedGameTime.TotalMilliseconds, this);
			if (!base.IsActive && this.isMusicPlaying)
			{
				if (MediaPlayer.State == MediaState.Playing)
				{
					MediaPlayer.Pause();
				}
				this.isMusicPlaying = false;
			}
			else if (base.IsActive && !this.isMusicPlaying)
			{
				if (MediaPlayer.State == MediaState.Paused)
				{
					MediaPlayer.Resume();
				}
				this.isMusicPlaying = true;
			}
			InputHelper.BeginUpdate(!base.IsActive);
			float num = this.OnTimeSpeed();
			FrameTime frameTime = new FrameTime(this.GameTime.ElapsedUpdate * num, this.GameTime.ElapsedUpdateSec);
			frameTime.EvaluteTimerMs(ref this.blockNextLoad);
			this.updateableComponents.Update(ref frameTime);
			if (this.Update(ref frameTime))
			{
				return;
			}
			this.interfaceManager.Update(ref frameTime);
			InputHelper.ForceUpdateInput(!base.IsActive);
			this.GameTime.EndUpdate();
			if (this.extraGcTimer.Sample(ref frameTime))
			{
				GC.Collect();
			}
		}

		// Token: 0x06000022 RID: 34
		protected abstract bool Update(ref FrameTime frameTime);

		// Token: 0x06000023 RID: 35 RVA: 0x00002660 File Offset: 0x00000860
		protected sealed override void Draw(GameTime gameTime)
		{
			this.GameTime.BeginDraw(gameTime.ElapsedGameTime.TotalMilliseconds);
			InputHelper.ForceUpdateInput(!base.IsActive);
			if (!this.initialSetup)
			{
				this.SetupWindowParameters(this.isInFullScreen);
				this.initialSetup = true;
			}
			if (this.prevScreenSize != this.WindowSize)
			{
				this.UpdateWindowParameters(true);
			}
			else if ((float)base.GraphicsDevice.Viewport.Width != this.WindowSize.X && this.WindowSize.Y > 0f)
			{
				this.graphicsManager.PreferredBackBufferWidth = (int)this.WindowSize.X;
				this.graphicsManager.PreferredBackBufferHeight = (int)this.WindowSize.Y;
				this.graphicsManager.ApplyChanges();
			}
			Engine.GS.FrameBegin();
			object obj = this.SyncRender;
			lock (obj)
			{
				this.Render();
			}
			if (this.blockNextLoad == 0f)
			{
				obj = VirtualTextureHelper.OutputSyncRoot;
				lock (obj)
				{
					this.loadTexturesTime.Restart();
					while (VirtualTextureHelper.Output.Count > 0)
					{
						this.blockNextLoad = 100f;
						ValueTuple<VirtualTexture, MemoryStream, VirtualTextureSource> valueTuple = VirtualTextureHelper.Output.Dequeue();
						valueTuple.Item1.Tex = this.Content.LoadFromMemory<Texture2D>(valueTuple.Item3.Uri, valueTuple.Item2, true);
						if (valueTuple.Item1.Tex.IsDisposed)
						{
							throw new Exception("Something went wrong with content cachhing");
						}
						if (this.loadTexturesTime.Elapsed.TotalMilliseconds > 16.0)
						{
							break;
						}
					}
				}
			}
			InputHelper.ForceUpdateInput(!base.IsActive);
			this.GameTime.EndDraw();
			base.Draw(gameTime);
		}

		// Token: 0x06000024 RID: 36
		protected abstract void Render();

		// Token: 0x06000025 RID: 37 RVA: 0x0000286C File Offset: 0x00000A6C
		public void FlashWindow()
		{
			try
			{
				SDL.SDL_FlashWindow(base.Window.Handle, SDL.SDL_FlashOperation.SDL_FLASH_BRIEFLY);
			}
			catch
			{
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000028A0 File Offset: 0x00000AA0
		private void SetupWindowParameters(bool fullscreen)
		{
			this.isInFullScreen = fullscreen;
			this.graphicsManager.IsFullScreen = fullscreen;
			this.graphicsManager.ApplyChanges();
			if (!fullscreen)
			{
				SDL.SDL_MaximizeWindow(base.Window.Handle);
				int preferredBackBufferWidth;
				int preferredBackBufferHeight;
				SDL.SDL_GetWindowSize(base.Window.Handle, out preferredBackBufferWidth, out preferredBackBufferHeight);
				if (base.GraphicsDevice.PresentationParameters.BackBufferWidth == 800)
				{
					this.graphicsManager.PreferredBackBufferWidth = preferredBackBufferWidth;
					this.graphicsManager.PreferredBackBufferHeight = preferredBackBufferHeight;
					this.graphicsManager.ApplyChanges();
				}
			}
			this.UpdateWindowParameters(true);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002934 File Offset: 0x00000B34
		private void UpdateWindowParameters(bool callEvents)
		{
			Vector2 windowSize = this.WindowSize;
			Vector2 obj;
			Vector2.Subtract(ref windowSize, ref this.prevScreenSize, out obj);
			this.UpdateAdavtiveUi();
			if (callEvents && this.EvSizeChanged != null)
			{
				this.EvSizeChanged(obj);
			}
			this.UpdateAdavtiveUi();
			this.prevScreenSize = this.WindowSize;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002988 File Offset: 0x00000B88
		private void UpdateAdavtiveUi()
		{
			float x = this.WindowSize.X;
			float y = this.WindowSize.Y;
			float num = (y < 700f || x < 1200f) ? (y * 1.1f) : Math.Min(y, 1080f);
			num /= this.AdaptiveUiExtraScale;
			this.adaptieUiRect = new Rectangle(0, 0, (int)Math.Round((double)(num * this.GetAspectRatio)), (int)Math.Round((double)num));
			Action<Vector2> evUiRectangleSizeChanged = this.EvUiRectangleSizeChanged;
			if (evUiRectangleSizeChanged == null)
			{
				return;
			}
			evUiRectangleSizeChanged(this.adaptieUiRect.WidthHeight() - this.adaptieUiRect.WidthHeight());
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002A2A File Offset: 0x00000C2A
		public void ChangeFullscreenMode(bool value)
		{
			this.SetupWindowParameters(value);
		}

		// Token: 0x04000008 RID: 8
		public Device GraphicsSystem;

		// Token: 0x04000009 RID: 9
		public new ContentManager Content;

		// Token: 0x0400000A RID: 10
		public readonly GameTimeEngine GameTime;

		// Token: 0x0400000B RID: 11
		internal object SyncRender = new object();

		// Token: 0x0400000C RID: 12
		internal bool isMusicPlaying;

		// Token: 0x0400000D RID: 13
		private float blockNextLoad;

		// Token: 0x0400000E RID: 14
		private Stopwatch loadTexturesTime = new Stopwatch();

		// Token: 0x0400000F RID: 15
		private Timer extraGcTimer = new Timer(900000f);

		// Token: 0x04000010 RID: 16
		private Vector2 prevScreenSize;

		// Token: 0x04000011 RID: 17
		private Rectangle adaptieUiRect;

		// Token: 0x04000012 RID: 18
		private float adaptiveUiScale = 1f;

		// Token: 0x04000013 RID: 19
		private bool isInFullScreen;

		// Token: 0x04000014 RID: 20
		private bool initialSetup;

		// Token: 0x04000015 RID: 21
		internal int monitorWidth;

		// Token: 0x04000016 RID: 22
		internal int monitorHeight;

		// Token: 0x04000017 RID: 23
		protected internal InterfaceManager interfaceManager;

		// Token: 0x04000018 RID: 24
		protected ScriptManager scriptManager;

		// Token: 0x04000019 RID: 25
		protected GraphicsDeviceManager graphicsManager;

		// Token: 0x0400001A RID: 26
		protected internal UpdateableObjCollection updateableComponents;

		// Token: 0x0400001B RID: 27
		protected GameMusicEngine musicManager;
	}
}
