using System;
using System.Data;
using System.IO;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000037 RID: 55
	public class sitefilemanage : SuperController
	{
		// Token: 0x0600007F RID: 127 RVA: 0x0000A57C File Offset: 0x0000877C
		protected override void Controller()
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
				if (this.action == "delete")
				{
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
				else if (this.action == "copy")
				{
					foreach (string str2 in @string.Split(new char[]
					{
						','
					}))
					{
						string string4 = FPRequest.GetString("type" + str2);
						string string5 = FPRequest.GetString("file" + str2);
						this.Copy(string5, string4);
					}
				}
			}
			this.filelist = this.GetFileList(this.m_sitepath, this.m_path);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x0000A774 File Offset: 0x00008974
		private void Delete(string filename, string type)
		{
			string mapPath = FPFile.GetMapPath(string.Concat(new string[]
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
					return;
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

		// Token: 0x06000081 RID: 129 RVA: 0x0000A834 File Offset: 0x00008A34
		private void Copy(string filename, string type)
		{
			string mapPath = FPFile.GetMapPath(string.Concat(new string[]
			{
				this.webpath,
				"sites/",
				this.m_sitepath,
				"/",
				(this.m_path == "/") ? "" : this.m_path,
				filename
			}));
			string mapPath2 = FPFile.GetMapPath(string.Concat(new string[]
			{
				this.webpath,
				"sites/",
				this.m_sitepath,
				"/",
				(this.m_path == "/") ? "" : this.m_path
			}));
			if (type == "文件夹")
			{
				if (Directory.Exists(mapPath))
				{
					int num = 0;
					string[] array = Directory.GetDirectories(mapPath2);
					for (int i = 0; i < array.Length; i++)
					{
						string text = Path.GetFileName(array[i]);
						if (text == filename)
						{
							num++;
						}
						else
						{
							if (text.IndexOf("-副本") >= 0)
							{
								text = text.Substring(0, text.LastIndexOf("-副本"));
							}
							if (text == filename)
							{
								num++;
							}
						}
					}
					string text2;
					if (num > 1)
					{
						text2 = filename + "-副本(" + num.ToString() + ")";
					}
					else
					{
						text2 = filename + "-副本";
					}
					FPFile.CopyDir(mapPath, mapPath2 + text2);
					return;
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
					string text2 = Path.GetFileNameWithoutExtension(filename);
					int num2 = 0;
					string[] array = Directory.GetFiles(mapPath2);
					for (int i = 0; i < array.Length; i++)
					{
						string text3 = Path.GetFileNameWithoutExtension(array[i]);
						if (text3 == text2)
						{
							num2++;
						}
						else
						{
							if (text3.IndexOf("-副本") >= 0)
							{
								text3 = text3.Substring(0, text3.LastIndexOf("-副本"));
							}
							if (text3 == text2)
							{
								num2++;
							}
						}
					}
					if (num2 > 1)
					{
						text2 = text2 + "-副本(" + num2.ToString() + ")";
					}
					else
					{
						text2 += "-副本";
					}
					File.Copy(mapPath, mapPath2 + text2 + Path.GetExtension(filename), true);
				}
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000AAA4 File Offset: 0x00008CA4
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

		// Token: 0x06000083 RID: 131 RVA: 0x0000AB74 File Offset: 0x00008D74
		private DataTable GetFileList(string m_path, string m_dirPath)
		{
			string mapPath = FPFile.GetMapPath(string.Concat(new string[]
			{
				WebConfig.WebPath,
				"sites/",
				m_path,
				"/",
				m_dirPath
			}));
			if (!Directory.Exists(mapPath))
			{
				this.ShowErr("文件夹：/" + m_path + ((m_dirPath == "") ? "" : ("/" + m_dirPath)) + "已不存在或被删除。");
				return new DataTable();
			}
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
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2["id"] = num;
					dataRow2["icon"] = this.GetFileTypeIco(fileInfo.Extension);
					dataRow2["name"] = fileInfo.Name;
					dataRow2["path"] = fileInfo.FullName;
					dataRow2["updatetime"] = fileInfo.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
					dataRow2["type"] = fileInfo.Extension.Substring(1, fileInfo.Extension.Length - 1);
					dataRow2["size"] = FPFile.FormatBytesStr(fileInfo.Length);
					dataTable.Rows.Add(dataRow2);
					num++;
				}
			}
			return dataTable;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000ADDC File Offset: 0x00008FDC
		private string GetFileTypeIco(string type)
		{
			if (type == "")
			{
				type = "directory";
			}
			if (type.StartsWith("."))
			{
				type = type.Substring(1, type.Length - 1);
			}
			string text = this.webpath + "common/file/" + type + ".gif";
			if (File.Exists(FPFile.GetMapPath(text)))
			{
				return text;
			}
			text = this.webpath + "common/file/" + type + ".png";
			if (File.Exists(FPFile.GetMapPath(text)))
			{
				return text;
			}
			return this.webpath + "common/file/unknow.gif";
		}

		// Token: 0x0400008E RID: 142
		protected DataTable filelist = new DataTable();

		// Token: 0x0400008F RID: 143
		protected string m_sitepath = FPRequest.GetString("sitepath");

		// Token: 0x04000090 RID: 144
		protected string m_path = FPRequest.GetString("path");

		// Token: 0x04000091 RID: 145
		protected string reurl = "";
	}
}
