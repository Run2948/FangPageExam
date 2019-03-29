<!DOCTYPE html>
<html lang="zh-CN" class="default-layout">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1" />
    <title>我的错题 - ${pagetitle}</title>
    <link rel="stylesheet" href="statics/css/report.css" />
    <%include(_iehack.aspx) %>
    <%plugin(jquery) %>
    <script type="text/javascript">
        $(function () {
            $('.sprite').click(function () {   // 获取所谓的父行
                $(this).toggleClass("sprite-selected");  // 添加/删除图标
                $('.child_' + $(this).attr('id')).toggle();  // 隐藏/显示所谓的子行
            });
        })
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
                            <li><a href="examhistory.aspx">考试记录</a></li>
                            <li class="active"><a href="incorrect.aspx">我的错题</a></li>
                            <li><a href="examnote.aspx">笔记题目</a></li>
                            <li><a href="favorite.aspx">我的收藏</a></li>
                        </ul>
                    </div>
                    <div class="box-bd">
                        <div class="exercise-list-wrap list-wrap">
                            <div class="list">
                                <div class="list-hd">
                                    <div class="name">共有${total}道错题</div>
                                </div>
                                <div class="list-bd">
                                    <table class="keypoint-table">
                                        <tbody>
                                            <%loop (ExamLogInfo) item examloglist %>
                                            <%if item.subcounts>0 %>
                                            <tr class="keypoint keypoint-level-0">
                                                <td class="name-col">
                                                    <span class="text toggle-expand">
                                                        <span id="row_${item.sortid}" class="sprite sprite-expand i-20"></span>
                                                        <%if item.wrongs>0 %>
                                                        <a href="questionview.aspx?sortid=${item.sortid}&action=wrong" target="_blank" class="btn btn-link link-button">${item.sortname}(共${item.wrongs}道错题)</a>
                                                        <%else %>
                                                        ${item.sortname}(共${item.wrongs}道错题)
                                                        <%/if %>
                                                    </span>
                                                </td>
                                                <td class="button-col">
                                                    <%if item.wrongs>0 %>
                                                    <a href="questionview.aspx?sortid=${item.sortid}&action=wrong" target="_blank" class="btn btn-link link-button"><span class="btn-inner">查看题目</span></a>&nbsp;&nbsp;
                                                    <a href="testview_csk.aspx?sortid=${item.sortid}" target="_blank" class="btn btn-link link-button"><span class="btn-inner">专项练习</span></a>
                                                    <%else %>
                                                    <span class="btn-inner">查看题目</span>&nbsp;&nbsp;
                                                    <span class="btn-inner">专项练习</span>
                                                    <%/if %>
                                                </td>
                                            </tr>
                                            ${GetChildSort(channelinfo.id,item.sortid,1)}
                                            <%else %>
                                            <tr class="keypoint keypoint-level-0">
                                                <td class="name-col">
                                                    <span class="text toggle-expand">
                                                        <span class="sprite sprite-expand-holder i-20"></span>
                                                        <%if item.wrongs>0 %>
                                                        <a href="questionview.aspx?sortid=${item.sortid}&action=wrong" target="_blank" class="btn btn-link link-button">${item.sortname}(共${item.wrongs}道错题)</a>
                                                        <%else %>
                                                        ${item.sortname}(共${item.wrongs}道错题)
                                                        <%/if %>
                                                    </span>
                                                </td>
                                                <td class="button-col">
                                                    <%if item.wrongs>0 %>
                                                    <a href="questionview.aspx?sortid=${item.sortid}&action=wrong" target="_blank" class="btn btn-link link-button"><span class="btn-inner">查看题目</span></a>&nbsp;&nbsp;
                                                    <a href="testview_csk.aspx?sortid=${item.sortid}" target="_blank" class="btn btn-link link-button"><span class="btn-inner">专项练习</span></a>
                                                    <%else %>
                                                    <span class="btn-inner">查看题目</span>&nbsp;&nbsp;
                                                    <span class="btn-inner">专项练习</span>
                                                    <%/if %>
                                                </td>
                                            </tr>
                                            <%/if %>
                                            <%/loop %>
                                        </tbody>
                                    </table>
                                </div>
                                <div class="list-ft"></div>
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
