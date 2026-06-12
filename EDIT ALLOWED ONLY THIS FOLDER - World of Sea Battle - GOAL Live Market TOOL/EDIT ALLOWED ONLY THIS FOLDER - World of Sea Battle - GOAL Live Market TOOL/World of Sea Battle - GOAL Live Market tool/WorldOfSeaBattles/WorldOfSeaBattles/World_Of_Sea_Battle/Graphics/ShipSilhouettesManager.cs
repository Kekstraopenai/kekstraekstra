using System;
using System.Collections.Generic;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x0200044F RID: 1103
	public class ShipSilhouettesManager
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x000CF530 File Offset: 0x000CD730
		public Tlist<ShipSilhouettesManager.LocalObject> SceneObjects
		{
			get
			{
				return this.{23492};
			}
		}

		// Token: 0x060017FA RID: 6138 RVA: 0x000CF538 File Offset: 0x000CD738
		public ShipSilhouettesManager()
		{
			this.{23492} = new Tlist<ShipSilhouettesManager.LocalObject>();
		}

		// Token: 0x060017FB RID: 6139 RVA: 0x000CF54C File Offset: 0x000CD74C
		public void Push(Tlist<ShipSilhouetteInstance> {23487})
		{
			for (int i = 0; i < this.{23492}.Size; i++)
			{
				ShipSilhouettesManager.LocalObject localObject = this.{23492}.Array[i];
				bool flag = false;
				for (int j = 0; j < {23487}.Size; j++)
				{
					if ({23487}.Array[j].uID == localObject.uID)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					this.{23492}.RemoveAt(i);
					i--;
				}
			}
			for (int k = 0; k < {23487}.Size; k++)
			{
				ShipSilhouetteInstance shipSilhouetteInstance = {23487}.Array[k];
				bool flag2 = false;
				for (int l = 0; l < this.{23492}.Size; l++)
				{
					if (this.{23492}.Array[l].uID == shipSilhouetteInstance.uID)
					{
						this.{23492}.Array[l].PushNext(shipSilhouetteInstance.PositionAndAxis);
						flag2 = true;
						break;
					}
				}
				if (!flag2 && Global.Game.WorldInstance.GetShipFromUID(shipSilhouetteInstance.uID) == null)
				{
					Tlist<ShipSilhouettesManager.LocalObject> tlist = this.{23492};
					ShipSilhouettesManager.LocalObject localObject2 = new ShipSilhouettesManager.LocalObject(Gameplay.ShipsStaticInfo.FromID((int)shipSilhouetteInstance.ShipStaicID), shipSilhouetteInstance, shipSilhouetteInstance.uID);
					tlist.Add(localObject2);
				}
			}
			this.{23493} = {23487};
		}

		// Token: 0x060017FC RID: 6140 RVA: 0x000CF694 File Offset: 0x000CD894
		public void OnShipRemoveSVN(Ship {23488})
		{
			if (Vector2.Distance(Global.Player.Position, {23488}.Position) < 165f)
			{
				return;
			}
			if ({23488}.UsedShip.IsInvisibilityBonusEnabled)
			{
				return;
			}
			if (!this.{23492}.Contains((ShipSilhouettesManager.LocalObject {23515}) => {23515}.uID == {23488}.uID))
			{
				OpenWorldFlag {10209} = OpenWorldFlag.Pirate;
				ShipNpc shipNpc = {23488} as ShipNpc;
				if (shipNpc != null)
				{
					{10209} = shipNpc.UsedShipNpc.Information.Extras.Flags;
				}
				else
				{
					ShipOtherPlayer shipOtherPlayer = {23488} as ShipOtherPlayer;
					if (shipOtherPlayer != null)
					{
						{10209} = ((RemotePlayerDynamicInfo)shipOtherPlayer.UsedShipPlayer).Flags;
					}
				}
				ShipSilhouettesManager.LocalObject localObject = new ShipSilhouettesManager.LocalObject({23488}.UsedShip.StaticInfo, new ShipSilhouetteInstance({23488}, false, ({23488} as IClientShip).GetClient.Guild.Fraction, {10209}), {23488}.uID);
				localObject.PushComputeLeftover({23488});
				this.{23492}.Add(localObject);
			}
		}

		// Token: 0x060017FD RID: 6141 RVA: 0x000CF7B0 File Offset: 0x000CD9B0
		public void OnShipDestructed(Ship {23489})
		{
			this.OnShipAddSVN({23489}.uID);
		}

		// Token: 0x060017FE RID: 6142 RVA: 0x000CF7C0 File Offset: 0x000CD9C0
		public bool OnShipAddSVN(int {23490})
		{
			return this.{23492}.Remove((ShipSilhouettesManager.LocalObject {23516}) => {23516}.uID == {23490});
		}

		// Token: 0x060017FF RID: 6143 RVA: 0x000CF7F1 File Offset: 0x000CD9F1
		public void Clean()
		{
			this.{23493} = null;
			this.{23492}.Clear();
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x000CF808 File Offset: 0x000CDA08
		public void Render()
		{
			for (int i = 0; i < this.{23492}.Size; i++)
			{
				ShipSilhouettesManager.LocalObject localObject = this.{23492}.Array[i];
				if (localObject.transparancy == 1f)
				{
					localObject.Render();
				}
			}
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x000CF84C File Offset: 0x000CDA4C
		public void RenderTransparancy()
		{
			for (int i = 0; i < this.{23492}.Size; i++)
			{
				ShipSilhouettesManager.LocalObject localObject = this.{23492}.Array[i];
				if (localObject.transparancy < 1f && localObject.transparancy > (Renderer.ReflectionsAreBeingDrawn ? 0.25f : 0f))
				{
					localObject.Render();
				}
			}
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x000CF8AC File Offset: 0x000CDAAC
		public void Update(ref FrameTime {23491})
		{
			for (int i = 0; i < this.{23492}.Size; i++)
			{
				this.{23492}.Array[i].Update(ref {23491});
			}
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x000CF8E2 File Offset: 0x000CDAE2
		public IEnumerable<Vector2> GetPoistions()
		{
			ShipSilhouettesManager.<GetPoistions>d__14 <GetPoistions>d__ = new ShipSilhouettesManager.<GetPoistions>d__14(-2);
			<GetPoistions>d__.<>4__this = this;
			return <GetPoistions>d__;
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x000CF8F2 File Offset: 0x000CDAF2
		public IEnumerable<Vector3> GetPoistionsAndAngle()
		{
			ShipSilhouettesManager.<GetPoistionsAndAngle>d__15 <GetPoistionsAndAngle>d__ = new ShipSilhouettesManager.<GetPoistionsAndAngle>d__15(-2);
			<GetPoistionsAndAngle>d__.<>4__this = this;
			return <GetPoistionsAndAngle>d__;
		}

		// Token: 0x0400167D RID: 5757
		private Tlist<ShipSilhouettesManager.LocalObject> {23492};

		// Token: 0x0400167E RID: 5758
		private Tlist<ShipSilhouetteInstance> {23493};

		// Token: 0x02000450 RID: 1104
		public class LocalObject
		{
			// Token: 0x06001805 RID: 6149 RVA: 0x000CF904 File Offset: 0x000CDB04
			public LocalObject(ShipStaticInfo {23497}, ShipSilhouetteInstance {23498}, int {23499})
			{
				this.transform = new Transform3D(new Vector3({23498}.PositionAndAxis.X, 0f, {23498}.PositionAndAxis.Y), new Vector3(0f, {23498}.PositionAndAxis.Z, 0f), new Vector3(0.3f));
				this.Model = new ModelTransformedScene({23497}.Model.shipMin, this.transform);
				this.uID = {23499};
				this.{23514} = {23498};
				this.{23507} = {23498}.PositionAndAxis;
				this.{23509} = {23498}.PositionAndAxis;
				this.FlagpolesScene = new ModelTransformedScene
				{
					Transform = this.transform
				};
				Tlist<UWModel> flagpolls = {23497}.Model.flagpolls;
				this.{23512} = new Tlist<ModelRenderer>();
				for (int i = 0; i < flagpolls.Size; i++)
				{
					ModelRenderer modelRenderer = new ModelRenderer(flagpolls.Array[i])
					{
						LocalTransformOrNull = new Transform3D()
					};
					modelRenderer.Tag = new Vector3(modelRenderer.Model.MeshParts[0].LocalSpaceBoundingBox.Min.X, 0f, modelRenderer.Model.CommonSphere.Center.Z);
					this.{23512}.Add(modelRenderer);
					if (modelRenderer.Model.Drawcalls[0].Material.Albedo.AssetName != "Flagpoll2")
					{
						this.FlagpolesScene.AddObject(modelRenderer, true);
					}
				}
			}

			// Token: 0x06001806 RID: 6150 RVA: 0x000CFA90 File Offset: 0x000CDC90
			public void PushNext(Vector3 {23500})
			{
				this.{23507} = new Vector3(this.transform.Translation.X, this.transform.Translation.Z, this.transform.Yaw);
				this.{23509} = {23500};
				if (this.{23513}.LengthSquared() == 0f)
				{
					this.{23513} = this.{23509} - this.{23507};
				}
				this.{23509} += this.{23513} * 0.5f * new Vector3(1f, 1f, 0f);
				this.{23513} = this.{23509} - this.{23507};
				this.{23511} = 0f;
			}

			// Token: 0x06001807 RID: 6151 RVA: 0x000CFB60 File Offset: 0x000CDD60
			public void PushComputeLeftover(Ship {23501})
			{
				ShipPositionInfo shipPositionInfo = {23501}.ReconstructPosition(CommonGameConfig.CurrentSettings.SpecialMsgTimer * 0.75f, {23501}.GetShipPositionInfo);
				this.PushNext(new Vector3(shipPositionInfo.Position.X, shipPositionInfo.Position.Y, shipPositionInfo.Rotation));
			}

			// Token: 0x06001808 RID: 6152 RVA: 0x000CFBB4 File Offset: 0x000CDDB4
			public void Update(ref FrameTime {23502})
			{
				this.{23511} += {23502}.secElapsed / CommonGameConfig.CurrentSettings.SpecialMsgTimer * 1000f;
				this.transform.Translation.X = MathHelper.Lerp(this.{23507}.X, this.{23509}.X, this.{23511});
				this.transform.Translation.Z = MathHelper.Lerp(this.{23507}.Y, this.{23509}.Y, this.{23511});
				this.transform.Yaw = Geometry.AxisLerp(this.{23507}.Z, this.{23509}.Z, MathHelper.Clamp(this.{23511}, 0f, 1f));
				if (this.Model.IsMainCameraVisible)
				{
					Vector3 {11450};
					float {23504};
					CommonGlobal.CurrentClientWeather.NormalAndHeightHelper(Global.Player.MapInfo, this.transform.Translation.X, this.transform.Translation.Z, out {11450}, out {23504});
					Vector2 vector = Geometry.RotateVector2({11450}.XZ(), this.transform.Yaw) * 0.66f;
					ShipSilhouettesManager.LocalObject.EvaluteSmooth(ref this.transform.Translation.Y, {23504}, 0.9f, {23502}.secElapsed);
					ShipSilhouettesManager.LocalObject.EvaluteSmooth(ref this.transform.Pitch, vector.X, 0.25f, {23502}.secElapsed);
					ShipSilhouettesManager.LocalObject.EvaluteSmooth(ref this.transform.Roll, vector.Y, 0.25f, {23502}.secElapsed);
				}
				float num = WosbVisibility.VisibleDistance(Global.Player, Global.Game.StaticSystem.GetSkyShader.DayOrNight, CommonGlobal.CurrentClientWeather.FogLevelClient);
				float num2 = WosbVisibility.ShipSilhouetteDistance(Global.Player, Global.Game.StaticSystem.GetSkyShader.DayOrNight, CommonGlobal.CurrentClientWeather.FogLevelClient);
				float num3 = (num2 - Vector3.Distance(Global.Player.Position3D, this.transform.Translation)) / (num2 - num);
				num3 -= (1f - num3) * num3 / 3f * 4f;
				this.transparancy = MathHelper.Clamp(num3, 0f, 1f);
			}

			// Token: 0x06001809 RID: 6153 RVA: 0x000CFDF4 File Offset: 0x000CDFF4
			private static void EvaluteSmooth(ref float {23503}, float {23504}, float {23505}, float {23506})
			{
				{23505} *= Math.Abs(Math.Min(({23503} - {23504}) / {23505}, 1f));
				if ({23503} < {23504})
				{
					{23503} = Math.Min({23503} + {23505} * {23506}, {23504});
					return;
				}
				if ({23503} > {23504})
				{
					{23503} = Math.Max({23503} - {23505} * {23506}, {23504});
				}
			}

			// Token: 0x0600180A RID: 6154 RVA: 0x000CFE44 File Offset: 0x000CE044
			public void Render()
			{
				if (this.{23514}.ShipFullDesignIdOrZero != 0)
				{
					Global.Render.CommonShader.SetOrResetTextureReplaceDesign(new int?((int)this.{23514}.ShipFullDesignIdOrZero));
				}
				Global.Render.CommonShader.RenderObject(this.Model, true, this.transparancy, false, 0f, false);
				Global.Render.CommonShader.SetOrResetTextureReplaceDesign(null);
				if (this.{23512}.Size > 0 && this.Model.IsMainCameraVisible && !Renderer.ReflectionsAreBeingDrawn)
				{
					for (int i = 0; i < this.{23512}.Size; i++)
					{
						ModelRenderer modelRenderer = this.{23512}.Array[i];
						Vector3 vector = (Vector3)modelRenderer.Tag;
						modelRenderer.LocalTransformOrNull.CreateCenterPivotRotation(new Vector3(vector.X, 0f, vector.Z), new Vector3(0f, Global.Game.WorldInstance.LastWindAxis - this.transform.Yaw, 0f));
					}
					Material material = LocalContent.WorldFlagTexture(this.{23514}.WorldFlags, false, this.{23514}.Fraction, this.{23514}.IsNps, this.{23514}.IsFanatics, this.{23514}.IsPlayerCaper);
					Global.Render.CommonShader.ConfigureAnimatedMeshInNextRenderObject(Vector2.Zero, AnimationType.Flagpoll);
					Global.Render.CommonShader.SetSubstituteTexture(material.Albedo.Tex);
					Global.Render.CommonShader.RenderObject(this.FlagpolesScene, false, this.transparancy, false, 0f, false);
					Global.Render.CommonShader.SetSubstituteTexture(null);
				}
			}

			// Token: 0x0400167F RID: 5759
			public ModelTransformedScene Model;

			// Token: 0x04001680 RID: 5760
			public ModelTransformedScene FlagpolesScene;

			// Token: 0x04001681 RID: 5761
			public int uID;

			// Token: 0x04001682 RID: 5762
			public Transform3D transform;

			// Token: 0x04001683 RID: 5763
			public float transparancy;

			// Token: 0x04001684 RID: 5764
			private Vector3 {23507};

			// Token: 0x04001685 RID: 5765
			private Vector3 {23508};

			// Token: 0x04001686 RID: 5766
			private Vector3 {23509};

			// Token: 0x04001687 RID: 5767
			private Vector3 {23510};

			// Token: 0x04001688 RID: 5768
			private float {23511};

			// Token: 0x04001689 RID: 5769
			private Tlist<ModelRenderer> {23512};

			// Token: 0x0400168A RID: 5770
			private Vector3 {23513};

			// Token: 0x0400168B RID: 5771
			private readonly ShipSilhouetteInstance {23514};
		}
	}
}
