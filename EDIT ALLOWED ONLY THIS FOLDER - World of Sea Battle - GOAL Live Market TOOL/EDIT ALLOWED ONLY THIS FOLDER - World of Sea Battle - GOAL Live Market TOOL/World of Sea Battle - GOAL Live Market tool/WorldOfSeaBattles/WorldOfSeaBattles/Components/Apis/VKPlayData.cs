using System;
using System.Runtime.CompilerServices;

namespace WorldOfSeaBattles.Components.Apis
{
	// Token: 0x020005B9 RID: 1465
	[RequiredMember]
	public class VKPlayData
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x0012DA12 File Offset: 0x0012BC12
		// (set) Token: 0x060021B3 RID: 8627 RVA: 0x0012DA1A File Offset: 0x0012BC1A
		[RequiredMember]
		public string Uid { get; set; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x060021B4 RID: 8628 RVA: 0x0012DA23 File Offset: 0x0012BC23
		// (set) Token: 0x060021B5 RID: 8629 RVA: 0x0012DA2B File Offset: 0x0012BC2B
		[RequiredMember]
		public string Token { get; set; }

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x0012DA34 File Offset: 0x0012BC34
		// (set) Token: 0x060021B7 RID: 8631 RVA: 0x0012DA3C File Offset: 0x0012BC3C
		[RequiredMember]
		public string Currency { get; set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x0012DA45 File Offset: 0x0012BC45
		public bool IsActive
		{
			get
			{
				return !string.IsNullOrEmpty(this.Uid);
			}
		}

		// Token: 0x060021B9 RID: 8633 RVA: 0x00003A7C File Offset: 0x00001C7C
		[Obsolete("Constructors of types with required members are not supported in this version of your compiler.", true)]
		[CompilerFeatureRequired("RequiredMembers")]
		public VKPlayData()
		{
		}

		// Token: 0x040020C6 RID: 8390
		[CompilerGenerated]
		private string {26586};

		// Token: 0x040020C7 RID: 8391
		[CompilerGenerated]
		private string {26587};

		// Token: 0x040020C8 RID: 8392
		[CompilerGenerated]
		private string {26588};
	}
}
