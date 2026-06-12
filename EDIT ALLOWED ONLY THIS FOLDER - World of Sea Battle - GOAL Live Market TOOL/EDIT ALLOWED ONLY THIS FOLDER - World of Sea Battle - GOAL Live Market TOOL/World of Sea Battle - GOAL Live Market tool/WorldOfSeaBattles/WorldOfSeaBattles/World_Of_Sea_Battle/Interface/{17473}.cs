using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000A5 RID: 165
	internal sealed class {17473} : {17507}
	{
		// Token: 0x06000438 RID: 1080 RVA: 0x00022DDD File Offset: 0x00020FDD
		public {17473}(Action<object> {17476}, params {17473}.Item[] {17477}) : base({17473}.GetButtons({17476}, {17477}).ToArray<Button>())
		{
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00022DF1 File Offset: 0x00020FF1
		private static IEnumerable<Button> GetButtons(Action<object> {17478}, {17473}.Item[] {17479})
		{
			{17473}.<GetButtons>d__3 <GetButtons>d__ = new {17473}.<GetButtons>d__3(-2);
			<GetButtons>d__.<>3__pick = {17478};
			<GetButtons>d__.<>3__options = {17479};
			return <GetButtons>d__;
		}

		// Token: 0x0400039C RID: 924
		private static readonly Color textCol = new Color(204, 189, 189);

		// Token: 0x020000A6 RID: 166
		public struct Item
		{
			// Token: 0x0600043B RID: 1083 RVA: 0x00022E23 File Offset: 0x00021023
			public Item(object {17486}, string {17487}, bool {17488} = true, ImageDecription {17489} = default(ImageDecription), ToolTipState {17490} = null, Action {17491} = null)
			{
				this.Tag = {17486};
				this.Text = {17487};
				this.Enable = {17488};
				this.UseIcon = {17489};
				this.ToolTip = {17490};
				this.OwnClickHandler = {17491};
			}

			// Token: 0x0400039D RID: 925
			public object Tag;

			// Token: 0x0400039E RID: 926
			public string Text;

			// Token: 0x0400039F RID: 927
			public bool Enable;

			// Token: 0x040003A0 RID: 928
			public ImageDecription UseIcon;

			// Token: 0x040003A1 RID: 929
			public ToolTipState ToolTip;

			// Token: 0x040003A2 RID: 930
			public Action OwnClickHandler;
		}
	}
}
