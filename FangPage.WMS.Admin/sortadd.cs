using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Model;
using LitJson;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200002D RID: 45
	public class sortadd : SuperController
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00009C14 File Offset: 0x00007E14
		protected override void View()
		{
			if (this.id > 0)
			{
				this.sortinfo = DbHelper.ExecuteModel<SortInfo>(this.id);
				this.parentid = this.sortinfo.parentid;
				this.channelid = this.sortinfo.channelid;
				this.appid = this.sortinfo.appid;
			}
			else
			{
				SortInfo sortInfo = SortBll.GetSortInfo(this.parentid);
				this.appid = sortInfo.appid;
			}
			if (this.ispost)
			{
				this.sortinfo.hidden = 0;
				this.sortinfo.types = "";
				this.sortinfo = FPRequest.GetModel<SortInfo>(this.sortinfo);
				if (this.sortinfo.channelid == 0)
				{
					this.ShowErr("请选择栏目频道。");
					return;
				}
				if (this.sortinfo.name == "")
				{
					this.ShowErr("栏目名称不能为空。");
					return;
				}
				if (this.isfile)
				{
					HttpPostedFile postedFile = FPRequest.Files["uploadimg"];
					UpLoad upLoad = new UpLoad();
					string json = upLoad.FileSaveAs(postedFile, "image", this.user, false, false, 16, 16);
					JsonData jsonData = JsonMapper.ToObject(json);
					if (jsonData["error"].ToString() == "")
					{
						if (this.sortinfo.icon != "")
						{
							if (File.Exists(FPUtils.GetMapPath(this.sortinfo.icon)))
							{
								File.Delete(FPUtils.GetMapPath(this.sortinfo.icon));
							}
						}
						this.sortinfo.icon = jsonData["filename"].ToString();
					}
				}
				string text = "0";
				SortInfo sortInfo2 = new SortInfo();
				if (this.sortinfo.id > 0)
				{
					if (DbHelper.ExecuteUpdate<SortInfo>(this.sortinfo) > 0)
					{
						if (this.sortinfo.parentid != this.parentid)
						{
							text = this.sortinfo.parentlist;
							if (this.sortinfo.parentid > 0)
							{
								sortInfo2 = DbHelper.ExecuteModel<SortInfo>(this.sortinfo.parentid);
								this.sortinfo.parentlist = sortInfo2.parentlist + "," + this.sortinfo.id;
							}
							else
							{
								this.sortinfo.parentlist = "0," + this.sortinfo.id.ToString();
							}
							StringBuilder stringBuilder = new StringBuilder();
							stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [parentlist]='{1}' WHERE [id]={2}|", DbConfigs.Prefix, this.sortinfo.parentlist, this.sortinfo.id);
							if (DbConfigs.DbType == DbType.Access)
							{
								stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [parentlist] =REPLACE([parentlist], '{1}', '{2}', 1, 1) WHERE [id] IN (SELECT [id] FROM [{0}WMS_SortInfo]  WHERE [parentlist] LIKE '{3},%')|", new object[]
								{
									DbConfigs.Prefix,
									text,
									this.sortinfo.parentlist,
									text
								});
							}
							else
							{
								stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [parentlist] =STUFF([parentlist],1,{1},'{2}') WHERE [id] IN (SELECT [id] FROM [{0}WMS_SortInfo]  WHERE [parentlist] LIKE '{3},%')|", new object[]
								{
									DbConfigs.Prefix,
									text.Length,
									this.sortinfo.parentlist,
									text
								});
							}
							stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [subcounts]=[subcounts]-1 WHERE [id]={1}|", DbConfigs.Prefix, this.parentid);
							stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [subcounts]=[subcounts]+1 WHERE [id]={1}", DbConfigs.Prefix, this.sortinfo.parentid);
							DbHelper.ExecuteSql(stringBuilder.ToString());
						}
						if (this.sortinfo.channelid != this.channelid && this.parentid == 0)
						{
							string sqlstring = string.Format("UPDATE [{0}WMS_SortInfo] SET [channelid]={1} WHERE [id] IN (SELECT [id] FROM [{0}WMS_SortInfo]  WHERE [parentlist] LIKE '{2},%')", DbConfigs.Prefix, this.sortinfo.channelid, this.sortinfo.parentlist);
							DbHelper.ExecuteSql(sqlstring);
						}
					}
					base.AddMsg("更新栏目成功！");
				}
				else
				{
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeAndWhere("parentid", this.parentid),
						DbHelper.MakeAndWhere("channelid", this.channelid)
					};
					this.sortinfo.display = FPUtils.StrToInt(DbHelper.ExecuteMax<SortInfo>("display", sqlparams).ToString()) + 1;
					this.id = DbHelper.ExecuteInsert<SortInfo>(this.sortinfo);
					if (this.id > 0)
					{
						if (this.sortinfo.parentid > 0)
						{
							sortInfo2 = DbHelper.ExecuteModel<SortInfo>(this.sortinfo.parentid);
							text = sortInfo2.parentlist + "," + this.id;
						}
						else
						{
							text = text + "," + this.id;
						}
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [parentlist]='{1}' WHERE [id]={2}|", DbConfigs.Prefix, text, this.id);
						stringBuilder.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [subcounts]=[subcounts]+1 WHERE [id]={1}", DbConfigs.Prefix, this.sortinfo.parentid);
						DbHelper.ExecuteSql(stringBuilder.ToString());
					}
					base.AddMsg("添加栏目成功！");
				}
				FPCache.Remove("FP_SORTTREE" + this.sortinfo.channelid);
				this.link = "sortmanage.aspx?channelid=" + this.sortinfo.channelid;
			}
			SqlParam[] sqlparams2 = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", 0),
				DbHelper.MakeAndWhere("channelid", this.channelid),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.id)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.channellist = DbHelper.ExecuteList<ChannelInfo>(orderby, new SqlParam[0]);
			this.sortlist = DbHelper.ExecuteList<SortInfo>(orderby, sqlparams2);
			this.sortapplist = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC);
			SqlParam sqlParam = DbHelper.MakeAndWhere("parentid", 0);
			this.typelist = DbHelper.ExecuteList<TypeInfo>(orderby, new SqlParam[]
			{
				sqlParam
			});
			base.SaveRightURL();
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000A2EC File Offset: 0x000084EC
		protected string GetChildSort(int parentid, string tree)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("channelid", this.channelid),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.id)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(orderby, sqlparams);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (SortInfo sortInfo in list)
			{
				string arg = "";
				if (sortInfo.id == this.parentid)
				{
					arg = "selected=\"selected\"";
				}
				stringBuilder.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", sortInfo.id, arg, tree + sortInfo.name);
				stringBuilder.Append(this.GetChildSort(sortInfo.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400005F RID: 95
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000060 RID: 96
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x04000061 RID: 97
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x04000062 RID: 98
		protected int appid = 0;

		// Token: 0x04000063 RID: 99
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x04000064 RID: 100
		protected List<ChannelInfo> channellist = new List<ChannelInfo>();

		// Token: 0x04000065 RID: 101
		protected List<SortInfo> sortlist = new List<SortInfo>();

		// Token: 0x04000066 RID: 102
		protected List<SortAppInfo> sortapplist = new List<SortAppInfo>();

		// Token: 0x04000067 RID: 103
		protected List<TypeInfo> typelist = new List<TypeInfo>();
	}
}
