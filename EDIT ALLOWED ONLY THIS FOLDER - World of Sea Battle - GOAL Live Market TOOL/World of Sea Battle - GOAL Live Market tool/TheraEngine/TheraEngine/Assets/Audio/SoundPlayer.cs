using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using TheraEngine.Graphics;
using TheraEngine.Helpers;

namespace TheraEngine.Assets.Audio
{
	// Token: 0x020001BC RID: 444
	internal class SoundPlayer
	{
		// Token: 0x06000AF7 RID: 2807 RVA: 0x00036C9F File Offset: 0x00034E9F
		public static void ApplyBackground(float {16116}, SoundEffectInstance {16117})
		{
			{16117}.Volume = {16116};
			{16117}.Play();
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x00036CAE File Offset: 0x00034EAE
		private static bool isBrokenValue(float {16118})
		{
			return float.IsNaN({16118}) || float.IsPositiveInfinity({16118}) || float.IsNegativeInfinity({16118}) || float.IsInfinity({16118});
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x00036CD0 File Offset: 0x00034ED0
		public static void Apply3D(SoundEffect {16119}, float {16120}, SoundEffectInstance {16121}, Camera {16122}, ref Vector3 {16123}, SoundOptions {16124} = SoundOptions.None)
		{
			if ({16120} <= 0f || SoundPlayer.isBrokenValue({16120}) || {16120} > 100f || SoundPlayer.isBrokenValue({16123}.X) || SoundPlayer.isBrokenValue({16123}.Y) || SoundPlayer.isBrokenValue({16123}.Z) || {16123} == {16122}.Position)
			{
				return;
			}
			{16121}.Volume = {16120};
			SoundPlayer.listener.Position = {16122}.Position;
			SoundPlayer.listener.Forward = {16122}.Direction;
			SoundPlayer.listener.Up = Vector3.Up;
			SoundPlayer.listener.Velocity = default(Vector3);
			SoundPlayer.emmiter.Position = {16123};
			SoundPlayer.emmiter.Up = Vector3.Zero;
			SoundPlayer.emmiter.Forward = Vector3.Zero;
			SoundEffect.DistanceScale = 19.23077f;
			float num = ({16124} == SoundOptions.DisableDistanceDeformation) ? 0f : Geometry.Saturate((({16122}.Position - {16123}).Length() - 50f) / 250f);
			if (num > 0f)
			{
				num = MathF.Pow(num, 1.5f);
				SoundPlayer.emmiter.DopplerScale = 100f * num;
				SoundPlayer.emmiter.Velocity = -({16122}.Position - {16123}).Normal();
			}
			else
			{
				SoundPlayer.emmiter.Velocity = Vector3.Zero;
			}
			{16121}.Apply3D(SoundPlayer.listener, SoundPlayer.emmiter);
			{16121}.Play(0f);
		}

		// Token: 0x0400089E RID: 2206
		private static AudioEmitter emmiter = new AudioEmitter();

		// Token: 0x0400089F RID: 2207
		private static AudioListener listener = new AudioListener();
	}
}
