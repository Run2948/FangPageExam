<%using(FangPage.WMS.#) %>
<div class="repeat-x default-t">
    <div class="header-left layout default-lt"></div>
    <div class="header-right layout default-rt"></div>
</div>
<div class="default-left repeat-y default-l"></div>
<div class="default-right repeat-y default-r"></div>
<div class="header">
    <div class="navbar-hd clearfix">
        <div class="nav-bar-links">
            <a href="http://www.fangpage.com" class="course-link course-menu-trigger" style="color:#ffffff"><strong>方配在线考试系统版本：${siteconfig.version}</strong></a>
            <%if isadmin %>
            <a href="${adminpath}/index.aspx" class="course-link course-menu-trigger" style="color:#ffffff" target="_blank"><strong>【登录考试系统后台】</strong></a>
            <%/if %>
        </div>
        <div class="pull-right navbar-links">
            <a href="logout.aspx" class="user-nav user-menu-trigger"><span class="sprite sprite-profile i-20"></span><span class="sprite sprite-profile-tip"><span class="email" style="color:#ffffff">${username}&nbsp;|&nbsp;[退出系统]</span><span class="sprite sprite-profile-tip-arrow"></span></span></a>
        </div>
    </div>
    <div class="navbar-bd">
        <div class="container">
            <a href="index.aspx" class="logo">
                <img src="statics/images/logo.png" alt="${siteconfig.name}">
            </a>
            <ul class="nav main-nav">
                <li <%if pagename=="index.aspx"%> class="active" <%/if %> ><a href="index.aspx">练习与考试</a></li>
                <li <%if pagename=="examreport.aspx" %> class="active" <%/if %> ><a href="examreport.aspx">能力评估报告</a></li>
                <li <%if pagename=="examhistory.aspx"||pagename=="incorrect.aspx"||pagename=="examnote.aspx"||pagename=="favorite.aspx" %> class="active" <%/if %> ><a href="examhistory.aspx">考试历史</a></li>
                <%if pagename=="examlist.aspx"||pagename=="myexamlist.aspx" %>
                <li class="active"><a href="${fullname}">${pagenav}</a></li>
                <%/if %>
            </ul>
        </div>
    </div>
    <span class="repeat-x header-shadow-repeat header-shadow-repeat-l"></span>
    <div class="header-shadow"></div>
    <span class="repeat-x header-shadow-repeat header-shadow-repeat-r"></span>
</div>
