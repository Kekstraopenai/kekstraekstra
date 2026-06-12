using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using SDL2;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;
using TheraEngine.Graphics;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using UWContentPipelineExtensionRuntime.Tags;
using UWEngine.Core;
using WorldOfSeaBattles;
using WorldOfSeaBattles.Components.Apis;
using WorldOfSeaBattles.Interface.DebugPanel;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Components.Client;
using World_Of_Sea_Battle.Components.Console;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Graphics.Effects;
using World_Of_Sea_Battle.Graphics.Shaders;
using World_Of_Sea_Battle.Interface;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004F0 RID: 1264
	internal sealed class Main : GameCore
	{
		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06001C08 RID: 7176 RVA: 0x00100872 File Offset: 0x000FEA72
		public EntryScene SceneEntry
		{
			get
			{
				return this.{25246};
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06001C09 RID: 7177 RVA: 0x0010087A File Offset: 0x000FEA7A
		public GameScene SceneGame
		{
			get
			{
				return this.{25247};
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06001C0A RID: 7178 RVA: 0x00100882 File Offset: 0x000FEA82
		public PortScene ScenePort
		{
			get
			{
				return this.{25248};
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001C0B RID: 7179 RVA: 0x0010088A File Offset: 0x000FEA8A
		public Scene GetCurrentScene
		{
			get
			{
				return this.{25251};
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001C0C RID: 7180 RVA: 0x00100892 File Offset: 0x000FEA92
		public GameSceneName GetCurrentSceneName
		{
			get
			{
				return this.{25253};
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06001C0D RID: 7181 RVA: 0x0010089C File Offset: 0x000FEA9C
		// (remove) Token: 0x06001C0E RID: 7182 RVA: 0x001008D4 File Offset: 0x000FEAD4
		public event Action EvEndLoading
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{25236};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25236}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{25236};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25236}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06001C0F RID: 7183 RVA: 0x0010090C File Offset: 0x000FEB0C
		// (remove) Token: 0x06001C10 RID: 7184 RVA: 0x00100944 File Offset: 0x000FEB44
		public event Action EvEntryToGame
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{25237};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25237}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{25237};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25237}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x06001C11 RID: 7185 RVA: 0x0010097C File Offset: 0x000FEB7C
		// (remove) Token: 0x06001C12 RID: 7186 RVA: 0x001009B4 File Offset: 0x000FEBB4
		public event Action EvPreviewEntryToGame
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{25238};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25238}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{25238};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25238}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06001C13 RID: 7187 RVA: 0x001009EC File Offset: 0x000FEBEC
		// (remove) Token: 0x06001C14 RID: 7188 RVA: 0x00100A24 File Offset: 0x000FEC24
		public event Action EvChangeScene
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{25239};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25239}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{25239};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{25239}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001C15 RID: 7189 RVA: 0x00100A5C File Offset: 0x000FEC5C
		public Main(GameParams {25220})
		{
			Steam.TryInit();
			Global.Settings = LocalSettings.LoadOrCreate();
			LanguageSettings.SetLauncherLocale({25220}.Locale ?? (Global.Settings.Language.LauncherLocale ?? LanguageSettings.GetExpectedDefaultLocale()));
			Global.Game = this;
			Global.Network = new NetworkManager();
			Global.RenderStats = new RenderStatistics();
			Session.VKPlay = new VKPlayData
			{
				Uid = {25220}.VKPlay_Uid,
				Token = {25220}.VKPlay_Token,
				Currency = {25220}.VKPlay_Currency
			};
			this.GameTime.TimeSpeed = 1f;
			this.SoundSystem = new SoundManager();
			this.WorldInstance = new WorldInstance();
			this.StaticSystem = new StaticSystem();
			this.InteractiveWorldSystem = new InteractiveWorldSystem();
			this.InterestPoints = new InterestPointsManager();
			this.ClientCommands = new ClientConsoleCommands(CommonGlobal.Commands);
			this.{25240} = false;
			this.{25252} = false;
			this.{25255} = new Timer(120000f);
			ContentTypeReaderManager.TypeResolver = delegate(string {25258})
			{
				if ({25258}.Contains("AnimationClip"))
				{
					return typeof(AnimationClip);
				}
				string text = {25258}.Contains("[[") ? Regex.Match({25258}, "\\[((?:\\w+\\.)+\\w+)\\,").Groups[1].Value : {25258};
				Type type = Type.GetType(text + (text.Contains("CommonDataTypes") ? ",Common" : ",TheraEngine"));
				if (type == null)
				{
					throw new NotImplementedException({25258});
				}
				return type;
			};
		}

		// Token: 0x06001C16 RID: 7190 RVA: 0x00100BB0 File Offset: 0x000FEDB0
		protected override void Initialize()
		{
			base.InitializeCore("Data", Global.Settings.FullscreenEnabled, Global.Settings.VerticalSyncEnabled, "World Of Sea Battle");
			this.scriptManager.Enabled = false;
			base.IsMouseVisible = true;
			CommonGlobal.SetInitialWeatherState();
			LightSourceOcclusionTest.OcclusionQueryFail += delegate(Exception {25259})
			{
				Helpers.SendError({25259}, "OcclusionQueryFail(new client)", true, false);
			};
			base.Initialize();
		}

		// Token: 0x06001C17 RID: 7191 RVA: 0x00100C23 File Offset: 0x000FEE23
		protected override void OnExiting(object {25221}, EventArgs {25222})
		{
			if (!this.{25240})
			{
				this.{25226}();
			}
		}

		// Token: 0x06001C18 RID: 7192 RVA: 0x00100C34 File Offset: 0x000FEE34
		protected override void LoadContent()
		{
			if (!File.Exists("Data\\Shaders\\MetarialPass\\ParticlesAndStaticMesh.xnb"))
			{
				Helpers.SendError(new Exception("Trying to load game without files. Xna"), "LoadContent", false, false);
				Engine.ShowMessageBox("No content files. Probably game wasn't downloaded or updated properly", "", SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION);
				Environment.Exit(-1);
				return;
			}
			ThreadPool.QueueUserWorkItem(delegate(object {25260})
			{
				ConnectionModule.fingerPrint = new FingerPrintF(true);
			});
			this.GameTime.TargetFPS = 30;
			ThreadPool.SetMinThreads(2 + Environment.ProcessorCount, 2 + Environment.ProcessorCount);
			if (this.{25254})
			{
				throw new InvalidOperationException("Device lost");
			}
			this.{25254} = true;
			base.Content.RootDirectory = "Data";
			Engine.LoadContent(this.Content, "\\Shaders", LocaleInfo.Current.Id.ToString());
			Global.Render = new Renderer();
			Global.Render.LoadContentAndInitialize(this.Content);
			this.{25249} = new UpdateableObjCollection();
			this.{25250} = new Renderable2DCollection();
			this.updateableComponents.Add(this.{25249});
			this.{25253} = GameSceneName.Loading;
			this.{25251} = null;
			AtlasObjs.Texture = Texture2DAtlas.Load(PathContent.tex_pack_objs, this);
			AtlasGameGui.Texture = Texture2DAtlas.Load(PathContent.tex_pack_gamegui, this);
			AtlasEntryGui.Texture = Texture2DAtlas.Load(PathContent.tex_pack_entrygui, this);
			AtlasPortGui.Texture = Texture2DAtlas.Load(PathContent.tex_pack_portgui, this);
			CommonAtlas.Texture = Texture2DAtlas.Load(PathContent.tex_pack_commongui, this);
			Global.Settings.LoadProperty();
			Global.Render.InitializeLoadingScreen(this.Content);
			this.SoundSystem.Initialize(this.Content);
			new Thread(new ThreadStart(this.{25234}))
			{
				Priority = ThreadPriority.Highest
			}.Start();
			this.EvEndLoading += this.{25235};
			base.LoadContent();
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x00100E12 File Offset: 0x000FF012
		private void {25223}()
		{
			Global.Render.InitializeLoadingScreen(this.Content);
		}

		// Token: 0x06001C1A RID: 7194 RVA: 0x00100E24 File Offset: 0x000FF024
		private void {25224}()
		{
			if (!this.{25241})
			{
				Thread.Sleep(1000);
			}
			if (Global.Settings.FullscreenEnabled)
			{
				Thread.Sleep(3000);
			}
			LoadingScreenRenderer.waitEndVideo.Wait();
			Stopwatch stopwatch = Stopwatch.StartNew();
			Global.Network.Initialize();
			Materials.InitializeTexturesDatabase(this.Content);
			CommonGlobal.LoadGameResources(this.Content, Materials.TexturesDatabase, false);
			Global.ServerInitializationThread();
			OtherTextures.Load(this.Content);
			ToolTip.TextureObj = OtherTextures.ToolTipTexture;
			ToolTip.Texture = ExpandoTexturePath.CreateBox(new Rectangle(0, 0, 200, 172), new Rectangle(0, 172, 200, 172), new Rectangle(200, 0, 200, 172), new Rectangle(200, 172, 200, 172));
			ToolTip.LightedCornerTexture = new Rectangle(400, 0, 400, 344);
			ToolTip.HeaderLineTexture = new Rectangle(0, 347, 378, 100);
			LocalContent.Initialize(this.Content);
			Global.Render.InitializeFromLoadContentThread();
			this.WorldInstance.Initialize(this.Content);
			this.InteractiveWorldSystem.Initialize(this.Content);
			this.StaticSystem.Initialize(this.Content);
			this.{25246} = new EntryScene();
			this.{25247} = new GameScene();
			this.{25248} = new PortScene();
			this.{25246}.Initialize(this.Content);
			this.{25247}.Initialize(this.Content);
			this.{25248}.Initialize(this.Content);
			this.ClientCommands.Initialize();
			stopwatch.Stop();
			if (stopwatch.Elapsed.TotalSeconds < 6.0)
			{
				Thread.Sleep((int)((6.0 - stopwatch.Elapsed.TotalSeconds) * 1000.0));
			}
			stopwatch.Stop();
			LoadingScreenRenderer.LoadingCompleted = true;
			if (!this.{25241})
			{
				Thread.Sleep(1000);
			}
			this.{25244} = true;
		}

		// Token: 0x06001C1B RID: 7195 RVA: 0x000329D4 File Offset: 0x00030BD4
		protected override float OnTimeSpeed()
		{
			return 1f;
		}

		// Token: 0x06001C1C RID: 7196 RVA: 0x00101050 File Offset: 0x000FF250
		protected override bool Update(ref FrameTime {25225})
		{
			if (this.{25242})
			{
				this.{25242} = false;
				this.{25226}();
				return true;
			}
			if (this.{25240})
			{
				return true;
			}
			if (this.{25244})
			{
				this.{25244} = false;
				Action action = this.{25236};
				if (action != null)
				{
					action();
				}
				this.{25236} = null;
			}
			if (this.{25252})
			{
				Global.Render.Update(ref {25225}, this.{25253});
				if (GameScene.GameHasInputFocus && ((this.{25253} != GameSceneName.Port && this.{25247}.MouseState == 0) || (this.{25248}.IsMainPage && this.{25247}.MouseState == 1) || this.{25253} == GameSceneName.Entry) && !{22279}.IsActive)
				{
					this.{25256} = Math.Min(3, this.{25256} + 1);
				}
				else
				{
					this.{25256} = 0;
				}
				if (InputHelper.IsClick(Keys.Escape))
				{
					if (this.{25256} >= 3)
					{
						{19381}.TryShow();
					}
					else
					{
						this.{25256} = 0;
					}
				}
				if (this.{25253} != GameSceneName.Loading && Global.Settings != null && this.{25255}.Sample(ref {25225}))
				{
					Global.Settings.OnSave(true);
				}
				if (this.{25253} == GameSceneName.Port || this.{25253} == GameSceneName.Game)
				{
					this.inactivityDuration += {25225}.secElapsed;
					if (this.inactivityDuration > 3600f && Global.Network.Conection.isStarted)
					{
						Global.Network.Send(new OnDisconnectionFromClientMsg("", Session.SafeExitFlags, true, false, null));
						this.inactivityDuration = 0f;
					}
				}
			}
			if (this.{25245} != null)
			{
				this.ChangeSceneToEntry();
				if (this.{25245} == DisconnectionFromServerMessage.ServerReboot.ToStringLocal())
				{
					int {17378} = 60;
					new {17312}(this.{25245}, {17378}, true);
				}
				else
				{
					new {17312}(this.{25245});
				}
				this.{25245} = null;
			}
			if (InputHelper.NowGamepadState.IsConnected && InputHelper.NowGamepadState.Triggers.Left < 0.5f)
			{
				Vector2 vector = InputHelper.NowGamepadState.ThumbSticks.Right * {25225}.secElapsed;
				vector.Y = -vector.Y;
				float num = vector.Length();
				if (num > 0f)
				{
					num *= num;
					vector = vector / vector.Length() * num * 100f;
				}
				Vector2 vector2 = new Vector2((float)Mouse.GetState().X, (float)Mouse.GetState().Y) + vector * 2000f;
				if (Global.Game.IsActive)
				{
					Mouse.SetPosition(Rand.Round(vector2.X), Rand.Round(vector2.Y));
				}
			}
			if (Local.QueuedHotReloadPath != null)
			{
				Local.Load(Local.QueuedHotReloadPath);
				Local.QueuedHotReloadPath = null;
			}
			Steam.Update();
			return false;
		}

		// Token: 0x06001C1D RID: 7197 RVA: 0x00101330 File Offset: 0x000FF530
		protected override void Render()
		{
			if (this.{25240})
			{
				return;
			}
			if (this.{25243})
			{
				this.{25243} = false;
				base.GetGraphicsManager.ApplyChanges();
			}
			Global.Render.GameDraw(this.{25252} ? this.{25251} : null, this.{25253}, this.{25250});
			this.{25257} = 0;
		}

		// Token: 0x06001C1E RID: 7198 RVA: 0x0010138E File Offset: 0x000FF58E
		public void DiviceApplyChanges()
		{
			this.{25243} = true;
		}

		// Token: 0x06001C1F RID: 7199 RVA: 0x00101398 File Offset: 0x000FF598
		public void ExitFromGame()
		{
			if (this.{25240})
			{
				return;
			}
			if ((this.{25253} == GameSceneName.Game || this.{25253} == GameSceneName.Port) && Session.IsFirstGameRun && Session.Account != null && !Global.Player.MapInfo.IsEducationMap)
			{
				Helpers.ExecuteBrowser(Session.VKPlay.IsActive ? Local.launcher_startsurvey_vk_ref : Local.launcher_startsurvey_ref, true);
			}
			this.{25242} = true;
		}

		// Token: 0x06001C20 RID: 7200 RVA: 0x00101404 File Offset: 0x000FF604
		private void {25226}()
		{
			{17053}.Exit();
			if (this.{25252})
			{
				this.{25251}.OnEnd();
			}
			if (Global.Network.IsStarted)
			{
				Global.Network.Stop(this.{25253} != GameSceneName.Entry);
			}
			if (this.InteractiveWorldSystem.IsActive)
			{
				this.InteractiveWorldSystem.Off();
			}
			if (this.WorldInstance.IsActive)
			{
				this.WorldInstance.Off();
			}
			if (this.StaticSystem.IsActive)
			{
				this.StaticSystem.Off();
			}
			if (this.SoundSystem.IsActive)
			{
				this.SoundSystem.Off();
			}
			Global.Settings.OnSave(false);
			Session.RemoveResources();
			Steam.Shutdown();
			base.Exit();
			this.{25240} = true;
		}

		// Token: 0x06001C21 RID: 7201 RVA: 0x001014CD File Offset: 0x000FF6CD
		public void ChangeSceneToEntry()
		{
			this.{25253} = GameSceneName.Entry;
			this.{25230}(this.{25246});
		}

		// Token: 0x06001C22 RID: 7202 RVA: 0x001014E4 File Offset: 0x000FF6E4
		public void ChangeSceneToGame()
		{
			bool flag = this.{25253} == GameSceneName.Entry;
			if (flag)
			{
				Action action = this.{25238};
				if (action != null)
				{
					action();
				}
				Action action2 = this.{25237};
				if (action2 != null)
				{
					action2();
				}
			}
			this.{25247}.PortableMode = (flag && Session.Account.SavedWorldIsEnteredPort);
			this.{25253} = GameSceneName.Game;
			this.{25230}(this.{25247});
			if (flag && Session.Account.SavedWorldIsEnteredPort)
			{
				this.ChangeSceneToPort(true, false);
			}
		}

		// Token: 0x06001C23 RID: 7203 RVA: 0x00101565 File Offset: 0x000FF765
		public void ChangeSceneToPort(bool {25227}, bool {25228})
		{
			this.{25248}.Prepare({25227}, {25228});
			this.{25253} = GameSceneName.Port;
			this.{25230}(this.{25248});
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x00101587 File Offset: 0x000FF787
		public void RegisterCallBreakToEntryOffline(string {25229})
		{
			this.{25245} = {25229};
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x00101590 File Offset: 0x000FF790
		private void {25230}(Scene {25231})
		{
			if (this.{25252})
			{
				this.{25251}.OnEnd();
			}
			Action action = this.{25239};
			if (action != null)
			{
				action();
			}
			this.{25251} = {25231};
			this.{25232}();
			this.Scenes.RemoveAll((IGraphicsElement {25262}) => {25262}.RemoveWhenSceneChanged);
			{25231}.OnBegin();
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x00101600 File Offset: 0x000FF800
		private void {25232}()
		{
			this.interfaceManager.ClearAll();
			bool flag = this.{25252};
			this.{25252} = true;
			this.{25249}.Clear();
			this.{25250}.Clear();
			Tlist<IUpdateableObject> updateableComponents = this.updateableComponents;
			IUpdateableObject updateableObject = this.{25249};
			int num = updateableComponents.IndexOf(updateableObject);
			this.{25249} = new UpdateableObjCollection();
			this.updateableComponents.Array[num] = this.{25249};
			this.{25249}.Add(Global.Network);
			if (!this.{25251}.UseStaticCamera)
			{
				Global.Camera.ResetState(this.{25253} == GameSceneName.Port);
				Global.Camera.SetTargetObject(Global.Player, this.{25253} == GameSceneName.Port, null);
				this.{25249}.Add(Global.Camera);
			}
			else
			{
				Global.Camera.ReserTargetObject();
			}
			if (this.{25251}.UseStaticWeather)
			{
				CommonGlobal.SetInitialWeatherState();
				Global.Render.GetSceneManager.CurrentCloudyLevel = 0f;
				Global.Render.GetSceneManager.WorldTime = 16f;
			}
			else
			{
				this.{25249}.Add(Global.Render.GetSceneManager);
			}
			this.{25249}.Add(this.{25251});
			Tlist<IRenderable2D> tlist = this.{25250};
			IRenderable2D interfaceManager = this.interfaceManager;
			tlist.Add(interfaceManager);
			if (this.{25251}.CleanScene && flag)
			{
				this.WorldInstance.Off();
				this.WorldInstance.On();
				this.InteractiveWorldSystem.Off();
				this.InteractiveWorldSystem.On();
				this.StaticSystem.Off();
				this.StaticSystem.On();
				this.SoundSystem.Off();
				this.SoundSystem.On();
			}
			else if (!flag)
			{
				this.WorldInstance.On();
				this.InteractiveWorldSystem.On();
				this.StaticSystem.On();
				this.SoundSystem.On();
			}
			this.InterestPoints.Clean();
			InputHelper.UnFullReset();
			Global.Render.CleanSupportTargets();
			Global.Game.GetInterfaceManager.Host.Opacity = 1f;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0010181E File Offset: 0x000FFA1E
		protected override void Dispose(bool {25233})
		{
			if ({25233})
			{
				this.{25252} = false;
			}
			base.Dispose({25233});
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x00101831 File Offset: 0x000FFA31
		[CompilerGenerated]
		private void {25234}()
		{
			this.{25224}();
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0010183C File Offset: 0x000FFA3C
		[CompilerGenerated]
		private void {25235}()
		{
			this.scriptManager.Enabled = true;
			this.updateableComponents.Add(this.StaticSystem);
			this.updateableComponents.Add(this.WorldInstance);
			this.updateableComponents.Add(this.SoundSystem);
			this.updateableComponents.Add(this.InteractiveWorldSystem);
			this.updateableComponents.Add(this.InterestPoints);
			this.updateableComponents.Add(CommonGlobal.WorldWeather);
			this.updateableComponents.Add(CommonGlobal.EducationMapWeather);
			Tlist<IUpdateableObject> updateableComponents = this.updateableComponents;
			IUpdateableObject updateableObject = CommonGlobal.PassingMapsWeather;
			updateableComponents.AddIfNotContains(updateableObject);
			Tlist<IUpdateableObject> updateableComponents2 = this.updateableComponents;
			updateableObject = CommonGlobal.ArenaWeather;
			updateableComponents2.AddIfNotContains(updateableObject);
			this.updateableComponents.Add(new DebugPanelActivator());
			this.interfaceManager.ClearAll();
			this.ChangeSceneToEntry();
			Global.Render.UploadLoadingScreen();
			this.{25241} = true;
			this.Content.ContentLoadingComplete();
			CommonGlobal.CommonDataContent.ContentLoadingComplete();
			CrewNotificationManager.Initialize();
			base.MusicManager.SetNextTrackFromSet(MusicSetTag.Entry, 0f, 15000f);
			CalendarEvents.OnLogbookMessage = delegate(string {25261})
			{
				Global.Settings.Logbook.Write({25261}, LBFlags.L1);
			};
		}

		// Token: 0x04001B18 RID: 6936
		public readonly StaticSystem StaticSystem;

		// Token: 0x04001B19 RID: 6937
		public readonly WorldInstance WorldInstance;

		// Token: 0x04001B1A RID: 6938
		public readonly SoundManager SoundSystem;

		// Token: 0x04001B1B RID: 6939
		public readonly InteractiveWorldSystem InteractiveWorldSystem;

		// Token: 0x04001B1C RID: 6940
		public readonly InterestPointsManager InterestPoints;

		// Token: 0x04001B1D RID: 6941
		public Tlist<IGraphicsElement> Scenes = new Tlist<IGraphicsElement>();

		// Token: 0x04001B1E RID: 6942
		public ClientConsoleCommands ClientCommands;

		// Token: 0x04001B1F RID: 6943
		[CompilerGenerated]
		private Action {25236};

		// Token: 0x04001B20 RID: 6944
		[CompilerGenerated]
		private Action {25237};

		// Token: 0x04001B21 RID: 6945
		[CompilerGenerated]
		private Action {25238};

		// Token: 0x04001B22 RID: 6946
		[CompilerGenerated]
		private Action {25239};

		// Token: 0x04001B23 RID: 6947
		private bool {25240};

		// Token: 0x04001B24 RID: 6948
		private bool {25241};

		// Token: 0x04001B25 RID: 6949
		private bool {25242};

		// Token: 0x04001B26 RID: 6950
		private bool {25243};

		// Token: 0x04001B27 RID: 6951
		private volatile bool {25244};

		// Token: 0x04001B28 RID: 6952
		private string {25245};

		// Token: 0x04001B29 RID: 6953
		private EntryScene {25246};

		// Token: 0x04001B2A RID: 6954
		private GameScene {25247};

		// Token: 0x04001B2B RID: 6955
		private PortScene {25248};

		// Token: 0x04001B2C RID: 6956
		private UpdateableObjCollection {25249};

		// Token: 0x04001B2D RID: 6957
		private Renderable2DCollection {25250};

		// Token: 0x04001B2E RID: 6958
		private Scene {25251};

		// Token: 0x04001B2F RID: 6959
		private bool {25252};

		// Token: 0x04001B30 RID: 6960
		private GameSceneName {25253};

		// Token: 0x04001B31 RID: 6961
		private bool {25254};

		// Token: 0x04001B32 RID: 6962
		public float inactivityDuration;

		// Token: 0x04001B33 RID: 6963
		private Timer {25255};

		// Token: 0x04001B34 RID: 6964
		private int {25256};

		// Token: 0x04001B35 RID: 6965
		private int {25257};
	}
}
