using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Core;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Graphics.Effects;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.GameSystems
{
	// Token: 0x020004CD RID: 1229
	internal sealed class WorldInstance : GameSceneSystem
	{
		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x000F247A File Offset: 0x000F067A
		public int TotalShipsCount
		{
			get
			{
				return this.{24949}.Size + this.{24951}.Size;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x000F2493 File Offset: 0x000F0693
		public Tlist<Isle> MapObjectLayer
		{
			get
			{
				return this.{24953};
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06001B09 RID: 6921 RVA: 0x000F249B File Offset: 0x000F069B
		public ShipSilhouettesManager ShipSilhouettes
		{
			get
			{
				return this.{24983};
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x000F24A3 File Offset: 0x000F06A3
		public ShallowsDetection ShallowsDetectionComponent
		{
			get
			{
				return this.{24984};
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06001B0B RID: 6923 RVA: 0x000F24AB File Offset: 0x000F06AB
		public int CannonballCount
		{
			get
			{
				return this.{24954}.Size;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x000F24B8 File Offset: 0x000F06B8
		public Tlist<ClientDrop> DropList
		{
			get
			{
				return this.{24945};
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06001B0D RID: 6925 RVA: 0x000F24C0 File Offset: 0x000F06C0
		public Tlist<ShipNpc> NpcList
		{
			get
			{
				return this.{24949};
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x000F24C8 File Offset: 0x000F06C8
		public Tlist<ShipOtherPlayer> PlayersList
		{
			get
			{
				return this.{24951};
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06001B0F RID: 6927 RVA: 0x000F24D0 File Offset: 0x000F06D0
		public Tlist<ClientCannonBall> CannonBalls
		{
			get
			{
				return this.{24954};
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x000F24D8 File Offset: 0x000F06D8
		public WorldMapInfo LoadedMap
		{
			get
			{
				return this.{24968};
			}
		}

		// Token: 0x06001B12 RID: 6930 RVA: 0x000F255C File Offset: 0x000F075C
		public override void Initialize(ContentManager {24849})
		{
			base.Initialize({24849});
			this.{24943} = new Tlist<BoardingHookRenderer>();
			this.{24944} = new Tlist<GameEffect>(10);
			this.{24946} = new UWEPool<ClientDrop>(40);
			this.{24945} = new Tlist<ClientDrop>(this.{24946}.Capacity);
			this.{24948} = new UWEPool<ShipFloodingDecoration>(20);
			this.{24947} = new Tlist<ShipFloodingDecoration>(this.{24948}.Capacity);
			this.{24950} = new UWEPool<ShipNpc>(40);
			this.{24949} = new Tlist<ShipNpc>(this.{24950}.Capacity);
			this.{24952} = new UWEPool<ShipOtherPlayer>(40);
			this.{24951} = new Tlist<ShipOtherPlayer>(this.{24952}.Capacity);
			this.{24953} = new Tlist<Isle>(100);
			this.MapVisibleObjectLayer = new Tlist<Isle>(100);
			this.{24955} = new UWEPool<ClientCannonBall>(300);
			this.{24954} = new Tlist<ClientCannonBall>(this.{24955}.Capacity);
			this.{24957} = new UWEPool<WoodDebris>(700);
			this.{24956} = new Tlist<WoodDebris>(this.{24957}.Capacity);
			this.{24958} = new InstancedModelRenderer();
			this.{24959} = new ModelTransformedScene();
			this.{24961} = new UWEPool<FireAreaVisualizer>(60);
			this.{24960} = new Tlist<FireAreaVisualizer>(this.{24961}.Capacity);
			this.{24963} = new UWEPool<ClientPowderKeg>(35);
			this.{24962} = new Tlist<ClientPowderKeg>(this.{24963}.Capacity);
			this.{24965} = new UWEPool<ClientMortarShot>(40);
			this.{24964} = new Tlist<ClientMortarShot>(this.{24965}.Capacity);
			this.{24966} = new Tlist<GunLightInReflection>(100);
			this.{24967} = new UWEPool<GunLightInReflection>(100);
			this.{24942} = new CountingSort<IClientShip>(0, 100, 50);
			this.{24971} = new Tlist<CannonBallWithWorldObjCollisionData>(10);
			this.{24972} = new Tlist<CannonBallWithWorldObjCollisionData>(10);
			Global.Game.EvEntryToGame += this.{24939};
			this.{24969} = new SpriteBatch3D<VertexPositionColorTexture>(this.{24955}.Capacity * 6);
			this.{24981} = new EnvironmentGraphics();
			this.{24983} = new ShipSilhouettesManager();
			this.{24984} = new ShallowsDetection();
			this.{24982} = new WindEnvironmentEffect(new Rectangle(2428, 261, 128, 128), AtlasObjs.Texture.Size, 50);
			this.windDir = BillboardParent_VPCT.CreatePlane(1f, 1f, 1f);
			this.windDir.SetUV(new Rectangle(2428, 261, 128, 128), AtlasObjs.Texture.Size);
			this.balloonDir = BillboardParent_VPCT.CreatePlane(1f, 1f, 1f);
			this.balloonDir.SetUV(new Rectangle(1995, 68, 168, 192), AtlasObjs.Texture.Size);
			InGameSightUi.CurrentInstance = new InGameSightUi();
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x000F2853 File Offset: 0x000F0A53
		public override void On()
		{
			InGameSightUi.CurrentInstance.Reset();
			base.On();
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000F2868 File Offset: 0x000F0A68
		public override void Off()
		{
			this.{24946}.Return(this.{24945});
			this.{24948}.Return(this.{24947});
			this.{24950}.Return(this.{24949});
			this.{24952}.Return(this.{24951});
			this.{24976}.Clear();
			this.{24975}.Clear();
			this.{24986} = 0;
			this.{24987} = 0;
			this.{24954}.Foreach(delegate(ClientCannonBall {24997})
			{
				{24997}.RemoveEffects();
			}, 0, this.{24954}.Size);
			this.{24955}.Return(this.{24954});
			this.{24961}.Return(this.{24960});
			this.{24963}.Return(this.{24962});
			this.{24965}.Return(this.{24964});
			this.{24967}.Return(this.{24966});
			this.{24971}.Clear();
			this.{24972}.Clear();
			this.{24957}.Return(this.{24956});
			this.{24977}.Clear();
			foreach (Isle isle in ((IEnumerable<Isle>)this.{24953}))
			{
				isle.Dispose();
			}
			this.{24953}.Clear();
			this.MapVisibleObjectLayer.Clear();
			this.{24968} = null;
			this.{24983}.Clean();
			this.{24943}.Clear();
			InGameSightUi.CurrentInstance.Reset();
			if (Global.Player != null)
			{
				Global.Player.ClearResources();
			}
			Global.Player = null;
			base.Off();
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000F2A34 File Offset: 0x000F0C34
		public override void Update(ref FrameTime {24850})
		{
			if (base.IsActive)
			{
				if (Global.Game.GetCurrentSceneName != GameSceneName.Entry)
				{
					InGameSightUi.CurrentInstance.Update(ref {24850});
				}
				if (Global.Game.GetCurrentSceneName != GameSceneName.Entry)
				{
					this.{24984}.Update(ref {24850});
					Global.Player.Update(ref {24850});
					try
					{
						for (int i = 0; i < this.{24976}.Size; i++)
						{
							if (Vector2.DistanceSquared(this.{24976}.Array[i].Position, Global.Player.Position) > 122500f)
							{
								this.RemoveDecoration(this.{24976}.Array[i]);
								i--;
							}
						}
					}
					catch (Exception {25356})
					{
						this.{24976}.Clear();
						Helpers.SendError({25356}, "decoShipsRemoveByDistance-Update", false, false);
					}
				}
				for (int j = 0; j < this.{24949}.Size; j++)
				{
					this.{24949}.Array[j].Update(ref {24850});
				}
				for (int k = 0; k < this.{24951}.Size; k++)
				{
					ShipOtherPlayer shipOtherPlayer = this.{24951}.Array[k];
					if (!shipOtherPlayer.IsDecoration || shipOtherPlayer.IsDecorationVisible)
					{
						shipOtherPlayer.Update(ref {24850});
					}
				}
				for (int l = 0; l < this.{24947}.Size; l++)
				{
					ShipFloodingDecoration shipFloodingDecoration = this.{24947}.Array[l];
					if (shipFloodingDecoration.Update(ref {24850}))
					{
						this.{24947}.FastRemoveAt(l);
						this.{24948}.Add(shipFloodingDecoration);
						l--;
					}
				}
				ClientDrop nearDrop = null;
				if (Global.Player != null && !Global.Player.IsDestroyed && !Global.Player.UsedShip.StaticInfo.IsBalloon)
				{
					float num = float.MaxValue;
					for (int m = 0; m < this.{24945}.Size; m++)
					{
						ClientDrop clientDrop = this.{24945}.Array[m];
						if (clientDrop.forgetAnimation == 0f)
						{
							float num2 = Vector2.DistanceSquared(clientDrop.Position, Global.Player.Position);
							if (num2 < num)
							{
								nearDrop = clientDrop;
								num = num2;
							}
						}
					}
				}
				ClientDrop.nearDrop = nearDrop;
				bool flag = false;
				for (int n = 0; n < this.{24945}.Size; n++)
				{
					this.{24945}.Array[n].UpdateClient(ref {24850}, out flag);
					if (flag)
					{
						this.{24946}.Add(this.{24945}.Array[n]);
						this.{24945}.FastRemoveAt(n);
						n--;
					}
				}
				int num3 = 0;
				int num4 = -1;
				bool flag2 = false;
				this.{24974}++;
				for (int num5 = 0; num5 < this.{24954}.Size; num5++)
				{
					ClientCannonBall clientCannonBall = this.{24954}.Array[num5];
					clientCannonBall.Update(ref {24850});
					CannonBall.HitType {16570};
					if (clientCannonBall.IsDestructed)
					{
						this.{24954}.FastRemoveAt(num5);
						this.{24955}.Add(clientCannonBall);
						num5--;
					}
					else if ((clientCannonBall.SenderUID == Global.Player.uID || num3 < 40) && clientCannonBall.uID % 2 == this.{24974} % 2 && clientCannonBall.CheckCollisionWithBuildings(out {16570}, out flag2, out num4, this.MapVisibleObjectLayer))
					{
						Ship shipFromUID = this.GetShipFromUID(clientCannonBall.SenderUID);
						if (clientCannonBall.SenderUID == Global.Player.uID || shipFromUID is Npc)
						{
							Tlist<CannonBallWithWorldObjCollisionData> tlist = this.{24971};
							CannonBallWithWorldObjCollisionData cannonBallWithWorldObjCollisionData = default(CannonBallWithWorldObjCollisionData);
							cannonBallWithWorldObjCollisionData.CannonBallUID = clientCannonBall.uID;
							cannonBallWithWorldObjCollisionData.Arg = ((clientCannonBall.HitPoint.Y < 1.9f) ? -1 : num4);
							tlist.Add(cannonBallWithWorldObjCollisionData);
						}
						clientCannonBall.CreateEffects({16570});
						this.{24954}.FastRemoveAt(num5);
						this.{24955}.Add(clientCannonBall);
						num5--;
					}
					if (flag2)
					{
						num3++;
					}
					flag2 = false;
				}
				for (int num6 = 0; num6 < this.{24962}.Size; num6++)
				{
					if (this.{24962}.Array[num6].Update(ref {24850}))
					{
						this.{24963}.Add(this.{24962}.Array[num6]);
						this.{24962}.FastRemoveAt(num6);
						num6--;
					}
				}
				for (int num7 = 0; num7 < this.{24956}.Size; num7++)
				{
					WoodDebris woodDebris = this.{24956}.Array[num7];
					woodDebris.Update(ref {24850}, true, out flag, this.{24956}.Size);
					if (flag)
					{
						this.{24956}.FastRemoveAt(num7);
						this.{24957}.Add(woodDebris);
						num7--;
					}
				}
				for (int num8 = 0; num8 < this.{24964}.Size; num8++)
				{
					ClientMortarShot clientMortarShot = this.{24964}.Array[num8];
					if (clientMortarShot.Update(this.MapVisibleObjectLayer, ref {24850}))
					{
						if (clientMortarShot.CollisionWithObject && clientMortarShot.SenderObjectUID == Global.Player.uID)
						{
							Tlist<CannonBallWithWorldObjCollisionData> tlist2 = this.{24972};
							CannonBallWithWorldObjCollisionData cannonBallWithWorldObjCollisionData = default(CannonBallWithWorldObjCollisionData);
							cannonBallWithWorldObjCollisionData.CannonBallUID = clientMortarShot.uID;
							cannonBallWithWorldObjCollisionData.Arg = clientMortarShot.CollisionIndex;
							tlist2.Add(cannonBallWithWorldObjCollisionData);
						}
						this.{24965}.Add(this.{24964}.Array[num8]);
						this.{24964}.FastRemoveAt(num8);
						num8--;
					}
				}
				for (int num9 = 0; num9 < this.{24943}.Size; num9++)
				{
					if (this.{24943}.Array[num9].Update(ref {24850}))
					{
						this.{24943}.FastRemoveAt(num9);
						num9--;
					}
				}
				Vector2 windXZNormal = CommonGlobal.CurrentClientWeather.WindXZNormal;
				this.LastWindAxis = MathF.Atan2(windXZNormal.Y, windXZNormal.X);
				flag = false;
				for (int num10 = 0; num10 < this.{24944}.Size; num10++)
				{
					this.{24944}.Array[num10].Update(ref {24850}, out flag);
					if (flag)
					{
						this.{24944}.FastRemoveAt(num10);
						num10--;
					}
				}
				for (int num11 = 0; num11 < this.{24960}.Size; num11++)
				{
					if (this.{24960}.Array[num11].Update(ref {24850}))
					{
						this.{24961}.Add(this.{24960}.Array[num11]);
						this.{24960}.FastRemoveAt(num11);
						num11--;
					}
				}
				for (int num12 = 0; num12 < this.{24966}.Size; num12++)
				{
					if (this.{24966}.Array[num12].Update(ref {24850}))
					{
						this.{24967}.Add(this.{24966}.Array[num12]);
						this.{24966}.FastRemoveAt(num12);
						num12--;
					}
				}
				this.MapVisibleObjectLayer.Clear();
				Vector2 vector = Engine.GS.Camera.Position.XZ();
				for (int num13 = 0; num13 < this.{24953}.Size; num13++)
				{
					Isle isle = this.{24953}.Array[num13];
					float num14;
					Vector2.DistanceSquared(ref vector, ref isle.Statement.GlobalPosition, out num14);
					float num15 = (Renderer.CameraFarPlane + isle.Statement.ModelGlobalBS.Radius * 1.2f) * isle.farDistMultiplier;
					if (num14 > num15 * num15)
					{
						isle.IsVisibleByDist = false;
					}
					else
					{
						isle.Update(ref {24850}, ref vector);
						if (isle.IsVisibleByDist)
						{
							this.MapVisibleObjectLayer.Add(isle);
						}
					}
				}
				if (this.{24973}.Sample(ref {24850}))
				{
					if (this.{24971}.Size != 0)
					{
						Global.Network.Send(new OnBallsCollisionWithObjectMsg(false, this.{24971}));
						this.{24971}.Clear();
					}
					if (this.{24972}.Size != 0)
					{
						Global.Network.Send(new OnBallsCollisionWithObjectMsg(true, this.{24972}));
						this.{24972}.Clear();
					}
				}
				this.{24983}.Update(ref {24850});
				if (Global.Game.GetCurrentSceneName == GameSceneName.Game && (this.{24986} != Global.Game.InteractiveWorldSystem.VisibleWorldQuests.Size || this.{24987} != Session.Account.Quests.ProgressRunningQuests.Size))
				{
					this.UpdateQuestsInSea();
				}
				if (Global.Player != null && Global.Settings.RendererSsaoAndRefractions)
				{
					float val = (CommonGlobal.CurrentClientWeather.RainingLevelClient - 0.3f) / 0.7f;
					if (CommonGlobal.CurrentClientWeather.WavesHeightClient > 0.8f)
					{
						int num16 = 0;
						while ((float)num16 < 50f * Math.Min(1f, val))
						{
							Vector2 vector2 = Global.Player.Position + Rand.DiskVector2(0f) * 100f;
							if (CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, vector2.X, vector2.Y) > CommonGlobal.CurrentClientWeather.WavesHeightClient * 0.1f)
							{
								FXEngine.WaterParticleLight(vector2.X0Y(), 2f, true, true);
							}
							num16++;
						}
					}
				}
			}
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000F338C File Offset: 0x000F158C
		public void Render2D()
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Entry)
			{
				return;
			}
			for (int i = 0; i < this.{24944}.Size; i++)
			{
				if (this.{24944}.Array[i].IsAlive)
				{
					this.{24944}.Array[i].Render2D();
				}
			}
			for (int j = 0; j < this.{24945}.Size; j++)
			{
				this.{24945}.Array[j].Render2D();
			}
			Global.Game.InteractiveWorldSystem.RenderBuildings2D();
			WorldInstance.<>c__DisplayClass79_0 CS$<>8__locals1;
			CS$<>8__locals1.camPos = Engine.GS.Camera.Position.XZ();
			foreach (ShipNpc shipNpc in ((IEnumerable<ShipNpc>)this.{24949}))
			{
				if (shipNpc.Client.IsVisible)
				{
					this.{24942}.Append(WorldInstance.<Render2D>g__distanceKey|79_0(shipNpc, ref CS$<>8__locals1), shipNpc);
				}
			}
			foreach (ShipOtherPlayer shipOtherPlayer in ((IEnumerable<ShipOtherPlayer>)this.{24951}))
			{
				if (shipOtherPlayer.Client.IsVisible && !shipOtherPlayer.IsDecoration)
				{
					this.{24942}.Append(WorldInstance.<Render2D>g__distanceKey|79_0(shipOtherPlayer, ref CS$<>8__locals1), shipOtherPlayer);
				}
			}
			this.{24942}.Sort();
			foreach (IClientShip clientShip in ((IEnumerable<IClientShip>)this.{24942}.LastResult))
			{
				clientShip.Render2D();
				clientShip.GetClient.RenderText();
			}
			using (IEnumerator<ShipSilhouettesManager.LocalObject> enumerator4 = ((IEnumerable<ShipSilhouettesManager.LocalObject>)this.{24983}.SceneObjects).GetEnumerator())
			{
				while (enumerator4.MoveNext())
				{
					ShipSilhouettesManager.LocalObject item = enumerator4.Current;
					if (item.Model.IsMainCameraVisible)
					{
						Rectangle rectangle = default(Rectangle);
						if (Session.LastMinimapAndGroupUpdate.enemies.Contains((EnemyStateTransfer {25008}) => {25008}.ShipUID == item.uID))
						{
							rectangle = AtlasObjs.c_shipMarkerByMinimap_targetRed;
						}
						else if (Session.LastMinimapAndGroupUpdate.allies.Contains((AllyStateTransfer {25009}) => {25009}.uID == item.uID))
						{
							rectangle = AtlasObjs.c_shipMarkerByMinimap_green;
						}
						if (rectangle.Width > 0)
						{
							Vector3 {14982} = ShipPartial.ComputeTopPoisiton(item.transform.Translation, Vector3.Distance(item.transform.Translation, Engine.GS.Camera.Position), item.Model.CombinedModelSpaceBS.Radius * item.transform.Scales.X);
							ShipPartial.DrawMarker(Global.Camera.GetProjection({14982}), rectangle, Vector2.Zero, 16f / (float)rectangle.Width, Color.White * 0.5f * item.transparancy);
						}
					}
				}
			}
			if (Global.Player != null)
			{
				Vector2 position = Global.Player.ReconstructPosition(Session.LastPing, new ShipPositionInfo(Global.Player.Client.ServerPositionDisplay.Position.XZ(), 0f)).Position;
				Global.Player.Client.ServerPositionDisplay.Display(Global.Player.Position3D, new Vector3(Global.Player.CreateServerTransform.Position.X, Global.Player.Position3D.Y, Global.Player.CreateServerTransform.Position.Y), new Vector3(position.X, Global.Player.Position3D.Y, position.Y));
				Debugging.Draw2D();
			}
			if (Global.Game.GetCurrentSceneName == GameSceneName.Game)
			{
				InGameSightUi.CurrentInstance.Render2D();
			}
			this.{24984}.Render2D();
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000F37E4 File Offset: 0x000F19E4
		public void Render3DCannonBalls()
		{
			if (Global.Player != null)
			{
				Vector3 position = Engine.GS.Camera.Position;
				this.{24969}.Reset();
				for (int i = 0; i < this.{24954}.Size; i++)
				{
					this.{24954}.Array[i].AddGeometry(ref position, this.{24969});
				}
				for (int j = 0; j < this.{24964}.Size; j++)
				{
					this.{24964}.Array[j].AddGeometry(ref position, this.{24969});
				}
				Global.Render.ItemsShader.SetForRender(Matrix.Identity, Vector4.One);
				Global.Render.ItemsShader.BeginPass(true, false);
				this.{24969}.Render(null);
			}
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000F38AC File Offset: 0x000F1AAC
		public void Render3DStaticItems()
		{
			if (Global.Player != null)
			{
				if (Global.Render.UiMode == InterfaceMode.Default)
				{
					this.{24981}.Render();
				}
				this.{24984}.Render3DStatics();
				if (this.{24944}.Size != 0)
				{
					float fogNearDistance = Global.Render.ItemsShader.fogNearDistance;
					float fogFarDistance = Global.Render.ItemsShader.fogFarDistance;
					Global.Render.ItemsShader.ManualSetFog(Renderer.FogStart_Isles, Renderer.FogEnd_Isles);
					for (int i = 0; i < this.{24944}.Size; i++)
					{
						this.{24944}.Array[i].Render3D();
					}
					Global.Render.ItemsShader.ManualSetFog(fogNearDistance, fogFarDistance);
				}
				if (Global.Game.GetCurrentSceneName == GameSceneName.Game && !Global.Player.IsDestroyed)
				{
					InGameSightUi.CurrentInstance.Render3D();
				}
				if (Global.Game.GetCurrentSceneName == GameSceneName.Port && {22279}.CurrentInstance != null)
				{
					InGameSightUi.CurrentInstance.Render3DForCannonsEquip();
				}
				for (int j = 0; j < this.{24960}.Size; j++)
				{
					this.{24960}.Array[j].Render();
				}
				for (int k = 0; k < this.{24962}.Size; k++)
				{
					this.{24962}.Array[k].RenderStatics();
				}
				for (int l = 0; l < this.{24945}.Size; l++)
				{
					this.{24945}.Array[l].RenderStatic();
				}
				if (Global.Player.MapInfo.WindEnable && Global.Game.GetCurrentSceneName == GameSceneName.Game)
				{
					Vector2 windXZNormal = CommonGlobal.CurrentClientWeather.WindXZNormal;
					float num = Vector2.Dot(windXZNormal, Global.Player.FastNormal);
					num = 0.75f + 0.25f * Math.Abs(num);
					Color value = Color.Lerp(Color.Orange, Color.SkyBlue, (Global.Player.physicsBody.LastWindEffectivity < 1f) ? (Global.Player.physicsBody.LastWindEffectivity * 0.8f) : 1f);
					this.LastWindColor = Color.Lerp(value, Color.Lime, Global.Player.PalyerMarchingModeBonus);
					if (Global.Settings.WindArrowMode == 2 && Global.Render.UiMode == InterfaceMode.Default && !Global.Player.MapInfo.IsEducationMap)
					{
						double num2 = 4.0;
						float num3 = 0.3f;
						Vector3 value2 = new Vector3(Global.Player.Position3D.X + windXZNormal.X * Global.Player.UsedShip.StaticInfo.BSRadius * 0.5f * num, 0f, Global.Player.Position3D.Z + windXZNormal.Y * Global.Player.UsedShip.StaticInfo.BSRadius * 0.5f * num);
						Vector3 value3 = new Vector3(Global.Player.Position3D.X + windXZNormal.X * (Global.Player.UsedShip.StaticInfo.BSRadius * 0.5f + 3f) * num, 0f, Global.Player.Position3D.Z + windXZNormal.Y * (Global.Player.UsedShip.StaticInfo.BSRadius * 0.5f + 3f) * num);
						Engine.GS.SetDepthBuffer(DepthBuffer.WithoutDepth);
						int num4 = 0;
						while ((double)num4 < num2)
						{
							float num5 = (float)((Global.Game.GameTotalTimeSec / 5.0 + (double)num4 / num2) % 1.0);
							Vector3 translation = Vector3.Lerp(value2, value3, num5);
							float scaleFactor = Geometry.Saturate(num5 / num3) * Geometry.Saturate((1f - num5) / num3);
							translation.Y = Global.Player.Position3D.Y - 2f;
							this.{24985}.Translation = translation;
							this.{24985}.Scales = new Vector3(2.4f);
							this.{24985}.Yaw = this.LastWindAxis;
							Global.Render.ItemsShader.SetForRender(this.{24985}.CreateWorldMatrix(), this.LastWindColor.ToVector4() * scaleFactor);
							Global.Render.ItemsShader.BeginPass(true, true);
							this.windDir.Render();
							num4++;
						}
						Engine.GS.SetDepthBuffer(DepthBuffer.ReadOnly);
					}
				}
				WhaleHarpoonController.DrawSight();
				if (Global.Player.UsedShip.StaticInfo.IsBalloon)
				{
					for (int m = 0; m < Global.Player.FirstController.LinearStateCode; m++)
					{
						this.{24985}.Translation = new Vector3(Global.Player.Position3D.X + Global.Player.FastNormal.X * Global.Player.UsedShip.StaticInfo.BSRadius * (0.4f + (float)m * 0.2f), Global.Player.Position3D.Y - 1.75f, Global.Player.Position3D.Z + Global.Player.FastNormal.Y * Global.Player.UsedShip.StaticInfo.BSRadius * (0.4f + (float)m * 0.2f));
						this.{24985}.Scales = new Vector3(1.4f);
						this.{24985}.Yaw = Global.Player.Rotation;
						Global.Render.ItemsShader.SetForRender(this.{24985}.CreateWorldMatrix(), (Color.White * 0.5f).ToVector4());
						Global.Render.ItemsShader.BeginPass(true, true);
						this.balloonDir.Render();
					}
				}
				Global.Player.Client.DrawEffectsAndDecals();
				Global.Player.Client.OcclusionQuery();
				for (int n = 0; n < this.{24949}.Size; n++)
				{
					ShipNpc shipNpc = this.{24949}.Array[n];
					if (shipNpc.Client.IsVisible)
					{
						shipNpc.Client.DrawEffectsAndDecals();
						shipNpc.Client.OcclusionQuery();
					}
				}
				for (int num6 = 0; num6 < this.{24951}.Size; num6++)
				{
					ShipOtherPlayer shipOtherPlayer = this.{24951}.Array[num6];
					if ((!shipOtherPlayer.IsDecoration || shipOtherPlayer.IsDecorationVisible) && shipOtherPlayer.Client.IsVisible)
					{
						shipOtherPlayer.Client.DrawEffectsAndDecals();
						shipOtherPlayer.Client.OcclusionQuery();
					}
				}
				if (Debugging.DebugInfo)
				{
					for (int num7 = 0; num7 < this.{24962}.Size; num7++)
					{
						ClientPowderKeg clientPowderKeg = this.{24962}.Array[num7];
						if (clientPowderKeg.Info.TriggerRadius != 0f)
						{
							float num8 = clientPowderKeg.Info.TriggerRadius;
							if (clientPowderKeg.SourceUID == Global.Player.uID)
							{
								num8 *= 1f + Global.Player.UsedShip.PowderKegIncreaseTriggerRadiusBonus;
							}
							Global.Render.ItemsShader.RenderCircle(new Vector3(clientPowderKeg.Position.X, 0f, clientPowderKeg.Position.Y), num8 - 0.5f, num8, Color.Red, GPUCircleType.SoftCircle, null);
							Global.Render.ItemsShader.RenderCircle(new Vector3(clientPowderKeg.Position.X, 0f, clientPowderKeg.Position.Y), clientPowderKeg.Info.DamageRadius - 0.5f, clientPowderKeg.Info.DamageRadius, Color.Yellow, GPUCircleType.SoftCircle, null);
						}
					}
				}
				for (int num9 = 0; num9 < this.{24962}.Size; num9++)
				{
					ClientPowderKeg clientPowderKeg2 = this.{24962}.Array[num9];
					if (clientPowderKeg2.Info.IsInvisibleMine && (clientPowderKeg2.SourceUID == Global.Player.uID || Session.IsShipContainsPlayerGroup(clientPowderKeg2.SourceUID)))
					{
						Global.Render.ItemsShader.RenderCircle(new Vector3(clientPowderKeg2.Position.X, 0f, clientPowderKeg2.Position.Y), clientPowderKeg2.Info.TriggerRadius - 0.5f, clientPowderKeg2.Info.TriggerRadius, Color.Red * 0.4f, GPUCircleType.SoftCircle, null);
					}
				}
				if (Global.Game != null)
				{
					Global.Render.ItemsShader.Texture.SetValue(OtherTextures.WindTrailTexture);
					Global.Render.ItemsShader.SetForRender(Matrix.Identity, Vector4.One);
					Global.Render.ItemsShader.BeginPass(true, false);
					Global.Render.ItemsShader.Texture.SetValue(AtlasObjs.Texture.Tex);
				}
			}
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000F41C0 File Offset: 0x000F23C0
		public void Render3DForReflections()
		{
			this.{24969}.Reset();
			for (int i = 0; i < this.{24966}.Size; i++)
			{
				this.{24966}.Array[i].Render(this.{24969});
			}
			Global.Render.ItemsShader.SetForRender(Matrix.Identity, Vector4.One);
			Global.Render.ItemsShader.BeginPass(true, true);
			this.{24969}.Render(null);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x000F423C File Offset: 0x000F243C
		public void Render3DShips()
		{
			Global.RenderStats.ShipAllCount = this.{24949}.Size + this.{24951}.Size;
			Global.RenderStats.ShipRenderCount = 0;
			for (int i = 0; i < this.{24949}.Size; i++)
			{
				ShipNpc shipNpc = this.{24949}.Array[i];
				if (shipNpc.Client.IsVisible)
				{
					Global.RenderStats.ShipRenderCount++;
					shipNpc.Client.RenderModel();
				}
			}
			Global.RenderStats.ShipPlayersRenderCount = 0;
			for (int j = 0; j < this.{24951}.Size; j++)
			{
				ShipOtherPlayer shipOtherPlayer = this.{24951}.Array[j];
				if ((!shipOtherPlayer.IsDecoration || shipOtherPlayer.IsDecorationVisible) && shipOtherPlayer.Client.IsVisible)
				{
					Global.RenderStats.ShipPlayersRenderCount++;
					shipOtherPlayer.Client.RenderModel();
				}
			}
			if (Global.Game.GetCurrentSceneName != GameSceneName.Entry && (Global.Camera.Zoom > 1f || Global.Camera.IsFreeMode) && (!Global.Player.IsDestroyed || Global.Player.UsedShipPlayer.CraftFrom.ID == 1))
			{
				Global.Render.CommonShader.DrawPayerLighter(true);
				Global.Render.CommonShader.SetMinShadowCascadeIs1(false);
				Global.Player.Client.RenderModel();
				Global.Render.CommonShader.SetMinShadowCascadeIs1(true);
				Global.Render.CommonShader.DrawPayerLighter(false);
			}
			this.{24983}.Render();
			if (Renderer.ReflectionsAreBeingDrawn)
			{
				this.{24983}.RenderTransparancy();
			}
			if (!Renderer.ReflectionsAreBeingDrawn)
			{
				if (Global.Player != null && Global.Player.MapInfo.Ports.Size > 0 && !Global.Player.IsPortEntry)
				{
					this.{24978}.Update(Global.Player.NearPort.EntryPos, Global.Player.NearPort.GlobalEntryRadius, LocalContent.Loaded.Buoys[1]);
					this.{24978}.Draw(false);
				}
				else
				{
					this.{24978}.Clean();
				}
				ShipCurrentPlayer player = Global.Player;
				if (player != null && player.MapInfo.IsWorldmap)
				{
					EventActionBase eventActionBase = Session.EventActionsPipeline.CurrentActions.FirstOrDefault(delegate(EventActionBase {24998})
					{
						EABehavior1 eabehavior2 = {24998}.Behavior as EABehavior1;
						return eabehavior2 != null && eabehavior2.Category == EActionCaterory.RandomPvpArena;
					});
					if (eventActionBase != null)
					{
						EABehavior1 eabehavior = eventActionBase.Behavior as EABehavior1;
						this.{24980}.Update(eabehavior.Position, (float)eabehavior.Argument2Int, LocalContent.Loaded.Buoys[0]);
						this.{24980}.Draw(false);
						goto IL_2BA;
					}
				}
				this.{24980}.Clean();
				IL_2BA:
				if (Global.Player != null && Global.Player.MapInfo.IsCircularShape)
				{
					this.{24979}.Update(Vector2.Zero, Global.Player.MapInfo.MapSize.X * 0.5f, LocalContent.Loaded.Buoys[2]);
					this.{24979}.Draw(false);
				}
				else
				{
					this.{24979}.Clean();
				}
				for (int k = 0; k < this.{24943}.Size; k++)
				{
					this.{24943}.Array[k].Render3D();
				}
			}
			for (int l = 0; l < this.{24947}.Size; l++)
			{
				this.{24947}.Array[l].Render();
			}
			if (Global.Player != null && Global.Player.MapInfo.IsWorldmap && Global.Settings.RendererSsaoAndRefractions && {18560}.closed && !Renderer.ReflectionsAreBeingDrawn)
			{
				WorldBitmap shallows = Global.Player.MapInfo.Shallows;
				Vector2 vector = Engine.GS.Camera.Position.XZ();
				float bilinear = shallows.GetBilinear(vector, delegate(byte {24999})
				{
					if ({24999} <= 0)
					{
						return 0f;
					}
					return 1f;
				});
				if (bilinear > 0f)
				{
					LocalContent.Loaded.Shallow.Transform.MiddleScale = 0.25500003f;
					Transform3D transform = LocalContent.Loaded.Shallow.Transform;
					transform.Scales.X = transform.Scales.X * 0.6f;
					Transform3D transform2 = LocalContent.Loaded.Shallow.Transform;
					transform2.Scales.Z = transform2.Scales.Z * 0.6f;
					int num = 550;
					for (int m = 0; m < 2; m++)
					{
						for (int n = 0; n < 2; n++)
						{
							LocalContent.Loaded.Shallow.Transform.Translation.X = MathF.Round(Engine.GS.Camera.Position.X / (float)num + (float)m - 0.5f) * (float)num;
							LocalContent.Loaded.Shallow.Transform.Translation.Z = MathF.Round(Engine.GS.Camera.Position.Z / (float)num + (float)n - 0.5f) * (float)num;
							LocalContent.Loaded.Shallow.Transform.Translation.Y = MathHelper.Lerp(-3f, 3f, bilinear) - Math.Max(0f, CommonGlobal.CurrentClientWeather.WavesHeightClient - 0.8f);
							Global.Render.CommonShader.RenderObjectFast(LocalContent.Loaded.Shallow);
						}
					}
					return;
				}
				Vector2 entryPos = Global.Player.NearPort.EntryPos;
				LocalContent.Loaded.Shallow.Transform.MiddleScale = 0.25500003f;
				Transform3D transform3 = LocalContent.Loaded.Shallow.Transform;
				transform3.Scales.X = transform3.Scales.X * 0.2f;
				Transform3D transform4 = LocalContent.Loaded.Shallow.Transform;
				transform4.Scales.Z = transform4.Scales.Z * 0.2f;
				Transform3D transform5 = LocalContent.Loaded.Shallow.Transform;
				transform5.Scales.Y = transform5.Scales.Y * 0.3f;
				LocalContent.Loaded.Shallow.Transform.Translation.X = entryPos.X;
				LocalContent.Loaded.Shallow.Transform.Translation.Z = entryPos.Y;
				LocalContent.Loaded.Shallow.Transform.Translation.Y = -2.3f;
				Global.Render.CommonShader.RenderObjectFast(LocalContent.Loaded.Shallow);
			}
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000F48DC File Offset: 0x000F2ADC
		public void Render3DNormal()
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				this.{24851}();
				return;
			}
			if (Global.Player != null)
			{
				for (int i = 0; i < this.{24977}.Size; i++)
				{
					WorldInstance.SimpleDecoration simpleDecoration = this.{24977}.Array[i];
					float num = Vector2.DistanceSquared(simpleDecoration.Scene.Transform.Translation.XZ(), Global.Player.Position);
					if (!simpleDecoration.RemoveEvent && simpleDecoration.RemoveWhen != null && simpleDecoration.RemoveWhen())
					{
						simpleDecoration.RemoveEvent = true;
					}
					if (simpleDecoration.RemoveEvent)
					{
						Transform3D transform = simpleDecoration.Scene.Transform;
						transform.Translation.Y = transform.Translation.Y - Global.Game.GameTime.ElapsedDrawReal / 1000f;
						if (simpleDecoration.Scene.Transform.Translation.Y < -10f)
						{
							this.{24977}.FastRemoveAt(i);
							i--;
						}
					}
					else if (simpleDecoration.Floating)
					{
						Vector3 vector;
						float y;
						CommonGlobal.CurrentClientWeather.NormalAndHeightHelper(Global.Player.MapInfo, simpleDecoration.Scene.Transform.Translation.X, simpleDecoration.Scene.Transform.Translation.Z, out vector, out y);
						simpleDecoration.Scene.Transform.Translation.Y = y;
						simpleDecoration.Scene.Transform.Roll = -vector.X;
						simpleDecoration.Scene.Transform.Pitch = -vector.Z;
					}
					if (num < 202500f)
					{
						if (simpleDecoration.UseIsleAndTerrain != null)
						{
							Global.Render.CommonShader.RenderIsle(simpleDecoration.Scene, false, simpleDecoration.UseIsleAndTerrain.Value);
						}
						else
						{
							Global.Render.CommonShader.RenderObject(simpleDecoration.Scene, false, 1f, false, 0f, false);
						}
					}
					else if (simpleDecoration.AutoRemove)
					{
						this.{24977}.FastRemoveAt(i);
						i--;
					}
				}
			}
			if (!Renderer.ReflectionsAreBeingDrawn)
			{
				for (int j = 0; j < this.{24945}.Size; j++)
				{
					this.{24945}.Array[j].Render();
				}
				for (int k = 0; k < this.{24962}.Size; k++)
				{
					this.{24962}.Array[k].Render3D();
				}
				for (int l = 0; l < this.{24964}.Size; l++)
				{
					this.{24964}.Array[l].Draw3D();
				}
				this.{24958}.CleanCache();
				for (int m = 0; m < this.{24956}.Size; m++)
				{
					this.{24956}.Array[m].Render(this.{24958});
				}
				this.{24958}.BuildCache();
				Global.Render.CommonShader.RenderObjectInstanced(this.{24959}, this.{24958}, 1f);
				this.{24851}();
				Debugging.Draw3D();
				return;
			}
			this.{24851}();
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000F4BF4 File Offset: 0x000F2DF4
		private void {24851}()
		{
			Global.Render.CommonShader.ConfigureAnimatedMeshInNextRenderObject(Vector2.Zero, AnimationType.Flora);
			Global.RenderStats.IsleRenderCount = 0;
			for (int i = 0; i < this.MapVisibleObjectLayer.Size; i++)
			{
				this.MapVisibleObjectLayer.Array[i].Render3D();
			}
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000F4C4C File Offset: 0x000F2E4C
		public void Render3DTransparantMesh()
		{
			if (!{18560}.closed)
			{
				{18560}.Render3D();
			}
			for (int i = 0; i < this.{24964}.Size; i++)
			{
				this.{24964}.Array[i].DrawStatic();
			}
			for (int j = 0; j < this.MapVisibleObjectLayer.Size; j++)
			{
				this.MapVisibleObjectLayer.Array[j].Render3DStatic();
			}
			this.{24983}.RenderTransparancy();
			if (!Renderer.ReflectionsAreBeingDrawn)
			{
				for (int k = 0; k < this.{24947}.Size; k++)
				{
					this.{24947}.Array[k].RenderTransparentGeometry();
				}
			}
			for (int l = 0; l < this.{24945}.Size; l++)
			{
				this.{24945}.Array[l].Render();
			}
			for (int m = 0; m < this.{24943}.Size; m++)
			{
				this.{24943}.Array[m].Render3DTransparent();
			}
			this.{24978}.Draw(true);
			this.{24980}.Draw(true);
			this.{24979}.Draw(true);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000F4D68 File Offset: 0x000F2F68
		public void Render3DTransparantMeshCurrentPlayer()
		{
			if (Global.Player != null && Global.Game.GetCurrentSceneName != GameSceneName.Entry && !Renderer.ReflectionsAreBeingDrawn)
			{
				Global.Render.CommonShader.SetMinShadowCascadeIs1(false);
				Engine.GS.SetDepthBuffer(DepthBuffer.ReadOnly);
				Engine.GS.SetDepthBuffer(DepthBuffer.ReadAndWrite);
				Engine.GS.SetColorBlendState(BlendState.AlphaBlend);
				Global.Render.CommonShader.SetMinShadowCascadeIs1(true);
				for (int i = 0; i < this.{24949}.Size; i++)
				{
					ShipNpc shipNpc = this.{24949}.Array[i];
					if (shipNpc.Client.IsVisible)
					{
						shipNpc.Client.RenderTransparantSail();
					}
				}
				for (int j = 0; j < this.{24951}.Size; j++)
				{
					ShipOtherPlayer shipOtherPlayer = this.{24951}.Array[j];
					if ((!shipOtherPlayer.IsDecoration || shipOtherPlayer.IsDecorationVisible) && shipOtherPlayer.Client.IsVisible)
					{
						shipOtherPlayer.Client.RenderTransparantSail();
					}
				}
				Global.Player.Client.RenderTransparantSail();
			}
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000F4E80 File Offset: 0x000F3080
		public void RenderToGBufferCurrentPlayer(IGBufferBuilder {24852})
		{
			if (Global.Game.GetCurrentSceneName != GameSceneName.Entry && (Global.Camera.Zoom > 1f || Global.Camera.IsFreeMode) && (!Global.Player.IsDestroyed || Global.Player.UsedShipPlayer.CraftFrom.ID == 1))
			{
				Global.Player.Client.CheckVisibilityAndRenderToGBuffer({24852});
			}
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000F4EEC File Offset: 0x000F30EC
		public void Render3DToGBuffer(IGBufferBuilder {24853}, bool {24854})
		{
			if ({24854})
			{
				for (int i = 0; i < this.{24949}.Size; i++)
				{
					this.{24949}.Array[i].Client.CheckVisibilityAndRenderToGBuffer({24853});
				}
				for (int j = 0; j < this.{24951}.Size; j++)
				{
					ShipOtherPlayer shipOtherPlayer = this.{24951}.Array[j];
					if (!shipOtherPlayer.IsDecoration || shipOtherPlayer.IsDecorationVisible)
					{
						shipOtherPlayer.Client.CheckVisibilityAndRenderToGBuffer({24853});
					}
				}
				for (int k = 0; k < this.{24977}.Size; k++)
				{
					if (this.{24977}.Array[k].Shadows)
					{
						this.{24977}.Array[k].Scene.CheckVisibilityAndRenderGBuffer({24853}, null);
					}
				}
				return;
			}
			for (int l = 0; l < this.MapVisibleObjectLayer.Size; l++)
			{
				this.MapVisibleObjectLayer.Array[l].CheckVisibilityAndRenderToGBuffer({24853});
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x000F4FE0 File Offset: 0x000F31E0
		public IEnumerable<IsleFlares> EnumerateLampLights()
		{
			WorldInstance.<EnumerateLampLights>d__90 <EnumerateLampLights>d__ = new WorldInstance.<EnumerateLampLights>d__90(-2);
			<EnumerateLampLights>d__.<>4__this = this;
			return <EnumerateLampLights>d__;
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000F4FF0 File Offset: 0x000F31F0
		public void SetMap(WorldMapInfo {24855})
		{
			if (this.{24968} != null && this.{24968} == {24855})
			{
				return;
			}
			this.ChangeWithPortScene(this.{24968} == null || {24855}.IsWorldmap != this.{24968}.IsWorldmap);
			this.{24968} = {24855};
			this.{24931}();
			this.{24983}.Clean();
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000F5050 File Offset: 0x000F3250
		public void InitializeEntryScene()
		{
			this.{24968} = null;
			this.{24932}(Gameplay.WorldMap);
			Vector2 vector;
			for (;;)
			{
				vector = Gameplay.WorldMap.RandomPositionFarOffAnything(70f, new float?((float)300));
				using (IEnumerator<IslePortInfo> enumerator = ((IEnumerable<IslePortInfo>)Gameplay.WorldMap.Ports).GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						IslePortInfo islePortInfo = enumerator.Current;
						if (Vector2.DistanceSquared(vector, islePortInfo.EntryPos) < 1440000f)
						{
							goto IL_71;
						}
					}
					continue;
				}
				break;
			}
			IL_71:
			Global.Camera.BuildDecorateSceneView(vector.X0Y());
			int[] {14494} = new int[]
			{
				10,
				11,
				19,
				28,
				36,
				25,
				39
			};
			this.CreateDecorationShip(Rand.Pick<int>({14494}), new ShipPositionInfo(new Vector2(-2f + GameCamera.entryScenePivotCenter.X, -18f + GameCamera.entryScenePivotCenter.Z) + vector, 0f));
			Vector2 value = new Vector2(1f, 0f);
			for (int i = 0; i < 10; i++)
			{
				Vector2 vector2 = vector + value * 100f + Rand.NextRadialVector2(10f, 200f);
				if (Vector2.Distance(vector2, Engine.GS.Camera.Position.XZ()) > 35f)
				{
					this.CreateDecorationShip(Rand.Pick<int>({14494}), new ShipPositionInfo(new Vector2(-2f + GameCamera.entryScenePivotCenter.X, -18f + GameCamera.entryScenePivotCenter.Z) + vector2, 0f));
				}
			}
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000F51F8 File Offset: 0x000F33F8
		public void ChangeWithPortScene(bool {24856})
		{
			if ({24856})
			{
				this.{24954}.Foreach(delegate(ClientCannonBall {25000})
				{
					{25000}.RemoveEffects();
				}, 0, this.{24954}.Size);
				this.{24955}.Return(this.{24954});
				this.{24961}.Return(this.{24960});
				this.{24963}.Return(this.{24962});
				this.{24948}.Return(this.{24947});
			}
			this.{24983}.Clean();
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000F528D File Offset: 0x000F348D
		public void SetPlayerUID(int {24857})
		{
			if (Global.Player == null)
			{
				this.{24970} = {24857};
				return;
			}
			Global.Player.uID = {24857};
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000F52AC File Offset: 0x000F34AC
		public void CreateDebris(Vector3 {24858}, Vector3 {24859}, HitMaterialEffect {24860})
		{
			if (this.{24956}.Size > 700)
			{
				return;
			}
			if (Global.Render.reduce3dEffects && Rand.Chanse(50f))
			{
				return;
			}
			WoodDebris woodDebris = this.{24957}.Pop();
			woodDebris.Initialize(ref {24859}, ref {24858}, {24860});
			this.{24956}.Add(woodDebris);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000F530C File Offset: 0x000F350C
		public void CreateFloodingDecoration(int {24861}, Vector2 {24862}, ClientDrop {24863}, int {24864})
		{
			for (int i = 0; i < this.{24947}.Size; i++)
			{
				if (this.{24947}.Array[i].ShipUID == {24864})
				{
					return;
				}
			}
			ShipFloodingDecoration shipFloodingDecoration = this.{24948}.Pop();
			shipFloodingDecoration.Create({24861}, {24862}, {24864});
			this.{24947}.Add(shipFloodingDecoration);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000F536C File Offset: 0x000F356C
		public void CreateFloodingDecoration(Ship {24865})
		{
			for (int i = 0; i < this.{24947}.Size; i++)
			{
				if (this.{24947}.Array[i].ShipUID == {24865}.uID)
				{
					return;
				}
			}
			this.{24983}.OnShipDestructed({24865});
			ShipFloodingDecoration shipFloodingDecoration = this.{24948}.Pop();
			shipFloodingDecoration.Create({24865});
			this.{24947}.Add(shipFloodingDecoration);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000F53D6 File Offset: 0x000F35D6
		public void AddFlashEffect(GameEffect {24866})
		{
			if ({24866} == null)
			{
				throw new ArgumentNullException();
			}
			this.{24944}.Add({24866});
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000F53EE File Offset: 0x000F35EE
		public IEnumerable<Ship> EnumerateShips(bool {24867}, bool {24868}, bool {24869})
		{
			WorldInstance.<EnumerateShips>d__99 <EnumerateShips>d__ = new WorldInstance.<EnumerateShips>d__99(-2);
			<EnumerateShips>d__.<>4__this = this;
			<EnumerateShips>d__.<>3__npcs = {24867};
			<EnumerateShips>d__.<>3__players = {24868};
			<EnumerateShips>d__.<>3__decorations = {24869};
			return <EnumerateShips>d__;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000F5413 File Offset: 0x000F3613
		public IEnumerable<Player> EnumerateAllPlayers()
		{
			WorldInstance.<EnumerateAllPlayers>d__100 <EnumerateAllPlayers>d__ = new WorldInstance.<EnumerateAllPlayers>d__100(-2);
			<EnumerateAllPlayers>d__.<>4__this = this;
			return <EnumerateAllPlayers>d__;
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000F5424 File Offset: 0x000F3624
		public bool GetClosestBoardingTarget(out Ship {24870})
		{
			if (Global.Player.IsMarchingMode)
			{
				{24870} = null;
				return false;
			}
			float num = float.MaxValue;
			Ship ship = null;
			foreach (Ship ship2 in this.EnumerateShips(true, true, false))
			{
				float num2 = Vector2.DistanceSquared(ship2.Position, Global.Player.Position);
				if (num2 < num && ((IClientShip)ship2).GetClient.AvailableForBoardingByPlayerWithDistance && Vector2.Dot((ship2.Position - Engine.GS.Camera.Position.XZ()).Normal(), Engine.GS.Camera.Direction.XZNormal()) > 0.2f)
				{
					num = num2;
					ship = ship2;
				}
			}
			{24870} = ship;
			return ship != null;
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000F5508 File Offset: 0x000F3708
		[NullableContext(2)]
		public bool GetClosestWhaleForHarpoon(out ClientDrop {24871})
		{
			{24871} = null;
			if (Global.Player.IsMarchingMode || Global.Player.IsDestroyedOrFlooding)
			{
				return false;
			}
			float num = float.MaxValue;
			ClientDrop clientDrop = null;
			foreach (ClientDrop clientDrop2 in ((IEnumerable<ClientDrop>)this.{24945}))
			{
				if (clientDrop2.Whale != null)
				{
					float num2 = Vector2.DistanceSquared(clientDrop2.Position, Global.Player.Position);
					if (clientDrop2.Whale.Health > 0f && clientDrop2.Whale.State != WhaleController.Status.Stunned && num2 < num)
					{
						Ship player = Global.Player;
						Vector2 position = clientDrop2.Position;
						if (player.DistanceToHitbox(position) < 20f + clientDrop2.Whale.Scale && -Vector2.Dot((Engine.GS.Camera.Position.XZ() - Global.Player.Position).Normal(), (clientDrop2.Position - Global.Player.Position).Normal()) > 0f)
						{
							num = num2;
							clientDrop = clientDrop2;
						}
					}
				}
			}
			{24871} = clientDrop;
			return clientDrop != null;
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000F5648 File Offset: 0x000F3848
		public Player ShadowMappingTarget()
		{
			if (Global.Player != null)
			{
				return Global.Player;
			}
			if (this.{24951} == null || this.{24951}.Size == 0)
			{
				return null;
			}
			return this.{24951}.Array[0];
		}

		// Token: 0x06001B2F RID: 6959 RVA: 0x000F567C File Offset: 0x000F387C
		internal void SpawnNpc(NpcCreatePacket {24872})
		{
			bool {16915} = this.{24983}.OnShipAddSVN({24872}.uID);
			ShipNpc shipNpc = this.{24950}.Pop();
			shipNpc.Initialize({24872}, {16915});
			this.{24949}.Add(shipNpc);
		}

		// Token: 0x06001B30 RID: 6960 RVA: 0x000F56BC File Offset: 0x000F38BC
		internal void SpawnPlayer(PlayerCreatePacket {24873})
		{
			bool {16929} = this.{24983}.OnShipAddSVN({24873}.uID);
			ShipOtherPlayer shipOtherPlayer = this.{24952}.Pop();
			shipOtherPlayer.Initialize(RemotePlayerDynamicInfo.Create({24873}), {24873}, Global.Player.MapInfo, {16929});
			this.{24951}.Add(shipOtherPlayer);
		}

		// Token: 0x06001B31 RID: 6961 RVA: 0x000F570C File Offset: 0x000F390C
		public Ship GetShipFromUID(int {24874})
		{
			if ({24874} < 1)
			{
				return null;
			}
			if (Global.Player != null && Global.Player.uID == {24874})
			{
				return Global.Player;
			}
			for (int i = 0; i < this.{24951}.Size; i++)
			{
				if (this.{24951}.Array[i].uID == {24874})
				{
					return this.{24951}.Array[i];
				}
			}
			for (int j = 0; j < this.{24949}.Size; j++)
			{
				if (this.{24949}.Array[j].uID == {24874})
				{
					return this.{24949}.Array[j];
				}
			}
			return null;
		}

		// Token: 0x06001B32 RID: 6962 RVA: 0x000F57AC File Offset: 0x000F39AC
		public ShipOtherPlayer GetOtherPlayerFromUID(int {24875})
		{
			for (int i = 0; i < this.{24951}.Size; i++)
			{
				if (this.{24951}.Array[i].uID == {24875})
				{
					return this.{24951}.Array[i];
				}
			}
			return null;
		}

		// Token: 0x06001B33 RID: 6963 RVA: 0x000F57F4 File Offset: 0x000F39F4
		public ShipOtherPlayer GetFromAccountSID(uint {24876})
		{
			for (int i = 0; i < this.{24951}.Size; i++)
			{
				if (this.{24951}.Array[i].AccountSID == {24876})
				{
					return this.{24951}.Array[i];
				}
			}
			return null;
		}

		// Token: 0x06001B34 RID: 6964 RVA: 0x000F583C File Offset: 0x000F3A3C
		public bool QueryAction<T>(int {24877}, Action<T> {24878}) where T : Ship
		{
			if (typeof(T) == typeof(Ship))
			{
				throw new NotSupportedException("use getFromUID");
			}
			T t = default(T);
			if (typeof(T) == typeof(ShipPlayerBase) || typeof(T) == typeof(ShipOtherPlayer))
			{
				for (int i = 0; i < this.{24951}.Size; i++)
				{
					if (this.{24951}.Array[i].uID == {24877})
					{
						t = (T)((object)this.{24951}.Array[i]);
					}
				}
			}
			else
			{
				for (int j = 0; j < this.{24949}.Size; j++)
				{
					if (this.{24949}.Array[j].uID == {24877})
					{
						t = (T)((object)this.{24949}.Array[j]);
					}
				}
			}
			if (t != null)
			{
				{24878}(t);
				return true;
			}
			return false;
		}

		// Token: 0x06001B35 RID: 6965 RVA: 0x000F5940 File Offset: 0x000F3B40
		public void ApplShipMovePacket(OnShipControllerChangeMsg {24879})
		{
			Ship shipFromUID = this.GetShipFromUID({24879}.StartPosition.UID);
			if (shipFromUID == null)
			{
				return;
			}
			if ({24879}.ControllerCode == 244 || {24879}.ControllerCode == 245)
			{
				Global.Network.CorrectPosition(shipFromUID, {24879}.StartPosition.Position, Session.LastPing);
				return;
			}
			ShipNpc shipNpc = shipFromUID as ShipNpc;
			if (shipNpc != null)
			{
				shipNpc.ApplyMovePacket({24879});
				return;
			}
			((ShipOtherPlayer)shipFromUID).ApplySetControlPacket({24879});
		}

		// Token: 0x06001B36 RID: 6966 RVA: 0x000F59B8 File Offset: 0x000F3BB8
		public void ApplyShipCollision(ref OnShipCollisionMsg {24880}, bool {24881})
		{
			Ship shipFromUID = this.GetShipFromUID({24880}.CollisionData.uID);
			if (shipFromUID != null)
			{
				shipFromUID.MakeCollisionDamage(ref {24880}.CollisionData, true);
				if (!Global.Player.IsPortEntry)
				{
					FXEngine.CreateCollisionEffect(shipFromUID, ref {24880}.CollisionData, !{24881});
				}
				if ({24881} && shipFromUID.uID == Global.Player.uID)
				{
					Global.Player.ResetSpeedTo2();
				}
				if (!{24881} && shipFromUID.uID == Global.Player.uID && Global.Player.NowSpeed >= 10f && Vector2.Dot(({24880}.CollisionData.CollisionPoint.XZ - Global.Player.Position).Normal, Global.Player.Normal) > 0.66f && !{24880}.CollisionData.SoftCollision)
				{
					Global.Player.ResetSpeedTo0();
				}
			}
		}

		// Token: 0x06001B37 RID: 6967 RVA: 0x000F5AA0 File Offset: 0x000F3CA0
		public void AddCannonBalls(ref OnCreateBallsMsg {24882})
		{
			Ship shipFromUID = this.GetShipFromUID({24882}.SenderUID);
			if (shipFromUID != null)
			{
				if (shipFromUID.uID == Global.Player.uID && {24882}.Balls.Length == 0)
				{
					Global.Player.SynchronizeCannonGunBucketsAfterComplete();
					return;
				}
				if (shipFromUID.uID == Global.Player.uID)
				{
					Global.Player.SynchronizeCannonGunStart({24882}.Balls, Gameplay.BallsInfo[(int){24882}.BallType]);
				}
				Player player = shipFromUID as Player;
				if (player != null)
				{
					player.OnCannonsShot({24882}.Balls.Length);
				}
				int num = {24882}.Balls.Length;
				Vector3 position3D = shipFromUID.Position3D;
				CommonShotRenderer<CommonShotInfo> clientWeaponsShooting = shipFromUID.ClientWeaponsShooting;
				bool shotIsProcessing = clientWeaponsShooting.ShotIsProcessing;
				clientWeaponsShooting.Buckets.Add({24882}.Balls);
				if (!shotIsProcessing)
				{
					clientWeaponsShooting.BeginGunEventCommon((int){24882}.BallType, {24882}.mode, {24882}.serverRandomValue, shipFromUID, {24882}.board, new Action<Ship, CommonShotInfo, CannonGunSoundEvent, int>(this.LocalCommonCannonGunSoundHandler));
				}
				else if ({24882}.useDoublesideMode)
				{
					clientWeaponsShooting.StartDoublesideGun();
				}
				else
				{
					clientWeaponsShooting.KeepShootingSingleMode(shipFromUID);
				}
				if ({24882}.destroyWeaponSlot != 255 && shipFromUID.UsedShip.Cannons.MakeBroken((int){24882}.destroyWeaponSlot, true) && shipFromUID.uID == Global.Player.uID)
				{
					{19994}.MeAndLogbook({19988}.Info, Local.WeaponDestroy, null);
					Global.Player.SynchronizeCannonGunBucketsBefore(null, (int){24882}.BallType, {24882}.mode == CannonsAttackMode.SingleCannon);
					return;
				}
			}
			else
			{
				foreach (CommonShotInfo {24885} in {24882}.Balls)
				{
					this.LocalCommonCannonGunCallback(null, {24882}.SenderUID, {24885}, (int){24882}.BallType, {24882}.mode);
				}
			}
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x000F5C58 File Offset: 0x000F3E58
		public void LocalCommonCannonGunCallback(Ship {24883}, int {24884}, CommonShotInfo {24885}, int {24886}, CannonsAttackMode {24887})
		{
			if ({24883} == null)
			{
				if (Global.Game.InteractiveWorldSystem.GetIsleByUID({24884}).Item1 == null)
				{
					return;
				}
				ClientCannonBall clientCannonBall = this.{24955}.Pop() ?? new ClientCannonBall();
				clientCannonBall.IsFromBuilding = true;
				clientCannonBall.InitializeUnitFromPacket(ref {24885}, {24885}.ServerPosition, null, {24886}, -1, {24887});
				this.{24954}.Add(clientCannonBall);
				if ({24886} == 12)
				{
					if (Rand.Chanse(60f))
					{
						Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.Firegun, clientCannonBall.StartPosition, 0.6f, false);
						return;
					}
				}
				else if (this.{24988}.Elapsed.TotalMilliseconds > 50.0)
				{
					this.{24892}(null, clientCannonBall.StartPosition, CannonClass.LiteCannon, 0.5f, {24886}, CannonBallExtraEffects.None, 0.9f);
					this.{24892}(null, clientCannonBall.StartPosition, CannonClass.DistanceCannon, 0.5f, {24886}, CannonBallExtraEffects.None, 0.9f);
					this.{24988}.Restart();
					return;
				}
			}
			else
			{
				CannonCommon cannonCommon = {24883}.UsedShip.Cannons.FindByLocation((int){24885}.SenderCannonSectionUID);
				if (cannonCommon == null)
				{
					return;
				}
				ClientCannonBall clientCannonBall2 = this.{24955}.Pop() ?? new ClientCannonBall();
				float num = Geometry.Saturate((float)({24883}.ClientWeaponsShooting.Buckets.Size - 30) / 30f) * 0.5f;
				clientCannonBall2.HasNotEffects = Rand.Chanse(num * 100f);
				clientCannonBall2.InitializeUnitFromPacket(ref {24885}, cannonCommon.GetPosition({24883}.Transform), {24883}, {24886}, {24883}.uID, {24887});
				this.{24954}.Add(clientCannonBall2);
				if (Global.Settings.RendererDynamicReflections)
				{
					GunLightInReflection gunLightInReflection = this.{24967}.Pop();
					if (gunLightInReflection != null)
					{
						gunLightInReflection.Initialize(clientCannonBall2.StartPosition);
						this.{24966}.Add(gunLightInReflection);
					}
				}
				if ({24883}.uID == Global.Player.uID)
				{
					Global.Player.SynchronizeCannonGunBucketsBefore(cannonCommon, {24886}, Global.Player.ClientWeaponsShooting.Mode == CannonsAttackMode.SingleCannon);
				}
				cannonCommon.StartRollbackEffect();
			}
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x000F5E64 File Offset: 0x000F4064
		public void LocalCommonCannonGunSoundHandler(Ship {24888}, CommonShotInfo {24889}, CannonGunSoundEvent {24890}, int {24891})
		{
			CannonClass {24895} = CannonClass.LiteCannon;
			Vector3 vector = {24888}.Position3D;
			if ({24890} != CannonGunSoundEvent.SingleShotAllCannons)
			{
				CannonCommon cannonCommon = {24888}.UsedShip.Cannons.FindByLocation((int){24889}.SenderCannonSectionUID);
				if (cannonCommon == null)
				{
					return;
				}
				vector = cannonCommon.GetPosition({24888}.Transform);
				{24895} = cannonCommon.GameInfo.Class;
			}
			if ({24891} == 12)
			{
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.Firegun, vector, 1f, false);
				return;
			}
			float {24899} = ({24888}.ClientWeaponsShooting.Mode == CannonsAttackMode.AllRandomized) ? 0.6f : (({24890} == CannonGunSoundEvent.SingleShotAllCannons) ? 0.6f : 1f);
			float {24896} = ({24890} == CannonGunSoundEvent.SingleShotAllCannons) ? 0.9f : (({24890} == CannonGunSoundEvent.TogetherSectionSingleGun) ? 0.8f : 0.6f);
			this.{24892}({24888}, vector, {24895}, {24896}, {24891}, {24889}.Effects, {24899});
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x000F5F28 File Offset: 0x000F4128
		[NullableContext(2)]
		private void {24892}(Ship {24893}, Vector3 {24894}, CannonClass {24895}, float {24896}, int {24897}, CannonBallExtraEffects {24898}, float {24899})
		{
			IClientShip clientShip = {24893} as IClientShip;
			if (clientShip != null)
			{
				ref Stopwatch ptr = ref clientShip.GetClient.GunSoundLimiter;
				if (ptr == null)
				{
					ptr = Stopwatch.StartNew();
				}
				else
				{
					if ((ptr.Elapsed.TotalMilliseconds < (double)(SoundManager.WeaponsThrottlingMaxMs * {24899}) && Rand.Chanse(SoundManager.WeaponsThrottlingMaxChanse)) || ptr.Elapsed.TotalMilliseconds < (double)SoundManager.WeaponsThrottlingMinMs)
					{
						return;
					}
					ptr.Restart();
				}
			}
			float num = MathHelper.Clamp((Vector3.Distance({24894}, Engine.GS.Camera.Position) - 20f) / 150f, 0f, 0.8f);
			num *= num;
			float {24707} = Geometry.Saturate({24896} * (1f - num) * (1f + 2f * num));
			Global.Game.SoundSystem.Play3DSound(({24895} == CannonClass.DistanceCannon) ? GameDynamicSoundName.CannonsGun_DistSingle : (({24895} == CannonClass.HeavyCannon) ? GameDynamicSoundName.CannonsGun_HeavySingle : (({24895} == CannonClass.Bombardier) ? GameDynamicSoundName.CannonsGun_BombardSingle : GameDynamicSoundName.CannonsGun_Lite)), {24894}, {24707}, {24893} == Global.Player);
			if (num > 0f)
			{
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.CannonsGunFar_Assist, {24894}, Geometry.Saturate({24896} * num * 3f), false);
			}
			if ({24897} == 2 || {24897} == 5 || {24898}.HasFlag(CannonBallExtraEffects.PhosphorBoost))
			{
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.CannonBallFire, {24894}, {24896}, {24893} == Global.Player);
			}
			if ({24897} == 3)
			{
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.CannonBallChain, {24894}, {24896}, {24893} == Global.Player);
			}
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x000F60A8 File Offset: 0x000F42A8
		public void ApplyCannonBallCollision(in DamageData {24900})
		{
			Ship ship = this.GetShipFromUID({24900}.TargetUID);
			if (ship != null && ship.IsDestroyed)
			{
				ship = null;
			}
			ClientCannonBall clientCannonBall = null;
			for (int i = 0; i < this.{24954}.Size; i++)
			{
				if (this.{24954}.Array[i].uID == {24900}.SourcePawnOrShipUID)
				{
					clientCannonBall = this.{24954}.Array[i];
					break;
				}
			}
			if (clientCannonBall != null)
			{
				if (ship == null)
				{
					clientCannonBall.HitPoint = clientCannonBall.Sphere;
				}
				else
				{
					clientCannonBall.HitPoint = ship.UsedShip.StaticInfo.ProjectHitPosition(ship, ship.Position3D + {24900}.ShipSpaceCollisionPoint, new Vector3?(clientCannonBall.StartMomentNormal));
				}
				clientCannonBall.TargetUIDBomberShell = {24900}.TargetUID;
				if ({24900}.Flags.HasFlag(SpecificDamageFlags.SingleSailDamage))
				{
					clientCannonBall.PassedSailHitboxes.Add({24900}.CollisionNodeID);
				}
				if ({24900}.Flags.HasFlag(SpecificDamageFlags.RemoveCannonBall))
				{
					clientCannonBall.ManualEndOfLifeEvent = true;
					clientCannonBall.Sphere = clientCannonBall.HitPoint;
				}
				try
				{
					clientCannonBall.HitboxType = ((ship == null) ? HitboxType.Corpus : ship.UsedShip.StaticInfo.HitboxTableByNodeID.Array[(int){24900}.CollisionNodeID].Type);
				}
				catch (Exception)
				{
					Helpers.SendError(new Exception(string.Concat(new string[]
					{
						"foundCBall.HitboxType, data.CollisionNodeID:",
						{24900}.CollisionNodeID.ToString(),
						", ship: ",
						ship.UsedShip.StaticInfo.ID.ToString(),
						", IsPlayer: ",
						(ship is Player).ToString(),
						", IsDestroyed: ",
						ship.IsDestroyed.ToString()
					})), "ApplyCannonBallCollision", false, false);
				}
				ClientCannonBall clientCannonBall2 = clientCannonBall;
				DamageData damageData = {24900};
				clientCannonBall2.HitMaterial = ((damageData.TotalCrewKills > 0) ? HitMaterialEffect.Crew : ((clientCannonBall.HitboxType == HitboxType.Sail) ? HitMaterialEffect.Sailes : HitMaterialEffect.Wood));
				clientCannonBall.HitDamage = {24900}.HealthDamage;
				clientCannonBall.DamageFlags = {24900}.Flags;
				if (clientCannonBall.BallInfo.AmmoType == CannonAmmoType.FalkonetBall)
				{
					FXEngine.FalkonetExplosion(clientCannonBall.HitPoint, ship, (int)clientCannonBall.BallInfo.ID, clientCannonBall.HitboxType);
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.WoodImpact, clientCannonBall.HitPoint, 0.6f, false);
				}
				else
				{
					FXEngine.HitCannonBallExtraEffects(CannonBall.HitType.Collision, clientCannonBall);
				}
				if (clientCannonBall.SenderUID == Global.Player.uID)
				{
					EducationHelper.OnMakeCannonBallDamage(clientCannonBall, ship);
					CannonCommon cannonCommon = Global.Player.UsedShip.Cannons.FindByLocation((int)clientCannonBall.SourceCannonLocationId);
					if (cannonCommon != null)
					{
						cannonCommon.ClientHitEffectMs = CannonsController.SightHitEffectDurationMs;
					}
				}
				if (ship != null)
				{
					if ({24900}.HealthDamage > 0f && clientCannonBall.SenderUID == Global.Player.uID && ((IClientShip)ship).GetClient.StatusColor != HealthBarStyle.Lime)
					{
						Session.LastSentCBDamageUID = ship.uID;
						if (Session.TimeFromLastSendedCBDamage == null)
						{
							Session.TimeFromLastSendedCBDamage = Stopwatch.StartNew();
						}
						else
						{
							Session.TimeFromLastSendedCBDamage.Restart();
						}
					}
					if (clientCannonBall.HitboxType == HitboxType.Corpus)
					{
						((IClientShip)ship).GetClient.DecalRenderer.HitEffect(clientCannonBall.HitDamage / ship.UsedShip.MaxHp, ship.Transform.InvTransform3X3(clientCannonBall.Sphere + new Vector3(0f, 0.5f, 0f)));
					}
					ShipNpc shipNpc = ship as ShipNpc;
					if (shipNpc != null && this.GetOtherPlayerFromUID(clientCannonBall.SenderUID) != null)
					{
						shipNpc.ProbablyWasDamagedByOtherPlayers = true;
					}
					((IClientShip)ship).GetClient.LastBallCollisionNormal = new Vector3(clientCannonBall.StartMomentNormal.X, clientCannonBall.StartMomentNormal.Z, 0f);
					if (ship == Global.Player && clientCannonBall.BallInfo.ID == 21)
					{
						{20059} currentInstance = {20059}.CurrentInstance;
						if (currentInstance != null)
						{
							currentInstance.PushSnowSprite();
						}
					}
				}
			}
			if (ship != null)
			{
				try
				{
					ship.MakeDamage({24900}, (clientCannonBall == null) ? -1 : clientCannonBall.SenderUID);
				}
				catch (Exception {25356})
				{
					Helpers.SendError({25356}, "Captured: ApplyCannonBallCollision, shipStaticId: " + ship.UsedShip.StaticInfo.ID.ToString(), true, false);
				}
				if ({24900}.Flags.HasFlag(SpecificDamageFlags.BoardingHookConnected) && clientCannonBall != null)
				{
					Ship shipFromUID = this.GetShipFromUID(clientCannonBall.SenderUID);
					if (shipFromUID != null)
					{
						foreach (BoardingHookRenderer boardingHookRenderer in ((IEnumerable<BoardingHookRenderer>)this.{24943}))
						{
							if (boardingHookRenderer.FalkonetBallUid == clientCannonBall.uID)
							{
								boardingHookRenderer.ConnectTarget(shipFromUID, ship);
							}
						}
					}
				}
				if ({24900}.Flags.HasFlag(SpecificDamageFlags.CorpusCritical) && {24900}.UCritCorpusTimeSec > 0f && ship != Global.Player)
				{
					new ShowPowerupItemFSEffect(ship, AtlasGameGui.Texture.Tex, new Rectangle(2701, 483, 34, 33));
				}
			}
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x000F65BC File Offset: 0x000F47BC
		public ClientDrop GetDropByUid(int {24901})
		{
			for (int i = 0; i < this.{24945}.Size; i++)
			{
				if (this.{24945}.Array[i].uID == {24901})
				{
					return this.{24945}.Array[i];
				}
			}
			return null;
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x000F6604 File Offset: 0x000F4804
		public ClientDrop GetDropByBindedShip(int {24902})
		{
			for (int i = 0; i < this.{24945}.Size; i++)
			{
				if (this.{24945}.Array[i].BindedShipFloodingDataOrNull != null && this.{24945}.Array[i].BindedShipFloodingDataOrNull.ShipUID == {24902})
				{
					return this.{24945}.Array[i];
				}
			}
			return null;
		}

		// Token: 0x06001B3E RID: 6974 RVA: 0x000F6664 File Offset: 0x000F4864
		public void AddDrop(ref DropTransferPacket {24903})
		{
			foreach (WorldInstance.SimpleDecoration simpleDecoration in ((IEnumerable<WorldInstance.SimpleDecoration>)this.{24977}))
			{
				if (Vector2.DistanceSquared(simpleDecoration.Scene.Transform.Translation.XZ(), {24903}.Position) < 25f && simpleDecoration.IsByQuest)
				{
					return;
				}
			}
			ClientDrop clientDrop = this.{24946}.Pop();
			clientDrop.InitializeFromPacket(ref {24903});
			clientDrop.LoadFactor = {24903}.LoadFactor;
			this.{24945}.Add(clientDrop);
			if ({24903}.BindedShipFloodingDataOrNull != null)
			{
				this.CreateFloodingDecoration((int){24903}.BindedShipFloodingDataOrNull.ShipStaticInfoID, clientDrop.Position, clientDrop, {24903}.BindedShipFloodingDataOrNull.ShipUID);
			}
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x000F6734 File Offset: 0x000F4934
		public void AddDropStaticModel(UWModel {24904}, Vector2 {24905}, float {24906}, float {24907}, bool {24908}, bool {24909}, Func<bool> {24910} = null)
		{
			foreach (WorldInstance.SimpleDecoration simpleDecoration in ((IEnumerable<WorldInstance.SimpleDecoration>)this.{24977}))
			{
				if (Vector2.DistanceSquared(simpleDecoration.Scene.Transform.Translation.XZ(), {24905}) < 1f && simpleDecoration.Scene.Tag == null && !simpleDecoration.Floating)
				{
					simpleDecoration.Scene.Transform.Yaw = {24906};
					simpleDecoration.Scene.SetModelData(0, {24904});
					return;
				}
			}
			IsleInstance isleInstance = Gameplay.WorldMap.GetVisibleIsles({24905}).FindNear({24905}, (IsleInstance {25001}) => {25001}.GlobalPosition);
			Tlist<WorldInstance.SimpleDecoration> tlist = this.{24977};
			WorldInstance.SimpleDecoration simpleDecoration2 = new WorldInstance.SimpleDecoration(new ModelTransformedScene(new ModelRenderer({24904}), new Transform3D
			{
				Translation = new Vector3({24905}.X, 0f, {24905}.Y),
				Yaw = {24906},
				Scales = new Vector3({24907})
			}), {24909}, new int?((isleInstance == null) ? -1 : isleInstance.ReplacebleXfxID), false)
			{
				RemoveWhen = {24910},
				IsByQuest = {24908}
			};
			tlist.Add(simpleDecoration2);
		}

		// Token: 0x06001B40 RID: 6976 RVA: 0x000F6880 File Offset: 0x000F4A80
		public void RemoveDropStaticModel(Vector2 {24911})
		{
			WorldInstance.SimpleDecoration simpleDecoration;
			if (this.{24977}.TryFind((WorldInstance.SimpleDecoration {25003}) => {25003}.Scene.Transform.Translation.XZ == {24911}, out simpleDecoration))
			{
				simpleDecoration.RemoveEvent = true;
			}
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x000F68BC File Offset: 0x000F4ABC
		public void CreatePowderKeg(ref OnCreatePowderKegsMsg {24912})
		{
			foreach (PowderKegTransferPacket powderKegTransferPacket in ((IEnumerable<PowderKegTransferPacket>){24912}.Data))
			{
				ClientPowderKeg clientPowderKeg = this.{24963}.Pop();
				clientPowderKeg.Init(powderKegTransferPacket.NowPosition, powderKegTransferPacket.uID, powderKegTransferPacket.SourceUID, Gameplay.PowderKegsInfo.FromID((int)powderKegTransferPacket.InfoID), powderKegTransferPacket.DriftingSpeed, powderKegTransferPacket.TtlInit);
				clientPowderKeg.Ttl = powderKegTransferPacket.TtlInit;
				clientPowderKeg.TtlToEnd = powderKegTransferPacket.Ttl;
				Ship shipFromUID = this.GetShipFromUID(powderKegTransferPacket.SourceUID);
				if (shipFromUID != null)
				{
					clientPowderKeg.InitializeAnimation(shipFromUID);
					Player player = shipFromUID as Player;
					if (player != null && clientPowderKeg.Ttl - clientPowderKeg.TtlToEnd < 1000f)
					{
						player.OnFalkonetOrKegShot();
					}
				}
				clientPowderKeg.TtlToEnd = powderKegTransferPacket.Ttl;
				this.{24962}.Add(clientPowderKeg);
			}
		}

		// Token: 0x06001B42 RID: 6978 RVA: 0x000F69B8 File Offset: 0x000F4BB8
		public void PowderKegExplosion(ref OnPowderKegExplosionMsg {24913})
		{
			Vector2 vector = default(Vector2);
			int num = -1;
			PowderKegInfo powderKegInfo = null;
			for (int i = 0; i < this.{24962}.Size; i++)
			{
				if (this.{24962}.Array[i].uID == {24913}.PowderKegUID)
				{
					ClientPowderKeg clientPowderKeg = this.{24962}.Array[i];
					powderKegInfo = clientPowderKeg.Info;
					vector = clientPowderKeg.Position;
					num = clientPowderKeg.SourceUID;
					this.{24962}.FastRemoveAt(i);
					this.{24963}.Add(clientPowderKeg);
					break;
				}
			}
			if (powderKegInfo != null)
			{
				if (powderKegInfo.AdditionalFireZone > 0f)
				{
					this.MakeBigFireArea(vector, powderKegInfo.AdditionalFireZone);
				}
				FXEngine.CreateMassiveExplosion(vector.X0Y(), false, true, false, true);
				if (powderKegInfo.isFirebrand || powderKegInfo.ID == 8)
				{
					FXEngine.CreateMassiveExplosion(vector.X0Y(), true, true, false, true);
				}
				if (powderKegInfo.IsInvisibleMine)
				{
					FXEngine.NewMassiveSplash(vector, 1.1f);
				}
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.PowderKegExplosion, vector.X0Y(), 0.9f, false);
				if ({24913}.damageToShips.Size > 0 && num == Global.Player.uID)
				{
					EducationHelper.SendDamageFromPowderKeg(powderKegInfo);
				}
				if ({24913}.damageToShips.Any((DamageData {25002}) => {25002}.TargetUID == Global.Player.uID))
				{
					EducationHelper.RecieveDamageFromPoderKeg(powderKegInfo);
				}
			}
			for (int j = 0; j < {24913}.damageToShips.Size; j++)
			{
				this.ApplyGeneralDamage({24913}.damageToShips.Array[j], num);
			}
		}

		// Token: 0x06001B43 RID: 6979 RVA: 0x000F6B48 File Offset: 0x000F4D48
		public void MakeBigFireArea(Vector2 {24914}, float {24915})
		{
			for (int i = 0; i < 7; i++)
			{
				this.ContactFireArea({24914} + Geometry.SubstructRotate(6.2831855f * (float)i / 7f, {24915}));
			}
			for (int j = 0; j < 4; j++)
			{
				this.ContactFireArea({24914} + Geometry.SubstructRotate(6.2831855f * (float)j / 4f, {24915} / 2.2f));
			}
			FXEngine.SampleFumesSmokeLong({24914}.X0Y() + Vector3.Up, 12f);
		}

		// Token: 0x06001B44 RID: 6980 RVA: 0x000F6BD0 File Offset: 0x000F4DD0
		public void ApplyGeneralDamage(in DamageData {24916}, int {24917})
		{
			Ship shipFromUID = this.GetShipFromUID({24916}.TargetUID);
			if (shipFromUID != null)
			{
				shipFromUID.MakeDamage({24916}, {24917});
				if ({24916}.SourcePawnType != DamageID.CannonBall && {24916}.SourcePawnType != DamageID.FalkonetBall)
				{
					((IClientShip)shipFromUID).GetClient.DecalRenderer.HitEffectGeneral({24916}.HealthDamage / shipFromUID.UsedShip.MaxHp);
					if ({24916}.SourcePawnType == DamageID.MortarShot && shipFromUID == Global.Player && {24916}.HealthDamage > 80f && WorldInstance.mortarDamageEffect.ElapsedMilliseconds > 3000L)
					{
						if ({24916}.HealthDamage > 110f)
						{
							Global.Render.PostProcess.GradientAnimationBegin(3000f, true, true);
						}
						Global.Render.PostProcess.GradientAnimationBegin(3000f, true, true);
						WorldInstance.mortarDamageEffect.Restart();
					}
					if ({24916}.SourcePawnType == DamageID.FireArea && shipFromUID == Global.Player && Rand.Chanse(30f))
					{
						Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.CannonBallFireHitShip, Global.Player.Position3D, 1f, false);
					}
				}
			}
		}

		// Token: 0x06001B45 RID: 6981 RVA: 0x000F6CE8 File Offset: 0x000F4EE8
		public void CreateMortarShot(in OnMortarAttackMsg {24918})
		{
			if ({24918}.SenderUID == -1)
			{
				this.CreateMortarShot(-1, -1, {24918}.Shot);
				return;
			}
			Ship shipFromUID = this.GetShipFromUID({24918}.SenderUID);
			if (shipFromUID == null)
			{
				return;
			}
			bool flag = Vector2.Dot({24918}.Shot.Direction.XY(), shipFromUID.FastNormal) > 0f;
			ShipPlayerBase shipPlayerBase = shipFromUID as ShipPlayerBase;
			if (shipPlayerBase != null)
			{
				shipPlayerBase.OnMortarShot(flag);
			}
			int num = 0;
			foreach (ValueTuple<CannonLocationInfo, CannonGameInfo> valueTuple in shipFromUID.UsedShip.StaticInfo.FetchMortars(flag, shipFromUID.UsedShip))
			{
				Vector3 position = valueTuple.Item1.GetPosition(shipFromUID);
				MortarShotParameters shot = {24918}.Shot;
				shot.StartPosition = position;
				this.CreateMortarShot({24918}.shotUid[num++], shipFromUID.uID, shot);
				if (num == {24918}.shotUid.Length)
				{
					break;
				}
			}
		}

		// Token: 0x06001B46 RID: 6982 RVA: 0x000F6DE8 File Offset: 0x000F4FE8
		public void CreateMortarShot(int {24919}, int {24920}, MortarShotParameters {24921})
		{
			ClientMortarShot clientMortarShot = this.{24965}.Pop();
			clientMortarShot.InitializeNewUnit({24919}, {24920}, {24921});
			this.{24964}.Add(clientMortarShot);
		}

		// Token: 0x06001B47 RID: 6983 RVA: 0x000F6E18 File Offset: 0x000F5018
		public ClientCannonBall GetFalkonetBall(int {24922})
		{
			for (int i = 0; i < this.{24954}.Size; i++)
			{
				if (this.{24954}.Array[i].uID == {24922})
				{
					return this.{24954}.Array[i];
				}
			}
			return null;
		}

		// Token: 0x06001B48 RID: 6984 RVA: 0x000F6E60 File Offset: 0x000F5060
		public void CreateFalkonetByCallback(Ship {24923}, FalkonetShotInfoRemote {24924}, bool {24925})
		{
			CannonBallInfo cannonBallInfo = Gameplay.BallsInfo.FromID((int){24924}.AmmoID);
			Vector3 vector = {24923}.Transform.Transform3X3({24924}.StartPosition);
			ClientCannonBall clientCannonBall = this.{24955}.Pop() ?? new ClientCannonBall();
			clientCannonBall.InitializeNewUnitFalk({24924}, {24923}.uID, vector);
			this.{24954}.Add(clientCannonBall);
			if (cannonBallInfo.IsBoardingHook)
			{
				BoardingHookRenderer boardingHookRenderer = new BoardingHookRenderer({24923}, clientCannonBall);
				this.{24943}.Add(boardingHookRenderer);
				if ({24923}.FalkonetShooting.CurrentShootingQueueIndex == 0)
				{
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.BoardingHook, vector, 1f, false);
					return;
				}
			}
			else if ({24925})
			{
				FXEngine.GunEffectSmall2(vector, {24924}.SightDirection, {24923}.physicsBody.VelocityPerSec / 60f, true);
				Global.Game.SoundSystem.Play3DSound((Vector3.Distance(vector, Engine.GS.Camera.Position) > 100f) ? GameDynamicSoundName.MarineGun_Far : GameDynamicSoundName.MarineGun_Near, vector, 1f, {24923} == Global.Player);
			}
		}

		// Token: 0x06001B49 RID: 6985 RVA: 0x000F6F68 File Offset: 0x000F5168
		public void DisconnectBoardingHooks(int {24926})
		{
			for (int i = 0; i < this.{24943}.Size; i++)
			{
				if (this.{24943}.Array[i].SourceShipUid == {24926})
				{
					this.{24943}.FastRemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x000F6FB0 File Offset: 0x000F51B0
		public void ContactFireArea(Vector2 {24927})
		{
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				return;
			}
			for (int i = 0; i < this.{24960}.Size; i++)
			{
				FireAreaVisualizer fireAreaVisualizer = this.{24960}.Array[i];
				if (fireAreaVisualizer.CanBeExtendedBy(ref {24927}))
				{
					fireAreaVisualizer.Extend({24927});
					return;
				}
			}
			FireAreaVisualizer fireAreaVisualizer2 = this.{24961}.Pop();
			fireAreaVisualizer2.Init({24927});
			this.{24960}.Add(fireAreaVisualizer2);
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x000F7024 File Offset: 0x000F5224
		public void RemoveSVNObjectsFromScene(int {24928})
		{
			if ({24928} == Global.Player.uID)
			{
				throw new InvalidOperationException();
			}
			Ship shipFromUID = this.GetShipFromUID({24928});
			if (shipFromUID != null)
			{
				this.{24983}.OnShipRemoveSVN(shipFromUID);
				Type type = shipFromUID.GetType();
				if (type.Equals(typeof(ShipOtherPlayer)))
				{
					Tlist<ShipOtherPlayer> tlist = this.{24951};
					ShipOtherPlayer shipOtherPlayer = (ShipOtherPlayer)shipFromUID;
					tlist.FastRemove(shipOtherPlayer);
					UWEPool<ShipOtherPlayer> uwepool = this.{24952};
					shipOtherPlayer = (ShipOtherPlayer)shipFromUID;
					uwepool.Add(shipOtherPlayer);
					return;
				}
				if (type.Equals(typeof(ShipNpc)))
				{
					Tlist<ShipNpc> tlist2 = this.{24949};
					ShipNpc shipNpc = (ShipNpc)shipFromUID;
					tlist2.FastRemove(shipNpc);
					UWEPool<ShipNpc> uwepool2 = this.{24950};
					shipNpc = (ShipNpc)shipFromUID;
					uwepool2.Add(shipNpc);
					return;
				}
			}
			for (int i = 0; i < this.{24945}.Size; i++)
			{
				if (this.{24945}.Array[i].uID == {24928} && this.{24945}.Array[i].forgetAnimation == 0f)
				{
					this.{24945}.Array[i].BeginRemoveAnimation();
					return;
				}
			}
			for (int j = 0; j < this.{24962}.Size; j++)
			{
				if (this.{24962}.Array[j].uID == {24928})
				{
					this.{24963}.Add(this.{24962}.Array[j]);
					this.{24962}.FastRemoveAt(j);
					return;
				}
			}
			Global.Game.InteractiveWorldSystem.TryRemoveDynamicBuilding({24928});
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x000F71A8 File Offset: 0x000F53A8
		internal void ToggleVisibleDistance()
		{
			for (int i = 0; i < this.{24953}.Size; i++)
			{
				this.{24953}.Array[i].ToggleVisibleDistance();
			}
		}

		// Token: 0x06001B4D RID: 6989 RVA: 0x000F71E0 File Offset: 0x000F53E0
		internal Isle AddTemportalWorldObject(IsleInstance {24929})
		{
			Tlist<Isle> tlist = this.{24953};
			Isle isle;
			Isle result = isle = new Isle({24929}, -1);
			tlist.Add(isle);
			return result;
		}

		// Token: 0x06001B4E RID: 6990 RVA: 0x000F7205 File Offset: 0x000F5405
		internal void RemoveTemporalObject(Isle {24930})
		{
			{24930}.Dispose();
			this.{24953}.Remove({24930});
			this.MapVisibleObjectLayer.FastRemove({24930});
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x000F7229 File Offset: 0x000F5429
		internal void RemoveAllMapLayer()
		{
			this.{24953}.Clear();
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000F7236 File Offset: 0x000F5436
		private void {24931}()
		{
			this.{24952}.Return(this.{24951});
			this.{24932}(this.{24968});
			Global.RenderStats.IsleAllCount = this.{24953}.Size;
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x000F726C File Offset: 0x000F546C
		private void {24932}(WorldMapInfo {24933})
		{
			foreach (Isle isle in ((IEnumerable<Isle>)this.{24953}))
			{
				isle.Dispose();
			}
			this.{24953}.Clear();
			foreach (IsleInstance isleInstance in ((IEnumerable<IsleInstance>){24933}.Isles))
			{
				Tlist<Isle> tlist = this.{24953};
				Isle isle2 = new Isle(isleInstance, isleInstance.ReplacebleXfxID);
				tlist.Add(isle2);
			}
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000F7310 File Offset: 0x000F5510
		public void UpdateQuestsInSea()
		{
			if (Global.Player == null || !Global.Player.InstanceAlive)
			{
				return;
			}
			try
			{
				foreach (Ship ship in ((IEnumerable<Ship>)this.{24975}))
				{
					if (Vector2.Distance(ship.Position, Global.Player.Position) > 250f)
					{
						this.RemoveDecoration(ship);
					}
					else
					{
						this.{24976}.Add(ship);
					}
				}
				this.{24975}.Clear();
				using (IEnumerator<QuestInfo> enumerator2 = ((IEnumerable<QuestInfo>)Global.Game.InteractiveWorldSystem.VisibleWorldQuests).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						QuestInfo quest = enumerator2.Current;
						if (Gameplay.WorldMap.CheckPosition(quest.LocationPos, 10f, true, false) && !this.{24975}.Contains((Ship {25004}) => Vector2.Distance({25004}.Position, quest.LocationPos) < 10f) && !this.{24976}.Contains((Ship {25005}) => Vector2.Distance({25005}.Position, quest.LocationPos) < 10f))
						{
							ShipClass classCretery = (quest.Company == QuestCompany.Trading) ? ShipClass.CargoShip : ShipClass.Battleship;
							IEnumerable<PlayerShipInfo> source = from {25006} in Gameplay.PlayersInfo
							where {25006}.CanBeUsedForNpc && {25006}.Class == classCretery && Math.Abs({25006}.Rank - Global.Player.UsedShipPlayer.CraftFrom.Rank) <= 2
							select {25006};
							Ship ship2 = this.CreateDecorationShip((int)Rand.Pick<PlayerShipInfo>(source.ToArray<PlayerShipInfo>()).ID, new ShipPositionInfo(quest.LocationPos, Rand.Angle()));
							this.{24975}.Add(ship2);
						}
					}
				}
				this.{24986} = Global.Game.InteractiveWorldSystem.VisibleWorldQuests.Size;
			}
			catch (Exception {25356})
			{
				this.{24975}.Clear();
				Helpers.SendError({25356}, "UpdateQuestsInSea", true, false);
			}
			foreach (QuestRunningProgress questRunningProgress in ((IEnumerable<QuestRunningProgress>)Session.Account.Quests.ProgressRunningQuests))
			{
				QuestIndividualLoot questIndividualLoot = questRunningProgress.CurrentStep as QuestIndividualLoot;
				if (questIndividualLoot != null && questRunningProgress.RuntimeParameter != null)
				{
					QuestIndividualLoot cachedStepRef = questIndividualLoot;
					UWModel wholeModel = LocalContent.Loaded.DropModels[questIndividualLoot.Model].First().WholeModel;
					Vector2 positionParameter = questRunningProgress.RuntimeParameter.PositionParameter;
					DropModel model = questIndividualLoot.Model;
					bool flag = model - DropModel.IsleFarming <= 1 || model == DropModel.IsleTreasures;
					Func<QuestRunningProgress, bool> <>9__4;
					this.AddDropStaticModel(wholeModel, positionParameter, 0f, 1f, true, !flag, delegate
					{
						if (Session.Account != null)
						{
							Tlist<QuestRunningProgress> progressRunningQuests = Session.Account.Quests.ProgressRunningQuests;
							Func<QuestRunningProgress, bool> method;
							if ((method = <>9__4) == null)
							{
								method = (<>9__4 = ((QuestRunningProgress {25007}) => {25007}.CurrentStep == cachedStepRef));
							}
							return !progressRunningQuests.Contains(method);
						}
						return false;
					});
				}
			}
			this.{24987} = Session.Account.Quests.ProgressRunningQuests.Size;
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000F763C File Offset: 0x000F583C
		[return: Nullable(2)]
		public ClientPowderKeg FindPowderKegNearPlayer(Func<ClientPowderKeg, bool> {24934})
		{
			for (int i = 0; i < this.{24962}.Size; i++)
			{
				if (Vector2.Distance(Global.Player.Position, this.{24962}[i].Position) < 60f && {24934}(this.{24962}[i]))
				{
					return this.{24962}[i];
				}
			}
			return null;
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000F76A8 File Offset: 0x000F58A8
		public void RemoveDecoration(Ship {24935})
		{
			this.{24976}.FastRemove({24935});
			for (int i = 0; i < this.{24951}.Size; i++)
			{
				if (this.{24951}.Array[i].IsDecoration && {24935} == this.{24951}.Array[i])
				{
					this.{24952}.Add(this.{24951}.Array[i]);
					this.{24951}.FastRemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x000F772C File Offset: 0x000F592C
		public Ship CreateDecorationShip(int {24936}, ShipPositionInfo {24937})
		{
			ShipOtherPlayer shipOtherPlayer = this.{24952}.Pop();
			PlayerShipInfo playerShipInfo = Gameplay.PlayersInfo.FromID({24936});
			CannonCollection cannons = new CannonCollection(playerShipInfo.StaticInfo, Gameplay.CannonsGameInfo.FromID(2));
			PlayerCreatePacket playerCreatePacket = new PlayerCreatePacket();
			playerCreatePacket.AccountSID = 1U;
			playerCreatePacket.Cannons = cannons;
			playerCreatePacket.Mortars = new Map<CannonGameInstance>();
			playerCreatePacket.ControlCode = 0;
			playerCreatePacket.DebugEnabled = false;
			playerCreatePacket.DesingElements = new SlottedMap<ShipDesignInfo>();
			playerCreatePacket.DynamicEffects = new DynamicEffectHelper();
			playerCreatePacket.FinalShipProperties = new ShipProperties(1000f, 1000f, 1f, 10f);
			playerCreatePacket.GuildInfo = new GuildShortInfo(string.Empty, FractionID.None);
			playerCreatePacket.WorldFlags = OpenWorldFlag.Pirate;
			playerCreatePacket.Name = string.Empty;
			playerCreatePacket.NowHP = new ShipFirstHp(playerShipInfo.PatHealth);
			playerCreatePacket.NowSailHP = WorldInstance.HbArray(playerShipInfo.StaticInfo.SailHitboxes.Length);
			playerCreatePacket.NowSpeed = 0f;
			playerCreatePacket.PlayerInfoID = (byte)playerShipInfo.ID;
			playerCreatePacket.PositionInfo = {24937};
			playerCreatePacket.AccRank = 1;
			playerCreatePacket.StaticMobility = 100f;
			playerCreatePacket.uID = 0;
			playerCreatePacket.Crew = new UnitCollection();
			PlayerCreatePacket playerCreatePacket2 = playerCreatePacket;
			float[] array = new float[17];
			array[4] = (float)playerShipInfo.PatPlacesUnits;
			playerCreatePacket2.PrecompuredBonusEffects = array;
			playerCreatePacket.CurrentCapacitySpeedFactor = 1f;
			playerCreatePacket.PublicDesigns = Tlist<PublicDesignInfo>.EmptyReadonly;
			playerCreatePacket.Reputations = new PlayerReputations();
			PlayerCreatePacket playerCreatePacket3 = playerCreatePacket;
			playerCreatePacket3.Crew.Add(Gameplay.UnitsInfo.FromID(1), 50, false);
			shipOtherPlayer.Initialize(RemotePlayerDynamicInfo.Create(playerCreatePacket3), playerCreatePacket3, Gameplay.WorldMap, true);
			shipOtherPlayer.IsDecoration = true;
			shipOtherPlayer.IsAnchored = true;
			this.{24951}.Add(shipOtherPlayer);
			return shipOtherPlayer;
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000F78F8 File Offset: 0x000F5AF8
		private static float[] HbArray(int {24938})
		{
			float[] array = new float[{24938}];
			for (int i = 0; i < {24938}; i++)
			{
				array[i] = 1f;
			}
			return array;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000F7938 File Offset: 0x000F5B38
		[CompilerGenerated]
		private void {24939}()
		{
			Global.Player = new ShipCurrentPlayer();
			Global.Player.Initialize(this.{24970}, Session.Account.SavedWorldPosition);
			Global.Player.SetBattleTimer(Session.Account.SavedRemainLockActionTimer);
			InGameSightUi.CannonSights.ShowGunModeAnimation();
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000F7988 File Offset: 0x000F5B88
		[CompilerGenerated]
		internal static int <Render2D>g__distanceKey|79_0(Ship {24940}, ref WorldInstance.<>c__DisplayClass79_0 {24941})
		{
			float num = Vector2.Distance({24940}.Position, {24941}.camPos) / 3f;
			return 99 - Math.Max(0, Math.Min(99, (int)num));
		}

		// Token: 0x040019B7 RID: 6583
		private const int limitDetailCollisionTestCannonBallsPerFrame = 40;

		// Token: 0x040019B8 RID: 6584
		private const int c_maxWoodDebrisCount = 700;

		// Token: 0x040019B9 RID: 6585
		public Tlist<Isle> MapVisibleObjectLayer;

		// Token: 0x040019BA RID: 6586
		public float LastWindAxis;

		// Token: 0x040019BB RID: 6587
		public Color LastWindColor;

		// Token: 0x040019BC RID: 6588
		private CountingSort<IClientShip> {24942};

		// Token: 0x040019BD RID: 6589
		private Tlist<BoardingHookRenderer> {24943};

		// Token: 0x040019BE RID: 6590
		private Tlist<GameEffect> {24944};

		// Token: 0x040019BF RID: 6591
		private Tlist<ClientDrop> {24945};

		// Token: 0x040019C0 RID: 6592
		private UWEPool<ClientDrop> {24946};

		// Token: 0x040019C1 RID: 6593
		private Tlist<ShipFloodingDecoration> {24947};

		// Token: 0x040019C2 RID: 6594
		private UWEPool<ShipFloodingDecoration> {24948};

		// Token: 0x040019C3 RID: 6595
		private Tlist<ShipNpc> {24949};

		// Token: 0x040019C4 RID: 6596
		private UWEPool<ShipNpc> {24950};

		// Token: 0x040019C5 RID: 6597
		private Tlist<ShipOtherPlayer> {24951};

		// Token: 0x040019C6 RID: 6598
		private UWEPool<ShipOtherPlayer> {24952};

		// Token: 0x040019C7 RID: 6599
		private Tlist<Isle> {24953};

		// Token: 0x040019C8 RID: 6600
		private Tlist<ClientCannonBall> {24954};

		// Token: 0x040019C9 RID: 6601
		private UWEPool<ClientCannonBall> {24955};

		// Token: 0x040019CA RID: 6602
		private Tlist<WoodDebris> {24956};

		// Token: 0x040019CB RID: 6603
		private UWEPool<WoodDebris> {24957};

		// Token: 0x040019CC RID: 6604
		private InstancedModelRenderer {24958};

		// Token: 0x040019CD RID: 6605
		private ModelTransformedScene {24959};

		// Token: 0x040019CE RID: 6606
		private Tlist<FireAreaVisualizer> {24960};

		// Token: 0x040019CF RID: 6607
		private UWEPool<FireAreaVisualizer> {24961};

		// Token: 0x040019D0 RID: 6608
		private Tlist<ClientPowderKeg> {24962};

		// Token: 0x040019D1 RID: 6609
		private UWEPool<ClientPowderKeg> {24963};

		// Token: 0x040019D2 RID: 6610
		private Tlist<ClientMortarShot> {24964};

		// Token: 0x040019D3 RID: 6611
		private UWEPool<ClientMortarShot> {24965};

		// Token: 0x040019D4 RID: 6612
		private Tlist<GunLightInReflection> {24966};

		// Token: 0x040019D5 RID: 6613
		private UWEPool<GunLightInReflection> {24967};

		// Token: 0x040019D6 RID: 6614
		private WorldMapInfo {24968};

		// Token: 0x040019D7 RID: 6615
		private SpriteBatch3D<VertexPositionColorTexture> {24969};

		// Token: 0x040019D8 RID: 6616
		private int {24970};

		// Token: 0x040019D9 RID: 6617
		private Tlist<CannonBallWithWorldObjCollisionData> {24971};

		// Token: 0x040019DA RID: 6618
		private Tlist<CannonBallWithWorldObjCollisionData> {24972};

		// Token: 0x040019DB RID: 6619
		private Timer {24973} = new Timer(90f);

		// Token: 0x040019DC RID: 6620
		private int {24974};

		// Token: 0x040019DD RID: 6621
		private Tlist<Ship> {24975} = new Tlist<Ship>();

		// Token: 0x040019DE RID: 6622
		private Tlist<Ship> {24976} = new Tlist<Ship>();

		// Token: 0x040019DF RID: 6623
		private Tlist<WorldInstance.SimpleDecoration> {24977} = new Tlist<WorldInstance.SimpleDecoration>();

		// Token: 0x040019E0 RID: 6624
		private BuoyCircle {24978} = new BuoyCircle();

		// Token: 0x040019E1 RID: 6625
		private BuoyCircle {24979} = new BuoyCircle();

		// Token: 0x040019E2 RID: 6626
		private BuoyCircle {24980} = new BuoyCircle();

		// Token: 0x040019E3 RID: 6627
		private EnvironmentGraphics {24981};

		// Token: 0x040019E4 RID: 6628
		private WindEnvironmentEffect {24982};

		// Token: 0x040019E5 RID: 6629
		private ShipSilhouettesManager {24983};

		// Token: 0x040019E6 RID: 6630
		private ShallowsDetection {24984};

		// Token: 0x040019E7 RID: 6631
		public BillboardParent_VPCT windDir;

		// Token: 0x040019E8 RID: 6632
		public BillboardParent_VPCT balloonDir;

		// Token: 0x040019E9 RID: 6633
		private Transform3D {24985} = new Transform3D();

		// Token: 0x040019EA RID: 6634
		private int {24986};

		// Token: 0x040019EB RID: 6635
		private int {24987};

		// Token: 0x040019EC RID: 6636
		private Stopwatch {24988} = Stopwatch.StartNew();

		// Token: 0x040019ED RID: 6637
		private static StopwatchGraph tester = new StopwatchGraph();

		// Token: 0x040019EE RID: 6638
		private static Stopwatch mortarDamageEffect = Stopwatch.StartNew();

		// Token: 0x020004CE RID: 1230
		private class SimpleDecoration
		{
			// Token: 0x06001B5A RID: 7002 RVA: 0x000F79BF File Offset: 0x000F5BBF
			public SimpleDecoration(ModelTransformedScene {24993}, bool {24994}, int? {24995}, bool {24996} = false)
			{
				this.Scene = {24993};
				this.Floating = {24994};
				this.UseIsleAndTerrain = {24995};
				this.AutoRemove = {24996};
			}

			// Token: 0x040019EF RID: 6639
			public ModelTransformedScene Scene;

			// Token: 0x040019F0 RID: 6640
			public bool Floating;

			// Token: 0x040019F1 RID: 6641
			public int? UseIsleAndTerrain;

			// Token: 0x040019F2 RID: 6642
			public bool AutoRemove;

			// Token: 0x040019F3 RID: 6643
			public bool Shadows = true;

			// Token: 0x040019F4 RID: 6644
			public bool RemoveEvent;

			// Token: 0x040019F5 RID: 6645
			public Func<bool> RemoveWhen;

			// Token: 0x040019F6 RID: 6646
			public bool IsByQuest;
		}
	}
}
