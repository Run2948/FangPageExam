using Aspose.Words;
using Aspose.Words.Saving;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace FP_Exam.Controller
{
	public class examresultmanage : AdminController
	{
		protected int examid = FPRequest.GetInt("examid");

		protected ExamInfo examinfo = new ExamInfo();

		protected List<ExamResult> examresultlist = new List<ExamResult>();

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected int sortid;

		protected SortInfo sortinfo = new SortInfo();

		protected string keyword = FPRequest.GetString("keyword");

		protected int score_start = FPRequest.GetInt("score_start");

		protected int score_end = FPRequest.GetInt("score_end");

		protected string status = FPRequest.GetString("status");

		protected int orderby = FPRequest.GetInt("orderby");

		protected override void Controller()
		{
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			bool flag = this.examinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该试卷不存在或已被删除。");
			}
			else
			{
				this.sortid = this.examinfo.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				bool ispost = this.ispost;
				if (ispost)
				{
					bool flag2 = this.action == "delete";
					if (flag2)
					{
						string @string = FPRequest.GetString("chkid");
						bool flag3 = DbHelper.ExecuteDelete<ExamResult>(@string) > 0;
						if (flag3)
						{
							SqlParam sqlParam = DbHelper.MakeAndWhere("resultid", WhereType.In, @string);
							DbHelper.ExecuteDelete<ExamResultTopic>(new SqlParam[]
							{
								sqlParam
							});
						}
					}
				}
				bool flag4 = this.examinfo.examdeparts == "" && this.examinfo.examuser == "" && this.examinfo.examroles == "";
				if (flag4)
				{
					List<SqlParam> list = new List<SqlParam>();
					list.Add(DbHelper.MakeAndWhere("examid", this.examid));
					bool flag5 = this.keyword != "";
					if (flag5)
					{
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeOrWhere("username", WhereType.Like, this.keyword),
							DbHelper.MakeOrWhere("realname", WhereType.Like, this.keyword)
						};
						string value = DbHelper.ExecuteField<UserInfo>(sqlparams);
						list.Add(DbHelper.MakeAndWhere("uid", WhereType.In, value));
					}
					bool flag6 = this.score_start > 0;
					if (flag6)
					{
						list.Add(DbHelper.MakeAndWhere("score", WhereType.GreaterThanEqual, this.score_start));
					}
					bool flag7 = this.score_end > 0;
					if (flag7)
					{
						list.Add(DbHelper.MakeAndWhere("score", WhereType.LessThanEqual, this.score_end));
					}
					bool flag8 = this.score_end > 0;
					if (flag8)
					{
						list.Add(DbHelper.MakeAndWhere("score", WhereType.LessThanEqual, this.score_end));
					}
					bool flag9 = this.status != "";
					if (flag9)
					{
						bool flag10 = FPArray.Contain(this.status, 1);
						if (flag10)
						{
							this.status = FPArray.Push(this.status, "2");
						}
						list.Add(DbHelper.MakeAndWhere("status", WhereType.In, this.status));
					}
					bool flag11 = this.orderby == 1;
					if (flag11)
					{
						list.Add(DbHelper.MakeOrderBy("score", OrderBy.DESC));
					}
					else
					{
						bool flag12 = this.orderby == 2;
						if (flag12)
						{
							list.Add(DbHelper.MakeOrderBy("score", OrderBy.DESC));
						}
					}
					list.Add(DbHelper.MakeOrderBy("id", OrderBy.ASC));
					bool flag13 = this.action == "export";
					if (flag13)
					{
						this.examresultlist = DbHelper.ExecuteList<ExamResult>(list.ToArray());
					}
					else
					{
						this.examresultlist = DbHelper.ExecuteList<ExamResult>(this.pager, list.ToArray());
					}
				}
				else
				{
					string text = "";
					bool flag14 = this.examinfo.examroles != "";
					if (flag14)
					{
						SqlParam sqlParam2 = DbHelper.MakeAndWhere("roleid", WhereType.In, this.examinfo.examroles);
						string item4 = DbHelper.ExecuteField<UserInfo>(new SqlParam[]
						{
							sqlParam2
						});
						text = FPArray.Push(text, item4);
					}
					bool flag15 = this.examinfo.examdeparts != "";
					if (flag15)
					{
						SqlParam sqlParam3 = DbHelper.MakeAndWhere("departid", WhereType.In, this.examinfo.examdeparts);
						string item2 = DbHelper.ExecuteField<UserInfo>(new SqlParam[]
						{
							sqlParam3
						});
						text = FPArray.Push(text, item2);
					}
					bool flag16 = this.examinfo.examuser != "";
					if (flag16)
					{
						SqlParam sqlParam4 = DbHelper.MakeAndWhere("id", WhereType.In, this.examinfo.examuser);
						string item3 = DbHelper.ExecuteField<UserInfo>(new SqlParam[]
						{
							sqlParam4
						});
						text = FPArray.Push(text, item3);
					}
					SqlParam sqlParam5 = DbHelper.MakeAndWhere("id", WhereType.In, text);
					List<UserInfo> list2 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
					{
						sqlParam5
					});
					SqlParam[] sqlparams2 = new SqlParam[]
					{
						DbHelper.MakeAndWhere("examid", this.examid),
						DbHelper.MakeOrderBy("id", OrderBy.DESC)
					};
					List<ExamResult> list3 = DbHelper.ExecuteList<ExamResult>(sqlparams2);
					using (List<UserInfo>.Enumerator enumerator = list2.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							UserInfo item = enumerator.Current;
							List<ExamResult> list4 = list3.FindAll((ExamResult exam_result) => exam_result.uid == item.id);
							bool flag17 = list4.Count > 0;
							if (flag17)
							{
								foreach (ExamResult current in list4)
								{
									ExamResult examResult = new ExamResult();
									examResult.uid = item.id;
									examResult.username = item.username;
									examResult.realname = item.realname;
									examResult.nickname = item.nickname;
									examResult.departname = item.departname;
									examResult.examid = this.examid;
									examResult.status = -1;
									examResult.id = current.id;
									examResult.score = current.score;
									examResult.starttime = current.starttime;
									examResult.examdatetime = current.examdatetime;
									examResult.utime = current.utime;
									examResult.status = current.status;
									examResult.questions++;
									examResult.ip = current.ip;
									this.examresultlist.Add(examResult);
								}
							}
							else
							{
								ExamResult examResult2 = new ExamResult();
								examResult2.uid = item.id;
								examResult2.username = item.username;
								examResult2.realname = item.realname;
								examResult2.nickname = item.nickname;
								examResult2.departname = item.departname;
								examResult2.examid = this.examid;
								examResult2.status = -1;
								this.examresultlist.Add(examResult2);
							}
						}
					}
					bool flag18 = this.keyword != "" || this.score_start > 0 || this.score_end > 0 || this.status != "";
					if (flag18)
					{
						list3 = new List<ExamResult>();
						foreach (ExamResult current2 in this.examresultlist)
						{
							bool flag19 = false;
							bool flag20 = this.keyword != "" && (current2.username.Contains(this.keyword) || current2.realname.Contains(this.keyword));
							if (flag20)
							{
								flag19 = true;
							}
							bool flag21 = this.score_start > 0;
							if (flag21)
							{
								flag19 = false;
								bool flag22 = current2.score >= (double)this.score_start;
								if (flag22)
								{
									flag19 = true;
								}
							}
							bool flag23 = this.score_end > 0;
							if (flag23)
							{
								flag19 = false;
								bool flag24 = current2.score <= (double)this.score_end;
								if (flag24)
								{
									flag19 = true;
								}
							}
							bool flag25 = this.status != "";
							if (flag25)
							{
								flag19 = false;
								bool flag26 = FPArray.Contain(this.status, -1) && current2.status == -1;
								if (flag26)
								{
									flag19 = true;
								}
								bool flag27 = FPArray.Contain(this.status, 0) && current2.status == 0;
								if (flag27)
								{
									flag19 = true;
								}
								bool flag28 = FPArray.Contain(this.status, 1) && current2.status >= 1;
								if (flag28)
								{
									flag19 = true;
								}
							}
							bool flag29 = flag19;
							if (flag29)
							{
								list3.Add(current2);
							}
						}
						this.examresultlist = new List<ExamResult>();
						foreach (ExamResult current3 in list3)
						{
							this.examresultlist.Add(current3);
						}
					}
					bool flag30 = this.action != "export" && this.action != "report";
					if (flag30)
					{
						this.pager.total = this.examresultlist.Count;
						int num = (this.pager.pageindex - 1) * this.pager.pagesize;
						int count = this.pager.pagesize;
						bool flag31 = num + this.pager.pagesize > this.pager.total;
						if (flag31)
						{
							count = this.pager.total - num;
						}
						this.examresultlist = this.examresultlist.GetRange(num, count);
					}
				}
				bool ispost2 = this.ispost;
				if (ispost2)
				{
					bool flag32 = this.action == "export";
					if (flag32)
					{
						HSSFWorkbook hSSFWorkbook = new HSSFWorkbook();
						HSSFSheet hSSFSheet = hSSFWorkbook.CreateSheet("Sheet1");
						HSSFCellStyle hSSFCellStyle = hSSFWorkbook.CreateCellStyle();
						hSSFCellStyle.Alignment = CellHorizontalAlignment.CENTER;
						hSSFCellStyle.VerticalAlignment = CellVerticalAlignment.CENTER;
						hSSFCellStyle.BorderTop = CellBorderType.THIN;
						hSSFCellStyle.BorderRight = CellBorderType.THIN;
						hSSFCellStyle.BorderLeft = CellBorderType.THIN;
						hSSFCellStyle.BorderBottom = CellBorderType.THIN;
						hSSFCellStyle.DataFormat = 0;
						HSSFFont hSSFFont = hSSFWorkbook.CreateFont();
						hSSFFont.Boldweight = 32767;
						hSSFCellStyle.SetFont(hSSFFont);
						HSSFRow hSSFRow = hSSFSheet.CreateRow(0);
						hSSFRow.CreateCell(0).SetCellValue("登录名");
						hSSFRow.CreateCell(1).SetCellValue("姓名");
						hSSFRow.CreateCell(2).SetCellValue("所在部门");
						hSSFRow.CreateCell(3).SetCellValue("考试总分");
						hSSFRow.CreateCell(4).SetCellValue("开始时间");
						hSSFRow.CreateCell(5).SetCellValue("答卷耗时");
						hSSFRow.CreateCell(6).SetCellValue("考试IP");
						hSSFRow.CreateCell(7).SetCellValue("考试状态");
						hSSFRow.CreateCell(8).SetCellValue("");
						hSSFRow.Height = 400;
						hSSFSheet.SetColumnWidth(2, 6000);
						hSSFSheet.SetColumnWidth(4, 6000);
						for (int i = 0; i < 8; i++)
						{
							hSSFRow.Cells[i].CellStyle = hSSFCellStyle;
						}
						HSSFCellStyle hSSFCellStyle2 = hSSFWorkbook.CreateCellStyle();
						hSSFCellStyle2.Alignment = CellHorizontalAlignment.CENTER;
						hSSFCellStyle2.VerticalAlignment = CellVerticalAlignment.CENTER;
						hSSFCellStyle2.BorderTop = CellBorderType.THIN;
						hSSFCellStyle2.BorderRight = CellBorderType.THIN;
						hSSFCellStyle2.BorderLeft = CellBorderType.THIN;
						hSSFCellStyle2.BorderBottom = CellBorderType.THIN;
						hSSFCellStyle2.DataFormat = 0;
						int num2 = 1;
						foreach (ExamResult current4 in this.examresultlist)
						{
							HSSFRow hSSFRow2 = hSSFSheet.CreateRow(num2);
							hSSFRow2.Height = 300;
							hSSFRow2.CreateCell(0).SetCellValue(current4.username);
							hSSFRow2.CreateCell(1).SetCellValue(current4.realname);
							hSSFRow2.CreateCell(2).SetCellValue(current4.departname);
							hSSFRow2.CreateCell(3).SetCellValue(current4.score.ToString());
							bool flag33 = current4.status >= 0;
							if (flag33)
							{
								hSSFRow2.CreateCell(4).SetCellValue(current4.examdatetime.ToString("yyyy-MM-dd HH:mm:dd"));
								hSSFRow2.CreateCell(5).SetCellValue(this.GetTime(current4.utime));
							}
							else
							{
								hSSFRow2.CreateCell(4).SetCellValue("");
								hSSFRow2.CreateCell(5).SetCellValue("");
							}
							hSSFRow2.CreateCell(6).SetCellValue(current4.ip);
							bool flag34 = current4.status == 1;
							if (flag34)
							{
								hSSFRow2.CreateCell(7).SetCellValue("已交卷");
							}
							else
							{
								bool flag35 = current4.status == 2;
								if (flag35)
								{
									hSSFRow2.CreateCell(7).SetCellValue("已阅卷");
								}
								else
								{
									bool flag36 = current4.status == 0;
									if (flag36)
									{
										hSSFRow2.CreateCell(7).SetCellValue("未交卷");
									}
									else
									{
										hSSFRow2.CreateCell(7).SetCellValue("缺考");
									}
								}
							}
							hSSFRow2.CreateCell(8).SetCellValue("");
							for (int j = 0; j < 8; j++)
							{
								hSSFRow2.Cells[j].CellStyle = hSSFCellStyle2;
							}
							num2++;
						}
						using (MemoryStream memoryStream = new MemoryStream())
						{
							hSSFWorkbook.Write(memoryStream);
							memoryStream.Flush();
							memoryStream.Position = 0L;
							hSSFSheet.Dispose();
							hSSFWorkbook.Dispose();
							base.Response.ContentType = "application/vnd.ms-excel";
							base.Response.ContentEncoding = Encoding.UTF8;
							base.Response.Charset = "";
							base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(this.examinfo.name + "成绩表.xls"));
							base.Response.BinaryWrite(memoryStream.GetBuffer());
							base.Response.Flush();
							base.Response.End();
						}
					}
					else
					{
						bool flag37 = this.action == "report";
						if (flag37)
						{
							Document document = new Document(FPFile.GetMapPath("template\\examreport.doc"));
							this.InsertText(document, "examtitle", this.examinfo.name);
							this.InsertText(document, "username", this.user.realname);
							this.InsertText(document, "total", this.examinfo.total.ToString() + "分");
							bool flag38 = this.examinfo.islimit == 1;
							if (flag38)
							{
								this.InsertText(document, "examtime", this.examinfo.starttime.ToString("yyyy-MM-dd HH:mm"));
							}
							else
							{
								this.InsertText(document, "examtime", "不限制");
							}
							this.InsertText(document, "exampass", (this.examinfo.passmark * this.examinfo.total / 100.0).ToString() + "分");
							this.InsertText(document, "qtime", this.examinfo.examtime.ToString() + "分钟");
							this.InsertText(document, "examuser", this.examinfo.exams.ToString() + "人");
							bool flag39 = this.examinfo.exams > 0;
							if (flag39)
							{
								this.InsertText(document, "examavg", (this.examinfo.score / (double)this.examinfo.exams).ToString("0.0"));
							}
							else
							{
								this.InsertText(document, "examavg", "0");
							}
							int[] array = new int[5];
							foreach (ExamResult current5 in this.examresultlist)
							{
								bool flag40 = current5.score < 60.0;
								if (flag40)
								{
									array[0]++;
								}
								else
								{
									bool flag41 = current5.score >= 60.0 && current5.score < 70.0;
									if (flag41)
									{
										array[1]++;
									}
									else
									{
										bool flag42 = current5.score >= 70.0 && current5.score < 80.0;
										if (flag42)
										{
											array[2]++;
										}
										else
										{
											bool flag43 = current5.score >= 80.0 && current5.score < 90.0;
											if (flag43)
											{
												array[3]++;
											}
											else
											{
												bool flag44 = current5.score >= 90.0;
												if (flag44)
												{
													array[4]++;
												}
											}
										}
									}
								}
							}
							int num3 = 1;
							int[] array2 = array;
							for (int k = 0; k < array2.Length; k++)
							{
								int num4 = array2[k];
								this.InsertText(document, "s" + num3, num4.ToString() + "人");
								bool flag45 = this.examinfo.exams > 0;
								if (flag45)
								{
									this.InsertText(document, "p" + num3, ((float)num4 / float.Parse(this.examinfo.exams.ToString()) * 100f).ToString("0.0") + "%");
								}
								else
								{
									this.InsertText(document, "p" + num3, "0.0%");
								}
								num3++;
							}
							document.Range.Bookmarks.Clear();
							SaveOptions saveOptions = SaveOptions.CreateSaveOptions(SaveFormat.Doc);
							document.Save(base.Response, this.examinfo.name + "_考试分析报告.doc", ContentDisposition.Attachment, saveOptions);
						}
					}
				}
			}
		}

		protected string GetTime(int utime)
		{
			int num = utime / 3600;
			int num2 = utime % 3600 / 60;
			int num3 = utime % 60;
			bool flag = num < 10;
			string text;
			if (flag)
			{
				text = "0" + num + "时";
			}
			else
			{
				text = num.ToString() + "时";
			}
			bool flag2 = num2 < 10;
			if (flag2)
			{
				text = text + "0" + num2.ToString() + "分";
			}
			else
			{
				text = text + num2.ToString() + "分";
			}
			bool flag3 = num3 < 10;
			if (flag3)
			{
				text = text + "0" + num3.ToString() + "秒";
			}
			else
			{
				text = text + num3.ToString() + "秒";
			}
			return text;
		}

		protected int GetTopicCount(int rid)
		{
			int num = 0;
			List<ExamResultTopic> examResultTopicList = ExamBll.GetExamResultTopicList(rid);
			foreach (ExamResultTopic current in examResultTopicList)
			{
				int[] array = FPArray.SplitInt(current.questionlist);
				for (int i = 0; i < array.Length; i++)
				{
					int num2 = array[i];
					bool flag = num2 > 1312;
					if (flag)
					{
						num++;
					}
				}
			}
			return num;
		}

		public void InsertText(Document doc, string bookmark, string text)
		{
			bool flag = doc.Range.Bookmarks[bookmark] != null;
			if (flag)
			{
				Bookmark bookmark2 = doc.Range.Bookmarks[bookmark];
				bookmark2.Text = text;
			}
		}
	}
}
