using System;
using System.IO;

namespace FangPage.Common
{
	// Token: 0x02000006 RID: 6
	internal class LocalContract : Contract
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002159 File Offset: 0x00000359
		public LocalContract(FileInfo fileInfo)
		{
			this.fileInfo = fileInfo;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000216C File Offset: 0x0000036C
		public long GetFileLength()
		{
			return this.fileInfo.Length;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000218C File Offset: 0x0000038C
		public string GetFileName()
		{
			return this.fileInfo.Name;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000021AC File Offset: 0x000003AC
		public string GetMimeType()
		{
			return "application/octet-stream";
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000021C4 File Offset: 0x000003C4
		public bool IsValid()
		{
			return this.fileInfo != null && this.fileInfo.Exists;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000021EC File Offset: 0x000003EC
		public void Write(Stream output)
		{
			using (BufferedStream bufferedStream = new BufferedStream(this.fileInfo.OpenRead()))
			{
				byte[] array = new byte[4096];
				int count;
				while ((count = bufferedStream.Read(array, 0, array.Length)) > 0)
				{
					output.Write(array, 0, count);
				}
			}
		}

		// Token: 0x0400001B RID: 27
		private FileInfo fileInfo;
	}
}
