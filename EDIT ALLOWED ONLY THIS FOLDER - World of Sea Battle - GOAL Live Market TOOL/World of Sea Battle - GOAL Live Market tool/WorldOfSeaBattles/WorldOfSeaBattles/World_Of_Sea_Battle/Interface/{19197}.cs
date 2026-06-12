using System;
using System.Runtime.CompilerServices;
using System.Text;
using Common;
using Common.Game;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020001C5 RID: 453
	public class {19197} : CustomUi
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x00051444 File Offset: 0x0004F644
		public {19197}(string {19201}) : base(true)
		{
			if ({19197}.fastOpenNext)
			{
				this.{19210} = 100f;
			}
			{19197}.fastOpenNext = false;
			this.{19211} = TextBlockBuilder.CreateBlock(300f, {19201}, Color.White, Fonts.Philosopher_16, -1f).Create();
			this.{19211}.Opacity = 0f;
			base.EvClick += this.{19208};
			base.AddChild(this.{19211});
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000514C4 File Offset: 0x0004F6C4
		public void AddOpenMoreButton(Action {19202})
		{
			Button bt = new Button(Vector2.Zero, new Rectangle(2644, 684, 161, 39), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			bt.SetText(Local.open_more, Fonts.Philosopher_14, Color.White, false);
			bt.EvClick += delegate(ClickUiEventArgs {19213})
			{
				bt.AllowMouseInput = false;
				{19202}();
			};
			bt.UpdateComplete += delegate(UiControl {19214})
			{
				{19214}.Opacity = this.{19211}.Opacity;
				{19214}.Pos = {19214}.Pos.SetY((float)Engine.GS.UIArea.Height * 0.5f + 55f);
			};
			base.AddChildPos(bt, PositionAlignment.Center, PositionAlignment.Center, 0f);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0005156C File Offset: 0x0004F76C
		private static string GetResponse(OnOpenChestMsg {19203}, ShopChestApplyHelper {19204})
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			for (int i = 0; i < {19203}.effects.Length; i++)
			{
				bool flag;
				string value = {19204}.Apply(Session.Account, {19203}.effects[i], {19203}.serverRandomValue, {19203}.ServerDate, out flag).ToString();
				stringBuilder.AppendLine(value);
				stringBuilder2.Append(value);
				if (i != {19203}.effects.Length - 1)
				{
					stringBuilder2.Append(", ");
				}
			}
			{19994}.Logbook(stringBuilder2.ToString(), LBFlags.L1);
			return stringBuilder.ToString();
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x000515FC File Offset: 0x0004F7FC
		public {19197}(OnOpenChestMsg {19205}, ShopChestApplyHelper {19206}) : this({19197}.GetResponse({19205}, {19206}))
		{
			bool flag = {19205}.statUseKeys == 0;
			if (flag)
			{
				OnOpenChestMsg.Flags flags = {19205}.flags;
				bool flag2 = flags <= OnOpenChestMsg.Flags.OpenTripleChest || flags - OnOpenChestMsg.Flags.OpenEmpireChest <= 2;
				flag = flag2;
			}
			if (flag)
			{
				this.AddOpenMoreButton(delegate
				{
					{19197}.fastOpenNext = true;
					{19994}.CloseAllShip();
					{20881}.TryCloseCurrentBackForm();
					DonationSystem.OpenChest({19205}.flags, {19205}.statUseKeys > 0, {19205}.statFinalPrice);
				});
			}
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x00051670 File Offset: 0x0004F870
		protected override void UserUpdate(ref FrameTime {19207})
		{
			this.{19210} += {19207}.secElapsed;
			this.{19211}.Opacity = MathHelper.Clamp(this.{19210} * 0.5f - 0.5f, 0f, 1f);
			UiControl uiControl = this.{19211};
			Vector2 vector = Engine.GS.UIArea.HalfWidthHeightInt() - this.{19211}.Pos.WH * 0.5f + new Vector2(0f, 10f);
			uiControl.Pos = new Marker(ref vector, ref this.{19211}.Pos.WH);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00051724 File Offset: 0x0004F924
		protected override void UserBackRender()
		{
			if (Engine.GS.CurrentTexture != AtlasPortGui.Texture.Tex)
			{
				this.{19212} = true;
				Engine.GS.SetTexture(AtlasPortGui.Texture.Tex);
			}
			Color color = Color.White * base.GetOpcaity();
			Vector2 vector = Engine.GS.UIArea.HalfWidthHeightInt();
			Device gs = Engine.GS;
			Vector2 vector2 = new Vector2((float)({19197}.c_mask.Width / 2), (float)({19197}.c_mask.Height / 2));
			gs.Draw({19197}.c_mask, vector, vector2, -this.{19210} * 0.5f, 2f, color);
			Engine.GS.Draw({19197}.c_mask, vector, new Vector2((float)({19197}.c_mask.Width / 2), (float)({19197}.c_mask.Height / 2)), this.{19210} * 0.7f, color);
			Device gs2 = Engine.GS;
			vector2 = new Vector2((float)({19197}.c_form.Width / 2), (float)({19197}.c_form.Height / 2));
			gs2.Draw({19197}.c_form, vector, vector2, 0f, Math.Min(1f, this.{19210} * 1.5f), color);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00051858 File Offset: 0x0004FA58
		protected override void UserFrontRender()
		{
			if (this.{19212})
			{
				Engine.GS.ReturnBackTexture();
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000518BB File Offset: 0x0004FABB
		[CompilerGenerated]
		private void {19208}(ClickUiEventArgs {19209})
		{
			new UiOpacityAnimation(this, 0f, 500f);
			new UiRemoveAction(this);
		}

		// Token: 0x04000933 RID: 2355
		private static readonly Rectangle c_mask = new Rectangle(2969, 621, 382, 382);

		// Token: 0x04000934 RID: 2356
		private static readonly Rectangle c_form = new Rectangle(2968, 1005, 400, 180);

		// Token: 0x04000935 RID: 2357
		private static bool fastOpenNext = false;

		// Token: 0x04000936 RID: 2358
		private float {19210};

		// Token: 0x04000937 RID: 2359
		private TextBlockControl {19211};

		// Token: 0x04000938 RID: 2360
		private bool {19212};
	}
}
