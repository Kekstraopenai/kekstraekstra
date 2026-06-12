using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Helpers;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x02000497 RID: 1175
	public class PathFinder
	{
		// Token: 0x060019C1 RID: 6593 RVA: 0x000E4BF4 File Offset: 0x000E2DF4
		private static Vector2 RotationSearch(in Vector2 {24299}, Vector2 {24300}, float {24301}, Func<Vector2, Vector2, bool> {24302})
		{
			float num = 0.12566371f;
			for (float num2 = num; num2 < 1.8849558f; num2 += num)
			{
				Vector2 vector;
				Geometry.RotateVector2Fast(ref {24300}, num2 * {24301}, out vector);
				if ({24302}({24299}, {24299} + vector))
				{
					return vector;
				}
			}
			return Vector2.Zero;
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000E4C44 File Offset: 0x000E2E44
		public void BuildAvailablePathes(PathFinder.Path {24303}, in Vector2 {24304}, in Vector2 {24305}, Func<Vector2, Vector2, bool> {24306})
		{
			int num = this.{24310};
			this.{24310} = num - 1;
			if (num < 0)
			{
				Tlist<Vector2> history = {24303}.History;
				Vector2 vector = new Vector2(100000000f, 0f);
				history.Add(vector);
				return;
			}
			float num2 = Math.Min(Vector2.Distance({24304}, {24305}), 70f);
			if (num2 < 5f)
			{
				{24303}.History.Add({24305});
				return;
			}
			{24303}.History.Add({24304});
			Vector2 vector2 = {24305} - {24304};
			vector2.Normalize();
			if (num2 >= 69f)
			{
				vector2 += this.{24311} * 0.2f;
			}
			vector2.Normalize();
			this.{24311} = vector2;
			vector2 *= num2;
			if ({24306}({24304}, {24304} + vector2))
			{
				Vector2 vector = {24304} + vector2;
				this.BuildAvailablePathes({24303}, vector, {24305}, {24306});
				return;
			}
			Vector2 value = PathFinder.RotationSearch({24304}, vector2, 1f, {24306});
			Vector2 value2 = PathFinder.RotationSearch({24304}, vector2, -1f, {24306});
			if (value2.LengthSquared() == 0f && value.LengthSquared() == 0f)
			{
				return;
			}
			PathFinder.Path path = {24303}.Clone();
			if (value.LengthSquared() != 0f)
			{
				Vector2 vector = {24304} + value;
				this.BuildAvailablePathes({24303}, vector, {24305}, {24306});
			}
			if (value2.LengthSquared() != 0f)
			{
				PathFinder.Path {24303}2 = path;
				Vector2 vector = {24304} + value2;
				this.BuildAvailablePathes({24303}2, vector, {24305}, {24306});
				this.AvailablePathes.Add(path);
			}
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000E4DEC File Offset: 0x000E2FEC
		public void RunTracing(in Vector2 {24307}, in Vector2 {24308}, Func<Vector2, Vector2, bool> {24309})
		{
			this.{24310} = 100;
			this.AvailablePathes.Clear();
			PathFinder.Path {24303} = new PathFinder.Path();
			this.BuildAvailablePathes({24303}, {24307}, {24308}, {24309});
			this.AvailablePathes.Add({24303});
			this.ShortestPath = null;
			float num = float.MaxValue;
			foreach (PathFinder.Path path in ((IEnumerable<PathFinder.Path>)this.AvailablePathes))
			{
				path.UpdateDistance();
				if (path.Distance < num)
				{
					this.ShortestPath = path;
					num = path.Distance;
				}
			}
		}

		// Token: 0x04001818 RID: 6168
		public Tlist<PathFinder.Path> AvailablePathes = new Tlist<PathFinder.Path>();

		// Token: 0x04001819 RID: 6169
		public PathFinder.Path ShortestPath;

		// Token: 0x0400181A RID: 6170
		private int {24310};

		// Token: 0x0400181B RID: 6171
		private Vector2 {24311};

		// Token: 0x02000498 RID: 1176
		public class Path
		{
			// Token: 0x060019C5 RID: 6597 RVA: 0x000E4EA0 File Offset: 0x000E30A0
			public void UpdateDistance()
			{
				this.Distance = 0f;
				for (int i = 0; i < this.History.Size - 1; i++)
				{
					this.Distance += Vector2.Distance(this.History.Array[i], this.History.Array[i + 1]);
				}
			}

			// Token: 0x060019C6 RID: 6598 RVA: 0x000E4F06 File Offset: 0x000E3106
			public PathFinder.Path Clone()
			{
				return new PathFinder.Path
				{
					History = this.History.Clone(),
					Distance = this.Distance
				};
			}

			// Token: 0x0400181C RID: 6172
			public float Distance;

			// Token: 0x0400181D RID: 6173
			public Tlist<Vector2> History = new Tlist<Vector2>();
		}
	}
}
