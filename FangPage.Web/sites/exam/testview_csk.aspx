<%controller(FP_Exam.Controller.testview) %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<title>强化练习 - ${pagetitle}</title>
<%plugin(jquery)%>
<%plugin(ztree)%>
<%plugin(layer) %>
<script type="text/javascript">
    var setting = {
        view: {
            dblClickExpand: true,
            showLine: true,
            selectedMulti: false
        },
        check: {
            enable: true,
            chkboxType :  { "Y" : "s", "N" : "s" }
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
    function GetCheckedAll() {
        var treeObj = $.fn.zTree.getZTreeObj("tree");
        var nodes = treeObj.getCheckedNodes(true);
        var msg = '';
        for (var i = 0; i < nodes.length; i++) {
            if(nodes[i].id!='')
            {
                if(msg!='')
                {
                    msg+=',';
                }
                msg += nodes[i].id;
            }
        }
        $("#sidlist").val();
        $("#sidlist").val(msg);
    }
    $(function () {
        $.fn.zTree.init($("#tree"), setting, zNodes);
        $("#btnsumit").click(function () {
            GetCheckedAll();
            $("#frmpost").submit();
        });
    })
</script>
<style type="text/css">
body {
	padding: 0px;
	margin: 0px auto;
	word-break: break-all;
	WORD-WRAP: break-word;
	FONT-SIZE: 12px;
	font-family: "Times New Roman", Times, serif;
	BACKGROUND: #f1f1f1;
	TEXT-ALIGN: center;
}
.head {
	height: 60px;
	background: url(statics/images/navbg.png);
	text-align: center;
	padding-top: 10px;
}
a.a:hover {
color:#000;
}
#header {
	MARGIN: 0px auto;
	HEIGHT: 60px;
}
#center-middle {
	width: 584px;
	margin: 0px auto;
	margin-top: -44px
}
.cbodywrap {
	width: 578px;
	background: #fff6b7;
	margin-left: 16px;
	_margin: 25px 0px 0px 25px;
}
#cbody {
	WIDTH: 515px;
	text-align: left;
	line-height: 28px;
	padding: 15px 15px 37px 15px;
	height: auto;
	background: url(statics/images/exambg.png) no-repeat 0 100%;
	margin-left: 16px;
	margin-top: 30px;
	_margin: 10px 0px 0px 4px;
	position: relative
}
.ptksbtn {
	width: 87px;
	height: 81px;
	background: url(statics/images/examtype2.png) no-repeat;
	position: absolute;
	left: -7px;
	top: -7px
}
.userfacemain {
	width: 101px;
	height: 101px;
	margin: 0 auto;
	position: relative;
	margin-top: 32px;
}
.userfacemain .userface {
	width: 101px;
	height: 101px;
	background: url(statics/images/userface02.png) no-repeat;
	position: absolute;
	margin-left: 18px
}
.userfacemain img {
	width: 101px;
	height: 101px;
	margin-left: 18px;
}
.IE6jiance {
	width: 650px;
	margin: 0px auto;
	margin-top: 25px;
	height: 35px;
	line-height: 35px;
	color: #de1c1c;
	font-size: 14px;
	background: url(statics/images/gth.png) no-repeat 0 4px;
	padding-left: 30px
}
.bottombg {
	width: 578px;
	height: 45px;
	background: url(statics/images/bottom.png) no-repeat;
	margin-left: 16px;
	margin-top: -6px
}
.clear {
	clear: both;
}
.blank10 {
	height: 10px;
	overflow: hidden
}
</style>
</head>
<body style="background:url(statics/images/examjt.png) no-repeat 50% 0;color:#777">
<form name="frmpost" method="post" action="test.aspx" id="frmpost" onsubmit="layer.msg('系统正在组卷，请稍后...', 0, 1);">
  <input type="hidden" id="sidlist" name="sidlist" value=""/>
  <input type="hidden" id="testtype" name="testtype" value="1"/>
  <div class="userfacemain">
    <div class="userface"></div>
    <img src="statics/images/noavatar_small.gif" onerror="this.src='statics/images/noavatar_small.gif';"></div>
  <div id="container">
    <div id="header"></div>
    <div id="center-header"></div>
    <div id="center-middle">
      <div class="cbodywrap">
        <div id="cbody">
          <div class="ptksbtn"></div>
          <table width="100%" height="163" align="center" cellpadding="0" cellspacing="1">
            <tr>
              <td height="50" colspan="2" align="center" style="background:#fffbe1"><span style="font-size:17px;text-align: center;color:#444;font-weight:bold; font-family:'微软雅黑'">强化练习</span></td>
            </tr>
            <tr>
              <td height="30" align="center" colspan="2" style="background:#fff url(statics/images/trbg.png) no-repeat 50% 100%;">
                  练习用户：${username}
              </td>
            </tr>
            <tr>
              <td width="120" height="30" align="center" style="background:#fff url(statics/images/trbg.png) no-repeat 0 100%; padding-left:20px;">练习题数：</td>
              <td align="left" style="background:#fff url(statics/images/trbg.png) no-repeat -270px 100%; padding-left:5px;border-left:1px solid #f4ecd2">
                  <input id="limit" name="limit" value="50" type="text" /></td>
            </tr>
            <tr>
              <td height="30" align="center" style=" padding-left:20px;background:#fff url(statics/images/trbg.png) no-repeat 0 100%;">练习题型：</td>
              <td align="left" style="padding-left:5px;background:#fff url(statics/images/trbg.png) no-repeat -270px 100%;border-left:1px solid #f4ecd2">
                <%if ischecked(1,examconfig.questiontype) %>  
                <input id="type" name="type" checked="checked" value="1" type="checkbox" />单选题
                <%/if %>
                <%if ischecked(2,examconfig.questiontype) %>  
                <input id="type" name="type" checked="checked" value="2" type="checkbox" />多选题
                <%/if %>
                <%if ischecked(3,examconfig.questiontype) %>  
                <input id="type" name="type" checked="checked" value="3" type="checkbox" />判断题
                <%/if %>
                <%if ischecked(4,examconfig.questiontype) %>  
                <input id="type" name="type" checked="checked" value="4" type="checkbox" />填空题
                <%/if %>
                <%if ischecked(5,examconfig.questiontype) %>  
                <input id="type" name="type" checked="checked" value="5" type="checkbox" />问答题
                <%/if %>
                <%if ischecked(6,examconfig.questiontype) %>  
                <input id="type" name="type" checked="checked" value="6" type="checkbox" />打字题
                <%/if %>
              </td>
            </tr>
            <tr>
              <td width="120" height="30" align="center" style="background:#fff url(statics/images/trbg.png) no-repeat 0 100%; padding-left:20px;">练习题库：</td>
              <td align="left" style="background:#fff url(statics/images/trbg.png) no-repeat -270px 100%; padding-left:5px;border-left:1px solid #f4ecd2">
              <div>
		       <ul id="tree" class="ztree"></ul>
	          </div>
              </td>
            </tr>
          </table>
        </div>
        <div class="clear"></div>
      </div>
      <div class="bottombg"></div>
      <div id="center-bottom"></div>
    </div>
  </div>
  <div style="text-align: center; line-height:30px;">
    <img id="btnsumit" src="statics/images/btn2.png" style="cursor:pointer;" border="0" />
  </div>
</form>
</body>
</html>