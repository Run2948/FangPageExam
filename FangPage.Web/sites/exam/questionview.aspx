<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN" lang="zh-CN">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<title>${pagenav} - ${pagetitle}</title>
<link type="text/css" rel="stylesheet" href="statics/css/exam.css"/>
<script type="text/javascript" src="statics/js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="statics/js/jquery-ui.min.js"></script>
<script type="text/javascript" src="statics/js/jquery.form.js"></script>
<script type="text/javascript" src="statics/js/popup.js"></script>
<script type="text/javascript" src="statics/js/exam.js"></script>
<%plugin(layer) %>
<%include(_limitkey.aspx) %>
<script type="text/javascript">
    $(function () {
        var ipt = $("label input");
        ipt.parent().removeClass("sd");
        ipt.filter(":checked").parent().addClass("sd");
        layer.use('extend/layer.ext.js');
    });
    function AddFav(qid, action) {
        $.post("favajax.aspx", {
            qid: qid,
            action: action
        }, function (data) {
            if (data.error > 0) {
                alert(data.message);
                return;
            }
            if (data.action == -1) {
                $("#fav_" + qid).html("<a href=\"javascript:AddFav(" + qid + ",1);\">收藏本题</a>");
                layer.msg('取消收藏成功!', 2, -1);
            }
            else if (data.action = 1) {
                $("#fav_" + qid).html("<a href=\"javascript:AddFav(" + qid + ",-1);\">取消收藏</a>");
                layer.msg('收藏本题成功!', 2, -1);
            }
        }, "json");
    }
    function EditNote(qid, num) {
        var note = $('#note_' + qid).html();
        layer.prompt({ type: 3, title: '(${sortinfo.name})第' + num + '题笔记', val: note }, function (val) {
            $.post("noteajax.aspx", {
                qid: qid,
                note: val
            }, function (data) {
                if (data.error > 0) {
                    alert(data.message);
                    return;
                }
                $('#note_' + qid).html(val);
                $('#shownote_' + qid).show();
                layer.msg('笔记保存成功!', 2, -1);
            }, "json");
        })
    }
</script>
</head>
<body>
<div class="hbx1">
  <div class="hbx2">
    <div class="hbx3"><img src="statics/images/top.jpg"/></div>
    <div class="hbx4">
      <div class="fr"><a href="testview_csk.aspx?channelid=2&sortid=${sortid}" class="btnq2">专项练习</a></div>
      <span class="write" style="font-size:14px;font-weight:bold">${pagenav}</span> 
    </div>
  </div>
</div>
<div class="hbx2">
<div class="rnav">
    <div class="rnavhd">${sortinfo.name}</div>
    <div class="rnavct">
      <div class="mb10"> 答题量：${examloginfo.answers}/${examloginfo.questions}题<br/>
        错题数：${examloginfo.wrongs}题<br/>
        正确率：${examloginfo.accuracy}%</div>
      <ul class="rnlt1 fc">
        <li><span class="bg1"></span>正确题</li>
        <li><span class="bg2"></span>错误题</li>
        <li><span class="bg3"></span>未答题</li>
        <li><span class="bg4"></span>问答题</li>
      </ul>
      <ul class="rnlt2 fc">
        <%set (int){en}=0  %>
        <%loop (ExamQuestion) item questionlist%>
          <%set {en}=en+1  %>
          <%if item.type==5%>
          <li><a href="#${en}" class="bg4">${en}</a></li>
          <%else if item.useranswer=="" %>
          <li><a href="#${en}" class="bg3">${en}</a></li>
          <%else if item.userscore>0 %>
          <li><a href="#${en}" class="bg1">${en}</a></li>
          <%else%>
          <li><a href="#${en}" class="bg2">${en}</a></li>
          <%/if %>
        <%/loop %>
      </ul>
    </div>
    <div class="rnavft"></div>
  </div>
<div class="wp wpa">
  <div class="wp2">
    <div class="wp3">
      <div class="wp4">
        <form id="testProcessForm" name="testProcessForm" action="testpost.aspx" method="post">
            <a id="1"></a>
            <div class="tit1 pd1"></div>
            <%set (int){perscore}=100 %>
            <%set (int){topicnum}=0 %>
            <%loop (ExamQuestion) item questionlist%>
            <%set {topicnum}=topicnum+1 %>
            <%if item.type==1 %>
            <%--单选题--%>
            <dl class="st tm_zt_0">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}</p>
              </dt>
              <dd>
                ${Option(item.option,item.ascount,item.optionlist)}
              </dd>
              <dd class="dAn fc">
                <span class="ft4 fl">您的答案：</span>
                 <span class="fl w2 bx7">
                  <%loop (string) str answerarr %>
                  <%if {str._id}<=item.ascount %>
                  <label><input type="radio" id="_${topicnum}" name="answer_${item.id}" <%if str==item.useranswer %> checked="checked" <%/if %> value="${str}"/>${str}</label>
                  <%/if %>
                  <%/loop%>
                  </span>
              </dd>
              <dd>
                <%if examconfig.showanswer==1 %>
                <div class="mb10">正确答案：<span class="ft11 ftc1">${item.answer}</span></div>
                <div class="mb10">答案解析：
                  ${item.explain}
                </div>
                <%/if %>
                <div class="mb10">
                <%if item.isfav==1 %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},-1)">取消收藏</a>&nbsp;&nbsp;
                <%else %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},1)">收藏本题</a>&nbsp;&nbsp;
                <%/if %>
                    <img src="statics/images/note.png" /><a href="javascript:EditNote(${item.id},${topicnum})">编辑笔记</a>
                </div>
                <div id="shownote_${item.id}" class="mb10" <%if item.note=="" %> style="display:none" <%/if %> >您的笔记：
                  <span id="note_${item.id}">${item.note}</span>
                </div>
              </dd>
            </dl>
            <%else if item.type==2 %>
            <%--多选题--%>
            <dl class="st tm_zt_0">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}</p>
              </dt>
              <dd>
                ${Option(item.option,item.ascount,item.optionlist)}
              </dd>
              <dd class="dAn fc">
                 <span class="ft4 fl">您的答案：</span>
                 <span class="fl w2 bx7">
                  <%loop (string) str answerarr %>
                  <%if {str._id}<=item.ascount %>
                  <label><input type="checkbox" id="_${topicnum}" name="answer_${item.id}" <%if ischecked(str,item.useranswer) %> checked="checked" <%/if %> value="${str}"/>${str}</label>
                  <%/if %>
                  <%/loop%>
                  </span>
              </dd>
              <dd>
                <%if examconfig.showanswer==1 %>
                <div class="mb10">正确答案：<span class="ft11 ftc1">${item.answer}</span></div>
                <div class="mb10">答案解析：
                  ${item.explain}
                </div>
                <%/if %>
                <div class="mb10">
                <%if item.isfav==1 %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},-1)">取消收藏</a>&nbsp;&nbsp;
                <%else %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},1)">收藏本题</a>&nbsp;&nbsp;
                <%/if %>
                <img src="statics/images/note.png" /><a href="javascript:EditNote(${item.id},${topicnum})">编辑笔记</a>
                </div>
                <div id="shownote_${item.id}" class="mb10" <%if item.note=="" %> style="display:none" <%/if %> >您的笔记：
                  <span id="note_${item.id}">${item.note}</span>
                </div>
              </dd>
            </dl>
            <%else if item.type==3 %>
            <%--判断题--%>
            <dl class="st tm_zt_0">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}</p>
              </dt>
              <dd class="dAn fc">
                <span class="ft4 fl">您的答案：</span>
                 <span class="fl w2 bx7">
                     <%if item.useranswer=="Y"%>
                     <label><input type="radio" name="answer_${item.id}" checked="checked" value="Y" disabled="disabled">正确</label>
                     <%else %>
                     <label><input type="radio" name="answer_${item.id}" value="Y" disabled="disabled">正确</label>
                     <%/if %>
                     <%if item.useranswer=="N"%>
                     <label><input type="radio" name="answer_${item.id}" checked="checked" value="N" disabled="disabled">错误</label>
                     <%else %>
                     <label><input type="radio" name="answer_${item.id}" value="N" disabled="disabled">错误</label>
                     <%/if %>
                 </span>
              </dd>
              <dd>
                <%if examconfig.showanswer==1 %>
                <div class="mb10">正确答案：
                <span class="ft11 ftc1">
                <%if {item.answer}=="Y"  %>
                正确
                <%else if {item.answer}=="N" %>
                错误
                <%/if %>
                </span></div>
                <div class="mb10">答案解析：
                  ${item.explain}
                </div>
                <%/if %>
                <div class="mb10">
                <%if item.isfav==1 %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},-1)">取消收藏</a>&nbsp;&nbsp;
                <%else %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},1)">收藏本题</a>&nbsp;&nbsp;
                <%/if %>
                <img src="statics/images/note.png" /><a href="javascript:EditNote(${item.id},${topicnum})">编辑笔记</a>
                </div>
                <div id="shownote_${item.id}" class="mb10" <%if item.note=="" %> style="display:none" <%/if %> >您的笔记：
                  <span id="note_${item.id}">${item.note}</span>
                </div>
              </dd>
            </dl>
            <%else if item.type==4 %>
            <%--填空题--%>
            <dl class="st tm_zt_0">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${FmAnswer(item.title,item.id,item.useranswer)}</p>
              </dt>
              <dd>
                <%if examconfig.showanswer==1 %>
                <div class="mb10">正确答案：
                <span class="ft11 ftc1">
                ${item.answer}
                </span></div>
                <div class="mb10">答案解析：
                  ${item.explain}
                </div>
                <%/if %>
                <div class="mb10">
                <%if item.isfav==1 %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},-1)">取消收藏</a>&nbsp;&nbsp;
                <%else %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},1)">收藏本题</a>&nbsp;&nbsp;
                <%/if %>
                <img src="statics/images/note.png" /><a href="javascript:EditNote(${item.id},${topicnum})">编辑笔记</a>
                </div>
                <div id="shownote_${item.id}" class="mb10" <%if item.note=="" %> style="display:none" <%/if %> >您的笔记：
                  <span id="note_${item.id}">${item.note}</span>
                </div>
              </dd>
            </dl>
            <%else if item.type==5 %>
            <%--问答题--%>
            <dl class="st tm_zt_${item.id}">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}</p>
              </dt>
              <dd>
                <div class="ft4">您的答案：</div>
                <textarea class="jdt" id="_${topicnum}" name="answer_${item.id}">${item.useranswer}</textarea>
              </dd>
              <dd>
                <%if examconfig.showanswer==1 %>
                <div class="mb10">正确答案：
                <span class="ft11 ftc1">
                ${item.answer}
                </span></div>
                <div class="mb10">答案解析：
                  ${item.explain}
                </div>
                <%/if %>
                <div class="mb10">
                <%if item.isfav==1 %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},-1)">取消收藏</a>&nbsp;&nbsp;
                <%else %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},1)">收藏本题</a>&nbsp;&nbsp;
                <%/if %>
                <img src="statics/images/note.png" /><a href="javascript:EditNote(${item.id},${topicnum})">编辑笔记</a>
                </div>
                <div id="shownote_${item.id}" class="mb10" <%if item.note=="" %> style="display:none" <%/if %> >您的笔记：
                  <span id="note_${item.id}">{item.note}</span>
                </div>
              </dd>
            </dl>
            <%else if item.type==6 %>
            <%--打字题--%>
            <dl class="st tm_zt_${item.id}">
              <dt class="nobold"><span class="num" id="${(topicnum+1)}">${topicnum}</span>
                <p>${item.title}</p>
              </dt>
              <dd>
                <div class="ft4">您的答案：</div>
                <textarea class="jdt" id="_${topicnum}" name="answer_${item.id}">${item.useranswer}</textarea>
              </dd>
              <dd>
                <%if examconfig.showanswer==1 %>
                <div class="mb10">答案解析：
                  ${item.explain}
                </div>
                <%/if %>
                <div class="mb10">
                <%if item.isfav==1 %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},-1)">取消收藏</a>&nbsp;&nbsp;
                <%else %>
                    <img src="statics/images/fav.gif" /><a id="fav_${item.id}" href="javascript:AddFav(${item.id},1)">收藏本题</a>&nbsp;&nbsp;
                <%/if %>
                <img src="statics/images/note.png" /><a href="javascript:EditNote(${item.id},${topicnum})">编辑笔记</a>
                </div>
                <div id="shownote_${item.id}" class="mb10" <%if item.note=="" %> style="display:none" <%/if %> >您的笔记：
                  <span id="note_${item.id}">${item.note}</span>
                </div>
              </dd>
            </dl>
            <%/if %>
        <%/loop %>
        <div style="clear:both;"></div>
      </form>
    </div>
    </div>
  </div>
</div>
</div>
</body>
</html>