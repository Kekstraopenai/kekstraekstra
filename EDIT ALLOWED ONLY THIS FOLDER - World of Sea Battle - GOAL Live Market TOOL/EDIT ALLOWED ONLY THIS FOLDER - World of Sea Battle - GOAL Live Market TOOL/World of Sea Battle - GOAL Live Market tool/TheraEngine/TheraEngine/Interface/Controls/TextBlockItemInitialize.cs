using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000B8 RID: 184
	public class TextBlockItemInitialize
	{
		// Token: 0x060004C1 RID: 1217 RVA: 0x000186E0 File Offset: 0x000168E0
		public TextBlockItemInitialize(string {13547}, Color {13548}, bool {13549} = false)
		{
			this.Text = {13547};
			this.Color = {13548};
			this.SingleLine = {13549};
		}

		// Token: 0x0400039B RID: 923
		public string Text;

		// Token: 0x0400039C RID: 924
		public Color Color;

		// Token: 0x0400039D RID: 925
		public bool SingleLine;
	}
}
