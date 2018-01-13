<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="CY.IotM.WebClient.Main" %>

<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE HTML>
<html>
<head>
    <title></title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            IotM.SetMainGridWidth(1);
            IotM.SetMainGridHeight(1);
            $('#mainTabs').tabs({ width: IotM.MainGridWidth, height: IotM.MainGridHeight });
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(1);
                var tab = $('#mainTabs').tabs('getSelected');
                var index = $('#mainTabs').tabs('getTabIndex', tab);
                $('#mainTabs').tabs({ width: IotM.MainGridWidth, height: IotM.MainGridHeight });
                $('#mainTabs').tabs('select', index);
            });
            //如果判断为电脑登陆
            //if ( !IotM.browser.versions.mobile) {
            //    MenuToMain("设备实时状态", "UserStatus.aspx");
            //}
        });
        function MenuToMain(text, url) {
            if ($('#mainTabs').tabs('exists', text)) {
                $('#mainTabs').tabs('select', text);

            } else {
                var content = '<iframe scrolling="auto" frameborder="0" src="' + url
            + '" name="' + text + '" style="width: 100%; height: 99%;"></iframe>';
                $('#mainTabs').tabs('add', { title: text, closable: true, content: content });
            };
        }
    </script>
</head>
<body>
    <div id="mainTabs">
    </div>
</body>
</html>
