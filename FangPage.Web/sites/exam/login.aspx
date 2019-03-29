<%controller(*.*)%>
<!DOCTYPE html>
<html lang="zh-CN" class="default-layout">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
    <meta name="renderer" content="webkit"/>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge,chrome=1"/>
    <title>${siteconfig.sitetitle}登录 ${siteconfig.version} - Powered By FangPage.COM</title>
    <link href="statics/css/login.css" rel="stylesheet"/>
    <script type="text/javascript" src="statics/js/jquery-1.8.2.min.js"></script>
    <%include(_iehack.aspx) %>
    <script type="text/javascript">
        $(function () {
            $("#register").click(function () {
                window.location.href = "register.aspx";
            })
            $("#sumitlogin").click(function () {
                $("#frmpost").submit();
            })
        });
    </script>
    <script type="text/javascript">
        var _hmt = _hmt || [];
        (function () {
            var hm = document.createElement("script");
            hm.src = "//hm.baidu.com/hm.js?35483845f92e384129fb5d03f9d7c3cf";
            var s = document.getElementsByTagName("script")[0];
            s.parentNode.insertBefore(hm, s);
        })();
    </script>
</head>
<body>
    <div class="wrap">
        <div class="repeat-x default-t">
            <div class="header-left layout default-lt"></div>
            <div class="header-right layout default-rt"></div>
        </div>

        <div class="default-left repeat-y default-l"></div>
        <div class="default-right repeat-y default-r"></div>
        <div class="header simple-header">
            <div class="container"><a href="http://fpexam.fangpage.com" target="_blank" class="logo">
                <img src="statics/images/logo.png" alt="方配在线考试系统"/></a></div>
        </div>
        <div class="container body-wrap main">
            <h1 class="page-header first"><span class="text"><span class="sprite sprite-notepad"></span>登录</span>
                <hr/>
            </h1>
            <div class="row">
                <div class="span8 offset4 login-form-wrap form-wrap">
                    <form id="frmpost" name="frmpost" method="post" action="" class="form">
                        <div class="form-content">
                            <div class="item-wrap text-item-wrap error">
                                <label for="username">用户：</label>
                                <span class="text-wrap">
                                    <input id="username" name="username" type="text" value="" placeholder="请输入用户名/邮箱/手机号"/>
                                </span><span class="item-message help-inline"></span>
                            </div>
                            <div class="item-wrap text-item-wrap">
                                <label for="password">密码：</label>
                                <span class="text-wrap">
                                    <input id="password" name="password" type="password" value="" placeholder="请输入密码"/>
                                </span><span class="item-message help-inline"></span>
                            </div>
                        </div>
                        <div class="button-row">
                            <button type="submit" class="visible-hidden"></button>
                            <!--<button tabindex="-1" type="button" class="btn btn-link link-button">找回密码</button>-->
                            <span id="sumitlogin" class="btn btn-primary submit-button"><span class="btn-inner">登&nbsp;录</span></span>
                            <div class="item-wrap">
                              <label for="persistent">
                                <input id="persistent" name="persistent" type="checkbox" value="true"/>
                                下次自动登录<span class="item-message help-inline"></span></label>
                            </div>
                        </div>
                        <div class="form-message text-red"></div>
                    </form>
                    <%if regconfig.regstatus==1 %>
                    <span id="register" class="btn btn-paper btn-paper-xxlarge link-button signup-button"><span class="fir fir-btn-paper-signup"></span></span>
                    <%/if %>
                </div>
            </div>
        </div>
        <div class="footer repeat-x default-b">
            <div class="footer-left layout default-lb"></div>
            <div class="footer-right layout default-rb"></div>
            <div class="container">
            <div class="links">
            <p class="text-center">
               Copyright &copy; 2013-${fmdate(datetime,"yyyy")} <a target="_blank" href="http://www.fangpage.com">方配软件(FangPage.Com)</a>&nbsp;&nbsp;版权所有，版本：${siteconfig.version}
            </p>
            <p class="text-center">
                官方网站：<a href="http://www.fangpage.com" target="_blank">http://www.fangpage.com</a>，QQ：12677206，电话：13481092810
            </p>
            </div>
            </div>
        </div>
    </div>
</body>
</html>
