<%controller(FangPage.Exam.Controller._examresult) %>
<%using(System) %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="zh-CN" lang="zh-CN">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>
<title>考试分析 - ${pagetitle}</title>
<link type="text/css" rel="stylesheet" href="statics/css/exam.css"/>
<script type="text/javascript" src="statics/js/jquery-1.8.2.min.js"></script>
<script type="text/javascript" src="statics/js/jquery-ui.min.js"></script>
<script type="text/javascript" src="statics/js/popup.js"></script>
<script type="text/javascript" src="statics/js/jquery.nicescroll.min.js"></script>
</head>
<body>
<div class="hbx1">
  <div class="hbx2">
    <div class="hbx3"><img src="statics/images/top.jpg"></div>
    <div class="hbx4 hbx4a">
      <div class="fr"><a href="javascript:;" class="btnq1" onclick="window.print()">打印</a> </div>
      <span class="tab3 tab3a">考试分析</span> 
      <a href="examanswer.aspx?resultid=${examresult.id}" class="tab3"> 答案解析</a>
    </div>
  </div>
</div>
<div class="hbx2">
  <div class="wp">
    <div class="wp2">
      <div class="wp3">
        <div class="wp4 wp4_1">
          <h1 class="qtTitle mb10">${examresult.examname}
            <%if examresult.examid>0 %>
            <%if examresult.status==1  %>
            (尚未阅卷)
            <%else if examresult.status==2  %>
            (已阅卷)
            <%/if %>
            <%/if %>
          </h1>
          <table class="tab5">
            <tbody>
              <tr class="tr1" id="result">
                <td class="td1"><div class="tit2">考试成绩单</div>
                  考生姓名：
                    <%if {examresult.IUser.realname}!="" %>
                    ${examresult.IUser.realname}
                    <%else %>
                    ${examresult.IUser.username}
                    <%/if %>
                    <br/>
                  考试得分：${(examresult.score*1.0)}分<br/>
                  答卷耗时：${(examresult.utime/60+1)}分钟<br/>
                  <div class="bdb1 mb10"></div>
                  试卷总分：${examresult.total}分<br/>
                  及格分数：${examresult.passmark}分<br/>
                  &nbsp;&nbsp;&nbsp;最高分：${maxscore}分<br/>
                  &nbsp;&nbsp;&nbsp;平均分：${avgscore}分 </td>
                <td class="td2">
                  <div class="bc"><span class="s1 s1t">分数</span><span class="s1 s1b">人数</span>
                    <div class="bck0">
                      <span class="s0 s0t">0分</span><span class="s0 s0b">0人</span>
                      <%loop (int) bck bcklist %>
                      <%if {bck._id}%2==0 %>
                      <div class="bck1" style="width:${(bck/examresult.total*100)}%"><span class="s0 s0t">${bck}分</span></div>
                      <%else %>
                       <div class="bck1" style="width:${(bck/examresult.total*100)}%"></div>
                      <%/if %>
                      <%/loop %>
                      <span class="s0 s0a s0t">${examresult.total}分</span><span class="s0 s0a s0b">${testers}人</span>
                    </div>
                    <div class="bch">
                      <div class="bch1">
                        <div class="bch1a"></div>
                        <div class="bch1b"></div>
                      </div>
                      <div class="bch2" style="width:${(examresult.score/examresult.total*100)}%">
                        <div class="bch2a"></div>
                        <div class="bch2b"></div>
                        <div class="bch2h"><span class="s0 s0a s0t">${examresult.score}分</span><span class="s0 s0a s0b">第${display}名</span></div>
                      </div>
                    </div>
                  </div>
                </td>
              </tr>
              <tr class="tr2" id="paper">
                <td class="td1"><div class="tit2">答题分析</div>
                  试题总数：${examresult.questions}道<br/>
                  错题总数：${examresult.wrongs}道<br/>
                  <div class="bdb1 mb10"></div></td>
                <td class="td2"><table class="tab6">
                    <tbody>
                      <tr>
                        <th>试卷大题</th>
                        <th>错题数/总题数</th>
                        <th>我的得分</th>
                        <th>总分</th>
                        <th>得分率</th>
                      </tr>
                      <%loop (ExamResultTopic) examtopic examtopiclist %>
                      <tr>
                        <td>${examtopic.title}</td>
                        <td>${examtopic.wrongs}/${examtopic.questions}</td>
                        <td>${examtopic.score}</td>
                        <td>${Math.Round(examtopic.perscore*examtopic.questions,1)}</td>
                        <td>${CalRate(examtopic.score,examtopic.perscore*examtopic.questions)}%</td>
                      </tr>
                      <%/loop %>
                    </tbody>
                  </table></td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  </div>
</div>
</body>
</html>