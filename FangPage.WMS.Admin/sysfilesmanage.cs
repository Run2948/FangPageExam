using System;
using System.Data;
using System.IO;
using FangPage.Common;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200001D RID: 29
	public class sysfilesmanage : SuperController
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00005F98 File Offset: 0x00004198
		protected override void Controller()
		{
			if (this.m_path.StartsWith("/"))
			{
				this.m_path = this.m_path.Substring(1, this.m_path.Length - 1);
			}
			if (this.m_path.IndexOf("/") >= 0)
			{
				this.reurl = "sysfilesmanage.aspx?path=" + this.m_path.Substring(0, this.m_path.LastIndexOf("/"));
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
			this.filelist = this.GetFileList(this.m_path);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00006154 File Offset: 0x00004354
		private void Delete(string filename, string type)
		{
			string mapPath = FPFile.GetMapPath(this.webpath + ((this.m_path == "/") ? "" : this.m_path) + filename);
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

		// Token: 0x06000042 RID: 66 RVA: 0x000061EC File Offset: 0x000043EC
		private void Copy(string filename, string type)
		{
			string mapPath = FPFile.GetMapPath(this.webpath + ((this.m_path == "/") ? "" : this.m_path) + filename);
			string mapPath2 = FPFile.GetMapPath(this.webpath + ((this.m_path == "/") ? "" : this.m_path));
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
			else if (File.Exists(mapPath))
			{
				if (File.GetAttributes(mapPath).ToString().IndexOf("ReadOnly") != -1)
				{
					File.SetAttributes(mapPath, FileAttributes.Normal);
				}
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

		// Token: 0x06000043 RID: 67 RVA: 0x00006410 File Offset: 0x00004610
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
						"filetype",
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

		// Token: 0x06000044 RID: 68 RVA: 0x000064FC File Offset: 0x000046FC
		private DataTable GetFileList(string m_path)
		{
			string mapPath = FPFile.GetMapPath(WebConfig.WebPath + ((m_path == "/") ? "" : m_path));
			if (!Directory.Exists(mapPath))
			{
				this.ShowErr("文件夹：/" + ((m_path == "") ? "" : ("/" + m_path)) + "已不存在或被删除。");
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
					dataRow["filetype"] = "folder";
					if (directoryInfo2.Name.ToLower() == "admin" && m_path == "/")
					{
						dataRow["type"] = "系统后台";
					}
					else if (directoryInfo2.Name.ToLower() == "app" && m_path == "/")
					{
						dataRow["type"] = "系统应用";
					}
					else if (directoryInfo2.Name.ToLower() == "bin" && m_path == "/")
					{
						dataRow["type"] = "系统运行库";
					}
					else if (directoryInfo2.Name.ToLower() == "cache" && m_path == "/")
					{
						dataRow["type"] = "系统缓存";
					}
					else if (directoryInfo2.Name.ToLower() == "common" && m_path == "/")
					{
						dataRow["type"] = "系统公用";
					}
					else if (directoryInfo2.Name.ToLower() == "datas" && m_path == "/")
					{
						dataRow["type"] = "系统数据库";
					}
					else if (directoryInfo2.Name.ToLower() == "config" && m_path == "/")
					{
						dataRow["type"] = "系统配置";
					}
					else if (directoryInfo2.Name.ToLower() == "plugins" && m_path == "/")
					{
						dataRow["type"] = "系统插件";
					}
					else if (directoryInfo2.Name.ToLower() == "sites" && m_path == "/")
					{
						dataRow["type"] = "系统站点";
					}
					else if (directoryInfo2.Name.ToLower() == "upload" && m_path == "/")
					{
						dataRow["type"] = "文件上传";
					}
					else
					{
						dataRow["type"] = "文件夹";
					}
					if (directoryInfo2.Name.ToLower() == "upload" && m_path == "/")
					{
						dataRow["size"] = "";
					}
					else
					{
						dataRow["size"] = FPFile.FormatBytesStr(FPFile.GetDirSize(directoryInfo2.FullName));
					}
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
					dataRow2["filetype"] = fileInfo.Extension.Substring(1, fileInfo.Extension.Length - 1);
					dataRow2["type"] = fileInfo.Extension.Substring(1, fileInfo.Extension.Length - 1);
					dataRow2["size"] = FPFile.FormatBytesStr(fileInfo.Length);
					dataTable.Rows.Add(dataRow2);
					num++;
				}
			}
			return dataTable;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00006A18 File Offset: 0x00004C18
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

		// Token: 0x04000042 RID: 66
		protected DataTable filelist = new DataTable();

		// Token: 0x04000043 RID: 67
		protected string m_path = FPRequest.GetString("path");

		// Token: 0x04000044 RID: 68
		protected string reurl = "";
	}
}
