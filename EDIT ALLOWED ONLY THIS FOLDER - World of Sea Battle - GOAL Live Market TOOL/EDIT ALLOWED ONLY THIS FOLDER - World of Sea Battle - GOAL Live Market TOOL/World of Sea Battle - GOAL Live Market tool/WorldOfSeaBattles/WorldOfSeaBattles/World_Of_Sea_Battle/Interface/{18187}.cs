using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Grphics.Device;
using TheraEngine.Interface;
using TheraEngine.Interface.Controls;
using World_Of_Sea_Battle.Graphics.Components;
using World_Of_Sea_Battle.Graphics.Shaders;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000113 RID: 275
	internal sealed class {18187}
	{
		// Token: 0x0600066D RID: 1645 RVA: 0x000322B0 File Offset: 0x000304B0
		public {18187}(Ship {18191}, BoardingCombatInstance {18192}, CrewConnector {18193})
		{
			this.combatInstance = {18192};
			this.myShip = {18191};
			CrewConnector.Approximator approximator;
			if ({18191} != null)
			{
				approximator = new CrewConnector.Approximator({18193});
			}
			else
			{
				CrewConnector.Approximator approximator2 = default(CrewConnector.Approximator);
				approximator = approximator2;
			}
			this.orderedPositions = approximator;
			Tlist<{18187}.VisualGroupConnection> tlist;
			if ({18191} != null)
			{
				CrewConnector.Approximator approximator2 = new CrewConnector.Approximator({18193});
				tlist = this.Create({18191}, approximator2, {18192}.VisualGroups);
			}
			else
			{
				tlist = new Tlist<{18187}.VisualGroupConnection>();
			}
			this.groupsVisualizerMy = tlist;
		}

		// Token: 0x0600066E RID: 1646 RVA: 0x00032314 File Offset: 0x00030514
		private Tlist<{18187}.VisualGroupConnection> Create(Ship {18194}, in CrewConnector.Approximator {18195}, Tlist<BoardingCombatVisualGroup> {18196})
		{
			Tlist<{18187}.VisualGroupConnection> tlist = new Tlist<{18187}.VisualGroupConnection>();
			int num = 0;
			float num2 = Math.Min(1f, ((float){18196}.Size - 1f) / 5f);
			Vector3 value = new Vector3(num2 * {18194}.UsedShip.StaticInfo.CorpusHalfLength / 0.3f * 0.9f, {18194}.UsedShip.StaticInfo.CorpusHalfHeight * 0.8f, 0f);
			Vector3 vector = value * new Vector3(-1f, 1f, 1f);
			value += {18194}.UsedShip.StaticInfo.CorpusShape.LocalCenter;
			vector += {18194}.UsedShip.StaticInfo.CorpusShape.LocalCenter;
			int num3 = 0;
			foreach (BoardingCombatVisualGroup boardingCombatVisualGroup in ((IEnumerable<BoardingCombatVisualGroup>){18196}))
			{
				float num4 = (float)num3++ / MathF.Max(1f, (float)({18196}.Size - 1));
				Vector3 {18206} = Vector3.Lerp(value, vector, num4);
				{18206}.Y += 1f - num4 * (1f - num4) * 4f;
				num += boardingCombatVisualGroup.InitialUnitsCount;
				Tlist<{18187}.VisualGroupConnection> tlist2 = tlist;
				{18187}.VisualGroupConnection visualGroupConnection = new {18187}.VisualGroupConnection(this, boardingCombatVisualGroup, {18194}, {18206});
				tlist2.Add(visualGroupConnection);
			}
			return tlist;
		}

		// Token: 0x0600066F RID: 1647 RVA: 0x00032488 File Offset: 0x00030688
		public void UpdateHpBar(ref FrameTime {18197})
		{
			foreach ({18187}.VisualGroupConnection visualGroupConnection in ((IEnumerable<{18187}.VisualGroupConnection>)this.groupsVisualizerMy))
			{
				visualGroupConnection.Update(ref {18197});
			}
		}

		// Token: 0x06000670 RID: 1648 RVA: 0x000324D4 File Offset: 0x000306D4
		public void PrepareDraw(Tlist<{18187}.VisualGroupConnection> {18198})
		{
			if (this.myShip == null || !this.myShip.InstanceAlive)
			{
				return;
			}
			foreach ({18187}.VisualGroupConnection visualGroupConnection in ((IEnumerable<{18187}.VisualGroupConnection>)this.groupsVisualizerMy))
			{
				visualGroupConnection.UpdatePosition(this.myShip);
				{18198}.Add(visualGroupConnection);
			}
		}

		// Token: 0x040005C6 RID: 1478
		public static readonly Rectangle c_tt_back = new Rectangle(2579, 106, 367, 44);

		// Token: 0x040005C7 RID: 1479
		public static readonly Rectangle c_focusUnchecked = new Rectangle(1660, 64, 44, 45);

		// Token: 0x040005C8 RID: 1480
		public static readonly Rectangle c_focusChecked = new Rectangle(1705, 64, 44, 45);

		// Token: 0x040005C9 RID: 1481
		public static readonly Rectangle c_protectUnchecked = new Rectangle(1660, 172, 44, 45);

		// Token: 0x040005CA RID: 1482
		public static readonly Rectangle c_protectChecked = new Rectangle(1705, 172, 44, 45);

		// Token: 0x040005CB RID: 1483
		public static readonly Rectangle c_musketIcon = new Rectangle(1662, 112, 65, 28);

		// Token: 0x040005CC RID: 1484
		public static readonly Rectangle c_musketIconReloading = new Rectangle(1732, 111, 65, 28);

		// Token: 0x040005CD RID: 1485
		public static readonly Rectangle c_warriorIcon = new Rectangle(1662, 141, 65, 28);

		// Token: 0x040005CE RID: 1486
		public static readonly Rectangle c_warriorIconReloading = new Rectangle(1732, 141, 65, 28);

		// Token: 0x040005CF RID: 1487
		public static readonly Rectangle c_sailorIcon = new Rectangle(1582, 179, 76, 35);

		// Token: 0x040005D0 RID: 1488
		public static readonly Rectangle c_progressStateBarLeft = new Rectangle(1211, 140, 337, 14);

		// Token: 0x040005D1 RID: 1489
		public static readonly Rectangle c_progressStateBarRight = new Rectangle(1211, 125, 337, 14);

		// Token: 0x040005D2 RID: 1490
		public static readonly Rectangle c_progressStatePoint = new Rectangle(1423, 74, 49, 49);

		// Token: 0x040005D3 RID: 1491
		public BoardingCombatInstance combatInstance;

		// Token: 0x040005D4 RID: 1492
		public CrewConnector.Approximator orderedPositions;

		// Token: 0x040005D5 RID: 1493
		public Tlist<{18187}.VisualGroupConnection> groupsVisualizerMy;

		// Token: 0x040005D6 RID: 1494
		public Ship myShip;

		// Token: 0x02000114 RID: 276
		public class VisualGroupConnection
		{
			// Token: 0x06000672 RID: 1650 RVA: 0x00003A7C File Offset: 0x00001C7C
			public VisualGroupConnection()
			{
			}

			// Token: 0x06000673 RID: 1651 RVA: 0x00032680 File Offset: 0x00030880
			public VisualGroupConnection({18187} {18203}, BoardingCombatVisualGroup {18204}, Ship {18205}, Vector3 {18206})
			{
				this.Group = {18204};
				this.Parent = {18203};
				this.{18212} = {18206};
				this.UpdatePosition({18205});
				this.HpBar = new HealthBar();
				this.HpBar.Initialize(() => 1f, new Func<float>(this.{18211}));
			}

			// Token: 0x06000674 RID: 1652 RVA: 0x000326F1 File Offset: 0x000308F1
			public void UpdatePosition(Ship {18207})
			{
				this.ComputedPosition3D = {18207}.Transform.Transform3X3(this.{18212}) + new Vector3(0f, 0.6f, 0f);
			}

			// Token: 0x06000675 RID: 1653 RVA: 0x00032723 File Offset: 0x00030923
			public void Update(ref FrameTime {18208})
			{
				this.HpBar.Update(ref {18208});
			}

			// Token: 0x06000676 RID: 1654 RVA: 0x00032734 File Offset: 0x00030934
			public void Draw(HealthBarStyle {18209}, Dictionary<byte, CheckboxControl> {18210} = null)
			{
				float num = 0f;
				float healthFactor = this.Group.HealthFactor;
				if (this.Parent.combatInstance.MyProtectionSetted.Contains(this.Group.VisualGroupIndex))
				{
					if (!this.HpBar.HasDamageEffect)
					{
						this.{18213} = true;
					}
				}
				else
				{
					this.{18213} = false;
				}
				Vector2? vector = (healthFactor == 0f) ? null : this.HpBar.RenderScaled(this.{18213}, this.ComputedPosition3D, {18209}, 1f, 0.5f * new Vector2((0.5f + 0.5f * (float)this.Group.InitialUnitsCount / 17f) * 0.8f, 1.1f), out num);
				if (vector != null)
				{
					vector = new Vector2?(Engine.GS.Camera.GetProjectionSmoothed(ref this.ComputedPosition3D));
				}
				if ({18210} != null)
				{
					if (vector != null)
					{
						CheckboxControl checkboxControl = {18210}[this.Group.VisualGroupIndex];
						checkboxControl.IsVisible = (healthFactor > 0f);
						UiControl uiControl = checkboxControl;
						Marker pos = checkboxControl.Pos;
						Vector2 vector2 = vector.Value - new Vector2(0f, 32f) - checkboxControl.Pos.WH / 2f;
						uiControl.Pos = pos.SetXY(vector2);
					}
					else
					{
						{18210}[this.Group.VisualGroupIndex].IsVisible = false;
					}
				}
				if (vector != null && !this.Group.IncludingLiveUnits.IsEmpty)
				{
					float reloadFactor = this.Group.ReloadFactor;
					Rectangle c_musketIconReloading = {18187}.c_musketIconReloading;
					Rectangle rectangle = {18187}.c_musketIcon;
					if (this.Group.IncludingLiveUnits.UnitInfo.First<GSILocalEnumerablePair<UnitInfo>>().Info.IsMusketeer)
					{
						rectangle = {18187}.c_musketIcon;
					}
					else if (this.Group.IncludingLiveUnits.UnitInfo.First<GSILocalEnumerablePair<UnitInfo>>().Info.Type == UnitType.Sailor)
					{
						rectangle = {18187}.c_sailorIcon;
					}
					else
					{
						rectangle = {18187}.c_warriorIcon;
					}
					Device gs = Engine.GS;
					Vector2 vector2 = vector.Value + new Vector2(0f, 25f) * num;
					Vector2 vector3 = {18187}.c_warriorIcon.WidthHeight() / 2f;
					float {14558} = 0f;
					float {14559} = num * 1.4f;
					Color white = Color.White;
					gs.Draw(rectangle, vector2, vector3, {14558}, {14559}, white);
				}
			}

			// Token: 0x06000677 RID: 1655 RVA: 0x000329BB File Offset: 0x00030BBB
			[CompilerGenerated]
			private float {18211}()
			{
				return this.Group.HealthFactor;
			}

			// Token: 0x040005D7 RID: 1495
			public BoardingCombatVisualGroup Group;

			// Token: 0x040005D8 RID: 1496
			public Vector3 ComputedPosition3D;

			// Token: 0x040005D9 RID: 1497
			public HealthBar HpBar;

			// Token: 0x040005DA RID: 1498
			public {18187} Parent;

			// Token: 0x040005DB RID: 1499
			private Vector3 {18212};

			// Token: 0x040005DC RID: 1500
			private bool {18213};
		}
	}
}
