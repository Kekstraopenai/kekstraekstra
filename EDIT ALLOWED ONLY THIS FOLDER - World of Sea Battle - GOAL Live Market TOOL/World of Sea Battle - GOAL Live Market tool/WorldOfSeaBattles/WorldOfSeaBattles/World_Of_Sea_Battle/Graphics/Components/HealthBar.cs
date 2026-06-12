using System;
using System.Text;
using Common;
using Common.Game;
using Microsoft.Xna.Framework;
using TheraEngine;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.GameObjects;

namespace World_Of_Sea_Battle.Graphics.Components
{
	// Token: 0x0200048C RID: 1164
	internal sealed class HealthBar : IPoolObject
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x000E227A File Offset: 0x000E047A
		public bool HasDamageEffect
		{
			get
			{
				return this.{24216} > 0f;
			}
		}

		// Token: 0x06001986 RID: 6534 RVA: 0x000E228C File Offset: 0x000E048C
		public void Initialize(Ship {24197})
		{
			this.Initialize(() => {24197}.UsedShip.MaxHp, () => {24197}.UsedShip.FirstHP.Summary);
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x000E22C4 File Offset: 0x000E04C4
		public void Initialize(Func<float> {24198}, Func<float> {24199})
		{
			this.{24217} = {24198};
			this.{24218} = {24199};
			this.{24219} = {24199}();
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000E22E0 File Offset: 0x000E04E0
		public void ClearResources()
		{
			this.{24214} = default(Vector2);
			this.{24215} = 0f;
			this.{24219} = 0f;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000E2304 File Offset: 0x000E0504
		public void Update(ref FrameTime {24200})
		{
			if (this.{24215} == 0f)
			{
				this.{24214}.Y = Math.Max(0f, this.{24214}.Y - {24200}.secElapsed * 0.14f);
				if (this.{24214}.Y == 0f)
				{
					this.{24214}.X = Math.Max(0f, this.{24214}.X - {24200}.secElapsed * 0.14f);
				}
			}
			{24200}.EvaluteTimerMs(ref this.{24215});
			{24200}.EvaluteTimerMs(ref this.{24216});
			float num = this.{24218}();
			if (num != this.{24219})
			{
				float num2 = num - this.{24219};
				if (num2 > 0f)
				{
					float num3 = MathHelper.Clamp(num2 / this.{24217}(), 0f, 1f);
					this.{24214}.X = this.{24214}.X - num3;
					if (this.{24214}.X < 0f)
					{
						this.{24214}.Y = Math.Max(0f, this.{24214}.Y + this.{24214}.X);
						this.{24214}.X = 0f;
					}
				}
				if (num2 < 0f)
				{
					float num4 = MathHelper.Clamp(-num2 / this.{24217}(), 0f, 1f);
					this.{24214}.X = this.{24214}.X + num4;
					if (num2 < -1f)
					{
						this.{24215} = 330f;
						this.{24216} = 500f;
					}
				}
				if (this.{24220} > 0f)
				{
					this.{24220} /= this.{24217}();
					this.{24214}.Y = this.{24214}.Y + this.{24220};
					this.{24214}.X = Math.Max(0f, this.{24214}.X - this.{24220});
					this.{24220} = 0f;
				}
				this.{24219} = num;
			}
			this.{24214}.X = Math.Min(1f, this.{24214}.X);
			this.{24214}.Y = Math.Min(1f, this.{24214}.Y);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000E2550 File Offset: 0x000E0750
		public Vector2? RenderScaled(bool {24201}, Vector3 {24202}, HealthBarStyle {24203}, float {24204}, Vector2 {24205}, out float {24206})
		{
			float num = Vector3.Distance({24202}, Engine.GS.Camera.Position);
			if (num < 500f && Engine.GS.Camera.IsVisible({24202}, 1f))
			{
				if (num > 150f)
				{
					float num2 = (num - 150f) / 350f;
				}
				Vector2 projection = Engine.GS.Camera.GetProjection(ref {24202});
				projection.X = (float)((int)projection.X);
				projection.Y = (float)((int)projection.Y);
				Vector2 vector = {24205} * (0.5f + Math.Min(1f, 25f / num)) * 0.8f;
				vector *= 0.9f;
				{24206} = vector.Y;
				HealthBarHelper.Draw({24201}, false, projection, {24203}, this.{24219} / this.{24217}(), this.{24214}, {24204}, vector, this.{24216} / 500f);
				return new Vector2?(projection);
			}
			{24206} = 0f;
			return null;
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000E2665 File Offset: 0x000E0865
		public void Render(Vector2 {24207}, HealthBarStyle {24208}, float {24209})
		{
			HealthBarHelper.DrawForShip({24207}, {24208}, this.{24219} / this.{24217}(), {24209}, this.{24216} / 500f, this.{24214});
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000E2694 File Offset: 0x000E0894
		public void RenderText(Vector2 {24210}, float {24211}, Ship {24212} = null)
		{
			if ({24212} != null && {24212}.UsedShip.FirstHP.FloodingFactor > 0f)
			{
				HealthBarStyle statusColor = ((IClientShip){24212}).GetClient.StatusColor;
				Color value = (statusColor == HealthBarStyle.Red) ? Color.Pink : ((statusColor == HealthBarStyle.Lime) ? Color.SoftLime : Color.White);
				HealthBarHelper.RenderTextDouble({24210}, Local.flooding_hp, string.Empty, value * {24211}, new Color?(value));
				return;
			}
			HealthBar.builder.Clear();
			HealthBar.builder.Append("/");
			HealthBar.builder.Append((int)Math.Ceiling((double)this.{24217}()));
			IClientShip clientShip = {24212} as IClientShip;
			HealthBarStyle? healthBarStyle;
			if (clientShip == null)
			{
				healthBarStyle = null;
			}
			else
			{
				ShipPartial getClient = clientShip.GetClient;
				healthBarStyle = ((getClient != null) ? new HealthBarStyle?(getClient.StatusColor) : null);
			}
			HealthBarStyle? healthBarStyle2 = healthBarStyle;
			ShipNpc shipNpc = {24212} as ShipNpc;
			bool flag;
			if (shipNpc != null)
			{
				int currentAgressionTargetUID = shipNpc.CurrentAgressionTargetUID;
				ShipCurrentPlayer player = Global.Player;
				int? num = (player != null) ? new int?(player.uID) : null;
				flag = (currentAgressionTargetUID == num.GetValueOrDefault() & num != null);
			}
			else
			{
				flag = false;
			}
			bool flag2 = flag;
			Color value2 = (healthBarStyle2 == HealthBarStyle.Red || flag2) ? Color.DarkRed : ((healthBarStyle2 == HealthBarStyle.Lime) ? Color.Green : ((healthBarStyle2 != null && healthBarStyle2.GetValueOrDefault() == HealthBarStyle.Blue) ? Color.Lerp(Color.Blue, Color.SkyBlue, 0.57f) : ((healthBarStyle2 == HealthBarStyle.Gray) ? Color.Gray : Color.Gray)));
			HealthBarHelper.RenderTextDouble({24210}, ((int)Math.Ceiling((double)this.{24218}())).ToString(), HealthBar.builder.ToString(), Color.White * {24211}, new Color?(value2));
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000E287E File Offset: 0x000E0A7E
		public void OnReducedDamageReceived(float {24213})
		{
			this.{24220} += {24213};
		}

		// Token: 0x040017EA RID: 6122
		private static StringBuilder builder = new StringBuilder();

		// Token: 0x040017EB RID: 6123
		private Vector2 {24214};

		// Token: 0x040017EC RID: 6124
		private float {24215};

		// Token: 0x040017ED RID: 6125
		private float {24216};

		// Token: 0x040017EE RID: 6126
		private Func<float> {24217};

		// Token: 0x040017EF RID: 6127
		private Func<float> {24218};

		// Token: 0x040017F0 RID: 6128
		private float {24219};

		// Token: 0x040017F1 RID: 6129
		private float {24220};
	}
}
