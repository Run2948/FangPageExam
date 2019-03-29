using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using FangPage.Data;
using FangPage.Exam;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;
using NPOI.HSSF.UserModel;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000018 RID: 24
	public class questionmanage : AdminController
	{
		// Token: 0x06000076 RID: 118 RVA: 0x0000BE54 File Offset: 0x0000A054
		protected override void View()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			if (this.sortinfo.id <= 0)
			{
				this.ShowErr("该题库已被删除或不存在");
			}
			else
			{
				if (this.channelid == 0)
				{
					this.channelid = this.sortinfo.channelid;
				}
				string childSorts = SortBll.GetChildSorts(this.sortinfo);
				List<SqlParam> list = new List<SqlParam>();
				list.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts));
				if (this.type > 0)
				{
					list.Add(DbHelper.MakeAndWhere("type", this.type));
				}
				if (this.keyword != "")
				{
					list.Add(DbHelper.MakeAndWhere("title", WhereType.Like, this.keyword));
				}
				if (this.ispost)
				{
					if (this.action == "delete")
					{
						string @string = FPRequest.GetString("chkid");
						string questionSorts = QuestionBll.GetQuestionSorts(@string);
						SortBll.UpdateSortPosts(questionSorts, -1);
						DbHelper.ExecuteDelete<ExamQuestion>(@string);
					}
					else if (this.action == "clear")
					{
						DbHelper.ExecuteDelete<ExamQuestion>(new SqlParam[]
						{
							list[0]
						});
					}
					else if (this.action == "move")
					{
						string @string = FPRequest.GetString("chkid");
						if (@string == "")
						{
							this.ShowErr("对不起，您未选择任何选项");
							return;
						}
						base.Response.Redirect(string.Concat(new object[]
						{
							"questionmove.aspx?channelid=",
							this.channelid,
							"&sortid=",
							this.sortid,
							"&pageindex=",
							this.pager.pageindex,
							"&chkid=",
							@string
						}));
					}
					else if (this.action == "export")
					{
						List<ExamQuestion> list2 = DbHelper.ExecuteList<ExamQuestion>(list.ToArray());
						HSSFWorkbook hssfworkbook = new HSSFWorkbook();
						HSSFSheet hssfsheet = hssfworkbook.CreateSheet("Sheet1");
						HSSFCellStyle hssfcellStyle = hssfworkbook.CreateCellStyle();
						hssfcellStyle.Alignment = CellHorizontalAlignment.CENTER;
						hssfcellStyle.VerticalAlignment = CellVerticalAlignment.CENTER;
						hssfcellStyle.BorderTop = CellBorderType.THIN;
						hssfcellStyle.BorderRight = CellBorderType.THIN;
						hssfcellStyle.BorderLeft = CellBorderType.THIN;
						hssfcellStyle.BorderBottom = CellBorderType.THIN;
						hssfcellStyle.DataFormat = 0;
						HSSFFont hssffont = hssfworkbook.CreateFont();
						hssffont.Boldweight = short.MaxValue;
						hssfcellStyle.SetFont(hssffont);
						HSSFRow hssfrow = hssfsheet.CreateRow(0);
						hssfrow.CreateCell(0).SetCellValue("题目类型");
						hssfrow.CreateCell(1).SetCellValue("题目标题");
						hssfrow.CreateCell(2).SetCellValue("选项A");
						hssfrow.CreateCell(3).SetCellValue("选项B");
						hssfrow.CreateCell(4).SetCellValue("选项C");
						hssfrow.CreateCell(5).SetCellValue("选项D");
						hssfrow.CreateCell(6).SetCellValue("选项E");
						hssfrow.CreateCell(7).SetCellValue("选项F");
						hssfrow.CreateCell(8).SetCellValue("正确答案");
						hssfrow.CreateCell(9).SetCellValue("答案关键词");
						hssfrow.CreateCell(10).SetCellValue("答案解释");
						hssfrow.CreateCell(11).SetCellValue("难易程度");
						hssfrow.CreateCell(12).SetCellValue("随机题目");
						hssfrow.CreateCell(13).SetCellValue("所在题库");
						hssfrow.CreateCell(14).SetCellValue("");
						hssfrow.Height = 400;
						hssfsheet.SetColumnWidth(1, 6000);
						for (int i = 0; i < 14; i++)
						{
							hssfrow.Cells[i].CellStyle = hssfcellStyle;
						}
						HSSFCellStyle hssfcellStyle2 = hssfworkbook.CreateCellStyle();
						hssfcellStyle2.Alignment = CellHorizontalAlignment.CENTER;
						hssfcellStyle2.VerticalAlignment = CellVerticalAlignment.CENTER;
						hssfcellStyle2.BorderTop = CellBorderType.THIN;
						hssfcellStyle2.BorderRight = CellBorderType.THIN;
						hssfcellStyle2.BorderLeft = CellBorderType.THIN;
						hssfcellStyle2.BorderBottom = CellBorderType.THIN;
						hssfcellStyle2.DataFormat = 0;
						int num = 1;
						foreach (ExamQuestion examQuestion in list2)
						{
							HSSFRow hssfrow2 = hssfsheet.CreateRow(num);
							hssfrow2.Height = 300;
							hssfrow2.CreateCell(0).SetCellValue(this.TypeStr(examQuestion.type));
							hssfrow2.CreateCell(1).SetCellValue(examQuestion.title.Trim());
							if (examQuestion.type == 1 || examQuestion.type == 2)
							{
								string[] array = FPUtils.SplitString(examQuestion.content, "§", 6);
								int num2 = 0;
								foreach (string cellValue in array)
								{
									hssfrow2.CreateCell(2 + num2).SetCellValue(cellValue);
									num2++;
								}
							}
							else if (examQuestion.type == 4)
							{
								if (examQuestion.upperflg == 1)
								{
									hssfrow2.CreateCell(2).SetCellValue("区分大小写");
								}
								else
								{
									hssfrow2.CreateCell(2).SetCellValue("");
								}
								if (examQuestion.orderflg == 1)
								{
									hssfrow2.CreateCell(3).SetCellValue("区分顺序");
								}
								else
								{
									hssfrow2.CreateCell(3).SetCellValue("");
								}
								hssfrow2.CreateCell(4).SetCellValue("");
								hssfrow2.CreateCell(5).SetCellValue("");
								hssfrow2.CreateCell(6).SetCellValue("");
								hssfrow2.CreateCell(7).SetCellValue("");
							}
							else
							{
								hssfrow2.CreateCell(2).SetCellValue(examQuestion.content.Trim());
								hssfrow2.CreateCell(3).SetCellValue("");
								hssfrow2.CreateCell(4).SetCellValue("");
								hssfrow2.CreateCell(5).SetCellValue("");
								hssfrow2.CreateCell(6).SetCellValue("");
								hssfrow2.CreateCell(7).SetCellValue("");
							}
							hssfrow2.CreateCell(8).SetCellValue(examQuestion.answer.Trim());
							hssfrow2.CreateCell(9).SetCellValue(examQuestion.answerkey.Trim());
							hssfrow2.CreateCell(10).SetCellValue(examQuestion.explain.Trim());
							hssfrow2.CreateCell(11).SetCellValue(this.DifficultyStr(examQuestion.difficulty));
							hssfrow2.CreateCell(12).SetCellValue((examQuestion.status == 1) ? "是" : "否");
							hssfrow2.CreateCell(13).SetCellValue("");
							hssfrow2.CreateCell(14).SetCellValue("");
							for (int i = 0; i < 14; i++)
							{
								hssfrow2.Cells[i].CellStyle = hssfcellStyle2;
							}
							num++;
						}
						using (MemoryStream memoryStream = new MemoryStream())
						{
							hssfworkbook.Write(memoryStream);
							memoryStream.Flush();
							memoryStream.Position = 0L;
							hssfsheet.Dispose();
							hssfworkbook.Dispose();
							base.Response.ContentType = "application/vnd.ms-excel";
							base.Response.ContentEncoding = Encoding.UTF8;
							base.Response.Charset = "";
							base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(this.sortinfo.name + "题库.xls"));
							base.Response.BinaryWrite(memoryStream.GetBuffer());
							base.Response.Flush();
							base.Response.End();
						}
					}
				}
				this.questionlist = DbHelper.ExecuteList<ExamQuestion>(this.pager, list.ToArray());
				if (this.sortinfo.posts != this.pager.total)
				{
					string sqlstring = string.Format("UPDATE [{0}WMS_SortInfo] SET [posts]={1} WHERE [id]={2}", DbConfigs.Prefix, this.pager.total, this.sortid);
					DbHelper.ExecuteSql(sqlstring);
				}
				base.SaveRightURL();
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x0000C7F8 File Offset: 0x0000A9F8
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

		// Token: 0x06000078 RID: 120 RVA: 0x0000C864 File Offset: 0x0000AA64
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

		// Token: 0x06000079 RID: 121 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		protected string FmAnswer(string content)
		{
			return content.Replace("(#answer)", "_______");
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000C8E8 File Offset: 0x0000AAE8
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

		// Token: 0x04000070 RID: 112
		protected int channelid = FPRequest.GetInt("channelid");

		// Token: 0x04000071 RID: 113
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x04000072 RID: 114
		protected int type = FPRequest.GetInt("type");

		// Token: 0x04000073 RID: 115
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x04000074 RID: 116
		protected Pager pager = FPRequest.GetModel<Pager>();

		// Token: 0x04000075 RID: 117
		protected List<ExamQuestion> questionlist = new List<ExamQuestion>();

		// Token: 0x04000076 RID: 118
		protected string keyword = FPRequest.GetString("keyword");

		// Token: 0x04000077 RID: 119
		protected ExamConfig examconfig = new ExamConfig();
	}
}
