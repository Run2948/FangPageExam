using System;
using System.Diagnostics;
using System.IO;
using System.Web;
using ICSharpCode.SharpZipLib.Zip;

namespace FangPage.Common
{
	// Token: 0x02000015 RID: 21
	public class FPZip : IDisposable
	{
		// Token: 0x06000116 RID: 278 RVA: 0x00007C10 File Offset: 0x00005E10
		public FPZip()
		{
			this.file = ZipFile.Create(this.ms);
			this.file.BeginUpdate();
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00007C42 File Offset: 0x00005E42
		public void AddDir(string dirpath)
		{
			this.AddDir(dirpath, "");
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00007C54 File Offset: 0x00005E54
		public void AddDir(string dirpath, string entryname)
		{
			bool flag = dirpath.EndsWith("\\") || dirpath.EndsWith("/");
			if (flag)
			{
				dirpath = dirpath.Substring(0, dirpath.Length - 1);
			}
			bool flag2 = !Directory.Exists(dirpath);
			if (!flag2)
			{
				bool flag3 = string.IsNullOrEmpty(entryname);
				if (flag3)
				{
					entryname = dirpath.Substring(dirpath.LastIndexOf("\\") + 1);
				}
				this.ZipDir(this.file, dirpath, entryname);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00007CD4 File Offset: 0x00005ED4
		public void AddFile(string filepath)
		{
			this.AddFile(filepath, "");
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00007CE4 File Offset: 0x00005EE4
		public void AddFile(string filepath, string entryname)
		{
			bool flag = !File.Exists(filepath);
			if (!flag)
			{
				bool flag2 = string.IsNullOrEmpty(entryname);
				if (flag2)
				{
					entryname = Path.GetFileName(filepath);
				}
				this.file.Add(filepath, entryname);
			}
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007D24 File Offset: 0x00005F24
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

		// Token: 0x0600011C RID: 284 RVA: 0x00007DC4 File Offset: 0x00005FC4
		public void ZipSave(string zippath)
		{
			this.file.CommitUpdate();
			byte[] array = new byte[this.ms.Length];
			this.ms.Position = 0L;
			this.ms.Read(array, 0, array.Length);
			string directoryName = Path.GetDirectoryName(zippath);
			string fileName = Path.GetFileName(zippath);
			bool flag = !Directory.Exists(directoryName);
			if (flag)
			{
				Directory.CreateDirectory(directoryName);
			}
			bool flag2 = File.Exists(zippath);
			if (flag2)
			{
				File.Delete(zippath);
			}
			using (FileStream fileStream = new FileStream(zippath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				fileStream.Write(array, 0, array.Length);
				fileStream.Close();
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00007E8C File Offset: 0x0000608C
		private void ZipDir(ZipFile file, string sitemappath, string entryname)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sitemappath);
			foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
			{
				this.ZipDir(file, sitemappath + "\\" + directoryInfo2.Name, entryname + "\\" + directoryInfo2.Name);
			}
			foreach (FileInfo fileInfo in directoryInfo.GetFiles())
			{
				file.Add(fileInfo.FullName, entryname + "\\" + fileInfo.Name);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007F2C File Offset: 0x0000612C
		public void Close()
		{
			this.ms.Close();
			this.file.Close();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00007F2C File Offset: 0x0000612C
		public void Dispose()
		{
			this.ms.Close();
			this.file.Close();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00007F48 File Offset: 0x00006148
		public static bool UnZip(string zipFilePath)
		{
			return FPZip.UnZip(zipFilePath, "");
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00007F68 File Offset: 0x00006168
		public static bool UnZip(string zipFilePath, string unZipDir)
		{
			bool flag = unZipDir == string.Empty;
			if (flag)
			{
				unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
			}
			bool flag2 = !unZipDir.EndsWith("\\");
			if (flag2)
			{
				unZipDir += "\\";
			}
			bool flag3 = !Directory.Exists(unZipDir);
			if (flag3)
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
					bool flag4 = text.Length > 0;
					if (flag4)
					{
						Directory.CreateDirectory(unZipDir + text);
					}
					bool flag5 = !text.EndsWith("\\");
					if (flag5)
					{
						text += "\\";
					}
					bool flag6 = fileName != string.Empty;
					if (flag6)
					{
						using (FileStream fileStream = File.Create(unZipDir + nextEntry.Name))
						{
							byte[] array = new byte[2048];
							for (;;)
							{
								int num = zipInputStream.Read(array, 0, array.Length);
								bool flag7 = num > 0;
								if (!flag7)
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

		// Token: 0x06000122 RID: 290 RVA: 0x00008120 File Offset: 0x00006320
		public static string UnRar(string rarPath, string unPath)
		{
			string mapPath = FPFile.GetMapPath("/bin/WinRAR.exe");
			bool exists = new FileInfo(mapPath).Exists;
			if (exists)
			{
				try
				{
					string text = "x -inul -y -o+ -v[t,b]";
					text = string.Concat(new string[]
					{
						text,
						" ",
						rarPath,
						" ",
						unPath
					});
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					processStartInfo.FileName = mapPath;
					processStartInfo.Arguments = text;
					processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
					Process process = new Process();
					processStartInfo.UseShellExecute = false;
					process.StartInfo = processStartInfo;
					process.Start();
					while (!process.HasExited)
					{
					}
					process.WaitForExit();
					return "";
				}
				catch (Exception ex)
				{
					return ex.Message;
				}
			}
			return "请安装WinRar.exe!";
		}

		// Token: 0x0400002A RID: 42
		private ZipFile file;

		// Token: 0x0400002B RID: 43
		private MemoryStream ms = new MemoryStream();
	}
}
