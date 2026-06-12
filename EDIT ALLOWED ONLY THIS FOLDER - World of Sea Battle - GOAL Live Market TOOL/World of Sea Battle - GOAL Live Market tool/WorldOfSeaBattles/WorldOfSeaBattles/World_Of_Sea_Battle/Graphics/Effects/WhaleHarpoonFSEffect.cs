using System;
using Common.Game;
using Common.Packets;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004AF RID: 1199
	internal sealed class WhaleHarpoonFSEffect : ShipEffect
	{
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x000DF572 File Offset: 0x000DD772
		private float maxTtl
		{
			get
			{
				return 500f;
			}
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000E9F90 File Offset: 0x000E8190
		public WhaleHarpoonFSEffect(Ship {24591}, Drop {24592}, Action<bool> {24593}) : base({24591})
		{
			this.{24598} = {24593};
			this.{24597} = this.maxTtl;
			this.{24600} = {24592}.uID;
			this.{24599} = {24592}.Position.X0Y;
			ShipStaticInfo staticInfo = {24591}.UsedShip.StaticInfo;
			CannonBallInfo {2548} = Gameplay.BallsInfo[13];
			Vector3 vector = {24591}.Position.X0Y - {24592}.Position.X0Y;
			vector = vector.Normal;
			Tlist<FalkonetShotInfoRemote> tlist = staticInfo.FetchActiveFalkonet({24591}, {2548}, vector, 30f);
			for (int i = 0; i < tlist.Size; i++)
			{
				ref FalkonetShotInfoRemote ptr = ref tlist.Array[i];
				ptr.StartPosition = {24591}.Transform.Transform3X3(ptr.StartPosition);
			}
			Vector3 startPosition = tlist.FindNear({24592}.Position, (FalkonetShotInfoRemote {24604}) => {24604}.StartPosition.XZ()).StartPosition;
			vector = {24591}.Position3D;
			vector = (this.{24602} = (vector.XZ.X0Y ^ startPosition.Y));
			this.{24601} = vector;
			vector = Engine.GS.Camera.Direction;
			this.{24603} = vector.XZ;
			Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.Harpoon, this.{24602}, 0.8f, false);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x000EA100 File Offset: 0x000E8300
		protected override bool Update(ref FrameTime {24594})
		{
			if ({24594}.EvaluteTimerMs2(ref this.{24597}))
			{
				this.{24595}(false);
				return true;
			}
			ClientDrop dropByUid = Global.Game.WorldInstance.GetDropByUid(this.{24600});
			if (dropByUid != null)
			{
				this.{24599} = dropByUid.Position.X0Y;
				float num = 1.1f * dropByUid.Whale.Scale * (1f + 0.5f * Math.Abs(Vector2.Dot(dropByUid.Whale.VelocityNormal, (dropByUid.Position - this.{24602}.XZ).Normal)));
				if (Vector2.Distance(this.{24602}.XZ, this.{24599}.XZ) < num)
				{
					this.{24595}(true);
					return true;
				}
			}
			Vector3 value = this.{24599} - Vector3.Up;
			value.XZ = this.{24601}.XZ + this.{24603} * Vector3.Distance(this.{24599}, this.{24601}) * 1.1f;
			this.{24602} = Vector3.Lerp(this.{24601}, value, 1f - this.{24597} / this.maxTtl);
			return false;
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x000EA244 File Offset: 0x000E8444
		private void {24595}(bool {24596})
		{
			if ({24596})
			{
				for (int i = 0; i < 5; i++)
				{
					FXEngine.CreateCrewBlood(this.{24602} + Rand.NextVector3(-1f, 1f));
				}
			}
			FXEngine.NewWaterSplash(this.{24602}.XZ, 0.9f, true);
			this.{24598}({24596});
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000EA2A4 File Offset: 0x000E84A4
		public override void Render3D()
		{
			Vector3 normal = (this.{24599} - this.{24602}).Normal;
			LocalContent.Loaded.WhaleHook.Transform.Translation = this.{24602};
			LocalContent.Loaded.WhaleHook.Transform.Yaw = MathF.Atan2(this.{24603}.Y, this.{24603}.X) + 3.1415927f;
			LocalContent.Loaded.WhaleHook.Transform.Pitch = 0f;
			LocalContent.Loaded.WhaleHook.Transform.Roll = MathF.Atan2(normal.Y, normal.XZ.Length());
			LocalContent.Loaded.WhaleHook.Transform.MiddleScale = 0.8f;
			Global.Render.CommonShader.RenderObject(LocalContent.Loaded.WhaleHook, true, 1f, false, 0f, false);
		}

		// Token: 0x040018B3 RID: 6323
		private float {24597};

		// Token: 0x040018B4 RID: 6324
		private Action<bool> {24598};

		// Token: 0x040018B5 RID: 6325
		private Vector3 {24599};

		// Token: 0x040018B6 RID: 6326
		private int {24600};

		// Token: 0x040018B7 RID: 6327
		private Vector3 {24601};

		// Token: 0x040018B8 RID: 6328
		private Vector3 {24602};

		// Token: 0x040018B9 RID: 6329
		private Vector2 {24603};
	}
}
