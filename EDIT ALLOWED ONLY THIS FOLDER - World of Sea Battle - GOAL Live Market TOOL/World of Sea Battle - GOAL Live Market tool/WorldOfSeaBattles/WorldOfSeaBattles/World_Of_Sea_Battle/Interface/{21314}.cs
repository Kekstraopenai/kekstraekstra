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
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000303 RID: 771
	public sealed class {21314}
	{
		// Token: 0x060010F3 RID: 4339 RVA: 0x0008DFC1 File Offset: 0x0008C1C1
		public static void Create(StackForm {21315})
		{
			{21314}.CheckEducationFlags();
			{21314}.Compose({21315});
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0008DFD0 File Offset: 0x0008C1D0
		private static void CheckEducationFlags()
		{
			if (Session.Account.EducationQuest[EducationOnboarding.BuildNextShip] && !Session.Account.EducationQuest[EducationOnboarding.CraftCannon] && !Session.Account.CannonsAtStorage.IsEmpty)
			{
				if (!Session.Account.EducationQuest[EducationOnboarding.GameTT_CountOfNeedCannons])
				{
					Session.Account.Gold = Math.Max(200, Session.Account.Gold);
				}
				EducationHelper.MakeFlag(EducationOnboarding.GameTT_CountOfNeedCannons, true);
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0008E050 File Offset: 0x0008C250
		private static void Compose(StackForm {21316})
		{
			{21314}.<>c__DisplayClass4_0 CS$<>8__locals1 = new {21314}.<>c__DisplayClass4_0();
			CS$<>8__locals1.stack = {21316};
			CS$<>8__locals1.stack.SortMode = UiOrientation.ExpansiveSize;
			CS$<>8__locals1.stack.Clear();
			{21294} {21294} = new {21294}(new Marker(0f, 0f, 957f, 35f), 5f, new Button[]
			{
				CS$<>8__locals1.<Compose>g__CreateTitleButton|0(0, Local.cannons_page_light),
				CS$<>8__locals1.<Compose>g__CreateTitleButton|0(1, Local.cannons_page_medium),
				CS$<>8__locals1.<Compose>g__CreateTitleButton|0(2, Local.cannons_page_heavy),
				CS$<>8__locals1.<Compose>g__CreateTitleButton|0(3, Local.cannons_page_mortar)
			});
			CS$<>8__locals1.stack.AddItem(new UiControl[]
			{
				{21294}
			});
			StackForm stack = CS$<>8__locals1.stack;
			Form form;
			switch ({21314}.currentIndex)
			{
			case 0:
				form = {21314}.CreatePage(CannonCategory.Light);
				break;
			case 1:
				form = {21314}.CreatePage(CannonCategory.Medium);
				break;
			case 2:
				form = {21314}.CreatePage(CannonCategory.Heavy);
				break;
			case 3:
				form = {21314}.CreatePage(CannonCategory.Mortar);
				break;
			default:
				throw new NotImplementedException();
			}
			stack.AddItem(new UiControl[]
			{
				form
			});
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0008E160 File Offset: 0x0008C360
		private static Form CreatePage(CannonCategory {21317})
		{
			{21314}.<>c__DisplayClass5_0 CS$<>8__locals1;
			CS$<>8__locals1.category = {21317};
			CannonGameInfo[] cannons = {21314}.GetCannons(CS$<>8__locals1.category);
			CS$<>8__locals1.stack = new StackForm(new Vector2(0f, 35f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if (!Global.Player.UsedShipPlayer.CraftFrom.IsCannonsAllowed(CS$<>8__locals1.category) || (CS$<>8__locals1.category == CannonCategory.Mortar && !Global.Player.UsedShipPlayer.Mortars.Any<CannonGameInstance>()))
			{
				Form form = new Form(new Marker(0f, 0f, 957f, 30f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				Label label = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Orange, Local.cannons_page_lowrank, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.AddChildPos(label.CenterX(), PositionAlignment.Center, PositionAlignment.Center, 0f);
				CS$<>8__locals1.stack.AddItem(new UiControl[]
				{
					form
				});
			}
			{21314}.<CreatePage>g__AddCategory|5_0(Local.cannons_subcat_LiteCannon, from {21325} in cannons
			where {21325}.Class == CannonClass.LiteCannon
			select {21325}, ref CS$<>8__locals1);
			{21314}.<CreatePage>g__AddCategory|5_0(Local.cannons_subcat_DistanceCannon, from {21326} in cannons
			where {21326}.Class == CannonClass.DistanceCannon
			select {21326}, ref CS$<>8__locals1);
			{21314}.<CreatePage>g__AddCategory|5_0(Local.cannons_subcat_HeavyCannon, from {21327} in cannons
			where {21327}.Class == CannonClass.HeavyCannon
			select {21327}, ref CS$<>8__locals1);
			{21314}.<CreatePage>g__AddCategory|5_0(Local.cannons_subcat_Bombardier, (from {21328} in cannons
			where {21328}.Class == CannonClass.Bombardier
			select {21328}).OrderBy(delegate(CannonGameInfo {21329})
			{
				if ({21329}.Feature != CannonFeature.DoubleShot)
				{
					return 0;
				}
				return -10;
			}), ref CS$<>8__locals1);
			{21314}.<CreatePage>g__AddCategory|5_0(Local.cannons_subcat_Other, from {21330} in cannons
			where {21330}.Class == CannonClass.Special
			select {21330}, ref CS$<>8__locals1);
			if (CS$<>8__locals1.category == CannonCategory.Mortar)
			{
				{21314}.<CreatePage>g__AddCategory|5_0(Local.cannons_subcat_Mortar, from {21331} in cannons
				where {21331}.Feature == CannonFeature.Default
				select {21331} into {21332}
				orderby (int)(({21332}.ID + 1) % 2)
				select {21332}, ref CS$<>8__locals1);
				{21314}.<CreatePage>g__AddCategory|5_0(Local.cannons_subcat_Other, from {21333} in cannons
				where {21333}.Feature > CannonFeature.Default
				select {21333}, ref CS$<>8__locals1);
			}
			Form form2 = new Form(CS$<>8__locals1.stack.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form2.AnimatedFocus = false;
			form2.PosHeight = 870f;
			form2.AddChild(CS$<>8__locals1.stack);
			return form2;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0008E428 File Offset: 0x0008C628
		private static CannonGameInfo[] GetCannons(CannonCategory {21318})
		{
			Tlist<CannonGameInfo> tlist = new Tlist<CannonGameInfo>();
			foreach (CannonGameInfo cannonGameInfo in ((IEnumerable<CannonGameInfo>)Gameplay.CannonsGameInfo))
			{
				if (!cannonGameInfo.IsRemoved && cannonGameInfo.Category == {21318})
				{
					tlist.Add(cannonGameInfo);
				}
			}
			tlist.SortTop(delegate(CannonGameInfo {21334})
			{
				if ({21334}.Class == CannonClass.Mortar)
				{
					return (int)(-(int){21334}.ID);
				}
				return -{21334}.CostAsGold.Value;
			});
			return tlist.ToArray();
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x0008E4D8 File Offset: 0x0008C6D8
		[CompilerGenerated]
		internal static void <CreatePage>g__AddCategory|5_0(string {21319}, IEnumerable<CannonGameInfo> {21320}, ref {21314}.<>c__DisplayClass5_0 {21321})
		{
			if (!{21320}.Any<CannonGameInfo>())
			{
				return;
			}
			Form form = new Form(new Marker(0f, 0f, 957f, 24f), {21314}.titleBack, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Gray,
				AnimatedFocus = false
			};
			form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat, {21319}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.Center, 0f);
			{21321}.stack.AddItem(new UiControl[]
			{
				form
			});
			if ({21321}.category == CannonCategory.Mortar)
			{
				CannonGameInfo[] array = {21320}.ToArray<CannonGameInfo>();
				for (int i = 0; i < array.Length; i += 3)
				{
					StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					stackForm.AddItem(new UiControl[]
					{
						new {21302}((array.Length > i) ? array[i] : null)
					});
					stackForm.AddItem(new UiControl[]
					{
						new {21302}((array.Length > i + 1) ? array[i + 1] : null)
					});
					stackForm.AddItem(new UiControl[]
					{
						new {21302}((array.Length > i + 2) ? array[i + 2] : null)
					});
					{21321}.stack.AddItem(new UiControl[]
					{
						stackForm
					});
				}
				return;
			}
			CannonGameInfo[] array2 = (from {21322} in {21320}
			where {21322}.Material == CannonMaterial.CastIron
			select {21322}).ToArray<CannonGameInfo>();
			CannonGameInfo[] array3 = (from {21323} in {21320}
			where {21323}.Material == CannonMaterial.Iron
			select {21323}).ToArray<CannonGameInfo>();
			CannonGameInfo[] array4 = (from {21324} in {21320}
			where {21324}.Material == CannonMaterial.Bronze
			select {21324}).ToArray<CannonGameInfo>();
			int num = Math.Max(array2.Length, Math.Max(array3.Length, array4.Length));
			for (int j = 0; j < num; j++)
			{
				StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				stackForm2.AddItem(new UiControl[]
				{
					new {21302}((array2.Length > j) ? array2[j] : null)
				});
				stackForm2.AddItem(new UiControl[]
				{
					new {21302}((array3.Length > j) ? array3[j] : null)
				});
				stackForm2.AddItem(new UiControl[]
				{
					new {21302}((array4.Length > j) ? array4[j] : null)
				});
				{21321}.stack.AddItem(new UiControl[]
				{
					stackForm2
				});
			}
		}

		// Token: 0x04000F7C RID: 3964
		private static Rectangle titleBack = new Rectangle(3062, 105, 625, 16);

		// Token: 0x04000F7D RID: 3965
		private static int currentIndex = 0;
	}
}
