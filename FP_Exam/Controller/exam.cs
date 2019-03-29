using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS;
using FangPage.WMS.Model;
using FP_Exam.Model;

namespace FP_Exam.Controller
{
	// Token: 0x0200001F RID: 31
	public class exam : LoginController
	{
		// Token: 0x0600008E RID: 142 RVA: 0x0000DE5C File Offset: 0x0000C05C
		protected override void View()
		{
			base.Response.Expires = 0;
			base.Response.CacheControl = "no-cache";
			base.Response.Cache.SetNoStore();
			this.examconfig = ExamConifgs.GetExamConfig();
			this.examresult = ExamBll.GetExamResult(this.resultid);
			if (this.examresult.id == 0)
			{
				this.ShowErr("对不起，该考试不存在或已被删除。");
			}
			else if (this.examresult.uid != this.userid)
			{
				this.ShowErr("对不起，您不是本次考试的主人。");
			}
			else if (this.examresult.status == 1)
			{
				this.ShowErr("对不起，该考试已完成。");
			}
			else
			{
				this.examinfo = DbHelper.ExecuteModel<ExamInfo>(this.examresult.examid);
				if (this.examresult.islimit == 1)
				{
					if (this.examresult.starttime <= DateTime.Now && DateTime.Now <= this.examresult.endtime)
					{
						TimeSpan timeSpan = new TimeSpan(DateTime.Now.Ticks);
						TimeSpan ts = new TimeSpan(this.examresult.starttime.Ticks);
						TimeSpan timeSpan2 = timeSpan.Subtract(ts).Duration();
						this.examresult.utime = Convert.ToInt32(timeSpan2.TotalSeconds);
					}
					else
					{
						this.examresult.utime = this.examresult.examtime * 60;
					}
					DbHelper.ExecuteUpdate<ExamResult>(this.examresult);
				}
				if (this.examresult.examtype == 1)
				{
					string macAddress = ExamConifgs.GetMacAddress(this.ip);
					if (this.examresult.ip != "")
					{
						if (this.examresult.ip != this.ip || this.examresult.mac != macAddress)
						{
							this.ShowErr("对不起，请在固定位置上进行考试。");
							return;
						}
					}
					else
					{
						SqlParam[] sqlparams = new SqlParam[]
						{
							DbHelper.MakeSet("ip", this.ip),
							DbHelper.MakeSet("mac", macAddress),
							DbHelper.MakeAndWhere("id", this.resultid)
						};
						DbHelper.ExecuteUpdate<ExamResult>(sqlparams);
					}
				}
				this.sortid = this.examresult.sortid;
				this.sortinfo = SortBll.GetSortInfo(this.sortid);
				this.examresult.passmark = this.examresult.passmark * this.examresult.total / 100.0;
				this.examtopiclist = ExamBll.GetExamResultTopicList(this.resultid);
				this.questionlist = new int[this.examresult.questions];
				int num = this.examresult.examtime * 60 - this.examresult.utime;
				int num2 = num / 3600;
				int num3 = (num - num2 * 3600) / 60;
				int num4 = (num - num2 * 3600 - num3 * 60) % 60;
				this.thetime = string.Concat(new string[]
				{
					num2.ToString("00"),
					":",
					num3.ToString("00"),
					":",
					num4.ToString("00")
				});
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000E224 File Offset: 0x0000C424
		protected List<ExamQuestion> GetQuestionList(ExamResultTopic resultinfo)
		{
			SqlParam sqlParam = DbHelper.MakeAndWhere("id", WhereType.In, resultinfo.questionlist);
			List<ExamQuestion> list = DbHelper.ExecuteList<ExamQuestion>(new SqlParam[]
			{
				sqlParam
			});
			int[] array = FPUtils.SplitInt(resultinfo.questionlist);
			string[] array2 = FPUtils.SplitString(resultinfo.answerlist, "§", array.Length);
			string[] array3 = FPUtils.SplitString(resultinfo.optionlist, "|", array.Length);
			List<ExamQuestion> list2 = new List<ExamQuestion>();
			int num = 0;
			foreach (int num2 in array)
			{
				foreach (ExamQuestion examQuestion in list)
				{
					if (examQuestion.id == num2)
					{
						examQuestion.useranswer = array2[num];
						examQuestion.optionlist = array3[num];
						list2.Add(examQuestion);
					}
				}
				num++;
			}
			return list2;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x0000E350 File Offset: 0x0000C550
		protected string FmAnswer(string content, int tid, string uanswer, int en)
		{
			string[] array = FPUtils.SplitString(content, "(#answer)");
			string[] array2 = FPUtils.SplitString(uanswer, ",", array.Length);
			content = "";
			int num = 0;
			foreach (string str in array)
			{
				if (num < array.Length - 1)
				{
					content = content + str + string.Format("<input type=\"text\" id=\"_{0}\" name=\"answer_{1}\" value=\"{2}\" class=\"tkt\"/>", en, tid, array2[num]);
				}
				else
				{
					content += str;
				}
				num++;
			}
			return content;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000E3F8 File Offset: 0x0000C5F8
		protected string Option(string[] opstr, int ascount, string optionlist)
		{
			string[] array = FPUtils.SplitString("A,B,C,D,E,F");
			int[] array2 = FPUtils.SplitInt(optionlist, ",", ascount);
			string text = "";
			if (ascount > opstr.Length)
			{
				ascount = opstr.Length;
			}
			for (int i = 0; i < ascount; i++)
			{
				if (optionlist != "")
				{
					string text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						array[i],
						".",
						opstr[array2[i]],
						"<br/>"
					});
				}
				else
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
			}
			return text;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x0000E4EC File Offset: 0x0000C6EC
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

		// Token: 0x06000093 RID: 147 RVA: 0x0000E540 File Offset: 0x0000C740
		protected string GetTxtImg(string txt, int qid)
		{
			string text = string.Concat(new object[]
			{
				this.webpath,
				"cache/qtxt_",
				qid,
				".jpg"
			});
			if (!File.Exists(FPUtils.GetMapPath(text)))
			{
				Bitmap bitmap = new Bitmap(710, 100);
				Graphics graphics = Graphics.FromImage(bitmap);
				Font font = new Font("宋体", 11f);
				SizeF sizeF = graphics.MeasureString(txt, font, 710);
				bitmap = new Bitmap(710, Convert.ToInt32(sizeF.Height));
				graphics = Graphics.FromImage(bitmap);
				graphics.Clear(Color.White);
				graphics.DrawString(txt, font, Brushes.Black, new Rectangle(0, 0, Convert.ToInt32(sizeF.Width), Convert.ToInt32(sizeF.Height)));
				bitmap.Save(FPUtils.GetMapPath(text), ImageFormat.Jpeg);
			}
			return text;
		}

		// Token: 0x04000090 RID: 144
		protected int resultid = FPRequest.GetInt("resultid");

		// Token: 0x04000091 RID: 145
		protected int sortid;

		// Token: 0x04000092 RID: 146
		protected ExamResult examresult = new ExamResult();

		// Token: 0x04000093 RID: 147
		protected SortInfo sortinfo = new SortInfo();

		// Token: 0x04000094 RID: 148
		protected List<ExamResultTopic> examtopiclist = new List<ExamResultTopic>();

		// Token: 0x04000095 RID: 149
		protected string[] answerarr = new string[]
		{
			"A",
			"B",
			"C",
			"D",
			"E",
			"F"
		};

		// Token: 0x04000096 RID: 150
		protected ExamConfig examconfig = new ExamConfig();

		// Token: 0x04000097 RID: 151
		protected int[] questionlist;

		// Token: 0x04000098 RID: 152
		protected string thetime = "00:00:00";

		// Token: 0x04000099 RID: 153
		protected ExamInfo examinfo = new ExamInfo();
	}
}
