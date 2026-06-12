using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;
using World_Of_Sea_Battle.Graphics;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000235 RID: 565
	internal sealed class {19994} : CustomUi
	{
		// Token: 0x06000CC5 RID: 3269 RVA: 0x000695BB File Offset: 0x000677BB
		public static void CloseAllShip()
		{
			{19994}.itemsShip.Clear();
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000695C7 File Offset: 0x000677C7
		public static string Logbook(string {19995}, LBFlags {19996} = LBFlags.L0)
		{
			Global.Settings.Logbook.Write({19995}, {19996});
			return {19995};
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000695DB File Offset: 0x000677DB
		public static string Me({19988} {19997}, string {19998}, params object[] {19999})
		{
			{19994}.Write({19997}, {19998}, null, {19999});
			return {19998};
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000695E7 File Offset: 0x000677E7
		public static void MeAndLogbook({19988} {20000}, string {20001}, LBFlags? {20002} = null)
		{
			{19994}.MeAndLogbook({20000}, {20001}, {20001}, {20002});
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000695F4 File Offset: 0x000677F4
		public static void MeAndLogbook({19988} {20003}, string {20004}, string {20005}, LBFlags? {20006} = null)
		{
			if (!string.IsNullOrEmpty({20004}))
			{
				Global.Settings.Logbook.Write({20004}, {20006} ?? (({20003} == {19988}.Big_Red) ? LBFlags.L2 : (({20003} == {19988}.Big_Yellow || {20003} == {19988}.Big_Monets) ? LBFlags.L1 : LBFlags.L0)));
			}
			{19994}.Write({20003}, {20005}, null, Array.Empty<object>());
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x00069650 File Offset: 0x00067850
		public static {19994} CurrentInstance
		{
			get
			{
				return {19994}.currentInstance;
			}
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x00069658 File Offset: 0x00067858
		public static bool HasBlockingKey(Keys {20007})
		{
			return {19994}.itemsNotif.Any(delegate({19994}.ItemNotif {20057})
			{
				Keys? keys = {20057}.AutoCloseKey;
				Keys key = {20007};
				if (!(keys.GetValueOrDefault() == key & keys != null))
				{
					keys = {20057}.AutoClickKey;
					key = {20007};
					return keys.GetValueOrDefault() == key & keys != null;
				}
				return true;
			});
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x00069688 File Offset: 0x00067888
		private static void Write({19988} {20008}, string {20009}, Action {20010} = null, params object[] {20011})
		{
			bool flag = {20008} - {19988}.Big_Gray <= 5;
			if (flag)
			{
				if ({20008} == {19988}.Big_Fraction && ({20011}.Count<object>() == 0 || !({20011}.ElementAt(0) is {19989})))
				{
					throw new ArgumentException("context should be FractionRepChangeInfo");
				}
				if ({19994}.currentInstance == null)
				{
					return;
				}
				int num = ({20008} == {19988}.Big_Fraction) ? 6500 : (({20008} == {19988}.Big_TEST) ? (Global.Player.MapInfo.IsEducationMap ? 100000000 : 50000) : 9000);
				if ({19994}.itemsNotif.Size > 0 && {20009} == {19994}.prevNotif)
				{
					return;
				}
				{19994}.prevNotif = {20009};
				{19994}.ItemNotif itemNotif = new {19994}.ItemNotif({20008}, {20009}, (float)num, false, {20011});
				itemNotif.ClickOrTimeout += {20010};
				{19994}.itemsNotif.Insert(0, itemNotif);
				{19994}.currentInstance.AddChild(itemNotif);
				{19994}.currentInstance.MoveToFrontLevel();
				{19994}.SoundEffect({20008});
				return;
			}
			else
			{
				flag = ({20008} - {19988}.Info <= 1);
				if (flag && {19994}.itemsShip.Size > 0 && {19994}.itemsShip.Array[{19994}.itemsShip.Size - 1].InitText == {20009})
				{
					return;
				}
				Texture2D {20053} = null;
				ValueTuple<ResourceInfo, int> parsedData = new ValueTuple<ResourceInfo, int>(null, 0);
				if ({20008} == {19988}.GiveResources && {20009}.Contains('+'))
				{
					int num2 = 0;
					foreach (ResourceInfo resourceInfo in ((IEnumerable<ResourceInfo>)Gameplay.ItemsInfo))
					{
						if ({20009}.Contains(resourceInfo.Name) && resourceInfo.Name.Length > num2)
						{
							{20053} = resourceInfo.IconTexture;
							if (resourceInfo.MediumCost.Value > 1000 || resourceInfo.ID == 37 || resourceInfo.ID == 38 || resourceInfo.ID == 15)
							{
								{20008} = {19988}.GiveRareResources;
							}
							int item;
							if ({20009}.Contains('+') && int.TryParse({20009}.Split('+', StringSplitOptions.None)[1], out item))
							{
								parsedData = new ValueTuple<ResourceInfo, int>(resourceInfo, item);
							}
							num2 = resourceInfo.Name.Length;
						}
					}
				}
				if ({19994}.itemsShip.Size == 0 && {19994}.currentInstance != null)
				{
					{19994}.currentInstance.{20019} = 0f;
				}
				if (parsedData.Item1 == null || !{19994}.itemsShip.Any(({19994}.ItemShip {20058}) => {20058}.TryAppend(parsedData)))
				{
					Tlist<{19994}.ItemShip> tlist = {19994}.itemsShip;
					{19994}.ItemShip itemShip = new {19994}.ItemShip(({20008} == {19988}.Big_TEST) ? default(Rectangle) : {19994}.pathes[{20008}], {20009}, {20008}, {20053})
					{
						ParsedData = parsedData
					};
					tlist.Add(itemShip);
				}
				{19994} {19994} = {19994}.currentInstance;
				if ({19994} == null)
				{
					return;
				}
				{19994}.MoveToFrontLevel();
				return;
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x00069954 File Offset: 0x00067B54
		private static void SoundEffect({19988} {20012})
		{
			if ({19994}.soundTimer.Elapsed.TotalSeconds > 1.0)
			{
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.UIMessage, 0.03f, ({20012} == {19988}.Big_TEST) ? 0.7f : 1f);
			}
			{19994}.soundTimer.Restart();
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000699B0 File Offset: 0x00067BB0
		public static void Invite(string {20013}, Action {20014}, Keys? {20015}, Keys? {20016}, float {20017} = -1f)
		{
			if ({19994}.currentInstance == null)
			{
				return;
			}
			if ({20015} != null)
			{
				if ({20016} != null)
				{
					{20013} = string.Concat(new string[]
					{
						{20013},
						" [",
						{20015}.Value.GetKeyName(),
						"] / [",
						{20016}.Value.GetKeyName(),
						"]"
					});
				}
				else
				{
					{20013} = {20013} + " [" + {20015}.Value.GetKeyName() + "]";
				}
			}
			{19994}.ItemNotif itemNotif = new {19994}.ItemNotif({19988}.Big_Gray, {20013}, ({20017} == -1f) ? 10000000f : {20017}, true, new object[1]);
			{19994}.currentInstance.AddChild(itemNotif);
			{19994}.itemsNotif.Insert(0, itemNotif);
			itemNotif.Click += {20014};
			itemNotif.AutoClickKey = {20015};
			itemNotif.AutoCloseKey = {20016};
			{19994}.SoundEffect({19988}.Info);
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x00069A94 File Offset: 0x00067C94
		private static Dictionary<{19988}, Rectangle> d()
		{
			return new Dictionary<{19988}, Rectangle>
			{
				{
					{19988}.Gold,
					new Rectangle(2030, 0, 164, 28)
				},
				{
					{19988}.GiveScrolls,
					new Rectangle(2030, 29, 164, 28)
				},
				{
					{19988}.GiveResources,
					new Rectangle(2030, 58, 164, 28)
				},
				{
					{19988}.GiveRareResources,
					new Rectangle(2030, 87, 164, 28)
				},
				{
					{19988}.GiveDoubloons,
					new Rectangle(2030, 116, 164, 28)
				},
				{
					{19988}.Minus,
					new Rectangle(2195, 0, 164, 28)
				},
				{
					{19988}.RedFlag,
					new Rectangle(2360, 0, 164, 28)
				},
				{
					{19988}.Okay,
					new Rectangle(2195, 29, 164, 28)
				},
				{
					{19988}.GiveCrew,
					new Rectangle(1560, 34, 164, 28)
				},
				{
					{19988}.Info,
					new Rectangle(1495, 79, 164, 28)
				},
				{
					{19988}.InfoRed,
					new Rectangle(2360, 29, 164, 28)
				}
			};
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x00069BC4 File Offset: 0x00067DC4
		public {19994}() : base(false)
		{
			this.AnimatedFocus = false;
			{19994}.currentInstance = this;
			base.EvRemoveFromContainer += delegate()
			{
				{19994}.currentInstance = null;
			};
			foreach ({19994}.ItemNotif {13204} in ((IEnumerable<{19994}.ItemNotif>){19994}.itemsNotif))
			{
				base.AddChild({13204});
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x00069C54 File Offset: 0x00067E54
		protected override void UserUpdate(ref FrameTime {20018})
		{
			float num = 10f / (9f + (float){19994}.itemsShip.Size);
			for (int i = 0; i < {19994}.itemsShip.Size; i++)
			{
				if ({19994}.itemsShip.Array[i].Update(ref {20018}, ({19994}.itemsShip.Array[i].IconPath == {19994}.pathes[{19988}.InfoRed]) ? (num * 0.66f) : ((Global.Game.GetCurrentSceneName != GameSceneName.Port || {19994}.itemsShip.Array[i].IconPath == {19994}.pathes[{19988}.Info]) ? num : (num * 2f))))
				{
					{19994}.itemsShip.RemoveAt(i);
					i--;
				}
			}
			bool flag = false;
			if ((Global.Game.GetCurrentSceneName == GameSceneName.Game) ? (!GameScene.GameHasInputFocus) : TextBox.IsThereInput)
			{
				flag = false;
			}
			Vector2 value = (Global.Game.GetCurrentSceneName == GameSceneName.Port) ? (({22478}.CurrentInstance == null) ? new Vector2((float)Engine.GS.UIArea.Width, (float)(Engine.GS.UIArea.Height - 110)) : new Vector2((float)Engine.GS.UIArea.Width, {22478}.CurrentInstance.Pos.XY.Y - 10f)) : (({22478}.CurrentInstance == null) ? new Vector2(0f, (float)(Engine.GS.UIArea.Height - 110)) : new Vector2(0f, {22478}.CurrentInstance.Pos.XY.Y - 10f));
			value.Y += 22f;
			for (int j = 0; j < {19994}.itemsNotif.Size; j++)
			{
				if ({19994}.itemsNotif.Array[j].Update(ref {20018}, ref flag))
				{
					{19994}.itemsNotif.Array[j].RemoveFromContainer();
					{19994}.itemsNotif.RemoveAt(j);
					j--;
				}
				else
				{
					value.Y -= {19994}.itemsNotif.Array[j].Pos.WH.Y;
					UiControl uiControl = {19994}.itemsNotif.Array[j];
					Marker pos = {19994}.itemsNotif.Array[j].Pos;
					Vector2 vector = value + new Vector2((float)(({19994}.itemsNotif.Array[j].Type == {19988}.Big_TEST) ? 0 : 22), 0f);
					uiControl.Pos = pos.SetXY(vector);
					if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
					{
						UiControl uiControl2 = {19994}.itemsNotif.Array[j];
						pos = {19994}.itemsNotif.Array[j].Pos;
						uiControl2.Pos = pos.Offset(-{19994}.itemsNotif.Array[j].Pos.WH.X - 30f, 0f);
					}
				}
			}
			this.{20019} += {20018}.msElapsed;
			base.IsVisible = (Global.Render.UiMode == InterfaceMode.Default);
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x00069F76 File Offset: 0x00068176
		protected override void UserBackRender()
		{
			this.{20020} = (AtlasGameGui.Texture.Tex != Engine.GS.CurrentTexture);
			if (this.{20020})
			{
				Engine.GS.SetTexture(AtlasGameGui.Texture.Tex);
			}
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x00069FB4 File Offset: 0x000681B4
		protected override void UserFrontRender()
		{
			if (Global.Player == null)
			{
				return;
			}
			Vector2 vector;
			if (Global.Game.GetCurrentSceneName == GameSceneName.Port && !Global.Game.ScenePort.IsMainPage)
			{
				vector = Engine.GS.MouseToUI - new Vector2(100f, (float)({19994}.itemsShip.Size * 30));
			}
			else
			{
				Vector2 vector2 = Engine.GS.UIArea.HalfWidthHeightInt();
				Vector3 position3D = Global.Player.Position3D;
				Vector3 position = Engine.GS.Camera.Position;
				float num;
				Vector3.Distance(ref position3D, ref position, out num);
				num = ((num < 5f) ? (num / 5f) : 1f);
				num *= num;
				vector = Engine.GS.Camera.GetProjection(position3D + new Vector3(0f, 2f, 0f));
				Vector2.Lerp(ref vector2, ref vector, num, out vector);
				vector.Y = Math.Min(vector.Y, (float)(Engine.GS.UIArea.Height - 160));
			}
			if (this.{20019} < 2000f)
			{
				float num2 = this.{20019} / 2000f;
				num2 = 4f * num2 * (1f - num2);
				float scaleFactor = 6.25f;
				if (Global.Game.GetCurrentSceneName == GameSceneName.Port)
				{
					num2 *= 0.4f;
				}
				Device gs = Engine.GS;
				Vector2 vector3 = vector + new Vector2(30f, 10f) - {19994}.c_shipItemsLight.WidthHeight() * scaleFactor * 0.5f;
				Vector2 vector4 = {19994}.c_shipItemsLight.WidthHeight() * scaleFactor;
				Rectangle rectangle = new Marker(ref vector3, ref vector4).ToRect();
				Color color = Color.White * num2;
				gs.Draw({19994}.c_shipItemsLight, rectangle, color);
			}
			Engine.GS.SetFont(Fonts.Arial_12);
			int num3 = 0;
			for (int i = 0; i < {19994}.itemsShip.Size; i++)
			{
				{19994}.ItemShip itemShip = {19994}.itemsShip.Array[i];
				int num4 = (int)((float)itemShip.IconPathLeft.Height * (itemShip.TwoLines ? 1.7f : 1f));
				Vector2 vector5 = vector;
				vector5.Y += (float)num3;
				vector5.X = (float)((int)vector5.X);
				vector5.Y = (float)((int)vector5.Y);
				num3 += num4 + 2;
				Color color2 = (itemShip.ttl < 1000f) ? (itemShip.TextColor * (itemShip.ttl / 1000f)) : itemShip.TextColor;
				Engine.GS.Draw(itemShip.IconPathLeft, vector5, color2);
				Device gs2 = Engine.GS;
				{19994}.ItemShip itemShip2 = itemShip;
				Rectangle rectangle = new Marker(vector5.X + (float)itemShip.IconPathLeft.Width, vector5.Y, itemShip.TextSize.X + 15f, (float)num4).ToRect();
				gs2.Draw(itemShip2.IconPathRight, rectangle, color2);
				if (itemShip.MiniIcon != null)
				{
					Device gs3 = Engine.GS;
					Texture2D miniIcon = itemShip.MiniIcon;
					rectangle = itemShip.MiniIcon.Bounds;
					Rectangle rectangle2 = new Marker(5f, 2f, 24f, 24f).Offset(vector5).ToRect();
					gs3.DrawCustomTexture(miniIcon, rectangle, rectangle2, color2);
				}
				Device gs4 = Engine.GS;
				string textDisplay = itemShip.TextDisplay;
				Vector2 vector3 = vector5 + {19994}.p_text;
				gs4.DrawString(textDisplay, vector3, color2);
			}
			Vector2 vector6 = (Global.Game.GetCurrentSceneName == GameSceneName.Port) ? (({22001}.CurrentInstance == null) ? new Vector2((float)(Engine.GS.UIArea.Width - {22478}.DefaultWidth - 40), (float)Engine.GS.UIArea.Height) : new Vector2((float)(Engine.GS.UIArea.Width - {22478}.DefaultWidth - 40), {22001}.CurrentInstance.Pos.XY.Y - 20f)) : (({22478}.CurrentInstance == null) ? new Vector2(0f, (float)(Engine.GS.UIArea.Height - 100)) : new Vector2(0f, {22478}.CurrentInstance.Pos.XY.Y - 10f));
			vector6.Y -= 22f;
			if (this.{20020})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0006A442 File Offset: 0x00068642
		public static void SessionFinished()
		{
			{19994}.itemsNotif.Clear();
			{19994}.itemsShip.Clear();
		}

		// Token: 0x04000BF3 RID: 3059
		private static {19994} currentInstance;

		// Token: 0x04000BF4 RID: 3060
		private static string prevNotif;

		// Token: 0x04000BF5 RID: 3061
		private static Stopwatch soundTimer = Stopwatch.StartNew();

		// Token: 0x04000BF6 RID: 3062
		private static readonly Vector2 p_text = new Vector2(35f, 5f);

		// Token: 0x04000BF7 RID: 3063
		private static readonly Rectangle c_shipItemsLight = new Rectangle(2030, 145, 48, 48);

		// Token: 0x04000BF8 RID: 3064
		private static readonly Rectangle c_notif_yellow = new Rectangle(1717, 353, 41, 43);

		// Token: 0x04000BF9 RID: 3065
		private static readonly Rectangle c_notif_monets = new Rectangle(1843, 353, 41, 43);

		// Token: 0x04000BFA RID: 3066
		private static readonly Rectangle c_notif_gray = new Rectangle(1759, 353, 41, 43);

		// Token: 0x04000BFB RID: 3067
		private static readonly Rectangle c_notif_red = new Rectangle(1801, 353, 41, 43);

		// Token: 0x04000BFC RID: 3068
		private static readonly Rectangle c_textBack = new Rectangle(1717, 397, 215, 28);

		// Token: 0x04000BFD RID: 3069
		private static readonly Rectangle c_notif_yellow_back = new Rectangle(1717, 426, 43, 65);

		// Token: 0x04000BFE RID: 3070
		private static readonly Rectangle c_notif_gray_back = new Rectangle(1761, 426, 43, 65);

		// Token: 0x04000BFF RID: 3071
		private static readonly Rectangle c_notif_red_back = new Rectangle(1805, 426, 43, 65);

		// Token: 0x04000C00 RID: 3072
		private static Dictionary<{19988}, Rectangle> pathes = {19994}.d();

		// Token: 0x04000C01 RID: 3073
		private static Tlist<{19994}.ItemNotif> itemsNotif = new Tlist<{19994}.ItemNotif>();

		// Token: 0x04000C02 RID: 3074
		private static Tlist<{19994}.ItemShip> itemsShip = new Tlist<{19994}.ItemShip>();

		// Token: 0x04000C03 RID: 3075
		private float {20019} = 10000f;

		// Token: 0x04000C04 RID: 3076
		private bool {20020};

		// Token: 0x02000236 RID: 566
		private class ItemNotif : CustomUi
		{
			// Token: 0x14000009 RID: 9
			// (add) Token: 0x06000CD8 RID: 3288 RVA: 0x0006A57C File Offset: 0x0006877C
			// (remove) Token: 0x06000CD9 RID: 3289 RVA: 0x0006A5B4 File Offset: 0x000687B4
			public event Action Click
			{
				[CompilerGenerated]
				add
				{
					Action action = this.{20044};
					Action action2;
					do
					{
						action2 = action;
						Action value2 = (Action)Delegate.Combine(action2, value);
						action = Interlocked.CompareExchange<Action>(ref this.{20044}, value2, action2);
					}
					while (action != action2);
				}
				[CompilerGenerated]
				remove
				{
					Action action = this.{20044};
					Action action2;
					do
					{
						action2 = action;
						Action value2 = (Action)Delegate.Remove(action2, value);
						action = Interlocked.CompareExchange<Action>(ref this.{20044}, value2, action2);
					}
					while (action != action2);
				}
			}

			// Token: 0x1400000A RID: 10
			// (add) Token: 0x06000CDA RID: 3290 RVA: 0x0006A5EC File Offset: 0x000687EC
			// (remove) Token: 0x06000CDB RID: 3291 RVA: 0x0006A624 File Offset: 0x00068824
			public event Action ClickOrTimeout
			{
				[CompilerGenerated]
				add
				{
					Action action = this.{20045};
					Action action2;
					do
					{
						action2 = action;
						Action value2 = (Action)Delegate.Combine(action2, value);
						action = Interlocked.CompareExchange<Action>(ref this.{20045}, value2, action2);
					}
					while (action != action2);
				}
				[CompilerGenerated]
				remove
				{
					Action action = this.{20045};
					Action action2;
					do
					{
						action2 = action;
						Action value2 = (Action)Delegate.Remove(action2, value);
						action = Interlocked.CompareExchange<Action>(ref this.{20045}, value2, action2);
					}
					while (action != action2);
				}
			}

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0006A65C File Offset: 0x0006885C
			private float LightAnimationEffect
			{
				get
				{
					if (this.{20043})
					{
						return 1f;
					}
					float num = this.InitialTtl - this.Ttl;
					if (num < 2000f)
					{
						float num2 = num / 2000f;
						return 4f * num2 * (1f - num2);
					}
					return 0f;
				}
			}

			// Token: 0x17000124 RID: 292
			// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0006A6AA File Offset: 0x000688AA
			public float Transparancy
			{
				get
				{
					return Math.Min(1f, Math.Min(this.Ttl / 1500f, (this.InitialTtl - this.Ttl) / 500f));
				}
			}

			// Token: 0x06000CDE RID: 3294 RVA: 0x0006A6DC File Offset: 0x000688DC
			public ItemNotif({19988} {20030}, string {20031}, float {20032}, bool {20033}, params object[] {20034}) : base(false)
			{
				base.PositionAlignment_X = PositionAlignment.LeftUp;
				base.PositionAlignment_Y = PositionAlignment.RightDown;
				base.Opacity = 0f;
				string text = {20031};
				if ({20030} == {19988}.Big_TEST)
				{
					this.AutoCloseKey = new Keys?(Keys.Enter);
				}
				else if (text.Length > 40 && !{20033})
				{
					this.HasCuttedText = true;
					text = text.Substring(0, 30) + "...";
				}
				this.Type = {20030};
				this.Text = {20031};
				this.Ttl = {20032};
				this.InitialTtl = {20032};
				this.CuttedText = text;
				this.Font = Fonts.Philosopher_14;
				this.{20043} = {20033};
				Rectangle {13194} = ({20030} == {19988}.Big_Red) ? {19994}.c_notif_red_back : (({20030} == {19988}.Big_Yellow || {20030} == {19988}.Big_Monets) ? {19994}.c_notif_yellow_back : {19994}.c_notif_gray_back);
				Rectangle {13190} = ({20030} == {19988}.Big_Fraction) ? Rectangle.Empty : (({20030} == {19988}.Big_TEST) ? Rectangle.Empty : (({20030} == {19988}.Big_Red) ? {19994}.c_notif_red : (({20030} == {19988}.Big_Yellow) ? {19994}.c_notif_yellow : (({20030} == {19988}.Big_Monets) ? {19994}.c_notif_monets : {19994}.c_notif_gray))));
				this.TextColor = (({20030} == {19988}.Big_TEST) ? Color.White : (({20030} == {19988}.Big_Red) ? new Color(255, 231, 219) : (({20030} == {19988}.Big_Yellow || {20030} == {19988}.Big_Monets) ? new Color(255, 249, 216) : new Color(195, 195, 195))));
				StackForm stackForm = new StackForm(new Vector2(0f, (float)((this.Type == {19988}.Big_TEST) ? 10 : 0)), (this.Type == {19988}.Big_Fraction) ? UiOrientation.Horizontal : UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if ({20034}.Length == 0)
				{
					{20034} = new object[1];
				}
				foreach (object obj in {20034})
				{
					if (this.Type == {19988}.Big_TEST)
					{
						if (obj is Rectangle)
						{
							Rectangle rectangle = (Rectangle)obj;
							stackForm.AddItem(new UiControl[]
							{
								new Image(new Marker(ref rectangle).Scale(Math.Min(1f, 200f / (float)rectangle.Width)), OtherTextures.Images, rectangle, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							});
						}
						else
						{
							stackForm.AddSpace(5f);
						}
					}
					if (this.Type == {19988}.Big_TEST)
					{
						stackForm.AddItem(new UiControl[]
						{
							TextBlockBuilder.CreateBlock(440f, {20031}, this.TextColor, this.Font, -1f).Create(Vector2.Zero)
						});
						stackForm.AddSpace(5f);
						stackForm.AddItem(new UiControl[]
						{
							StackForm.Stack(Vector2.Zero, UiOrientation.Horizontal, new UiControl[]
							{
								new Image(Vector2.Zero, CommonAtlas.Texture.Tex, {17312}.c_keyBlock_enter, PositionAlignment.LeftUp, PositionAlignment.LeftUp),
								new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Gray, " " + Local.close, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							})
						});
					}
					else if (this.Type == {19988}.Big_Fraction)
					{
						{19989} {19989} = ({19989})obj;
						int num = 30;
						Rectangle fractionFlagPrerender = CommonAtlas.GetFractionFlagPrerender({19989}.Fraction);
						int num2 = fractionFlagPrerender.Width * num / fractionFlagPrerender.Height;
						stackForm.AddItem(new UiControl[]
						{
							new Image(new Marker(0f, 0f, (float)num2, (float)num), CommonAtlas.Texture.Tex, fractionFlagPrerender, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
						Color color = ({19989}.Amount < 0f) ? Color.Tomato : Color.LimeGreen;
						Form form = new Form(new Marker(0f, 0f, 20f, (float)num), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						int num3 = 10;
						string {13345};
						if ({19989}.Amount < 0f)
						{
							form.AddChildPos(new Image(new Marker(0f, 0f, (float)num3, (float)num3), CommonAtlas.Texture.Tex, new Rectangle(219, 103, 48, 14), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								BasicColor = color
							}, PositionAlignment.Center, PositionAlignment.Center, 0f);
							string text2 = {19989}.Amount.ToString("0.##");
							{13345} = text2.Substring(1, text2.Length - 1);
						}
						else
						{
							form.AddChildPos(new Image(new Marker(0f, 0f, (float)num3, (float)num3), CommonAtlas.Texture.Tex, new Rectangle(268, 103, 48, 14), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
							{
								BasicColor = color
							}, PositionAlignment.Center, PositionAlignment.Center, 0f);
							{13345} = {19989}.Amount.ToString("0.##");
						}
						stackForm.AddItem(new UiControl[]
						{
							form
						});
						Label label = new Label(Vector2.Zero, this.Font, color, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						Form form2 = new Form(new Marker(0f, -4f, label.Pos.WH.X, (float)num), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
						form2.AddChild(label);
						stackForm.AddItem(new UiControl[]
						{
							form2
						});
					}
					else
					{
						stackForm.AddItem(new UiControl[]
						{
							new Label(Vector2.Zero, this.Font, this.TextColor, this.CuttedText, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						});
					}
					stackForm.AddSpace(10f);
					base.Pos = ((this.Type == {19988}.Big_TEST) ? stackForm.Pos.Border(15f, 20f) : ((this.Type == {19988}.Big_Fraction) ? stackForm.Pos.Border(10f, 5f) : stackForm.Pos.Border(15f, 5f)));
					this.AnimatedFocus = false;
					this.TexturePath = ((this.Type == {19988}.Big_TEST) ? new Rectangle(1143, 1879, 262, 97) : {19994}.c_textBack);
					this.BasicColor = Color.White;
					base.AddChild(stackForm);
					base.AddChildPos(new Form(Vector2.Zero, {13190}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					}, PositionAlignment.LeftUp, PositionAlignment.Center, -25f);
					Form form3 = new Form(base.Pos, {13194}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
					{
						AnimatedFocus = false
					};
					base.AddChild(form3);
					form3.UpdateComplete += this.{20038};
				}
				base.EvClick += this.{20040};
			}

			// Token: 0x06000CDF RID: 3295 RVA: 0x0006AD24 File Offset: 0x00068F24
			public bool Update(ref FrameTime {20035}, ref bool {20036})
			{
				if (!{20036} && !this.{20042})
				{
					if (this.AutoClickKey != null && InputHelper.IsClick(this.AutoClickKey.Value))
					{
						this.OnClick();
						{20036} = true;
					}
					if (this.AutoCloseKey != null && InputHelper.IsClick(this.AutoCloseKey.Value))
					{
						this.{20042} = true;
						{20036} = true;
						this.Close();
					}
				}
				base.Opacity = ((this.Type == {19988}.Big_TEST && {19779}.CurrentInstance != null) ? 0f : this.Transparancy);
				if (!Global.Game.IsActive && !this.{20043})
				{
					return false;
				}
				if (this.{20042})
				{
					{20035}.EvaluteTimerMs2(ref this.Ttl);
					{20035}.EvaluteTimerMs2(ref this.Ttl);
				}
				{20035}.EvaluteTimerMs2(ref this.Ttl);
				if (this.Ttl == 0f)
				{
					Action action = this.{20045};
					if (action != null)
					{
						action();
					}
				}
				return this.Ttl == 0f;
			}

			// Token: 0x06000CE0 RID: 3296 RVA: 0x0006AE28 File Offset: 0x00069028
			internal void Close()
			{
				if (this.Ttl > 1500f)
				{
					this.Ttl = 1500f;
				}
			}

			// Token: 0x06000CE1 RID: 3297 RVA: 0x0006AE44 File Offset: 0x00069044
			public void OnClick()
			{
				if (this.{20042})
				{
					return;
				}
				this.{20042} = true;
				if (this.HasCuttedText)
				{
					new {17312}(this.Text);
				}
				this.Ttl = 1500f;
				this.HasCuttedText = false;
				Action action = this.{20044};
				if (action == null)
				{
					return;
				}
				action();
			}

			// Token: 0x06000CE2 RID: 3298 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserBackRender()
			{
			}

			// Token: 0x06000CE3 RID: 3299 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserFrontRender()
			{
			}

			// Token: 0x06000CE4 RID: 3300 RVA: 0x00003100 File Offset: 0x00001300
			protected override void UserUpdate(ref FrameTime {20037})
			{
			}

			// Token: 0x06000CE5 RID: 3301 RVA: 0x0006AE97 File Offset: 0x00069097
			[CompilerGenerated]
			private void {20038}(UiControl {20039})
			{
				{20039}.Opacity = this.LightAnimationEffect;
			}

			// Token: 0x06000CE6 RID: 3302 RVA: 0x0006AEA5 File Offset: 0x000690A5
			[CompilerGenerated]
			private void {20040}(ClickUiEventArgs {20041})
			{
				this.OnClick();
			}

			// Token: 0x04000C05 RID: 3077
			private const float c_ghostingStart = 1500f;

			// Token: 0x04000C06 RID: 3078
			public {19988} Type;

			// Token: 0x04000C07 RID: 3079
			public string Text;

			// Token: 0x04000C08 RID: 3080
			public float Ttl;

			// Token: 0x04000C09 RID: 3081
			public float InitialTtl;

			// Token: 0x04000C0A RID: 3082
			public string CuttedText;

			// Token: 0x04000C0B RID: 3083
			public CustomSpriteFont Font;

			// Token: 0x04000C0C RID: 3084
			public bool HasCuttedText;

			// Token: 0x04000C0D RID: 3085
			public Color TextColor;

			// Token: 0x04000C0E RID: 3086
			public Keys? AutoClickKey;

			// Token: 0x04000C0F RID: 3087
			public Keys? AutoCloseKey;

			// Token: 0x04000C10 RID: 3088
			private bool {20042};

			// Token: 0x04000C11 RID: 3089
			private bool {20043};

			// Token: 0x04000C12 RID: 3090
			[CompilerGenerated]
			private Action {20044};

			// Token: 0x04000C13 RID: 3091
			[CompilerGenerated]
			private Action {20045};
		}

		// Token: 0x02000237 RID: 567
		private class ItemShip
		{
			// Token: 0x06000CE7 RID: 3303 RVA: 0x0006AEB0 File Offset: 0x000690B0
			public ItemShip(Rectangle {20050}, string {20051}, {19988} {20052}, Texture2D {20053})
			{
				this.TextColor = (({20052} == {19988}.InfoRed) ? new Color(255, 130, 120) : Color.White);
				this.InitText = {20051};
				this.IconPath = {20050};
				this.ttl = 5000f;
				this.IconPathLeft = new Rectangle({20050}.X, {20050}.Y, 33, {20050}.Height);
				this.IconPathRight = new Rectangle({20050}.X + 33, {20050}.Y, {20050}.Width - 33, {20050}.Height);
				this.MiniIcon = {20053};
				this.ParsedData = new ValueTuple<ResourceInfo, int>(null, 0);
				this.TwoLines = ({20051}.Length > 60);
				if (this.TwoLines)
				{
					int num = {20051}.IndexOf(' ', 55);
					if (num != -1)
					{
						string str = {20051}.Substring(0, num);
						string newLine = Environment.NewLine;
						string text = {20051};
						int num2 = num + 1;
						{20051} = str + newLine + text.Substring(num2, text.Length - num2);
					}
					this.ttl *= 1.5f;
				}
				this.TextDisplay = {20051};
				this.TextSize = Fonts.Arial_12.Measure({20051});
			}

			// Token: 0x06000CE8 RID: 3304 RVA: 0x0006AFD8 File Offset: 0x000691D8
			public bool TryAppend(ValueTuple<ResourceInfo, int> {20054})
			{
				if (this.ParsedData.Item1 == {20054}.Item1)
				{
					this.ParsedData.Item2 = this.ParsedData.Item2 + {20054}.Item2;
					this.TextDisplay = {20054}.Item1.Name + " +" + this.ParsedData.Item2.ToString();
					this.ttl = 5000f;
					return true;
				}
				return false;
			}

			// Token: 0x06000CE9 RID: 3305 RVA: 0x0006B046 File Offset: 0x00069246
			public bool Update(ref FrameTime {20055}, float {20056})
			{
				this.ttl -= {20055}.msElapsed * {20056};
				return this.ttl < 0f;
			}

			// Token: 0x04000C14 RID: 3092
			public const int InitTtl = 5000;

			// Token: 0x04000C15 RID: 3093
			public float ttl;

			// Token: 0x04000C16 RID: 3094
			public readonly Rectangle IconPath;

			// Token: 0x04000C17 RID: 3095
			public readonly Rectangle IconPathLeft;

			// Token: 0x04000C18 RID: 3096
			public readonly Rectangle IconPathRight;

			// Token: 0x04000C19 RID: 3097
			public readonly Vector2 TextSize;

			// Token: 0x04000C1A RID: 3098
			public readonly Texture2D MiniIcon;

			// Token: 0x04000C1B RID: 3099
			public readonly Color TextColor;

			// Token: 0x04000C1C RID: 3100
			public ValueTuple<ResourceInfo, int> ParsedData;

			// Token: 0x04000C1D RID: 3101
			public readonly bool TwoLines;

			// Token: 0x04000C1E RID: 3102
			public string TextDisplay;

			// Token: 0x04000C1F RID: 3103
			public string InitText;
		}
	}
}
