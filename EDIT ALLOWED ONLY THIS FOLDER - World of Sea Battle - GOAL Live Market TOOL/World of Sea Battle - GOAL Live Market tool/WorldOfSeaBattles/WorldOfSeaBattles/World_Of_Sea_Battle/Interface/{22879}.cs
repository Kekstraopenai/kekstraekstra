using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000400 RID: 1024
	internal sealed class {22879} : CustomUi
	{
		// Token: 0x170001BC RID: 444
		// (get) Token: 0x0600163F RID: 5695 RVA: 0x000BB589 File Offset: 0x000B9789
		private static float itemSize
		{
			get
			{
				return 22f;
			}
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x000BB590 File Offset: 0x000B9790
		public {22879}(GSI {22881}) : base(new Marker((float)Engine.GS.UIArea.Width - {22879}.cWidth * 2f, 0f, {22879}.cWidth * 2f, 70f + (float)(({22881}.NamesCount + 1) / 2) * {22879}.itemSize), new Rectangle(1077, 339, 130, 197), PositionAlignment.RightDown, PositionAlignment.RightDown, Color.White, false)
		{
			base.Pos = base.Pos.SetY((float)Engine.GS.UIArea.Height * 0.5f - base.Pos.WH.Y * 0.5f);
			this.AnimatedFocus = false;
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(8f, 0f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			StackForm stackForm2 = new StackForm(base.Pos.XY + new Vector2(8f + {22879}.cWidth, 0f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.AddSpace({22879}.itemSize * 0.25f);
			stackForm2.AddSpace({22879}.itemSize * 0.25f);
			stackForm.AddItem(new UiControl[]
			{
				new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat, Local.total_resources_wm, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			stackForm2.AddSpace(20f);
			int num = 0;
			foreach (GSILocalEnumerablePair<ResourceInfo> gsilocalEnumerablePair in from {22883} in {22881}.ResourceInfo
			orderby {22883}.Count descending
			select {22883})
			{
				Form form = new Form(new Marker(0f, 0f, {22879}.cWidth, {22879}.itemSize), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				Image {12952} = new Image(new Marker(0f, 0f, {22879}.itemSize, {22879}.itemSize), gsilocalEnumerablePair.Info.IconTexture, gsilocalEnumerablePair.Info.IconTexture.Bounds, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				form.AddChildPos({12952}, PositionAlignment.LeftUp, PositionAlignment.Center, 1f);
				form.AddChildPos(new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat * 0.7f, StringHelper.GetValueOfK(gsilocalEnumerablePair.Count) + " " + gsilocalEnumerablePair.Info.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp), PositionAlignment.LeftUp, PositionAlignment.Center, {22879}.itemSize + 4f);
				if (num++ > {22881}.NamesCount / 2)
				{
					stackForm2.AddItem(new UiControl[]
					{
						form
					});
				}
				else
				{
					stackForm.AddItem(new UiControl[]
					{
						form
					});
				}
				form.ToolTipState = new ToolTipState(gsilocalEnumerablePair.Info.Name, "", Array.Empty<ToolTipCharacteristics>());
				Tlist<ValueTuple<string, int>> tlist = new Tlist<ValueTuple<string, int>>();
				foreach (IslePortInfo islePortInfo in ((IEnumerable<IslePortInfo>)Gameplay.WorldMap.Ports))
				{
					if (Session.Account.ResourcesInPorts.PortHasResources(islePortInfo.PortID, false))
					{
						int num2 = Session.Account.ResourcesInPorts.GetHolder(islePortInfo.PortID)[(int)gsilocalEnumerablePair.Info.ID];
						if (Global.Player.NearPort == islePortInfo)
						{
							num2 += Global.Player.ResourcesOfHold[(int)gsilocalEnumerablePair.Info.ID];
						}
						if (num2 > 0)
						{
							Tlist<ValueTuple<string, int>> tlist2 = tlist;
							ValueTuple<string, int> valueTuple = new ValueTuple<string, int>(islePortInfo.PortName, num2);
							tlist2.Add(valueTuple);
						}
					}
				}
				tlist.SortTop((ValueTuple<string, int> {22884}) => {22884}.Item2);
				bool flag = tlist.Size > 10;
				tlist.Size = Math.Min(10, tlist.Size);
				foreach (ValueTuple<string, int> valueTuple2 in ((IEnumerable<ValueTuple<string, int>>)tlist))
				{
					form.ToolTip.CurrentContent.AppendText(valueTuple2.Item1 + ": " + valueTuple2.Item2.ToString(), Color.Wheat, false, false);
				}
				foreach (PersonalIsleStatus personalIsleStatus in ((IEnumerable<PersonalIsleStatus>)Session.Account.PersonalIsles.Data))
				{
					int num3 = personalIsleStatus.StorageResources[(int)gsilocalEnumerablePair.Info.ID];
					if (num3 > 0)
					{
						form.ToolTip.CurrentContent.AppendText("\"" + personalIsleStatus.Name + "\": " + num3.ToString(), Color.Wheat, false, false);
					}
				}
				if (flag)
				{
					form.ToolTip.CurrentContent.AppendText("...", Color.Wheat, false, false);
				}
			}
			base.AddChild(stackForm);
			base.AddChild(stackForm2);
			base.IsVisible = false;
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserUpdate(ref FrameTime {22882})
		{
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserBackRender()
		{
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x0400140F RID: 5135
		private static float cWidth = 220f;
	}
}
