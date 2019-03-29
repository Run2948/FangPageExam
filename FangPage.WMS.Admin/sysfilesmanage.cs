using System;
using System.Data;
using System.IO;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000016 RID: 22
	public class sysfilesmanage : SuperController
	{
		// Token: 0x06000031 RID: 49 RVA: 0x000050F0 File Offset: 0x000032F0
		protected override void View()
		{
			if (this.m_path.StartsWith("/"))
			{
				this.m_path = this.m_path.Substring(1, this.m_path.Length - 1);
			}
			if (this.m_path.IndexOf("/") >= 0)
			{
				this.reurl = "sysfilesmanage.aspx?&path=" + this.m_path.Substring(0, this.m_path.LastIndexOf("/"));
			}
			else
			{
				this.reurl = "sysfilesmanage.aspx";
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
			this.filelist = this.GetFileList(this.m_path);
			base.SaveRightURL();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00005278 File Offset: 0x00003478
		private void Delete(string filename, string type)
		{
			string mapPath = FPUtils.GetMapPath(this.webpath + ((this.m_path == "/") ? "" : this.m_path) + filename);
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

		// Token: 0x06000033 RID: 51 RVA: 0x0000532C File Offset: 0x0000352C
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

		// Token: 0x06000034 RID: 52 RVA: 0x00005404 File Offset: 0x00003604
		private DataTable GetFileList(string m_path)
		{
			string mapPath = FPUtils.GetMapPath(WebConfig.WebPath + ((m_path == "/") ? "" : m_path));
			DataTable result;
			if (!Directory.Exists(mapPath))
			{
				this.ShowErr("文件夹：/" + ((m_path == "") ? "" : ("/" + m_path)) + "已不存在或被删除。");
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

		// Token: 0x06000035 RID: 53 RVA: 0x000056A8 File Offset: 0x000038A8
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

		// Token: 0x04000029 RID: 41
		protected DataTable filelist = new DataTable();

		// Token: 0x0400002A RID: 42
		protected string m_path = FPRequest.GetString("path");

		// Token: 0x0400002B RID: 43
		protected string reurl = "";
	}
}
