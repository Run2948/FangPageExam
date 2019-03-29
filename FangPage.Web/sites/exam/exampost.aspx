<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN" lang="zh-CN">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<title>考试结束 - ${pagetitle}</title>
<link type="text/css" rel="stylesheet" href="statics/css/exam.css"/>
<script type="text/javascript" src="statics/js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="statics/js/jquery-ui.min.js"></script>
<script type="text/javascript" src="statics/js/popup.js"></script>
<%include(_limitkey.aspx) %>
</head>
<body>
<div class="hbx1">
  <div class="hbx2">
    <div class="hbx3"><img src="statics/images/top.jpg"></div>
    <div class="hbx4">
      <div class="fr"> </div>
    </div>
  </div>
</div>
<div class="wp">
  <div class="wp2">
    <div class="wp3">
      <%if ispost %>
      <div class="wp4">
        <h1 class="qtTitle">${examresult.examname}</h1>
        <div class="ta_c bx4">恭喜您顺利完成本次考试！<span id="testScore">您的得分为<span>${examresult.score}</span>分</span>
            <%if expinfo.id>0 %>
              <br />
              <%if examresult.exnote!="" %> ${examresult.exnote}，<%/if %>
              考试获得经验值为${examresult.exp}。
            <%/if %>
        </div>
        <div id="tip" class="ta_c mb20 ft12">想知道自己的考试情况吗？点击下面的按钮查看详情</div>
          <div class="ta_c mb10"> 
          <a id="analisisLink" href="examresult.aspx?resultid=${examresult.id}" class="btnq3">考试分析</a> 
          <a id="answerLink" href="examanswer.aspx?resultid=${examresult.id}" class="btnq3 ml2">答案解析</a>
          </div>
      </div>
      <%else %>
      <div class="wp4">
        <h1 class="qtTitle">对不起，您尚未进行考试提交</h1>
      </div>
      <%/if %>
    </div>
  </div>
</div>
</body>
</html>