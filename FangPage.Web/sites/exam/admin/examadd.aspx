<%using(FangPage.WMS.#) %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>添加编辑考试</title>
    <link href="${adminpath}/css/admin.css" rel="stylesheet" type="text/css"/>
    <link href="${adminpath}/css/tab.css" rel="stylesheet" type="text/css"/>
    <%plugin(jquery)%>
    <%plugin(ztree)%>
    <%plugin(editor)%>
    <%plugin(calendar)%>
    <%plugin(layer) %>
    <script type="text/javascript" src="${adminpath}/js/admin.js"></script>
    <script type="text/javascript">
    <!--
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
		$("#examdeparts").val();
		$("#examdeparts").val(msg);
    }

    function GetDepartName()
    {
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
                msg += nodes[i].name;
            }
        }
        $("#departname").html(msg);
    }

    $(function () {
        <%set (string){navurl}="exammanage.aspx"%>
        PageNav("${GetSortNav(sortinfo,navurl)}|添加编辑考试,${rawurl}");
        var h = 390;
        $("#table").height(h);
        $("#tree").height(h - 40);
        var zTree=$.fn.zTree.init($("#tree"), setting, zNodes);
        KindEditor.create('#content', {
            resizeType: 1,
            uploadJson: '${webpath}/tools/uploadajax.aspx?sortid=${sortid}',
            fileManagerJson: '${webpath}/tools/filemanagerajax.aspx',
            newlineTag: "br",
            items: ['fontname', 'fontsize', '|', 'forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
					'removeformat', '|', 'image', 'flash', 'media', '|', 'link', 'unlink'],
                    afterBlur: function () { this.sync(); }
        });
        $('#islimit').click(function () {
            if(this.checked==true)
            {
                $("#showlimittime").show();
            }
            else
            {
               $("#showlimittime").hide();
            }
        });
        $("input[name=btnsave]").click(function () {
            GetCheckedAll();
            $("#frmpost").submit();
        });
        $("input[name=btnback]").click(function () {
            window.location.href = "exammanage.aspx?sortid=${sortid}";
        });
        $("#btnsaveas").click(function () {
            $("#action").val("saveas");
            $("#frmpost").submit();
        });
        $("#btncleardepart").click(function () {
            zTree.checkAllNodes(false);
            $("#departname").html("");
        });
        $("#btnclearuser").click(function () {
            $("#examusername").html("");
            $("#examuser").val("");
        });
        GetDepartName();
        $.post("userexamajax.aspx", {
            examuser: '${examinfo.examuser}',
            status: status
        }, function (data) {
            $("#examusername").html(data.uname);
        }, "json");
        var index = layer.getFrameIndex(window.name);
        var index1= layer.getFrameIndex(window.name);
        $("#btndepartsearch").click(function(){
            index1=$.layer({
                type: 1,
                shade: [0],
                title: '选择考试部门',
                maxmin: false,
                area: ['500px' , '440px'],
                page: {dom : '#showdepart'}
            }); 
        });
        $('#btnclose').click(function () {
            layer.close(index1);
        });
        $('#btnok').click(function () {
            GetDepartName();
            layer.close(index1);
        });
        $('#btnexamuser').click(function(){
                index=$.layer({
                type: 2,
                shade: [0],
                fix: false,
                title: '选择考试用户',
                maxmin: false,
                iframe: {src : 'userselect.aspx?examuser='+$("#examuser").val()},
                area: ['500px' , '480px']
            }); 
        });
        $('#btninportuser').click(function(){
            index=$.layer({
                type: 2,
                shade: [0],
                fix: false,
                title: '导入考试用户',
                maxmin: false,
                iframe: {src : 'userimport.aspx?examuser='+$("#examuser").val()},
                area: ['400px' , '180px']
            }); 
        });
    })
	//-->
    </script>
</head>
<body>
  <form name="frmpost" method="post" action="" id="frmpost">
  <input id="action" name="action" value="" type="hidden" />
  <input id="tabactive" name="tabactive" value="${tabactive}" type="hidden" />
  <input id="examdeparts" type="hidden" name="examdeparts" value="" />
    <div class="newslistabout">
      <div class="ntab4">
        <div class="tabtitle">
          <ul>
            <li id="one1" onclick="setTab('one',1,3)" <%if tabactive==1 %> class="active" <%else %> class="normal" <%/if %> ><a href="#">考试设置</a> </li>
            <li id="one2" onclick="setTab('one',2,3)" <%if tabactive==2 %> class="active" <%else %> class="normal" <%/if %> ><a href="#">考试权限</a></li>
            <li id="one3" onclick="setTab('one',3,3)" <%if tabactive==3 %> class="active" <%else %> class="normal" <%/if %> ><a href="#">试卷设置</a></li>
            <%if id>0 %>
            <li class="normal"><a href="examexpmanage.aspx?examid=${id}">奖励设置</a></li>
            <%/if %>
          </ul>
        </div>
      </div>
      <div id="con_one_1" <%if tabactive!=1 %> style="display:none" <%/if %> >
      <table class="borderkuang" border="0" cellspacing="0" cellpadding="0" width="100%">
      <tbody>
        <tr>
          <td class="newstitle" bgcolor="#ffffff">考试基本设置，所在栏目：${sortinfo.name}</td>
        </tr>
      </tbody>
      </table>
      <table style="width:100%;" cellpadding="2" cellspacing="1" class="border">
        <tbody>
            <tr>
            <td class="td_class"> 考试名称： </td>
            <td><input name="name" type="text" value="${examinfo.name}" id="name" style="height:21px;width:300px;"/></td>
            </tr>
            <%if sortinfo.types!="" %>
            <tr>
            <td class="td_class"> 考试分类： </td>
            <td>
              <%loop (TypeInfo) types typelist %>
              <select id="typeid" name="typeid">
                <option value="">选择${types.name}</option>
                <%loop (TypeInfo) types2 TypeBll.GetTypeList(types.id) %>
                <option value="${types2.id}" <%if ischecked(types2.id,examinfo.typelist) %> selected="selected" <%/if %> >${types2.name}</option>
                <%/loop %>
              </select>
              <%/loop %>
            </td>
            </tr>
            <%/if %>
            <tr>
            <td class="td_class"> 及格分数： </td>
            <td><input name="passmark" type="text" value="${examinfo.passmark}" id="passmark" style="height:21px;width:300px;"/>&nbsp;%(百分比)</td>
            </tr>
            <tr>
            <td class="td_class"> 考试次数： </td>
            <td><input name="repeats" type="text" value="${examinfo.repeats}" id="repeats" style="height:21px;width:300px;"/>&nbsp;0为无限制</td>
            </tr>
            <tr>
            <td class="td_class"> 答题时间： </td>
            <td><input name="examtime" type="text" value="${examinfo.examtime}" id="examtime" style="height:21px;width:300px;"/>&nbsp;分钟</td>
            </tr>
            <tr>
            <td class="td_class"> 考试时间： </td>
            <td>
                <input id="islimit" name="islimit" value="1" <%if examinfo.islimit==1 %> checked="checked" <%/if %> type="checkbox" />是/否使用考试时间限制
            </td>
            </tr>
            <tr id="showlimittime" <%if examinfo.islimit!=1 %> style="display: none" <%/if %> >
            <td class="td_class" > 考试时间设置： </td>
            <td>
                <input name="starttime" type="text" value='${fmdate(examinfo.starttime,"yyyy-MM-dd HH:mm")}' id="starttime" onfocus="WdatePicker({el:'starttime',dateFmt:'yyyy-MM-dd HH:mm'})" readonly="readonly" style="height:21px;width:120px;"/>至
                <input name="endtime" type="text" value='${fmdate(examinfo.endtime,"yyyy-MM-dd HH:mm")}' id="endtime" onfocus="WdatePicker({el:'endtime',dateFmt:'yyyy-MM-dd HH:mm'})" readonly="readonly" style="height:21px;width:120px;"/>
            </td>
            </tr>
            <tr>
            <td class="td_class"> 查看答案： </td>
            <td>
              <input id="showanswer" name="showanswer" value="1" <%if examinfo.showanswer==1 %> checked="checked" <%/if %> type="checkbox" />是/否允许用户提交后查看考试的答案
            </td>
            </tr>
            <tr>
            <td class="td_class"> 考试记录： </td>
            <td>
              <input id="allowdelete" name="allowdelete" value="1" <%if examinfo.allowdelete==1 %> checked="checked" <%/if %> type="checkbox" />是/否允许考试完成之后删除考试记录
            </td>
            </tr>
            <tr>
            <td class="td_class"> 固定位置： </td>
            <td>
              <input id="examtype" name="examtype" value="1" <%if examinfo.examtype==1 %> checked="checked" <%/if %> type="checkbox" />是/否固定位置考试，只支持在局域网的考试
            </td>
            </tr>
            <tr>
            <td class="td_class"> 页面复制： </td>
            <td>
              <input id="iscopy" name="iscopy" value="1" <%if examinfo.iscopy==1 %> checked="checked" <%/if %> type="checkbox" />是/否允许考试页面右键复制功能
            </td>
            </tr>
            <tr>
            <td class="td_class"> 题目排序： </td>
            <td>
                <input id="display" name="display" value="0" <%if examinfo.display==0 %> checked="checked" <%/if %> type="radio" />随机排序
                <input id="display" name="display" value="1" <%if examinfo.display==1 %> checked="checked" <%/if %> type="radio" />固定排序
            </td>
            </tr>
            <tr>
            <td class="td_class"> 选项排序： </td>
            <td>
                <input id="optiondisplay" name="optiondisplay" value="0" <%if examinfo.optiondisplay==0 %> checked="checked" <%/if %> type="radio" />随机排序
                <input id="optiondisplay" name="optiondisplay" value="1" <%if examinfo.optiondisplay==1 %> checked="checked" <%/if %> type="radio" />固定排序
            </td>
            </tr>
            <tr>
            <td class="td_class"> 考试状态： </td>
            <td>
               <input id="status" name="status" value="1" <%if examinfo.status==1 %> checked="checked" <%/if %> type="radio" />开启
               <input id="status" name="status" value="0" <%if examinfo.status==0 %> checked="checked" <%/if %> type="radio" />关闭
            </td>
            </tr>
            <tr>
            <td class="td_class"></td>
            <td height="25">
            <input type="button" name="btnsave" value="保存" id="btnsave1" class="adminsubmit_short"/>
            <%if id>0 %>
            <input type="button" name="btnsaveas" value="另存为" id="btnsaveas" class="adminsubmit_short"/>
            <%/if %>
            <input type="button" name="btnback" value="返回" id="btnback1" class="adminsubmit_short"/>
            </td>
            </tr>
        </tbody>
      </table>
      </div>
      <div id="con_one_2" <%if tabactive!=2 %> style="display:none" <%/if %> >
      <table class="borderkuang" border="0" cellspacing="0" cellpadding="0" width="100%">
      <tbody>
        <tr>
          <td class="newstitle" bgcolor="#ffffff">考试权限设置，所在栏目：${sortinfo.name}</td>
        </tr>
      </tbody>
      </table>
      <table style="width:100%;" cellpadding="2" cellspacing="1" class="border">
        <tbody>
        <tr>
        <td class="td_class"> 使用积分： </td>
        <td><input name="credits" type="text" value="${examinfo.credits}" id="credits" style="height:21px;width:300px;"/></td>
        </tr>
        <tr>
        <td class="td_class"> 考试角色： </td>
        <td>
        <%loop (RoleInfo) roles rolelist %>
        <%if roles.id>=5 %>
        <input id="examroles" name="examroles" value="${roles.id}" <%if ischecked(roles.id,examinfo.examroles) %> checked="checked" <%/if %> type="checkbox" />${roles.name}
        <%/if %>
        <%/loop %>
        </td>
        </tr>
        <tr>
        <td class="td_class" valign="top"> 考试部门： </td>
        <td valign="top">
        <span id="departname"></span><br />
        <input type="button" name="btndepartsearch" value="选择部门" id="btndepartsearch" class="adminsubmit_short"/>
        <input type="button" name="btncleardepart" value="清空" id="btncleardepart" class="adminsubmit_short"/>
        </td>
        </tr>
        <tr>
        <td class="td_class"> 考试用户： </td>
        <td>
          <span id="examusername"></span><br />
          <input id="examuser" name="examuser" value="${examinfo.examuser}" type="hidden"/>
          <input type="button" name="btnexamuser" value="选择用户" id="btnexamuser" class="adminsubmit_short"/>
          <input type="button" name="btninportuser" value="导入用户" id="btninportuser" class="adminsubmit_short"/>
          <input type="button" name="btnclearuser" value="清空" id="btnclearuser" class="adminsubmit_short"/>
        </td>
        </tr>
        <tr>
            <td class="td_class"></td>
            <td height="25">
            <input type="button" name="btnsave" value="保存" id="btnsave2" class="adminsubmit_short"/>
            <input type="button" name="btnback" value="返回" id="btnback2" class="adminsubmit_short"/>
            </td>
        </tr>
        </tbody>
       </table>
      </div>
      <div id="con_one_3" <%if tabactive!=3 %> style="display:none" <%/if %> >
      <table style="width:100%" class="borderkuang" border="0" cellspacing="0" cellpadding="0">
      <tbody>
        <tr>
          <td class="newstitle" bgcolor="#ffffff">试卷简介，所在栏目：${sortinfo.name}</td>
        </tr>
      </tbody>
      </table>
      <table style="width:100%;" cellpadding="2" cellspacing="1" class="border">
        <tbody>
        <tr>
        <td class="td_class"> 试卷标题： </td>
        <td><input name="description" type="text" value="${examinfo.description}" id="description" style="height:21px;width:400px;"/></td>
        </tr>
        <tr>
        <td class="td_class"> 试卷说明： </td>
        <td>
         <textarea id="content" name="content" style="width:100%; height:300px" cols="20" rows="2">${examinfo.content}</textarea>
        </td>
        </tr>
        <tr>
            <td class="td_class"></td>
            <td height="25">
            <input type="button" name="btnsave" value="保存" id="btnsave3" class="adminsubmit_short"/>
            <input type="button" name="btnback" value="返回" id="btnback3" class="adminsubmit_short"/>
            </td>
         </tr>
        </tbody>
       </table>
      </div>
    </div>
    <div id="showdepart" style="display:none">
     <table id="table" cellpadding="0" cellspacing="0" border="0" style="width: 485px; margin: 0px;">
        <tr>
            <td style="width: 485px; border: solid 1px #93C7D4; vertical-align: top;">
                <ul id="tree" class="ztree" style="width: 485px; overflow: auto;"></ul>
            </td>
            </tr>
            <tr>
            <td height="25" align="right">
            <input type="button" name="btnok" value="确定" id="btnok" class="adminsubmit_short"/>
            <input type="button" name="btnclose" value="关闭" id="btnclose" class="adminsubmit_short"/>
            </td>
            </tr>
      </table>
	</div>
</form>
<%include(_dopost.aspx) %>
</body>
</html>
