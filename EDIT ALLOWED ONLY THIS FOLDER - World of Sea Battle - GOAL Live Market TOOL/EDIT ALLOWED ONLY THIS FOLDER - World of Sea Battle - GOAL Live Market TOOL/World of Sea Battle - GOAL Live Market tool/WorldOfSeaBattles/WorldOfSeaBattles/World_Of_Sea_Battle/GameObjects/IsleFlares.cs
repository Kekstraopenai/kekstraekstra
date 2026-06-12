using System;
using Microsoft.Xna.Framework;
using TheraEngine.Helpers;
using TheraEngine.Scene.Lighting;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000045 RID: 69
	public struct IsleFlares
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0000F908 File Offset: 0x0000DB08
		public Vector3 Color
		{
			get
			{
				Vector3 vector = HashHelper.SphericalVectorFromLerp((float)HashHelper.greaterInt(this.UniqueKey, 10000) / 1000f);
				return new Vector3(0.6f + 0.2f * vector.X, 0.6f + 0.2f * vector.Y, 0.2f);
			}
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x0000F960 File Offset: 0x0000DB60
		public IsleFlares(Vector3 {16649}, IsleFlaresSize {16650}, int {16651}, float {16652} = 1f)
		{
			this.Position = {16649};
			this.Size = {16650};
			this.UniqueKey = {16651};
			this.Transparancy = {16652};
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x0000F980 File Offset: 0x0000DB80
		public void Draw(LightFlareRender {16653}, float {16654})
		{
			if (Renderer.ReflectionsAreBeingDrawn)
			{
				{16653}.RenderSimpleFlare(this.Position, (this.Size == IsleFlaresSize.Big) ? 50f : ((this.Size == IsleFlaresSize.Small) ? 12f : 6f), (1f - Global.Game.StaticSystem.GetSkyShader.DayOrNight) * MathF.Pow({16654} * 10f, (this.Size == IsleFlaresSize.Big) ? 0.25f : 0.5f) * ((this.Size == IsleFlaresSize.Ship) ? 0.45f : 0.6f) * this.Transparancy, this.Color * 2f);
				return;
			}
			{16653}.RenderSimpleFlare(this.Position, (this.Size == IsleFlaresSize.Big) ? 50f : ((this.Size == IsleFlaresSize.Small) ? 12f : 3f), (1f - Global.Game.StaticSystem.GetSkyShader.DayOrNight) * MathF.Pow({16654}, (this.Size == IsleFlaresSize.Big) ? 0.25f : 0.5f) * ((this.Size == IsleFlaresSize.Ship) ? 0.45f : 0.6f) * this.Transparancy, this.Color);
		}

		// Token: 0x04000187 RID: 391
		public Vector3 Position;

		// Token: 0x04000188 RID: 392
		public IsleFlaresSize Size;

		// Token: 0x04000189 RID: 393
		public int UniqueKey;

		// Token: 0x0400018A RID: 394
		public float Transparancy;
	}
}
