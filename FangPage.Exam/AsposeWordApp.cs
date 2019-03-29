using System;
using System.Data;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Web;
using Aspose.Words;
using Aspose.Words.Saving;
using Aspose.Words.Tables;

namespace FangPage.Exam
{
	// Token: 0x02000002 RID: 2
	public class AsposeWordApp
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public int Docversion
		{
			get
			{
				return this._docversion;
			}
			set
			{
				this._docversion = value;
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002072 File Offset: 0x00000272
		public void Open()
		{
			this.oDoc = new Document();
			this.oWordApplic = new DocumentBuilder(this.oDoc);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002094 File Offset: 0x00000294
		public void Open(string strFileName)
		{
			if (!string.IsNullOrEmpty(strFileName))
			{
				this.oDoc = new Document(strFileName);
				this.oWordApplic = new DocumentBuilder(this.oDoc);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020CC File Offset: 0x000002CC
		public void Save(string strFileName)
		{
			if (this.Docversion == 2007)
			{
				this.oDoc.Save(strFileName, SaveFormat.Docx);
			}
			else
			{
				this.oDoc.Save(strFileName, SaveFormat.Doc);
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002114 File Offset: 0x00000314
		public void Save(HttpResponse response, string fileName)
		{
			SaveOptions saveOptions = SaveOptions.CreateSaveOptions(SaveFormat.Doc);
			this.oDoc.Save(response, fileName, ContentDisposition.Attachment, saveOptions);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x0000213C File Offset: 0x0000033C
		public void Write(string strText, double conSize, bool conBold, string conAlign)
		{
			this.oWordApplic.Bold = conBold;
			this.oWordApplic.Font.Size = conSize;
			if (conAlign != null)
			{
				if (conAlign == "left")
				{
					this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Left;
					goto IL_9F;
				}
				if (conAlign == "center")
				{
					this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Center;
					goto IL_9F;
				}
				if (conAlign == "right")
				{
					this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Right;
					goto IL_9F;
				}
			}
			this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Left;
			IL_9F:
			this.oWordApplic.Write(strText);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021F8 File Offset: 0x000003F8
		public void Writeln(string strText, double conSize, bool conBold, string conAlign)
		{
			this.oWordApplic.Bold = conBold;
			this.oWordApplic.Font.Size = conSize;
			if (conAlign != null)
			{
				if (conAlign == "left")
				{
					this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Left;
					goto IL_9F;
				}
				if (conAlign == "center")
				{
					this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Center;
					goto IL_9F;
				}
				if (conAlign == "right")
				{
					this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Right;
					goto IL_9F;
				}
			}
			this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Left;
			IL_9F:
			this.oWordApplic.Writeln(strText);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022B4 File Offset: 0x000004B4
		public void InsertText(string strBookmarkName, string text)
		{
			if (this.oDoc.Range.Bookmarks[strBookmarkName] != null)
			{
				Bookmark bookmark = this.oDoc.Range.Bookmarks[strBookmarkName];
				bookmark.Text = text;
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002300 File Offset: 0x00000500
		public void InsertFile(string vfilename)
		{
			Document srcDoc = new Document(vfilename);
			Node previousSibling = this.oWordApplic.CurrentParagraph.PreviousSibling;
			AsposeWordApp.InsertDocument(previousSibling, this.oDoc, srcDoc);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002334 File Offset: 0x00000534
		public void InsertFile(string vfilename, string strBookmarkName, int pNum)
		{
			this.oWordApplic.Document.Range.Replace(new Regex(strBookmarkName), new AsposeWordApp.InsertDocumentAtReplaceHandler(vfilename, pNum), false);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000235C File Offset: 0x0000055C
		public static void InsertDocument(Node insertAfterNode, Document mainDoc, Document srcDoc)
		{
			if (insertAfterNode.NodeType != NodeType.Paragraph & insertAfterNode.NodeType != NodeType.Table)
			{
				throw new Exception("The destination node should be either a paragraph or table.");
			}
			CompositeNode parentNode = insertAfterNode.ParentNode;
			while (srcDoc.LastSection.Body.LastParagraph != null && !srcDoc.LastSection.Body.LastParagraph.HasChildNodes)
			{
				srcDoc.LastSection.Body.LastParagraph.Remove();
			}
			NodeImporter nodeImporter = new NodeImporter(srcDoc, mainDoc, ImportFormatMode.KeepSourceFormatting);
			int count = srcDoc.Sections.Count;
			for (int i = 0; i < count; i++)
			{
				Section section = srcDoc.Sections[i];
				int count2 = section.Body.ChildNodes.Count;
				for (int j = 0; j < count2; j++)
				{
					Node srcNode = section.Body.ChildNodes[j];
					Node node = nodeImporter.ImportNode(srcNode, true);
					parentNode.InsertAfter(node, insertAfterNode);
					insertAfterNode = node;
				}
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002480 File Offset: 0x00000680
		private static void InsertDocument(Node insertAfterNode, Document srcDoc)
		{
			if (!insertAfterNode.NodeType.Equals(NodeType.Paragraph) & !insertAfterNode.NodeType.Equals(NodeType.Table))
			{
				throw new ArgumentException("The destination node should be either a paragraph or table.");
			}
			CompositeNode parentNode = insertAfterNode.ParentNode;
			NodeImporter nodeImporter = new NodeImporter(srcDoc, insertAfterNode.Document, ImportFormatMode.KeepSourceFormatting);
			foreach (object obj in srcDoc.Sections)
			{
				Section section = (Section)obj;
				foreach (object obj2 in section.Body)
				{
					Node node = (Node)obj2;
					if (node.NodeType.Equals(NodeType.Paragraph))
					{
						Paragraph paragraph = (Paragraph)node;
						if (paragraph.IsEndOfSection && !paragraph.HasChildNodes)
						{
							continue;
						}
					}
					Node node2 = nodeImporter.ImportNode(node, true);
					parentNode.InsertAfter(node2, insertAfterNode);
					insertAfterNode = node2;
				}
			}
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002604 File Offset: 0x00000804
		public void InsertLineBreak()
		{
			this.oWordApplic.InsertBreak(BreakType.LineBreak);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002614 File Offset: 0x00000814
		public void InsertLineBreak(int nline)
		{
			for (int i = 0; i < nline; i++)
			{
				this.oWordApplic.InsertBreak(BreakType.LineBreak);
			}
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002640 File Offset: 0x00000840
		public bool InsertScoreTable(bool dishand, bool distab, string handText)
		{
			bool result;
			try
			{
				this.oWordApplic.StartTable();
				this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Left;
				this.oWordApplic.InsertCell();
				this.oWordApplic.CellFormat.Width = 100.0;
				this.oWordApplic.CellFormat.PreferredWidth = PreferredWidth.FromPoints(115.0);
				this.oWordApplic.CellFormat.Borders.LineStyle = LineStyle.None;
				this.oWordApplic.StartTable();
				this.oWordApplic.RowFormat.Height = 20.2;
				this.oWordApplic.InsertCell();
				this.oWordApplic.CellFormat.Borders.LineStyle = LineStyle.Single;
				this.oWordApplic.Font.Size = 10.5;
				this.oWordApplic.Bold = false;
				this.oWordApplic.Write("评卷人");
				this.oWordApplic.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
				this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Center;
				this.oWordApplic.CellFormat.Width = 50.0;
				this.oWordApplic.CellFormat.PreferredWidth = PreferredWidth.FromPoints(50.0);
				this.oWordApplic.RowFormat.Height = 20.0;
				this.oWordApplic.InsertCell();
				this.oWordApplic.CellFormat.Borders.LineStyle = LineStyle.Single;
				this.oWordApplic.Font.Size = 10.5;
				this.oWordApplic.Bold = false;
				this.oWordApplic.Write("得分");
				this.oWordApplic.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
				this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Center;
				this.oWordApplic.CellFormat.Width = 50.0;
				this.oWordApplic.CellFormat.PreferredWidth = PreferredWidth.FromPoints(50.0);
				this.oWordApplic.EndRow();
				this.oWordApplic.RowFormat.Height = 25.0;
				this.oWordApplic.InsertCell();
				this.oWordApplic.RowFormat.Height = 25.0;
				this.oWordApplic.InsertCell();
				this.oWordApplic.EndRow();
				this.oWordApplic.EndTable();
				this.oWordApplic.InsertCell();
				this.oWordApplic.CellFormat.Width = 390.0;
				this.oWordApplic.CellFormat.PreferredWidth = PreferredWidth.Auto;
				this.oWordApplic.CellFormat.Borders.LineStyle = LineStyle.None;
				this.oWordApplic.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
				this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Left;
				this.oWordApplic.Font.Size = 11.0;
				this.oWordApplic.Bold = true;
				this.oWordApplic.Write(handText);
				this.oWordApplic.EndRow();
				this.oWordApplic.RowFormat.Height = 28.0;
				this.oWordApplic.EndTable();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000029F0 File Offset: 0x00000BF0
		public bool InsertTable(DataTable dt, bool haveBorder)
		{
			Table table = this.oWordApplic.StartTable();
			ParagraphAlignment alignment = this.oWordApplic.ParagraphFormat.Alignment;
			this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Center;
			for (int i = 0; i < dt.Rows.Count; i++)
			{
				this.oWordApplic.RowFormat.Height = 25.0;
				for (int j = 0; j < dt.Columns.Count; j++)
				{
					this.oWordApplic.InsertCell();
					this.oWordApplic.Font.Size = 10.5;
					this.oWordApplic.Font.Name = "宋体";
					this.oWordApplic.CellFormat.VerticalAlignment = CellVerticalAlignment.Center;
					this.oWordApplic.ParagraphFormat.Alignment = ParagraphAlignment.Center;
					this.oWordApplic.CellFormat.Width = 60.0;
					this.oWordApplic.CellFormat.PreferredWidth = PreferredWidth.FromPoints(50.0);
					if (haveBorder)
					{
						this.oWordApplic.CellFormat.Borders.LineStyle = LineStyle.Single;
					}
					this.oWordApplic.Write(dt.Rows[i][j].ToString());
				}
				this.oWordApplic.EndRow();
			}
			this.oWordApplic.EndTable();
			this.oWordApplic.ParagraphFormat.Alignment = alignment;
			table.Alignment = TableAlignment.Center;
			table.PreferredWidth = PreferredWidth.Auto;
			table.SetBorder(BorderType.Left, LineStyle.Single, 2.0, Color.Black, true);
			table.SetBorder(BorderType.Right, LineStyle.Single, 2.0, Color.Black, true);
			table.SetBorder(BorderType.Top, LineStyle.Single, 2.0, Color.Black, true);
			table.SetBorder(BorderType.Bottom, LineStyle.Single, 2.0, Color.Black, true);
			return true;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002C13 File Offset: 0x00000E13
		public void InsertPagebreak()
		{
			this.oWordApplic.InsertBreak(BreakType.PageBreak);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002C23 File Offset: 0x00000E23
		public void InsertBookMark(string BookMark)
		{
			this.oWordApplic.StartBookmark(BookMark);
			this.oWordApplic.EndBookmark(BookMark);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002C40 File Offset: 0x00000E40
		public void MoveToBookmark(string strBookMarkName)
		{
			this.oWordApplic.MoveToBookmark(strBookMarkName);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002C50 File Offset: 0x00000E50
		public void ClearBookMark()
		{
			this.oDoc.Range.Bookmarks.Clear();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002C69 File Offset: 0x00000E69
		public void ReplaceText(string oleText, string newText)
		{
			this.oDoc.Range.Replace(oleText, newText, false, false);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002C84 File Offset: 0x00000E84
		public void SetPaperSize(string papersize)
		{
			if (papersize != null)
			{
				if (!(papersize == "A4"))
				{
					if (!(papersize == "A4H"))
					{
						if (!(papersize == "A3"))
						{
							if (!(papersize == "A3H"))
							{
								if (!(papersize == "16K"))
								{
									if (papersize == "8KH")
									{
										foreach (object obj in this.oDoc)
										{
											Section section = (Section)obj;
											section.PageSetup.PageWidth = double.Parse("36.4 ");
											section.PageSetup.PageHeight = double.Parse("25.7");
											section.PageSetup.Orientation = Orientation.Landscape;
											section.PageSetup.TextColumns.SetCount(2);
											section.PageSetup.TextColumns.EvenlySpaced = true;
											section.PageSetup.TextColumns.LineBetween = true;
										}
									}
								}
								else
								{
									foreach (object obj2 in this.oDoc)
									{
										Section section = (Section)obj2;
										section.PageSetup.PaperSize = PaperSize.B5;
										section.PageSetup.Orientation = Orientation.Portrait;
									}
								}
							}
							else
							{
								foreach (object obj3 in this.oDoc)
								{
									Section section = (Section)obj3;
									section.PageSetup.PaperSize = PaperSize.A3;
									section.PageSetup.Orientation = Orientation.Landscape;
									section.PageSetup.TextColumns.SetCount(2);
									section.PageSetup.TextColumns.EvenlySpaced = true;
									section.PageSetup.TextColumns.LineBetween = true;
								}
							}
						}
						else
						{
							foreach (object obj4 in this.oDoc)
							{
								Section section = (Section)obj4;
								section.PageSetup.PaperSize = PaperSize.A3;
								section.PageSetup.Orientation = Orientation.Portrait;
							}
						}
					}
					else
					{
						foreach (object obj5 in this.oDoc)
						{
							Section section = (Section)obj5;
							section.PageSetup.PaperSize = PaperSize.A4;
							section.PageSetup.Orientation = Orientation.Landscape;
							section.PageSetup.TextColumns.SetCount(2);
							section.PageSetup.TextColumns.EvenlySpaced = true;
							section.PageSetup.TextColumns.LineBetween = true;
						}
					}
				}
				else
				{
					foreach (object obj6 in this.oDoc)
					{
						Section section = (Section)obj6;
						section.PageSetup.PaperSize = PaperSize.A4;
						section.PageSetup.Orientation = Orientation.Portrait;
						section.PageSetup.VerticalAlignment = PageVerticalAlignment.Top;
					}
				}
			}
		}

		// Token: 0x04000001 RID: 1
		private int _docversion;

		// Token: 0x04000002 RID: 2
		private Document oDoc;

		// Token: 0x04000003 RID: 3
		private DocumentBuilder oWordApplic;

		// Token: 0x02000003 RID: 3
		private class InsertDocumentAtReplaceHandler : IReplacingCallback
		{
			// Token: 0x06000019 RID: 25 RVA: 0x00003070 File Offset: 0x00001270
			public InsertDocumentAtReplaceHandler(string filename, int _pNum)
			{
				this.vfilename = filename;
				this.pNum = _pNum;
			}

			// Token: 0x0600001A RID: 26 RVA: 0x0000308C File Offset: 0x0000128C
			ReplaceAction IReplacingCallback.Replacing(ReplacingArgs e)
			{
				Document document = new Document(this.vfilename);
				document.FirstSection.Body.FirstParagraph.InsertAfter(new Run(document, this.pNum + "."), null);
				Node matchNode = e.MatchNode;
				Paragraph insertAfterNode = (Paragraph)e.MatchNode.ParentNode;
				AsposeWordApp.InsertDocument(insertAfterNode, document);
				e.MatchNode.Remove();
				e.MatchNode.Range.Delete();
				return ReplaceAction.Skip;
			}

			// Token: 0x04000004 RID: 4
			private readonly string vfilename;

			// Token: 0x04000005 RID: 5
			private readonly int pNum;
		}
	}
}
