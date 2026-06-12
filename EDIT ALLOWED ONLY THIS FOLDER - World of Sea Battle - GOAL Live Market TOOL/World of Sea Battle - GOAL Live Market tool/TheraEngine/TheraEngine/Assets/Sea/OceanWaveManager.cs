using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Helpers;

namespace TheraEngine.Assets.Sea
{
	// Token: 0x0200018F RID: 399
	public class OceanWaveManager
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x00033978 File Offset: 0x00031B78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private float {15751}(float {15752}, float {15753}, float {15754}, float {15755}, float {15756}, bool {15757})
		{
			float num = 0f;
			for (int i = 0; i < OceanWaveManager.waves.Length; i++)
			{
				OceanWaveManager.Wave wave = OceanWaveManager.waves[i];
				float num2 = ({15753} + this.WavePosition.X * wave.speed * {15756}) * wave.direction.X + ({15754} + this.WavePosition.Y * wave.speed * {15756}) * wave.direction.Y;
				float num3 = wave.height * (this.{15774}(0.5f + 0.5f * Geometry.Fsin(num2 * wave.length)) - 0.4f);
				if ({15757})
				{
					num += num3 * MathHelper.Lerp(1.1f, {15752}, wave.heightMofifyMask);
				}
				else
				{
					num += num3;
				}
			}
			return num * {15755} * 2f;
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x00033A50 File Offset: 0x00031C50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private Vector2 {15758}(float {15759}, float {15760}, float {15761}, float {15762})
		{
			Vector2 result = default(Vector2);
			for (int i = 0; i < 7; i++)
			{
				OceanWaveManager.Wave wave = OceanWaveManager.waves[i];
				float num = ({15759} + this.WavePosition.X * wave.speed * {15762}) * wave.direction.X + ({15760} + this.WavePosition.Y * wave.speed * {15762}) * wave.direction.Y;
				float num2 = Geometry.Fsin(num * wave.length);
				float num3 = 2f * wave.length * wave.height * 0.15625f * OceanWaveManager.f_pow4(num2 + 1f) * MathF.Cos(num * wave.length);
				result.X += num3 * wave.direction.X * {15761};
				result.Y += num3 * wave.direction.Y * {15761};
			}
			return result;
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00033B4C File Offset: 0x00031D4C
		public float OnlyHeight(float {15763}, float {15764}, float {15765})
		{
			{15764} = -{15764};
			{15765} = -{15765};
			return (this.{15751}({15763}, {15764}, {15765}, 4f, 0.7f, true) + this.{15751}({15763}, {15765} * 5f, {15764} * 5f, 1f, -1f, false) + this.{15751}({15763}, {15764} / 2f, {15765} / 2f, 0.5f, -1.7f, false)) * 0.075f + 0.1f;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00033BC4 File Offset: 0x00031DC4
		public Vector3 NormalOnly(float {15766}, float {15767}, float {15768})
		{
			Vector3 result;
			float num;
			this.GetSimpleStatement({15766}, {15767}, {15767}, out result, out num);
			return result;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00033BE0 File Offset: 0x00031DE0
		public void GetSimpleStatement(float {15769}, float {15770}, float {15771}, out Vector3 {15772}, out float {15773})
		{
			{15773} = this.OnlyHeight({15769}, {15770}, {15771});
			Vector2 vector = default(Vector2) + this.{15758}(-{15770}, -{15771}, 4f * {15769}, 0.7f);
			vector += this.{15758}(-{15770} * 5f, -{15771} * 5f, 1f, -1f);
			{15772}.Y = 1f;
			{15772}.X = -vector.X * 0.075f;
			{15772}.Z = -vector.Y * 0.075f;
			{15772}.Normalize();
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x00033C80 File Offset: 0x00031E80
		private float {15774}(float {15775})
		{
			float num = {15775} * {15775} * {15775} * {15775} * {15775};
			return 0.3f * num * num + 0.7f * num;
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00033CA8 File Offset: 0x00031EA8
		private static float displaced(float {15776})
		{
			return {15776} - 0.3f * {15776} * (1f - {15776});
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00033CBB File Offset: 0x00031EBB
		private static float f_pow5(float {15777})
		{
			return {15777} * {15777} * {15777} * {15777} * {15777};
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00033CC6 File Offset: 0x00031EC6
		private static float f_pow4(float {15778})
		{
			return {15778} * {15778} * {15778} * {15778};
		}

		// Token: 0x040007CD RID: 1997
		private const int NUM_WAVES = 7;

		// Token: 0x040007CE RID: 1998
		public static OceanWaveManager.Wave[] waves = new OceanWaveManager.Wave[]
		{
			new OceanWaveManager.Wave(new Vector2(-0.3446038f, -0.9387482f), 0.1951219, 0.7926829, 2.716418, 0.5),
			new OceanWaveManager.Wave(new Vector2(-0.9534001f, -0.3017089f), 0.2926829, 0.6097561, 0.7575758, 0.0),
			new OceanWaveManager.Wave(new Vector2(0.7714894f, -0.6362421f), 0.1646342, 0.6707317, 3.62963, 0.8),
			new OceanWaveManager.Wave(new Vector2(-0.999201f, -0.03996807f), 0.1585366, 0.6707317, 1.846154, 0.5),
			new OceanWaveManager.Wave(new Vector2(0.2649824f, -0.9642532f), 0.2682927, 0.3658536, 5.800001, 0.0),
			new OceanWaveManager.Wave(new Vector2(0.693433f, -0.7205212f), 0.2743903, 0.3048781, 4.0, 0.0),
			new OceanWaveManager.Wave(new Vector2(0.9275026f, -0.3738168f), 0.09846154, 0.4878049, 8.66997, 1.0)
		};

		// Token: 0x040007CF RID: 1999
		public Vector2 WavePosition;

		// Token: 0x02000190 RID: 400
		public class Wave
		{
			// Token: 0x06000A4C RID: 2636 RVA: 0x00033E85 File Offset: 0x00032085
			public Wave(Vector2 {15784}, double {15785}, double {15786}, double {15787}, double {15788})
			{
				this.direction = {15784};
				this.direction.Normalize();
				this.length = (float){15785};
				this.height = (float){15786};
				this.speed = (float){15787};
				this.heightMofifyMask = (float){15788};
			}

			// Token: 0x06000A4D RID: 2637 RVA: 0x00002EF6 File Offset: 0x000010F6
			public Wave()
			{
			}

			// Token: 0x040007D0 RID: 2000
			public Vector2 direction;

			// Token: 0x040007D1 RID: 2001
			public float length;

			// Token: 0x040007D2 RID: 2002
			public float height;

			// Token: 0x040007D3 RID: 2003
			public float speed;

			// Token: 0x040007D4 RID: 2004
			public float heightMofifyMask;
		}
	}
}
