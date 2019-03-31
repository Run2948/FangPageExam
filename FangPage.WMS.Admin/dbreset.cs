using System;
using System.Data;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x02000003 RID: 3
	public class dbreset : SuperController
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000233C File Offset: 0x0000053C
		protected override void Controller()
		{
			this.backurl = "dbreset.aspx";
			if (this.ispost)
			{
				if (this.action == "table" && DbConfigs.DbType == FangPage.Data.DbType.SqlServer)
				{
					string @string = FPRequest.GetString("tablename");
					if (string.IsNullOrEmpty(@string))
					{
						this.ShowErr("没有选择要重置的表。");
						return;
					}
					string text = "";
					foreach (string text2 in FPArray.SplitString(@string))
					{
						if (text != "")
						{
							text += "GO\r\n";
						}
						text = text + "TRUNCATE TABLE " + text2;
						if (text2.EndsWith("WMS_Department"))
						{
							if (text != "")
							{
								text += "GO\r\n";
							}
							text += string.Format("UPDATE [{0}WMS_UserInfo] SET [departid]=0,[departname]=''", DbConfigs.Prefix);
						}
					}
					string text3 = DbHelper.ExecuteSql(text);
					if (text3 != "")
					{
						this.ShowErr(text3);
						return;
					}
				}
				else if (this.action == "table" && DbConfigs.DbType == FangPage.Data.DbType.Access)
				{
					string string2 = FPRequest.GetString("tablename");
					if (string.IsNullOrEmpty(string2))
					{
						this.ShowErr("没有选择要重置的表。");
						return;
					}
					string text4 = "";
					foreach (string text5 in FPArray.SplitString(string2))
					{
						if (text4 != "")
						{
							text4 += "GO\r\n";
						}
						text4 = text4 + "DELETE FROM [" + text5 + "]";
						if (text5.EndsWith("WMS_Department"))
						{
							if (text4 != "")
							{
								text4 += "GO\r\n";
							}
							text4 += string.Format("UPDATE [{0}WMS_UserInfo] SET [departid]=0,[departname]=''", DbConfigs.Prefix);
						}
					}
					string text6 = DbHelper.ExecuteSql(text4);
					if (text6 != "")
					{
						this.ShowErr(text6);
						return;
					}
				}
				else if (this.action == "system" && DbConfigs.DbType == FangPage.Data.DbType.SqlServer)
				{
					string string3 = FPRequest.GetString("tables");
					if (string.IsNullOrEmpty(string3))
					{
						this.ShowErr("没有输入要重置的表。");
						return;
					}
					string text7 = "";
					foreach (string str in FPArray.SplitString(string3))
					{
						if (text7 != "")
						{
							text7 += "|";
						}
						text7 = text7 + "TRUNCATE TABLE " + str;
					}
					string text8 = DbHelper.ExecuteSql(text7);
					if (text8 != "")
					{
						this.ShowErr(text8);
						return;
					}
				}
				else if (this.action == "system" && DbConfigs.DbType == FangPage.Data.DbType.Access)
				{
					string string4 = FPRequest.GetString("tables");
					if (string.IsNullOrEmpty(string4))
					{
						this.ShowErr("没有输入要重置的表。");
						return;
					}
					string text9 = "";
					foreach (string str2 in FPArray.SplitString(string4))
					{
						if (text9 != "")
						{
							text9 += "|";
						}
						text9 = text9 + "DELETE FROM [" + str2 + "]";
					}
					string text10 = DbHelper.ExecuteSql(text9);
					if (text10 != "")
					{
						this.ShowErr(text10);
						return;
					}
				}
			}
			this.dbtablelist = DbHelper.GetDbTableList();
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000026C0 File Offset: 0x000008C0
		protected bool IsWMSTable(string tablename)
		{
			return tablename.StartsWith(DbConfigs.Prefix) && FPArray.InArray(tablename, this.system_table) == -1;
		}

		// Token: 0x04000003 RID: 3
		protected DataTable dbtablelist = new DataTable();

		// Token: 0x04000004 RID: 4
		protected string system_table = string.Format("{0}WMS_UserInfo,{0}WMS_UserGrade,{0}WMS_TaskInfo,{0}WMS_RoleInfo,{0}WMS_Permission,{0}WMS_MsgTempInfo,{0}WMS_MenuInfo,{0}WMS_DesktopInfo,{0}WMS_CreditInfo,{0}WMS_CorpInfo,{0}WMS_AttachType,{0}WMS_ChannelInfo", DbConfigs.Prefix);
	}
}
