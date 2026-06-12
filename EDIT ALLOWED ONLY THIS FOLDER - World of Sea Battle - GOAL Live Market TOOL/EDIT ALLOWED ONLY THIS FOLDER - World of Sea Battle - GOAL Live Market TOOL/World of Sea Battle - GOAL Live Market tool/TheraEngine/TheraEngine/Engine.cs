using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using SDL2;
using TheraEngine.Assets.Shaders.InternalShaders;
using TheraEngine.Components;
using TheraEngine.Core;
using TheraEngine.Grphics.Device;
using TheraEngine.PlatformTools;
using UWEngine.Core;

namespace TheraEngine
{
	// Token: 0x0200001D RID: 29
	public class Engine
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003B70 File Offset: 0x00001D70
		// (set) Token: 0x06000093 RID: 147 RVA: 0x00003B77 File Offset: 0x00001D77
		public static Texture2D RandomNormals { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003B7F File Offset: 0x00001D7F
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003B86 File Offset: 0x00001D86
		public static Texture2D SimpleNoise { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003B8E File Offset: 0x00001D8E
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003B95 File Offset: 0x00001D95
		public static Texture2D FillerTexture { get; private set; }

		// Token: 0x06000098 RID: 152 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public static void LoadContent(ContentManager {11406}, string {11407}, string {11408})
		{
			Engine.CheckPath(ref {11407});
			Engine.FillerTexture = {11406}.Load<Texture2D>("filler.fx");
			Engine.ParticlesAndStaticMeshEffect = {11406}.Load<Effect>({11407} + "MetarialPass\\ParticlesAndStaticMesh");
			Engine.OnePass4x4KernelEffect = {11406}.Load<Effect>({11407} + "Filters\\OnePass4x4Kernel");
			Engine.GaussianEffect = {11406}.Load<Effect>({11407} + "Filters\\GaussianBlur");
			Engine.PoissonEffect = {11406}.Load<Effect>({11407} + "Filters\\PossionBlur");
			Engine.ScreenPlaneShader = new InternalScreenPlaneShader({11406}, {11407} + "2D\\ScreenPlane");
			Engine.DownsampleAndBlurGBufferEffect = {11406}.Load<Effect>({11407} + "GBuffer\\DownsampleAndBlurGBuffer");
			Engine.GSSDO = {11406}.Load<Effect>({11407} + "GlobalIllumination\\GSSDO");
			Engine.postProcessPass2 = {11406}.Load<Effect>({11407} + "PostProcess\\PostProcessPass2");
			Engine.bloomMapGenerate = {11406}.Load<Effect>({11407} + "PostProcess\\Bloom");
			Engine.SsChromaticAberrationEffect = {11406}.Load<Effect>({11407} + "PostProcess\\SsChromaticAberration");
			Engine.SpyglassEffect = {11406}.Load<Effect>({11407} + "PostProcess\\Spyglass");
			Engine.HdrColorGradiens = {11406}.Load<Effect>({11407} + "PostProcess\\HdrColorGradiens");
			Engine.oceanFoam = {11406}.Load<Effect>({11407} + "Ocean\\Foam");
			Engine.trailShaderEffect = {11406}.Load<Effect>({11407} + "MetarialPass\\TrailShader");
			Engine.volumetricSightEffect = {11406}.Load<Effect>({11407} + "MetarialPass\\VolumetricSight");
			Engine.OceanNormalMapGeneratorEffect = {11406}.Load<Effect>({11407} + "Ocean\\OceanMap");
			Engine.RandomNormals = {11406}.Load<Texture2D>({11407} + "Textures\\RandomNormals");
			Engine.SimpleNoise = {11406}.Load<Texture2D>({11407} + "Textures\\SimpleNoise");
			Engine.GBuffer = {11406}.Load<Effect>({11407} + "GBuffer\\GBufferBuild");
			Engine.LoadFonts({11406}, {11408});
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00003D70 File Offset: 0x00001F70
		public static void LoadFonts(ContentManager {11409}, string {11410})
		{
			string {14786} = "\\Textures\\prerender\\fonts";
			Engine.CheckPath(ref {14786});
			Fonts.Load({11409}, {14786}, {11410});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003D92 File Offset: 0x00001F92
		private static void CheckPath(ref string {11411})
		{
			if (string.IsNullOrEmpty({11411}))
			{
				throw new ArgumentException();
			}
			if ({11411}[{11411}.Length - 1] != '\\')
			{
				{11411} += "\\";
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00003DC5 File Offset: 0x00001FC5
		public static void ShowMessageBox(string {11412}, string {11413} = "", SDL.SDL_MessageBoxFlags {11414} = SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION)
		{
			SDL.SDL_ShowSimpleMessageBox({11414}, {11413}, {11412}, IntPtr.Zero);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003DD8 File Offset: 0x00001FD8
		public static void ShowTextFile(string {11415})
		{
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				FileName = {11415},
				UseShellExecute = true
			};
			try
			{
				Process.Start(startInfo);
			}
			catch
			{
				Engine.ShowMessageBox("WIN32Error", "", SDL.SDL_MessageBoxFlags.SDL_MESSAGEBOX_INFORMATION);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003E28 File Offset: 0x00002028
		public static int MonitorWidth
		{
			get
			{
				return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00003E39 File Offset: 0x00002039
		public static int MonitorHeight
		{
			get
			{
				return GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003E4A File Offset: 0x0000204A
		public static bool CapsLockEnabled
		{
			get
			{
				return (SDL.SDL_GetModState() & SDL.SDL_Keymod.KMOD_CAPS) == SDL.SDL_Keymod.KMOD_CAPS;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00003E61 File Offset: 0x00002061
		public static bool ClipboardContainsText
		{
			get
			{
				return SDL.SDL_HasClipboardText() == SDL.SDL_bool.SDL_TRUE;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003E6B File Offset: 0x0000206B
		public static string ClipboardText
		{
			get
			{
				return SDL.SDL_GetClipboardText();
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00003E74 File Offset: 0x00002074
		public static void SetClipboardText(string {11416})
		{
			try
			{
				SDL.SDL_SetClipboardText({11416});
			}
			catch
			{
			}
		}

		// Token: 0x0400008C RID: 140
		internal static Effect ParticlesAndStaticMeshEffect;

		// Token: 0x0400008D RID: 141
		internal static Effect OnePass4x4KernelEffect;

		// Token: 0x0400008E RID: 142
		internal static Effect GaussianEffect;

		// Token: 0x0400008F RID: 143
		internal static Effect PoissonEffect;

		// Token: 0x04000090 RID: 144
		internal static Effect DownsampleAndBlurGBufferEffect;

		// Token: 0x04000091 RID: 145
		internal static Effect GSSDO;

		// Token: 0x04000092 RID: 146
		internal static Effect postProcessPass2;

		// Token: 0x04000093 RID: 147
		internal static Effect bloomMapGenerate;

		// Token: 0x04000094 RID: 148
		internal static Effect oceanFoam;

		// Token: 0x04000095 RID: 149
		internal static Effect trailShaderEffect;

		// Token: 0x04000096 RID: 150
		internal static Effect volumetricSightEffect;

		// Token: 0x04000097 RID: 151
		internal static Effect OceanNormalMapGeneratorEffect;

		// Token: 0x04000098 RID: 152
		internal static Effect SsChromaticAberrationEffect;

		// Token: 0x04000099 RID: 153
		internal static Effect SpyglassEffect;

		// Token: 0x0400009A RID: 154
		internal static Effect GBuffer;

		// Token: 0x0400009B RID: 155
		internal static Effect HdrColorGradiens;

		// Token: 0x0400009C RID: 156
		internal static InternalScreenPlaneShader ScreenPlaneShader;

		// Token: 0x0400009D RID: 157
		public static GameCore Game;

		// Token: 0x0400009E RID: 158
		public static Device GS;

		// Token: 0x040000A2 RID: 162
		public static IPlatformTools PlatformTools;
	}
}
