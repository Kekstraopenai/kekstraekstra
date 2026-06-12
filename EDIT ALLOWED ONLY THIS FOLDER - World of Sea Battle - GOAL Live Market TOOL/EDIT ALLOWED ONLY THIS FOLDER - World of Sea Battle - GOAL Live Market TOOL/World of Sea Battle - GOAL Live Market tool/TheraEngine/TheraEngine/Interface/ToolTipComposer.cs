using System;

namespace TheraEngine.Interface
{
	// Token: 0x0200007B RID: 123
	public class ToolTipComposer : Composer
	{
		// Token: 0x060002E3 RID: 739 RVA: 0x00010CDE File Offset: 0x0000EEDE
		public ToolTipComposer() : base(350f, 1f)
		{
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00010CF0 File Offset: 0x0000EEF0
		public ToolTipComposer(string {12511}, string {12512}, ComposerTextStyle {12513}) : this()
		{
			if (!string.IsNullOrEmpty({12511}))
			{
				base.AddHeader({12511}, null);
			}
			if (!string.IsNullOrEmpty({12512}))
			{
				base.AddText({12512}, {12513}, true);
			}
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00010D2E File Offset: 0x0000EF2E
		public ToolTipState CreateToolTip()
		{
			return new ToolTipState(this);
		}
	}
}
