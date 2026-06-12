using System;

namespace TheraEngine
{
	// Token: 0x02000027 RID: 39
	public struct FingerPrintF
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00008370 File Offset: 0x00006570
		public bool IsUndefined
		{
			get
			{
				return this.Bytes.Length == 0;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000837C File Offset: 0x0000657C
		public long Part1
		{
			get
			{
				return BitConverter.ToInt64(this.Bytes, 0);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600015B RID: 347 RVA: 0x0000838A File Offset: 0x0000658A
		public long Part2
		{
			get
			{
				return BitConverter.ToInt64(this.Bytes, 8);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00008398 File Offset: 0x00006598
		public long Part3
		{
			get
			{
				return BitConverter.ToInt64(this.Bytes, 16);
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x000083A7 File Offset: 0x000065A7
		public FingerPrintF(bool {11750})
		{
			this.Bytes = Engine.PlatformTools.MatchFingerprint();
		}

		// Token: 0x0600015E RID: 350 RVA: 0x000083BC File Offset: 0x000065BC
		public FingerPrintF(string {11751})
		{
			try
			{
				if ({11751} == "undefined")
				{
					this.Bytes = new byte[0];
				}
				else
				{
					this.Bytes = Convert.FromBase64String({11751});
				}
				if (this.Bytes.Length != 24)
				{
					this.Bytes = FingerPrintF.empty;
				}
			}
			catch (Exception)
			{
				this.Bytes = FingerPrintF.empty;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00008428 File Offset: 0x00006628
		public override string ToString()
		{
			if (this.IsUndefined)
			{
				return "undefined";
			}
			return Convert.ToBase64String(this.Bytes);
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00008444 File Offset: 0x00006644
		public override int GetHashCode()
		{
			if (this.IsUndefined)
			{
				return 0;
			}
			return BitConverter.ToInt32(this.Bytes, 0) ^ BitConverter.ToInt32(this.Bytes, 4) ^ BitConverter.ToInt32(this.Bytes, 8) ^ BitConverter.ToInt32(this.Bytes, 12) ^ BitConverter.ToInt32(this.Bytes, 16) ^ BitConverter.ToInt32(this.Bytes, 20);
		}

		// Token: 0x06000161 RID: 353 RVA: 0x000084AC File Offset: 0x000066AC
		public override bool Equals(object {11752})
		{
			if (!({11752} is FingerPrintF))
			{
				return false;
			}
			FingerPrintF fingerPrintF = (FingerPrintF){11752};
			if (fingerPrintF.IsUndefined || this.IsUndefined)
			{
				return false;
			}
			int num = 0;
			if (this.Part1 == fingerPrintF.Part1)
			{
				num++;
			}
			if (this.Part2 == fingerPrintF.Part2)
			{
				num++;
			}
			if (this.Part3 == fingerPrintF.Part3)
			{
				num++;
			}
			return num == 3;
		}

		// Token: 0x040000BD RID: 189
		private static byte[] empty = new byte[0];

		// Token: 0x040000BE RID: 190
		public byte[] Bytes;
	}
}
