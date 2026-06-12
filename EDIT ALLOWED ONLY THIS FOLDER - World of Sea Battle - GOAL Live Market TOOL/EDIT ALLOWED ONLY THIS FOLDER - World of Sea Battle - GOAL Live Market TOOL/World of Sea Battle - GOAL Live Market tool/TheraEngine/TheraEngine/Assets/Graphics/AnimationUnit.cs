using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using UWContentPipelineExtensionRuntime.Tags;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x020001A4 RID: 420
	public class AnimationUnit
	{
		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x00035046 File Offset: 0x00033246
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0003504E File Offset: 0x0003324E
		public int CyclesPassed { get; private set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x00035057 File Offset: 0x00033257
		public bool IsFinished
		{
			get
			{
				return this.currentClipValue == null || this.currentKeyframe >= this.currentClipValue.Keyframes.Count;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000A9B RID: 2715 RVA: 0x0003507E File Offset: 0x0003327E
		private TimeSpan StartTime
		{
			get
			{
				if (this.Labels.CurrentTime == null)
				{
					return TimeSpan.Zero;
				}
				return this.Labels.CurrentTimeItem1;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000A9C RID: 2716 RVA: 0x000350A3 File Offset: 0x000332A3
		private TimeSpan EndTime
		{
			get
			{
				if (this.Labels.CurrentTime == null)
				{
					return this.currentClipValue.Duration;
				}
				return this.Labels.CurrentTimeItem2;
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000350D0 File Offset: 0x000332D0
		public AnimationUnit(SkinningData {15933})
		{
			if ({15933} == null)
			{
				throw new ArgumentNullException("skinningData");
			}
			this.{15944} = {15933};
			this.{15940} = new Matrix[{15933}.BindPose.Count];
			this.Labels = new AnimationLabelManager(this);
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00035134 File Offset: 0x00033334
		public void StartClip(AnimationClip {15934}, TimeSpan? {15935} = null)
		{
			if (this.currentClipValue != null && this.currentKeyframe != 0 && this.{15941} == null)
			{
				this.{15941} = new Matrix[this.{15940}.Length];
				this.{15940}.CopyTo(this.{15941}, 0);
				this.{15942} = 1f;
				this.{15943} = (float)((this.currentClipValue != {15934}) ? 800 : 500);
			}
			if ({15934} == null)
			{
				throw new ArgumentNullException("clip");
			}
			this.currentClipValue = {15934};
			this.currentTimeValue = ({15935} ?? TimeSpan.Zero);
			this.currentKeyframe = 0;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000351E1 File Offset: 0x000333E1
		public void Update(TimeSpan {15936}, bool {15937})
		{
			this.UpdateBoneTransforms({15936}, {15937});
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000351EC File Offset: 0x000333EC
		public void UpdateBoneTransforms(TimeSpan {15938}, bool {15939})
		{
			if (this.currentClipValue == null)
			{
				throw new InvalidOperationException("AnimationPlayer.Update was called before StartClip");
			}
			{15938} = new TimeSpan((long)((float){15938}.Ticks * this.TimeScale));
			if ({15939})
			{
				{15938} += this.currentTimeValue;
				while ({15938} >= this.EndTime)
				{
					int cyclesPassed = this.CyclesPassed;
					this.CyclesPassed = cyclesPassed + 1;
					{15938} -= this.EndTime - this.StartTime;
				}
			}
			if ({15938} < TimeSpan.Zero || {15938} >= this.EndTime)
			{
				throw new ArgumentOutOfRangeException("time");
			}
			List<Keyframe> keyframes = this.currentClipValue.Keyframes;
			if ({15938} < this.currentTimeValue)
			{
				while (this.currentKeyframe > 0)
				{
					this.currentKeyframe--;
					Keyframe keyframe = keyframes[this.currentKeyframe];
					this.{15940}[keyframe.Bone] = keyframe.Transform;
					if ({15938} < keyframe.Time)
					{
						this.currentKeyframe++;
						break;
					}
				}
			}
			if (this.{15942} > 0f)
			{
				this.{15942} -= Engine.Game.GameTime.ElapsedDrawReal / this.{15943};
				if (this.{15942} <= 0f)
				{
					this.{15941} = null;
					this.{15942} = 0f;
				}
			}
			this.currentTimeValue = {15938};
			while (this.currentKeyframe < keyframes.Count)
			{
				Keyframe keyframe2 = keyframes[this.currentKeyframe];
				if (keyframe2.Time > this.currentTimeValue)
				{
					break;
				}
				this.{15940}[keyframe2.Bone] = keyframe2.Transform;
				this.currentKeyframe++;
			}
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000353B4 File Offset: 0x000335B4
		public Matrix[] GetBoneTransforms()
		{
			if (this.{15942} > 0f)
			{
				for (int i = 0; i < this.{15940}.Length; i++)
				{
					Matrix.Lerp(ref this.{15940}[i], ref this.{15941}[i], this.{15942}, out AnimationUnit.tempArray[i]);
				}
				return AnimationUnit.tempArray;
			}
			return this.{15940};
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0003541C File Offset: 0x0003361C
		public Matrix[] GetSkinTransforms()
		{
			Matrix[] boneTransforms = this.GetBoneTransforms();
			AnimationUnit.worldTransformsStatic[0] = boneTransforms[0] * this.RootTransform;
			for (int i = 1; i < this.{15944}.BindPose.Count; i++)
			{
				int num = this.{15944}.SkeletonHierarchy[i];
				Matrix.Multiply(ref boneTransforms[i], ref AnimationUnit.worldTransformsStatic[num], out AnimationUnit.worldTransformsStatic[i]);
			}
			for (int j = 0; j < this.{15944}.BindPose.Count; j++)
			{
				Matrix matrix = this.{15944}.InverseBindPose[j];
				Matrix.Multiply(ref matrix, ref AnimationUnit.worldTransformsStatic[j], out AnimationUnit.skinTransformsStatic[j]);
			}
			return AnimationUnit.skinTransformsStatic;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x000354EC File Offset: 0x000336EC
		public AnimationClip CurrentClip
		{
			get
			{
				return this.currentClipValue;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x000354F4 File Offset: 0x000336F4
		public TimeSpan CurrentTime
		{
			get
			{
				return this.currentTimeValue;
			}
		}

		// Token: 0x04000824 RID: 2084
		private const int maxBones = 50;

		// Token: 0x04000825 RID: 2085
		private static Matrix[] worldTransformsStatic = new Matrix[50];

		// Token: 0x04000826 RID: 2086
		private static Matrix[] skinTransformsStatic = new Matrix[50];

		// Token: 0x04000827 RID: 2087
		private static Matrix[] tempArray = new Matrix[50];

		// Token: 0x04000828 RID: 2088
		internal AnimationClip currentClipValue;

		// Token: 0x04000829 RID: 2089
		internal TimeSpan currentTimeValue;

		// Token: 0x0400082A RID: 2090
		internal int currentKeyframe;

		// Token: 0x0400082B RID: 2091
		private Matrix[] {15940};

		// Token: 0x0400082C RID: 2092
		private Matrix[] {15941};

		// Token: 0x0400082D RID: 2093
		private float {15942};

		// Token: 0x0400082E RID: 2094
		private float {15943};

		// Token: 0x0400082F RID: 2095
		private SkinningData {15944};

		// Token: 0x04000830 RID: 2096
		public float TimeScale = 1f;

		// Token: 0x04000831 RID: 2097
		public Matrix RootTransform = Matrix.Identity;

		// Token: 0x04000832 RID: 2098
		[CompilerGenerated]
		private int {15945};

		// Token: 0x04000833 RID: 2099
		public readonly AnimationLabelManager Labels;
	}
}
