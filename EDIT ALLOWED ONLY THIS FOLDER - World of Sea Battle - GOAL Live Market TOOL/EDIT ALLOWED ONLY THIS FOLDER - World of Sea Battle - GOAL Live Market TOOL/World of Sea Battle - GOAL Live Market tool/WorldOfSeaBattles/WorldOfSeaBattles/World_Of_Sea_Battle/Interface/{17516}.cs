using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Microsoft.Xna.Framework;
using TheraEngine.Interface.Controls;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000AD RID: 173
	internal sealed class {17516} : {17507}
	{
		// Token: 0x06000452 RID: 1106 RVA: 0x0002366C File Offset: 0x0002186C
		public static void TryCreate(IStorageAsset {17518})
		{
			Button[] array = {17516}.GetButtons({17518}).ToArray<Button>();
			if (array.Length == 0)
			{
				return;
			}
			new {17516}(array);
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00023691 File Offset: 0x00021891
		private {17516}(params Button[] {17519}) : base({17519})
		{
			{17516}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{17516}.CurrentInstance = null;
			};
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x000236C5 File Offset: 0x000218C5
		private static IEnumerable<Button> GetButtons(IStorageAsset {17520})
		{
			{17516}.<GetButtons>d__4 <GetButtons>d__ = new {17516}.<GetButtons>d__4(-2);
			<GetButtons>d__.<>3__res = {17520};
			return <GetButtons>d__;
		}

		// Token: 0x040003BF RID: 959
		public static {17516} CurrentInstance;

		// Token: 0x040003C0 RID: 960
		private static readonly Color textCol = new Color(204, 189, 189);
	}
}
