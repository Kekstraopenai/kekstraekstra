using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000179 RID: 377
	internal sealed class {18738} : {17625}
	{
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060008A4 RID: 2212 RVA: 0x00042DFC File Offset: 0x00040FFC
		// (remove) Token: 0x060008A5 RID: 2213 RVA: 0x00042E34 File Offset: 0x00041034
		public event Action Finished
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{18745};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{18745}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{18745};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{18745}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00042E6C File Offset: 0x0004106C
		public {18738}() : base(470f, {17625}.v_backVerticalSkullDark, {17604}.InGameWindowBlockShipDialogWithoutDeco, {17625}.c_icBook, Array.Empty<{17625}.DynamicTittle>())
		{
			{18738} <>4__this = this;
			Image extraBack = new Image(new Marker(0f, 0f, (float)Engine.GS.UIArea.Width, (float)Engine.GS.UIArea.Height), CommonAtlas.Texture.Tex, new Rectangle(570, 3792, 400, 205), PositionAlignment.Both, PositionAlignment.Both);
			extraBack.MoveToBackLevel();
			base.AddChildPos(new Form(new Marker(0f, 0f, (float)CommonAtlas.backgroundDecor.Width * 1.05f, (float)CommonAtlas.backgroundDecor.Height * 1.05f), CommonAtlas.backgroundDecor, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false,
				BasicColor = new Color(1f, 1f, 1f, 0.5f)
			}, PositionAlignment.Center, PositionAlignment.LeftUp, 210f);
			Form form = new Form(new Marker(0f, 0f, (float)CommonAtlas.topScroll.Width, (float)CommonAtlas.topScroll.Height), CommonAtlas.topScroll, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form.Pos = form.Pos.ScaleWidth(1.2f);
			base.AddChildPos(form, PositionAlignment.Center, PositionAlignment.LeftUp, 130f);
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Button button = new Button(Vector2.Zero, new Rectangle(881, 217, 24, 67), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {18748})
			{
				Session.Account.SelectedCaptainIcon = (byte)((int)(Session.Account.SelectedCaptainIcon + 1) % CommonAtlas.captainIcons.Length);
			});
			stackForm.AddItem(new UiControl[]
			{
				new TextureHost(AtlasEntryGui.Texture.Tex, button.Pos).AddChild(button)
			});
			byte b = 11;
			Rectangle {13185} = CommonAtlas.captainIcons[(int)b];
			Session.Account.SelectedCaptainIcon = b;
			Form form2 = new Form(new Marker(0f, 0f, ref {13185}).Border(5f), {17625}.c_verticalCard, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			Form form3 = new Form(form2.Pos.Border(-5f), {13185}, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			form3.UpdateComplete += delegate(UiControl {18749})
			{
				({18749} as Form).TexturePath = Session.CaptainIcon;
			};
			form2.AddChildPos(form3, PositionAlignment.Center, PositionAlignment.Center, 0f);
			stackForm.AddItem(new UiControl[]
			{
				form2
			});
			Button button2 = new Button(Vector2.Zero, new Rectangle(906, 217, 24, 67), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {18750})
			{
				Session.Account.SelectedCaptainIcon = (byte)(((int)(Session.Account.SelectedCaptainIcon - 1) + CommonAtlas.captainIcons.Length) % CommonAtlas.captainIcons.Length);
			});
			stackForm.AddItem(new UiControl[]
			{
				new TextureHost(AtlasEntryGui.Texture.Tex, button2.Pos).AddChild(button2)
			});
			StackForm stackForm2 = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm2.AddSpace(10f);
			TextBox inputName = new TextBox(Vector2.Zero, new Rectangle(484, 148, 264, 36), Fonts.Arial_12, Color.LightGray, AtlasEntryGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			inputName.CharsMode = SpecialCharMode.SlimCharMap;
			TextBox inputName2 = inputName;
			TextBox.Moderator {13698};
			if (({13698} = {18738}.<>O.<0>__ModerateNickname) == null)
			{
				{13698} = ({18738}.<>O.<0>__ModerateNickname = new TextBox.Moderator(Gcc.ModerateNickname));
			}
			inputName2.AttachModerator({13698}, Color.Yellow);
			inputName.DefaultText = Local.EntryScene_3;
			stackForm2.AddItem(new UiControl[]
			{
				new TextureHost(AtlasEntryGui.Texture.Tex, inputName.Pos).AddChild(inputName)
			});
			Color {13344} = new Color(0.2f, 0.2f, 0.2f, 1f);
			base.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_16, {13344}, Local.select_character_replace_1, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 145f);
			base.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_16Bold, {13344}, Local.select_character_replace_2, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 160f);
			base.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.LeftUp, 280f);
			base.AddChildPos(stackForm2, PositionAlignment.Center, PositionAlignment.LeftUp, 370f);
			StackForm stackForm3 = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm3.AddSpace(10f);
			TextBox inputRefSid = new TextBox(Vector2.Zero, new Rectangle(484, 148, 264, 36), Fonts.Arial_12, Color.LightSkyBlue, AtlasEntryGui.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				PosWidth = 220f
			};
			inputRefSid.CharsMode = SpecialCharMode.OnlyWordsAndNumbers;
			inputRefSid.AttachMaxLengthModerator(10, null, this.{18747});
			inputRefSid.DefaultText = Local.EntryScene_4;
			stackForm3.AddItem(new UiControl[]
			{
				new TextureHost(AtlasEntryGui.Texture.Tex, inputRefSid.Pos).AddChild(inputRefSid)
			});
			base.AddChildPos(stackForm3, PositionAlignment.Center, PositionAlignment.LeftUp, 420f);
			StackForm stackForm4 = new StackForm(Vector2.Zero, UiOrientation.HorizontalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			AnimatedButton confirmBt = new AnimatedButton(Vector2.Zero, AtlasEntryGui.btPlayPassive, AtlasEntryGui.btPlayPassive, AtlasEntryGui.btPlayActive, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			confirmBt.SetText(Local.select_character_accept, Fonts.Philosopher_18, Color.Wheat, false);
			confirmBt.UpdateComplete += delegate(UiControl {18751})
			{
				bool flag = inputName.Text.Length > 0 && !inputName.HasModeratorError;
				bool flag2 = inputRefSid.Text == string.Empty || (inputRefSid.Text.Length > 0 && !inputRefSid.HasModeratorError);
				{18751}.IsVisible = (flag && flag2);
			};
			confirmBt.IsVisible = false;
			stackForm4.AddItem(new UiControl[]
			{
				new TextureHost(AtlasEntryGui.Texture.Tex, confirmBt.Pos).AddChild(confirmBt)
			});
			base.AddChildPos(stackForm4, PositionAlignment.Center, PositionAlignment.LeftUp, 465f);
			{18738}.CurrentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{18738}.CurrentInstance = null;
				if (extraBack != null)
				{
					extraBack.MoveToFrontLevel();
					new UiOpacityAnimation(extraBack, 0f, 2000f);
					new UiRemoveAction(extraBack);
					extraBack = null;
				}
			};
			confirmBt.EvClick += delegate(ClickUiEventArgs {18752})
			{
				uint num;
				if (<>4__this.{18742}(inputRefSid, out num))
				{
					{18752}.Sender.AllowMouseInput = false;
					uint {9009};
					uint.TryParse(inputRefSid.Text, out {9009});
					Global.Network.Send(new OnSetStartCharacterInfoMsg(inputName.Text, {9009}));
				}
			};
			this.{18746} = delegate(OnSetStartCharacterInfoResultMsg {18753})
			{
				confirmBt.AllowMouseInput = true;
				if (!{18753}.ReferalFriendSIDIsValid)
				{
					new {17312}(Local.EntryScene_16);
					return;
				}
				if (!{18753}.NameIsValid)
				{
					inputName.SetCustomErrorUntilTextChange(Local.EntryScene_13);
					return;
				}
				<>4__this.BlockAndClose();
				Action action = <>4__this.{18745};
				if (action == null)
				{
					return;
				}
				action();
			};
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x000434BB File Offset: 0x000416BB
		public void Response(in OnSetStartCharacterInfoResultMsg {18741})
		{
			Action<OnSetStartCharacterInfoResultMsg> action = this.{18746};
			if (action == null)
			{
				return;
			}
			action({18741});
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x000434D4 File Offset: 0x000416D4
		private bool {18742}(TextBox {18743}, out uint {18744})
		{
			if (string.IsNullOrEmpty({18743}.Text))
			{
				{18744} = 0U;
				return true;
			}
			if (uint.TryParse({18743}.Text, out {18744}) && {18744} != 0U)
			{
				return true;
			}
			IEnumerable<char> text = {18743}.Text;
			Func<char, bool> predicate;
			if ((predicate = {18738}.<>O.<1>__IsNumber) == null)
			{
				predicate = ({18738}.<>O.<1>__IsNumber = new Func<char, bool>(char.IsNumber));
			}
			if (!text.Any(predicate))
			{
				{18744} = 0U;
				return true;
			}
			new {17312}(Local.EntryScene_25);
			return false;
		}

		// Token: 0x040007A8 RID: 1960
		public static {18738} CurrentInstance;

		// Token: 0x040007A9 RID: 1961
		[CompilerGenerated]
		private Action {18745};

		// Token: 0x040007AA RID: 1962
		private Action<OnSetStartCharacterInfoResultMsg> {18746};

		// Token: 0x040007AB RID: 1963
		private Color {18747} = Color.DarkRed;

		// Token: 0x0200017A RID: 378
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x040007AC RID: 1964
			public static TextBox.Moderator <0>__ModerateNickname;

			// Token: 0x040007AD RID: 1965
			public static Func<char, bool> <1>__IsNumber;
		}
	}
}
