SET IDENTITY_INSERT [dbo].[FP_WMS_UserInfo] ON
INSERT [dbo].[FP_WMS_UserInfo] ([id], [roleid], [departid], [gradeid], [type], [username], [password], [password2], [email], [isemail], [mobile], [ismobile], [realname], [isreal], [nickname], [avatar], [sex], [exp], [credits], [regip], [joindatetime], [onlinestate], [lastip], [lastvisit], [secques], [authstr], [authtime], [authflag], [vipdate], [bday], [bloodtype], [height], [weight], [married], [education], [school], [job], [position], [politics], [company], [nation], [phone], [qq], [blog], [province], [city], [address], [zipcode], [note], [idcard], [isidcard], [idcardface], [idcardback], [idcardper], [idcarvalidity], [content]) VALUES (1, 1, 0, 1, NULL, N'admin', N'e10adc3949ba59abbe56e057f20f883e', N'', N'12677206@qq.com', 1, N'13481092810', 1, N'方配', 0, N'方配', N'', -1, 0, 0, N'', CAST(0x0000A31900000000 AS DateTime), 1, N'0.0.0.0', CAST(0x0000A3F200A6AACC AS DateTime), N'', N'', CAST(0x0000A31900000000 AS DateTime), 0, N'', N',,', N'', N'', N'', 0, N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', 0, N'', N'', N'', CAST(0x0000A3E1012F3C84 AS DateTime), N'')
INSERT [dbo].[FP_WMS_UserInfo] ([id], [roleid], [departid], [gradeid], [type], [username], [password], [password2], [email], [isemail], [mobile], [ismobile], [realname], [isreal], [nickname], [avatar], [sex], [exp], [credits], [regip], [joindatetime], [onlinestate], [lastip], [lastvisit], [secques], [authstr], [authtime], [authflag], [vipdate], [bday], [bloodtype], [height], [weight], [married], [education], [school], [job], [position], [politics], [company], [nation], [phone], [qq], [blog], [province], [city], [address], [zipcode], [note], [idcard], [isidcard], [idcardface], [idcardback], [idcardper], [idcarvalidity], [content]) VALUES (2, 5, 0, 1, N'', N'test', N'e10adc3949ba59abbe56e057f20f883e', N'', N'', 0, N'', 0, N'测试', 0, N'测试', N'', -1, 0, 0, N'127.0.0.1', CAST(0x0000A3E80176E05C AS DateTime), 0, N'127.0.0.1', CAST(0x0000A3EF012C1770 AS DateTime), N'', N'', CAST(0x0000A3E80176E05C AS DateTime), 0, N'', N',,', N'', N'', N'', 0, N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', N'', 0, N'', N'', N'', CAST(0x0000A3E80176E05C AS DateTime), N'')
SET IDENTITY_INSERT [dbo].[FP_WMS_UserInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[FP_WMS_UserGrade] ON
INSERT [dbo].[FP_WMS_UserGrade] ([id], [name], [stars], [explower], [expupper], [description]) VALUES (1, N'文盲', 0, 0, 100, N'')
INSERT [dbo].[FP_WMS_UserGrade] ([id], [name], [stars], [explower], [expupper], [description]) VALUES (2, N'书童', 1, 100, 300, N'')
INSERT [dbo].[FP_WMS_UserGrade] ([id], [name], [stars], [explower], [expupper], [description]) VALUES (3, N'导师', 3, 300, 600, N'')
INSERT [dbo].[FP_WMS_UserGrade] ([id], [name], [stars], [explower], [expupper], [description]) VALUES (4, N'砖家', 4, 600, 1000, N'')
INSERT [dbo].[FP_WMS_UserGrade] ([id], [name], [stars], [explower], [expupper], [description]) VALUES (5, N'叫兽', 5, 1000, 100000, N'')
SET IDENTITY_INSERT [dbo].[FP_WMS_UserGrade] OFF
GO
SET IDENTITY_INSERT [dbo].[FP_WMS_RoleInfo] ON
INSERT [dbo].[FP_WMS_RoleInfo] ([id], [name], [description], [desktopurl], [sorts], [menus], [desktop], [permission], [isadmin], [isupload], [isdownload]) VALUES (1, N'系统管理员', N'', N'', N'1,2', N'29', N'1,3,4,5', N'', 0, 0, 0)
INSERT [dbo].[FP_WMS_RoleInfo] ([id], [name], [description], [desktopurl], [sorts], [menus], [desktop], [permission], [isadmin], [isupload], [isdownload]) VALUES (2, N'游客', N'', NULL, NULL, NULL, NULL, N'', 0, NULL, NULL)
INSERT [dbo].[FP_WMS_RoleInfo] ([id], [name], [description], [desktopurl], [sorts], [menus], [desktop], [permission], [isadmin], [isupload], [isdownload]) VALUES (3, N'等待验证', N'', NULL, NULL, NULL, NULL, N'', 0, NULL, NULL)
INSERT [dbo].[FP_WMS_RoleInfo] ([id], [name], [description], [desktopurl], [sorts], [menus], [desktop], [permission], [isadmin], [isupload], [isdownload]) VALUES (4, N'禁止访问', N'', N'', N'', N'', N'', N'', 0, 0, 0)
INSERT [dbo].[FP_WMS_RoleInfo] ([id], [name], [description], [desktopurl], [sorts], [menus], [desktop], [permission], [isadmin], [isupload], [isdownload]) VALUES (5, N'注册用户', N'', N'', N'', N'29', N'3', N'', 1, 1, 1)
SET IDENTITY_INSERT [dbo].[FP_WMS_RoleInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[FP_WMS_Permission] ON
INSERT [dbo].[FP_WMS_Permission] ([id], [name], [flagpage], [isadd], [isupdate], [isdelete], [isaudit], [status]) VALUES (1, N'栏目管理', N'admin/sort/sortmanage.aspx
admin/sort/sortadd.aspx', 1, 1, 0, 0, 1)
INSERT [dbo].[FP_WMS_Permission] ([id], [name], [flagpage], [isadd], [isupdate], [isdelete], [isaudit], [status]) VALUES (2, N'用户查看', N'admin/user/usermanage.aspx', 0, 0, 0, 0, 1)
SET IDENTITY_INSERT [dbo].[FP_WMS_Permission] OFF
GO
SET IDENTITY_INSERT [dbo].[FP_WMS_MenuInfo] ON
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (1, 0, N'系统管理', N'', N'leftframe', 1, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (2, 1, N'系统常规设置', N'global', N'leftframe', 1, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (3, 2, N'系统基础配置', N'global/sysconfigmanage.aspx', N'mainframe', 1, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (5, 2, N'系统文件管理', N'global/sysfilesmanage.aspx', N'mainframe', 3, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (6, 2, N'系统缓存管理', N'global/cachemanage.aspx', N'mainframe', 4, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (7, 2, N'系统日志管理', N'global/syslogmanage.aspx', N'mainframe', 5, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (8, 13, N'邮箱认证配置', N'user/emailconfigmanage.aspx', N'mainframe', 7, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (9, 2, N'系统菜单管理', N'global/sysmenumanage.aspx', N'mainframe', 7, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (10, 40, N'上传图片配置', N'global/watermarkset.aspx', N'mainframe', 10, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (11, 40, N'上传附件类型', N'global/attachtypemanage.aspx', N'mainframe', 11, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (12, 40, N'上传附件管理', N'global/attachmanage.aspx', N'mainframe', 12, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (13, 1, N'系统用户设置', N'user', N'leftframe', 3, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (14, 13, N'用户角色管理', N'user/rolemanage.aspx', N'mainframe', 1, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (15, 13, N'操作权限管理', N'user/permissionmanage.aspx', N'mainframe', 2, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (16, 13, N'用户部门管理', N'user/departmentmanage.aspx', N'mainframe', 3, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (17, 13, N'用户级别管理', N'user/usergrademanage.aspx', N'mainframe', 4, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (18, 13, N'用户注册配置', N'user/regconfigmanage.aspx', N'mainframe', 5, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (19, 13, N'等待验证用户', N'user/userauditing.aspx', N'mainframe', 8, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (20, 13, N'系统用户管理', N'user/usermanage.aspx', N'mainframe', 9, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (21, 1, N'系统栏目设置', N'sort', N'leftframe', 2, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (22, 2, N'系统站点管理', N'global/sitemanage.aspx', N'mainframe', 2, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (23, 21, N'站点栏目管理', N'sort/sortmanage.aspx', N'mainframe', 3, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (25, 1, N'系统数据库设置', N'dbset', N'leftframe', 5, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (26, 25, N'在线运行SQL命令', N'dbset/runsql.aspx', N'mainframe', 1, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (27, 25, N'数据库备份还原', N'dbset/dbbackup.aspx', N'mainframe', 2, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (28, 25, N'数据库收缩优化', N'dbset/dbshrink.aspx', N'mainframe', 3, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (29, 0, N'内容管理', N'sorttree.aspx?channelid=1', N'leftframe', 2, 0)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (30, 2, N'系统桌面管理', N'global/desktopmanage.aspx', N'mainframe', 8, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (31, 21, N'栏目功能管理', N'sort/sortappmanage.aspx', N'mainframe', 2, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (32, 21, N'信息分类管理', N'sort/typemanage.aspx', N'mainframe', 4, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (34, 21, N'栏目频道管理', N'sort/channelmanage.aspx', N'mainframe', 1, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (36, 2, N'系统插件管理', N'global/pluginmanage.aspx', N'mainframe', 9, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (39, 25, N'重置库表标识列', N'dbset/dbreset.aspx', N'mainframe', 4, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (40, 1, N'上传文件设置', N'global', N'mainframe', 4, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (41, 2, N'系统应用管理', N'global/appmanage.aspx', N'mainframe', 3, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (42, 2, N'系统计划任务', N'global/taskmanage.aspx', N'mainframe', 11, 1)
INSERT [dbo].[FP_WMS_MenuInfo] ([id], [parentid], [name], [url], [target], [display], [system]) VALUES (43, 13, N'短信认证配置', N'user/smsconfigmanage.aspx', N'mainframe', 6, 1)
SET IDENTITY_INSERT [dbo].[FP_WMS_MenuInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[FP_WMS_DesktopInfo] ON
INSERT [dbo].[FP_WMS_DesktopInfo] ([id], [uid], [name], [icon], [url], [target], [description], [hidden], [system]) VALUES (1, 1, N'系统设置', N'/admin/images/icon32/sys.gif', N'global/sysconfigmanage.aspx', N'mainframe', N'', 0, 1)
INSERT [dbo].[FP_WMS_DesktopInfo] ([id], [uid], [name], [icon], [url], [target], [description], [hidden], [system]) VALUES (2, 1, N'站点管理', N'/admin/images/icon32/sites.gif', N'global/sitemanage.aspx', N'mainframe', N'', 0, 1)
INSERT [dbo].[FP_WMS_DesktopInfo] ([id], [uid], [name], [icon], [url], [target], [description], [hidden], [system]) VALUES (3, 1, N'角色管理', N'/admin/images/icon32/role.gif', N'user/rolemanage.aspx', N'mainframe', N'', 0, 1)
INSERT [dbo].[FP_WMS_DesktopInfo] ([id], [uid], [name], [icon], [url], [target], [description], [hidden], [system]) VALUES (4, 1, N'用户管理', N'/admin/images/icon32/users.gif', N'user/usermanage.aspx', N'mainframe', N'', 0, 1)
INSERT [dbo].[FP_WMS_DesktopInfo] ([id], [uid], [name], [icon], [url], [target], [description], [hidden], [system]) VALUES (5, 1, N'栏目管理', N'/admin/images/icon32/sort.gif', N'sort/sortmanage.aspx', N'mainframe', N'', 0, 1)
SET IDENTITY_INSERT [dbo].[FP_WMS_DesktopInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[FP_WMS_ChannelInfo] ON
INSERT [dbo].[FP_WMS_ChannelInfo] ([id], [name], [display], [markup]) VALUES (1, N'内容管理', 1, N'')
SET IDENTITY_INSERT [dbo].[FP_WMS_ChannelInfo] OFF
GO
SET IDENTITY_INSERT [dbo].[FP_WMS_AttachType] ON
INSERT [dbo].[FP_WMS_AttachType] ([id], [extension], [maxsize], [type]) VALUES (1, N'jpg', 2048, N'image')
INSERT [dbo].[FP_WMS_AttachType] ([id], [extension], [maxsize], [type]) VALUES (3, N'gif', 1024, N'image')
INSERT [dbo].[FP_WMS_AttachType] ([id], [extension], [maxsize], [type]) VALUES (4, N'zip', 4096, N'file')
INSERT [dbo].[FP_WMS_AttachType] ([id], [extension], [maxsize], [type]) VALUES (6, N'png', 2048, N'image')
INSERT [dbo].[FP_WMS_AttachType] ([id], [extension], [maxsize], [type]) VALUES (7, N'rar', 4096, N'file')
INSERT [dbo].[FP_WMS_AttachType] ([id], [extension], [maxsize], [type]) VALUES (8, N'doc', 4096, N'file')
INSERT [dbo].[FP_WMS_AttachType] ([id], [extension], [maxsize], [type]) VALUES (9, N'xls', 4096, N'file')
INSERT [dbo].[FP_WMS_AttachType] ([id], [extension], [maxsize], [type]) VALUES (10, N'flv', 102400, N'media')
INSERT [dbo].[FP_WMS_AttachType] ([id], [extension], [maxsize], [type]) VALUES (11, N'ppt', 4096, N'file')
SET IDENTITY_INSERT [dbo].[FP_WMS_AttachType] OFF
