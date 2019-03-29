<!DOCTYPE html>
<html lang="zh-CN" class="default-layout">
<head>
    <meta name="renderer" content="webkit"/>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"/>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
    <title>${pagetitle} ${siteconfig.version} - Powered By FangPage.COM</title>
    <link rel="stylesheet" href="statics/css/index.css"/>
    <script type="text/javascript" src="statics/js/jquery-1.8.2.min.js"></script>
    <%plugin(layer) %>
    <%include(_iehack.aspx) %>
    <script type="text/javascript">
        $(function () {
            $("#instant").click(function () {
                window.open("testview_instant.aspx");
            })
            $("#csk").click(function () {
                window.open("testview_csk.aspx");
            })
            $("#paper").click(function () {
                window.location.href="examlist.aspx?channelid=2";
            })
            $("#myexam").click(function () {
                window.location.href = "examlist.aspx?channelid=3";
            })
        });
    </script>
</head>
<body>
    <div class="wrap">
        <%include(_header.aspx) %>
        <div class="container body-wrap main">
            <div class="home-wrap">
                <div class="section-wrap false false">
                    <div class="section">
                        <span class="sprite pull-left sprite-section-smart-15"></span>
                        <div class="overflow">
                            <h2><span class="fir fir-title"><span class="fir-text">随机练习</span></span></h2>
                            <div class="content">
                                <p>覆盖所有题库，综合随机组卷，提升您的综合能力</p>
                            </div>
                            <div class="button-row">
                                <span id="instant" class="btn btn-primary create-exercise">
                                    <div class="btn-inner">随机练习</div>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="section-wrap false section-right">
                    <div class="section">
                        <span class="sprite pull-left sprite-section-csk"></span>
                        <div class="overflow">
                            <h2><span class="fir fir-title"><span class="fir-text">强化练习</span></span></h2>
                            <div class="content">
                                <p>自主选择专项或具体考点，各个击破</p>
                            </div>
                            <div class="button-row">
                                <span id="csk" class="btn btn-primary select-csk"><span class="btn-inner">强化练习</span></span></div>
                        </div>
                    </div>
                </div>
                <div class="section-wrap false false">
                    <div class="section">
                        <span class="sprite pull-left sprite-section-smart-paper"></span>
                        <div class="overflow">
                            <h2><span class="fir fir-title"><span class="fir-text">模拟考试</span></span></h2>
                            <div class="content">
                                <p>系统为你提供历年考试的真题试卷进行模拟考试</p>
                            </div>
                            <div class="button-row">
                                <span id="paper" class="btn btn-primary link-button gaq">
                                    <div class="btn-inner">模拟考试</div>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="section-wrap false false">
                    <div class="section">
                        <span class="sprite pull-left sprite-section-continue"></span>
                        <div class="overflow">
                            <h2><span class="fir fir-title"><span class="fir-text">正式考试</span></span></h2>
                            <div class="content">
                                <p>属于您自己的正式考试</p>
                            </div>
                            <div class="button-row">
                                <span id="myexam" class="btn btn-primary link-button gaq">
                                    <div class="btn-inner">正式考试</div>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="hide"></div>
        </div>
        <%include(_footer.aspx) %>
    </div>
</body>
</html>
