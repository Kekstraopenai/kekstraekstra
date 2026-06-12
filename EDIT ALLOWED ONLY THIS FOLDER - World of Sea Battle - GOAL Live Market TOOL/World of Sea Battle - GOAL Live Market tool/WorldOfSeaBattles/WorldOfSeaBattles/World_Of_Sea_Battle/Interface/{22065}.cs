using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000382 RID: 898
	internal class {22065} : Label
	{
		// Token: 0x0600138F RID: 5007 RVA: 0x000A57BC File Offset: 0x000A39BC
		public {22065}(Vector2 {22072}, float {22073}, CustomSpriteFont {22074}, string {22075}, [TupleElementNames(new string[]
		{
			"val",
			"isOrange"
		})] Func<ValueTuple<string, bool>> {22076}, Color? {22077} = null) : base({22072}, {22074}, new Color(183, 193, 204), {22075}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			{22065} <>4__this = this;
			Color statColor = {22077} ?? new Color(168, 154, 148);
			Label internalLabel = new Label({22072} + new Vector2({22073}, 0f), {22074}, statColor, "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(new UiControl[]
			{
				internalLabel
			});
			base.UpdateComplete += delegate(UiControl {22078})
			{
				ValueTuple<string, bool> valueTuple = {22076}();
				internalLabel.Text = valueTuple.Item1;
				internalLabel.BasicColor = (valueTuple.Item2 ? Color.OrangeRed : statColor);
				internalLabel.Pos = internalLabel.Pos.SetX(<>4__this.Pos.XY.X + {22073} - internalLabel.Pos.WH.X);
			};
		}
	}
}
