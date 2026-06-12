using System;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Components.Client;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001CF RID: 463
	internal sealed class {19243}
	{
		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00052A4F File Offset: 0x00050C4F
		public bool CanRemove
		{
			get
			{
				return this.{19252} == 0f;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00052A5E File Offset: 0x00050C5E
		public int EngagedCrewAmount
		{
			get
			{
				return this.{19256};
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00052A66 File Offset: 0x00050C66
		public bool IsLooting
		{
			get
			{
				return this.{19260} != null || this.{19252} > 0.1f;
			}
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x00052A80 File Offset: 0x00050C80
		public {19243}(ClientDrop {19245})
		{
			this.{19251} = {19245};
			this.{19258} = 0f;
			this.{19259} = 1f;
			this.{19253} = (({19245}.Method == NDropInteropMethod.PickUp || {19245}.Method == NDropInteropMethod.MiningByCrew) ? 0f : (({19245}.ModelType == DropModel.IsleTreasures) ? 10f : (({19245}.ModelType == DropModel.RuinsFarming) ? 10f : (({19245}.ModelType == DropModel.DebrisWithBox) ? 0.4f : (({19245}.ModelType == DropModel.RealShipLoot) ? 0.75f : (({19245}.ModelType == DropModel.Fishing || {19245}.ModelType == DropModel.Whale) ? 3f : 7f))))));
			if ({19245}.Method == NDropInteropMethod.LongWatching && this.{19253} > 5f)
			{
				float num = Rand.Range(0.15f, 0.2f);
				float num2 = Rand.Range(0.3f, 0.8f);
				this.{19258} = Geometry.Saturate(num2 - num / 2f);
				this.{19259} = Geometry.Saturate(num2 + num / 2f);
			}
			float num3 = 1f + (({19245}.ModelType == DropModel.Fishing) ? Global.Player.UsedShip.FishingSpeedBonus : Global.Player.UsedShip.DropLootingSpeedBonus({19245}.ModelType));
			this.{19253} /= num3;
			this.{19254} = (({19245}.ModelType == DropModel.Fishing) ? Local.ClientDrop_6 : (({19245}.ModelType == DropModel.FishingShip) ? Local.ClientDrop_9 : (({19245}.ModelType == DropModel.Whale) ? Local.ClientDrop_8 : (({19245}.ModelType == DropModel.WorldFortLoot) ? Local.ClientDrop_7 : (({19245}.Method == NDropInteropMethod.PickUp) ? Local.ClientDrop_3 : Local.ClientDrop_4)))));
			if ({19245}.ModelType == DropModel.Whale)
			{
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.Harpoon, {19245}.Position.X0Y(), 0.7f, false);
			}
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00052C5C File Offset: 0x00050E5C
		public void UpdateInput(ref FrameTime {19246}, bool {19247})
		{
			this.{19257} = (!{19247} && this.{19251}.forgetAnimation == 0f);
			{19261} {19261} = this.{19260};
			if ({19261} != null)
			{
				{19261}.Update(ref {19246});
			}
			if (this.{19251}.forgetAnimation > 0f)
			{
				return;
			}
			if (!GameScene.GameHasInputFocus || !{19247})
			{
				if (Global.Player.UsedShip.ProtectCrewWhenLooting)
				{
					this.{19256} = 0;
				}
				this.{19252} = Math.Max(0f, this.{19252} - {19246}.secElapsed * 0.25f);
				return;
			}
			float num = Vector2.Distance(Global.Player.Position, this.{19251}.Position);
			bool flag = InputHelper.IsClick(Global.Game.InteractiveWorldSystem.CurrentPickDropKey);
			if (this.{19253} == 0f)
			{
				if ((flag || (num < 4f + Global.Player.UsedShip.StaticInfo.BSRadius * 0.3f && this.{19251}.Method == NDropInteropMethod.PickUp)) && !this.{19255})
				{
					this.{19255} = true;
					this.{19249}();
				}
				return;
			}
			if (this.{19251}.ModelType == DropModel.Fishing && Global.Player.GetBattleTimer == 0f)
			{
				EducationHelper.MakeFlag(EducationOnboarding.OpenFinshingContact, true);
			}
			if (this.{19260} != null)
			{
				if (flag)
				{
					this.{19249}();
				}
			}
			else if (InputHelper.NowInputState.IsDown(Global.Game.InteractiveWorldSystem.CurrentPickDropKey))
			{
				if (this.{19252} == 0f)
				{
					if (Global.Player.UsedShip.Crew.Count == 0)
					{
						{19994}.Me({19988}.Info, Local.ClientDrop_0, Array.Empty<object>());
						return;
					}
					this.{19252} = 0.0001f;
					if (flag)
					{
						this.{19248}();
					}
				}
				else
				{
					this.{19252} = Math.Min(this.{19253}, this.{19252} + {19246}.secElapsed);
					if (this.{19258} <= 0f && this.{19259} >= 1f && this.{19252} >= this.{19253})
					{
						this.{19249}();
					}
				}
			}
			else
			{
				if (this.{19258} > 0f || this.{19259} < 1f)
				{
					float num2 = this.{19252} / this.{19253};
					if (num2 > this.{19258} && num2 < this.{19259} && InputHelper.LastInputState.IsDown(Global.Game.InteractiveWorldSystem.CurrentPickDropKey))
					{
						this.{19249}();
					}
				}
				{19246}.EvaluteTimerSec(ref this.{19252});
			}
			if (this.{19251}.NeedsCrew && this.{19260} == null)
			{
				float num3 = Math.Max(1f, (float)Global.Player.UsedShip.Crew.Count * ((this.{19251}.ModelType == DropModel.RuinsFarming) ? 0.2f : 0.1f));
				float num4 = this.{19252} / this.{19253};
				this.{19256} = Math.Max(this.{19256}, (int)Math.Round((double)(num3 * num4)));
			}
			if (this.{19258} > 0f || this.{19259} < 1f)
			{
				float num5 = HashHelper.NoiseCurve(this.{19251}.Ttl / 1000f);
				this.{19258} += num5 * {19246}.secElapsed * 0.13f;
				this.{19259} += num5 * {19246}.secElapsed * 0.13f;
				if (this.{19258} < 0f)
				{
					this.{19259} += -this.{19258};
					this.{19258} = 0f;
				}
				if (this.{19259} > 1f)
				{
					this.{19258} -= this.{19259} - 1f;
					this.{19259} = 1f;
				}
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00053028 File Offset: 0x00051228
		private void {19248}()
		{
			if (this.{19251}.NeedsCrew || this.{19253} > 4f)
			{
				Global.Player.ResetSpeedTo0();
				return;
			}
			if (this.{19251}.ModelType == DropModel.Fishing)
			{
				Global.Player.ResetSpeedTo1();
				return;
			}
			Global.Player.ResetSpeedTo2();
		}

		// Token: 0x06000A6A RID: 2666 RVA: 0x00053080 File Offset: 0x00051280
		private void {19249}()
		{
			if (this.{19251}.Method == NDropInteropMethod.MiningByCrew)
			{
				if (this.{19260} == null)
				{
					if (this.{19251}.UseCrewHungry > 1f - Session.Account.FoodAtShip.CurrentHungerLevel)
					{
						this.{19255} = false;
						return;
					}
					if (this.{19251}.UseCrewHungry > 0f && Global.Player.UsedShip.CrewFactor < 0.5f)
					{
						this.{19255} = false;
						return;
					}
					this.{19248}();
					this.{19260} = new {19261}(this.{19251}.DisplayResourcesOnWorldmap);
					Session.Account.FoodAtShip.CurrentHungerLevel += this.{19251}.UseCrewHungry;
					NetworkManager network = Global.Network;
					DropOpenRequestMsg dropOpenRequestMsg = new DropOpenRequestMsg(this.{19251}.uID, Vector2.Zero, 0)
					{
						UseHungryLevel = true
					};
					network.Send(dropOpenRequestMsg);
					this.{19253} = 0.001f;
				}
				else
				{
					EducationHelper.MakeFlag(EducationOnboarding.MakeIsleActivity, true);
					ValueTuple<GSI, bool> valueTuple = this.{19260}.PickCurrent();
					GSI item = valueTuple.Item1;
					bool item2 = valueTuple.Item2;
					if (item != null)
					{
						NetworkManager network2 = Global.Network;
						DropOpenRequestMsg dropOpenRequestMsg = new DropOpenRequestMsg(this.{19251}.uID, Vector2.Zero, 0)
						{
							MiningPick = item,
							MiningPickSuccessful = item2
						};
						network2.Send(dropOpenRequestMsg);
					}
				}
			}
			else
			{
				Global.Network.Send(new DropOpenRequestMsg(this.{19251}.uID, Vector2.Zero, 0));
			}
			if (this.{19251}.ModelType == DropModel.Fishing)
			{
				this.{19250}();
				this.{19252} = 0f;
			}
			this.{19256} = 0;
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x00053228 File Offset: 0x00051428
		public void WhenDisembark()
		{
			if (this.EngagedCrewAmount > 0)
			{
				Global.Network.Send(new DropOpenRequestMsg(0, Vector2.Zero, this.EngagedCrewAmount));
			}
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x00053254 File Offset: 0x00051454
		public void Draw()
		{
			Vector3 value = this.{19251}.Position.X0Y();
			Vector3 vector = value + new Vector3(0f, 4f, 0f);
			float num = 1f - this.{19251}.forgetAnimation;
			num *= num;
			num *= num;
			Color {14517} = Color.White * num;
			if (Engine.GS.Camera.IsVisible(vector, 3f))
			{
				Vector2 projection = Engine.GS.Camera.GetProjection(ref value);
				if (this.{19260} == null)
				{
					Rectangle rectangle = new Rectangle(773, 359, 150, 33);
					Rectangle rectangle2 = new Rectangle(773, 393, 150, 33);
					Rectangle rectangle3 = new Rectangle(847, 327, 76, 31);
					Rectangle rectangle4 = new Rectangle(775, 427, 146, 6);
					Rectangle {11433} = new Rectangle(527, 538, 200, 200);
					bool flag = this.{19256} > 0 && this.{19257};
					string text = string.Empty;
					bool flag2 = false;
					if (this.{19251}.UseCrewHungry > 0f)
					{
						if (Global.Player.UsedShip.CrewFactor < 0.5f)
						{
							flag2 = true;
							text = Local.half_units_problem;
						}
						else
						{
							if (this.{19251}.UseCrewHungry > 1f - Session.Account.FoodAtShip.CurrentHungerLevel)
							{
								flag2 = true;
							}
							DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
							defaultInterpolatedStringHandler.AppendFormatted(Local.level_of_hunger_2);
							defaultInterpolatedStringHandler.AppendLiteral(" -");
							defaultInterpolatedStringHandler.AppendFormatted<float>(this.{19251}.UseCrewHungry * 100f, "R0");
							defaultInterpolatedStringHandler.AppendLiteral("%");
							text = defaultInterpolatedStringHandler.ToStringAndClear();
						}
					}
					Vector2 value2 = projection + new Vector2((float)(-(float)rectangle.Width / 2), (float)(-(float)rectangle.Height - 70));
					Device gs = Engine.GS;
					Rectangle rectangle5 = (flag || flag2) ? rectangle2 : rectangle;
					gs.Draw(rectangle5, value2, {14517});
					Engine.GS.SetFont(Fonts.F_m14_ThinBold);
					string str = string.Empty;
					if (Global.Game.InteractiveWorldSystem.CurrentPickDropKey != Global.Settings.kb_Action.Key)
					{
						str = "[" + Global.Game.InteractiveWorldSystem.CurrentPickDropKey.GetKeyName() + "] ";
					}
					Device gs2 = Engine.GS;
					string {14599} = flag ? Local.ClientDrop_AlertMode : (str + this.{19254});
					Vector2 vector2 = value2 + new Vector2(38f, 5f);
					gs2.DrawString({14599}, vector2, {14517});
					if (text.Length > 0)
					{
						Device gs3 = Engine.GS;
						string {14599}2 = text;
						vector2 = value2 + new Vector2(0f, 27f);
						Color color = (flag2 ? Color.Lerp(Color.OrangeRed, Color.White, 0.5f) : Color.White) * num;
						gs3.DrawString({14599}2, vector2, color);
					}
					float num2 = this.{19252} / this.{19253};
					if (num2 > 0f)
					{
						float scale = MathF.Sqrt(num2 * (1f - num2) * 4f);
						Device gs4 = Engine.GS;
						vector2 = value2 + rectangle.HalfWidthHeight();
						Vector2 vector3 = {11433}.HalfWidthHeight();
						float {14558} = this.{19252} * 0.25f;
						float {14559} = 0.9f;
						Color color = Color.White * scale * 0.3f;
						gs4.Draw({11433}, vector2, vector3, {14558}, {14559}, color);
					}
					if (this.{19256} > 0)
					{
						Vector2 value3 = value2 + new Vector2((float)((rectangle.Width - rectangle3.Width) / 2), -32f);
						Engine.GS.Draw(rectangle3, value3, {14517});
						Device gs5 = Engine.GS;
						string {14599}3 = this.{19256}.ToString();
						vector2 = value3 + new Vector2(32f, 5f);
						gs5.DrawString({14599}3, vector2, {14517});
					}
					if ((this.{19258} > 0f || this.{19259} < 1f) && num2 > 0f)
					{
						float num3 = 0.8f;
						Rectangle rectangle6 = new Rectangle(3, 730, 199, 31);
						Vector2 value4 = value2 + new Vector2(-6f, 46f);
						Device gs6 = Engine.GS;
						vector2 = Vector2.Zero;
						gs6.Draw(rectangle6, value4, vector2, 0f, num3, {14517});
						Marker marker = new Marker(7f, 8f, 184f, 16f);
						Rectangle rectangle7 = new Rectangle(335, 691, 38, 72);
						Device gs7 = Engine.GS;
						vector2 = value4 + num3 * new Vector2(marker.XY.X + marker.WH.X * num2 - (float)(rectangle7.Width / 2), (float)(-(float)rectangle7.Height / 2) + marker.Center.Y);
						Vector2 vector3 = Vector2.Zero;
						gs7.Draw(rectangle7, vector2, vector3, 0f, num3, {14517});
						Device gs8 = Engine.GS;
						rectangle5 = new Marker(marker.XY.X + marker.WH.X * this.{19258}, marker.XY.Y, marker.WH.X * (this.{19259} - this.{19258}), marker.WH.Y).Scale(num3).Offset(value4).ToRect();
						Color color = Color.Lime * num;
						gs8.Draw(rectangle4, rectangle5, color);
						return;
					}
					if (num2 > 0f)
					{
						Device gs9 = Engine.GS;
						vector2 = value2 + new Vector2(2f, 31f);
						gs9.DrawProgressbar(rectangle4, vector2, num2, {14517});
						return;
					}
				}
				else
				{
					this.{19260}.Draw(projection);
				}
			}
		}

		// Token: 0x06000A6D RID: 2669 RVA: 0x00053850 File Offset: 0x00051A50
		private void {19250}()
		{
			if (Global.Player.ResourcesOfHold[36] > 0)
			{
				Global.Player.ResourcesOfHold.AddOrRemove(36, -1);
				string name = Gameplay.ItemsInfo.FromID(36).Name;
				{19994}.Me({19988}.Minus, name, Array.Empty<object>());
				{19994}.Logbook(name + " -1", LBFlags.L0);
			}
		}

		// Token: 0x04000961 RID: 2401
		private ClientDrop {19251};

		// Token: 0x04000962 RID: 2402
		private float {19252};

		// Token: 0x04000963 RID: 2403
		private float {19253};

		// Token: 0x04000964 RID: 2404
		private string {19254};

		// Token: 0x04000965 RID: 2405
		private bool {19255};

		// Token: 0x04000966 RID: 2406
		private int {19256};

		// Token: 0x04000967 RID: 2407
		private bool {19257};

		// Token: 0x04000968 RID: 2408
		private float {19258};

		// Token: 0x04000969 RID: 2409
		private float {19259};

		// Token: 0x0400096A RID: 2410
		private {19261} {19260};
	}
}
