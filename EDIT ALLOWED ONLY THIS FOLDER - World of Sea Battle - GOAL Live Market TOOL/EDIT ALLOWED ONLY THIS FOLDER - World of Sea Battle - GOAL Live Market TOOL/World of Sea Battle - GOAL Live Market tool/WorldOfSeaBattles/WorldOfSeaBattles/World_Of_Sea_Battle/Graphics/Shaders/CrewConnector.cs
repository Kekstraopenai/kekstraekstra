using System;
using System.Collections.Generic;
using System.Linq;
using Common.Game;
using Common.Resources;
using Microsoft.Xna.Framework;
using TheraEngine.Collections;
using TheraEngine.Components;
using TheraEngine.Helpers;
using TheraEngine.Scene;
using World_Of_Sea_Battle.Core;
using World_Of_Sea_Battle.Graphics.Components;

namespace World_Of_Sea_Battle.Graphics.Shaders
{
	// Token: 0x02000462 RID: 1122
	public class CrewConnector
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600186E RID: 6254 RVA: 0x000D3A79 File Offset: 0x000D1C79
		public Sequence Shuffler
		{
			get
			{
				return this.{23657};
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600186F RID: 6255 RVA: 0x000D3A81 File Offset: 0x000D1C81
		public GSI GetLastVersion
		{
			get
			{
				return this.{23653};
			}
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x000D3A8C File Offset: 0x000D1C8C
		public void Initialize(Ship {23642}, Action<UnitScene> {23643}, Action<UnitScene> {23644})
		{
			this.{23654} = {23643};
			this.{23655} = {23644};
			int num = Math.Max({23642}.UsedShip.Crew.Count, {23642}.MaxUnitPlacesCount(true));
			int num2 = num / 4 - 5;
			Tlist<ValueTuple<Vector3, float>> tlist = {23642}.VisualCrewPositions(num2).Clone();
			this.{23657} = new Sequence({23642}.uID + num);
			tlist.Shuffle(this.{23657});
			for (int i = 0; i < Math.Min(tlist.Size, num2); i++)
			{
				Tlist<CrewConnector.Place> places = this.Places;
				CrewConnector.Place place = new CrewConnector.Place(tlist.Array[i], null);
				places.Add(place);
			}
			this.{23656} = 1f / (float)num * (float)this.Places.Size;
			this.Update({23642}, null);
		}

		// Token: 0x06001871 RID: 6257 RVA: 0x000D3B5A File Offset: 0x000D1D5A
		public void Clean()
		{
			this.Places.Clear();
			this.{23653}.Clean();
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x000D3B74 File Offset: 0x000D1D74
		public void Update(Ship {23645}, Vector3? {23646})
		{
			if (Debugging.CrewDebnugging)
			{
				foreach (CrewConnector.Place place in ((IEnumerable<CrewConnector.Place>)this.Places))
				{
					if (place.CrewOrNull == null)
					{
						place.CrewOrNull = CrewConnector.CreateCrewUnitScene({23645}, UnitRole.Musketeer, place.PosAtShip, this.{23657});
						this.{23654}(place.CrewOrNull);
					}
				}
				return;
			}
			if (!GSI.Equals({23645}.UsedShip.Crew.Raw, this.{23653}))
			{
				GSI gsi;
				GSI gsi2;
				GSI.PutDiff(this.{23653}, {23645}.UsedShip.Crew.Raw, out gsi, out gsi2);
				this.{23653}.Clean();
				this.{23653}.Add({23645}.UsedShip.Crew.Raw);
				this.{23652}.Clear();
				foreach (GSILocalEnumerablePair<UnitInfo> gsilocalEnumerablePair in ((IEnumerable<GSILocalEnumerablePair<UnitInfo>>)gsi2.UnitInfo))
				{
					int num = Rand.Round((float)gsilocalEnumerablePair.Count * this.{23656});
					int num2 = gsilocalEnumerablePair.Count - num;
					for (int i = 0; i < num2; i++)
					{
						CrewConnector.Place place2 = this.Places.Rand((CrewConnector.Place {23668}) => {23668}.CrewOrNull != null, null);
						if (place2 == null)
						{
							break;
						}
						UnitScene unitScene = CrewConnector.CreateCrewUnitScene({23645}, UnitRole.Sailfish, place2.PosAtShip, Rand.Instance);
						unitScene.OnDeath({23645});
						this.{23654}(unitScene);
						Tlist<CrewConnector.Place> tempPlaces = this.TempPlaces;
						CrewConnector.Place place3 = new CrewConnector.Place(place2.PosAtShip, unitScene);
						tempPlaces.Add(place3);
					}
					if (num != 0)
					{
						UnitRole role = CrewConnector.GetRole(gsilocalEnumerablePair.Info);
						this.{23652}.Add(this.Places);
						this.{23652}.Shuffle(this.{23657});
						foreach (CrewConnector.Place place4 in ((IEnumerable<CrewConnector.Place>)this.{23652}))
						{
							if (place4.CrewOrNull != null && place4.CrewOrNull.Role == role && !place4.CrewOrNull.IsDeath)
							{
								place4.CrewOrNull.OnDeath({23645});
								if (--num == 0)
								{
									break;
								}
							}
						}
						this.{23652}.Clear();
					}
				}
				foreach (GSILocalEnumerablePair<UnitInfo> gsilocalEnumerablePair2 in ((IEnumerable<GSILocalEnumerablePair<UnitInfo>>)gsi.UnitInfo))
				{
					int num3 = Rand.Round((float)gsilocalEnumerablePair2.Count * this.{23656});
					if (num3 != 0)
					{
						UnitRole role2 = CrewConnector.GetRole(gsilocalEnumerablePair2.Info);
						this.{23652}.Add(this.Places);
						this.{23652}.Shuffle(this.{23657});
						foreach (CrewConnector.Place place5 in ((IEnumerable<CrewConnector.Place>)this.{23652}))
						{
							if (place5.CrewOrNull == null)
							{
								place5.CrewOrNull = CrewConnector.CreateCrewUnitScene({23645}, role2, place5.PosAtShip, this.{23657});
								this.{23654}(place5.CrewOrNull);
								if (--num3 == 0)
								{
									break;
								}
							}
						}
						this.{23652}.Clear();
					}
				}
			}
			foreach (CrewConnector.Place place6 in ((IEnumerable<CrewConnector.Place>)this.Places))
			{
				if (place6.CrewOrNull != null && place6.CrewOrNull.IsDeath && place6.CrewOrNull.Transparancy == 0f)
				{
					this.{23655}(place6.CrewOrNull);
					place6.CrewOrNull = null;
				}
			}
			for (int j = 0; j < this.TempPlaces.Size; j++)
			{
				CrewConnector.Place place7 = this.TempPlaces.Array[j];
				if (place7.CrewOrNull.IsDeath && place7.CrewOrNull.Transparancy == 0f)
				{
					this.{23655}(place7.CrewOrNull);
					this.TempPlaces.FastRemoveAt(j);
					j--;
				}
			}
			this.TempPlaces.RemoveAll((CrewConnector.Place {23669}) => {23669}.CrewOrNull.IsDeath && {23669}.CrewOrNull.Transparancy == 0f);
			if ({23645} is Npc && !{23645}.UsedShip.Crew.Raw.IsEmpty)
			{
				if (this.Places.All((CrewConnector.Place {23670}) => {23670}.CrewOrNull == null))
				{
					this.{23653}.Clean();
				}
			}
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x000D40E4 File Offset: 0x000D22E4
		public static UnitRole GetRole(UnitInfo {23647})
		{
			if (!{23647}.IsMusketeer)
			{
				return UnitRole.Sailfish;
			}
			return UnitRole.Musketeer;
		}

		// Token: 0x06001874 RID: 6260 RVA: 0x000D40F4 File Offset: 0x000D22F4
		public static UnitScene CreateCrewUnitScene(Ship {23648}, UnitRole {23649}, ValueTuple<Vector3, float> {23650}, Sequence {23651})
		{
			ModelRenderer modelRenderer = new ModelRenderer(LocalContent.Loaded.GetRandomUnitModel({23649}, {23651}), null, {23651}.Range(0.7f, 0.9f));
			modelRenderer.LocalTransformOrNull = new Transform3D({23650}.Item1, new Vector3(0f, {23650}.Item2 + {23651}.Range(-0.2f, 0.2f), 0f), Vector3.One * 0.011f * {23651}.Range(0.97f, 1.03f));
			UnitScene unitScene = new UnitScene(modelRenderer, {23649}, {23648}.Transform);
			modelRenderer.Tag = unitScene;
			return unitScene;
		}

		// Token: 0x040016CE RID: 5838
		public Tlist<CrewConnector.Place> Places = new Tlist<CrewConnector.Place>(50);

		// Token: 0x040016CF RID: 5839
		public Tlist<CrewConnector.Place> TempPlaces = new Tlist<CrewConnector.Place>(50);

		// Token: 0x040016D0 RID: 5840
		private Tlist<CrewConnector.Place> {23652} = new Tlist<CrewConnector.Place>();

		// Token: 0x040016D1 RID: 5841
		private GSI {23653} = new GSI();

		// Token: 0x040016D2 RID: 5842
		private Action<UnitScene> {23654};

		// Token: 0x040016D3 RID: 5843
		private Action<UnitScene> {23655};

		// Token: 0x040016D4 RID: 5844
		private float {23656};

		// Token: 0x040016D5 RID: 5845
		private Sequence {23657};

		// Token: 0x02000463 RID: 1123
		public class Place
		{
			// Token: 0x06001876 RID: 6262 RVA: 0x000D41CB File Offset: 0x000D23CB
			public Place(ValueTuple<Vector3, float> {23660}, UnitScene {23661})
			{
				this.PosAtShip = {23660};
				this.CrewOrNull = {23661};
			}

			// Token: 0x040016D6 RID: 5846
			public ValueTuple<Vector3, float> PosAtShip;

			// Token: 0x040016D7 RID: 5847
			public UnitScene CrewOrNull;
		}

		// Token: 0x02000464 RID: 1124
		public struct Approximator
		{
			// Token: 0x06001877 RID: 6263 RVA: 0x000D41E4 File Offset: 0x000D23E4
			public Approximator(CrewConnector {23663})
			{
				this.{23666} = {23663}.{23656};
				this.OrderedNonNullPlaces = new Tlist<CrewConnector.Place>();
				foreach (CrewConnector.Place place in ((IEnumerable<CrewConnector.Place>){23663}.Places))
				{
					if (place.CrewOrNull != null)
					{
						this.OrderedNonNullPlaces.Add(place);
					}
				}
				this.OrderedNonNullPlaces.SortTop((CrewConnector.Place {23667}) => {23667}.PosAtShip.Item1.X);
			}

			// Token: 0x06001878 RID: 6264 RVA: 0x000D4280 File Offset: 0x000D2480
			public Vector3 GetApproxMiddlePosition(int {23664}, int {23665})
			{
				if (this.OrderedNonNullPlaces.Size == 0)
				{
					return Vector3.Zero;
				}
				int num = Math.Min(this.OrderedNonNullPlaces.Size - 1, Rand.Round((float){23664} * this.{23666}));
				int num2 = Math.Max(1, Rand.Round((float){23665} * this.{23666}));
				num2 = Math.Min(this.OrderedNonNullPlaces.Size - num, num2);
				Vector3 vector = default(Vector3);
				for (int i = num; i < num + num2; i++)
				{
					vector += this.OrderedNonNullPlaces.Array[i].PosAtShip.Item1;
				}
				return vector / (float)num2;
			}

			// Token: 0x040016D8 RID: 5848
			public Tlist<CrewConnector.Place> OrderedNonNullPlaces;

			// Token: 0x040016D9 RID: 5849
			private float {23666};
		}
	}
}
