using System;
using Common;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Graphics;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Interface;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000549 RID: 1353
	internal class GameCamera : FollowCamera
	{
		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06001EB7 RID: 7863 RVA: 0x00112A43 File Offset: 0x00110C43
		// (set) Token: 0x06001EB8 RID: 7864 RVA: 0x00112A4B File Offset: 0x00110C4B
		public bool EnabledMouseRead
		{
			get
			{
				return this.{25803};
			}
			set
			{
				if (!this.{25803} && value)
				{
					this.ResetState(false);
				}
				this.{25803} = value;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06001EB9 RID: 7865 RVA: 0x00112A68 File Offset: 0x00110C68
		public float ZoomFactor
		{
			get
			{
				float num = this.Zoom - 3.5f;
				float num2 = 18.5f;
				return 1f - num / num2;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06001EBA RID: 7866 RVA: 0x00112A91 File Offset: 0x00110C91
		public bool IsMinZoom
		{
			get
			{
				return this.Zoom - 1f < 3.5f && this.{25805} != null && !this.{25805}.UsedShip.StaticInfo.IsBalloon;
			}
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001EBB RID: 7867 RVA: 0x00112AC8 File Offset: 0x00110CC8
		public float SightingAxisOffset
		{
			get
			{
				float result = (1f - (this.Zoom - 3.5f) / 18.5f) * 3.1415927f * 0.025f;
				if (this.IsSpyglass)
				{
					result = 0f;
				}
				return result;
			}
		}

		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001EBC RID: 7868 RVA: 0x00112B09 File Offset: 0x00110D09
		public GameCameraEffects CameraEffects
		{
			get
			{
				return this.{25812};
			}
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x00112B14 File Offset: 0x00110D14
		private float spygllassEffectValue
		{
			get
			{
				if (!Global.Settings.SpyglassAnimation)
				{
					return this.IsSpyglass > false;
				}
				if (!this.IsSpyglass)
				{
					return this.{25813}.CurrentSoftValueSmoothstep * 0.25f;
				}
				return this.{25813}.CurrentSoftValueSmoothstep * 0.5f + 0.5f;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x00112B69 File Offset: 0x00110D69
		public bool EnableMouseByActivation
		{
			get
			{
				return this.{25804} == 0;
			}
		}

		// Token: 0x06001EBF RID: 7871 RVA: 0x00112B74 File Offset: 0x00110D74
		public GameCamera() : base(Renderer.CameraNearPlane, Renderer.CameraFarPlane, 55f)
		{
			this.Zoom = 14f;
			this.{25809} = 14f;
			this.{25803} = false;
			this.EnabledMouseZoom = true;
			this.{25812} = new GameCameraEffects();
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x00112BF8 File Offset: 0x00110DF8
		public override void Update(ref FrameTime {25777})
		{
			try
			{
				if (!this.IsFreeMode)
				{
					if (Global.Settings.kb_Spyglass.IsClick && GameScene.GameHasInputFocus && Global.Game.GetCurrentSceneName == GameSceneName.Game)
					{
						Global.Network.Send(new OnSpyglassStatusMsg(true));
						this.IsSpyglass = true;
						this.{25810} = this.Zoom;
						this.Zoom = this.{25811};
						this.{25809} = this.Zoom;
						this.{25817} = this.rotates.X;
						this.rotates.X = 0f;
						Global.Game.AdaptiveUiExtraScale *= 1.2f;
					}
					if (this.IsSpyglass)
					{
						this.{25811} = this.Zoom;
						this.{25816} = 10000f;
						if (Global.Settings.kb_Spyglass.IsRelease || Global.Game.GetCurrentSceneName != GameSceneName.Game)
						{
							Global.Network.Send(new OnSpyglassStatusMsg(false));
							this.IsSpyglass = false;
							this.Zoom = this.{25810};
							this.{25809} = this.Zoom;
							this.rotates.X = this.{25817};
							Global.Settings.UiScale = Global.Settings.UiScale;
						}
					}
					else if ({25777}.EvaluteTimerMs2(ref this.{25816}))
					{
						this.{25811} = 14f;
					}
				}
				this.{25813}.Evalute(ref {25777}, this.IsSpyglass);
				if (Global.Player == null)
				{
					this.CurrentFov = 55f;
				}
				else
				{
					float num = MathHelper.Lerp(50f, 43f, Geometry.Saturate(4.5f - this.Zoom) * !this.IsFreeMode);
					this.CurrentFov = MathHelper.Lerp(num + (55f - num) * (1f - Geometry.Saturate(this.ZoomFactor)), 3.8500001f + 21.5f * (1f - Geometry.Saturate(MathF.Pow(Math.Max(0f, this.ZoomFactor), 0.7f)) / 0.88f), this.spygllassEffectValue);
					this.CurrentFov -= 1f * Geometry.Saturate(this.CurrentFov / 10f - 1f);
					if (Global.Player.IsPortEntry)
					{
						this.CurrentFov -= 5f;
					}
				}
				this.CurrentFov += this.{25812}.FovOffset;
				base.BuildProjMatrix(1f, Renderer.CameraFarPlane, this.CurrentFov);
				this.AxisOffset.X = this.SightingAxisOffset;
				if (this.{25808})
				{
					float num2 = this.{25805}.UsedShip.StaticInfo.BSRadius + 2f;
					Geometry.Evalute(ref this.Zoom, num2, {25777}.secElapsed * (1f + Math.Abs(this.Zoom - num2)) * 2f);
					this.{25808} = (this.Zoom != num2);
					this.{25809} = this.Zoom;
				}
				float num3 = (this.{25805} == null) ? 1f : (Math.Min(1.05f, Math.Max(3.3f, this.{25805}.UsedShip.StaticInfo.CorpusHalfLength) / 5.3f) * 1.5600001f * 1.05f);
				if (this.{25803} && Global.Game.IsActive)
				{
					if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
					{
						if (InputHelper.LastMouseState.RightPressed || InputHelper.LastMouseState.LeftPressed)
						{
							if (!this.{25800})
							{
								this.{25801} = InputHelper.NowMouseState.Position;
							}
							this.{25800} = true;
						}
					}
					else
					{
						this.{25801} = Global.Game.XScreenIntCenter;
					}
					Vector2 vector = this.{25801} - InputHelper.NowMouseState.Position;
					if (this.{25804} > 0)
					{
						vector = default(Vector2);
					}
					if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
					{
						this.{25801} -= vector * 0.05f;
					}
					if (float.IsNaN(vector.X) || float.IsNaN(vector.Y))
					{
						vector = default(Vector2);
					}
					else if (UiControl.EditorMode == null)
					{
						Mouse.SetPosition((int)this.{25801}.X, (int)this.{25801}.Y);
						MouseState state = Mouse.GetState();
						if (Math.Abs(state.X - (int)this.{25801}.X) >= 2 || Math.Abs(state.Y - (int)this.{25801}.Y) >= 2)
						{
							vector = default(Vector2);
						}
					}
					vector *= -Global.Settings.MouseSensetivity * 2f;
					Vector2 vector2 = (vector + this.{25814} + this.{25815}) / 3f;
					vector2 *= MathHelper.Lerp(1f, 0.01f, Math.Max(0.25f * this.spygllassEffectValue, this.spygllassEffectValue * this.ZoomFactor));
					this.{25814} = this.{25815};
					this.{25815} = vector;
					this.{25799}.X = this.{25799}.X - vector2.Y / 2f * (this.IsMinZoom ? 0.66f : 1f) * (float)Global.Settings.MouseControlInversion.Y;
					this.{25799}.Y = this.{25799}.Y + vector2.X / 2f * (this.IsMinZoom ? 0.66f : 1f) * (float)Global.Settings.MouseControlInversion.X;
					bool flag = true;
					if (!this.{25808} && this.EnabledMouseZoom && flag)
					{
						float num4 = (float)InputHelper.NowMouseState.ScrollValue;
						float num5 = num4 - this.{25802};
						if (num5 != 0f && !{18560}.disallowCamZoom)
						{
							if (this.IsFreeMode)
							{
								if (num5 > 0f)
								{
									this.{25807} *= 1.2f;
								}
								else
								{
									this.{25807} /= 1.2f;
								}
							}
							else
							{
								this.{25809} -= (float)Math.Sign(num5) * 16f / 1000f * 112f * num3 * MathHelper.Clamp(this.Zoom / (14f * num3), 0.8f, 1.35f);
							}
						}
						this.{25802} = num4;
					}
					FreeCameraMode freeCameraMode = (Session.Account != null) ? FreeCameraMode.AvailablePartial : FreeCameraMode.Disallow;
					if (Global.Settings.kb_FreeCamera.IsClick && freeCameraMode != FreeCameraMode.Disallow && !this.IsSpyglass)
					{
						if (this.IsFreeMode)
						{
							this.IsFreeMode = false;
							this.freeCameraPosition = default(Vector3);
							this.freeCameraPositionSmooth = default(Vector3);
							this.{25807} = 1f;
						}
						else
						{
							this.IsFreeMode = true;
							this.freeCameraPosition = this.TargetObjectPosition;
							this.freeCameraPositionSmooth = this.TargetObjectPosition;
							this.{25807} = 1f;
						}
					}
					if (this.IsFreeMode)
					{
						this.Zoom = 0f;
						this.{25809} = 0f;
						Vector3 vector3 = this.direction * 0.15289909f;
						if (InputHelper.NowInputState.IsDown(Keys.W))
						{
							this.{25806} += vector3;
						}
						if (InputHelper.NowInputState.IsDown(Keys.S))
						{
							this.{25806} -= vector3;
						}
						if (InputHelper.NowInputState.IsDown(Keys.D))
						{
							this.{25806} += Vector3.Transform(new Vector3(vector3.X, 0f, vector3.Z), Matrix.CreateRotationY(-1.5707964f));
						}
						if (InputHelper.NowInputState.IsDown(Keys.A))
						{
							this.{25806} += Vector3.Transform(new Vector3(vector3.X, 0f, vector3.Z), Matrix.CreateRotationY(1.5707964f));
						}
						if (InputHelper.NowInputState.IsDown(Keys.R) && {18560}.closed)
						{
							this.{25806} += new Vector3(0f, 0.15289909f, 0f);
						}
						if ((double)this.{25806}.Length() > Math.Sqrt(3.0))
						{
							this.{25806}.Normalize();
						}
						this.{25806} /= 1.1f;
						this.freeCameraPositionSmooth += this.{25806} * this.{25807} * {25777}.secElapsed / 0.016666f * 0.5f;
						this.freeCameraPosition += (this.freeCameraPositionSmooth - this.freeCameraPosition) * {25777}.secElapsed * 2f;
						if (!{18560}.closed || Global.Settings.DisableSmoothCamera)
						{
							this.freeCameraPosition = this.freeCameraPositionSmooth;
						}
						if (freeCameraMode == FreeCameraMode.AvailablePartial)
						{
							if (Global.Player == null || Global.Game.GetCurrentSceneName != GameSceneName.Game)
							{
								this.IsFreeMode = false;
							}
							else
							{
								this.freeCameraPosition.Y = Math.Max(this.freeCameraPosition.Y, CommonGlobal.CurrentClientWeather.WavesHeightClient * 1.5f + 0.5f);
								this.freeCameraPosition.Y = Math.Min(this.freeCameraPosition.Y, 30f);
								Vector3 {11448} = Global.Player.Position3D - this.freeCameraPosition;
								this.freeCameraPosition = Global.Player.Position3D - {11448}.Normal() * Math.Min(180f, {11448}.Length());
							}
						}
						this.freeCameraPositionSmooth.Y = Math.Max(CommonGlobal.CurrentClientWeather.HeightOnlyHelper((Global.Player == null) ? Gameplay.MapsForPassing.Array[0] : Global.Player.MapInfo, this.freeCameraPositionSmooth.X, this.freeCameraPositionSmooth.Z) - 1.5f + this.rotates.X * 2f + 0.25f, this.freeCameraPositionSmooth.Y);
					}
				}
				if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					this.{25804} = 0;
				}
				else if (this.{25803} && Global.Game.IsActive)
				{
					if (!InputHelper.NowMouseState.LeftPressed && !InputHelper.NowMouseState.RightPressed)
					{
						this.{25804} = Math.Max(0, this.{25804} - 1);
					}
				}
				else
				{
					this.{25804} = 3;
				}
				float num6 = Math.Min(4.49f, 3.5f * num3);
				float max = 22f * num3;
				this.{25809} = MathHelper.Clamp(this.{25809}, num6, max);
				if (this.Zoom != this.{25809})
				{
					Geometry.Evalute(ref this.Zoom, this.{25809}, {25777}.secElapsed * 10f * (0.1f + Math.Abs(this.{25809} - this.Zoom)));
				}
				float num7 = (Global.Game.GetCurrentSceneName == GameSceneName.Port) ? 1.01f : 0f;
				this.Zoom = MathHelper.Clamp(this.Zoom, num6 + num7, max);
				if (float.IsInfinity(this.{25799}.X) || float.IsInfinity(this.{25799}.Y))
				{
					this.{25799} = default(Vector2);
				}
				if ({18560}.closed)
				{
					this.rotates.X = MathHelper.Clamp(this.rotates.X + this.{25799}.X * 0.006f * 0.016666f * 2f * {25777}.msElapsed / 16.6666f, -1.2566371f, 0.22f);
				}
				else
				{
					this.rotates.X = this.rotates.X + this.{25799}.X * 0.006f * 0.016666f * 2f * {25777}.msElapsed / 16.6666f;
				}
				this.rotates.Y = Geometry.AxisNorm(this.rotates.Y + this.{25799}.Y * 0.006f * 0.016666f * 2f * {25777}.msElapsed / 16.6666f);
				if (!this.IsFreeMode && this.{25805} != null)
				{
					if (Global.Settings.HorizonTilt)
					{
						float num8 = this.{25805}.Transform.Pitch * 0.3f;
						float num9 = Math.Abs(this.rotates.Z - num8);
						Geometry.AngularMovement(ref this.rotates.Z, num8, num9 * {25777}.secElapsed * 0.4f);
					}
					else
					{
						this.rotates.Z = 0f;
					}
					this.{25778}();
				}
				{25777}.Multiply(ref this.{25799}.X, 0.85131615f);
				{25777}.Multiply(ref this.{25799}.Y, 0.85131615f);
				if ((double)Math.Abs(this.{25799}.X) < 0.005)
				{
					this.{25799}.X = 0f;
				}
				if ((double)Math.Abs(this.{25799}.Y) < 0.005)
				{
					this.{25799}.Y = 0f;
				}
				if (this.IsSpyglass)
				{
					float num10 = MathHelper.Clamp(this.rotates.X, -0.075f, 0.05f);
					float num11 = 1f;
					{25777}.Multiply(ref num11, Math.Max(0f, 0.7f - Math.Abs(num10 - this.rotates.X)));
					this.rotates.X = MathHelper.Lerp(this.rotates.X, num10, 1f - num11);
					this.Zoom = Math.Max(5.5f, this.Zoom);
					this.Zoom = Math.Min(15f, this.Zoom);
					this.{25809} = Math.Min(15f, this.{25809});
					this.SpecialOffset = Vector3.Zero;
				}
				if (this.SpecialOffset.X + this.SpecialOffset.Y + this.SpecialOffset.Z != 0f)
				{
					this.SpecialOffset *= 0.98f;
					if (this.SpecialOffset.Length() < 0.01f)
					{
						this.SpecialOffset = Vector3.Zero;
					}
				}
				if (this.{25805} != null)
				{
					this.{25812}.Update(ref {25777}, this.{25805});
				}
				else
				{
					this.{25812}.StopEffects();
				}
				this.BuildPropertiesFollowCamera(CameraBuildOptions.Default);
				base.Update(ref {25777});
			}
			catch (NullReferenceException)
			{
				int num12 = this.{25818};
				this.{25818} = num12 + 1;
				if (num12 >= 3)
				{
					throw;
				}
			}
		}

		// Token: 0x06001EC1 RID: 7873 RVA: 0x00113B18 File Offset: 0x00111D18
		private void {25778}()
		{
			ShipStaticInfo staticInfo = this.{25805}.UsedShip.StaticInfo;
			ShipStaticInfo shipStaticInfo = Gameplay.ShipsStaticInfo.FromID(2);
			float num = Math.Min(9f, staticInfo.BSRadius);
			float num2 = staticInfo.CorpusHalfLength / 7f * 0.55f;
			float num3 = (staticInfo.CorpusShape.LocalCenter.Y + staticInfo.CorpusShape.FinalHeight * 0.5f) * 0.3f * 0.8f;
			float num4 = (shipStaticInfo.CorpusShape.LocalCenter.Y + shipStaticInfo.CorpusShape.FinalHeight * 0.5f) * 0.3f * 0.8f;
			if (staticInfo.ID == 58)
			{
				num4 += (InputHelper.GraphicsTestKeyDown ? (4f * Math.Max(0f, this.ZoomFactor)) : 0f);
			}
			float scaleFactor = staticInfo.CorpusShape.LocalCenter.X * 0.3f * (this.IsMinZoom ? 1f : 0.5f);
			Vector2 value = base.Direction.XZNormal();
			Vector2 normal = this.{25805}.Normal;
			float num5 = MathHelper.Clamp(Vector2.Dot(normal, value), -1f, 1f);
			float num6 = Math.Abs(num5);
			bool flag = false;
			if (this.IsMinZoom)
			{
				bool graphicsTestKeyDown = InputHelper.GraphicsTestKeyDown;
				this.OffsetOfTarget.X = 0f;
				this.OffsetOfTarget.Z = 0f;
				float num7 = staticInfo.CorpusShape.FinalLength / 6f;
				float num8 = staticInfo.CorpusShape.FinalWidth / 5f;
				float x = Geometry.VectorizedAngle2DAspectRationLerp(MathF.Acos(num5), num8, num7);
				this.OffsetOfTarget.X = this.OffsetOfTarget.X + normal.X * (float)Math.Sign(num5) * num6 * num7 * 0.3f;
				this.OffsetOfTarget.Z = this.OffsetOfTarget.Z + normal.Y * (float)Math.Sign(num5) * num6 * num7 * 0.3f;
				this.OffsetOfTarget.X = this.OffsetOfTarget.X + normal.X * (float)Math.Sign(num5) * num6 * num6 * num7 * 0.5f * ((num5 > 0f) ? 1.15f : 1.3f);
				this.OffsetOfTarget.Z = this.OffsetOfTarget.Z + normal.Y * (float)Math.Sign(num5) * num6 * num6 * num7 * 0.5f * ((num5 > 0f) ? 1.15f : 1.3f);
				Vector2 vector;
				Geometry.RotateVector2Fast(ref normal, 1.5707964f, out vector);
				float num9 = Vector2.Dot(vector, value);
				this.OffsetOfTarget.X = this.OffsetOfTarget.X + vector.X * num9 * Math.Max(3f, num8) * 0.66f;
				this.OffsetOfTarget.Z = this.OffsetOfTarget.Z + vector.Y * num9 * Math.Max(3f, num8) * 0.66f;
				float num10 = MathF.Sin(x);
				num10 = num10 * num10 * (float)Math.Sign(num10);
				num3 = ((num10 < 0f) ? MathHelper.Lerp(this.{25805}.UsedShip.StaticInfo.LeftCannonsBindCamera, this.{25805}.UsedShip.StaticInfo.BackCannonsBindCamera, -num10) : MathHelper.Lerp(this.{25805}.UsedShip.StaticInfo.LeftCannonsBindCamera, this.{25805}.UsedShip.StaticInfo.RearCannonsBindCamera, num10));
				num3 += this.rotates.X * 1.9f + 0.5f * num7 / 7f;
				if (staticInfo.ID == 1)
				{
					num3 = 1f;
				}
				else
				{
					num3 += num6 * num6 * 0.5f * num7 / 7f;
				}
				flag = true;
			}
			else
			{
				this.OffsetOfTarget.X = (float)(-(float)Math.Sign(num5)) * num6 * normal.X * num * num2;
				this.OffsetOfTarget.Z = (float)(-(float)Math.Sign(num5)) * num6 * normal.Y * num * num2;
				num3 = 0.5f + MathHelper.Lerp(num3, num4, 0.7f) + Math.Min(0.8f, Math.Max(0f, -0.1f + (Math.Max(this.Zoom, 3.5f) + 3f) / 22f * 0.9f));
				num3 = MathHelper.Lerp(num3, Math.Max(num3 * 0.75f, this.{25805}.UsedShip.StaticInfo.LeftCannonsBindCamera * 1.1f), Geometry.Saturate(this.ZoomFactor));
			}
			if (this.{25813}.CurrentSoftValue > 0f)
			{
				this.OffsetOfTarget.X = this.OffsetOfTarget.X + MathF.Cos(this.rotates.Y - 1.5707964f) * 25f * this.spygllassEffectValue;
				this.OffsetOfTarget.Z = this.OffsetOfTarget.Z + MathF.Sin(this.rotates.Y - 1.5707964f) * 25f * this.spygllassEffectValue;
			}
			this.OffsetOfTarget.Y = 0f;
			Vector2 vector2 = normal * scaleFactor;
			this.OffsetOfTarget.X = this.OffsetOfTarget.X + vector2.X;
			this.OffsetOfTarget.Z = this.OffsetOfTarget.Z + vector2.Y;
			this.OffsetOfTarget.Y = this.OffsetOfTarget.Y + Math.Max(0.2f, this.rotates.X * 2.5f * Math.Max(0f, this.Zoom / 22f) + this.Zoom * 0.2f) * 0.7f;
			this.OffsetOfTarget.Y = this.OffsetOfTarget.Y + (1f - Geometry.InverseLerp(-0.7f, -0.16f, this.rotates.X)) * 1f * Geometry.Saturate(this.ZoomFactor);
			this.OffsetOfTarget += this.{25812}.PositionOffset;
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
			{
				this.OffsetOfTarget.Y = this.OffsetOfTarget.Y - 1f * MathF.Sqrt(Geometry.Saturate(this.ZoomFactor));
			}
			if (float.IsNaN(this.OffsetOfTarget.X + this.OffsetOfTarget.Y + this.OffsetOfTarget.Z))
			{
				throw new InvalidOperationException("Camera error");
			}
			this.OffsetOfTarget.X = this.OffsetOfTarget.X + MathF.Cos(this.rotates.Y - 1.5707964f) * 3f * Math.Max(0f, 1f - this.ZoomFactor * 1.2f);
			this.OffsetOfTarget.Z = this.OffsetOfTarget.Z + MathF.Sin(this.rotates.Y - 1.5707964f) * 3f * Math.Max(0f, 1f - this.ZoomFactor * 1.2f);
			if (flag)
			{
				Matrix matrix;
				this.{25805}.Transform.CreateInvertedfulWorld(out matrix);
				Vector3 vector3 = Vector3.Transform(this.Position, matrix);
				float num11 = 0f;
				float num12 = 0f;
				for (int i = 0; i < this.{25805}.UsedShip.StaticInfo.CrewPositions.Size; i++)
				{
					float y = this.{25805}.UsedShip.StaticInfo.CrewPositions.Array[i].Item1.Y;
					float num13 = Math.Abs(this.{25805}.UsedShip.StaticInfo.CrewPositions.Array[i].Item1.X - vector3.X);
					float num14 = 1f / (3f + num13);
					num12 += num14;
					num11 += y * num14;
				}
				this.OffsetOfTarget.Y = this.OffsetOfTarget.Y + (num11 / num12 * this.{25805}.Transform.Scales.Y + 0.5f + num6 * num6 * 0.5f);
				return;
			}
			this.OffsetOfTarget.Y = this.OffsetOfTarget.Y + num3;
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x00114370 File Offset: 0x00112570
		public void SetFreePosition(Vector3 {25779}, Vector3 {25780})
		{
			this.IsFreeMode = true;
			this.freeCameraPosition = {25779};
			this.rotates = {25780};
			this.OffsetOfTarget = default(Vector3);
			this.BuildPropertiesFollowCamera(CameraBuildOptions.Default);
			this.{25808} = false;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x001143A4 File Offset: 0x001125A4
		public void SetTargetObject(Ship {25781}, bool {25782}, Vector2? {25783} = null)
		{
			this.{25812}.StopEffects();
			this.{25805} = {25781};
			this.{25808} = false;
			if (!{25782})
			{
				this.Zoom = (this.{25809} = 13f);
			}
			if ({25783} != null)
			{
				this.rotates = new Vector3({25783}.Value, this.rotates.Z);
			}
			else
			{
				this.rotates = new Vector3(-0.2f, this.rotates.Y, 0f);
			}
			this.BuildPropertiesFollowCamera(CameraBuildOptions.Default);
			this.OffsetOfTarget.X = 0f;
			this.OffsetOfTarget.Y = 0f;
			this.OffsetOfTarget.Z = 0f;
			this.{25778}();
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x00114466 File Offset: 0x00112666
		public void ReserTargetObject()
		{
			this.{25805} = null;
			this.{25808} = false;
		}

		// Token: 0x06001EC5 RID: 7877 RVA: 0x00114476 File Offset: 0x00112676
		public void RunFocusInObjectAnimation()
		{
			this.{25808} = true;
		}

		// Token: 0x06001EC6 RID: 7878 RVA: 0x0011447F File Offset: 0x0011267F
		public void StartApproximatingAnimation(Vector3 {25784})
		{
			this.SpecialOffset = {25784};
		}

		// Token: 0x06001EC7 RID: 7879 RVA: 0x00114488 File Offset: 0x00112688
		protected override void BuildPropertiesFollowCamera(CameraBuildOptions {25785})
		{
			float zoom = this.Zoom;
			this.Zoom += this.{25812}.ZoomOffset;
			if (this.IsFreeMode)
			{
				this.TargetObjectPosition = this.freeCameraPosition;
			}
			else if (this.{25805} != null)
			{
				this.TargetObjectPosition = this.{25805}.Transform.Translation;
			}
			base.BuildPropertiesFollowCamera({25785});
			this.Zoom = zoom;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x001144F8 File Offset: 0x001126F8
		public void BuildDecorateSceneView(Vector3 {25786})
		{
			GameCamera.entryScenePivotCenter = default(Vector3);
			this.OffsetOfTarget = Vector3.Zero;
			this.TargetObjectPosition = new Vector3(-4f, 4f, -4f) + GameCamera.entryScenePivotCenter + {25786};
			this.Position = new Vector3(0f, 5f, 0f) + GameCamera.entryScenePivotCenter + {25786};
			this.rotates = new Vector3(0f, 1.13f, 0f);
			this.SpecialOffset = Vector3.Zero;
			this.Zoom = 14f;
			this.{25809} = 14f;
			this.IsSpyglass = false;
			this.BuildPropertiesFollowCamera(CameraBuildOptions.Default);
		}

		// Token: 0x06001EC9 RID: 7881 RVA: 0x001145B8 File Offset: 0x001127B8
		public void ResetState(bool {25787})
		{
			if (Global.Game.GetCurrentSceneName != GameSceneName.Port)
			{
				Vector2 xscreenIntCenter = Global.Game.XScreenIntCenter;
				if (Global.Game.IsActive)
				{
					Mouse.SetPosition((int)xscreenIntCenter.X, (int)xscreenIntCenter.Y);
					InputHelper.NowMouseState.Position = xscreenIntCenter;
				}
			}
			this.{25800} = false;
			this.{25802} = (float)InputHelper.NowMouseState.ScrollValue;
			if ({25787})
			{
				this.Zoom = (this.{25809} = 21f);
			}
		}

		// Token: 0x06001ECA RID: 7882 RVA: 0x00114638 File Offset: 0x00112838
		public Matrix CreateReflectionBasisMatrix(bool {25788})
		{
			Vector3 value;
			Vector3.Add(ref this.TargetObjectPosition, ref this.OffsetOfTarget, out value);
			Vector3 vector = default(Vector3);
			Vector3 direction = this.direction;
			value - direction * Math.Max(this.Zoom + 4f, 1f);
			float num = -1f;
			vector.Y = Math.Max(num + 1f, 0f);
			Vector3 vector2 = (value - direction * Math.Max(this.Zoom + 4f, 1f) + ((this.Zoom + 4f == 0f) ? default(Vector3) : vector) + this.SpecialOffset) * new Vector3(1f, -1f, 1f);
			Vector3 value2 = value * new Vector3(1f, -1f, 1f);
			vector2 -= value2;
			return Matrix.CreateLookAt(Vector3.Zero, (float)({25788} ? -1 : 1) * vector2, Vector3.Down) * Matrix.CreateRotationZ(-this.rotates.Z);
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x00114770 File Offset: 0x00112970
		public void QueryParaboloidRender(Action {25789}, bool {25790}, bool {25791})
		{
			Vector3 position = this.Position;
			Matrix viewMatrix = base.ViewMatrix;
			Vector3 rotates = this.rotates;
			Vector3 targetObjectPosition = this.TargetObjectPosition;
			float zoom = this.Zoom;
			Vector3 direction = base.Direction;
			Matrix viewMultiplyProj = this.viewMultiplyProj;
			this.Zoom += this.{25812}.ZoomOffset;
			Vector3 vector = this.TargetObjectPosition + this.OffsetOfTarget;
			Vector3 vector2 = this.Position * new Vector3(1f, -1f, 1f) - Vector3.Up;
			Vector3 vector3 = vector * new Vector3(1f, -1f, 1f) - Vector3.Up;
			this.viewMatrix = ({25790} ? Matrix.CreateLookAt(vector2, vector3, Vector3.Down) : Matrix.CreateLookAt(vector3, vector2, Vector3.Down)) * Matrix.CreateRotationZ(-this.rotates.Z);
			if ({25791})
			{
				base.BuildProjMatrix(1f, Renderer.CameraFarPlane, Global.Camera.CurrentFov);
			}
			else
			{
				base.BuildProjMatrix(1f, Renderer.CameraFarPlane, 90f);
			}
			this.direction = (this.Position - vector).Normal();
			{25789}();
			this.rotates = rotates;
			this.Position = position;
			this.TargetObjectPosition = targetObjectPosition;
			this.Zoom = zoom;
			this.viewMatrix = viewMatrix;
			this.direction = direction;
			this.viewMultiplyProj = viewMultiplyProj;
			base.BuildProjMatrix(Renderer.CameraNearPlane, Renderer.CameraFarPlane, Global.Camera.CurrentFov);
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0011490C File Offset: 0x00112B0C
		public void QueryInstantiatedRender(Action {25792}, Vector3 {25793}, Vector3 {25794})
		{
			Vector3 position = this.Position;
			Matrix viewMatrix = base.ViewMatrix;
			Vector3 rotates = this.rotates;
			Vector3 targetObjectPosition = this.TargetObjectPosition;
			Vector3 offsetOfTarget = this.OffsetOfTarget;
			float zoom = this.Zoom;
			Vector3 direction = base.Direction;
			float num = this.{25809};
			Vector3 specialOffset = this.SpecialOffset;
			float currentFov = this.CurrentFov;
			this.Position = {25793};
			Vector3 position2 = this.Position;
			this.viewMatrix = Matrix.CreateLookAt(position2, {25794}, Vector3.Up);
			base.BuildProjMatrix(1f, Renderer.CameraFarPlane, 42f + this.{25812}.FovOffset);
			this.direction = (this.Position - {25794}).Normal();
			{25792}();
			this.rotates = rotates;
			this.Position = position;
			this.TargetObjectPosition = targetObjectPosition;
			this.OffsetOfTarget = offsetOfTarget;
			this.Zoom = zoom;
			this.viewMatrix = viewMatrix;
			this.direction = direction;
			this.SpecialOffset = specialOffset;
			this.{25809} = num;
			this.BuildPropertiesFollowCamera(CameraBuildOptions.Default);
			base.BuildProjMatrix(Renderer.CameraNearPlane, Renderer.CameraFarPlane, currentFov);
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x00114A28 File Offset: 0x00112C28
		public bool RotateToTarget(Vector2 {25795}, float {25796})
		{
			Vector2 position = Global.Player.Position;
			float {25797} = Geometry.GetRotate(position, {25795}) + 1.5707964f;
			return this.RotateToTarget({25797}, {25796});
		}

		// Token: 0x06001ECE RID: 7886 RVA: 0x00114A58 File Offset: 0x00112C58
		public bool RotateToTarget(float {25797}, float {25798})
		{
			float x = base.Rotation.X;
			float y = base.Rotation.Y;
			Geometry.AngularMovement(ref x, -0.14f, {25798} * 1f);
			Geometry.AngularMovement(ref y, {25797}, {25798} * 2f);
			base.Rotation = new Vector3(x, y, base.Rotation.Z);
			return Geometry.AxisDistance(y, {25797}) < 0.15f;
		}

		// Token: 0x06001ECF RID: 7887 RVA: 0x00114AC5 File Offset: 0x00112CC5
		internal void OnOcclusionCloseCamera()
		{
			if (this.{25813}.CurrentSoftValue > 0f)
			{
				return;
			}
			this.Zoom -= 1f;
			this.{25809} = this.Zoom;
		}

		// Token: 0x04001E39 RID: 7737
		public static Vector3 entryScenePivotCenter = new Vector3(0f, 0f, 5000f);

		// Token: 0x04001E3A RID: 7738
		public const float CameraAngle = 55f;

		// Token: 0x04001E3B RID: 7739
		private const float CameraAngle_min = 50f;

		// Token: 0x04001E3C RID: 7740
		private const float CameraAngle_Spyglass_min = 7f;

		// Token: 0x04001E3D RID: 7741
		private const float CameraAngle_Spyglass_max = 25f;

		// Token: 0x04001E3E RID: 7742
		private const float CameraOffsetScale = 1.5600001f;

		// Token: 0x04001E3F RID: 7743
		internal const float c_MinZoom = 3.5f;

		// Token: 0x04001E40 RID: 7744
		internal const float c_MaxZoom = 22f;

		// Token: 0x04001E41 RID: 7745
		internal const float c_DefaultZoom = 14f;

		// Token: 0x04001E42 RID: 7746
		internal const float c_CameraSoftness = 2f;

		// Token: 0x04001E43 RID: 7747
		internal const float c_CamReduce = 1.17f;

		// Token: 0x04001E44 RID: 7748
		internal const float c_MinimalXLevel = -1.2566371f;

		// Token: 0x04001E45 RID: 7749
		internal const float c_MaximalXLevel = 0.22f;

		// Token: 0x04001E46 RID: 7750
		public bool EnabledMouseZoom;

		// Token: 0x04001E47 RID: 7751
		public bool IsFreeMode;

		// Token: 0x04001E48 RID: 7752
		public float CurrentFov = 55f;

		// Token: 0x04001E49 RID: 7753
		public bool IsSpyglass;

		// Token: 0x04001E4A RID: 7754
		private Vector2 {25799};

		// Token: 0x04001E4B RID: 7755
		private bool {25800};

		// Token: 0x04001E4C RID: 7756
		private Vector2 {25801};

		// Token: 0x04001E4D RID: 7757
		private float {25802};

		// Token: 0x04001E4E RID: 7758
		private bool {25803};

		// Token: 0x04001E4F RID: 7759
		private int {25804};

		// Token: 0x04001E50 RID: 7760
		private Ship {25805};

		// Token: 0x04001E51 RID: 7761
		internal Vector3 freeCameraPositionSmooth;

		// Token: 0x04001E52 RID: 7762
		internal Vector3 freeCameraPosition;

		// Token: 0x04001E53 RID: 7763
		private Vector3 {25806};

		// Token: 0x04001E54 RID: 7764
		private float {25807};

		// Token: 0x04001E55 RID: 7765
		private bool {25808};

		// Token: 0x04001E56 RID: 7766
		private float {25809};

		// Token: 0x04001E57 RID: 7767
		private float {25810};

		// Token: 0x04001E58 RID: 7768
		private float {25811} = 14f;

		// Token: 0x04001E59 RID: 7769
		private GameCameraEffects {25812};

		// Token: 0x04001E5A RID: 7770
		private SoftTrigger {25813} = new SoftTrigger(0f, 1f, 1.8f);

		// Token: 0x04001E5B RID: 7771
		private Vector2 {25814};

		// Token: 0x04001E5C RID: 7772
		private Vector2 {25815};

		// Token: 0x04001E5D RID: 7773
		private float {25816};

		// Token: 0x04001E5E RID: 7774
		private float {25817};

		// Token: 0x04001E5F RID: 7775
		private int {25818};
	}
}
