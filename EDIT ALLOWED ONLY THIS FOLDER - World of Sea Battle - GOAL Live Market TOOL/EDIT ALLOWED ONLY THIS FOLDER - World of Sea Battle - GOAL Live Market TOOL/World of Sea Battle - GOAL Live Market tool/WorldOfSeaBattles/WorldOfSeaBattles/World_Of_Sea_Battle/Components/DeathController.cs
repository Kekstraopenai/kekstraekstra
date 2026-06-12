using System;
using Common;
using Common.Game;
using Common.Resources;
using ManualPacketSerialization;
using ManualPacketSerialization.Externs;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components.Architecture;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Components
{
	// Token: 0x02000539 RID: 1337
	internal class DeathController : IMPSerializable, IUpdateableObject
	{
		// Token: 0x06001DCF RID: 7631 RVA: 0x0010EBDC File Offset: 0x0010CDDC
		public void OnDeath()
		{
			if (!Global.Player.MapInfo.IsWorldmap && Global.Player.MapInfo.IsEnableArenaUi && Session.CurrentArenaSession != null && Session.CurrentArenaSession.ModeInfo.RespawnLimit != 0 && !Session.CurrentArenaSession.ModeInfo.FastRespawn)
			{
				this.BlockRespawnOnSeaSec = (float)(15 + this.{25663}.Size * 8);
			}
		}

		// Token: 0x06001DD0 RID: 7632 RVA: 0x0010EC4C File Offset: 0x0010CE4C
		public void OnRespawn(PortEnteringType {25652}, bool {25653}, bool {25654})
		{
			if ({25653})
			{
				if (Session.EngagingInPortBattle != PbsBatlleSide.None)
				{
					this.BlockPortExitSec = (float)Math.Min(Math.Max(0, this.{25662}.Size - 1) * 9, 60);
				}
				else
				{
					this.BlockPortExitSec = (float)(35 + 25 * this.{25662}.Size);
					if ({25652} != PortEnteringType.Port)
					{
						this.BlockPortExitSec /= 2f;
					}
					if ({25652} == PortEnteringType.Port && {25654})
					{
						this.BlockPortExitSec /= 3f;
					}
				}
				float num = 1f - Session.Game.RecoveryAfterSinkBonus;
				this.BlockPortExitSec = Math.Max(0f, this.BlockPortExitSec - this.TimeBeingOnBoat / 2f) * num;
				Tlist<float> tlist = this.{25662};
				float num2 = 840f;
				tlist.Add(num2);
				if (Session.LastDeathByMyself)
				{
					Tlist<float> tlist2 = this.{25662};
					num2 = 840f;
					tlist2.Add(num2);
				}
			}
			else
			{
				this.BlockPortExitSec = 0f;
				Tlist<float> tlist3 = this.{25663};
				float num2 = 1200f;
				tlist3.Add(num2);
			}
			this.TimeBeingOnBoat = 0f;
		}

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0010ED65 File Offset: 0x0010CF65
		public void OnDisembarkPB()
		{
			this.{25662}.Clear();
		}

		// Token: 0x06001DD2 RID: 7634 RVA: 0x0010ED72 File Offset: 0x0010CF72
		public void OnTravelUse()
		{
			this.BlockPortExitSec = 0f;
		}

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0010ED72 File Offset: 0x0010CF72
		public void OnExitFromPort()
		{
			this.BlockPortExitSec = 0f;
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0010ED80 File Offset: 0x0010CF80
		public void Update(ref FrameTime {25655})
		{
			this.Evalute(this.{25662}, ref {25655});
			if (Global.Player.MapInfo.IsWorldmap)
			{
				this.{25663}.Clear();
			}
			{25655}.EvaluteTimerSec(ref this.BlockPortExitSec);
			{25655}.EvaluteTimerSec(ref this.BlockRespawnOnSeaSec);
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0010EDD0 File Offset: 0x0010CFD0
		private void Evalute(Tlist<float> {25656}, ref FrameTime {25657})
		{
			for (int i = 0; i < {25656}.Size; i++)
			{
				if ({25657}.EvaluteTimerSec2(ref {25656}.Array[i]))
				{
					{25656}.FastRemoveAt(i);
					i--;
				}
			}
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x0010EE0D File Offset: 0x0010D00D
		void IMPSerializable.{25658}(WriterExtern {25659})
		{
			{25659}.WriteTlist(this.{25662});
			{25659}.WriteTlist(this.{25663});
			{25659}.WriteStruct<float>(this.BlockPortExitSec);
			{25659}.WriteStruct<float>(this.BlockRespawnOnSeaSec);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x0010EE3F File Offset: 0x0010D03F
		void IMPSerializable.{25660}(WriterExtern {25661})
		{
			{25661}.ReadTlist(out this.{25662});
			{25661}.ReadTlist(out this.{25663});
			{25661}.ReadStruct<float>(out this.BlockPortExitSec);
			{25661}.ReadStruct<float>(out this.BlockRespawnOnSeaSec);
		}

		// Token: 0x04001DB8 RID: 7608
		private Tlist<float> {25662} = new Tlist<float>();

		// Token: 0x04001DB9 RID: 7609
		private Tlist<float> {25663} = new Tlist<float>();

		// Token: 0x04001DBA RID: 7610
		public float BlockPortExitSec;

		// Token: 0x04001DBB RID: 7611
		public float BlockRespawnOnSeaSec;

		// Token: 0x04001DBC RID: 7612
		public float TimeBeingOnBoat;
	}
}
