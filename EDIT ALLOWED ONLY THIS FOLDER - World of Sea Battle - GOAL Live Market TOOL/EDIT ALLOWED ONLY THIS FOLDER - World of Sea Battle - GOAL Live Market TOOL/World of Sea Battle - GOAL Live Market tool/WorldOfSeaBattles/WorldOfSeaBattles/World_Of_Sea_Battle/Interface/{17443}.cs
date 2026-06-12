using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000A3 RID: 163
	public struct {17443}
	{
		// Token: 0x06000434 RID: 1076 RVA: 0x00022D2C File Offset: 0x00020F2C
		public {17443}(string {17454}, string {17455}, Rectangle {17456}, bool {17457} = false, float {17458} = 0f)
		{
			this.Text = {17454};
			this.Description = {17455};
			this.Icon = {17456};
			this.Disallow = {17457};
			this.AutoTimeoutSec = {17458};
			this.IconTex = null;
			this.AddKeyboardKey = true;
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00022D61 File Offset: 0x00020F61
		public {17443}(string {17459}, string {17460}, Texture2D {17461}, bool {17462} = false, float {17463} = 0f)
		{
			this.Text = {17459};
			this.Description = {17460};
			this.Icon = default(Rectangle);
			this.Disallow = {17462};
			this.AutoTimeoutSec = {17463};
			this.IconTex = {17461};
			this.AddKeyboardKey = true;
		}

		// Token: 0x04000392 RID: 914
		public string Text;

		// Token: 0x04000393 RID: 915
		public string Description;

		// Token: 0x04000394 RID: 916
		public Rectangle Icon;

		// Token: 0x04000395 RID: 917
		public Texture2D IconTex;

		// Token: 0x04000396 RID: 918
		public bool Disallow;

		// Token: 0x04000397 RID: 919
		public float AutoTimeoutSec;

		// Token: 0x04000398 RID: 920
		public bool AddKeyboardKey;
	}
}
