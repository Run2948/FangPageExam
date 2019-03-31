using FangPage.Common;
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
	public class examtopicselect : AdminController
	{
		protected int examtopicid = FPRequest.GetInt("examtopicid");

		protected int paper = FPRequest.GetInt("paper");

		protected int sortid = FPRequest.GetInt("sortid");

		protected int typeid = FPRequest.GetInt("typeid");

		protected int select = FPRequest.GetInt("select");

		protected ExamInfo examinfo = new ExamInfo();

		protected SortInfo examsortinfo = new SortInfo();

		protected ExamTopic examtopic = new ExamTopic();

		protected List<ExamQuestion> questionlist = new List<ExamQuestion>();

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected string keyword = FPRequest.GetString("keyword");

		protected override void Controller()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("question");
			this.examtopic = DbHelper.ExecuteModel<ExamTopic>(this.examtopicid);
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examtopic.examid);
			this.examsortinfo = SortBll.GetSortInfo(this.examinfo.sortid);
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("channelid", this.channelinfo.id));
			list.Add(DbHelper.MakeAndWhere("type", this.examtopic.type));
			bool flag = this.sortid > 0;
			if (flag)
			{
				SortInfo sortInfo = SortBll.GetSortInfo(this.sortid);
				string childSorts = SortBll.GetChildSorts(sortInfo);
				list.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts));
			}
			bool flag2 = this.typeid > 0;
			if (flag2)
			{
				list.Add(DbHelper.MakeAndWhere("typelist", WhereType.Contain, this.typeid));
			}
			bool flag3 = this.select > 0;
			if (flag3)
			{
				list.Add(DbHelper.MakeAndWhere("id", WhereType.In, this.examtopic.questionlist));
			}
			bool flag4 = this.keyword != "";
			if (flag4)
			{
				list.Add(DbHelper.MakeAndWhere("title", WhereType.Like, this.keyword));
			}
			this.questionlist = DbHelper.ExecuteList<ExamQuestion>(this.pager, list.ToArray());
		}

		protected string FmAnswer(string content)
		{
			return content.Replace("(#answer)", "_______");
		}

		protected bool IsSelected(int tid)
		{
			return FPArray.InArray(tid, this.examtopic.questionlist) >= 0;
		}

		protected string Option(string[] opstr, int ascount)
		{
			string[] array = FPArray.SplitString("A,B,C,D,E,F,G,H");
			string text = "";
			bool flag = ascount > opstr.Length;
			if (flag)
			{
				ascount = opstr.Length;
			}
			for (int i = 0; i < ascount; i++)
			{
				text = string.Concat(new string[]
				{
					text,
					array[i],
					".",
					opstr[i],
					"<br/>"
				});
			}
			return text;
		}

		protected string GetPaper(int paper)
		{
			string result;
			switch (paper)
			{
			case 1:
				result = "A卷";
				break;
			case 2:
				result = "B卷";
				break;
			case 3:
				result = "C卷";
				break;
			case 4:
				result = "D卷";
				break;
			default:
				result = "";
				break;
			}
			return result;
		}
	}
}
