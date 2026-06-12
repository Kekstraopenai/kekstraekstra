using System;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200019C RID: 412
	internal sealed class {18937} : CustomUi
	{
		// Token: 0x06000955 RID: 2389 RVA: 0x000492D0 File Offset: 0x000474D0
		public {18937}() : base(false)
		{
			{18937}.CurrentInstance = this;
			this.AnimatedFocus = false;
			base.EvRemoveFromContainer += delegate()
			{
				{18937}.CurrentInstance = null;
			};
			if (Session.CurrentArenaSession.ModeInfo.WinKretery == ArenaWinKretery.DestroyFort)
			{
				new Marker(30f, 8f, 104f, 19f);
				new Marker(202f, 8f, 104f, 19f);
				this.{18943} = delegate()
				{
				};
			}
		}

		// Token: 0x06000956 RID: 2390 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserUpdate(ref FrameTime {18938})
		{
		}

		// Token: 0x06000957 RID: 2391 RVA: 0x00049381 File Offset: 0x00047581
		protected override void UserBackRender()
		{
			if (Session.CurrentArenaSession == null)
			{
				return;
			}
			ArenaWinKretery winKretery = Session.CurrentArenaSession.ModeInfo.WinKretery;
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x000493A0 File Offset: 0x000475A0
		private void {18939}(Vector2 {18940}, Rectangle {18941})
		{
			Device gs = Engine.GS;
			Vector2 vector = {18940} - new Vector2((float)({18941}.Width / 2), (float)({18941}.Height + 4));
			gs.Draw({18941}, vector);
		}

		// Token: 0x06000959 RID: 2393 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x0600095A RID: 2394 RVA: 0x00024672 File Offset: 0x00022872
		public override void Dispose()
		{
			base.Dispose();
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x0002467A File Offset: 0x0002287A
		protected override void CleanResources()
		{
			base.CleanResources();
		}

		// Token: 0x04000865 RID: 2149
		public static readonly Rectangle p_upIcon_focusRed = new Rectangle(2199, 123, 62, 72);

		// Token: 0x04000866 RID: 2150
		public static readonly Rectangle p_fort_protectedTowersNoDestructed = new Rectangle(2262, 123, 62, 72);

		// Token: 0x04000867 RID: 2151
		public static {18937} CurrentInstance;

		// Token: 0x04000868 RID: 2152
		private bool {18942};

		// Token: 0x04000869 RID: 2153
		private Action {18943};

		// Token: 0x0400086A RID: 2154
		private StackForm {18944};
	}
}
