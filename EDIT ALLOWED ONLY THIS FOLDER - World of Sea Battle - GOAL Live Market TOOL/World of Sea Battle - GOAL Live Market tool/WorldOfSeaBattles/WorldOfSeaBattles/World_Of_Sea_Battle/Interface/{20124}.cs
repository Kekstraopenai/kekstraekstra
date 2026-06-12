using System;
using System.Linq;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000240 RID: 576
	internal class {20124}
	{
		// Token: 0x06000D19 RID: 3353 RVA: 0x0006D0CF File Offset: 0x0006B2CF
		public {20124}({17625} {20126})
		{
			this.{20133} = {20126};
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0006D0E0 File Offset: 0x0006B2E0
		public void Loader(ListItemViewControl {20127})
		{
			int num = 20;
			Form form = new Form(new Marker(0f, 0f, this.{20133}.Pos.WH.X - 65f, this.{20133}.Pos.WH.Y - 90f - (float)num), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			FractionID[] {20111} = FractionDecorator.AvailableFractions.Concat(new <>z__ReadOnlySingleElementList<FractionID>(FractionID.None)).ToArray<FractionID>();
			this.{20134} = new {20105}(form.Pos.XY, {20111});
			form.AddChild(this.{20134});
			float num2 = this.{20134}.Pos.WH.X + 20f;
			this.{20137} = form.Pos.WH.X - num2;
			Form form2 = new Form(new Marker(num2, form.Pos.XY.Y, this.{20137}, form.Pos.WH.Y), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			this.{20128}(form2);
			form.AddChild(form2);
			this.{20134}.SelectFraction(FractionID.None);
			this.{20130}(FractionID.None);
			{20127}.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, 0f, (float)num), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			{20127}.AddItem(new UiControl[]
			{
				form
			});
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0006D248 File Offset: 0x0006B448
		private void {20128}(Form {20129})
		{
			this.{20138} = new TextBox(new Marker(0f, 0f, 70f, 30f), {20124}.c_textBox_small, Fonts.Philosopher_14, Color.Black, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				DefaultText = Local.GuildsWindow_11
			};
			this.{20138}.AttachModerator(delegate(ref string {20140})
			{
				if ({20140}.Length == 0)
				{
					return string.Empty;
				}
				if ({20140}.Length != 3)
				{
					return Local.GuildsWindow_2;
				}
				if (ChatCensure.ValidateGuildTag({20140}))
				{
					return null;
				}
				return Local.GuildsWindow_13;
			}, Color.Yellow);
			this.{20139} = new TextBox(new Marker(0f, 0f, 300f, 30f), {20124}.c_textBox_big, Fonts.Philosopher_14, Color.Black, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				DefaultText = Local.GuildsWindow_12
			};
			this.{20139}.AttachModerator(delegate(ref string {20141})
			{
				return Gcc.ModerateText({20141}, Gcc.GuildNameLimits.Item1, Gcc.GuildNameLimits.Item2);
			}, Color.Yellow);
			{20129}.AddChildPos({17625}.CreateHorizontalUnion(80f, new UiControl[]
			{
				this.{20138},
				this.{20139}
			}), PositionAlignment.LeftUp, PositionAlignment.LeftUp, 0f, 10f, false);
			this.{20135} = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{20129}.AddChildPos(this.{20135}, PositionAlignment.LeftUp, PositionAlignment.LeftUp, 0f, 60f, false);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0006D3A0 File Offset: 0x0006B5A0
		private void {20130}(FractionID {20131})
		{
			this.{20135}.Clear();
			this.{20136} = ({20131} == FractionID.None);
			RTI rti = GuildCommon.GuildCreateCostGold(this.{20136}, false);
			int num = GuildCommon.MinimalCaptainRank(this.{20136});
			int num2 = GuildCommon.GuildMaxPlayersLim(this.{20136});
			string {13617} = this.{20136} ? Local.GuildsWindow_createText_flotilia : Local.GuildsWindow_createText_guild;
			Color color = Color.Gold * 0.95f;
			Color color2 = Color.Red * 0.95f;
			TextBlockBuilder textBlockBuilder = new TextBlockBuilder(Fonts.Philosopher_14, 1f);
			textBlockBuilder.Write(Local.CreateGuild_requirements, color);
			textBlockBuilder.WriteLine("   - " + StringHelper.BigValueHelper(rti.Value) + " " + Local.gold2, (rti.Value > Session.Account.Gold) ? color2 : color);
			textBlockBuilder.WriteLine("   - " + Local.CreateGuild_captain_rank(num), (num > Session.Account.Rang) ? color2 : color);
			textBlockBuilder.WriteLine("   - " + Local.CreateGuild_max_players(num2), color);
			this.{20135}.AddItem(new UiControl[]
			{
				TextBlockBuilder.CreateBlock(this.{20137} - 40f, {13617}, Color.Gray, Fonts.Philosopher_14, 0f).Create(Vector2.Zero)
			});
			this.{20135}.AddSpace(20f);
			this.{20135}.AddItem(new UiControl[]
			{
				textBlockBuilder.Create(Vector2.Zero)
			});
			this.{20134}.Button.IsVisible = false;
			this.{20135}.AddSpace(20f);
			this.{20135}.AddItem(new UiControl[]
			{
				new Button(Vector2.Zero, {17625}.c_btGray_big, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.create, Fonts.Philosopher_14, Color.White, false).ExClick(delegate(ClickUiEventArgs {20142})
				{
					if (!this.{20132}())
					{
						return;
					}
					if (this.{20138}.HasModeratorError || string.IsNullOrEmpty(this.{20138}.Text))
					{
						return;
					}
					if (this.{20139}.HasModeratorError)
					{
						return;
					}
					string guildTag = this.{20138}.Text;
					string guildName = this.{20139}.Text;
					{17312}.AskPrice(Local.GuildsWindow_3q(this.{20136} ? Local.GuildsWindow_3_f : Local.GuildsWindow_3, guildName), GuildCommon.GuildCreateCostGold(this.{20136}, false).Value, delegate
					{
						this.{20138}.Text = string.Empty;
						this.{20133}.AllowMouseInput = false;
						Global.Network.Send(new OnCreateGuildQuery(guildTag, guildName, this.{20136} ? FractionID.None : {20131}));
					}, false);
				})
			});
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0006D5C0 File Offset: 0x0006B7C0
		private bool {20132}()
		{
			return GuildCommon.MinimalCaptainRank(this.{20136}) <= Session.Account.Rang && GuildCommon.GuildCreateCostGold(this.{20136}, false).Value <= Session.Account.Gold;
		}

		// Token: 0x04000C57 RID: 3159
		private static readonly Rectangle c_textBox_small = new Rectangle(2562, 1214, 152, 25);

		// Token: 0x04000C58 RID: 3160
		private static readonly Rectangle c_textBox_big = new Rectangle(2414, 1188, 300, 25);

		// Token: 0x04000C59 RID: 3161
		private readonly {17625} {20133};

		// Token: 0x04000C5A RID: 3162
		private {20105} {20134};

		// Token: 0x04000C5B RID: 3163
		private StackForm {20135};

		// Token: 0x04000C5C RID: 3164
		private bool {20136};

		// Token: 0x04000C5D RID: 3165
		private float {20137};

		// Token: 0x04000C5E RID: 3166
		private TextBox {20138};

		// Token: 0x04000C5F RID: 3167
		private TextBox {20139};
	}
}
