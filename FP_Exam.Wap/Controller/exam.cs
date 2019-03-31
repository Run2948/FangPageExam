using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Wap.Controller
{
	public class exam : LoginController
	{
		protected int resultid = FPRequest.GetInt("resultid");

		protected ExamResult examresult = new ExamResult();

		protected ExamConfig examconfig = new ExamConfig();

		protected ExamInfo examinfo = new ExamInfo();

		protected List<ExamResultTopic> examtopiclist = new List<ExamResultTopic>();

		protected override void Controller()
		{
			base.Response.Expires = 0;
			base.Response.CacheControl = "no-cache";
			base.Response.Cache.SetNoStore();
			this.examconfig = ExamConifgs.GetExamConfig();
			this.examresult = ExamBll.GetExamResult(this.resultid);
			bool flag = this.examresult.id == 0;
			if (flag)
			{
				base.AddErr("对不起，该考试不存在或已被删除。");
			}
			else
			{
				bool flag2 = this.examresult.uid != this.userid;
				if (flag2)
				{
					base.AddErr("对不起，您不是本次考试的主人。");
				}
				else
				{
					bool flag3 = this.examresult.status == 1;
					if (flag3)
					{
						base.AddErr("对不起，该考试已完成。");
					}
					else
					{
						this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examresult.examid);
						bool flag4 = this.examinfo.status == 0;
						if (flag4)
						{
							bool flag5 = !this.isperm && this.examinfo.uid != this.userid;
							if (flag5)
							{
								base.AddErr("对不起，该考试已关闭。");
								return;
							}
						}
						bool flag6 = this.examinfo.client["mobile"] != "1";
						if (flag6)
						{
							this.ShowErr("对不起，该考试不能在手机版上考试，请使用电脑版打开。");
						}
						else
						{
							bool flag7 = this.examinfo.channelid == 2 && this.examconfig.teststatus == 0;
							if (flag7)
							{
								base.AddErr("对不起，系统已关闭了用户练习，不能再进行模拟考试。");
							}
							else
							{
								this.examtopiclist = ExamBll.GetExamResultTopicList(this.resultid);
							}
						}
					}
				}
			}
		}

		protected List<ExamQuestion> GetQuestionList(ExamResultTopic resultinfo)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, resultinfo.questionlist);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			int[] array = FPArray.SplitInt(resultinfo.questionlist);
			string[] array2 = FPArray.SplitString(resultinfo.answerlist, "§", array.Length);
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			int num = 0;
			int[] array3 = array;
			for (int i = 0; i < array3.Length; i++)
			{
				int num2 = array3[i];
				foreach (ExamQuestion current in list)
				{
					bool flag = current.id == num2;
					if (flag)
					{
						current.useranswer = array2[num];
						list2.Add(current);
					}
				}
				num++;
			}
			return list2;
		}
	}
}
