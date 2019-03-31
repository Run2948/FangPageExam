using Aspose.Words;
using Aspose.Words.Saving;
using Aspose.Words.Tables;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace FP_Exam.Controller
{
	public class examread : LoginController
	{
		protected int resultid = FPRequest.GetInt("resultid");

		protected ExamResult examresult = new ExamResult();

		protected List<ExamResultTopic> examtopicresultlist = new List<ExamResultTopic>();

		protected string[] answerarr = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F",
			"G",
			"H"
		};

		protected double maxscore = 0.0;

		protected double avgscore = 0.0;

		protected int testers = 0;

		protected int display = 0;

		protected Dictionary<int, ExamLogInfo> examloglist = new Dictionary<int, ExamLogInfo>();

		protected List<string> videoimg = new List<string>();

		protected override void Controller()
		{
			bool flag = !this.isperm;
			if (flag)
			{
				this.ShowErr("对不起，您没有权限阅卷。");
			}
			else
			{
				this.examresult = DbHelper.ExecuteModel<ExamResult>(this.resultid);
				bool flag2 = this.examresult.id == 0;
				if (flag2)
				{
					this.ShowErr("该考生的试卷不存在或已被删除。");
				}
				else
				{
					bool flag3 = this.examresult.attachid != "";
					if (flag3)
					{
						SqlParam sqlParam = DbHelper.MakeAndWhere("description", "考试上传视频图片");
						List<AttachInfo> attachList = AttachBll.GetAttachList(this.examresult.attachid, new SqlParam[]
						{
							sqlParam
						});
						bool flag4 = attachList.Count > 0;
						if (flag4)
						{
							this.videoimg.Add(attachList[0].filename);
						}
						bool flag5 = attachList.Count >= 2;
						if (flag5)
						{
							this.videoimg.Add(attachList[attachList.Count / 2].filename);
						}
						bool flag6 = attachList.Count >= 3;
						if (flag6)
						{
							this.videoimg.Add(attachList[attachList.Count - 1].filename);
						}
					}
					this.examloglist = ExamBll.GetExamLogList(this.examresult.channelid, this.userid);
					this.examtopicresultlist = ExamBll.GetExamResultTopicList(this.resultid);
					bool ispost = this.ispost;
					if (ispost)
					{
						Document document = document = new Document(FPFile.GetMapPath(this.webpath + "exam/admin/template/examresult.doc"));
						DocumentBuilder documentBuilder = new DocumentBuilder(document);
						bool flag7 = document.Range.Bookmarks["examtitle"] != null;
						if (flag7)
						{
							Bookmark bookmark = document.Range.Bookmarks["examtitle"];
							bookmark.Text = this.examresult.examname;
						}
						bool flag8 = document.Range.Bookmarks["subtitle"] != null;
						if (flag8)
						{
							Bookmark bookmark2 = document.Range.Bookmarks["subtitle"];
							bookmark2.Text = this.GetPaper(this.examresult.paper);
						}
						bool flag9 = document.Range.Bookmarks["exam_uname"] != null;
						if (flag9)
						{
							Bookmark bookmark3 = document.Range.Bookmarks["exam_uname"];
							bookmark3.Text = this.examresult.username;
						}
						bool flag10 = document.Range.Bookmarks["exam_address"] != null;
						if (flag10)
						{
							Bookmark bookmark4 = document.Range.Bookmarks["exam_address"];
							bookmark4.Text = this.examresult.address;
						}
						bool flag11 = document.Range.Bookmarks["exam_realname"] != null;
						if (flag11)
						{
							Bookmark bookmark5 = document.Range.Bookmarks["exam_realname"];
							bookmark5.Text = this.examresult.realname;
						}
						bool flag12 = document.Range.Bookmarks["exam_idcard"] != null;
						if (flag12)
						{
							Bookmark bookmark6 = document.Range.Bookmarks["exam_idcard"];
							bookmark6.Text = this.examresult.idcard;
						}
						bool flag13 = document.Range.Bookmarks["exam_departname"] != null;
						if (flag13)
						{
							Bookmark bookmark7 = document.Range.Bookmarks["exam_departname"];
							bookmark7.Text = this.examresult.departname;
						}
						bool flag14 = document.Range.Bookmarks["exam_nickname"] != null;
						if (flag14)
						{
							Bookmark bookmark8 = document.Range.Bookmarks["exam_nickname"];
							bookmark8.Text = this.examresult.nickname;
						}
						documentBuilder.MoveToBookmark("content");
						DataTable dataTable = new DataTable();
						DataColumn column = new DataColumn("s0", Type.GetType("System.String"));
						dataTable.Columns.Add(column);
						int num = 1;
						foreach (ExamResultTopic current in this.examtopicresultlist)
						{
							column = new DataColumn("s" + num, Type.GetType("System.String"));
							dataTable.Columns.Add(column);
							num++;
						}
						column = new DataColumn("s" + num, Type.GetType("System.String"));
						dataTable.Columns.Add(column);
						DataRow dataRow = dataTable.NewRow();
						dataRow["s0"] = "题  号";
						num = 1;
						foreach (ExamResultTopic current2 in this.examtopicresultlist)
						{
							dataRow["s" + num] = this.GetNum(num);
							num++;
						}
						dataRow["s" + num] = "总  分";
						dataTable.Rows.Add(dataRow);
						dataRow = dataTable.NewRow();
						dataRow["s0"] = "得  分";
						num = 1;
						foreach (ExamResultTopic current3 in this.examtopicresultlist)
						{
							dataRow["s" + num] = current3.score;
							num++;
						}
						dataRow["s" + num] = this.examresult.score;
						dataTable.Rows.Add(dataRow);
						this.InsertTable(documentBuilder, dataTable, true);
						num = 1;
						List<ImgInfo> imagelist = new List<ImgInfo>();
						foreach (ExamResultTopic current4 in this.examtopicresultlist)
						{
							documentBuilder.Font.Color = Color.Black;
							documentBuilder.Writeln();
							string strText = string.Concat(new object[]
							{
								current4.title,
								"  (共",
								current4.questions,
								"题，每题",
								current4.perscore,
								"分，共",
								(double)current4.questions * current4.perscore,
								"分) 本题共得分：",
								current4.score
							});
							this.Writeln(documentBuilder, strText, 14.0, true, "left");
							foreach (ExamQuestion current5 in this.GetQuestionList(current4))
							{
								bool flag15 = current5.userscore > 0.0;
								int status;
								if (flag15)
								{
									status = 1;
								}
								else
								{
									status = 2;
								}
								bool flag16 = current5.type == "TYPE_RADIO" || current5.type == "TYPE_MULTIPLE";
								if (flag16)
								{
									this.OptionImg(documentBuilder, string.Concat(new object[]
									{
										num,
										"、",
										current5.title,
										"  ",
										current5.useranswer
									}), imagelist, true, status);
									string[] array = FPArray.SplitString(current5.content, "§", 8);
									string[] array2 = FPArray.SplitString("A,B,C,D,E,F,G,H");
									int num2 = current5.ascount;
									bool flag17 = num2 > array.Length;
									if (flag17)
									{
										num2 = array.Length;
									}
									for (int i = 0; i < num2; i++)
									{
										this.OptionImg(documentBuilder, array2[i] + "." + array[i], imagelist, false, 0);
									}
									documentBuilder.Font.Color = Color.Red;
									documentBuilder.Writeln("正确答案：" + current5.answer);
									documentBuilder.Writeln("本题得分：" + current5.userscore);
									bool flag18 = current5.explain != "";
									if (flag18)
									{
										this.OptionImg(documentBuilder, "答案解析：" + current5.explain, imagelist, false, 0);
									}
								}
								else
								{
									bool flag19 = current5.type == "TYPE_TRUE_FALSE";
									if (flag19)
									{
										string text = "";
										bool flag20 = current5.useranswer == "Y";
										if (flag20)
										{
											text = "√";
										}
										else
										{
											bool flag21 = current5.useranswer == "N";
											if (flag21)
											{
												text = "×";
											}
										}
										this.OptionImg(documentBuilder, string.Concat(new object[]
										{
											num,
											"、",
											current5.title,
											" ",
											text
										}), imagelist, false, status);
										documentBuilder.Font.Color = Color.Red;
										string str = "";
										bool flag22 = current5.answer == "Y";
										if (flag22)
										{
											str = "√";
										}
										else
										{
											bool flag23 = current5.answer == "N";
											if (flag23)
											{
												str = "×";
											}
										}
										documentBuilder.Writeln("正确答案：" + str);
										documentBuilder.Writeln("本题得分：" + current5.userscore);
										bool flag24 = current5.explain != "";
										if (flag24)
										{
											this.OptionImg(documentBuilder, "答案解析：" + current5.explain, imagelist, false, 0);
										}
									}
									else
									{
										bool flag25 = current5.type == "TYPE_BLANK";
										if (flag25)
										{
											this.WriteAnswer(documentBuilder, num + "、" + current5.title, imagelist, current5.useranswer, status);
											documentBuilder.Font.Color = Color.Red;
											documentBuilder.Writeln("正确答案：" + current5.answer);
											documentBuilder.Writeln("本题得分：" + current5.userscore);
											bool flag26 = current5.explain != "";
											if (flag26)
											{
												this.OptionImg(documentBuilder, "答案解析：" + current5.explain, imagelist, false, 0);
											}
										}
										else
										{
											bool flag27 = current5.type == "TYPE_ANSWER";
											if (flag27)
											{
												this.OptionImg(documentBuilder, num + "、" + current5.title, imagelist, false, 0);
												this.OptionImg(documentBuilder, "答：" + current5.useranswer, imagelist, false, 0);
												this.OptionImg(documentBuilder, "正确答案：" + current5.answer, imagelist, false, 0);
												documentBuilder.Font.Color = Color.Red;
												documentBuilder.Writeln("本题得分：" + current5.userscore);
												bool flag28 = current5.explain != "";
												if (flag28)
												{
													this.OptionImg(documentBuilder, "答案解析：" + current5.explain, imagelist, false, 0);
												}
											}
											else
											{
												bool flag29 = current5.type == "TYPE_OPERATION";
												if (flag29)
												{
													this.OptionImg(documentBuilder, num + "、" + current5.title, imagelist, false, 0);
													this.OptionImg(documentBuilder, "答：" + current5.useranswer, imagelist, false, 0);
													this.OptionImg(documentBuilder, "正确答案：" + current5.answer, imagelist, false, 0);
													documentBuilder.Font.Color = Color.Red;
													documentBuilder.Writeln("本题得分：" + current5.userscore);
													bool flag30 = current5.explain != "";
													if (flag30)
													{
														this.OptionImg(documentBuilder, "答案解析：" + current5.explain, imagelist, false, 0);
													}
												}
											}
										}
									}
								}
								num++;
							}
						}
						document.Range.Bookmarks.Clear();
						ExamConfig examConfig = ExamConifgs.GetExamConfig();
						bool flag31 = examConfig.exporttype == 1;
						if (flag31)
						{
							SaveOptions saveOptions = SaveOptions.CreateSaveOptions(SaveFormat.Doc);
							document.Save(base.Response, this.examresult.title + "_" + this.examresult.realname + ".doc", ContentDisposition.Attachment, saveOptions);
						}
						else
						{
							SaveOptions saveOptions2 = SaveOptions.CreateSaveOptions(SaveFormat.Pdf);
							document.Save(base.Response, this.examresult.title + "_" + this.examresult.realname + ".pdf", ContentDisposition.Attachment, saveOptions2);
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
			string[] array3 = FPArray.SplitString(resultinfo.scorelist, "|", array.Length);
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			int num = 0;
			int[] array4 = array;
			for (int i = 0; i < array4.Length; i++)
			{
				int num2 = array4[i];
				foreach (ExamQuestion current in list)
				{
					bool flag = current.id == num2;
					if (flag)
					{
						current.useranswer = array2[num];
						current.userscore = (double)FPUtils.StrToFloat(array3[num]);
						list2.Add(current);
					}
				}
				num++;
			}
			return list2;
		}

		protected string FmAnswer(string content, int tid, string uanswer)
		{
			string[] array = FPArray.SplitString(content, new string[]
			{
				"(#answer)",
				"___",
				"____",
				"_____",
				"______"
			});
			string[] array2 = FPArray.SplitString(uanswer, ",", array.Length);
			content = "";
			int num = 0;
			string[] array3 = array;
			for (int i = 0; i < array3.Length; i++)
			{
				string str = array3[i];
				bool flag = num < array.Length - 1;
				if (flag)
				{
					content = content + str + string.Format("<input type=\"text\" id=\"answer_{0}\" name=\"answer_{0}\" value=\"{1}\" class=\"tkt\" readonly=\"readonly\"/>", tid, array2[num]);
				}
				else
				{
					content += str;
				}
				num++;
			}
			return content;
		}

		protected string Option(string[] opstr, int ascount)
		{
			string[] array = FPArray.SplitString("A,B,C,D,E,F,G,H");
			string text = "";
			bool flag = ascount > opstr.Length;
			if (flag)
			{
				ascount = opstr.Length;
			}
			for (int i = 0; i < ascount; i++)
			{
				text = string.Concat(new string[]
				{
					text,
					array[i],
					".",
					opstr[i],
					"<br/>"
				});
			}
			return text;
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
				text = "0" + num + ":";
			}
			else
			{
				text = num.ToString() + ":";
			}
			bool flag2 = num2 < 10;
			if (flag2)
			{
				text = text + "0" + num2.ToString() + ":";
			}
			else
			{
				text = text + num2.ToString() + ":";
			}
			bool flag3 = num3 < 10;
			if (flag3)
			{
				text = text + "0" + num3.ToString();
			}
			else
			{
				text += num3.ToString();
			}
			return text;
		}

		protected string CalRate(double myscore, double total)
		{
			return (myscore / total * 100.0).ToString("0.0");
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

		protected void WriteAnswer(DocumentBuilder builder, string content, List<ImgInfo> imagelist, string uanswer, int status)
		{
			string[] array = FPArray.SplitString(content, new string[]
			{
				"(#answer)",
				"___",
				"____",
				"_____",
				"______"
			});
			string[] array2 = FPArray.SplitString(uanswer, ",", array.Length);
			content = "";
			int num = 0;
			string[] array3 = array;
			for (int i = 0; i < array3.Length; i++)
			{
				string content2 = array3[i];
				bool flag = num < array.Length - 1;
				if (flag)
				{
					builder.Font.Underline = Underline.None;
					this.OptionImg(builder, content2, imagelist, false, 0, false);
					builder.Font.Underline = Underline.Single;
					builder.Write(" " + array2[num] + " ");
				}
				else
				{
					builder.Font.Underline = Underline.None;
					this.OptionImg(builder, content2, imagelist, false, status);
				}
				num++;
			}
		}

		protected void OptionImg(DocumentBuilder builder, string content, List<ImgInfo> imagelist, bool bold, int status)
		{
			this.OptionImg(builder, content, imagelist, bold, status, true);
		}

		protected void OptionImg(DocumentBuilder builder, string content, List<ImgInfo> imagelist, bool bold, int status, bool is_writein)
		{
			Regex regex = new Regex("</?(?!img|a|/a|br)[^>]*>", RegexOptions.IgnoreCase);
			content = regex.Replace(content, "");
			builder.Bold = bold;
			builder.Font.Size = 12.0;
			builder.Font.Color = Color.Black;
			HtmlDocument htmlDocument = new HtmlDocument();
			htmlDocument.LoadHtml(content);
			HtmlNodeCollection htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//br");
			bool flag = htmlNodeCollection != null;
			if (flag)
			{
				foreach (HtmlNode current in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					string text = FPRandom.CreateCode(20);
					imagelist.Add(new ImgInfo
					{
						key = text,
						file = "",
						src = ""
					});
					HtmlNode newChild = HtmlNode.CreateNode("<bookmark key=\"" + text + "\"/></bookmark>");
					current.ParentNode.ReplaceChild(newChild, current);
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//a[@href]");
			bool flag2 = htmlNodeCollection != null;
			if (flag2)
			{
				foreach (HtmlNode current2 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					string value = current2.Attributes["href"].Value;
					bool flag3 = !string.IsNullOrEmpty(value);
					if (flag3)
					{
						bool flag4 = value.StartsWith("/") && File.Exists(FPFile.GetMapPath(value));
						if (flag4)
						{
							string text2 = FPRandom.CreateCode(20);
							HtmlNodeCollection htmlNodeCollection2 = current2.SelectNodes("//img[@src]");
							ImgInfo imgInfo = new ImgInfo();
							imgInfo.key = text2;
							imgInfo.file = value;
							bool flag5 = htmlNodeCollection2 != null;
							if (flag5)
							{
								imgInfo.src = htmlNodeCollection2[0].Attributes["src"].Value;
							}
							else
							{
								imgInfo.src = this.GetTxtImg(current2.InnerText, text2);
							}
							imagelist.Add(imgInfo);
							HtmlNode newChild2 = HtmlNode.CreateNode("<bookmark key=\"" + text2 + "\"/></bookmark>");
							current2.ParentNode.ReplaceChild(newChild2, current2);
						}
					}
					else
					{
						HtmlNode newChild3 = HtmlNode.CreateNode(current2.InnerText);
						current2.ParentNode.ReplaceChild(newChild3, current2);
					}
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//img[@src]");
			bool flag6 = htmlNodeCollection != null;
			if (flag6)
			{
				foreach (HtmlNode current3 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					string text3 = FPRandom.CreateCode(20);
					ImgInfo imgInfo2 = new ImgInfo();
					imgInfo2.key = text3;
					imgInfo2.src = current3.Attributes["src"].Value;
					bool flag7 = current3.Attributes["width"] != null;
					if (flag7)
					{
						imgInfo2.width = FPUtils.StrToDouble(current3.Attributes["width"].Value);
					}
					bool flag8 = current3.Attributes["height"] != null;
					if (flag8)
					{
						imgInfo2.height = FPUtils.StrToDouble(current3.Attributes["height"].Value);
					}
					imagelist.Add(imgInfo2);
					HtmlNode newChild4 = HtmlNode.CreateNode("<bookmark key=\"" + text3 + "\"></bookmark>");
					current3.ParentNode.ReplaceChild(newChild4, current3);
				}
			}
			htmlNodeCollection = htmlDocument.DocumentNode.SelectNodes("//a[@name]");
			bool flag9 = htmlNodeCollection != null;
			if (flag9)
			{
				foreach (HtmlNode current4 in ((IEnumerable<HtmlNode>)htmlNodeCollection))
				{
					current4.Remove();
				}
			}
			string[] array = FPArray.SplitString(htmlDocument.DocumentNode.InnerHtml, "</bookmark>");
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text4 = array2[i];
				HtmlDocument htmlDocument2 = new HtmlDocument();
				htmlDocument2.LoadHtml(text4);
				HtmlNodeCollection htmlNodeCollection3 = htmlDocument2.DocumentNode.SelectNodes("//bookmark[@key]");
				bool flag10 = htmlNodeCollection3 != null;
				if (flag10)
				{
					string text5 = "";
					foreach (HtmlNode current5 in ((IEnumerable<HtmlNode>)htmlNodeCollection3))
					{
						text5 = current5.Attributes["key"].Value;
						current5.Remove();
					}
					builder.Write(htmlDocument2.DocumentNode.InnerHtml);
					bool flag11 = text5 != "";
					if (flag11)
					{
						builder.StartBookmark(text5);
						builder.EndBookmark(text5);
					}
				}
				else
				{
					builder.Write(text4);
				}
			}
			bool flag12 = status == 1;
			if (flag12)
			{
				builder.InsertImage(FPFile.GetMapPath(this.webpath + "exam/admin/images/status-right.png"), 16.0, 16.0);
			}
			else
			{
				bool flag13 = status == 2;
				if (flag13)
				{
					builder.InsertImage(FPFile.GetMapPath(this.webpath + "exam/admin/images/status-wrong.png"), 16.0, 16.0);
				}
			}
			if (is_writein)
			{
				builder.Writeln();
			}
		}

		public bool InsertTable(DocumentBuilder builder, DataTable dt, bool haveBorder)
		{
			Table table = builder.StartTable();
			ParagraphAlignment alignment = builder.ParagraphFormat.Alignment;
			builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				builder.RowFormat.Height = 25.0;
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					builder.InsertCell();
					builder.Font.Size = 10.5;
					builder.Font.Name = "宋体";
					builder.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
					builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
					builder.CellFormat.Width = 60.0;
					builder.CellFormat.PreferredWidth = PreferredWidth.FromPoints(50.0);
					if (haveBorder)
					{
						builder.CellFormat.Borders.LineStyle = LineStyle.Single;
					}
					builder.Write(dt.Rows[i][j].ToString());
				}
				builder.EndRow();
			}
			builder.EndTable();
			builder.ParagraphFormat.Alignment = alignment;
			table.Alignment = TableAlignment.Center;
			table.PreferredWidth = PreferredWidth.Auto;
			table.SetBorder(BorderType.Left, LineStyle.Single, 2.0, Color.Black, true);
			table.SetBorder(BorderType.Right, LineStyle.Single, 2.0, Color.Black, true);
			table.SetBorder(BorderType.Top, LineStyle.Single, 2.0, Color.Black, true);
			table.SetBorder(BorderType.Bottom, LineStyle.Single, 2.0, Color.Black, true);
			return true;
		}

		public void Write(DocumentBuilder builder, string strText, double conSize, bool conBold, string conAlign)
		{
			builder.Bold = conBold;
			builder.Font.Size = conSize;
			if (!(conAlign == "left"))
			{
				if (!(conAlign == "center"))
				{
					if (!(conAlign == "right"))
					{
						builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
					}
					else
					{
						builder.ParagraphFormat.Alignment = ParagraphAlignment.Right;
					}
				}
				else
				{
					builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
				}
			}
			else
			{
				builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
			}
			builder.Write(strText);
		}

		public void Writeln(DocumentBuilder builder, string strText, double conSize, bool conBold, string conAlign)
		{
			builder.Bold = conBold;
			builder.Font.Size = conSize;
			if (!(conAlign == "left"))
			{
				if (!(conAlign == "center"))
				{
					if (!(conAlign == "right"))
					{
						builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
					}
					else
					{
						builder.ParagraphFormat.Alignment = ParagraphAlignment.Right;
					}
				}
				else
				{
					builder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
				}
			}
			else
			{
				builder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
			}
			builder.Writeln(strText);
		}

		public void InsertLineBreak(DocumentBuilder builder)
		{
			builder.Writeln();
		}

		public void InsertLineBreak(DocumentBuilder builder, int nline)
		{
			for (int i = 0; i < nline; i++)
			{
				builder.Writeln();
			}
		}

		protected string GetNum(int num)
		{
			string result;
			switch (num)
			{
			case 1:
				result = "一";
				break;
			case 2:
				result = "二";
				break;
			case 3:
				result = "三";
				break;
			case 4:
				result = "四";
				break;
			case 5:
				result = "五";
				break;
			case 6:
				result = "六";
				break;
			case 7:
				result = "七";
				break;
			case 8:
				result = "八";
				break;
			case 9:
				result = "九";
				break;
			case 10:
				result = "十";
				break;
			default:
				result = "";
				break;
			}
			return result;
		}

		protected string GetTxtImg(string txt, string qid)
		{
			string text = string.Concat(new string[]
			{
				this.webpath,
				"cache/",
				Path.GetFileNameWithoutExtension(txt),
				"_",
				qid,
				".jpg"
			});
			bool flag = File.Exists(FPFile.GetMapPath(text));
			if (flag)
			{
				File.Delete(FPFile.GetMapPath(text));
			}
			Bitmap bitmap = new Bitmap(1, 1);
			Graphics graphics = Graphics.FromImage(bitmap);
			System.Drawing.Font font = new System.Drawing.Font("宋体", 11f);
			StringFormat stringFormat = new StringFormat(StringFormatFlags.NoClip);
			SizeF sizeF = graphics.MeasureString(txt, font, PointF.Empty, stringFormat);
			int width = (int)(sizeF.Width + 1f);
			int height = (int)(sizeF.Height + 1f);
			Rectangle r = new Rectangle(0, 0, width, height);
			bitmap.Dispose();
			bitmap = new Bitmap(width, height);
			graphics = Graphics.FromImage(bitmap);
			graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
			graphics.Clear(Color.White);
			graphics.DrawString(txt, font, Brushes.Black, r, stringFormat);
			bitmap.Save(FPFile.GetMapPath(text), ImageFormat.Jpeg);
			return text;
		}
	}
}
