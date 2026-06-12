using System;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.GameObjects
{
	// Token: 0x02000040 RID: 64
	internal sealed class ClientPowderKeg : PowderKeg, IPoolObject
	{
		// Token: 0x060001DC RID: 476 RVA: 0x0000EE75 File Offset: 0x0000D075
		void IPoolObject.{16621}()
		{
			this.{16627} = 1500f;
			this.{16629} = false;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000EE8C File Offset: 0x0000D08C
		public void InitializeAnimation(Ship {16622})
		{
			if (this.Ttl - this.TtlToEnd > 1000f || this.TtlToEnd < 500f)
			{
				return;
			}
			this.{16626} = Rand.NextRadialVector2(3f, 5f);
			this.{16625} = {16622}.PowderKegStartPosition;
			this.{16627} = 0f;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000EEE8 File Offset: 0x0000D0E8
		public override bool Update(ref FrameTime {16623})
		{
			if (this.{16627} < 1500f && this.{16627} + {16623}.msElapsed > 1500f)
			{
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.Slamming_Big, this.Position.X0Y(), 1f, false);
				FXEngine.NewWaterSplash(this.Position, 1.4f, false);
			}
			this.{16627} += {16623}.msElapsed * (this.Info.IsInvisibleMine ? 0.7f : 1f);
			if (!this.Info.IsInvisibleMine && Rand.Chanse(16f * {16623}.Factor))
			{
				this.{16624}();
				FXEngine.SampleFumesSmoke(this.{16628}.Transform.Translation + Rand.NextVector3(-0.4f, 0.4f), 0.15f, 0.75f, 0.7f);
			}
			if (base.ShouldExplodeAfterTimeFinish && this.TtlToEnd < 1700f && this.TtlToEnd + {16623}.msElapsed >= 1700f)
			{
				this.{16629} = true;
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.PowderKegWick, new Vector3(this.Position.X, 0f, this.Position.Y), 1f, false);
			}
			if (!this.Info.IsInvisibleMine && this.{16629} && Rand.Chanse(8f * {16623}.Factor))
			{
				this.{16624}();
				FXEngine.SampleFlameAndSmoke(this.{16628}.Transform.Translation + Rand.NextVector3(-0.5f, 0.5f), 0.2f, true, false, true, null, 1f);
			}
			base.Update(ref {16623});
			return false;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		public void Render3D()
		{
			if (this.Info.IsInvisibleMine && this.{16627} > 1500f)
			{
				return;
			}
			this.{16624}();
			Global.Render.CommonShader.RenderObject(this.{16628}, false, 1f, false, 0f, false);
			float num = (this.{16627} < 1500f) ? (this.{16627} / 1500f) : 1f;
			float num2 = (1f - num) * num * 4f * 0.5f * Geometry.Saturate(Vector2.Distance(this.Position, Global.Camera.Position.XZ()) / 50f);
			if (num2 > 0f)
			{
				Global.Render.CommonShader.RenderOutline(this.{16628}, Color.OrangeRed.ToVector4() * num2);
			}
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00003100 File Offset: 0x00001300
		public void RenderStatics()
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000F194 File Offset: 0x0000D394
		private void {16624}()
		{
			ModelTransformedScene modelTransformedScene;
			if (this.Info.ID != 1)
			{
				if (this.Info.ID != 2)
				{
					if (this.Info.ID != 3)
					{
						if (this.Info.ID != 4)
						{
							if (this.Info.ID != 5)
							{
								if (this.Info.ID != 6)
								{
									if (this.Info.ID != 7)
									{
										if (this.Info.ID != 8)
										{
											throw new NotSupportedException();
										}
										modelTransformedScene = LocalContent.Loaded.powderKegPackByModelNumber[5];
									}
									else
									{
										modelTransformedScene = LocalContent.Loaded.powderKegPackByModelNumber[0];
									}
								}
								else
								{
									modelTransformedScene = LocalContent.Loaded.powderKegPackByModelNumber[4];
								}
							}
							else
							{
								modelTransformedScene = LocalContent.Loaded.powderKegPackByModelNumber[4];
							}
						}
						else
						{
							modelTransformedScene = LocalContent.Loaded.powderKegPackByModelNumber[3];
						}
					}
					else
					{
						modelTransformedScene = LocalContent.Loaded.powderKegPackByModelNumber[2];
					}
				}
				else
				{
					modelTransformedScene = LocalContent.Loaded.powderKegPackByModelNumber[1];
				}
			}
			else
			{
				modelTransformedScene = LocalContent.Loaded.powderKegPackByModelNumber[0];
			}
			this.{16628} = modelTransformedScene;
			float num = (this.{16627} < 1500f) ? (this.{16627} / 1500f) : 1f;
			Vector3 vector = CommonGlobal.CurrentClientWeather.NormalOnlyHelper(Global.Player.MapInfo, this.Position.X, this.Position.Y);
			this.{16628}.Transform.Translation.X = this.Position.X * num + this.{16625}.X * (1f - num);
			this.{16628}.Transform.Translation.Z = this.Position.Y * num + this.{16625}.Z * (1f - num);
			this.{16628}.Transform.Translation.Y = (CommonGlobal.CurrentClientWeather.HeightOnlyHelper(Global.Player.MapInfo, this.Position.X, this.Position.Y) - (float)(this.Info.IsInvisibleMine ? 3 : 0) - (1f - MathHelper.Clamp(this.TtlToEnd / 4000f, 0f, 1f)) * 7f) * num * 0f + this.{16625}.Y * (1f - num) + num * (1f - num) * 4f * 2f;
			this.{16628}.Transform.Pitch = vector.X * 2f + this.{16626}.X * (1f - num);
			this.{16628}.Transform.Roll = vector.Z * 2f + this.{16626}.Y * (1f - num);
		}

		// Token: 0x04000175 RID: 373
		private Vector3 {16625};

		// Token: 0x04000176 RID: 374
		private Vector2 {16626};

		// Token: 0x04000177 RID: 375
		private float {16627} = 1500f;

		// Token: 0x04000178 RID: 376
		private ModelTransformedScene {16628};

		// Token: 0x04000179 RID: 377
		private bool {16629};
	}
}
