using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Game;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000B6 RID: 182
	internal sealed class {17558} : {17507}
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x00024607 File Offset: 0x00022807
		public {17558}({17549} {17560}) : base({17558}.GetButtons({17560}).ToArray<Button>())
		{
			this.{17566} = {17560};
			Session.EvGroupChanged += this.{17563};
			base.EvRemoveFromContainer += this.{17565};
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00024644 File Offset: 0x00022844
		private static IEnumerable<Button> GetButtons({17549} {17561})
		{
			{17558}.<GetButtons>d__2 <GetButtons>d__ = new {17558}.<GetButtons>d__2(-2);
			<GetButtons>d__.<>3__cache = {17561};
			return <GetButtons>d__;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00024654 File Offset: 0x00022854
		private void {17562}()
		{
			base.ClearAllChild();
			base.Load({17558}.GetButtons(this.{17566}).ToArray<Button>());
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00024654 File Offset: 0x00022854
		private void {17563}(GroupCommon {17564})
		{
			base.ClearAllChild();
			base.Load({17558}.GetButtons(this.{17566}).ToArray<Button>());
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x00024682 File Offset: 0x00022882
		[CompilerGenerated]
		private void {17565}()
		{
			Session.EvGroupChanged -= this.{17563};
		}

		// Token: 0x040003E2 RID: 994
		private {17549} {17566};
	}
}
