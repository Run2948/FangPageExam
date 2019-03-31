using Aspose.Words;
using Aspose.Words.Drawing;
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
	public class outputpaper : AdminController
	{
		protected ChannelInfo channelinfo = new ChannelInfo();

		protected int examid = FPRequest.GetInt("examid");

		protected int paper = FPRequest.GetInt("paper");

		protected ExamInfo examinfo = new ExamInfo();

		protected List<ExamTopic> examtopiclist = new List<ExamTopic>();

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

		protected string papersize = FPRequest.GetString("papersize");

		protected int papertype = FPRequest.GetInt("papertype");

		protected override void Controller()
		{
			this.channelinfo = ChannelBll.GetChannelInfo("question");
			bool flag = this.channelinfo.id == 0;
			if (flag)
			{
				this.ShowErr("对不起，目前系统尚未创建题目库频道。");
			}
			else
			{
				bool ispost = this.ispost;
				if (ispost)
				{
					this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examid);
					this.examinfo.passmark = this.examinfo.passmark * this.examinfo.total / 100.0;
					this.examtopiclist = ExamBll.GetExamTopicList(this.examid, this.paper);
					int num = 0;
					for (int i = 0; i < this.examtopiclist.Count; i++)
					{
						this.examtopiclist[i].questionlist = QuestionBll.GetTopicQuestion(this.channelinfo.id, this.examtopiclist[i]);
						string[] array = FPArray.SplitString(this.examtopiclist[i].questionlist);
						bool flag2 = this.examinfo.display == 0;
						if (flag2)
						{
							this.examtopiclist[i].questionlist = QuestionBll.GetRandom(array, array.Length);
						}
						num += array.Length;
						this.examtopiclist[i].questions = array.Length;
					}
					this.examinfo.questions = num;
					bool flag3 = this.papersize == "a4";
					Document document;
					if (flag3)
					{
						document = new Document(FPFile.GetMapPath(this.webpath + "exam/admin/template/exampaper_a4.doc"));
					}
					else
					{
						document = new Document(FPFile.GetMapPath(this.webpath + "exam/admin/template/exampaper_a3.doc"));
					}
					DocumentBuilder documentBuilder = new DocumentBuilder(document);
					bool flag4 = document.Range.Bookmarks["examtitle"] != null;
					if (flag4)
					{
						Bookmark bookmark = document.Range.Bookmarks["examtitle"];
						bookmark.Text = this.examinfo.title;
					}
					bool flag5 = document.Range.Bookmarks["subtitle"] != null;
					if (flag5)
					{
						Bookmark bookmark2 = document.Range.Bookmarks["subtitle"];
						bookmark2.Text = this.GetPaper(this.paper);
					}
					documentBuilder.MoveToBookmark("content");
					DataTable dataTable = new DataTable();
					DataColumn column = new DataColumn("s0", Type.GetType("System.String"));
					dataTable.Columns.Add(column);
					int num2 = 1;
					foreach (ExamTopic current in this.examtopiclist)
					{
						column = new DataColumn("s" + num2, Type.GetType("System.String"));
						dataTable.Columns.Add(column);
						num2++;
					}
					column = new DataColumn("s" + num2, Type.GetType("System.String"));
					dataTable.Columns.Add(column);
					DataRow dataRow = dataTable.NewRow();
					dataRow["s0"] = "题  号";
					num2 = 1;
					foreach (ExamTopic current2 in this.examtopiclist)
					{
						dataRow["s" + num2] = this.GetNum(num2);
						num2++;
					}
					dataRow["s" + num2] = "总  分";
					dataTable.Rows.Add(dataRow);
					dataRow = dataTable.NewRow();
					dataRow["s0"] = "得  分";
					num2 = 1;
					foreach (ExamTopic current3 in this.examtopiclist)
					{
						dataRow["s" + num2] = "";
						num2++;
					}
					dataRow["s" + num2] = "";
					dataTable.Rows.Add(dataRow);
					this.InsertTable(documentBuilder, dataTable, true);
					num2 = 1;
					Dictionary<int, List<ExamQuestion>> dictionary = new Dictionary<int, List<ExamQuestion>>();
					List<ImgInfo> list = new List<ImgInfo>();
					foreach (ExamTopic current4 in this.examtopiclist)
					{
						documentBuilder.Writeln();
						this.Writeln(documentBuilder, string.Concat(new object[]
						{
							current4.title,
							"  (共",
							current4.questions,
							"题，每题",
							current4.perscore,
							"分，共",
							(double)current4.questions * current4.perscore,
							"分)"
						}), 14.0, true, "left");
						dictionary.Add(current4.id, this.GetQuestionList(current4.questionlist));
						foreach (ExamQuestion current5 in dictionary[current4.id])
						{
							bool flag6 = current5.type == "TYPE_RADIO" || current5.type == "TYPE_MULTIPLE";
							if (flag6)
							{
								bool flag7 = this.papertype == 1;
								if (flag7)
								{
									this.OptionImg(documentBuilder, string.Concat(new object[]
									{
										num2,
										"、",
										current5.title,
										"  ",
										current5.answer
									}), list, true);
									string[] array2 = FPArray.SplitString(current5.content, "§", 8);
									string[] array3 = FPArray.SplitString("A,B,C,D,E,F,G,H");
									int num3 = current5.ascount;
									bool flag8 = num3 > array2.Length;
									if (flag8)
									{
										num3 = array2.Length;
									}
									for (int j = 0; j < num3; j++)
									{
										this.OptionImg(documentBuilder, array3[j] + "." + array2[j], list, false);
									}
									bool flag9 = current5.explain != "";
									if (flag9)
									{
										this.OptionImg(documentBuilder, "答案解析：" + current5.explain, list, false);
									}
								}
								else
								{
									this.OptionImg(documentBuilder, num2 + "、" + current5.title, list, true);
									string[] array4 = FPArray.SplitString(current5.content, "§", 8);
									string[] array5 = FPArray.SplitString("A,B,C,D,E,F,G,H");
									int num4 = current5.ascount;
									bool flag10 = num4 > array4.Length;
									if (flag10)
									{
										num4 = array4.Length;
									}
									for (int k = 0; k < num4; k++)
									{
										this.OptionImg(documentBuilder, array5[k] + "." + array4[k], list, false);
									}
								}
							}
							else
							{
								bool flag11 = current5.type == "TYPE_TRUE_FALSE";
								if (flag11)
								{
									bool flag12 = this.papertype == 1;
									if (flag12)
									{
										string text = "";
										bool flag13 = current5.answer == "Y";
										if (flag13)
										{
											text = "√";
										}
										else
										{
											bool flag14 = current5.answer == "N";
											if (flag14)
											{
												text = "×";
											}
										}
										this.OptionImg(documentBuilder, string.Concat(new object[]
										{
											num2,
											"、",
											current5.title,
											"  (",
											text,
											")"
										}), list, false);
										bool flag15 = current5.explain != "";
										if (flag15)
										{
											this.OptionImg(documentBuilder, "答案解析：" + current5.explain, list, false);
										}
									}
									else
									{
										this.OptionImg(documentBuilder, string.Concat(new object[]
										{
											num2,
											"、",
											current5.title,
											"  (    )"
										}), list, false);
									}
								}
								else
								{
									bool flag16 = current5.type == "TYPE_BLANK";
									if (flag16)
									{
										bool flag17 = this.papertype == 1;
										if (flag17)
										{
											this.OptionImg(documentBuilder, num2 + "、" + this.FmAnswer(current5.title), list, false);
											this.Writeln(documentBuilder, "答案：" + current5.answer, 12.0, false, "left");
											bool flag18 = current5.explain != "";
											if (flag18)
											{
												this.OptionImg(documentBuilder, "答案解析：" + current5.explain, list, false);
											}
										}
										else
										{
											this.OptionImg(documentBuilder, num2 + "、" + this.FmAnswer(current5.title), list, false);
										}
									}
									else
									{
										bool flag19 = current5.type == "TYPE_ANSWER";
										if (flag19)
										{
											bool flag20 = this.papertype == 1;
											if (flag20)
											{
												this.OptionImg(documentBuilder, num2 + "、" + current5.title, list, false);
												this.OptionImg(documentBuilder, "答：" + current5.answer, list, false);
												bool flag21 = current5.explain != "";
												if (flag21)
												{
													this.OptionImg(documentBuilder, "答案解析：" + current5.explain, list, false);
												}
											}
											else
											{
												this.OptionImg(documentBuilder, num2 + "、" + current5.title, list, true);
												this.InsertLineBreak(documentBuilder, 6);
											}
										}
										else
										{
											bool flag22 = current5.type == "TYPE_OPERATION";
											if (flag22)
											{
												bool flag23 = this.papertype == 1;
												if (flag23)
												{
													this.OptionImg(documentBuilder, num2 + "、" + current5.title, list, false);
													this.OptionImg(documentBuilder, "答：" + current5.answer, list, false);
													bool flag24 = current5.explain != "";
													if (flag24)
													{
														this.OptionImg(documentBuilder, "答案解析：" + current5.explain, list, false);
													}
												}
												else
												{
													this.OptionImg(documentBuilder, num2 + "、" + current5.title, list, false);
													this.InsertLineBreak(documentBuilder, 6);
												}
											}
										}
									}
								}
							}
							num2++;
						}
					}
					bool flag25 = this.papertype == 0;
					if (flag25)
					{
						documentBuilder.InsertBreak(BreakType.PageBreak);
						this.Writeln(documentBuilder, "参考答案", 14.0, true, "center");
						num2 = 1;
						foreach (ExamTopic current6 in this.examtopiclist)
						{
							this.InsertLineBreak(documentBuilder);
							this.Writeln(documentBuilder, current6.title, 12.0, true, "left");
							foreach (ExamQuestion current7 in dictionary[current6.id])
							{
								bool flag26 = current7.type == "TYPE_RADIO" || current7.type == "TYPE_MULTIPLE";
								if (flag26)
								{
									this.Write(documentBuilder, string.Concat(new object[]
									{
										num2,
										"、",
										current7.answer,
										"  "
									}), 12.0, false, "left");
								}
								else
								{
									bool flag27 = current7.type == "TYPE_TRUE_FALSE";
									if (flag27)
									{
										string text2 = "";
										bool flag28 = current7.answer == "Y";
										if (flag28)
										{
											text2 = "√";
										}
										else
										{
											bool flag29 = current7.answer == "N";
											if (flag29)
											{
												text2 = "×";
											}
										}
										this.Write(documentBuilder, string.Concat(new object[]
										{
											num2,
											"、",
											text2,
											"  "
										}), 12.0, false, "left");
									}
									else
									{
										bool flag30 = current7.type == "TYPE_ANSWER" || current7.type == "TYPE_OPERATION";
										if (flag30)
										{
											this.OptionImg(documentBuilder, num2 + "、" + current7.answer, list, false);
										}
										else
										{
											this.Write(documentBuilder, string.Concat(new object[]
											{
												num2,
												"、",
												current7.answer,
												"  "
											}), 12.0, false, "left");
										}
									}
								}
								num2++;
							}
						}
					}
					foreach (ImgInfo current8 in list)
					{
						documentBuilder.MoveToBookmark(current8.key);
						bool flag31 = current8.file == "" && current8.src == "";
						if (flag31)
						{
							documentBuilder.Writeln();
						}
						else
						{
							string mapPath = FPFile.GetMapPath(current8.src);
							bool flag32 = !File.Exists(mapPath);
							if (!flag32)
							{
								bool flag33 = current8.file != "";
								if (flag33)
								{
									bool flag34 = File.Exists(FPFile.GetMapPath(current8.file));
									if (flag34)
									{
										Bitmap presentation = new Bitmap(mapPath);
										Shape shape = documentBuilder.InsertOleObject(FPFile.GetMapPath(current8.file), false, true, presentation);
										shape.VerticalAlignment = VerticalAlignment.Center;
									}
								}
								else
								{
									bool flag35 = current8.width > 0.0 && current8.height > 0.0;
									if (flag35)
									{
										documentBuilder.InsertImage(mapPath, current8.width, current8.height);
									}
									else
									{
										documentBuilder.InsertImage(mapPath);
									}
								}
							}
						}
					}
					document.Range.Bookmarks.Clear();
					SaveOptions saveOptions = SaveOptions.CreateSaveOptions(SaveFormat.Doc);
					document.Save(base.Response, this.examinfo.title + this.GetPaper(this.paper) + ".doc", ContentDisposition.Attachment, saveOptions);
				}
			}
		}

		protected List<ExamQuestion> GetQuestionList(string questionids)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, questionids);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			int[] array = FPArray.SplitInt(questionids);
			for (int i = 0; i < array.Length; i++)
			{
				int num = array[i];
				foreach (ExamQuestion current in list)
				{
					bool flag = current.id == num;
					if (flag)
					{
						list2.Add(current);
					}
				}
			}
			return list2;
		}

		protected string FmAnswer(string content)
		{
			return content.Replace("(#answer)", "__________");
		}

		protected void OptionImg(DocumentBuilder builder, string content, List<ImgInfo> imagelist, bool bold)
		{
			Regex regex = new Regex("</?(?!img|a|/a|br)[^>]*>", RegexOptions.IgnoreCase);
			content = regex.Replace(content, "");
			builder.Bold = bold;
			builder.Font.Size = 12.0;
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
			builder.Writeln();
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
