<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>选择用户</title>
    <%plugin(jquery)%>
    <%plugin(ztree)%>
    <link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
    <link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
    <link href="${adminpath}/css/tab.css" rel="stylesheet" type="text/css"/>
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
        function expandNode(e) {
            var zTree = $.fn.zTree.getZTreeObj("tree"),
			type = e.data.type;
            if (type == "expandall") {
                zTree.expandAll(true);
            } else if (type == "collapseall") {
                zTree.expandAll(false);
            }
        }
        $(function () {
            var h = $(window).height()-90;
            $("#table").height(h);
            $("#tree").height(h - $("#divbutton").height());
            $("#frmmaindetail").height(h - 3);
            $.fn.zTree.init($("#tree"), setting, zNodes);
            $("#btnexpandall").bind("click", {type:"expandall"}, expandNode);
            $("#btncollapseall").bind("click", {type:"collapseall"}, expandNode);
            var index = parent.layer.getFrameIndex(window.name);
            $("#btnclose").click(function () {
                parent.layer.close(index);
            });
            $("#btnok").click(function () {
                var str="";
                $("input[name='chkid']:checkbox",window.frames["frmmaindetail"].document).each(function(){ 
                    if ($(this).attr("checked")) { 
                        if(str!='')
                        {
                            str+=',';
                        }
                        str += $(this).val(); 
                    }
                })
                var examuser=parent.$('#examuser').val();
                
                if(examuser!='')
                {
                    examuser+=",";
                }
                examuser+=str;

                $.post("userexamajax.aspx", {
                    examuser: examuser,
                    status: status
                }, function (data) {
                    parent.$("#examusername").html(data.uname);
                    parent.$('#examuser').val(examuser);
                    parent.layer.close(index);
                }, "json");
            })
            $("#btnuserok").click(function () {
                var examuser=$("#examuser",window.frames["frmmaindetail"].document).val();
                $.post("userexamajax.aspx", {
                    examuser: examuser,
                    status: status
                }, function (data) {
                    parent.$("#examusername").html(data.uname);
                    parent.$('#examuser').val(examuser);
                    parent.layer.close(index);
                }, "json");
            })
        });
    </script>
</head>
<body>
    <form method="post" action="" name="frmpost" id="frmpost">
    <div id="table" class="newslistabout">
      <div class="ntab4">
        <div class="tabtitle">
          <ul>
            <%if tab==0 %> 
              <li class="active"><a href="userselect.aspx?tab=0&examuser=${examuser}">选择用户</a></li>
            <%else %>
              <li class="normal"><a href="userselect.aspx?tab=0&examuser=${examuser}">选择用户</a></li>
            <%/if %>
            <%if tab==1 %> 
              <li class="active"><a href="userselect.aspx?tab=1&examuser=${examuser}">已选用户</a></li>
            <%else %>
              <li class="normal"><a href="userselect.aspx?tab=1&examuser=${examuser}">已选用户</a></li>
            <%/if %>
          </ul>
        </div>
      </div>
      <%if tab==1 %>
      <table cellpadding="0" cellspacing="0" border="0" style="width: 499px; margin: 0px;">
        <tr>
            <td id="tdcontent" style="border: solid 1px #93C7D4; vertical-align: top;">
                <div style="padding: 2px;">
                    <iframe id="frmmaindetail" name="frmmaindetail" src="userexam.aspx?tab=${tab}&examuser=${examuser}" frameborder="0" scrolling="auto" style="width: 100%;"></iframe>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" height="25" align="right">
               <input type="button" name="btnuserok" value="确定" id="btnuserok" class="adminsubmit_short"/>
               <input type="button" name="btnclose" value="关闭" id="btnclose" class="adminsubmit_short"/>
            </td>
        </tr>
      </table>
      <%else %>
      <table cellpadding="0" cellspacing="0" border="0" style="width: 499px; margin: 0px;">
        <tr>
            <td style="width: 230px; border: solid 1px #93C7D4; vertical-align: top;">
                <div class="newslist" id="divbutton">
                <div class="newsicon"  style="width: 230px;">
                    <ul>
                        <li style="background: url(${adminpath}/images/refresh.gif) 2px 6px no-repeat;margin-left:5px;"><a href="userselect.aspx">刷新</a></li>
                        <li style="background: url(images/down.gif) 2px 6px no-repeat"><a id="btnexpandall" href="javascript:void();">展开</a></li>
                        <li style="background: url(images/up.gif) 2px 6px no-repeat"><a id="btncollapseall" href="javascript:void();">收缩</a></li>
                    </ul>
                </div>
                </div>
                <ul id="tree" class="ztree" style="width: 230px; overflow: auto;"></ul>
            </td>
            <td style="width: 2px;"></td>
            <td id="tdcontent" style="border: solid 1px #93C7D4; vertical-align: top;">
                <div style="padding: 2px;">
                    <iframe id="frmmaindetail" name="frmmaindetail" src="usersearch.aspx?tab=${tab}&examuser=${examuser}" frameborder="0" scrolling="auto" style="width: 100%;"></iframe>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3" height="25" align="right">
               <input type="button" name="btnok" value="确定" id="btnok" class="adminsubmit_short"/>
               <input type="button" name="btnclose" value="关闭" id="btnclose" class="adminsubmit_short"/>
            </td>
        </tr>
      </table>
      <%/if %>
    </div>
    </form>
</body>
</html>
