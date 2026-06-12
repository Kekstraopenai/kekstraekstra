using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002F4 RID: 756
	internal class {21168} : {21102}
	{
		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x0008BA2C File Offset: 0x00089C2C
		private static float SizeModifier
		{
			get
			{
				return 0.3f;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06001095 RID: 4245 RVA: 0x0008BA33 File Offset: 0x00089C33
		private static float MaxPartWidth
		{
			get
			{
				return (float){21168}.c_wave.Width * {21168}.SizeModifier;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x0008BA46 File Offset: 0x00089C46
		private static float VerticalOffset
		{
			get
			{
				return 64f;
			}
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0008BA50 File Offset: 0x00089C50
		public {21168}({21266} {21170}) : base(Marker.Zero)
		{
			base.Form = new Form(this.unscaledMarker, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
			IReadOnlyList<{21111}> ships = {21170}.Ships;
			int[] widths = {21266}.Widths;
			float num = {21170}.UnscaledMarker.XY.X;
			bool flag = false;
			int num2 = -1;
			bool[] array = new bool[ships.Count];
			bool flag2 = false;
			for (int i = ships.Count - 1; i >= 0; i--)
			{
				{21111} {21111} = ships[i];
				if ({21111} != null)
				{
					if (num2 == -1)
					{
						num2 = i;
					}
					if (!{21111}.NotResearched)
					{
						flag2 = true;
					}
				}
				array[i] = flag2;
			}
			bool flag3 = false;
			for (int j = 1; j <= ships.Count; j++)
			{
				{21111} {21178} = ships[j - 1];
				{21111} {21111}2 = (j < ships.Count) ? ships[j] : null;
				float num3 = (j == 1) ? num : (num + (float)widths[j - 1] / 2f);
				float {21176} = ((j < ships.Count) ? (num + (float)widths[j - 1] + (float)widths[j] / 2f) : (num + (float)widths[j - 1] / 2f)) - num3;
				bool flag4 = j == num2;
				bool flag5 = {21111}2 != null && {21111}2.NotResearched;
				bool flag6 = {21111}2 == null;
				bool flag7 = j >= array.Length || !array[j];
				if (flag4 && !flag)
				{
					flag3 = true;
					if (flag5)
					{
						flag = true;
					}
				}
				else if ((flag6 || flag5) && !flag3 && flag7)
				{
					flag = true;
				}
				this.{21174}(num3, {21176}, flag, {21178}, {21111}2);
				num += (float)widths[j - 1];
			}
			this.unscaledMarker = {21170}.UnscaledMarker.SetY({21168}.VerticalOffset).SetHeight(10f);
			base.Form.Pos = this.unscaledMarker;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x0008BC40 File Offset: 0x00089E40
		public void HideParts(bool {21171}, FractionID? {21172}, bool {21173})
		{
			bool flag = false;
			if ({21171})
			{
				flag = !this.{21188}.Any(({21168}.Part {21199}) => {21199}.CardPrev != null && !{21199}.CardPrev.IsDisallowInCurrentPort);
			}
			if ({21173})
			{
				{21168}.Part part = this.{21188}.First(({21168}.Part {21200}) => {21200}.CardPrev != null);
				flag = !Session.Account.Shipyard.ContainsInfo(part.CardPrev.Ship);
			}
			if ({21172} != null && {21172}.GetValueOrDefault() != FractionID.None)
			{
				flag = true;
			}
			for (int i = 0; i < this.{21188}.Count; i++)
			{
				{21168}.Part part2 = this.{21188}[i];
				bool flag2 = false;
				if (part2.CardNext != null)
				{
					flag2 = part2.CardNext.NotResearched;
				}
				bool flag3;
				if (i + 2 < this.{21188}.Count)
				{
					{21111} cardPrev = this.{21188}[i + 2].CardPrev;
					flag3 = (cardPrev != null && cardPrev.Hidden);
				}
				else
				{
					flag3 = false;
				}
				bool flag4 = flag3;
				if ((flag2 || flag4) && !flag)
				{
					flag = true;
				}
				part2.Image.Opacity = (flag ? this.{21190} : 1f);
			}
		}

		// Token: 0x06001099 RID: 4249 RVA: 0x0008BD88 File Offset: 0x00089F88
		private void {21174}(float {21175}, float {21176}, bool {21177}, {21111} {21178}, {21111} {21179})
		{
			if (this.{21189} > 0f)
			{
				float num = Math.Min(this.{21189}, {21176});
				float {21182} = {21168}.MaxPartWidth - this.{21189};
				this.{21180}({21175}, {21182}, num, {21177}, {21178}, {21179});
				{21175} += num;
				this.{21189} -= num;
				{21176} -= num;
			}
			while ({21176} >= {21168}.MaxPartWidth)
			{
				this.{21180}({21175}, 0f, {21168}.MaxPartWidth, {21177}, {21178}, {21179});
				{21176} -= {21168}.MaxPartWidth;
				{21175} += {21168}.MaxPartWidth;
			}
			if ({21176} > 0f)
			{
				this.{21180}({21175}, 0f, {21176}, {21177}, {21178}, {21179});
				this.{21189} = {21168}.MaxPartWidth - {21176};
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0008BE3C File Offset: 0x0008A03C
		private void {21180}(float {21181}, float {21182}, float {21183}, bool {21184}, {21111} {21185}, {21111} {21186})
		{
			Rectangle rectangle = {21184} ? {21168}.c_waveGray : {21168}.c_wave;
			Rectangle {13273} = new Rectangle(rectangle.X + (int){21182}, rectangle.Y, (int){21183}, rectangle.Height);
			Image image = new Image(new Marker({21181}, 0f, {21183}, (float)rectangle.Height * {21168}.SizeModifier), AtlasPortGui.Texture.Tex, {13273}, PositionAlignment.LeftUp, PositionAlignment.LeftUp)
			{
				Opacity = ({21184} ? this.{21190} : 1f)
			};
			base.Form.AddChild(image);
			this.{21188}.Add(new {21168}.Part
			{
				UnscaledPos = image.Pos.Border(0.5f),
				Image = image,
				CardPrev = {21185},
				CardNext = {21186}
			});
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0008BF10 File Offset: 0x0008A110
		protected override void DoScale(float {21187})
		{
			foreach ({21168}.Part part in this.{21188})
			{
				UiControl image = part.Image;
				Marker marker = part.UnscaledPos.Scale({21187});
				Vector2 offset = base.Offset;
				marker = marker.Offset(offset);
				Vector2 vector = this.unscaledMarker.XY * {21187};
				image.Pos = marker.Offset(vector);
			}
		}

		// Token: 0x04000F35 RID: 3893
		private static Rectangle c_wave = new Rectangle(1461, 673, 154, 16);

		// Token: 0x04000F36 RID: 3894
		private static Rectangle c_waveGray = new Rectangle(1461, 691, 154, 16);

		// Token: 0x04000F37 RID: 3895
		private readonly List<{21168}.Part> {21188} = new List<{21168}.Part>();

		// Token: 0x04000F38 RID: 3896
		private float {21189};

		// Token: 0x04000F39 RID: 3897
		private float {21190} = 0.2f;

		// Token: 0x020002F5 RID: 757
		[RequiredMember]
		private readonly struct Part
		{
			// Token: 0x17000160 RID: 352
			// (get) Token: 0x0600109D RID: 4253 RVA: 0x0008BFE0 File Offset: 0x0008A1E0
			// (set) Token: 0x0600109E RID: 4254 RVA: 0x0008BFE8 File Offset: 0x0008A1E8
			[RequiredMember]
			public Marker UnscaledPos { get; set; }

			// Token: 0x17000161 RID: 353
			// (get) Token: 0x0600109F RID: 4255 RVA: 0x0008BFF1 File Offset: 0x0008A1F1
			// (set) Token: 0x060010A0 RID: 4256 RVA: 0x0008BFF9 File Offset: 0x0008A1F9
			[RequiredMember]
			public Image Image { get; set; }

			// Token: 0x17000162 RID: 354
			// (get) Token: 0x060010A1 RID: 4257 RVA: 0x0008C002 File Offset: 0x0008A202
			// (set) Token: 0x060010A2 RID: 4258 RVA: 0x0008C00A File Offset: 0x0008A20A
			[RequiredMember]
			public {21111} CardPrev { get; set; }

			// Token: 0x17000163 RID: 355
			// (get) Token: 0x060010A3 RID: 4259 RVA: 0x0008C013 File Offset: 0x0008A213
			// (set) Token: 0x060010A4 RID: 4260 RVA: 0x0008C01B File Offset: 0x0008A21B
			[RequiredMember]
			public {21111} CardNext { get; set; }

			// Token: 0x04000F3A RID: 3898
			[CompilerGenerated]
			private readonly Marker {21195};

			// Token: 0x04000F3B RID: 3899
			[CompilerGenerated]
			private readonly Image {21196};

			// Token: 0x04000F3C RID: 3900
			[CompilerGenerated]
			private readonly {21111} {21197};

			// Token: 0x04000F3D RID: 3901
			[CompilerGenerated]
			private readonly {21111} {21198};
		}
	}
}
