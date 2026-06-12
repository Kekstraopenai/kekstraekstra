using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Collections;

namespace TheraEngine.Graphics
{
	// Token: 0x0200013F RID: 319
	public class DynamicOcclusionSystem
	{
		// Token: 0x0600090D RID: 2317 RVA: 0x0002C31A File Offset: 0x0002A51A
		public DynamicOcclusionSystem(IEnumerable<DynamicOcclusionSystem.Request> {14998})
		{
			this.Source = {14998};
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x0002C354 File Offset: 0x0002A554
		[NullableContext(2)]
		public void Draw(ParticlesAndStaticMesh {14999})
		{
			Vector3 direction = Engine.GS.Camera.Direction;
			foreach (DynamicOcclusionSystem.Request request in this.Source)
			{
				LightSourceOcclusionTest lightSourceOcclusionTest;
				if (!this.Tests.TryGetValue(request.DictKey, out lightSourceOcclusionTest))
				{
					lightSourceOcclusionTest = new LightSourceOcclusionTest
					{
						QuerySize = request.QuerySize,
						UpdateIntervalMs = (float)this.TestUpdateInterval
					};
					this.Tests.Add(request.DictKey, lightSourceOcclusionTest);
				}
				Vector3 vector = request.Position - Engine.GS.Camera.Position;
				bool {15025} = Vector3.Dot(vector, direction) > 0.1f;
				lightSourceOcclusionTest.Tag = 0;
				lightSourceOcclusionTest.BeginTest(request.Position, {15025}, this.MinSkipDistance > 0f && vector.LengthSquared() < this.MinSkipDistance * this.MinSkipDistance, {14999});
			}
			foreach (KeyValuePair<int, LightSourceOcclusionTest> keyValuePair in this.Tests)
			{
				keyValuePair.Value.Tag = (int)keyValuePair.Value.Tag + 1;
				if ((int)keyValuePair.Value.Tag > this.RemoveTimeFrames)
				{
					Tlist<int> tlist = this.{15000};
					int key = keyValuePair.Key;
					tlist.Add(key);
				}
			}
			foreach (int key2 in ((IEnumerable<int>)this.{15000}))
			{
				LightSourceOcclusionTest lightSourceOcclusionTest2;
				this.Tests.Remove(key2, out lightSourceOcclusionTest2);
				lightSourceOcclusionTest2.Dispose();
			}
			this.{15000}.Clear();
		}

		// Token: 0x04000613 RID: 1555
		public Dictionary<int, LightSourceOcclusionTest> Tests = new Dictionary<int, LightSourceOcclusionTest>();

		// Token: 0x04000614 RID: 1556
		public int RemoveTimeFrames = 600;

		// Token: 0x04000615 RID: 1557
		public int TestUpdateInterval = 100;

		// Token: 0x04000616 RID: 1558
		public IEnumerable<DynamicOcclusionSystem.Request> Source;

		// Token: 0x04000617 RID: 1559
		public float MinSkipDistance;

		// Token: 0x04000618 RID: 1560
		private Tlist<int> {15000} = new Tlist<int>(50);

		// Token: 0x02000140 RID: 320
		public struct Request
		{
			// Token: 0x0600090F RID: 2319 RVA: 0x0002C554 File Offset: 0x0002A754
			public Request(Vector3 {15004}, int {15005}, int {15006})
			{
				this.Position = {15004};
				this.DictKey = {15005};
				this.QuerySize = {15006};
			}

			// Token: 0x04000619 RID: 1561
			public Vector3 Position;

			// Token: 0x0400061A RID: 1562
			public int DictKey;

			// Token: 0x0400061B RID: 1563
			public int QuerySize;
		}
	}
}
