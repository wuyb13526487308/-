<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="PayMentDetail.aspx.cs" Inherits="CY.IoTM.OnLineBusiness.PayMentDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="script/bootstrap/bootstrap-table.min.css" rel="stylesheet" />
    <link href="script/bootstrap/bootstrap-datetimepicker.min.css" rel="stylesheet" />
    <script src="script/bootstrap/bootstrap-table.js"></script>
    <script src="script/bootstrap/bootstrap-table-zh-CN.min.js"></script>
    <script src="script/bootstrap/bootstrap-datetimepicker.min.js"></script>
    <script src="script/bootstrap/bootstrap-datetimepicker.zh-CN.js"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">



     <div class="panel-heading">缴费明细查询</div>

     <div class="panel-body">


         <form class="form-horizontal">
      
            <div class="form-group">

                <label for="txtDate1" class="control-label col-md-2">起始时间:</label>
                <div class="row col-md-3">
                     <input size="12" type="text"  id="txtDate1" readonly class="form-control">

                </div>
             
                <label for="txtDate2" class="control-label col-md-2">结束时间:</label>
                <div class="row col-md-3">
                <input size="12" type="text"  id="txtDate2" readonly class="form-control">
                </div>



            </div>

            <div class="form-group">
                <div class="row col-md-offset-10">
                <button type="button" class="btn btn-primary"  id="selectBtn" >&nbsp;查询&nbsp;</button>
                </div>
            </div>
         
             
           </form>

           <table id="tb_departments"></table>

     </div>


 

<%--     <div class="userCenter">
        <h3 class="lst">缴费明细查询</h3>



         <div id="tb" style="margin-left: 50px;font-size:16px">

                  &nbsp;&nbsp;起止时间：<input class="easyui-datebox" 
                id="txtDate1" style="width: 120px;height:30px"/>
            &nbsp;&nbsp;至：<input class="easyui-datebox" 
                id="txtDate2" style="width: 120px;height:30px"/>

        </div>

        <div class="rightdiv_blue" style="border: 0; padding: 0; margin-top: 20px;margin-bottom: 20px; margin-right: 100px;"><a  id="selectBtn" href="javascript:void()">查询</a></div>
        <div class="clear"></div>
    </div>


    <div class="userCenter">


 
    <div id="dataGrid" > </div>


    </div>--%>







    <script type="text/javascript">


        var App = {};


        //App.LoadDataGrid = function (queryParams) {
        //    var url = "../Handler/PayMentManage.ashx?AType=GetPayMent";
        //    $('#dataGrid').datagrid({
        //        title: '缴费明细列表',
        //        url:url,
        //        height:300,
        //        border:false,
        //        pagination: false,
        //        rownumbers: true,
        //        singleSelect: true,
        //        sort: "GasUserID",
        //        order: "ASC",
        //        columns: [
        //                   [
        //                        { field: 'GasUserID', title: '户号', width: 120, align: 'center' },
        //                        { field: 'GasUserName', title: '户名', width: 120, align: 'center' },
        //                        { field: 'GasUserAddress', title: '地址', width: 250, align: 'center'},
        //                        { field: 'PayMoney', title: '支付金额', width: 80, align: 'center' },
        //                        { field: 'PayTime', title: '缴费时间', width: 120, align: 'center' },
        //                        { field: 'Balance', title: '余额', width: 80, align: 'center' }
        //                   ]
                            
        //        ],
        //        queryParams: queryParams,
        //        onLoadSuccess: function () {  },
        //        onBeforeLoad: function (param) { }
        //    });
        //};


        $(function () {

            //1.初始化Table
            var oTable = new TableInit();
            oTable.Init();


            $("#txtDate1").val(Olb.MyDateformatter(new Date().dateAdd('d', -30)));

            $("#txtDate2").val(Olb.MyDateformatter(new Date()));

            $('#txtDate1').datetimepicker({
                minView: "month", //选择日期后，不会再跳转去选择时分秒 
                format: "yyyy-mm-dd", //选择日期后，文本框显示的日期格式 
                language: 'zh-CN', //汉化 
                autoclose: true //选择日期后自动关闭 
            });



            $('#txtDate2').datetimepicker({
                minView: "month", //选择日期后，不会再跳转去选择时分秒 
                format: "yyyy-mm-dd", //选择日期后，文本框显示的日期格式 
                language: 'zh-CN', //汉化 
                autoclose: true //选择日期后自动关闭 
            });


           $("#selectBtn").on("click", App.SelectData);
        });


        App.SelectData = function () {
            //var queryParams = {
            //    Date1: $('#txtDate1').datebox('getValue'),
            //       // + ' ' + $('#txtTime1').timespinner('getValue')
            //    Date2: $('#txtDate2').datebox('getValue')
            //        //+ ' ' + $('#txtTime2').timespinner('getValue')
            //};

            //$('#dataGrid').datagrid('options').queryParams = queryParams;
            //$('#dataGrid').datagrid('load');
        }


        var TableInit = function () {
            var oTableInit = new Object();
            //初始化Table
            oTableInit.Init = function () {
                $('#tb_departments').bootstrapTable({
                    url: '../Handler/PayMentManage.ashx?AType=GetPayMent',         //请求后台的URL（*）
                    method: 'post',                      //请求方式（*）
                    //toolbar: '#toolbar',                //工具按钮用哪个容器
                    striped: true,                      //是否显示行间隔色
                    cache: false,                       //是否使用缓存，默认为true，所以一般情况下需要设置一下这个属性（*）
                    pagination: true,                   //是否显示分页（*）
                    sortable: false,                     //是否启用排序
                    sortOrder: "asc",                   //排序方式
                    queryParams: oTableInit.queryParams,//传递参数（*）
                    sidePagination: "server",           //分页方式：client客户端分页，server服务端分页（*）
                    pageNumber: 1,                       //初始化加载第一页，默认第一页
                    pageSize: 10,                       //每页的记录行数（*）
                    pageList: [10, 25, 50, 100],        //可供选择的每页的行数（*）
                    //search: true,                       //是否显示表格搜索，此搜索是客户端搜索，不会进服务端，所以，个人感觉意义不大
                    //strictSearch: true,
                    //showColumns: true,                  //是否显示所有的列
                    //showRefresh: true,                  //是否显示刷新按钮
                    minimumCountColumns: 2,             //最少允许的列数
                    clickToSelect: true,                //是否启用点击选中行
                    height: 400,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                    uniqueId: "ID",                     //每一行的唯一标识，一般为主键列
                    //showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
                    //cardView: false,                    //是否显示详细视图
                    //detailView: false,                   //是否显示父子表
                    columns: [
                    //{
                    //checkbox: true
                    //},
                    {
                        field: 'GasUserID',
                        title: '户号'
                    }, {
                        field: 'GasUserName',
                        title: '户名'
                    }, {
                        field: 'GasUserAddress',
                        title: '地址'
                    }, {
                        field: 'PayMoney',
                        title: '支付金额'
                    },
                    {
                        field: 'PayTime',
                        title: '缴费时间'
                    },
                      {
                          field: 'Balance',
                          title: '余额'
                      }

                    ]
                });
            };

          

            //得到查询的参数
            oTableInit.queryParams = function (params) {
                var temp = {   //这里的键的名字和控制器的变量名必须一直，这边改动，控制器也需要改成一样的
                    limit: params.limit,   //页面大小
                    offset: params.offset,  //页码
                    departmentname: $("#txt_search_departmentname").val(),
                    statu: $("#txt_search_statu").val()
                };
                return temp;
            };
            return oTableInit;
        };



      

    </script>




</asp:Content>
