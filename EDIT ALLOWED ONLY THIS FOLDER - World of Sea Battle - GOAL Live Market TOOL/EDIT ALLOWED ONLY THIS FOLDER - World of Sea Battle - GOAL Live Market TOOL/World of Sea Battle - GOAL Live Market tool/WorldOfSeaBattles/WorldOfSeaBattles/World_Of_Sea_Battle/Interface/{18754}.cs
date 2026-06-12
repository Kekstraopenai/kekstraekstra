using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Data;
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
	// Token: 0x0200017D RID: 381
	internal sealed class {18754} : {17068}
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x00043740 File Offset: 0x00041940
		public {18754}() : base(new Marker((float)(Engine.GS.UIArea.Width - {18754}.c_Back.Width - 20), (float)(Engine.GS.UIArea.Height / 2 - {18754}.c_Back.Height / 2), (float){18754}.c_Back.Width, (float){18754}.c_Back.Height), {18754}.c_Back, {17068}.BlockingWay.NoBackground, false)
		{
			this.AnimatedFocus = false;
			this.{18756}({18754}.Page.Graphic);
			new UiMarkerAndOpacityAnimation(this, 0f, 1f, base.Pos.Offset(50f, 0f), base.Pos, 500f, UiAmimationCurve.Linear);
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserUpdate(ref FrameTime {18755})
		{
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x000437FC File Offset: 0x000419FC
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(OtherTextures.StartConfugirator.Tex);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x000201CC File Offset: 0x0001E3CC
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x060008B7 RID: 2231 RVA: 0x00043814 File Offset: 0x00041A14
		private void {18756}({18754}.Page {18757})
		{
			base.ClearAllChild();
			if ({18757} != {18754}.Page.Close)
			{
				string {13345} = ({18757} == {18754}.Page.Graphic) ? Local.StartConfigurator_title_1 : Local.StartConfigurator_title_2;
				this.{18764} = new Label(Vector2.Zero, Fonts.NotoSansJP_14, Color.White, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).CenterX();
				base.AddChildPos(this.{18764}, PositionAlignment.Center, PositionAlignment.LeftUp, 12f);
			}
			switch ({18757})
			{
			case {18754}.Page.Graphic:
				this.{18765} = Global.Settings.Language.CurrentGameLocale;
				this.{18759}();
				return;
			case {18754}.Page.Gamma:
			{
				Locale? currentGameLocale = Global.Settings.Language.CurrentGameLocale;
				Locale? locale = this.{18765};
				if (!(currentGameLocale.GetValueOrDefault() == locale.GetValueOrDefault() & currentGameLocale != null == (locale != null)))
				{
					GameplayAssist.ReloadQuests();
				}
				this.{18760}();
				return;
			}
			case {18754}.Page.Close:
				base.BlockAndClose();
				return;
			default:
				throw new NotSupportedException();
			}
		}

		// Token: 0x060008B8 RID: 2232 RVA: 0x000438F4 File Offset: 0x00041AF4
		private void {18758}()
		{
			string startConfigurator_title_ = Local.StartConfigurator_title_1;
			base.RemoveChild(this.{18764});
			this.{18764} = new Label(Vector2.Zero, Fonts.NotoSansJP_14, Color.White, startConfigurator_title_, PositionAlignment.LeftUp, PositionAlignment.LeftUp).CenterX();
			base.AddChildPos(this.{18764}, PositionAlignment.Center, PositionAlignment.LeftUp, 12f);
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x0004394C File Offset: 0x00041B4C
		private void {18759}()
		{
			{18754}.<>c__DisplayClass19_0 CS$<>8__locals1 = new {18754}.<>c__DisplayClass19_0();
			CS$<>8__locals1.<>4__this = this;
			StackForm {12952} = CS$<>8__locals1.<LoadGraphicSettingsPage>g__LoadGraphicOptions|4();
			base.AddChildPos({12952}, PositionAlignment.Center, PositionAlignment.LeftUp, 120f);
			CS$<>8__locals1.button = new Button(Vector2.Zero, {18754}.c_Button, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			CS$<>8__locals1.button.Pos = CS$<>8__locals1.button.Pos.Scale(1.2f);
			CS$<>8__locals1.button.SetText(Local.accept, Fonts.Philosopher_16, Color.White, false);
			CS$<>8__locals1.button.EvClick += delegate(ClickUiEventArgs {18770})
			{
				CS$<>8__locals1.<>4__this.{18756}({18754}.Page.Gamma);
			};
			base.AddChildPos(CS$<>8__locals1.button, PositionAlignment.Center, PositionAlignment.RightDown, 40f);
			Form form = new Form(new Marker(0f, 0f, base.Pos.WH.X - 10f, 200f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.TexturePath = AtlasEntryGui.whitePixel;
			form.BasicColor = Color.Black * 0.25f;
			base.AddChildPos(form, PositionAlignment.Center, PositionAlignment.LeftUp, 380f);
			Locale[] array = (from {18766} in Enum.GetValues<Locale>()
			where !new LocaleInfo({18766}).IsDevLocale
			select {18766}).ToArray<Locale>();
			Array.IndexOf<Locale>(array, LocaleInfo.Current.Id);
			int num = Array.IndexOf<Locale>(array, Locale.En);
			int num2 = Array.IndexOf<Locale>(array, Locale.Ru);
			array[num] = Locale.Ru;
			array[num2] = Locale.En;
			BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(default(Vector2), 2, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Locale[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				{18754}.<>c__DisplayClass19_1 CS$<>8__locals2 = new {18754}.<>c__DisplayClass19_1();
				CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
				CS$<>8__locals2.item = array2[i];
				Form langRectangle = new Form(new Marker(base.Pos.XY.X + 10f, base.Pos.XY.Y + 100f, 180f, 40f), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				langRectangle.TexturePath = AtlasEntryGui.WhiteRectangle;
				Label label = new Label(default(Vector2), Fonts.NotoSansJP_14, Color.White, CS$<>8__locals2.item.GetInfo().Item1, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				langRectangle.AddChildPos(label, PositionAlignment.Center, PositionAlignment.Center, 0f);
				langRectangle.UpdateComplete += delegate(UiControl {18778})
				{
					langRectangle.BasicColor = ((LocaleInfo.Current.Id == CS$<>8__locals2.item) ? Color.Wheat : Color.Gray);
					label.BasicColor = ((LocaleInfo.Current.Id == CS$<>8__locals2.item) ? Color.Wheat : Color.Gray);
					label.Font = ((LocaleInfo.Current.Id == CS$<>8__locals2.item) ? Fonts.NotoSansJP_14_Bold : Fonts.NotoSansJP_14);
				};
				langRectangle.EvClick += delegate(ClickUiEventArgs {18777})
				{
					CS$<>8__locals2.CS$<>8__locals1.<LoadGraphicSettingsPage>g__ApplyLang|5(CS$<>8__locals2.item);
				};
				blocksStackFormControl.AddItem(new UiControl[]
				{
					langRectangle
				});
			}
			form.AddChildPos(blocksStackFormControl, PositionAlignment.Center, PositionAlignment.Center, 0f);
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x00043C20 File Offset: 0x00041E20
		private void {18760}()
		{
			StackForm gammaStack = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Rectangle[] array = {18754}.c_Gamma;
			for (int i = 0; i < array.Length; i++)
			{
				Rectangle path = array[i];
				float {11535} = 0f;
				float {11536} = 0f;
				Vector2 vector = path.WidthHeight() * 0.9f;
				Image image = new Image(new Marker({11535}, {11536}, ref vector), () => OtherTextures.StartConfugirator.Tex, () => path, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				gammaStack.AddItem(new UiControl[]
				{
					image
				});
				gammaStack.AddSpace(10f);
			}
			base.AddChildPos(gammaStack, PositionAlignment.Center, PositionAlignment.LeftUp, 100f);
			gammaStack.IsVisible = false;
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.VerticalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			TextBlockBuilder textBlockBuilder = TextBlockBuilder.CreateBlock(360f, Local.StartConfigurator_gamma_help, Color.White, Fonts.Philosopher_16, 1f);
			stackForm.AddItem(new UiControl[]
			{
				textBlockBuilder.CreateCentroid()
			});
			stackForm.AddSpace(10f);
			ProgressSelectBar progressSelectBar = new ProgressSelectBar(new Marker(0f, 0f, 400f, 30f), {18754}.c_ProgressBarActive, {18754}.c_ProgressBarPassive, {18754}.c_ProgressBarPointer, Color.White, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			progressSelectBar.ExpansionSetVals(Global.Settings.GammaSetting + 0.5f, 1f);
			progressSelectBar.ExpansionChangeEvent(delegate(ProgressBarChangeEventArgs {18779})
			{
				Global.Settings.GammaSetting = {18779}.NewValue - 0.5f;
				gammaStack.Brightness = 0.5f + {18779}.NewValue;
			});
			stackForm.AddItem(new UiControl[]
			{
				progressSelectBar
			});
			stackForm.AddSpace(10f);
			StackForm stackForm2 = stackForm;
			UiControl[] array2 = new UiControl[1];
			array2[0] = new Button(Vector2.Zero, {18754}.c_Button, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.set_light_day, Fonts.Philosopher_14, Color.White * 0.8f, false).ExClick(delegate(ClickUiEventArgs {18767})
			{
				Global.Render.GetSceneManager.WorldTime = 12f;
			});
			stackForm2.AddItem(array2);
			stackForm.AddSpace(5f);
			StackForm stackForm3 = stackForm;
			UiControl[] array3 = new UiControl[1];
			array3[0] = new Button(Vector2.Zero, {18754}.c_Button, PositionAlignment.LeftUp, PositionAlignment.LeftUp).SetText(Local.set_night, Fonts.Philosopher_14, Color.White * 0.8f, false).ExClick(delegate(ClickUiEventArgs {18768})
			{
				Global.Render.GetSceneManager.WorldTime = 1f;
			});
			stackForm3.AddItem(array3);
			stackForm.AddSpace(5f);
			StackForm stackForm4 = stackForm;
			UiControl[] array4 = new UiControl[1];
			array4[0] = new CheckboxControl(Vector2.Zero, new Rectangle(536, 532, 25, 25), new Rectangle(510, 532, 25, 25), Global.Settings.BrightNight, PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExCheckEvent(delegate(CheckboxCheckedEventArgs {18769})
			{
				Global.Settings.BrightNight = {18769}.NewValue;
			}).SetText(Local.GameSettingsWindow_brightNight, Fonts.Philosopher_14, Color.White * 0.5f);
			stackForm4.AddItem(array4);
			base.AddChildPos(stackForm, PositionAlignment.Center, PositionAlignment.LeftUp, 100f);
			Button button = new Button(Vector2.Zero, {18754}.c_Button, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.Pos = button.Pos.Scale(1.2f);
			button.SetText(Local.accept, Fonts.Philosopher_16, Color.White, false);
			button.EvClick += delegate(ClickUiEventArgs {18780})
			{
				this.{18756}({18754}.Page.Close);
			};
			base.AddChildPos(button, PositionAlignment.Center, PositionAlignment.RightDown, 40f);
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x060008BC RID: 2236 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x060008BE RID: 2238 RVA: 0x000440DA File Offset: 0x000422DA
		[CompilerGenerated]
		internal static string[] <LoadGraphicSettingsPage>g__GetPresetsName|19_3()
		{
			return new string[]
			{
				Local.StartConfigurator_graphic_1,
				Local.StartConfigurator_graphic_2,
				Local.StartConfigurator_graphic_3,
				Local.StartConfigurator_graphic_4
			};
		}

		// Token: 0x040007B7 RID: 1975
		private static readonly Rectangle c_Back = new Rectangle(0, 0, 500, 680);

		// Token: 0x040007B8 RID: 1976
		private static readonly Rectangle c_Button = new Rectangle(503, 363, 212, 37);

		// Token: 0x040007B9 RID: 1977
		private static readonly Rectangle c_Preset = new Rectangle(503, 402, 251, 30);

		// Token: 0x040007BA RID: 1978
		private static readonly Rectangle c_ProgressBarActive = new Rectangle(531, 434, 295, 26);

		// Token: 0x040007BB RID: 1979
		private static readonly Rectangle c_ProgressBarPassive = new Rectangle(531, 434, 295, 26);

		// Token: 0x040007BC RID: 1980
		private static readonly Rectangle c_ProgressBarPointer = new Rectangle(503, 434, 27, 27);

		// Token: 0x040007BD RID: 1981
		private static readonly Rectangle[] c_Gamma = new Rectangle[]
		{
			new Rectangle(503, 0, 480, 115),
			new Rectangle(503, 120, 480, 120),
			new Rectangle(503, 245, 480, 115)
		};

		// Token: 0x040007BE RID: 1982
		private int {18761} = 1;

		// Token: 0x040007BF RID: 1983
		private Form[] {18762};

		// Token: 0x040007C0 RID: 1984
		private Label[] {18763};

		// Token: 0x040007C1 RID: 1985
		private Label {18764};

		// Token: 0x040007C2 RID: 1986
		private Locale? {18765};

		// Token: 0x0200017E RID: 382
		private enum Page
		{
			// Token: 0x040007C4 RID: 1988
			Graphic,
			// Token: 0x040007C5 RID: 1989
			Gamma,
			// Token: 0x040007C6 RID: 1990
			Close
		}
	}
}
