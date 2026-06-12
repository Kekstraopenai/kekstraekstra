using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;

namespace TheraEngine.Interface
{
	// Token: 0x0200009A RID: 154
	public abstract class UiControl : DisposableObject
	{
		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000390 RID: 912 RVA: 0x00014038 File Offset: 0x00012238
		// (remove) Token: 0x06000391 RID: 913 RVA: 0x0001406C File Offset: 0x0001226C
		public static event Action<UiControl> ClickButtonEffectHandler;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000392 RID: 914 RVA: 0x000140A0 File Offset: 0x000122A0
		// (remove) Token: 0x06000393 RID: 915 RVA: 0x000140D8 File Offset: 0x000122D8
		public event Action<UiControl> UpdateComplete
		{
			[CompilerGenerated]
			add
			{
				Action<UiControl> action = this.{12978};
				Action<UiControl> action2;
				do
				{
					action2 = action;
					Action<UiControl> value2 = (Action<UiControl>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<UiControl>>(ref this.{12978}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<UiControl> action = this.{12978};
				Action<UiControl> action2;
				do
				{
					action2 = action;
					Action<UiControl> value2 = (Action<UiControl>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<UiControl>>(ref this.{12978}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000394 RID: 916 RVA: 0x00014110 File Offset: 0x00012310
		// (remove) Token: 0x06000395 RID: 917 RVA: 0x00014148 File Offset: 0x00012348
		public event Action<UiControl> RenderComplete
		{
			[CompilerGenerated]
			add
			{
				Action<UiControl> action = this.{12979};
				Action<UiControl> action2;
				do
				{
					action2 = action;
					Action<UiControl> value2 = (Action<UiControl>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<UiControl>>(ref this.{12979}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<UiControl> action = this.{12979};
				Action<UiControl> action2;
				do
				{
					action2 = action;
					Action<UiControl> value2 = (Action<UiControl>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<UiControl>>(ref this.{12979}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000396 RID: 918 RVA: 0x00014180 File Offset: 0x00012380
		// (remove) Token: 0x06000397 RID: 919 RVA: 0x000141B8 File Offset: 0x000123B8
		public event Action<ClickUiEventArgs> EvClick
		{
			[CompilerGenerated]
			add
			{
				Action<ClickUiEventArgs> action = this.{12980};
				Action<ClickUiEventArgs> action2;
				do
				{
					action2 = action;
					Action<ClickUiEventArgs> value2 = (Action<ClickUiEventArgs>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ClickUiEventArgs>>(ref this.{12980}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ClickUiEventArgs> action = this.{12980};
				Action<ClickUiEventArgs> action2;
				do
				{
					action2 = action;
					Action<ClickUiEventArgs> value2 = (Action<ClickUiEventArgs>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ClickUiEventArgs>>(ref this.{12980}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000398 RID: 920 RVA: 0x000141F0 File Offset: 0x000123F0
		// (remove) Token: 0x06000399 RID: 921 RVA: 0x00014228 File Offset: 0x00012428
		public event Action<ClickUiEventArgs> EvClickEmptiness
		{
			[CompilerGenerated]
			add
			{
				Action<ClickUiEventArgs> action = this.{12981};
				Action<ClickUiEventArgs> action2;
				do
				{
					action2 = action;
					Action<ClickUiEventArgs> value2 = (Action<ClickUiEventArgs>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ClickUiEventArgs>>(ref this.{12981}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ClickUiEventArgs> action = this.{12981};
				Action<ClickUiEventArgs> action2;
				do
				{
					action2 = action;
					Action<ClickUiEventArgs> value2 = (Action<ClickUiEventArgs>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ClickUiEventArgs>>(ref this.{12981}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600039A RID: 922 RVA: 0x00014260 File Offset: 0x00012460
		// (remove) Token: 0x0600039B RID: 923 RVA: 0x00014298 File Offset: 0x00012498
		public event Action<ClickUiEventArgs> EvRightButtonClick
		{
			[CompilerGenerated]
			add
			{
				Action<ClickUiEventArgs> action = this.{12982};
				Action<ClickUiEventArgs> action2;
				do
				{
					action2 = action;
					Action<ClickUiEventArgs> value2 = (Action<ClickUiEventArgs>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ClickUiEventArgs>>(ref this.{12982}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ClickUiEventArgs> action = this.{12982};
				Action<ClickUiEventArgs> action2;
				do
				{
					action2 = action;
					Action<ClickUiEventArgs> value2 = (Action<ClickUiEventArgs>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ClickUiEventArgs>>(ref this.{12982}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600039C RID: 924 RVA: 0x000142D0 File Offset: 0x000124D0
		// (remove) Token: 0x0600039D RID: 925 RVA: 0x00014308 File Offset: 0x00012508
		public event Action EvGotMouseFocus
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{12983};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{12983}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{12983};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{12983}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600039E RID: 926 RVA: 0x00014340 File Offset: 0x00012540
		// (remove) Token: 0x0600039F RID: 927 RVA: 0x00014378 File Offset: 0x00012578
		public event Action EvLostMouseFocus
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{12984};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{12984}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{12984};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{12984}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060003A0 RID: 928 RVA: 0x000143B0 File Offset: 0x000125B0
		// (remove) Token: 0x060003A1 RID: 929 RVA: 0x000143E8 File Offset: 0x000125E8
		public event Action EvRemoveFromContainer
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{12985};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{12985}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{12985};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{12985}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060003A2 RID: 930 RVA: 0x00014420 File Offset: 0x00012620
		// (remove) Token: 0x060003A3 RID: 931 RVA: 0x00014458 File Offset: 0x00012658
		internal event Action<UiControl> EvSingleChildWasRemoved
		{
			[CompilerGenerated]
			add
			{
				Action<UiControl> action = this.{12986};
				Action<UiControl> action2;
				do
				{
					action2 = action;
					Action<UiControl> value2 = (Action<UiControl>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<UiControl>>(ref this.{12986}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<UiControl> action = this.{12986};
				Action<UiControl> action2;
				do
				{
					action2 = action;
					Action<UiControl> value2 = (Action<UiControl>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<UiControl>>(ref this.{12986}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060003A4 RID: 932 RVA: 0x00014490 File Offset: 0x00012690
		// (remove) Token: 0x060003A5 RID: 933 RVA: 0x000144C8 File Offset: 0x000126C8
		internal event Action SomethingWasRemoved
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{12987};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{12987}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{12987};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{12987}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060003A6 RID: 934 RVA: 0x00014500 File Offset: 0x00012700
		// (remove) Token: 0x060003A7 RID: 935 RVA: 0x00014538 File Offset: 0x00012738
		private event Action _EvDynamicStorageStateChanged
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{12988};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{12988}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{12988};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{12988}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060003A8 RID: 936 RVA: 0x0001456D File Offset: 0x0001276D
		// (remove) Token: 0x060003A9 RID: 937 RVA: 0x00014589 File Offset: 0x00012789
		internal event Action EvDynamicStorageStateChanged
		{
			add
			{
				if (!this.IsDynamicStorage)
				{
					throw new InvalidOperationException("This is not dynamic storage!");
				}
				this._EvDynamicStorageStateChanged += value;
			}
			remove
			{
				if (!this.IsDynamicStorage)
				{
					throw new InvalidOperationException("This is not dynamic storage!");
				}
				this._EvDynamicStorageStateChanged -= value;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000145A5 File Offset: 0x000127A5
		// (set) Token: 0x060003AB RID: 939 RVA: 0x000145AD File Offset: 0x000127AD
		public bool AntiMissclick { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003AC RID: 940 RVA: 0x000145B6 File Offset: 0x000127B6
		// (set) Token: 0x060003AD RID: 941 RVA: 0x000145BE File Offset: 0x000127BE
		public bool InheritOpacity { get; set; } = true;

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000145C7 File Offset: 0x000127C7
		// (set) Token: 0x060003AF RID: 943 RVA: 0x000145D0 File Offset: 0x000127D0
		public Marker Pos
		{
			get
			{
				return this.{12989};
			}
			set
			{
				if (this.{12989} == value)
				{
					return;
				}
				if (float.IsNaN(value.XY.X) || float.IsNaN(value.XY.Y) || float.IsNaN(value.WH.X) || float.IsNaN(value.WH.Y))
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(33, 1);
					defaultInterpolatedStringHandler.AppendLiteral("Marker received for Pos has NaN: ");
					defaultInterpolatedStringHandler.AppendFormatted<Marker>(value);
					throw new ArgumentException(defaultInterpolatedStringHandler.ToStringAndClear());
				}
				if (value.WH.X < 0f || value.WH.Y < 0f)
				{
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(41, 1);
					defaultInterpolatedStringHandler2.AppendLiteral("Marker received for Pos has negative XY: ");
					defaultInterpolatedStringHandler2.AppendFormatted<Marker>(value);
					throw new ArgumentException(defaultInterpolatedStringHandler2.ToStringAndClear());
				}
				for (int i = 0; i < this.{12992}.Size; i++)
				{
					this.{12992}.Array[i].SetBoundsFromParent(this.{12989}, value);
				}
				this.{12989} = value;
				if (!this.isMarkerChangedCall)
				{
					this.isMarkerChangedCall = true;
					this.MarkerChanged();
					this.isMarkerChangedCall = false;
				}
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x00014700 File Offset: 0x00012900
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x0001471C File Offset: 0x0001291C
		public float PosWidth
		{
			get
			{
				return this.Pos.Width;
			}
			set
			{
				Marker pos = this.Pos;
				pos.Width = value;
				this.Pos = pos;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x00014740 File Offset: 0x00012940
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x0001475C File Offset: 0x0001295C
		public float PosHeight
		{
			get
			{
				return this.Pos.Height;
			}
			set
			{
				Marker pos = this.Pos;
				pos.Height = value;
				this.Pos = pos;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0001477F File Offset: 0x0001297F
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x00014787 File Offset: 0x00012987
		public PositionAlignment PositionAlignment_X { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x00014790 File Offset: 0x00012990
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00014798 File Offset: 0x00012998
		public PositionAlignment PositionAlignment_Y { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x000147A1 File Offset: 0x000129A1
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x000147A9 File Offset: 0x000129A9
		public bool IsVisible
		{
			get
			{
				return this.{12990};
			}
			set
			{
				if (this.{12990} != value)
				{
					if (this.{12990} && !value)
					{
						this.ShutdownFocus();
					}
					this.{12990} = value;
				}
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003BA RID: 954 RVA: 0x000147CC File Offset: 0x000129CC
		public bool CheckHaveDirectFocus
		{
			get
			{
				if (this.InputMode != MouseInputMode.Focused)
				{
					return false;
				}
				for (int i = 0; i < this.{12992}.Size; i++)
				{
					if (this.{12992}.Array[i].InputMode != MouseInputMode.NoFocus && this.{12992}.Array[i].BasicColor.A != 0)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0001482A File Offset: 0x00012A2A
		public MouseInputMode InputMode
		{
			get
			{
				return this.{12991};
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00014832 File Offset: 0x00012A32
		public UiControl GetParent
		{
			get
			{
				return this.{12994};
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0001483C File Offset: 0x00012A3C
		public string DebugChildren
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (UiControl uiControl in ((IEnumerable<UiControl>)this.{12992}))
				{
					stringBuilder.AppendLine(uiControl.GetType().ToString());
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003BE RID: 958 RVA: 0x000148A0 File Offset: 0x00012AA0
		// (set) Token: 0x060003BF RID: 959 RVA: 0x000148A8 File Offset: 0x00012AA8
		public bool AllowMouseInput { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x000148B1 File Offset: 0x00012AB1
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x000148B9 File Offset: 0x00012AB9
		public float Opacity
		{
			get
			{
				return this.{12997};
			}
			set
			{
				if (this.{12997} != value)
				{
					this.{12997} = MathHelper.Clamp(value, 0f, 1f);
					if (this.{12997} == 0f)
					{
						this.ShutdownFocus();
					}
				}
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x000148ED File Offset: 0x00012AED
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x000148FC File Offset: 0x00012AFC
		public float Brightness
		{
			get
			{
				return this.{12998} + this.ownExtraBrightness;
			}
			set
			{
				if (value < 0f)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.{12998} = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00014918 File Offset: 0x00012B18
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x00014920 File Offset: 0x00012B20
		public bool BrightnessBlinkingMode { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00014929 File Offset: 0x00012B29
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x00014931 File Offset: 0x00012B31
		public float FirstOpacity
		{
			get
			{
				return this.{12999};
			}
			set
			{
				this.{12999} = MathHelper.Clamp(value, 0f, 1f);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00014949 File Offset: 0x00012B49
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x00014951 File Offset: 0x00012B51
		public bool RenderToDepthMap { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060003CA RID: 970 RVA: 0x0001495A File Offset: 0x00012B5A
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00014962 File Offset: 0x00012B62
		public bool DisableDepthFocusTest { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0001496B File Offset: 0x00012B6B
		// (set) Token: 0x060003CD RID: 973 RVA: 0x00014973 File Offset: 0x00012B73
		public ToolTip ToolTip { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0001497C File Offset: 0x00012B7C
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00014990 File Offset: 0x00012B90
		public ToolTipState ToolTipState
		{
			get
			{
				ToolTip toolTip = this.ToolTip;
				if (toolTip == null)
				{
					return null;
				}
				return toolTip.CurrentContent;
			}
			set
			{
				if (value == null)
				{
					ToolTip toolTip = this.ToolTip;
					if (toolTip != null)
					{
						toolTip.CloseIfIsOpen();
					}
					this.ToolTip = null;
					return;
				}
				if (this.ToolTip == null)
				{
					this.ToolTip = new ToolTip(value);
					return;
				}
				this.ToolTip.CurrentContent = value;
				this.ToolTip.RefreshIfIsOpen();
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000149E5 File Offset: 0x00012BE5
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x000149ED File Offset: 0x00012BED
		public bool UseScissor { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000149F6 File Offset: 0x00012BF6
		public int AnimationsCount
		{
			get
			{
				return this.{12993}.Size;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000E21A File Offset: 0x0000C41A
		internal virtual bool IsDynamicStorage
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00014A04 File Offset: 0x00012C04
		public UiControl(Marker {12922}, PositionAlignment {12923}, PositionAlignment {12924}, Color {12925}, bool {12926} = false)
		{
			this.ToolTip = null;
			this.{12990} = true;
			this.{12991} = MouseInputMode.NoFocus;
			this.{12992} = new Tlist<UiControl>(0);
			this.{12993} = new Tlist<UiAction>(0);
			this.removeQueueReferences = new Tlist<UiControl>(0);
			this.{12996} = false;
			this.{12997} = 1f;
			this.{12998} = 1f;
			this.{12999} = 1f;
			this.AllowMouseInput = true;
			this.RenderToDepthMap = true;
			this.PositionAlignment_X = {12923};
			this.PositionAlignment_Y = {12924};
			if (float.IsNaN({12922}.XY.X) || float.IsNaN({12922}.XY.Y) || float.IsNaN({12922}.WH.X) || float.IsNaN({12922}.WH.Y))
			{
				throw new ArgumentException("Marker received for UiControl.ctor has NaN");
			}
			this.{12989} = {12922};
			this.BasicColor = {12925};
			if (!{12926})
			{
				Engine.Game.interfaceManager.parent.AddChild(this);
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x00014B16 File Offset: 0x00012D16
		public UiControl(Marker {12927}, PositionAlignment {12928}, Color {12929}) : this({12927}, {12928}, {12928}, {12929}, false)
		{
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00014B24 File Offset: 0x00012D24
		internal virtual void Update(ref FrameTime {12930}, ref int {12931})
		{
			if (this.{12993}.Size != 0 && this.{12993}.Array[0].Update(ref {12930}, this))
			{
				this.{12993}.Array[0].OnCompleted(this);
				this.{12993}.RemoveAt(0);
			}
			ProgressSelectBar progressSelectBar = this as ProgressSelectBar;
			if (progressSelectBar != null && progressSelectBar.Triggered && (InputHelper.NowMouseState.LeftPressed || InputHelper.NowMouseState.RightPressed))
			{
				this.{12991} = MouseInputMode.Down;
			}
			else if (this.{12990} && this.{12997} > 0f && (this.{12994} == null || this.{12994}.{12990}))
			{
				if (this.{12994} != null && this.{12994}.{12991} == MouseInputMode.NoFocus && !this.{12994}.DisableDepthFocusTest)
				{
					if (this.{12991} != MouseInputMode.NoFocus)
					{
						this.ShutdownFocus();
					}
				}
				else if (this.AllowMouseInput)
				{
					bool flag;
					this.{12932}(ref {12931}, out flag);
					if (this.{12991} == MouseInputMode.NoFocus && flag && !InputHelper.NowMouseState.LeftPressed)
					{
						this.{12991} = MouseInputMode.Focused;
						Action action = this.{12983};
						if (action != null)
						{
							action();
						}
					}
					if (this.{12991} == MouseInputMode.Focused)
					{
						if (!flag)
						{
							this.ShutdownFocus();
						}
						else if (InputHelper.NowMouseState.LeftPressed)
						{
							if (this.AntiMissclick)
							{
								this.missclickProtectionStatus = Math.Min(1f, this.missclickProtectionStatus + {12930}.secElapsed * 2.5f);
							}
							if (!this.AntiMissclick || this.missclickProtectionStatus >= 1f)
							{
								bool flag2 = false;
								if (this.{12980} != null)
								{
									if (this is Button || this is AnimatedButton || this is LabelButton || this is CheckboxControl || this is Form)
									{
										Engine.Game.GetInterfaceManager.OnGettingInteropFocus(this);
									}
									ClickUiEventArgs clickUiEventArgs = new ClickUiEventArgs(this, Engine.GS.MouseToUI);
									this.{12980}(clickUiEventArgs);
									flag2 = clickUiEventArgs.DoNotHandling;
									if (!flag2 && UiControl.ClickButtonEffectHandler != null && (base.GetType() == typeof(Button) || base.GetType() == typeof(AnimatedButton)))
									{
										UiControl.ClickButtonEffectHandler(this);
									}
								}
								if (flag2)
								{
									this.{12991} = MouseInputMode.NoFocus;
								}
								else
								{
									this.{12991} = MouseInputMode.Down;
								}
							}
						}
					}
					if (this.{12991} == MouseInputMode.Down)
					{
						if (!flag)
						{
							this.ShutdownFocus();
						}
						else if (!InputHelper.NowMouseState.LeftPressed)
						{
							this.{12991} = MouseInputMode.Focused;
						}
					}
					if (this.{12982} != null && this.{12991} == MouseInputMode.Focused && InputHelper.NowMouseState.RightPressed && !InputHelper.LastMouseState.RightPressed)
					{
						this.{12982}(new ClickUiEventArgs(this, Engine.GS.MouseToUI));
					}
				}
				else if (this.{12991} != MouseInputMode.NoFocus)
				{
					this.ShutdownFocus();
				}
			}
			else if (this.{12991} != MouseInputMode.NoFocus)
			{
				this.ShutdownFocus();
			}
			{12930}.EvaluteTimerSec(ref this.missclickProtectionStatus);
			this.{13001} = false;
			if (UiControl.EditorMode != null && this.{12991} == MouseInputMode.Focused && this.GetParent != null)
			{
				this.{13001} = true;
				int num = 0;
				for (int i = 0; i < this.{12992}.Size; i++)
				{
					if (this.{12992}.Array[i].{12991} == MouseInputMode.NoFocus)
					{
						num++;
					}
				}
				if (num == this.{12992}.Size)
				{
					if (InputHelper.NowMouseState.RightPressed)
					{
						UiControl.EditorMode.Select(this);
						Marker pos = this.Pos;
						Vector2 vector = Engine.GS.MouseToUI - Engine.GS.MouseToUIPrev;
						this.Pos = pos.Offset(vector);
					}
				}
				else
				{
					this.{13001} = false;
				}
			}
			ToolTip toolTip = this.ToolTip;
			if (toolTip != null)
			{
				toolTip.Update({12930}.msElapsed, this);
			}
			this.{12996} = true;
			for (int j = this.{12992}.Size - 1; j > -1; j--)
			{
				this.{12992}.Array[j].Update(ref {12930}, ref j);
			}
			this.{12996} = false;
			for (int k = 0; k < this.removeQueueReferences.Size; k++)
			{
				UiControl obj = this.removeQueueReferences.Array[k];
				this.{12992}.Remove(obj);
				Action<UiControl> action2 = this.{12986};
				if (action2 != null)
				{
					action2(obj);
				}
			}
			if (this.removeQueueReferences.Size > 0)
			{
				Action action3 = this.{12987};
				if (action3 != null)
				{
					action3();
				}
			}
			this.removeQueueReferences.Clear();
			for (int l = 0; l < this.{12992}.Size; l++)
			{
				if (this.{12992}.Array[l].{12995} != null)
				{
					UiControl {12965} = this.{12992}.Array[l];
					LayoutBinding value = this.{12992}.Array[l].{12995}.Value;
					this.{12964}({12965}, value);
				}
			}
			if (InputHelper.LeftWasClicked && this.{12991} == MouseInputMode.Down)
			{
				if (this.{12992}.Count((UiControl {13012}) => {13012}.{12991} != MouseInputMode.NoFocus && {13012}.{12980} != null) == 0)
				{
					Action<ClickUiEventArgs> action4 = this.{12981};
					if (action4 != null)
					{
						action4(new ClickUiEventArgs(this, Engine.GS.MouseToUI));
					}
				}
			}
			if (this.{13000} != null)
			{
				this.{13000}();
				this.{13000} = null;
			}
			Action<UiControl> action5 = this.{12978};
			if (action5 == null)
			{
				return;
			}
			action5(this);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0001509C File Offset: 0x0001329C
		public void ForceUpdateComplete()
		{
			Action<UiControl> action = this.{12978};
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000150B0 File Offset: 0x000132B0
		private void {12932}(ref int {12933}, out bool {12934})
		{
			if (this.{12994} != null && !this.DisableDepthFocusTest)
			{
				for (int i = {12933} + 1; i < this.{12994}.{12992}.Size; i++)
				{
					UiControl uiControl = this.{12994}.{12992}.Array[i];
					if (uiControl != null && uiControl.{12991} != MouseInputMode.NoFocus && uiControl.RenderToDepthMap)
					{
						{12934} = false;
						return;
					}
				}
			}
			Vector2 mouseToUI = Engine.GS.MouseToUI;
			{12934} = this.{12935}(mouseToUI, true);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0001512C File Offset: 0x0001332C
		private bool {12935}(in Vector2 {12936}, bool {12937})
		{
			return ((!{12937} && !this.UseScissor) || this.Pos.Collision({12936})) && (this.{12994} == null || this.{12994}.{12935}({12936}, false));
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00015170 File Offset: 0x00013370
		internal virtual void Render()
		{
			if (UiControl.EditorMode != null && this.{13001})
			{
				Device gs = Engine.GS;
				Rectangle rectangle = new Rectangle(10000, 10000, 0, 0);
				Rectangle rectangle2 = this.Pos.ToRect();
				Color color = Color.Red * 0.3f;
				gs.Draw(rectangle, rectangle2, color);
			}
			this.RenderChilds();
			Action<UiControl> action = this.{12979};
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000151E4 File Offset: 0x000133E4
		internal void RenderChilds()
		{
			if (this.{12992}.Size > 0)
			{
				Rectangle scissorRectangle = Engine.GS.graphicsDevice.ScissorRectangle;
				Marker marker = new Marker(ref scissorRectangle);
				if ((float)Engine.Game.AdaptiveUiRect.Width != Engine.Game.WindowSize.X)
				{
					Vector2 value = Engine.Game.AdaptiveUiRect.WidthHeight() / Engine.Game.WindowSize;
					marker.XY *= value;
					marker.WH *= value;
					Rectangle adaptiveUiRect = Engine.Game.AdaptiveUiRect;
					Marker marker2 = new Marker(ref adaptiveUiRect);
					Marker.Intersection(ref marker, ref marker2, out marker);
				}
				for (int i = 0; i < this.{12992}.Size; i++)
				{
					UiControl uiControl = this.{12992}.Array[i];
					if (uiControl.{12990} && uiControl.{12997} > 0f)
					{
						Marker marker3;
						Marker.Intersection(ref marker, ref uiControl.{12989}, out marker3);
						if (marker3.WH.X * marker3.WH.Y > 0f)
						{
							if (uiControl.UseScissor)
							{
								Engine.GS.SetScissor(marker3.ToRect(), true);
							}
							uiControl.Render();
							if (uiControl.UseScissor)
							{
								Engine.GS.SetScissor(scissorRectangle, false);
							}
						}
					}
				}
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x00015357 File Offset: 0x00013557
		public void ImitateClick(bool {12938} = false)
		{
			if ({12938})
			{
				Action<ClickUiEventArgs> action = this.{12981};
				if (action == null)
				{
					return;
				}
				action(new ClickUiEventArgs(this, Vector2.Zero));
				return;
			}
			else
			{
				Action<ClickUiEventArgs> action2 = this.{12980};
				if (action2 == null)
				{
					return;
				}
				action2(new ClickUiEventArgs(this, Vector2.Zero));
				return;
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00015393 File Offset: 0x00013593
		public void RemoveFromContainer()
		{
			UiControl uiControl = this.{12994};
			if (uiControl != null)
			{
				uiControl.RemoveAt(this, true);
			}
			ToolTip toolTip = this.ToolTip;
			if (toolTip == null)
			{
				return;
			}
			toolTip.Shutdown();
		}

		// Token: 0x060003DE RID: 990 RVA: 0x000153B8 File Offset: 0x000135B8
		public void MoveToFrontLevel()
		{
			if (this.{12994} == null || (this.{12994}.{12992}.IndexOf(this) == this.{12994}.{12992}.Size - 1 && this.{12994}.{13000} == null))
			{
				return;
			}
			if (this.{12994}.{12996})
			{
				UiControl uiControl = this.{12994};
				uiControl.{13000} = (Action)Delegate.Combine(uiControl.{13000}, new Action(this.{12949}));
				return;
			}
			this.{12949}();
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001543C File Offset: 0x0001363C
		public void MoveToBackLevel()
		{
			if (this.{12994} == null || this.{12994}.{12992}.IndexOf(this) == 0)
			{
				return;
			}
			if (this.{12994}.{12996})
			{
				UiControl uiControl = this.{12994};
				uiControl.{13000} = (Action)Delegate.Combine(uiControl.{13000}, new Action(this.{12950}));
				return;
			}
			this.{12950}();
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x000154A1 File Offset: 0x000136A1
		public Tlist<UiControl> GetChildren
		{
			get
			{
				return this.{12992};
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000154A9 File Offset: 0x000136A9
		public UiControl FirstChild()
		{
			if (this.{12992}.Size == 0)
			{
				return null;
			}
			return this.{12992}.Array[0];
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x000154C7 File Offset: 0x000136C7
		public int CountChild()
		{
			return this.{12992}.Size;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x000154D4 File Offset: 0x000136D4
		public float GetOpcaity()
		{
			float result;
			this.GetOpcaity(out result);
			return result;
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x000154EC File Offset: 0x000136EC
		protected float GetBrightness()
		{
			float result;
			this.GetBrightness(out result);
			return result;
		}

		// Token: 0x060003E5 RID: 997 RVA: 0x00015504 File Offset: 0x00013704
		protected void GetOpcaity(out float {12939})
		{
			if (this.{12994} != null)
			{
				this.{12994}.GetOpcaity(out {12939});
				{12939} *= this.{12997} * this.{12999};
			}
			else
			{
				{12939} = this.{12997} * this.{12999};
			}
			if (!this.InheritOpacity)
			{
				{12939} = ({12939} != 0f);
			}
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00015560 File Offset: 0x00013760
		protected void GetBrightness(out float {12940})
		{
			if (this.{12994} != null)
			{
				this.{12994}.GetBrightness(out {12940});
				{12940} *= this.Brightness;
			}
			else
			{
				{12940} = this.Brightness;
			}
			if (this.BrightnessBlinkingMode)
			{
				{12940} *= 1.7f + (float)Math.Sin(Engine.Game.GameTotalTimeSec * 4.099999904632568) * 0.7f;
			}
		}

		// Token: 0x060003E7 RID: 999 RVA: 0x000155CC File Offset: 0x000137CC
		protected static Color ComputeColor(float {12941}, float {12942}, Color {12943})
		{
			if ({12942} != 1f)
			{
				{12943} *= {12942};
			}
			if ({12941} == 1f)
			{
				return {12943};
			}
			if ({12941} < 1f)
			{
				{12943} = {12943}.Multiply(new Color({12941}, {12941}, {12941}, 1f));
			}
			else
			{
				{12943} = {12943}.Multiply(new Color(1f, 1f, 1f, 1f / {12941}));
			}
			return {12943};
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060003E8 RID: 1000
		protected abstract bool AllowResize { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00015637 File Offset: 0x00013837
		public bool AllowResizeEditor
		{
			get
			{
				return this.AllowResize;
			}
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00015640 File Offset: 0x00013840
		internal void SetBoundsFromParent(Marker {12944}, Marker {12945})
		{
			if (this.PositionAlignment_X != PositionAlignment.Disabled && this.PositionAlignment_Y != PositionAlignment.Disabled)
			{
				Vector2 zero = Vector2.Zero;
				Vector2 zero2 = Vector2.Zero;
				if (this.PositionAlignment_X != PositionAlignment.Disabled)
				{
					zero.X = {12945}.XY.X - {12944}.XY.X;
					if (this.PositionAlignment_X == PositionAlignment.Center)
					{
						zero.X += ({12945}.WH.X - {12944}.WH.X) * 0.5f;
					}
					if (this.PositionAlignment_X == PositionAlignment.Both && this.AllowResize)
					{
						zero2.X = {12945}.WH.X - {12944}.WH.X;
					}
					if (this.PositionAlignment_X == PositionAlignment.RightDown)
					{
						zero.X += {12945}.WH.X - {12944}.WH.X;
					}
				}
				if (this.PositionAlignment_Y != PositionAlignment.Disabled)
				{
					zero.Y = {12945}.XY.Y - {12944}.XY.Y;
					if (this.PositionAlignment_Y == PositionAlignment.Center)
					{
						zero.Y += ({12945}.WH.Y - {12944}.WH.Y) * 0.5f;
					}
					if (this.PositionAlignment_Y == PositionAlignment.Both && this.AllowResize)
					{
						zero2.Y = {12945}.WH.Y - {12944}.WH.Y;
					}
					if (this.PositionAlignment_Y == PositionAlignment.RightDown)
					{
						zero.Y += {12945}.WH.Y - {12944}.WH.Y;
					}
				}
				Vector2 vector = this.{12989}.XY + zero;
				Vector2 vector2 = this.{12989}.WH + zero2;
				this.Pos = new Marker(ref vector, ref vector2);
			}
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x0000C282 File Offset: 0x0000A482
		protected internal virtual void MarkerChanged()
		{
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x0001580C File Offset: 0x00013A0C
		internal virtual void ShutdownFocus()
		{
			if (this.{12991} != MouseInputMode.NoFocus)
			{
				this.{12991} = MouseInputMode.NoFocus;
				Action action = this.{12984};
				if (action != null)
				{
					action();
				}
			}
			ToolTip toolTip = this.ToolTip;
			if (toolTip != null)
			{
				toolTip.Shutdown();
			}
			for (int i = 0; i < this.{12992}.Size; i++)
			{
				this.{12992}.Array[i].ShutdownFocus();
			}
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00015872 File Offset: 0x00013A72
		private void {12946}(UiControl {12947})
		{
			if ({12947} == null)
			{
				throw new InvalidOperationException("Internal error");
			}
			UiControl uiControl = this.{12994};
			if (uiControl != null)
			{
				uiControl.RemoveAt(this, false);
			}
			this.{12994} = {12947};
		}

		// Token: 0x060003EE RID: 1006 RVA: 0x0001589C File Offset: 0x00013A9C
		private void {12948}()
		{
			this.{12994} = null;
			this.{12995} = null;
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000158B1 File Offset: 0x00013AB1
		protected void OnDynamicStorageStateChanged()
		{
			if (!this.IsDynamicStorage)
			{
				throw new InvalidOperationException("This is not dynamic storage!");
			}
			Action action = this.{12988};
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000158D8 File Offset: 0x00013AD8
		private void {12949}()
		{
			if (this.{12994} == null)
			{
				return;
			}
			if (this.{12994}.removeQueueReferences.Contains(this))
			{
				return;
			}
			this.{12994}.{12992}.Remove(this);
			this.{12994}.{12992}.Add(this);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x00015928 File Offset: 0x00013B28
		private void {12950}()
		{
			if (this.{12994} == null)
			{
				return;
			}
			if (this.{12994}.removeQueueReferences.Contains(this))
			{
				return;
			}
			this.{12994}.{12992}.Remove(this);
			this.{12994}.{12992}.Insert(0, this);
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x00015978 File Offset: 0x00013B78
		protected internal void AddChild(params UiControl[] {12951})
		{
			foreach (UiControl {12969} in {12951})
			{
				this.{12968}({12969});
			}
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000159A0 File Offset: 0x00013BA0
		public UiControl AddChildPos(UiControl {12952}, PositionAlignment {12953}, PositionAlignment {12954}, float {12955} = 0f)
		{
			LayoutBinding layoutBinding = new LayoutBinding({12953}, {12954}, ({12953} == PositionAlignment.Center) ? 0f : {12955}, ({12954} == PositionAlignment.Center) ? 0f : {12955}, false);
			return this.InternalAddChildPos({12952}, layoutBinding);
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000159DC File Offset: 0x00013BDC
		public UiControl AddChildPos(UiControl {12956}, PositionAlignment {12957}, PositionAlignment {12958}, float {12959}, float {12960}, bool {12961} = false)
		{
			LayoutBinding layoutBinding = new LayoutBinding({12957}, {12958}, {12959}, {12960}, {12961});
			return this.InternalAddChildPos({12956}, layoutBinding);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00015A00 File Offset: 0x00013C00
		protected UiControl InternalAddChildPos(UiControl {12962}, in LayoutBinding {12963})
		{
			this.{12964}({12962}, {12963});
			this.AddChild({12962});
			{12962}.{12995} = ({12963}.updateCentroid ? new LayoutBinding?({12963}) : null);
			return {12962};
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00015A44 File Offset: 0x00013C44
		private void {12964}(UiControl {12965}, in LayoutBinding {12966})
		{
			if ({12966}.xMark == PositionAlignment.Both || {12966}.yMark == PositionAlignment.Both)
			{
				throw new NotSupportedException();
			}
			Marker pos = {12965}.Pos;
			if ({12966}.xMark == PositionAlignment.Center)
			{
				pos.XY.X = this.Pos.Center.X - pos.WH.X / 2f + {12966}.borderX;
			}
			if ({12966}.yMark == PositionAlignment.Center)
			{
				pos.XY.Y = this.Pos.Center.Y - pos.WH.Y / 2f;
			}
			if ({12966}.xMark == PositionAlignment.LeftUp)
			{
				pos.XY.X = this.Pos.XY.X + {12966}.borderX;
			}
			if ({12966}.yMark == PositionAlignment.LeftUp)
			{
				pos.XY.Y = this.Pos.XY.Y + {12966}.borderY;
			}
			if ({12966}.xMark == PositionAlignment.RightDown)
			{
				pos.XY.X = this.Pos.XY.X + this.Pos.WH.X - {12966}.borderX - pos.WH.X;
			}
			if ({12966}.yMark == PositionAlignment.RightDown)
			{
				pos.XY.Y = this.Pos.XY.Y + this.Pos.WH.Y - {12966}.borderY - pos.WH.Y;
			}
			{12965}.Pos = pos;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00015BDC File Offset: 0x00013DDC
		internal void AddChild(UiControl {12967})
		{
			this.{12968}({12967});
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00015BE5 File Offset: 0x00013DE5
		private void {12968}(UiControl {12969})
		{
			{12969}.ShutdownFocus();
			{12969}.{12946}(this);
			this.{12992}.Add({12969});
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00015C04 File Offset: 0x00013E04
		[NullableContext(2)]
		protected void RemoveAt(UiControl {12970}, bool {12971} = true)
		{
			if ({12970} == null)
			{
				return;
			}
			if (this.{12996})
			{
				if (this.removeQueueReferences.Contains({12970}))
				{
					return;
				}
				this.{12972}({12970}, {12971});
				this.removeQueueReferences.Add({12970});
				return;
			}
			else
			{
				this.{12992}.Remove({12970});
				Action<UiControl> action = this.{12986};
				if (action != null)
				{
					action({12970});
				}
				this.{12972}({12970}, {12971});
				Action action2 = this.{12987};
				if (action2 == null)
				{
					return;
				}
				action2();
				return;
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00015C7C File Offset: 0x00013E7C
		private void {12972}(UiControl {12973}, bool {12974})
		{
			if ({12973}.{12994} == null)
			{
				return;
			}
			{12973}.ShutdownFocus();
			{12973}.{12948}();
			if ({12974} && {12973}.{12985} != null)
			{
				{12973}.{12985}();
			}
			for (int i = 0; i < {12973}.{12992}.Size; i++)
			{
				{12973}.{12992}.Array[i].{12975}();
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00015CDC File Offset: 0x00013EDC
		private void {12975}()
		{
			if (this.{12994} == null)
			{
				return;
			}
			this.ShutdownFocus();
			Action action = this.{12985};
			if (action != null)
			{
				action();
			}
			for (int i = 0; i < this.{12992}.Size; i++)
			{
				this.{12992}.Array[i].{12975}();
			}
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00015D34 File Offset: 0x00013F34
		protected internal void ClearAllChild()
		{
			if (this.{12992} == null)
			{
				return;
			}
			if (this.{12996})
			{
				for (int i = 0; i < this.{12992}.Size; i++)
				{
					this.RemoveAt(this.{12992}.Array[i], true);
				}
				return;
			}
			while (this.{12992}.Size > 0)
			{
				this.RemoveAt(this.{12992}.Array[this.{12992}.Size - 1], true);
			}
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x00015DAB File Offset: 0x00013FAB
		internal void AddAction(UiAction {12976})
		{
			this.{12993}.Add({12976});
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00015DBA File Offset: 0x00013FBA
		public void RemoveAnimations()
		{
			this.{12993}.Clear();
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x00015DC8 File Offset: 0x00013FC8
		public void RemoveAnimationsWithoutCurrent()
		{
			if (this.{12993}.Size == 1)
			{
				return;
			}
			if (this.{12993}.Size > 1)
			{
				UiAction uiAction = this.{12993}.Array[0];
				this.{12993}.Clear();
				this.{12993}.Add(uiAction);
				return;
			}
			this.{12993}.Clear();
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00015E24 File Offset: 0x00014024
		public void RemoveClickEvents()
		{
			this.{12980} = null;
			this.{12982} = null;
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00015E34 File Offset: 0x00014034
		public void RemoveDestroyEvents()
		{
			this.{12985} = null;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x00015E40 File Offset: 0x00014040
		internal void Foreach(Action<UiControl> {12977})
		{
			for (int i = 0; i < this.{12992}.Size; i++)
			{
				{12977}(this.{12992}.Array[i]);
			}
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00015E76 File Offset: 0x00014076
		public override void Dispose()
		{
			this.CleanResources();
			base.Dispose();
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00015E84 File Offset: 0x00014084
		protected virtual void CleanResources()
		{
			this.{12948}();
			this.{12988} = null;
			this.{12980} = null;
			this.{12982} = null;
			this.{12983} = null;
			this.{12984} = null;
			this.{12985} = null;
			Tlist<UiControl> tlist = this.{12992};
			if (tlist != null)
			{
				tlist.Clear();
			}
			this.{12992} = null;
			Tlist<UiControl> tlist2 = this.removeQueueReferences;
			if (tlist2 != null)
			{
				tlist2.Clear();
			}
			this.removeQueueReferences = null;
			ToolTip toolTip = this.ToolTip;
			if (toolTip != null)
			{
				toolTip.Shutdown();
			}
			this.ToolTip = null;
			this.{12993} = null;
		}

		// Token: 0x040002FC RID: 764
		public static IUiEditorConnection EditorMode;

		// Token: 0x040002FE RID: 766
		[CompilerGenerated]
		private Action<UiControl> {12978};

		// Token: 0x040002FF RID: 767
		[CompilerGenerated]
		private Action<UiControl> {12979};

		// Token: 0x04000300 RID: 768
		[CompilerGenerated]
		private Action<ClickUiEventArgs> {12980};

		// Token: 0x04000301 RID: 769
		[CompilerGenerated]
		private Action<ClickUiEventArgs> {12981};

		// Token: 0x04000302 RID: 770
		[CompilerGenerated]
		private Action<ClickUiEventArgs> {12982};

		// Token: 0x04000303 RID: 771
		[CompilerGenerated]
		private Action {12983};

		// Token: 0x04000304 RID: 772
		[CompilerGenerated]
		private Action {12984};

		// Token: 0x04000305 RID: 773
		[CompilerGenerated]
		private Action {12985};

		// Token: 0x04000306 RID: 774
		[CompilerGenerated]
		private Action<UiControl> {12986};

		// Token: 0x04000307 RID: 775
		[CompilerGenerated]
		private Action {12987};

		// Token: 0x04000308 RID: 776
		[CompilerGenerated]
		private Action {12988};

		// Token: 0x04000309 RID: 777
		private Marker {12989};

		// Token: 0x0400030A RID: 778
		private bool {12990};

		// Token: 0x0400030B RID: 779
		private MouseInputMode {12991};

		// Token: 0x0400030C RID: 780
		private Tlist<UiControl> {12992};

		// Token: 0x0400030D RID: 781
		private Tlist<UiAction> {12993};

		// Token: 0x0400030E RID: 782
		protected Tlist<UiControl> removeQueueReferences;

		// Token: 0x0400030F RID: 783
		private UiControl {12994};

		// Token: 0x04000310 RID: 784
		private LayoutBinding? {12995};

		// Token: 0x04000311 RID: 785
		protected bool isMarkerChangedCall;

		// Token: 0x04000312 RID: 786
		private bool {12996};

		// Token: 0x04000313 RID: 787
		private float {12997};

		// Token: 0x04000314 RID: 788
		private float {12998};

		// Token: 0x04000315 RID: 789
		protected float ownExtraBrightness;

		// Token: 0x04000316 RID: 790
		private float {12999};

		// Token: 0x04000317 RID: 791
		private Action {13000};

		// Token: 0x04000318 RID: 792
		private bool {13001};

		// Token: 0x04000319 RID: 793
		protected float missclickProtectionStatus;

		// Token: 0x0400031A RID: 794
		[CompilerGenerated]
		private bool {13002};

		// Token: 0x0400031B RID: 795
		[CompilerGenerated]
		private bool {13003};

		// Token: 0x0400031C RID: 796
		public Color BasicColor;

		// Token: 0x0400031D RID: 797
		[CompilerGenerated]
		private PositionAlignment {13004};

		// Token: 0x0400031E RID: 798
		[CompilerGenerated]
		private PositionAlignment {13005};

		// Token: 0x0400031F RID: 799
		[CompilerGenerated]
		private bool {13006};

		// Token: 0x04000320 RID: 800
		[CompilerGenerated]
		private bool {13007};

		// Token: 0x04000321 RID: 801
		[CompilerGenerated]
		private bool {13008};

		// Token: 0x04000322 RID: 802
		[CompilerGenerated]
		private bool {13009};

		// Token: 0x04000323 RID: 803
		[CompilerGenerated]
		private ToolTip {13010};

		// Token: 0x04000324 RID: 804
		[CompilerGenerated]
		private bool {13011};
	}
}
