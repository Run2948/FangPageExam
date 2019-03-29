<%using(FangPage.WMS.#) %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>考试奖励设置</title>
    <link href="${adminpath}/css/admin.css" rel="stylesheet" type="text/css"/>
    <link href="${adminpath}/css/tab.css" rel="stylesheet" type="text/css"/>
    <link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
    <%plugin(jquery)%>
    <%plugin(layer) %>
    <script type="text/javascript" src="${adminpath}/js/admin.js"></script>
    <script type="text/javascript">
        $(function () {
            <%set (string){navurl}="exammanage.aspx"%>
            PageNav("${GetSortNav(sortinfo,navurl)}|添加编辑试卷,${rawurl}");
            $("#btnupdate").click(function () {
                $("#action").val("update");
                $("#frmpost").submit();
            })
            $("#btnadd").click(function () {
                $("#action").val("add");
                $("#frmpost").submit();
            })
            $("#btndefault").click(function () {
                $("#action").val("default");
                $("#frmpost").submit();
            })
        })
        function DeleteItem(eid) {
            if (confirm("你确定要删除吗？删除之后将无法进行恢复")) {
                $("#action").val("delete");
                $("#eid").val(eid);
                $("#frmpost").submit();
            }
        }
    </script>
</head>
<body>
  <form name="frmpost" method="post" action="" id="frmpost">
    <input type="hidden" name="eid" id="eid" value="" /> 
    <input type="hidden" name="action" id="action" value="" /> 
    <div class="ntcplist">
      <div class="ntab4">
        <div class="tabtitle">
          <ul>
            <li class="normal"><a href="examadd.aspx?id=${examid}&tabactive=1">考试设置</a> </li>
            <li class="normal"><a href="examadd.aspx?id=${examid}&tabactive=2">考试权限</a></li>
            <li class="normal"><a href="examadd.aspx?id=${examid}&tabactive=3">试卷简介</a></li>
            <li class="active"><a href="examexpmanage.aspx?examid=${examid}">奖励设置</a></li>
          </ul>
        </div>
      </div>
      <table style="background-color:white;width:100%">
        <tbody>
          <tr>
          <td>
           <table class="datalist" border="1" rules="all" cellspacing="0">
           <tbody>
            <tr class="thead">
                <td>
                  分数下限
                </td>
                <td>
                  分数上限
                </td>
                <td>
                  奖励经验值
                </td>
                <td>
                  奖励积分
                </td>
                <td>
                  评语
                </td>
                <td width="100">
                  操作
                </td>
            </tr>
            <%loop (ExpInfo) item explist %>
             <%if {item.id}==id %>
             <tr class="tlist" onmouseover="curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'" onmouseout="this.style.backgroundColor=curcolor">
                <td>
                    <input type="text" style="width:100px" name="exp_scorelower" id="exp_scorelower" value="${item.scorelower}" />
                </td>
                <td>
                    <input type="text" style="width:100px" name="exp_scoreupper" id="exp_scoreupper" value="${item.scoreupper}" />
                </td>
                <td>
                    <input type="text" style="width:100px" name="exp_exp" id="exp_exp" value="${item.exp}" />
                </td>
                <td>
                    <input type="text" style="width:100px" name="exp_credits" id="exp_credits" value="${item.credits}" />
                </td>
                <td>
                    <input type="text" style="width:200px" name="exp_comment" id="exp_comment" value="${item.comment}" />
                 </td>
                <td>
                    <a id="btnupdate" style="width: 30px; display: inline-block; color: #1317fc"  href="javascript:void()">更新</a>
                    <a style="width: 30px; display: inline-block; color: #1317fc"  href="?examid=${examid}">取消</a>
                </td>
            </tr>
            <%else %>
             <tr class="tlist" onmouseover="curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'" onmouseout="this.style.backgroundColor=curcolor">
                <td>
                    ${item.scorelower}
                </td>
                <td>
                    ${item.scoreupper}
                </td>
                <td>
                    ${item.exp}
                </td>
                <td>
                    ${item.credits}
                </td>
                <td>
                    ${item.comment}
                </td>
                <td>
                    <a style="width: 30px; display: inline-block; color: #1317fc"  href="?id=${item.id}&examid=${examid}">编辑</a>
                    <a style="width: 30px; display: inline-block; color: #1317fc"  href="javascript:DeleteItem(${item.id})">删除</a>
                </td>
             </tr>
             <%/if %>
            <%/loop %>
            <%if id==0 %>
            <tr class="tlist" onmouseover="curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'" onmouseout="this.style.backgroundColor=curcolor">
                <td>
                    <input type="text" style="width:100px" name="scorelower" id="scorelower" value="" />
                </td>
                <td>
                    <input type="text" style="width:100px" name="scoreupper" id="scoreupper" value="" />
                </td>
                <td>
                    <input type="text" style="width:100px" name="exp" id="exp" value="" />
                </td>
                <td>
                    <input type="text" style="width:100px" name="credits" id="credits" value="" />
                </td>
                <td>
                    <input type="text" style="width:200px" name="comment" id="comment" value="" />
                </td>
                <td>
                    <a id="btnadd" style="width: 30px; display: inline-block; color: #1317fc"  href="javascript:void()">添加</a>
                    <%if explist.Count==0 %>
                    <a id="btndefault" style="width: 30px; display: inline-block; color: #1317fc"  href="javascript:void()">默认</a>
                    <%/if %>
                </td>
            </tr>
            <%/if %>
            </tbody>
            </table>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
</form>
<%include(_dopost.aspx) %>
</body>
</html>
