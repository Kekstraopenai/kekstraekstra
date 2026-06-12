using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002F0 RID: 752
	internal class {21102}
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0008A458 File Offset: 0x00088658
		// (set) Token: 0x06001071 RID: 4209 RVA: 0x0008A460 File Offset: 0x00088660
		public Form Form { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0008A469 File Offset: 0x00088669
		// (set) Token: 0x06001073 RID: 4211 RVA: 0x0008A471 File Offset: 0x00088671
		public Vector2 Offset { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06001074 RID: 4212 RVA: 0x0008A47A File Offset: 0x0008867A
		public Marker UnscaledMarker
		{
			get
			{
				return this.unscaledMarker;
			}
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0008A482 File Offset: 0x00088682
		public {21102}(Marker {21106})
		{
			this.unscaledMarker = {21106};
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0008A49C File Offset: 0x0008869C
		public void Scale(float {21107})
		{
			if (this.scaleFactor == {21107})
			{
				return;
			}
			this.scaleFactor = {21107};
			UiControl form = this.Form;
			Marker marker = this.unscaledMarker.Scale({21107});
			Vector2 offset = this.Offset;
			form.Pos = marker.Offset(offset);
			this.DoScale({21107});
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x00003100 File Offset: 0x00001300
		protected virtual void DoScale(float {21108})
		{
		}

		// Token: 0x04000F0A RID: 3850
		[CompilerGenerated]
		private Form {21109};

		// Token: 0x04000F0B RID: 3851
		[CompilerGenerated]
		private Vector2 {21110};

		// Token: 0x04000F0C RID: 3852
		protected Marker unscaledMarker;

		// Token: 0x04000F0D RID: 3853
		protected float scaleFactor = 1f;
	}
}
