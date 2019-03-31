using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;

namespace FangPage.WMS.Admin
{
	// Token: 0x0200003A RID: 58
	public class sortadd : SuperController
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000B814 File Offset: 0x00009A14
		protected override void Controller()
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
			this.channelinfo = ChannelBll.GetChannelInfo(this.channelid);
			if (this.channelinfo.id == 0)
			{
				this.ShowErr("对不起，该栏目频道不存在或已被删除。");
				return;
			}
			if (this.sortinfo.attach_img == "")
			{
				this.sortinfo.attach_img = FPRandom.CreateCode(20);
			}
			if (this.ispost)
			{
				this.sortinfo.types = "";
				this.sortinfo.showtype = 0;
				this.sortinfo = FPRequest.GetModel<SortInfo>(this.sortinfo);
				this.sortinfo.channelid = this.channelid;
				if (this.sortinfo.name == "")
				{
					this.ShowErr("栏目名称不能为空。");
					return;
				}
				if (this.isfile)
				{
					HttpPostedFile files = FPRequest.Files["uploadimg"];
					if (this.sortinfo.attach_icon == "")
					{
						this.sortinfo.attach_icon = FPRandom.CreateCode(20);
					}
					AttachInfo attachInfo = AttachBll.UploadImg(files, this.sortinfo.attach_icon, this.userid, "sort_icon", 16, 16);
					if (attachInfo.id > 0)
					{
						this.sortinfo.icon = attachInfo.filename;
					}
				}
				string text = "0";
				SortInfo sortInfo2 = new SortInfo();
				if (this.sortinfo.id > 0)
				{
					if (DbHelper.ExecuteUpdate<SortInfo>(this.sortinfo) > 0 && this.sortinfo.parentid != this.parentid)
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
					this.sortinfo.id = DbHelper.ExecuteInsert<SortInfo>(this.sortinfo);
					if (this.sortinfo.id > 0)
					{
						if (this.sortinfo.parentid > 0)
						{
							sortInfo2 = DbHelper.ExecuteModel<SortInfo>(this.sortinfo.parentid);
							text = sortInfo2.parentlist + "," + this.sortinfo.id;
						}
						else
						{
							text = text + "," + this.sortinfo.id;
						}
						StringBuilder stringBuilder2 = new StringBuilder();
						stringBuilder2.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [parentlist]='{1}' WHERE [id]={2}|", DbConfigs.Prefix, text, this.sortinfo.id);
						stringBuilder2.AppendFormat("UPDATE [{0}WMS_SortInfo] SET [subcounts]=[subcounts]+1 WHERE [id]={1}", DbConfigs.Prefix, this.sortinfo.parentid);
						DbHelper.ExecuteSql(stringBuilder2.ToString());
					}
					base.AddMsg("添加栏目成功！");
				}
				if (this.sortinfo.icon != "")
				{
					AttachBll.UpdateOnceAttach(this.sortinfo.attach_icon, this.sortinfo.id);
				}
				else
				{
					AttachBll.Delete(this.sortinfo.attach_icon);
				}
				if (this.sortinfo.img != "")
				{
					AttachBll.UpdateOnceAttach(this.sortinfo.attach_img, this.sortinfo.id);
				}
				else
				{
					AttachBll.Delete(this.sortinfo.attach_img);
				}
				FPCache.Remove("FP_SORTTREE" + this.sortinfo.channelid);
				this.backurl = "sortmanage.aspx?channelid=" + this.sortinfo.channelid;
			}
			SqlParam[] sqlparams2 = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", 0),
				DbHelper.MakeAndWhere("channelid", this.channelid),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.id),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.sortlist = DbHelper.ExecuteList<SortInfo>(sqlparams2);
			List<SqlParam> list = new List<SqlParam>();
			if (this.channelinfo.sortapps != "")
			{
				list.Add(DbHelper.MakeAndWhere("id", WhereType.In, this.channelinfo.sortapps));
			}
			this.sortapplist = DbHelper.ExecuteList<SortAppInfo>(OrderBy.ASC, list.ToArray());
			if (this.appid == 0 && this.sortapplist.Count > 0)
			{
				this.appid = this.sortapplist[0].id;
			}
			SqlParam[] sqlparams3 = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", 0),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.typelist = DbHelper.ExecuteList<TypeInfo>(sqlparams3);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000BEB4 File Offset: 0x0000A0B4
		protected string GetChildSort(int parentid, string tree)
		{
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("channelid", this.channelid),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.id),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			});
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

		// Token: 0x04000097 RID: 151
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000098 RID: 152
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x04000099 RID: 153
		protected int parentid = FPRequest.GetInt("parentid");

		// Token: 0x0400009A RID: 154
		protected int appid;

		// Token: 0x0400009B RID: 155
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x0400009C RID: 156
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x0400009D RID: 157
		protected List<SortInfo> sortlist = new List<SortInfo>();

		// Token: 0x0400009E RID: 158
		protected List<SortAppInfo> sortapplist = new List<SortAppInfo>();

		// Token: 0x0400009F RID: 159
		protected List<TypeInfo> typelist = new List<TypeInfo>();
	}
}
