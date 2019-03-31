using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FP_Exam.Controller
{
	public class questionmove : AdminController
	{
		protected int channelid = FPRequest.GetInt("channelid");

		protected string idlist = FPRequest.GetString("chkid");

		protected int sortid = FPRequest.GetInt("sortid");

		protected int targetid = FPRequest.GetInt("targetid");

		protected SortInfo sortinfo = new SortInfo();

		protected List<SortInfo> sortlist = new List<SortInfo>();

		protected int pageindex = FPRequest.GetInt("pageindex");

		protected override void Controller()
		{
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			bool ispost = this.ispost;
			if (ispost)
			{
				bool flag = this.targetid == 0;
				if (flag)
				{
					this.ShowErr("对不起，您没有选择移动至目标题库。");
					return;
				}
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeUpdate("sortid", this.targetid),
					DbHelper.MakeUpdate("typelist", ""),
					DbHelper.MakeAndWhere("id", WhereType.In, this.idlist)
				};
				DbHelper.ExecuteUpdate<ExamQuestion>(sqlparams);
				SortBll.UpdateSortPosts(this.sortid, -1);
				SortBll.UpdateSortPosts(this.targetid, 1);
				QuestionBll.UpdateSortQuestion(this.sortinfo.channelid, this.sortid);
				bool flag2 = this.sortinfo.types != "";
				if (flag2)
				{
					QuestionBll.UpdateSortQuestion(this.sortinfo.channelid, this.sortid, this.sortinfo.types);
				}
				SortInfo sortInfo = SortBll.GetSortInfo(this.targetid);
				QuestionBll.UpdateSortQuestion(sortInfo.channelid, this.targetid);
				bool flag3 = sortInfo.types != "";
				if (flag3)
				{
					QuestionBll.UpdateSortQuestion(sortInfo.channelid, this.targetid, sortInfo.types);
				}
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
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.sortid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			this.sortlist = DbHelper.ExecuteList<SortInfo>(sqlparams2);
		}

		protected string GetChildSort(int parentid, string tree)
		{
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("parentid", parentid),
				DbHelper.MakeAndWhere("channelid", this.channelid),
				DbHelper.MakeAndWhere("id", WhereType.NotEqual, this.sortid),
				DbHelper.MakeOrderBy("display", OrderBy.ASC)
			};
			List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(sqlparams);
			StringBuilder stringBuilder = new StringBuilder();
			tree = "│  " + tree;
			foreach (SortInfo current in list)
			{
				string arg = "";
				stringBuilder.AppendFormat("<option value=\"{0}\" {1}>{2}</option>", current.id, arg, tree + current.name);
				stringBuilder.Append(this.GetChildSort(current.id, tree));
			}
			return stringBuilder.ToString();
		}
	}
}
