using System;
using System.Data;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FangPage.Exam.Model;

namespace FangPage.Exam.Controller
{
	// Token: 0x02000017 RID: 23
	public class questionimport : AdminController
	{
		// Token: 0x06000071 RID: 113 RVA: 0x0000B6F8 File Offset: 0x000098F8
		protected override void View()
		{
			this.sortinfo = SortBll.GetSortInfo(this.sortid);
			if (this.ispost)
			{
				if (!this.isfile)
				{
					this.ShowErr("请选择要导入的本地Excel表文件");
					return;
				}
				string mapPath = FPUtils.GetMapPath(this.webpath + "cache");
				string fileName = Path.GetFileName(FPRequest.Files["uploadfile"].FileName);
				string a = Path.GetExtension(fileName).ToLower();
				if (a != ".xls")
				{
					this.ShowErr("该文件不是Excel表文件类型");
					return;
				}
				if (!Directory.Exists(mapPath))
				{
					Directory.CreateDirectory(mapPath);
				}
				if (File.Exists(mapPath + "\\" + fileName))
				{
					File.Delete(mapPath + "\\" + fileName);
				}
				FPRequest.Files["uploadfile"].SaveAs(mapPath + "\\" + fileName);
				DataTable excelTable = FPExcel.GetExcelTable(mapPath + "\\" + fileName);
				if (excelTable.Columns.Count < 14)
				{
					this.ShowErr("对不起，Excel表格列不得少于14。");
					return;
				}
				if (excelTable.Rows.Count > 0)
				{
					int num = excelTable.Rows.Count - 1;
					for (int i = 0; i < excelTable.Rows.Count; i++)
					{
						DataRow dataRow = excelTable.Rows[num - i];
						ExamQuestion examQuestion = new ExamQuestion();
						examQuestion.uid = this.userid;
						examQuestion.channelid = this.sortinfo.channelid;
						examQuestion.type = this.TypeInt(dataRow.ItemArray[0].ToString());
						examQuestion.title = dataRow.ItemArray[1].ToString();
						if (examQuestion.type != 0 && !(examQuestion.title == ""))
						{
							for (int j = 0; j < 6; j++)
							{
								if (dataRow.ItemArray[2 + j].ToString() != "")
								{
									ExamQuestion examQuestion2 = examQuestion;
									examQuestion2.content += ((examQuestion.content == "") ? dataRow.ItemArray[2 + j].ToString() : ("§" + dataRow.ItemArray[2 + j].ToString()));
								}
							}
							if (examQuestion.type == 1 || examQuestion.type == 2)
							{
								examQuestion.answer = dataRow.ItemArray[8].ToString().Trim().ToUpper();
								examQuestion.ascount = FPUtils.SplitString(examQuestion.content, "§").Length;
							}
							else if (examQuestion.type == 3)
							{
								if (FPUtils.InArray(dataRow.ItemArray[8].ToString().Trim(), "y,Y,正确,对"))
								{
									examQuestion.answer = "Y";
								}
								else
								{
									examQuestion.answer = "N";
								}
								examQuestion.ascount = 2;
							}
							else if (examQuestion.type == 4)
							{
								foreach (string a2 in FPUtils.SplitString(examQuestion.content, "§"))
								{
									if (a2 == "区分大小写")
									{
										examQuestion.upperflg = 1;
									}
									else if (a2 == "区分顺序")
									{
										examQuestion.orderflg = 1;
									}
								}
								examQuestion.answer = dataRow.ItemArray[8].ToString().Trim();
								examQuestion.ascount = FPUtils.SplitString(examQuestion.answer).Length;
							}
							else
							{
								examQuestion.answer = dataRow.ItemArray[8].ToString().Trim();
								examQuestion.ascount = 1;
							}
							examQuestion.answerkey = dataRow.ItemArray[9].ToString();
							examQuestion.explain = dataRow.ItemArray[10].ToString();
							examQuestion.difficulty = this.DifficultyInt(dataRow.ItemArray[11].ToString().Trim());
							examQuestion.status = ((dataRow.ItemArray[12].ToString().Trim() == "否") ? 0 : 1);
							examQuestion.sortid = this.GetSortId(dataRow.ItemArray[13].ToString());
							if (examQuestion.sortid == 0)
							{
								examQuestion.sortid = this.sortid;
							}
							DbHelper.ExecuteInsert<ExamQuestion>(examQuestion);
							SortBll.UpdateSortPosts(examQuestion.sortid, 1);
						}
					}
				}
				if (File.Exists(mapPath + "\\" + fileName))
				{
					File.Delete(mapPath + "\\" + fileName);
				}
				base.Response.Redirect("questionmanage.aspx?sortid=" + this.sortid);
			}
			base.SaveRightURL();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		protected int TypeInt(string _type)
		{
			switch (_type)
			{
			case "单选题":
				return 1;
			case "选择题":
				return 1;
			case "多选题":
				return 2;
			case "判断题":
				return 3;
			case "对错题":
				return 3;
			case "填空题":
				return 4;
			case "问答题":
				return 5;
			}
			return 0;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000BD88 File Offset: 0x00009F88
		protected int DifficultyInt(string difficulty)
		{
			if (difficulty != null)
			{
				if (difficulty == "易")
				{
					return 0;
				}
				if (difficulty == "较易")
				{
					return 1;
				}
				if (difficulty == "一般")
				{
					return 2;
				}
				if (difficulty == "较难")
				{
					return 3;
				}
				if (difficulty == "难")
				{
					return 4;
				}
			}
			return 2;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000BDF8 File Offset: 0x00009FF8
		protected int GetSortId(string sortname)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("name", sortname);
			SortInfo sortInfo = DbHelper.ExecuteModel<SortInfo>(new SqlParam[]
			{
				sqlParam
			});
			return sortInfo.id;
		}

		// Token: 0x0400006E RID: 110
		protected int sortid = FPRequest.GetInt("sortid");

		// Token: 0x0400006F RID: 111
		protected SortInfo sortinfo = new SortInfo();
	}
}
