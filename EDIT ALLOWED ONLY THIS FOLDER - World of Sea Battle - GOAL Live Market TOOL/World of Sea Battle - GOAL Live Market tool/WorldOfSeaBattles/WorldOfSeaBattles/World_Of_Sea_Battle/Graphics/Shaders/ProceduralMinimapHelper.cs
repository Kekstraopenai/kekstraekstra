using System;
using System.Collections.Generic;
using System.Linq;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Graphics;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Graphics.Shaders
{
	// Token: 0x0200045E RID: 1118
	public static class ProceduralMinimapHelper
	{
		// Token: 0x06001866 RID: 6246 RVA: 0x000D353C File Offset: 0x000D173C
		public static void Draw(int {23636}, WorldMapInfo {23637})
		{
			if (ProceduralMinimapHelper.MapTexture == null || ProceduralMinimapHelper.MapTexture.Size.X != (float){23636})
			{
				RenderTarget mapTexture = ProceduralMinimapHelper.MapTexture;
				if (mapTexture != null)
				{
					mapTexture.Dispose();
				}
				ProceduralMinimapHelper.MapTexture = new RenderTarget({23636}, {23636}, SurfaceFormat.Color, DepthFormat.None, 0, false, "mapTexture", false);
			}
			ProceduralMinimapHelper.PrevMapInfo = {23637};
			IRenderTarget currentOutput = Engine.GS.CurrentOutput;
			Engine.GS.SetRenderTarget(ProceduralMinimapHelper.MapTexture);
			Engine.GS.ClearRenderTarget(Color.Transparent);
			Engine.GS.Begin2D(false);
			Color color = new Color(9, 21, 30) * 0.9f;
			Engine.GS.SetTexture(CommonAtlas.Texture.Tex);
			using (IEnumerator<IsleInstance> enumerator = ((IEnumerable<IsleInstance>){23637}.CollisableObjects).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IsleInstance obj = enumerator.Current;
					if (obj.TransformedShallowsPoints != null && obj.TransformedShallowsPoints.Size > 0)
					{
						using (List<List<Vector2>>.Enumerator enumerator2 = ProceduralMinimapHelper.ClusterPoints(obj.TransformedShallowsPoints.ToList<Vector2>(), 20f).GetEnumerator())
						{
							Func<Vector2, float> <>9__0;
							while (enumerator2.MoveNext())
							{
								IEnumerable<Vector2> source = enumerator2.Current;
								Func<Vector2, float> keySelector;
								if ((keySelector = <>9__0) == null)
								{
									keySelector = (<>9__0 = ((Vector2 {23640}) => Geometry.GetRotate(obj.GlobalPosition, {23640})));
								}
								List<Vector2> list = source.OrderBy(keySelector).ToList<Vector2>();
								list.Add(list.First<Vector2>());
								int num = 7;
								Vector2 vector = Vector2.Zero;
								foreach (Vector2 vector2 in list)
								{
									Vector2 vector3 = vector2.YX / {23637}.MapSize * (float){23636} + new Vector2((float){23636}) / 2f;
									vector3.Y = (float){23636} - vector3.Y;
									Device gs = Engine.GS;
									Rectangle rectangle = new Rectangle((int)vector3.X, (int)vector3.Y, num, num);
									Vector2 vector4 = new Vector2(0.5f);
									gs.Draw(CommonAtlas.whitePixel, rectangle, vector4, Rand.Angle(), color);
									if (vector.X != 0f)
									{
										int num2 = 6;
										float num3 = Vector2.Distance(vector, vector3);
										int num4 = num2;
										while ((float)num4 < num3)
										{
											Vector2 vector5 = Vector2.Lerp(vector, vector3, (float)num4 / num3);
											Device gs2 = Engine.GS;
											rectangle = new Rectangle((int)vector5.X, (int)vector5.Y, num, num);
											vector4 = new Vector2(0.5f);
											gs2.Draw(CommonAtlas.whitePixel, rectangle, vector4, Rand.Angle(), color);
											num4 += num2;
										}
									}
									vector = vector3;
								}
							}
							continue;
						}
					}
					if (obj.ModelGlobalBS.Radius > 15f)
					{
						Vector2 vector6 = obj.GlobalPosition.YX / {23637}.MapSize * (float){23636} + new Vector2((float){23636}) / 2f;
						vector6.Y = (float){23636} - vector6.Y;
						Device gs3 = Engine.GS;
						Rectangle rectangle = new Rectangle((int)vector6.X, (int)vector6.Y, 6, 6);
						Vector2 vector4 = new Vector2(0.5f);
						gs3.Draw(CommonAtlas.whitePixel, rectangle, vector4, Rand.Angle(), color);
					}
				}
			}
			Engine.GS.End2D();
			Engine.GS.SetRenderTarget(currentOutput);
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x000D391C File Offset: 0x000D1B1C
		private static List<List<Vector2>> ClusterPoints(List<Vector2> {23638}, float {23639} = 80f)
		{
			List<List<Vector2>> list = new List<List<Vector2>>();
			HashSet<Vector2> hashSet = new HashSet<Vector2>({23638});
			while (hashSet.Count > 0)
			{
				List<Vector2> list2 = new List<Vector2>();
				Queue<Vector2> queue = new Queue<Vector2>();
				Vector2 item = hashSet.First<Vector2>();
				queue.Enqueue(item);
				hashSet.Remove(item);
				while (queue.Count > 0)
				{
					Vector2 current = queue.Dequeue();
					list2.Add(current);
					foreach (Vector2 item2 in (from {23641} in hashSet
					where Vector2.Distance({23641}, current) < {23639}
					select {23641}).ToList<Vector2>())
					{
						queue.Enqueue(item2);
						hashSet.Remove(item2);
					}
				}
				if (list2.Count > 0)
				{
					list.Add(list2);
				}
			}
			return list;
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x000D3A2C File Offset: 0x000D1C2C
		public static void Dispose()
		{
			RenderTarget mapTexture = ProceduralMinimapHelper.MapTexture;
			if (mapTexture != null)
			{
				mapTexture.Dispose();
			}
			ProceduralMinimapHelper.MapTexture = null;
			ProceduralMinimapHelper.PrevMapInfo = null;
		}

		// Token: 0x040016C7 RID: 5831
		public static RenderTarget MapTexture;

		// Token: 0x040016C8 RID: 5832
		public static WorldMapInfo PrevMapInfo;
	}
}
