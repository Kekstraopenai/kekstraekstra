using System;
using System.Collections.Generic;
using System.Linq;
using TheraEngine.Helpers;

namespace TheraEngine.Collections
{
	// Token: 0x0200011B RID: 283
	public class ScoreDictionary<T>
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060007FF RID: 2047 RVA: 0x0002814C File Offset: 0x0002634C
		public KeyValuePair<T, float> MostScrore
		{
			get
			{
				return this.mostScore;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x00028154 File Offset: 0x00026354
		public float TotalScore
		{
			get
			{
				return this.totalScore;
			}
		}

		// Token: 0x1700016E RID: 366
		public float this[T key]
		{
			get
			{
				float result;
				if (this.dict.TryGetValue(key, out result))
				{
					return result;
				}
				return 0f;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00028180 File Offset: 0x00026380
		public int Count
		{
			get
			{
				return this.dict.Count;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0002818D File Offset: 0x0002638D
		public Dictionary<T, float> BaseDictionary
		{
			get
			{
				return this.dict;
			}
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00028195 File Offset: 0x00026395
		public ScoreDictionary()
		{
			this.dict = new Dictionary<T, float>();
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x000281A8 File Offset: 0x000263A8
		public ScoreDictionary(Dictionary<T, float> baseDict)
		{
			this.dict = baseDict;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x000281B7 File Offset: 0x000263B7
		public ScoreDictionary(int approxRealItemsCount)
		{
			this.dict = new Dictionary<T, float>(HashHelper.NextSize(false, approxRealItemsCount));
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x000281D4 File Offset: 0x000263D4
		public void AddScore(T key, float addScore)
		{
			float num;
			if (this.dict.TryGetValue(key, out num))
			{
				num += addScore;
				this.dict[key] = num;
			}
			else
			{
				num = addScore;
				this.dict.Add(key, addScore);
			}
			if (num > this.mostScore.Value)
			{
				this.mostScore = new KeyValuePair<T, float>(key, num);
			}
			this.totalScore += addScore;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0002823C File Offset: 0x0002643C
		public void Clear()
		{
			this.dict.Clear();
			this.mostScore = default(KeyValuePair<T, float>);
			this.totalScore = 0f;
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00028260 File Offset: 0x00026460
		public void Multiply(float value)
		{
			if (this.dict.Count == 0)
			{
				return;
			}
			this.mostScore = new KeyValuePair<T, float>(this.mostScore.Key, this.mostScore.Value * value);
			foreach (T t in this.dict.Keys.ToArray<T>())
			{
				Dictionary<T, float> dictionary = this.dict;
				T key = t;
				dictionary[key] *= value;
			}
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x000282DC File Offset: 0x000264DC
		public IEnumerable<KeyValuePair<T, float>> GetFilteredKeyValuePairs(Func<T, bool> condition = null)
		{
			if (condition == null)
			{
				return from kv in this.dict
				orderby kv.Key
				select kv;
			}
			return from kv in this.dict
			where condition(kv.Key)
			orderby kv.Key
			select kv;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x00028364 File Offset: 0x00026564
		public static ScoreDictionary<T> Merge(params ScoreDictionary<T>[] dicts)
		{
			return ScoreDictionary<T>.Merge(dicts);
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0002836C File Offset: 0x0002656C
		public static ScoreDictionary<T> Merge(IEnumerable<ScoreDictionary<T>> dicts)
		{
			ScoreDictionary<T> scoreDictionary = new ScoreDictionary<T>();
			foreach (ScoreDictionary<T> scoreDictionary2 in dicts)
			{
				foreach (KeyValuePair<T, float> keyValuePair in scoreDictionary2.BaseDictionary)
				{
					scoreDictionary.AddScore(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return scoreDictionary;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00028404 File Offset: 0x00026604
		public ScoreDictionary<T> Extract(Func<T, bool> keySelector)
		{
			ScoreDictionary<T> scoreDictionary = new ScoreDictionary<T>();
			foreach (KeyValuePair<T, float> keyValuePair in this.BaseDictionary)
			{
				if (keySelector(keyValuePair.Key))
				{
					scoreDictionary.AddScore(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return scoreDictionary;
		}

		// Token: 0x04000599 RID: 1433
		private Dictionary<T, float> dict;

		// Token: 0x0400059A RID: 1434
		private KeyValuePair<T, float> mostScore;

		// Token: 0x0400059B RID: 1435
		private float totalScore;
	}
}
