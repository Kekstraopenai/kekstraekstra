using System;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000273 RID: 627
	internal class {20363} : StackForm
	{
		// Token: 0x06000DEA RID: 3562 RVA: 0x00075674 File Offset: 0x00073874
		public {20363}() : base(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			base.AddItem(new UiControl[]
			{
				{20363}.CreateHeader()
			});
			GuildQuests questsCache = {20364}.CurrentInstance.QuestsCache;
			if (questsCache != null)
			{
				base.AddSpace(10f);
				{20338} {20338} = new {20338}(Session.Guild.IsFlotilia ? FractionID.Espaniol : Session.Guild.Fraction, questsCache, 0.74f, (float)({20364}.ContentWidth - 40));
				Form form = new Form(new Marker(0f, 0f, (float){20364}.ContentWidth, {20338}.Pos.WH.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					AnimatedFocus = false
				};
				form.AddChildPos({20338}, PositionAlignment.Center, PositionAlignment.Center, 0f);
				base.AddItem(new UiControl[]
				{
					form
				});
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00075740 File Offset: 0x00073940
		private static Form CreateHeader()
		{
			Form form = new Form(new Marker(0f, 0f, (float){20364}.ContentWidth, 100f), {20363}.c_headerBack, Color.Gray, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 10f);
			textBlockBuilder.WriteLines(Local.guild_quests_body, Color.White * 0.7f, Fonts.Philosopher_14, form.Pos.WH.X - 40f, new float?((float)-6));
			form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_16Bold, Color.White, Session.Guild.IsFlotilia ? Local.flotilia_limit : Local.guild_quests_header(Session.Guild.Fraction.GetName()), PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.LeftUp, 20f, 6f, false);
			form.AddChildPos(textBlockBuilder.Create(), PositionAlignment.LeftUp, PositionAlignment.LeftUp, 20f, 34f, false);
			if (Session.Guild.IsFlotilia)
			{
				form.Opacity = 0.5f;
			}
			return form;
		}

		// Token: 0x04000D0E RID: 3342
		public static readonly Rectangle c_headerBack = new Rectangle(159, 223, 284, 44);
	}
}
