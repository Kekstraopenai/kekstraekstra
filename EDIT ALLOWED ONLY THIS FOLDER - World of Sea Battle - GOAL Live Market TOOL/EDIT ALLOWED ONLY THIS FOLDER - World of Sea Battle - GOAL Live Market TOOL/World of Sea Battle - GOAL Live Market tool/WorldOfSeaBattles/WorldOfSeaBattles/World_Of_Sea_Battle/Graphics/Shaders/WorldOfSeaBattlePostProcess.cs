using System;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;

namespace World_Of_Sea_Battle.Graphics.Shaders
{
	// Token: 0x0200046B RID: 1131
	internal sealed class WorldOfSeaBattlePostProcess
	{
		// Token: 0x0600189D RID: 6301 RVA: 0x000D46D7 File Offset: 0x000D28D7
		public WorldOfSeaBattlePostProcess()
		{
			this.{23703} = new Tlist<WorldOfSeaBattlePostProcess.GradientAnimation>(3);
			this.{23704} = new Tlist<IntroScreenRenderer>();
			this.{23702} = 1f;
		}

		// Token: 0x0600189E RID: 6302 RVA: 0x000D4704 File Offset: 0x000D2904
		public void Update(ref FrameTime {23696})
		{
			{23696}.EvaluteTimerSec(ref this.{23705});
			this.ColorDistort = this.{23705} / 4f;
			this.{23702} = 1f;
			for (int i = 0; i < this.{23703}.Size; i++)
			{
				WorldOfSeaBattlePostProcess.GradientAnimation gradientAnimation = this.{23703}.Array[i];
				gradientAnimation.TTL += {23696}.msElapsed;
				if (gradientAnimation.IsLighten ? (gradientAnimation.TTL > gradientAnimation.Duration) : (gradientAnimation.TTL > gradientAnimation.Duration * 2f))
				{
					this.{23703}.RemoveAt(i);
					i--;
				}
				else if (gradientAnimation.IsLighten)
				{
					this.{23702} += (gradientAnimation.Duration - gradientAnimation.TTL) / gradientAnimation.Duration * 0.2f;
				}
				else
				{
					this.{23702} *= Math.Abs(gradientAnimation.TTL - gradientAnimation.Duration) / gradientAnimation.Duration;
				}
			}
			for (int j = 0; j < this.{23704}.Size; j++)
			{
				if (this.{23704}.Array[j].Update(ref {23696}))
				{
					this.{23704}.FastRemoveAt(j);
					j--;
				}
			}
			this.ColorMultiplier = new Vector3(this.{23702});
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x000D485C File Offset: 0x000D2A5C
		public void RenderFrontLayer()
		{
			for (int i = 0; i < this.{23704}.Size; i++)
			{
				this.{23704}.Array[i].Render();
			}
		}

		// Token: 0x060018A0 RID: 6304 RVA: 0x000D4891 File Offset: 0x000D2A91
		public void PlayerDeath()
		{
			this.{23705} = 4f;
		}

		// Token: 0x060018A1 RID: 6305 RVA: 0x000D48A0 File Offset: 0x000D2AA0
		public void GradientAnimationBegin(float {23697}, bool {23698}, bool {23699} = true)
		{
			WorldOfSeaBattlePostProcess.GradientAnimation gradientAnimation = new WorldOfSeaBattlePostProcess.GradientAnimation({23697}, {23698});
			this.{23703}.Add(gradientAnimation);
			if (!{23699})
			{
				gradientAnimation.TTL = {23697};
			}
		}

		// Token: 0x060018A2 RID: 6306 RVA: 0x000D48CC File Offset: 0x000D2ACC
		public void RunIntroScreen(IntroScreenRenderer {23700})
		{
			this.{23704}.Add({23700});
		}

		// Token: 0x04001700 RID: 5888
		public float Contrast;

		// Token: 0x04001701 RID: 5889
		public float ColorDistort;

		// Token: 0x04001702 RID: 5890
		public Vector3 ColorMultiplier;

		// Token: 0x04001703 RID: 5891
		public float BloodScreenIntensity;

		// Token: 0x04001704 RID: 5892
		private const float c_playerDeathEffectTime = 4f;

		// Token: 0x04001705 RID: 5893
		private float {23701};

		// Token: 0x04001706 RID: 5894
		private float {23702};

		// Token: 0x04001707 RID: 5895
		private Tlist<WorldOfSeaBattlePostProcess.GradientAnimation> {23703};

		// Token: 0x04001708 RID: 5896
		private Tlist<IntroScreenRenderer> {23704};

		// Token: 0x04001709 RID: 5897
		private float {23705};

		// Token: 0x0400170A RID: 5898
		public float AddBright;

		// Token: 0x0200046C RID: 1132
		private class GradientAnimation
		{
			// Token: 0x060018A3 RID: 6307 RVA: 0x000D48DB File Offset: 0x000D2ADB
			public GradientAnimation(float {23708}, bool {23709})
			{
				this.Duration = {23708};
				this.TTL = 0f;
				this.IsLighten = {23709};
			}

			// Token: 0x0400170B RID: 5899
			public readonly float Duration;

			// Token: 0x0400170C RID: 5900
			public float TTL;

			// Token: 0x0400170D RID: 5901
			public bool IsLighten;
		}
	}
}
