<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head>
<title>试卷[${examinfo.name}] 确定进入考试 - ${pagetitle}</title>
<script type="text/javascript" src="statics/js/jquery-1.8.2.min.js"></script>
<%plugin(layer) %>
<script type="text/javascript">
    $(document).ready(function () {
        if (! -[1, ] && !window.XMLHttpRequest) {
            $(".IE6jiance").show();
            $.dialog.alert('您当前使用的浏览器版本太低，建议升级到更高版本的浏览器！', function () { });
        }
    });
	</script>
    <!--[if IE 6]>
	<script src="/sites/exam/statics/js/iepng.js" type="text/javascript"></script>
	<script type="text/javascript">
	   EvPNG.fix('div, ul, img, li, input'); 
	</script>
	<![endif]-->
    <style type="text/css">
    body {
	    PADDING: 0px;
	    MARGIN: 0px auto;
	    WORD-BREAK: break-all;
	    WORD-WRAP: break-word;
	    FONT-SIZE: 12px;
	    font-family: "Times New Roman", Times, serif;
	    BACKGROUND: #f1f1f1;
	    TEXT-ALIGN: center;
    }
    .head {
	    height: 60px;
	    background: url(statics/images/navbg.png);
	    text-align: center;
	    padding-top: 10px;
    }
    a.a:hover {
    color:#000
    }
    #header {
	    MARGIN: 0px auto;
	    HEIGHT: 60px;
    }
    #center-middle {
	    width: 584px;
	    margin: 0px auto;
	    margin-top: -44px
    }
    .cbodywrap {
	    width: 578px;
	    background: #fff6b7;
	    margin-left: 16px;
	    _margin: 25px 0px 0px 25px;
    }
    #cbody {
	    WIDTH: 515px;
	    text-align: left;
	    line-height: 28px;
	    padding: 15px 15px 37px 15px;
	    height: auto;
	    background: url(statics/images/exambg.png) no-repeat 0 100%;
	    margin-left: 16px;
	    margin-top: 30px;
	    _margin: 10px 0px 0px 4px;
	    position: relative
    }
    .ptksbtn {
	    width: 87px;
	    height: 81px;
	    background: url(statics/images/examtype3.png) no-repeat;
	    position: absolute;
	    left: -7px;
	    top: -7px
    }
    .userfacemain {
	    width: 101px;
	    height: 101px;
	    margin: 0 auto;
	    position: relative;
	    margin-top: 32px;
    }
    .userfacemain .userface {
	    width: 101px;
	    height: 101px;
	    background: url(statics/images/userface02.png) no-repeat;
	    position: absolute;
	    margin-left: 18px
    }
    .userfacemain img {
	    width: 101px;
	    height: 101px;
	    margin-left: 18px;
    }
    .IE6jiance {
	    width: 650px;
	    margin: 0px auto;
	    margin-top: 25px;
	    height: 35px;
	    line-height: 35px;
	    color: #de1c1c;
	    font-size: 14px;
	    background: url(statics/images/gth.png) no-repeat 0 4px;
	    padding-left: 30px
    }
    .bottombg {
	    width: 578px;
	    height: 45px;
	    background: url(statics/images/bottom.png) no-repeat;
	    margin-left: 16px;
	    margin-top: -6px
    }
    .clear {
	    clear: both;
    }
    .blank10 {
	    height: 10px;
	    overflow: hidden
    }
    </style>
</head>
<body style="background:url(statics/images/examjt.png) no-repeat 50% 0;color:#777">
<form name="frmpost" method="post" action="" id="frmpost" onsubmit="layer.msg('系统正在组卷，请稍后...', 0, 1);">
  <div class="userfacemain">
    <div class="userface"></div>
    <img src="statics/images/noavatar_small.gif" onerror="this.src='statics/images/noavatar_small.gif';"></div>
  <div id="container">
    <div id="header"></div>
    <div id="center-header"></div>
    <div id="center-middle">
      <div class="cbodywrap">
        <div id="cbody">
          <div class="ptksbtn"></div>
          <table width="100%" height="163" align="center" cellpadding="0" cellspacing="1">
            <tr>
              <td height="50" colspan="2" align="center" style="background:#fffbe1"><span style="font-size:17px;text-align: center;color:#444;font-weight:bold; font-family:'微软雅黑'">${examinfo.name}</span></td>
            </tr>
            <tr>
              <td height="30" colspan="2" align="center" style="background:#fff url(statics/images/trbg.png) no-repeat 50% 100%;"><strong style="font-size:14px;color: #FF3300; font-weight: bold;">
              ${username}
              </strong></td>
            </tr>
            <tr>
              <td height="30" align="center" colspan="2" style="background:#fff url(statics/images/trbg.png) no-repeat 50% 100%;">考试时间：
                <%if examinfo.islimit==1 %>
                ${fmdate(examinfo.starttime,"yyyy-MM-dd HH:mm")}至${fmdate(examinfo.endtime,"yyyy-MM-dd HH:mm")}
                <%else %>
                无限制
                <%/if %>
              </td>
            </tr>
            <tr>
              <td width="49%" height="30" align="left" style="background:#fff url(statics/images/trbg.png) no-repeat 0 100%; padding-left:50px;">答题时间：${examinfo.examtime}分钟</td>
              <td width="51%" align="left" style="background:#fff url(statics/images/trbg.png) no-repeat -270px 100%; padding-left:20px;border-left:1px solid #f4ecd2">试卷总分：${examinfo.total}分</td>
            </tr>
            <tr>
              <td height="30" align="left" style=" padding-left:50px;background:#fff url(statics/images/trbg.png) no-repeat 0 100%;">所需积分：<font color="#ff3300">${examinfo.credits}</font></td>
              <td align="left" style="padding-left:20px;background:#fff url(statics/images/trbg.png) no-repeat -270px 100%;border-left:1px solid #f4ecd2">可用积分：${user.credits} </td>
            </tr>
          </table>
        </div>
        <div class="clear"></div>
      </div>
      <div class="bottombg"></div>
      <div id="center-bottom"></div>
    </div>
  </div>
  <div style="text-align: center; line-height:30px;">
    <input type="image" name="startexam" id="startexam" src="statics/images/btn2.png" style="border-width:0px;"/>
  </div>
  <br/>
  <div class="IE6jiance" style="display:none"> 检测到您的浏览器为较低版本IE6，为了保证考试质量，建议升级浏览器版本或更换其他高版本浏览器！ </div>
</form>
</body>
</html>