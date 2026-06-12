using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Shaders.PublicShaders;
using TheraEngine.Components.Scene;
using TheraEngine.Graphics;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;

namespace TheraEngine.Scene.Lighting
{
	// Token: 0x0200005D RID: 93
	public class LightFlareRender
	{
		// Token: 0x06000271 RID: 625 RVA: 0x0000E3B8 File Offset: 0x0000C5B8
		public LightFlareRender(Rectangle {12269}, Rectangle {12270}, Rectangle {12271}, Rectangle {12272}, Rectangle {12273}, Rectangle {12274})
		{
			this.{12296} = {12269};
			this.{12288} = {12270};
			this.{12291} = {12271};
			this.{12292} = {12272};
			this.{12294} = {12273};
			this.{12295} = {12274};
			this.{12297} = new Vector2((float)({12269}.Width / 2), (float)({12269}.Height / 2));
			this.{12289} = new Vector2((float)({12270}.Width / 2), (float)({12270}.Height / 2));
			this.{12290} = new Vector2((float)({12271}.Width / 2), (float)({12271}.Height / 2));
			this.{12293} = new Vector2((float)({12272}.Width / 2), (float)({12272}.Height / 2));
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000E470 File Offset: 0x0000C670
		public void RenderAmbientLight(LightSourceOcclusionTest {12275}, SceneManager {12276}, SkyRenderer {12277})
		{
			float lastResult = {12275}.LastResult;
			float num = (1f - {12277}.Sunrise) * 0.75f + 0.25f;
			this.{12278}({12276}, {12276}.MoonLightSource, this.{12294}, {12276}.MoonAlpha * lastResult * 0.5f);
			this.{12278}({12276}, {12276}.SunLightSource, this.{12295}, {12276}.SunAlpha * lastResult * 0.5f * num);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		private void {12278}(SceneManager {12279}, AmbientLightSource {12280}, Rectangle {12281}, float {12282})
		{
			float num = Vector3.Dot(Engine.GS.Camera.Direction, {12280}.LightDirectionForRender);
			if (num <= 0f || {12282} <= 0f)
			{
				return;
			}
			bool flag = {12280} == {12279}.MoonLightSource;
			Vector2 projection = Engine.GS.Camera.GetProjection({12280}.RenderPosition);
			Vector2 vector = Engine.GS.CurrentOutputSize * 0.5f;
			float num2 = Vector2.Distance(projection, vector);
			float num3 = (1f + (1f - Math.Abs(num2)) / (vector.Length() + 64f)) * (flag ? 0.2f : 1f) * {12282};
			Color color2;
			if (num3 >= 0.01f)
			{
				Color color = new Color(num3, num3, num3, num3 - 0.09803922f);
				float rotate = Geometry.GetRotate(vector, projection);
				Vector2 vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 1.5f);
				Engine.GS.Draw(this.{12291}, vector2, this.{12290}, 0f, color);
				vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 2f);
				Device gs = Engine.GS;
				Vector2 {14552} = this.{12293};
				float {14553} = 0f;
				color2 = new Color(num3, num3, num3, num3 - 50f);
				gs.Draw(this.{12292}, vector2, {14552}, {14553}, color2);
				vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 4f);
				Engine.GS.Draw(this.{12291}, vector2, this.{12290}, 0f, color);
				vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 6f);
				Engine.GS.Draw(this.{12292}, vector2, this.{12293}, 0f, color);
				vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 8f);
				Device gs2 = Engine.GS;
				float {14558} = 0f;
				float {14559} = 0.7f;
				color2 = new Color(num3, 0f, 0f, num3 - 50f);
				gs2.Draw(this.{12291}, vector2, this.{12290}, {14558}, {14559}, color2);
				vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 10f);
				Engine.GS.Draw(this.{12291}, vector2, this.{12290}, 0f, color);
				vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 15f);
				Engine.GS.Draw(this.{12292}, vector2, this.{12293}, 0f, color);
			}
			Vector2 vector3 = flag ? new Vector2(310f, 310f / Engine.Game.GetAspectRatio) : new Vector2(128f);
			Vector2 vector4;
			vector4.X = Engine.GS.CurrentOutputSize.X / vector3.X;
			vector4.Y = Engine.GS.CurrentOutputSize.Y / vector3.Y;
			Vector2 projection2 = Engine.GS.Camera.GetProjection({12280}.RenderPosition);
			Device gs3 = Engine.GS;
			Vector2 vector5 = new Vector2((float){12281}.Width, (float){12281}.Height) * 0.5f;
			float {14564} = 0f;
			color2 = Color.White * {12282} * (num * (flag ? 0.8f : 0.6f));
			gs3.Draw({12281}, projection2, vector5, {14564}, vector4, color2);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000E848 File Offset: 0x0000CA48
		public void RenderSimpleFlare(Vector3 {12283}, float {12284}, float {12285}, Vector3 {12286})
		{
			if ({12285} > 0.01f && Engine.GS.Camera.IsVisible({12283}, 2f))
			{
				{12284} *= Engine.GS.CurrentOutputSize.Length() / 1000f;
				{12284} /= Math.Max(1f, Vector3.Distance(Engine.GS.Camera.Position, {12283}));
				Vector2 projection = Engine.GS.Camera.GetProjection({12283});
				Vector2 vector = Engine.GS.CurrentOutputSize * 0.5f;
				float num = Vector2.Distance(projection, vector);
				float num2 = (1f + (1f - Math.Abs(num)) / (vector.Length() + 64f)) * {12285};
				if (num2 >= 0.01f)
				{
					float rotate = Geometry.GetRotate(vector, projection);
					float num3 = Math.Min(1f, num2 * 2f);
					Vector2 vector2 = vector + Geometry.SubstructRotate(rotate, num);
					Device gs = Engine.GS;
					float {14558} = 0f;
					float {14559} = {12284};
					Color color = new Color(num3 * {12286}.X, num3 * {12286}.Y, num3 * {12286}.Z, num3 - 15f) * 0.5f;
					gs.Draw(this.{12292}, vector2, this.{12293}, {14558}, {14559}, color);
					Device gs2 = Engine.GS;
					float {14558}2 = 0f;
					float {14559}2 = {12284} * 0.5f;
					color = new Color(num3 * {12286}.X, num3 * {12286}.Y, num3 * {12286}.Z, num3 - 15f) * 0.5f;
					gs2.Draw(this.{12292}, vector2, this.{12293}, {14558}2, {14559}2, color);
					Device gs3 = Engine.GS;
					float {14558}3 = 0f;
					float {14559}3 = 0.3f * {12284};
					color = new Color(num3 * {12286}.X, num3 * {12286}.Y, num3 * {12286}.Z, num3 - 15f) * 0.5f;
					gs3.Draw(this.{12296}, vector2, this.{12297}, {14558}3, {14559}3, color);
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000EA58 File Offset: 0x0000CC58
		public void RenderPointlight(PointLight {12287})
		{
			float num = 0.5f;
			if (num != 0f && Engine.GS.Camera.IsVisible({12287}.Position, 2f))
			{
				Vector2 projection = Engine.GS.Camera.GetProjection({12287}.Position);
				Vector2 vector = Engine.GS.CurrentOutputSize * 0.5f;
				float num2 = Vector2.Distance(projection, vector);
				float num3 = 1f + (1f - Math.Abs(num2)) / (vector.Length() + 64f);
				num3 *= num;
				if (num3 >= 0.01f && {12287}.OlccusionaryFlaresOpacity != 0f)
				{
					Color color = new Color(num3, num3, num3, num3 - 0.09803922f) * {12287}.OlccusionaryFlaresOpacity;
					float rotate = Geometry.GetRotate(vector, projection);
					float num4 = Math.Min(1f, num3 * 2f);
					Vector2 vector2 = vector + Geometry.SubstructRotate(rotate, num2);
					Device gs = Engine.GS;
					float {14558} = 0f;
					float {14559} = 0.5f * {12287}.CentralFlareScale;
					Color color2 = new Color(num4, num4, num4, num4 - 15f);
					gs.Draw(this.{12296}, vector2, this.{12297}, {14558}, {14559}, color2);
					vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 2f);
					Engine.GS.Draw(this.{12291}, vector2, this.{12290}, 0f, 0.4f, color);
					vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 3f);
					Device gs2 = Engine.GS;
					Vector2 {14552} = this.{12293};
					float {14553} = 0f;
					color2 = new Color(num3, num3, num3, num3 - 50f);
					gs2.Draw(this.{12292}, vector2, {14552}, {14553}, color2);
					vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 5f);
					Engine.GS.Draw(this.{12291}, vector2, this.{12290}, 0f, color);
					vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 7f);
					Engine.GS.Draw(this.{12292}, vector2, this.{12293}, 0f, color);
					vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 9f);
					Device gs3 = Engine.GS;
					float {14558}2 = 0f;
					float {14559}2 = 0.7f;
					color2 = new Color(num3, 0f, 0f, num3 - 50f) * {12287}.OlccusionaryFlaresOpacity;
					gs3.Draw(this.{12291}, vector2, this.{12290}, {14558}2, {14559}2, color2);
					vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 11f);
					Engine.GS.Draw(this.{12291}, vector2, this.{12290}, 0f, color);
					vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 15f);
					Engine.GS.Draw(this.{12292}, vector2, this.{12293}, 0f, color);
					vector2 = vector + Geometry.SubstructRotate(rotate, num2 / 19f);
					Device gs4 = Engine.GS;
					float {14558}3 = 0f;
					float {14559}3 = 0.7f;
					color2 = new Color(num3, 0f, 0f, num3 - 50f) * {12287}.OlccusionaryFlaresOpacity;
					gs4.Draw(this.{12291}, vector2, this.{12290}, {14558}3, {14559}3, color2);
				}
			}
		}

		// Token: 0x040001F0 RID: 496
		private Rectangle {12288};

		// Token: 0x040001F1 RID: 497
		private Vector2 {12289};

		// Token: 0x040001F2 RID: 498
		private Vector2 {12290};

		// Token: 0x040001F3 RID: 499
		private Rectangle {12291};

		// Token: 0x040001F4 RID: 500
		private Rectangle {12292};

		// Token: 0x040001F5 RID: 501
		private Vector2 {12293};

		// Token: 0x040001F6 RID: 502
		private Rectangle {12294};

		// Token: 0x040001F7 RID: 503
		private Rectangle {12295};

		// Token: 0x040001F8 RID: 504
		private Rectangle {12296};

		// Token: 0x040001F9 RID: 505
		private Vector2 {12297};
	}
}
