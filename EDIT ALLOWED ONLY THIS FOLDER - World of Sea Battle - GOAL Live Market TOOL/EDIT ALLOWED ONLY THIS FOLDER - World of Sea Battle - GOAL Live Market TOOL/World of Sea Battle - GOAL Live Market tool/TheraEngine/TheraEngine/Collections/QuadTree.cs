using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;

namespace TheraEngine.Collections
{
	// Token: 0x02000117 RID: 279
	public class QuadTree
	{
		// Token: 0x060007E8 RID: 2024 RVA: 0x00027870 File Offset: 0x00025A70
		public QuadTree(QuadTree.Area bounds)
		{
			this.bounds = bounds;
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x0002788C File Offset: 0x00025A8C
		public bool Insert(in Vector2 point)
		{
			if (!this.bounds.Contains(point))
			{
				return false;
			}
			if (this.points.Count < 16 && this.topLeftTree == null)
			{
				this.points.Add(point);
				return true;
			}
			if (this.topLeftTree == null)
			{
				this.Subdivide();
			}
			return this.topLeftTree.Insert(point) || this.topRightTree.Insert(point) || this.bottomLeftTree.Insert(point) || this.bottomRightTree.Insert(point);
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x00027928 File Offset: 0x00025B28
		private void Subdivide()
		{
			float x = this.bounds.X;
			float y = this.bounds.Y;
			float num = this.bounds.Width / 2f;
			float num2 = this.bounds.Height / 2f;
			this.topLeftTree = new QuadTree(new QuadTree.Area(x, y, num, num2));
			this.topRightTree = new QuadTree(new QuadTree.Area(x + num, y, num, num2));
			this.bottomLeftTree = new QuadTree(new QuadTree.Area(x, y + num2, num, num2));
			this.bottomRightTree = new QuadTree(new QuadTree.Area(x + num, y + num2, num, num2));
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000279CC File Offset: 0x00025BCC
		public int CountPointsInArea(ref QuadTree.Area range)
		{
			int num = 0;
			if (!this.bounds.Intersects(ref range))
			{
				return num;
			}
			for (int i = 0; i < this.points.Count; i++)
			{
				if (range.Contains(this.points[i]))
				{
					num++;
				}
			}
			if (this.topLeftTree == null)
			{
				return num;
			}
			num += this.topLeftTree.CountPointsInArea(ref range);
			num += this.topRightTree.CountPointsInArea(ref range);
			num += this.bottomLeftTree.CountPointsInArea(ref range);
			return num + this.bottomRightTree.CountPointsInArea(ref range);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x00027A60 File Offset: 0x00025C60
		public void FetchPointsInArea(ref QuadTree.Area range, ICollection<Vector2> addTo)
		{
			if (!this.bounds.Intersects(ref range))
			{
				return;
			}
			for (int i = 0; i < this.points.Count; i++)
			{
				if (range.Contains(this.points[i]))
				{
					addTo.Add(this.points[i]);
				}
			}
			if (this.topLeftTree == null)
			{
				return;
			}
			this.topLeftTree.FetchPointsInArea(ref range, addTo);
			this.topRightTree.FetchPointsInArea(ref range, addTo);
			this.bottomLeftTree.FetchPointsInArea(ref range, addTo);
			this.bottomRightTree.FetchPointsInArea(ref range, addTo);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x00027AF8 File Offset: 0x00025CF8
		public Vector2? FindClosest(Vector2 target, ref float closestDistance)
		{
			Vector2? vector = null;
			for (int i = 0; i < this.points.Count; i++)
			{
				float num = Vector2.DistanceSquared(this.points[i], target);
				if (num < closestDistance)
				{
					closestDistance = num;
					vector = new Vector2?(this.points[i]);
				}
			}
			if (this.topLeftTree != null)
			{
				vector = this.FindClosestInSubtree(target, this.topLeftTree, vector, ref closestDistance);
				vector = this.FindClosestInSubtree(target, this.topRightTree, vector, ref closestDistance);
				vector = this.FindClosestInSubtree(target, this.bottomLeftTree, vector, ref closestDistance);
				vector = this.FindClosestInSubtree(target, this.bottomRightTree, vector, ref closestDistance);
			}
			return vector;
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x00027B9A File Offset: 0x00025D9A
		private Vector2? FindClosestInSubtree(Vector2 target, QuadTree subtree, Vector2? currentClosest, ref float closestDistance)
		{
			if (subtree.bounds.DistanceToSq(target) < closestDistance)
			{
				return subtree.FindClosest(target, ref closestDistance);
			}
			return currentClosest;
		}

		// Token: 0x04000587 RID: 1415
		private const int MAX_POINTS = 16;

		// Token: 0x04000588 RID: 1416
		private readonly QuadTree.Area bounds;

		// Token: 0x04000589 RID: 1417
		private readonly List<Vector2> points = new List<Vector2>();

		// Token: 0x0400058A RID: 1418
		private QuadTree topLeftTree;

		// Token: 0x0400058B RID: 1419
		private QuadTree topRightTree;

		// Token: 0x0400058C RID: 1420
		private QuadTree bottomLeftTree;

		// Token: 0x0400058D RID: 1421
		private QuadTree bottomRightTree;

		// Token: 0x02000118 RID: 280
		public readonly struct Area
		{
			// Token: 0x060007EF RID: 2031 RVA: 0x00027BB8 File Offset: 0x00025DB8
			public Area(float x, float y, float width, float height)
			{
				this.X = x;
				this.Y = y;
				this.Width = width;
				this.Height = height;
			}

			// Token: 0x060007F0 RID: 2032 RVA: 0x00027BD8 File Offset: 0x00025DD8
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool Contains(Vector2 value)
			{
				return this.X <= value.X && this.Y <= value.Y && value.X < this.X + this.Width && value.Y < this.Y + this.Height;
			}

			// Token: 0x060007F1 RID: 2033 RVA: 0x00027C30 File Offset: 0x00025E30
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool Contains(Vector2 value, float radius)
			{
				float num = Math.Max(this.X, Math.Min(value.X, this.X + this.Width));
				float num2 = Math.Max(this.Y, Math.Min(value.Y, this.Y + this.Height));
				float num3 = value.X - num;
				float num4 = value.Y - num2;
				return num3 * num3 + num4 * num4 < radius * radius;
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x060007F2 RID: 2034 RVA: 0x00027CA1 File Offset: 0x00025EA1
			public float Right
			{
				get
				{
					return this.X + this.Width;
				}
			}

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x060007F3 RID: 2035 RVA: 0x00027CB0 File Offset: 0x00025EB0
			public float Bottom
			{
				get
				{
					return this.Y + this.Height;
				}
			}

			// Token: 0x060007F4 RID: 2036 RVA: 0x00027CBF File Offset: 0x00025EBF
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool Intersects(ref QuadTree.Area value)
			{
				return value.X < this.Right && this.X < value.Right && value.Y < this.Bottom && this.Y < value.Bottom;
			}

			// Token: 0x060007F5 RID: 2037 RVA: 0x00027CFC File Offset: 0x00025EFC
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public float DistanceTo(Vector2 point)
			{
				float num = Math.Max(Math.Max(this.X - point.X, 0f), point.X - (this.X + this.Width));
				float num2 = Math.Max(this.Y - point.Y, 0f);
				num2 = Math.Max(num2, point.Y - (this.Y + this.Height));
				return MathF.Sqrt(num * num + num2 * num2);
			}

			// Token: 0x060007F6 RID: 2038 RVA: 0x00027D78 File Offset: 0x00025F78
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public float DistanceToSq(Vector2 point)
			{
				float num = Math.Max(Math.Max(this.X - point.X, 0f), point.X - (this.X + this.Width));
				float num2 = Math.Max(this.Y - point.Y, 0f);
				num2 = Math.Max(num2, point.Y - (this.Y + this.Height));
				return num * num + num2 * num2;
			}

			// Token: 0x0400058E RID: 1422
			public readonly float X;

			// Token: 0x0400058F RID: 1423
			public readonly float Y;

			// Token: 0x04000590 RID: 1424
			public readonly float Width;

			// Token: 0x04000591 RID: 1425
			public readonly float Height;
		}
	}
}
