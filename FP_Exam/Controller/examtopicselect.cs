using System;
using System.Collections.Generic;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x02000012 RID: 18
	public class examtopicselect : AdminController
	{
		// Token: 0x06000059 RID: 89 RVA: 0x00009654 File Offset: 0x00007854
		protected override void View()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("exam_question");
			this.examtopic = DbHelper.ExecuteModel<ExamTopic>(this.examtopicid);
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examtopic.examid);
			this.examsortinfo = SortBll.GetSortInfo(this.examinfo.sortid);
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("channelid", this.channelinfo.id));
			list.Add(DbHelper.MakeAndWhere("type", this.examtopic.type));
			if (this.sortid > 0)
			{
				SortInfo sortInfo = SortBll.GetSortInfo(this.sortid);
				string childSorts = SortBll.GetChildSorts(sortInfo);
				list.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts));
			}
			if (this.select > 0)
			{
				list.Add(DbHelper.MakeAndWhere("id", WhereType.In, this.examtopic.questionlist));
			}
			if (this.keyword != "")
			{
				list.Add(DbHelper.MakeAndWhere("title", WhereType.Like, this.keyword));
			}
			this.questionlist = DbHelper.ExecuteList<ExamQuestion>(this.pager, list.ToArray());
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000097A8 File Offset: 0x000079A8
		protected string TypeStr(int _type)
		{
			string result;
			switch (_type)
			{
			case 1:
				result = "单选题";
				break;
			case 2:
				result = "多选题";
				break;
			case 3:
				result = "判断题";
				break;
			case 4:
				result = "填空题";
				break;
			case 5:
				result = "问答题";
				break;
			case 6:
				result = "打字题";
				break;
			default:
				result = "题目";
				break;
			}
			return result;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00009814 File Offset: 0x00007A14
		protected string FmAnswer(string content)
		{
			return content.Replace("(#answer)", "_______");
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00009838 File Offset: 0x00007A38
		protected bool IsSelected(int tid)
		{
			return FPUtils.InArray(tid, this.examtopic.questionlist);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000985C File Offset: 0x00007A5C
		protected string DifficultyStr(int difficulty)
		{
			string result;
			switch (difficulty)
			{
			case 0:
				result = "易";
				break;
			case 1:
				result = "较易";
				break;
			case 2:
				result = "一般";
				break;
			case 3:
				result = "较难";
				break;
			case 4:
				result = "难";
				break;
			default:
				result = "";
				break;
			}
			return result;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x000098BC File Offset: 0x00007ABC
		protected string Option(string[] opstr, int ascount)
		{
			string[] array = FPUtils.SplitString("A,B,C,D,E,F");
			string text = "";
			if (ascount > opstr.Length)
			{
				ascount = opstr.Length;
			}
			for (int i = 0; i < ascount; i++)
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					array[i],
					".",
					opstr[i],
					"<br/>"
				});
			}
			return text;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00009944 File Offset: 0x00007B44
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

		// Token: 0x0400004D RID: 77
		protected int examtopicid = FPRequest.GetInt("examtopicid");

		// Token: 0x0400004E RID: 78
		protected int paper = FPRequest.GetInt("paper");

		// Token: 0x0400004F RID: 79
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x04000050 RID: 80
		protected int select = FPRequest.GetInt("select");

		// Token: 0x04000051 RID: 81
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x04000052 RID: 82
		protected SortInfo examsortinfo = new SortInfo();

		// Token: 0x04000053 RID: 83
		protected ExamTopic examtopic = new ExamTopic();

		// Token: 0x04000054 RID: 84
		protected List<ExamQuestion> questionlist = new List<ExamQuestion>();

		// Token: 0x04000055 RID: 85
		protected Pager pager = FPRequest.GetModel<Pager>();

		// Token: 0x04000056 RID: 86
		protected ChannelInfo channelinfo = new ChannelInfo();

		// Token: 0x04000057 RID: 87
		protected string keyword = FPRequest.GetString("keyword");
	}
}
