using System;
using System.Runtime.CompilerServices;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Assets.Content;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameSystems;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000199 RID: 409
	internal sealed class {18909} : CustomUi
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00048803 File Offset: 0x00046A03
		public static bool BlockShip
		{
			get
			{
				{18909} currentInstance = {18909}.CurrentInstance;
				return currentInstance != null && !currentInstance.{18927};
			}
		}

		// Token: 0x06000944 RID: 2372 RVA: 0x00048818 File Offset: 0x00046A18
		public {18909}(OnLoadArena {18913}, bool {18914}, string {18915} = null)
		{
			Vector2 vector = new Vector2((float)(Engine.GS.UIArea.Width / 2 - 379), (float)(Engine.GS.UIArea.Height / 2 - 110));
			Vector2 vector2 = new Vector2(759f, 440f);
			base..ctor(new Marker(ref vector, ref vector2), Rectangle.Empty, PositionAlignment.Center, PositionAlignment.Center, Color.Transparent, false);
			{18909}.CurrentInstance = this;
			this.AnimatedFocus = false;
			this.{18927} = {18914};
			string {18918} = ({18913}.Mode == ArenaMode.DuelRating) ? "" : Local.rating;
			this.{18916}({18909}.p_leftSide[0], {18918});
			for (int i = 0; i < {18913}.YourSideInterfaceInfo.Size; i++)
			{
				this.{18919}({18913}.YourSideInterfaceInfo.Array[i], {18909}.p_leftSide[1 + i], {18914} ? 0f : (2000f * (float)i / (float){18913}.YourSideInterfaceInfo.Size), false);
			}
			this.{18916}({18909}.p_rightSide[0], {18918});
			for (int j = 0; j < {18913}.EnemySideInterfaceInfo.Size; j++)
			{
				this.{18919}({18913}.EnemySideInterfaceInfo.Array[j], {18909}.p_rightSide[1 + j], {18914} ? 0f : (2000f * (float)j / (float){18913}.EnemySideInterfaceInfo.Size), true);
			}
			if (!Global.Game.IsActive && !{18914})
			{
				Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Bell, 0.03f, 1f);
			}
			this.{18926} = new Tlist<{18909}.CountItem>();
			if (!{18914})
			{
				new UiOpacityAnimation(this, 0f, 1f, 700f);
				int num = ({18913}.YourSideInterfaceInfo.Size == 1) ? 7 : 10;
				new UiActionsSleep(this, (float)(num * 1000 - 2000));
				new UiOpacityAnimation(this, 1f, 0f, 1000f);
				new UiActor(this, delegate()
				{
					Global.Game.SoundSystem.PlaySound(GameStaticSoundName.Bell, 0.03f, 1f);
				});
				new UiRemoveAction(this);
				for (int k = 1; k <= num - 1; k++)
				{
					Tlist<{18909}.CountItem> tlist = this.{18926};
					{18909}.CountItem countItem = new {18909}.CountItem((float)k, (float)num);
					tlist.Add(countItem);
				}
			}
			if (!string.IsNullOrEmpty({18915}))
			{
				base.AddChild(new Label(base.Pos.Center.X, base.Pos.XY.Y + 180f, Fonts.Philosopher_16, Color.White * 0.9f, {18915}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			}
			base.AddChild({18909}.<.ctor>g__MakeMapName|9_1(Gameplay.ArenaMaps[(int){18913}.MapID].Name));
			base.EvRemoveFromContainer += delegate()
			{
				{18909}.CurrentInstance = null;
			};
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x00048B10 File Offset: 0x00046D10
		private void {18916}(Vector2 {18917}, string {18918})
		{
			CustomSpriteFont philosopher_12Bold = Fonts.Philosopher_12Bold;
			Vector2 vector = base.Pos.XY + {18917} - new Vector2(4f, 3f);
			Form form = new Form(new Marker(ref vector, 319f, 24f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Black * 0.6f,
				AnimatedFocus = false
			};
			base.AddChild(form);
			form.AddChild(new Label(base.Pos.XY + {18917}, philosopher_12Bold, Color.LightGray, Local.ArenaPlayersInfoUi_1, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(base.Pos.XY + {18917} + new Vector2(70f, 0f), philosopher_12Bold, Color.LightGray, Local.name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			form.AddChild(new Label(base.Pos.XY + {18917} + new Vector2(220f, 0f), philosopher_12Bold, Color.LightGray, {18918}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00048C2C File Offset: 0x00046E2C
		private void {18919}(string {18920}, Vector2 {18921}, float {18922}, bool {18923})
		{
			CustomSpriteFont philosopher_ = Fonts.Philosopher_12;
			string {13345};
			string text;
			string text2;
			try
			{
				string[] array = {18920}.Split(new string[]
				{
					"$?"
				}, StringSplitOptions.RemoveEmptyEntries);
				{13345} = array[0];
				text = array[1];
				text2 = array[2];
			}
			catch
			{
				{13345} = "";
				text = ({18920} ?? "");
				text2 = "";
			}
			Vector2 vector = base.Pos.XY + {18921} - new Vector2(4f, 3f);
			Form form = new Form(new Marker(ref vector, 319f, 24f), AtlasGameGui.rect_asset_whitepixel_1px, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BasicColor = Color.Black * 0.5f,
				AnimatedFocus = false
			};
			base.AddChild(form);
			form.AddChild(new Label(base.Pos.XY + {18921}, philosopher_, Color.White, {13345}, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			Form form2 = form;
			Vector2 {13342} = base.Pos.XY + {18921} + new Vector2(70f, 0f);
			CustomSpriteFont {13343} = philosopher_;
			Color white = Color.White;
			string {13345}2;
			if ({18923})
			{
				ArenaCacheItem currentArenaSession = Session.CurrentArenaSession;
				bool flag;
				if (currentArenaSession == null)
				{
					flag = false;
				}
				else
				{
					ArenaModeSettings modeInfo = currentArenaSession.ModeInfo;
					flag = ((modeInfo != null) ? new bool?(modeInfo.HideNames) : null).GetValueOrDefault();
				}
				if (flag)
				{
					{13345}2 = "?";
					goto IL_141;
				}
			}
			{13345}2 = text;
			IL_141:
			form2.AddChild(new Label({13342}, {13343}, white, {13345}2, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			if (text2 != "-")
			{
				form.AddChild(new Label(base.Pos.XY + {18921} + new Vector2(250f, 0f), philosopher_, Color.White, text2, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			}
			form.Opacity = 0f;
			new UiActionsSleep(form, {18922});
			new UiOpacityAnimation(form, 1f, 500f);
		}

		// Token: 0x06000947 RID: 2375 RVA: 0x00048E04 File Offset: 0x00047004
		protected override void UserUpdate(ref FrameTime {18924})
		{
			for (int i = 0; i < this.{18926}.Size; i++)
			{
				if (this.{18926}.Array[i].Update(ref {18924}))
				{
					this.{18926}.RemoveAt(i);
					i--;
				}
			}
			if (this.{18927} && Global.Settings.kb_OpenLogbook.IsRelease)
			{
				base.RemoveFromContainer();
			}
		}

		// Token: 0x06000948 RID: 2376 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06000949 RID: 2377 RVA: 0x00048E6C File Offset: 0x0004706C
		protected override void UserFrontRender()
		{
			for (int i = 0; i < this.{18926}.Size; i++)
			{
				this.{18926}.Array[i].Render();
			}
		}

		// Token: 0x0600094A RID: 2378 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x0600094B RID: 2379 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0004903C File Offset: 0x0004723C
		[CompilerGenerated]
		internal static Form <.ctor>g__MakeMapName|9_1(string {18925})
		{
			Rectangle {13185} = new Rectangle(2240, 1900, 938, 75);
			int width = Engine.GS.UIArea.Width;
			Form form = new Form(new Marker((float)(-(float)Math.Max(0, width - Engine.GS.UIArea.Width)) * 0.5f, {18909}.CurrentInstance.Pos.XY.Y - 100f, (float)width, (float)(150 * width) / 1920f), {13185}, Color.White * 0.7f, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_24Bold, Color.White, {18925}, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.Center, PositionAlignment.LeftUp, 0f, form.PosHeight * 0.2f, false);
			return form;
		}

		// Token: 0x04000858 RID: 2136
		public static {18909} CurrentInstance;

		// Token: 0x04000859 RID: 2137
		private static readonly Vector2 p_versus = new Vector2(294f, 97f);

		// Token: 0x0400085A RID: 2138
		private static readonly Vector2[] p_leftSide = new Vector2[]
		{
			new Vector2(8f, 10f),
			new Vector2(8f, 45f),
			new Vector2(8f, 68f),
			new Vector2(8f, 91f),
			new Vector2(8f, 114f),
			new Vector2(8f, 137f),
			new Vector2(8f, 160f),
			new Vector2(8f, 183f)
		};

		// Token: 0x0400085B RID: 2139
		private static readonly Vector2[] p_rightSide = new Vector2[]
		{
			new Vector2(440f, 10f),
			new Vector2(440f, 45f),
			new Vector2(440f, 68f),
			new Vector2(440f, 91f),
			new Vector2(440f, 114f),
			new Vector2(440f, 137f),
			new Vector2(440f, 160f),
			new Vector2(440f, 183f)
		};

		// Token: 0x0400085C RID: 2140
		private Tlist<{18909}.CountItem> {18926};

		// Token: 0x0400085D RID: 2141
		private bool {18927};

		// Token: 0x0200019A RID: 410
		private class CountItem
		{
			// Token: 0x0600094E RID: 2382 RVA: 0x0004910C File Offset: 0x0004730C
			public CountItem(float {18930}, float {18931})
			{
				this.{18934} = (({18930} == 1f) ? Local.ArenaJoinGui_14 : ({18930} - 1f).ToString());
				this.{18935} = Fonts.Philosopher_36.Measure(this.{18934});
				this.{18933} = ({18931} - {18930}) * 1000f;
			}

			// Token: 0x0600094F RID: 2383 RVA: 0x00049168 File Offset: 0x00047368
			public bool Update(ref FrameTime {18932})
			{
				this.{18933} -= {18932}.msElapsed;
				if (this.{18933} < 0f)
				{
					this.{18936} += {18932}.msElapsed;
				}
				return this.{18936} > 2000f;
			}

			// Token: 0x06000950 RID: 2384 RVA: 0x000491B8 File Offset: 0x000473B8
			public void Render()
			{
				if (this.{18936} == 0f)
				{
					return;
				}
				float num = (this.{18936} < 600f) ? (2f - this.{18936} / 600f) : (1f - 0.5f * (this.{18936} - 600f) / 1400f);
				num *= 1.4f;
				Vector2 value = new Vector2(0f, -100f + this.{18936} / 2000f * 40f);
				Engine.GS.SetFont(Fonts.Philosopher_36);
				Device gs = Engine.GS;
				string {14626} = this.{18934};
				Vector2 {14627} = Engine.GS.UIArea.HalfWidthHeightInt() + value;
				Color color = Color.White * (1f - this.{18936} / 2000f);
				gs.DrawString({14626}, {14627}, color, 0f, this.{18935}, num);
			}

			// Token: 0x0400085E RID: 2142
			private float {18933};

			// Token: 0x0400085F RID: 2143
			private string {18934};

			// Token: 0x04000860 RID: 2144
			private Vector2 {18935};

			// Token: 0x04000861 RID: 2145
			private float {18936};
		}
	}
}
