using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Saving;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using HtmlAgilityPack;
using NPOI.HSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace FP_Exam.Controller
{
	public class questionmanage : AdminController
	{
		protected int channelid = FPRequest.GetInt("channelid");

		protected ChannelInfo channelinfo = new ChannelInfo();

		protected int sortid = FPRequest.GetInt("sortid");

		protected SortInfo sortinfo = new SortInfo();

		protected int typeid = FPRequest.GetInt("typeid");

		protected string type = FPRequest.GetString("type");

		protected Pager pager = FPRequest.GetModel<Pager>();

		protected List<ExamQuestion> questionlist = new List<ExamQuestion>();

		protected string keyword = FPRequest.GetString("keyword");

		protected ExamConfig examconfig = new ExamConfig();

		protected override void Controller()
		{
			this.examconfig = ExamConifgs.GetExamConfig();
			this.channelinfo = ChannelBll.GetChannelInfo(this.channelid);
			bool flag = this.channelinfo.id == 0 && this.channelid > 0;
			if (flag)
			{
				this.ShowErr("对不起，该频道不存在或已被删除。");
			}
			else
			{
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				bool flag2 = this.sortinfo.id == 0 && this.sortid > 0;
				if (flag2)
				{
					this.ShowErr("对不起，该题库已被删除或不存在。");
				}
				else
				{
					bool flag3 = this.channelid == 0 && this.sortinfo.id > 0;
					if (flag3)
					{
						this.channelid = this.sortinfo.channelid;
					}
					bool flag4 = this.sortinfo.id == 0;
					if (flag4)
					{
						this.sortinfo.name = this.channelinfo.name;
					}
					List<SqlParam> list = new List<SqlParam>();
					list.Add(DbHelper.MakeAndWhere("parentid", WhereType.IsNullOrEmpty));
					bool flag5 = this.channelid > 0;
					if (flag5)
					{
						list.Add(DbHelper.MakeAndWhere("channelid", this.channelid));
					}
					bool flag6 = this.sortid > 0;
					if (flag6)
					{
						string childSorts = SortBll.GetChildSorts(this.sortinfo);
						list.Add(DbHelper.MakeAndWhere("sortid", WhereType.In, childSorts));
					}
					bool flag7 = this.typeid > 0;
					if (flag7)
					{
						list.Add(DbHelper.MakeAndWhere("typelist", WhereType.Contain, this.typeid));
					}
					bool flag8 = this.type != "";
					if (flag8)
					{
						list.Add(DbHelper.MakeAndWhere("type", this.type));
					}
					bool flag9 = this.keyword != "";
					if (flag9)
					{
						list.Add(DbHelper.MakeAndWhere("title", WhereType.Like, this.keyword));
					}
					bool ispost = this.ispost;
					if (ispost)
					{
						bool flag10 = this.action == "delete";
						if (flag10)
						{
							string @string = FPRequest.GetString("chkid");
							DbHelper.ExecuteDelete<ExamQuestion>(@string);
							QuestionBll.UpdateQuestionPost(@string);
							TypeBll.ResetTypePosts<ExamQuestion>(this.sortinfo.types, this.sortid);
							QuestionBll.UpdateSortQuestion(this.channelid, this.sortid);
							bool flag11 = this.sortinfo.types != "";
							if (flag11)
							{
								QuestionBll.UpdateSortQuestion(this.channelid, this.sortid, this.sortinfo.types);
							}
						}
						else
						{
							bool flag12 = this.action == "clear";
							if (flag12)
							{
								DbHelper.ExecuteDelete<ExamQuestion>(list.ToArray());
								SortBll.ResetSortPosts<ExamQuestion>(this.sortid);
								TypeBll.ResetTypePosts<ExamQuestion>(this.sortinfo.types, this.sortid);
								QuestionBll.UpdateSortQuestion(this.channelid, this.sortid);
								bool flag13 = this.sortinfo.types != "";
								if (flag13)
								{
									QuestionBll.UpdateSortQuestion(this.channelid, this.sortid, this.sortinfo.types);
								}
							}
							else
							{
								bool flag14 = this.action == "move";
								if (flag14)
								{
									string string2 = FPRequest.GetString("chkid");
									bool flag15 = string2 == "";
									if (flag15)
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
										string2
									}));
								}
								else
								{
									bool flag16 = this.action == "export";
									if (flag16)
									{
										string string3 = FPRequest.GetString("chkid");
										bool flag17 = string3 != "";
										if (flag17)
										{
											list = new List<SqlParam>();
											list.Add(DbHelper.MakeAndWhere("id", WhereType.In, string3));
										}
										List<ExamQuestion> list2 = DbHelper.ExecuteList<ExamQuestion>(list.ToArray());
										List<ImgInfo> list3 = new List<ImgInfo>();
										bool flag18 = FPRequest.GetString("extype") == "excel";
										if (flag18)
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
											hSSFRow.CreateCell(0).SetCellValue("题目类型");
											hSSFRow.CreateCell(1).SetCellValue("题目标题");
											hSSFRow.CreateCell(2).SetCellValue("选项A");
											hSSFRow.CreateCell(3).SetCellValue("选项B");
											hSSFRow.CreateCell(4).SetCellValue("选项C");
											hSSFRow.CreateCell(5).SetCellValue("选项D");
											hSSFRow.CreateCell(6).SetCellValue("选项E");
											hSSFRow.CreateCell(7).SetCellValue("选项F");
											hSSFRow.CreateCell(8).SetCellValue("选项G");
											hSSFRow.CreateCell(9).SetCellValue("选项H");
											hSSFRow.CreateCell(10).SetCellValue("正确答案");
											hSSFRow.CreateCell(11).SetCellValue("答案关键词");
											hSSFRow.CreateCell(12).SetCellValue("答案解释");
											hSSFRow.CreateCell(13).SetCellValue("难易程度");
											hSSFRow.CreateCell(14).SetCellValue("");
											hSSFRow.Height = 400;
											hSSFSheet.SetColumnWidth(1, 6000);
											for (int i = 0; i < 14; i++)
											{
												hSSFRow.Cells[i].CellStyle = hSSFCellStyle;
											}
											HSSFCellStyle hSSFCellStyle2 = hSSFWorkbook.CreateCellStyle();
											hSSFCellStyle2.VerticalAlignment = CellVerticalAlignment.CENTER;
											hSSFCellStyle2.BorderTop = CellBorderType.THIN;
											hSSFCellStyle2.BorderRight = CellBorderType.THIN;
											hSSFCellStyle2.BorderLeft = CellBorderType.THIN;
											hSSFCellStyle2.BorderBottom = CellBorderType.THIN;
											hSSFCellStyle2.DataFormat = 0;
											int num = 1;
											foreach (ExamQuestion current in list2)
											{
												HSSFRow hSSFRow2 = hSSFSheet.CreateRow(num);
												hSSFRow2.Height = 300;
												hSSFRow2.CreateCell(0).SetCellValue(current.typestr);
												hSSFRow2.CreateCell(1).SetCellValue(current.title.Trim());
												bool flag19 = current.type == "TYPE_RADIO" || current.type == "TYPE_MULTIPLE";
												if (flag19)
												{
													string[] array = FPArray.SplitString(current.content, "§", 8);
													int num2 = 0;
													string[] array2 = array;
													for (int j = 0; j < array2.Length; j++)
													{
														string cellValue = array2[j];
														hSSFRow2.CreateCell(2 + num2).SetCellValue(cellValue);
														num2++;
													}
												}
												else
												{
													bool flag20 = current.type == "TYPE_BLANK";
													if (flag20)
													{
														bool flag21 = current.upperflg == 1;
														if (flag21)
														{
															hSSFRow2.CreateCell(2).SetCellValue("区分大小写");
														}
														else
														{
															hSSFRow2.CreateCell(2).SetCellValue("");
														}
														bool flag22 = current.orderflg == 1;
														if (flag22)
														{
															hSSFRow2.CreateCell(3).SetCellValue("区分顺序");
														}
														else
														{
															hSSFRow2.CreateCell(3).SetCellValue("");
														}
														hSSFRow2.CreateCell(4).SetCellValue("");
														hSSFRow2.CreateCell(5).SetCellValue("");
														hSSFRow2.CreateCell(6).SetCellValue("");
														hSSFRow2.CreateCell(7).SetCellValue("");
														hSSFRow2.CreateCell(8).SetCellValue("");
														hSSFRow2.CreateCell(9).SetCellValue("");
													}
													else
													{
														hSSFRow2.CreateCell(2).SetCellValue(current.content.Trim());
														hSSFRow2.CreateCell(3).SetCellValue("");
														hSSFRow2.CreateCell(4).SetCellValue("");
														hSSFRow2.CreateCell(5).SetCellValue("");
														hSSFRow2.CreateCell(6).SetCellValue("");
														hSSFRow2.CreateCell(7).SetCellValue("");
														hSSFRow2.CreateCell(8).SetCellValue("");
														hSSFRow2.CreateCell(9).SetCellValue("");
													}
												}
												bool flag23 = current.type == "TYPE_TRUE_FALSE";
												if (flag23)
												{
													string cellValue2 = "";
													bool flag24 = current.answer.Trim() == "Y";
													if (flag24)
													{
														cellValue2 = "正确";
													}
													else
													{
														bool flag25 = current.answer.Trim() == "N";
														if (flag25)
														{
															cellValue2 = "错误";
														}
													}
													hSSFRow2.CreateCell(10).SetCellValue(cellValue2);
												}
												else
												{
													hSSFRow2.CreateCell(10).SetCellValue(current.answer.Trim());
												}
												hSSFRow2.CreateCell(11).SetCellValue(current.answerkey.Trim());
												hSSFRow2.CreateCell(12).SetCellValue(current.explain.Trim());
												hSSFRow2.CreateCell(13).SetCellValue(current.diffstr);
												hSSFRow2.CreateCell(14).SetCellValue("");
												for (int k = 0; k < 14; k++)
												{
													bool flag26 = k == 0 || k == 11;
													if (flag26)
													{
														hSSFCellStyle2.Alignment = CellHorizontalAlignment.CENTER;
													}
													else
													{
														hSSFCellStyle2.Alignment = CellHorizontalAlignment.LEFT;
													}
													hSSFRow2.Cells[k].CellStyle = hSSFCellStyle2;
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
												base.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(this.sortinfo.name + "题库.xls"));
												base.Response.BinaryWrite(memoryStream.GetBuffer());
												base.Response.Flush();
												base.Response.End();
											}
										}
										else
										{
											Document document = new Document();
											DocumentBuilder documentBuilder = new DocumentBuilder(document);
											documentBuilder.Bold = true;
											documentBuilder.Font.Size = 18.0;
											documentBuilder.ParagraphFormat.LineSpacing = 15.0;
											documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Center;
											documentBuilder.Writeln(this.sortinfo.name);
											documentBuilder.InsertBreak(BreakType.LineBreak);
											documentBuilder.Bold = false;
											documentBuilder.Font.Size = 11.0;
											documentBuilder.ParagraphFormat.Alignment = ParagraphAlignment.Left;
											int num3 = 1;
											foreach (ExamQuestion current2 in list2)
											{
												string content = string.Concat(new string[]
												{
													num3.ToString(),
													"、[",
													current2.typestr,
													"]",
													current2.title
												});
												content = this.FmAnswer(content);
												this.OptionImg(documentBuilder, content, list3);
												bool flag27 = current2.type == "TYPE_RADIO" || current2.type == "TYPE_MULTIPLE";
												if (flag27)
												{
													string[] array3 = FPArray.SplitString(current2.content, "§", 8);
													string[] array4 = FPArray.SplitString("A,B,C,D,E,F,G,H");
													int num4 = current2.ascount;
													bool flag28 = num4 > array3.Length;
													if (flag28)
													{
														num4 = array3.Length;
													}
													for (int l = 0; l < num4; l++)
													{
														this.OptionImg(documentBuilder, array4[l] + "." + array3[l].Trim(), list3);
													}
												}
												else
												{
													bool flag29 = current2.type == "TYPE_BLANK";
													if (flag29)
													{
														bool flag30 = current2.upperflg == 1;
														if (flag30)
														{
															documentBuilder.Writeln("[区分大小写]");
														}
														bool flag31 = current2.orderflg == 1;
														if (flag31)
														{
															documentBuilder.Writeln("[区分顺序]");
														}
													}
												}
												bool flag32 = current2.typestr != "TYPE_COMPREHEND";
												if (flag32)
												{
													bool flag33 = current2.type == "TYPE_TRUE_FALSE" && current2.answer != "";
													if (flag33)
													{
														string str = "";
														bool flag34 = current2.answer.Trim() == "Y";
														if (flag34)
														{
															str = "正确";
														}
														else
														{
															bool flag35 = current2.answer.Trim() == "N";
															if (flag35)
															{
																str = "错误";
															}
														}
														documentBuilder.Writeln("[参考答案]" + str);
													}
													else
													{
														bool flag36 = current2.type == "TYPE_ANSWER" || current2.type == "TYPE_OPERATION";
														if (flag36)
														{
															this.OptionImg(documentBuilder, "[参考答案]" + current2.explain, list3);
														}
														else
														{
															documentBuilder.Writeln("[参考答案]" + current2.answer);
														}
													}
													this.OptionImg(documentBuilder, "[答案解析]" + current2.explain, list3);
												}
												documentBuilder.Writeln("[难易程度]" + current2.diffstr);
												documentBuilder.Writeln();
												num3++;
											}
											foreach (ImgInfo current3 in list3)
											{
												documentBuilder.MoveToBookmark(current3.key);
												bool flag37 = current3.file == "" && current3.src == "";
												if (flag37)
												{
													documentBuilder.Writeln();
												}
												else
												{
													string mapPath = FPFile.GetMapPath(current3.src);
													bool flag38 = !File.Exists(mapPath);
													if (!flag38)
													{
														bool flag39 = current3.file != "";
														if (flag39)
														{
															bool flag40 = File.Exists(FPFile.GetMapPath(current3.file));
															if (flag40)
															{
																Bitmap presentation = new Bitmap(mapPath);
																Shape shape = documentBuilder.InsertOleObject(FPFile.GetMapPath(current3.file), false, true, presentation);
																shape.VerticalAlignment = VerticalAlignment.Center;
															}
														}
														else
														{
															bool flag41 = current3.width > 0.0 && current3.height > 0.0;
															if (flag41)
															{
																documentBuilder.InsertImage(mapPath, current3.width, current3.height);
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
											document.Save(base.Response, this.sortinfo.name + "题库.doc", ContentDisposition.Attachment, saveOptions);
										}
									}
								}
							}
						}
					}
					bool flag42 = this.action == "reset" && this.sortid == 0;
					if (flag42)
					{
						QuestionBll.ResetQuestionSort();
						FPCache.Insert("ExamQuestionReset", "已重置题库统计。");
					}
					this.questionlist = DbHelper.ExecuteList<ExamQuestion>(this.pager, list.ToArray());
				}
			}
		}

		protected string FmAnswer(string content)
		{
			return content.Replace("(#answer)", "______");
		}

		protected void OptionImg(DocumentBuilder builder, string content, List<ImgInfo> imagelist)
		{
			Regex regex = new Regex("</?(?!img|a|/a|br)[^>]*>", RegexOptions.IgnoreCase);
			content = regex.Replace(content, "");
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
