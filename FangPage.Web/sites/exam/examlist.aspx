<%using(FangPage.WMS.#) %>
<!DOCTYPE html>
<html lang="zh-CN" class="default-layout">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"/>
<title>${pagenav} - ${pagetitle}</title>
<link rel="stylesheet" href="statics/css/report.css"/>
<%include(_iehack.aspx) %>
<%plugin(jquery) %>
<script type="text/javascript">
    function ExamContinu(id) {
        window.open("examview.aspx?examid=" + id);
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
            <li <%if sortid==0%> class="active" <%/if %>><a href="examlist.aspx?channelid=${channelid}">全部</a></li>
            <%loop (SortInfo) sorts sortlist %>
            <li <%if sorts.id==sortid%> class="active" <%/if %> ><a href="examlist.aspx?channelid=${channelid}&sortid=${sorts.id}">${sorts.name}</a></li>
            <%/loop %>
          </ul>
        </div>
        <div class="box-bd">
          <div class="exercise-list-wrap list-wrap">
            <div class="list">
              <div class="list-bd">
                <%loop (ExamInfo) item examlist %>
                <div class="exercise">
                  <div class="pull-right">
                      <span class="btn btn-normal open-exercise"><span onclick="ExamContinu(${item.id});" class="btn-inner">开始考试</span></span>
                  </div>
                  <div class="name"><a href="examview.aspx?examid=${item.id}" target="_blank">${item.name}</a></div>
                  <div class="meta">
                      <%if item.islimit==1 %>
                      <span class="muted">考试时间：${fmdate(item.starttime,"yyyy-MM-dd HH:mm")}至${fmdate(item.endtime,"yyyy-MM-dd HH:mm")}，</span>
                      <%else %>
                      <span class="muted">考试时间：无限制，</span>
                      <%/if %>
                      <span class="muted">考试人数：${item.exams}人，平均分：${item.avgscore}分</span></div>
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