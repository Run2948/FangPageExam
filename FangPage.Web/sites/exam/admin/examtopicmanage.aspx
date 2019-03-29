<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta content="text/html; charset=utf-8" http-equiv="content-type"/>
<title>试卷题目设置</title>
<link rel="stylesheet" type="text/css" href="${adminpath}/css/admin.css"/>
<link href="${adminpath}/css/datagrid.css" rel="stylesheet" type="text/css"/>
<link href="css/exam.css" rel="stylesheet" type="text/css"/>
<link rel="stylesheet" type="text/css" href="${adminpath}/css/tab.css"/>
<%plugin(jquery)%>
<%plugin(layer) %>
<script type="text/javascript" src="${adminpath}/js/admin.js"></script>
<script type="text/javascript">
    $(function () {
        <%set (string){navurl}="exammanage.aspx"%>
        PageNav("${GetSortNav(sortinfo,navurl)}|${examinfo.name},${rawurl}");
        $('#checkall').click(function () {
            $('input[name=chkid]').attr("checked", this.checked)
        });
        $('.sprite').click(function () {   // 获取所谓的父行
            $(this).toggleClass("sprite-selected");  // 添加/删除图标
            $('.child_' + $(this).attr('id')).toggle();  // 隐藏/显示所谓的子行
        });
        $('.sprite-selected').click(function () {   // 获取所谓的父行
            if ($(this).attr('class') == 'sprite-selected')
            {
                $(this).attr('class','sprite');
            }
            else
            {
                $(this).attr('class', 'sprite-selected');
            }
            $('.child_' + $(this).attr('id')).toggle();  // 隐藏/显示所谓的子行
        });
        $('#btndisplay').click(function () {
            $("#action").val("display");
            $("#frmpost").submit();
        });
        $('#btnaddpaper').click(function () {
            $("#action").val("addpaper");
            $("#frmpost").submit();
        });
        $('#submitdel').click(function () {
            if (confirm("你确定要删除该份试卷吗？删除之后将无法进行恢复")) {
                $("#action").val("delpaper");
                $("#frmpost").submit();
            }
        });
        $('#btnsaveas').click(function () {
            $("#action").val("saveas");
            $("#frmpost").submit();
        });
        var index = layer.getFrameIndex(window.name);
        $('#outputpaper').click(function () {
            index = $.layer({
                type: 2,
                shade: [0],
                fix: false,
                title: '导出试卷',
                maxmin: false,
                iframe: { src: 'outputpaper.aspx?examid=${examid}&paper=${paper}'},
                area: ['485px', '185px']
            });
        });
    })
    function DeleteExamTopic(examtopicid) {
        if (confirm("您确定要删除该大题吗？删除之后将无法进行恢复")) {
            $("#action").val("delete");
            $("#examtopicid").val(examtopicid);
            $("#frmpost").submit();
        }
    }
    function DeleteTopic(examtopicid,tid) {
        if (confirm("您确定要从试卷中取消加入该试题吗？")) {
            $("#action").val("deletetopic");
            $("#examtopicid").val(examtopicid);
            $("#tid").val(tid);
            $("#frmpost").submit();
        }
    }
</script>
</head>
<body>
  <form id="frmpost" method="post" name="frmpost" action="">
  <input type="hidden" name="action" id="action" value=""/>
  <input type="hidden" name="examtopicid" id="examtopicid" value=""/>
  <input type="hidden" name="tid" id="tid" value=""/>
  <div class="ntab4">
        <div class="tabtitle">
          <ul id="mytab1">
            <%if examinfo.papers>=1 %>
              <%if paper==1 %>
              <li class="active"><a href="?examid=${examid}&paper=1">A卷</a> </li>
              <%else %>
              <li class="normal"><a href="?examid=${examid}&paper=1">A卷</a> </li>
              <%/if %>
            <%/if %>
            <%if examinfo.papers>=2 %>
              <%if paper==2 %>
              <li class="active"><a href="?examid=${examid}&paper=2">B卷</a> </li>
              <%else %>
              <li class="normal"><a href="?examid=${examid}&paper=2">B卷</a> </li>
              <%/if %>
            <%/if %>
            <%if examinfo.papers>=3 %>
              <%if paper==3 %>
              <li class="active"><a href="?examid=${examid}&paper=3">C卷</a> </li>
              <%else %>
              <li class="normal"><a href="?examid=${examid}&paper=3">C卷</a> </li>
              <%/if %>
            <%/if %>
            <%if examinfo.papers>=4 %>
              <%if paper==4 %>
              <li class="active"><a href="?examid=${examid}&paper=4">D卷</a> </li>
              <%else %>
              <li class="normal"><a href="?examid=${examid}&paper=4">D卷</a> </li>
              <%/if %>
            <%/if %>
            <%if examinfo.papers<4 %>
            <li class="normal"><a id="btnaddpaper" href="javascript:void();">添加试卷</a> </li>
            <%/if %>
          </ul>
      </div>
  </div>
  <table class="ntcplist">
    <tr>
      <td>
      <div class="newslist">
          <div class="newsicon">
            <ul>
              <li style="background: url(${adminpath}/images/delete.gif) 2px 6px no-repeat"><a id="submitdel" href="#">删除试卷</a></li>
              <li style="background: url(${adminpath}/images/add.gif) 2px 6px no-repeat"><a href="examtopicadd.aspx?examid=${examid}&paper=${paper}">添加大题</a></li>
              <li style="background: url(${adminpath}/images/save.gif) 2px 6px no-repeat"><a id="btnsaveas" href="javascript:void();">另存为</a></li>
              <li style="background: url(images/display.gif) 2px 6px no-repeat"><a id="btndisplay" href="javascript:void();">保存排序</a></li>
              <li style="background: url(images/report.png) 2px 6px no-repeat"><a href="../exampreview.aspx?examid=${examid}&paper=${paper}" target="_blank">试卷预览</a></li>
              <li style="background: url(images/down.gif) 2px 6px no-repeat"><a id="outputpaper" href="#">导出试卷</a></li>
              <li style="background: url(${adminpath}/images/refresh.gif) 2px 6px no-repeat"><a href="${pagename}?examid=${examid}&paper=${paper}">刷新</a></li>
              <li style="background: url(${adminpath}/images/return.gif) 2px 6px no-repeat"><a href="exammanage.aspx?sortid=${sortid}">返回</a></li>
              <li style="float:right; width:auto">
               <strong>总分：<span id="total" style="color:Red">100</span>分，总题目数：<span style="color:Red">${examinfo.questions}</span>题</strong>
              </li>
            </ul>
          </div>
        </div>
      </td>
    </tr>
    </table>
    <table class="ntcplist">
    <tr>
      <td>
      <table class="datalist" border="1" rules="all" cellspacing="0">
          <tbody>
            <%set (double){total}=0 %>
            <%loop (ExamTopic) examtopic examtopiclist %>
            <tr style="color:#1317fc;" class="thead">
              <td width="40" align="center" ><div id="row_${examtopic.id}" <%if examtopic.id==examtopicid %> class="sprite-selected" <%else %> class="sprite" <%/if %>></div></td>
        	  <td align="left" valign="middle">
                 ${examtopic.title}(总题数<span style="color:Red">${examtopic.questions}</span>题，固定题<span style="color:Red">${examtopic.curquestions}</span>题，随机题<span style="color:Red">${(examtopic.questions-examtopic.curquestions)}</span>题，每题<span style="color:Red">${examtopic.perscore}</span>分，共<span style="color:Red">${(examtopic.perscore*examtopic.questions)}</span>分)
              </td>
              <td width="80">所在题库</td>
              <td width="180">
              <a style="color:#1317fc;" href="examtopicadd.aspx?id=${examtopic.id}&paper=${paper}">编辑</a>
              <a style="color:#1317fc;" href="javascript:DeleteExamTopic(${examtopic.id})">删除</a>
              <a style="color:#1317fc;" href="questionselect.aspx?examtopicid=${examtopic.id}&paper=${paper}">手工选题</a>
              <a style="color:#1317fc;" href="examtopicrandom.aspx?examtopicid=${examtopic.id}&paper=${paper}">随机选题</a>
              </td>
            </tr>
            <%set {total}=total+examtopic.perscore*examtopic.questions %>
            <%set (int){topicnum}=0 %>
            <%loop (ExamQuestion) item QuestionBll.GetQuestionList(examtopic.questionlist) %>
            <%set {topicnum}=topicnum+1 %>
            <tr class="tlist child_row_${examtopic.id}" <%if examtopic.id!=examtopicid %> style="display:none;" <%/if %> onmouseover="curcolor=this.style.backgroundColor;this.style.backgroundColor='#cbe3f4'" onmouseout="this.style.backgroundColor=curcolor">
              <td width="40" align="center" >
                  <input id="display_${item.id}" name="display_${item.id}" style="text-align:center;width:36px;" value="${topicnum}" type="text" />
              </td>
              <td align="left">
              <strong>
                      ${TypeStr(item.type)}：
                      ${FmAnswer(item.title)}
              </strong>
              <%if item.type==1||item.type==2 %>
              <div style="height: 2px; overflow: hidden;"></div>
              ${Option(item.option,item.ascount)}
              <%/if %>
              <div style="height: 5px; overflow: hidden; border-bottom-color: rgb(204, 204, 204); border-bottom-width: 1px; border-bottom-style: dashed;"></div>
              <div class="tips">
              <%if item.type!=6 %>
              <div style="color:Red">
              <%if item.type==3 %>
                 <%if item.answer=="Y" %>
                 参考答案：正确
                 <%else if item.answer=="N"%>
                 参考答案：错误
                 <%/if %>
              <%else %>
                 参考答案：${item.answer}
              <%/if %>
              </div>
              <%/if %>     
              <span style="color:Red">难易程度：${DifficultyStr(item.difficulty)}，考过次数：${item.exams}，做错次数：${item.wrongs}</span><br />
              <%if {item.explain}!="" %>
              <span style="color:Red">答案解析：${item.explain}</span> 
              <%else %>
              <span style="color:Red">答案解析：暂无解析</span>
              <%/if %>
              </div>
              </td>
              <td align="center">${item.SortInfo.name}</td>
              <td>
              <a style="color: #1317fc" href="questionadd.aspx?id=${item.id}&examid=${examid}&examtopicid=${examtopic.id}">编辑试题</a>&nbsp;&nbsp;
              <a style="color: #1317fc" href="javascript:DeleteTopic(${examtopic.id},${item.id})">取消加入</a>
              </td>
            </tr>
            <%/loop %>
            <%/loop %>
          </tbody>
          <script type="text/javascript">
              $("#total").html('${total}');
          </script>
        </table>
        </td>
    </tr>
  </table>
  </form>
</body>
</html>
