using System;
using Common.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x0200044E RID: 1102
	public class ShipDestructionHelper
	{
		// Token: 0x060017F0 RID: 6128 RVA: 0x000CF250 File Offset: 0x000CD450
		public void Initialize(Ship {23472})
		{
			this.{23485} = {23472};
			this.{23486} = new byte[576];
			float hpFactor = {23472}.UsedShip.HpFactor;
			this.HitEffectGeneral(1f - hpFactor);
			this.{23484}();
		}

		// Token: 0x060017F1 RID: 6129 RVA: 0x000CF293 File Offset: 0x000CD493
		public void CleanResouces()
		{
			this.{23486} = null;
			Texture2D intencityMap = this.IntencityMap;
			if (intencityMap != null)
			{
				intencityMap.Dispose();
			}
			this.IntencityMap = null;
		}

		// Token: 0x060017F2 RID: 6130 RVA: 0x000CF2B4 File Offset: 0x000CD4B4
		public void Restore(float {23473})
		{
			for (int i = 0; i < this.{23486}.Length; i++)
			{
				this.{23486}[i] = (byte)Math.Max(0, (int)this.{23486}[i] - Rand.Round(255f * {23473} * 1.5f));
			}
			this.{23484}();
		}

		// Token: 0x060017F3 RID: 6131 RVA: 0x000CF304 File Offset: 0x000CD504
		public void HitEffectGeneral(float {23474})
		{
			for (int i = 0; i < 32; i++)
			{
				if (Rand.Chanse({23474} * 100f))
				{
					this.{23477}(1f, Geometry.SubstructRotateFast((float)i / 32f * 6.2831855f, Rand.Range(0.9f, 1.1f)));
				}
			}
		}

		// Token: 0x060017F4 RID: 6132 RVA: 0x000CF35C File Offset: 0x000CD55C
		public void HitEffect(float {23475}, Vector3 {23476})
		{
			Vector2 {23479} = {23476}.Normal().XZ();
			this.{23477}({23475} * 66f, {23479});
			this.{23484}();
		}

		// Token: 0x060017F5 RID: 6133 RVA: 0x000CF38C File Offset: 0x000CD58C
		private void {23477}(float {23478}, Vector2 {23479})
		{
			float num = {23479}.X * 0.5f + 0.5f;
			float num2 = {23479}.Y * 0.5f + 0.5f;
			int num3 = (int)MathHelper.Clamp(num * 24f, 0f, 24f);
			int num4 = (int)MathHelper.Clamp(num2 * 24f, 0f, 24f);
			this.{23480}(num3, num4, {23478} * 0.7f);
			this.{23480}(num3 + 1, num4, {23478} * 0.4f);
			this.{23480}(num3 - 1, num4, {23478} * 0.4f);
			this.{23480}(num3, num4 + 1, {23478} * 0.4f);
			this.{23480}(num3, num4 - 1, {23478} * 0.4f);
		}

		// Token: 0x060017F6 RID: 6134 RVA: 0x000CF440 File Offset: 0x000CD640
		private void {23480}(int {23481}, int {23482}, float {23483})
		{
			int num = Math.Min(Math.Max(0, {23481}), 23) + Math.Min(Math.Max(0, {23482}), 23) * 24;
			this.{23486}[num] = (byte)Math.Min(255, (int)this.{23486}[num] + Rand.Round({23483} * 255f));
		}

		// Token: 0x060017F7 RID: 6135 RVA: 0x000CF498 File Offset: 0x000CD698
		private void {23484}()
		{
			if (!Global.Settings.HighDetailing)
			{
				return;
			}
			if (!Global.Game.IsActive || ShipDestructionHelper.hadError)
			{
				return;
			}
			if (this.IntencityMap == null)
			{
				try
				{
					this.IntencityMap = new Texture2D(Engine.GS.graphicsDevice, 24, 24, false, SurfaceFormat.Alpha8);
				}
				catch (Exception {25356})
				{
					if (!ShipDestructionHelper.hadError)
					{
						ShipDestructionHelper.hadError = true;
						Helpers.SendError({25356}, "ShipDestructuinHelper -> InstallTextureData", false, false);
					}
				}
			}
			Texture2D intencityMap = this.IntencityMap;
			if (intencityMap == null)
			{
				return;
			}
			intencityMap.SetData<byte>(this.{23486});
		}

		// Token: 0x04001678 RID: 5752
		private const int resolution = 24;

		// Token: 0x04001679 RID: 5753
		public Texture2D IntencityMap;

		// Token: 0x0400167A RID: 5754
		private Ship {23485};

		// Token: 0x0400167B RID: 5755
		private byte[] {23486};

		// Token: 0x0400167C RID: 5756
		private static bool hadError;
	}
}
