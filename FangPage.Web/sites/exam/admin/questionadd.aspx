<%using(FangPage.WMS.Model) %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>添加编辑题目</title>
    <link href="${adminpath}/css/masterpage.css" rel="stylesheet" type="text/css"/>
    <link href="${adminpath}/css/admin.css" rel="stylesheet" type="text/css"/>
    <%plugin(jquery)%>
    <%plugin(editor)%>
    <%plugin(layer) %>
    <script type="text/javascript" src="${adminpath}/js/admin.js"></script>
    <script type="text/javascript">
        $(function () {
            KindEditor.create('#title,#answer5,#explain', {
                resizeType: 1,
                uploadJson: '${webpath}/tools/uploadajax.aspx?sortid=${sortid}',
                fileManagerJson: '${webpath}/tools/filemanagerajax.aspx',
                newlineTag: "br",
                pasteType : 1,
                items: ['fontsize','forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
						'removeformat', '|', 'image', 'flash', 'media', '|', 'link', 'unlink'],
                afterBlur: function () { this.sync(); }
            });
            KindEditor.create('#option1_0,#option1_1,#option1_2,#option1_3,#option1_4,#option1_5,#option2_0,#option2_1,#option2_2,#option2_3,#option2_4,#option2_5', {
                resizeType: 0,
                uploadJson: '${webpath}tools/uploadajax.aspx?sortid=${sortid}',
                fileManagerJson: '${webpath}/tools/filemanagerajax.aspx',
                minWidth: "300px",
                minHeight: "80px",
                newlineTag: "br",
                pasteType: 1,
                items: ['forecolor', 'hilitecolor', 'bold', 'italic', 'underline',
						'removeformat', '|', 'image'],
                afterBlur: function () { this.sync(); }
            });
            ShowTopic();
            ShowAnswerOption(1);
            ShowAnswerOption(2);
            $("#btnsaveback").click(function () {
                $("#frmpost").submit();
            });
            $("#btnsavecontinue").click(function () {
                $("#action").val("continue");
                $("#frmpost").submit();
            });
            $("#btnback").click(function () {
                window.location.href = "${reurl}";
            });
            <%set (string){navurl}="questionmanage.aspx"%>
            PageNav("${GetSortNav(sortinfo,navurl)}|添加编辑题目,${rawurl}");
        })

        function ShowTopic() {
            var v = $("#type").val();
            if (v == 1 || v == 2) {
                $("#tr_content").show();
            }
            else {
                $("#tr_content").hide();
            }

            if (v == 4) {
                $("#tk_title").show();
            }
            else {
                $("#tk_title").hide();
            }

            if (v == 5) {
                $("#tr_answerkey").show();
            }
            else {
                $("#tr_answerkey").hide();
            }

            for (i = 1; i <= 5; i++) {
                $("#tr_answer"+ i).hide();
            }
            $("#tr_answer" + v).show();
        }

        function ShowAnswerOption(type) {
            var v = $("#ascount"+type).val();
            for (i = 2; i <= 9; i++) {
                $("#answer" + type + "_" + i).hide();
            }
            for (var i = 2; i <= v; i++) {
                $("#answer" + type + "_" + i).show();
            }
        }
    </script>
</head>
<body>
  <form name="frmpost" method="post" action="" id="frmpost">
     <input type="hidden" id="action" name="action" value="" />
     <div class="newslistabout">
     <table class="borderkuang" border="0" cellspacing="0" cellpadding="0" width="100%">
      <tbody>
        <tr>
          <td class="newstitle" bgcolor="#ffffff">添加编辑题目，所在题库：${sortinfo.name}</td>
        </tr>
      </tbody>
      </table>
      <table style="width:100%;" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"> 题目类型： </td>
        <td>
            <select id="type" name="type" onchange="ShowTopic()">
                <%if ischecked(1,examconfig.questiontype) %>
                <option value="1" <%if type==1 %> selected="selected" <%/if %> >单选题</option>
                <%/if %>
                <%if ischecked(2,examconfig.questiontype) %>
                <option value="2" <%if type==2 %> selected="selected" <%/if %> >多选题</option>
                <%/if %>
                <%if ischecked(3,examconfig.questiontype) %>
                <option value="3" <%if type==3 %> selected="selected" <%/if %> >判断题</option>
                <%/if %>
                <%if ischecked(4,examconfig.questiontype) %>
                <option value="4" <%if type==4 %> selected="selected" <%/if %> >填空题</option>
                <%/if %>
                <%if ischecked(5,examconfig.questiontype) %>
                <option value="5" <%if type==5 %> selected="selected" <%/if %> >问答题</option>
                <%/if %>
                <%if ischecked(6,examconfig.questiontype) %>
                <option value="6" <%if type==6 %> selected="selected" <%/if %> >打字题</option>
                <%/if %>
            </select>
        </td>
        </tr>
       </table>
       <table style="width:100%;" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"> 题目内容： </td>
        <td>
            <span id="tk_title" style="color:Red">
            填空题在需要用户作答的地方放(#answer)标签
            </span>
            <textarea id="title" name="title" style="width:100%;height:80px" cols="80" rows="2">${questioninfo.title}</textarea>
        </td>
        </tr>
        </table>
        <table id="tr_answer1" style="width:100%;" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"> 答案选项：<br /><br />
            <select id="ascount1" name="ascount1" onchange="ShowAnswerOption(1)" style="width:60px;margin-right:5px;">
                <option value="2" <%if ascount==2%> selected="selected" <%/if %> >2个</option>
                <option value="3" <%if ascount==3%> selected="selected" <%/if %> >3个</option>
                <option value="4" <%if ascount==4%> selected="selected" <%/if %> >4个</option>
                <option value="5" <%if ascount==5%> selected="selected" <%/if %> >5个</option>
                <option value="6" <%if ascount==6%> selected="selected" <%/if %> >6个</option>
            </select>
        </td>
        <td>
           <table cellpadding="0" cellspacing="0">
             <tr>
               <td id="answer1_1">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">A</td>
                           <td rowspan="2"><textarea id="option1_0" name="option1_0" style="height:100px;width:320px">${questioninfo.option2[0]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer1" id="answer1_a" <%if questioninfo.answer=="a" %> checked="checked" <%/if %> value="A" type="radio" />
                           </td>
                       </tr>
                   </table> 
               </td>
               <td id="answer1_2">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">B</td>
                           <td rowspan="2"><textarea id="option1_1" name="option1_1" style="height:100px;width:320px">${questioninfo.option2[1]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer1" id="answer1_b" <%if questioninfo.answer=="b" %> checked="checked" <%/if %> value="B" type="radio" />
                           </td>
                       </tr>
                   </table>
               </td>
            </tr>
            <tr>
               <td id="answer1_3">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">C</td>
                           <td rowspan="2"><textarea id="option1_2" name="option1_2" style="height:100px;width:320px">${questioninfo.option2[2]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer1" id="answer1_c" <%if questioninfo.answer=="c" %> checked="checked" <%/if %> value="C" type="radio" />
                           </td>
                       </tr>
                   </table> 
               </td>
               <td id="answer1_4">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">D</td>
                           <td rowspan="2"><textarea id="option1_3" name="option1_3" style="height:100px;width:320px">${questioninfo.option2[3]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer1" id="answer1_d" <%if questioninfo.answer=="d" %> checked="checked" <%/if %> value="D" type="radio" />
                           </td>
                       </tr>
                   </table>
               </td>
            </tr>
            <tr>
               <td id="answer1_5">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">E</td>
                           <td rowspan="2"><textarea id="option1_4" name="option1_4" style="height:100px;width:320px">${questioninfo.option2[4]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer1" id="answer1_e" <%if questioninfo.answer=="e" %> checked="checked" <%/if %> value="E" type="radio" />
                           </td>
                       </tr>
                   </table> 
               </td>
               <td id="answer1_6">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">F</td>
                           <td rowspan="2"><textarea id="option1_5" name="option1_5" style="height:100px;width:320px">${questioninfo.option2[5]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer1" id="answer1_f" <%if questioninfo.answer=="f" %> checked="checked" <%/if %> value="F" type="radio" />
                           </td>
                       </tr>
                   </table>
               </td>
            </tr>
           </table>
        </td>
        </tr>
        </table>
        <table id="tr_answer2" style="width:100%;" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"> 答案选项：<br /><br />
            <select id="ascount2" name="ascount2" onchange="ShowAnswerOption(2)" style="width:60px;margin-right:5px;">
                <option value="2" <%if ascount==2%> selected="selected" <%/if %> >2个</option>
                <option value="3" <%if ascount==3%> selected="selected" <%/if %> >3个</option>
                <option value="4" <%if ascount==4%> selected="selected" <%/if %> >4个</option>
                <option value="5" <%if ascount==5%> selected="selected" <%/if %> >5个</option>
                <option value="6" <%if ascount==6%> selected="selected" <%/if %> >6个</option>
            </select>
        </td>
        <td>
           <table cellpadding="0" cellspacing="0">
             <tr>
               <td id="answer2_1">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">A</td>
                           <td rowspan="2"><textarea id="option2_0" name="option2_0" style="height:100px;width:320px">${questioninfo.option2[0]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer2" id="answer2_a" <%if questioninfo.answer=="a" %> checked="checked" <%/if %> value="A" type="checkbox" />
                           </td>
                       </tr>
                   </table> 
               </td>
               <td id="answer2_2">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">B</td>
                           <td rowspan="2"><textarea id="option2_1" name="option2_1" style="height:100px;width:320px">${questioninfo.option2[1]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer2" id="answer2_b" <%if questioninfo.answer=="b" %> checked="checked" <%/if %> value="B" type="checkbox" />
                           </td>
                       </tr>
                   </table>
               </td>
            </tr>
            <tr>
               <td id="answer2_3">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">C</td>
                           <td rowspan="2"><textarea id="option2_2" name="option2_2" style="height:100px;width:320px">${questioninfo.option2[2]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer2" id="answer2_c" <%if questioninfo.answer=="c" %> checked="checked" <%/if %> value="C" type="checkbox" />
                           </td>
                       </tr>
                   </table> 
               </td>
               <td id="answer2_4">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">D</td>
                           <td rowspan="2"><textarea id="option2_3" name="option2_3" style="height:100px;width:320px">${questioninfo.option2[3]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer2" id="answer2_d" <%if questioninfo.answer=="d" %> checked="checked" <%/if %> value="D" type="checkbox" />
                           </td>
                       </tr>
                   </table>
               </td>
            </tr>
            <tr>
               <td id="answer2_5">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">E</td>
                           <td rowspan="2"><textarea id="option2_4" name="option2_4" style="height:100px;width:320px">${questioninfo.option2[4]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer2" id="answer2_e" <%if questioninfo.answer=="e" %> checked="checked" <%/if %> value="E" type="checkbox" />
                           </td>
                       </tr>
                   </table> 
               </td>
               <td id="answer2_6">
                   <table cellpadding="0" cellspacing="0">
                       <tr>
                           <td style="width:30px;height:25px;background-color:#64b4ff;text-align:center;color:#ffffff">F</td>
                           <td rowspan="2"><textarea id="option2_5" name="option2_5" style="height:100px;width:320px">${questioninfo.option2[5]}</textarea></td>
                       </tr>
                       <tr>
                           <td style="align-content:center;background-color:#e9cdcd;padding-left:5px;">
                               <input name="answer2" id="answer2_f" <%if questioninfo.answer=="f" %> checked="checked" <%/if %> value="F" type="checkbox" />
                           </td>
                       </tr>
                   </table>
               </td>
            </tr>
           </table>
        </td>
        </tr>
        </table>
        <table id="tr_answer3" style="width:100%;display:none" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"> 题目答案： </td>
        <td>
            <label for="answer3_y"><input type="radio" name="answer3" id="answer3_y" <%if questioninfo.answer=="y" %> checked="checked" <%/if %> value="Y" />正确</label>
            <label for="answer3_n"><input type="radio" name="answer3" id="answer3_n" <%if questioninfo.answer=="n" %> checked="checked" <%/if %> value="N" />错误</label>
        </td>
        </tr>
        </table>
        <table id="tr_answer4" style="width:100%;display:none" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"> 题目答案： </td>
        <td>
            <span style="color:Red">
            填空题如果有多个空格，各个空格的答案之间用英文逗号(,)隔开。
            <input id="upperflg" name="upperflg" <%if questioninfo.upperflg==1 %> checked="checked" <%/if %> value="1" type="checkbox" />答案区分大小写，<input id="orderflg" name="orderflg" <%if questioninfo.orderflg==1 %> checked="checked" <%/if %> value="1" type="checkbox" />答案区分顺序
            </span>
            <textarea id="answer4" name="answer4" style="width:100%;height:50px" cols="20" rows="2">${questioninfo.answer}</textarea>
        </td>
        </tr>
        </table>
        <table id="tr_answer5" style="width:100%;display:none" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"> 题目答案： </td>
        <td>
            <textarea id="answer5" name="answer5" style="width:100%;height:50px" cols="20" rows="2">${questioninfo.answer}</textarea>
        </td>
        </tr>
        </table>
        <table id="tr_answerkey" style="width:100%;display:none" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"> 答案关键词： </td>
        <td>
            <span style="color:Red">
            问答题将根据设置的关键词进行评分,多个关键字之间用英文逗号(,)隔开。
            </span>
            <textarea id="answerkey" name="answerkey" style="width:100%;height:50px" cols="20" rows="2">${questioninfo.answerkey}</textarea>
        </td>
        </tr>
        </table>
        <table style="width:100%;" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"> 答案解析： </td>
        <td>
        <textarea id="explain" name="explain" style="width:100%;height:80px" cols="20"  rows="2">${questioninfo.explain}</textarea>
        </td>
        </tr>
        </table>
        <table style="width:100%;" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"> 题目难度： </td>
        <td>
            <select id="difficulty" name="difficulty" style="width:200px;">
			    <option value="0" <%if questioninfo.difficulty==0 %> selected="selected" <%/if %> >易</option>
			    <option value="1" <%if questioninfo.difficulty==1 %> selected="selected" <%/if %> >较易</option>
			    <option value="2" <%if questioninfo.difficulty==2 %> selected="selected" <%/if %> >一般</option>
			    <option value="3" <%if questioninfo.difficulty==3 %> selected="selected" <%/if %> >较难</option>
			    <option value="4" <%if questioninfo.difficulty==4 %> selected="selected" <%/if %> >难</option>
			</select>
        </td>
        </tr>
        <tr>
        <td class="td_class" style="width:80px"> 随机题目： </td>
        <td>
            <label for="status"><input id="status" name="status" value="1" <%if questioninfo.status==1 %> checked="checked" <%/if %> type="checkbox" />是/否允许组卷时随机抽取该题目</label>
        </td>
        </tr>
        <tr>
        <td class="td_class" style="width:80px"> 清除代码： </td>
        <td>
            <label for="isclear"><input id="isclear" name="isclear" value="1" <%if questioninfo.isclear==1 %> checked="checked" <%/if %> type="checkbox" />是/否提交时清除Word多余的代码，用在从Word或网页拷贝题目时</label>
        </td>
        </tr>
        </table>
        <table style="width:100%;" cellpadding="0" cellspacing="0" class="border">
        <tr>
        <td class="td_class" style="width:80px"></td>
        <td>
        <input type="button" name="btnsaveback" value="保存返回" id="btnsaveback" class="adminsubmit_short" />
        <input type="button" name="btnsavecontinue" value="保存继续" id="btnsavecontinue" class="adminsubmit_short"/>
        <input type="button" name="btnback" value="返回" id="btnback" class="adminsubmit_short"/>
        </td>
        </tr>
       </table>
    </div>
</form>
<%include(_dopost.aspx) %>
</body>
</html>
