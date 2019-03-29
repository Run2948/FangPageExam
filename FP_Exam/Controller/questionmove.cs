using System;
using System.Collections.Generic;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000019 RID: 25
	public class questionmove : AdminController
	{
		// Token: 0x0600007C RID: 124 RVA: 0x0000C9F0 File Offset: 0x0000ABF0
		protected override void View()
		{
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			if (this.ispost)
			{
				if (this.targetid == 0)
				{
					this.ShowErr("对不起，您没有选择移动至目标题库。");
					return;
				}
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeSet("sortid", this.targetid),
					DbHelper.MakeAndWhere("id", WhereType.In, this.idlist)
				};
				DbHelper.ExecuteUpdate<ExamQuestion>(sqlparams);
				SortBll.UpdateSortPosts(this.sortid, -1);
				SortBll.UpdateSortPosts(this.targetid, 1);
				base.Response.Redirect(string.Concat(new object[]
				{
					"questionmanage.aspx?channelid=",
					this.channelid,
					"&sortid=",
					this.sortid,
					"&pageindex=",
					this.pageindex
				}));
			}
			SqlParam[] sqlparams2 = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", 0),
				DbHelper.MakeAndWhere("channelid", this.channelid),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.sortid)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			this.sortlist = DbHelper.ExecuteList<SortInfo>(orderby, sqlparams2);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000CB6C File Offset: 0x0000AD6C
		protected string GetChildSort(int parentid, string tree)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("channelid", this.channelid),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.sortid)
			};
			OrderByParam orderby = DbHelper.MakeOrderBy("display", OrderBy.ASC);
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(orderby, sqlparams);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (SortInfo sortInfo in list)
			{
				string arg = "";
				stringBuilder.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", sortInfo.id, arg, tree + sortInfo.name);
				stringBuilder.Append(this.GetChildSort(sortInfo.id, tree));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000078 RID: 120
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x04000079 RID: 121
		protected string idlist = FPRequest.GetString("chkid");

		// Token: 0x0400007A RID: 122
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x0400007B RID: 123
		protected int targetid = FPRequest.GetInt("targetid");

		// Token: 0x0400007C RID: 124
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x0400007D RID: 125
		protected List<SortInfo> sortlist = new List<SortInfo>();

		// Token: 0x0400007E RID: 126
		protected int pageindex = FPRequest.GetInt("pageindex");
	}
}
