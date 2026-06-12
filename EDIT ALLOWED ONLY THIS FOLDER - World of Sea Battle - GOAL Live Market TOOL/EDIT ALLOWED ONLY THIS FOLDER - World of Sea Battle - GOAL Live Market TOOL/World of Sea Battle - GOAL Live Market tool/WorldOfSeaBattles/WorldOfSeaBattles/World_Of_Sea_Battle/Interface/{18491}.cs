using System;
using System.Collections.Generic;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Helpers;
using TheraEngine.Interface;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000149 RID: 329
	internal sealed class {18491} : CustomUi
	{
		// Token: 0x06000781 RID: 1921 RVA: 0x0003BC17 File Offset: 0x00039E17
		public {18491}() : base(false)
		{
			this.{18514} = Global.Player;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserUpdate(ref FrameTime {18492})
		{
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0003BC2C File Offset: 0x00039E2C
		protected override void UserFrontRender()
		{
			this.{18495}();
			Engine.GS.Line2D(AtlasGameGui.rect_asset_whitepixel_1px, Engine.GS.Camera.GetProjection(this.{18515}.X0Y()), Engine.GS.Camera.GetProjection(this.{18516}.X0Y()), this.canForward ? Color.Lime : Color.Red, 2);
			Engine.GS.Line2D(AtlasGameGui.rect_asset_whitepixel_1px, Engine.GS.Camera.GetProjection(this.{18515}.X0Y()), Engine.GS.Camera.GetProjection(this.{18517}.X0Y()), this.canForwardNear ? Color.Lime : Color.Red, 5);
			Engine.GS.Line2D(AtlasGameGui.rect_asset_whitepixel_1px, Engine.GS.Camera.GetProjection(this.{18515}.X0Y()), Engine.GS.Camera.GetProjection(this.{18518}.X0Y()), this.canLeft ? Color.Lime : Color.Red, 2);
			Engine.GS.Line2D(AtlasGameGui.rect_asset_whitepixel_1px, Engine.GS.Camera.GetProjection(this.{18515}.X0Y()), Engine.GS.Camera.GetProjection(this.{18519}.X0Y()), this.canRight ? Color.Lime : Color.Red, 2);
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0003BD9F File Offset: 0x00039F9F
		private IEnumerable<Vector3> EnumerateFences()
		{
			{18491}.<EnumerateFences>d__16 <EnumerateFences>d__ = new {18491}.<EnumerateFences>d__16(-2);
			<EnumerateFences>d__.<>4__this = this;
			return <EnumerateFences>d__;
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0003BDAF File Offset: 0x00039FAF
		public bool CheckIsNotInHomeArea(in Vector2 {18493}, float {18494})
		{
			if (!this.{18514}.MapInfo.CheckPositionBorder({18493}, {18494}, true))
			{
				return true;
			}
			bool isWorldmap = this.{18514}.MapInfo.IsWorldmap;
			return false;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0003BDDC File Offset: 0x00039FDC
		private void {18495}()
		{
			Vector2 position = this.{18514}.Position;
			Vector2 normal = this.{18514}.Normal;
			this.{18515} = position + normal * (7f + this.{18514}.UsedShip.StaticInfo.CorpusHalfLength);
			this.{18516} = position + normal * (65f + this.{18514}.UsedShip.StaticInfo.CorpusHalfLength);
			this.{18517} = position + normal * (25f + this.{18514}.UsedShip.StaticInfo.CorpusHalfLength);
			position + normal * (20f + this.{18514}.UsedShip.StaticInfo.CorpusHalfLength);
			this.{18518} = position + Geometry.RotateVector2(normal, 1.0995574f) * (40f + this.{18514}.UsedShip.StaticInfo.CorpusHalfLength);
			this.{18519} = position + Geometry.RotateVector2(normal, -1.0995574f) * (40f + this.{18514}.UsedShip.StaticInfo.CorpusHalfLength);
			this.canLeft = (this.canRight = (this.canForward = (this.canForwardNear = true)));
			float {18494} = 50f;
			this.CheckIsNotInHomeArea(this.{18515}, {18494});
			this.canForward = !this.CheckIsNotInHomeArea(this.{18516}, {18494});
			this.canForwardNear = !this.CheckIsNotInHomeArea(this.{18517}, {18494});
			this.canLeft = !this.CheckIsNotInHomeArea(this.{18518}, {18494});
			this.canRight = !this.CheckIsNotInHomeArea(this.{18519}, {18494});
			foreach (Vector3 vector in this.EnumerateFences())
			{
				if (this.{18502}(this.{18515}, vector))
				{
					if (this.canForward && !this.{18508}(this.{18515}, this.{18516}, vector, false))
					{
						this.canForwardNear = false;
						this.canForward = false;
					}
					if (this.canLeft && !this.{18508}(this.{18515}, this.{18518}, vector, false))
					{
						this.canLeft = false;
					}
					if (this.canRight && !this.{18508}(this.{18515}, this.{18519}, vector, false))
					{
						this.canRight = false;
					}
				}
				else
				{
					if (this.canForwardNear && this.{18496}(this.{18515}, this.{18517}, vector.X, vector.Y, vector.Z))
					{
						this.canForwardNear = false;
					}
					if (this.canForward && this.{18496}(this.{18515}, this.{18516}, vector.X, vector.Y, vector.Z))
					{
						this.canForward = false;
					}
					if (this.canLeft && this.{18496}(this.{18515}, this.{18518}, vector.X, vector.Y, vector.Z))
					{
						this.canLeft = false;
					}
					if (this.canRight && this.{18496}(this.{18515}, this.{18519}, vector.X, vector.Y, vector.Z))
					{
						this.canRight = false;
					}
				}
			}
			if (this.{18514}.MapInfo.Shallows.GetBilinear(this.{18518}, new Func<byte, float>(this.ShallowFunction)) > 0.66f)
			{
				this.canLeft = false;
			}
			if (this.{18514}.MapInfo.Shallows.GetBilinear(this.{18519}, new Func<byte, float>(this.ShallowFunction)) > 0.66f)
			{
				this.canRight = false;
			}
			if (this.{18514}.MapInfo.Shallows.GetBilinear(this.{18516}, new Func<byte, float>(this.ShallowFunction)) > 0.66f)
			{
				this.canForward = false;
				this.canForwardNear = (this.{18514}.MapInfo.Shallows.GetBilinear(this.{18517}, new Func<byte, float>(this.ShallowFunction)) <= 0.66f);
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0003C24C File Offset: 0x0003A44C
		private bool {18496}(in Vector2 {18497}, in Vector2 {18498}, float {18499}, float {18500}, float {18501})
		{
			float num = {18497}.X;
			float num2 = {18497}.Y;
			float x = {18498}.X;
			float num3 = {18498}.Y;
			num -= {18499};
			num2 -= {18500};
			float num4 = x - {18499};
			num3 -= {18500};
			float num5 = num4 - num;
			float num6 = num3 - num2;
			float num7 = num5 * num5 + num6 * num6;
			float num8 = 2f * (num * num5 + num2 * num6);
			float num9 = num * num + num2 * num2 - {18501} * {18501};
			if (-num8 < 0f)
			{
				return num9 < 0f;
			}
			if (-num8 < 2f * num7)
			{
				return 4f * num7 * num9 - num8 * num8 < 0f;
			}
			return num7 + num8 + num9 < 0f;
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0003C2FF File Offset: 0x0003A4FF
		private bool {18502}(in Vector2 {18503}, in Vector3 {18504})
		{
			return Vector2.DistanceSquared({18503}, {18504}.XY()) < {18504}.Z * {18504}.Z;
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0003C326 File Offset: 0x0003A526
		private float {18505}(in Vector2 {18506}, in Vector3 {18507})
		{
			return Vector2.Distance({18506}, {18507}.XY()) - {18507}.Z;
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0003C348 File Offset: 0x0003A548
		private bool {18508}(Vector2 {18509}, Vector2 {18510}, in Vector3 {18511}, bool {18512} = false)
		{
			Vector2 value = ({18509} - {18511}.XY()).Normal();
			Vector2 value2 = ({18510} - {18509}).Normal();
			return Vector2.Dot(value, value2) > ({18512} ? 0f : 0.9f);
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0003C390 File Offset: 0x0003A590
		public float ShallowFunction(byte {18513})
		{
			if ((int){18513} <= this.{18514}.UsedShipPlayer.CraftFrom.Rank + 1)
			{
				return 0f;
			}
			return 1f;
		}

		// Token: 0x040006AC RID: 1708
		public const float c_isleSizeCollisionThreshold = 6f;

		// Token: 0x040006AD RID: 1709
		public const float freeFloatingMapBorderRadius = 50f;

		// Token: 0x040006AE RID: 1710
		internal bool canForwardNear;

		// Token: 0x040006AF RID: 1711
		internal bool canForward;

		// Token: 0x040006B0 RID: 1712
		internal bool canRight;

		// Token: 0x040006B1 RID: 1713
		internal bool canLeft;

		// Token: 0x040006B2 RID: 1714
		private Player {18514};

		// Token: 0x040006B3 RID: 1715
		private Vector2 {18515};

		// Token: 0x040006B4 RID: 1716
		private Vector2 {18516};

		// Token: 0x040006B5 RID: 1717
		private Vector2 {18517};

		// Token: 0x040006B6 RID: 1718
		private Vector2 {18518};

		// Token: 0x040006B7 RID: 1719
		private Vector2 {18519};
	}
}
