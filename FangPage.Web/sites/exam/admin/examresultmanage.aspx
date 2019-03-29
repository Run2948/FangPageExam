<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="content-type"/>
<title>考试成绩管理</title>
<link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
<link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
<%plugin(jquery)%>
<%plugin(layer) %>
<script type="text/javascript" src="${adminpath}/js/admin.js"></script>
<script type="text/javascript">
    $(function () {
        PageNav("${sortinfo.name},${rawpath}/exammanage.aspx?sortid=${examinfo.sortid}|${examinfo.name},${rawpath}/exammanage.aspx?sortid=${examinfo.sortid}${pagenav}|考试成绩管理,${rawurl}");
        $('#checkall').click(function () {
            $('input[name=chkid]').attr("checked", this.checked)
        })
        $("#submitdel").click(function () {
            if (confirm("您确定要给所选的考生进行重考吗？")) {
                $("#action").val("delete");
                $("#frmpost").submit();
            }
        })
        $("#btnexport").click(function () {
            $("#action").val("export");
            $("#frmpost").submit();
        })
        $("#btnreport").click(function () {
            $("#action").val("report");
            $("#frmpost").submit();
        })
        var index = layer.getFrameIndex(window.name);
        $("#btnsearch").click(function () {
            index = $.layer({
                type: 1,
                shade: [0],
                fix: false,
                title: '考生搜索',
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

    function DeleteExam(id) {
        if (confirm("您确定给该考生重考吗？")) {
            $("#action").val("delete");
            $('#chkid_' + id).attr("checked", "checked");
            $("#frmpost").submit();
        }
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
              <li style="background: url(images/xls.gif) 2px 6px no-repeat"><a id="btnexport" href="#">导出成绩表</a></li>
              <li style="background: url(images/question.gif) 2px 6px no-repeat"><a id="btnreport" href="#">考试分析报告</a></li>
              <li style="background: url(images/report.png) 2px 6px no-repeat"><a id="btnsearch" href="#">搜索</a></li>
              <li style="background: url(${adminpath}/images/delete.gif) 2px 6px no-repeat"><a id="submitdel" href="#">重考</a></li>
              <li style="background: url(${adminpath}/images/refresh.gif) 2px 6px no-repeat"><a href="examresultmanage.aspx?examid=${examid}">刷新</a></li>
              <li style="background: url(${adminpath}/images/return.gif) 2px 6px no-repeat"><a href="exammanage.aspx?sortid=${examinfo.sortid}">返回</a></li>
              <li style="float:right; width:auto">
              <strong>
              ${examinfo.name}：考试人数${pager.total}人</strong>
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
        	  <td>用户名</td>
        	  <td>姓名</td>
              <td>所在部门</td>
              <td>考试得分</td>
              <td>开始时间</td>
              <td>考试用时</td>
              <td>考试状态</td>
              <td>是否阅卷</td>
              <td width="110">操作</td>
            </tr>
            <%loop (ExamResult) item examresultlist %>
            <tr class="tlist" onmouseover="curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'" onmouseout="this.style.backgroundColor=curcolor">
              <td><input id="chkid_${item.id}" name="chkid" value="${item.id}" type="checkbox"/></td>
              <td>${item.IUser.username}</td>
              <td align="center">${item.IUser.realname}</td>
              <td align="center">${item.IUser.Department.name}</td>
              <td align="center">${item.score}</td>
              <td align="center">
                  <%if {item.status}>=0 %>
                  ${fmdate(item.examdatetime,"yyyy-MM-dd HH:mm:ss")}
                  <%else %>
                  ----
                  <%/if %>
              </td>
              <td align="center">
                  <%if {item.status}>=0 %>
                  ${(item.utime/60+1)}分钟
                  <%else %>
                  ----
                  <%/if %>
              </td>
              <td align="center">
              <%if {item.status}>=1 %>
              <span style="color:#1317fc">已交卷</span> 
              <%else if {item.status}==0 %>
              <span style="color:#00ff21">未交卷</span>
              <%else %>
              <span style="color:#ff0000">未考试</span>
              <%/if %>
              </td>
              <td align="center">
              <%if {item.status}==2 %>
              <span style="color:#1317fc">已阅卷</span>
              <%else if {item.status}==1 %>
              <span style="color:#00ff21">未阅卷</span>
              <%/if %>
              </td>
              <td>
              <a style="color: #1317fc" href="javascript:DeleteExam(${item.id})">重考</a>
              <%if {item.id}==0 %>
              <a style="color: #1317fc" href="javascript:alert('对不起，该考生尚未参加考试');" target="_blank">阅卷</a>
              <%else %>
              <a style="color: #1317fc" href="../examread.aspx?resultid=${item.id}" target="_blank">阅卷</a>
              <%/if %>
              <a style="color: #1317fc" href="examresultprint.aspx?resultid=${item.id}" target="_blank">成绩单</a>
              </td>
            </tr>
            <%/loop %>
          </tbody>
        </table>
        </td>
    </tr>
    <tr>
       <td align="left">共有${pager.total}条记录，页次：${pager.pageindex}/${pager.pagecount}，${pager.pagesize}条每页</td>
       <td align="right"><div class="pages">${pager.pagenum}</div></td>
    </tr>
  </table>
  </form>
  <div id="showsearch" style="display:none">
  <form id="frmsearch" method="get" name="frmsearch" action="">
  <input type="hidden" name="examid" id="examid" value="${examid}"/>
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
