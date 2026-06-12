using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TheraEngine.Input
{
	// Token: 0x020000E4 RID: 228
	public static class InputHelper
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0001F6FC File Offset: 0x0001D8FC
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0001F703 File Offset: 0x0001D903
		public static bool LeftWasClicked { get; private set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0001F70B File Offset: 0x0001D90B
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0001F712 File Offset: 0x0001D912
		public static bool LeftWasReleased { get; private set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0001F71A File Offset: 0x0001D91A
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0001F721 File Offset: 0x0001D921
		public static bool RightWasClicked { get; private set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0001F729 File Offset: 0x0001D929
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0001F730 File Offset: 0x0001D930
		public static bool RightWasReleased { get; private set; }

		// Token: 0x0600061A RID: 1562 RVA: 0x0001F79C File Offset: 0x0001D99C
		private static void UpdateInputUsingXna()
		{
			KeyboardState state;
			try
			{
				state = Keyboard.GetState();
			}
			catch (Exception)
			{
				return;
			}
			foreach (Keys item in state.GetPressedKeys())
			{
				if (!InputHelper.keyQueue.Contains(item))
				{
					InputHelper.keyQueue.Add(item);
				}
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001F7F8 File Offset: 0x0001D9F8
		internal static void ForceUpdateInput(bool {14327})
		{
			if (!{14327})
			{
				InputHelper.UpdateInputUsingXna();
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001F804 File Offset: 0x0001DA04
		internal static void BeginUpdate(bool {14328})
		{
			InputHelper.LastInputState.SetData(InputHelper.NowInputState);
			InputHelper.LastMouseState.SetData(InputHelper.NowMouseState);
			if (!{14328})
			{
				InputHelper.UpdateInputUsingXna();
				MouseState state = Mouse.GetState();
				InputHelper.NowMouseState.SetData(state.RightButton == ButtonState.Pressed, state.LeftButton == ButtonState.Pressed || InputHelper.emulateLeftClick, state.ScrollWheelValue, new Vector2((float)state.X, (float)state.Y));
				InputHelper.emulateLeftClick = false;
				if (InputHelper.keyQueue.Count > 0)
				{
					InputHelper.gamepadDisableCounter++;
					if (InputHelper.gamepadDisableCounter > 2)
					{
						InputHelper.IsGamepadActive = false;
					}
				}
				InputHelper.NowInputState.SetData(InputHelper.keyQueue);
				InputHelper.keyQueueUpdated = true;
				InputHelper.keyQueue.Clear();
			}
			InputHelper.lastFrameActive = InputHelper.nowFrameActive;
			InputHelper.nowFrameActive = Engine.Game.IsActive;
			InputHelper.LeftWasClicked = (InputHelper.NowMouseState.LeftPressed && !InputHelper.LastMouseState.LeftPressed && InputHelper.nowFrameActive);
			InputHelper.LeftWasReleased = (!InputHelper.NowMouseState.LeftPressed && InputHelper.LastMouseState.LeftPressed && InputHelper.nowFrameActive);
			InputHelper.RightWasClicked = (InputHelper.NowMouseState.RightPressed && !InputHelper.LastMouseState.RightPressed && InputHelper.nowFrameActive);
			InputHelper.RightWasReleased = (!InputHelper.NowMouseState.RightPressed && InputHelper.LastMouseState.RightPressed && InputHelper.nowFrameActive);
			InputHelper.GraphicsTestKeyDown = InputHelper.NowInputState.IsDown(Keys.O);
			InputHelper.IsGamepadConnected = false;
			if (!InputHelper.hasGamepadError)
			{
				try
				{
					if (GamePad.GetCapabilities(PlayerIndex.One).GamePadType == GamePadType.GamePad)
					{
						InputHelper.IsGamepadConnected = true;
						InputHelper.PrevGamepadState = InputHelper.NowGamepadState;
						InputHelper.NowGamepadState = GamePad.GetState(PlayerIndex.One, GamePadDeadZone.Circular);
						if (InputHelper.NowGamepadState.Triggers.Right > 0.5f)
						{
							InputHelper.emulateLeftClick = true;
						}
						if (InputHelper.NowGamepadState.Triggers.Left > 0.5f)
						{
							InputHelper.emulatedScroll += InputHelper.NowGamepadState.ThumbSticks.Right.Y * InputHelper.NowGamepadState.ThumbSticks.Right.Y * (float)Math.Sign(InputHelper.NowGamepadState.ThumbSticks.Right.Y) * 4f;
						}
						if (InputHelper.NowGamepadState.Buttons.A == ButtonState.Pressed || InputHelper.NowGamepadState.Buttons.B == ButtonState.Pressed || InputHelper.NowGamepadState.Buttons.Back == ButtonState.Pressed || InputHelper.NowGamepadState.Buttons.Start == ButtonState.Pressed || InputHelper.NowGamepadState.Buttons.X == ButtonState.Pressed || InputHelper.NowGamepadState.Buttons.Y == ButtonState.Pressed || InputHelper.NowGamepadState.Buttons.LeftShoulder == ButtonState.Pressed || InputHelper.NowGamepadState.Buttons.RightShoulder == ButtonState.Pressed || InputHelper.NowGamepadState.Buttons.LeftStick == ButtonState.Pressed || InputHelper.NowGamepadState.Buttons.RightStick == ButtonState.Pressed || InputHelper.NowGamepadState.Triggers.Left > 0.5f || InputHelper.NowGamepadState.Triggers.Right > 0.5f)
						{
							InputHelper.gamepadDisableCounter = 0;
							InputHelper.IsGamepadActive = true;
						}
						InputHelper.NowMouseState.ScrollValue += (int)InputHelper.emulatedScroll;
					}
					else
					{
						InputHelper.NowGamepadState = default(GamePadState);
						InputHelper.PrevGamepadState = default(GamePadState);
					}
				}
				catch (Exception)
				{
					InputHelper.hasGamepadError = true;
					InputHelper.NowGamepadState = default(GamePadState);
					InputHelper.PrevGamepadState = default(GamePadState);
				}
			}
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001FBFC File Offset: 0x0001DDFC
		public static bool IsClick(Keys {14329})
		{
			return InputHelper.LastInputState.IsUp(ref {14329}) && InputHelper.NowInputState.IsDown(ref {14329});
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000C282 File Offset: 0x0000A482
		public static void UnFullReset()
		{
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001FC1A File Offset: 0x0001DE1A
		public static void EmulateClick(Keys {14330})
		{
			InputHelper.keyQueue.Add({14330});
		}

		// Token: 0x0400048F RID: 1167
		public const bool EmulateKbByGamepad = true;

		// Token: 0x04000490 RID: 1168
		public static bool GraphicsTestKeyDown;

		// Token: 0x04000491 RID: 1169
		public static readonly MouseInput NowMouseState = new MouseInput();

		// Token: 0x04000492 RID: 1170
		public static readonly MouseInput LastMouseState = new MouseInput();

		// Token: 0x04000493 RID: 1171
		public static readonly KeyboardInput NowInputState = KeyboardInput.Empty;

		// Token: 0x04000494 RID: 1172
		public static readonly KeyboardInput LastInputState = KeyboardInput.Empty;

		// Token: 0x04000495 RID: 1173
		public static GamePadState NowGamepadState;

		// Token: 0x04000496 RID: 1174
		public static GamePadState PrevGamepadState;

		// Token: 0x04000497 RID: 1175
		public static bool IsGamepadConnected;

		// Token: 0x04000498 RID: 1176
		public static bool IsGamepadActive;

		// Token: 0x0400049D RID: 1181
		private static bool lastFrameActive;

		// Token: 0x0400049E RID: 1182
		private static bool nowFrameActive;

		// Token: 0x0400049F RID: 1183
		private static List<Keys> keyQueue = new List<Keys>(10);

		// Token: 0x040004A0 RID: 1184
		private static bool emulateLeftClick = false;

		// Token: 0x040004A1 RID: 1185
		private static bool keyQueueUpdated;

		// Token: 0x040004A2 RID: 1186
		private static object sr = new object();

		// Token: 0x040004A3 RID: 1187
		private static bool hasGamepadError;

		// Token: 0x040004A4 RID: 1188
		private static float emulatedScroll = 0f;

		// Token: 0x040004A5 RID: 1189
		private static int gamepadDisableCounter = 0;
	}
}
