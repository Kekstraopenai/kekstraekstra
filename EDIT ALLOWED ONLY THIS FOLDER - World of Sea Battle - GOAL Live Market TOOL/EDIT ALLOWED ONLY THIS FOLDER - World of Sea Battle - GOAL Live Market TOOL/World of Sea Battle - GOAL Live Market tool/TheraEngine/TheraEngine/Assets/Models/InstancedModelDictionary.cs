using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;
using TheraEngine.Core;
using TheraEngine.Graphics.Models;

namespace TheraEngine.Assets.Models
{
	// Token: 0x02000197 RID: 407
	public class InstancedModelDictionary
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x06000A63 RID: 2659 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected virtual bool IsServerBuild
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x0000E21A File Offset: 0x0000C41A
		protected virtual bool LoadNullShapes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000A65 RID: 2661 RVA: 0x000340A2 File Offset: 0x000322A2
		public InstancedModelDictionary(InstancedMaterialDictionary {15812}, ContentManager {15813}, string {15814})
		{
			this.{15825} = {15812};
			this.{15826} = {15813};
			this.{15827} = {15814};
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x000340CA File Offset: 0x000322CA
		public Dictionary<string, InstancedModel>.KeyCollection LoadedNames()
		{
			return this.{15823}.Keys;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x000340D8 File Offset: 0x000322D8
		public InstancedModel Contains(string {15815})
		{
			InstancedModel result;
			if (this.{15823}.TryGetValue({15815}, out result))
			{
				return result;
			}
			InstancedModel result2;
			this.{15823}.Add({15815}, result2 = this.{15816}({15815}));
			return result2;
		}

		// Token: 0x06000A68 RID: 2664 RVA: 0x00034110 File Offset: 0x00032310
		private InstancedModel {15816}(string {15817})
		{
			string text = this.{15827} + {15817};
			Model model;
			Model model2;
			if (this.IsServerBuild)
			{
				model = this.{15826}.TryLoadOrNull<Model>(text);
				model2 = model;
			}
			else
			{
				model = this.{15826}.TryLoadOrNull<Model>(text + "_lod1");
				model2 = this.{15826}.Load<Model>(text);
			}
			Model model3 = this.{15826}.TryLoadOrNull<Model>(text + "_shape");
			return this.CreateModel(this.{15825}, model2, model ?? model2, (model3 == null) ? null : UWOpenModel.CreateAll(null, model3), {15817});
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x000341A0 File Offset: 0x000323A0
		protected virtual InstancedModel CreateModel(InstancedMaterialDictionary {15818}, Model {15819}, Model {15820}, UWOpenModel {15821}, string {15822})
		{
			UWModel uwmodel = UWModel.CreateAll({15818}, {15819});
			UWModel {15808} = ({15819} == {15820}) ? uwmodel : UWModel.CreateAll({15818}, {15820});
			return new InstancedModel(uwmodel, {15808});
		}

		// Token: 0x040007ED RID: 2029
		private Dictionary<string, InstancedModel> {15823} = new Dictionary<string, InstancedModel>();

		// Token: 0x040007EE RID: 2030
		private Tlist<string> {15824};

		// Token: 0x040007EF RID: 2031
		private InstancedMaterialDictionary {15825};

		// Token: 0x040007F0 RID: 2032
		private ContentManager {15826};

		// Token: 0x040007F1 RID: 2033
		private string {15827};
	}
}
