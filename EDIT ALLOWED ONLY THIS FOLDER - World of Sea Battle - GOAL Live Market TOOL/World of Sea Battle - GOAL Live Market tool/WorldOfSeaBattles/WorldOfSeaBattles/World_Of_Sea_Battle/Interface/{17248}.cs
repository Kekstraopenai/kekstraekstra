using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000089 RID: 137
	internal class {17248} : CustomUi
	{
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x0001FC2E File Offset: 0x0001DE2E
		private static ShipClass[] ClassesOrder
		{
			get
			{
				ShipClass[] array = new ShipClass[5];
				RuntimeHelpers.InitializeArray(array, fieldof(<PrivateImplementationDetails>.9C67595BDCB8F21525B46D16D646E56DB60A8821DD85D07C4B828659BE408F70).FieldHandle);
				return array;
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001FC41 File Offset: 0x0001DE41
		private static int GetShipOrder(PlayerShipInfo {17253})
		{
			if ({17253}.Coolness == PlayerShipCoolness.Empire)
			{
				return 9;
			}
			if ({17253}.Coolness == PlayerShipCoolness.Elite)
			{
				return 11;
			}
			if ({17253}.Coolness != PlayerShipCoolness.Unique)
			{
				return (int)((short)Array.IndexOf<ShipClass>({17248}.ClassesOrder, {17253}.Class));
			}
			return 10;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0001FC78 File Offset: 0x0001DE78
		public {17248}(Vector2 {17254}, bool {17255}, Action<PlayerShipDynamicInfo> {17256}, params PlayerShipDynamicInfo[] {17257}) : base(false)
		{
			{17248} <>4__this = this;
			this.{17292} = {17257};
			base.AddChild(new Form(new Marker(-1200f, (float)(Engine.GS.UIArea.Height - 800), 2400f, 1600f), new Marker(ref CommonAtlas.whiteDot).Border(1f).ToRect(), new Color(0.07f, 0.07f, 0.07f, 1f), PositionAlignment.LeftUp, PositionAlignment.RightDown)
			{
				AnimatedFocus = false
			});
			this.{17290} = new StackForm({17254}, {17255} ? UiOrientation.HorizontalBottom : UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			base.AddChild(this.{17290});
			if ({17255})
			{
				this.{17290}.PositionAlignment_Y = PositionAlignment.RightDown;
			}
			ScoreDictionary<int> scoreDictionary = new ScoreDictionary<int>();
			foreach (PlayerShipDynamicInfo playerShipDynamicInfo in {17257})
			{
				scoreDictionary.AddScore(playerShipDynamicInfo.CraftFrom.Rank, 1f);
			}
			if ({17257}.Length > 5 && scoreDictionary.Count >= 2)
			{
				{17248}.pickedRank = (({17248}.pickedRank == -2) ? Global.Player.UsedShipPlayer.CraftFrom.Rank : {17248}.pickedRank);
				Button button = null;
				StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Button button2 = this.CreateFavoriteShipsButton({17256}, {17254}, {17255});
				stackForm.AddItem(new UiControl[]
				{
					button2
				});
				if ({17248}.pickedRank == -1)
				{
					button = button2;
				}
				Button button3 = this.CreateAllShipsButton({17256}, {17254}, {17255});
				stackForm.AddItem(new UiControl[]
				{
					button3
				});
				if ({17248}.pickedRank == 0)
				{
					button = button3;
				}
				for (int j = 1; j < 8; j++)
				{
					int count = (int)scoreDictionary[j];
					string {17280} = StringHelper.ToRoman(j) + " " + Local.rang_is(string.Empty).ToLower();
					Label label;
					Button button4 = this.{17279}({17280}, count, out label);
					stackForm.AddItem(new UiControl[]
					{
						button4
					});
					int capturedRank = j;
					button4.UpdateComplete += delegate(UiControl {17309})
					{
						{17309}.Brightness = (({17248}.pickedRank == capturedRank) ? 1f : 0.6f);
					};
					IEnumerable<PlayerShipDynamicInfo> filteredShips = from {17310} in {17257}
					where {17310}.CraftFrom.Rank == capturedRank
					select {17310};
					Func<int> <>9__3;
					Func<IEnumerable<PlayerShipDynamicInfo>> <>9__4;
					button4.EvClick += delegate(ClickUiEventArgs {17311})
					{
						{17248} <>4__this = <>4__this;
						Action<PlayerShipDynamicInfo> onClick = {17256};
						Vector2 fixPosition = {17254};
						bool fixToBottom = {17255};
						Func<int> {17286};
						if (({17286} = <>9__3) == null)
						{
							{17286} = (<>9__3 = (() => count));
						}
						Func<IEnumerable<PlayerShipDynamicInfo>> {17287};
						if (({17287} = <>9__4) == null)
						{
							{17287} = (<>9__4 = (() => filteredShips));
						}
						<>4__this.OnRankButtonClick(onClick, fixPosition, fixToBottom, {17286}, {17287}, capturedRank, false);
					};
					if (j == {17248}.pickedRank)
					{
						button = button4;
					}
				}
				stackForm.AddSpace(2f);
				stackForm.Opacity = 0f;
				new UiOpacityAnimation(stackForm, 1f, 400f);
				this.{17290}.AddSpace(2f);
				this.{17290}.AddItem(new UiControl[]
				{
					stackForm
				});
				if (button != null)
				{
					button.ImitateClick(false);
				}
				UiControl uiControl = this.{17290};
				Marker pos = this.{17290}.Pos;
				Vector2 vector = {17254} - new Vector2(0f, this.{17290}.Pos.WH.Y);
				uiControl.Pos = pos.SetXY(vector);
				return;
			}
			this.{17290}.AddItem(new UiControl[]
			{
				this.CreateIcons({17257}, {17255}, {17256}, true, false)
			});
			if ({17255})
			{
				UiControl uiControl2 = this.{17290};
				Marker pos = this.{17290}.Pos;
				Vector2 vector = {17254} - new Vector2(0f, this.{17290}.Pos.WH.Y);
				uiControl2.Pos = pos.SetXY(vector);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00020064 File Offset: 0x0001E264
		protected override void UserUpdate(ref FrameTime {17258})
		{
			if (this.{17290}.Pos.WH.X > (float)Engine.GS.UIArea.Width)
			{
				int num = 400;
				if (Engine.GS.MouseToUI.X < (float)(Engine.GS.UIArea.Width / 6))
				{
					this.{17290}.Pos = this.{17290}.Pos.SetX(Math.Min(0f, this.{17290}.Pos.XY.X + {17258}.secElapsed * (float)num));
				}
				if (Engine.GS.MouseToUI.X > (float)(Engine.GS.UIArea.Width - Engine.GS.UIArea.Width / 6))
				{
					this.{17290}.Pos = this.{17290}.Pos.SetX(Math.Max(-(this.{17290}.Pos.WH.X - (float)Engine.GS.UIArea.Width), this.{17290}.Pos.XY.X - {17258}.secElapsed * (float)num));
				}
			}
			this.RankButtonClicked = Math.Max(0, this.RankButtonClicked - 1);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000201CC File Offset: 0x0001E3CC
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x000201D8 File Offset: 0x0001E3D8
		private UiControl CreateIcons(IEnumerable<PlayerShipDynamicInfo> {17259}, bool {17260}, Action<PlayerShipDynamicInfo> {17261}, bool {17262}, bool {17263})
		{
			{17228}[] array = (from {17293} in {17259}
			select new {17228}(Vector2.Zero, {17293}.CraftFrom).AddStatusIcons({17293})).ToArray<{17228}>();
			Tlist<{17228}> tlist = new Tlist<{17228}>();
			int {17269} = (array.Length > 20) ? -3 : 0;
			UiControl uiControl;
			if ({17262})
			{
				uiControl = this.BuildShipGroups<int>(tlist, array, ({17228} {17294}) => {17294}.Ship.Rank, (int {17295}) => -{17295}, {17261}, {17269}, true);
			}
			else
			{
				uiControl = this.BuildShipGroups<ShipClass>(tlist, array, ({17228} {17296}) => {17296}.Ship.Class, (ShipClass {17297}) => Array.IndexOf<ShipClass>({17248}.ClassesOrder, {17297}), {17261}, {17269}, false);
			}
			foreach ({17228} {17228} in ((IEnumerable<{17228}>)tlist))
			{
				{17228}.Opacity = 0f;
				float num = Vector2.Distance({17228}.Pos.XY, uiControl.Pos.XY + ({17260} ? new Vector2(0f, uiControl.Pos.WH.Y) : default(Vector2)));
				int num2 = (int)(Math.Max(0f, num - 50f) / 2f);
				new UiActionsSleep({17228}, (float)num2);
				new UiOpacityAnimation({17228}, 1f, 500f);
			}
			if ({17263})
			{
				Action<CheckboxCheckedEventArgs> <>9__5;
				foreach ({17228} {17228}2 in array)
				{
					{17228}2.AddFavoriteCheckbox();
					CheckboxControl checkboxControl = {17228}2.CheckboxControl;
					Action<CheckboxCheckedEventArgs> {13130};
					if (({13130} = <>9__5) == null)
					{
						{13130} = (<>9__5 = delegate(CheckboxCheckedEventArgs {17304})
						{
							int num3 = {17259}.Count((PlayerShipDynamicInfo {17298}) => Global.Settings.FavoriteShips.Contains((byte){17298}.CraftFrom.ID));
							this.{17291}.Text = num3.ToString();
						});
					}
					checkboxControl.EvCheck += {13130};
				}
			}
			return uiControl;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x000203F8 File Offset: 0x0001E5F8
		private StackForm BuildShipGroups<T>(Tlist<{17228}> {17264}, IEnumerable<{17228}> {17265}, Func<{17228}, T> {17266}, Func<T, int> {17267}, Action<PlayerShipDynamicInfo> {17268}, int {17269}, bool {17270})
		{
			UiOrientation {14079} = {17270} ? UiOrientation.HorizontalBottom : UiOrientation.Vertical;
			UiOrientation {14079}2 = (!{17270}) ? UiOrientation.HorizontalBottom : UiOrientation.Vertical;
			StackForm stackForm = new StackForm(Vector2.Zero, {14079}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BorderThickness = (float){17269}
			};
			foreach (IEnumerable<{17228}> source in from {17305} in {17265}.GroupBy({17266})
			orderby {17267}({17305}.Key)
			select {17305})
			{
				StackForm stackForm2 = new StackForm(Vector2.Zero, {14079}2, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BorderThickness = (float){17269}
				};
				using (IEnumerator<{17228}> enumerator2 = source.OrderBy(({17228} {17303}) => {17248}.GetShipOrder({17303}.Ship)).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						{17228} card = enumerator2.Current;
						stackForm2.AddItem(new UiControl[]
						{
							card
						});
						{17264}.Add(card);
						card.EvClick += delegate(ClickUiEventArgs {17306})
						{
							this.OnShipCardClick({17268}, card);
						};
					}
				}
				stackForm.AddItem(new UiControl[]
				{
					stackForm2
				});
			}
			return stackForm;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00020574 File Offset: 0x0001E774
		private void OnShipCardClick(Action<PlayerShipDynamicInfo> {17271}, {17228} {17272})
		{
			if ({17272}.IsCheckboxClicked())
			{
				this.WasCheckboxClick = true;
				return;
			}
			{17271}({17272}.UsedShipInfo);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x00020594 File Offset: 0x0001E794
		private Button CreateFavoriteShipsButton(Action<PlayerShipDynamicInfo> {17273}, Vector2 {17274}, bool {17275})
		{
			Func<int> countGetter = () => this.{17292}.Count((PlayerShipDynamicInfo {17299}) => Global.Settings.FavoriteShips.Contains((byte){17299}.CraftFrom.ID));
			Button button = this.{17279}(Local.favorite_ships, countGetter(), out this.{17291});
			Func<IEnumerable<PlayerShipDynamicInfo>> filteredShipsGetter = () => from {17300} in this.{17292}
			where Global.Settings.FavoriteShips.Contains((byte){17300}.CraftFrom.ID)
			select {17300};
			button.UpdateComplete += delegate(UiControl {17301})
			{
				{17301}.Brightness = (({17248}.pickedRank == -1) ? 1f : 0.6f);
			};
			button.EvClick += delegate(ClickUiEventArgs {17307})
			{
				this.OnRankButtonClick({17273}, {17274}, {17275}, countGetter, filteredShipsGetter, -1, true);
			};
			return button;
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0002063C File Offset: 0x0001E83C
		private Button CreateAllShipsButton(Action<PlayerShipDynamicInfo> {17276}, Vector2 {17277}, bool {17278})
		{
			int count = this.{17292}.Length;
			Label label;
			Button button = this.{17279}(Local.all_ships, count, out label);
			button.UpdateComplete += delegate(UiControl {17302})
			{
				{17302}.Brightness = (({17248}.pickedRank == 0) ? 1f : 0.6f);
			};
			Func<int> <>9__2;
			Func<IEnumerable<PlayerShipDynamicInfo>> <>9__3;
			button.EvClick += delegate(ClickUiEventArgs {17308})
			{
				{17248} <>4__this = this;
				Action<PlayerShipDynamicInfo> onClick = {17276};
				Vector2 fixPosition = {17277};
				bool fixToBottom = {17278};
				Func<int> {17286};
				if (({17286} = <>9__2) == null)
				{
					{17286} = (<>9__2 = (() => count));
				}
				Func<IEnumerable<PlayerShipDynamicInfo>> {17287};
				if (({17287} = <>9__3) == null)
				{
					{17287} = (<>9__3 = (() => this.{17292}));
				}
				<>4__this.OnRankButtonClick(onClick, fixPosition, fixToBottom, {17286}, {17287}, 0, true);
			};
			return button;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000206C4 File Offset: 0x0001E8C4
		private Button {17279}(string {17280}, int {17281}, out Label {17282})
		{
			Button button = new Button(Vector2.Zero, new Rectangle(504, 1594, 184, 40), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.Pos = button.Pos.Scale(0.95f);
			button.SetText({17280}, Fonts.Philosopher_16, Color.Wheat, true);
			{17282} = new Label(new Vector2(button.Pos.WH.X - 26f, button.Pos.XY.Y + 21f), Fonts.Philosopher_14Bold, ({17281} == 0) ? Color.Gray : Color.White, {17281}.ToString(), PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center();
			button.AddChild({17282});
			return button;
		}

		// Token: 0x060003CC RID: 972 RVA: 0x00020784 File Offset: 0x0001E984
		private void OnRankButtonClick(Action<PlayerShipDynamicInfo> {17283}, Vector2 {17284}, bool {17285}, Func<int> {17286}, Func<IEnumerable<PlayerShipDynamicInfo>> {17287}, int {17288}, bool {17289})
		{
			this.RankButtonClicked = 3;
			if ({17286}() != 0)
			{
				{17248}.pickedRank = {17288};
				UiControl uiControl = this.CreateIcons({17287}(), {17285}, {17283}, {17289}, true);
				if (this.{17290}.GetChildren.Size > 2)
				{
					this.{17290}.GetChildren.Last().RemoveFromContainer();
				}
				this.{17290}.AddItem(new UiControl[]
				{
					uiControl
				});
				if ({17285})
				{
					this.{17290}.Pos = this.{17290}.Pos.SetXY({17284});
				}
			}
		}

		// Token: 0x0400030D RID: 781
		private static int pickedRank = -2;

		// Token: 0x0400030E RID: 782
		public int RankButtonClicked;

		// Token: 0x0400030F RID: 783
		public bool WasCheckboxClick;

		// Token: 0x04000310 RID: 784
		private StackForm {17290};

		// Token: 0x04000311 RID: 785
		private Label {17291};

		// Token: 0x04000312 RID: 786
		private readonly PlayerShipDynamicInfo[] {17292};
	}
}
