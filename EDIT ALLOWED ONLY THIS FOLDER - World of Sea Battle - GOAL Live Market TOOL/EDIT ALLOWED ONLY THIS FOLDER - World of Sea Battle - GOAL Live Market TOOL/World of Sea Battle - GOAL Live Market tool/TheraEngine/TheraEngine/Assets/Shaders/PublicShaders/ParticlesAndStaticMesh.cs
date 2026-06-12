using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Components;
using TheraEngine.Components.Scene;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;
using TheraEngine.ProcedureGeneration.Generation3D;

namespace TheraEngine.Assets.Shaders.PublicShaders
{
	// Token: 0x0200017C RID: 380
	public class ParticlesAndStaticMesh : Shader
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x0003097F File Offset: 0x0002EB7F
		private static Effect SetTech(Effect {15440})
		{
			{15440}.CurrentTechnique = {15440}.Techniques["CurrentTechnique"];
			return {15440};
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x00030998 File Offset: 0x0002EB98
		public ParticlesAndStaticMesh() : base(ParticlesAndStaticMesh.SetTech(Engine.ParticlesAndStaticMeshEffect))
		{
			this.World = base.GetProperty("World");
			this.View = base.GetProperty("View");
			this.ViewProj = base.GetProperty("ViewProj");
			this.Texture = base.GetProperty("EffectTexture");
			this.DepthBufferTexture = base.GetProperty("DepthBuffer");
			this.DepthBufferDataMask = base.GetProperty("DepthBufferDataMask");
			this.ColorBasic = base.GetProperty("BasicColor");
			this.FogStart = base.GetProperty("FogStart");
			this.FogEnd = base.GetProperty("FogEnd");
			this.CameraPosition = base.GetProperty("CameraPosition");
			this.SimulationTime = base.GetProperty("SimulationTime");
			this.wawePosition = base.GetProperty("wawePosition");
			this.height = base.GetProperty("height");
			this.MainLightDirection = base.GetProperty("MainLightDirection");
			this.MainLightColor = base.GetProperty("MainLightColor");
			this.VPShadow = base.GetProperty("VPShadow");
			this.UVOffset = base.GetProperty("UVOffset");
			this.TXAAKernel = base.GetProperty("TXAAKernel");
			this.{15489} = base.GetPass("WithFog");
			this.{15491} = base.GetPass("WithoutFog");
			this.{15490} = base.GetPass("WithFogNoTx");
			this.{15492} = base.GetPass("WithFogAndDepthCompare");
			this.{15493} = base.GetPass("WithFogAndWaterSampling");
			this.{15494} = base.GetPass("VolumtericParticles");
			this.RenderVolumtericParticlesHeatMap = base.GetPass("VolumtericParticlesHeatMap");
			this.{15495} = base.GetPass("VolumtericParticlesWithShadow");
			this.{15496} = base.GetPass("TextureNoColor");
			this.{15497} = base.GetPass("WorldMapFog");
			this.CircleColor = base.GetProperty("CircleColor");
			this.CircleCenter = base.GetProperty("CircleCenter");
			this.CircleRange = base.GetProperty("CircleRange");
			this.SectorFarColor = base.GetProperty("SectorFarColor");
			this.SectorAxis = base.GetProperty("SectorAxis");
			this.SightDownLineXyXy = base.GetProperty("SightDownLineXyXy");
			this.SightUpLineXyXy = base.GetProperty("SightUpLineXyXy");
			this.SightCannonLeftRightXyXy = base.GetProperty("SightCannonLeftRightXyXy");
			this.SightCannonHasDraw = base.GetProperty("SightCannonHasDraw");
			this.CameraFarPlane = base.GetProperty("CameraFarPlane");
			this.WorldMapFogParam = base.GetProperty("WorldMapFogParam");
			this.WorldMapFogParam2 = base.GetProperty("WorldMapFogParam2");
			this.{15498} = base.GetPass("HardCircle");
			this.{15499} = base.GetPass("SoftCircle");
			this.{15500} = base.GetPass("PulsarCircle");
			this.{15501} = base.GetPass("DeliquescentCircle");
			this.{15502} = base.GetPass("HardCircleWithBorder");
			this.{15503} = base.GetPass("Sector");
			this.{15504} = base.GetPass("SightShader");
			ParticlesAndStaticMesh.circleRender = new BillboardParent_VPC();
			ParticlesAndStaticMesh.circleRender.SetCol(Color.White);
			ParticlesAndStaticMesh.grid5x5 = PlaneGenerator.Begin_VertexPositionTexture(new Vector4(0f, 0f, 1f, 1f), 1f, 1f, Vector3.Zero, 6);
			ParticlesAndStaticMesh.grid10x10 = PlaneGenerator.Begin_VertexPositionTexture(new Vector4(0f, 0f, 1f, 1f), 1f, 1f, Vector3.Zero, 11);
			ParticlesAndStaticMesh.grid20x20 = PlaneGenerator.Begin_VertexPositionTexture(new Vector4(0f, 0f, 1f, 1f), 1f, 1f, Vector3.Zero, 21);
			ParticlesAndStaticMesh.grid40x40 = PlaneGenerator.Begin_VertexPositionTexture(new Vector4(0f, 0f, 1f, 1f), 1f, 1f, Vector3.Zero, 41);
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x00030DC7 File Offset: 0x0002EFC7
		public void SetBasicProperties(Camera {15441}, Texture2DAtlas {15442}, SceneManager {15443}, SkyRenderer {15444})
		{
			this.SetBasicProperties({15441}, {15442}.texture, {15443}, {15444});
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x00030DDC File Offset: 0x0002EFDC
		public void SetBasicProperties(Camera {15445}, Texture2D {15446}, SceneManager {15447}, SkyRenderer {15448})
		{
			this.Texture.SetValue({15446});
			this.View.SetValue({15445}.ViewMatrix);
			this.ViewProj.SetValue({15445}.ViewMultiplyProjection);
			this.CameraFarPlane.SetValue({15445}.FarPlane);
			this.CameraPosition.SetValue({15445}.Position);
			this.SimulationTime.SetValue((float)Engine.Game.GameTotalTimeSec);
			this.MainLightDirection.SetValue({15447}.CurrentLightDirection);
			this.MainLightColor.SetValue({15448}.CurrentDiffuseColor);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00030E72 File Offset: 0x0002F072
		public void SetForRender(Matrix {15449}, Vector4 {15450})
		{
			this.ColorBasic.SetValue({15450});
			this.World.SetValue({15449});
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x00030E8C File Offset: 0x0002F08C
		public void ManualSetFog(float {15451}, float {15452})
		{
			this.fogNearDistance = {15451};
			this.fogFarDistance = {15452};
			this.FogStart.SetValue({15451});
			this.FogEnd.SetValue({15452});
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x00030EB4 File Offset: 0x0002F0B4
		public void RenderCircle(Vector3 {15453}, float {15454}, float {15455}, Color {15456}, GPUCircleType {15457}, UWModel {15458} = null)
		{
			if ({15454} * {15455} <= 0f || {15454} > {15455})
			{
				throw new ArgumentOutOfRangeException();
			}
			this.CircleColor.SetValue({15456}.ToVector4());
			this.CircleCenter.SetValue({15453});
			this.CircleRange.SetValue(new Vector2({15454}, {15455}));
			switch ({15457})
			{
			case GPUCircleType.HardCircle:
				this.{15498}.Apply();
				break;
			case GPUCircleType.SoftCircle:
				this.{15499}.Apply();
				break;
			case GPUCircleType.HardCirclePulsar:
				this.{15500}.Apply();
				break;
			case GPUCircleType.Deliquescent:
				this.{15501}.Apply();
				break;
			case GPUCircleType.HardWithBorder:
				this.{15502}.Apply();
				break;
			default:
				throw new NotSupportedException();
			}
			if ({15458} != null)
			{
				{15458}.OptimizedRenderAllBuffers();
				return;
			}
			if ({15455} <= 5f)
			{
				ParticlesAndStaticMesh.grid5x5.Render();
				return;
			}
			if ({15455} <= 10f)
			{
				ParticlesAndStaticMesh.grid10x10.Render();
				return;
			}
			if ({15455} <= 40f)
			{
				ParticlesAndStaticMesh.grid20x20.Render();
				return;
			}
			ParticlesAndStaticMesh.grid40x40.Render();
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x00030FB8 File Offset: 0x0002F1B8
		public void RenderSector(Vector3 {15459}, float {15460}, float {15461}, float {15462}, float {15463}, Color {15464}, Color {15465}, bool {15466} = false)
		{
			if ({15460} * {15461} <= 0f || {15460} > {15461})
			{
				throw new ArgumentOutOfRangeException();
			}
			this.CircleColor.SetValue({15464}.ToVector4());
			this.CircleCenter.SetValue({15459});
			this.CircleRange.SetValue(new Vector2({15460}, {15461}));
			this.SectorFarColor.SetValue({15465}.ToVector4());
			this.SectorAxis.SetValue(new Vector2({15462}, {15463}));
			this.{15503}.Apply();
			if ({15466})
			{
				ParticlesAndStaticMesh.grid40x40.Render();
				return;
			}
			if ({15461} <= 5f)
			{
				ParticlesAndStaticMesh.grid5x5.Render();
				return;
			}
			if ({15461} <= 10f)
			{
				ParticlesAndStaticMesh.grid10x10.Render();
				return;
			}
			if ({15461} <= 40f)
			{
				ParticlesAndStaticMesh.grid20x20.Render();
				return;
			}
			ParticlesAndStaticMesh.grid40x40.Render();
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0003108C File Offset: 0x0002F28C
		public void RenderProjectedSight(Vector2 {15467}, Vector2 {15468}, Vector2 {15469}, Vector2 {15470}, Vector2 {15471}, Vector2 {15472}, Color {15473})
		{
			this.CircleColor.SetValue({15473}.ToVector4());
			this.SightDownLineXyXy.SetValue(new Vector4({15467}.X, {15467}.Y, {15468}.X, {15468}.Y));
			this.SightUpLineXyXy.SetValue(new Vector4({15469}.X, {15469}.Y, {15470}.X, {15470}.Y));
			this.SightCannonLeftRightXyXy.SetValue(new Vector4({15471}.X, {15471}.Y, {15472}.X, {15472}.Y));
			this.SightCannonHasDraw.SetValue({15471}.X != 0f || {15471}.Y != 0f);
			BoundingSphere boundingSphere = BoundingSphere.CreateFromPoints(new Vector3[]
			{
				new Vector3({15467}.X, 0f, {15467}.Y),
				new Vector3({15468}.X, 0f, {15468}.Y),
				new Vector3({15469}.X, 0f, {15469}.Y),
				new Vector3({15470}.X, 0f, {15470}.Y)
			});
			this.CircleRange.SetValue(new Vector2(boundingSphere.Radius + 2f));
			this.CircleCenter.SetValue(boundingSphere.Center);
			this.{15504}.Apply();
			ParticlesAndStaticMesh.grid40x40.Render();
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0003121C File Offset: 0x0002F41C
		public void RenderWaterDecal(Texture2D {15474}, Vector2 {15475}, Vector4 {15476}, float {15477}, float {15478})
		{
			Texture2D valueTexture2D = this.Texture.GetValueTexture2D();
			this.Texture.SetValue({15474});
			this.{15505}.Translation = {15475}.X0Y();
			this.{15505}.Translation.Y = -100f;
			this.{15505}.MiddleScale = {15477};
			this.{15505}.Yaw = {15478};
			this.SetForRender(this.{15505}.CreateWorldMatrix(), {15476});
			this.{15493}.Apply();
			ParticlesAndStaticMesh.grid40x40.Render();
			this.Texture.SetValue(valueTexture2D);
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000312B4 File Offset: 0x0002F4B4
		public void SetOceanData(Vector2 {15479}, float {15480})
		{
			this.height.SetValue({15480});
			this.wawePosition.SetValue({15479});
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000312CE File Offset: 0x0002F4CE
		public void BeginPass(bool {15481}, bool {15482})
		{
			if (!{15481})
			{
				this.{15490}.Apply();
				return;
			}
			if ({15482})
			{
				this.{15489}.Apply();
				return;
			}
			this.{15491}.Apply();
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000312F9 File Offset: 0x0002F4F9
		public void BeginPassTextureNoColor()
		{
			this.{15496}.Apply();
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x00031308 File Offset: 0x0002F508
		public void BeginPassWorldMapFog(Vector2 {15483}, float {15484}, Vector2 {15485}, Vector2 {15486})
		{
			this.WorldMapFogParam2.SetValue({15486});
			this.WorldMapFogParam.SetValue(new Vector4({15483}.X, {15483}.Y, {15484}, {15485}.Y / {15485}.X));
			this.{15497}.Apply();
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00031357 File Offset: 0x0002F557
		public void VolumtericParticlesWithFog()
		{
			this.{15494}.Apply();
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x00031364 File Offset: 0x0002F564
		public void BeginDepthCompareParticles(Texture2D {15487}, Vector4 {15488})
		{
			if ({15487} == null)
			{
				throw new ArgumentNullException();
			}
			if ({15488}.X + {15488}.Y + {15488}.Z + {15488}.W != 1f)
			{
				throw new ArgumentException("Неверно задан параметр DepthBuffer:dataMask");
			}
			this.DepthBufferTexture.SetValue({15487});
			this.DepthBufferDataMask.SetValue({15488});
			this.{15492}.Apply();
		}

		// Token: 0x04000719 RID: 1817
		private readonly EffectPass {15489};

		// Token: 0x0400071A RID: 1818
		private readonly EffectPass {15490};

		// Token: 0x0400071B RID: 1819
		private readonly EffectPass {15491};

		// Token: 0x0400071C RID: 1820
		private readonly EffectPass {15492};

		// Token: 0x0400071D RID: 1821
		private readonly EffectPass {15493};

		// Token: 0x0400071E RID: 1822
		private readonly EffectPass {15494};

		// Token: 0x0400071F RID: 1823
		public readonly EffectPass RenderVolumtericParticlesHeatMap;

		// Token: 0x04000720 RID: 1824
		private readonly EffectPass {15495};

		// Token: 0x04000721 RID: 1825
		private readonly EffectPass {15496};

		// Token: 0x04000722 RID: 1826
		private readonly EffectPass {15497};

		// Token: 0x04000723 RID: 1827
		private EffectPass {15498};

		// Token: 0x04000724 RID: 1828
		private EffectPass {15499};

		// Token: 0x04000725 RID: 1829
		private EffectPass {15500};

		// Token: 0x04000726 RID: 1830
		private EffectPass {15501};

		// Token: 0x04000727 RID: 1831
		private EffectPass {15502};

		// Token: 0x04000728 RID: 1832
		private EffectPass {15503};

		// Token: 0x04000729 RID: 1833
		private EffectPass {15504};

		// Token: 0x0400072A RID: 1834
		public EffectParameter TXAAKernel;

		// Token: 0x0400072B RID: 1835
		public EffectParameter Texture;

		// Token: 0x0400072C RID: 1836
		public EffectParameter DepthBufferTexture;

		// Token: 0x0400072D RID: 1837
		public EffectParameter DepthBufferDataMask;

		// Token: 0x0400072E RID: 1838
		public EffectParameter CameraFarPlane;

		// Token: 0x0400072F RID: 1839
		public EffectParameter World;

		// Token: 0x04000730 RID: 1840
		public EffectParameter View;

		// Token: 0x04000731 RID: 1841
		public EffectParameter ViewProj;

		// Token: 0x04000732 RID: 1842
		public EffectParameter ColorBasic;

		// Token: 0x04000733 RID: 1843
		public EffectParameter CameraPosition;

		// Token: 0x04000734 RID: 1844
		public EffectParameter FogStart;

		// Token: 0x04000735 RID: 1845
		public EffectParameter FogEnd;

		// Token: 0x04000736 RID: 1846
		public EffectParameter CircleColor;

		// Token: 0x04000737 RID: 1847
		public EffectParameter CircleCenter;

		// Token: 0x04000738 RID: 1848
		public EffectParameter CircleRange;

		// Token: 0x04000739 RID: 1849
		public EffectParameter SectorAxis;

		// Token: 0x0400073A RID: 1850
		public EffectParameter SectorFarColor;

		// Token: 0x0400073B RID: 1851
		public EffectParameter SimulationTime;

		// Token: 0x0400073C RID: 1852
		public EffectParameter MainLightDirection;

		// Token: 0x0400073D RID: 1853
		public EffectParameter MainLightColor;

		// Token: 0x0400073E RID: 1854
		public EffectParameter VPShadow;

		// Token: 0x0400073F RID: 1855
		public EffectParameter wawePosition;

		// Token: 0x04000740 RID: 1856
		public EffectParameter height;

		// Token: 0x04000741 RID: 1857
		public EffectParameter UVOffset;

		// Token: 0x04000742 RID: 1858
		public EffectParameter SightDownLineXyXy;

		// Token: 0x04000743 RID: 1859
		public EffectParameter SightUpLineXyXy;

		// Token: 0x04000744 RID: 1860
		public EffectParameter SightCannonLeftRightXyXy;

		// Token: 0x04000745 RID: 1861
		public EffectParameter SightCannonHasDraw;

		// Token: 0x04000746 RID: 1862
		public EffectParameter WorldMapFogParam;

		// Token: 0x04000747 RID: 1863
		public EffectParameter WorldMapFogParam2;

		// Token: 0x04000748 RID: 1864
		private static BillboardParent_VPC circleRender;

		// Token: 0x04000749 RID: 1865
		private static UserIndexedMesh grid5x5;

		// Token: 0x0400074A RID: 1866
		private static UserIndexedMesh grid10x10;

		// Token: 0x0400074B RID: 1867
		private static UserIndexedMesh grid20x20;

		// Token: 0x0400074C RID: 1868
		private static UserIndexedMesh grid40x40;

		// Token: 0x0400074D RID: 1869
		private Transform3D {15505} = new Transform3D();

		// Token: 0x0400074E RID: 1870
		public float fogNearDistance;

		// Token: 0x0400074F RID: 1871
		public float fogFarDistance;
	}
}
