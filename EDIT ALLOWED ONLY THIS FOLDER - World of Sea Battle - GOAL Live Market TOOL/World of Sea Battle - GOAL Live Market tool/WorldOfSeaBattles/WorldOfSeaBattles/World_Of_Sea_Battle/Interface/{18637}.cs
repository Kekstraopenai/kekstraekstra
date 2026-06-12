using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000163 RID: 355
	internal abstract class {18637} : IDisposable
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000843 RID: 2115 RVA: 0x000415B8 File Offset: 0x0003F7B8
		// (remove) Token: 0x06000844 RID: 2116 RVA: 0x000415F0 File Offset: 0x0003F7F0
		public event Action Completed
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{18641};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{18641}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{18641};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{18641}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x00041628 File Offset: 0x0003F828
		public virtual void Begin()
		{
			string text = this.BuildText();
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			this.{18643} = true;
			Vector2 {13342} = new Vector2((float)(Engine.GS.UIArea.Width / 2 - 100), 50f);
			this.{18645} = new Label({13342}, Fonts.Arial_12, Color.Black, text, PositionAlignment.Center, PositionAlignment.LeftUp);
			{13342}.X += 1f;
			{13342}.Y -= 1f;
			this.{18644} = new Label({13342}, Fonts.Arial_12, Color.White, text, PositionAlignment.Center, PositionAlignment.LeftUp);
			this.{18644}.Opacity = 0f;
			this.{18645}.Opacity = 0f;
			new UiOpacityAnimation(this.{18644}, 1f, 250f);
			new UiOpacityAnimation(this.{18645}, 1f, 500f);
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0004170C File Offset: 0x0003F90C
		public virtual void Update(ref FrameTime {18640})
		{
			if ({17312}.CurrentInstance == null && {18640}.EvaluteTimerMs2(ref this.timeoutMs))
			{
				this.OnCompleted();
			}
			if (!this.{18643})
			{
				return;
			}
			string text = this.BuildText();
			if (string.IsNullOrEmpty(text) || string.Equals(this.{18644}.Text, text))
			{
				return;
			}
			this.{18644}.Text = text;
			this.{18645}.Text = text;
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x00041778 File Offset: 0x0003F978
		public void OnCompleted()
		{
			Action action = this.{18641};
			if (action != null)
			{
				action();
			}
			if (this.{18642} != null)
			{
				this.{18642}.RemoveFromContainer();
				this.{18642} = null;
			}
			if (this.{18643})
			{
				new UiOpacityAnimation(this.{18644}, 0f, 500f);
				new UiRemoveAction(this.{18644});
				new UiOpacityAnimation(this.{18645}, 0f, 125f);
				new UiRemoveAction(this.{18645});
				this.{18645} = null;
				this.{18644} = null;
				this.{18643} = false;
			}
		}

		// Token: 0x06000848 RID: 2120
		protected abstract string BuildText();

		// Token: 0x06000849 RID: 2121 RVA: 0x00003100 File Offset: 0x00001300
		public virtual void Dispose()
		{
		}

		// Token: 0x0400076E RID: 1902
		private const int animationTime = 500;

		// Token: 0x0400076F RID: 1903
		[CompilerGenerated]
		private Action {18641};

		// Token: 0x04000770 RID: 1904
		private Form {18642};

		// Token: 0x04000771 RID: 1905
		private bool {18643};

		// Token: 0x04000772 RID: 1906
		private Label {18644};

		// Token: 0x04000773 RID: 1907
		private Label {18645};

		// Token: 0x04000774 RID: 1908
		protected float timeoutMs;
	}
}
