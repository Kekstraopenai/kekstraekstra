using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Microsoft.Xna.Framework.Input;
using TheraEngine.Input;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x0200050B RID: 1291
	internal class KeynameHolder
	{
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06001CD8 RID: 7384 RVA: 0x00108C74 File Offset: 0x00106E74
		// (remove) Token: 0x06001CD9 RID: 7385 RVA: 0x00108CAC File Offset: 0x00106EAC
		private event Action<Keys> change
		{
			[CompilerGenerated]
			add
			{
				Action<Keys> action = this.{25392};
				Action<Keys> action2;
				do
				{
					action2 = action;
					Action<Keys> value2 = (Action<Keys>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Keys>>(ref this.{25392}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Keys> action = this.{25392};
				Action<Keys> action2;
				do
				{
					action2 = action;
					Action<Keys> value2 = (Action<Keys>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Keys>>(ref this.{25392}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06001CDA RID: 7386 RVA: 0x00108CE1 File Offset: 0x00106EE1
		// (remove) Token: 0x06001CDB RID: 7387 RVA: 0x00108CEA File Offset: 0x00106EEA
		public event Action<Keys> EvChange
		{
			add
			{
				this.change += value;
			}
			remove
			{
				this.change -= value;
			}
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06001CDC RID: 7388 RVA: 0x00108CF3 File Offset: 0x00106EF3
		public Keys Key
		{
			get
			{
				return this.{25393};
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06001CDD RID: 7389 RVA: 0x00108CFB File Offset: 0x00106EFB
		public Buttons[] GamepadKeys
		{
			get
			{
				return this.{25394};
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06001CDE RID: 7390 RVA: 0x00108D04 File Offset: 0x00106F04
		public bool IsClick
		{
			get
			{
				if (InputHelper.IsGamepadConnected && InputHelper.IsGamepadActive && !this.BlockGamepad)
				{
					for (int i = 0; i < this.{25394}.Length; i++)
					{
						if (InputHelper.NowGamepadState.IsButtonDown(this.{25394}[i]) && i == 0 && InputHelper.PrevGamepadState.IsButtonUp(this.{25394}[i]))
						{
							return true;
						}
					}
				}
				return InputHelper.IsClick(this.{25393});
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06001CDF RID: 7391 RVA: 0x00108D74 File Offset: 0x00106F74
		public bool IsRelease
		{
			get
			{
				if (InputHelper.IsGamepadConnected && InputHelper.IsGamepadActive && !this.BlockGamepad)
				{
					for (int i = 0; i < this.{25394}.Length; i++)
					{
						if (InputHelper.NowGamepadState.IsButtonUp(this.{25394}[i]) && i == 0 && InputHelper.PrevGamepadState.IsButtonDown(this.{25394}[i]))
						{
							return true;
						}
					}
				}
				return InputHelper.LastInputState.IsDown(this.{25393}) && InputHelper.NowInputState.IsUp(this.{25393});
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06001CE0 RID: 7392 RVA: 0x00108DFC File Offset: 0x00106FFC
		public bool IsDown
		{
			get
			{
				if (InputHelper.IsGamepadConnected && InputHelper.IsGamepadActive && !this.BlockGamepad)
				{
					for (int i = 0; i < this.{25394}.Length; i++)
					{
						if (InputHelper.NowGamepadState.IsButtonDown(this.{25394}[i]))
						{
							return true;
						}
					}
				}
				return InputHelper.NowInputState.IsDown(ref this.{25393});
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001CE1 RID: 7393 RVA: 0x00108E58 File Offset: 0x00107058
		public string KeyToString
		{
			get
			{
				if (!InputHelper.IsGamepadConnected || !InputHelper.IsGamepadActive)
				{
					return this.{25393}.GetKeyName();
				}
				return this.GamePadKeyToString;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001CE2 RID: 7394 RVA: 0x00108E7A File Offset: 0x0010707A
		private bool BlockGamepad
		{
			get
			{
				return this.GamepadBlockKey != null && InputHelper.NowGamepadState.IsButtonDown(this.GamepadBlockKey.Value);
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06001CE3 RID: 7395 RVA: 0x00108EA0 File Offset: 0x001070A0
		private string GamePadKeyToString
		{
			get
			{
				if (this.{25394}.Length == 0)
				{
					return "?";
				}
				string text = this.{25394}[0].GetKeyName();
				for (int i = 1; i < this.{25394}.Length; i++)
				{
					text = text + "+" + this.{25394}[i].GetKeyName();
				}
				return text;
			}
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x00108EF7 File Offset: 0x001070F7
		public void Set(Keys {25389})
		{
			this.{25393} = {25389};
			Action<Keys> action = this.{25392};
			if (action == null)
			{
				return;
			}
			action({25389});
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x00108F11 File Offset: 0x00107111
		public KeynameHolder(Keys {25390}, params Buttons[] {25391})
		{
			this.{25393} = {25390};
			this.{25394} = {25391};
		}

		// Token: 0x04001C86 RID: 7302
		[CompilerGenerated]
		private Action<Keys> {25392};

		// Token: 0x04001C87 RID: 7303
		private Keys {25393};

		// Token: 0x04001C88 RID: 7304
		private Buttons[] {25394};

		// Token: 0x04001C89 RID: 7305
		public Buttons? GamepadBlockKey;
	}
}
