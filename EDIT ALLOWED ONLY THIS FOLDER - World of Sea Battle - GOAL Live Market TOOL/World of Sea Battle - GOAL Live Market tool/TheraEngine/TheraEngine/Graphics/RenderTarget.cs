using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Collections;

namespace TheraEngine.Graphics
{
	// Token: 0x02000148 RID: 328
	public class RenderTarget : DisposableObject, IRenderTarget
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x0002CD6D File Offset: 0x0002AF6D
		public static long SummarySizeInBytes
		{
			get
			{
				return RenderTarget.allocatedMemoryAll;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x0002CD74 File Offset: 0x0002AF74
		public static int ActiveCount
		{
			get
			{
				return RenderTarget.aciveCount;
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x0002CD7B File Offset: 0x0002AF7B
		public RenderTarget2D Resource
		{
			get
			{
				return this.{15067};
			}
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0002CD83 File Offset: 0x0002AF83
		public bool IsCurrent
		{
			get
			{
				return this.{15068};
			}
		}

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x0002CD8B File Offset: 0x0002AF8B
		public RenderTargetBinding[] Targets
		{
			get
			{
				return this._set;
			}
		}

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0002CD93 File Offset: 0x0002AF93
		public int MultisamplersCount
		{
			get
			{
				return this.{15070};
			}
		}

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600092C RID: 2348 RVA: 0x0002CD9B File Offset: 0x0002AF9B
		public long SizeInBytes
		{
			get
			{
				return (long)this.{15067}.SizeInBytes;
			}
		}

		// Token: 0x0600092D RID: 2349 RVA: 0x0002CDAC File Offset: 0x0002AFAC
		public RenderTarget(int {15053}, int {15054}, SurfaceFormat {15055}, DepthFormat {15056}, int {15057}, bool {15058}, string {15059}, bool {15060} = false)
		{
			if (Engine.Game == null || Engine.GS == null)
			{
				this.Dispose();
				return;
			}
			this.{15067} = new RenderTarget2D(Engine.GS.graphicsDevice, {15053}, {15054}, {15060}, {15055}, {15056}, {15057}, {15058} ? RenderTargetUsage.DiscardContents : RenderTargetUsage.PreserveContents);
			this.DiscardContentInReset = {15058};
			this.{15067}.Disposing += this.{15064};
			this.Size = new Vector2((float){15053}, (float){15054});
			this.PixelSize = new Vector2(1f / this.Size.X, 1f / this.Size.Y);
			this.HalfPixel = new Vector2(-0.5f / this.Size.X, 0.5f / this.Size.Y);
			this.Bounds = this.{15067}.Bounds;
			this.PixelFormat = {15055};
			this.DepthBufferFormat = {15056};
			this.MipMaps = {15060};
			this.{15070} = {15057};
			this.AssetName = {15059};
			RenderTarget.ActiveRenderTargets.Add(this);
			RenderTargetBinding renderTargetBinding = new RenderTargetBinding(this.{15067})
			{
				RenderTarget = 
				{
					Tag = this
				}
			};
			this._set = new RenderTargetBinding[]
			{
				renderTargetBinding
			};
			RenderTarget.allocatedMemoryAll += this.SizeInBytes;
		}

		// Token: 0x0600092E RID: 2350 RVA: 0x0002CF08 File Offset: 0x0002B108
		public bool Compare(RenderTarget {15061})
		{
			return !{15061}.IsDisposed && (this.Bounds == {15061}.Bounds && this.PixelFormat == {15061}.PixelFormat && this.{15067}.DepthStencilFormat == {15061}.{15067}.DepthStencilFormat && this.{15067}.MultiSampleCount == {15061}.{15067}.MultiSampleCount) && this.MipMaps == {15061}.MipMaps;
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0002CF80 File Offset: 0x0002B180
		public bool CompareSize(RenderTarget {15062})
		{
			return this.Size.X == {15062}.Size.X && this.Size.Y == {15062}.Size.Y;
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0002CFB4 File Offset: 0x0002B1B4
		public RenderTarget Copy(string {15063})
		{
			if (base.IsDisposed)
			{
				throw new ObjectDisposedException("Current RenderTarget");
			}
			return new RenderTarget(this.Bounds.Width, this.Bounds.Height, this.PixelFormat, this.{15067}.DepthStencilFormat, this.{15067}.MultiSampleCount, this.MipMaps, {15063}, false);
		}

		// Token: 0x06000931 RID: 2353 RVA: 0x0002D014 File Offset: 0x0002B214
		public override void Dispose()
		{
			if (this.{15068})
			{
				throw new InvalidOperationException("Невозможно освободить ресурс, в который проиходит отрисовка");
			}
			if (this.{15067} != null && !this.{15067}.IsDisposed)
			{
				this.{15069} = true;
				this.{15067}.Dispose();
				RenderTarget.allocatedMemoryAll -= this.SizeInBytes;
				RenderTarget.aciveCount--;
				RenderTarget.ActiveRenderTargets.Remove(this);
			}
			base.Dispose();
		}

		// Token: 0x06000933 RID: 2355 RVA: 0x0002D0A6 File Offset: 0x0002B2A6
		[CompilerGenerated]
		private void {15064}([Nullable(2)] object {15065}, EventArgs {15066})
		{
			if (this.{15069})
			{
				return;
			}
			this.Dispose();
		}

		// Token: 0x04000635 RID: 1589
		public static readonly Tlist<RenderTarget> ActiveRenderTargets = new Tlist<RenderTarget>(50);

		// Token: 0x04000636 RID: 1590
		private static long allocatedMemoryAll = 0L;

		// Token: 0x04000637 RID: 1591
		private static int aciveCount = 0;

		// Token: 0x04000638 RID: 1592
		private RenderTarget2D {15067};

		// Token: 0x04000639 RID: 1593
		private bool {15068};

		// Token: 0x0400063A RID: 1594
		private bool {15069};

		// Token: 0x0400063B RID: 1595
		internal readonly RenderTargetBinding[] _set;

		// Token: 0x0400063C RID: 1596
		private int {15070};

		// Token: 0x0400063D RID: 1597
		public string AssetName;

		// Token: 0x0400063E RID: 1598
		public readonly Vector2 Size;

		// Token: 0x0400063F RID: 1599
		public readonly Vector2 PixelSize;

		// Token: 0x04000640 RID: 1600
		public readonly Vector2 HalfPixel;

		// Token: 0x04000641 RID: 1601
		public readonly Rectangle Bounds;

		// Token: 0x04000642 RID: 1602
		public readonly SurfaceFormat PixelFormat;

		// Token: 0x04000643 RID: 1603
		public readonly DepthFormat DepthBufferFormat;

		// Token: 0x04000644 RID: 1604
		public readonly bool MipMaps;

		// Token: 0x04000645 RID: 1605
		public readonly bool DiscardContentInReset;
	}
}
