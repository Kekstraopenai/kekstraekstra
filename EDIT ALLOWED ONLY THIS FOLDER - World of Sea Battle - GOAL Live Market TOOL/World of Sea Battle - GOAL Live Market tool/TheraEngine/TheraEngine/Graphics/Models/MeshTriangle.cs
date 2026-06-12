using System;
using Microsoft.Xna.Framework;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x02000161 RID: 353
	public struct MeshTriangle
	{
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600097F RID: 2431 RVA: 0x0002E36C File Offset: 0x0002C56C
		public float Radius
		{
			get
			{
				return Math.Max(Math.Max(Vector3.Distance(this.A, this.B), Vector3.Distance(this.A, this.C)), Vector3.Distance(this.B, this.C));
			}
		}

		// Token: 0x06000980 RID: 2432 RVA: 0x0002E3AC File Offset: 0x0002C5AC
		public MeshTriangle(in VertexPositionNormal {15159}, in VertexPositionNormal {15160}, in VertexPositionNormal {15161})
		{
			this.A = {15159}.Position;
			this.B = {15160}.Position;
			this.C = {15161}.Position;
			this.Center = this.A + this.B + this.C;
			this.Center /= 3f;
			this.MiddleNormal = {15159}.Normal + {15160}.Normal + {15161}.Normal;
			this.MiddleNormal.Normalize();
			this.IsNormalCorrect = Math.Max(0f, Vector3.Dot({15159}.Normal, {15160}.Normal)) * Math.Max(0f, Vector3.Dot({15161}.Normal, {15160}.Normal));
		}

		// Token: 0x0400067B RID: 1659
		public Vector3 Center;

		// Token: 0x0400067C RID: 1660
		public Vector3 A;

		// Token: 0x0400067D RID: 1661
		public Vector3 B;

		// Token: 0x0400067E RID: 1662
		public Vector3 C;

		// Token: 0x0400067F RID: 1663
		public Vector3 MiddleNormal;

		// Token: 0x04000680 RID: 1664
		public float IsNormalCorrect;
	}
}
