using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;
using TheraEngine.Interface.Controls;

namespace TheraEngine.Interface
{
	// Token: 0x02000091 RID: 145
	public sealed class InterfaceManager : IUpdateableObject, IRenderable2D
	{
		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0001381B File Offset: 0x00011A1B
		public InterfaceManager.Parent Host
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00013824 File Offset: 0x00011A24
		public float MaxFillingTheScreenFactor
		{
			get
			{
				float num = 0f;
				foreach (UiControl uiControl in ((IEnumerable<UiControl>)this.parent.GetChildren))
				{
					if (uiControl.BasicColor.A != 0 && uiControl.Pos.WH.X != this.parent.Pos.WH.X && uiControl.IsVisible)
					{
						Marker pos = uiControl.Pos;
						num = Math.Max(MathF.Sqrt(pos.WH.X * pos.WH.Y / (this.parent.Pos.WH.X * this.parent.Pos.WH.Y)), num);
					}
				}
				return num;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00013914 File Offset: 0x00011B14
		public bool HasFocusControls
		{
			get
			{
				foreach (UiControl uiControl in ((IEnumerable<UiControl>)this.parent.GetChildren))
				{
					if (uiControl.InputMode == MouseInputMode.Focused && uiControl.BasicColor.A > 0)
					{
						Form form = uiControl as Form;
						if (form == null || form.TexturePath.Width <= 0)
						{
							return true;
						}
						if (form.TexturePath.Width > 0 || form.CheckHaveDirectFocus)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000372 RID: 882 RVA: 0x000139B0 File Offset: 0x00011BB0
		public bool HasFocusOrDownControls
		{
			get
			{
				foreach (UiControl uiControl in ((IEnumerable<UiControl>)this.parent.GetChildren))
				{
					MouseInputMode inputMode = uiControl.InputMode;
					bool flag = inputMode - MouseInputMode.Focused <= 1;
					if (flag && uiControl.BasicColor.A > 0)
					{
						Form form = uiControl as Form;
						if (form == null || form.TexturePath.Width <= 0)
						{
							return true;
						}
						if (form.TexturePath.Width > 0 || form.CheckHaveDirectFocus)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000373 RID: 883 RVA: 0x00013A60 File Offset: 0x00011C60
		// (remove) Token: 0x06000374 RID: 884 RVA: 0x00013A98 File Offset: 0x00011C98
		public event Action<UiControl> GettingInteropFocus
		{
			[CompilerGenerated]
			add
			{
				Action<UiControl> action = this.{12827};
				Action<UiControl> action2;
				do
				{
					action2 = action;
					Action<UiControl> value2 = (Action<UiControl>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<UiControl>>(ref this.{12827}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<UiControl> action = this.{12827};
				Action<UiControl> action2;
				do
				{
					action2 = action;
					Action<UiControl> value2 = (Action<UiControl>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<UiControl>>(ref this.{12827}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00013AD0 File Offset: 0x00011CD0
		public InterfaceManager()
		{
			Vector2 zero = Vector2.Zero;
			Rectangle uiarea = Engine.GS.UIArea;
			this.parent = new InterfaceManager.Parent(new Marker(ref zero, ref uiarea));
			this.{12826} = true;
			Engine.Game.EvUiRectangleSizeChanged += this.{12824};
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00013B28 File Offset: 0x00011D28
		public void Update(ref FrameTime {12822})
		{
			if (this.{12826})
			{
				int num = 0;
				this.parent.Update(ref {12822}, ref num);
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00013B4D File Offset: 0x00011D4D
		public void Render2D()
		{
			this.parent.Render();
			Engine.GS.ResetScissor();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00013B64 File Offset: 0x00011D64
		public void ClearAll()
		{
			this.parent.Remove();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00013B74 File Offset: 0x00011D74
		public string TryDisplayOpenUserWindowsForLog()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.parent != null)
			{
				Tlist<UiControl> getChildren = this.parent.GetChildren;
				for (int i = 0; i < getChildren.Size; i++)
				{
					if (getChildren.Array[i] is CustomUi)
					{
						stringBuilder.Append(getChildren.Array[i].GetType());
						if (i != getChildren.Size - 1)
						{
							stringBuilder.Append(",");
						}
					}
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00013BEC File Offset: 0x00011DEC
		internal void OnGettingInteropFocus(UiControl {12823})
		{
			Action<UiControl> action = this.{12827};
			if (action == null)
			{
				return;
			}
			action({12823});
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00013C00 File Offset: 0x00011E00
		[CompilerGenerated]
		private void {12824}(Vector2 {12825})
		{
			if (this.parent != null)
			{
				UiControl uiControl = this.parent;
				Vector2 zero = Vector2.Zero;
				Rectangle uiarea = Engine.GS.UIArea;
				uiControl.Pos = new Marker(ref zero, ref uiarea);
			}
		}

		// Token: 0x040002DD RID: 733
		internal InterfaceManager.Parent parent;

		// Token: 0x040002DE RID: 734
		private bool {12826};

		// Token: 0x040002DF RID: 735
		[CompilerGenerated]
		private Action<UiControl> {12827};

		// Token: 0x02000092 RID: 146
		public class Parent : UiControl
		{
			// Token: 0x0600037C RID: 892 RVA: 0x00013C3A File Offset: 0x00011E3A
			public Parent(Marker {12829}) : base({12829}, PositionAlignment.Both, PositionAlignment.Both, Color.White, true)
			{
			}

			// Token: 0x0600037D RID: 893 RVA: 0x00013C4B File Offset: 0x00011E4B
			internal void Remove()
			{
				base.ClearAllChild();
			}

			// Token: 0x170000A4 RID: 164
			// (get) Token: 0x0600037E RID: 894 RVA: 0x0000DD52 File Offset: 0x0000BF52
			protected override bool AllowResize
			{
				get
				{
					return true;
				}
			}
		}
	}
}
