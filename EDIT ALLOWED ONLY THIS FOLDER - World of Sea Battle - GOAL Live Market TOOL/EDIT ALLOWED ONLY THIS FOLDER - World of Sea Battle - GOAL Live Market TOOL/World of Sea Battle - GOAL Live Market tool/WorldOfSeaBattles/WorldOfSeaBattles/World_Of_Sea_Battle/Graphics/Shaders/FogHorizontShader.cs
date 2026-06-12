using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Assets.Shaders;
using TheraEngine.Components;
using TheraEngine.Core;
using TheraEngine.Graphics;
using TheraEngine.ProcedureGeneration.Generation3D;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Graphics.Shaders
{
	// Token: 0x0200045B RID: 1115
	internal sealed class FogHorizontShader : Shader
	{
		// Token: 0x06001855 RID: 6229 RVA: 0x000D3148 File Offset: 0x000D1348
		public FogHorizontShader(string {23616}, ContentManager {23617}) : base({23616}, {23617})
		{
			this.{23620} = base.GetPass(0);
			this.{23621} = base.GetPass(1);
			this.{23624} = base.GetProperty("CameraPosition");
			this.{23623} = base.GetProperty("ViewProj");
			this.{23622} = base.GetProperty("World");
			this.{23625} = base.GetProperty("LightColor");
			this.{23627} = base.GetProperty("LightDirection");
			this.{23626} = base.GetProperty("FogAmounts");
			this.{23628} = base.GetProperty("DiffuseLightColor");
			this.{23630} = base.GetProperty("hazeTex");
			this.{23629} = base.GetProperty("txOffset");
			this.{23631} = RingAreaGenerator.Begin_VertexPositionColor(12, -16f, 3f, 30f, Color.Transparent, Color.White, Color.Transparent, 160f);
			this.{23632} = PlaneGenerator.Begin_VertexPositionTexture(new Vector4(0f, 0f, 1f, 1f), 1f, 1f, Vector3.Zero, 4);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x000D3274 File Offset: 0x000D1474
		public void Render(bool {23618})
		{
			this.{23624}.SetValue(Engine.GS.Camera.Position);
			this.{23627}.SetValue(-Global.Render.GetSceneManager.SunLightSource.LightDirectionForRender);
			this.{23626}.SetValue(Global.Render.CommonShader.FogLerpFactorLastValue);
			this.{23628}.SetValue(Global.Game.StaticSystem.GetSkyShader.CurrentDiffuseColor);
			this.{23623}.SetValue(Engine.GS.Camera.ViewMultiplyProjection);
			this.{23625}.SetValue(new Vector4(Global.Game.StaticSystem.GetSkyShader.CurrentFogColor, 1f));
			float num = 1f;
			Matrix value = new Transform3D(new Vector3(Engine.GS.Camera.Position.X, (float)(Renderer.ReflectionsAreBeingDrawn ? -1 : 1) * Engine.GS.Camera.Position.Y / 1.7f - 3f + 1f, Engine.GS.Camera.Position.Z), Vector3.Zero, new Vector3(num, 1f, num)).CreateWorldMatrix();
			this.{23622}.SetValue(value);
			this.{23620}.Apply();
			this.{23631}.Render();
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x000D33E0 File Offset: 0x000D15E0
		public void RenderHaze(float {23619})
		{
			if (Renderer.ReflectionsAreBeingDrawn || {23619} == 0f)
			{
				return;
			}
			this.{23629}.SetValue((float)(Global.Game.GameTotalTimeSec * 0.0024999999441206455 % 1.0));
			this.{23630}.SetValue(OtherTextures.HazeLayerTex);
			this.{23623}.SetValue(Engine.GS.Camera.ViewMultiplyProjection);
			this.{23625}.SetValue(new Vector4(Global.Game.StaticSystem.GetSkyShader.CurrentFogColor * 0.8f, 1f) * {23619} * 1.5f);
			Matrix value = new Transform3D(new Vector3(Engine.GS.Camera.Position.X, 0.5f, Engine.GS.Camera.Position.Z), Vector3.Zero, new Vector3(600f, 1f, 600f)).CreateWorldMatrix();
			this.{23622}.SetValue(value);
			this.{23621}.Apply();
			this.{23632}.Render();
		}

		// Token: 0x040016B5 RID: 5813
		public static float VolumtericFogMultiplier = 1f;

		// Token: 0x040016B6 RID: 5814
		private const float c_downLevel = -16f;

		// Token: 0x040016B7 RID: 5815
		private const float c_CentralLevel = 3f;

		// Token: 0x040016B8 RID: 5816
		private const float c_upLevel = 30f;

		// Token: 0x040016B9 RID: 5817
		private const float c_radius = 160f;

		// Token: 0x040016BA RID: 5818
		private EffectPass {23620};

		// Token: 0x040016BB RID: 5819
		private EffectPass {23621};

		// Token: 0x040016BC RID: 5820
		private EffectParameter {23622};

		// Token: 0x040016BD RID: 5821
		private EffectParameter {23623};

		// Token: 0x040016BE RID: 5822
		private EffectParameter {23624};

		// Token: 0x040016BF RID: 5823
		private EffectParameter {23625};

		// Token: 0x040016C0 RID: 5824
		private EffectParameter {23626};

		// Token: 0x040016C1 RID: 5825
		private EffectParameter {23627};

		// Token: 0x040016C2 RID: 5826
		private EffectParameter {23628};

		// Token: 0x040016C3 RID: 5827
		private EffectParameter {23629};

		// Token: 0x040016C4 RID: 5828
		private EffectParameter {23630};

		// Token: 0x040016C5 RID: 5829
		private UserMesh {23631};

		// Token: 0x040016C6 RID: 5830
		private UserIndexedMesh {23632};
	}
}
