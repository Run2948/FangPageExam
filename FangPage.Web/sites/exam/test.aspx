<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN" lang="zh-CN">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<title><%if testtype==1 %> 专项智能练习 <%else %> 快速智能练习 <%/if %> - ${pagetitle}</title>
<link type="text/css" rel="stylesheet" href="statics/css/exam.css"/>
<script type="text/javascript" src="statics/js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="statics/js/jquery-ui.min.js"></script>
<script type="text/javascript" src="statics/js/jquery.form.js"></script>
<script type="text/javascript" src="statics/js/popup.js"></script>
<script type="text/javascript" src="statics/js/exam.js"></script>
<script type="text/javascript" src="statics/js/jquery.nicescroll.min.js"></script>
<%include(_limitkey.aspx) %>
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
        document.oncontextmenu = new Function('event.returnValue=false;');
        document.onselectstart = new Function('event.returnValue=false;');
        nice();
        window.ascrail2000 = $('#ascrail2000');
        $('.rnav').mouseover(function () {
            niceback($('.hbx1').hasClass("fixed"));
        });
    });
</script>
</head>
<body>
<div class="hbx1">
  <div class="hbx2">
    <div class="hbx3"><img src="statics/images/top.jpg"/></div>
    <div class="hbx4">
      <div class="fr"><a href="#" class="btnq2" onclick="submitTest();return false;">我要交卷</a> </div>
      <span class="theTime" id="thetime">${thetime}</span><span class="line1"></span><span class="write">答题时间：60分钟</span></div>
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
        <%loop (ExamQuestion) item questionlist%>
          <%set {en}=en+1  %>
          <li><a href="#${en}" id="fc_${en}" class="bg3">${en}</a></li>
        <%/loop %>
      </ul>
      <script type="text/javascript">
          $(function () {
              $('.rnlt2 a').click(function () {
                  var top = $($(this).attr('href')).offset().top + 120;
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
      <div class="wp4">
        <h1 class="qtTitle"><%if testtype==1 %> 专项智能练习 <%else %> 快速智能练习 <%/if %></h1>
        <div class="bx1 pd1m mb20">
          <div>
            <table class="tab1">
              <tbody>
                <tr>
                  <td>练习用户：<%if user.realname!="" %>${user.realname}<%else %>${user.username}<%/if %></td>
                  <td>练习总分：100分</td>
                  <td>及格分数：60分</td>
                  <td>答题时间：60分钟</td>
                  <td>练习题数：${questionlist.Count}题</td>
                </tr>
              </tbody>
            </table>
          </div>
        </div>
        <form id="testProcessForm" name="testProcessForm" action="testpost.aspx" method="post">
          <input type="hidden" id="starttime" name="starttime" value="${starttime}"/>
          <input type="hidden" id="qidlist" name="qidlist" value="${qidlist}"/>
          <input type="hidden" id="utime" name="utime" value="0"/>
          <input type="hidden" id="autotime" value="0"/>
          <input type="hidden" id="examtime" value="60"/>
          <input type="hidden" id="isexam" value="1"/>
            <a id="1"></a>
            <div class="tit1 pd1"></div>
            <%set (int){perscore}=100/limit %>
            <div class="tit1 pd1"><%if testtype==1 %> 专项智能练习 <%else %> 快速智能练习 <%/if %><span class="ft3">(共${limit}题，每题${perscore}分，共100分)</span></div>
            <%set (int){topicnum}=0 %>
            <%loop (ExamQuestion) item questionlist%>
            <%set {topicnum}=topicnum+1 %>
            <%if item.type==1 %>
            <%--单选题--%>
            <dl class="st tm_zt_0">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}</p>
              </dt>
              <dd>
                ${Option(item.option,item.ascount)}
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
                <p>${item.title}</p>
              </dt>
              <dd>
                ${Option(item.option,item.ascount)}
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
                  <label><input type="radio" id="${topicnum}" name="answer_${item.id}" value="Y"/>正确</label>
                  <label><input type="radio" id="${topicnum}" name="answer_${item.id}" value="N"/>错误</label>
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
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>问答题：</p>
                <p>${item.title}</p>
              </dt>
              <dd>
                <div class="ft4">请在下面填写答案</div>
                <textarea class="jdt" id="_${topicnum}" name="answer_${item.id}"></textarea>
              </dd>
            </dl>
            <%else if item.type==6 %>
            <%--打字题--%>
            <dl class="st tm_zt_{item.id}">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>打字题：</p>
                <p>${item.title}</p>
              </dt>
              <dd>
                <div class="ft4">请在下面打字</div>
                <textarea class="jdt" id="_${topicnum}" name="answer_${item.id}"></textarea>
              </dd>
            </dl>
            <%/if %>
        <%/loop %>
        <br/>
        <div class="ta_c mb10"><a href="#" class="btnq3" onclick="submitTest();return false;">我要交卷</a></div>
        <div style="clear:both;"></div>
      </form>
    </div>
    </div>
  </div>
</div>
</div>
</body>
</html>