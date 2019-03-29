<%@ Page language="c#" AutoEventWireup="false" EnableViewState="false" Inherits="FangPage.Exam.Controller.examtopicselect" %>
<%@ Import namespace="FangPage.MVC" %>
<%@ Import namespace="FangPage.Exam" %>
<%@ Import namespace="FangPage.Exam.Model" %>

<script runat="server">
override protected void OnInitComplete(EventArgs e)
{
	/*方配软件技术有限公司，官方网站：http://www.fangpage.com，站点版本：3.7*/
	base.OnInitComplete(e);
	int loop__id=0;
	ViewBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n");
	ViewBuilder.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n");
	ViewBuilder.Append("<head>\r\n");
	ViewBuilder.Append("<meta content=\"text/html; charset=utf-8\" http-equiv=\"content-type\">\r\n");
	ViewBuilder.Append("<title>手工选择考试题目</title>\r\n");
	ViewBuilder.Append("	" + meta.ToString() + "\r\n");
	ViewBuilder.Append("<link rel=\"stylesheet\" type=\"text/css\" href=\"" + adminpath.ToString() + "css/admin.css\">\r\n");
	ViewBuilder.Append("<link href=\"" + adminpath.ToString() + "css/datagrid.css\" rel=\"stylesheet\" type=\"text/css\">\r\n");
	ViewBuilder.Append("" + plugins("jquery") + "\r\n");
	ViewBuilder.Append("" + plugins("layer") + "\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\" src=\"" + adminpath.ToString() + "js/admin.js\"></");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("<script type=\"text/javascript\">\r\n");
	ViewBuilder.Append("    function AddTopic(_tid, _qid, action) {\r\n");
	ViewBuilder.Append("        $.post(\"examtopicajax.aspx\", {\r\n");
	ViewBuilder.Append("            tid: _tid,\r\n");
	ViewBuilder.Append("            qid: _qid,\r\n");
	ViewBuilder.Append("            action: action\r\n");
	ViewBuilder.Append("        }, function (data) {\r\n");
	ViewBuilder.Append("            if (data.error > 0) {\r\n");
	ViewBuilder.Append("                alert(data.message);\r\n");
	ViewBuilder.Append("                return;\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            $(\"#curquestions\").html(data.curquestions);\r\n");
	ViewBuilder.Append("            $(\"#questionlist\").val(data.questionlist);\r\n");
	ViewBuilder.Append("            if (data.action == -1) {\r\n");
	ViewBuilder.Append("                $(\"#addtopic_\" + _qid).html(\"<a href=\\\"javascript:AddTopic(\" + _tid + \",\" + _qid + \",1);\\\"><img title=\\\"点击取消试题\\\" src=\\\"" + webpath.ToString() + "sites/exam/admin/images/select.gif\\\" /></a>\");\r\n");
	ViewBuilder.Append("                layer.msg('取消加入成功!', 2, -1);\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("            else if (data.susscee = -1) {\r\n");
	ViewBuilder.Append("                $(\"#addtopic_\" + _qid).html(\"<a href=\\\"javascript:AddTopic(\" + _tid + \",\" + _qid + \",-1);\\\"><img title=\\\"点击添加试题\\\" src=\\\"" + webpath.ToString() + "sites/exam/admin/images/state1.gif\\\" /></a>\");\r\n");
	ViewBuilder.Append("                layer.msg('加入试卷成功!', 2, -1);\r\n");
	ViewBuilder.Append("            }\r\n");
	ViewBuilder.Append("        }, \"json\");\r\n");
	ViewBuilder.Append("    }\r\n");
	ViewBuilder.Append("    $(function () {\r\n");
	ViewBuilder.Append("        var index = layer.getFrameIndex(window.name)\r\n");
	ViewBuilder.Append("        $(\"#btnsearch\").click(function () {\r\n");
	ViewBuilder.Append("            index=$.layer({\r\n");
	ViewBuilder.Append("                type: 1,\r\n");
	ViewBuilder.Append("                shade: [0],\r\n");
	ViewBuilder.Append("                fix: false,\r\n");
	ViewBuilder.Append("                title: '题目搜索',\r\n");
	ViewBuilder.Append("                maxmin: false,\r\n");
	ViewBuilder.Append("                page: { dom: '#showsearch' },\r\n");
	ViewBuilder.Append("                area: ['400px', '200px']\r\n");
	ViewBuilder.Append("            });\r\n");
	ViewBuilder.Append("        })\r\n");
	ViewBuilder.Append("        $('#btnback').click(function () {\r\n");
	ViewBuilder.Append("            layer.close(index);\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("        $('#btnok').click(function () {\r\n");
	ViewBuilder.Append("            $(\"#frmpost\").submit();\r\n");
	ViewBuilder.Append("        });\r\n");
	ViewBuilder.Append("    })\r\n");
	ViewBuilder.Append("</");
	ViewBuilder.Append("script>\r\n");
	ViewBuilder.Append("</head>\r\n");
	ViewBuilder.Append("<body>\r\n");
	ViewBuilder.Append("  <form id=\"frmpost\" method=\"get\" name=\"frmpost\" action=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"action\" id=\"action\" value=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"paper\" id=\"paper\" value=\"" + paper.ToString() + "\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"questionlist\" id=\"questionlist\" value=\"\">\r\n");
	ViewBuilder.Append("  <input type=\"hidden\" name=\"examtopicid\" id=\"examtopicid\" value=\"" + examtopicid.ToString() + "\">\r\n");
	ViewBuilder.Append("  <table class=\"ntcplist\">\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td colspan=\"2\">\r\n");
	ViewBuilder.Append("      <div class=\"newslist\">\r\n");
	ViewBuilder.Append("          <div class=\"newsicon\">\r\n");
	ViewBuilder.Append("            <ul>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/refresh.gif) 2px 6px no-repeat\"><a href=\"examtopicselect.aspx?examtopicid=" + examtopicid.ToString() + "&paper=" + paper.ToString() + "\">刷新</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + webpath.ToString() + "sites/exam/admin/images/state1.gif) 2px 6px no-repeat\"><a href=\"examtopicselect.aspx?examtopicid=" + examtopicid.ToString() + "&paper=" + paper.ToString() + "&select=1\">已选</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + webpath.ToString() + "sites/exam/admin/images/report.png) 2px 6px no-repeat\"><a id=\"btnsearch\" href=\"#\">搜索</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"background: url(" + adminpath.ToString() + "images/return.gif) 2px 6px no-repeat\"><a target=\"_parent\" href=\"examtopicmanage.aspx?examid=" + examinfo.id.ToString().Trim() + "&paper=" + paper.ToString() + "\">返回</a></li>\r\n");
	ViewBuilder.Append("              <li style=\"float:right; width:auto\">\r\n");
	ViewBuilder.Append("                 <strong>手工选题：[" + examinfo.name.ToString().Trim() + "" + GetPaper(paper).ToString() + "]</strong>\r\n");
	ViewBuilder.Append("              </li>\r\n");
	ViewBuilder.Append("            </ul>\r\n");
	ViewBuilder.Append("          </div>\r\n");
	ViewBuilder.Append("        </div>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("      <td colspan=\"2\">\r\n");
	ViewBuilder.Append("      <table class=\"datalist\" border=\"1\" rules=\"all\" cellspacing=\"0\">\r\n");
	ViewBuilder.Append("          <tbody>\r\n");
	ViewBuilder.Append("            <tr style=\"color:#1317fc\" class=\"thead\">\r\n");
	ViewBuilder.Append("        	  <td align=\"left\" valign=\"middle\">\r\n");
	ViewBuilder.Append("              <img src=\"" + webpath.ToString() + "sites/exam/admin/images/tag.gif\">" + examtopic.title.ToString().Trim() + "(总题数<span style=\"color:Red\">" + examtopic.questions.ToString().Trim() + "</span>题，已选固定题<span style=\"color:Red\" id=\"curquestions\">" + examtopic.curquestions.ToString().Trim() + "</span>题)\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("        	  <td width=\"120\">所在题库</td>\r\n");
	ViewBuilder.Append("              <td width=\"60\">加入试卷</td>\r\n");
	ViewBuilder.Append("              <td width=\"60\">操作</td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	loop__id=0;
	foreach(ExamQuestion item in questionlist)
	{
	loop__id++;

	ViewBuilder.Append("            <tr class=\"tlist\" onmouseover=\"curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'\" onmouseout=\"this.style.backgroundColor=curcolor\">\r\n");
	ViewBuilder.Append("              <td align=\"left\">\r\n");
	ViewBuilder.Append("              <strong>" + (pager.pagesize*(pager.pageindex-1)+loop__id).ToString().Trim() + "、\r\n");
	ViewBuilder.Append("                      " + TypeStr(item.type).ToString() + "：\r\n");
	ViewBuilder.Append("                      " + FmAnswer(item.title).ToString() + "\r\n");
	ViewBuilder.Append("              </strong>\r\n");

	if (item.type==1||item.type==2)
	{

	ViewBuilder.Append("              <div style=\"height: 2px; overflow: hidden;\"></div>\r\n");
	ViewBuilder.Append("              " + Option(item.option,item.ascount).ToString() + "\r\n");

	}	//end if

	ViewBuilder.Append("              <div style=\"height: 5px; overflow: hidden; border-bottom-color: rgb(204, 204, 204); border-bottom-width: 1px; border-bottom-style: dashed;\"></div>\r\n");
	ViewBuilder.Append("              <div class=\"tips\">\r\n");

	if (item.type!=6)
	{

	ViewBuilder.Append("              <div style=\"color:Red\">\r\n");

	if (item.type==3)
	{


	if (item.answer=="Y")
	{

	ViewBuilder.Append("                 参考答案：正确\r\n");

	}
	else if (item.answer=="N")
	{

	ViewBuilder.Append("                 参考答案：错误\r\n");

	}	//end if


	}
	else
	{

	ViewBuilder.Append("                 参考答案：" + item.answer.ToString().Trim() + "\r\n");

	}	//end if

	ViewBuilder.Append("              </div>\r\n");

	}	//end if

	ViewBuilder.Append("              <span style=\"color:Red\">难易程度：" + DifficultyStr(item.difficulty).ToString() + "，考过次数：" + item.exams.ToString().Trim() + "，做错次数：" + item.wrongs.ToString().Trim() + "</span><br>\r\n");

	if (item.explain!="")
	{

	ViewBuilder.Append("              <span style=\"color:Red\">答案解析：" + item.explain.ToString().Trim() + "</span> \r\n");

	}
	else
	{

	ViewBuilder.Append("              <span style=\"color:Red\">答案解析：暂无解析</span>\r\n");

	}	//end if

	ViewBuilder.Append("              </div>\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td align=\"center\">" + item.SortInfo.name.ToString().Trim() + "</td>\r\n");
	ViewBuilder.Append("              <td align=\"center\" id=\"addtopic_" + item.id.ToString().Trim() + "\">\r\n");

	if (IsSelected(item.id))
	{

	ViewBuilder.Append("              <a href=\"javascript:AddTopic(" + examtopic.id.ToString().Trim() + "," + item.id.ToString().Trim() + ",-1);\"><img title=\"点击取消试题\" src=\"" + webpath.ToString() + "sites/exam/admin/images/state1.gif\"></a> \r\n");

	}
	else
	{

	ViewBuilder.Append("              <a href=\"javascript:AddTopic(" + examtopic.id.ToString().Trim() + "," + item.id.ToString().Trim() + ",1);\"><img title=\"点击添加试题\" src=\"" + webpath.ToString() + "sites/exam/admin/images/select.gif\"></a>\r\n");

	}	//end if

	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("              <td>\r\n");
	ViewBuilder.Append("              <a style=\"color: #1317fc\" href=\"questionadd.aspx?id=" + item.id.ToString().Trim() + "&examtopicid=" + examtopic.id.ToString().Trim() + "\">编辑</a><br>\r\n");
	ViewBuilder.Append("              </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");

	}	//end loop

	ViewBuilder.Append("          </tbody>\r\n");
	ViewBuilder.Append("        </table>\r\n");
	ViewBuilder.Append("        </td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("    <tr>\r\n");
	ViewBuilder.Append("       <td align=\"left\">共有" + pager.total.ToString().Trim() + "道题目，页次：" + pager.pageindex.ToString().Trim() + "/" + pager.pagecount.ToString().Trim() + "，" + pager.pagesize.ToString().Trim() + "道每页</td>\r\n");
	ViewBuilder.Append("       <td align=\"right\"><div class=\"pages\">" + pager.pagenum.ToString().Trim() + "</div></td>\r\n");
	ViewBuilder.Append("    </tr>\r\n");
	ViewBuilder.Append("  </table>\r\n");
	ViewBuilder.Append("  <div id=\"showsearch\" style=\"display:none\">\r\n");
	ViewBuilder.Append("  <table style=\"width:400px;height:163px;\" cellpadding=\"2\" cellspacing=\"1\" class=\"border\">\r\n");
	ViewBuilder.Append("        <tbody>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td style=\"width:80px;text-align:right;\"> 搜索： </td>\r\n");
	ViewBuilder.Append("            <td align=\"left\"><input name=\"keyword\" type=\"text\" value=\"" + keyword.ToString() + "\" id=\"keyword\" style=\"height:21px;width:250px;\"></td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("            <tr>\r\n");
	ViewBuilder.Append("            <td height=\"80\" colspan=\"2\" align=\"center\">\r\n");
	ViewBuilder.Append("            <input type=\"button\" name=\"btnok\" value=\"确定\" id=\"btnok\" class=\"adminsubmit_short\">&nbsp;&nbsp;\r\n");
	ViewBuilder.Append("            <input type=\"button\" name=\"btnback\" value=\"取消\" id=\"btnback\" class=\"adminsubmit_short\">\r\n");
	ViewBuilder.Append("            </td>\r\n");
	ViewBuilder.Append("            </tr>\r\n");
	ViewBuilder.Append("        </tbody>\r\n");
	ViewBuilder.Append("   </table>\r\n");
	ViewBuilder.Append("   </div>\r\n");
	ViewBuilder.Append("  </form>\r\n");
	ViewBuilder.Append("</body>\r\n");
	ViewBuilder.Append("</html>\r\n");

	Response.Write(ViewBuilder.ToString());
}
</script>
