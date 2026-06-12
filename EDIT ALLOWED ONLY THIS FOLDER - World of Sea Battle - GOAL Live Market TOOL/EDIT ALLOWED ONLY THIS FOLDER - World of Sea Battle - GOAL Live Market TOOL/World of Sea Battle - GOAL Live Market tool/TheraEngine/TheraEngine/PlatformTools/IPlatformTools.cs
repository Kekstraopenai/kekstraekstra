using System;
using Microsoft.Xna.Framework.Input;

namespace TheraEngine.PlatformTools
{
	// Token: 0x02000070 RID: 112
	public interface IPlatformTools
	{
		// Token: 0x060002C3 RID: 707
		void Configure();

		// Token: 0x060002C4 RID: 708
		string GetInputLayoutLanguage();

		// Token: 0x060002C5 RID: 709
		byte[] MatchFingerprint();

		// Token: 0x060002C6 RID: 710
		string OpenFileDialog(string {12463} = null, string {12464} = null);

		// Token: 0x060002C7 RID: 711
		string MapKeyboardKey(Keys {12465});
	}
}
