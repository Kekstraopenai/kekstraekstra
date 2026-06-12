using System;
using System.Collections.Generic;

namespace TheraEngine.Assets.Graphics
{
	// Token: 0x020001A5 RID: 421
	public class AnimationDictionary
	{
		// Token: 0x06000AA6 RID: 2726 RVA: 0x00035524 File Offset: 0x00033724
		public AnimationDictionary AddLabel(object {15946}, int {15947}, int {15948}, double {15949})
		{
			if ({15948} - {15947} <= 1)
			{
				throw new ArgumentException();
			}
			double item = (double){15947} / {15949};
			double item2 = (double){15948} / {15949};
			this.animations.Add({15946}, new ValueTuple<double, double>(item, item2));
			return this;
		}

		// Token: 0x04000834 RID: 2100
		internal readonly Dictionary<object, ValueTuple<double, double>> animations = new Dictionary<object, ValueTuple<double, double>>(150);
	}
}
