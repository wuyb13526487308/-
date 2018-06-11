<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonitorShow.aspx.cs" Inherits="CY.IotM.WebClient.IotM.Monitor.MonitorShow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>获取监视信息</title>
    <link href="../../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../../Scripts/highcharts3.0.5/highcharts.js"></script>
    <script type="text/javascript">

        var dscId = "";//采集端服务器Id        
        var yCpu = 0;//cpu占用率值
        var ylink = 0;//个数
        var showchartstate = false; //是否显示图表
        var timeout = false; //启动及关闭按钮  
        var dscIdArray = new Array();
        function timer() {
            if (timeout) return;
            getOneMonitorInfo(); 
            setTimeout(timer, 1000); //time是指本身,延时递归调用自己,1000为间隔调用时间,单位毫秒  
             
        }
        $(function () {
            timer();
            setInterval(function () {
                SaveHisData();
            },1000)
            $('#dlg').dialog('close');
            Highcharts.setOptions({
                global: {
                    useUTC: false
                }
            }); 
        });
        function getOneMonitorInfo() {
            if (dscId == "") {
                return;
            }
            if (showchartstate == false) {
                showChart();//显示图表
                showchartstate = true;
            }
            //是否保存监视信息
            //var hasChk = $('#cb_savedata').is(':checked');
            $.post("../../Handler/Monitor/MonitorShow.ashx?AType=oneinfo",
                 { dscId: dscId },
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              var info = eval('(' + data.TxtMessage + ')');
                              $("#dscname").html(dscId);
                              $("#slinkcount").html(info.LinkCount);
                              $("#scpu").html(info.Cpu);
                              yCpu = info.Cpu;
                              ylink = info.LinkCount;
                              $("#sneicun").html(info.Memory);
                              $("#stime").html(new Date().toLocaleString());
                              $("#serrormsg").html("");
                          }
                          else
                              $("#serrormsg").html(data.TxtMessage);

                      }
                  }, "json");
        }

        //显示图表
        var chart;
        function showChart() {
            chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'container',
                    type: 'spline',
                    animation: Highcharts.svg,
                    // don't animate in old IE 
                    marginRight: 10,
                    events:
                        {
                            load: function () {
                                // set up the updating of the chart each second 
                                var series = this.series[0];
                                var series1 = this.series[1];
                                setInterval(function () {
                                    var x = (new Date()).getTime();
                                    series.addPoint([x, yCpu], true, true);
                                    series1.addPoint([x, ylink], true, true);
                                }, 1000);
                            }
                        }
                },
                title: {
                    text: 'CPU占用率'
                },
                xAxis: {
                    type: 'datetime',
                    tickPixelInterval: 150
                },
                plotOptions: {
                    spline: {
                        lineWidth: 2,
                        marker: {
                            enabled: false
                        }
                    }
                },
                yAxis: {
                    min: 0, max: 1000,
                    title: { text: 'CPU占用率%' },
                    plotLines: [{ value: 0, width: 1, color: '#808080' }]
                },
                credits: {
                    enabled: false // remove high chart logo hyper-link  
                },
                series: [
                    {
                        name: 'CPU占用率', data: (function () {
                            // generate an array of random data 
                            var data = [], time = (new Date()).getTime(), i;
                            for (i = -119; i <= 0; i++) {
                                data.push({ x: time + i * 1000, y: 0 });
                            }
                            return data;
                        })()
                    },
                     {
                         name: '连接数量', data: (function () {
                             // generate an array of random data 
                             var data = [], time = (new Date()).getTime(), i;
                             for (i = -119; i <= 0; i++) {
                                 data.push({ x: time + i * 1000, y: 0 });
                             }
                             return data;
                         })()
                     }
                ]
            });
        }

        //删除图表
        function clearChart() {
            if (chart != null) {
                showchartstate = false;
                chart.destroy();
            }
        }
        //弹出历史监视信息窗口
        function showdialog()
        {
            $('#dlg').dialog('open');
            InitTreeData();
        }
        //关闭历史监视信息窗口
        function closedialog()
        {
            $('#dlg').dialog('close');
            $('#tree2').val("");
        }
        function SaveHisData()
        {
            //是否保存监视信息
            var hasChk = $('#cb_savedata').is(':checked');
            if (hasChk == false)
            {
                return;
            }
            var nodes = $('#tt').tree('getChildren');
            var dscIdList = "";
            for (var i = 0; i < nodes.length; i++) {
                if (nodes[i].id != "0") {
                    dscIdList += nodes[i].id + ",";
                }
            }
            if (dscIdList.length > 0) {
                dscIdList = dscIdList.substring(0, dscIdList.length - 1);

                $.post("../../Handler/Monitor/MonitorShow.ashx?AType=SAVEHISDATA",
                    { dscIdList: dscIdList },
                     function (data, textStatus) {
                         if (textStatus == 'success') {
                             if (data.Result) {
                             }
                             else
                                 $("#serrormsg").html(data.TxtMessage);

                         }
                     }, "json");
            }
        }
    </script>
    <style type="text/css"> 
    </style>
    </head>
<body>  
    <div id="cc" class="easyui-layout" style="width:1200px;height:630px;"> 
		<div data-options="region:'west',split:true" title="服务器列表" style="width:200px;">
             <ul id="tt" class="easyui-tree"  data-options="url:'../../Handler/Monitor/MonitorShow.ashx?AType=list',method:'get',animate:true,checkbox:false,
                 onClick:function(node)
                 {
                 dscId=node.id;clearChart();
                 
                 }">

             </ul>
		</div>
		<div data-options="region:'center',title:'服务器监视信息'">          
            <table>
                <tr><td>服务器编号：</td><td><span id="dscname"></span></td></tr>
                <tr> <td>表连接数量：</td><td><span id="slinkcount"></span></td> </tr>
                <tr> <td>CPU占用率：</td><td><span id="scpu"></span>%</td> </tr>
                <tr> <td>内存使用：</td> <td><span id="sneicun"></span>G</td> </tr>
                <tr> <td>当前时间：</td><td><span id="stime"></span></td> </tr>               
            </table>
            <div id="container" style=" height:490px">

            </div>
            <span id="serrormsg"></span> 
		</div>
	</div>
    <div style="margin:20px 0;">
        <input type="checkbox" id="cb_savedata" checked="checked" />保存监视信息
		<a href="javascript:void(0)" class="easyui-linkbutton" onclick="showdialog()">历史监视信息查看</a> 
	</div>
    <div id="dlg" class="easyui-dialog" title="Basic Dialog"  data-options="iconCls:'icon-save',resizable:true" style="width:1100px;height:630px;padding:15px">
		 <div id="ttt" class="easyui-layout" style="width:100%;height:98%;"> 
		<div data-options="region:'west',split:true" title="服务器列表" style="width:200px;">
             <ul id="tree2" class="easyui-tree" style="width:200px;">

             </ul>
		</div>
		<div data-options="region:'center',title:'服务器监视信息'"  >          
             
            <div id="containerHis" style="width:100%;height:100%" >

            </div> 
		</div>
	</div>
	</div>

</body>
</html>
<script type="text/javascript">
    //获取数据文件目录
    function InitTreeData() {
        $('#tree2').tree({
            url: '../../Handler/Monitor/MonitorShow.ashx?AType=FILELIST',
            checkbox: false,
            onClick: function (node) {
                filename = node.text; 
                binddata();
                if (chartHis != null) { 
                    chartHis.destroy();
                } 
            },
            loadFilter: function (data, textStatus) {                
                if (data.Result) {
                    //将json数据转成对象
                    var info = eval('(' + data.TxtMessage + ')');
                    var jsonstr = "";
                    jsonstr = "[";
                    //拼接显示数据
                    for (var i = 0; i < info.rows.length; i++) {
                        jsonstr += "{\"id\":" + i + ",\"text\":\"" + info.rows[i].FileFolder + "\",\"state\":\"closed\",\"children\":[";
                        var filenameArry = new Array();
                        filenameArry = info.rows[i].fileName.split(';');                        
                        if (filenameArry.length == 1)
                        {
                            jsonstr += "{},";
                        }
                        for (var j = 0; j < filenameArry.length; j++) {
                            if (filenameArry[j] == "") {
                                continue;
                            }
                            jsonstr += "{\"text\":\"" + filenameArry[j] + "\"},";
                        }
                        if (jsonstr.length > 0) {
                            jsonstr = jsonstr.substring(0, jsonstr.length - 1)
                        }
                        jsonstr += "]},";
                    }
                    if (jsonstr.length > 0) {
                        jsonstr = jsonstr.substring(0, jsonstr.length - 1)
                    }
                    jsonstr += "]";
                    //将对象数据转成json
                    var contact = JSON.parse(jsonstr);
                    return contact;
                } else { alert(data.TxtMessage); }
            }
        });
    }
    var filename = ""; 
    var chartHis;
    //显示图表
    function showChartHis() {
        chartHis = new Highcharts.Chart({
            chart: {
                renderTo: 'containerHis',
                type: 'spline',
                animation: Highcharts.svg,
                // don't animate in old IE 
                marginRight: 10 
            },
            title: {
                text: ''
            },
            plotOptions: {
                spline: {
                    lineWidth: 2, 
                    marker: {
                        enabled: false 
                    }
                }
            },           
            xAxis: {//x轴
                type: 'datetime',
                tickInterval: 30,
                categories: xdataArry,
                labels: {
                    formatter: function ()
                    {   //显示：时分秒
                        return Highcharts.dateFormat('%H:%M:%S', this.value);
                    }
                }
            },
            yAxis: {
                min: 0, max: 1000,
                title: { text: '' },
                plotLines: [{ value: 0, width: 1, color: '#808080' }]
            },
            credits: {
                enabled: false // remove high chart logo hyper-link  
            },
            series: [
                {
                    name: 'CPU占用率', data: CPUdataArry
                },
                 {
                     name: '连接数量', data: LINKdataArry
                 }
            ]
        });
    }

    var xdataArry = [];//x轴 时间
    var CPUdataArry = [];//cpu数组
    var LINKdataArry = [];//link数组
    //将文件中数据显示到chart中
    function binddata() {
        $.post("../../Handler/Monitor/MonitorShow.ashx?AType=ONEHISDATA",
        { filename: filename },
            function (data, textStatus) {
                if (data.Result) {
                    var info = eval('(' + data.TxtMessage + ')');
                    //清空数组
                    xdataArry = [];
                    CPUdataArry = [];
                    LINKdataArry = [];
                    //遍历数据，向数组中填充数据
                    for (var i = 0; i < info.rows.length; i++) {
                        var x = (new Date(info.rows[i].DataDateTime)).getTime();
                        var cpu = info.rows[i].CPU;
                        var link = info.rows[i].Link;
                        xdataArry.push(x);
                        CPUdataArry.push(cpu);
                        LINKdataArry.push(link);
                    }
                    //在图表中显示数据
                    showChartHis();
                }
            }, "json");
    }
</script>
 