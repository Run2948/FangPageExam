<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="content-type"/>
<title>手工选择考试题目</title>
<link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
<link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
<%plugin(jquery)%>
<%plugin(layer) %>
<script type="text/javascript" src="${adminpath}/js/admin.js"></script>
<script type="text/javascript">
    function AddTopic(_tid, _qid, action) {
        $.post("examtopicajax.aspx", {
            tid: _tid,
            qid: _qid,
            action: action
        }, function (data) {
            if (data.error > 0) {
                alert(data.message);
                return;
            }
            $("#curquestions").html(data.curquestions);
            $("#questionlist").val(data.questionlist);
            if (data.action == -1) {
                $("#addtopic_" + _qid).html("<a href=\"javascript:AddTopic(" + _tid + "," + _qid + ",1);\"><img title=\"点击取消试题\" src=\"${webpath}/sites/exam/admin/images/select.gif\" /></a>");
                layer.msg('取消加入成功!', 2, -1);
            }
            else if (data.susscee = -1) {
                $("#addtopic_" + _qid).html("<a href=\"javascript:AddTopic(" + _tid + "," + _qid + ",-1);\"><img title=\"点击添加试题\" src=\"${webpath}/sites/exam/admin/images/state1.gif\" /></a>");
                layer.msg('加入试卷成功!', 2, -1);
            }
        }, "json");
    }
    $(function () {
        var index = layer.getFrameIndex(window.name)
        $("#btnsearch").click(function () {
            index=$.layer({
                type: 1,
                shade: [0],
                fix: false,
                title: '题目搜索',
                maxmin: false,
                page: { dom: '#showsearch' },
                area: ['400px', '200px']
            });
        })
        $('#btnback').click(function () {
            layer.close(index);
        });
        $('#btnok').click(function () {
            $("#frmpost").submit();
        });
    })
</script>
</head>
<body>
  <form id="frmpost" method="get" name="frmpost" action="">
  <input type="hidden" name="action" id="action" value=""/>
  <input type="hidden" name="paper" id="paper" value="${paper}"/>
  <input type="hidden" name="questionlist" id="questionlist" value=""/>
  <input type="hidden" name="examtopicid" id="examtopicid" value="${examtopicid}"/>
  <table class="ntcplist">
    <tr>
      <td colspan="2">
      <div class="newslist">
          <div class="newsicon">
            <ul>
              <li style="background: url(${adminpath}/images/refresh.gif) 2px 6px no-repeat"><a href="examtopicselect.aspx?examtopicid=${examtopicid}&paper=${paper}">刷新</a></li>
              <li style="background: url(images/state1.gif) 2px 6px no-repeat"><a href="examtopicselect.aspx?examtopicid=${examtopicid}&paper=${paper}&select=1">已选</a></li>
              <li style="background: url(images/report.png) 2px 6px no-repeat"><a id="btnsearch" href="#">搜索</a></li>
              <li style="background: url(${adminpath}/images/return.gif) 2px 6px no-repeat"><a target="_parent" href="examtopicmanage.aspx?examid=${examinfo.id}&paper=${paper}">返回</a></li>
              <li style="float:right; width:auto">
                 <strong>手工选题：[${examinfo.name}${GetPaper(paper)}]</strong>
              </li>
            </ul>
          </div>
        </div>
        </td>
    </tr>
    <tr>
      <td colspan="2">
      <table class="datalist" border="1" rules="all" cellspacing="0">
          <tbody>
            <tr style="color:#1317fc" class="thead">
        	  <td align="left" valign="middle">
              <img src="images/tag.gif" />${examtopic.title}(总题数<span style="color:Red">${examtopic.questions}</span>题，已选固定题<span style="color:Red" id="curquestions">${examtopic.curquestions}</span>题)
              </td>
        	  <td width="120">所在题库</td>
              <td width="60">加入试卷</td>
              <td width="60">操作</td>
            </tr>
            <%loop (ExamQuestion) item questionlist %>
            <tr class="tlist" onmouseover="curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'" onmouseout="this.style.backgroundColor=curcolor">
              <td align="left">
              <strong>${(pager.pagesize*(pager.pageindex-1)+loop__id)}、
                      ${TypeStr(item.type)}：
                      ${FmAnswer(item.title)}
              </strong>
              <%if item.type==1||item.type==2 %>
              <div style="height: 2px; overflow: hidden;"></div>
              ${Option(item.option,item.ascount)}
              <%/if %>
              <div style="height: 5px; overflow: hidden; border-bottom-color: rgb(204, 204, 204); border-bottom-width: 1px; border-bottom-style: dashed;"></div>
              <div class="tips">
              <%if item.type!=6 %>
              <div style="color:Red">
              <%if item.type==3 %>
                 <%if item.answer=="Y" %>
                 参考答案：正确
                 <%else if item.answer=="N"%>
                 参考答案：错误
                 <%/if %>
              <%else %>
                 参考答案：${item.answer}
              <%/if %>
              </div>
              <%/if %>
              <span style="color:Red">难易程度：${DifficultyStr(item.difficulty)}，考过次数：${item.exams}，做错次数：${item.wrongs}</span><br />
              <%if {item.explain}!="" %>
              <span style="color:Red">答案解析：${item.explain}</span> 
              <%else %>
              <span style="color:Red">答案解析：暂无解析</span>
              <%/if %>
              </div>
              </td>
              <td align="center">${item.SortInfo.name}</td>
              <td align="center" id="addtopic_${item.id}">
              <%if IsSelected(item.id) %>
              <a href="javascript:AddTopic(${examtopic.id},${item.id},-1);"><img title="点击取消试题" src="images/state1.gif" /></a> 
              <%else %>
              <a href="javascript:AddTopic(${examtopic.id},${item.id},1);"><img title="点击添加试题" src="images/select.gif" /></a>
              <%/if %>
              </td>
              <td>
              <a style="color: #1317fc" href="questionadd.aspx?id=${item.id}&examtopicid=${examtopic.id}">编辑</a><br />
              </td>
            </tr>
            <%/loop %>
          </tbody>
        </table>
        </td>
    </tr>
    <tr>
       <td align="left">共有${pager.total}道题目，页次：${pager.pageindex}/${pager.pagecount}，${pager.pagesize}道每页</td>
       <td align="right"><div class="pages">${pager.pagenum}</div></td>
    </tr>
  </table>
  <div id="showsearch" style="display:none">
  <table style="width:400px;height:163px;" cellpadding="2" cellspacing="1" class="border">
        <tbody>
            <tr>
            <td style="width:80px;text-align:right;"> 搜索： </td>
            <td align="left"><input name="keyword" type="text" value="${keyword}" id="keyword" style="height:21px;width:250px;"/></td>
            </tr>
            <tr>
            <td height="80" colspan="2" align="center">
            <input type="button" name="btnok" value="确定" id="btnok" class="adminsubmit_short"/>&nbsp;&nbsp;
            <input type="button" name="btnback" value="取消" id="btnback" class="adminsubmit_short"/>
            </td>
            </tr>
        </tbody>
   </table>
   </div>
  </form>
</body>
</html>
