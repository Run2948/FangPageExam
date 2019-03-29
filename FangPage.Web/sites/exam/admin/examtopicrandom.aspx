<%using(FangPage.WMS.Model) %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="content-type"/>
<title>随机选题设置</title>
<link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
<link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
<%plugin(jquery)%>
<%plugin(layer) %>
<script type="text/javascript" src="${adminpath}/js/admin.js"></script>
<script type="text/javascript">
    $(function () {
        <%set (string){navurl}="exammanage.aspx"%>
        PageNav("${GetSortNav(sortinfo,navurl)}|试题设置,${rawpath}/examtopicmanage.aspx?examid=${examinfo.id}|随机选题,${rawurl}");
        $("#btnsava").click(function () {
            $("#action").val("save");
            $("#frmpost").submit();
        })
        $("#btncreate").click(function () {
            if (confirm("你确定要随机生成固定题吗？生成后将清除以下的随机设置")) {
                $("#action").val("create");
                $("#frmpost").submit();
            }
        })
    })
</script>
</head>
<body>
  <form id="frmpost" method="post" name="frmpost" action="">
  <input id="action" name="action" value="" type="hidden" />
  <table class="ntcplist" >
    <tr>
      <td>
       <div class="newslist">
          <div class="newsicon">
            <ul>
              <li style="background: url(${adminpath}/images/save.gif) 2px 6px no-repeat"><a id="btnsava" href="javascript:void()">保存随机设置</a></li>
              <li style="background: url(images/question.gif) 2px 6px no-repeat"><a id="btncreate" href="javascript:void()">随机生成固定题</a></li>
              <li style="background: url(${adminpath}/images/refresh.gif) 2px 6px no-repeat"><a href="${rawurl}">刷新</a> </li>
              <li style="background: url(${adminpath}/images/return.gif) 2px 6px no-repeat"><a href="examtopicmanage.aspx?examid=${examinfo.id}&paper=${paper}">返回</a></li>
              <li style="float:right; width:auto"><strong>随机选题：[${examinfo.name}${GetPaper(paper)}]</strong></li>
            </ul>
          </div>
        </div>
       </td>
    </tr>
    <tr>
      <td>
      <table class="datalist" border="1" rules="all" cellspacing="0">
          <tbody>
            <tr class="thead">
        	  <td align="left">${examtopic.title}(总题数<span style="color:Red">${examtopic.questions}</span>题，固定题<span style="color:Red" id="curquestions">${examtopic.curquestions}</span>题，随机题<span style="color:Red">${(examtopic.questions-examtopic.curquestions)}</span>题)</td>
              <td>固定题</td>
              <td>已设随机题<span style="color:#ff0000">${examtopic.randoms}</span>题</td>
            </tr>
            <%set (string){tree}="├" %>
            <%loop (SortInfo) sorts sortlist %>
            <%if ischecked(sorts.id,role.sorts)==false&&roleid!=1 %>
              <%continue%>
            <%/if %>
            <%set (string){hidden}="" %>
            <%if sorts.hidden==1 %>
            <%set {hidden}="_hidden" %>
            <%/if %>
            <tr class="tlist" onmouseover="curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'" onmouseout="this.style.backgroundColor=curcolor">
              <td align="left">├
              <%if {sorts.icon}!="" %>
              <img src="${sorts.icon}" width="16" height="16"  />
              <%else if {sorts.subcounts}>0 %>
              <img src="${adminpath}/images/folders${hidden}.gif" width="16" height="16"  />
              <%else %>
              <img src="${adminpath}/images/folder${hidden}.gif" width="16" height="16"  />
              <%/if %>
              ${sorts.name}(${GetQuestionCount(sorts.id)})</td>
              <td>${GetCurCount(sorts.id)}</td>
              <td>
                  <input id="randomcount_${sorts.id}" name="randomcount_${sorts.id}" value="${GetRandomCount(sorts.id)}" type="text" />  
              </td>
            </tr>
            ${ShowChildSort(sorts.id,tree)}
            <%/loop %>
          </tbody>
        </table>
        </td>
    </tr>
  </table>
</form>
<%if ispost %>
<script type="text/javascript">
    layer.msg('${msg}', 0, 1);
    var bar = 0;
    count();
    function count() {
        bar = bar + 4;
        if (bar < 80) {
            setTimeout("count()", 100);
        }
        else {
            window.location.href = "${link}";
        }
    }
</script>
<%/if %>
</body>
</html>
