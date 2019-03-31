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
	public class examconfig : AdminController
	{
		protected ExamConfig examconfiginfo = new ExamConfig();

		protected override void Controller()
		{
			this.examconfiginfo = ExamConifgs.GetExamConfig();
			bool ispost = this.ispost;
			if (ispost)
			{
				bool flag = this.action == "save";
				if (flag)
				{
					this.examconfiginfo.showanswer = 0;
					this.examconfiginfo = FPRequest.GetModel<ExamConfig>(this.examconfiginfo);
					bool flag2 = this.examconfiginfo.testcount == 0;
					if (flag2)
					{
						this.examconfiginfo.testcount = 80;
					}
					bool flag3 = this.examconfiginfo.testtime == 0;
					if (flag3)
					{
						this.examconfiginfo.testtime = 60;
					}
					ExamConifgs.SaveConfig(this.examconfiginfo);
					base.AddMsg("考试配置保存成功。");
				}
				else
				{
					bool flag4 = this.action == "reset";
					if (flag4)
					{
						QuestionBll.ResetQuestionSort();
						FPCache.Insert("ExamQuestionReset", "已重置题库统计。");
						base.AddMsg("题库统计重置成功。");
					}
					else
					{
						bool flag5 = this.action == "clear";
						if (flag5)
						{
							bool flag6 = DbConfigs.DbType == DbType.SqlServer;
							if (flag6)
							{
								string text = string.Format("TRUNCATE TABLE {0}Exam_ExamQuestion ", DbConfigs.Prefix);
								text += "GO\r\n";
								text += string.Format("TRUNCATE TABLE {0}Exam_ExamLogInfo ", DbConfigs.Prefix);
								text += "GO\r\n";
								text += string.Format("TRUNCATE TABLE {0}Exam_ExamNote ", DbConfigs.Prefix);
								text += "GO\r\n";
								text += string.Format("TRUNCATE TABLE {0}Exam_ExamResult ", DbConfigs.Prefix);
								text += "GO\r\n";
								text += string.Format("TRUNCATE TABLE {0}Exam_ExamResultTopic ", DbConfigs.Prefix);
								text += "GO\r\n";
								text += string.Format("UPDATE [{0}Exam_ExamInfo] SET [score]=0 ", DbConfigs.Prefix);
								text += "GO\r\n";
								text += string.Format("UPDATE [{0}Exam_ExamTopic] SET [questionlist]=''", DbConfigs.Prefix);
								DbHelper.ExecuteSql(text);
							}
							else
							{
								DbHelper.ExecuteDelete<ExamQuestion>(new SqlParam[0]);
								DbHelper.ExecuteDelete<ExamLogInfo>(new SqlParam[0]);
								DbHelper.ExecuteDelete<ExamNote>(new SqlParam[0]);
								DbHelper.ExecuteDelete<ExamResult>(new SqlParam[0]);
								DbHelper.ExecuteDelete<ExamResultTopic>(new SqlParam[0]);
								string text2 = string.Format("UPDATE [{0}Exam_ExamInfo] SET [score]=0", DbConfigs.Prefix);
								text2 += "GO\r\n";
								text2 = string.Format("UPDATE [{0}Exam_ExamTopic] SET [questionlist]=''", DbConfigs.Prefix);
								DbHelper.ExecuteSql(text2);
							}
							SqlParam sqlParam = DbHelper.MakeAndWhere("markup", "question");
							SortAppInfo sortAppInfo = DbHelper.ExecuteModel<SortAppInfo>(new SqlParam[]
							{
								sqlParam
							});
							bool flag7 = sortAppInfo.id > 0;
							if (flag7)
							{
								SqlParam sqlParam2 = DbHelper.MakeAndWhere("appid", sortAppInfo.id);
								List<SortInfo> list = DbHelper.ExecuteList<SortInfo>(new SqlParam[]
								{
									sqlParam2
								});
								SortBll.ResetSortPosts<ExamQuestion>(list);
								foreach (SortInfo current in list)
								{
									TypeBll.ResetTypePosts<ExamQuestion>(current.types, current.id);
									QuestionBll.UpdateSortQuestion(current.channelid, current.id);
									bool flag8 = current.types != "";
									if (flag8)
									{
										QuestionBll.UpdateSortQuestion(current.channelid, current.id, current.types);
									}
								}
							}
							base.AddMsg("题库清空成功。");
						}
					}
				}
			}
		}
	}
}
