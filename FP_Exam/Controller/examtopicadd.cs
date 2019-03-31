using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Text;

namespace FP_Exam.Controller
{
	public class examtopicadd : AdminController
	{
		protected int id = FPRequest.GetInt("id");

		protected int paper = FPRequest.GetInt("paper");

		protected int examid = FPRequest.GetInt("examid");

		protected ExamInfo examinfo = new ExamInfo();

		protected ExamTopic examtopic = new ExamTopic();

		protected SortInfo sortinfo = new SortInfo();

		protected ExamConfig examconfig = new ExamConfig();

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			bool flag = this.id > 0;
			if (flag)
			{
				this.examtopic = DbHelper.ExecuteModel<ExamTopic>(this.id);
				bool flag2 = this.examtopic.id == 0;
				if (flag2)
				{
					this.ShowErr("对不起，该试卷大题不存在或已被删除。");
					return;
				}
				this.examid = this.examtopic.examid;
				this.paper = this.examtopic.paper;
			}
			else
			{
				SqlParam[] sqlparams = new SqlParam[]
				{
					DbHelper.MakeAndWhere("examid", this.examid),
					DbHelper.MakeAndWhere("paper", this.paper)
				};
				this.examtopic.display = FPUtils.StrToInt(DbHelper.ExecuteMax<ExamTopic>("display", sqlparams).ToString()) + 1;
			}
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			this.sortinfo = SortBll.GetSortInfo(this.examinfo.sortid);
			bool ispost = this.ispost;
			if (ispost)
			{
				double num = this.examtopic.perscore * (double)this.examtopic.questions;
				int questions = this.examtopic.questions;
				this.examtopic = FPRequest.GetModel<ExamTopic>(this.examtopic);
				bool flag3 = this.examtopic.questions < this.examtopic.curquestions + this.examtopic.randoms;
				if (flag3)
				{
					this.ShowErr("设定的总题目数不能小于当前手工选题和随机选题之和。");
				}
				else
				{
					bool flag4 = this.examtopic.id > 0;
					if (flag4)
					{
						bool flag5 = DbHelper.ExecuteUpdate<ExamTopic>(this.examtopic) > 0;
						if (flag5)
						{
							bool flag6 = this.paper == 1;
							if (flag6)
							{
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamInfo] SET [total]=[total]-{1},[questions]=[questions]-{2} WHERE [id]={3}|", new object[]
								{
									DbConfigs.Prefix,
									num,
									questions,
									this.examid
								});
								stringBuilder.AppendFormat("UPDATE [{0}Exam_ExamInfo] SET [total]=[total]+{1},[questions]=[questions]+{2} WHERE [id]={3}", new object[]
								{
									DbConfigs.Prefix,
									this.examtopic.perscore * (double)this.examtopic.questions,
									this.examtopic.questions,
									this.examid
								});
								DbHelper.ExecuteSql(stringBuilder.ToString());
							}
						}
					}
					else
					{
						bool flag7 = DbHelper.ExecuteInsert<ExamTopic>(this.examtopic) > 0;
						if (flag7)
						{
							bool flag8 = this.paper == 1;
							if (flag8)
							{
								StringBuilder stringBuilder2 = new StringBuilder();
								stringBuilder2.AppendFormat("UPDATE [{0}Exam_ExamInfo] SET [total]=[total]+{1},[questions]=[questions]+{2} WHERE [id]={3}", new object[]
								{
									DbConfigs.Prefix,
									this.examtopic.perscore * (double)this.examtopic.questions,
									this.examtopic.questions,
									this.examid
								});
								DbHelper.ExecuteSql(stringBuilder2.ToString());
							}
						}
					}
					base.Response.Redirect(string.Concat(new object[]
					{
						"examtopicmanage.aspx?examid=",
						this.examid,
						"&paper=",
						this.paper
					}));
				}
			}
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
