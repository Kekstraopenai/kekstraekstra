using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Core;

namespace WorldOfSeaBattles.Interface.DebugPanel
{
	// Token: 0x0200059E RID: 1438
	public class DebugStatsForm : Form
	{
		// Token: 0x06002142 RID: 8514 RVA: 0x00129520 File Offset: 0x00127720
		public DebugStatsForm(Vector2 {26411}) : base(new Marker({26411}.X, {26411}.Y + 25f, ref DebugStatsForm.statsPanel), DebugStatsForm.statsPanel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.AnimatedFocus = false;
			this.{26439} = new Timer(500f);
			this.{26413} = new ValueCounter();
			this.{26414} = new ValueCounter();
			this.{26415} = new ValueCounter();
			this.{26416} = new ValueCounter();
			this.{26417} = new ValueCounter();
			this.{26418} = new ValueCounter();
			Label label = new Label(base.Pos.XY + new Vector2(5f, 2f), Fonts.Arial_10, Color.Aquamarine, "UWE::Update", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label2 = new Label(base.Pos.XY + new Vector2(5f, 15f), Fonts.Arial_8, Color.Wheat, "FPS:", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26419} = new Label(base.Pos.XY + new Vector2(115f, 15f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label3 = new Label(base.Pos.XY + new Vector2(5f, 28f), Fonts.Arial_8, Color.Wheat, "ELAPSED:", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26420} = new Label(base.Pos.XY + new Vector2(115f, 28f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label4 = new Label(base.Pos.XY + new Vector2(5f, 41f), Fonts.Arial_8, Color.Wheat, "METHOD-TIME:", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26421} = new Label(base.Pos.XY + new Vector2(115f, 41f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label5 = new Label(base.Pos.XY + new Vector2(5f, 51f), Fonts.Arial_10, Color.Aquamarine, "UWE::Render", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label6 = new Label(base.Pos.XY + new Vector2(5f, 64f), Fonts.Arial_8, Color.Wheat, "FPS:", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26422} = new Label(base.Pos.XY + new Vector2(115f, 64f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label7 = new Label(base.Pos.XY + new Vector2(5f, 77f), Fonts.Arial_8, Color.Wheat, "ELAPSED:", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26423} = new Label(base.Pos.XY + new Vector2(115f, 77f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label8 = new Label(base.Pos.XY + new Vector2(5f, 90f), Fonts.Arial_8, Color.Wheat, "METHOD-TIME:", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26424} = new Label(base.Pos.XY + new Vector2(115f, 90f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label9 = new Label(base.Pos.XY + new Vector2(5f, 100f), Fonts.Arial_10, Color.Aquamarine, "GPU", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Label label10 = new Label(base.Pos.XY + new Vector2(5f, 113f), Fonts.Arial_8, Color.Wheat, "METHOD-TIME:", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26425} = new Label(base.Pos.XY + new Vector2(115f, 113f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26426} = new Label(base.Pos.XY + new Vector2(5f, 126f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26427} = new Label(base.Pos.XY + new Vector2(5f, 139f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26428} = new Label(base.Pos.XY + new Vector2(5f, 152f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26429} = new Label(base.Pos.XY + new Vector2(5f, 165f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26430} = new Label(base.Pos.XY + new Vector2(5f, 178f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26431} = new Label(base.Pos.XY + new Vector2(5f, 191f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26432} = new Label(base.Pos.XY + new Vector2(5f, 204f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26433} = new Label(base.Pos.XY + new Vector2(5f, 217f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26434} = new Label(base.Pos.XY + new Vector2(5f, 230f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26435} = new Label(base.Pos.XY + new Vector2(5f, 243f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26436} = new Label(base.Pos.XY + new Vector2(5f, 256f), Fonts.Arial_8, Color.White, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26437} = new Label(base.Pos.XY + new Vector2(5f, 269f), Fonts.Arial_8, Color.Yellow, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{26438} = new Label(base.Pos.XY + new Vector2(5f, 282f), Fonts.Arial_8, Color.Yellow, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(new UiControl[]
			{
				label,
				label2,
				label3,
				label4,
				this.{26419},
				this.{26420},
				this.{26421},
				label5,
				label6,
				label7,
				label8,
				this.{26422},
				this.{26423},
				this.{26424},
				label9,
				label10,
				this.{26425},
				this.{26426},
				this.{26427},
				this.{26428},
				this.{26429},
				this.{26430},
				this.{26431},
				this.{26432},
				this.{26433},
				this.{26434},
				this.{26435},
				this.{26436},
				this.{26437},
				this.{26438}
			});
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00129D74 File Offset: 0x00127F74
		public void UserUpdate(ref FrameTime {26412})
		{
			this.{26419}.Text = Math.Round((double)((float)(1000.0 / (Global.Game.GameTotalTimeMs - this.{26440}))), 3).ToString();
			this.{26440} = Global.Game.GameTotalTimeMs;
			this.{26413}.Push(Global.Game.GameTime.TimeUpdate);
			this.{26414}.Push(Global.Game.GameTime.ElapsedUpdate);
			this.{26415}.Push((float)((int)(1000f / Global.Game.GameTime.ElapsedUpdate)));
			this.{26416}.Push(Global.Game.GameTime.ElapsedDrawReal);
			this.{26417}.Push(Global.Game.GameTime.TimeDraw);
			this.{26418}.Push(Global.Game.GameTime.TimeDraw);
			this.{26420}.Text = Math.Round((double)this.{26414}.Avg, 3).ToString();
			this.{26421}.Text = Math.Round((double)this.{26413}.Avg, 3).ToString();
			this.{26422}.Text = Math.Round((double)this.{26415}.Avg, 3).ToString();
			this.{26423}.Text = Math.Round((double)this.{26417}.Avg, 3).ToString();
			this.{26424}.Text = Math.Round((double)this.{26416}.Avg, 3).ToString();
			this.{26425}.Text = Math.Round((double)this.{26418}.Avg, 3).ToString();
			if (this.{26439}.Sample(ref {26412}))
			{
				this.{26414}.AvgAndClean();
				this.{26413}.AvgAndClean();
				this.{26415}.AvgAndClean();
				this.{26417}.AvgAndClean();
				this.{26416}.AvgAndClean();
				this.{26418}.AvgAndClean();
			}
			Label label = this.{26426};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(19, 2);
			defaultInterpolatedStringHandler.AppendLiteral("TimeDiff: ");
			defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Round(Global.Game.GameTime.LastTimeDiff, 1));
			defaultInterpolatedStringHandler.AppendLiteral(", inter: ");
			defaultInterpolatedStringHandler.AppendFormatted<int>((int)Global.Game.GameTime.LastTicksInternalDiff);
			label.Text = defaultInterpolatedStringHandler.ToStringAndClear();
			this.{26427}.Text = "Camera position: " + Global.Camera.Position.ToStringRound(2);
			Label label2 = this.{26428};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(22, 1);
			defaultInterpolatedStringHandler2.AppendLiteral("All 3D particles now: ");
			defaultInterpolatedStringHandler2.AppendFormatted<int>(Global.Render.ParticleManager3D.Count);
			label2.Text = defaultInterpolatedStringHandler2.ToStringAndClear();
			this.{26428}.BasicColor = ((Global.Render.ParticleManager3D.Count > 600) ? Color.Yellow : Color.White);
			Label label3 = this.{26429};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler3 = new DefaultInterpolatedStringHandler(20, 1);
			defaultInterpolatedStringHandler3.AppendLiteral("Rendered ships now: ");
			defaultInterpolatedStringHandler3.AppendFormatted<int>(Global.RenderStats.ShipRenderCount);
			label3.Text = defaultInterpolatedStringHandler3.ToStringAndClear();
			this.{26429}.BasicColor = ((Global.RenderStats.ShipRenderCount > 12) ? Color.Yellow : Color.White);
			Label label4 = this.{26430};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler4 = new DefaultInterpolatedStringHandler(19, 1);
			defaultInterpolatedStringHandler4.AppendLiteral("Updated ships now: ");
			defaultInterpolatedStringHandler4.AppendFormatted<int>(Global.RenderStats.ShipAllCount);
			label4.Text = defaultInterpolatedStringHandler4.ToStringAndClear();
			this.{26430}.BasicColor = ((Global.RenderStats.ShipAllCount > 12) ? Color.Yellow : Color.White);
			Label label5 = this.{26431};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler5 = new DefaultInterpolatedStringHandler(32, 1);
			defaultInterpolatedStringHandler5.AppendLiteral("Particle 3D optimization level: ");
			defaultInterpolatedStringHandler5.AppendFormatted<float>(Global.Render.ParticleManager3D.CurrentPerformanceFactor);
			label5.Text = defaultInterpolatedStringHandler5.ToStringAndClear();
			Label label6 = this.{26432};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler6 = new DefaultInterpolatedStringHandler(32, 1);
			defaultInterpolatedStringHandler6.AppendLiteral("Ocean particles rendered count: ");
			defaultInterpolatedStringHandler6.AppendFormatted<int>(Global.RenderStats.OceanParticleRenderCount);
			label6.Text = defaultInterpolatedStringHandler6.ToStringAndClear();
			this.{26432}.BasicColor = ((Global.RenderStats.OceanParticleRenderCount > 500) ? Color.Yellow : Color.White);
			DeviceStatistics statistics = Engine.GS.Statistics;
			Label label7 = this.{26433};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler7 = new DefaultInterpolatedStringHandler(26, 1);
			defaultInterpolatedStringHandler7.AppendLiteral("dxrender.BeginRender2D(): ");
			defaultInterpolatedStringHandler7.AppendFormatted<int>(statistics.CountRender2DCycle);
			label7.Text = defaultInterpolatedStringHandler7.ToStringAndClear();
			Label label8 = this.{26434};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler8 = new DefaultInterpolatedStringHandler(21, 1);
			defaultInterpolatedStringHandler8.AppendLiteral("dxrender.Render2D(): ");
			defaultInterpolatedStringHandler8.AppendFormatted<int>(statistics.CountRender2DMethod);
			label8.Text = defaultInterpolatedStringHandler8.ToStringAndClear();
			Label label9 = this.{26435};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler9 = new DefaultInterpolatedStringHandler(28, 1);
			defaultInterpolatedStringHandler9.AppendLiteral("dxrender.SetRenderTarget(): ");
			defaultInterpolatedStringHandler9.AppendFormatted<int>(statistics.CountSetRenderTargetMethod);
			label9.Text = defaultInterpolatedStringHandler9.ToStringAndClear();
			Label label10 = this.{26436};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler10 = new DefaultInterpolatedStringHandler(15, 1);
			defaultInterpolatedStringHandler10.AppendLiteral("GPU Drawcalls: ");
			defaultInterpolatedStringHandler10.AppendFormatted<int>(statistics.Drawcalls);
			label10.Text = defaultInterpolatedStringHandler10.ToStringAndClear();
			Label label11 = this.{26437};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler11 = new DefaultInterpolatedStringHandler(22, 1);
			defaultInterpolatedStringHandler11.AppendLiteral("Send vertices to GPU: ");
			defaultInterpolatedStringHandler11.AppendFormatted<int>(statistics.VerticesRenderedCount);
			label11.Text = defaultInterpolatedStringHandler11.ToStringAndClear();
			Label label12 = this.{26438};
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler12 = new DefaultInterpolatedStringHandler(60, 3);
			defaultInterpolatedStringHandler12.AppendLiteral("GPU Memory RenderTarget: ");
			defaultInterpolatedStringHandler12.AppendFormatted<int>((int)((double)RenderTarget.SummarySizeInBytes / 1048576.0));
			defaultInterpolatedStringHandler12.AppendLiteral(" mb ");
			defaultInterpolatedStringHandler12.AppendLiteral("Textures: ");
			defaultInterpolatedStringHandler12.AppendFormatted<int>((int)((double)Texture2D.SummarySizeInBytes / 1048576.0));
			defaultInterpolatedStringHandler12.AppendLiteral(" mb");
			defaultInterpolatedStringHandler12.AppendLiteral("VertexBuffers: ");
			defaultInterpolatedStringHandler12.AppendFormatted<long>(VertexBuffer.SummarySizeInBytes / 1048576L);
			defaultInterpolatedStringHandler12.AppendLiteral(" mb");
			label12.Text = defaultInterpolatedStringHandler12.ToStringAndClear();
		}

		// Token: 0x0400203F RID: 8255
		private static readonly Rectangle statsPanel = new Rectangle(1214, 254, 270, 300);

		// Token: 0x04002040 RID: 8256
		private ValueCounter {26413};

		// Token: 0x04002041 RID: 8257
		private ValueCounter {26414};

		// Token: 0x04002042 RID: 8258
		private ValueCounter {26415};

		// Token: 0x04002043 RID: 8259
		private ValueCounter {26416};

		// Token: 0x04002044 RID: 8260
		private ValueCounter {26417};

		// Token: 0x04002045 RID: 8261
		private ValueCounter {26418};

		// Token: 0x04002046 RID: 8262
		private Label {26419};

		// Token: 0x04002047 RID: 8263
		private Label {26420};

		// Token: 0x04002048 RID: 8264
		private Label {26421};

		// Token: 0x04002049 RID: 8265
		private Label {26422};

		// Token: 0x0400204A RID: 8266
		private Label {26423};

		// Token: 0x0400204B RID: 8267
		private Label {26424};

		// Token: 0x0400204C RID: 8268
		private Label {26425};

		// Token: 0x0400204D RID: 8269
		private Label {26426};

		// Token: 0x0400204E RID: 8270
		private Label {26427};

		// Token: 0x0400204F RID: 8271
		private Label {26428};

		// Token: 0x04002050 RID: 8272
		private Label {26429};

		// Token: 0x04002051 RID: 8273
		private Label {26430};

		// Token: 0x04002052 RID: 8274
		private Label {26431};

		// Token: 0x04002053 RID: 8275
		private Label {26432};

		// Token: 0x04002054 RID: 8276
		private Label {26433};

		// Token: 0x04002055 RID: 8277
		private Label {26434};

		// Token: 0x04002056 RID: 8278
		private Label {26435};

		// Token: 0x04002057 RID: 8279
		private Label {26436};

		// Token: 0x04002058 RID: 8280
		private Label {26437};

		// Token: 0x04002059 RID: 8281
		private Label {26438};

		// Token: 0x0400205A RID: 8282
		private Timer {26439};

		// Token: 0x0400205B RID: 8283
		private double {26440};
	}
}
