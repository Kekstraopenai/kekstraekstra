using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Common;
using Common.Account;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003CA RID: 970
	internal class {22585} : {17625}
	{
		// Token: 0x06001532 RID: 5426 RVA: 0x000B2984 File Offset: 0x000B0B84
		public {22585}() : base(700f, {17625}.c_back3, {17604}.InGameWindow, {17625}.c_icPeople, new {17625}.DynamicTittle[]
		{
			Local.FirendsWindow_0
		})
		{
			if ({22585}.CurrentInstance != null)
			{
				throw new InvalidOperationException();
			}
			{22585}.CurrentInstance = this;
			Session.FriendsChanged += this.{22589};
			base.EvRemoveFromContainer += this.{22590};
			this.{22606} = (Session.FriendsCache.Size < 10);
			this.{22608} = true;
			this.{22586}();
			EducationHelper.MakeFlag(EducationOnboarding.OpenSocialMenu, true);
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x000B2A38 File Offset: 0x000B0C38
		private void {22586}()
		{
			base.ComposeTabWithScroll(null, true, false, new Action<ListItemViewControl>[]
			{
				new Action<ListItemViewControl>(this.{22591})
			});
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x000B2A64 File Offset: 0x000B0C64
		public void GlobalSearchResponse(OnGlobalPlayerSearch {22587})
		{
			if ({22587}.FoundSID == 0U)
			{
				this.{22612}.FontColor = Color.OrangeRed;
				return;
			}
			this.{22612}.FontColor = Color.LightGreen;
			new {17558}(new {17549}({22587}.FoundSID, {22587}.QueryName, Array.Empty<{17549}.OptionalAction>()));
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x000B2AB6 File Offset: 0x000B0CB6
		protected override void UserUpdate(ref FrameTime {22588})
		{
			base.UserUpdate(ref {22588});
			if (this.{22610})
			{
				this.{22586}();
				this.{22610} = false;
			}
			if (this.{22611} != null)
			{
				this.{22611}.IsEnter = true;
			}
			this.{22611} = null;
		}

		// Token: 0x06001536 RID: 5430 RVA: 0x000B2AEF File Offset: 0x000B0CEF
		private void {22589}()
		{
			this.{22610} = true;
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x000B2B56 File Offset: 0x000B0D56
		[CompilerGenerated]
		private void {22590}()
		{
			{22585}.CurrentInstance = null;
			Session.FriendsChanged -= this.{22589};
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x000B2B70 File Offset: 0x000B0D70
		[CompilerGenerated]
		private void {22591}(ListItemViewControl {22592})
		{
			BlocksStackFormControl blocksStackFormControl = new BlocksStackFormControl(Vector2.Zero, 2, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Color colorOnlinePl = Color.SoftLime;
			Color colorOfflinePl = Color.White * 0.5f;
			if (Session.FriendsRequests.Size > 0)
			{
				blocksStackFormControl.AddItem(new UiControl[]
				{
					new LabelButton(Vector2.Zero, (this.{22605} ? "V" : ">") + Local.incomingFriendRequests, Fonts.Philosopher_14Bold, Color.Gray, Color.White, new Action<ClickUiEventArgs>(this.{22593}))
				});
				blocksStackFormControl.NewLine();
				if (this.{22605})
				{
					using (IEnumerator<FriendRequest> enumerator = ((IEnumerable<FriendRequest>)Session.FriendsRequests).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							{22585}.<>c__DisplayClass13_1 CS$<>8__locals2 = new {22585}.<>c__DisplayClass13_1();
							CS$<>8__locals2.item = enumerator.Current;
							Button button = new Button(Vector2.Zero, {22585}.c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
							button.SetText(CS$<>8__locals2.item.CachedName, Fonts.Arial_12, colorOnlinePl, false);
							blocksStackFormControl.AddItem(new UiControl[]
							{
								button
							});
							button.AddChildPos(new Button(Vector2.Zero, new Rectangle(992, 107, 36, 36), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {22618})
							{
								Tlist<FriendRequest> friendsRequests = Session.FriendsRequests;
								Predicate<FriendRequest> predicate;
								if ((predicate = CS$<>8__locals2.<>9__7) == null)
								{
									predicate = (CS$<>8__locals2.<>9__7 = ((FriendRequest {22617}) => {22617}.FromSID == CS$<>8__locals2.item.FromSID));
								}
								friendsRequests.RemoveAll(predicate);
								button.Opacity = 0.3f;
								button.AllowMouseInput = false;
							}), PositionAlignment.RightDown, PositionAlignment.Center, 5f);
							Button button5 = new Button(Vector2.Zero, new Rectangle(1029, 107, 36, 36), PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExClick(delegate(ClickUiEventArgs {22619})
							{
								if (Session.Account.Friends.Size >= 300)
								{
									new {17312}(Local.friends_overcrowded);
									return;
								}
								Global.Network.Send(new OnFriendRequest(CS$<>8__locals2.item.FromSID, true));
								button.Opacity = 0.3f;
								button.AllowMouseInput = false;
							});
							button.AddChildPos(button5, PositionAlignment.LeftUp, PositionAlignment.Center, 5f);
							if (Session.Account.Friends.Size >= 300)
							{
								button5.Opacity = 0.5f;
							}
							if (CS$<>8__locals2.item.Flags == 2)
							{
								button.AddChildPos(new Label(Vector2.Zero, Fonts.Arial_9, colorOnlinePl * 0.5f, Local.refferalCode, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.RightDown, -1f);
							}
						}
					}
				}
			}
			blocksStackFormControl.NewLine();
			blocksStackFormControl.AddItem(new UiControl[]
			{
				new LabelButton(Vector2.Zero, (this.{22606} ? "V" : ">") + Local.FirendsWindow_1, Fonts.Philosopher_14Bold, Color.Gray, Color.White, new Action<ClickUiEventArgs>(this.{22595}))
			});
			blocksStackFormControl.NewLine();
			if (this.{22606})
			{
				int num = 0;
				foreach (Ship ship in Global.Game.WorldInstance.EnumerateShips(false, true, false))
				{
					ShipOtherPlayer shipOtherPlayer = (ShipOtherPlayer)ship;
					bool flag;
					string name = ((IClientShip)shipOtherPlayer).GetClient.GetName(out flag);
					num++;
					Button button2 = new Button(Vector2.Zero, {22585}.c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					button2.SetText(name, Fonts.Arial_12, colorOnlinePl, false);
					blocksStackFormControl.AddItem(new UiControl[]
					{
						button2
					});
					uint accSid = shipOtherPlayer.AccountSID;
					if (!flag)
					{
						button2.EvClick += delegate(ClickUiEventArgs {22620})
						{
							new {17558}(new {17549}(accSid, name, Array.Empty<{17549}.OptionalAction>()));
						};
						button2.EvRightButtonClick += delegate(ClickUiEventArgs {22621})
						{
							new {17558}(new {17549}(accSid, name, Array.Empty<{17549}.OptionalAction>()));
						};
					}
					else
					{
						button2.Opacity = 0.44f;
					}
				}
				if (num == 0)
				{
					Button button3 = new Button(Vector2.Zero, {22585}.c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						BasicColor = Color.Gray
					};
					button3.SetText(Local.FirendsWindow_2, Fonts.Arial_12, Color.Gray * 0.7f, false);
					blocksStackFormControl.AddItem(new UiControl[]
					{
						button3
					});
				}
			}
			string text = (this.{22607} ? "V" : ">") + Local.FirendsWindow_3;
			if (Session.Account.Friends.Size >= 300)
			{
				string str = text;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 2);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(Session.Account.Friends.Size);
				defaultInterpolatedStringHandler.AppendLiteral(" / ");
				defaultInterpolatedStringHandler.AppendFormatted<int>(300);
				text = str + defaultInterpolatedStringHandler.ToStringAndClear();
			}
			blocksStackFormControl.NewLine();
			blocksStackFormControl.AddItem(new UiControl[]
			{
				new LabelButton(Vector2.Zero, text, Fonts.Philosopher_14Bold, Color.Gray, Color.White, new Action<ClickUiEventArgs>(this.{22597}))
			});
			blocksStackFormControl.NewLine();
			if (this.{22607})
			{
				TextBox textBox = new TextBox(Vector2.Zero, {22585}.c_item, Fonts.Philosopher_14, Color.Wheat, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = Color.Gray
				};
				textBox.DefaultText = Local.search;
				textBox.Text = ((this.{22609} == null) ? "" : this.{22609});
				blocksStackFormControl.AddItem(new UiControl[]
				{
					textBox
				});
				blocksStackFormControl.NewLine();
				if (!string.IsNullOrEmpty(this.{22609}))
				{
					this.{22611} = textBox;
				}
				textBox.EvTextChanged += delegate(string {22616})
				{
					this.{22609} = {22616};
					this.{22610} = true;
					textBox.AllowMouseInput = false;
				};
				Tlist<FriendCacheItem> tlist = Session.FriendsCache.Clone();
				tlist.SortTop((FriendCacheItem {22613}) => ({22613}.IsOnline > false) ? 1 : 0);
				for (int i = 0; i < tlist.Size; i++)
				{
					FriendCacheItem item = tlist.Array[i];
					if (string.IsNullOrEmpty(this.{22609}) || item.Name.IndexOf(this.{22609}, 0, StringComparison.InvariantCultureIgnoreCase) != -1)
					{
						Button button = new Button(Vector2.Zero, {22585}.c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						button.SetText(item.Name, Fonts.Arial_12, item.IsOnline ? colorOnlinePl : colorOfflinePl, true);
						button.Tag = item;
						button.UpdateComplete += delegate(UiControl {22622})
						{
							button.TextColor = (item.IsOnline ? colorOnlinePl : colorOfflinePl);
						};
						button.EvRightButtonClick += delegate(ClickUiEventArgs {22614})
						{
							FriendCacheItem friendCacheItem = (FriendCacheItem){22614}.Sender.Tag;
							new {17558}(new {17549}(friendCacheItem.AccountSID, friendCacheItem.Name, Array.Empty<{17549}.OptionalAction>()));
						};
						button.EvClick += delegate(ClickUiEventArgs {22615})
						{
							FriendCacheItem friendCacheItem = (FriendCacheItem){22615}.Sender.Tag;
							new {17558}(new {17549}(friendCacheItem.AccountSID, friendCacheItem.Name, Array.Empty<{17549}.OptionalAction>()));
						};
						blocksStackFormControl.AddItem(new UiControl[]
						{
							button
						});
					}
				}
			}
			blocksStackFormControl.NewLine();
			blocksStackFormControl.AddItem(new UiControl[]
			{
				new LabelButton(Vector2.Zero, (this.{22608} ? "V" : ">") + Local.FirendsWindow_5, Fonts.Philosopher_14Bold, Color.Gray, Color.White, new Action<ClickUiEventArgs>(this.{22599}))
			});
			blocksStackFormControl.AddItem(new UiControl[]
			{
				new Form(Marker.OneUnit, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			if (this.{22608})
			{
				this.{22612} = new TextBox(Vector2.Zero, {22585}.c_item, Fonts.Philosopher_14, Color.White * 0.9f, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
				{
					BasicColor = Color.Gray
				};
				this.{22612}.DefaultText = Local.FirendsWindow_4;
				this.{22612}.EvTextChanged += this.{22601};
				blocksStackFormControl.AddItem(new UiControl[]
				{
					this.{22612}
				});
				Button button4 = new Button(Vector2.Zero, {22585}.c_item, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				button4.SetText(Local.find_out + "...", Fonts.Arial_12, Color.White * 0.9f, false);
				blocksStackFormControl.AddItem(new UiControl[]
				{
					button4
				});
				button4.EvClick += this.{22603};
			}
			{22592}.AddItem(new UiControl[]
			{
				blocksStackFormControl
			});
		}

		// Token: 0x0600153A RID: 5434 RVA: 0x000B3424 File Offset: 0x000B1624
		[CompilerGenerated]
		private void {22593}(ClickUiEventArgs {22594})
		{
			this.{22605} = !this.{22605};
			this.{22610} = true;
			{22594}.Sender.AllowMouseInput = false;
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x000B3448 File Offset: 0x000B1648
		[CompilerGenerated]
		private void {22595}(ClickUiEventArgs {22596})
		{
			this.{22606} = !this.{22606};
			this.{22610} = true;
			{22596}.Sender.AllowMouseInput = false;
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000B346C File Offset: 0x000B166C
		[CompilerGenerated]
		private void {22597}(ClickUiEventArgs {22598})
		{
			this.{22607} = !this.{22607};
			this.{22610} = true;
			{22598}.Sender.AllowMouseInput = false;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x000B3490 File Offset: 0x000B1690
		[CompilerGenerated]
		private void {22599}(ClickUiEventArgs {22600})
		{
			this.{22608} = !this.{22608};
			this.{22610} = true;
			{22600}.Sender.AllowMouseInput = false;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x000B34B4 File Offset: 0x000B16B4
		[CompilerGenerated]
		private void {22601}(string {22602})
		{
			this.{22612}.FontColor = Color.White * 0.9f;
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x000B34D0 File Offset: 0x000B16D0
		[CompilerGenerated]
		private void {22603}(ClickUiEventArgs {22604})
		{
			if (this.{22612}.Text.Length > 0)
			{
				Global.Network.Send(new OnGlobalPlayerSearch(this.{22612}.Text, 0U));
			}
		}

		// Token: 0x04001320 RID: 4896
		public static {22585} CurrentInstance;

		// Token: 0x04001321 RID: 4897
		public static readonly Rectangle c_itemDropdown = new Rectangle(1315, 697, 320, 36);

		// Token: 0x04001322 RID: 4898
		public static readonly Rectangle c_item = new Rectangle(994, 660, 320, 36);

		// Token: 0x04001323 RID: 4899
		public static readonly Rectangle c_itemInput = new Rectangle(994, 660, 320, 36);

		// Token: 0x04001324 RID: 4900
		private bool {22605} = true;

		// Token: 0x04001325 RID: 4901
		private bool {22606};

		// Token: 0x04001326 RID: 4902
		private bool {22607} = true;

		// Token: 0x04001327 RID: 4903
		private bool {22608} = true;

		// Token: 0x04001328 RID: 4904
		private string {22609};

		// Token: 0x04001329 RID: 4905
		private bool {22610};

		// Token: 0x0400132A RID: 4906
		private TextBox {22611};

		// Token: 0x0400132B RID: 4907
		private TextBox {22612};
	}
}
