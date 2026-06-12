using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003DF RID: 991
	internal class {22676} : CustomUi
	{
		// Token: 0x06001596 RID: 5526 RVA: 0x000B5D7C File Offset: 0x000B3F7C
		public {22676}(bool {22678})
		{
			Vector2 zero = Vector2.Zero;
			base..ctor(new Marker(ref zero, ref {22676}.c_active), {22676}.c_active, PositionAlignment.LeftUp, PositionAlignment.RightDown, Color.Transparent, false);
			this.{22684} = {22678};
			if ({22478}.currentButton != null)
			{
				throw new Exception();
			}
			base.EvRemoveFromContainer += this.{22681};
			base.EvClick += this.{22682};
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x000B5DF4 File Offset: 0x000B3FF4
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
			Rectangle rectangle = base.Pos.ToRect();
			Device gs = Engine.GS;
			Rectangle rectangle2 = this.{22684} ? {22676}.c_active : {22676}.c_passive;
			gs.Draw(rectangle2, rectangle);
			if (this.{22684})
			{
				Device gs2 = Engine.GS;
				Color color = Color.White * (float)(0.5 + 0.5 * Math.Sin(Global.Game.GameTotalTimeSec * 4.0)) * base.GetOpcaity();
				gs2.Draw({22676}.c_activeMask, rectangle, color);
			}
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x000B5EAC File Offset: 0x000B40AC
		protected override void UserFrontRender()
		{
			if (this.{22684})
			{
				Engine.GS.SetFont(Fonts.Philosopher_14);
				Device gs = Engine.GS;
				string {14599} = (this.{22685}.Length > 20) ? (this.{22685}.Substring(0, 20) + "...") : this.{22685};
				Vector2 vector = new Vector2(base.Pos.End.X + 5f, base.Pos.Center.Y - 9f);
				Color color = Color.White * (0.7f * this.{22686} / 20000f);
				gs.DrawString({14599}, vector, color);
			}
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x000B5F65 File Offset: 0x000B4165
		protected override void UserUpdate(ref FrameTime {22679})
		{
			{22679}.EvaluteTimerMs(ref this.{22686});
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x000B5F73 File Offset: 0x000B4173
		public void SetBlinked(string {22680})
		{
			this.{22684} = true;
			this.{22685} = {22680};
			this.{22686} = 20000f;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x000B5F8E File Offset: 0x000B418E
		public void Unset()
		{
			this.{22684} = false;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x000B5FE7 File Offset: 0x000B41E7
		[CompilerGenerated]
		private void {22681}()
		{
			{22478}.currentButton = null;
			Session.LSEventButtonState = this.{22684};
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x000B5FFA File Offset: 0x000B41FA
		[CompilerGenerated]
		private void {22682}(ClickUiEventArgs {22683})
		{
			base.RemoveFromContainer();
			if ({22478}.currentLSViewBox == null || {22478}.currentLSViewBox.IsVisible)
			{
				return;
			}
			{22478}.currentLSViewBox.IsVisible = true;
			{22478}.currentLSViewBox.GotFocus();
		}

		// Token: 0x04001382 RID: 4994
		public static readonly Rectangle c_active = new Rectangle(2269, 99, 64, 41);

		// Token: 0x04001383 RID: 4995
		public static readonly Rectangle c_activeMask = new Rectangle(2334, 99, 64, 41);

		// Token: 0x04001384 RID: 4996
		public static readonly Rectangle c_passive = new Rectangle(2269, 141, 64, 41);

		// Token: 0x04001385 RID: 4997
		private bool {22684};

		// Token: 0x04001386 RID: 4998
		private string {22685} = "";

		// Token: 0x04001387 RID: 4999
		private float {22686};
	}
}
