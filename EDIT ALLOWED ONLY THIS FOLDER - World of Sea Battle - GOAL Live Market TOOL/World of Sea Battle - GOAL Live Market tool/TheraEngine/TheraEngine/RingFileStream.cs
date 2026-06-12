using System;
using System.IO;

namespace TheraEngine
{
	// Token: 0x0200002A RID: 42
	public class RingFileStream
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000168 RID: 360 RVA: 0x000085F5 File Offset: 0x000067F5
		public int BlocksAvailable
		{
			get
			{
				if (!this.{11775})
				{
					return this.{11774};
				}
				return this.{11772};
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x0000860C File Offset: 0x0000680C
		public RingFileStream(int {11764}, int {11765}, string {11766}, bool {11767})
		{
			if ({11767})
			{
				long val = new DriveInfo(Path.GetPathRoot(Path.GetFullPath({11766}))).AvailableFreeSpace / (long){11764};
				{11765} = (int)Math.Min((long){11765}, val);
			}
			this.{11771} = {11764};
			this.{11772} = {11765};
			this.{11773} = File.Open({11766}, FileMode.OpenOrCreate);
			if (this.{11773}.Length >= 4L)
			{
				byte[] array = new byte[4];
				this.{11773}.Position = 0L;
				this.{11773}.Read(array, 0, 4);
				int num = BitConverter.ToInt32(array, 0);
				this.{11774} = (int)Math.Min((long)Math.Abs(num), this.{11773}.Length / (long){11764});
				this.{11775} = (num < 0);
			}
			if (this.{11773}.Length < (long)({11764} * {11765} + 4))
			{
				this.{11773}.SetLength((long)({11764} * {11765} + 4));
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00008700 File Offset: 0x00006900
		public void Write(byte[] {11768})
		{
			if ({11768}.Length > this.{11771})
			{
				throw new InvalidOperationException();
			}
			object obj = this.{11776};
			lock (obj)
			{
				if (this.{11774} + 1 == this.{11772})
				{
					this.{11775} = true;
				}
				int num = (this.{11774} + 1) % this.{11772};
				this.{11773}.Position = 0L;
				this.{11773}.Write(BitConverter.GetBytes(num * (this.{11775} ? -1 : 1)), 0, 4);
				this.{11773}.Position = (long)(this.{11774} * this.{11771} + 4);
				this.{11773}.Write({11768}, 0, {11768}.Length);
				this.{11774} = num;
				if (this.{11774} % this.FlushThrottle == 0)
				{
					this.{11773}.Flush();
				}
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x000087EC File Offset: 0x000069EC
		public void Read(int {11769}, Action<byte[]> {11770})
		{
			byte[] array = new byte[this.{11771}];
			object obj = this.{11776};
			lock (obj)
			{
				{11769} = Math.Min(this.BlocksAvailable, {11769});
				for (int i = 1; i <= {11769}; i++)
				{
					int num = this.{11774} - i;
					if (num < 0)
					{
						num = this.{11772} + num;
					}
					this.{11773}.Position = (long)(num * this.{11771} + 4);
					this.{11773}.Read(array, 0, this.{11771});
					{11770}(array);
				}
			}
		}

		// Token: 0x040000C2 RID: 194
		private int {11771};

		// Token: 0x040000C3 RID: 195
		private int {11772};

		// Token: 0x040000C4 RID: 196
		private FileStream {11773};

		// Token: 0x040000C5 RID: 197
		private int {11774};

		// Token: 0x040000C6 RID: 198
		private bool {11775};

		// Token: 0x040000C7 RID: 199
		private object {11776} = new object();

		// Token: 0x040000C8 RID: 200
		private byte[] {11777};

		// Token: 0x040000C9 RID: 201
		public int FlushThrottle = 10;
	}
}
