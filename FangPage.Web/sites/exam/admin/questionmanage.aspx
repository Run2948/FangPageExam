<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="content-type"/>
<title>考试题目管理</title>
<link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
<link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
<%plugin(jquery)%>
<%plugin(layer) %>
<script type="text/javascript" src="${adminpath}/js/admin.js"></script>
<script type="text/javascript">
    $(function () {
        PageNav("${GetSortNav(sortinfo,pagename)}");
        $('#checkall').click(function () {
            $('input[name=chkid]').attr("checked", this.checked)
        })
        $("#submitdel").click(function () {
            if (confirm("你确定要删除吗？删除之后将无法进行恢复")) {
                $("#action").val("delete");
                $("#frmpost").submit();
            }
        })
        $("#submitclear").click(function () {
            if (confirm("你确定要清空该题库的试题吗？清空之后将无法进行恢复")) {
                $("#action").val("clear");
                $("#frmpost").submit();
            }
        })
        $("#btnexport").click(function () {
            $("#action").val("export");
            $("#frmpost").submit();
        })
        $("#btnmove").click(function () {
            $("#action").val("move");
            $("#frmpost").submit();
        })
        var index = layer.getFrameIndex(window.name);
        $("#btnsearch").click(function () {
            index = $.layer({
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
            $("#frmsearch").submit();
        });
    })
    function DeleteTopic(id) {
        if (confirm("你确定要删除吗？删除之后将无法进行恢复")) {
            $("#action").val("delete");
            $('#chkid_' + id).attr("checked", "checked");
            $("#frmpost").submit();
        }
    }
    function EditStatus(qid, status) {
        $.post("questionajax.aspx", {
            qid: qid,
            status: status
        }, function (data) {
            if (data.error > 0) {
                alert(data.message);
                return;
            }
            if (data.status == 0) {
                $("#addtopic_" + qid).html("<a href=\"javascript:EditStatus(" + qid + ",1);\"><img title=\"点击添加随机\" src=\"${webpath}/sites/exam/admin/images/state0.gif\" /></a>");
                layer.msg('取消随机成功!', 2, -1);
            }
            else if (data.status = 1) {
                $("#addtopic_" + qid).html("<a href=\"javascript:EditStatus(" + qid + ",0);\"><img title=\"点击取消随机\" src=\"${webpath}/sites/exam/admin/images/state1.gif\" /></a>");
                layer.msg('选择随机成功!', 2, -1);
            }
        }, "json");
    }
</script>
</head>
<body>
  <form id="frmpost" method="post" name="frmpost" action="">
  <input type="hidden" name="action" id="action" value=""/>
  <table class="ntcplist">
    <tr>
      <td colspan="2">
      <div class="newslist">
          <div class="newsicon">
            <ul>
              <li style="background: url(${adminpath}/images/delete.gif) 2px 6px no-repeat"><a id="submitdel" href="#">删除</a></li>
              <li style="background: url(${adminpath}/images/add.gif) 2px 6px no-repeat"><a href="questionadd.aspx?sortid=${sortid}">添加</a></li>
              <li style="background: url(images/paper.gif) 2px 6px no-repeat"><a id="btnmove" href="javascript:void()">移动</a></li>
              <li style="background: url(images/report.png) 2px 6px no-repeat"><a id="btnsearch" href="#">搜索</a></li>
              <li style="background: url(images/xls.gif) 2px 6px no-repeat"><a id="btnexport" href="#">导出</a></li>
              <li style="background: url(images/xls.gif) 2px 6px no-repeat"><a href="questionimport.aspx?sortid=${sortid}">导入</a></li>
              <li style="background: url(${adminpath}/images/refresh.gif) 2px 6px no-repeat"><a href="${pagename}?sortid=${sortid}">刷新</a></li>
              <li style="background: url(images/clear.gif) 2px 6px no-repeat"><a id="submitclear" href="#">清空</a></li>
              <li style="float:right; width:auto">
              <strong>${sortinfo.name}：共有${pager.total}道题目</strong>
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
            <tr class="thead">
              <td width="40"><input id="checkall" name="checkall" type="checkbox"/></td>
        	  <td align="left" valign="middle">
              
              <%if type==0 %>
              <a href="?sortid=${sortid}&type=0"><span style="color:Red">所有题型</span></a>&nbsp;&nbsp;
              <%else %>
              <a href="?sortid=${sortid}&type=0">所有题型</a>&nbsp;&nbsp;
              <%/if %>
              
              <%if ischecked(1,examconfig.questiontype) %>
              <%if type==1 %>
              <a href="?sortid=${sortid}&type=1"><span style="color:Red">单选题</span></a>&nbsp;&nbsp;
              <%else %>
              <a href="?sortid=${sortid}&type=1">单选题</a>&nbsp;&nbsp;
              <%/if %>
              <%/if %>
              
              <%if ischecked(2,examconfig.questiontype) %>
              <%if type==2 %>
              <a href="?sortid=${sortid}&type=2"><span style="color:Red">多选题</span></a>&nbsp;&nbsp;
              <%else %>
              <a href="?sortid=${sortid}&type=2">多选题</a>&nbsp;&nbsp;
              <%/if %>
              <%/if %>
              
              <%if ischecked(3,examconfig.questiontype) %>
              <%if type==3 %>
              <a href="?sortid=${sortid}&type=3"><span style="color:Red">判断题</span></a>&nbsp;&nbsp;
              <%else %>
              <a href="?sortid=${sortid}&type=3">判断题</a>&nbsp;&nbsp;
              <%/if %>
              <%/if %>
              
              <%if ischecked(4,examconfig.questiontype) %>
              <%if type==4 %>
              <a href="?sortid=${sortid}&type=4"><span style="color:Red">填空题</span></a>&nbsp;&nbsp;
              <%else %>
              <a href="?sortid=${sortid}&type=4">填空题</a>&nbsp;&nbsp;
              <%/if %>
              <%/if %>
              
              <%if ischecked(5,examconfig.questiontype) %>
              <%if type==5 %>
              <a href="?sortid=${sortid}&type=5"><span style="color:Red">问答题</span></a>&nbsp;&nbsp;
              <%else %>
              <a href="?sortid=${sortid}&type=5">问答题</a>&nbsp;&nbsp;
              <%/if %>
              <%/if %>
              
              <%if ischecked(6,examconfig.questiontype) %>
              <%if type==6 %>
              <a href="?sortid=${sortid}&type=6"><span style="color:Red">打字题</span></a>&nbsp;&nbsp;
              <%else %>
              <a href="?sortid=${sortid}&type=6">打字题</a>&nbsp;&nbsp;
              <%/if %>
              <%/if %>
              </td>
        	  <td width="120">所在题库</td>
              <td width="40">随机题</td>
              <td width="60">操作</td>
            </tr>
            <%loop (ExamQuestion) item questionlist %>
            <tr class="tlist" onmouseover="curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'" onmouseout="this.style.backgroundColor=curcolor">
              <td><input id="chkid_${item.id}" name="chkid" value="${item.id}" type="checkbox"/></td>
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
              <%if {item.status}==1 %>
              <a href="javascript:EditStatus(${item.id},0);"><img title="点击取消随机" src="images/state1.gif" /></a> 
              <%else %>
              <a href="javascript:EditStatus(${item.id},1);"><img title="点击选择随机" src="images/state0.gif" /></a>
              <%/if %>
              </td>
              <td>
              <a style="color: #1317fc" href="questionadd.aspx?id=${item.id}&backtype=${type}">编辑</a><br /><br />
              <a style="color: #1317fc" href="javascript:DeleteTopic(${item.id})">删除</a>
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
  </form>
  <div id="showsearch" style="display:none">
  <form id="frmsearch" method="get" name="frmsearch" action="">
  <input type="hidden" name="sortid" id="sortid" value="${sortid}"/>
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
   </form>
   </div>
</body>
</html>
