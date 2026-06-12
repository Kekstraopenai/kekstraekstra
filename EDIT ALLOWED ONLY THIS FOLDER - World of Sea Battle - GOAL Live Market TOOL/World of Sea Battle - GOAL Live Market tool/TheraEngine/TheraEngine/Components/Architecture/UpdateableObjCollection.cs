using System;
using TheraEngine.Collections;

namespace TheraEngine.Components.Architecture
{
	// Token: 0x0200010C RID: 268
	public class UpdateableObjCollection : Tlist<IUpdateableObject>, IUpdateableObject
	{
		// Token: 0x060007AF RID: 1967 RVA: 0x00026DAB File Offset: 0x00024FAB
		public UpdateableObjCollection() : base(12)
		{
			this.{14889} = new Tlist<int>(8);
			this.{14890} = new Tlist<IUpdateableObject>(8);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x00026DD8 File Offset: 0x00024FD8
		public void Update(ref FrameTime {14884})
		{
			this.{14888} = true;
			for (int i = 0; i < this.Size; i++)
			{
				this.Array[i].Update(ref {14884});
			}
			this.{14888} = false;
			object obj = this.{14891};
			lock (obj)
			{
				for (int j = 0; j < this.{14889}.Size; j++)
				{
					int num = this.{14889}.Array[j];
					base.RemoveAt(num);
					for (int k = 0; k < this.{14889}.Size; k++)
					{
						if (k != j && this.{14889}.Array[k] > num)
						{
							this.{14889}.Array[k]--;
						}
					}
				}
				this.{14889}.Clear();
				for (int l = 0; l < this.{14890}.Size; l++)
				{
					base.Add(this.{14890}.Array[l]);
				}
				this.{14890}.Clear();
			}
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x00026F04 File Offset: 0x00025104
		public void Add(IUpdateableObject {14885})
		{
			object obj = this.{14891};
			lock (obj)
			{
				if (this.{14888})
				{
					this.{14890}.Add({14885});
				}
				else
				{
					base.Add({14885});
				}
			}
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x00026F60 File Offset: 0x00025160
		public new void RemoveAt(int {14886})
		{
			object obj = this.{14891};
			lock (obj)
			{
				if (this.{14888})
				{
					if (this.{14889}.IndexOf({14886}) == -1)
					{
						this.{14889}.Add({14886});
					}
				}
				else
				{
					base.RemoveAt({14886});
				}
			}
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x00026FCC File Offset: 0x000251CC
		public bool RemoveAt(IUpdateableObject {14887})
		{
			int num = base.IndexOf({14887});
			if (num >= 0)
			{
				this.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x04000568 RID: 1384
		private volatile bool {14888};

		// Token: 0x04000569 RID: 1385
		private Tlist<int> {14889};

		// Token: 0x0400056A RID: 1386
		private Tlist<IUpdateableObject> {14890};

		// Token: 0x0400056B RID: 1387
		private object {14891} = new object();
	}
}
