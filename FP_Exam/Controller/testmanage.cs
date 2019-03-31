using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class testmanage : AdminController
	{
		protected int channelid = FPRequest.GetInt("channelid");

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected int sortid = FPRequest.GetInt("sortid");

		protected SortInfo sortinfo = new SortInfo();

		protected int typeid = FPRequest.GetInt("typeid");

		protected TypeInfo typeinfo = new TypeInfo();

		protected List<TestInfo> testlist = new List<TestInfo>();

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected string keyword = FPRequest.GetString("keyword");

		protected override void Controller()
		{
			this.channelinfo = ChannelBll.GetChannelInfo(this.channelid);
			bool flag = this.channelinfo.id == 0 && this.channelid > 0;
			if (flag)
			{
				this.ShowErr("对不起，该频道不存在或已被删除。");
			}
			else
			{
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				bool flag2 = this.sortinfo.id == 0 && this.sortid > 0;
				if (flag2)
				{
					this.ShowErr("对不起，该试卷库不存在或已被删除。");
				}
				else
				{
					bool flag3 = this.channelid == 0 && this.sortinfo.id > 0;
					if (flag3)
					{
						this.channelid = this.sortinfo.channelid;
					}
					bool flag4 = this.sortinfo.id == 0;
					if (flag4)
					{
						this.sortinfo.name = this.channelinfo.name;
					}
					bool ispost = this.ispost;
					if (ispost)
					{
						string @string = FPRequest.GetString("chkid");
						bool flag5 = this.action == "delete";
						if (flag5)
						{
							string examSorts = ExamBll.GetExamSorts(@string);
							SortBll.UpdateSortPosts(examSorts, -1);
							DbHelper.ExecuteDelete<TestInfo>(@string);
						}
					}
					List<SqlParam> list = new List<SqlParam>();
					bool flag6 = this.channelid > 0;
					if (flag6)
					{
						list.Add(DbHelper.MakeAndWhere("channelid", this.channelid));
					}
					bool flag7 = this.sortid > 0;
					if (flag7)
					{
						string childSorts = SortBll.GetChildSorts(this.sortinfo);
						list.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts));
					}
					else
					{
						list.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, this.role.sorts));
					}
					bool flag8 = this.keyword != "";
					if (flag8)
					{
						list.Add(DbHelper.MakeAndWhere("name", WhereType.Like, this.keyword));
					}
					bool flag9 = this.typeid > 0;
					if (flag9)
					{
						this.typeinfo = DbHelper.ExecuteModel<TypeInfo>(this.typeid);
						this.pagenav = "|" + this.typeinfo.name + "," + this.rawurl;
						list.Add(DbHelper.MakeAndWhere("typelist", WhereType.Contain, this.typeid));
					}
					this.testlist = DbHelper.ExecuteList<TestInfo>(this.pager, list.ToArray());
				}
			}
		}
	}
}
