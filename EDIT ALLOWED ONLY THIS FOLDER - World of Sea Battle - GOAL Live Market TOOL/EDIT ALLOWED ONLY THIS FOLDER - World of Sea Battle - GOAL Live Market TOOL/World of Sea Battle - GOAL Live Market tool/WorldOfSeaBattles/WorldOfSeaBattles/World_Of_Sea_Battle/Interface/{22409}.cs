using System;
using System.Linq;
using System.Runtime.CompilerServices;
using Common;
using Common.Resources;
using CommonDataTypes;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Grphics.Device;
using TheraEngine.Input;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using TheraEngine.Interface.Eventing;
using TheraEngine.Scene.Lighting;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020003B1 RID: 945
	internal sealed class {22409} : CustomUi
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x000ADF96 File Offset: 0x000AC196
		public ShipDesignCategory Type
		{
			get
			{
				return this.{22441};
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060014A5 RID: 5285 RVA: 0x000ADF9E File Offset: 0x000AC19E
		public bool IsMoving
		{
			get
			{
				return this.{22429}() || this.{22444};
			}
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000ADFB0 File Offset: 0x000AC1B0
		public static {22409} Create(ShipDesignCategory {22417}, bool {22418} = true)
		{
			if ({22417} == ShipDesignCategory.BowFigure || {22417} == ShipDesignCategory.Satellite)
			{
				Global.Camera.RunFocusInObjectAnimation();
			}
			if ({22417} == ShipDesignCategory.BowFigure)
			{
				return new {22409}(Global.Player.UsedShip.StaticInfo.BowFigurePosition + Global.Player.UsedShipPlayer.BowFigurePosition, Global.Player.UsedShipPlayer.StaticInfo.BowFigurePosition, delegate(Vector3 {22447})
				{
					Global.Player.UsedShipPlayer.BowFigurePosition.X = {22447}.X - Global.Player.UsedShipPlayer.StaticInfo.BowFigurePosition.X;
					Global.Player.UsedShipPlayer.BowFigurePosition.Y = {22447}.Y - Global.Player.UsedShipPlayer.StaticInfo.BowFigurePosition.Y;
					Global.Player.UsedShipPlayer.BowFigurePosition.Z = {22447}.Z;
					Global.Player.Client.UpdateDesignsPositions();
				}, ShipDesignCategory.BowFigure, {22418});
			}
			if ({22417} == ShipDesignCategory.Satellite)
			{
				return new {22409}(new Vector3(Global.Player.UsedShipPlayer.BigLampPosition, 0f), Vector3.Zero, delegate(Vector3 {22448})
				{
					Global.Player.UsedShipPlayer.BigLampPosition.X = {22448}.X;
					Global.Player.UsedShipPlayer.BigLampPosition.Y = {22448}.Y;
					Global.Player.Client.UpdateDesignsPositions();
				}, ShipDesignCategory.Satellite, {22418});
			}
			if ({22417} == ShipDesignCategory.Decal1 || {22417} == ShipDesignCategory.Decal2)
			{
				return new {22409}({22417}, {22418});
			}
			throw new NotSupportedException();
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000AE094 File Offset: 0x000AC294
		private {22409}(ShipDesignCategory {22419}, bool {22420} = true) : base({22420})
		{
			{22409}.CurrentInstance = this;
			this.{22441} = ShipDesignCategory.Decal1;
			this.{22442} = false;
			int num = Global.Player.UsedShip.StaticInfo.SailHitboxes.Length;
			Tlist<SailHitbox> tlist = new Tlist<SailHitbox>();
			foreach (SailHitbox sailHitbox in Global.Player.UsedShip.StaticInfo.SailHitboxes)
			{
				tlist.Add(sailHitbox);
			}
			Tlist<CheckboxControl> checkboxes = new Tlist<CheckboxControl>();
			for (int j = 0; j < num; j++)
			{
				SailHitbox sailHb = Global.Player.UsedShip.StaticInfo.SailHitboxes[j];
				CheckboxControl checkbox = new CheckboxControl(Vector2.Zero, AtlasPortGui.vd_checkBox_26px_true, AtlasPortGui.vd_checkBox_26px_false, (({22419} == ShipDesignCategory.Decal1) ? Global.Player.UsedShipPlayer.Decal1SelectedSailesBits : Global.Player.UsedShipPlayer.Decal2SelectedSailesBits)[sailHb.SailStrengthIndex], PositionAlignment.LeftUp, PositionAlignment.LeftUp).ExCheckEvent(delegate(CheckboxCheckedEventArgs {22449})
				{
					if ({22419} == ShipDesignCategory.Decal1)
					{
						Global.Player.UsedShipPlayer.Decal1SelectedSailesBits[sailHb.SailStrengthIndex] = {22449}.NewValue;
						return;
					}
					Global.Player.UsedShipPlayer.Decal2SelectedSailesBits[sailHb.SailStrengthIndex] = {22449}.NewValue;
				});
				checkboxes.Add(checkbox);
				checkbox.UpdateComplete += delegate(UiControl {22450})
				{
					Vector3 visualPosition = sailHb.VisualPosition;
					Vector3 {14982};
					Global.Player.Transform.Transform3X3(ref visualPosition, out {14982});
					UiControl checkbox = checkbox;
					Marker pos = checkbox.Pos;
					Vector2 vector = Engine.GS.Camera.GetProjection({14982}) - new Vector2(5f);
					checkbox.Pos = pos.SetXY(vector);
					if (checkboxes.Any((CheckboxControl {22451}) => {22451} != {22450} && {22451}.Pos.ToRect().Intersects(checkbox.Pos.ToRect())))
					{
						checkbox.Pos = checkbox.Pos.Offset(0f, checkbox.Pos.Height);
					}
				};
				base.AddChild(checkbox);
			}
			if ({22420})
			{
				this.{22426}();
			}
			base.EvRemoveFromContainer += delegate()
			{
				{22409}.CurrentInstance = null;
			};
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000AE250 File Offset: 0x000AC450
		public {22409}(Vector3 {22421}, Vector3 {22422}, Action<Vector3> {22423}, ShipDesignCategory {22424}, bool {22425} = true) : base({22425})
		{
			{22409}.CurrentInstance = this;
			this.{22438} = {22423};
			this.{22439} = {22421};
			this.{22440} = {22422};
			this.{22441} = {22424};
			this.{22442} = {22425};
			if (this.{22439}.Z < {22409}.scaleClamp.X)
			{
				this.{22439}.Z = 1f;
			}
			if ({22424} == ShipDesignCategory.BowFigure)
			{
				this.{22445} = new Button(Vector2.Zero, new Rectangle(1573, 68, 44, 44), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{22446} = new Button(Vector2.Zero, new Rectangle(1573, 113, 44, 44), PositionAlignment.LeftUp, PositionAlignment.LeftUp);
				this.{22445}.EvClick += this.{22432};
				this.{22446}.EvClick += this.{22434};
				base.AddChild(new UiControl[]
				{
					this.{22445},
					this.{22446}
				});
			}
			if ({22425})
			{
				this.{22426}();
			}
			float num = 1f - Global.Game.StaticSystem.GetSkyShader.DayOrNight;
			this.{22443} = new PointLight(Engine.GS.Camera.Position, Color.Wheat.ToVector3(), 0.15f * num, 200f);
			this.{22443}.DrawFlares = PointLightFlaresMode.Disable;
			Global.Render.Pointlights.Add(this.{22443});
			base.EvRemoveFromContainer += delegate()
			{
				try
				{
					Global.Render.Pointlights.Remove({22409}.CurrentInstance.{22443});
				}
				catch
				{
				}
				{22409}.CurrentInstance = null;
			};
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x000AE3F4 File Offset: 0x000AC5F4
		private void {22426}()
		{
			Button button = new Button(new Vector2((float)Engine.GS.UIArea.Width * 0.5f - (float)AtlasPortGui.buttonBlueBack.Width * 0.5f, (float)Engine.GS.UIArea.Height - (float)AtlasPortGui.buttonBlueBack.Height * 1.1f), AtlasPortGui.buttonBlueBack, PositionAlignment.Center, PositionAlignment.Center).SetText(Local.to_back, Fonts.Philosopher_14, Color.White * 0.7f, false);
			button.Pos = button.Pos.ScaleOfCenter(1.1f);
			button.EvClick += this.{22436};
			base.AddChild(button);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x000AE4B0 File Offset: 0x000AC6B0
		private void {22427}(float {22428})
		{
			this.{22439}.Z = MathHelper.Clamp(this.{22439}.Z + {22428}, {22409}.scaleClamp.X, {22409}.scaleClamp.Y);
			this.{22438}(this.{22439});
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x000AE500 File Offset: 0x000AC700
		private bool {22429}()
		{
			if (this.{22441} != ShipDesignCategory.Decal1 && InputHelper.NowMouseState.LeftPressed)
			{
				Marker marker = this.{22431}();
				Vector2 mouseToUI = Engine.GS.MouseToUI;
				return marker.Collision(mouseToUI);
			}
			return false;
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x000AE540 File Offset: 0x000AC740
		protected override void UserUpdate(ref FrameTime {22430})
		{
			if (this.{22429}())
			{
				this.{22444} = true;
			}
			if (!InputHelper.NowMouseState.LeftPressed)
			{
				this.{22444} = false;
			}
			if (this.{22444})
			{
				Vector3 position3D = Global.Player.Position3D;
				Vector3 vector = position3D + new Vector3(Global.Player.Normal.X, 0f, Global.Player.Normal.Y);
				Vector2 projectionSmoothed = Engine.GS.Camera.GetProjectionSmoothed(ref position3D);
				Vector2 projectionSmoothed2 = Engine.GS.Camera.GetProjectionSmoothed(ref vector);
				float num = (this.{22441} == ShipDesignCategory.BowFigure) ? 0.02f : 0.03f;
				this.{22439}.X = this.{22439}.X + num * (Engine.GS.MouseToUIPrev.X - Engine.GS.MouseToUI.X) * (float)((projectionSmoothed.X > projectionSmoothed2.X) ? 1 : -1);
				this.{22439}.Y = this.{22439}.Y + num * (Engine.GS.MouseToUIPrev.Y - Engine.GS.MouseToUI.Y);
				if (this.{22441} == ShipDesignCategory.BowFigure)
				{
					float num2 = 4f;
					this.{22439}.X = MathHelper.Clamp(this.{22439}.X - this.{22440}.X, -num2, num2) + this.{22440}.X;
					this.{22439}.Y = MathHelper.Clamp(this.{22439}.Y - this.{22440}.Y, -num2, num2) + this.{22440}.Y;
				}
				else
				{
					this.{22439}.X = MathHelper.Clamp(this.{22439}.X, Global.Player.UsedShip.StaticInfo.CorpusShape.LocalCenter.X - Global.Player.UsedShip.StaticInfo.CorpusHalfLength / 0.3f - 4f, Global.Player.UsedShip.StaticInfo.CorpusShape.LocalCenter.X + Global.Player.UsedShip.StaticInfo.CorpusHalfLength / 0.3f + 9f);
					this.{22439}.Y = MathHelper.Clamp(this.{22439}.Y, 0.5f, 2f * Global.Player.UsedShip.StaticInfo.BSRadius);
				}
				this.{22438}(this.{22439});
			}
			if (this.{22443} != null)
			{
				this.{22443}.Position = Engine.GS.Camera.Position;
			}
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x000AE7F0 File Offset: 0x000AC9F0
		protected override void UserBackRender()
		{
			if (this.{22441} != ShipDesignCategory.Decal1)
			{
				Vector2 xy = this.{22431}().XY;
				Engine.GS.Draw({22409}.c_selection, xy);
				if (this.{22441} == ShipDesignCategory.BowFigure)
				{
					UiControl uiControl = this.{22445};
					Vector2 vector = xy - new Vector2(40f, -5f);
					Marker pos = this.{22445}.Pos;
					uiControl.Pos = new Marker(ref vector, ref pos.WH);
					UiControl uiControl2 = this.{22446};
					vector = xy - new Vector2(40f, -45f);
					pos = this.{22445}.Pos;
					uiControl2.Pos = new Marker(ref vector, ref pos.WH);
				}
			}
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x000AE8A8 File Offset: 0x000ACAA8
		private Marker {22431}()
		{
			Vector3 vector = this.{22439} * new Vector3(1f, 1f, 0f);
			Vector3 vector2;
			Global.Player.Transform.Transform3X3(ref vector, out vector2);
			Vector2 vector3 = Engine.GS.Camera.GetProjection(ref vector2) - new Vector2((float){22409}.c_selection.Width * 0.5f, (float){22409}.c_selection.Height * 0.5f);
			return new Marker(ref vector3, ref {22409}.c_selection);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x000AE934 File Offset: 0x000ACB34
		protected override void UserFrontRender()
		{
			if (this.{22442})
			{
				Engine.GS.SetFont(Fonts.Philosopher_16);
				Device gs = Engine.GS;
				string portVisualPlaceObjectAtShip_ = Local.PortVisualPlaceObjectAtShip_0;
				Vector2 vector = new Vector2((float)Engine.GS.UIArea.Width * 0.5f - 150f, 30f);
				Color white = Color.White;
				gs.DrawString(portVisualPlaceObjectAtShip_, vector, white);
			}
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000AE9C2 File Offset: 0x000ACBC2
		[CompilerGenerated]
		private void {22432}(ClickUiEventArgs {22433})
		{
			this.{22427}(0.2f);
		}

		// Token: 0x060014B2 RID: 5298 RVA: 0x000AE9CF File Offset: 0x000ACBCF
		[CompilerGenerated]
		private void {22434}(ClickUiEventArgs {22435})
		{
			this.{22427}(-0.2f);
		}

		// Token: 0x060014B3 RID: 5299 RVA: 0x000AC3EC File Offset: 0x000AA5EC
		[CompilerGenerated]
		private void {22436}(ClickUiEventArgs {22437})
		{
			base.RemoveFromContainer();
		}

		// Token: 0x040012A4 RID: 4772
		public static {22409} CurrentInstance;

		// Token: 0x040012A5 RID: 4773
		private const float scaleValuePerClick = 0.1f;

		// Token: 0x040012A6 RID: 4774
		private static readonly Vector2 scaleClamp = new Vector2(0.5f, 1.5f);

		// Token: 0x040012A7 RID: 4775
		private static readonly Rectangle c_selection = new Rectangle(1714, 1, 94, 94);

		// Token: 0x040012A8 RID: 4776
		private Action<Vector3> {22438};

		// Token: 0x040012A9 RID: 4777
		private Vector3 {22439};

		// Token: 0x040012AA RID: 4778
		private Vector3 {22440};

		// Token: 0x040012AB RID: 4779
		private ShipDesignCategory {22441};

		// Token: 0x040012AC RID: 4780
		private bool {22442} = true;

		// Token: 0x040012AD RID: 4781
		private PointLight {22443};

		// Token: 0x040012AE RID: 4782
		private bool {22444};

		// Token: 0x040012AF RID: 4783
		private Button {22445};

		// Token: 0x040012B0 RID: 4784
		private Button {22446};
	}
}
