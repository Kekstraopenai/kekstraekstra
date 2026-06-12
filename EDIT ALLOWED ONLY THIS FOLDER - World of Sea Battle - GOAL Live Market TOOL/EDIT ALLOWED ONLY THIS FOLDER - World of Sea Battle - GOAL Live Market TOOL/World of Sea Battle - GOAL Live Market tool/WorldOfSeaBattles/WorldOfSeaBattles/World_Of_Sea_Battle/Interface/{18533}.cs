using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x0200014B RID: 331
	internal sealed class {18533} : {17068}
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x0003C634 File Offset: 0x0003A834
		public {18533}(string {18537}, Func<IslePortInfo, bool> {18538}, Action<IslePortInfo, {18533}> {18539})
		{
			Vector2 vector = Engine.GS.UIArea.HalfWidthHeightInt() - new Vector2(350f, 650f) / 2f;
			Vector2 vector2 = new Vector2(350f, 650f);
			base..ctor(new Marker(ref vector, ref vector2), {17625}.c_back1, {17068}.BlockingWay.BackgroundClosing, false);
			{18533} <>4__this = this;
			if (!Global.Player.IsPortEntry)
			{
				throw new NotSupportedException();
			}
			StackForm stackForm = new StackForm(base.Pos.XY + new Vector2(7f, 5f), UiOrientation.Vertical, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				BorderThickness = -2f
			};
			stackForm.AddItem(new UiControl[]
			{
				TextBlockBuilder.CreateBlock(base.Pos.WH.X - 60f, {18537}, Color.LightGray, Fonts.Arial_10, -2f).Create()
			});
			stackForm.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, 1f, 20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			using (IEnumerator<IslePortInfo> enumerator = (from {18540} in Gameplay.WorldMap.Ports.Where({18538})
			orderby {18540}.PortNameShort
			select {18540}).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IslePortInfo port = enumerator.Current;
					bool flag = Global.Player.NearPort == port && Global.Player.NearPortType == PortEnteringType.Port;
					stackForm.AddItem(new UiControl[]
					{
						new LabelButton(Vector2.Zero, flag ? Local.current_port(port.PortNameShort) : port.PortNameShort, Fonts.Arial_10, Color.Gray, Color.White, flag ? null : new Action<ClickUiEventArgs>(delegate(ClickUiEventArgs {18541})
						{
							{18539}(port, <>4__this);
						}))
					});
				}
			}
			stackForm.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, 1f, 20f), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			base.AddChild(stackForm);
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x000201CC File Offset: 0x0001E3CC
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
		}
	}
}
