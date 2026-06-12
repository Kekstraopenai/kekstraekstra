using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace TheraEngine.Input
{
	// Token: 0x020000E5 RID: 229
	public class KeyboardInput
	{
		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001FC27 File Offset: 0x0001DE27
		internal static KeyboardInput Empty
		{
			get
			{
				return new KeyboardInput
				{
					DownKeys = new Keys[0],
					{14338} = 0
				};
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001FC41 File Offset: 0x0001DE41
		public bool IsDown(Keys {14331})
		{
			return this.IsDown(ref {14331});
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001FC4B File Offset: 0x0001DE4B
		public bool IsUp(Keys {14332})
		{
			return this.IsUp(ref {14332});
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x0001FC58 File Offset: 0x0001DE58
		public bool IsDown(ref Keys {14333})
		{
			for (int i = 0; i < this.{14338}; i++)
			{
				if (this.DownKeys[i] == {14333})
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x0001FC88 File Offset: 0x0001DE88
		public bool IsUp(ref Keys {14334})
		{
			for (int i = 0; i < this.{14338}; i++)
			{
				if (this.DownKeys[i] == {14334})
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001FCB5 File Offset: 0x0001DEB5
		internal void SetData(KeyboardState {14335})
		{
			this.DownKeys = {14335}.GetPressedKeys();
			this.{14338} = this.DownKeys.Length;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001FCD2 File Offset: 0x0001DED2
		internal void SetData(List<Keys> {14336})
		{
			this.DownKeys = {14336}.ToArray();
			this.{14338} = this.DownKeys.Length;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001FCEE File Offset: 0x0001DEEE
		internal void SetData(KeyboardInput {14337})
		{
			this.DownKeys = {14337}.DownKeys;
			this.{14338} = this.DownKeys.Length;
		}

		// Token: 0x040004A6 RID: 1190
		private int {14338};

		// Token: 0x040004A7 RID: 1191
		public Keys[] DownKeys;
	}
}
