using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.API;

namespace FP_Editor.Controller
{
	// Token: 0x02000002 RID: 2
	public class filemanagerajax : LoginController
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected override void Controller()
		{
			string text = this.webpath + "upload/";
			string text2 = this.webpath + "upload/";
			string text3 = "gif,jpg,jpeg,png,bmp";
			string mapPath = FPFile.GetMapPath(text);
			FPRequest.GetString("dir");
			string text4 = FPRequest.GetString("path");
			text4 = (string.IsNullOrEmpty(text4) ? "" : text4);
			string path;
			string value;
			string text5;
			string value2;
			if (text4 == "")
			{
				path = mapPath;
				value = text2;
				text5 = "";
				value2 = "";
			}
			else
			{
				path = mapPath + text4;
				value = text2 + text4;
				text5 = text4;
				value2 = Regex.Replace(text5, "(.*?)[^\\/]+\\/$", "$1");
			}
			string text6 = FPRequest.GetString("order");
			text6 = (string.IsNullOrEmpty(text6) ? "" : text6.ToLower());
			if (Regex.IsMatch(text4, "\\.\\."))
			{
				base.Response.Write("不允许使用上一级目录。");
				base.Response.End();
			}
			if (text4 != "" && !text4.EndsWith("/"))
			{
				base.Response.Write("目录格式错误。");
				base.Response.End();
			}
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			string[] directories = Directory.GetDirectories(path);
			string[] files = Directory.GetFiles(path);
			if (!(text6 == "size"))
			{
				if (!(text6 == "type"))
				{
					if (!(text6 == "name"))
					{
					}
					Array.Sort(directories, new filemanagerajax.NameSorter());
					Array.Sort(files, new filemanagerajax.NameSorter());
				}
				else
				{
					Array.Sort(directories, new filemanagerajax.NameSorter());
					Array.Sort(files, new filemanagerajax.TypeSorter());
				}
			}
			else
			{
				Array.Sort(directories, new filemanagerajax.NameSorter());
				Array.Sort(files, new filemanagerajax.SizeSorter());
			}
			Hashtable hashtable = new Hashtable();
			hashtable["moveup_dir_path"] = value2;
			hashtable["current_dir_path"] = text5;
			hashtable["current_url"] = value;
			hashtable["total_count"] = directories.Length + files.Length;
			List<Hashtable> list = new List<Hashtable>();
			hashtable["file_list"] = list;
			for (int i = 0; i < directories.Length; i++)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(directories[i]);
				Hashtable hashtable2 = new Hashtable();
				hashtable2["is_dir"] = true;
				hashtable2["has_file"] = (directoryInfo.GetFileSystemInfos().Length != 0);
				hashtable2["filesize"] = 0;
				hashtable2["is_photo"] = false;
				hashtable2["filetype"] = "";
				hashtable2["filename"] = directoryInfo.Name;
				hashtable2["datetime"] = directoryInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
				list.Add(hashtable2);
			}
			for (int j = 0; j < files.Length; j++)
			{
				FileInfo fileInfo = new FileInfo(files[j]);
				Hashtable hashtable3 = new Hashtable();
				hashtable3["is_dir"] = false;
				hashtable3["has_file"] = false;
				hashtable3["filesize"] = fileInfo.Length;
				hashtable3["is_photo"] = (Array.IndexOf<string>(text3.Split(new char[]
				{
					','
				}), fileInfo.Extension.Substring(1).ToLower()) >= 0);
				hashtable3["filetype"] = fileInfo.Extension.Substring(1);
				hashtable3["filename"] = fileInfo.Name;
				hashtable3["datetime"] = fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
				list.Add(hashtable3);
			}
			FPResponse.WriteJson(hashtable);
		}

		// Token: 0x02000004 RID: 4
		public class NameSorter : IComparer
		{
			// Token: 0x06000007 RID: 7 RVA: 0x0000258C File Offset: 0x0000078C
			public int Compare(object x, object y)
			{
				if (x == null && y == null)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				FileSystemInfo fileSystemInfo = new FileInfo(x.ToString());
				FileInfo fileInfo = new FileInfo(y.ToString());
				return fileSystemInfo.FullName.CompareTo(fileInfo.FullName);
			}
		}

		// Token: 0x02000005 RID: 5
		public class SizeSorter : IComparer
		{
			// Token: 0x06000009 RID: 9 RVA: 0x000025DC File Offset: 0x000007DC
			public int Compare(object x, object y)
			{
				if (x == null && y == null)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				FileInfo fileInfo = new FileInfo(x.ToString());
				FileInfo fileInfo2 = new FileInfo(y.ToString());
				return fileInfo.Length.CompareTo(fileInfo2.Length);
			}
		}

		// Token: 0x02000006 RID: 6
		public class TypeSorter : IComparer
		{
			// Token: 0x0600000B RID: 11 RVA: 0x00002628 File Offset: 0x00000828
			public int Compare(object x, object y)
			{
				if (x == null && y == null)
				{
					return 0;
				}
				if (x == null)
				{
					return -1;
				}
				if (y == null)
				{
					return 1;
				}
				FileSystemInfo fileSystemInfo = new FileInfo(x.ToString());
				FileInfo fileInfo = new FileInfo(y.ToString());
				return fileSystemInfo.Extension.CompareTo(fileInfo.Extension);
			}
		}
	}
}
