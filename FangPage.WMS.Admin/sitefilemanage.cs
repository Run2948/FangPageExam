using System;
using System.Data;
using System.IO;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200002A RID: 42
	public class sitefilemanage : SuperController
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000089F0 File Offset: 0x00006BF0
		protected override void View()
		{
			if (this.m_path.StartsWith("/"))
			{
				this.m_path = this.m_path.Substring(1, this.m_path.Length - 1);
			}
			if (this.m_path.IndexOf("/") >= 0)
			{
				this.reurl = "sitefilemanage.aspx?sitepath=" + this.m_sitepath + "&path=" + this.m_path.Substring(0, this.m_path.LastIndexOf("/"));
			}
			else if (this.m_path == "")
			{
				this.reurl = "sitemanage.aspx";
			}
			else
			{
				this.reurl = "sitefilemanage.aspx?sitepath=" + this.m_sitepath;
			}
			if (!this.m_path.EndsWith("/"))
			{
				this.m_path += "/";
			}
			if (this.ispost)
			{
				if (!this.isperm)
				{
					this.ShowErr("对不起，您没有权限操作。");
					return;
				}
				string @string = FPRequest.GetString("fileid");
				foreach (string str in @string.Split(new char[]
				{
					','
				}))
				{
					string string2 = FPRequest.GetString("type" + str);
					string string3 = FPRequest.GetString("file" + str);
					this.Delete(string3, string2);
				}
			}
			this.filelist = this.GetFileList(this.m_sitepath, this.m_path);
			base.SaveRightURL();
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00008BC0 File Offset: 0x00006DC0
		private void Delete(string filename, string type)
		{
			string mapPath = FPUtils.GetMapPath(string.Concat(new string[]
			{
				this.webpath,
				"sites/",
				this.m_sitepath,
				"/",
				(this.m_path == "/") ? "" : this.m_path,
				filename
			}));
			if (type == "文件夹")
			{
				if (Directory.Exists(mapPath))
				{
					Directory.Delete(mapPath, true);
				}
			}
			else
			{
				if (File.GetAttributes(mapPath).ToString().IndexOf("ReadOnly") != -1)
				{
					File.SetAttributes(mapPath, FileAttributes.Normal);
				}
				if (File.Exists(mapPath))
				{
					File.Delete(mapPath);
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00008CA0 File Offset: 0x00006EA0
		private DataTable CreateDataTable()
		{
			return new DataTable
			{
				Columns = 
				{
					{
						"id",
						Type.GetType("System.Int32")
					},
					{
						"icon",
						typeof(string)
					},
					{
						"name",
						typeof(string)
					},
					{
						"path",
						typeof(string)
					},
					{
						"updateTime",
						typeof(string)
					},
					{
						"type",
						typeof(string)
					},
					{
						"size",
						typeof(string)
					}
				}
			};
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00008D78 File Offset: 0x00006F78
		private DataTable GetFileList(string m_path, string m_dirPath)
		{
			string mapPath = FPUtils.GetMapPath(string.Concat(new string[]
			{
				WebConfig.WebPath,
				"sites/",
				m_path,
				"/",
				m_dirPath
			}));
			DataTable result;
			if (!Directory.Exists(mapPath))
			{
				this.ShowErr("文件夹：/" + m_path + ((m_dirPath == "") ? "" : ("/" + m_dirPath)) + "已不存在或被删除。");
				result = new DataTable();
			}
			else
			{
				DataTable dataTable = this.CreateDataTable();
				DirectoryInfo directoryInfo = new DirectoryInfo(mapPath);
				int num = 1;
				foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
				{
					if (directoryInfo2 != null)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["id"] = num;
						dataRow["icon"] = this.GetFileTypeIco("");
						dataRow["name"] = directoryInfo2.Name;
						dataRow["path"] = directoryInfo2.FullName;
						dataRow["updatetime"] = directoryInfo2.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
						dataRow["type"] = "文件夹";
						dataRow["size"] = "";
						dataTable.Rows.Add(dataRow);
						num++;
					}
				}
				foreach (FileInfo fileInfo in directoryInfo.GetFiles())
				{
					if (fileInfo != null)
					{
						DataRow dataRow = dataTable.NewRow();
						dataRow["id"] = num;
						dataRow["icon"] = this.GetFileTypeIco(fileInfo.Extension);
						dataRow["name"] = fileInfo.Name;
						dataRow["path"] = fileInfo.FullName;
						dataRow["updatetime"] = fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
						dataRow["type"] = fileInfo.Extension.Substring(1, fileInfo.Extension.Length - 1);
						dataRow["size"] = FPUtils.FormatBytesStr(fileInfo.Length);
						dataTable.Rows.Add(dataRow);
						num++;
					}
				}
				result = dataTable;
			}
			return result;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00009030 File Offset: 0x00007230
		private string GetFileTypeIco(string type)
		{
			string str = "../images/filetype/";
			string text = type.ToLower();
			switch (text)
			{
			case "":
				return str + "directory.gif";
			case ".txt":
				return str + "txt.gif";
			case ".html":
				return str + "htm.gif";
			case ".htm":
				return str + "htm.gif";
			case ".config":
				return str + "config.gif";
			case ".xml":
				return str + "xml.gif";
			case ".css":
				return str + "css.gif";
			case ".js":
				return str + "js.gif";
			case ".jpg":
				return str + "jpg.gif";
			case ".gif":
				return str + "gif.gif";
			case ".png":
				return str + "img.gif";
			case ".bmp":
				return str + "img.gif";
			case ".aspx":
				return str + "aspx.gif";
			case ".ascx":
				return str + "ascx.gif";
			case ".iso":
				return str + "iso.gif";
			case ".avi":
				return str + "avi.gif";
			case ".doc":
				return str + "doc.gif";
			case ".mp3":
				return str + "mp3.gif";
			case ".pdf":
				return str + "pdf.gif";
			case ".rar":
				return str + "rar.gif";
			case ".wmv":
				return str + "wmv.gif";
			case ".zip":
				return str + "zip.gif";
			case ".asp":
				return str + "asp.gif";
			case ".swf":
				return str + "swf.gif";
			}
			return str + "unknown.gif";
		}

		// Token: 0x04000057 RID: 87
		protected DataTable filelist = new DataTable();

		// Token: 0x04000058 RID: 88
		protected string m_sitepath = FPRequest.GetString("sitepath");

		// Token: 0x04000059 RID: 89
		protected string m_path = FPRequest.GetString("path");

		// Token: 0x0400005A RID: 90
		protected string reurl = "";
	}
}
