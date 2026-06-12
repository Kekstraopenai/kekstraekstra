using System;
using Common.Packets;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Scenes;

namespace WorldOfSeaBattles.Interface.DebugPanel
{
	// Token: 0x0200059A RID: 1434
	public class DebugPanel : CustomUi
	{
		// Token: 0x1700031D RID: 797
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x0012905D File Offset: 0x0012725D
		public static bool IsActive
		{
			get
			{
				return DebugPanel.instance != null;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x00129067 File Offset: 0x00127267
		// (set) Token: 0x0600212F RID: 8495 RVA: 0x0012906E File Offset: 0x0012726E
		public static bool Pinned
		{
			get
			{
				return DebugPanel.pinned;
			}
			set
			{
				if (DebugPanel.pinned == value)
				{
					return;
				}
				DebugPanel.pinned = value;
				DebugPanel.Close();
				DebugPanel.Open();
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x0012908C File Offset: 0x0012728C
		public DebugPanel(bool {26398})
		{
			Vector2 vector = new Vector2((float)(Engine.GS.UIArea.Width - DebugPanel.main.Width), 50f);
			base..ctor(new Marker(ref vector, ref DebugPanel.main), AtlasGameGui.rect_asset_transparent_1px, PositionAlignment.RightDown, PositionAlignment.LeftUp, Color.White, false);
			this.AllowDragDrop = true;
			this.AnimatedFocus = false;
			if ({26398})
			{
				Global.Game.SceneGame.IncreaseMouse();
				GameScene.IncreaseGameInput();
				base.EvRemoveFromContainer += delegate()
				{
					GameScene.DecreaseGameInput();
					Global.Game.SceneGame.DecreaseMouse();
				};
			}
			this.{26404} = new DebugStatsForm(base.Pos.XY);
			this.{26405} = new DebugConsoleForm(new Vector2(this.{26404}.Pos.End.X, base.Pos.XY.Y));
			this.{26406} = new DebugActionsForm(new Vector2(this.{26405}.Pos.End.X, base.Pos.XY.Y + 25f));
			base.AddChild(new UiControl[]
			{
				this.{26404},
				this.{26405},
				this.{26406}
			});
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x00003100 File Offset: 0x00001300
		public static void Open()
		{
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x001291DC File Offset: 0x001273DC
		public static void Close()
		{
			if (DebugPanel.instance == null)
			{
				return;
			}
			DebugPanel.savedHistory = DebugPanel.instance.{26405}.GetHistory();
			DebugPanel.savedContent = DebugPanel.instance.{26405}.GetContent();
			DebugPanel.instance.RemoveFromContainer();
			DebugPanel.instance = null;
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00129229 File Offset: 0x00127429
		protected override void UserUpdate(ref FrameTime {26399})
		{
			this.{26404}.UserUpdate(ref {26399});
			this.{26405}.UserUpdate(ref {26399});
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x00129244 File Offset: 0x00127444
		protected override void UserBackRender()
		{
			if (this.{26407} = (Engine.GS.CurrentTexture != AtlasGameGui.Texture.Tex))
			{
				Engine.GS.SetTexture(AtlasGameGui.Texture);
			}
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00129284 File Offset: 0x00127484
		protected override void UserFrontRender()
		{
			if (this.{26407})
			{
				Engine.GS.ReturnBackTexture();
			}
			DebugRenderTargets.Render();
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x0012929D File Offset: 0x0012749D
		public static void WriteToConsole(string {26400}, Color {26401})
		{
			DebugPanel debugPanel = DebugPanel.instance;
			if (debugPanel == null)
			{
				return;
			}
			debugPanel.{26405}.Write({26400}, {26401});
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x001292B8 File Offset: 0x001274B8
		public static void Execute(string {26402}, bool {26403} = false)
		{
			if (string.IsNullOrWhiteSpace({26402}))
			{
				return;
			}
			Global.Network.Send(new OnConsoleCommandExecute
			{
				RawString = {26402},
				Silent = {26403}
			});
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x001292F6 File Offset: 0x001274F6
		public static void Clear()
		{
			DebugPanel debugPanel = DebugPanel.instance;
			if (debugPanel == null)
			{
				return;
			}
			debugPanel.{26405}.ClearContent();
		}

		// Token: 0x04002032 RID: 8242
		private static DebugPanel instance;

		// Token: 0x04002033 RID: 8243
		private static ConsoleHistory savedHistory;

		// Token: 0x04002034 RID: 8244
		private static TextBlockBuilder savedContent;

		// Token: 0x04002035 RID: 8245
		private static readonly Rectangle main = new Rectangle(10000, 10000, 1011, 350);

		// Token: 0x04002036 RID: 8246
		private static bool pinned = false;

		// Token: 0x04002037 RID: 8247
		private DebugStatsForm {26404};

		// Token: 0x04002038 RID: 8248
		private DebugConsoleForm {26405};

		// Token: 0x04002039 RID: 8249
		private DebugActionsForm {26406};

		// Token: 0x0400203A RID: 8250
		private bool {26407};
	}
}
