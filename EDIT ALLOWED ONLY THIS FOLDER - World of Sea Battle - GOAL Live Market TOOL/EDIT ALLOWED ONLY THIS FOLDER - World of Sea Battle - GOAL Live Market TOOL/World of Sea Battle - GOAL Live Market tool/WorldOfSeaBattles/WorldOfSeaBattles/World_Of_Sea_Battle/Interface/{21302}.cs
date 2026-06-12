using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000300 RID: 768
	public class {21302} : Form
	{
		// Token: 0x060010E7 RID: 4327 RVA: 0x0008D7B2 File Offset: 0x0008B9B2
		public {21302}(CannonGameInfo {21304}) : base(new Marker(0f, 0f, 319f, 103f), {21302}.back, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.{21307} = {21304};
			this.AnimatedFocus = false;
			this.{21305}();
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0008D7F0 File Offset: 0x0008B9F0
		private void {21305}()
		{
			if (this.{21307} == null)
			{
				this.TexturePath = {21302}.emptyBack;
				return;
			}
			bool disallowByWorkshop = this.{21307}.NeedsFactoryToCraft && !Session.Account.ResourcesInPorts.IsFactoryOpened(Global.Player.NearPort.PortID, FactoryType.PortTopWeapons);
			bool flag = EducationHelper.MakeInvisibleSomeCraftsAndWeapons && this.{21307}.CraftingType > ShipCannonCrafting.ByGold;
			base.AddChild(new Image(new Marker(5f, 7f, 89f, 89f), this.{21307}.IconTexture, this.{21307}.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			base.AddChild(new Label(new Vector2(104f, 15f), Fonts.Philosopher_14, {21338}.TextColor * 0.8f, this.{21307}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			Vector2 {13189} = new Vector2(272f, 52f);
			if (this.{21307}.CraftingType == ShipCannonCrafting.NotAvailable)
			{
				base.AddChild(new Form({13189}, {21302}.starIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
				{13189}.X -= 30f;
			}
			if (Global.Player.UsedShip.Cannons.Items.Any((CannonCommon {21308}) => {21308}.GameInfo.ID == this.{21307}.ID))
			{
				base.AddChild(new Form({13189}, {21302}.existingIcon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				});
			}
			Color countColor = new Color(149, 138, 124);
			LiveLabel inStorage = null;
			base.AddChild(inStorage = new LiveLabel(new Vector2(105f, 53f), Fonts.Arial_9, countColor, this.{21307}, delegate(object {21309})
			{
				CannonGameInfo info = (CannonGameInfo){21309};
				int num2 = Session.Account.UsedMortarsAtStorage.Count((CannonGameInstance {21313}) => (short){21313}.InfoID == info.ID) + Session.Account.CannonsAtStorage[(int)info.ID];
				if (inStorage != null)
				{
					inStorage.BasicColor = countColor * ((num2 > 0) ? 1f : 0.7f);
				}
				return Local.storage_d + num2.ToString();
			}, 100));
			int num = 0;
			foreach (PlayerShipDynamicInfo playerShipDynamicInfo in Session.Account.Shipyard.List)
			{
				using (IEnumerator<CannonCommon> enumerator2 = ((IEnumerable<CannonCommon>)playerShipDynamicInfo.Cannons.Items).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						if (enumerator2.Current.GameInfo == this.{21307})
						{
							num++;
						}
					}
				}
				using (IEnumerator<CannonGameInstance> enumerator3 = playerShipDynamicInfo.Mortars.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						if (enumerator3.Current.Info == this.{21307})
						{
							num++;
						}
					}
				}
			}
			Vector2 {13342} = new Vector2(105f, 67f);
			CustomSpriteFont arial_ = Fonts.Arial_9;
			Color {13344} = countColor * ((num > 0) ? 1f : 0.7f);
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(2, 2);
			defaultInterpolatedStringHandler.AppendFormatted(Local.ProfileWatchWindow_2);
			defaultInterpolatedStringHandler.AppendLiteral(": ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(num);
			base.AddChild(new Label({13342}, arial_, {13344}, defaultInterpolatedStringHandler.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			if (flag)
			{
				base.Opacity = 0.5f;
			}
			Action<TextBlockBuilder> <>9__4;
			Action<ValueTuple<int, int>> <>9__5;
			base.EvClick += delegate(ClickUiEventArgs {21310})
			{
				CraftingRecipe craftingRecipe;
				if (this.{21307}.CraftingType != ShipCannonCrafting.NotAvailable)
				{
					(craftingRecipe = new CraftingRecipe(this.{21307}.Craft.InputItems, this.{21307}.Craft.InputMoney)).ReduceCraftCost = {21302}.CalculateReduceCraftCost(this.{21307});
				}
				else
				{
					craftingRecipe = null;
				}
				CraftingRecipe craftingRecipe2 = craftingRecipe;
				if ({17177}.CurrentInstance == null)
				{
					new {17177}(true);
				}
				{17177}.CurrentInstance.CleanTabs();
				{17177} currentInstance = {17177}.CurrentInstance;
				string name = this.{21307}.Name;
				bool {17191} = false;
				string {17192} = "";
				Action<TextBlockBuilder> {17193};
				if (({17193} = <>9__4) == null)
				{
					{17193} = (<>9__4 = delegate(TextBlockBuilder {21311})
					{
						this.{21307}.ToolTip({21311}, false);
					});
				}
				CraftingRecipe {17194} = craftingRecipe2;
				RTIf {17195} = 0f;
				int {17196} = 1;
				bool {17197} = false;
				Action<ValueTuple<int, int>> {17198};
				if (({17198} = <>9__5) == null)
				{
					{17198} = (<>9__5 = delegate([TupleElementNames(new string[]
					{
						"resCount",
						"btIndex"
					})] ValueTuple<int, int> {21312})
					{
						Session.Account.CannonsAtStorage.AddOrRemove((int)this.{21307}.ID, {21312}.Item1);
						EducationHelper.MakeFlag(EducationOnboarding.CraftCannon, false);
					});
				}
				bool {17199} = false;
				string {17200} = null;
				object {17201};
				if (!disallowByWorkshop)
				{
					{17201} = null;
				}
				else
				{
					({17201} = new string[1])[0] = Local.not_having_factory;
				}
				currentInstance.SetData(name, {17191}, {17192}, {17193}, {17194}, {17195}, {17196}, {17197}, {17198}, {17199}, {17200}, {17201}, 1, true, disallowByWorkshop ? 0 : int.MaxValue, false, -1f);
				if (craftingRecipe2 == null)
				{
					{17177}.CurrentInstance.AddDownText(Local.PortWorkshopPage_17);
				}
			};
			{20431}.CannonToolTIp(this.{21307}, this, false, false, null);
			if (Global.Player.DebugEnabled)
			{
				TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Arial_10, -1f);
				textBlockBuilder.WriteLine(this.{21307}.MaxDistance.ToString(), Color.Yellow);
				textBlockBuilder.WriteLine(this.{21307}.Penetration.ToString(), Color.OrangeRed);
				textBlockBuilder.WriteLine(Local.cannon_reload_time(this.{21307}.ReloadTime / 1000f), Color.Cyan);
				textBlockBuilder.WriteLine(Local.cannon_max_axis((int)MathHelper.ToDegrees(this.{21307}.MaxAxis)), Color.Gray);
				base.AddChild(textBlockBuilder.Create(base.Pos.XY + new Vector2(248f, 35f)));
				TextBlockBuilder textBlockBuilder2 = new TextBlockBuilder(Fonts.Philosopher_14Bold, -1f);
				TextBlockBuilder textBlockBuilder3 = textBlockBuilder2;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 1);
				defaultInterpolatedStringHandler2.AppendFormatted<int>((int)((this.{21307}.Penetration - 2f) * 60f / (this.{21307}.ReloadTime / 1000f)));
				defaultInterpolatedStringHandler2.AppendLiteral(" /s");
				textBlockBuilder3.WriteLine(defaultInterpolatedStringHandler2.ToStringAndClear(), Color.SlateBlue);
				base.AddChild(textBlockBuilder2.Create(base.Pos.XY + new Vector2(188f, 55f)));
			}
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0008DCD0 File Offset: 0x0008BED0
		private static float CalculateReduceCraftCost(CannonGameInfo {21306})
		{
			float num = (float)Session.Account.CaptainSkills[PDynamicAccountBonus.PReduceTopWeaponsCraftCost] / 100f;
			return ({21306}.NeedsFactoryToCraft ? num : 0f) + Session.Game.WeaponCraftDiscount;
		}

		// Token: 0x04000F70 RID: 3952
		private static Rectangle existingIcon = new Rectangle(2662, 0, 27, 27);

		// Token: 0x04000F71 RID: 3953
		private static Rectangle starIcon = new Rectangle(2690, 0, 27, 27);

		// Token: 0x04000F72 RID: 3954
		private static Rectangle emptyBack = new Rectangle(3284, 161, 343, 111);

		// Token: 0x04000F73 RID: 3955
		private static Rectangle back = new Rectangle(3284, 273, 343, 111);

		// Token: 0x04000F74 RID: 3956
		private readonly CannonGameInfo {21307};
	}
}
