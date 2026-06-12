using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Common;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TheraEngine;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Graphics;
using TheraEngine.Grphics.Device;
using TheraEngine.Helpers;
using TheraEngine.Input;
using TheraEngine.Interface;
using World_Of_Sea_Battle.Components;
using World_Of_Sea_Battle.Core;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x02000154 RID: 340
	internal sealed class {18561} : CustomUi
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x000070D7 File Offset: 0x000052D7
		private bool drawShallowPoints
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x000030FD File Offset: 0x000012FD
		private bool showHazardAreas
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060007B7 RID: 1975 RVA: 0x000030FD File Offset: 0x000012FD
		private bool showPorts
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x000030FD File Offset: 0x000012FD
		private bool showDetails
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060007B9 RID: 1977 RVA: 0x000070D7 File Offset: 0x000052D7
		private bool showSeamlessWaters
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x000030FD File Offset: 0x000012FD
		private bool showPbPbjects
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0003D320 File Offset: 0x0003B520
		public {18561}(int {18565}, WorldMapInfo {18566}, IEnumerable<IsleInstance> {18567}) : base(true)
		{
			this.{18586} = {18566};
			this.{18581} = {18565};
			this.{18587} = new RenderTarget((int)((float){18565} * (this.{18586}.MapSize.Y / this.{18586}.MapSize.X)), {18565}, SurfaceFormat.Color, DepthFormat.None, 0, true, "temp", false);
			this.{18583} = new Tlist<{18561}.ItemReference>();
			{18561}.IsOpen = true;
			this.{18585} = new Tlist<{18561}.HazardAreaReference>();
			WorldMapInfo worldMapInfo = this.{18586};
			this.{18586} = Gameplay.WorldMap;
			this.{18582} = new Tlist<string>(File.ReadAllLines("World.xml"));
			for (int i = 0; i < this.{18582}.Size; i++)
			{
				string text = this.{18582}.Array[i];
				if (text.Contains("LocalPosition"))
				{
					{18561}.ItemReference itemReference = new {18561}.ItemReference();
					itemReference.LineNumber = i;
					string text2 = this.Between(text, ">", "<");
					if (!string.IsNullOrEmpty(text2))
					{
						string[] array = text2.Replace('.', ',').Split(' ', StringSplitOptions.None);
						string s = this.Between(this.{18582}.Array[i + 1], ">", "<").Replace('.', ',');
						string s2 = this.Between(this.{18582}.Array[i + 2], ">", "<").Replace('.', ',');
						string pathRaw = this.Between(this.{18582}.Array[i + 3], ">", "<");
						string s3 = this.Between(this.{18582}.Array[i + 4], ">", "<");
						string text3 = this.{18582}.Array[i + 5].Contains("<Parameters />") ? string.Empty : this.Between(this.{18582}.Array[i + 5], ">", "<");
						itemReference.LocalPosition = new Vector2(float.Parse(array[0]), float.Parse(array[1]));
						itemReference.LocalPositionY = ((array.Length == 3) ? float.Parse(array[2]) : 0f);
						itemReference.Rotation = float.Parse(s);
						itemReference.Scale = float.Parse(s2);
						itemReference.Parameter = text3;
						itemReference.TerrainID = int.Parse(s3);
						itemReference.IsFactory = pathRaw.Contains("fc_buildings_all");
						itemReference.IsTrader = pathRaw.Contains("trader_");
						itemReference.IsPBRespawn = pathRaw.Contains("avanpost");
						itemReference.IsAltar = pathRaw.Contains("altar");
						itemReference.IsPbObject = (pathRaw.Contains("guildfort") || pathRaw.Contains("t_mortar"));
						itemReference.IsPortPharos = pathRaw.Contains("pharos_");
						if (text3.Contains("port"))
						{
							itemReference.PortName = Local.Current(text3);
						}
						List<IsleInstance> list = (from {18592} in Gameplay.WorldMap.CollisableObjects
						where pathRaw.Contains({18592}.ModelData.Model.ModelName) || {18592}.ModelData.Model.ModelName.Contains(pathRaw)
						select {18592}).ToList<IsleInstance>();
						if (list.Count == 0)
						{
							itemReference.ShallowPointsLocalSpace = new Tlist<Vector2>();
						}
						else
						{
							itemReference.ShallowPointsLocalSpace = new Tlist<Vector2>();
							Matrix matrix;
							list[0].GlobalTransform.CreateInvertedfulWorld(out matrix);
							if (list[0].TransformedShallowsPoints != null)
							{
								foreach (Vector2 {11452} in ((IEnumerable<Vector2>)list[0].TransformedShallowsPoints))
								{
									Vector3 {11450} = Vector3.Transform({11452}.X0Y(), matrix);
									Tlist<Vector2> shallowPointsLocalSpace = itemReference.ShallowPointsLocalSpace;
									Vector2 vector = {11450}.XZ();
									shallowPointsLocalSpace.Add(vector);
								}
							}
						}
						if (itemReference.ShallowPointsLocalSpace.Size > 0)
						{
							float y = itemReference.ShallowPointsLocalSpace.Max((Vector2 {18590}) => {18590}.Length());
							itemReference.ApproxSSRadius = Vector2.Distance(this.{18571}(new Vector2(0f, 0f)), this.{18571}(new Vector2(0f, y))) * 0.5f * itemReference.Scale * 0.246f;
						}
						else
						{
							itemReference.ApproxSSRadius = 1f;
						}
						this.{18583}.Add(itemReference);
					}
				}
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0003D7B0 File Offset: 0x0003B9B0
		public string Between(string {18568}, string {18569}, string {18570})
		{
			int num = {18568}.IndexOf({18569}) + {18569}.Length;
			int num2 = {18568}.IndexOf({18570}, num);
			int num3 = num;
			return {18568}.Substring(num3, num2 - num3);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0003D7E4 File Offset: 0x0003B9E4
		public void Overwrite()
		{
			foreach ({18561}.ItemReference itemReference in ((IEnumerable<{18561}.ItemReference>)this.{18583}))
			{
				if (itemReference.LineNumber != -1)
				{
					string str = (itemReference.LocalPosition.X.ToString() + " " + itemReference.LocalPosition.Y.ToString() + ((itemReference.LocalPositionY == 0f) ? string.Empty : (" " + itemReference.LocalPositionY.ToString()))).Replace(',', '.');
					string str2 = itemReference.Rotation.ToString().Replace(',', '.');
					this.{18582}.Array[itemReference.LineNumber] = "          <LocalPosition>" + str + "</LocalPosition>";
					this.{18582}.Array[itemReference.LineNumber + 1] = "          <Rotation>" + str2 + "</Rotation>";
				}
			}
			File.WriteAllLines("World.xml", this.{18582});
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("PositionX, PositionY, Axis - with same order");
			foreach ({18561}.ItemReference itemReference2 in ((IEnumerable<{18561}.ItemReference>)this.{18583}))
			{
				if (itemReference2.LineNumber == -1)
				{
					string str3 = (itemReference2.LocalPosition.X.ToString() + ", " + itemReference2.LocalPosition.Y.ToString()).Replace(',', '.');
					stringBuilder.AppendLine(str3 + ", " + itemReference2.Rotation.ToString());
				}
			}
			stringBuilder.AppendLine("Positions of hazard areas");
			foreach ({18561}.HazardAreaReference hazardAreaReference in ((IEnumerable<{18561}.HazardAreaReference>)this.{18585}))
			{
				string str4 = (hazardAreaReference.Pos.X.ToString() + ", " + hazardAreaReference.Pos.Y.ToString()).Replace(',', '.');
				stringBuilder.AppendLine(hazardAreaReference.HazardIndex.ToString() + ": " + str4);
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0003DA4C File Offset: 0x0003BC4C
		private Vector2 {18571}(Vector2 {18572})
		{
			{18572} *= 0.82f;
			Vector2 vector = this.showSeamlessWaters ? Gameplay.WorldMaxMapSizeXY : this.{18586}.MapSize;
			float num = {18572}.Y / vector.Y + 0.5f;
			float num2 = (1f - {18572}.X) / vector.X + 0.5f;
			Vector2 result;
			result.X = num * (float)this.{18581} * vector.Y / vector.X;
			result.Y = num2 * (float)this.{18581};
			return result;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0003DAE0 File Offset: 0x0003BCE0
		private Vector2 {18573}(Vector2 {18574})
		{
			Vector2 vector = this.showSeamlessWaters ? Gameplay.WorldMaxMapSizeXY : this.{18586}.MapSize;
			Vector2 result;
			result.X = (1f - {18574}.Y / (float)this.{18581} - 0.5f) * vector.Y / (vector.Y / vector.X);
			result.Y = ({18574}.X / (float)this.{18581} * (1f / (vector.Y / vector.X)) - 0.5f) * vector.Y;
			return result;
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x0003DB78 File Offset: 0x0003BD78
		private void {18575}(float {18576}, ref FrameTime {18577})
		{
			Vector2 vector = default(Vector2);
			int num = 0;
			foreach ({18561}.ItemReference itemReference in ((IEnumerable<{18561}.ItemReference>)this.{18583}))
			{
				if (itemReference.IsSelected)
				{
					vector += itemReference.LocalPosition;
					num++;
				}
			}
			if (num == 0)
			{
				return;
			}
			vector /= (float)num;
			float num2 = {18576} * {18577}.secElapsed * 0.7f;
			foreach ({18561}.ItemReference itemReference2 in ((IEnumerable<{18561}.ItemReference>)this.{18583}))
			{
				if (itemReference2.IsSelected)
				{
					Vector2 v = itemReference2.LocalPosition - vector;
					itemReference2.LocalPosition = Geometry.RotateVector2(v, num2) + vector;
					itemReference2.Rotation += num2;
				}
			}
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0003DC74 File Offset: 0x0003BE74
		protected override void UserUpdate(ref FrameTime {18578})
		{
			if (InputHelper.LeftWasClicked)
			{
				this.{18584} = Engine.GS.MouseToUI;
			}
			if (InputHelper.NowMouseState.LeftPressed && InputHelper.NowInputState.IsUp(Keys.W) && InputHelper.NowInputState.IsUp(Keys.S))
			{
				Vector2 vector = Vector2.Min(this.{18584}, Engine.GS.MouseToUI);
				Vector2 vector2 = Vector2.Max(Engine.GS.MouseToUI, this.{18584});
				foreach ({18561}.ItemReference itemReference in ((IEnumerable<{18561}.ItemReference>)this.{18583}))
				{
					Vector2 vector3 = this.{18571}(itemReference.LocalPosition);
					bool flag = vector3.X > vector.X - itemReference.ApproxSSRadius && vector3.X < vector2.X + itemReference.ApproxSSRadius && vector3.Y > vector.Y - itemReference.ApproxSSRadius && vector3.Y < vector2.Y + itemReference.ApproxSSRadius;
					if (InputHelper.NowInputState.IsDown(Keys.LeftControl))
					{
						itemReference.IsSelected = flag;
					}
					else
					{
						itemReference.IsSelected = flag;
					}
				}
			}
			if (this.showHazardAreas && InputHelper.NowInputState.IsDown(Keys.LeftAlt) && InputHelper.NowMouseState.LeftPressed)
			{
				Vector2 value = this.{18573}(Engine.GS.MouseToUI) - this.{18573}(Engine.GS.MouseToUIPrev);
				using (IEnumerator<{18561}.HazardAreaReference> enumerator2 = ((IEnumerable<{18561}.HazardAreaReference>)this.{18585}).GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						{18561}.HazardAreaReference hazardAreaReference = enumerator2.Current;
						if (hazardAreaReference.IsSelected || InputHelper.LeftWasClicked)
						{
							Vector2 vector4 = this.{18571}(hazardAreaReference.Pos);
							float num = this.{18571}(hazardAreaReference.Pos + new Vector2(0f, 6400f)).X - vector4.X;
							if (Vector2.Distance(vector4, Engine.GS.MouseToUI) < num / 2f)
							{
								hazardAreaReference.Pos += value;
								hazardAreaReference.IsSelected = true;
							}
						}
					}
					goto IL_288;
				}
			}
			foreach ({18561}.HazardAreaReference hazardAreaReference2 in ((IEnumerable<{18561}.HazardAreaReference>)this.{18585}))
			{
				hazardAreaReference2.IsSelected = false;
			}
			IL_288:
			if (InputHelper.IsClick(Keys.S) && InputHelper.NowInputState.IsDown(Keys.LeftControl))
			{
				this.Overwrite();
			}
			float num2 = InputHelper.NowInputState.IsDown(Keys.W) ? 1f : (InputHelper.NowInputState.IsDown(Keys.S) ? 0.1f : 0f);
			if (InputHelper.NowMouseState.LeftPressed && num2 > 0f)
			{
				Vector2 value2 = this.{18573}(Engine.GS.MouseToUI) - this.{18573}(Engine.GS.MouseToUIPrev);
				foreach ({18561}.ItemReference itemReference2 in ((IEnumerable<{18561}.ItemReference>)this.{18583}))
				{
					if (itemReference2.IsSelected)
					{
						itemReference2.LocalPosition += value2 * num2;
					}
				}
			}
			if (InputHelper.NowInputState.IsDown(Keys.Q))
			{
				this.{18575}(1f, ref {18578});
			}
			if (InputHelper.NowInputState.IsDown(Keys.E))
			{
				this.{18575}(-1f, ref {18578});
			}
			Vector2 vector5 = this.{18573}(Engine.GS.MouseToUI);
			Global.Camera.freeCameraPosition = new Vector3(vector5.X, 45f, vector5.Y);
			Global.Camera.Rotation = new Vector3(-0.6f, 1.5707964f, 0f);
			if (InputHelper.IsClick(Keys.Delete))
			{
				foreach ({18561}.ItemReference itemReference3 in (from {18591} in this.{18583}
				where {18591}.IsSelected
				select {18591}).ToArray<{18561}.ItemReference>())
				{
					this.{18583}.FastRemove(itemReference3);
					this.{18582}.RemoveRange(itemReference3.LineNumber - 1, 8);
					foreach ({18561}.ItemReference itemReference4 in ((IEnumerable<{18561}.ItemReference>)this.{18583}))
					{
						if (itemReference4.LineNumber > itemReference3.LineNumber)
						{
							itemReference4.LineNumber -= 8;
						}
					}
				}
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0003E178 File Offset: 0x0003C378
		protected override void UserBackRender()
		{
			if (!this.{18588})
			{
				Engine.GS.End2D();
				Engine.GS.SetRenderTarget(this.{18587});
				Engine.GS.Begin2D(false);
			}
			Engine.GS.SetTexture(CommonAtlas.Texture);
			Device gs = Engine.GS;
			Rectangle rectangle = new Rectangle(0, 0, (int)((float)this.{18581} * this.{18586}.MapSize.Y / this.{18586}.MapSize.X), this.{18581});
			gs.Draw(CommonAtlas.whitePixel, rectangle);
			if (this.showSeamlessWaters)
			{
				this.{18579}(-Gameplay.WorldMapSizeXY * 0.5f / 0.82f);
				this.{18579}(-Gameplay.WorldMaxAvailableSizeXY * 0.5f / 0.82f);
			}
			foreach ({18561}.ItemReference itemReference in ((IEnumerable<{18561}.ItemReference>)this.{18583}))
			{
				if (!itemReference.Hide)
				{
					Color value = itemReference.IsSelected ? Color.Black : Color.Gray;
					if (itemReference.IsPbObject)
					{
						if (!this.showPbPbjects)
						{
							continue;
						}
						value = Color.DarkRed;
					}
					if (this.drawShallowPoints && itemReference.ShallowPointsLocalSpace.Size > 0)
					{
						using (IEnumerator<Vector2> enumerator2 = ((IEnumerable<Vector2>)itemReference.ShallowPointsLocalSpace).GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Vector2 v = enumerator2.Current;
								Vector2 vector = this.{18571}(Geometry.RotateVector2(v, itemReference.Rotation) * itemReference.Scale * 0.246f + itemReference.LocalPosition);
								Device gs2 = Engine.GS;
								rectangle = new Rectangle((int)vector.X - 2, (int)vector.Y - 2, 4, 4);
								gs2.Draw(CommonAtlas.whitePixel, rectangle, value);
							}
							goto IL_233;
						}
						goto IL_1D5;
					}
					goto IL_1D5;
					IL_233:
					Color color;
					if ((itemReference.IsFactory || itemReference.IsTrader || itemReference.IsAltar) && this.showDetails)
					{
						Vector2 vector2 = this.{18571}(itemReference.LocalPosition);
						Device gs3 = Engine.GS;
						rectangle = new Rectangle((int)vector2.X - 5, (int)vector2.Y - 5, 10, 10);
						color = (itemReference.IsPBRespawn ? Color.Cyan : (itemReference.IsFactory ? Color.Orange : (itemReference.IsAltar ? Color.Violet : Color.Lime))) * 0.8f;
						gs3.Draw(CommonAtlas.whitePixel, rectangle, color);
						continue;
					}
					continue;
					IL_1D5:
					Vector2 vector3 = this.{18571}(itemReference.LocalPosition);
					Device gs4 = Engine.GS;
					rectangle = new Rectangle((int)vector3.X - 2, (int)vector3.Y - 2, 4, 4);
					color = value * ((itemReference.ShallowPointsLocalSpace.Size == 0) ? 1.2f : 1f);
					gs4.Draw(CommonAtlas.whitePixel, rectangle, color);
					goto IL_233;
				}
			}
			bool showHazardAreas = this.showHazardAreas;
			if (this.showPorts)
			{
				foreach ({18561}.ItemReference itemReference2 in ((IEnumerable<{18561}.ItemReference>)this.{18583}))
				{
					if (!string.IsNullOrEmpty(itemReference2.PortName))
					{
						Vector2 vector4 = this.{18571}(itemReference2.LocalPosition);
						Engine.GS.SetFont(Fonts.F_m14_ThinBold);
						Device gs5 = Engine.GS;
						rectangle = new Rectangle((int)vector4.X - 4, (int)vector4.Y - 4, 8, 8);
						Color color = Color.DarkBlue;
						gs5.Draw(CommonAtlas.whitePixel, rectangle, color);
						Device gs6 = Engine.GS;
						string portName = itemReference2.PortName;
						color = Color.Blue * 0.7f;
						gs6.DrawStringCentered(portName, vector4, color);
					}
				}
			}
			if (!this.{18588})
			{
				Engine.GS.End2D();
				Engine.GS.ReturnRenderTarget();
				using (FileStream fileStream = File.Create("world_map_render.png"))
				{
					this.{18587}.Resource.SaveAsPng(fileStream, this.{18587}.Resource.Width, this.{18587}.Resource.Height);
				}
				this.{18587}.Dispose();
				this.{18587} = null;
				this.{18588} = true;
				Engine.GS.Begin2D(true);
			}
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0003E62C File Offset: 0x0003C82C
		private void {18579}(Vector2 {18580})
		{
			Engine.GS.Line2D(CommonAtlas.whitePixel, this.{18571}({18580}), this.{18571}(new Vector2(-{18580}.X, {18580}.Y)), Color.Red, 1);
			Engine.GS.Line2D(CommonAtlas.whitePixel, this.{18571}({18580}), this.{18571}(new Vector2({18580}.X, -{18580}.Y)), Color.Red, 1);
			Engine.GS.Line2D(CommonAtlas.whitePixel, this.{18571}(-{18580}), this.{18571}(new Vector2({18580}.X, -{18580}.Y)), Color.Red, 1);
			Engine.GS.Line2D(CommonAtlas.whitePixel, this.{18571}(-{18580}), this.{18571}(new Vector2(-{18580}.X, {18580}.Y)), Color.Red, 1);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00003100 File Offset: 0x00001300
		protected override void UserFrontRender()
		{
		}

		// Token: 0x040006DA RID: 1754
		public static bool IsOpen;

		// Token: 0x040006DB RID: 1755
		private int {18581} = 950;

		// Token: 0x040006DC RID: 1756
		private Tlist<string> {18582};

		// Token: 0x040006DD RID: 1757
		private Tlist<{18561}.ItemReference> {18583};

		// Token: 0x040006DE RID: 1758
		private Vector2 {18584};

		// Token: 0x040006DF RID: 1759
		private Tlist<{18561}.HazardAreaReference> {18585};

		// Token: 0x040006E0 RID: 1760
		private WorldMapInfo {18586};

		// Token: 0x040006E1 RID: 1761
		private RenderTarget {18587};

		// Token: 0x040006E2 RID: 1762
		private bool {18588};

		// Token: 0x02000155 RID: 341
		private class ItemReference
		{
			// Token: 0x170000E7 RID: 231
			// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0003E715 File Offset: 0x0003C915
			public bool IsWinter
			{
				get
				{
					return this.TerrainID == 6 || this.TerrainID == 7 || this.TerrainID == 20 || this.TerrainID == 22;
				}
			}

			// Token: 0x170000E8 RID: 232
			// (get) Token: 0x060007C6 RID: 1990 RVA: 0x0003E73F File Offset: 0x0003C93F
			public bool IsDark
			{
				get
				{
					return this.TerrainID == 23 || this.TerrainID == 24 || this.TerrainID == 25 || this.TerrainID == 22;
				}
			}

			// Token: 0x060007C7 RID: 1991 RVA: 0x0003E76C File Offset: 0x0003C96C
			public {18561}.ItemReference Clone(int {18589})
			{
				return new {18561}.ItemReference
				{
					LineNumber = {18589},
					LocalPosition = this.LocalPosition,
					LocalPositionY = this.LocalPositionY,
					Rotation = this.Rotation,
					Scale = this.Scale,
					Parameter = string.Empty,
					ModelReference = this.ModelReference,
					IsSelected = this.IsSelected,
					ShallowPointsLocalSpace = this.ShallowPointsLocalSpace,
					PortName = this.PortName,
					TerrainID = this.TerrainID,
					IsFactory = this.IsFactory,
					IsTrader = this.IsTrader,
					IsPBRespawn = this.IsPBRespawn,
					IsAltar = this.IsAltar,
					IsPbObject = this.IsPbObject,
					Hide = this.Hide,
					ApproxSSRadius = this.ApproxSSRadius
				};
			}

			// Token: 0x040006E3 RID: 1763
			public int LineNumber;

			// Token: 0x040006E4 RID: 1764
			public Vector2 LocalPosition;

			// Token: 0x040006E5 RID: 1765
			public float LocalPositionY;

			// Token: 0x040006E6 RID: 1766
			public float Rotation;

			// Token: 0x040006E7 RID: 1767
			public float Scale;

			// Token: 0x040006E8 RID: 1768
			public string Parameter;

			// Token: 0x040006E9 RID: 1769
			public IsleModelInfo ModelReference;

			// Token: 0x040006EA RID: 1770
			public bool IsSelected;

			// Token: 0x040006EB RID: 1771
			public Tlist<Vector2> ShallowPointsLocalSpace;

			// Token: 0x040006EC RID: 1772
			public string PortName;

			// Token: 0x040006ED RID: 1773
			public int TerrainID;

			// Token: 0x040006EE RID: 1774
			public bool IsFactory;

			// Token: 0x040006EF RID: 1775
			public bool IsTrader;

			// Token: 0x040006F0 RID: 1776
			public bool IsPBRespawn;

			// Token: 0x040006F1 RID: 1777
			public bool IsAltar;

			// Token: 0x040006F2 RID: 1778
			public bool IsPbObject;

			// Token: 0x040006F3 RID: 1779
			public bool Hide;

			// Token: 0x040006F4 RID: 1780
			public bool IsPortPharos;

			// Token: 0x040006F5 RID: 1781
			public float ApproxSSRadius;
		}

		// Token: 0x02000156 RID: 342
		private class HazardAreaReference
		{
			// Token: 0x040006F6 RID: 1782
			public Vector2 Pos;

			// Token: 0x040006F7 RID: 1783
			public int HazardIndex;

			// Token: 0x040006F8 RID: 1784
			public bool IsSelected;
		}
	}
}
