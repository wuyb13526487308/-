<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdMPlay.aspx.cs" Inherits="CY.IotM.WebClient.AdM.AdMPlay" %>
<html >
<head>
<title> ImagePlayer Display </title>
        <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.form.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/detailview.js" type="text/javascript" ></script>

    <script type="text/javascript" src="../Scripts/AdM/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../Scripts/AdM/jquery.fn.imgplayer.min.js"></script>

</head>
<body>
<div id="imgPlayer_xq" style="margin-left:auto;margin-right:auto;margin-top:5px;display:none;">
<%=imgArray %>
</div>
<div id="console" style="font-size:12px;margin:5px;text-align:center;">
  <input id="btn-start" type="button" value="开始" /> 
  <input id="btn-stop" type="button" value="停止" />
</div>


<script type="text/javascript">
    var player = $("#imgPlayer_xq").playImgs({
        imgCSS: { 'width': '700px', 'height': '450px' },
        width: '700px',
        height: '450px',
        time: '5000',
        transition: 1,
        duration: 2000
    }).start();

    $("#btn-start").bind("click", player.start);
    $("#btn-stop").bind("click", player.stop);
</script>
</body>
</html>
