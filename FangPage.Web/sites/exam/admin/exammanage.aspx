<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="content-type"/>
<title>考试试卷管理</title>
<link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
<link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
<%plugin(jquery)%>
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
        $("#submitsum").click(function () {
            if (confirm("你确定要重新所选试卷的统计吗？")) {
                $("#action").val("sum");
                $("#frmpost").submit();
            }
        })
    })
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
              <li style="background: url(images/create.gif) 2px 6px no-repeat"><a href="examadd.aspx?sortid=${sortid}&typeid=${typeid}">添加</a></li>
              <li style="background: url(images/tag.gif) 2px 6px no-repeat"><a id="submitsum" href="#">重新统计</a></li>
              <li style="background: url(images/report.png) 2px 6px no-repeat"><a id="btnsearch" href="examsearch.aspx?sortid=${sortid}&typeid=${typeid}">搜索</a></li>
              <li style="background: url(${adminpath}/images/refresh.gif) 2px 6px no-repeat"><a href="exammanage.aspx?sortid=${sortid}&typeid=${typeid}">刷新</a></li>
              <li style="float:right; width:auto">
              <strong>
              ${sortinfo.name}<%if typeid>0 %> >${typeinfo.name}<%/if %>
              ：共有${pager.total}场考试</strong>
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
        	  <td>考试名称</td>
        	  <td>所在栏目</td>
              <td>考试时间</td>
              <td width="60">考试人数</td>
              <td width="60">总平均分</td>
              <td width="40">状态</td>
              <td width="220">考试操作</td>
            </tr>
            <%loop (ExamInfo) item examlist %>
            <tr class="tlist" onmouseover="curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'" onmouseout="this.style.backgroundColor=curcolor">
              <td><input id="chkid_${item.id}" name="chkid" value="${item.id}" type="checkbox"/></td>
              <td align="left">
              <a href="examadd.aspx?id=${item.id}">${item.name}</a>
              </td>
              <td align="center">${item.SortInfo.name}</td>
              <td align="center">
                  <%if item.islimit==1  %>
                  ${fmdate(item.starttime,"yyyy-MM-dd HH:mm")}至${fmdate(item.endtime,"yyyy-MM-dd HH:mm")}
                  <%else %>
                  无限制
                  <%/if %>
              </td>
              <td align="center">${item.exams}</td>
              <td align="center">${item.avgscore}</td>
              <td align="center">
              <%if {item.status}==1 %>
              <img src="images/state1.gif" alt="已开启" title="已开启" />
              <%else %>
              <img src="images/state0.gif" alt="已关闭" title="已关闭" />
              <%/if %>
              </td>
              <td>
              <a style="color: #1317fc" href="examadd.aspx?id=${item.id}">考试设置</a>
              <a style="color: #1317fc" href="examtopicmanage.aspx?examid=${item.id}">试题设置</a>
              <a style="color: #1317fc" href="exammonitor.aspx?examid=${item.id}">考试监控</a>
              <a style="color: #1317fc" href="examresultmanage.aspx?examid=${item.id}">成绩管理</a>
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
</body>
</html>
