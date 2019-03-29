using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;
using NPOI.HSSF.UserModel;

namespace FP_Exam.Controller
{
	// Token: 0x0200000C RID: 12
	public class examresultmanage : AdminController
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000064B8 File Offset: 0x000046B8
		protected override void View()
		{
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			if (this.examinfo.id == 0)
			{
				this.ShowErr("对不起，该试卷不存在或已被删除。");
			}
			else
			{
				this.sortid = this.examinfo.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				if (this.ispost)
				{
					if (this.action == "delete")
					{
						string @string = FPRequest.GetString("chkid");
						if (DbHelper.ExecuteDelete<ExamResult>(@string) > 0)
						{
							SqlParam sqlParam = DbHelper.MakeAndWhere("resultid", WhereType.In, @string);
							DbHelper.ExecuteDelete<ExamResultTopic>(new SqlParam[]
							{
								sqlParam
							});
						}
					}
				}
				if (this.examinfo.examdeparts == "" && this.examinfo.examuser == "" && this.examinfo.examroles == "")
				{
					List<SqlParam> list = new List<SqlParam>();
					list.Add(DbHelper.MakeAndWhere("examid", this.examid));
					if (this.keyword != "")
					{
						string text = "0";
						SqlParam sqlParam2 = DbHelper.MakeAndWhere(string.Format("([username] LIKE '%{0}%' OR [realname] LIKE '%{0}%')", this.keyword), WhereType.Custom, "");
						List<UserInfo> list2 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
						{
							sqlParam2
						});
						foreach (UserInfo userInfo in list2)
						{
							if (text != "")
							{
								text += ",";
							}
							text += userInfo.id;
						}
						list.Add(DbHelper.MakeAndWhere("uid", WhereType.In, text));
					}
					if (this.action == "export")
					{
						OrderByParam[] orderbys = new OrderByParam[]
						{
							DbHelper.MakeOrderBy("score", OrderBy.DESC),
							DbHelper.MakeOrderBy("id", OrderBy.ASC)
						};
						this.examresultlist = DbHelper.ExecuteList<ExamResult>(orderbys, list.ToArray());
					}
					else
					{
						this.examresultlist = DbHelper.ExecuteList<ExamResult>(this.pager, list.ToArray());
					}
				}
				else
				{
					string text = "";
					if (this.examinfo.examroles != "")
					{
						SqlParam sqlParam2 = DbHelper.MakeAndWhere("roleid", WhereType.In, this.examinfo.examroles);
						List<UserInfo> list2 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
						{
							sqlParam2
						});
						foreach (UserInfo userInfo in list2)
						{
							if (!FPUtils.InArray(userInfo.id, text))
							{
								ExamResult examResult = new ExamResult();
								examResult.uid = userInfo.id;
								examResult.examid = this.examid;
								examResult.status = -1;
								this.examresultlist.Add(examResult);
								if (text != "")
								{
									text += ",";
								}
								text += userInfo.id;
							}
						}
					}
					if (this.examinfo.examdeparts != "")
					{
						SqlParam sqlParam2 = DbHelper.MakeAndWhere("departid", WhereType.In, this.examinfo.examdeparts);
						List<UserInfo> list2 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
						{
							sqlParam2
						});
						foreach (UserInfo userInfo in list2)
						{
							if (!FPUtils.InArray(userInfo.id, text))
							{
								ExamResult examResult = new ExamResult();
								examResult.uid = userInfo.id;
								examResult.examid = this.examid;
								examResult.status = -1;
								this.examresultlist.Add(examResult);
								if (text != "")
								{
									text += ",";
								}
								text += userInfo.id;
							}
						}
					}
					if (this.examinfo.examuser != "")
					{
						SqlParam sqlParam2 = DbHelper.MakeAndWhere("id", WhereType.In, this.examinfo.examuser);
						List<UserInfo> list2 = DbHelper.ExecuteList<UserInfo>(new SqlParam[]
						{
							sqlParam2
						});
						foreach (UserInfo userInfo in list2)
						{
							if (!FPUtils.InArray(userInfo.id, text))
							{
								ExamResult examResult = new ExamResult();
								examResult.uid = userInfo.id;
								examResult.examid = this.examid;
								examResult.status = -1;
								this.examresultlist.Add(examResult);
								if (text != "")
								{
									text += ",";
								}
								text += userInfo.id;
							}
						}
					}
					SqlParam sqlParam3 = DbHelper.MakeAndWhere("examid", this.examid);
					OrderByParam orderby = DbHelper.MakeOrderBy("id", OrderBy.ASC);
					List<ExamResult> list3 = DbHelper.ExecuteList<ExamResult>(orderby, new SqlParam[]
					{
						sqlParam3
					});
					int num = 0;
					foreach (ExamResult examResult2 in this.examresultlist)
					{
						foreach (ExamResult examResult3 in list3)
						{
							if (examResult3.uid == examResult2.uid)
							{
								this.examresultlist[num].id = examResult3.id;
								this.examresultlist[num].score = examResult3.score;
								this.examresultlist[num].starttime = examResult3.starttime;
								this.examresultlist[num].examdatetime = examResult3.examdatetime;
								this.examresultlist[num].utime = examResult3.utime;
								this.examresultlist[num].status = examResult3.status;
								this.examresultlist[num].questions++;
								this.examresultlist[num].ip = examResult3.ip;
							}
						}
						num++;
					}
					if (this.keyword != "")
					{
						list3 = new List<ExamResult>();
						foreach (ExamResult examResult2 in this.examresultlist)
						{
							if (examResult2.IUser.username.Contains(this.keyword) || examResult2.IUser.realname.Contains(this.keyword))
							{
								list3.Add(examResult2);
							}
						}
						this.examresultlist = new List<ExamResult>();
						foreach (ExamResult examResult2 in list3)
						{
							this.examresultlist.Add(examResult2);
						}
					}
					if (this.action != "export" && this.action != "report")
					{
						this.pager.total = this.examresultlist.Count;
						int num2 = (this.pager.pageindex - 1) * this.pager.pagesize;
						int count = this.pager.pagesize;
						if (num2 + this.pager.pagesize > this.pager.total)
						{
							count = this.pager.total - num2;
						}
						this.examresultlist = this.examresultlist.GetRange(num2, count);
					}
				}
				if (this.ispost)
				{
					if (this.action == "export")
					{
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
						hssfrow.CreateCell(0).SetCellValue("用户名");
						hssfrow.CreateCell(1).SetCellValue("姓名");
						hssfrow.CreateCell(2).SetCellValue("所在部门");
						hssfrow.CreateCell(3).SetCellValue("考试得分");
						hssfrow.CreateCell(4).SetCellValue("开始时间");
						hssfrow.CreateCell(5).SetCellValue("考试用时");
						hssfrow.CreateCell(6).SetCellValue("考试状态");
						hssfrow.CreateCell(7).SetCellValue("");
						hssfrow.Height = 400;
						hssfsheet.SetColumnWidth(2, 6000);
						hssfsheet.SetColumnWidth(4, 6000);
						for (int i = 0; i < 7; i++)
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
						int num3 = 1;
						foreach (ExamResult examResult2 in this.examresultlist)
						{
							HSSFRow hssfrow2 = hssfsheet.CreateRow(num3);
							hssfrow2.Height = 300;
							hssfrow2.CreateCell(0).SetCellValue(examResult2.IUser.username);
							hssfrow2.CreateCell(1).SetCellValue(examResult2.IUser.realname);
							hssfrow2.CreateCell(2).SetCellValue(examResult2.IUser.Department.name);
							hssfrow2.CreateCell(3).SetCellValue(examResult2.score.ToString());
							if (examResult2.status >= 0)
							{
								hssfrow2.CreateCell(4).SetCellValue(examResult2.examdatetime.ToString("yyyy-MM-dd HH:mm:dd"));
								hssfrow2.CreateCell(5).SetCellValue((examResult2.utime / 60 + 1).ToString() + "分钟");
							}
							else
							{
								hssfrow2.CreateCell(4).SetCellValue("");
								hssfrow2.CreateCell(5).SetCellValue("");
							}
							if (examResult2.status == 1)
							{
								hssfrow2.CreateCell(6).SetCellValue("已交卷");
							}
							else if (examResult2.status == 2)
							{
								hssfrow2.CreateCell(6).SetCellValue("已阅卷");
							}
							else if (examResult2.status == 0)
							{
								hssfrow2.CreateCell(6).SetCellValue("未交卷");
							}
							else
							{
								hssfrow2.CreateCell(6).SetCellValue("缺考");
							}
							hssfrow2.CreateCell(7).SetCellValue("");
							for (int i = 0; i < 7; i++)
							{
								hssfrow2.Cells[i].CellStyle = hssfcellStyle2;
							}
							num3++;
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
							base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(this.examinfo.name + "成绩表.xls"));
							base.Response.BinaryWrite(memoryStream.GetBuffer());
							base.Response.Flush();
							base.Response.End();
						}
					}
					else if (this.action == "report")
					{
						AsposeWordApp asposeWordApp = new AsposeWordApp();
						asposeWordApp.Open(FPUtils.GetMapPath("images\\examreport.doc"));
						asposeWordApp.InsertText("examtitle", this.examinfo.name);
						asposeWordApp.InsertText("username", this.user.realname);
						asposeWordApp.InsertText("total", this.examinfo.total.ToString() + "分");
						if (this.examinfo.islimit == 1)
						{
							asposeWordApp.InsertText("examtime", this.examinfo.starttime.ToString("yyyy-MM-dd HH:mm"));
						}
						else
						{
							asposeWordApp.InsertText("examtime", "不限制");
						}
						asposeWordApp.InsertText("exampass", (this.examinfo.passmark * this.examinfo.total / 100.0).ToString() + "分");
						asposeWordApp.InsertText("qtime", this.examinfo.examtime.ToString() + "分钟");
						asposeWordApp.InsertText("examuser", this.examinfo.exams.ToString() + "人");
						if (this.examinfo.exams > 0)
						{
							asposeWordApp.InsertText("examavg", (this.examinfo.score / (double)this.examinfo.exams).ToString("0.0"));
						}
						else
						{
							asposeWordApp.InsertText("examavg", "0");
						}
						int[] array = new int[5];
						foreach (ExamResult examResult2 in this.examresultlist)
						{
							if (examResult2.score < 60.0)
							{
								array[0]++;
							}
							else if (examResult2.score >= 60.0 && examResult2.score < 70.0)
							{
								array[1]++;
							}
							else if (examResult2.score >= 70.0 && examResult2.score < 80.0)
							{
								array[2]++;
							}
							else if (examResult2.score >= 80.0 && examResult2.score < 90.0)
							{
								array[3]++;
							}
							else if (examResult2.score >= 90.0)
							{
								array[4]++;
							}
						}
						int i = 1;
						foreach (int num4 in array)
						{
							asposeWordApp.InsertText("s" + i, num4.ToString() + "人");
							asposeWordApp.InsertText("p" + i, (num4 / this.examinfo.exams * 100).ToString("0.0") + "%");
							i++;
						}
						asposeWordApp.Save(base.Response, this.examinfo.name + "_考试分析报告.doc");
					}
				}
				base.SaveRightURL();
			}
		}

		// Token: 0x04000027 RID: 39
		protected int examid = FPRequest.GetInt("examid");

		// Token: 0x04000028 RID: 40
		protected ExamInfo examinfo = new ExamInfo();

		// Token: 0x04000029 RID: 41
		protected List<ExamResult> examresultlist = new List<ExamResult>();

		// Token: 0x0400002A RID: 42
		protected Pager pager = FPRequest.GetModel<Pager>();

		// Token: 0x0400002B RID: 43
		protected int sortid;

		// Token: 0x0400002C RID: 44
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x0400002D RID: 45
		protected string keyword = FPRequest.GetString("keyword");
	}
}
