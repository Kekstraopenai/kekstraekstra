using System;
using TheraEngine.Helpers;

namespace TheraEngine
{
	// Token: 0x02000015 RID: 21
	public struct RTI
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000314A File Offset: 0x0000134A
		public bool IsInitialized
		{
			get
			{
				return this.{11355} != 0;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003158 File Offset: 0x00001358
		public RTI(int {11351})
		{
			this.{11355} = Rand.Int();
			this.{11356} = Rand.Int();
			if (this.{11355} == 0)
			{
				this.{11355} = 1;
			}
			this.{11357} = ({11351} ^ this.{11355});
			this.{11358} = ({11351} ^ this.{11356});
			this.CacheNotChange = {11351};
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000031B0 File Offset: 0x000013B0
		// (set) Token: 0x0600006C RID: 108 RVA: 0x0000321E File Offset: 0x0000141E
		public int Value
		{
			get
			{
				if (this.{11355} == 0)
				{
					throw new NullReferenceException("RTI is not initialized correctly");
				}
				int num = this.{11357} ^ this.{11355};
				int num2 = this.{11358} ^ this.{11356};
				if (num != num2)
				{
					throw new AccessViolationException("RTI protected memory fail");
				}
				int cacheNotChange = this.CacheNotChange;
				if (cacheNotChange != num)
				{
					throw new AccessViolationException("RTI protected memory changed to " + cacheNotChange.ToString());
				}
				return cacheNotChange;
			}
			set
			{
				if (this.{11355} == 0)
				{
					throw new NullReferenceException("RTI is not initialized correctly");
				}
				this.{11357} = (value ^ this.{11355});
				this.{11358} = (value ^ this.{11356});
				this.CacheNotChange = value;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003256 File Offset: 0x00001456
		public override string ToString()
		{
			return this.CacheNotChange.ToString();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003263 File Offset: 0x00001463
		public override int GetHashCode()
		{
			return this.CacheNotChange;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000326B File Offset: 0x0000146B
		public static implicit operator RTI(float {11353})
		{
			return new RTI((int){11353});
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003274 File Offset: 0x00001474
		public static implicit operator RTI(int {11354})
		{
			return new RTI({11354});
		}

		// Token: 0x0400005F RID: 95
		private readonly int {11355};

		// Token: 0x04000060 RID: 96
		private readonly int {11356};

		// Token: 0x04000061 RID: 97
		private int {11357};

		// Token: 0x04000062 RID: 98
		private int {11358};

		// Token: 0x04000063 RID: 99
		public int CacheNotChange;
	}
}
