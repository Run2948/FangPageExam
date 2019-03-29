<%using(FangPage.WMS.Model) %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>移动题目</title>
    <link href="${adminpath}/css/masterpage.css" rel="stylesheet" type="text/css"/>
    <link href="${adminpath}/css/admin.css" rel="stylesheet" type="text/css"/>
    <%plugin(jquery)%>
    <script type="text/javascript" src="${adminpath}/js/admin.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btnok").click(function () {
                $("#frmpost").submit();
            });
            $("#btnback").click(function () {
                PageBack("questionmanage.aspx?channelid=${channelid}&sortid=${sortid}");
            });
            <%set (string){navurl}="questionmanage.aspx"%>
            PageNav("${GetSortNav(sortinfo,navurl)}|移动题目,${rawurl}");
        })
    </script>
</head>
<body>
  <form name="frmpost" method="post" action="" id="frmpost">
     <input type="hidden" id="action" name="action" value="" />
     <div class="newslistabout">
     <table class="borderkuang" border="0" cellspacing="0" cellpadding="0" width="100%">
      <tbody>
        <tr>
          <td class="newstitle" bgcolor="#ffffff">移动题目，当前所在题库：${sortinfo.name}</td>
        </tr>
      </tbody>
      </table>
      <table style="width:100%;" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:150px"> 移动至目标题库： </td>
        <td>
              <%set (string){tree}="├" %>
              <select id="targetid" name="targetid" style="width: 204px">
                  <option value="0">目标题库</option>
                  <%loop (SortInfo) item sortlist %>
                  <option value="${item.id}">├${item.name}</option>
                  ${GetChildSort(item.id,tree)}
                  <%/loop %>
              </select>
        </td>
        </tr>
        </table>
        <table style="width:100%;" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"></td>
        <td>
        <input type="button" name="btnok" value="确定" id="btnok" class="adminsubmit_short" />
        <input type="button" name="btnback" value="返回" id="btnback" class="adminsubmit_short"/>
        </td>
        </tr>
       </table>
    </div>
</form>
<%include(_dopost.aspx) %>
</body>
</html>
