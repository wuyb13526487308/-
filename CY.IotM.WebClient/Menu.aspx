<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="CY.IotM.WebClient.Menu" %>

<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>主菜单</title>
    <link href="Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="Scripts/IotM.js" type="text/javascript"></script>
    <script src="Scripts/IotM.Menu.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            IotM.Menu.LoadMenu();
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $('#treeMenu').tree("resize");
            });
        });
    </script>
</head>
<body>
    <div style="margin: 2px 0;">
    </div>
    <ul class="easyui-tree" id="treeMenu">
    </ul>
    <input type="hidden" id="hidMenuCode" />
</body>
</html>
