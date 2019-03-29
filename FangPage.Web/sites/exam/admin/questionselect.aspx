<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>手工选题设置</title>
    <%plugin(jquery)%>
    <%plugin(ztree)%>
    <link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
    <link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
    <script type="text/javascript" src="${adminpath}/js/admin.js"></script>
    <script type="text/javascript">
        var setting = {
            view: {
                dblClickExpand: true,
                showLine: true,
                selectedMulti: false
            },
            data: {
                simpleData: {
                    enable: true,
                    idKey: "id",
                    pIdKey: "pId",
                    rootPId: ""
                }
            }
        };
        var zNodes = [
        ${zNodes}
        ];
        $(function () {
            var h = $(window).height()-15;
            $("#table").height(h);
            $("#tree").height(h - $("#divbutton").height());
            $("#frmmaindetail").height(h - 3);
            $.fn.zTree.init($("#tree"), setting, zNodes);
            <%set (string){navurl}="exammanage.aspx"%>
            PageNav("${GetSortNav(sortinfo,navurl)}|试题设置,${rawpath}/examtopicmanage.aspx?examid=${examinfo.id}&paper=${paper}|手工选题,${rawurl}");
        });
    </script>
</head>
<body>
    <form method="post" action="" name="frmpost" id="frmpost">
        <table id="table" cellpadding="0" cellspacing="0" border="0" style="width: 100%; margin: 0px;">
            <tr>
                <td style="width: 200px; border: solid 1px #93C7D4; vertical-align: top;">
                   <div class="newslist" id="divbutton">
                    <div class="newsicon"  style="width: 200px;">
                        <ul>
                            <li style="background: url(${adminpath}/images/refresh.gif) 2px 6px no-repeat"><a href="${pagename}?examtopicid=${examtopicid}&paper=${paper}">刷新</a></li>
                            <li style="background: url(${adminpath}/images/return.gif) 2px 6px no-repeat"><a href="examtopicmanage.aspx?examid=${examinfo.id}&paper=${paper}">返回</a></li>
                        </ul>
                    </div>
                    </div>
                    <ul id="tree" class="ztree" style="width: 180px; overflow: auto;"></ul>
                </td>
                <td style="width: 2px;"></td>
                <td id="tdcontent" style="border: solid 1px #93C7D4; vertical-align: top;">
                    <div style="padding: 2px;">
                        <iframe id="frmmaindetail" name="frmmaindetail" src="examtopicselect.aspx?examtopicid=${examtopicid}&paper=${paper}" frameborder="0" scrolling="auto" style="width: 100%;"></iframe>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
