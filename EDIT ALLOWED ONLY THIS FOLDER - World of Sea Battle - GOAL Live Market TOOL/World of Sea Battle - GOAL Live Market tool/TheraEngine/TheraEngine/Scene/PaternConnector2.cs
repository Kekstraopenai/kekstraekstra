using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using TheraEngine.Collections;

namespace TheraEngine.Scene
{
	// Token: 0x02000044 RID: 68
	public class PaternConnector2<TFrom, TTo>
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001F3 RID: 499 RVA: 0x0000B4F0 File Offset: 0x000096F0
		// (set) Token: 0x060001F4 RID: 500 RVA: 0x0000B4F8 File Offset: 0x000096F8
		public IEnumerable<TFrom> Source { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001F5 RID: 501 RVA: 0x0000B501 File Offset: 0x00009701
		// (set) Token: 0x060001F6 RID: 502 RVA: 0x0000B509 File Offset: 0x00009709
		public Func<IEnumerable<TFrom>> SourceAsFunc { get; set; }

		// Token: 0x060001F7 RID: 503 RVA: 0x0000B512 File Offset: 0x00009712
		public PaternConnector2(IEnumerable<TFrom> {11988}, Func<TFrom, TTo> {11989}, Func<TFrom, TFrom, bool> {11990}, Action<TTo> {11991} = null)
		{
			this.Source = {11988};
			this.CreateFunc = {11989};
			this.DisposingFunc = {11991};
			this.EqualityComparer = {11990};
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B54D File Offset: 0x0000974D
		public PaternConnector2(Func<IEnumerable<TFrom>> {11992}, Func<TFrom, TTo> {11993}, Func<TFrom, TFrom, bool> {11994}, Action<TTo> {11995} = null)
		{
			this.SourceAsFunc = {11992};
			this.CreateFunc = {11993};
			this.DisposingFunc = {11995};
			this.EqualityComparer = {11994};
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B588 File Offset: 0x00009788
		public void Update()
		{
			if (this.SourceAsFunc != null)
			{
				this.Source = this.SourceAsFunc();
			}
			int i;
			int j;
			for (i = 0; i < this.prev.Size; i = j + 1)
			{
				if (!this.Source.Any((TFrom {11997}) => this.EqualityComparer({11997}, this.prev.Array[i])))
				{
					Action<TTo> disposingFunc = this.DisposingFunc;
					if (disposingFunc != null)
					{
						disposingFunc(this.Targets.Array[i]);
					}
					this.Targets.FastRemoveAt(i);
					this.prev.FastRemoveAt(i);
					j = i;
					i = j - 1;
				}
				j = i;
			}
			using (IEnumerator<TFrom> enumerator = this.Source.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TFrom item = enumerator.Current;
					if (!this.prev.Any((TFrom {11998}) => this.EqualityComparer({11998}, item)))
					{
						this.prev.Add(item);
						Tlist<TTo> targets = this.Targets;
						TTo tto = this.CreateFunc(item);
						targets.Add(tto);
					}
				}
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x0000B6F4 File Offset: 0x000098F4
		public void Clean(bool {11996})
		{
			this.prev.Clear();
			if ({11996} && this.DisposingFunc != null)
			{
				for (int i = 0; i < this.Targets.Size; i++)
				{
					this.DisposingFunc(this.Targets.Array[i]);
				}
			}
			this.Targets.Clear();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x0000B754 File Offset: 0x00009954
		[return: TupleElementNames(new string[]
		{
			"Key",
			"Value"
		})]
		public IEnumerable<ValueTuple<TFrom, TTo>> ToDictionary()
		{
			PaternConnector2<TFrom, TTo>.<ToDictionary>d__17 <ToDictionary>d__ = new PaternConnector2<TFrom, TTo>.<ToDictionary>d__17(-2);
			<ToDictionary>d__.<>4__this = this;
			return <ToDictionary>d__;
		}

		// Token: 0x0400013B RID: 315
		public Tlist<TTo> Targets = new Tlist<TTo>();

		// Token: 0x0400013C RID: 316
		private Tlist<TFrom> prev = new Tlist<TFrom>();

		// Token: 0x0400013D RID: 317
		public Func<TFrom, TTo> CreateFunc;

		// Token: 0x0400013E RID: 318
		public Action<TTo> DisposingFunc;

		// Token: 0x0400013F RID: 319
		public Func<TFrom, TFrom, bool> EqualityComparer;
	}
}
