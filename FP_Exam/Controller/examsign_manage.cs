using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
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
	public class examsign_manage : LoginController
	{
		protected int examid = FPRequest.GetInt("examid");

		protected int sortid = FPRequest.GetInt("sortid");

		protected string uname = FPRequest.GetString("uname");

		protected string mobile = FPRequest.GetString("mobile");

		protected string idcard = FPRequest.GetString("idcard");

		protected string status = FPRequest.GetString("status");

		protected ExamInfo examinfo = new ExamInfo();

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected List<ExamSignInfo> examsignlist = new List<ExamSignInfo>();

		protected override void Controller()
		{
			this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
			bool flag = this.examinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，该考试不存在或已被删除。");
			}
			bool ispost = this.ispost;
			if (ispost)
			{
				string @string = FPRequest.GetString("chkid");
				bool flag2 = this.action == "audit";
				if (flag2)
				{
					SqlParam[] sqlparams = new SqlParam[]
					{
						DbHelper.MakeUpdate("status", 2),
						DbHelper.MakeAndWhere("id", WhereType.In, @string)
					};
					DbHelper.ExecuteUpdate<ExamSignInfo>(sqlparams);
					SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, @string);
					string text = DbHelper.ExecuteField<ExamSignInfo>("uid", new SqlParam[]
					{
						sqlParam
					});
					bool flag3 = text != "";
					if (flag3)
					{
						this.examinfo.examuser = FPArray.Push(this.examinfo.examuser, text);
						SqlParam[] sqlparams2 = new SqlParam[]
						{
							DbHelper.MakeUpdate("examuser", this.examinfo.examuser),
							DbHelper.MakeAndWhere("id", this.examinfo.id)
						};
						DbHelper.ExecuteUpdate<ExamInfo>(sqlparams2);
					}
				}
				else
				{
					bool flag4 = this.action == "unaudit";
					if (flag4)
					{
						SqlParam[] sqlparams3 = new SqlParam[]
						{
							DbHelper.MakeUpdate("status", 3),
							DbHelper.MakeAndWhere("id", WhereType.In, @string)
						};
						DbHelper.ExecuteUpdate<ExamSignInfo>(sqlparams3);
						SqlParam sqlParam2 = DbHelper.MakeAndWhere("id", WhereType.In, @string);
						string text2 = DbHelper.ExecuteField<ExamSignInfo>("uid", new SqlParam[]
						{
							sqlParam2
						});
						bool flag5 = text2 != "";
						if (flag5)
						{
							this.examinfo.examuser = FPArray.Remove(this.examinfo.examuser, text2);
							SqlParam[] sqlparams4 = new SqlParam[]
							{
								DbHelper.MakeUpdate("examuser", this.examinfo.examuser),
								DbHelper.MakeAndWhere("id", this.examinfo.id)
							};
							DbHelper.ExecuteUpdate<ExamInfo>(sqlparams4);
						}
					}
					else
					{
						bool flag6 = this.action == "delete";
						if (flag6)
						{
							DbHelper.ExecuteDelete<ExamSignInfo>(@string);
							SqlParam sqlParam3 = DbHelper.MakeAndWhere("id", WhereType.In, @string);
							string text3 = DbHelper.ExecuteField<ExamSignInfo>("uid", new SqlParam[]
							{
								sqlParam3
							});
							bool flag7 = text3 != "";
							if (flag7)
							{
								this.examinfo.examuser = FPArray.Remove(this.examinfo.examuser, text3);
								SqlParam[] sqlparams5 = new SqlParam[]
								{
									DbHelper.MakeUpdate("examuser", this.examinfo.examuser),
									DbHelper.MakeAndWhere("id", this.examinfo.id)
								};
								DbHelper.ExecuteUpdate<ExamInfo>(sqlparams5);
							}
						}
					}
				}
			}
			List<SqlParam> list = new List<SqlParam>();
			list.Add(DbHelper.MakeAndWhere("examid", this.examid));
			bool flag8 = this.uname != "";
			if (flag8)
			{
				list.Add(DbHelper.MakeAndWhere("signer[uname]", WhereType.Like, this.uname));
			}
			bool flag9 = this.mobile != "";
			if (flag9)
			{
				list.Add(DbHelper.MakeAndWhere("signer[mobile]", WhereType.Like, this.mobile));
			}
			bool flag10 = this.idcard != "";
			if (flag10)
			{
				list.Add(DbHelper.MakeAndWhere("signer[idcard]", WhereType.Like, this.idcard));
			}
			bool flag11 = this.status != "";
			if (flag11)
			{
				list.Add(DbHelper.MakeAndWhere("status", WhereType.In, this.status));
			}
			bool flag12 = this.ispost && this.action == "export";
			if (flag12)
			{
				string string2 = FPRequest.GetString("chkid");
				bool flag13 = string2 != "";
				if (flag13)
				{
					list.Add(DbHelper.MakeAndWhere("id", WhereType.In, string2));
				}
				this.examsignlist = DbHelper.ExecuteList<ExamSignInfo>(list.ToArray());
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
				hSSFRow.CreateCell(0).SetCellValue("准考证号");
				hSSFRow.CreateCell(1).SetCellValue("姓名");
				hSSFRow.CreateCell(2).SetCellValue("性别");
				hSSFRow.CreateCell(3).SetCellValue("身份证号");
				hSSFRow.CreateCell(4).SetCellValue("民族");
				hSSFRow.CreateCell(5).SetCellValue("出生年月");
				hSSFRow.CreateCell(6).SetCellValue("手机号");
				hSSFRow.CreateCell(7).SetCellValue("电子邮箱");
				hSSFRow.CreateCell(8).SetCellValue("工作单位");
				hSSFRow.CreateCell(9).SetCellValue("工作年限");
				hSSFRow.CreateCell(10).SetCellValue("毕业院校");
				hSSFRow.CreateCell(11).SetCellValue("所学专业");
				hSSFRow.CreateCell(12).SetCellValue("报名时间");
				hSSFRow.CreateCell(13).SetCellValue("审核状态");
				hSSFRow.Height = 400;
				hSSFSheet.SetColumnWidth(0, 6000);
				hSSFSheet.SetColumnWidth(3, 6000);
				hSSFSheet.SetColumnWidth(5, 4500);
				hSSFSheet.SetColumnWidth(6, 4500);
				hSSFSheet.SetColumnWidth(7, 4500);
				hSSFSheet.SetColumnWidth(8, 6000);
				for (int i = 0; i < 14; i++)
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
				int num = 1;
				foreach (ExamSignInfo current in this.examsignlist)
				{
					HSSFRow hSSFRow2 = hSSFSheet.CreateRow(num);
					hSSFRow2.Height = 300;
					hSSFRow2.CreateCell(0).SetCellValue(current.ikey);
					hSSFRow2.CreateCell(1).SetCellValue(current.signer["uname"]);
					hSSFRow2.CreateCell(2).SetCellValue(current.signer["sex"]);
					hSSFRow2.CreateCell(3).SetCellValue(current.signer["idcard"]);
					hSSFRow2.CreateCell(4).SetCellValue(current.signer["nation"]);
					hSSFRow2.CreateCell(5).SetCellValue(current.signer["bday"]);
					hSSFRow2.CreateCell(6).SetCellValue(current.signer["mobile"]);
					hSSFRow2.CreateCell(7).SetCellValue(current.signer["email"]);
					hSSFRow2.CreateCell(8).SetCellValue(current.signer["company"]);
					hSSFRow2.CreateCell(9).SetCellValue(current.signer["joblimit"]);
					hSSFRow2.CreateCell(10).SetCellValue(current.signer["shcool"]);
					hSSFRow2.CreateCell(11).SetCellValue(current.signer["profession"]);
					hSSFRow2.CreateCell(12).SetCellValue(current.postdatetime.ToString("yyyy-MM-dd HH:mm:dd"));
					bool flag14 = current.status == 1;
					if (flag14)
					{
						hSSFRow2.CreateCell(13).SetCellValue("未审核");
					}
					else
					{
						bool flag15 = current.status == 2;
						if (flag15)
						{
							hSSFRow2.CreateCell(13).SetCellValue("已通过");
						}
						else
						{
							bool flag16 = current.status == 3;
							if (flag16)
							{
								hSSFRow2.CreateCell(13).SetCellValue("未通过");
							}
							else
							{
								hSSFRow2.CreateCell(13).SetCellValue("未提交");
							}
						}
					}
					for (int j = 0; j < 14; j++)
					{
						hSSFRow2.Cells[j].CellStyle = hSSFCellStyle2;
					}
					num++;
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
					base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(this.examinfo.name + "报名表.xls"));
					base.Response.BinaryWrite(memoryStream.GetBuffer());
					base.Response.Flush();
					base.Response.End();
				}
			}
			else
			{
				this.examsignlist = DbHelper.ExecuteList<ExamSignInfo>(this.pager, list.ToArray());
			}
		}
	}
}
