using System;
using System.Runtime.CompilerServices;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Grphics.Device;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001D0 RID: 464
	internal class {19261}
	{
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x000538B4 File Offset: 0x00051AB4
		public bool IsEmpty
		{
			get
			{
				return this.{19271}.IsEmpty;
			}
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000538C1 File Offset: 0x00051AC1
		public {19261}(GSI {19263})
		{
			this.{19272} = {19263};
			this.{19271} = {19263}.Clone();
			this.{19264}();
			this.{19270} = 0.5f;
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000538F0 File Offset: 0x00051AF0
		private void {19264}()
		{
			if (this.{19271}.IsEmpty)
			{
				return;
			}
			int num = 5;
			double num2 = Math.Floor((double)MathHelper.Lerp((float)num, 1f, (float)this.{19271}.GetTotalItemsCount() / (float)this.{19272}.GetTotalItemsCount()));
			num2 = Math.Min((double)(num - 1), num2);
			this.{19268} = new Tlist<ResourceInfo>();
			int num3 = 0;
			while ((double)num3 < (double)num - num2)
			{
				Tlist<ResourceInfo> tlist = this.{19268};
				ResourceInfo resourceInfo = Gameplay.ItemsInfo.FromID(this.{19271}.RandomName());
				tlist.Add(resourceInfo);
				num3++;
			}
			int num4 = 0;
			while ((double)num4 < num2)
			{
				Tlist<ResourceInfo> tlist2 = this.{19268};
				ResourceInfo resourceInfo = null;
				tlist2.Add(resourceInfo);
				num4++;
			}
			this.{19268}.Shuffle();
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x000539B0 File Offset: 0x00051BB0
		[return: TupleElementNames(new string[]
		{
			"removeAmount",
			"successfull"
		})]
		public ValueTuple<GSI, bool> PickCurrent()
		{
			if (this.{19273} != 0f)
			{
				return default(ValueTuple<GSI, bool>);
			}
			this.{19274} = (int)Math.Floor((double)Math.Min((float)(this.{19268}.Size - 1), this.{19269} * (float)this.{19268}.Size));
			ResourceInfo resourceInfo = this.{19268}[this.{19274}];
			bool flag = resourceInfo != null;
			GSI gsi = (resourceInfo == null) ? this.{19272}.Clone().RandomCut((float)this.{19272}.GetTotalItemsCount() * 0.25f, -1) : new GSI().Exs((int)resourceInfo.ID, (int)Math.Ceiling((double)((float)this.{19272}[(int)resourceInfo.ID] * 0.5f)));
			gsi.Limitize(this.{19271});
			this.{19271}.Remove(gsi, 1, false);
			this.{19270} = (float)Math.Sign(this.{19270}) * (Math.Abs(this.{19270}) + 0.33f);
			this.{19273} = 700f;
			if (flag)
			{
				if (this.{19271}.IsEmpty)
				{
					Global.Game.SoundSystem.PlaySound(GameStaticSoundName.V_Victory, 0.03f, 1f);
				}
				else
				{
					Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Equip1, 0.03f, 1f);
				}
			}
			return new ValueTuple<GSI, bool>(gsi, flag);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x00053B10 File Offset: 0x00051D10
		public void Update(ref FrameTime {19265})
		{
			if ({19265}.EvaluteTimerMs2(ref this.{19273}))
			{
				this.{19264}();
			}
			if (this.{19273} == 0f)
			{
				this.{19270} = (float)((this.{19270} == 0f) ? 1 : Math.Sign(this.{19270})) * (Math.Abs(this.{19270}) + {19265}.secElapsed / 4f);
				this.{19270} = Math.Min(this.{19270}, 2f);
				this.{19269} += this.{19270} * {19265}.secElapsed;
				if (this.{19269} > 1f)
				{
					this.{19269} = 1f;
					this.{19270} *= -1f;
				}
				if (this.{19269} < 0f)
				{
					this.{19269} = 0f;
					this.{19270} *= -1f;
				}
			}
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x00053C00 File Offset: 0x00051E00
		public void Draw(Vector2 {19266})
		{
			int num = 48;
			{19266} -= new Vector2((float)(this.{19268}.Size * num) / 2f, 0f);
			Vector2 vector = {19266};
			Rectangle rectangle = new Rectangle(927, 303, 64, 64);
			Rectangle rectangle2 = new Rectangle(733, 601, 65, 120);
			Rectangle rectangle3 = new Rectangle(943, 370, 48, 48);
			Rectangle rectangle4 = new Rectangle(3, 664, 257, 65);
			Device gs = Engine.GS;
			Vector2 vector2 = {19266} + new Vector2(-8f, -9f);
			Color white = Color.White;
			gs.Draw(rectangle4, vector2, white);
			Rectangle rectangle6;
			for (int i = 0; i < this.{19268}.Size; i++)
			{
				ResourceInfo resourceInfo = this.{19268}[i];
				Rectangle rectangle5 = new Marker(ref vector, (float)num, (float)num).ToRect();
				if (resourceInfo == null)
				{
					Device gs2 = Engine.GS;
					white = Color.White;
					gs2.Draw(rectangle, rectangle5, white);
				}
				else
				{
					Device gs3 = Engine.GS;
					Texture2D iconTexture = resourceInfo.IconTexture;
					rectangle6 = resourceInfo.IconTexture.Bounds;
					white = Color.White;
					gs3.DrawCustomTexture(iconTexture, rectangle6, rectangle5, white);
				}
				vector.X += (float)num;
				if (this.{19273} != 0f && i == this.{19274})
				{
					Device gs4 = Engine.GS;
					white = Color.White;
					gs4.Draw(rectangle3, rectangle5, white);
				}
			}
			Device gs5 = Engine.GS;
			vector2 = {19266} + new Vector2((float)(this.{19268}.Size * num) * this.{19269} - (float)(rectangle2.Width / 2), -23f);
			rectangle6 = new Marker(ref vector2, 49f, 90f).ToRect();
			white = Color.White;
			gs5.Draw(rectangle2, rectangle6, white);
		}

		// Token: 0x0400096B RID: 2411
		private int {19267};

		// Token: 0x0400096C RID: 2412
		private Tlist<ResourceInfo> {19268};

		// Token: 0x0400096D RID: 2413
		private float {19269};

		// Token: 0x0400096E RID: 2414
		private float {19270};

		// Token: 0x0400096F RID: 2415
		private GSI {19271};

		// Token: 0x04000970 RID: 2416
		private GSI {19272};

		// Token: 0x04000971 RID: 2417
		private float {19273};

		// Token: 0x04000972 RID: 2418
		private int {19274};
	}
}
