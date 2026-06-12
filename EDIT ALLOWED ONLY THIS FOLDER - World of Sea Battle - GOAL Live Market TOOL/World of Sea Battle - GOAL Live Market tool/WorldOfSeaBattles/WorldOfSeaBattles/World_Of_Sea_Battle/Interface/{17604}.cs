using System;
using System.Runtime.CompilerServices;

namespace World_Of_Sea_Battle.Interface
{
	// Token: 0x020000BC RID: 188
	public class {17604}
	{
		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0002598B File Offset: 0x00023B8B
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00025993 File Offset: 0x00023B93
		public bool CreateBlockingBackground { get; set; } = true;

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0002599C File Offset: 0x00023B9C
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x000259A4 File Offset: 0x00023BA4
		public bool BackgroundIsTransparent { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x000259AD File Offset: 0x00023BAD
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x000259B5 File Offset: 0x00023BB5
		public bool CloseThroughBackground { get; set; } = true;

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000259BE File Offset: 0x00023BBE
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x000259C6 File Offset: 0x00023BC6
		public bool BlockGameInput { get; set; } = true;

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000259CF File Offset: 0x00023BCF
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x000259D7 File Offset: 0x00023BD7
		public bool StopShip { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000259E0 File Offset: 0x00023BE0
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x000259E8 File Offset: 0x00023BE8
		public bool IsScrollablePage { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000259F1 File Offset: 0x00023BF1
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x000259F9 File Offset: 0x00023BF9
		public bool AddBackgroundParticles { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004AE RID: 1198 RVA: 0x00025A02 File Offset: 0x00023C02
		// (set) Token: 0x060004AF RID: 1199 RVA: 0x00025A0A File Offset: 0x00023C0A
		public bool QuickClosing { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004B0 RID: 1200 RVA: 0x00025A13 File Offset: 0x00023C13
		// (set) Token: 0x060004B1 RID: 1201 RVA: 0x00025A1B File Offset: 0x00023C1B
		public bool AddDecorations { get; set; } = true;

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00025A24 File Offset: 0x00023C24
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x00025A2C File Offset: 0x00023C2C
		public bool DarkHeader { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x00025A35 File Offset: 0x00023C35
		public static {17604} InGameWindow
		{
			get
			{
				return new {17604}();
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x00025A3C File Offset: 0x00023C3C
		public static {17604} InGameWindowWithoutDeco
		{
			get
			{
				return new {17604}
				{
					AddDecorations = false
				};
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00025A4A File Offset: 0x00023C4A
		public static {17604} InGameWindowBlockShip
		{
			get
			{
				return new {17604}
				{
					StopShip = true
				};
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x00025A58 File Offset: 0x00023C58
		public static {17604} InGameWindowBlockShipDialogWithoutDeco
		{
			get
			{
				return new {17604}
				{
					StopShip = true,
					CloseThroughBackground = false,
					AddDecorations = false
				};
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x00025A74 File Offset: 0x00023C74
		public static {17604} PortPage
		{
			get
			{
				return new {17604}
				{
					BlockGameInput = false,
					IsScrollablePage = true,
					QuickClosing = true
				};
			}
		}

		// Token: 0x040003FB RID: 1019
		[CompilerGenerated]
		private bool {17615};

		// Token: 0x040003FC RID: 1020
		[CompilerGenerated]
		private bool {17616};

		// Token: 0x040003FD RID: 1021
		[CompilerGenerated]
		private bool {17617};

		// Token: 0x040003FE RID: 1022
		[CompilerGenerated]
		private bool {17618};

		// Token: 0x040003FF RID: 1023
		[CompilerGenerated]
		private bool {17619};

		// Token: 0x04000400 RID: 1024
		[CompilerGenerated]
		private bool {17620};

		// Token: 0x04000401 RID: 1025
		[CompilerGenerated]
		private bool {17621};

		// Token: 0x04000402 RID: 1026
		[CompilerGenerated]
		private bool {17622};

		// Token: 0x04000403 RID: 1027
		[CompilerGenerated]
		private bool {17623};

		// Token: 0x04000404 RID: 1028
		[CompilerGenerated]
		private bool {17624};
	}
}
