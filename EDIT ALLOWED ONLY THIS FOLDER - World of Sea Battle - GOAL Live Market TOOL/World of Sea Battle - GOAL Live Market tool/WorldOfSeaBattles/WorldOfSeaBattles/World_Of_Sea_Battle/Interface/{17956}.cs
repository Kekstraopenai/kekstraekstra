using System;
using System.Collections.Generic;
using Common.Data;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Animation;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000F6 RID: 246
	internal class {17956} : {17625}, {18027}.IProvider
	{
		// Token: 0x060005EB RID: 1515 RVA: 0x0002EC38 File Offset: 0x0002CE38
		public {17956}(string {17961}, {17956}.Storage {17962}, {17956}.Storage {17963}, Action<{17956}.Movement> {17964}) : base(600f, {17625}.c_back2, {17604}.InGameWindowBlockShip, Rectangle.Empty, Array.Empty<{17625}.DynamicTittle>())
		{
			{17956}.CurrentInstance = this;
			this.AnimatedFocus = false;
			this.{17971} = {17962};
			this.{17972} = {17963};
			this.{17973} = {17964};
			base.EvRemoveFromContainer += delegate()
			{
				{17956}.CurrentInstance = null;
			};
			if (!string.IsNullOrEmpty({17961}))
			{
				this.{17971}.CanMoveFromHere = false;
				this.{17972}.CanMoveFromHere = false;
				this.hasBanner = true;
				this.BannerHelper({17961});
			}
			this.{17970} = new {18027}(base.Pos.XY + new Vector2(15f, 70f), this, Rectangle.Empty);
			this.{17970}.Place1Name = {17962}.MoveText;
			this.{17970}.Place2Name = {17963}.MoveText;
			base.AddChild(this.{17970});
			this.Reload();
			Label label = new Label(base.Pos.XY + new Vector2(base.Pos.WH.X * 0.25f, 25f), Fonts.Philosopher_18, Color.White * 0.8f, {17962}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center();
			Label label2 = new Label(base.Pos.XY + new Vector2(base.Pos.WH.X * 0.75f, 25f), Fonts.Philosopher_18, Color.White * 0.8f, {17963}.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center();
			base.AddChild(new UiControl[]
			{
				label,
				label2
			});
			UiControl uiControl = this.{17970};
			Marker pos = this.{17970}.Pos;
			uiControl.Pos = pos.SetHeight(this.{17970}.Pos.WH.Y * 1.5f);
			if (this.{17970}.Pos.WH.Y > base.Pos.WH.Y - 60f)
			{
				pos = this.{17970}.Pos;
				Viewbox viewbox = new Viewbox(new Marker(ref pos.XY, this.{17970}.Pos.WH.X, base.Pos.WH.Y - 30f - (this.{17970}.Pos.XY.Y - base.Pos.XY.Y)), CommonAtlas.c_scrollUp, CommonAtlas.c_scrollDown, CommonAtlas.c_scrollPoint, CommonAtlas.whitePixel);
				base.AddChild(viewbox);
				viewbox.AddItem(new UiControl[]
				{
					this.{17970}
				});
				return;
			}
			UiControl uiControl2 = this.{17970};
			pos = this.{17970}.Pos;
			uiControl2.Pos = pos.SetHeight(base.Pos.WH.Y - 60f);
		}

		// Token: 0x060005EC RID: 1516 RVA: 0x0002EF3E File Offset: 0x0002D13E
		public void Reload()
		{
			this.{17970}.ReloadTable();
		}

		// Token: 0x060005ED RID: 1517 RVA: 0x0002EF4C File Offset: 0x0002D14C
		protected void BannerHelper(string {17965})
		{
			Form form = new Form(base.Pos, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			Color {13344} = new Color(255, 66, 21) * 0.5764706f;
			form.AddChild({18328}.c_bannerDownRed, new Marker(0f, base.Pos.WH.Y - 60f, base.Pos.WH.X, 60f).Offset(base.Pos.XY));
			form.AddChild(new Label(base.Pos.XY + new Vector2(base.Pos.WH.X / 2f, base.Pos.WH.Y - 33f), Fonts.Philosopher_14, {13344}, {17965}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
			form.EvClick += delegate(ClickUiEventArgs {17992})
			{
				{17992}.Sender.RemoveAnimations();
				new UiBrightnessAnimation({17992}.Sender, 1f, 10f, 300f);
				new UiBrightnessAnimation({17992}.Sender, 10f, 1f, 400f);
			};
			base.AddChild(form);
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0002F060 File Offset: 0x0002D260
		protected void TooltipBannerHelper(string {17966})
		{
			Color lightGray = Color.LightGray;
			base.AddChild({18328}.c_bannerDownRed, new Marker(0f, base.Pos.WH.Y - 60f, base.Pos.WH.X, 30f).Offset(base.Pos.XY));
			base.AddChild(new Label(base.Pos.XY + new Vector2(base.Pos.WH.X / 2f, base.Pos.WH.Y - 33f), Fonts.Philosopher_14, lightGray, {17966}, PositionAlignment.LeftUp, PositionAlignment.LeftUp).Center());
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0002F124 File Offset: 0x0002D324
		public IEnumerable<{18027}.ItemBind> EnumerateItemsAtShip()
		{
			{17956}.<EnumerateItemsAtShip>d__13 <EnumerateItemsAtShip>d__ = new {17956}.<EnumerateItemsAtShip>d__13(-2);
			<EnumerateItemsAtShip>d__.<>4__this = this;
			return <EnumerateItemsAtShip>d__;
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0002F134 File Offset: 0x0002D334
		public IEnumerable<{18027}.ItemBind> EnumerateItemsAtRight()
		{
			{17956}.<EnumerateItemsAtRight>d__14 <EnumerateItemsAtRight>d__ = new {17956}.<EnumerateItemsAtRight>d__14(-2);
			<EnumerateItemsAtRight>d__.<>4__this = this;
			return <EnumerateItemsAtRight>d__;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00017097 File Offset: 0x00015297
		public UiControl AddFooter({18027} {17967}, Marker {17968}, float {17969})
		{
			return null;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00003100 File Offset: 0x00001300
		public void TryMoveAllToShip()
		{
		}

		// Token: 0x04000540 RID: 1344
		public static {17956} CurrentInstance;

		// Token: 0x04000541 RID: 1345
		private {18027} {17970};

		// Token: 0x04000542 RID: 1346
		private {17956}.Storage {17971};

		// Token: 0x04000543 RID: 1347
		private {17956}.Storage {17972};

		// Token: 0x04000544 RID: 1348
		private Action<{17956}.Movement> {17973};

		// Token: 0x04000545 RID: 1349
		protected bool hasBanner;

		// Token: 0x020000F7 RID: 247
		public enum StorageId
		{
			// Token: 0x04000547 RID: 1351
			Left,
			// Token: 0x04000548 RID: 1352
			Right
		}

		// Token: 0x020000F8 RID: 248
		public struct Storage
		{
			// Token: 0x060005F3 RID: 1523 RVA: 0x0002F144 File Offset: 0x0002D344
			public Storage(string {17979}, GSI {17980}, GSI {17981}, bool {17982}, string {17983})
			{
				this.Name = {17979};
				this.Resources = {17980};
				this.Ammo = {17981};
				this.CanMoveFromHere = {17982};
				this.MoveText = {17983};
			}

			// Token: 0x04000549 RID: 1353
			public string Name;

			// Token: 0x0400054A RID: 1354
			public GSI Resources;

			// Token: 0x0400054B RID: 1355
			public GSI Ammo;

			// Token: 0x0400054C RID: 1356
			public bool CanMoveFromHere;

			// Token: 0x0400054D RID: 1357
			public string MoveText;
		}

		// Token: 0x020000F9 RID: 249
		public struct Movement
		{
			// Token: 0x060005F4 RID: 1524 RVA: 0x0002F16B File Offset: 0x0002D36B
			public Movement({17956}.StorageId {17988}, {17956}.StorageId {17989}, IStorageAsset {17990}, int {17991})
			{
				this.From = {17988};
				this.To = {17989};
				this.ID = {17990};
				this.Count = {17991};
			}

			// Token: 0x0400054E RID: 1358
			public {17956}.StorageId From;

			// Token: 0x0400054F RID: 1359
			public {17956}.StorageId To;

			// Token: 0x04000550 RID: 1360
			public IStorageAsset ID;

			// Token: 0x04000551 RID: 1361
			public int Count;
		}
	}
}
