using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Core;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface.Controls;
using TheraEngine.Scene;
using TheraEngine.Scene.Lighting;
using UWContentPipelineExtensionRuntime;
using UWContentPipelineExtensionRuntime.Tags;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Interface;

namespace World_Of_Sea_Battle.Scenes
{
	// Token: 0x0200001B RID: 27
	internal sealed class PortVisualScene
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00004BCA File Offset: 0x00002DCA
		// (set) Token: 0x0600008F RID: 143 RVA: 0x00004BD2 File Offset: 0x00002DD2
		public bool Loaded { get; private set; }

		// Token: 0x06000090 RID: 144 RVA: 0x00004BDC File Offset: 0x00002DDC
		public PortVisualScene(ContentManager {16286})
		{
			this.{16316} = new Tlist<Transform3D>();
			this.{16315} = new Tlist<ModelTransformedScene>();
			this.{16317} = new Tlist<ModelTransformedScene>();
			this.{16318} = new ModelTransformedScene(LocalContent.Loaded.FloatingVerfy[0], new Transform3D());
			for (int i = 1; i < LocalContent.Loaded.FloatingVerfy.Length; i++)
			{
				this.{16318}.AddObject(new ModelRenderer(LocalContent.Loaded.FloatingVerfy[i])
				{
					LocalTransformOrNull = new Transform3D()
				}, true);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004CA0 File Offset: 0x00002EA0
		public void Connect()
		{
			IEnumerable<Shipway> shipwaysFunc = from {16353} in Session.Account.Shipyard.Shipways
			where Global.Player.NearPortType == PortEnteringType.Port && (int){16353}.PortID == Global.Player.NearPort.PortID
			select {16353};
			this.{16320} = new PaternConnector2<Shipway, ModelTransformedScene>(shipwaysFunc, delegate(Shipway {16360})
			{
				ModelTransformedScene modelTransformedScene2 = new ModelTransformedScene();
				modelTransformedScene2.AddObject({16360}.ShipInfo.StaticInfo.Model.corpus).Tag = 0f;
				modelTransformedScene2.AddObject({16360}.ShipInfo.StaticInfo.Model.masts).Tag = 0.3f;
				foreach (UWModel {11956} in ((IEnumerable<UWModel>){16360}.ShipInfo.StaticInfo.Model.mastRotateFrames))
				{
					modelTransformedScene2.AddObject({11956}).Tag = 0.5f;
				}
				modelTransformedScene2.AddObject({16360}.ShipInfo.StaticInfo.Model.unallocatdRoops).Tag = 0.6f;
				int num = Array.IndexOf<Shipway>(shipwaysFunc.ToArray<Shipway>(), {16360});
				ModelHardpoint[] array = (from {16354} in Global.Player.NearPort.ModelData.Model.ExternalHardpoints
				where {16354}.HardpointID == WorldOfSeaBattleHardpointID.HPAnchoredShip
				select {16354}).ToArray<ModelHardpoint>();
				if (Global.Player.NearPort.ModelData.Model.ModelName.Contains("port_center_big_1"))
				{
					array = array.Reverse<ModelHardpoint>().ToArray<ModelHardpoint>();
				}
				if (num < array.Length)
				{
					Vector3 {14807} = Global.Player.NearPort.GlobalTransform.Transform3X3(array[num].Transform.Translation);
					modelTransformedScene2.Transform = new Transform3D({14807}, new Vector3(0f, MathF.Atan2(array[num].Transform.Forward.Z, array[num].Transform.Forward.X) + Global.Player.NearPort.GlobalTransform.Yaw + 1.5707964f, 0f), new Vector3(0.3f));
				}
				else
				{
					modelTransformedScene2.Transform = new Transform3D(Global.Player.Position3D + new Vector3(10f, 0f, 10f), new Vector3(0f, 0f, 0f), new Vector3(0.3f));
				}
				modelTransformedScene2.Tag = {16360};
				Vector3 pos = modelTransformedScene2.Transform.Translation + new Vector3(0f, modelTransformedScene2.CombinedModelSpaceBS.Radius * 0.3f * 0.7f, 0f);
				Tlist<PortVisualScene.TIPRenderer> tlist2 = this.{16313};
				Action<ValueTuple<int, int>> <>9__21;
				PortVisualScene.TIPRenderer tiprenderer = new PortVisualScene.TIPRenderer({16360}, () => new Vector3?(pos), () => string.Empty, () => true, () => true, delegate()
				{
					if ({16360}.TimeToFinishSec == 0f)
					{
						base.<Connect>g__CallFinish|19();
						return;
					}
					GSI {5396} = Gameplay.ShipwayQuickFinishPrice({16360}.ShipInfo, 1f - {16360}.TimeToFinishSec / {16360}.InitialTime);
					{17177} {17177} = new {17177}(false);
					string shipName = {16360}.ShipInfo.ShipName;
					bool {17191} = false;
					string shipway_quick_build = Local.shipway_quick_build;
					Action<TextBlockBuilder> {17193} = delegate(TextBlockBuilder {16355})
					{
					};
					CraftingRecipe {17194} = new CraftingRecipe({5396});
					RTIf {17195} = 0f;
					int {17196} = 1;
					bool {17197} = true;
					Action<ValueTuple<int, int>> {17198};
					if (({17198} = <>9__21) == null)
					{
						{17198} = (<>9__21 = delegate([TupleElementNames(new string[]
						{
							"resCount",
							"btIndex"
						})] ValueTuple<int, int> {16362})
						{
							{16360}.TimeToFinishSec = 0f;
							{17177}.CurrentInstance.BlockAndClose();
							base.<Connect>g__CallFinish|19();
						});
					}
					{17177}.SetData(shipName, {17191}, shipway_quick_build, {17193}, {17194}, {17195}, {17196}, {17197}, {17198}, false, null, new string[]
					{
						Local.shop
					}, 1, true, int.MaxValue, false, -1f);
				}, Rectangle.Empty);
				tlist2.Add(tiprenderer);
				return modelTransformedScene2;
			}, (Shipway {16356}, Shipway {16357}) => {16356} == {16357}, delegate(ModelTransformedScene {16361})
			{
				this.{16313}.RemoveAll((PortVisualScene.TIPRenderer {16363}) => {16363}.id.Equals({16361}.Tag));
			});
			this.{16313} = new Tlist<PortVisualScene.TIPRenderer>();
			if (Global.Player.NearPortType == PortEnteringType.Port)
			{
				IslePortInfo port = Global.Player.NearPort;
				this.AddTip(PortTipConnection.Hq, PortVisualScene.c_tip_guildHq, () => Local.port_guild_hall, () => true, () => port.AllowCapture || port.AllowCapturePiratePort || port.IsArabPort, delegate
				{
					if (Session.GuildUnreceivedSalary > 0 && Session.Game.NearPortRelation == Relation.Ally)
					{
						new {17312}(Local.guild_take_salary_q(Session.GuildUnreceivedSalary), delegate(int {16358})
						{
							if ({16358} == 0)
							{
								Global.Network.Send(new OnGuildSalaryAction(0));
							}
							else if ({16358} == 1)
							{
								Global.Network.Send(new OnGuildSalaryAction(-1));
							}
							else
							{
								PortVisualScene.<Connect>g__AccessibilityApi|28_27(PortTipConnection.Hq, delegate
								{
									new {20664}();
								});
							}
							EducationHelper.MakeFlag(EducationOnboarding.GameTT_FirstGuildSalary, true);
						}, new {17443}[]
						{
							new {17443}(Local.take_2, "", {17312}.cIconAccept, false, 0f),
							new {17443}(Local.TavernaCommonUi_7, Local.PortNumerticInputBasicWindow_9, {17312}.cIconShield, false, 0f),
							new {17443}(Local.decide_later, "", {17312}.cIconSpyglass, false, 0f)
						});
						return;
					}
					PortVisualScene.<Connect>g__AccessibilityApi|28_27(PortTipConnection.Hq, delegate
					{
						new {20664}();
					});
				});
				this.AddTip(PortTipConnection.Verfy, PortVisualScene.c_tip_verfy, () => Local.PortVisualScene_0, () => EducationHelper.IsPortButtonAvailableAndOpenable(PortTipConnection.Verfy), () => true, delegate
				{
					PortVisualScene.<Connect>g__AccessibilityApi|28_27(PortTipConnection.Verfy, delegate
					{
						Global.Game.ScenePort.verfHandler(null);
					});
				});
				this.AddTip(PortTipConnection.Trade, PortVisualScene.c_tip_trade, () => Global.Game.ScenePort.TradehouseName, () => EducationHelper.IsPortButtonAvailableAndOpenable(PortTipConnection.Trade), () => true, delegate
				{
					PortVisualScene.<Connect>g__AccessibilityApi|28_27(PortTipConnection.Trade, delegate
					{
						Global.Game.ScenePort.tradeHandler(null);
					});
				});
				this.AddTip(PortTipConnection.Workshop, PortVisualScene.c_tip_worksh, () => Local.CommonBuildingWindow_1, () => EducationHelper.IsPortButtonAvailableAndOpenable(PortTipConnection.Workshop), () => true, delegate
				{
					PortVisualScene.<Connect>g__AccessibilityApi|28_27(PortTipConnection.Workshop, delegate
					{
						Global.Game.ScenePort.workshopHandler(null);
					});
				});
				this.AddTip(PortTipConnection.RealShop, PortVisualScene.c_tip_realshop, () => Local.PortVisualScene_2, () => EducationHelper.IsPortButtonAvailableAndOpenable(PortTipConnection.RealShop), () => !PlatformTuning.DisableShop && !PlatformTuning.DisableShopWindowNow, delegate
				{
					PortVisualScene.<Connect>g__AccessibilityApi|28_27(PortTipConnection.RealShop, delegate
					{
						Global.Game.ScenePort.realShopHandler(null, null);
					});
				});
				this.AddTip(PortTipConnection.Taverna, new Rectangle(1081, 92, 46, 54), () => Global.Game.ScenePort.QuesthouseName, () => EducationHelper.IsPortButtonAvailableAndOpenable(PortTipConnection.Taverna), () => true, delegate
				{
					PortVisualScene.<Connect>g__AccessibilityApi|28_27(PortTipConnection.Taverna, delegate
					{
						new {21031}();
					});
				});
				this.AddTip(PortTipConnection.PirateTrader, PortVisualScene.c_tip_passingmaps, () => Local.PirateTrader, () => true, () => port.Type == PortType.PirateBay || Session.Game.NearPortStatus.CapturerFraction == FractionID.Pirate, delegate
				{
					new {18281}(72, Local.PirateTrader, WosbTrading.PirateTrader.ToArray());
				});
				this.AddTip(PortTipConnection.OverseasTrader, PortVisualScene.c_tip_passingmaps, () => Local.OverseasTrader, () => true, () => CalendarEvents.HasTrader && Session.Account.Rang >= 7, delegate
				{
					new {18281}(69, Local.OverseasTrader, CalendarEvents.CurrentEvent.GoldfishTrader.ToArray());
				});
				PointLightArrayHolder pointlights = Global.Render.Pointlights;
				PointLight pointLight = new PointLight(port.VisualTips[PortTipConnection.RealShop].Array[0], Color.Orange.ToVector3(), 1.2f, 45f);
				pointLight.OlccusionaryFlaresOpacity = 0f;
				PointLight {12312} = pointLight;
				this.{16321} = pointLight;
				pointlights.Add({12312});
				Vector3 value = port.VisualTips[PortTipConnection.QuestUnit].Array[0];
				Transform3D transform3D = new Transform3D(port.GlobalTransform);
				transform3D.MiddleScale *= 0.013200001f;
				transform3D.Translation = value - new Vector3(0f, 2.2f, 0f);
				Transform3D transform3D2 = transform3D;
				Vector2 zero = Vector2.Zero;
				Vector2 vector = transform3D.Translation.XZ() - port.EntryPos;
				transform3D2.Yaw = Geometry.GetRotate(zero, vector) + 1.5707964f;
				UWModel uwmodel = LocalContent.Loaded.UnitsOfficer[0];
				ModelRenderer questUnitRenderer = new ModelRenderer(uwmodel, null, 0.5f)
				{
					LocalVisible = false
				};
				if (uwmodel.SkinningDataOrNull != null)
				{
					Dictionary<UnitAnimation, AnimationClip> dictionary = (Dictionary<UnitAnimation, AnimationClip>)questUnitRenderer.Model.Tag;
					questUnitRenderer.Animation.StartClip(dictionary[UnitAnimation.Idle_1], null);
				}
				Tlist<ModelTransformedScene> tlist = this.{16315};
				ModelTransformedScene modelTransformedScene = new ModelTransformedScene(questUnitRenderer, transform3D)
				{
					Tag = true
				};
				tlist.Add(modelTransformedScene);
				this.AddTip(PortTipConnection.QuestUnit, new Rectangle(1548, 296, 43, 58), () => Local.somebodyInPort, () => true, () => questUnitRenderer.LocalVisible = (this.{16310}() != null), delegate
				{
					this.{16310}()();
				});
				Vector2 vector2 = (port.MiddleInfrastructurePostion - port.EntryPos.X0Y()).XZNormal();
				Engine.GS.Camera.Rotation = new Vector3(Engine.GS.Camera.Rotation.X, MathF.Atan2(vector2.Y, vector2.X) + 1.5707964f, Engine.GS.Camera.Rotation.Z);
			}
			else if (Global.Player.NearPortType != PortEnteringType.Miniport && Global.Player.NearPortType == PortEnteringType.PersonalIsle)
			{
				PersonalIsleStatus.InternalBuilding {16293} = PersonalIsleStatus.InternalBuilding.Pub;
				PortTipConnection {16294} = (PortTipConnection)100;
				string personal_isle_b7_name = Local.personal_isle_b7_name;
				Func<string> {16296} = () => Local.personal_isle_b7_tt;
				Action {16297};
				if (({16297} = PortVisualScene.<>O.<0>__OpenPubWindow) == null)
				{
					{16297} = (PortVisualScene.<>O.<0>__OpenPubWindow = new Action({20547}.OpenPubWindow));
				}
				this.AddTipPI({16293}, {16294}, personal_isle_b7_name, {16296}, {16297});
				PersonalIsleStatus.InternalBuilding {16293}2 = PersonalIsleStatus.InternalBuilding.Factory;
				PortTipConnection {16294}2 = (PortTipConnection)100;
				string personal_isle_b6_name = Local.personal_isle_b6_name;
				Func<string> {16296}2 = () => Local.personal_isle_b6_tt;
				Action {16297}2;
				if (({16297}2 = PortVisualScene.<>O.<1>__OpenFactoryWindow) == null)
				{
					{16297}2 = (PortVisualScene.<>O.<1>__OpenFactoryWindow = new Action({20547}.OpenFactoryWindow));
				}
				this.AddTipPI({16293}2, {16294}2, personal_isle_b6_name, {16296}2, {16297}2);
				if (!PlatformTuning.DisableShop)
				{
					this.AddTipPI(PersonalIsleStatus.InternalBuilding.BigStorage, PortTipConnection.RealShop, Local.PortVisualScene_2, () => "", delegate
					{
						Global.Game.ScenePort.realShopHandler(null, null);
					});
				}
				this.AddTipPI(PersonalIsleStatus.InternalBuilding.BigStorage, (PortTipConnection)100, Local.personal_isle_b4_name, () => Local.personal_isle_b4_tt, null);
				PersonalIsleStatus.InternalBuilding {16293}3 = PersonalIsleStatus.InternalBuilding.Dok;
				PortTipConnection {16294}3 = (PortTipConnection)100;
				string personal_isle_b1_name = Local.personal_isle_b1_name;
				Func<string> {16296}3 = () => Local.personal_isle_b1_tt;
				Action {16297}3;
				if (({16297}3 = PortVisualScene.<>O.<2>__OpenDokWindow) == null)
				{
					{16297}3 = (PortVisualScene.<>O.<2>__OpenDokWindow = new Action({20547}.OpenDokWindow));
				}
				this.AddTipPI({16293}3, {16294}3, personal_isle_b1_name, {16296}3, {16297}3);
				PersonalIsleStatus.InternalBuilding {16293}4 = PersonalIsleStatus.InternalBuilding.Workshop;
				PortTipConnection {16294}4 = (PortTipConnection)100;
				string personal_isle_b3_name = Local.personal_isle_b3_name;
				Func<string> {16296}4 = () => Local.personal_isle_b3_tt;
				Action {16297}4;
				if (({16297}4 = PortVisualScene.<>O.<3>__OpenWorkshopWindow) == null)
				{
					{16297}4 = (PortVisualScene.<>O.<3>__OpenWorkshopWindow = new Action({20547}.OpenWorkshopWindow));
				}
				this.AddTipPI({16293}4, {16294}4, personal_isle_b3_name, {16296}4, {16297}4);
				this.AddTipPI(PersonalIsleStatus.InternalBuilding.Avanpost, (PortTipConnection)100, Local.personal_isle_b5_name, () => Local.personal_isle_b5_tt(Global.Game.ScenePort.CurrentPersonalIsle.ExpeditionDistance, Global.Game.ScenePort.CurrentPersonalIsle.ExpeditionPriceCT), null);
				this.AddTipPI(PersonalIsleStatus.InternalBuilding.Lighthouse, (PortTipConnection)100, Local.personal_isle_b8_name, () => Local.personal_isle_b8_tt, null);
			}
			{20791}.OnClicked += this.{16298};
			this.{16322} = 0;
			this.Loaded = true;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005654 File Offset: 0x00003854
		private void AddTip(PortTipConnection {16287}, Rectangle {16288}, Func<string> {16289}, Func<bool> {16290}, Func<bool> {16291}, Action {16292})
		{
			Vector3 pos = Global.Player.NearPort.VisualTips[{16287}].Array[0];
			if ({16287} == PortTipConnection.OverseasTrader && !Global.Player.MapInfo.CheckPosition(pos.XZ(), true, false, null))
			{
				pos = (Geometry.RotateVector2(Global.Player.NearPort.VisualTips[PortTipConnection.PirateTrader].Array[0].XZ() - Global.Player.NearPort.EntryPos, -0.5f) + Global.Player.NearPort.EntryPos).X0Y() + new Vector3(0f, pos.Y, 0f);
			}
			if ({16287} == PortTipConnection.QuestUnit)
			{
				pos += new Vector3(0f, 4f, 0f);
			}
			Tlist<PortVisualScene.TIPRenderer> tlist = this.{16313};
			PortVisualScene.TIPRenderer tiprenderer = new PortVisualScene.TIPRenderer({16287}, () => new Vector3?(pos), {16289}, {16290}, {16291}, {16292}, {16288});
			tlist.Add(tiprenderer);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005790 File Offset: 0x00003990
		private void AddTipPI(PersonalIsleStatus.InternalBuilding {16293}, PortTipConnection {16294}, string {16295}, Func<string> {16296}, Action {16297})
		{
			PortVisualScene.<>c__DisplayClass30_0 CS$<>8__locals1 = new PortVisualScene.<>c__DisplayClass30_0();
			CS$<>8__locals1.id = {16294};
			CS$<>8__locals1.id2 = {16293};
			CS$<>8__locals1.text = {16295};
			CS$<>8__locals1.desc = {16296};
			CS$<>8__locals1.hasOwnUi = {16297};
			PersonalIsleStatus currentPersonalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
			UWModel uwmodel = currentPersonalIsle.Place.ModelData.Connections[(CS$<>8__locals1.id == PortTipConnection.RealShop) ? "ConnectionRealShop" : IsleInnerModelsHelper.PersonalIsleConnectionsL1[CS$<>8__locals1.id2]];
			CS$<>8__locals1.pos = currentPersonalIsle.Place.GlobalTransform.Transform3X3(uwmodel.CommonSphere.Center) + new Vector3(0f, 0.5f, 0f);
			CS$<>8__locals1.pos.Y = Math.Max(3f, CS$<>8__locals1.pos.Y);
			if (CS$<>8__locals1.id == PortTipConnection.RealShop)
			{
				PortVisualScene.<>c__DisplayClass30_0 CS$<>8__locals2 = CS$<>8__locals1;
				CS$<>8__locals2.pos.Y = CS$<>8__locals2.pos.Y - 0.5f;
			}
			if (CS$<>8__locals1.id2 == PersonalIsleStatus.InternalBuilding.Pub)
			{
				PortVisualScene.<>c__DisplayClass30_0 CS$<>8__locals3 = CS$<>8__locals1;
				CS$<>8__locals3.pos.Y = CS$<>8__locals3.pos.Y + 1.3f;
			}
			if (CS$<>8__locals1.id2 == PersonalIsleStatus.InternalBuilding.Factory)
			{
				PortVisualScene.<>c__DisplayClass30_0 CS$<>8__locals4 = CS$<>8__locals1;
				CS$<>8__locals4.pos.Y = CS$<>8__locals4.pos.Y + 0.3f;
			}
			if (CS$<>8__locals1.id2 == PersonalIsleStatus.InternalBuilding.BigStorage)
			{
				PortVisualScene.<>c__DisplayClass30_0 CS$<>8__locals5 = CS$<>8__locals1;
				CS$<>8__locals5.pos.Y = CS$<>8__locals5.pos.Y + 0.3f;
			}
			if (CS$<>8__locals1.id2 == PersonalIsleStatus.InternalBuilding.Avanpost)
			{
				PortVisualScene.<>c__DisplayClass30_0 CS$<>8__locals6 = CS$<>8__locals1;
				CS$<>8__locals6.pos.Y = CS$<>8__locals6.pos.Y + 1.3f;
			}
			CS$<>8__locals1.visibleText = CS$<>8__locals1.text;
			while (CS$<>8__locals1.visibleText.Length < 8)
			{
				CS$<>8__locals1.visibleText = " " + CS$<>8__locals1.visibleText + " ";
			}
			Tlist<PortVisualScene.TIPRenderer> tlist = this.{16313};
			PortVisualScene.TIPRenderer tiprenderer = new PortVisualScene.TIPRenderer((CS$<>8__locals1.id == PortTipConnection.RealShop) ? CS$<>8__locals1.id : CS$<>8__locals1.id2, () => new Vector3?(CS$<>8__locals1.pos), () => CS$<>8__locals1.visibleText, () => true, () => true, delegate()
			{
				{20547}.OpenBuildBuildingWindow(CS$<>8__locals1.id, CS$<>8__locals1.id2, CS$<>8__locals1.text, CS$<>8__locals1.desc(), CS$<>8__locals1.hasOwnUi);
			}, Rectangle.Empty);
			tlist.Add(tiprenderer);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000059CA File Offset: 0x00003BCA
		private void {16298}()
		{
			if (this.{16314} != null && Global.Game.ScenePort.IsMainPage && this.{16314}.isVisible())
			{
				this.{16314}.onClick();
			}
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00005A08 File Offset: 0x00003C08
		public void Disconnect()
		{
			if (!this.Loaded)
			{
				return;
			}
			this.{16314} = null;
			{20791}.OnClicked -= this.{16298};
			if (this.{16321} != null)
			{
				Global.Render.Pointlights.Remove(this.{16321});
			}
			this.{16321} = null;
			if (this.{16323} != null)
			{
				Global.Game.WorldInstance.RemoveDecoration(this.{16323});
			}
			this.{16323} = null;
			ModelTransformedScene modelTransformedScene = this.{16324};
			if (modelTransformedScene != null)
			{
				modelTransformedScene.Clear();
			}
			this.{16322} = 0;
			this.{16315}.Clear();
			this.{16317}.Clear();
			if (Global.Player != null && Global.Player.InstanceAlive)
			{
				Global.Game.WorldInstance.UpdateQuestsInSea();
			}
			this.Loaded = false;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00005AD8 File Offset: 0x00003CD8
		public void LoopEffects(ref FrameTime {16299})
		{
			if (!this.Loaded)
			{
				return;
			}
			int num = this.{16322};
			this.{16322} = num + 1;
			if (num >= 2)
			{
				this.{16302}();
			}
			foreach (ModelTransformedScene modelTransformedScene in ((IEnumerable<ModelTransformedScene>)this.{16317}))
			{
				modelTransformedScene.Transform.Translation.Y = CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, modelTransformedScene.Transform.Translation.X, modelTransformedScene.Transform.Translation.Z);
			}
			this.{16325}.Evalute(ref {16299}, this.{16314} != null);
			this.{16319}.Evalute(ref {16299}, !Global.Player.CraftFrom.StaticInfo.IsBalloon && (Global.Player.UsedShipPlayer.ClientTimeToRestoreIntegrity > 0f || Global.Settings.DeathController.BlockPortExitSec > 0f));
			if ({22279}.CurrentInstance != null)
			{
				this.{16319}.CurrentSoftValue = Math.Min(0.2f, this.{16319}.CurrentSoftValue);
			}
			this.{16320}.Update();
			foreach (ModelTransformedScene modelTransformedScene2 in ((IEnumerable<ModelTransformedScene>)this.{16320}.Targets))
			{
				Shipway shipway = modelTransformedScene2.Tag as Shipway;
				foreach (ModelRenderer modelRenderer in ((IEnumerable<ModelRenderer>)modelTransformedScene2.GetModels))
				{
					modelRenderer.LocalVisible = ((float)modelRenderer.Tag <= shipway.BuildingFactor);
				}
			}
			if (!Session.Account.EducationQuest[EducationOnboarding.InteropQuestUnit] && Session.Account.Rang == 1 && Session.Account.EducationQuest[EducationOnboarding.OpenWorldMap] && {17312}.CurrentInstance != null)
			{
				Global.Camera.RotateToTarget(this.{16313}.First(delegate(PortVisualScene.TIPRenderer {16359})
				{
					object id = {16359}.id;
					return id is PortTipConnection && (PortTipConnection)id == PortTipConnection.QuestUnit;
				}).globalPos().Value.XZ, {16299}.secElapsed);
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00005D4C File Offset: 0x00003F4C
		private PortVisualScene.TIPRenderer {16300}(object {16301})
		{
			PortVisualScene.TIPRenderer result;
			if (this.{16313}.TryFind((PortVisualScene.TIPRenderer {16364}) => {16301}.Equals({16364}.id), out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00005D84 File Offset: 0x00003F84
		private void {16302}()
		{
			if (Global.Player.NearPortType != PortEnteringType.Port)
			{
				return;
			}
			if (CalendarEvents.HasTrader)
			{
				if (this.{16323} == null)
				{
					PortVisualScene.TIPRenderer tiprenderer = this.{16300}(PortTipConnection.OverseasTrader);
					if (tiprenderer != null)
					{
						ShipPositionInfo {24937} = new ShipPositionInfo(new Vector2(tiprenderer.globalPos().Value.X, tiprenderer.globalPos().Value.Z), 1f);
						this.{16323} = Global.Game.WorldInstance.CreateDecorationShip(12, {24937});
					}
				}
			}
			else if (this.{16323} != null)
			{
				Global.Game.WorldInstance.RemoveDecoration(this.{16323});
				this.{16323} = null;
			}
			if (this.{16324} == null)
			{
				this.{16324} = new ModelTransformedScene();
			}
			if (Global.Player.NearPort.Type == PortType.PirateBay || Session.Game.NearPortStatus.CapturerFraction == FractionID.Pirate)
			{
				if (this.{16324}.CountModels == 0)
				{
					PortVisualScene.TIPRenderer tiprenderer2 = this.{16300}(PortTipConnection.PirateTrader);
					if (tiprenderer2 != null)
					{
						Vector3 value = tiprenderer2.globalPos().Value;
						Vector3 vector = Global.Player.Position3D - value;
						float num = MathF.Atan2(vector.Z, vector.X);
						this.{16324}.Transform = new Transform3D(new Transform3D(new Vector3(value.X, 0f, value.Z), new Vector3(0f, num + 0.62831855f, 0f), new Vector3(0.45000002f)));
						this.{16324}.AddObject(LocalContent.Loaded.DropdecorPassing);
						this.{16317}.Add(this.{16324});
						this.{16315}.Add(this.{16324});
						return;
					}
				}
			}
			else if (this.{16324}.CountModels > 0)
			{
				ModelTransformedScene modelTransformedScene = this.{16324};
				if (modelTransformedScene != null)
				{
					modelTransformedScene.Clear();
				}
				this.{16317}.Remove(this.{16324});
				this.{16315}.Remove(this.{16324});
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00005FAC File Offset: 0x000041AC
		public void Render()
		{
			if (!this.Loaded)
			{
				return;
			}
			if (this.{16321} != null)
			{
				this.{16321}.Intensivity = 1f - Global.Game.StaticSystem.GetSkyShader.DayOrNight;
			}
			if (this.{16319}.CurrentSoftValue > 0f)
			{
				this.{16303}(Global.Player.Transform, Math.Min(9f, Global.Player.UsedShip.StaticInfo.CorpusHalfLength) / 6f);
			}
			Global.Render.CommonShader.ConfigureAnimatedMeshInNextRenderObject(Vector2.Zero, AnimationType.Flora);
			for (int i = 0; i < this.{16315}.Size; i++)
			{
				object tag = this.{16315}.Array[i].Tag;
				if (tag is bool && (bool)tag)
				{
					Global.Render.CommonShader.RenderObject(this.{16315}.Array[i], false, 1f, false, 0f, false);
				}
				else
				{
					Global.Render.CommonShader.RenderIsle(this.{16315}.Array[i], false, -1);
				}
			}
			if (this.{16319}.CurrentSoftValue >= 1f)
			{
				Global.Render.CommonShader.RenderObject(this.{16318}, true, 1f, false, 0f, false);
			}
			foreach (ModelTransformedScene modelTransformedScene in ((IEnumerable<ModelTransformedScene>)this.{16320}.Targets))
			{
				Global.Render.CommonShader.RenderObject(modelTransformedScene, true, 1f, false, 0f, false);
				if (modelTransformedScene.Transform.Translation.Y == 0f)
				{
					this.{16303}(modelTransformedScene.Transform, (modelTransformedScene.Tag as Shipway).ShipInfo.StaticInfo.CorpusHalfLength / 6f);
					Global.Render.CommonShader.RenderObject(this.{16318}, false, 1f, false, 0f, false);
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000061C8 File Offset: 0x000043C8
		private void {16303}(Transform3D {16304}, float {16305})
		{
			this.{16318}.Transform.Translation = {16304}.Translation;
			this.{16318}.Transform.RotatesAll = {16304}.RotatesAll;
			this.{16318}.Transform.MiddleScale = {16304}.MiddleScale * {16305};
			Matrix matrix;
			this.{16318}.Transform.CreateWorldMatrix(out matrix);
			Matrix matrix2;
			Matrix.CreateFromYawPitchRoll(-this.{16318}.Transform.Yaw, -this.{16318}.Transform.Pitch, -this.{16318}.Transform.Roll, out matrix2);
			for (int i = 1; i < this.{16318}.CountModels; i++)
			{
				Vector3 center = this.{16318}.GetModels[i].Model.CommonSphere.Center;
				Vector3 vector = Vector3.Transform(center, matrix);
				Vector3 vector2 = Vector3.Transform(center, matrix2);
				this.{16318}.GetModels[i].LocalTransformOrNull.Translation.Y = -this.{16318}.Transform.Translation.Y + CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, vector.X, vector.Z) - (vector2.Y - center.Y);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000631C File Offset: 0x0000451C
		public void RenderStatic()
		{
			if (!this.Loaded)
			{
				return;
			}
			if (this.{16319}.CurrentSoftValue > 0f && this.{16319}.CurrentSoftValue < 1f)
			{
				Global.Render.CommonShader.RenderObject(this.{16318}, true, this.{16319}.CurrentSoftValueSmoothstep, false, 0f, false);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006380 File Offset: 0x00004580
		public void RenderToGBuffer(IGBufferBuilder {16306})
		{
			if (!this.Loaded)
			{
				return;
			}
			for (int i = 0; i < this.{16315}.Size; i++)
			{
				if (this.{16315}.Array[i].IsMainCameraVisible)
				{
					this.{16315}.Array[i].CheckVisibilityAndRenderGBuffer({16306}, null);
				}
			}
			foreach (ModelTransformedScene modelTransformedScene in ((IEnumerable<ModelTransformedScene>)this.{16320}.Targets))
			{
				if (modelTransformedScene.IsMainCameraVisible)
				{
					modelTransformedScene.CheckVisibilityAndRenderGBuffer({16306}, null);
				}
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006424 File Offset: 0x00004624
		public void Render2D()
		{
			if (!this.Loaded)
			{
				return;
			}
			if (!Global.Game.ScenePort.IsVisibleMainUi || ({20547}.CurrentInstance != null && {20547}.CurrentInstance.DecorMode) || {19086}.CurrentMenu != null || Global.Game.ScenePort.IsPortExiting)
			{
				return;
			}
			this.{16314} = null;
			float maxValue = float.MaxValue;
			for (int i = 0; i < this.{16313}.Size; i++)
			{
				this.{16313}.Array[i].Update(ref this.{16314}, ref maxValue);
			}
			Device gs = Engine.GS;
			gs.SetTexture(AtlasPortGui.Texture);
			for (int j = 0; j < this.{16313}.Size; j++)
			{
				this.{16313}.Array[j].Render(gs, this.{16314} == this.{16313}.Array[j]);
			}
			if (this.{16314} != null && Global.Game.GetInterfaceManager.Host.CheckHaveDirectFocus && Global.Game.ScenePort.IsMainPage && Global.Player.NearPortType == PortEnteringType.Port)
			{
				this.{16314}.RendeHighlight(gs);
				if (Global.Settings.DeathController.BlockPortExitSec == 0f)
				{
					object id = this.{16314}.id;
					if (id is PortTipConnection)
					{
						PortTipConnection portTipConnection = (PortTipConnection)id;
						if (this.{16314}.isActive())
						{
							if (portTipConnection == PortTipConnection.Verfy)
							{
								this.{16307}(Local.PortVisualScene_0, new string[]
								{
									Local.PortVisualScene_40,
									Local.PortVisualScene_41
								});
							}
							if (portTipConnection == PortTipConnection.Trade)
							{
								this.{16307}(Global.Game.ScenePort.TradehouseName, new string[]
								{
									Local.PortVisualScene_42,
									Local.PortVisualScene_43,
									Local.PortVisualScene_44,
									Local.PortVisualScene_45,
									Local.PortVisualScene_46
								});
							}
							if (portTipConnection == PortTipConnection.Workshop)
							{
								this.{16307}(Local.CommonBuildingWindow_1, new string[]
								{
									Local.PortVisualScene_47,
									Local.PortVisualScene_48,
									Local.PortVisualScene_49
								});
							}
							if (portTipConnection == PortTipConnection.RealShop)
							{
								this.{16307}(Local.PortVisualScene_2, new string[]
								{
									Local.PortVisualScene_50
								});
							}
							if (portTipConnection == PortTipConnection.Taverna)
							{
								this.{16307}(this.{16314}.text(), new string[]
								{
									Local.PortVisualScene_52
								});
							}
							if (portTipConnection == PortTipConnection.Hq)
							{
								this.{16307}(this.{16314}.text(), new string[]
								{
									Local.port_guild_hall_tt
								});
							}
						}
					}
				}
			}
			gs.ReturnBackTexture();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x000066BC File Offset: 0x000048BC
		private void {16307}(string {16308}, params string[] {16309})
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000066CC File Offset: 0x000048CC
		private Action {16310}()
		{
			if (Session.Account.Rang != 1)
			{
				EducationHelper.MakeFlag(EducationOnboarding.InteropQuestUnit, true);
				EducationHelper.MakeFlag(EducationOnboarding.OpenWorldMap, true);
			}
			if (!Session.Account.EducationQuest.HasFlag(EducationOnboarding.OpenWorldMap))
			{
				return null;
			}
			if (!Session.Account.EducationQuest.HasFlag(EducationOnboarding.InteropQuestUnit))
			{
				return delegate()
				{
					EducationHelper.MakeFlag(EducationOnboarding.InteropQuestUnit, true);
					new {19779}(false, null, null);
				};
			}
			return EducationHelper.GetEducationQuestNeedToFinishInPort();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00006829 File Offset: 0x00004A29
		[CompilerGenerated]
		internal static void <Connect>g__AccessibilityApi|28_27(PortTipConnection {16311}, Action {16312})
		{
			if (!EducationHelper.IsPortButtonAvailableAndOpenable({16311}))
			{
				EducationHelper.ShowBlockingByQuestError();
				return;
			}
			{16312}();
		}

		// Token: 0x04000056 RID: 86
		private static readonly Rectangle c_tip_passingmaps = new Rectangle(1240, 296, 43, 58);

		// Token: 0x04000057 RID: 87
		private static readonly Rectangle c_tip_arena = new Rectangle(1284, 296, 43, 58);

		// Token: 0x04000058 RID: 88
		private static readonly Rectangle c_tip_verfy = new Rectangle(1328, 296, 43, 58);

		// Token: 0x04000059 RID: 89
		private static readonly Rectangle c_tip_worksh = new Rectangle(1372, 296, 43, 58);

		// Token: 0x0400005A RID: 90
		private static readonly Rectangle c_tip_trade = new Rectangle(1416, 296, 43, 58);

		// Token: 0x0400005B RID: 91
		private static readonly Rectangle c_tip_realshop = new Rectangle(1460, 296, 43, 58);

		// Token: 0x0400005C RID: 92
		private static readonly Rectangle c_tip_guildHq = new Rectangle(1504, 296, 43, 58);

		// Token: 0x0400005D RID: 93
		private static readonly Rectangle c_notifyMarker = new Rectangle(1412, 218, 16, 16);

		// Token: 0x0400005E RID: 94
		private static readonly Rectangle c_notifyMarker2 = new Rectangle(1429, 218, 16, 16);

		// Token: 0x0400005F RID: 95
		private Tlist<PortVisualScene.TIPRenderer> {16313};

		// Token: 0x04000060 RID: 96
		private PortVisualScene.TIPRenderer {16314};

		// Token: 0x04000061 RID: 97
		private Tlist<ModelTransformedScene> {16315};

		// Token: 0x04000062 RID: 98
		private Tlist<Transform3D> {16316};

		// Token: 0x04000063 RID: 99
		private Tlist<ModelTransformedScene> {16317};

		// Token: 0x04000064 RID: 100
		private ModelTransformedScene {16318};

		// Token: 0x04000065 RID: 101
		private SoftTrigger {16319} = new SoftTrigger(0f, 1f, 0.4f);

		// Token: 0x04000066 RID: 102
		private PaternConnector2<Shipway, ModelTransformedScene> {16320};

		// Token: 0x04000067 RID: 103
		private PointLight {16321};

		// Token: 0x04000068 RID: 104
		private int {16322};

		// Token: 0x04000069 RID: 105
		private Ship {16323};

		// Token: 0x0400006A RID: 106
		private ModelTransformedScene {16324};

		// Token: 0x0400006B RID: 107
		private SoftTrigger {16325} = new SoftTrigger(0f, 1f, 2f);

		// Token: 0x0400006C RID: 108
		[CompilerGenerated]
		private bool {16326};

		// Token: 0x0200001C RID: 28
		private class TIPRenderer
		{
			// Token: 0x060000A2 RID: 162 RVA: 0x00006840 File Offset: 0x00004A40
			public TIPRenderer(object {16334}, Func<Vector3?> {16335}, Func<string> {16336}, Func<bool> {16337}, Func<bool> {16338}, Action {16339}, Rectangle {16340})
			{
				this.id = {16334};
				this.globalPos = {16335};
				this.text = {16336};
				this.font = Fonts.Philosopher_14;
				this.last2DPos = new Vector2(0f, -500f);
				this.onClick = {16339};
				this.isActive = {16337};
				this.isVisible = {16338};
				this.mainIcon = {16340};
			}

			// Token: 0x060000A3 RID: 163 RVA: 0x000068A8 File Offset: 0x00004AA8
			public void Update(ref PortVisualScene.TIPRenderer {16341}, ref float {16342})
			{
				Vector3? vector = this.globalPos();
				if (vector != null)
				{
					Camera camera = Engine.GS.Camera;
					Vector3 value = vector.Value;
					if (camera.IsVisible(value, 3f) && this.isVisible())
					{
						Vector3 value2 = vector.Value;
						this.last2DPos = Engine.GS.Camera.GetProjectionSmoothed(ref value2);
						bool flag = !InputHelper.NowMouseState.RightPressed && Engine.GS.MouseToUI.X > this.last2DPos.X - 60f && Engine.GS.MouseToUI.X < this.last2DPos.X + 60f && Engine.GS.MouseToUI.Y > this.last2DPos.Y - 40f && Engine.GS.MouseToUI.Y < this.last2DPos.Y + 80f;
						float num = Vector2.Distance(Engine.GS.MouseToUI, this.last2DPos);
						if (flag && num < {16342})
						{
							{16341} = this;
							{16342} = num;
						}
						return;
					}
				}
				this.last2DPos = new Vector2(0f, -500f);
			}

			// Token: 0x060000A4 RID: 164 RVA: 0x000069EC File Offset: 0x00004BEC
			public void RendeHighlight(Device {16343})
			{
				if (!this.isVisible())
				{
					return;
				}
				Rectangle rectangle = new Rectangle(641, 89, 81, 74);
				Vector2 vector = new Vector2(this.last2DPos.X - (float)(rectangle.Width / 2), this.last2DPos.Y);
				Color color = Color.White * 0.5f;
				{16343}.Draw(rectangle, vector, color);
			}

			// Token: 0x060000A5 RID: 165 RVA: 0x00006A5C File Offset: 0x00004C5C
			private void {16344}(float {16345}, bool {16346})
			{
				string text = this.text();
				Vector2 vector = this.font.Measure(text);
				Engine.GS.SetFont(this.font);
				Vector2 value = new Vector2(this.last2DPos.X - vector.X / 2f, this.last2DPos.Y - vector.Y / 2f - 20f);
				if (text.Length > 0)
				{
					Device gs = Engine.GS;
					Rectangle rectangle = new Marker(ref value, ref vector).Border(12f, 8f).ToRect();
					Color color = Color.White * 0.9f * {16345};
					gs.Draw(PortVisualScene.TIPRenderer.c_back, rectangle, color);
					Device gs2 = Engine.GS;
					string {14632} = text;
					Vector2 vector2 = value + new Vector2(1f, -1f);
					Color value2;
					if (!{16346})
					{
						object obj = this.id;
						value2 = ((obj is PortTipConnection && (PortTipConnection)obj == PortTipConnection.QuestUnit) ? Color.Lerp(new Color(229, 194, 90), Color.Gold, 0.5f + 0.5f * (float)Math.Sin(Global.Game.GameTotalTimeSec * 8.0)) : new Color(229, 194, 90));
					}
					else
					{
						value2 = Color.Gold;
					}
					color = value2 * MathF.Sqrt({16345});
					gs2.DrawStringFloat({14632}, vector2, color);
				}
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x00006BD8 File Offset: 0x00004DD8
			private void {16347}(string {16348}, float {16349}, bool {16350} = false)
			{
				Vector2 vector = this.last2DPos;
				if ({16350})
				{
					vector.Y -= 50f;
				}
				Engine.GS.SetFont(Fonts.Philosopher_14Bold);
				Device gs = Engine.GS;
				Rectangle rectangle = new Rectangle(1240, 355, 202, 32);
				Vector2 vector2 = new Vector2(vector.X - 101f, vector.Y - 16f);
				Color color = Color.White * {16349};
				gs.Draw(rectangle, vector2, color);
				Device gs2 = Engine.GS;
				color = new Color(29, 21, 10) * {16349};
				gs2.DrawStringCentered({16348}, vector, color);
			}

			// Token: 0x060000A7 RID: 167 RVA: 0x00006C80 File Offset: 0x00004E80
			public void Render(Device {16351}, bool {16352})
			{
				float opacity = Global.Game.GetInterfaceManager.Host.Opacity;
				float num = this.isActive() ? 1f : 0.3f;
				num *= opacity;
				Shipway shipway = this.id as Shipway;
				if (shipway != null)
				{
					this.{16347}((shipway.TimeToFinishSec == 0f) ? Local.shipway_is_ready2 : StringHelper.TimeMMMSS((double)shipway.TimeToFinishSec), num, false);
					return;
				}
				object obj = this.id;
				Vector2 vector;
				Color color;
				if (!(obj is PersonalIsleStatus.InternalBuilding))
				{
					this.{16344}(num, {16352});
					vector = this.last2DPos + new Vector2((float)(-(float)this.mainIcon.Width / 2), 0f);
					color = Color.White * num;
					{16351}.Draw(this.mainIcon, vector, color);
					obj = this.id;
					if (obj is PortTipConnection && (PortTipConnection)obj == PortTipConnection.RealShop)
					{
						if (Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.PDiscountOnShopItems) > 0f)
						{
							vector = this.last2DPos + new Vector2(0f, -39f) - PortVisualScene.c_notifyMarker.HalfWidthHeight();
							color = Color.White * num;
							{16351}.Draw(PortVisualScene.c_notifyMarker, vector, color);
						}
						else if (Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.PDiscountOnShopPremium) > 0f)
						{
							vector = this.last2DPos + new Vector2(0f, -39f) - PortVisualScene.c_notifyMarker2.HalfWidthHeight();
							color = Color.White * num;
							{16351}.Draw(PortVisualScene.c_notifyMarker2, vector, color);
						}
						else if (Session.EventActionsPipeline.EventCategoryAmount(EActionCaterory.PDiscountOnShopChest) > 0f)
						{
							vector = this.last2DPos + new Vector2(0f, -39f) - PortVisualScene.c_notifyMarker2.HalfWidthHeight();
							color = Color.White * num;
							{16351}.Draw(PortVisualScene.c_notifyMarker2, vector, color);
						}
					}
					obj = this.id;
					if (obj is PortTipConnection && (PortTipConnection)obj == PortTipConnection.Hq && Session.Guild != null && Session.GuildUnreceivedSalary != 0 && Session.Game.NearPortRelation == Relation.Ally)
					{
						this.{16347}(Local.guild_salary_take, num, true);
					}
					return;
				}
				PersonalIsleStatus.InternalBuilding item = (PersonalIsleStatus.InternalBuilding)obj;
				PersonalIsleStatus currentPersonalIsle = Global.Game.ScenePort.CurrentPersonalIsle;
				Engine.GS.SetFont(this.font);
				string text = this.text();
				Vector2 value = this.font.Measure(text);
				Vector2 vector2 = this.last2DPos - value * 0.5f;
				Vector2 value2 = this.last2DPos;
				Geometry.IntVector(ref value2);
				if (currentPersonalIsle.InternalBuildings.Contains(item))
				{
					this.{16344}(opacity, {16352});
					return;
				}
				Color wheat = Color.Wheat;
				Device gs = Engine.GS;
				Rectangle rectangle = new Marker(ref vector2, ref value).Border(12f, 12f).ToRect();
				color = Color.White * 0.9f * opacity;
				gs.Draw(PortVisualScene.TIPRenderer.c_pi_back_unbuild, rectangle, color);
				Device gs2 = Engine.GS;
				string {14610} = text;
				vector = value2 + new Vector2(0f, 5f);
				color = ({16352} ? Color.Gold : wheat) * opacity;
				gs2.DrawStringCentered({14610}, vector, color);
				Engine.GS.SetFont(Fonts.Arial_10);
				Device gs3 = Engine.GS;
				string build = Local.Build;
				vector = value2 - new Vector2(0f, 10f);
				color = ({16352} ? Color.Gold : wheat) * opacity;
				gs3.DrawStringCentered(build, vector, color);
			}

			// Token: 0x0400006D RID: 109
			public Func<Vector3?> globalPos;

			// Token: 0x0400006E RID: 110
			public Vector2 last2DPos;

			// Token: 0x0400006F RID: 111
			public Func<string> text;

			// Token: 0x04000070 RID: 112
			public Func<bool> isActive;

			// Token: 0x04000071 RID: 113
			public Func<bool> isVisible;

			// Token: 0x04000072 RID: 114
			public CustomSpriteFont font;

			// Token: 0x04000073 RID: 115
			public object id;

			// Token: 0x04000074 RID: 116
			public Action onClick;

			// Token: 0x04000075 RID: 117
			public Rectangle mainIcon;

			// Token: 0x04000076 RID: 118
			private static readonly Rectangle c_back = new Rectangle(165, 77, 290, 37);

			// Token: 0x04000077 RID: 119
			private static readonly Rectangle c_pi_back_build = new Rectangle(941, 106, 131, 37);

			// Token: 0x04000078 RID: 120
			private static readonly Rectangle c_pi_back_unbuild = new Rectangle(941, 144, 131, 37);
		}

		// Token: 0x0200001D RID: 29
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000079 RID: 121
			public static Action <0>__OpenPubWindow;

			// Token: 0x0400007A RID: 122
			public static Action <1>__OpenFactoryWindow;

			// Token: 0x0400007B RID: 123
			public static Action <2>__OpenDokWindow;

			// Token: 0x0400007C RID: 124
			public static Action <3>__OpenWorkshopWindow;
		}
	}
}
