using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000C9 RID: 201
	public class BlocksStackFormControl : UiControl
	{
		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0001B9DF File Offset: 0x00019BDF
		// (set) Token: 0x06000535 RID: 1333 RVA: 0x0001B9E7 File Offset: 0x00019BE7
		public int MaxItemsCountInLine
		{
			get
			{
				return this.{13826};
			}
			set
			{
				if (value < 1)
				{
					throw new ArgumentOutOfRangeException();
				}
				this.{13826} = value;
				this.{13819}();
			}
		}

		// Token: 0x06000536 RID: 1334 RVA: 0x0001BA00 File Offset: 0x00019C00
		public BlocksStackFormControl(Vector2 {13811}, int {13812}, PositionAlignment {13813} = PositionAlignment.LeftUp, PositionAlignment {13814} = PositionAlignment.LeftUp)
		{
			Vector2 zero = Vector2.Zero;
			base..ctor(new Marker(ref {13811}, ref zero), {13813}, {13814}, Color.White, false);
			this.{13826} = {13812};
			this.{13824} = 0f;
			this.{13823} = 0f;
			this.{13825} = 0;
			this.{13827} = 0f;
			base.SomethingWasRemoved += this.{13822};
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x00016B89 File Offset: 0x00014D89
		internal override void Update(ref FrameTime {13815}, ref int {13816})
		{
			base.Update(ref {13815}, ref {13816});
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x0001BA77 File Offset: 0x00019C77
		internal override void Render()
		{
			base.Render();
		}

		// Token: 0x06000539 RID: 1337 RVA: 0x0001BA80 File Offset: 0x00019C80
		public void AddItem(params UiControl[] {13817})
		{
			foreach (UiControl uiControl in {13817})
			{
				uiControl.PositionAlignment_X = PositionAlignment.LeftUp;
				uiControl.PositionAlignment_Y = PositionAlignment.LeftUp;
				if (!this.HorizontalCentered)
				{
					this.{13820}(uiControl);
				}
			}
			base.AddChild({13817});
			base.OnDynamicStorageStateChanged();
		}

		// Token: 0x0600053A RID: 1338 RVA: 0x0001724A File Offset: 0x0001544A
		public void RemoveAt(UiControl {13818})
		{
			base.RemoveAt({13818}, true);
		}

		// Token: 0x0600053B RID: 1339 RVA: 0x0001BACC File Offset: 0x00019CCC
		public void Clear()
		{
			base.ClearAllChild();
			ref Marker ptr = ref base.Pos;
			Vector2 zero = Vector2.Zero;
			base.Pos = new Marker(ref ptr.XY, ref zero);
			this.{13825} = 0;
			this.{13827} = 0f;
			this.{13824} = 0f;
			this.{13823} = 0f;
			base.OnDynamicStorageStateChanged();
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected override bool AllowResize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000DD52 File Offset: 0x0000BF52
		internal override bool IsDynamicStorage
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600053E RID: 1342 RVA: 0x0001BB2E File Offset: 0x00019D2E
		public void ForceRebuild()
		{
			this.{13819}();
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001BB38 File Offset: 0x00019D38
		private void {13819}()
		{
			Marker pos = base.Pos;
			Vector2 zero = Vector2.Zero;
			base.Pos = new Marker(ref pos.XY, ref zero);
			this.{13825} = 0;
			this.{13827} = 0f;
			this.{13824} = 0f;
			this.{13823} = 0f;
			Tlist<UiControl> getChildren = base.GetChildren;
			float[] array = null;
			if (this.HorizontalCentered)
			{
				array = new float[(int)Math.Ceiling((double)getChildren.Size / (double)this.{13826})];
			}
			for (int i = 0; i < getChildren.Size; i++)
			{
				this.{13820}(getChildren.Array[i]);
				if (this.HorizontalCentered)
				{
					array[(int)Math.Floor((double)i / (double)this.{13826})] += getChildren.Array[i].Pos.WH.X + this.BorderThickness;
				}
			}
			if (this.HorizontalCentered)
			{
				for (int j = 0; j < array.Length; j++)
				{
					array[j] = (this.MaxWidth - array[j]) * 0.5f;
				}
				for (int k = 0; k < getChildren.Size; k++)
				{
					int num = (int)Math.Floor((double)k / (double)this.{13826});
					UiControl uiControl = getChildren.Array[k];
					pos = getChildren.Array[k].Pos;
					uiControl.Pos = pos.SetX(getChildren.Array[k].Pos.XY.X + array[num]);
				}
			}
			base.OnDynamicStorageStateChanged();
		}

		// Token: 0x06000540 RID: 1344 RVA: 0x0001BCC4 File Offset: 0x00019EC4
		private void {13820}(UiControl {13821})
		{
			if (this.{13825} == this.{13826} || this.{13824} + {13821}.Pos.WH.X > this.MaxWidth)
			{
				this.{13825} = 0;
				this.{13824} = 0f;
				this.{13823} += this.{13827};
				this.{13827} = 0f;
			}
			this.{13827} = MathHelper.Max(this.{13827}, {13821}.Pos.WH.Y + this.BorderThickness);
			float {11527} = base.Pos.XY.Y + this.{13823};
			float {11526} = base.Pos.XY.X + this.{13824};
			this.{13824} += {13821}.Pos.WH.X + this.BorderThickness;
			this.{13825}++;
			Marker marker = new Marker({11526}, {11527}, {13821}.Pos.WH.X, {13821}.Pos.WH.Y);
			Marker pos = base.Pos;
			base.Pos = new Marker(ref pos, 0f, 0f, MathHelper.Max(base.Pos.WH.X, this.{13824}), MathHelper.Max(base.Pos.WH.Y, this.{13823} + marker.WH.Y));
			{13821}.Pos = marker;
		}

		// Token: 0x06000541 RID: 1345 RVA: 0x0001BE48 File Offset: 0x0001A048
		public void NewLine()
		{
			this.{13825} = this.{13826};
		}

		// Token: 0x06000542 RID: 1346 RVA: 0x0001BB2E File Offset: 0x00019D2E
		[CompilerGenerated]
		private void {13822}()
		{
			this.{13819}();
		}

		// Token: 0x04000405 RID: 1029
		public float MaxWidth = float.MaxValue;

		// Token: 0x04000406 RID: 1030
		public float BorderThickness;

		// Token: 0x04000407 RID: 1031
		public bool HorizontalCentered;

		// Token: 0x04000408 RID: 1032
		private float {13823};

		// Token: 0x04000409 RID: 1033
		private float {13824};

		// Token: 0x0400040A RID: 1034
		private int {13825};

		// Token: 0x0400040B RID: 1035
		private int {13826};

		// Token: 0x0400040C RID: 1036
		private float {13827};
	}
}
