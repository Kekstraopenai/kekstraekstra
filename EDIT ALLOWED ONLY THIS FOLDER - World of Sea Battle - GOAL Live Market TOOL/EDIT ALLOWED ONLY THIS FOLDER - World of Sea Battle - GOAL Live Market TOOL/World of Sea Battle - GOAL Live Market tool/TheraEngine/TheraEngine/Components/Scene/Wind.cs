using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Components.Scene
{
	// Token: 0x02000108 RID: 264
	public class Wind
	{
		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600079E RID: 1950 RVA: 0x00026C4D File Offset: 0x00024E4D
		public static Wind Zero
		{
			get
			{
				return new Wind(0.1f, 0.36678052f, new Vector3(0f, 0f, 0f));
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00026C72 File Offset: 0x00024E72
		public static Wind ForwardLower
		{
			get
			{
				return new Wind(2f, 0.36678052f, new Vector3(0f, 0f, -1f));
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00026C97 File Offset: 0x00024E97
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x00026C9F File Offset: 0x00024E9F
		public float Power
		{
			get
			{
				return this.{14880};
			}
			set
			{
				this.{14880} = value;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00026CA8 File Offset: 0x00024EA8
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x00026CB0 File Offset: 0x00024EB0
		public float Dencity
		{
			get
			{
				return this.{14881};
			}
			set
			{
				this.{14881} = value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x00026CB9 File Offset: 0x00024EB9
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x00026CC1 File Offset: 0x00024EC1
		public Vector3 Direction
		{
			get
			{
				return this.{14882};
			}
			set
			{
				this.{14882} = value;
				this.{14882}.Normalize();
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x00026CD5 File Offset: 0x00024ED5
		public Vector3 GetFinalPower
		{
			get
			{
				return this.{14882} * this.{14880} * (this.{14881} / 1.6f);
			}
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00026CF9 File Offset: 0x00024EF9
		public Wind()
		{
			this.SetFromClone(Wind.Zero);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00026D0C File Offset: 0x00024F0C
		public Wind(float {14875}, float {14876}, Vector3 {14877})
		{
			this.{14880} = {14875};
			this.{14881} = {14876};
			this.Direction = {14877};
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00026D29 File Offset: 0x00024F29
		public void SetFromClone(Wind {14878})
		{
			this.{14880} = {14878}.{14880};
			this.{14881} = {14878}.{14881};
			this.{14882} = {14878}.{14882};
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x00026D50 File Offset: 0x00024F50
		public void Rotate(float {14879})
		{
			Matrix matrix;
			Matrix.CreateRotationY({14879}, out matrix);
			Vector3.Transform(ref this.{14882}, ref matrix, out this.{14882});
		}

		// Token: 0x04000565 RID: 1381
		private float {14880};

		// Token: 0x04000566 RID: 1382
		private float {14881};

		// Token: 0x04000567 RID: 1383
		private Vector3 {14882};
	}
}
