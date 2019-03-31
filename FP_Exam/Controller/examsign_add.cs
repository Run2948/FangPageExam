using FangPage.Common;
using FangPage.Data;
using FangPage.MVC;
using FangPage.WMS.Bll;
using FangPage.WMS.Model;
using FangPage.WMS.Web;
using FP_Exam.Model;
using System;
using System.Collections.Generic;

namespace FP_Exam.Controller
{
	public class examsign_add : LoginController
	{
		protected int id = FPRequest.GetInt("id");

		protected UserInfo userinfo = new UserInfo();

		protected ExamSignInfo examsigninfo = new ExamSignInfo();

		protected List<ExamInfo> examlist = new List<ExamInfo>();

		protected override void Controller()
		{
			bool flag = this.id > 0;
			if (flag)
			{
				this.examsigninfo = DbHelper.ExecuteModel<ExamSignInfo>(this.id);
				bool flag2 = this.examsigninfo.id == 0;
				if (flag2)
				{
					this.ShowErr("该报名不存在或已被删除。");
				}
				bool flag3 = this.examsigninfo.uid != this.userid;
				if (flag3)
				{
					this.ShowErr("对不起，您没有权限。");
				}
			}
			bool flag4 = this.examsigninfo.status == 2;
			if (flag4)
			{
				this.ShowErr("对不起，该报名已提交，不能再更改。");
			}
			bool flag5 = this.examsigninfo.status == 2;
			if (flag5)
			{
				this.ShowErr("对不起，该报名已审核通过，不能再更改。");
			}
			SqlParam[] sqlparams = new SqlParam[]
			{
				DbHelper.MakeAndWhere("status", 1),
				DbHelper.MakeAndWhere("issign", 1)
			};
			this.examlist = DbHelper.ExecuteList<ExamInfo>(sqlparams);
			bool flag6 = this.examsigninfo.id == 0;
			if (flag6)
			{
				this.examsigninfo.uid = this.userid;
			}
			this.userinfo = UserBll.GetMapUserInfo(this.examsigninfo.uid);
			bool flag7 = this.examsigninfo.id == 0;
			if (flag7)
			{
				this.examsigninfo.signer["uname"] = this.userinfo.realname;
				this.examsigninfo.signer["sex"] = this.userinfo.sex;
				this.examsigninfo.signer["bday"] = this.userinfo.extend["bday"];
				this.examsigninfo.signer["nation"] = this.userinfo.extend["nation"];
				this.examsigninfo.signer["joblimit"] = this.userinfo.extend["joblimit"];
				this.examsigninfo.signer["idcard"] = this.userinfo.idcard;
				this.examsigninfo.signer["mobile"] = this.userinfo.mobile;
				this.examsigninfo.signer["email"] = this.userinfo.email;
				this.examsigninfo.signer["shcool"] = this.userinfo.extend["shcool"];
				this.examsigninfo.signer["profession"] = this.userinfo.extend["profession"];
				this.examsigninfo.signer["company"] = this.userinfo.extend["company"];
			}
			bool ispost = this.ispost;
			if (ispost)
			{
				this.examsigninfo = FPRequest.GetModel<ExamSignInfo>(this.examsigninfo);
				bool flag8 = this.examsigninfo.examid == 0;
				if (flag8)
				{
					this.ShowErr("对不起，您没有选择考试。");
				}
				bool flag9 = this.examsigninfo.id == 0;
				if (flag9)
				{
					SqlParam[] sqlparams2 = new SqlParam[]
					{
						DbHelper.MakeAndWhere("uid", this.userid),
						DbHelper.MakeAndWhere("examid", this.examsigninfo.examid)
					};
					int num = DbHelper.ExecuteCount<ExamSignInfo>(sqlparams2);
					bool flag10 = num > 0;
					if (flag10)
					{
						this.ShowErr("对不起，该考试您已报过名，不能重复报名。");
					}
				}
				bool isfile = this.isfile;
				if (isfile)
				{
					bool flag11 = FPRequest.Files["img"] != null;
					if (flag11)
					{
						AttachInfo attachInfo = AttachBll.UploadImg(FPRequest.Files["img"], this.examsigninfo.attachid, this.userid, this.setupinfo.markup, 800, 800);
						bool flag12 = attachInfo.id > 0;
						if (flag12)
						{
							AttachBll.DeleteByFileName(this.examsigninfo.img);
							this.examsigninfo.img = attachInfo.filename;
						}
					}
					bool flag13 = FPRequest.Files["payimg"] != null;
					if (flag13)
					{
						AttachInfo attachInfo2 = AttachBll.UploadImg(FPRequest.Files["payimg"], this.examsigninfo.attachid, this.userid, this.setupinfo.markup, 800, 800);
						bool flag14 = attachInfo2.id > 0;
						if (flag14)
						{
							AttachBll.DeleteByFileName(this.examsigninfo.payimg);
							this.examsigninfo.payimg = attachInfo2.filename;
						}
					}
				}
				bool flag15 = this.action == "post";
				if (flag15)
				{
					bool flag16 = this.examsigninfo.img == "";
					if (flag16)
					{
						this.ShowErr("对不起，您没有上传附款凭证。");
					}
					bool flag17 = this.examsigninfo.payimg == "";
					if (flag17)
					{
						this.ShowErr("对不起，您没有上传附款凭证。");
					}
					this.examsigninfo.reason = "";
					this.examsigninfo.status = 1;
				}
				bool flag18 = this.examsigninfo.id > 0;
				if (flag18)
				{
					this.examsigninfo.postdatetime = DbUtils.GetDateTime();
					DbHelper.ExecuteUpdate<ExamSignInfo>(this.examsigninfo);
				}
				else
				{
					this.examsigninfo.uid = this.userid;
					DbHelper.ExecuteInsert<ExamSignInfo>(this.examsigninfo);
				}
				this.userinfo.realname = this.examsigninfo.signer["uname"];
				this.userinfo.sex = this.examsigninfo.signer["sex"];
				this.userinfo.extend["bday"] = this.examsigninfo.signer["bday"];
				this.userinfo.extend["nation"] = this.examsigninfo.signer["nation"];
				this.userinfo.extend["joblimit"] = this.examsigninfo.signer["joblimit"];
				this.userinfo.idcard = this.examsigninfo.signer["idcard"];
				this.userinfo.mobile = this.examsigninfo.signer["mobile"];
				this.userinfo.email = this.examsigninfo.signer["email"];
				this.userinfo.extend["shcool"] = this.examsigninfo.signer["shcool"];
				this.userinfo.extend["profession"] = this.examsigninfo.signer["profession"];
				this.userinfo.extend["company"] = this.examsigninfo.signer["company"];
				DbHelper.ExecuteUpdate<UserInfo>(this.userinfo);
				FPResponse.Redirect("examsign_list.aspx");
			}
			bool flag19 = this.examsigninfo.attachid == "";
			if (flag19)
			{
				this.examsigninfo.attachid = FPRandom.CreateAuth(32);
			}
		}
	}
}
