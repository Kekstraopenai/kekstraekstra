using System;
using Common.Game;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;

namespace World_Of_Sea_Battle.Graphics.Shaders
{
	// Token: 0x02000468 RID: 1128
	public class WeatherManager : IMPSerializable
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06001883 RID: 6275 RVA: 0x000D4389 File Offset: 0x000D2589
		public float IsDay
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06001884 RID: 6276 RVA: 0x000D4390 File Offset: 0x000D2590
		public Vector3 CycleLightDirection
		{
			get
			{
				Vector3? overrideLightDirection = this.OverrideLightDirection;
				if (overrideLightDirection == null)
				{
					return this.SunLightDirectionForRendering;
				}
				return overrideLightDirection.GetValueOrDefault();
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06001885 RID: 6277 RVA: 0x000D43BC File Offset: 0x000D25BC
		public Vector3 CurrentLightDirection
		{
			get
			{
				Vector3? overrideLightDirection = this.OverrideLightDirection;
				if (overrideLightDirection != null)
				{
					return overrideLightDirection.GetValueOrDefault();
				}
				if (this.SunLightDirectionForRendering.Y >= -0.14f)
				{
					return -this.SunLightDirectionForRendering;
				}
				return this.SunLightDirectionForRendering;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x06001886 RID: 6278 RVA: 0x000D4408 File Offset: 0x000D2608
		public Vector3 CurrentLightDirectionStepped
		{
			get
			{
				Vector3? overrideLightDirection = this.OverrideLightDirection;
				if (overrideLightDirection != null)
				{
					return overrideLightDirection.GetValueOrDefault();
				}
				if (this.SunLightDirectionForRendering.Y >= -0.14f)
				{
					return -this.SunLightDirection;
				}
				return this.SunLightDirection;
			}
		}

		// Token: 0x06001887 RID: 6279 RVA: 0x000D4454 File Offset: 0x000D2654
		public void Update(ref FrameTime {23675}, Player {23676} = null)
		{
			this.WorldTime += {23675}.msElapsed * this.TimeSpeed;
			if (this.WorldTime > 24f)
			{
				this.WorldTime -= 24f;
			}
			float num;
			if (this.WorldTime > 6f && this.WorldTime < 21.5f)
			{
				num = -(this.WorldTime - 6f) / 15.5f * 3.1415927f;
			}
			else if (this.WorldTime < 6f)
			{
				num = 3.1415927f - (this.WorldTime / 6f * 6f / 8.5f + 0.29411766f) * 3.1415927f;
			}
			else
			{
				num = 3.1415927f - (this.WorldTime - 21.5f) / 2.5f * 2.5f / 8.5f * 3.1415927f;
			}
			Vector3 vector = -Vector3.Transform(Vector3.Up, Matrix.CreateRotationX((float)(Math.Round((double)(num * 100f)) / 100.0) - 1.5707964f));
			Vector3 vector2 = -Vector3.Transform(Vector3.Up, Matrix.CreateRotationX(num - 1.5707964f));
			vector = this.OverrideLightDirection.GetValueOrDefault(vector);
			vector2 = this.OverrideLightDirection.GetValueOrDefault(vector2);
			vector.Z += 0.5f * vector.Y;
			vector.Normalize();
			vector2.Z += 0.5f * vector2.Y;
			vector2.Normalize();
			this.SunLightDirection = vector;
			this.SunLightDirectionForRendering = vector2;
			this.MoonLightDirection = -vector;
			this.MoonLightDirectionForRendering = -vector2;
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x000D4602 File Offset: 0x000D2802
		void IMPSerializable.{23677}(WriterExtern {23678})
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001889 RID: 6281 RVA: 0x000D4602 File Offset: 0x000D2802
		void IMPSerializable.{23679}(WriterExtern {23680})
		{
			throw new NotImplementedException();
		}

		// Token: 0x040016E7 RID: 5863
		public const float horizon1 = 6f;

		// Token: 0x040016E8 RID: 5864
		public const float horizon2 = 21.5f;

		// Token: 0x040016E9 RID: 5865
		public float WorldTime;

		// Token: 0x040016EA RID: 5866
		public float TimeSpeed;

		// Token: 0x040016EB RID: 5867
		public Vector3? OverrideLightDirection;

		// Token: 0x040016EC RID: 5868
		public Vector3 SunLightDirection;

		// Token: 0x040016ED RID: 5869
		public Vector3 SunLightDirectionForRendering;

		// Token: 0x040016EE RID: 5870
		public Vector3 MoonLightDirection;

		// Token: 0x040016EF RID: 5871
		public Vector3 MoonLightDirectionForRendering;

		// Token: 0x040016F0 RID: 5872
		private WeatherSample {23681};

		// Token: 0x040016F1 RID: 5873
		private Tlist<WeatherArea> {23682};
	}
}
