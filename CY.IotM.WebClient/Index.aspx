<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="CY.IotM.WebClient.Index" %>

<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>物联网表管理系统</title>
    <link id="easyuiTheme" href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Menu.js" type="text/javascript"></script>
     <script src="../Scripts/AppData.js" type="text/javascript"></script>
    <style type="text/css">
         body
        {
            font: 12px/12px Arial, sans-serif, Verdana, Tahoma;
            padding: 0;
            margin: 0;
        }
        a:link
        {
            text-decoration: none;
        }
        a:visited
        {
            text-decoration: none;
        }
        a:hover
        {
            text-decoration: underline;
        }
        a:active
        {
            text-decoration: none;
        }
        .cs-north
        {
            height: 30px;
        }
        .cs-north-bg
        {
            width: 100%;
            height: 100%;
            background: url(Images/Banal.jpg) repeat-x bottom;
        }
        .cs-north-logo
        {
            position: absolute;
            top: 8px;
            color: #E7E7E7;
            height: 16px;
            padding-left: 45px;
            font-size: 14px;
            font-weight: bold;
            background: url('Images/logo.png') no-repeat left;
        }
        .cs-west
        {
            width: 200px;
            padding: 0px;
        }
        .cs-south
        {
            height: 25px;
            background: url('Scripts/easyui1.3.3/themes/pepper-grinder/images/ui-bg_fine-grain_15_ffffff_60x60.png') repeat-x;
            padding: 0px;
            text-align: center;
        }
        .cs-navi-tab
        {
            padding: 5px;
            display: block;
            line-height: 18px;
            height: 18px;
            padding-left: 16px;
            text-decoration: none;
            border: 1px solid white;
            border-bottom: 1px #E5E5E5 solid;
        }
        .cs-navi-tab:hover
        {
            background: #FFEEAC;
            border: 1px solid #DB9F00;
        }
        .cs-tab-menu
        {
            width: 120px;
        }
        .cs-home-remark
        {
            padding: 10px;
        }
        .wrapper
        {
            float: right;
            height: 30px;
            margin-left: 10px;
        }
        .ui-skin-nav
        {
            float: right;
            padding: 0;
            margin-right: 10px;
            list-style: none outside none;
            height: 20px;
            visibility: hidden;
        }
        
        .ui-skin-nav .li-skinitem
        {
            float: left;
            font-size: 12px;
            line-height: 30px;
            margin-left: 10px;
            text-align: center;
        }
        .ui-skin-nav .li-skinitem span
        {
            cursor: pointer;
            width: 10px;
            height: 10px;
            display: inline-block;
        }
        .ui-skin-nav .li-skinitem span.cs-skin-on
        {
            border: 1px solid #FFFFFF;
        }
        
        .ui-skin-nav .li-skinitem span.gray
        {
            background-color: gray;
        }
        .ui-skin-nav .li-skinitem span.pepper-grinder
        {
            background-color: #BC3604;
        }
        .ui-skin-nav .li-skinitem span.blue
        {
            background-color: blue;
        }
        .ui-skin-nav .li-skinitem span.cupertino
        {
            background-color: #D7EBF9;
        }
        .ui-skin-nav .li-skinitem span.dark-hive
        {
            background-color: black;
        }
        .ui-skin-nav .li-skinitem span.sunny
        {
            background-color: #FFE57E;
        }
        .img-menu
        {
            height: 18px;
            width: 18px;
        }
        .space
        {
            color: #E7E7E7;
        }
        .l-topmenu-welcome
        {
            position: absolute;
            height: 24px;
            line-height: 24px;
            right: 30px;
            top: 3px;
            color: #070A0C;
        }



    </style>
    <script type="text/javascript">
        $(function () {
            IotM.CheckLogin();
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
            });
        });


        //$(function () {
        //    var easyuiTheme = "default";
        //    var cookieTheme = "gray";//这里呢 我们可以将想要更换的主题名称存到cookie中，每次加载时JS操作读取下，这样就可以达到个性化更换主题的目的了，这里我就不详细阐述了
        //    var href = $("#easyuiTheme").attr("href").replace(easyuiTheme, cookieTheme);
        //    $("#easyuiTheme").attr("href", href);
        //});


        function addTab(title, url) {
            if ($('#tabs').tabs('exists', title)) {
                $('#tabs').tabs('select', title); //选中并刷新
                var currTab = $('#tabs').tabs('getSelected');
                var url = $(currTab.panel('options').content).attr('src');
                if (url != undefined && currTab.panel('options').title != '首页') {
                    $('#tabs').tabs('update', {
                        tab: currTab,
                        options: {
                            content: createFrame(url)
                        }
                    })
                }
            } else {
                var content = createFrame(url);
                $('#tabs').tabs('add', {
                    title: title,
                    content: content,
                    closable: true
                });
            }
            tabClose();
        }
        function createFrame(url) {
            var s = '<iframe scrolling="no" frameborder="0"  src="' + url + '" style="width:100%;height:99.9%;"></iframe>';
            return s;
        }

        function tabClose() {
            /*双击关闭TAB选项卡*/
            $(".tabs-inner").dblclick(function () {
                var subtitle = $(this).children(".tabs-closable").text();
                $('#tabs').tabs('close', subtitle);
            })
            /*为选项卡绑定右键*/
            $(".tabs-inner").bind('contextmenu', function (e) {
                $('#mm').menu('show', {
                    left: e.pageX,
                    top: e.pageY
                });

                var subtitle = $(this).children(".tabs-closable").text();

                $('#mm').data("currtab", subtitle);
                $('#tabs').tabs('select', subtitle);
                return false;
            });
        }
        //绑定右键菜单事件
        function tabCloseEven() {
            //刷新
            $('#mm-tabupdate').click(function () {
                var currTab = $('#tabs').tabs('getSelected');
                var url = $(currTab.panel('options').content).attr('src');
                if (url != undefined && currTab.panel('options').title != '首页') {
                    $('#tabs').tabs('update', {
                        tab: currTab,
                        options: {
                            content: createFrame(url)
                        }
                    })
                }
            })
            //关闭当前
            $('#mm-tabclose').click(function () {
                var currtab_title = $('#mm').data("currtab");
                $('#tabs').tabs('close', currtab_title);
            })
            //全部关闭
            $('#mm-tabcloseall').click(function () {
                $('.tabs-inner span').each(function (i, n) {
                    var t = $(n).text();
                    if (t != '首页') {
                        $('#tabs').tabs('close', t);
                    }
                });
            });
            //关闭除当前之外的TAB
            $('#mm-tabcloseother').click(function () {
                var prevall = $('.tabs-selected').prevAll();
                var nextall = $('.tabs-selected').nextAll();
                if (prevall.length > 0) {
                    prevall.each(function (i, n) {
                        var t = $('a:eq(0) span', $(n)).text();
                        if (t != '首页') {
                            $('#tabs').tabs('close', t);
                        }
                    });
                }
                if (nextall.length > 0) {
                    nextall.each(function (i, n) {
                        var t = $('a:eq(0) span', $(n)).text();
                        if (t != '首页') {
                            $('#tabs').tabs('close', t);
                        }
                    });
                }
                return false;
            });
            //关闭当前右侧的TAB
            $('#mm-tabcloseright').click(function () {
                var nextall = $('.tabs-selected').nextAll();
                if (nextall.length == 0) {
                    //msgShow('系统提示','后边没有啦~~','error');
                    alert('后边没有啦~~');
                    return false;
                }
                nextall.each(function (i, n) {
                    var t = $('a:eq(0) span', $(n)).text();
                    $('#tabs').tabs('close', t);
                });
                return false;
            });
            //关闭当前左侧的TAB
            $('#mm-tabcloseleft').click(function () {
                var prevall = $('.tabs-selected').prevAll();
                if (prevall.length == 0) {
                    alert('到头了，前边没有啦~~');
                    return false;
                }
                prevall.each(function (i, n) {
                    var t = $('a:eq(0) span', $(n)).text();
                    $('#tabs').tabs('close', t);
                });
                return false;
            });

            //退出
            $("#mm-exit").click(function () {
                $('#mm').menu('hide');
            })
        }




        $(function () {
            //初始菜单栏
            Application.data.getLeftMenu(function (js) {
                var isSelect = false;
                var pushData = eval('(' + js + ')');
                pushData = eval('(' + pushData.TxtMessage + ')');
                var leftMenu = $(".cs-west div");
                for (var i = 0, l = pushData.length; i < l; i++) {
                    //是否选中
                    isSelect = false;
                    if (i == 0) isSelect = true;

                    var menu = $("<div></div>");
                    //添加子项
                    if (pushData[i].Children.length > 0) {
                        var cMenu = pushData[i].Children;
                        for (var j = 0, k = cMenu.length; j < k; j++) {
                            menu.append("<a class='cs-navi-tab' href=javascript:addTab('" + cMenu[j].Name + "','" + cMenu[j].Url
                            + "');><img class='img-menu' align='middle' border=0 src='" + cMenu[j].Img
                            + "' />&nbsp;&nbsp;" + cMenu[j].Name + "</a>");
                        }
                    }
                    //添加分组
                    $('.easyui-accordion').accordion('add', {
                        title: pushData[i].Name,
                        content: menu,
                        iconCls: 'icon-reload',
                        selected: isSelect
                    });
                }
            }, this);

            //tab页操作事件
            tabCloseEven();

            var themes = {
                'black': 'themes/black/easyui.css',
                'pepper-grinder': 'themes/pepper-grinder/easyui.css',
                'bootstrap': 'themes/bootstrap/easyui.css',
                'default': 'themes/default/easyui.css',
                'gray': 'themes/gray/easyui.css',
                'metro': 'themes/metro/easyui.css',
                'cupertino': 'themes/cupertino/easyui.css'
            };

            var skins = $('.li-skinitem span').click(function () {
                var $this = $(this);
                if ($this.hasClass('cs-skin-on')) return;
                skins.removeClass('cs-skin-on');
                $this.addClass('cs-skin-on');
                var skin = $this.attr('rel');
                $('#appstyle').attr('href', themes[skin]);
                setCookie('cs-skin', skin);
                skin == 'dark-hive' ? $('.cs-north-logo').css('color', '#FFFFFF') : $('.cs-north-logo').css('color', '#000000');
            });

            if (getCookie('cs-skin')) {
                var skin = getCookie('cs-skin');
                $('#appstyle').attr('href', themes[skin]);
                $this = $('.li-skinitem span[rel=' + skin + ']');
                $this.addClass('cs-skin-on');
                skin == 'dark-hive' ? $('.cs-north-logo').css('color', '#FFFFFF') : $('.cs-north-logo').css('color', '#000000');
            }
        });

        function ReFreshCurrentPage() {
            var currTab = $('#tabs').tabs('getSelected');
            var url = $(currTab.panel('options').content).attr('src');
            if (url != undefined && currTab.panel('options').title != '首页') {
                $('#tabs').tabs('update', {
                    tab: currTab,
                    options: {
                        content: createFrame(url)
                    }
                })
            }
        }

    </script>
</head>
<body>
    <%--<div id="wrap" class="easyui-layout" fit="true">
        <div id="topDiv" data-options="region:'north',border:1,onCollapse:IotM.WindowResize,onExpand:IotM.WindowResize,height:49">
            <table style="width:100%">
                <tr>
                    <td style="width: 35%; text-align: right">
                        <div>
                            <span style="font-size: large;" id="txtCompanyInfo"></span>
                            <div id="txtCompany"></div>
                        </div>
                    </td>                    
                </tr>
            </table>
        </div>
       
       <div id="menuDiv" data-options="region:'west',split:true,border:0,onCollapse:IotM.WindowResize,onExpand:IotM.WindowResize"
            title="主菜单" style="width: 150px; height: auto">
            <iframe src="Menu.aspx" frameborder="0" scrolling="no" name="menuFrame" id="menuFrame"
                style="width: 100%; height: 99%"></iframe>
        </div>
        <div id="mainDiv" data-options="region:'center',split:true,border:0" style="width: 100%; height: 100%">
            <iframe src="Main.aspx" frameborder="0" scrolling="no" name="mainFrame" id="mainFrame"
                style="width: 100%; height: 99%"></iframe>
        </div>


        <div id="bottomDiv" data-options="region:'south',split:true,border:1" style="height: 30px; text-align: center; vertical-align: middle;">
          
        </div>
    </div>--%>


     <div id="wrap" class="easyui-layout" fit="true">


       <div region="north" border="true" class="cs-north">
        <div class="cs-north-bg">
            <div id="CompanyName" class="cs-north-logo">
                物联网表管理系统</div>
            <div class="l-topmenu-welcome">
                  <div>
                   <%-- <span style="font-size: large;" id="txtCompanyInfo"></span>--%>
                   <div style="color: #E7E7E7" id="txtCompany"></div>
                 </div>
            </div>
        </div>
    </div>  




        <div region="west" border="true" split="true" title="功能面板" class="cs-west">
            <div class="easyui-accordion" fit="true" border="false">
            </div>
        </div>



        <div id="mainDiv" region="center" border="true" >
           
       

        <div id="tabs" class="easyui-tabs" fit="true" border="false" data-options="tools:'#tab-tools'" >
            <div title="首页">
                <div class="cs-home-remark">
                   
                </div>
            </div>
        </div>
        <div id="tab-tools">
		    <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-reload'" onclick="ReFreshCurrentPage()"></a>
	    </div>

      </div>

       


      <div region="south" border="false" class="cs-south">
        <div style="margin:10px;">
            郑州创源智能设备有限公司@2015</div>
        </div>
    </div>

    <div id="mm" class="easyui-menu cs-tab-menu">
        <div id="mm-tabupdate">
            刷新</div>
        <div class="menu-sep">
        </div>
        <div id="mm-tabclose">
            关闭</div>
        <div id="mm-tabcloseother">
            关闭其他</div>
        <div id="mm-tabcloseall">
            关闭全部</div>
    </div>


</body>
</html>
