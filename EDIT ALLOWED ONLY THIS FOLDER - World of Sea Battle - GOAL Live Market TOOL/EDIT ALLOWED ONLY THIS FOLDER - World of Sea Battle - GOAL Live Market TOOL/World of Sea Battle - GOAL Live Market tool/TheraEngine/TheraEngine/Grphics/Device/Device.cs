using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Content;
using TheraEngine.Assets.Graphics;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Graphics.Models;
using TheraEngine.Helpers;
using TheraEngine.Input;

namespace TheraEngine.Grphics.Device
{
	// Token: 0x020000F6 RID: 246
	public class Device
	{
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x00022400 File Offset: 0x00020600
		public Rectangle UIArea
		{
			get
			{
				return Engine.Game.AdaptiveUiRect;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x0002240C File Offset: 0x0002060C
		public Vector2 MouseToUI
		{
			get
			{
				Vector2 result;
				result.X = (float)((int)(InputHelper.NowMouseState.Position.X * (float)this.UIArea.Width / Engine.Game.WindowSize.X));
				result.Y = (float)((int)(InputHelper.NowMouseState.Position.Y * (float)this.UIArea.Height / Engine.Game.WindowSize.Y));
				return result;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x00022484 File Offset: 0x00020684
		public Vector2 MouseToUIPrev
		{
			get
			{
				Vector2 result;
				result.X = (float)((int)(InputHelper.LastMouseState.Position.X * (float)this.UIArea.Width / Engine.Game.WindowSize.X));
				result.Y = (float)((int)(InputHelper.LastMouseState.Position.Y * (float)this.UIArea.Height / Engine.Game.WindowSize.Y));
				return result;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x000224FC File Offset: 0x000206FC
		public bool Is2DBatchActive
		{
			get
			{
				return this.{14711};
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x00022504 File Offset: 0x00020704
		public Texture2D CurrentTexture
		{
			get
			{
				return this.{14712}.Get(0);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x00022512 File Offset: 0x00020712
		public IRenderTarget CurrentOutput
		{
			get
			{
				return this.{14713}.Get(0);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x00022520 File Offset: 0x00020720
		public Vector2 CurrentOutputSize
		{
			get
			{
				IRenderTarget renderTarget = this.{14713}.Get(0);
				if (renderTarget == null)
				{
					return Engine.Game.WindowSize;
				}
				return ((RenderTarget)renderTarget.Targets[0].RenderTarget.Tag).Size;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x00022568 File Offset: 0x00020768
		public Matrix UIMatrix
		{
			get
			{
				return Matrix.CreateScale(this.CurrentOutputSize.X / (float)this.UIArea.Width, this.CurrentOutputSize.Y / (float)this.UIArea.Height, 1f);
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x000225A4 File Offset: 0x000207A4
		public Device(GraphicsDevice {14503})
		{
			this.graphicsDevice = {14503};
			this.{14709} = new SpriteBatch({14503});
			this.{14711} = false;
			this.{14710} = 0;
			this.Render2DProperties = new Device.ManualRender2DProperties();
			this.{14712} = new Device.TextureQueue();
			this.{14713} = new Device.RenderTargetQueue();
			this.Statistics = new DeviceStatistics();
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000226EC File Offset: 0x000208EC
		public void SetFont(CustomSpriteFont {14504})
		{
			if (this.Font != {14504})
			{
				this.Font = {14504};
				DeviceStatistics statistics = this.Statistics;
				int countUnselectedTexture = statistics.CountUnselectedTexture;
				statistics.CountUnselectedTexture = countUnselectedTexture + 1;
			}
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00022720 File Offset: 0x00020920
		public void SetTexture(Texture2D {14505})
		{
			if (this.{14712}.first != {14505})
			{
				DeviceStatistics statistics = this.Statistics;
				int countUnselectedTexture = statistics.CountUnselectedTexture;
				statistics.CountUnselectedTexture = countUnselectedTexture + 1;
				this.{14712}.Add({14505});
			}
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0002275C File Offset: 0x0002095C
		public void SetTexture(Texture2DAtlas {14506})
		{
			this.SetTexture({14506}.texture);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x0002276A File Offset: 0x0002096A
		public void ReturnBackTexture()
		{
			this.SetTexture(this.{14712}.Get(1));
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00022780 File Offset: 0x00020980
		public void Begin2D(bool {14507})
		{
			DeviceStatistics statistics = this.Statistics;
			int countRender2DCycle = statistics.CountRender2DCycle;
			statistics.CountRender2DCycle = countRender2DCycle + 1;
			this.{14709}.Begin(SpriteSortMode.Immediate, this.Render2DProperties.DefaultBlendState, this.Render2DProperties.DefaultSamplerState, DepthStencilState.None, this.Render2DProperties.DefaultRasterizerState, null, {14507} ? this.UIMatrix : Matrix.Identity);
			this.{14711} = true;
			if (this.{14714} != null)
			{
				this.SetScissor(this.{14714}.Value, false);
			}
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x0002280C File Offset: 0x00020A0C
		public void Begin2D(bool {14508}, Effect {14509}, DepthStencilState {14510} = null)
		{
			DeviceStatistics statistics = this.Statistics;
			int countRender2DCycle = statistics.CountRender2DCycle;
			statistics.CountRender2DCycle = countRender2DCycle + 1;
			this.{14709}.Begin(SpriteSortMode.Immediate, this.Render2DProperties.DefaultBlendState, this.Render2DProperties.DefaultSamplerState, ({14510} == null) ? DepthStencilState.None : {14510}, this.Render2DProperties.DefaultRasterizerState, {14509}, {14508} ? this.UIMatrix : Matrix.Identity);
			this.{14711} = true;
			if (this.{14714} != null)
			{
				this.SetScissor(this.{14714}.Value, false);
			}
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x000228A0 File Offset: 0x00020AA0
		public void End2D()
		{
			if (this.graphicsDevice.ScissorRectangle.WidthHeight() != Engine.Game.WindowSize)
			{
				this.{14714} = new Rectangle?(this.graphicsDevice.ScissorRectangle);
			}
			this.{14709}.End();
			this.{14711} = false;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x000228F8 File Offset: 0x00020AF8
		public void DrawProgressbar(in Rectangle {14511}, in Vector2 {14512}, float {14513})
		{
			this.{14589}();
			float width = {14513} * (float){14511}.Width;
			this.{14709}.Draw(this.{14712}.first, new Marker(ref {14512}, ref {14511}).SetWidth(width).ToRect(), new Rectangle?({14511}.SetWidth({14513} * (float){14511}.Width)), this.{14705});
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00022964 File Offset: 0x00020B64
		public void DrawProgressbar(in Rectangle {14514}, in Vector2 {14515}, float {14516}, Color {14517})
		{
			this.{14589}();
			float width = {14516} * (float){14514}.Width;
			this.{14709}.Draw(this.{14712}.first, new Marker(ref {14515}, ref {14514}).SetWidth(width).ToRect(), new Rectangle?({14514}.SetWidth({14516} * (float){14514}.Width)), {14517});
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x000229CC File Offset: 0x00020BCC
		public void DrawProgressbar(in Rectangle {14518}, in Rectangle {14519}, in Vector2 {14520}, float {14521}, float {14522} = 1f, Color? {14523} = null)
		{
			this.{14589}();
			float num = {14521} * (float){14518}.Width;
			float num2 = (1f - {14521}) * (float){14519}.Width;
			Rectangle rectangle = new Marker(ref {14520}, num, (float){14518}.Height).ScaleSize({14522}).ToRect();
			Rectangle destinationRectangle = new Marker({14520}.X + (float)rectangle.Width, {14520}.Y, num2, (float){14519}.Height).ScaleSize({14522}).ToRect();
			this.{14709}.Draw(this.{14712}.first, rectangle, new Rectangle?({14518}.SetWidth(num)), {14523} ?? this.{14705});
			this.{14709}.Draw(this.{14712}.first, destinationRectangle, new Rectangle?({14519}.SetWidth(num2).AddX(num)), {14523} ?? this.{14705});
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00022AE4 File Offset: 0x00020CE4
		public void DrawProgressbarVertical(in Rectangle {14524}, in Rectangle {14525}, in Vector2 {14526}, float {14527}, float {14528} = 1f, Color? {14529} = null)
		{
			this.{14589}();
			float num = (1f - {14527}) * (float){14525}.Height;
			float num2 = {14527} * (float){14524}.Height;
			Rectangle rectangle = new Marker(ref {14526}, (float){14525}.Width, num).ScaleSize({14528}).ToRect();
			Rectangle destinationRectangle = new Marker({14526}.X, {14526}.Y + (float)rectangle.Height, (float){14524}.Width, num2).ScaleSize({14528}).ToRect();
			this.{14709}.Draw(this.{14712}.first, rectangle, new Rectangle?({14525}.SetHeight(num)), {14529} ?? this.{14705});
			this.{14709}.Draw(this.{14712}.first, destinationRectangle, new Rectangle?({14524}.SetHeight(num2).AddY(num)), {14529} ?? this.{14705});
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x00022BFC File Offset: 0x00020DFC
		public void Draw(in Rectangle {14530}, in Vector2 {14531})
		{
			this.{14589}();
			this.{14709}.Draw(this.{14712}.first, {14531}, new Rectangle?({14530}), this.{14705}, 0f, this.{14707}, 1f, SpriteEffects.None, 1f);
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00022C54 File Offset: 0x00020E54
		public void Draw(in Rectangle {14532}, in Vector2 {14533}, in Vector2 {14534}, float {14535})
		{
			this.{14589}();
			this.{14709}.Draw(this.{14712}.first, {14533}, new Rectangle?({14532}), this.{14705}, {14535}, {14534}, 1f, SpriteEffects.None, 1f);
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00022CA8 File Offset: 0x00020EA8
		public void Draw(in Rectangle {14536}, in Vector2 {14537}, in Vector2 {14538}, float {14539}, float {14540})
		{
			this.{14589}();
			this.{14709}.Draw(this.{14712}.first, {14537}, new Rectangle?({14536}), this.{14705}, {14539}, {14538}, {14540}, SpriteEffects.None, 1f);
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00022CF8 File Offset: 0x00020EF8
		public void Draw(in Rectangle {14541}, in Rectangle {14542})
		{
			this.{14589}();
			this.{14709}.Draw(this.{14712}.first, {14542}, new Rectangle?({14541}), this.{14705}, 0f, this.{14707}, SpriteEffects.None, 1f);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00022D4C File Offset: 0x00020F4C
		public void Draw(in Rectangle {14543}, in Rectangle {14544}, in Vector2 {14545}, float {14546})
		{
			this.{14589}();
			this.{14709}.Draw(this.{14712}.first, {14544}, new Rectangle?({14543}), this.{14705}, {14546}, {14545}, SpriteEffects.None, 1f);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00022D9C File Offset: 0x00020F9C
		public void Draw(in Rectangle {14547}, in Vector2 {14548}, in Color {14549})
		{
			this.{14589}();
			if (this.{14712}.first != null)
			{
				this.{14709}.Draw(this.{14712}.first, {14548}, new Rectangle?({14547}), {14549}, 0f, this.{14707}, 1f, SpriteEffects.None, 1f);
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00022E00 File Offset: 0x00021000
		public void Draw(in Rectangle {14550}, in Vector2 {14551}, Vector2 {14552}, float {14553}, in Color {14554})
		{
			this.{14589}();
			this.{14709}.Draw(this.{14712}.first, {14551}, new Rectangle?({14550}), {14554}, {14553}, {14552}, 1f, SpriteEffects.None, 1f);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x00022E50 File Offset: 0x00021050
		public void Draw(in Rectangle {14555}, in Vector2 {14556}, in Vector2 {14557}, float {14558}, float {14559}, in Color {14560})
		{
			this.{14589}();
			this.{14709}.Draw(this.{14712}.first, {14556}, new Rectangle?({14555}), {14560}, {14558}, {14557}, {14559}, SpriteEffects.None, 1f);
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00022EA4 File Offset: 0x000210A4
		public void Draw(in Rectangle {14561}, in Vector2 {14562}, in Vector2 {14563}, float {14564}, in Vector2 {14565}, in Color {14566})
		{
			this.{14589}();
			this.{14709}.Draw(this.{14712}.first, {14562}, new Rectangle?({14561}), {14566}, {14564}, {14563}, {14565}, SpriteEffects.None, 1f);
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00022EFC File Offset: 0x000210FC
		public void Draw(in Rectangle {14567}, in Rectangle {14568}, in Color {14569})
		{
			this.{14589}();
			if (this.{14712}.first != null)
			{
				this.{14709}.Draw(this.{14712}.first, {14568}, new Rectangle?({14567}), {14569}, 0f, this.{14707}, SpriteEffects.None, 1f);
			}
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x00022F5C File Offset: 0x0002115C
		public void DrawCustomTexture(Texture2D {14570}, in Rectangle {14571}, in Rectangle {14572}, in Color {14573})
		{
			DeviceStatistics statistics = this.Statistics;
			int countUnselectedTexture = statistics.CountUnselectedTexture;
			statistics.CountUnselectedTexture = countUnselectedTexture + 1;
			this.{14589}();
			this.{14709}.Draw({14570}, {14572}, new Rectangle?({14571}), {14573}, 0f, this.{14707}, SpriteEffects.None, 1f);
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x00022FBC File Offset: 0x000211BC
		public void DrawCustomTexture(Texture2D {14574}, in Rectangle {14575}, in Vector2 {14576}, in Color {14577})
		{
			DeviceStatistics statistics = this.Statistics;
			int countUnselectedTexture = statistics.CountUnselectedTexture;
			statistics.CountUnselectedTexture = countUnselectedTexture + 1;
			this.{14589}();
			this.{14709}.Draw({14574}, new Rectangle((int){14576}.X, (int){14576}.Y, {14575}.Width, {14575}.Height), new Rectangle?({14575}), {14577}, 0f, this.{14707}, SpriteEffects.None, 1f);
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00023034 File Offset: 0x00021234
		public void DrawCustomTexture(Texture2D {14578}, in Rectangle {14579}, in Rectangle {14580}, in Vector2 {14581}, float {14582}, in Color {14583})
		{
			DeviceStatistics statistics = this.Statistics;
			int countUnselectedTexture = statistics.CountUnselectedTexture;
			statistics.CountUnselectedTexture = countUnselectedTexture + 1;
			this.{14589}();
			this.{14709}.Draw({14578}, {14580}, new Rectangle?({14579}), {14583}, {14582}, {14581}, SpriteEffects.None, 1f);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x00023090 File Offset: 0x00021290
		public void Draw(in Rectangle {14584}, in Rectangle {14585}, in Vector2 {14586}, float {14587}, in Color {14588})
		{
			this.{14589}();
			if (this.{14712}.first != null)
			{
				this.{14709}.Draw(this.{14712}.first, {14585}, new Rectangle?({14584}), {14588}, {14587}, {14586}, SpriteEffects.None, 1f);
			}
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x000230EC File Offset: 0x000212EC
		private void {14589}()
		{
			int num;
			if (this.{14710} == 1)
			{
				DeviceStatistics statistics = this.Statistics;
				num = statistics.CountUnselectedTexture;
				statistics.CountUnselectedTexture = num + 1;
				this.{14710} = 0;
			}
			DeviceStatistics statistics2 = this.Statistics;
			num = statistics2.CountRender2DMethod;
			statistics2.CountRender2DMethod = num + 1;
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x00023133 File Offset: 0x00021333
		public void DrawStringCenteredShadow(string {14590}, in Vector2 {14591}, in Color {14592}, float {14593} = 0.8f)
		{
			this.DrawStringCenteredShadow({14590}, {14591}, {14592}, 1f, {14593});
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x00023148 File Offset: 0x00021348
		public void DrawStringCenteredShadow(string {14594}, in Vector2 {14595}, in Color {14596}, float {14597}, float {14598} = 1f)
		{
			Vector2 vector = {14595} - this.Font.MeasureWithoutDownscale({14594}) * 0.5f * {14597} / this.Font.Downscale;
			Vector2 {14627} = vector + new Vector2(-1f, -1f);
			Color black = Color.Black;
			Color color = {14596};
			color = black * ((float)color.A / 255f * {14598});
			this.DrawString({14594}, {14627}, color, 0f, this.{14707}, {14597});
			Vector2 {14627}2 = vector + new Vector2(1f, 1f);
			Color black2 = Color.Black;
			color = {14596};
			color = black2 * ((float)color.A / 255f * {14598});
			this.DrawString({14594}, {14627}2, color, 0f, this.{14707}, {14597});
			this.DrawString({14594}, vector, {14596}, 0f, this.{14707}, {14597});
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x00023240 File Offset: 0x00021440
		public void DrawString(string {14599}, in Vector2 {14600}, in Color {14601})
		{
			this.DrawString({14599}, {14600}, {14601}, 0f, this.{14707}, 1f);
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x00023260 File Offset: 0x00021460
		public void DrawStringShadowed(string {14602}, in Vector2 {14603}, in Color {14604})
		{
			Color black = Color.Black;
			Color color = {14604};
			Color color2 = black * ((float)color.A / 255f);
			this.DrawString({14602}, {14603} + new Vector2(-1f, 1f), color2, 0f, this.{14707}, 1f);
			this.DrawString({14602}, {14603} + new Vector2(1f, 1f), color2, 0f, this.{14707}, 1f);
			this.DrawString({14602}, {14603} + new Vector2(1f, -1f), color2, 0f, this.{14707}, 1f);
			this.DrawString({14602}, {14603} + new Vector2(-1f, -1f), color2, 0f, this.{14707}, 1f);
			this.DrawString({14602}, {14603}, {14604}, 0f, this.{14707}, 1f);
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x00023378 File Offset: 0x00021578
		public void DrawStringShadowed(string {14605}, in Vector2 {14606}, in Color {14607}, float {14608}, float {14609} = 1f)
		{
			Color black = Color.Black;
			Color color = {14607};
			Color color2 = black * ((float)color.A / 255f) * {14609};
			this.DrawString({14605}, {14606} + new Vector2(-1f, 1f), color2, 0f, this.{14707}, {14608});
			this.DrawString({14605}, {14606} + new Vector2(1f, 1f), color2, 0f, this.{14707}, {14608});
			this.DrawString({14605}, {14606} + new Vector2(1f, -1f), color2, 0f, this.{14707}, {14608});
			this.DrawString({14605}, {14606} + new Vector2(-1f, -1f), color2, 0f, this.{14707}, {14608});
			this.DrawString({14605}, {14606}, {14607}, 0f, this.{14707}, {14608});
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00023488 File Offset: 0x00021688
		public void DrawStringCentered(string {14610}, in Vector2 {14611}, in Color {14612})
		{
			Vector2 {14627} = {14611} - this.Font.MeasureWithoutDownscale({14610}) * 0.5f / this.Font.Downscale;
			this.DrawString({14610}, {14627}, {14612}, 0f, this.{14707}, 1f);
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x000234E0 File Offset: 0x000216E0
		public void DrawStringCentered(string {14613}, in Vector2 {14614}, in Color {14615}, float {14616})
		{
			Vector2 {14627} = {14614} - this.Font.MeasureWithoutDownscale({14613}) * 0.5f * {14616} / this.Font.Downscale;
			this.DrawString({14613}, {14627}, {14615}, 0f, this.{14707}, {14616});
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0002353C File Offset: 0x0002173C
		public void DrawStringXCentered(string {14617}, in Vector2 {14618}, in Color {14619}, float {14620} = 1f)
		{
			Vector2 {14627} = {14618} - new Vector2(this.Font.MeasureWithoutDownscale({14617}).X * 0.5f * {14620} / this.Font.Downscale, 0f);
			this.DrawString({14617}, {14627}, {14619}, 0f, this.{14707}, {14620});
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x0002359C File Offset: 0x0002179C
		public Vector2 DrawStringClipped(string {14621}, Vector2 {14622}, in Color {14623}, float {14624}, float {14625} = 1f)
		{
			Geometry.IntVector(ref {14622});
			this.{14710} = 1;
			DeviceStatistics statistics = this.Statistics;
			int i = statistics.CountRender2DMethod;
			statistics.CountRender2DMethod = i + 1;
			Vector2 zero = Vector2.Zero;
			string[] array = {14621}.SplitSmart(new char[]
			{
				' '
			});
			StringBuilder stringBuilder = new StringBuilder();
			float x = this.Font.MeasureWithoutDownscale(" ").X;
			foreach (string text in array)
			{
				Vector2 vector = this.Font.MeasureWithoutDownscale(text);
				if (stringBuilder.Length > 0 && this.Font.MeasureWithoutDownscale(stringBuilder.ToString()).X + vector.X + x > {14624} * this.Font.Downscale)
				{
					this.{14709}.DrawString(this.Font.baseFont, this.Font.AlternativeFont, stringBuilder.ToString(), {14622}, {14623}, 0f, this.{14707}, 1f / this.Font.Downscale, SpriteEffects.None, 0f);
					{14622}.Y += vector.Y / this.Font.Downscale * {14625};
					zero.Y += vector.Y / this.Font.Downscale * {14625};
					stringBuilder.Clear();
				}
				if (!stringBuilder.ToString().EndsWithCjk())
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append(text);
			}
			if (stringBuilder.Length > 0)
			{
				this.{14709}.DrawString(this.Font.baseFont, this.Font.AlternativeFont, stringBuilder.ToString(), {14622}, {14623}, 0f, this.{14707}, 1f / this.Font.Downscale * {14625}, SpriteEffects.None, 0f);
				zero.Y += this.Font.MeasureWithoutDownscale(stringBuilder.ToString()).Y / this.Font.Downscale * {14625};
			}
			zero.X = {14624};
			return zero;
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x000237BC File Offset: 0x000219BC
		public void DrawString(string {14626}, Vector2 {14627}, in Color {14628}, float {14629}, Vector2 {14630}, float {14631})
		{
			Geometry.IntVector(ref {14627});
			this.{14710} = 1;
			DeviceStatistics statistics = this.Statistics;
			int countRender2DMethod = statistics.CountRender2DMethod;
			statistics.CountRender2DMethod = countRender2DMethod + 1;
			if (this.Font == Fonts.Philosopher_12)
			{
				Color value = {14628};
				float num = 1f;
				float num2 = 2f;
				Color color = {14628};
				Color color2 = value * (num / (num2 - (float)color.A / 255f) * 0.9f);
				this.{14709}.DrawString(this.Font.baseFont, this.Font.AlternativeFont, {14626}, {14627}, color2, {14629}, {14630}, {14631} / this.Font.Downscale, SpriteEffects.None, 1f);
				this.{14709}.DrawString(this.Font.baseFont, this.Font.AlternativeFont, {14626}, {14627} + new Vector2(0.1f, 0.25f), color2, {14629}, {14630}, {14631} / this.Font.Downscale, SpriteEffects.None, 1f);
				return;
			}
			this.{14709}.DrawString(this.Font.baseFont, this.Font.AlternativeFont, {14626}, {14627}, {14628}, {14629}, {14630}, {14631} / this.Font.Downscale, SpriteEffects.None, 1f);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00023900 File Offset: 0x00021B00
		public void DrawStringFloat(string {14632}, in Vector2 {14633}, in Color {14634})
		{
			this.{14710} = 1;
			DeviceStatistics statistics = this.Statistics;
			int countRender2DMethod = statistics.CountRender2DMethod;
			statistics.CountRender2DMethod = countRender2DMethod + 1;
			if (this.Font == Fonts.Philosopher_12)
			{
				Color value = {14634};
				float num = 1f;
				float num2 = 2f;
				Color color = {14634};
				Color color2 = value * (num / (num2 - (float)color.A / 255f) * 0.9f);
				this.{14709}.DrawString(this.Font.baseFont, this.Font.AlternativeFont, {14632}, {14633}, color2, 0f, this.{14707}, 1f / this.Font.Downscale, SpriteEffects.None, 1f);
				this.{14709}.DrawString(this.Font.baseFont, this.Font.AlternativeFont, {14632}, {14633} + new Vector2(0.1f, 0.25f), color2, 0f, this.{14707}, 1f / this.Font.Downscale, SpriteEffects.None, 1f);
				return;
			}
			this.{14709}.DrawString(this.Font.baseFont, this.Font.AlternativeFont, {14632}, {14633}, {14634}, 0f, this.{14707}, 1f / this.Font.Downscale, SpriteEffects.None, 1f);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00023A68 File Offset: 0x00021C68
		public void Line2D(Rectangle {14635}, Vector2 {14636}, Vector2 {14637}, Color {14638}, int {14639})
		{
			Rectangle rectangle;
			float {14587};
			this.{14651}({14636}, {14637}, {14639}, out rectangle, out {14587});
			Vector2 zero = Vector2.Zero;
			this.Draw({14635}, rectangle, zero, {14587}, {14638});
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00023A98 File Offset: 0x00021C98
		public void Line2D(Texture2D {14640}, Rectangle {14641}, Vector2 {14642}, Vector2 {14643}, Color {14644}, int {14645})
		{
			Rectangle rectangle;
			float {14582};
			this.{14651}({14642}, {14643}, {14645}, out rectangle, out {14582});
			Vector2 zero = Vector2.Zero;
			this.DrawCustomTexture({14640}, {14641}, rectangle, zero, {14582}, {14644});
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00023ACC File Offset: 0x00021CCC
		public void DottedLine2D(Rectangle {14646}, Vector2 {14647}, Vector2 {14648}, Color {14649}, int {14650})
		{
			int width = {14646}.Width;
			int num = (int)(Vector2.Distance({14647}, {14648}) / (float)width);
			if (num == 0)
			{
				return;
			}
			if (num > Engine.GS.UIArea.Width * 5 / width)
			{
				return;
			}
			float num2 = 1f / (float)num;
			for (float num3 = 0f; num3 < 1f; num3 += num2 * 2f)
			{
				Rectangle rectangle;
				float {14587};
				this.{14651}(Vector2.Lerp({14647}, {14648}, num3), Vector2.Lerp({14647}, {14648}, num3 + num2), {14650}, out rectangle, out {14587});
				Vector2 zero = Vector2.Zero;
				this.Draw({14646}, rectangle, zero, {14587}, {14649});
			}
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00023B60 File Offset: 0x00021D60
		private void {14651}(Vector2 {14652}, Vector2 {14653}, int {14654}, out Rectangle {14655}, out float {14656})
		{
			Vector2 vector;
			Vector2.Subtract(ref {14653}, ref {14652}, out vector);
			{14656} = MathF.Atan2(vector.Y, vector.X);
			{14655} = new Rectangle
			{
				X = (int){14652}.X,
				Y = (int){14652}.Y,
				Width = (int)vector.Length(),
				Height = {14654}
			};
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00023BD0 File Offset: 0x00021DD0
		public void Render3DSquere<T>(T[] {14657}, VertexDeclaration {14658}) where T : struct, IVertexType
		{
			this.graphicsDevice.DrawUserPrimitives<T>(PrimitiveType.TriangleList, {14657}, 0, 2, {14658});
			this.Statistics.VerticesRenderedCount += 6;
			DeviceStatistics statistics = this.Statistics;
			int drawcalls = statistics.Drawcalls;
			statistics.Drawcalls = drawcalls + 1;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00023C18 File Offset: 0x00021E18
		public void Render3DBufferedPart(VertexBuffer {14659}, IndexBuffer {14660}, int {14661}, int {14662}, int {14663}, int {14664})
		{
			this.graphicsDevice.SetVertexBuffer({14659});
			this.graphicsDevice.Indices = {14660};
			this.graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, {14661}, 0, {14662}, {14663}, {14664});
			this.Statistics.VerticesRenderedCount += {14664} * 3;
			DeviceStatistics statistics = this.Statistics;
			int drawcalls = statistics.Drawcalls;
			statistics.Drawcalls = drawcalls + 1;
		}

		// Token: 0x060006E5 RID: 1765 RVA: 0x00023C7C File Offset: 0x00021E7C
		public void Render3DUserIndPrimitives<T>(PrimitiveType {14665}, T[] {14666}, short[] {14667}, int {14668}, int {14669}, VertexDeclaration {14670}) where T : struct, IVertexType
		{
			this.graphicsDevice.DrawUserIndexedPrimitives<T>({14665}, {14666}, 0, {14668}, {14667}, 0, {14669}, {14670});
			this.Statistics.VerticesRenderedCount += {14669} * 3;
			DeviceStatistics statistics = this.Statistics;
			int drawcalls = statistics.Drawcalls;
			statistics.Drawcalls = drawcalls + 1;
		}

		// Token: 0x060006E6 RID: 1766 RVA: 0x00023CCC File Offset: 0x00021ECC
		public void Render3DUserPrimitives<T>(PrimitiveType {14671}, T[] {14672}, int {14673}, VertexDeclaration {14674}) where T : struct, IVertexType
		{
			this.graphicsDevice.DrawUserPrimitives<T>({14671}, {14672}, 0, {14673}, {14674});
			this.Statistics.VerticesRenderedCount += {14673} * 3;
			DeviceStatistics statistics = this.Statistics;
			int drawcalls = statistics.Drawcalls;
			statistics.Drawcalls = drawcalls + 1;
		}

		// Token: 0x060006E7 RID: 1767 RVA: 0x00023D14 File Offset: 0x00021F14
		public void Render3DLine<T>(T {14675}, T {14676}) where T : struct, IVertexType
		{
			this.graphicsDevice.DrawUserIndexedPrimitives<T>(PrimitiveType.LineList, new T[]
			{
				{14675},
				{14676}
			}, 0, 2, this.{14708}, 0, 1);
			this.Statistics.VerticesRenderedCount += 2;
		}

		// Token: 0x060006E8 RID: 1768 RVA: 0x00023D60 File Offset: 0x00021F60
		public void Render3DUserMesh(VertexBuffer {14677}, PrimitiveType {14678}, int {14679})
		{
			this.graphicsDevice.SetVertexBuffer({14677});
			this.graphicsDevice.DrawPrimitives({14678}, 0, {14679});
			this.Statistics.VerticesRenderedCount += {14679} * 3;
			DeviceStatistics statistics = this.Statistics;
			int drawcalls = statistics.Drawcalls;
			statistics.Drawcalls = drawcalls + 1;
		}

		// Token: 0x060006E9 RID: 1769 RVA: 0x00023DB4 File Offset: 0x00021FB4
		public void Render3DCompressedMesh(DeviceStreamContext[] {14680}, VertexBufferBinding[] {14681}, bool {14682})
		{
			int num = {14680}.Length;
			this.graphicsDevice.SetVertexBuffers({14681});
			for (int i = 0; i < num; i++)
			{
				DeviceStreamContext deviceStreamContext = {14680}[i];
				this.graphicsDevice.Indices = deviceStreamContext.IndexBuffer;
				for (int j = 0; j < deviceStreamContext.Sets.Size; j++)
				{
					MeshPartData meshPartData = deviceStreamContext.Sets.Array[j];
					this.graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, meshPartData.vertexOffset, 0, meshPartData.verticesCount, meshPartData.vertexStartIndex, meshPartData.PrimitiveCount);
					this.Statistics.VerticesRenderedCount += meshPartData.PrimitiveCount * 3;
					DeviceStatistics statistics = this.Statistics;
					int drawcalls = statistics.Drawcalls;
					statistics.Drawcalls = drawcalls + 1;
				}
			}
		}

		// Token: 0x060006EA RID: 1770 RVA: 0x00023E78 File Offset: 0x00022078
		public void RenderInstancing(int {14683}, int {14684}, DynamicVertexBuffer {14685}, VertexBuffer {14686}, IndexBuffer {14687}, int {14688}, int {14689}, int {14690}, int {14691} = 1)
		{
			this.{14715}[0] = new VertexBufferBinding({14686}, 0, 0);
			this.{14715}[1] = new VertexBufferBinding({14685}, 0, {14691});
			this.graphicsDevice.SetVertexBuffers(this.{14715});
			this.graphicsDevice.Indices = {14687};
			this.graphicsDevice.DrawInstancedPrimitives(PrimitiveType.TriangleList, {14684}, 0, {14688}, {14683}, {14689}, {14690});
			this.Statistics.VerticesRenderedCount += {14689} * 3 * {14690};
			DeviceStatistics statistics = this.Statistics;
			int drawcalls = statistics.Drawcalls;
			statistics.Drawcalls = drawcalls + 1;
		}

		// Token: 0x060006EB RID: 1771 RVA: 0x00023F10 File Offset: 0x00022110
		internal void FrameBegin()
		{
			this.Statistics.Reset();
			this.{14714} = null;
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x00023F29 File Offset: 0x00022129
		public void SetColorBlendState(BlendState {14692})
		{
			this.graphicsDevice.BlendState = {14692};
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x00023F37 File Offset: 0x00022137
		public void SetDepthBuffer(DepthBuffer {14693})
		{
			this.graphicsDevice.DepthStencilState = {14693}.DepthState;
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x00023F4C File Offset: 0x0002214C
		public void SetRasterizerStateOptionAA(bool {14694})
		{
			if (this.loadedRS.CullMode > CullMode.None)
			{
				if ({14694})
				{
					this.SetRasterizerState(this.rsCacheCullingAa);
					return;
				}
				this.SetRasterizerState(this.rsCacheCullingNoAa);
				return;
			}
			else
			{
				if ({14694})
				{
					this.SetRasterizerState(this.rsCacheNocullingAa);
					return;
				}
				this.SetRasterizerState(this.rsCacheNocullingNoAa);
				return;
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x00023FA2 File Offset: 0x000221A2
		public void SetRasterizerStateOptions(bool {14695}, bool {14696})
		{
			if ({14696})
			{
				if ({14695})
				{
					this.SetRasterizerState(this.rsCacheCullingAa);
					return;
				}
				this.SetRasterizerState(this.rsCacheCullingNoAa);
				return;
			}
			else
			{
				if ({14695})
				{
					this.SetRasterizerState(this.rsCacheNocullingAa);
					return;
				}
				this.SetRasterizerState(this.rsCacheNocullingNoAa);
				return;
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x00023FE0 File Offset: 0x000221E0
		public void SetRasterizerStateUiScissor()
		{
			this.SetRasterizerState(this.rsCacheScissorUi);
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00023FEE File Offset: 0x000221EE
		public void SetRasterizerState(RasterizerState {14697})
		{
			this.graphicsDevice.RasterizerState = {14697};
			this.loadedRS = {14697};
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00024003 File Offset: 0x00022203
		public RasterizerState GetRasterizerState()
		{
			return this.graphicsDevice.RasterizerState;
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00024010 File Offset: 0x00022210
		public void ResetDepthBuffer()
		{
			this.graphicsDevice.DepthStencilState = DepthStencilState.None;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x00024024 File Offset: 0x00022224
		public void SetRenderTarget(IRenderTarget {14698})
		{
			if ({14698} == null)
			{
				this.ResetRenderTargets();
				return;
			}
			DeviceStatistics statistics = this.Statistics;
			int countSetRenderTargetMethod = statistics.CountSetRenderTargetMethod;
			statistics.CountSetRenderTargetMethod = countSetRenderTargetMethod + 1;
			this.graphicsDevice.SetRenderTargets({14698}.Targets);
			this.{14713}.Add({14698});
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x00024070 File Offset: 0x00022270
		public void ReturnRenderTarget()
		{
			IRenderTarget renderTarget = this.{14713}.Get(1);
			this.SetRenderTarget(renderTarget);
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00024094 File Offset: 0x00022294
		public void ResetRenderTargets()
		{
			DeviceStatistics statistics = this.Statistics;
			int countSetRenderTargetMethod = statistics.CountSetRenderTargetMethod;
			statistics.CountSetRenderTargetMethod = countSetRenderTargetMethod + 1;
			this.graphicsDevice.SetRenderTargets(null);
			this.{14713}.Add(null);
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x000240CE File Offset: 0x000222CE
		public void ClearRenderTarget()
		{
			this.graphicsDevice.Clear(this.{14706});
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x000240E1 File Offset: 0x000222E1
		public void ClearRenderTarget(Color {14699})
		{
			this.graphicsDevice.Clear({14699});
		}

		// Token: 0x060006F9 RID: 1785 RVA: 0x000240F0 File Offset: 0x000222F0
		public void ClearRenderTargetAndBuffer()
		{
			this.graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, this.{14706}, this.graphicsDevice.Viewport.MaxDepth, 0);
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x00024124 File Offset: 0x00022324
		public void ClearRenderTargetAndDepthBuffer(Color {14700})
		{
			this.graphicsDevice.Clear(ClearOptions.Target | ClearOptions.DepthBuffer, {14700}, this.graphicsDevice.Viewport.MaxDepth, 0);
		}

		// Token: 0x060006FB RID: 1787 RVA: 0x00024154 File Offset: 0x00022354
		public void ClearDepthBuffer()
		{
			this.graphicsDevice.Clear(ClearOptions.DepthBuffer, Color.Transparent, this.graphicsDevice.Viewport.MaxDepth, 0);
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00024188 File Offset: 0x00022388
		public void SetScissor(Rectangle {14701}, bool {14702})
		{
			if ({14702})
			{
				Vector2 vector = new Vector2(this.CurrentOutputSize.X / (float)this.UIArea.Width, this.CurrentOutputSize.Y / (float)this.UIArea.Height);
				{14701}.X = (int)((float){14701}.X * vector.X);
				{14701}.Y = (int)((float){14701}.Y * vector.Y);
				{14701}.Width = (int)((float){14701}.Width * vector.X);
				{14701}.Height = (int)((float){14701}.Height * vector.Y);
			}
			Rectangle xscreenRectangle = Engine.Game.XScreenRectangle;
			Rectangle.Intersect(ref {14701}, ref xscreenRectangle, out {14701});
			if ({14701}.Width > 0)
			{
				try
				{
					this.graphicsDevice.ScissorRectangle = {14701};
				}
				catch (ArgumentException)
				{
				}
			}
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x0002426C File Offset: 0x0002246C
		public void ResetScissor()
		{
			try
			{
				this.graphicsDevice.ScissorRectangle = Engine.Game.XScreenRectangle;
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x060006FE RID: 1790 RVA: 0x000242A4 File Offset: 0x000224A4
		public DeviceRenderPropertiesCache CacheRSAndBS()
		{
			return new DeviceRenderPropertiesCache
			{
				BS = this.graphicsDevice.BlendState,
				RS = this.graphicsDevice.RasterizerState
			};
		}

		// Token: 0x060006FF RID: 1791 RVA: 0x000242DE File Offset: 0x000224DE
		public void LoadCache(DeviceRenderPropertiesCache {14703})
		{
			this.SetColorBlendState({14703}.BS);
			this.SetRasterizerState({14703}.RS);
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x000242F8 File Offset: 0x000224F8
		public RenderTarget CreateScreeRenderTragte(string {14704})
		{
			return new RenderTarget(Engine.Game.XScreenRectangle.Width, Engine.Game.XScreenRectangle.Height, SurfaceFormat.Color, DepthFormat.None, 0, true, {14704}, false);
		}

		// Token: 0x040004E5 RID: 1253
		private readonly Color {14705} = Color.White;

		// Token: 0x040004E6 RID: 1254
		private readonly Color {14706} = Color.Transparent;

		// Token: 0x040004E7 RID: 1255
		private readonly Vector2 {14707} = Vector2.Zero;

		// Token: 0x040004E8 RID: 1256
		private readonly int[] {14708} = new int[]
		{
			0,
			1
		};

		// Token: 0x040004E9 RID: 1257
		public CustomSpriteFont Font;

		// Token: 0x040004EA RID: 1258
		public readonly DeviceStatistics Statistics;

		// Token: 0x040004EB RID: 1259
		public readonly GraphicsDevice graphicsDevice;

		// Token: 0x040004EC RID: 1260
		public Camera Camera;

		// Token: 0x040004ED RID: 1261
		public Device.ManualRender2DProperties Render2DProperties;

		// Token: 0x040004EE RID: 1262
		private SpriteBatch {14709};

		// Token: 0x040004EF RID: 1263
		private int {14710};

		// Token: 0x040004F0 RID: 1264
		private bool {14711};

		// Token: 0x040004F1 RID: 1265
		private Device.TextureQueue {14712};

		// Token: 0x040004F2 RID: 1266
		private Device.RenderTargetQueue {14713};

		// Token: 0x040004F3 RID: 1267
		private Rectangle? {14714};

		// Token: 0x040004F4 RID: 1268
		internal RasterizerState loadedRS = new RasterizerState();

		// Token: 0x040004F5 RID: 1269
		public readonly RasterizerState rsCacheCullingNoAa = new RasterizerState
		{
			CullMode = CullMode.CullCounterClockwiseFace,
			MultiSampleAntiAlias = false
		};

		// Token: 0x040004F6 RID: 1270
		public readonly RasterizerState rsCullingShadowMap = new RasterizerState
		{
			CullMode = CullMode.CullClockwiseFace,
			MultiSampleAntiAlias = false
		};

		// Token: 0x040004F7 RID: 1271
		public readonly RasterizerState rsCacheCullingAa = new RasterizerState
		{
			CullMode = CullMode.CullCounterClockwiseFace,
			MultiSampleAntiAlias = true
		};

		// Token: 0x040004F8 RID: 1272
		public readonly RasterizerState rsCacheNocullingNoAa = new RasterizerState
		{
			CullMode = CullMode.None,
			MultiSampleAntiAlias = false
		};

		// Token: 0x040004F9 RID: 1273
		public readonly RasterizerState rsCacheNocullingAa = new RasterizerState
		{
			CullMode = CullMode.None,
			MultiSampleAntiAlias = true
		};

		// Token: 0x040004FA RID: 1274
		public readonly RasterizerState rsCacheScissorUi = new RasterizerState
		{
			CullMode = CullMode.None,
			MultiSampleAntiAlias = false,
			ScissorTestEnable = true
		};

		// Token: 0x040004FB RID: 1275
		private VertexBufferBinding[] {14715} = new VertexBufferBinding[2];

		// Token: 0x020000F7 RID: 247
		protected class RenderTargetQueue
		{
			// Token: 0x06000701 RID: 1793 RVA: 0x00024323 File Offset: 0x00022523
			public RenderTargetQueue()
			{
				this.Clear();
			}

			// Token: 0x06000702 RID: 1794 RVA: 0x00024331 File Offset: 0x00022531
			public void Add(IRenderTarget {14716})
			{
				this.secound = this.first;
				this.first = {14716};
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x00024346 File Offset: 0x00022546
			public IRenderTarget Get(int {14717})
			{
				if ({14717} != 0)
				{
					return this.secound;
				}
				return this.first;
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x00024358 File Offset: 0x00022558
			public void Clear()
			{
				this.first = null;
				this.secound = null;
			}

			// Token: 0x040004FC RID: 1276
			internal IRenderTarget first;

			// Token: 0x040004FD RID: 1277
			internal IRenderTarget secound;
		}

		// Token: 0x020000F8 RID: 248
		protected class TextureQueue
		{
			// Token: 0x06000705 RID: 1797 RVA: 0x00024368 File Offset: 0x00022568
			public TextureQueue()
			{
				this.Clear();
			}

			// Token: 0x06000706 RID: 1798 RVA: 0x00024376 File Offset: 0x00022576
			public void Add(Texture2D {14718})
			{
				this.secound = this.first;
				this.first = {14718};
			}

			// Token: 0x06000707 RID: 1799 RVA: 0x0002438B File Offset: 0x0002258B
			public Texture2D Get(int {14719})
			{
				if ({14719} != 0)
				{
					return this.secound;
				}
				return this.first;
			}

			// Token: 0x06000708 RID: 1800 RVA: 0x0002439D File Offset: 0x0002259D
			public void Clear()
			{
				this.first = null;
				this.secound = null;
			}

			// Token: 0x040004FE RID: 1278
			internal Texture2D first;

			// Token: 0x040004FF RID: 1279
			internal Texture2D secound;
		}

		// Token: 0x020000F9 RID: 249
		public class ManualRender2DProperties
		{
			// Token: 0x06000709 RID: 1801 RVA: 0x000243AD File Offset: 0x000225AD
			public ManualRender2DProperties()
			{
				this.DefaultBlendState = BlendState.AlphaBlend;
				this.DefaultRasterizerState = new RasterizerState
				{
					CullMode = CullMode.None,
					ScissorTestEnable = true
				};
				this.DefaultSamplerState = SamplerState.LinearClamp;
			}

			// Token: 0x04000500 RID: 1280
			public BlendState DefaultBlendState;

			// Token: 0x04000501 RID: 1281
			public RasterizerState DefaultRasterizerState;

			// Token: 0x04000502 RID: 1282
			public SamplerState DefaultSamplerState;
		}
	}
}
