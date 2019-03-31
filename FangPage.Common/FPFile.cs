using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web;

namespace FangPage.Common
{
	// Token: 0x0200000D RID: 13
	public class FPFile
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003BCC File Offset: 0x00001DCC
		public static string GetMapPath(string virPath)
		{
			bool flag = HttpContext.Current != null;
			string result;
			if (flag)
			{
				result = HttpContext.Current.Server.MapPath(virPath);
			}
			else
			{
				result = FPFile.GetMapPath(AppDomain.CurrentDomain.BaseDirectory, virPath);
			}
			return result;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003C10 File Offset: 0x00001E10
		public static string GetMapPath(string strPath, string virPath)
		{
			virPath = virPath.Replace("/", "\\");
			strPath = strPath.TrimEnd(new char[]
			{
				'\\'
			});
			for (;;)
			{
				bool flag = virPath.StartsWith("..\\");
				if (!flag)
				{
					break;
				}
				bool flag2 = strPath != "";
				if (flag2)
				{
					strPath = Path.GetDirectoryName(strPath);
				}
				virPath = virPath.Substring(3);
			}
			bool flag3 = virPath != "";
			if (flag3)
			{
				strPath = FPFile.Combine(strPath, virPath).TrimEnd(new char[]
				{
					'\\'
				});
			}
			return strPath;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003CB4 File Offset: 0x00001EB4
		public static string GetExt(string filename)
		{
			bool flag = string.IsNullOrEmpty(filename);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool flag2 = filename.LastIndexOf(".") > 0;
				if (flag2)
				{
					result = filename.Substring(filename.LastIndexOf(".") + 1);
				}
				else
				{
					result = "";
				}
			}
			return result;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003D08 File Offset: 0x00001F08
		public static string ReadFile(string filename)
		{
			bool flag = !File.Exists(filename);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				using (FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
				{
					using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
					{
						result = streamReader.ReadToEnd();
					}
				}
			}
			return result;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003D80 File Offset: 0x00001F80
		public static void WriteFile(string filename, string content)
		{
			bool flag = !Directory.Exists(Path.GetDirectoryName(filename));
			if (flag)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(filename));
			}
			bool flag2 = File.Exists(filename) && File.GetAttributes(filename).ToString().IndexOf("ReadOnly") != -1;
			if (flag2)
			{
				File.SetAttributes(filename, FileAttributes.Normal);
			}
			using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
			{
				byte[] bytes = Encoding.UTF8.GetBytes(content);
				fileStream.Write(bytes, 0, bytes.Length);
				fileStream.Close();
			}
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003E3C File Offset: 0x0000203C
		public static void AppendFile(string filename, string content)
		{
			bool flag = !Directory.Exists(Path.GetDirectoryName(filename));
			if (flag)
			{
				Directory.CreateDirectory(Path.GetDirectoryName(filename));
			}
			bool flag2 = File.Exists(filename) && File.GetAttributes(filename).ToString().IndexOf("ReadOnly") != -1;
			if (flag2)
			{
				File.SetAttributes(filename, FileAttributes.Normal);
			}
			bool flag3 = !File.Exists(filename);
			if (flag3)
			{
				using (FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write))
				{
					StreamWriter streamWriter = new StreamWriter(fileStream);
					streamWriter.WriteLine(content);
					streamWriter.Close();
					fileStream.Close();
				}
			}
			else
			{
				using (FileStream fileStream2 = new FileStream(filename, FileMode.Append, FileAccess.Write))
				{
					StreamWriter streamWriter2 = new StreamWriter(fileStream2);
					streamWriter2.WriteLine(content);
					streamWriter2.Close();
					fileStream2.Close();
				}
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003F54 File Offset: 0x00002154
		public static void FileCoppy(string orignFile, string newFile)
		{
			bool flag = string.IsNullOrEmpty(orignFile) || string.IsNullOrEmpty(newFile);
			if (!flag)
			{
				bool flag2 = !File.Exists(orignFile);
				if (!flag2)
				{
					bool flag3 = !Directory.Exists(Path.GetDirectoryName(newFile));
					if (flag3)
					{
						Directory.CreateDirectory(Path.GetDirectoryName(newFile));
					}
					File.Copy(orignFile, newFile, true);
				}
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003FB4 File Offset: 0x000021B4
		public static void FileDel(string path)
		{
			bool flag = string.IsNullOrEmpty(path) || !File.Exists(path);
			if (!flag)
			{
				File.Delete(path);
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003FE4 File Offset: 0x000021E4
		public static void FileMove(string orignFile, string newFile)
		{
			bool flag = string.IsNullOrEmpty(orignFile) || string.IsNullOrEmpty(newFile);
			if (!flag)
			{
				bool flag2 = !File.Exists(orignFile);
				if (!flag2)
				{
					bool flag3 = !Directory.Exists(Path.GetDirectoryName(newFile));
					if (flag3)
					{
						Directory.CreateDirectory(Path.GetDirectoryName(newFile));
					}
					File.Move(orignFile, newFile);
				}
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004044 File Offset: 0x00002244
		public static void CreateDir(string path)
		{
			bool flag = string.IsNullOrEmpty(path);
			if (flag)
			{
				throw new ArgumentException(path);
			}
			bool flag2 = !Directory.Exists(path);
			if (flag2)
			{
				Directory.CreateDirectory(path);
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000407C File Offset: 0x0000227C
		public static void CopyDir(string sourcePath, string targetPath)
		{
			bool flag = !Directory.Exists(sourcePath);
			if (!flag)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(sourcePath);
				bool flag2 = !Directory.Exists(targetPath);
				if (flag2)
				{
					Directory.CreateDirectory(targetPath);
				}
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					fileInfo.CopyTo(targetPath + "\\" + fileInfo.Name, true);
				}
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					FPFile.CopyDir(directoryInfo2.FullName, targetPath + "\\" + directoryInfo2.Name);
				}
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x0000413C File Offset: 0x0000233C
		public static string Combine(string path1, string path2)
		{
			char c = '/';
			bool flag = path1.IndexOf('\\') >= 0;
			if (flag)
			{
				path1 = path1.Replace("/", "\\");
				path2 = path2.Replace("/", "\\");
				c = '\\';
			}
			else
			{
				path1 = path1.Replace("\\", "/");
				path2 = path2.Replace("\\", "/");
			}
			bool flag2 = path1 == "";
			string result;
			if (flag2)
			{
				result = path2;
			}
			else
			{
				bool flag3 = path2 == "";
				if (flag3)
				{
					result = path1;
				}
				else
				{
					path1 = path1.TrimEnd(new char[]
					{
						c
					});
					path2 = path2.TrimStart(new char[]
					{
						c
					});
					result = path1 + c.ToString() + path2;
				}
			}
			return result;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004210 File Offset: 0x00002410
		public static long GetDirSize(string dirPath)
		{
			long num = 0L;
			bool flag = !Directory.Exists(dirPath);
			long result;
			if (flag)
			{
				result = num;
			}
			else
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					num += fileInfo.Length;
				}
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					num += FPFile.GetDirSize(directoryInfo2.FullName);
				}
				result = num;
			}
			return result;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000042A4 File Offset: 0x000024A4
		public static void DeleteDir(string dir)
		{
			bool flag = string.IsNullOrEmpty(dir);
			if (flag)
			{
				throw new ArgumentException(dir);
			}
			bool flag2 = !Directory.Exists(dir);
			if (!flag2)
			{
				foreach (string text in Directory.GetFileSystemEntries(dir))
				{
					bool flag3 = File.Exists(text);
					if (flag3)
					{
						File.Delete(text);
					}
					else
					{
						FPFile.DeleteDir(text);
					}
				}
				Directory.Delete(dir, true);
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000431C File Offset: 0x0000251C
		public static void ClearDir(string dirPath)
		{
			bool flag = !Directory.Exists(dirPath);
			if (!flag)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					bool flag2 = fileInfo.Attributes.ToString().IndexOf("ReadOnly") != -1;
					if (flag2)
					{
						File.SetAttributes(fileInfo.FullName, FileAttributes.Normal);
					}
					File.Delete(fileInfo.FullName);
				}
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					Directory.Delete(directoryInfo2.FullName, true);
				}
			}
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000043E4 File Offset: 0x000025E4
		public static string Execute(string filepath, string args)
		{
			return FPFile.Execute(filepath, args, true, ProcessWindowStyle.Hidden);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004400 File Offset: 0x00002600
		public static string Execute(string filepath, string args, bool waitexit)
		{
			return FPFile.Execute(filepath, args, waitexit, ProcessWindowStyle.Hidden);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000441C File Offset: 0x0000261C
		public static string Execute(string filepath, string args, bool waitexit, ProcessWindowStyle WindowStyle)
		{
			bool exists = new FileInfo(filepath).Exists;
			if (exists)
			{
				try
				{
					ProcessStartInfo processStartInfo = new ProcessStartInfo();
					processStartInfo.FileName = filepath;
					processStartInfo.Arguments = args;
					processStartInfo.WindowStyle = WindowStyle;
					Process process = new Process();
					processStartInfo.UseShellExecute = false;
					process.StartInfo = processStartInfo;
					process.Start();
					while (!process.HasExited)
					{
					}
					if (waitexit)
					{
						process.WaitForExit();
					}
					return "";
				}
				catch (Exception ex)
				{
					return ex.Message;
				}
			}
			return "程序文件不存在";
		}

		// Token: 0x0600009A RID: 154 RVA: 0x000044CC File Offset: 0x000026CC
		public static string FormatBytesStr(long bytes)
		{
			bool flag = bytes >= 1073741824L;
			string result;
			if (flag)
			{
				result = string.Format("{0:F}", (double)bytes / 1073741824.0) + " G";
			}
			else
			{
				bool flag2 = bytes >= 1048576L;
				if (flag2)
				{
					result = string.Format("{0:F}", (double)bytes / 1048576.0) + " M";
				}
				else
				{
					bool flag3 = bytes >= 1024L;
					if (flag3)
					{
						result = string.Format("{0:F}", (double)bytes / 1024.0) + " K";
					}
					else
					{
						result = bytes.ToString() + " B";
					}
				}
			}
			return result;
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000045A0 File Offset: 0x000027A0
		public static bool IsFileUse(string filename)
		{
			bool result = true;
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read, FileShare.None);
				result = false;
			}
			catch
			{
			}
			finally
			{
				bool flag = fileStream != null;
				if (flag)
				{
					fileStream.Close();
				}
			}
			return result;
		}
	}
}
