using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;
using TheraEngine.Graphics;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x0200049E RID: 1182
	public class InterestPointsManager : IUpdateableObject
	{
		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x000030FD File Offset: 0x000012FD
		private static bool occlusionSystem
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x000E6D4B File Offset: 0x000E4F4B
		public float SpyglassMakeNear
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000E6D52 File Offset: 0x000E4F52
		public static bool IsBuildingResearched(Vector2 {24382})
		{
			return Session.Account.FogOfWar.IsOpened({24382}, 0);
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x000E6D65 File Offset: 0x000E4F65
		[Nullable(2)]
		public Ship ShipInSight
		{
			[NullableContext(2)]
			get
			{
				return this.{24392}.GetShip;
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x000E6D72 File Offset: 0x000E4F72
		public int ShipInSightUidOrZero
		{
			get
			{
				Ship shipInSight = this.ShipInSight;
				if (shipInSight == null)
				{
					return 0;
				}
				return shipInSight.uID;
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x000E6D85 File Offset: 0x000E4F85
		private static float uiNearDist
		{
			get
			{
				return 0.18229167f * (float)Engine.GS.UIArea.Width;
			}
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x000E6DA0 File Offset: 0x000E4FA0
		public InterestPointsManager()
		{
			this.{24397} = new DynamicOcclusionSystem(this.{24398});
			this.{24397}.MinSkipDistance = 150f;
			this.{24397}.TestUpdateInterval = 500;
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000E6E10 File Offset: 0x000E5010
		public void Clean()
		{
			this.VisibleObjetcs.Size = 0;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000E6E20 File Offset: 0x000E5020
		public void Update(ref FrameTime {24383})
		{
			this.Clean();
			if (Global.Player == null)
			{
				return;
			}
			float num = Renderer.CameraFarPlane - 200f;
			num *= 1f - 0.33f * CommonGlobal.CurrentClientWeather.FogLevelClient;
			if (Global.Camera.IsSpyglass)
			{
				num *= 1f + this.SpyglassMakeNear;
			}
			else
			{
				num -= 200f;
			}
			this.{24398}.Size = 0;
			foreach (BuildingTarget buildingTarget in this.FetchTargets(num))
			{
				if (InterestPointsManager.occlusionSystem)
				{
					Tlist<DynamicOcclusionSystem.Request> tlist = this.{24398};
					DynamicOcclusionSystem.Request request = new DynamicOcclusionSystem.Request(buildingTarget.Center.X0Y() + new Vector3(0f, 7f, 0f), buildingTarget.Center.GetHashCode(), 25);
					tlist.Add(request);
					LightSourceOcclusionTest lightSourceOcclusionTest;
					if (this.{24397}.Tests.TryGetValue(buildingTarget.Center.GetHashCode(), out lightSourceOcclusionTest) && lightSourceOcclusionTest.LastResult > 0.5f)
					{
						this.VisibleObjetcs.Add(buildingTarget);
					}
				}
				else
				{
					this.VisibleObjetcs.Add(buildingTarget);
				}
			}
			this.ObjectAtSightTimeSec += {24383}.secElapsed;
			this.{24393} = this.{24390}();
			BuildingTarget? objectAtSight = this.GetObjectAtSight();
			if (objectAtSight != null)
			{
				Vector2? vector = (this.ObjectAtSight != null) ? new Vector2?(this.ObjectAtSight.GetValueOrDefault().Center) : null;
				if (!(vector != ((objectAtSight != null) ? new Vector2?(objectAtSight.GetValueOrDefault().Center) : null)))
				{
					goto IL_219;
				}
			}
			this.ObjectAtSightTimeSec = 0f;
			this.{24395} = 0f;
			IL_219:
			this.ObjectAtSight = objectAtSight;
			this.{24392} = new WeakShipReference(this.{24393});
			this.{24384}({24383});
			using (Dictionary<int, float>.KeyCollection.Enumerator enumerator2 = this.{24396}.Keys.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					int keyUID = enumerator2.Current;
					if (!this.VisibleObjetcs.Any((BuildingTarget {24401}) => {24401}.AsDynamic != null && {24401}.AsDynamic.GetValueOrDefault().uIDinScene == keyUID))
					{
						this.{24399}.Add(keyUID);
					}
				}
			}
			this.{24399}.Clear();
			Ship ship = this.{24393};
			int num2 = (ship != null) ? ship.uID : ((this.ObjectAtSight != null) ? this.ObjectAtSight.GetValueOrDefault().Center.GetHashCode() : 0);
			if (num2 != this.{24400})
			{
				if (num2 != 0)
				{
					Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIButtonInterop, 0.03f, 0.35f);
				}
				this.{24400} = num2;
			}
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x000E716C File Offset: 0x000E536C
		public void DrawOcclusionTest()
		{
			if (InterestPointsManager.occlusionSystem)
			{
				this.{24397}.Draw(Global.Render.ItemsShader);
			}
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x000E718C File Offset: 0x000E538C
		private void {24384}(FrameTime {24385})
		{
			this.CurrentTargetResearchLevel = 0f;
			if (this.ShipInSight != null)
			{
				this.CurrentTargetResearchLevel = ((IClientShip)this.{24393}).GetClient.ResearchingBySpyglassLevel;
			}
			else if (this.ObjectAtSight != null)
			{
				float num = Vector2.Distance(Global.Player.Position, this.ObjectAtSight.Value.Center);
				BuildingTarget value = this.ObjectAtSight.Value;
				if (value.AsDynamic != null)
				{
					value = this.ObjectAtSight.Value;
					int uIDinScene = value.AsDynamic.Value.uIDinScene;
					float num3;
					float num2 = this.{24396}.TryGetValue(uIDinScene, out num3) ? num3 : 0f;
					InterestPointsManager.UpdateResearch(num, ref num2, {24385}, true);
					this.{24396}[uIDinScene] = num2;
					this.CurrentTargetResearchLevel = num2;
				}
				else if (InterestPointsManager.IsBuildingResearched(this.ObjectAtSight.Value.Center))
				{
					this.CurrentTargetResearchLevel = 1f;
				}
				else
				{
					InterestPointsManager.UpdateResearch(num * 0.5f, ref this.{24395}, {24385}, true);
					this.CurrentTargetResearchLevel = this.{24395};
				}
			}
			if (this.CurrentTargetResearchLevel >= 1f && this.ObjectAtSight != null)
			{
				PlayerMapFogOfWar fogOfWar = Session.Account.FogOfWar;
				BuildingTarget value = this.ObjectAtSight.Value;
				fogOfWar.WhenResearchBuilding(value.Center);
			}
			if (this.ObjectAtSight == null)
			{
				this.{24395} = Math.Max(0f, this.{24395} - {24385}.secElapsed / 4f);
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000E7320 File Offset: 0x000E5520
		public static void UpdateResearch(float {24386}, ref float {24387}, FrameTime {24388}, bool {24389} = true)
		{
			if ({24386} < 170f)
			{
				{24387} = 1f;
				return;
			}
			float num = Math.Min(1f, ({24386} - 170f) / 70f);
			{24387} = Math.Min(1f, {24387} + {24388}.secElapsed * (3f - 1.5f * num) / 4f);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x000E7380 File Offset: 0x000E5580
		[NullableContext(2)]
		private Ship {24390}()
		{
			this.{24394} = InterestPointsManager.uiNearDist;
			Ship result = null;
			float num = InterestPointsManager.uiNearDist * 3f;
			foreach (Ship ship in Global.Game.WorldInstance.EnumerateShips(true, true, false))
			{
				if (!ship.IsDestroyed && ((IClientShip)ship).GetClient.IsVisibleWithOcclusionQueryAndCorpusTransparancy && ((IClientShip)ship).GetClient.IsVisible)
				{
					ShipNpc shipNpc = ship as ShipNpc;
					if (shipNpc == null || shipNpc.FirebrandTargetUID == 0)
					{
						Vector3 value = ship.Position3D ^ ship.UsedShip.StaticInfo.CorpusHalfHeight;
						Vector2 vector = Engine.GS.UIArea.HalfWidthHeight();
						Vector2 projection = Global.Camera.GetProjection(ref value);
						float num2 = Vector2.Distance(Global.Camera.GetProjection(value ^ ship.UsedShip.StaticInfo.CorpusHalfLength * 1.8f), projection);
						if (!Global.Camera.IsSpyglass)
						{
							num2 += InterestPointsManager.uiNearDist * 0.5f;
							projection.Y *= 0.66f;
							vector.Y *= 0.66f;
						}
						float num3;
						Vector2.Distance(ref projection, ref vector, out num3);
						if (num3 < num2 && num3 < num)
						{
							num = num3;
							result = ship;
						}
					}
				}
			}
			this.{24394} = num;
			return result;
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x000E7518 File Offset: 0x000E5718
		private BuildingTarget? GetObjectAtSight()
		{
			Vector2 vector = Engine.GS.UIArea.HalfWidthHeight();
			float num = this.{24394};
			if (this.{24393} != null)
			{
				num *= 0.5f;
			}
			BuildingTarget? result = null;
			foreach (BuildingTarget buildingTarget in ((IEnumerable<BuildingTarget>)this.VisibleObjetcs))
			{
				if (Global.Camera.IsSpyglass || !buildingTarget.ShowOnlyInSpyglass)
				{
					Vector3 value = buildingTarget.Center.X0Y ^ 5f;
					Vector2 value2 = Engine.GS.Camera.Position.XZ - value.XZ;
					if (Global.Camera.IsSpyglass)
					{
						value.XZ += value2 * this.SpyglassMakeNear;
					}
					if (Global.Camera.IsVisible(value, 1f))
					{
						Vector2 projection = Global.Camera.GetProjection(ref value);
						float num2 = Vector2.Distance(Global.Camera.GetProjection(value + new Vector3(0f, buildingTarget.Radius, 0f)), projection);
						if (!Global.Camera.IsSpyglass)
						{
							num2 += InterestPointsManager.uiNearDist * 0.5f;
							projection.Y *= 0.66f;
							vector.Y *= 0.66f;
						}
						float num3;
						Vector2.Distance(ref projection, ref vector, out num3);
						if (num3 < num2 && num3 < num)
						{
							this.{24393} = null;
							num = num3;
							result = new BuildingTarget?(buildingTarget);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x000E76DC File Offset: 0x000E58DC
		private IEnumerable<BuildingTarget> FetchTargets(float {24391})
		{
			InterestPointsManager.<FetchTargets>d__35 <FetchTargets>d__ = new InterestPointsManager.<FetchTargets>d__35(-2);
			<FetchTargets>d__.<>3__visibleDistance = {24391};
			return <FetchTargets>d__;
		}

		// Token: 0x0400183C RID: 6204
		private const float c_maxSightPreparationTimeMax = 4f;

		// Token: 0x0400183D RID: 6205
		private const float c_sighttPreparationMinDist = 170f;

		// Token: 0x0400183E RID: 6206
		private const float c_sighttPreparationMaxDist = 240f;

		// Token: 0x0400183F RID: 6207
		public Tlist<BuildingTarget> VisibleObjetcs = new Tlist<BuildingTarget>();

		// Token: 0x04001840 RID: 6208
		public BuildingTarget? ObjectAtSight;

		// Token: 0x04001841 RID: 6209
		public float ObjectAtSightTimeSec;

		// Token: 0x04001842 RID: 6210
		public float CurrentTargetResearchLevel;

		// Token: 0x04001843 RID: 6211
		private WeakShipReference {24392};

		// Token: 0x04001844 RID: 6212
		[Nullable(2)]
		private Ship {24393};

		// Token: 0x04001845 RID: 6213
		private float {24394};

		// Token: 0x04001846 RID: 6214
		private float {24395};

		// Token: 0x04001847 RID: 6215
		private Dictionary<int, float> {24396} = new Dictionary<int, float>();

		// Token: 0x04001848 RID: 6216
		private DynamicOcclusionSystem {24397};

		// Token: 0x04001849 RID: 6217
		private Tlist<DynamicOcclusionSystem.Request> {24398} = new Tlist<DynamicOcclusionSystem.Request>();

		// Token: 0x0400184A RID: 6218
		private Tlist<int> {24399} = new Tlist<int>();

		// Token: 0x0400184B RID: 6219
		private int {24400};
	}
}
