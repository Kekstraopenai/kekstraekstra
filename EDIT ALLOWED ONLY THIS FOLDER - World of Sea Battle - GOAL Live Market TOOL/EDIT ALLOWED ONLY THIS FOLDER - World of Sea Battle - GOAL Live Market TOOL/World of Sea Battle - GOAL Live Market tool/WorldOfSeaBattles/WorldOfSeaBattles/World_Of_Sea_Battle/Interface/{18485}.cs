using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Game;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000147 RID: 327
	internal sealed class {18485} : {17625}
	{
		// Token: 0x0600077A RID: 1914 RVA: 0x0003B7C5 File Offset: 0x000399C5
		public {18485}() : base(1400f, {17625}.c_back1, {17604}.InGameWindowBlockShip, Rectangle.Empty, Array.Empty<{17625}.DynamicTittle>())
		{
			this.{18486}();
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0003B7EC File Offset: 0x000399EC
		private void {18486}()
		{
			base.ComposeTabWithScroll(null, true, true, new Action<ListItemViewControl>[]
			{
				new Action<ListItemViewControl>(this.{18487})
			});
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0003B818 File Offset: 0x00039A18
		[CompilerGenerated]
		private void {18487}(ListItemViewControl {18488})
		{
			{18488}.AddItem(new UiControl[]
			{
				new Form(new Marker(0f, 0f, base.ContentArea.WH.X - 1f, 1f), Rectangle.Empty, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			});
			StackForm stackForm = new StackForm(Vector2.Zero, UiOrientation.ExpansiveSize, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			stackForm.Pos = stackForm.Pos.SetWidth(1400f);
			{18488}.AddItem(new UiControl[]
			{
				stackForm
			});
			Vector2 vector = default(Vector2);
			foreach (ResourceInfo resourceInfo in ((IEnumerable<ResourceInfo>)Gameplay.ItemsInfo))
			{
				if (resourceInfo.AllowStorage == ResourceIDStatus.ok)
				{
					Form form = new Form(Vector2.Zero, {18417}.c_pageItem, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
					form.AllowDragDrop = true;
					form.AddChild(new Image(new Marker(5f, 4f, 59f, 59f), resourceInfo.IconTexture, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					form.AddChild(new Label(new Vector2(65f, 5f), Fonts.Philosopher_16, Color.White * 0.9f, resourceInfo.Name, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					form.AddChild(new Label(new Vector2(65f, 25f), Fonts.Arial_12, Color.Yellow, resourceInfo.MediumCost.Value.ToString() ?? "", PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					form.AddChild(new Label(new Vector2(65f, 45f), Fonts.Arial_12, Color.Cyan, resourceInfo.Mass.ToString() ?? "", PositionAlignment.LeftUp, PositionAlignment.LeftUp));
					{20431}.Set(form, resourceInfo, 0, null);
					int num = 0;
					foreach (WosbCrafting.Recepie recepie in WosbCrafting.Workshop)
					{
						if (recepie.Output == resourceInfo)
						{
							float num2 = recepie.Craft.InputItems.ResourceInfo.Sum((GSILocalEnumerablePair<ResourceInfo> {18489}) => {18489}.Info.Mass * (float){18489}.Count);
							int num3 = recepie.Craft.InputItems.ResourceInfo.Sum((GSILocalEnumerablePair<ResourceInfo> {18490}) => {18490}.Info.MediumCost.Value * {18490}.Count);
							form.AddChild(new Label(new Vector2((float)(165 + num * 60), 25f), Fonts.Arial_12, Color.Yellow * 0.5f, num3.ToString() ?? "", PositionAlignment.LeftUp, PositionAlignment.LeftUp));
							form.AddChild(new Label(new Vector2((float)(165 + num * 60), 45f), Fonts.Arial_12, Color.Cyan * 0.5f, num2.ToString() ?? "", PositionAlignment.LeftUp, PositionAlignment.LeftUp));
							num++;
						}
					}
					UiControl uiControl = form;
					Marker pos = form.Pos;
					Vector2 vector2 = vector * {18417}.c_pageItem.WidthHeight();
					uiControl.Pos = pos.SetXY(vector2);
					stackForm.AddItem(new UiControl[]
					{
						form
					});
					vector.X += 1f;
					if (vector.X >= 3f)
					{
						vector.X = 0f;
						vector.Y += 1f;
					}
				}
			}
		}
	}
}
