using System;
using Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Interface;

namespace WorldOfSeaBattles.Interface.BasicUi
{
	// Token: 0x020005A3 RID: 1443
	public static class KeyNotationUi
	{
		// Token: 0x0600214E RID: 8526 RVA: 0x0012A538 File Offset: 0x00128738
		public static UiControl CreateKeyThenText(string {26443}, Keys {26444}, Color {26445}, int {26446} = 24)
		{
			StackForm stackForm = new StackForm(default(Vector2), UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddItem(new UiControl[]
			{
				KeyNotationUi.Create({26444}, {26445}, {26446})
			});
			stackForm.AddSpace((float)({26446} / 16));
			stackForm.AddItem(new UiControl[]
			{
				new Label(default(Vector2), Fonts.Philosopher_14, {26445}, {26443}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			return stackForm;
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0012A5A0 File Offset: 0x001287A0
		private static Rectangle GetPath(Keys {26447})
		{
			if ({26447} == Keys.Enter)
			{
				return {19086}.c_btHolderEnter;
			}
			if ({26447} != Keys.Space)
			{
				return {19086}.c_btHolderEmpty;
			}
			return {19086}.c_btHolderSpace;
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0012A5BD File Offset: 0x001287BD
		private static CustomSpriteFont PickFont(int {26448})
		{
			if ({26448} <= 24)
			{
				return Fonts.Arial_10;
			}
			if ({26448} <= 30)
			{
				return Fonts.Arial_12;
			}
			if ({26448} <= 36)
			{
				return Fonts.Philosopher_14;
			}
			if ({26448} > 48)
			{
				return Fonts.Philosopher_18;
			}
			return Fonts.Philosopher_16;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0012A5F0 File Offset: 0x001287F0
		private static Label PickFontAndCreateLabel(string {26449}, int {26450}, Color {26451})
		{
			return new Label(default(Vector2), KeyNotationUi.PickFont({26450}), {26451}, {26449}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0012A618 File Offset: 0x00128818
		public static UiControl Create(Keys {26452}, Color {26453}, int {26454} = 24)
		{
			Rectangle path = KeyNotationUi.GetPath({26452});
			Rectangle {13273} = ((float)Engine.GS.UIArea.Width > Engine.Game.WindowSize.X) ? new Marker(ref path).Border(1f).ToRect() : path;
			if (path != {19086}.c_btHolderEmpty)
			{
				return new Image(new Marker(0f, 0f, (float){26454}, (float){26454}), CommonAtlas.Texture.Tex, {13273}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = {26453}
				};
			}
			string text = {26452}.GetKeyName();
			if (string.IsNullOrEmpty(text))
			{
				text = "?";
			}
			Label label = KeyNotationUi.PickFontAndCreateLabel(text, {26454}, {26453});
			if (text.Length == 1)
			{
				Image image = new Image(new Marker(0f, 0f, (float){26454}, (float){26454}), CommonAtlas.Texture.Tex, {13273}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				image.BasicColor = {26453};
				image.AddChildPos(label, PositionAlignment.Center, PositionAlignment.Center, 0f);
				label.Pos = label.Pos.Offset(0f, 1f);
				return image;
			}
			Image image2 = new Image(new Marker(0f, 0f, (float)({26454} / 2) + label.PosWidth + 2f, (float){26454}), CommonAtlas.Texture.Tex, {13273}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			image2.BasicColor = {26453};
			image2.AddChildPos(label, PositionAlignment.Center, PositionAlignment.Center, 0f);
			label.Pos = label.Pos.Offset(0f, 1f);
			return image2;
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0012A794 File Offset: 0x00128994
		public static void CreateByDraw(Keys {26455}, Vector2 {26456}, int {26457}, Color {26458})
		{
			Rectangle path = KeyNotationUi.GetPath({26455});
			Rectangle rectangle;
			if (path != {19086}.c_btHolderEmpty)
			{
				Device gs = Engine.GS;
				Texture2D tex = CommonAtlas.Texture.Tex;
				rectangle = new Rectangle({26456}.X, {26456}.Y, (float){26457}, (float){26457}, false);
				gs.DrawCustomTexture(tex, path, rectangle, {26458});
				return;
			}
			string text = {26455}.GetKeyName();
			if (string.IsNullOrEmpty(text))
			{
				text = "?";
			}
			Engine.GS.SetFont(KeyNotationUi.PickFont({26457}));
			Vector2 vector;
			if (text.Length == 1)
			{
				Device gs2 = Engine.GS;
				Texture2D tex2 = CommonAtlas.Texture.Tex;
				rectangle = new Rectangle({26456}.X, {26456}.Y, (float){26457}, (float){26457}, false);
				gs2.DrawCustomTexture(tex2, path, rectangle, {26458});
				Device gs3 = Engine.GS;
				string {14610} = text;
				vector = {26456} + new Vector2((float){26457}) / 2f + new Vector2(0f, 1f);
				gs3.DrawStringCentered({14610}, vector, {26458});
				return;
			}
			Vector2 vector2 = Engine.GS.Font.Measure(text);
			Device gs4 = Engine.GS;
			Texture2D tex3 = CommonAtlas.Texture.Tex;
			rectangle = new Rectangle(0f, 0f, (float)({26457} / 2) + vector2.X + 2f, (float){26457}, false);
			gs4.DrawCustomTexture(tex3, path, rectangle, {26458});
			Device gs5 = Engine.GS;
			string {14610}2 = text;
			vector = {26456} + new Vector2((float){26457}) / 2f + new Vector2(vector2.X / 2f, 1f);
			gs5.DrawStringCentered({14610}2, vector, {26458});
		}
	}
}
