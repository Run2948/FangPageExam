<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN" lang="zh-CN">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<title>试卷[${examinfo.name}${GetPaper(paper)}]预览 - ${pagetitle}</title>
<link type="text/css" rel="stylesheet" href="statics/css/exam.css"/>
<link href="${adminpath}/css/admin.css" rel="stylesheet" type="text/css"/>
<script type="text/javascript" src="statics/js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="statics/js/jquery-ui.min.js"></script>
<script type="text/javascript" src="statics/js/jquery.form.js"></script>
<script type="text/javascript" src="statics/js/popup.js"></script>
<script type="text/javascript" src="statics/js/exam.js"></script>
<%plugin(layer) %>
<%include(_limitkey.aspx) %>
<script type="text/javascript">
    $(function () {
        var index = layer.getFrameIndex(window.name);
        $('#importpaper').click(function () {
            index = $.layer({
                type: 1,
                shade: [0],
                fix: false,
                title: '导出试卷',
                maxmin: false,
                page: { dom: '#importpage' },
                area: ['485px', '185px']
            });
        });
        $("#btnclose").click(function () {
            layer.close(index);
        });
        $("#btnuserok").click(function () {
            $("#testProcessForm").submit();
        })
    });
</script>
</head>
<body>
<form id="testProcessForm" name="testProcessForm" action="" method="post">
<div class="hbx1">
  <div class="hbx2">
    <div class="hbx3"><img src="statics/images/top.jpg"></div>
    <div class="hbx4">
      <div class="fr"><a id="importpaper" href="javascript:void();" class="btnq2">导出试卷</a></div>
      <span class="theTime" id="thetime">00:00:00</span><span class="line1"></span><span class="write">答题时间：${examinfo.examtime}分钟</span>
      <%if examinfo.islimit==1 %>
      <span class="line1"></span>考试期限：${fmdate(examinfo.starttime,"yyyy-MM-dd HH:mm")}~${fmdate(examinfo.endtime,"yyyy-MM-dd HH:mm")}
      <%/if %>
    </div>
  </div>
</div>
<div class="hbx2">
<div class="rnav">
    <div class="rnavhd">答题卡</div>
    <div class="rnavct">
      <ul class="rnlt1 fc">
        <li><span class="bg1"></span>已答题</li>
        <li><span class="bg3"></span>未答题</li>
      </ul>
      <ul class="rnlt2 fc">
        <%set (int){en}=0  %>
        <%loop (int) examtopic questionlist %>
          <%set {en}=en+1  %>
          <li><a href="#${en}" id="fc_${en}" class="bg3">${en}</a></li>
        <%/loop %>
      </ul>
    </div>
    <div class="rnavft"></div>
  </div>
<div class="wp wpa">
  <div class="wp2">
    <div class="wp3">
      <div class="wp4">
        <h1 class="qtTitle">${examinfo.name}${GetPaper(paper)}</h1>
        <div class="tit1 pd1">考试说明</div>
        <div class="bx1 pd1m mb20">
          <div>
            <table class="tab1">
              <tbody>
                <tr>
                  <td>考试用户：<%if user.realname!="" %>${user.realname}<%else %>${user.username}<%/if %></td>
                  <td>试卷总分：${examinfo.total}分</td>
                  <td>及格分数：${examinfo.passmark}分</td>
                  <td>答题时间：${examinfo.examtime}分钟</td>
                  <td>考试题数：${examinfo.questions}题</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
          <input type="hidden" id="examid" name="examid" value="${examid}"/>
          <input type="hidden" id="starttime" name="starttime" value="${starttime}"/>
          <input type="hidden" id="utime" name="utime" value="0"/>
          <input type="hidden" id="autotime" value="0"/>
          <input type="hidden" id="examtime" value="0"/>
            <%set (int){topicnum}=0 %>
            <%loop (ExamTopic) examtopic examtopiclist %>
            <%set (string){qidlist}="" %>
            <%if examtopic.questions>0 %>
            <div id="1" class="tit1 pd1"></div>
            <div class="tit1 pd1">${examtopic.title}<span class="ft3">(共${examtopic.questions}题，每题${examtopic.perscore}分，共${(examtopic.questions*examtopic.perscore)}分)</span></div>
            <%loop (ExamQuestion) item GetQuestionList(examtopic.questionlist) %>
            <%set {topicnum}=topicnum+1 %>
            <%if qidlist!="" %>
            <%set {qidlist}={qidlist}+"," %>
            <%/if %>
            <%set {qidlist}={qidlist}+item.id %>
            <%if item.type==1 %>
            <%--单选题--%>
            <dl class="st tm_zt_0">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}<%if examtopic.type==6 %>（单选）<%/if %></p>
              </dt>
              <dd>
                ${Option(item.option,item.ascount,item.optionlist)}
              </dd>
              <dd class="dAn fc">
                <span class="ft4 fl">选择答案：</span>
                 <span class="fl w2 bx7">
                  <%loop (string) str answerarr %>
                  <%if {str._id}<=item.ascount %>
                  <label><input type="radio" id="_${topicnum}" name="answer_${item.id}" value="${str}"/>${str}</label>
                  <%/if %>
                  <%/loop%>
                  </span>
              </dd>
            </dl>
            <%else if item.type==2 %>
            <%--多选题--%>
            <dl class="st tm_zt_0">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}<%if examtopic.type==6 %>（多选）<%/if %></p>
              </dt>
              <dd>
                ${Option(item.option,item.ascount,item.optionlist)}
              </dd>
              <dd class="dAn fc">
                <span class="ft4 fl">选择答案：</span>
                 <span class="fl w2 bx7">
                  <%loop (string) str answerarr %>
                  <%if {str._id}<=item.ascount %>
                  <label><input type="checkbox" id="_${topicnum}" name="answer_${item.id}" value="${str}"/>${str}</label>
                  <%/if %>
                  <%/loop%>
                  </span>
              </dd>
            </dl>
            <%else if item.type==3 %>
            <%--判断题--%>
            <dl class="st tm_zt_0">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}</p>
              </dt>
              <dd class="dAn fc">
                <span class="ft4 fl">选择答案：</span>
                 <span class="fl w2 bx7">
                  <label><input type="radio" id="_${topicnum}" name="answer_${item.id}" value="Y"/>正确</label>
                  <label><input type="radio" id="_${topicnum}" name="answer_${item.id}" value="N"/>错误</label>
                  </span>
              </dd>
            </dl>
            <%else if item.type==4 %>
            <%--填空题--%>
            <dl class="st tm_zt_0">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${FmAnswer(item.title,item.id,topicnum)}</p>
              </dt>
            </dl>
            <%else if item.type==5 %>
            <%--问答题--%>
            <dl class="st tm_zt_${item.id}">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">{topicnum}</span>
                <p>${item.title}</p>
              </dt>
              <dd>
                <div class="ft4">填写答案</div>
                <textarea class="jdt" id="_${topicnum}" name="answer_${item.id}"></textarea>
              </dd>
            </dl>
            <%else if item.type==6 %>
            <%--打字题--%>
            <dl class="st tm_zt_${item.id}">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}</p>
              </dt>
              <dd>
                <div class="ft4">填写答案</div>
                <textarea class="jdt" id="_${topicnum}" name="answer_4${item.id}"></textarea>
              </dd>
            </dl>
            <%/if %>
        <%/if %>
        <%/loop %>
        <input id="qidlist_${examtopic.id}" name="qidlist_${examtopic.id}" value="${qidlist}" type="hidden" />
        <%/loop %>
        <div style="clear:both;"></div>
    </div>
    </div>
  </div>
</div>
</div>
<div id="importpage" style="display:none;">
    <table cellpadding="0" cellspacing="0" border="0" style="width: 483px;height:150px; margin: 0px;">
        <tr>
            <td colspan="3" style="border: solid 1px #93C7D4; vertical-align:middle;height:100px;padding-left:10px;">
               <table colspan="3" style="height:40px;">
                   <tr>
                    <td style="width:70px;height:50px;">试卷纸张：</td>
                    <td>
                       <input id="papersize" name="papersize" value="a4" checked="checked" type="radio" />A4&nbsp;&nbsp;&nbsp;&nbsp;<input id="papersize" name="papersize" value="a3" type="radio" />A3
                    </td>
                </tr>
                <tr>
                    <td style="width:70px;">试卷类型：</td>
                    <td>
                       <input id="papertype" name="papertype" type="radio" checked="checked" value="0" />学生用卷（答案集中在卷尾）&nbsp;&nbsp;&nbsp;&nbsp;<input id="papertype" name="papertype" value="1" type="radio" />教师用卷（每题后面跟答案）<br />
                    </td>
                </tr>
               </table>
            </td>
        </tr>
        <tr>
            <td colspan="3" height="25" align="right">
               <input type="button" name="btnuserok" value="下载" id="btnuserok" class="adminsubmit_short"/>
               <input type="button" name="btnclose" value="关闭" id="btnclose" class="adminsubmit_short"/>
            </td>
        </tr>
      </table>
</div>
</form>
</body>
</html>