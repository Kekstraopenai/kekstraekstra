using System;
using Common.Game;
using Common.Packets;
using TheraEngine.Collections;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x020004F3 RID: 1267
	internal sealed class ClientEventActionsPipeline : EventActionsPipelineBase
	{
		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001C31 RID: 7217 RVA: 0x00101A3D File Offset: 0x000FFC3D
		public Tlist<EventActionBase> CurrentActions
		{
			get
			{
				return this.actions;
			}
		}

		// Token: 0x06001C32 RID: 7218 RVA: 0x00101A45 File Offset: 0x000FFC45
		public void UpdateWaitActions(Tlist<EventActionBase> {25263})
		{
			this.WaitActions = {25263};
		}

		// Token: 0x06001C33 RID: 7219 RVA: 0x00101A50 File Offset: 0x000FFC50
		public void UpdateWaitActions(OnChangesEventActionsMsg {25264})
		{
			for (int i = 0; i < (int){25264}.RemoveWaitActionsCount; i++)
			{
				int num = {25264}.RemoveWaitActions[i];
				for (int j = 0; j < this.WaitActions.Size; j++)
				{
					if (this.WaitActions.Array[j].AID == num)
					{
						this.WaitActions.FastRemoveAt(j);
						break;
					}
				}
			}
			for (int k = 0; k < {25264}.AddWaitActions.Size; k++)
			{
				this.WaitActions.Add({25264}.AddWaitActions.Array[k]);
			}
		}

		// Token: 0x06001C34 RID: 7220 RVA: 0x00101AE4 File Offset: 0x000FFCE4
		public void ActionBeginEvenet(EventActionBase {25265})
		{
			base.Add({25265});
		}

		// Token: 0x06001C35 RID: 7221 RVA: 0x00101AED File Offset: 0x000FFCED
		public void ActionEndEvent(int {25266})
		{
			base.Remove({25266});
		}

		// Token: 0x06001C36 RID: 7222 RVA: 0x00101AF6 File Offset: 0x000FFCF6
		public void Clear()
		{
			base.ClearAll();
		}

		// Token: 0x04001B41 RID: 6977
		public Tlist<EventActionBase> WaitActions;
	}
}
