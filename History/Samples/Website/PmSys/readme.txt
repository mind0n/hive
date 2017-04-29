ASP.NET权限管理系统(FrameWork) 1.0.7 Release 说明文档

1.起源
在我们开发软件过程中,经常会需要开发一个后台管理程序.用于管理我们开发的前台软件.
当你开发的软件越来越多的时候,你的管理后台也会越来越多.这时如果有一个统一的管理后台.来进来管理.有统一台帐号,统一的授权来进行多个后台的管理就好了.
FrameWork就是在这样的需求下产生了. 

2.功能
其主要的功能就是,进行后台管理模块整合.
1)其可以支持多个应用,多个模块的权限管理.
2)支持Access和MsSql200/2005及Oracle9i以上.
3)采用角色来进行权限的授权,每个用户可以属于多个角色,享有交差权限.
4)整合方便,权限检测采用httpmodule方式检测.基本不用对原有程序进行修改,便可将原有程序进行整合.
5)可视化编辑,全新的Menu和Tab控件.支持拖拉式编辑.
6)统一的事件日志管理,所有登陆操作都有安全记录.
7)每个模块的每个栏目对应一个目录, 栏目的权限默认抽象分为(查看/新增/修改/删除/排序/打印/备用A/备用B),每栏目权限可最多扩展为20个自定义权限.
8)可通过目录中web.config来进行目录文件权限配置,可直接将某个文件名设定到相对应的权限上.

3.开发环境
Vs2005 sp1 + win2003 标准版 + Mssql2000/2005

4.初始用户名:admin 密码:admin



感谢您选择 ASP.NET权限管理系统(FrameWork) 产品。希望我们的努力能为您提供一个高效快速和强大的权限管理开发框架。

www.supesoft.com 为 ASP.NET权限管理系统(FrameWork) 产品的开发商，ASP.NET权限管理系统(FrameWork) 官方讨论区网址为 http://FrameWork.supesoft.com/bbs。

ASP.NET 权限管理系统(FrameWork) 著作权已在中华人民共和国国家版权局注册，著作权受到法律和国际公约保护。使用者：无论个人或组织、盈利与否、用途如何（包括以学习和研究为目的），均需仔细阅读本协议，在理解、同意、并遵守本协议的全部条款后，方可开始使用 ASP.NET权限管理系统(FrameWork) 软件。



协议许可的权利

   1. 您可以在完全遵守本最终用户授权协议的基础上，将本软件应用于非商业用途，而不必支付软件版权授权费用。
   2. 您可以在协议规定的约束和限制范围内修改 ASP.NET权限管理系统(FrameWork) 源代码(如果被提供的话)或界面以适应您的网站要求。
   3. 您拥有使用本软件构建的管理系统全部资料及相关信息的所有权，并独立承担与资料和信息的相关法律义务。
   4. 获得商业授权之后，您可以将本软件应用于商业用途，同时依据所购买的授权类型中确定的技术支持期限、技术支持方式和技术支持内容，自购买时刻起，在技术支持期限内拥有通过指定的方式获得指定范围内的技术支持服务。商业授权用户享有反映和提出意见的权力，相关意见将被作为首要考虑，但没有一定被采纳的承诺或保证。


协议规定的约束和限制

   1. 未获商业授权之前，不得将本软件用于商业用途（包括但不限于企业网站、经营性网站、以营利为目或实现盈利的网站）。购买商业授权请访问http://FrameWork.supesoft.com了解详情。
   2. 不得对本软件或与之关联的商业授权进行出租、出售、抵押或发放子许可证。
   3. 无论如何，即无论用途如何、是否经过修改或美化、修改程度如何，只要使用 ASP.NET权限管理系统(FrameWork) 的整体或任何部分，未经许可，后台管理页面页脚处的 Supesoft 名称和http://www.supesoft.com 的链接都必须保留，而不能清除或修改。
   4. 禁止在 ASP.NET权限管理系统(FrameWork) 的整体或任何部分基础上以发展任何派生版本、修改版本或第三方版本用于重新分发。
   5. 如果您未能遵守本协议的条款，您的授权将被终止，所被许可的权利将被收回，并承担相应法律责任。


有限担保和免责声明

   1. 本软件及所附带的文件是作为不提供任何明确的或隐含的赔偿或担保的形式提供的。
   2. 用户出于自愿而使用本软件，您必须了解使用本软件的风险，在尚未购买产品技术服务之前，我们不承诺提供任何形式的技术支持、使用担保，也不承担任何因使用本软件而产生问题的相关责任。
   3. Supesoft不对使用本软件构建系统中的资料或信息承担责任。


附注:

有关 ASP.NET权限管理系统(FrameWork) 最终用户授权协议、商业授权与技术服务的详细内容，均由 ASP.NET权限管理系统(FrameWork) 官方网站独家提供。www.supesoft.com 拥有在不事先通知的情况下，修改授权协议和服务价目表的权力，修改后的协议或价目表对自改变之日起的新授权用户生效。

电子文本形式的授权协议如同双方书面签署的协议一样，具有完全的和等同的法律效力。您一旦开始安装 ASP.NET权限管理系统(FrameWork) ，即被视为完全理解并接受本协议的各项条款，在享有上述条款授予的权力的同时，受到相关的约束和限制。协议许可范围以外的行为，将直接违反本授权协议并构成侵权，我们有权随时终止授权，责令停止损害，并保留追究相关责任的权力。

1.0.7版更新记录

1.TabOptionWebControls能自动拉出，但HeadMenuWebControls却不能
2.支持在web.config中配置Url参数权限判断
3.用户管理模块中,用户类型增加管理员.只有管理员和超用用户才能设置角色.且只能操作自己创建的角色.超级用户可以设置所有角色给用户.禁止登陆必需是管理员和超级用户才能使用 
4.角色管理中必需是管理员才能进入角管理色模块.且管理员只能操作自己创建的角色.超级用户可以操作所有用户创建角色.
5.在角色管理中,当前用户可分配的角色权限不能超过自身的权限.
6.异常日志,增加出错网址记录.
7.增加流行菜单icon图标.
8.增加自定义权限功能,每模块可自定义20个权限
9.增加查看自己登陆日志 ok
10.修改应用字段,增加title,disabled,Readonly属性
11.修改在线用户处理类,无法正常清除到期用户.
12.修改出错时,只针对manager目录跳转
13.http://www.supesoft.com/bbs/disp.asp?B_T_ID=851
14.http://www.supesoft.com/bbs/disp.asp?B_T_ID=855
15.修正ManagementClass无权限读取错误
16在Firefox下选择部门后，清除再选择部门，文本框为空, 树菜单在forefox下打开和关闭无效?
17.将在线用户缓存放入统一缓存管理中. 
18.将缓存调用统一接口 
19.增加为FileTxtLogs类增加 WriteServiceLog 方法,用于记录服务程序日志.
20.应用字段模块中,点击列表应用字段无效
21.修改应用模块编码,删除原对应模块编码的权限.
22.修改用户多角色权限重叠判断bug.
23.修改操作提示信息使用gzip压缩存储为cookies.

1.0.7全新安装说明:
1.数据库安装说明:
	a)Access数据库,直接使用.
	b)Sql数据库,执行FrameWork.web\Manager\database\mssql\目录中的:
	  1.FrameWork.sql
	  2.存储过程.sql
      3.数据初始化脚本.sql
      4.完成安装.
    c)Oracle数据库,执行FrameWork.web\Manager\database\Oracle\目录中的:
      1.Oracle.Sql
      2.完成安装.
2.前台web直接复制文件到网站中即可.
3.如需修改数据库或都配置,请打开web.config进行修改,有说明.
	  

1.0.6升级至1.0.7更新说明:
1.数据库更新:
  a)Access数据库
    1.打开FrameWork.web\Manager\database\access\FrameWork.config.
    2.为sys_Module表增加,M_Icon字段,类型为字符,长度255.允许空值.
    3.为sys_Roles表增加R_UserID字段,类型为数值.打开sys_Roles表,将R_UserID字段值设置为1.
    4.打开FrameWork.web\Manager\database\access\DBBack.mdb,将sys_ModuleExtPermission表复制到FrameWork.config数据库.
  b)Sql数据库,执行FrameWork.web\Manager\database\mssql\目录中的:
    1.FrameWork1.0.7表结构更新.sql
    2.FrameWork1.0.7存储过程更新.sql
    3.FrameWork1.0.7数据更新.sql
    3.完成更新.
  c)Oracle数据库,执行FrameWork.web\Manager\database\Oracle\目录中的:
    1.FrameWork1.0.7表结构更新(Oracle).sql
    2.FrameWork1.0.7数据更新(Oracle).sql
    3.完成更新.
2.前台Web更新将FrameWork.web\manager目录中所有文件替换原有文件.
3.完成更新.

1.0.6版更新记录

1.角色管理,可以增加用户 
2.重启应用,跳转路径问题 
3.修改验证码,验证码修改为默认不显示，出错次数到就显示。
4.左侧经典菜单中，点击选中项高亮或变色，在隐藏、开启后仍保留，以记录当前状态 
5.对于 sys_FieldValue表 增加 V_Code 列,增加点文字选择 
6.增加table_body和table_none不带宽度css 
7.实现缓存接口,通过扩展可支持多站点统一权限库布署 
8.当时间字段可以为空的,增加DateTime? 
9.应用及其模块分类和模块后再仅仅删除应用将删除不干净 
10.部门反向获取用户列表 
11.是否自动检测有新版本更新配置 
12.app_HttpGZip,app_AuthMethod,app_Auth 增加是否在manager目录判断 
13.事件日志管理中用户的登陆系统、退出系统操作，用户名无法记录
14.角色,增加用户,没有进行权限判断,搜索日志问题
15.用户管理页,必须是"超级用户"或"备用A"权限才可以修改用户角色和密码.admin帐号密码和状态 不能被修改 
16.增加"日志列表"和"用户列表" 表头排序功能. 
17.登陆login.aspx检测是否登陆.

更新说明:
1.数据库更新:
  a)Access数据库,打开FrameWork.web\Manager\database\access\FrameWork.config,为sys_FieldValue表增加,V_Code字段,类型为字符,长度100.允许空值.
  b)Sql数据库,执行FrameWork.web\Manager\database\mssql\目录中的:FrameWork1.06表结构更新.sql,FrameWork1.0.6存储过程更新.sql二个文件,完成更新.
  c)Oracle数据库,执行FrameWork.web\Manager\database\Oracle\目录中的:FrameWork1.06表结构更新(Oracle).sql文件完成更新.
2.打开FrameWork.Web目录中的web.config文件,在<appSettings>节点增加如下节点:
    <!--缓存类配置(命名空间) FrameWorkCache.IloveYou-->
    <add key="Cachenamespace" value="FrameWork"/>
    <!--缓存类配置(类名) FrameWorkCache.IloveYou.FrameWorkCacheRemoting-->
    <add key="CacheclassName" value="FrameWork.HttpWebCache"/>
    <!--Remoting服务器地址-->
    <add key="FrameWorkCacheService" value="tcp://localhost:9742/FrameWorkCache.CacheManager"/>
3.将FrameWork.web\manager目录中所有文件替换原有文件.
4.完成更新.

1.0.5版更新记录
 
1.数据库配置出错,死循环Message.aspx出错
2.分类管理移动分类支持firefox.
3.若上传文件为RAR，打开的话就会报错
4.更新一些js支持firefox
5.将原来根目录文件移到Manager子目录
6.写日志目录配置(在web.config中配置),上传文件目录配置(在后台系统设置中配置)
7.TabOptionWebControls控件在vs2008下设计视图中不能正常显示
8.增加在线用户表,可以设置在线用户是存放于数据库或缓存中
9.修正Access数据库分页最后页显示不正确
10.修正MultiListBox控件,选项重复移除错乱问题
11.提示信息框修改为session保存

更新说明:
1.将你原有FrameWork.Web\module\目录下你自己开发的模块文件(不是FrameWork文件夹)复制到manager\module里面
2.修改刚复制所有的aspx文件中的第一行,找到MasterPageFile="~/MasterPage/PageTemplate.Master"修改为MasterPageFile="~/Manager/MasterPage/PageTemplate.Master".
3.在线用户表更新:
  a)Access数据库,打开FrameWork.web\Manager\database\access\DBBack.mdb,将sys_Online表复制到原有的数据库.
  b)Sql数据库,执行FrameWork.web\Manager\database\mssql\SQL在线用户数据库升级.sql
  c)Oracle数据库,执行FrameWork.web\Manager\database\Oracle\Oracle在线用户数据库升级.sql
4.完成更新.

1.0.4版更新记录
 
1.增加Oracle数据库支持
2.增加网页http压缩功能
3.没有权限按钮下,firefox显示问题
4.数据库无修改，直接替换原有文件即可升级。

1.0.3版更新记录
 
1.修改写日志文件无效
2.修改出错事件先后顺序
3.应用模块管理中,查看模块,默认没有选中
4.修改日期输入出错1999/11/22
5.修改查询分页默认值为10到99999999
6.增加对Firefox浏览器支持
7.自动检测更新出错
8.数据库无修改，直接替换原有文件即可升级。

1.0.2版更新记录
 
1.修改写日志文件方法 
2.修改上传图片缩图效果 
3.应用模块配置,点击模块分类.默认显示模块列表
4.操作日志,显示IP增加查询链接 
5.应用模块管理,增加子分类数 
6.直接关闭ie,出现提示.
7.应用模块列表,增加显示应用ID
8.增加对ASP.NET通用权限管理系统(FrameWork)专用代码生成工具DDBuildTools支持.介绍请访问:http://framework.supesoft.com/help/?DDBuildTools.htm
9.用户资料增加显示最后操作时间及IP
10.数据库无修改，直接替换原有文件即可升级。

1.0.1版 更新说明：
1.管理员信息只能是管理员才能修改和增加.
2.只有管理员才能设定角色,admin帐号信息无法修改或删除
3.登陆增加是否IE浏览器检测提示 
4.增加上传图片类型,大小限制.增加文件读取程序
5.增加Licensed正确检测. 
6.增加同一IP登陆密码出错5次禁止30 分钟登陆
7.删除部门,无法显示出部门
8.权限检测,增加通过方法属性来进行检测
9.用户信息增加删除标识,日志查询可查删除用户
10.数据库无修改，直接替换原有文件即可升级。

1.0.0版 更新说明：
1.一些操作上的小修改。
2.增加IP地址查询链接
3.取消模块名称必需建在Module下的权限检测限制。
4.模块目录未设置的权限的文件全都默认为"查看"权限(目前支持在web.config配置文件权限或在程序中使用FrameWorkPermission.CheckPermissionVoid方法来检测权限)
5.增加内存使用状态查询。
6.增加开发模式下用户在线时间过期无效功能(重新编译文件后不需要再重新登陆，需要些功能请将Web.config中的OnlineMinute设为0)
7.授权修改为BSD协议。
8.数据库无修改，直接替换原有文件即可升级。