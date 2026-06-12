using System;
using System.Linq;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Graphics
{
	// Token: 0x02000444 RID: 1092
	internal sealed class IntroScreenRenderer
	{
		// Token: 0x06001799 RID: 6041 RVA: 0x000CA5C8 File Offset: 0x000C87C8
		public IntroScreenRenderer(string {23368}, bool {23369}, IntroScreenRenderer.ExtraEffects {23370}, GameStaticSoundName? {23371})
		{
			this.{23377} = {23370};
			this.{23375} = {23369};
			string[] source = {23368}.Split(new string[]
			{
				"$"
			}, StringSplitOptions.RemoveEmptyEntries);
			this.{23373} = {23368}.Replace("$", Environment.NewLine);
			this.{23376} = source.Max((string {23378}) => {23378}.Length);
			this.{23374} = 0f;
			Global.Game.SoundSystem.PlaySound({23371}.GetValueOrDefault(GameStaticSoundName.Mapscreen), 0.03f, 1f);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x000CA66D File Offset: 0x000C886D
		public bool Update(ref FrameTime {23372})
		{
			this.{23374} += {23372}.msElapsed * (this.{23375} ? 0.66f : 1.3f);
			return this.{23374} > 6000f;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x000CA6A4 File Offset: 0x000C88A4
		public void Render()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
			float scale = (this.{23374} < 500f) ? (this.{23374} / 500f) : ((this.{23374} < 3000f) ? 1f : (1f - (this.{23374} - 3000f) / 3000f));
			float scale2 = (this.{23374} < 500f) ? (this.{23374} / 500f) : ((this.{23374} < 2000f) ? 1f : (1f - (this.{23374} - 2000f) / 3000f));
			Device gs = Engine.GS;
			Rectangle rectangle = Engine.GS.UIArea;
			Color color = Color.Black * scale2;
			gs.Draw(CommonAtlas.whitePixel, rectangle, color);
			Vector2 vector2;
			for (int i = 0; i < 400; i++)
			{
				Vector2 vector = new Vector2((float)(Engine.GS.UIArea.Width - 400), (float)Engine.GS.UIArea.Height);
				float num = HashHelper.greater(i) * 2f + 0.25f;
				float num2 = (this.{23374} + (float)HashHelper.greaterInt(i, 2000)) % 2000f;
				num2 /= 2000f;
				vector.Y -= num2 * num * 400f;
				vector.X += MathF.Sin(num2 * (HashHelper.greater(i * i * i) + 0.5f) * 0.4f + 6f * HashHelper.greater(i * i)) * 500f * num;
				float scale3 = ((num2 < 0.2f) ? (num2 / 0.2f) : ((num2 < 0.6f) ? 1f : (1f - (num2 - 0.6f) / 0.4f))) / num;
				Device gs2 = Engine.GS;
				vector2 = new Vector2(3f, 3f);
				rectangle = new Marker(ref vector, ref vector2).ToRect();
				color = Color.Lerp(Color.OrangeRed, Color.White, num2) * scale * scale3 * 0.2f;
				gs2.Draw(CommonAtlas.whitePixel, rectangle, color);
				if (i % 5 == 0)
				{
					Device gs3 = Engine.GS;
					vector2 = new Vector2(120f, 120f);
					rectangle = new Marker(ref vector, ref vector2).ToRect();
					color = Color.Lerp(Color.Cyan, Color.SeaGreen, num2) * scale * scale3 * 0.01f;
					gs3.Draw(CommonAtlas.whiteDot, rectangle, color);
				}
			}
			if (this.{23377} != IntroScreenRenderer.ExtraEffects.None)
			{
				Rectangle rectangle2 = (this.{23377} == IntroScreenRenderer.ExtraEffects.Blood) ? CommonAtlas.pBlood : CommonAtlas.pLight;
				for (int j = 0; j < 50; j++)
				{
					float num3 = (this.{23374} + (float)HashHelper.greaterInt(j, 2000)) % 2000f;
					num3 /= 2000f;
					float scale4 = (num3 < 0.2f) ? (num3 / 0.2f) : ((num3 < 0.6f) ? 1f : (1f - (num3 - 0.6f) / 0.4f));
					Vector2 vector3 = new Vector2((float)(Engine.GS.UIArea.Width - 300), (float)(Engine.GS.UIArea.Height - 70));
					vector3.X += (HashHelper.greater(-9310 + j * j) - 0.5f) * 700f;
					vector3.Y += (HashHelper.greater(2255 + j * j) - 0.5f) * 400f;
					Device gs4 = Engine.GS;
					vector2 = new Vector2(100f, 100f);
					rectangle = new Marker(ref vector3, ref vector2).ToRect();
					color = Color.White * scale * scale4 * 0.3f;
					gs4.Draw(rectangle2, rectangle, color);
				}
			}
			float num4 = (this.{23374} < 1000f) ? 0f : ((this.{23374} < 2000f) ? ((this.{23374} - 1000f) / 1000f) : ((this.{23374} < 3000f) ? 1f : Geometry.Saturate(1f - (this.{23374} - 3000f) / 2000f)));
			Engine.GS.SetFont((this.{23373}.Length > 20) ? Fonts.Philosopher_24 : Fonts.Philosopher_36);
			Device gs5 = Engine.GS;
			string {14599} = this.{23373};
			vector2 = new Vector2((float)(Engine.GS.UIArea.Width - 350 - this.{23376} * 15) - ((this.{23374} < 3000f) ? (num4 * 30f) : 30f), (float)(Engine.GS.UIArea.Height - 160));
			color = Color.White * num4;
			gs5.DrawString({14599}, vector2, color);
		}

		// Token: 0x040015F0 RID: 5616
		public static readonly float AnimationTime = 500f;

		// Token: 0x040015F1 RID: 5617
		private string {23373};

		// Token: 0x040015F2 RID: 5618
		private float {23374};

		// Token: 0x040015F3 RID: 5619
		private bool {23375};

		// Token: 0x040015F4 RID: 5620
		private int {23376};

		// Token: 0x040015F5 RID: 5621
		private IntroScreenRenderer.ExtraEffects {23377};

		// Token: 0x02000445 RID: 1093
		public enum ExtraEffects
		{
			// Token: 0x040015F7 RID: 5623
			None,
			// Token: 0x040015F8 RID: 5624
			Blood,
			// Token: 0x040015F9 RID: 5625
			Lights
		}
	}
}
