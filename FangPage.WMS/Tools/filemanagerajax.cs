using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using FangPage.MVC;
using LitJson;

namespace FangPage.WMS.Tools
{
	// Token: 0x0200003F RID: 63
	public class filemanagerajax : AdminController
	{
		// Token: 0x060002CD RID: 717 RVA: 0x0000A804 File Offset: 0x00008A04
		protected override void View()
		{
			string strPath = this.webpath + "upload/";
			string text = this.webpath + "upload/";
			string text2 = "gif,jpg,jpeg,png,bmp";
			string mapPath = FPUtils.GetMapPath(strPath);
			string @string = FPRequest.GetString("dir");
			string text3 = FPRequest.GetString("path");
			text3 = (string.IsNullOrEmpty(text3) ? "" : text3);
			string path;
			string value;
			string text4;
			string value2;
			if (text3 == "")
			{
				path = mapPath;
				value = text;
				text4 = "";
				value2 = "";
			}
			else
			{
				path = mapPath + text3;
				value = text + text3;
				text4 = text3;
				value2 = Regex.Replace(text4, "(.*?)[^\\/]+\\/$", "$1");
			}
			string text5 = FPRequest.GetString("order");
			text5 = (string.IsNullOrEmpty(text5) ? "" : text5.ToLower());
			if (Regex.IsMatch(text3, "\\.\\."))
			{
				base.Response.Write("不允许使用上一级目录。");
				base.Response.End();
			}
			if (text3 != "" && !text3.EndsWith("/"))
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
			string text6 = text5;
			if (text6 != null)
			{
				if (text6 == "size")
				{
					Array.Sort(directories, new filemanagerajax.NameSorter());
					Array.Sort(files, new filemanagerajax.SizeSorter());
					goto IL_21D;
				}
				if (text6 == "type")
				{
					Array.Sort(directories, new filemanagerajax.NameSorter());
					Array.Sort(files, new filemanagerajax.TypeSorter());
					goto IL_21D;
				}
				if (!(text6 == "name"))
				{
				}
			}
			Array.Sort(directories, new filemanagerajax.NameSorter());
			Array.Sort(files, new filemanagerajax.NameSorter());
			IL_21D:
			Hashtable hashtable = new Hashtable();
			hashtable["moveup_dir_path"] = value2;
			hashtable["current_dir_path"] = text4;
			hashtable["current_url"] = value;
			hashtable["total_count"] = directories.Length + files.Length;
			List<Hashtable> list = new List<Hashtable>();
			hashtable["file_list"] = list;
			for (int i = 0; i < directories.Length; i++)
			{
				DirectoryInfo directoryInfo = new DirectoryInfo(directories[i]);
				Hashtable hashtable2 = new Hashtable();
				hashtable2["is_dir"] = true;
				hashtable2["has_file"] = (directoryInfo.GetFileSystemInfos().Length > 0);
				hashtable2["filesize"] = 0;
				hashtable2["is_photo"] = false;
				hashtable2["filetype"] = "";
				hashtable2["filename"] = directoryInfo.Name;
				hashtable2["datetime"] = directoryInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
				list.Add(hashtable2);
			}
			for (int i = 0; i < files.Length; i++)
			{
				FileInfo fileInfo = new FileInfo(files[i]);
				Hashtable hashtable2 = new Hashtable();
				hashtable2["is_dir"] = false;
				hashtable2["has_file"] = false;
				hashtable2["filesize"] = fileInfo.Length;
				hashtable2["is_photo"] = (Array.IndexOf<string>(text2.Split(new char[]
				{
					','
				}), fileInfo.Extension.Substring(1).ToLower()) >= 0);
				hashtable2["filetype"] = fileInfo.Extension.Substring(1);
				hashtable2["filename"] = fileInfo.Name;
				hashtable2["datetime"] = fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
				list.Add(hashtable2);
			}
			base.Response.AddHeader("Content-Type", "application/json; charset=UTF-8");
			base.Response.Write(JsonMapper.ToJson(hashtable));
			base.Response.End();
		}

		// Token: 0x02000040 RID: 64
		public class NameSorter : IComparer
		{
			// Token: 0x060002CF RID: 719 RVA: 0x0000ACC0 File Offset: 0x00008EC0
			public int Compare(object x, object y)
			{
				int result;
				if (x == null && y == null)
				{
					result = 0;
				}
				else if (x == null)
				{
					result = -1;
				}
				else if (y == null)
				{
					result = 1;
				}
				else
				{
					FileInfo fileInfo = new FileInfo(x.ToString());
					FileInfo fileInfo2 = new FileInfo(y.ToString());
					result = fileInfo.FullName.CompareTo(fileInfo2.FullName);
				}
				return result;
			}
		}

		// Token: 0x02000041 RID: 65
		public class SizeSorter : IComparer
		{
			// Token: 0x060002D1 RID: 721 RVA: 0x0000AD3C File Offset: 0x00008F3C
			public int Compare(object x, object y)
			{
				int result;
				if (x == null && y == null)
				{
					result = 0;
				}
				else if (x == null)
				{
					result = -1;
				}
				else if (y == null)
				{
					result = 1;
				}
				else
				{
					FileInfo fileInfo = new FileInfo(x.ToString());
					FileInfo fileInfo2 = new FileInfo(y.ToString());
					result = fileInfo.Length.CompareTo(fileInfo2.Length);
				}
				return result;
			}
		}

		// Token: 0x02000042 RID: 66
		public class TypeSorter : IComparer
		{
			// Token: 0x060002D3 RID: 723 RVA: 0x0000ADBC File Offset: 0x00008FBC
			public int Compare(object x, object y)
			{
				int result;
				if (x == null && y == null)
				{
					result = 0;
				}
				else if (x == null)
				{
					result = -1;
				}
				else if (y == null)
				{
					result = 1;
				}
				else
				{
					FileInfo fileInfo = new FileInfo(x.ToString());
					FileInfo fileInfo2 = new FileInfo(y.ToString());
					result = fileInfo.Extension.CompareTo(fileInfo2.Extension);
				}
				return result;
			}
		}
	}
}
