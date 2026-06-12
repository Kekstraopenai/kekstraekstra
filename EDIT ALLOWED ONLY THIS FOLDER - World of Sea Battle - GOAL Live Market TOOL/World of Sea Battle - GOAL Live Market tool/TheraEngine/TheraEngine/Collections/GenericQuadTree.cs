using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TheraEngine.Collections
{
	// Token: 0x0200011A RID: 282
	public class GenericQuadTree<T> where T : class, IGenericQuadTreeItem
	{
		// Token: 0x060007F8 RID: 2040 RVA: 0x00027DEE File Offset: 0x00025FEE
		public GenericQuadTree(QuadTree.Area bounds)
		{
			this.bounds = bounds;
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00027E08 File Offset: 0x00026008
		public bool Insert(T point)
		{
			if (!this.bounds.Contains(point.Position))
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

		// Token: 0x060007FA RID: 2042 RVA: 0x00027EA4 File Offset: 0x000260A4
		private void Subdivide()
		{
			float x = this.bounds.X;
			float y = this.bounds.Y;
			float num = this.bounds.Width / 2f;
			float num2 = this.bounds.Height / 2f;
			this.topLeftTree = new GenericQuadTree<T>(new QuadTree.Area(x, y, num, num2));
			this.topRightTree = new GenericQuadTree<T>(new QuadTree.Area(x + num, y, num, num2));
			this.bottomLeftTree = new GenericQuadTree<T>(new QuadTree.Area(x, y + num2, num, num2));
			this.bottomRightTree = new GenericQuadTree<T>(new QuadTree.Area(x + num, y + num2, num, num2));
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00027F48 File Offset: 0x00026148
		public int CountPointsInArea(ref QuadTree.Area range)
		{
			int num = 0;
			if (!this.bounds.Intersects(ref range))
			{
				return num;
			}
			for (int i = 0; i < this.points.Count; i++)
			{
				if (range.Contains(this.points[i].Position))
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

		// Token: 0x060007FC RID: 2044 RVA: 0x00027FE8 File Offset: 0x000261E8
		public void FetchPointsInArea(ref QuadTree.Area range, ICollection<T> addTo)
		{
			if (!this.bounds.Intersects(ref range))
			{
				return;
			}
			for (int i = 0; i < this.points.Count; i++)
			{
				if (range.Contains(this.points[i].Position))
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

		// Token: 0x060007FD RID: 2045 RVA: 0x00028088 File Offset: 0x00026288
		public T FindClosest(Vector2 target, ref float closestDistance)
		{
			T t = default(T);
			for (int i = 0; i < this.points.Count; i++)
			{
				float num = Vector2.DistanceSquared(this.points[i].Position, target);
				if (num < closestDistance)
				{
					closestDistance = num;
					t = this.points[i];
				}
			}
			if (this.topLeftTree != null)
			{
				t = this.FindClosestInSubtree(target, this.topLeftTree, t, ref closestDistance);
				t = this.FindClosestInSubtree(target, this.topRightTree, t, ref closestDistance);
				t = this.FindClosestInSubtree(target, this.bottomLeftTree, t, ref closestDistance);
				t = this.FindClosestInSubtree(target, this.bottomRightTree, t, ref closestDistance);
			}
			return t;
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x0002812E File Offset: 0x0002632E
		private T FindClosestInSubtree(Vector2 target, GenericQuadTree<T> subtree, T currentClosest, ref float closestDistance)
		{
			if (subtree.bounds.DistanceToSq(target) < closestDistance)
			{
				return subtree.FindClosest(target, ref closestDistance);
			}
			return currentClosest;
		}

		// Token: 0x04000592 RID: 1426
		private const int MAX_POINTS = 16;

		// Token: 0x04000593 RID: 1427
		private readonly QuadTree.Area bounds;

		// Token: 0x04000594 RID: 1428
		private readonly List<T> points = new List<T>();

		// Token: 0x04000595 RID: 1429
		private GenericQuadTree<T> topLeftTree;

		// Token: 0x04000596 RID: 1430
		private GenericQuadTree<T> topRightTree;

		// Token: 0x04000597 RID: 1431
		private GenericQuadTree<T> bottomLeftTree;

		// Token: 0x04000598 RID: 1432
		private GenericQuadTree<T> bottomRightTree;
	}
}
