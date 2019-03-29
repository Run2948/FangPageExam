<%controller(*.*)%>
<!DOCTYPE html>
<html lang="zh-CN" class="default-layout">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
    <title>用户注册 - ${pagetitle}</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge, chrome = 1"/>
    <link href="statics/css/login.css" rel="stylesheet"/>
    <script type="text/javascript" src="statics/js/jquery-1.8.2.min.js"></script>
    <%include(_iehack.aspx) %>
    <script type="text/javascript">
        $(function () {
            $("#login").click(function () {
                window.location.href = "login.aspx";
            })
            $("#sumitreg").click(function () {
                $("#frmpost").submit();
            })
        });
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
            <div class="container"><a href="index.aspx" class="logo">
                <img src="statics/images/logo.png" alt="方配在线考试系统"></a></div>
        </div>
        <div class="container body-wrap main">
            <h1 class="page-header first"><span class="text"><span class="sprite sprite-notepad"></span>注册</span>
                <hr/>
            </h1>
            <div class="row">
                <div class="span8 offset4 signup-form-wrap form-wrap">
                    <%if ispost %>
                      ${msg}<br />
                      <a href="index.aspx">首页</a>|<a href="login.aspx">登录</a>|<a href="${reurl}">返回</a>
                    <%else %>
                    <form id="frmpost" name="frmpost" method="post" action="" class="form">
                        <div class="form-content">
                            <div class="item-wrap text-item-wrap">
                                <label for="signupFormEmailInput">登录帐号：</label>
                                <span class="text-wrap">
                                    <input id="username" name="username" type="text" placeholder="请输入用户名"/>
                                </span><span class="item-message help-inline"></span>
                            </div>
                            <div class="item-wrap text-item-wrap">
                                <label for="password">登录密码：</label>
                                <span class="text-wrap">
                                    <input id="password" name="password" type="password" placeholder="请输入 6 个以上字符"/>
                                </span><span class="item-message help-inline"></span>
                            </div>
                            <div class="item-wrap text-item-wrap">
                                <label for="repeat">确认密码：</label>
                                <span class="text-wrap">
                                    <input id="repeat" name="repeat" type="password" placeholder="请输入 6 个以上字符"/>
                                </span><span class="item-message help-inline"></span>
                            </div>
                            <div class="item-wrap text-item-wrap">
                                <label for="realname">真实姓名：</label>
                                <span class="text-wrap">
                                    <input id="realname" name="realname" type="text" placeholder="请输入您的真实姓名"/>
                                </span><span class="item-message help-inline"></span>
                            </div>
                        </div>
                        <div class="button-row">
                            <button type="submit" class="visible-hidden"></button>
                            <span id="sumitreg" class="btn btn-primary fir-wrap submit-button"><span class="btn-inner"><span class="fir fir-btn-normal-submit"><span class="fir-text">注册</span></span></span></span>
                            <div id="signupFormAgree" class="item-wrap">
                                <label for="signupFormAgreeInput">
                                    <input id="rules" name="rules" type="checkbox" value="1" checked=""/>
                                    已阅读并同意<a href="http://www.fangpage.com" target="_blank">使用条款和隐私策略</a><span class="item-message help-inline"></span></label>
                            </div>
                        </div>
                    </form>
                    <span id="login" class="btn btn-paper btn-paper-xxlarge link-button login-button"><span class="fir fir-btn-paper-login"></span></span>
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
                       Copyright &copy; 2014 <a target="_blank" href="http://www.fangpage.com">方配软件(FangPage.Com)</a>&nbsp;&nbsp;版权所有
                    </p>
                    <p class="text-center">方配官方网站：<a target="_blank" href="http://www.fangpage.com">http://www.fangpage.com</a></p>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
