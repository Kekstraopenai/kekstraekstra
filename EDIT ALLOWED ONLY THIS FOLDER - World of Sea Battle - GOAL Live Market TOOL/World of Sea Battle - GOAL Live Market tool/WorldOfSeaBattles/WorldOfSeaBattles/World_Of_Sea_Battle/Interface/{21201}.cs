using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Components;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Constants;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020002F7 RID: 759
	internal class {21201} : CustomUi
	{
		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0008C058 File Offset: 0x0008A258
		public static int OffsetX
		{
			get
			{
				return 300;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060010AA RID: 4266 RVA: 0x0008C05F File Offset: 0x0008A25F
		// (set) Token: 0x060010AB RID: 4267 RVA: 0x0008C067 File Offset: 0x0008A267
		public bool HideDisallowed
		{
			get
			{
				return this.{21237};
			}
			set
			{
				this.{21237} = value;
				this.{21216}();
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060010AC RID: 4268 RVA: 0x0008C076 File Offset: 0x0008A276
		// (set) Token: 0x060010AD RID: 4269 RVA: 0x0008C07E File Offset: 0x0008A27E
		public bool HideNotHaving
		{
			get
			{
				return this.{21238};
			}
			set
			{
				this.{21238} = value;
				this.{21216}();
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060010AE RID: 4270 RVA: 0x0008C08D File Offset: 0x0008A28D
		// (set) Token: 0x060010AF RID: 4271 RVA: 0x0008C095 File Offset: 0x0008A295
		public FractionID? OnlyFraction
		{
			get
			{
				return this.{21239};
			}
			set
			{
				this.{21239} = value;
				this.{21216}();
			}
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x0008C0A4 File Offset: 0x0008A2A4
		public {21201}(float {21208}, float {21209}, {21078} {21210}) : base(new Marker(0f, 0f, 1000f, 700f), CommonAtlas.whitePixel, PositionAlignment.Center, PositionAlignment.Center, Color.Transparent, false)
		{
			this.AnimatedFocus = false;
			this.AllowDragDrop = true;
			base.EvIntegerDrop += this.{21219};
			this.{21234} = {21209};
			this.{21235} = {21208};
			for (int i = 0; i < {21266}.Widths.Length; i++)
			{
				int {278} = 7 - i;
				Label label = new Label(Vector2.Zero, Fonts.Philosopher_14Bold, Color.Wheat, StringHelper.ToRoman({278}) ?? "", PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				new Image(Vector2.Zero, AtlasPortGui.Texture.Tex, {21201}.c_rankDecorator, PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{21229}.Add(label);
				base.AddChild(label);
			}
			base.AddChild(this.{21230} = new Label(Vector2.Zero, Fonts.Philosopher_14, Color.Wheat, Local.PortVerfyPage_PremiumAndUniqShips, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			base.AddChild(this.{21231} = new Image(Vector2.Zero, AtlasPortGui.Texture.Tex, {21201}.c_decoratorLargeLeft, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			base.AddChild(this.{21232} = new Image(Vector2.Zero, AtlasPortGui.Texture.Tex, {21201}.c_decoratorLargeRight, PositionAlignment.LeftUp, PositionAlignment.LeftUp));
			float num = 0f;
			float num2 = 0f;
			foreach ({21201}.LineInfo lineInfo in {21201}.Lines)
			{
				{21266} {21266} = new {21266}(new Marker(0f, num, 100f, lineInfo.Height), lineInfo.ShipIds, {21210}, lineInfo.HasLine);
				if (lineInfo.HasLine)
				{
					{21266}.AddLine();
				}
				num += lineInfo.Height;
				num2 = Math.Max(num2, {21266}.Width);
				this.{21227}.Add({21266});
				base.AddChild({21266}.Form);
			}
			this.{21233} = Vector2.Zero;
			this.{21228} = new Marker(0f, 0f, num2, num + 20f);
			base.Pos = this.{21228}.Scale(this.{21236});
			this.MoveView(new Vector2(0f, {21208}), false);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x0008C304 File Offset: 0x0008A504
		public bool MoveView(Vector2 {21211}, bool {21212} = false)
		{
			if ({21211} == Vector2.Zero && !{21212})
			{
				return false;
			}
			float num = this.{21235} + this.{21234};
			float num2 = (float)Engine.GS.UIArea.Height - base.Pos.WH.Y - this.{21234};
			num2 = Math.Min(num, num2);
			this.{21233} += {21211};
			this.{21233} = new Vector2(((float)Engine.GS.UIArea.Width - base.Pos.WH.X) / 2f, Math.Clamp(this.{21233}.Y, num2, num));
			base.Pos = base.Pos.SetXY(this.{21233});
			return false;
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x0008C3D4 File Offset: 0x0008A5D4
		public float GetScrollProgress()
		{
			float num = this.{21235} + this.{21234};
			float num2 = (float)Engine.GS.UIArea.Height - base.Pos.WH.Y - this.{21234};
			num2 = Math.Min(num, num2);
			if (num - num2 == 0f)
			{
				return 1f;
			}
			return (this.{21233}.Y - num2) / (num - num2);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x0008C444 File Offset: 0x0008A644
		public void SetScrollFromProgress(float {21213})
		{
			float num = this.{21235} + this.{21234};
			float num2 = (float)Engine.GS.UIArea.Height - base.Pos.WH.Y - this.{21234};
			num2 = Math.Min(num, num2);
			float num3 = num2 + (num - num2) * {21213};
			this.MoveView(new Vector2(0f, num3 - this.{21233}.Y), false);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x0008C4B8 File Offset: 0x0008A6B8
		public void Scale(float {21214})
		{
			{21201}.<>c__DisplayClass33_0 CS$<>8__locals1;
			CS$<>8__locals1.<>4__this = this;
			{21214} *= Math.Clamp((float)Engine.GS.UIArea.Width / 1080f, 0.5f, 2f);
			if (this.{21236} == {21214})
			{
				return;
			}
			this.{21236} = {21214};
			foreach ({21266} {21266} in this.{21227})
			{
				{21266}.Offset = this.{21233};
				{21266}.Scale({21214});
			}
			float num = 0f;
			for (int i = 0; i < {21266}.Widths.Length; i++)
			{
				int num2 = {21266}.Widths[i];
				Label label = this.{21229}[i];
				num += (float)num2;
				Vector2 vector = this.{21233} + new Vector2((float){21201}.OffsetX + num - (float)(num2 / 2), 10f) * {21214};
				label.ScaleOfCentr = {21214};
				label.Pos = label.Pos.SetXY(vector);
				label.Center();
				Vector2 vector2 = {21201}.c_rankDecorator.WidthHeight() * {21214} * 0.35f;
				new Vector2(-vector2.X / 2f, 3f * {21214});
			}
			Marker pos = base.Pos;
			Vector2 vector3 = this.{21228}.WH * {21214};
			base.Pos = pos.Resize(vector3);
			this.MoveView(new Vector2(this.{21233}.X - this.{21233}.X * {21214}, 0f), false);
			CS$<>8__locals1.headerScale = {21214} * 0.8f;
			Marker marker = base.Pos.SetHeight(50f * CS$<>8__locals1.headerScale).Offset(0f, 550f * {21214});
			this.{21230}.ScaleOfCentr = CS$<>8__locals1.headerScale;
			this.{21230}.Pos = marker.Offset(base.Pos.WH.X / 2f, -5f * {21214});
			this.{21230}.CenterX();
			this.{21231}.Pos = this.{21221}({21201}.c_decoratorLargeLeft, -1, ref CS$<>8__locals1);
			this.{21232}.Pos = this.{21221}({21201}.c_decoratorLargeRight, 1, ref CS$<>8__locals1);
			float num3 = (float)Engine.GS.UIArea.Width - base.Pos.WH.X - this.{21234};
			if (num3 > 0f)
			{
				base.Pos = base.Pos.SetWidth(base.Pos.WH.X + num3);
				foreach (UiControl uiControl in ((IEnumerable<UiControl>)base.GetChildren))
				{
					uiControl.Pos = uiControl.Pos.Offset(num3 / 2f, 0f);
				}
			}
			this.MoveView(Vector2.Zero, true);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x0008C7F0 File Offset: 0x0008A9F0
		public void ShowStats(bool {21215})
		{
			this.ForEachCard(delegate({21111} {21265})
			{
				{21265}.ShowStats = {21215};
			});
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x0008C81C File Offset: 0x0008AA1C
		private void {21216}()
		{
			this.ForEachCard(new Action<{21111}>(this.{21225}));
			foreach ({21266} {21266} in this.{21227})
			{
				{21266}.HideLineParts(this.HideDisallowed, this.OnlyFraction, this.HideNotHaving);
			}
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x0008C890 File Offset: 0x0008AA90
		protected override void UserUpdate(ref FrameTime {21217})
		{
			this.ForEachCard(delegate({21111} {21264})
			{
				{21264}.Update();
			});
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000201BB File Offset: 0x0001E3BB
		protected override void UserBackRender()
		{
			Engine.GS.SetTexture(CommonAtlas.Texture);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000201CC File Offset: 0x0001E3CC
		protected override void UserFrontRender()
		{
			Engine.GS.ReturnBackTexture();
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x0008C8B8 File Offset: 0x0008AAB8
		private void ForEachCard(Action<{21111}> {21218})
		{
			foreach ({21266} {21266} in this.{21227})
			{
				foreach ({21111} {21111} in {21266}.Ships)
				{
					if ({21111} != null)
					{
						{21218}({21111});
					}
				}
			}
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0008CB2A File Offset: 0x0008AD2A
		[CompilerGenerated]
		private bool {21219}(Vector2 {21220})
		{
			return this.MoveView({21220}, false);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x0008CB34 File Offset: 0x0008AD34
		[CompilerGenerated]
		private Marker {21221}(Rectangle {21222}, int {21223}, ref {21201}.<>c__DisplayClass33_0 {21224})
		{
			Vector2 vector = Vector2.Zero;
			Marker marker = new Marker(ref vector, ref {21222}).ScaleSize({21224}.headerScale).ScaleSize(0.3f);
			vector = this.{21230}.Pos.Center;
			Marker marker2 = marker.Offset(vector).Offset(-marker.WH.X / 2f, 0f);
			Vector2 vector2 = new Vector2((float)({21223} * 225), -16f) * {21224}.headerScale;
			return marker2.Offset(vector2);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x0008CBD0 File Offset: 0x0008ADD0
		[CompilerGenerated]
		private void {21225}({21111} {21226})
		{
			if (this.OnlyFraction != null)
			{
				FractionID fraction = {21226}.Ship.Fraction;
				FractionID? onlyFraction = this.OnlyFraction;
				if (!(fraction == onlyFraction.GetValueOrDefault() & onlyFraction != null))
				{
					goto IL_6B;
				}
			}
			bool hidden;
			if (!this.HideDisallowed || !{21226}.IsDisallowInCurrentPort)
			{
				hidden = (this.HideNotHaving && !Session.Account.Shipyard.ContainsInfo({21226}.Ship));
				goto IL_6C;
			}
			IL_6B:
			hidden = true;
			IL_6C:
			{21226}.Hidden = hidden;
			{21226}.Form.Opacity = ({21226}.Hidden ? 0.6f : 1f);
			{21226}.Form.Brightness = ({21226}.Hidden ? 0.5f : 1f);
			{21226}.Form.AnimatedFocus = !{21226}.Hidden;
		}

		// Token: 0x04000F41 RID: 3905
		public static readonly Rectangle c_rankDecorator = new Rectangle(983, 624, 88, 45);

		// Token: 0x04000F42 RID: 3906
		public static readonly Rectangle c_decoratorLargeLeft = new Rectangle(325, 1628, 571, 107);

		// Token: 0x04000F43 RID: 3907
		public static readonly Rectangle c_decoratorLargeRight = new Rectangle(1191, 1628, 571, 107);

		// Token: 0x04000F44 RID: 3908
		private static readonly {21201}.LineInfo[] Lines = new {21201}.LineInfo[]
		{
			new {21201}.LineInfo(20f, false, Array.Empty<int>()),
			new {21201}.LineInfo(80f, true, new int[]
			{
				2,
				27,
				24,
				58,
				57,
				50,
				0
			}),
			new {21201}.LineInfo(80f, true, new int[]
			{
				6,
				4,
				22,
				11,
				19,
				41,
				31
			}),
			new {21201}.LineInfo(80f, true, new int[]
			{
				15,
				9,
				10,
				17,
				53,
				32,
				49
			}),
			new {21201}.LineInfo(80f, true, new int[]
			{
				0,
				8,
				59,
				25,
				44,
				52,
				55
			}),
			new {21201}.LineInfo(80f, true, new int[]
			{
				0,
				5,
				47,
				0,
				40,
				54,
				60
			}),
			new {21201}.LineInfo(80f, true, new int[]
			{
				0,
				56,
				12,
				14,
				35,
				62,
				65
			}),
			new {21201}.LineInfo(80f, false, Array.Empty<int>()),
			new {21201}.LineInfo(80f, false, new int[]
			{
				0,
				63,
				18,
				7,
				20,
				28,
				36
			}),
			new {21201}.LineInfo(80f, false, new int[]
			{
				0,
				16,
				71,
				13,
				68,
				39,
				61
			}),
			new {21201}.LineInfo(80f, false, new int[]
			{
				0,
				3,
				23,
				67,
				69,
				70,
				46
			}),
			new {21201}.LineInfo(80f, false, new int[]
			{
				0,
				0,
				72,
				34,
				26,
				38,
				0
			}),
			new {21201}.LineInfo(80f, false, new int[]
			{
				0,
				0,
				0,
				74,
				73,
				75,
				0
			})
		};

		// Token: 0x04000F45 RID: 3909
		private readonly List<{21266}> {21227} = new List<{21266}>();

		// Token: 0x04000F46 RID: 3910
		private readonly Marker {21228};

		// Token: 0x04000F47 RID: 3911
		private readonly List<Label> {21229} = new List<Label>();

		// Token: 0x04000F48 RID: 3912
		private readonly Label {21230};

		// Token: 0x04000F49 RID: 3913
		private readonly Image {21231};

		// Token: 0x04000F4A RID: 3914
		private readonly Image {21232};

		// Token: 0x04000F4B RID: 3915
		private Vector2 {21233};

		// Token: 0x04000F4C RID: 3916
		private float {21234};

		// Token: 0x04000F4D RID: 3917
		private float {21235};

		// Token: 0x04000F4E RID: 3918
		private float {21236};

		// Token: 0x04000F4F RID: 3919
		private bool {21237};

		// Token: 0x04000F50 RID: 3920
		private bool {21238};

		// Token: 0x04000F51 RID: 3921
		private FractionID? {21239};

		// Token: 0x020002F8 RID: 760
		public class LineInfo : IEquatable<{21201}.LineInfo>
		{
			// Token: 0x060010BF RID: 4287 RVA: 0x0008CCA0 File Offset: 0x0008AEA0
			public LineInfo(float {21244}, bool {21245}, params int[] {21246})
			{
				this.Height = {21244};
				this.HasLine = {21245};
				this.ShipIds = {21246};
				base..ctor();
			}

			// Token: 0x17000168 RID: 360
			// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0008CCBD File Offset: 0x0008AEBD
			[Nullable(1)]
			[CompilerGenerated]
			protected virtual Type EqualityContract
			{
				[NullableContext(1)]
				[CompilerGenerated]
				get
				{
					return typeof({21201}.LineInfo);
				}
			}

			// Token: 0x17000169 RID: 361
			// (get) Token: 0x060010C1 RID: 4289 RVA: 0x0008CCC9 File Offset: 0x0008AEC9
			// (set) Token: 0x060010C2 RID: 4290 RVA: 0x0008CCD1 File Offset: 0x0008AED1
			public float Height { get; set; }

			// Token: 0x1700016A RID: 362
			// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0008CCDA File Offset: 0x0008AEDA
			// (set) Token: 0x060010C4 RID: 4292 RVA: 0x0008CCE2 File Offset: 0x0008AEE2
			public bool HasLine { get; set; }

			// Token: 0x1700016B RID: 363
			// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0008CCEB File Offset: 0x0008AEEB
			// (set) Token: 0x060010C6 RID: 4294 RVA: 0x0008CCF3 File Offset: 0x0008AEF3
			public int[] ShipIds { get; set; }

			// Token: 0x060010C7 RID: 4295 RVA: 0x0008CCFC File Offset: 0x0008AEFC
			[NullableContext(1)]
			[CompilerGenerated]
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("LineInfo");
				stringBuilder.Append(" { ");
				if (this.PrintMembers(stringBuilder))
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.Append('}');
				return stringBuilder.ToString();
			}

			// Token: 0x060010C8 RID: 4296 RVA: 0x0008CD48 File Offset: 0x0008AF48
			[NullableContext(1)]
			[CompilerGenerated]
			protected virtual bool PrintMembers(StringBuilder {21250})
			{
				RuntimeHelpers.EnsureSufficientExecutionStack();
				{21250}.Append("Height = ");
				{21250}.Append(this.Height.ToString());
				{21250}.Append(", HasLine = ");
				{21250}.Append(this.HasLine.ToString());
				{21250}.Append(", ShipIds = ");
				{21250}.Append(this.ShipIds);
				return true;
			}

			// Token: 0x060010C9 RID: 4297 RVA: 0x0008CDC2 File Offset: 0x0008AFC2
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator !=({21201}.LineInfo {21251}, {21201}.LineInfo {21252})
			{
				return !({21251} == {21252});
			}

			// Token: 0x060010CA RID: 4298 RVA: 0x0008CDCE File Offset: 0x0008AFCE
			[NullableContext(2)]
			[CompilerGenerated]
			public static bool operator ==({21201}.LineInfo {21253}, {21201}.LineInfo {21254})
			{
				return {21253} == {21254} || ({21253} != null && {21253}.Equals({21254}));
			}

			// Token: 0x060010CB RID: 4299 RVA: 0x0008CDE4 File Offset: 0x0008AFE4
			[CompilerGenerated]
			public override int GetHashCode()
			{
				return ((EqualityComparer<Type>.Default.GetHashCode(this.EqualityContract) * -1521134295 + EqualityComparer<float>.Default.GetHashCode(this.{21261})) * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(this.{21262})) * -1521134295 + EqualityComparer<int[]>.Default.GetHashCode(this.{21263});
			}

			// Token: 0x060010CC RID: 4300 RVA: 0x0008CE46 File Offset: 0x0008B046
			[NullableContext(2)]
			[CompilerGenerated]
			public override bool Equals(object {21255})
			{
				return this.Equals({21255} as {21201}.LineInfo);
			}

			// Token: 0x060010CD RID: 4301 RVA: 0x0008CE54 File Offset: 0x0008B054
			[NullableContext(2)]
			[CompilerGenerated]
			public virtual bool Equals({21201}.LineInfo {21256})
			{
				return this == {21256} || ({21256} != null && this.EqualityContract == {21256}.EqualityContract && EqualityComparer<float>.Default.Equals(this.{21261}, {21256}.{21261}) && EqualityComparer<bool>.Default.Equals(this.{21262}, {21256}.{21262}) && EqualityComparer<int[]>.Default.Equals(this.{21263}, {21256}.{21263}));
			}

			// Token: 0x060010CF RID: 4303 RVA: 0x0008CECD File Offset: 0x0008B0CD
			[CompilerGenerated]
			protected LineInfo([Nullable(1)] {21201}.LineInfo {21257})
			{
				this.Height = {21257}.{21261};
				this.HasLine = {21257}.{21262};
				this.ShipIds = {21257}.{21263};
			}

			// Token: 0x060010D0 RID: 4304 RVA: 0x0008CEF9 File Offset: 0x0008B0F9
			[CompilerGenerated]
			public void Deconstruct(out float {21258}, out bool {21259}, out int[] {21260})
			{
				{21258} = this.Height;
				{21259} = this.HasLine;
				{21260} = this.ShipIds;
			}

			// Token: 0x04000F52 RID: 3922
			[CompilerGenerated]
			private readonly float {21261};

			// Token: 0x04000F53 RID: 3923
			[CompilerGenerated]
			private readonly bool {21262};

			// Token: 0x04000F54 RID: 3924
			[CompilerGenerated]
			private readonly int[] {21263};
		}
	}
}
