using System;
using System.Collections.Generic;
using System.Linq;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using UWContentPipelineExtensionRuntime.Tags;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Graphics.Effects
{
	// Token: 0x020004B1 RID: 1201
	internal sealed class WhalesFormWaterFSEffect : ShipEffect
	{
		// Token: 0x06001A4B RID: 6731 RVA: 0x000EA3BC File Offset: 0x000E85BC
		public WhalesFormWaterFSEffect(Ship {24606}) : base({24606})
		{
			Vector2 vector = Engine.GS.Camera.Direction.XZNormal();
			Vector2 value = {24606}.Position + vector * 70f;
			vector = Geometry.RotateVector2(vector, 1.5707964f);
			for (int i = 0; i < 8; i++)
			{
				Tlist<WhalesFormWaterFSEffect.WhaleInstance> tlist = this.{24608};
				WhalesFormWaterFSEffect.WhaleInstance whaleInstance = new WhalesFormWaterFSEffect.WhaleInstance(value + Rand.NextVector2(-15f, 15f), vector);
				tlist.Add(whaleInstance);
			}
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000EA448 File Offset: 0x000E8648
		protected override bool Update(ref FrameTime {24607})
		{
			for (int i = 0; i < this.{24608}.Size; i++)
			{
				if (this.{24608}[i].Update(ref {24607}))
				{
					this.{24608}.RemoveAt(i);
					i--;
				}
			}
			return this.{24608}.Size == 0;
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00003100 File Offset: 0x00001300
		public override void Render2D()
		{
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000EA4A0 File Offset: 0x000E86A0
		public override void Render3D()
		{
			foreach (WhalesFormWaterFSEffect.WhaleInstance whaleInstance in ((IEnumerable<WhalesFormWaterFSEffect.WhaleInstance>)this.{24608}))
			{
				whaleInstance.Draw();
			}
		}

		// Token: 0x040018BC RID: 6332
		private Tlist<WhalesFormWaterFSEffect.WhaleInstance> {24608} = new Tlist<WhalesFormWaterFSEffect.WhaleInstance>();

		// Token: 0x020004B2 RID: 1202
		private class WhaleInstance
		{
			// Token: 0x17000235 RID: 565
			// (get) Token: 0x06001A4F RID: 6735 RVA: 0x000EA4EC File Offset: 0x000E86EC
			private float linearSpeed
			{
				get
				{
					return 5.5f;
				}
			}

			// Token: 0x17000236 RID: 566
			// (get) Token: 0x06001A50 RID: 6736 RVA: 0x000EA4F3 File Offset: 0x000E86F3
			private ValueTuple<float, float> heightRange
			{
				get
				{
					return new ValueTuple<float, float>(-3f, 0.4f);
				}
			}

			// Token: 0x06001A51 RID: 6737 RVA: 0x000EA504 File Offset: 0x000E8704
			public WhaleInstance(Vector2 {24611}, Vector2 {24612})
			{
				this.{24615} = Rand.Range(2000f, 3000f);
				this.{24614} = Rand.Range(0f, -4000f);
				this.{24616} = {24611}.X0Y();
				this.{24617} = {24612};
				UWModel uwmodel = Rand.Pick<UWModel>(new UWModel[]
				{
					LocalContent.Loaded.SpermWhale,
					LocalContent.Loaded.WhaleOrca
				});
				this.{24618} = new ModelTransformedScene();
				this.{24618}.AddObject(new ModelRenderer(uwmodel, uwmodel.SkinningDataOrNull.AnimationClips.First<KeyValuePair<string, AnimationClip>>().Key, 3.4f), true);
				this.{24620} = new Timer(300f);
			}

			// Token: 0x06001A52 RID: 6738 RVA: 0x000EA5C4 File Offset: 0x000E87C4
			internal bool Update(ref FrameTime {24613})
			{
				if (this.{24614} > 0f)
				{
					this.{24616}.X = this.{24616}.X - this.{24617}.X * {24613}.secElapsed * this.linearSpeed;
					this.{24616}.Z = this.{24616}.Z - this.{24617}.Y * {24613}.secElapsed * this.linearSpeed;
				}
				if (this.{24620}.Sample(ref {24613}) && this.{24616}.Y > -3f && this.{24616}.Y < 0f)
				{
					FXEngine.CreateSingleWaterParticle2(this.{24616}, 3f, true, 1f);
				}
				this.{24614} += {24613}.msElapsed;
				return this.{24614} > this.{24615};
			}

			// Token: 0x06001A53 RID: 6739 RVA: 0x000EA698 File Offset: 0x000E8898
			internal void Draw()
			{
				float num = Geometry.Saturate(this.{24614} / this.{24615});
				float num2 = num * (1f - num) * 4f;
				this.{24616}.Y = MathHelper.Lerp(this.heightRange.Item1, this.heightRange.Item2, num2);
				bool flag = false;
				if (this.{24619} == 0 && num2 > 0.5f)
				{
					flag = true;
					this.{24619} = 1;
				}
				else if (this.{24619} == 1 && num2 < 0.5f)
				{
					flag = true;
					this.{24619} = 2;
				}
				if (flag)
				{
					for (int i = 0; i < 10; i++)
					{
						FXEngine.CreateSingleWaterParticle2(this.{24616} + this.{24617}.X0Y() * (float)i * 0.2f, 3f, true, 1f);
					}
				}
				this.{24618}.Transform.Scales = new Vector3(0.3f);
				this.{24618}.Transform.Translation = this.{24616};
				this.{24618}.Transform.Pitch = -(num - 0.5f) * 0.6f;
				this.{24618}.Transform.Yaw = MathF.Atan2(this.{24617}.Y, this.{24617}.X) + 1.5707964f;
				Global.Render.CommonShader.RenderObject(this.{24618}, false, 1f, false, 0f, false);
			}

			// Token: 0x040018BD RID: 6333
			private float {24614};

			// Token: 0x040018BE RID: 6334
			private float {24615};

			// Token: 0x040018BF RID: 6335
			private Vector3 {24616};

			// Token: 0x040018C0 RID: 6336
			private Vector2 {24617};

			// Token: 0x040018C1 RID: 6337
			private ModelTransformedScene {24618};

			// Token: 0x040018C2 RID: 6338
			private int {24619};

			// Token: 0x040018C3 RID: 6339
			private Timer {24620};
		}
	}
}
