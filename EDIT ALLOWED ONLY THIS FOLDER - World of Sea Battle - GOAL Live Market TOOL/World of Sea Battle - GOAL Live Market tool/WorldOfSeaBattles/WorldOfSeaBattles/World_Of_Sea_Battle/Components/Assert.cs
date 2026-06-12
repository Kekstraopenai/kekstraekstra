using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Common;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x020004FE RID: 1278
	internal class Assert
	{
		// Token: 0x06001C56 RID: 7254 RVA: 0x00104880 File Offset: 0x00102A80
		public static void Report(bool {25297}, string {25298})
		{
			if ({25297})
			{
				if (Assert.transferredKeys.ContainsKey({25298}))
				{
					return;
				}
				Assert.transferredKeys.Add({25298}, 1);
				object obj = new StackTrace(true);
				StringBuilder stringBuilder = new StringBuilder();
				TlistFormatter.StackTarceToStr(obj.ToString(), stringBuilder);
				Helpers.SendError(new AssertFailException(), {25298} + stringBuilder.ToString(), false, false);
			}
		}

		// Token: 0x04001C5C RID: 7260
		private static Dictionary<string, int> transferredKeys = new Dictionary<string, int>(3000);
	}
}
