using System;
using Common.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004A2 RID: 1186
	internal sealed class BoardingFSEffect : ShipEffect
	{
		// Token: 0x06001A04 RID: 6660 RVA: 0x000E8058 File Offset: 0x000E6258
		public BoardingFSEffect(Ship {24432}, Ship {24433}, float {24434}) : base({24432})
		{
			this.{24444} = new WeakShipReference({24433});
			BoardingFSEffect.ActiveEffects.Add(this);
			this.{24446} = WosbBoarding.DurationMs + 4100f + 500f + 2000f;
			this.{24441} = this.{24446} - {24434};
			this.{24442} = Color.White;
			this.{24445} = new Timer(400f);
			if ({24434} == 0f)
			{
				Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.BoardingBegin, {24432}.Position3D, 1f, false);
				this.{24448} = 8000f;
			}
			this.{24439} = LocalContent.Loaded.Effect3;
			this.{24440} = new Transform3D({24432}.Position3D, Vector3.Zero, new Vector3({24432}.UsedShip.StaticInfo.BSRadius * 0.43f, {24432}.UsedShip.StaticInfo.BSRadius * 0.66f, {24432}.UsedShip.StaticInfo.BSRadius * 0.43f));
			BillboardParent_VPCT[] array = new BillboardParent_VPCT[3];
			array[0] = BillboardParent_VPCT.CreatePlane(2.5f, 2.5f, 0.25f);
			array[0].SetUV(BoardingFSEffect.circleTexturePath, AtlasObjs.Texture.Size);
			array[1] = BillboardParent_VPCT.CreatePlane(2f, 2f, -0.5f);
			array[1].SetUV(BoardingFSEffect.circleTexturePath, AtlasObjs.Texture.Size);
			array[2] = BillboardParent_VPCT.CreatePlane(2f, 2f, 0.75f);
			array[2].SetUV(BoardingFSEffect.circleTexturePath, AtlasObjs.Texture.Size);
			this.{24443} = new SpriteBatch3D<VertexPositionColorTexture>(18);
			this.{24443}.Add(array[0].array);
			this.{24443}.Add(array[1].array);
			this.{24443}.Add(array[2].array);
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x000E823E File Offset: 0x000E643E
		protected override void Lost_Callback(ShipCleanupEventArgs {24435})
		{
			BoardingFSEffect.ActiveEffects.FastRemove(this);
			base.Lost_Callback({24435});
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x000E8254 File Offset: 0x000E6454
		public void CheckAndQueueToRemove(int {24436}, int {24437})
		{
			if (this.ship.uID == {24436} || this.ship.uID == {24437} || this.{24444}.UID == {24436} || this.{24444}.UID == {24437})
			{
				this.{24441} = Math.Min(this.{24441}, 2000f);
			}
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000E82B0 File Offset: 0x000E64B0
		protected override bool Update(ref FrameTime {24438})
		{
			if (this.{24444}.GetShipNotDestroyed == null)
			{
				this.{24441} = Math.Min(this.{24441}, 2000f);
			}
			else
			{
				Ship getShip = this.{24444}.GetShip;
				if (this.ship == Global.Player)
				{
					this.{24440}.Translation = this.ship.Position3D;
				}
				else if (getShip == Global.Player)
				{
					this.{24440}.Translation = getShip.Position3D;
				}
				else
				{
					this.{24440}.Translation = (this.ship.Position3D + getShip.Position3D) * 0.5f;
				}
				if ({24438}.EvaluteTimerMs2(ref this.{24448}))
				{
					this.{24448} = 7500f;
					Global.Game.SoundSystem.Play3DSound(GameDynamicSoundName.BoardingContinue, this.ship.Position3D, 1f, false);
				}
				this.{24447} = this.ship.UsedShip.BoardingFailureDisatnce(this.ship, getShip, false);
				this.{24440}.Scales.X = this.{24447};
				this.{24440}.Scales.Z = this.{24447};
			}
			this.{24440}.Yaw += {24438}.secElapsed * 1.5f;
			this.{24441} -= {24438}.msElapsed;
			if (this.{24445}.Sample(ref {24438}))
			{
				FXEngine.SampleFumesSmoke(this.{24440}.Translation + Rand.NextVector3(-4f, 4f), 1f, 1f, 1f);
			}
			this.{24442} = Color.White * ((this.{24441} > this.{24446} + 2000f) ? ((this.{24441} - this.{24446} + 2000f) / 500f) : ((this.{24441} < 2000f) ? (this.{24441} / 2000f) : 1f)) * 0.1f;
			bool flag = this.{24441} < 0f;
			if (flag)
			{
				BoardingFSEffect.ActiveEffects.FastRemove(this);
			}
			return flag;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000E84DC File Offset: 0x000E66DC
		public override void Render3D()
		{
			if (this.{24447} > 0f)
			{
				Global.Render.ItemsShader.RenderCircle(new Vector3(this.{24440}.Translation.X, -100f, this.{24440}.Translation.Z), this.{24447}, this.{24447} + 1f, Color.OrangeRed * 0.2f, GPUCircleType.SoftCircle, null);
			}
			Global.Render.ItemsShader.SetForRender(this.{24440}.CreateWorldMatrix(), this.{24442}.ToVector4());
			Global.Render.ItemsShader.BeginPass(false, true);
			this.{24439}.Render();
			Global.Render.ItemsShader.BeginPass(true, true);
			this.{24443}.Render(null);
		}

		// Token: 0x04001859 RID: 6233
		public static Tlist<BoardingFSEffect> ActiveEffects = new Tlist<BoardingFSEffect>();

		// Token: 0x0400185A RID: 6234
		private const float c_startTime = 500f;

		// Token: 0x0400185B RID: 6235
		private const float c_endTime = 2000f;

		// Token: 0x0400185C RID: 6236
		private static readonly Rectangle circleTexturePath = new Rectangle(1600, 1, 164, 164);

		// Token: 0x0400185D RID: 6237
		private UserMesh {24439};

		// Token: 0x0400185E RID: 6238
		private Transform3D {24440};

		// Token: 0x0400185F RID: 6239
		private float {24441};

		// Token: 0x04001860 RID: 6240
		private Color {24442};

		// Token: 0x04001861 RID: 6241
		private SpriteBatch3D<VertexPositionColorTexture> {24443};

		// Token: 0x04001862 RID: 6242
		private WeakShipReference {24444};

		// Token: 0x04001863 RID: 6243
		private Timer {24445};

		// Token: 0x04001864 RID: 6244
		private float {24446};

		// Token: 0x04001865 RID: 6245
		private float {24447};

		// Token: 0x04001866 RID: 6246
		private float {24448};
	}
}
