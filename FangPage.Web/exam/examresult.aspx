<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FP_Exam.Controller._examresult" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FP_Exam" %>
<%@ Import namespace="FP_Exam.Model" %>

<%@ Import namespace="System" %>
<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：V3.8*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\" xml:lang=\"zh-CN\" lang=\"zh-CN\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\">\r\n");
	ViewBuilder.Append("<title>考试分析 - " + pagetitle.ToString() + "</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link type=\"text/css\" rel=\"stylesheet\" href=\"" + webpath.ToString() + "sites/exam/statics/css/exam.css\">\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery-1.8.2.min.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery-ui.min.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/popup.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + webpath.ToString() + "sites/exam/statics/js/jquery.nicescroll.min.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("<div class=\"hbx1\">\r\n");
	ViewBuilder.Append("  <div class=\"hbx2\">\r\n");
	ViewBuilder.Append("    <div class=\"hbx3\"><img src=\"" + webpath.ToString() + "sites/exam/statics/images/top.jpg\"></div>\r\n");
	ViewBuilder.Append("    <div class=\"hbx4 hbx4a\">\r\n");
	ViewBuilder.Append("      <div class=\"fr\"><a href=\"javascript:;\" class=\"btnq1\" onclick=\"window.print()\">打印</a> </div>\r\n");
	ViewBuilder.Append("      <span class=\"tab3 tab3a\">考试分析</span> \r\n");
	ViewBuilder.Append("      <a href=\"examanswer.aspx?resultid=" + examresult.id.ToString().Trim() + "\" class=\"tab3\"> 答案解析</a>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("<div class=\"hbx2\">\r\n");
	ViewBuilder.Append("  <div class=\"wp\">\r\n");
	ViewBuilder.Append("    <div class=\"wp2\">\r\n");
	ViewBuilder.Append("      <div class=\"wp3\">\r\n");
	ViewBuilder.Append("        <div class=\"wp4 wp4_1\">\r\n");
	ViewBuilder.Append("          <h1 class=\"qtTitle mb10\">" + examresult.examname.ToString().Trim() + "\r\n");

	if (examresult.examid>0)
	{


	if (examresult.status==1)
	{

	ViewBuilder.Append("            (尚未阅卷)\r\n");

	}
	else if (examresult.status==2)
	{

	ViewBuilder.Append("            (已阅卷)\r\n");

	}	//end if


	}	//end if

	ViewBuilder.Append("          </h1>\r\n");
	ViewBuilder.Append("          <table class=\"tab5\">\r\n");
	ViewBuilder.Append("            <tbody>\r\n");
	ViewBuilder.Append("              <tr class=\"tr1\" id=\"result\">\r\n");
	ViewBuilder.Append("                <td class=\"td1\"><div class=\"tit2\">考试成绩单</div>\r\n");
	ViewBuilder.Append("                  考生姓名：\r\n");

	if (examresult.IUser.realname!="")
	{

	ViewBuilder.Append("                    " + examresult.IUser.realname.ToString().Trim() + "\r\n");

	}
	else
	{

	ViewBuilder.Append("                    " + examresult.IUser.username.ToString().Trim() + "\r\n");

	}	//end if

	ViewBuilder.Append("                    <br>\r\n");
	ViewBuilder.Append("                  考试得分：" + (examresult.score*1.0).ToString().Trim() + "分<br>\r\n");
	ViewBuilder.Append("                  答卷耗时：" + (examresult.utime/60+1).ToString().Trim() + "分钟<br>\r\n");
	ViewBuilder.Append("                  <div class=\"bdb1 mb10\"></div>\r\n");
	ViewBuilder.Append("                  试卷总分：" + examresult.total.ToString().Trim() + "分<br>\r\n");
	ViewBuilder.Append("                  及格分数：" + examresult.passmark.ToString().Trim() + "分<br>\r\n");
	ViewBuilder.Append("                  &nbsp;&nbsp;&nbsp;最高分：" + maxscore.ToString() + "分<br>\r\n");
	ViewBuilder.Append("                  &nbsp;&nbsp;&nbsp;平均分：" + avgscore.ToString() + "分 </td>\r\n");
	ViewBuilder.Append("                <td class=\"td2\">\r\n");
	ViewBuilder.Append("                  <div class=\"bc\"><span class=\"s1 s1t\">分数</span><span class=\"s1 s1b\">人数</span>\r\n");
	ViewBuilder.Append("                    <div class=\"bck0\">\r\n");
	ViewBuilder.Append("                      <span class=\"s0 s0t\">0分</span><span class=\"s0 s0b\">0人</span>\r\n");

	loop__id=0;
	foreach(int bck in bcklist)
	{
	loop__id++;


	if (loop__id%2==0)
	{

	ViewBuilder.Append("                      <div class=\"bck1\" style=\"width:" + (bck/examresult.total*100).ToString().Trim() + "%\"><span class=\"s0 s0t\">" + bck.ToString() + "分</span></div>\r\n");

	}
	else
	{

	ViewBuilder.Append("                       <div class=\"bck1\" style=\"width:" + (bck/examresult.total*100).ToString().Trim() + "%\"></div>\r\n");

	}	//end if


	}	//end loop

	ViewBuilder.Append("                      <span class=\"s0 s0a s0t\">" + examresult.total.ToString().Trim() + "分</span><span class=\"s0 s0a s0b\">" + testers.ToString() + "人</span>\r\n");
	ViewBuilder.Append("                    </div>\r\n");
	ViewBuilder.Append("                    <div class=\"bch\">\r\n");
	ViewBuilder.Append("                      <div class=\"bch1\">\r\n");
	ViewBuilder.Append("                        <div class=\"bch1a\"></div>\r\n");
	ViewBuilder.Append("                        <div class=\"bch1b\"></div>\r\n");
	ViewBuilder.Append("                      </div>\r\n");
	ViewBuilder.Append("                      <div class=\"bch2\" style=\"width:" + (examresult.score/examresult.total*100).ToString().Trim() + "%\">\r\n");
	ViewBuilder.Append("                        <div class=\"bch2a\"></div>\r\n");
	ViewBuilder.Append("                        <div class=\"bch2b\"></div>\r\n");
	ViewBuilder.Append("                        <div class=\"bch2h\"><span class=\"s0 s0a s0t\">" + examresult.score.ToString().Trim() + "分</span><span class=\"s0 s0a s0b\">第" + display.ToString() + "名</span></div>\r\n");
	ViewBuilder.Append("                      </div>\r\n");
	ViewBuilder.Append("                    </div>\r\n");
	ViewBuilder.Append("                  </div>\r\n");
	ViewBuilder.Append("                </td>\r\n");
	ViewBuilder.Append("              </tr>\r\n");
	ViewBuilder.Append("              <tr class=\"tr2\" id=\"paper\">\r\n");
	ViewBuilder.Append("                <td class=\"td1\"><div class=\"tit2\">答题分析</div>\r\n");
	ViewBuilder.Append("                  试题总数：" + examresult.questions.ToString().Trim() + "道<br>\r\n");
	ViewBuilder.Append("                  错题总数：" + examresult.wrongs.ToString().Trim() + "道<br>\r\n");
	ViewBuilder.Append("                  <div class=\"bdb1 mb10\"></div></td>\r\n");
	ViewBuilder.Append("                <td class=\"td2\"><table class=\"tab6\">\r\n");
	ViewBuilder.Append("                    <tbody>\r\n");
	ViewBuilder.Append("                      <tr>\r\n");
	ViewBuilder.Append("                        <th>试卷大题</th>\r\n");
	ViewBuilder.Append("                        <th>错题数/总题数</th>\r\n");
	ViewBuilder.Append("                        <th>我的得分</th>\r\n");
	ViewBuilder.Append("                        <th>总分</th>\r\n");
	ViewBuilder.Append("                        <th>得分率</th>\r\n");
	ViewBuilder.Append("                      </tr>\r\n");

	loop__id=0;
	foreach(ExamResultTopic examtopic in examtopiclist)
	{
	loop__id++;

	ViewBuilder.Append("                      <tr>\r\n");
	ViewBuilder.Append("                        <td>" + examtopic.title.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("                        <td>" + examtopic.wrongs.ToString().Trim() + "/" + examtopic.questions.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("                        <td>" + examtopic.score.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("                        <td>" + Math.Round(examtopic.perscore*examtopic.questions,1).ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("                        <td>" + CalRate(examtopic.score,examtopic.perscore*examtopic.questions).ToString() + "%</td>\r\n");
	ViewBuilder.Append("                      </tr>\r\n");

	}	//end loop

	ViewBuilder.Append("                    </tbody>\r\n");
	ViewBuilder.Append("                  </table></td>\r\n");
	ViewBuilder.Append("              </tr>\r\n");
	ViewBuilder.Append("            </tbody>\r\n");
	ViewBuilder.Append("          </table>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("      </div>\r\n");
	ViewBuilder.Append("    </div>\r\n");
	ViewBuilder.Append("  </div>\r\n");
	ViewBuilder.Append("</div>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
