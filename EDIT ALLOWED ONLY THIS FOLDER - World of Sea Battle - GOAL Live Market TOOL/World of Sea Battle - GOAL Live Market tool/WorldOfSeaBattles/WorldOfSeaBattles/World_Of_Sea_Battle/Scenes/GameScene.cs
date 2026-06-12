using System;
using System.Collections.Generic;
using Common.Account;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Core;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Scenes
{
	// Token: 0x02000034 RID: 52
	internal sealed class GameScene : Scene
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000A7C9 File Offset: 0x000089C9
		public static void IncreaseGameInput()
		{
			GameScene.inputToGameCounter++;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x0000A7D7 File Offset: 0x000089D7
		public static void DecreaseGameInput()
		{
			if (GameScene.inputToGameCounter == 0)
			{
				return;
			}
			GameScene.inputToGameCounter--;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000A7ED File Offset: 0x000089ED
		public static bool GameHasInputFocus
		{
			get
			{
				return GameScene.inputToGameCounter == 0 && !KeyInputControl.IsInputElements;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000A800 File Offset: 0x00008A00
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000A808 File Offset: 0x00008A08
		public int MouseState
		{
			get
			{
				return this.{16489};
			}
			set
			{
				this.{16489} = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000030FD File Offset: 0x000012FD
		public override bool CleanScene
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000030FD File Offset: 0x000012FD
		public override bool UseStaticCamera
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000030FD File Offset: 0x000012FD
		public override bool UseStaticWeather
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A819 File Offset: 0x00008A19
		public override void Initialize(ContentManager {16485})
		{
			Global.Game.EvEntryToGame += delegate()
			{
			};
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A844 File Offset: 0x00008A44
		public override void OnBegin()
		{
			Global.Game.IsMouseVisible = false;
			if (!this.PortableMode)
			{
				this.CreateInterface();
				if (Global.Player.MapInfo.IsEducationMap)
				{
					Global.Settings.ResetForNewAccount();
					{18781} {18781} = {18781}.TryCreateWosbIntro();
					if ({18781} == null)
					{
						new {18593}(null);
					}
					else
					{
						{18781}.EvRemoveFromContainer += delegate()
						{
							new {18593}(null);
						};
					}
				}
			}
			this.{16488}();
			this.{16489} = 0;
			this.{16490} = false;
			if (!this.PortableMode)
			{
				if (Global.Player.IsDestroyed)
				{
					Global.Game.GetInterfaceManager.ClearAll();
					new {19215}();
				}
				EducationHelper.EnterToGameFromPort();
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A906 File Offset: 0x00008B06
		public override void OnEnd()
		{
			this.{16489} = 0;
			this.{16490} = false;
			Global.Camera.EnabledMouseRead = true;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A924 File Offset: 0x00008B24
		public override void Update(ref FrameTime {16486})
		{
			if (InputHelper.IsGamepadConnected)
			{
				if (Global.Settings.kb_KeyShowMouse.IsClick)
				{
					if (this.{16490})
					{
						this.{16490} = false;
						this.DecreaseMouse();
					}
					else if (!this.{16490})
					{
						this.{16490} = true;
						this.IncreaseMouse();
					}
				}
			}
			else
			{
				bool isDown = Global.Settings.kb_KeyShowMouse.IsDown;
				if (this.{16490} && !isDown)
				{
					this.{16490} = false;
					this.DecreaseMouse();
				}
				if (!this.{16490} && isDown)
				{
					this.{16490} = true;
					this.IncreaseMouse();
				}
			}
			Global.Game.IsMouseVisible = (this.{16489} != 0);
			Global.Camera.EnabledMouseRead = (this.{16489} == 0);
			if (Global.Player != null && Global.Player.MapInfo.IsWorldmap)
			{
				WeatherEngine weather = Global.Player.MapInfo.Weather;
				Vector2 position = Global.Player.Position;
				if (weather.RainingLevel(position) > 0.1f && !Global.Player.IsEntryToPortZoneContains)
				{
					EducationHelper.MakeFlag(EducationOnboarding.GameTT_OpenStorm, false);
				}
			}
			if (Global.Player != null && Global.Player.MapInfo.IsWorldmap)
			{
				if (Global.Player.IsMarchingMode && Session.TimeFromLastReceivedDamageSec > 100f && Session.TimeFromLastSendedCBDamageSec > 100f && Vector2.Distance(Global.Player.Position, Global.Player.NearPort.EntryPos) > 700f && !Global.Settings.kb_KeyShowMouse.IsDown)
				{
					this.{16491} += {16486}.msElapsed;
				}
				else
				{
					this.{16491} = 0f;
				}
				Geometry.InverseLerp(50000f, 100000f, this.{16491});
				return;
			}
			Global.Game.GetInterfaceManager.Host.Opacity = 1f;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000AAFC File Offset: 0x00008CFC
		public override void Render2D()
		{
			Engine.GS.SetTexture(AtlasGameGui.Texture);
			if (Debugging.ShowDropIslePositions)
			{
				foreach (Vector2 {11452} in ((IEnumerable<Vector2>)Gameplay.WorldMap.PositionsForDropInIsle))
				{
					Vector3 vector = {11452}.X0Y();
					if (Engine.GS.Camera.IsVisible(vector, 1f))
					{
						Vector2 projection = Engine.GS.Camera.GetProjection(ref vector);
						Device gs = Engine.GS;
						Rectangle rectangle = new Marker(ref projection, 8f, 8f).Offset(-4f, -4f).ToRect();
						Color color = Color.Cyan;
						gs.Draw(AtlasGameGui.rect_asset_whitepixel_1px, rectangle, color);
					}
				}
				Vector2 vector2 = Engine.GS.Camera.Position.XZ();
				for (int i = 0; i < 30000; i++)
				{
					WorldMapInfo worldMap = Gameplay.WorldMap;
					Vector3 vector3 = new Vector3(vector2.X, vector2.Y, 1000f);
					Vector3 vector4 = worldMap.RandomPosition(vector3, 0f, 0f, null).X0Y();
					if (Engine.GS.Camera.IsVisible(vector4, 1f))
					{
						Vector2 projection2 = Engine.GS.Camera.GetProjection(ref vector4);
						Device gs2 = Engine.GS;
						Rectangle rectangle = new Marker(ref projection2, 8f, 8f).Offset(-4f, -4f).ToRect();
						Color color = Color.Cyan;
						gs2.Draw(AtlasGameGui.rect_asset_whitepixel_1px, rectangle, color);
					}
				}
			}
			Engine.GS.SetTexture(AtlasObjs.Texture);
			Global.Game.StaticSystem.Render2DItems();
			Global.Game.WorldInstance.Render2D();
			Engine.GS.SetTexture(AtlasGameGui.Texture);
			if (Global.Camera.CameraEffects.MortarSpyglassEffect > 0f)
			{
				Device gs3 = Engine.GS;
				Rectangle rectangle = new Rectangle(932, 1869, 209, 109);
				Rectangle uiarea = Engine.GS.UIArea;
				Color color = Color.White * Global.Camera.CameraEffects.MortarSpyglassEffect;
				gs3.Draw(rectangle, uiarea, color);
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000AD54 File Offset: 0x00008F54
		public void IncreaseMouse()
		{
			if (this.{16489} == 0)
			{
				this.{16487}();
			}
			this.{16489}++;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000AD72 File Offset: 0x00008F72
		public void DecreaseMouse()
		{
			if (this.{16489} == 1)
			{
				this.{16488}();
			}
			if (this.{16489} != 0)
			{
				this.{16489}--;
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00003100 File Offset: 0x00001300
		private void {16487}()
		{
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000AD99 File Offset: 0x00008F99
		private void {16488}()
		{
			InputHelper.UnFullReset();
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000ADA0 File Offset: 0x00008FA0
		public void CreateInterface()
		{
			if (!Global.Player.MapInfo.IsEducationMap && !CommonWorldShader.GraphicsEngineerMode)
			{
				new {19891}();
				new {19548}();
				new {20059}();
				new {19970}();
				new {19717}();
				new {22478}(false);
				if (Session.CurrentArenaSession != null && !Session.CurrentArenaSession.ModeInfo.IsDuetl)
				{
					new {18937}();
				}
			}
			new {19994}();
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000AE0E File Offset: 0x0000900E
		public void CreateSlimInterface()
		{
			new {19891}();
			new {19717}();
			new {22478}(false);
			new {19994}();
		}

		// Token: 0x04000112 RID: 274
		private static int inputToGameCounter;

		// Token: 0x04000113 RID: 275
		private int {16489};

		// Token: 0x04000114 RID: 276
		private bool {16490};

		// Token: 0x04000115 RID: 277
		private float {16491};

		// Token: 0x04000116 RID: 278
		public bool PortableMode;
	}
}
