<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="content-type"/>
<title>考试监控</title>
<link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
<link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
<%plugin(jquery)%>
<script type="text/javascript" src="${adminpath}/js/admin.js"></script>
<script type="text/javascript">
    $(function () {
        PageNav("${sortinfo.name},${rawpath}/exammanage.aspx?sortid=${examinfo.sortid}|${examinfo.name},${rawpath}/exammanage.aspx?sortid=${examinfo.sortid}${pagenav}|考试监控,${rawurl}");
        $('#checkall').click(function () {
            $('input[name=chkid]').attr("checked", this.checked)
        })
        $("#submitdel").click(function () {
            if (confirm("您确定要删除该考生的考试吗？")) {
                $("#action").val("delete");
                $("#frmpost").submit();
            }
        })
        $("#submitchange").click(function () {
            if (confirm("您确定要给所选的考生进行换位置吗？")) {
                $("#action").val("change");
                $("#frmpost").submit();
            }
        })
    })

    function ChangeExam(id) {
        if (confirm("您确定要给该考生换位置吗？")) {
            $("#action").val("change");
            $('#chkid_' + id).attr("checked", "checked");
            $("#frmpost").submit();
        }
    }
</script>
<meta http-equiv="refresh" content="20"/>
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
              <li style="background: url(images/tag.gif) 2px 6px no-repeat"><a id="submitchange" href="#">换位</a></li>
              <li style="background: url(${adminpath}/images/refresh.gif) 2px 6px no-repeat"><a href="${rawurl}">刷新</a></li>
              <li style="background: url(${adminpath}/images/return.gif) 2px 6px no-repeat"><a href="exammanage.aspx?sortid=${examinfo.sortid}">返回</a></li>
              <li style="float:right; width:auto">
              <strong>${examinfo.name}：在考人数${examresultlist.Count}人</strong>
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
              <td>座位号</td>
              <td>所在部门</td>
              <td>开始时间</td>
              <td>考试用时</td>
              <td>IP地址</td>
              <td>在线状态</td>
              <td width="60">操作</td>
            </tr>
            <%loop (ExamResult) item examresultlist %>
            <tr class="tlist" onmouseover="curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'" onmouseout="this.style.backgroundColor=curcolor">
              <td><input id="chkid_${item.id}" name="chkid" value="${item.id}" type="checkbox"/></td>
              <td>${item.IUser.username}</td>
              <td align="center">${item.IUser.realname}</td>
              <td align="center">${item.IUser.nickname}</td>
              <td align="center">${item.IUser.Department.name}</td>
              <td align="center">${fmdate(item.examdatetime,"yyyy-MM-dd HH:mm:ss")}</td>
              <td align="center">${(item.utime/60+1)}分钟</td>
              <td align="center">${item.ip}</td>
              <td>
                  <%if item.IUser.onlinestate==1 %>
                  <span style="color:#1317fc">在线</span>
                  <%else %>
                  <span style="color:#00ff21">离线</span>
                  <%/if %>
              </td>
              <td>
              <a style="color: #1317fc" href="javascript:ChangeExam(${item.id})">换位</a>
              </td>
            </tr>
            <%/loop %>
          </tbody>
        </table>
        </td>
    </tr>
  </table>
  </form>
</body>
</html>
