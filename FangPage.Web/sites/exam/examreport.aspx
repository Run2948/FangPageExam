<!DOCTYPE html>
<html lang="zh-CN" class="default-layout">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"/>
<title>综合能力报告 - ${pagetitle}</title>
<link rel="stylesheet" href="statics/css/report.css"/>
<%plugin(jquery) %>
<script src="${plupath}/jqchart/jquery.jqChart.min.js" type="text/javascript"></script>
<!--[if IE]>
    <script lang="javascript" type="text/javascript" src="${plupath}/jqchart/canvas.js"></script>
<![endif]-->
<script type="text/javascript" src="${plupath}/progressbar/jquery.progressbar.min.js"></script>
<%include(_iehack.aspx) %>
<!--[if gte IE 9]>
  <style type="text/css">
    .gradient {
       filter: none;
    }
  </style>
<![endif]-->
<script type="text/javascript">
    $(document).ready(function () {
        $('#jqchart').jqChart({
            title: { text: '考试成绩趋势图' },
            axes: [
                {
                    location: 'left',//y轴位置，取值：left,right
                    minimum: 0,//y轴刻度最小值
                    maximum: 100,//y轴刻度最大值
                    interval: 10//刻度间距
                }
            ],
            series: [
                //数据1开始
                {
                    type: 'line',//图表类型，取值：column 柱形图，line 线形图
                    title: '分数',//标题
                    data: [${examresult}]//数据内容，格式[[x轴标题,数值1],[x轴标题,数值2],......]
                },
                //数据1结束		
            ]
        });

        $(".progressBar").progressBar({ showText: true, barImage: '${plupath}/progressbar/images/progressbg_red.gif' });

        $('.sprite').click(function () {   // 获取所谓的父行
            $(this).toggleClass("sprite-selected");  // 添加/删除图标
            $('.child_' + $(this).attr('id')).toggle();  // 隐藏/显示所谓的子行
        });
    });
</script>
</head>
<body>
<div class="wrap">
  <%include(_header.aspx) %>
  <div class="container body-wrap main">
    <div class="report-wrap user-report-wrap row">
      <div class="span12 meta-area">
        <div class="box-wrap score-box">
          <div class="box">
            <div class="box-bd clearfix">
              <div class="user-score">
                <div class="left-column pull-left">
                  <div class="score-info forecast-score">
                    <div class="report-circle avg sprite-blue-circle"><span class="number text-xxlarge">${avg_my}</span><span class="unit">分</span> </div>
                    <div class="lbl-wrap"> <span class="lbl-large">考试平均分</span></div>
                  </div>
                  <div class="item-row"> <span class="lbl">全站排名：</span> <span class="score"><span class="number user-index">${avg_display}</span><span class="number total-user"> / ${examusers}<span class="unit">名</span></span></span> </div>
                  <div class="item-row last"> <span class="lbl">全站平均分：</span> <span class="score"><span class="number">${avg_total}</span><span class="unit">分</span></span> </div>
                </div>
                <div class="middle-column pull-left">
                  <div class="question-count">
                    <div class="report-circle avg sprite-red-circle"> <span class="number text-xxlarge">${accuracy_my}</span> <span class="unit">%</span> </div>
                    <div class="lbl-wrap"> <span class="lbl-large">答题正确率</span> </div>
                  </div>
                  <div class="item-row"> <span class="lbl">全站排名：</span> <span class="score"><span class="number user-index">${accuracy_display}</span><span class="number total-user"> / ${examusers}<span class="unit">名</span></span></span> </div>
                  <div class="item-row last"> <span class="lbl">全站总正确率：</span> <span class="score"><span class="number">${accuracy_total}</span><span class="unit">%</span></span> </div>
                </div>
                <div class="right-column">
                  <div id="jqchart" style="width:100%;height:340px;" class="trend-image-wrap">
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
      <div class="span12">
        <div class="box-wrap">
          <div class="box">
            <div class="box-bd clearfix">
              <h3 class="text-label text-label-green"><span class="text-label-inner bold">能力图表</span></h3><h3 class="text-label text-label-green"><span class="text-label-inner bold">能力等级:${user.UserGrade.name}</span></h3>
              <div class="user-csk-table-wrap csk-table-wrap">
                <table class="csk-table table">
                  <thead>
                    <tr>
                      <th class="name-col">知识点</th>
                      <th class="count-col">答题量</th>
                      <th class="count-col">正确题</th>
                      <th class="capacity-col last">正确率</th>
                    </tr>
                  </thead>
                  <tbody>
                    <%loop (ExamLogInfo) item examloglist %>
                    <%if item.subcounts>0 %>
                    <tr class="keypoint keypoint-level-0">
                      <td class="name-col"><span class="text toggle-expand"><span id="row_${item.sortid}" class="sprite sprite-expand"></span>${item.sortname}</span></td>
                      <td class="count-col">${item.answers}道/${item.questions}道</td>
                      <td class="count-col">${(item.answers-item.wrongs)}道</td>
                      <td class="capacity-col">
                          <span class="progressBar">${item.accuracy}%</span>
                      </td>
                    </tr>
                    ${GetChildSort(channelinfo.id,item.sortid,1)}
                    <%else %>
                    <tr class="keypoint keypoint-level-0">
                      <td class="name-col"><span class="text toggle-expand"><span class="sprite sprite-expand sprite-noexpand"></span>${item.sortname}</span></td>
                      <td class="count-col">${item.answers}道/${item.questions}道</td>
                      <td class="count-col">${(item.answers-item.wrongs)}道</td>
                      <td class="capacity-col"><span class="progressBar">${item.accuracy}%</span></td>
                    </tr>
                    <%/if %>
                    <%/loop %>
                  </tbody>
                </table>
              </div>
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