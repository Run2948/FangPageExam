using System;
using System.IO;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;

namespace FangPage.MVC
{
	// Token: 0x02000011 RID: 17
	public class FPZip : IDisposable
	{
		// Token: 0x060000E7 RID: 231 RVA: 0x0000AFAD File Offset: 0x000091AD
		public FPZip()
		{
			this.file = ZipFile.Create(this.ms);
			this.file.BeginUpdate();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000AFE0 File Offset: 0x000091E0
		public void AddDirectory(string dirpath)
		{
			this.AddDirectory(dirpath, "");
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000AFF0 File Offset: 0x000091F0
		public void AddDirectory(string dirpath, string entryname)
		{
			if (dirpath.EndsWith("\\") || dirpath.EndsWith("/"))
			{
				dirpath = dirpath.Substring(0, dirpath.Length - 1);
			}
			if (Directory.Exists(dirpath))
			{
				if (string.IsNullOrEmpty(entryname))
				{
					entryname = dirpath.Substring(dirpath.LastIndexOf("\\") + 1);
				}
				this.ZipDirectory(this.file, dirpath, entryname);
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000B074 File Offset: 0x00009274
		public void AddFile(string filepath)
		{
			this.AddFile(filepath, "");
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000B084 File Offset: 0x00009284
		public void AddFile(string filepath, string entryname)
		{
			if (File.Exists(filepath))
			{
				if (string.IsNullOrEmpty(entryname))
				{
					entryname = Path.GetFileName(filepath);
				}
				this.file.Add(filepath, entryname);
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000B0C4 File Offset: 0x000092C4
		public void ZipDown(string zipname)
		{
			this.file.CommitUpdate();
			byte[] array = new byte[this.ms.Length];
			this.ms.Position = 0L;
			this.ms.Read(array, 0, array.Length);
			HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + zipname);
			HttpContext.Current.Response.BinaryWrite(array);
			HttpContext.Current.Response.Flush();
			HttpContext.Current.Response.End();
		}

		// Token: 0x060000ED RID: 237 RVA: 0x0000B164 File Offset: 0x00009364
		public void ZipSave(string zippath)
		{
			this.file.CommitUpdate();
			byte[] array = new byte[this.ms.Length];
			this.ms.Position = 0L;
			this.ms.Read(array, 0, array.Length);
			string directoryName = Path.GetDirectoryName(zippath);
			string fileName = Path.GetFileName(zippath);
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			if (File.Exists(zippath))
			{
				File.Delete(zippath);
			}
			using (FileStream fileStream = new FileStream(zippath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				fileStream.Write(array, 0, array.Length);
				fileStream.Close();
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000B230 File Offset: 0x00009430
		private void ZipDirectory(ZipFile file, string sitemappath, string entryname)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sitemappath);
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				this.ZipDirectory(file, sitemappath + "\\" + directoryInfo2.Name, entryname + "\\" + directoryInfo2.Name);
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				file.Add(fileInfo.FullName, entryname + "\\" + fileInfo.Name);
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x0000B2DE File Offset: 0x000094DE
		public void Close()
		{
			this.ms.Close();
			this.file.Close();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000B2F9 File Offset: 0x000094F9
		public void Dispose()
		{
			this.ms.Close();
			this.file.Close();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000B314 File Offset: 0x00009514
		public static bool UnZipFile(string zipFilePath)
		{
			return FPZip.UnZipFile(zipFilePath, "");
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x0000B334 File Offset: 0x00009534
		public static bool UnZipFile(string zipFilePath, string unZipDir)
		{
			if (unZipDir == string.Empty)
			{
				unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
			}
			if (!unZipDir.EndsWith("\\"))
			{
				unZipDir += "\\";
			}
			if (!Directory.Exists(unZipDir))
			{
				Directory.CreateDirectory(unZipDir);
			}
			using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(zipFilePath)))
			{
				ZipEntry nextEntry;
				while ((nextEntry = zipInputStream.GetNextEntry()) != null)
				{
					string text = Path.GetDirectoryName(nextEntry.Name);
					string fileName = Path.GetFileName(nextEntry.Name);
					if (text.Length > 0)
					{
						Directory.CreateDirectory(unZipDir + text);
					}
					if (!text.EndsWith("\\"))
					{
						text += "\\";
					}
					if (fileName != string.Empty)
					{
						using (FileStream fileStream = File.Create(unZipDir + nextEntry.Name))
						{
							byte[] array = new byte[2048];
							for (;;)
							{
								int num = zipInputStream.Read(array, 0, array.Length);
								if (num <= 0)
								{
									break;
								}
								fileStream.Write(array, 0, num);
							}
						}
					}
				}
			}
			return true;
		}

		// Token: 0x0400003B RID: 59
		private ZipFile file;

		// Token: 0x0400003C RID: 60
		private MemoryStream ms = new MemoryStream();
	}
}
