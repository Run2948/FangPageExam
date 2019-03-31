using Aspose.Words;
using Aspose.Words.Saving;
using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace FP_Exam.Controller
{
	public class examsign_list : LoginController
	{
		protected List<ExamSignInfo> examsignlist = new List<ExamSignInfo>();

		protected override void Controller()
		{
			bool ispost = this.ispost;
			if (ispost)
			{
				int @int = FPRequest.GetInt("signid");
				ExamSignInfo examSignInfo = DbHelper.ExecuteModel<ExamSignInfo>(@int);
				bool flag = examSignInfo.id == 0;
				if (flag)
				{
					this.ShowErr("对不起，该报名不存在或已被删除。");
				}
				bool flag2 = examSignInfo.uid != this.userid;
				if (flag2)
				{
					this.ShowErr("对不起，您没有权限操作该报名。");
				}
				ExamInfo examInfo = DbHelper.ExecuteModel<ExamInfo>(examSignInfo.examid);
				bool flag3 = examInfo.id == 0;
				if (flag3)
				{
					this.ShowErr("对不起，该考试不存在或已被删除。");
				}
				bool flag4 = this.action == "download";
				if (flag4)
				{
					bool flag5 = examSignInfo.status != 2;
					if (flag5)
					{
						this.ShowErr("对不起，该报名尚未审核通过，不能下载准考证。");
					}
					Document document = new Document(FPFile.GetMapPath(this.webpath + "exam/admin/template/examsign.doc"));
					DocumentBuilder documentBuilder = new DocumentBuilder(document);
					bool flag6 = document.Range.Bookmarks["ikey"] != null;
					if (flag6)
					{
						documentBuilder.MoveToBookmark("ikey");
						documentBuilder.Write(examSignInfo.ikey);
					}
					bool flag7 = document.Range.Bookmarks["examname"] != null;
					if (flag7)
					{
						documentBuilder.MoveToBookmark("examname");
						documentBuilder.Write(examInfo.title);
					}
					string[] keys = examSignInfo.signer.Keys;
					for (int i = 0; i < keys.Length; i++)
					{
						string text = keys[i];
						bool flag8 = document.Range.Bookmarks[text] != null;
						if (flag8)
						{
							documentBuilder.MoveToBookmark(text);
							documentBuilder.Write(examSignInfo.signer[text]);
						}
					}
					bool flag9 = document.Range.Bookmarks["examtime"] != null;
					if (flag9)
					{
						documentBuilder.MoveToBookmark("examtime");
						documentBuilder.Write(examInfo.opentime);
					}
					bool flag10 = document.Range.Bookmarks["examaddress"] != null;
					if (flag10)
					{
						documentBuilder.MoveToBookmark("examaddress");
						documentBuilder.Write(examInfo.address);
					}
					bool flag11 = document.Range.Bookmarks["img"] != null;
					if (flag11)
					{
						string mapPath = FPFile.GetMapPath(examSignInfo.img);
						bool flag12 = File.Exists(mapPath);
						if (flag12)
						{
							documentBuilder.MoveToBookmark("img");
							documentBuilder.InsertImage(FPFile.GetMapPath(examSignInfo.img), 195.0, 240.0);
						}
					}
					SaveOptions saveOptions = SaveOptions.CreateSaveOptions(SaveFormat.Pdf);
					document.Save(base.Response, examSignInfo.signer["uname"] + "_" + examSignInfo.ikey + "_准考证.pdf", ContentDisposition.Attachment, saveOptions);
				}
				else
				{
					bool flag13 = this.action == "delete";
					if (flag13)
					{
						bool flag14 = examSignInfo.status == 2;
						if (flag14)
						{
							this.ShowErr("对不起，该报名已提交，不能删除。");
						}
						bool flag15 = examSignInfo.status == 2;
						if (flag15)
						{
							this.ShowErr("对不起，该报名已审核通过，不能删除。");
						}
						DbHelper.ExecuteDelete<ExamSignInfo>(@int);
					}
				}
			}
			SqlParam sqlParam = DbHelper.MakeAndWhere("uid", this.userid);
			this.examsignlist = DbHelper.ExecuteList<ExamSignInfo>(new SqlParam[]
			{
				sqlParam
			});
		}
	}
}
