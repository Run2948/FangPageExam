<!DOCTYPE html>
<html lang="zh-CN" class="default-layout">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"/>
<title>考试历史 - ${pagetitle}</title>
<link rel="stylesheet" href="statics/css/report.css"/>
<%include(_iehack.aspx) %>
<%plugin(jquery) %>
<script type="text/javascript">
    function ExamContinu(id) {
        window.open("exam.aspx?resultid=" + id);
    }
</script>
<!--[if gte IE 9]>
  <style type="text/css">
    .gradient {
       filter: none;
    }
  </style>
<![endif]-->
</head>
<body>
<div class="wrap">
  <%include(_header.aspx) %>
  <div class="container body-wrap main">
    <div class="box-wrap history-wrap">
      <div class="box">
        <div class="box-hd">
          <ul class="nav nav-underline">
            <li class="active"><a href="examhistory.aspx">考试记录</a></li>
            <li><a href="incorrect.aspx">我的错题</a></li>
            <li><a href="examnote.aspx">笔记题目</a></li>
            <li><a href="favorite.aspx">我的收藏</a></li>
          </ul>
        </div>
        <div class="box-bd">
          <div class="exercise-list-wrap list-wrap">
            <div class="list">
              <div class="list-bd">
                <%loop (ExamResult) item examresultlist%>
                <div class="exercise">
                  <%if item.status==0 %>
                  <div class="pull-right">
                      <span class="btn btn-link" style="color:#ff0000">未完成</span> 
                      <span class="btn btn-normal open-exercise"><span onclick="ExamContinu(${item.id});" class="btn-inner" style="color:#ff0000">继续考试</span></span>
                  </div>
                  <div class="name"><a href="exam.aspx?resultid=${item.id}" target="_blank" style="color:#ff0000">${item.examname}(未完成)</a></div>
                  <%else %>
                  <div class="pull-right">
                    <a href="examresult.aspx?resultid=${item.id}" target="_blank" class="btn btn-link link-button"><span class="btn-inner">考试分析</span></a>&nbsp;&nbsp;
                    <a href="examanswer.aspx?resultid=${item.id}" target="_blank" class="btn btn-link link-button"><span class="btn-inner">答案解析</span></a>
                  </div>
                  <div class="name"><a href="examresult.aspx?resultid=${item.id}" target="_blank">${item.examname}(得分：${item.score}分)</a></div>
                  <%/if %>
                  <div class="meta"> <span class="muted">考试时间: ${fmdate(item.starttime,"yyyy年MM月dd日 HH:mm)}</span> <span class="muted"> </span> </div>
                </div>
                <%/loop %>
              </div>
              <div class="list-ft"> </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <%include(_footer.aspx) %>
</div>
</body>
</html>