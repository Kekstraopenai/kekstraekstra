using System;
using System.Runtime.CompilerServices;
using System.Threading;
using Common.Game;
using TheraEngine;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004AB RID: 1195
	internal abstract class ShipEffect : GameEffect
	{
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06001A2E RID: 6702 RVA: 0x000E9523 File Offset: 0x000E7723
		public override bool IsAlive
		{
			get
			{
				return !this.{24546};
			}
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06001A2F RID: 6703 RVA: 0x000E9530 File Offset: 0x000E7730
		// (remove) Token: 0x06001A30 RID: 6704 RVA: 0x000E9568 File Offset: 0x000E7768
		public event Action EndLife
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{24547};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{24547}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{24547};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{24547}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000E959D File Offset: 0x000E779D
		public ShipEffect(Ship {24540}) : base(true)
		{
			this.ship = {24540};
			this.ship.ManualLostEvent.Subscribe(new Action<ShipCleanupEventArgs>(this.Lost_Callback));
			this.{24546} = false;
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000E95D1 File Offset: 0x000E77D1
		public sealed override void Update(ref FrameTime {24541}, out bool {24542})
		{
			if (this.{24546})
			{
				{24542} = true;
				this.{24545}();
				return;
			}
			{24542} = this.Update(ref {24541});
			if ({24542})
			{
				this.{24545}();
			}
		}

		// Token: 0x06001A33 RID: 6707
		protected abstract bool Update(ref FrameTime {24543});

		// Token: 0x06001A34 RID: 6708 RVA: 0x000E95F8 File Offset: 0x000E77F8
		protected virtual void Lost_Callback(ShipCleanupEventArgs {24544})
		{
			{24544}.Source.ManualLostEvent.Unsubscribe(new Action<ShipCleanupEventArgs>(this.Lost_Callback));
			this.{24546} = true;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000E961E File Offset: 0x000E781E
		private void {24545}()
		{
			Action action = this.{24547};
			if (action != null)
			{
				action();
			}
			this.{24547} = null;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000E9638 File Offset: 0x000E7838
		public virtual void ManualRemove()
		{
			this.{24546} = true;
		}

		// Token: 0x04001890 RID: 6288
		protected Ship ship;

		// Token: 0x04001891 RID: 6289
		private bool {24546};

		// Token: 0x04001892 RID: 6290
		[CompilerGenerated]
		private Action {24547};
	}
}
