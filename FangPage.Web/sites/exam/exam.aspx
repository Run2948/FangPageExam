<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN" lang="zh-CN">
<head>
<meta name="renderer" content="webkit"/>
<meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"/>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<title>${examresult.examname} - ${pagetitle}</title>
<link type="text/css" rel="stylesheet" href="statics/css/exam.css"/>
<script type="text/javascript" src="statics/js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="statics/js/jquery-ui.min.js"></script>
<script type="text/javascript" src="statics/js/jquery.form.js"></script>
<script type="text/javascript" src="statics/js/popup.js"></script>
<script type="text/javascript" src="statics/js/exam.js"></script>
<script type="text/javascript" src="statics/js/jquery.nicescroll.min.js"></script>
<%if examinfo.iscopy==1 %>
<%include(_limitkey.aspx) %>
<%/if %>
<script type="text/javascript">
    function nice() {
        window.niceObj = $(".rnlt2").niceScroll({ cursorcolor: "#6E737B", cursoropacitymin: 1, cursorwidth: "6px", cursorborder: "none", cursorborderradius: "4px" });
    }

    function niceback(b) {
        if (!window.ascrail2000) return;
        if (b) {
            window.ascrail2000.css({
                "position": "fixed",
                "z-index": '9999',
                'top': '100px'
            });
        } else {
            window.ascrail2000.css({
                "position": "absolute",
                "z-index": '9999',
                'top': '170px'
            });
        }
        return true;
    }

    $(function () {
        var ipt = $("label input");
        ipt.parent().removeClass("sd");
        ipt.filter(":checked").parent().addClass("sd");
        nice();
        window.ascrail2000 = $('#ascrail2000');
        $('.rnav').mouseover(function () {
            niceback($('.hbx1').hasClass("fixed"));
        });
    });
    <%if examinfo.iscopy==1 %>
    document.oncontextmenu = new Function('event.returnValue=false;');
    document.onselectstart = new Function('event.returnValue=false;');
    <%/if%>
</script>
<script type="text/javascript">
    (window != top) && $(function () {
        var rnav = $(".rnav");
        $(window.parent).bind("scroll", function () {
            var tpd = $(parent.document.getElementById('frameContent')).offset().top;
            rnav.css({
                top: Math.max($(this).scrollTop() - tpd + 10, 52) + "px"
            });
        }).scroll();
    });
</script>
</head>
<body>
<noscript>
   <iframe src="*"></iframe>
</noscript>
<div class="hbx1">
  <div class="hbx2">
    <div class="hbx3"><img src="statics/images/top.jpg"></div>
    <div class="hbx4">
      <div class="fr"><a href="javascript:;" class="btnq1" onclick="extemporeSave()">保存答案</a><a href="#" class="btnq2" onclick="submitExam();return false;">我要交卷</a> </div>
      <span class="theTime" id="thetime">${thetime}</span>
      <%if examresult.islimit==1 %>
      <span class="line1"></span><span class="write">考试时间：${fmdate(examresult.starttime,"yyyy-MM-dd HH:mm")}至${fmdate(examresult.endtime,"yyyy-MM-dd HH:mm")}</span>
      <%else %>
      <span class="line1"></span><span class="write">答题时间：${examresult.examtime}分钟</span>
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
      <ul class="rnlt2 fc" tabindex="5000" style="overflow-y: hidden; outline: none;">
        <%set (int){en}=0  %>
        <%loop (int) examtopic questionlist %>
          <%set {en}=en+1  %>
          <li><a href="#${en}" id="fc_${en}" class="bg3">${en}</a></li>
        <%/loop %>
      </ul>
      <script type="text/javascript">
          $(function () {
              $('.rnlt2 a').click(function () {
                  var top = $($(this).attr('href')).offset().top+120;
                  $(window).scrollTop(top);
                  return false;
              });
          });
	</script>
    </div>
    <div class="rnavft"></div>
</div>
<div class="wp wpa">
  <div class="wp2">
    <div class="wp3">
      <div class="wp4 wp4_none">
        <h1 class="qtTitle">${examresult.examname}${GetPaper(examresult.paper)}</h1>
        <div class="bx1 pd1m mb20">
          <div>
            <table class="tab1">
              <tbody>
                <tr>
                  <td>考生姓名：<%if user.realname!="" %>${user.realname}<%else %>${user.username}<%/if %></td>
                  <td>试卷总分：${examresult.total}分</td>
                  <td>及格分数：${examresult.passmark}分</td>
                  <td>答题时间：${examresult.examtime}分钟</td>
                  <td>答题总数：${examresult.questions}题</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        <form id="testProcessForm" name="testProcessForm" action="exampost.aspx" method="post">
          <input type="hidden" id="resultid" name="resultid" value="${resultid}"/>
          <input type="hidden" id="starttime" value="${examresult.starttime}"/>
          <input type="hidden" id="utime" name="utime" value="0"/>
          <input type="hidden" id="autotime" value="${examconfig.autotime}"/>
          <input type="hidden" id="examtime" value="${examresult.examtime}"/>
          <input type="hidden" id="isexam" value="1"/>
            <%set (int){topicnum}=0 %>
            <%loop (ExamResultTopic) examtopic examtopiclist %>
            <%if examtopic.questions>0 %>
            <input type="hidden" id="qidlist_${examtopic.id}" name="qidlist_${examtopic.id}" value="${examtopic.questionlist}"/>
            <div id="1" class="tit1 pd1"></div>
            <div class="tit1 pd1">${examtopic.title}<span class="ft3">(共${examtopic.questions}题，每题${examtopic.perscore}分，共${(examtopic.questions*examtopic.perscore)}分)</span></div>
            <%loop (ExamQuestion) item GetQuestionList(examtopic) %>
            <%set {topicnum}=topicnum+1 %>
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
                  <%loop (string) str answerarr%>
                  <%if {str._id}<=item.ascount %>
                  <label><input type="radio" id="_${topicnum}" <%if str==item.useranswer %> checked="checked" <%/if %> name="answer_${item.id}" value="${str}"/>${str}</label>
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
                  <label><input type="checkbox" id="_${topicnum}" <%if ischecked(str,item.useranswer) %> checked="checked" <%/if %> name="answer_${item.id}" value="${str}"/>${str}</label>
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
                 <%if item.useranswer=="Y"%>
                 <label><input type="radio" id="_${topicnum}" name="answer_${item.id}" checked="checked" value="Y"/>正确</label>
                 <%else %>
                 <label><input type="radio" id="_${topicnum}" name="answer_${item.id}" value="Y"/>正确</label>
                 <%/if %>
                 <%if item.useranswer=="N"%>
                 <label><input type="radio" id="_${topicnum}" name="answer_${item.id}" checked="checked" value="N" />错误</label>
                 <%else %>
                 <label><input type="radio" id="_${topicnum}" name="answer_${item.id}" value="N"/>错误</label>
                 <%/if %>
                 </span>
              </dd>
            </dl>
            <%else if item.type==4 %>
            <%--填空题--%>
            <dl class="st tm_zt_0">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${FmAnswer(item.title,item.id,item.useranswer,topicnum)}</p>
              </dt>
            </dl>
            <%else if item.type==5 %>
            <%--问答题--%>
            <dl class="st tm_zt_${item.id}">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}</p>
              </dt>
              <dd>
                <div class="ft4">填写答案</div>
                <textarea class="jdt" rows="5" id="_${topicnum}" name="answer_${item.id}">${item.useranswer}</textarea>
              </dd>
            </dl>
            <%else if item.type==6 %>
            <%--打字题--%>
            <dl class="st tm_zt_${item.id}">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p><img src="${GetTxtImg(item.title,item.id)}" /></p>
              </dt>
              <dd>
                <div class="ft4">填写答案</div>
                <textarea class="jdt" rows="5" id="_${topicnum}" name="answer_${item.id}">${item.useranswer}</textarea>
              </dd>
            </dl>
            <%/if %>
        <%/if %>
        <%/loop %>
        <%/loop %>
        <br/>
        <div class="ta_c mb10"><a href="javascript:void()" class="btnq3" onclick="submitExam();return false;">我要交卷</a></div>
        <div style="clear:both;"></div>
      </form>
    </div>
    </div>
  </div>
</div>
</div>
</body>
</html>