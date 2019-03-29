using System;
using System.Text;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x0200000E RID: 14
	public class examtopicadd : AdminController
	{
		// Token: 0x06000046 RID: 70 RVA: 0x000078E4 File Offset: 0x00005AE4
		protected override void View()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			if (this.id > 0)
			{
				this.examtopic = DbHelper.ExecuteModel<ExamTopic>(this.id);
				if (this.examtopic.id == 0)
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
				this.examtopic.display = FPRequest.GetInt(DbHelper.ExecuteMax<ExamTopic>("display", sqlparams).ToString()) + 1;
			}
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			this.sortinfo = SortBll.GetSortInfo(this.examinfo.sortid);
			if (this.ispost)
			{
				double num = this.examtopic.perscore * (double)this.examtopic.questions;
				int questions = this.examtopic.questions;
				this.examtopic = FPRequest.GetModel<ExamTopic>(this.examtopic);
				if (this.examtopic.questions < this.examtopic.curquestions + this.examtopic.randoms)
				{
					this.ShowErr("设定的总题目数不能小于当前手工选题和随机选题之和。");
					return;
				}
				if (this.examtopic.id > 0)
				{
					if (DbHelper.ExecuteUpdate<ExamTopic>(this.examtopic) > 0)
					{
						if (this.paper == 1)
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
				else if (DbHelper.ExecuteInsert<ExamTopic>(this.examtopic) > 0)
				{
					if (this.paper == 1)
					{
						StringBuilder stringBuilder = new StringBuilder();
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
				base.Response.Redirect(string.Concat(new object[]
				{
					"examtopicmanage.aspx?examid=",
					this.examid,
					"&paper=",
					this.paper
				}));
			}
			base.SaveRightURL();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00007C88 File Offset: 0x00005E88
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

		// Token: 0x04000030 RID: 48
		protected int id = FPRequest.GetInt("id");

		// Token: 0x04000031 RID: 49
		protected int paper = FPRequest.GetInt("paper");

		// Token: 0x04000032 RID: 50
		protected int examid = FPRequest.GetInt("examid");

		// Token: 0x04000033 RID: 51
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x04000034 RID: 52
		protected ExamTopic examtopic = new ExamTopic();

		// Token: 0x04000035 RID: 53
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x04000036 RID: 54
		protected ExamConfig examconfig = new ExamConfig();
	}
}
