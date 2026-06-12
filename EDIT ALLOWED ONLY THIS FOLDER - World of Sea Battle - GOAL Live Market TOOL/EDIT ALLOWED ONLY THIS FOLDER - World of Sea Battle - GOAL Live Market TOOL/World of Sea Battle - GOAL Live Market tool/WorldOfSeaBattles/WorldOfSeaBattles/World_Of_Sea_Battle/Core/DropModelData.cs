using System;
using Microsoft.Xna.Framework.Graphics;
using TheraEngine.Collections;
using TheraEngine.Graphics.Models;

namespace World_Of_Sea_Battle.Core
{
	// Token: 0x020004E1 RID: 1249
	public struct DropModelData
	{
		// Token: 0x06001BC5 RID: 7109 RVA: 0x000FAF1C File Offset: 0x000F911C
		public DropModelData(Model {25134}, string {25135})
		{
			this.SourceName = {25135};
			this.FloatingParts = UWModel.Separate(Materials.TexturesDatabase, {25134}, "Floating");
			Tlist<UWModel> tlist = UWModel.Separate(Materials.TexturesDatabase, {25134}, "Fixed");
			this.FixedPart = ((tlist.Size == 0) ? null : tlist.Array[0]);
			Tlist<UWModel> tlist2 = UWModel.Separate(Materials.TexturesDatabase, {25134}, "World");
			this.WorldDecorPart = ((tlist2.Size == 0) ? null : tlist2.Array[0]);
			this.WholeModel = UWModel.CreateAll(Materials.TexturesDatabase, {25134});
			foreach (ModelMesh modelMesh in {25134}.Meshes)
			{
				if (!modelMesh.Name.Contains("Floating") && !modelMesh.Name.Contains("Fixed") && !modelMesh.Name.Contains("World"))
				{
					throw new InvalidOperationException("Check drop models");
				}
			}
		}

		// Token: 0x04001A71 RID: 6769
		public Tlist<UWModel> FloatingParts;

		// Token: 0x04001A72 RID: 6770
		public UWModel FixedPart;

		// Token: 0x04001A73 RID: 6771
		public UWModel WorldDecorPart;

		// Token: 0x04001A74 RID: 6772
		public UWModel WholeModel;

		// Token: 0x04001A75 RID: 6773
		public string SourceName;
	}
}
