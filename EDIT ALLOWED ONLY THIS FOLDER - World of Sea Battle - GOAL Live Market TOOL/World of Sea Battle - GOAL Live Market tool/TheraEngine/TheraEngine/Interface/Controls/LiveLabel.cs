using System;
using Microsoft.Xna.Framework;
using TheraEngine.Assets.Content;

namespace TheraEngine.Interface.Controls
{
	// Token: 0x020000AF RID: 175
	public class LiveLabel : Label
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x00017B30 File Offset: 0x00015D30
		public LiveLabel(float {13404}, float {13405}, CustomSpriteFont {13406}, Color {13407}, Func<string> {13408}, int {13409}) : this(new Vector2((float)((int){13404}), (float)((int){13405})), {13406}, {13407}, null, (object {13437}) => {13408}(), {13409})
		{
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x00017B70 File Offset: 0x00015D70
		public LiveLabel(Vector2 {13410}, CustomSpriteFont {13411}, Color {13412}, Func<string> {13413}, int {13414}) : this({13410}, {13411}, {13412}, null, (object {13438}) => {13413}(), {13414})
		{
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x00017BA4 File Offset: 0x00015DA4
		public LiveLabel(Vector2 {13415}, CustomSpriteFont {13416}, Color {13417}, Func<LiveLabel, string> {13418}, int {13419}) : this({13415}, {13416}, {13417}, null, (object {13436}) => "", {13419})
		{
			LiveLabel <>4__this = this;
			this.GetText = ((object {13439}) => {13418}(<>4__this));
			base.Text = this.GetText(this.CaptureContext);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00017C1C File Offset: 0x00015E1C
		public LiveLabel(Vector2 {13420}, CustomSpriteFont {13421}, Color {13422}, Color {13423}, string {13424}, Func<int> {13425}, int {13426}) : this({13420}, {13421}, {13422}, () => "", {13426})
		{
			LiveLabel <>4__this = this;
			this.GetText = delegate(object {13440})
			{
				if ({13425}() == 0)
				{
					<>4__this.BasicColor = {13422};
				}
				else
				{
					<>4__this.BasicColor = {13423};
				}
				return {13424} + {13425}().ToString();
			};
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00017C96 File Offset: 0x00015E96
		public LiveLabel(Vector2 {13427}, CustomSpriteFont {13428}, Color {13429}, object {13430}, Func<object, string> {13431}, int {13432}) : base({13427}, {13428}, {13429}, {13431}({13430}), PositionAlignment.LeftUp, PositionAlignment.LeftUp)
		{
			this.CaptureContext = {13430};
			this.GetText = {13431};
			this.UpdateInetrvalMs = {13432};
			this.{13435} = 1f;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00017CCF File Offset: 0x00015ECF
		internal override void Update(ref FrameTime {13433}, ref int {13434})
		{
			if ({13433}.EvaluteTimerMs2(ref this.{13435}))
			{
				this.{13435} = (float)this.UpdateInetrvalMs;
				base.Text = this.GetText(this.CaptureContext);
			}
			base.Update(ref {13433}, ref {13434});
		}

		// Token: 0x04000375 RID: 885
		public object CaptureContext;

		// Token: 0x04000376 RID: 886
		public Func<object, string> GetText;

		// Token: 0x04000377 RID: 887
		public int UpdateInetrvalMs;

		// Token: 0x04000378 RID: 888
		private float {13435};
	}
}
