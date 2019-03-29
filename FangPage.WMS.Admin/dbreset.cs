using System;
using System.Data;
using FangPage.Data;
using FangPage.MVC;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000003 RID: 3
	public class dbreset : SuperController
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000023A8 File Offset: 0x000005A8
		protected override void View()
		{
			this.link = "dbreset.aspx";
			if (this.ispost)
			{
				if (DbConfigs.DbType == FangPage.Data.DbType.Access)
				{
					this.ShowErr("对不起，本操作不支持Access数据库版本，请安装SqlServer版本。");
					return;
				}
				if (this.action == "table")
				{
					string @string = FPRequest.GetString("tablename");
					if (string.IsNullOrEmpty(@string))
					{
						this.ShowErr("没有选择要重置的表。");
						return;
					}
					string text = "";
					string[] array = FPUtils.SplitString(FPFile.ReadFile(FPUtils.GetMapPath("sqlreset.sql")), "GO\r\n", 8);
					string text2 = "";
					foreach (string text3 in FPUtils.SplitString(@string))
					{
						if (text != "")
						{
							text += "GO\r\n";
						}
						text = text + "TRUNCATE TABLE " + text3;
						if (text3.EndsWith("WMS_UserInfo"))
						{
							if (text2 != "")
							{
								text2 += "GO\r\n";
							}
							text2 += array[0];
						}
						else if (text3.EndsWith("WMS_UserGrade"))
						{
							if (text2 != "")
							{
								text2 += "GO\r\n";
							}
							text2 += array[1];
						}
						else if (text3.EndsWith("WMS_RoleInfo"))
						{
							if (text2 != "")
							{
								text2 += "GO\r\n";
							}
							text2 += array[2];
						}
						else if (text3.EndsWith("WMS_Permission"))
						{
							if (text2 != "")
							{
								text2 += "GO\r\n";
							}
							text2 += array[3];
						}
						else if (text3.EndsWith("WMS_MenuInfo"))
						{
							if (text2 != "")
							{
								text2 += "GO\r\n";
							}
							text2 += array[4];
						}
						else if (text3.EndsWith("WMS_DesktopInfo"))
						{
							if (text2 != "")
							{
								text2 += "GO\r\n";
							}
							text2 += array[5];
						}
						else if (text3.EndsWith("WMS_ChannelInfo"))
						{
							if (text2 != "")
							{
								text2 += "GO\r\n";
							}
							text2 += array[6];
						}
						else if (text3.EndsWith("WMS_AttachType"))
						{
							if (text2 != "")
							{
								text2 += "GO\r\n";
							}
							text2 += array[7];
						}
					}
					if (text2 != "")
					{
						if (text != "")
						{
							text += "GO\r\n";
						}
						text += text2;
					}
					DbHelper.ExecuteSql(text);
				}
				else if (this.action == "system")
				{
					this.dbtablelist = DbHelper.GetDbTableList();
					string text = "";
					foreach (object obj in this.dbtablelist.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						if (dataRow["TABLE_NAME"].ToString().StartsWith(DbConfigs.Prefix))
						{
							if (text != "")
							{
								text += "|";
							}
							text = text + "TRUNCATE TABLE " + dataRow["TABLE_NAME"].ToString();
						}
					}
					DbHelper.ExecuteSql(text);
					text = FPFile.ReadFile(FPUtils.GetMapPath("sqlreset.sql")).Replace("|", "");
					DbHelper.ExecuteSql(text);
					this.link = this.adminpath + "logout.aspx";
				}
			}
			this.dbtablelist = DbHelper.GetDbTableList();
			base.SaveRightURL();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000289C File Offset: 0x00000A9C
		protected bool IsWMSTable(string tablename)
		{
			return tablename.StartsWith(DbConfigs.Prefix);
		}

		// Token: 0x04000003 RID: 3
		protected DataTable dbtablelist = new DataTable();
	}
}
