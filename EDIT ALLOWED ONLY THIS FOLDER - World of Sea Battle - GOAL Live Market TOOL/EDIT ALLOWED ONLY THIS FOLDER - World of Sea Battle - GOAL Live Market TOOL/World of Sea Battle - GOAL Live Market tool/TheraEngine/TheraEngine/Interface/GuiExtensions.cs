using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface
{
	// Token: 0x0200007F RID: 127
	public static class GuiExtensions
	{
		// Token: 0x06000313 RID: 787 RVA: 0x000119B0 File Offset: 0x0000FBB0
		public static CheckboxControl ExCheckEvent(this CheckboxControl {12626}, Action<CheckboxCheckedEventArgs> {12627})
		{
			{12626}.EvCheck += {12627};
			return {12626};
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000119BA File Offset: 0x0000FBBA
		public static T ExClick<T>(this T {12628}, Action<ClickUiEventArgs> {12629}) where T : UiControl
		{
			{12628}.EvClick += {12629};
			return {12628};
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000119C9 File Offset: 0x0000FBC9
		public static T ExToolTip<T>(this T {12630}, ToolTip {12631}) where T : UiControl
		{
			{12630}.ToolTip = {12631};
			return {12630};
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000119D8 File Offset: 0x0000FBD8
		public static DropdownControl<TItem> ExpansionSelect<TItem>(this DropdownControl<TItem> {12632}, TItem {12633})
		{
			{12632}.SelectByValue({12633});
			return {12632};
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000119E2 File Offset: 0x0000FBE2
		public static DropdownControl<TItem> ExpansionChangeEvent<TItem>(this DropdownControl<TItem> {12634}, Action<SelItem<TItem>> {12635})
		{
			{12634}.EvChangeItem += {12635};
			return {12634};
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000119EC File Offset: 0x0000FBEC
		public static KeyInputControl ExpansionChangeKey(this KeyInputControl {12636}, Action<Keys> {12637})
		{
			{12636}.EvChangeKey += {12637};
			return {12636};
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000119F6 File Offset: 0x0000FBF6
		public static IEnumerable<UiControl> StackVerticalLine(Vector2 {12638}, params UiControl[] {12639})
		{
			GuiExtensions.<StackVerticalLine>d__6 <StackVerticalLine>d__ = new GuiExtensions.<StackVerticalLine>d__6(-2);
			<StackVerticalLine>d__.<>3__XcenterYfixed = {12638};
			<StackVerticalLine>d__.<>3__controls = {12639};
			return <StackVerticalLine>d__;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00011A0D File Offset: 0x0000FC0D
		public static IEnumerable<UiControl> StackByVerticalLineF(params UiControl[] {12640})
		{
			GuiExtensions.<StackByVerticalLineF>d__7 <StackByVerticalLineF>d__ = new GuiExtensions.<StackByVerticalLineF>d__7(-2);
			<StackByVerticalLineF>d__.<>3__controls = {12640};
			return <StackByVerticalLineF>d__;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000C282 File Offset: 0x0000A482
		public static void CreateViewbox()
		{
		}
	}
}
