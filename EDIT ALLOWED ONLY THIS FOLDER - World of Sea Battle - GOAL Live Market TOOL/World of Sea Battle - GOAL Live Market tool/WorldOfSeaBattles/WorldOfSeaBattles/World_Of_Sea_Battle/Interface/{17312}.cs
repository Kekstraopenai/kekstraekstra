using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Common;
using Common.Account;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Scenes;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000093 RID: 147
	internal sealed class {17312} : {17068}
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x00020B3F File Offset: 0x0001ED3F
		public static void TryCloseEducation()
		{
			if ({17312}.CurrentInstance != null && {17312}.CurrentInstance.{17421})
			{
				{17312}.CurrentInstance.BlockAndClose();
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x00020B5E File Offset: 0x0001ED5E
		private static Color TextColor
		{
			get
			{
				return new Color(170, 170, 170);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00020B74 File Offset: 0x0001ED74
		private float spaceKeyAnimationTime
		{
			get
			{
				return 0.6f;
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060003F5 RID: 1013 RVA: 0x00020B7C File Offset: 0x0001ED7C
		// (remove) Token: 0x060003F6 RID: 1014 RVA: 0x00020BB4 File Offset: 0x0001EDB4
		public event Action EvCloseByAnyButton
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{17414};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{17414}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{17414};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{17414}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060003F7 RID: 1015 RVA: 0x00020BEC File Offset: 0x0001EDEC
		// (remove) Token: 0x060003F8 RID: 1016 RVA: 0x00020C24 File Offset: 0x0001EE24
		public event Action EvCloseBySlidesFinished
		{
			[CompilerGenerated]
			add
			{
				Action action = this.{17415};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{17415}, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action action = this.{17415};
				Action action2;
				do
				{
					action2 = action;
					Action value2 = (Action)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action>(ref this.{17415}, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x000070D7 File Offset: 0x000052D7
		protected override bool CanBeWindow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x00020C5C File Offset: 0x0001EE5C
		public static {17312} AskPrice(string {17352}, RTI {17353}, Action {17354}, bool {17355} = true)
		{
			Action<int> {17386} = delegate(int {17427})
			{
				if ({17427} == 0 && {17353}.Value <= Session.Account.Gold)
				{
					if ({17355})
					{
						Session.Account.Gold -= {17353}.Value;
					}
					{17354}();
				}
			};
			{17443}[] array = new {17443}[2];
			int num = 0;
			string {17454};
			if ({17353}.Value <= Session.Account.Gold)
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
				defaultInterpolatedStringHandler.AppendFormatted(Local.yes);
				defaultInterpolatedStringHandler.AppendLiteral(", -");
				defaultInterpolatedStringHandler.AppendFormatted(StringHelper.BigValueHelper({17353}.Value));
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.gold2);
				{17454} = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			else
			{
				{17454} = "-" + StringHelper.BigValueHelper({17353}.Value) + " " + Local.gold2;
			}
			array[num] = new {17443}({17454}, ({17353}.Value > Session.Account.Gold) ? Local.gold_not_enough : "", {17312}.cIconMoney, {17353}.Value > Session.Account.Gold, 0f);
			array[1] = new {17443}(Local.no, string.Empty, {17312}.cIconReject, false, 0f);
			return new {17312}({17352}, {17386}, array);
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00020DA4 File Offset: 0x0001EFA4
		public static {17312} AskPriceGuild(string {17356}, string {17357}, RTI {17358}, RTI {17359}, Action {17360})
		{
			Action<int> {17386} = delegate(int {17428})
			{
				if (Session.Guild == null)
				{
					return;
				}
				if ({17428} == 0)
				{
					if ({17358}.Value > Session.Guild.ConquerBadges)
					{
						new {17312}(Local.conquer_badges_not_enough);
						return;
					}
					if ({17359}.Value > Session.Guild.Treasury)
					{
						new {17312}(Local.gold_ingots_not_enough);
						return;
					}
					{17360}();
				}
			};
			{17443}[] array = new {17443}[2];
			int num = 0;
			string str;
			if ({17358}.Value <= 0)
			{
				str = "";
			}
			else
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler.AppendLiteral("-");
				defaultInterpolatedStringHandler.AppendFormatted<int>({17358}.Value);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.conquer_badges);
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				str = defaultInterpolatedStringHandler.ToStringAndClear();
			}
			string str2;
			if ({17359}.Value <= 0)
			{
				str2 = "";
			}
			else
			{
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler2.AppendLiteral("-");
				defaultInterpolatedStringHandler2.AppendFormatted<int>({17359}.Value);
				defaultInterpolatedStringHandler2.AppendLiteral(" ");
				defaultInterpolatedStringHandler2.AppendFormatted(Local.ingots);
				defaultInterpolatedStringHandler2.AppendLiteral(" ");
				str2 = defaultInterpolatedStringHandler2.ToStringAndClear();
			}
			array[num] = new {17443}({17357}, str + str2, {17312}.cIconMoney, {17358}.Value > Session.Guild.ConquerBadges || {17359}.Value > Session.Guild.Treasury, 0f);
			array[1] = new {17443}(Local.to_back2, string.Empty, {17312}.cIconReject, false, 0f);
			return new {17312}({17356}, {17386}, array);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00020F24 File Offset: 0x0001F124
		public static {17312} AskPriceMonets(string {17361}, RTI {17362}, Action {17363}, bool {17364} = true)
		{
			if ({17362}.Value <= Session.Account.Monets.Value)
			{
				Action<int> {17386} = delegate(int {17429})
				{
					if ({17429} == 0 && {17362}.Value <= Session.Account.Monets.Value)
					{
						if ({17364})
						{
							PlayerAccount account = Session.Account;
							account.Monets.Value = account.Monets.Value - {17362}.Value;
						}
						{17363}();
					}
				};
				{17443}[] array = new {17443}[2];
				int num = 0;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 3);
				defaultInterpolatedStringHandler.AppendFormatted(Local.yes);
				defaultInterpolatedStringHandler.AppendLiteral(", -");
				defaultInterpolatedStringHandler.AppendFormatted(StringHelper.BigValueHelper({17362}.Value));
				defaultInterpolatedStringHandler.AppendLiteral(" ");
				defaultInterpolatedStringHandler.AppendFormatted(Local.monets);
				array[num] = new {17443}(defaultInterpolatedStringHandler.ToStringAndClear(), string.Empty, {17312}.cIconMoney, false, 0f);
				array[1] = new {17443}(Local.no, string.Empty, {17312}.cIconReject, false, 0f);
				return new {17312}({17361}, {17386}, array);
			}
			Global.Game.ScenePort.realShopHandler(null, null);
			{20881}.ShowBuyMonetsToolTip({17362}.Value);
			return null;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0002103A File Offset: 0x0001F23A
		public {17312}(string {17365}) : this({17365}, delegate(int {17423})
		{
		}, new string[]
		{
			"OK"
		})
		{
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x00021070 File Offset: 0x0001F270
		public {17312}(string {17366}, CustomSpriteFont {17367}) : this({17366}, delegate(int {17424})
		{
		}, new {17443}[]
		{
			new {17443}("OK", "", default(Rectangle), false, 0f)
		}, {17068}.BlockingWay.BackgroundBlocking, true, 47, {17367})
		{
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000210D4 File Offset: 0x0001F2D4
		public {17312}(string {17368}, Action {17369}, bool {17370} = false) : this({17368}, delegate(int {17430})
		{
			if ({17430} == 0)
			{
				{17369}();
			}
		}, new {17443}[]
		{
			new {17443}(Local.to_continue, "", default(Rectangle), false, (float)({17370} ? 10 : 0)),
			new {17443}(Local.undo, "", default(Rectangle), false, 0f)
		})
		{
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00021158 File Offset: 0x0001F358
		public {17312}(string {17371}, Action {17372}, Action {17373}) : this({17371}, delegate(int {17431})
		{
			if ({17431} == 0)
			{
				Action yes = {17372};
				if (yes == null)
				{
					return;
				}
				yes();
				return;
			}
			else
			{
				Action no = {17373};
				if (no == null)
				{
					return;
				}
				no();
				return;
			}
		}, new {17443}[]
		{
			new {17443}(Local.yes, "", {17312}.cIconAccept, false, 0f),
			new {17443}(Local.no, "", {17312}.cIconReject, false, 0f)
		})
		{
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000211D4 File Offset: 0x0001F3D4
		public {17312}(string {17374}, Action<int> {17375}, params string[] {17376}) : this({17374}, {17375}, {17312}.Repack({17376}))
		{
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000211E4 File Offset: 0x0001F3E4
		public {17312}(string {17377}, int {17378}, bool {17379} = true) : this({17377}, delegate(int {17425})
		{
		}, new {17443}[]
		{
			new {17443}("OK", string.Empty, null, {17379}, (float){17378})
		})
		{
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x00021237 File Offset: 0x0001F437
		public {17312}(bool {17380}, params {17464}[] {17381}) : this({17380}, 0, {17381})
		{
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00021244 File Offset: 0x0001F444
		private {17312}(bool {17382}, int {17383}, params {17464}[] {17384})
		{
			{17312}.<>c__DisplayClass60_0 CS$<>8__locals1 = new {17312}.<>c__DisplayClass60_0();
			CS$<>8__locals1.slides = {17384};
			CS$<>8__locals1.idx = {17383};
			CS$<>8__locals1.vertical = {17382};
			this..ctor(string.Empty, delegate(int {17426})
			{
			}, {17312}.Repack(new string[]
			{
				Local.to_continue + " 00/00"
			}), CS$<>8__locals1.vertical ? {17068}.BlockingWay.NoBackground : {17068}.BlockingWay.BackgroundBlocking, !CS$<>8__locals1.vertical, CS$<>8__locals1.vertical ? 29 : 47, null);
			CS$<>8__locals1.<>4__this = this;
			this.{17421} = CS$<>8__locals1.vertical;
			this.{17417} = CS$<>8__locals1.slides;
			this.{17418} = CS$<>8__locals1.slides.Length;
			this.{17420} = CS$<>8__locals1.idx;
			Vector2 zero = Vector2.Zero;
			CustomSpriteFont philosopher_ = Fonts.Philosopher_14;
			Color skyBlue = Color.SkyBlue;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(1, 2);
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.{17420} + 1);
			defaultInterpolatedStringHandler.AppendLiteral("/");
			defaultInterpolatedStringHandler.AppendFormatted<int>(this.{17418});
			Label label = new Label(zero, philosopher_, skyBlue, defaultInterpolatedStringHandler.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if (this.{17420} == this.{17418} - 1)
			{
				this.{17416}[0].Text = (Local.close ?? "");
			}
			else
			{
				this.{17416}[0].Text = (Local.to_continue ?? "");
			}
			this.{17416}[0].AddChildPos(label, PositionAlignment.RightDown, PositionAlignment.Center, 20f);
			label.IsVisible = (this.{17418} > 1);
			{17312}.<>c__DisplayClass60_1 CS$<>8__locals2;
			CS$<>8__locals2.content = new StackForm(Vector2.Zero, UiOrientation.VerticalCentroid, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			if (CS$<>8__locals1.vertical)
			{
				CS$<>8__locals1.<.ctor>g__AddImage|1(true, ref CS$<>8__locals2);
				this.{17422} = TextBlockBuilder.CreateBlockSpecial(380f, 200f, CS$<>8__locals1.slides[CS$<>8__locals1.idx].Text, {17312}.TextColor, Color.Lerp(Color.Gold, Color.LightYellow, 0.5f), Fonts.PhilosopherSizes, Fonts.Philosopher_16, -1).CreateCentroid();
				int num = 30;
				Vector2 vector = Vector2.Zero;
				Form form = new Form(new Marker(ref vector, this.{17422}.PosWidth + (float)num, this.{17422}.PosHeight), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.AddChildPos(this.{17422}, PositionAlignment.LeftUp, PositionAlignment.LeftUp, (float)num, 0f, false);
				CS$<>8__locals2.content.AddItem(new UiControl[]
				{
					form
				});
				this.TexturePath = new Rectangle(3675, 1793, 418, 440);
				Vector2 size = this.TexturePath.WidthHeight() * 1.2f;
				base.UpdateComplete += delegate(UiControl {17434})
				{
					CS$<>8__locals1.<>4__this.Pos = new Marker((float)Engine.GS.UIArea.Width - size.X + 1f, (float)(Engine.GS.UIArea.Height / 2) - size.Y / 2f, ref size);
				};
				base.Pos = new Marker((float)Engine.GS.UIArea.Width - size.X + 2f, (float)(Engine.GS.UIArea.Height / 2) - size.Y / 2f, ref size);
				base.PositionAlignment_X = PositionAlignment.RightDown;
				base.PositionAlignment_Y = PositionAlignment.Center;
				foreach (UiControl uiControl in ((IEnumerable<UiControl>)base.GetChildren))
				{
					Marker pos = uiControl.Pos;
					vector = new Vector2(-10f, -75f);
					uiControl.Pos = pos.Offset(vector);
				}
				base.AddChildPos(CS$<>8__locals2.content, PositionAlignment.LeftUp, PositionAlignment.LeftUp, base.Pos.HalfSize.X - CS$<>8__locals2.content.Pos.HalfSize.X + 60f, base.Pos.HalfSize.Y - CS$<>8__locals2.content.Pos.HalfSize.Y - 60f, false);
				return;
			}
			this.{17422} = TextBlockBuilder.CreateBlockSpecial(500f, CS$<>8__locals1.slides[CS$<>8__locals1.idx].Text, {17312}.TextColor, Color.Lerp(Color.Gold, Color.LightYellow, 0.5f), this.{17419}, this.{17419}, -1).CreateCentroid();
			CS$<>8__locals2.content.AddItem(new UiControl[]
			{
				this.{17422}
			});
			CS$<>8__locals1.<.ctor>g__AddImage|1(false, ref CS$<>8__locals2);
			base.AddChildPos(CS$<>8__locals2.content, PositionAlignment.LeftUp, PositionAlignment.LeftUp, base.Pos.HalfSize.X - CS$<>8__locals2.content.Pos.HalfSize.X + 20f, 180f, false);
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x00021720 File Offset: 0x0001F920
		public {17312}(string {17385}, Action<int> {17386}, params {17443}[] {17387}) : this({17385}, {17386}, {17387}, {17068}.BlockingWay.BackgroundBlocking, true, 47, null)
		{
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x00021730 File Offset: 0x0001F930
		private {17312}(string {17388}, Action<int> {17389}, {17443}[] {17390}, {17068}.BlockingWay {17391}, bool {17392}, int {17393} = 47, CustomSpriteFont {17394} = null)
		{
			this.MissclickProtection = 500f;
			this.ClickedButtonIndex = -1;
			this.{17419} = Fonts.Philosopher_16;
			base..ctor(Marker.FromCentrScreen(Engine.GS.UIArea.WidthHeight(), {17312}.c_main), {17312}.c_main, {17391}, {17392});
			base.Pos = base.Pos.Offset(0f, -40f);
			if ({17390}.Length == 0)
			{
				throw new ArgumentException("closeVariants");
			}
			if ({17394} != null)
			{
				this.{17419} = {17394};
			}
			int num = ({17388}.Length > 200) ? 600 : (({17388}.Length > 100) ? 500 : 450);
			int num2 = ({17388}.Length > 200) ? 160 : 184;
			base.AddChild(this.{17422} = TextBlockBuilder.CreateBlockSpecial((float)num, {17388}, {17312}.TextColor, Color.Lerp(Color.Gold, Color.LightYellow, 0.5f), this.{17419}, this.{17419}, -1).CreateCentroid(base.Pos.XY + new Vector2(base.Pos.WH.X / 2f, (float)num2)));
			this.{17413} = {17389};
			this.AnimatedFocus = false;
			{17312}.CurrentInstance = this;
			base.EvRemoveFromContainer += this.{17410};
			if ({17390}[0].Icon.X > 0 || {17390}[0].IconTex != null)
			{
				float num3 = base.Pos.Center.X - (float)({17312}.c_icon_holder.Width / 2) - (float)(200 * ({17390}.Length - 1) / 2);
				int num4 = 0;
				for (int i = 0; i < {17390}.Length; i++)
				{
					{17443} {17443} = {17390}[i];
					AnimatedButton animatedButton = new AnimatedButton(new Vector2(num3, base.Pos.XY.Y + 323f), {17312}.c_icon_holder, {17312}.c_icon_holder, {17443}.Disallow ? {17312}.c_icon_holder : {17312}.c_icon_holder_hl, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					num3 += 200f;
					animatedButton.AnimatedFocus = false;
					animatedButton.DisableDepthFocusTest = true;
					animatedButton.AddChild(new Label(animatedButton.Pos.XY + new Vector2((float)({17312}.c_icon_holder.Width / 2), 55f), Fonts.Philosopher_14, Color.SkyBlue, {17443}.Text, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
					animatedButton.AddChild(TextBlockBuilder.CreateBlock(180f, {17443}.Description, {17443}.Disallow ? (Color.OrangeRed * 0.7f) : ({17312}.TextColor * 0.9f), Fonts.Arial_9, -1f).CreateCentroid(new Vector2(animatedButton.Pos.Center.X, animatedButton.Pos.XY.Y + 71f)));
					if ({17443}.IconTex != null)
					{
						animatedButton.AddChild(new Image(new Vector2(animatedButton.Pos.Center.X - (float)({17443}.IconTex.Width / 2), animatedButton.Pos.XY.Y - 23f), {17443}.IconTex, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					}
					else
					{
						animatedButton.AddChild(new Form(new Vector2(animatedButton.Pos.Center.X - (float)({17443}.Icon.Width / 2), animatedButton.Pos.XY.Y - 23f), {17443}.Icon, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
						{
							AnimatedFocus = false
						});
					}
					base.AddChild(animatedButton);
					int cached = num4;
					if ({17443}.Disallow)
					{
						animatedButton.AllowMouseInput = false;
					}
					else
					{
						animatedButton.EvClick += delegate(ClickUiEventArgs {17435})
						{
							this.EmulateClick(cached);
						};
					}
					num4++;
				}
			}
			else
			{
				StackForm stack = new StackForm(base.Pos.XY + new Vector2(base.Pos.WH.X / 2f, 410f), UiOrientation.Horizontal, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Keys[] array = new Keys[{17390}.Length];
				this.{17416} = new Button[{17390}.Length];
				if ({17390}.Length == 1)
				{
					array[0] = Keys.Enter;
				}
				if ({17390}.Length == 2)
				{
					array[0] = Keys.Enter;
					array[1] = Keys.Escape;
				}
				for (int j = 0; j < {17390}.Length; j++)
				{
					this.{17416}[j] = this.{17396}({17390}[j].Text, {17390}[j].AutoTimeoutSec, j, {17390}[j].AddKeyboardKey ? array[j] : Keys.None);
					if ({17390}[j].Disallow)
					{
						this.{17416}[j].AllowMouseInput = false;
					}
					stack.AddItem(new UiControl[]
					{
						this.{17416}[j]
					});
				}
				stack.Pos = stack.Pos.Offset(-stack.Pos.WH.X / 2f, 0f);
				stack.UpdateComplete += delegate(UiControl {17436})
				{
					stack.Opacity = Geometry.Saturate(Math.Max(0.3f * (1f - this.MissclickProtection / 10000f), 1f - this.MissclickProtection / 500f));
				};
				base.AddChild(stack);
			}
			base.RemoveAnimations();
			new UiMarkerAndOpacityAnimation(this, 0.0001f, 1f, base.Pos.Border(-7f), base.Pos, 260f, UiAmimationCurve.Linear);
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00021D08 File Offset: 0x0001FF08
		private static {17443}[] Repack(string[] {17395})
		{
			{17443}[] array = new {17443}[{17395}.Length];
			for (int i = 0; i < {17395}.Length; i++)
			{
				array[i] = new {17443}({17395}[i], string.Empty, Rectangle.Empty, false, 0f)
				{
					AddKeyboardKey = (i == 0 || {17395}.Length <= 2)
				};
			}
			return array;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x00021D68 File Offset: 0x0001FF68
		private Button {17396}(string {17397}, float {17398}, int {17399}, Keys {17400} = Keys.None)
		{
			CustomSpriteFont font = Fonts.Philosopher_14Bold;
			Vector2 vector = font.Measure({17397});
			Button button = new Button(Vector2.Zero, (vector.X > 105f) ? {17312}.cButton_long : {17312}.cButton_short, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			button.Pos = button.Pos.Scale(1.05f);
			button.SetText("  " + {17397}, font, Color.White, false);
			button.EvClick += delegate(ClickUiEventArgs {17437})
			{
				this.EmulateClick({17399});
			};
			if ({17400} != Keys.None)
			{
				Form form = new Form(new Vector2(14f, 12f), ({17400} == Keys.Enter) ? {17312}.c_keyBlock_enter : (({17400} == Keys.Escape) ? {17312}.c_keyBlock_esc : {17312}.c_keyBlock_free), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				if ({17400} != Keys.Enter && {17400} != Keys.Escape)
				{
					form.AddChild(new Label(form.Pos.Center, Fonts.Arial_12, new Color(160, 160, 160), {17400}.GetKeyName(), PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
				}
				form.RenderToDepthMap = false;
				button.AddChild(form);
				button.UpdateComplete += delegate(UiControl {17438})
				{
					if (InputHelper.IsClick({17400}) && this.AllowMouseInput && {17438}.AllowMouseInput)
					{
						{17438}.ImitateClick(false);
					}
				};
			}
			if ({17398} > 0f)
			{
				float cache = {17398};
				button.Opacity = 0.5f;
				button.AllowMouseInput = false;
				button.UpdateComplete += delegate(UiControl {17440})
				{
					cache -= Global.Game.GameTime.ElapsedUpdateSec;
					Button button;
					if (cache <= 0f)
					{
						button.AllowMouseInput = true;
						button.Opacity = 1f;
						button.SetText({17397}, font, Color.White, false);
						return;
					}
					button = button;
					DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(3, 2);
					defaultInterpolatedStringHandler.AppendFormatted({17397});
					defaultInterpolatedStringHandler.AppendLiteral(" (");
					defaultInterpolatedStringHandler.AppendFormatted<double>(Math.Ceiling((double)cache));
					defaultInterpolatedStringHandler.AppendLiteral(")");
					button.SetText(defaultInterpolatedStringHandler.ToStringAndClear(), font, Color.White, false);
				};
			}
			Rectangle path = new Rectangle(458, 205, 169, 38);
			Form form2 = new Form(Vector2.Zero, path, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				AnimatedFocus = false
			};
			button.AddChildPos(form2, PositionAlignment.Center, PositionAlignment.Center, 0f);
			form2.UpdateComplete += delegate(UiControl {17439})
			{
				{17439}.PosWidth = this.{17412} / this.spaceKeyAnimationTime * (float)path.Width;
			};
			return button;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x00021FDC File Offset: 0x000201DC
		public {17312}(Action<string> {17401}, int {17402}, string {17403}, string {17404} = null, Action<TextBox> {17405} = null)
		{
			{17312}.<>c__DisplayClass65_0 CS$<>8__locals1 = new {17312}.<>c__DisplayClass65_0();
			CS$<>8__locals1.accept = {17401};
			CS$<>8__locals1.maxLength = {17402};
			this..ctor({17403}, delegate(int {17441})
			{
				if ({17441} == 0)
				{
					CS$<>8__locals1.accept({17312}.recordedText);
				}
			}, new string[]
			{
				Local.accept,
				Local.undo
			});
			CS$<>8__locals1.<>4__this = this;
			{17312}.<>c__DisplayClass65_1 CS$<>8__locals2 = new {17312}.<>c__DisplayClass65_1();
			CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
			{17312}.<>c__DisplayClass65_1 CS$<>8__locals3 = CS$<>8__locals2;
			Vector2 {13342} = base.Pos.XY + new Vector2(555f, 310f);
			CustomSpriteFont arial_ = Fonts.Arial_12;
			Color white = Color.White;
			DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(4, 1);
			defaultInterpolatedStringHandler.AppendLiteral("0 / ");
			defaultInterpolatedStringHandler.AppendFormatted<int>(CS$<>8__locals2.CS$<>8__locals1.maxLength);
			CS$<>8__locals3.cUsingnfo = new Label({13342}, arial_, white, defaultInterpolatedStringHandler.ToStringAndClear(), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			{17312}.<>c__DisplayClass65_1 CS$<>8__locals4 = CS$<>8__locals2;
			Vector2 vector = base.Pos.XY + new Vector2(56f, 240f);
			CS$<>8__locals4.inputBox = new TextBox(new Marker(ref vector, 563f, 102f), CommonAtlas.whitePixel, Fonts.Arial_12, Color.White * 0.9f, CommonAtlas.whitePixel, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.White * 0.1f
			};
			CS$<>8__locals2.inputBox.EnabledMultipliedLines = true;
			CS$<>8__locals2.inputBox.AllowNewLine = false;
			CS$<>8__locals2.inputBox.IsEnter = true;
			CS$<>8__locals2.inputBox.EvTextChanged += delegate(string {17442})
			{
				bool flag = CS$<>8__locals2.inputBox.Text.Length > CS$<>8__locals2.CS$<>8__locals1.maxLength;
				bool flag2 = !CS$<>8__locals2.inputBox.HasModeratorError && !flag;
				Label cUsingnfo = CS$<>8__locals2.cUsingnfo;
				DefaultInterpolatedStringHandler defaultInterpolatedStringHandler2 = new DefaultInterpolatedStringHandler(3, 2);
				defaultInterpolatedStringHandler2.AppendFormatted<int>(CS$<>8__locals2.inputBox.Text.Length);
				defaultInterpolatedStringHandler2.AppendLiteral(" / ");
				defaultInterpolatedStringHandler2.AppendFormatted<int>(CS$<>8__locals2.CS$<>8__locals1.maxLength);
				cUsingnfo.Text = defaultInterpolatedStringHandler2.ToStringAndClear();
				CS$<>8__locals2.cUsingnfo.BasicColor = (flag ? Color.OrangeRed : Color.White);
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.{17416}[0].AllowMouseInput = flag2;
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.{17416}[0].BasicColor = (flag2 ? Color.White : (Color.White * 0.5f));
				CS$<>8__locals2.CS$<>8__locals1.<>4__this.{17416}[0].TextColor = (flag2 ? Color.White : (Color.White * 0.5f));
				{17312}.recordedText = (flag2 ? CS$<>8__locals2.inputBox.Text : null);
			};
			if ({17405} == null)
			{
				CS$<>8__locals2.inputBox.AttachMaxLengthModerator(CS$<>8__locals2.CS$<>8__locals1.maxLength + 10, null, Color.Transparent);
			}
			if ({17405} != null)
			{
				{17405}(CS$<>8__locals2.inputBox);
			}
			CS$<>8__locals2.inputBox.IsEnter = true;
			base.AddChild(new UiControl[]
			{
				CS$<>8__locals2.cUsingnfo,
				CS$<>8__locals2.inputBox
			});
			GameScene.IncreaseGameInput();
			base.EvRemoveFromContainer += delegate()
			{
				GameScene.DecreaseGameInput();
			};
			if (!string.IsNullOrEmpty({17404}))
			{
				CS$<>8__locals2.inputBox.Text = {17404};
			}
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000221E8 File Offset: 0x000203E8
		protected override void UserUpdate(ref FrameTime {17406})
		{
			this.{17411} += {17406}.secElapsed * 0.5f;
			{17406}.EvaluteTimerMs(ref this.MissclickProtection);
			if (Debugging.DebugInfo)
			{
				this.MissclickProtection = 0f;
			}
			if ({17406}.EvaluteTimerMs2(ref this.TimeoutMs))
			{
				this.EmulateClick(0);
			}
			if (this.{17416} != null && ((this.{17418} == 0) ? (this.{17416}.Length == 1) : (this.{17420} == this.{17418} - 1)) && InputHelper.IsClick(Keys.Escape))
			{
				this.EmulateClick(0);
			}
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00022280 File Offset: 0x00020480
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
			if (!this.{17421})
			{
				Device gs = Engine.GS;
				Rectangle rectangle = new Rectangle(2933, 0, 733, 486);
				Rectangle rectangle2 = base.Pos.ScaleOfCenter(1.1f).Offset(4f, 60f).ToRect();
				Color color = Color.White * base.GetOpcaity();
				gs.Draw(rectangle, rectangle2, color);
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0002230C File Offset: 0x0002050C
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
			Engine.GS.SetFont(this.{17419});
			if (this.TimeoutMs != 0f)
			{
				Device gs = Engine.GS;
				string {14599} = Local.BigEventMsg_3 + Math.Ceiling((double)(this.TimeoutMs / 1000f)).ToString() + Local.StringConstants_80;
				Vector2 vector = new Vector2(base.Pos.Center.X - 80f, this.{17422}.Pos.End.Y + 10f);
				Color wheat = Color.Wheat;
				gs.DrawString({14599}, vector, wheat);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000223BC File Offset: 0x000205BC
		public void EmulateClick(int {17407})
		{
			if (this.MissclickProtection != 0f)
			{
				return;
			}
			this.ClickedButtonIndex = {17407};
			this.{17413}({17407});
			base.BlockAndClose();
			Action action = this.{17414};
			if (action != null)
			{
				action();
			}
			if (this.{17418} > 0 && this.{17420} + 1 < this.{17418} && this.IsClosedByHand)
			{
				this.{17420} = Math.Min(this.{17420} + 1, this.{17418} - 1);
				new {17312}(this.{17421}, this.{17420}, this.{17417}).{17415} = this.{17415};
				return;
			}
			if (this.{17418} > 0)
			{
				Action action2 = this.{17415};
				if (action2 == null)
				{
					return;
				}
				action2();
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00022660 File Offset: 0x00020860
		[CompilerGenerated]
		internal static Vector2 <.ctor>g__ShrinkToMaxSize|60_2(Rectangle {17408}, float {17409} = 1f)
		{
			if ((float){17408}.Width <= (float){17312}.SlideImageMaxWidth * {17409} && (float){17408}.Height <= (float){17312}.SlideImageMaxHeight * {17409})
			{
				return {17408}.WidthHeight();
			}
			float num = Math.Min((float){17312}.SlideImageMaxWidth * {17409} * 1f / (float){17408}.Width, (float){17312}.SlideImageMaxHeight * {17409} * 1f / (float){17408}.Height);
			return new Vector2((float){17408}.Width * num, (float){17408}.Height * num);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000226DF File Offset: 0x000208DF
		[CompilerGenerated]
		private void {17410}()
		{
			if ({17312}.CurrentInstance == this)
			{
				{17312}.CurrentInstance = null;
			}
		}

		// Token: 0x0400033F RID: 831
		public static {17312} CurrentInstance;

		// Token: 0x04000340 RID: 832
		public static readonly Rectangle cIconShipFire = new Rectangle(1200, 1286, 68, 68);

		// Token: 0x04000341 RID: 833
		public static readonly Rectangle cIconShield = new Rectangle(1200, 1355, 68, 68);

		// Token: 0x04000342 RID: 834
		public static readonly Rectangle cIconSpyglass = new Rectangle(1200, 1424, 68, 68);

		// Token: 0x04000343 RID: 835
		public static readonly Rectangle cIconAccept = new Rectangle(1200, 1493, 68, 68);

		// Token: 0x04000344 RID: 836
		public static readonly Rectangle cIconReject = new Rectangle(1200, 1562, 68, 68);

		// Token: 0x04000345 RID: 837
		public static readonly Rectangle cIconPirateFlag = new Rectangle(1269, 1562, 68, 68);

		// Token: 0x04000346 RID: 838
		public static readonly Rectangle cIconMoney = new Rectangle(1131, 1286, 68, 68);

		// Token: 0x04000347 RID: 839
		public static readonly Rectangle cIconPlus = new Rectangle(1131, 1355, 68, 68);

		// Token: 0x04000348 RID: 840
		public static readonly Rectangle c_keyBlock_enter = new Rectangle(1097, 768, 34, 21);

		// Token: 0x04000349 RID: 841
		public static readonly Rectangle cButton_short = new Rectangle(1132, 768, 153, 43);

		// Token: 0x0400034A RID: 842
		public static readonly Rectangle cButton_long = new Rectangle(1286, 768, 257, 43);

		// Token: 0x0400034B RID: 843
		public static readonly int SlideImageMaxWidth = 530;

		// Token: 0x0400034C RID: 844
		public static readonly int SlideImageMaxHeight = 164;

		// Token: 0x0400034D RID: 845
		private static readonly Rectangle c_main = new Rectangle(1132, 812, 648, 473);

		// Token: 0x0400034E RID: 846
		private static readonly Rectangle c_decor = new Rectangle(1479, 1762, 341, 146);

		// Token: 0x0400034F RID: 847
		private static readonly Rectangle c_icon_holder = new Rectangle(1270, 1286, 198, 136);

		// Token: 0x04000350 RID: 848
		private static readonly Rectangle c_icon_holder_hl = new Rectangle(1270, 1423, 198, 136);

		// Token: 0x04000351 RID: 849
		private static readonly Rectangle c_keyBlock_esc = new Rectangle(1097, 790, 34, 21);

		// Token: 0x04000352 RID: 850
		private static readonly Rectangle c_keyBlock_free = new Rectangle(1097, 812, 34, 21);

		// Token: 0x04000353 RID: 851
		private const int slideImageYSpace = 10;

		// Token: 0x04000354 RID: 852
		private const float xSpace = 6f;

		// Token: 0x04000355 RID: 853
		private float {17411};

		// Token: 0x04000356 RID: 854
		private float {17412};

		// Token: 0x04000357 RID: 855
		private static string recordedText = "";

		// Token: 0x04000358 RID: 856
		private Action<int> {17413};

		// Token: 0x04000359 RID: 857
		public float MissclickProtection;

		// Token: 0x0400035A RID: 858
		[CompilerGenerated]
		private Action {17414};

		// Token: 0x0400035B RID: 859
		[CompilerGenerated]
		private Action {17415};

		// Token: 0x0400035C RID: 860
		public float TimeoutMs;

		// Token: 0x0400035D RID: 861
		public string ClickedButtonText;

		// Token: 0x0400035E RID: 862
		public int ClickedButtonIndex;

		// Token: 0x0400035F RID: 863
		private Button[] {17416};

		// Token: 0x04000360 RID: 864
		private readonly {17464}[] {17417};

		// Token: 0x04000361 RID: 865
		private readonly int {17418};

		// Token: 0x04000362 RID: 866
		private CustomSpriteFont {17419};

		// Token: 0x04000363 RID: 867
		private int {17420};

		// Token: 0x04000364 RID: 868
		private bool {17421};

		// Token: 0x04000365 RID: 869
		private TextBlockControl {17422};
	}
}
