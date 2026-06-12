using System;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Assets.Graphics;
using TheraEngine.Collections;

namespace TheraEngine.Graphics.Models
{
	// Token: 0x0200016B RID: 363
	public sealed class UWOpenModel : UWModel
	{
		// Token: 0x060009AF RID: 2479 RVA: 0x0002F396 File Offset: 0x0002D596
		private UWOpenModel()
		{
		}

		// Token: 0x060009B0 RID: 2480 RVA: 0x0002F39E File Offset: 0x0002D59E
		private UWOpenModel(InstancedMaterialDictionary {15242}, Model {15243}, params int[] {15244}) : base({15242}, {15243}, {15244})
		{
		}

		// Token: 0x060009B1 RID: 2481 RVA: 0x0002F3AC File Offset: 0x0002D5AC
		protected override void OnInit()
		{
			int num = this.MeshParts.Length;
			this.MeshPartsSource = new MeshPartOpenData[num];
			for (int i = 0; i < num; i++)
			{
				this.MeshPartsSource[i] = new MeshPartOpenData(this.MeshParts[i]);
			}
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002F3F0 File Offset: 0x0002D5F0
		public static UWOpenModel Create(InstancedMaterialDictionary {15245}, Model {15246}, string {15247})
		{
			int count = {15246}.Meshes.Count;
			Tlist<int> tlist = new Tlist<int>(count);
			for (int i = 0; i < count; i++)
			{
				if ({15246}.Meshes[i].Name.Contains({15247}))
				{
					tlist.Add(i);
				}
			}
			if (tlist.Size == 0)
			{
				return null;
			}
			return new UWOpenModel({15245}, {15246}, tlist.ToArray());
		}

		// Token: 0x060009B3 RID: 2483 RVA: 0x0002F454 File Offset: 0x0002D654
		public new static UWOpenModel CreateAll(InstancedMaterialDictionary {15248}, Model {15249})
		{
			int count = {15249}.Meshes.Count;
			int[] array = new int[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = i;
			}
			return new UWOpenModel({15248}, {15249}, array);
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002F48C File Offset: 0x0002D68C
		public new static UWOpenModel CreateAlone(InstancedMaterialDictionary {15250}, Model {15251}, int {15252})
		{
			return new UWOpenModel({15250}, {15251}, new int[]
			{
				{15252}
			});
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x0002F49F File Offset: 0x0002D69F
		public new static UWOpenModel CreateRange(InstancedMaterialDictionary {15253}, Model {15254}, params int[] {15255})
		{
			return new UWOpenModel({15253}, {15254}, {15255});
		}

		// Token: 0x060009B6 RID: 2486 RVA: 0x0002F4AC File Offset: 0x0002D6AC
		public new static UWOpenModel CreateStartOf(InstancedMaterialDictionary {15256}, Model {15257}, int {15258})
		{
			int[] array = new int[{15257}.Meshes.Count - {15258}];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = {15258} + i;
			}
			return new UWOpenModel({15256}, {15257}, array);
		}

		// Token: 0x060009B7 RID: 2487 RVA: 0x0002F4E8 File Offset: 0x0002D6E8
		public new static UWOpenModel CreateIdentity()
		{
			return new UWOpenModel();
		}

		// Token: 0x040006A3 RID: 1699
		public MeshPartOpenData[] MeshPartsSource;
	}
}
