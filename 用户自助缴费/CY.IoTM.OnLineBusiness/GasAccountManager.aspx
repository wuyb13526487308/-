<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="GasAccountManager.aspx.cs" Inherits="CY.IoTM.OnLineBusiness.GasAccountManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="script/bootstrap/bootstrap-table.min.css" rel="stylesheet" />
    <script src="script/bootstrap/bootstrap-table.js"></script>
    <script src="script/bootstrap/bootstrap-table-zh-CN.min.js"></script>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">



     <div class="panel-heading">燃气账户管理</div>

     <div class="panel-body">

            <div class="form-group">
                <button type="button" class="btn btn-primary"  onclick="App.ShowAddWindow()" >+添加燃气账号</button>
            </div>

          <div class="form-group">

           <table id="tb_departments"></table>
          </div>

     </div>



    <div class="modal fade" id="AddAccount" tabindex="-1" role="dialog">
      <div class="modal-dialog">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title">添加燃气账号</h4>
          </div>
          <div class="modal-body">


         <div class="form-group">
            <label for="CompanyBox" class="control-label">燃气公司:</label>
            <select class="form-control" id="CompanyBox">
            </select>

          </div>
          <div class="form-group">
            <label for="GasUserId" class="control-label">户号:</label>
            <input id="GasUserId" class="form-control" type="text"/>
          </div>

          <div class="form-group">
             <label for="CheckCode" class="control-label">验证码:</label>
             <input id="CheckCode"  class="form-control" type="text"/>
          </div>
          
           <div class="form-group">
                   <img src="CheckCode.aspx" id="imgValidateCode" style="cursor: pointer;"  align="center"/>
            <a id="linka" href="javascript:void()">换一个</a>

            </div>

          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">取消</button>
            <button type="button" class="btn btn-primary">确认添加</button>
          </div>
        </div><!-- /.modal-content -->
      </div><!-- /.modal-dialog -->
    </div><!-- /.modal -->




    <script type="text/javascript">

        var App = {};

        /*公司列表*/
        App.CompanyList = [];
        App.InitCompanyList = function () {
            $.ajax({
                url: "../Handler/GasUserManage.ashx?AType=GetCompany",
                async: false,
                type: "POST",
                success: function (data, textStatus) {
                    if (textStatus == 'success') {
                        data = eval('(' + data.TxtMessage + ')');
                        if (data != null && data.total > 0) {
                            App.CompanyList = data.rows;
                        } else {
                            App.CompanyList = [{ MenuCode: '', Name: '' }];
                        }
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    $.messager.alert('警告', "获取公司失败。", 'warn');
                },
                dataType: "json"
            }
           );
        };

        App.LoadCompanyComboBox = function (id, withAll, required) {
            App.InitCompanyList();
            var data = Olb.ObjectClone(App.CompanyList);
            if (withAll) {
                data.push({ MenuCode: '', Name: '全部' });
            }

            $(data).each(function (index, rec) {

                $('#' + id).append("<option value='" + rec.Id + "'>" + rec.Name + "</option>");
            })

        };





        App.ShowAddWindow = function () {

           
            $('#AddAccount').modal('show');

        }



        App.AddGasUserClick = function () {

            if (!App.check("CheckCode")) {
                return;
            }

            var data = { companyId: $("#CompanyBox").combobox("getValue"), userId: $("#GasUserId").val() };

            $.ajax({
                url: "../Handler/GasUserManage.ashx?AType=GetGasUser",
                type: "POST",
                cache: false,
                dataType: "json",
                async: false,
                data: data,
                success: function (ret) {
                    if (ret.Result) {
                        var user = eval('(' + ret.TxtMessage + ')');
                        $.messager.confirm("确认", "添加燃气账户:" + user.UserName + "'?", function (r) {
                            if (r == true) {
                                $.post("../Handler/GasUserManage.ashx?AType=AddGasUser",
                                     data,
                                      function (data, textStatus) {
                                          if (textStatus == 'success') {
                                              if (data.Result) {
                                                  $('#dataGrid').datagrid('reload');

                                                  $('#AddAccount').window('close');
                                                  $.messager.alert('提示', "关联成功", 'info', function () {
                                                  });
                                              }
                                              else
                                                  $.messager.alert('警告', data.TxtMessage, 'warn');

                                          }
                                      }, "json");
                            }
                        });
                    }
                    else {
                        $.messager.alert('警告', data.TxtMessage, 'warn');
                    }
                }
                });
        }


        App.check = function (objID) {
           
            var cName = $("#" + objID).val();
            if (cName == undefined || cName == null || $.trim(cName) == "") {
                $.messager.alert('提示', "请输入验证码", 'info');
                return false;
            } else {
                var result = false;
                $.ajax({
                    url: "Handler/LogonManage.ashx?Atype=checkCode",
                    type: "POST",
                    cache: false,
                    async: false,
                    dataType: "json",
                    data: { "Code": cName },
                    success: function (ret) {
                        if (ret.Result) {
                            result = true;
                        } else {
                            $.messager.alert('警告', "验证码错误，请重新输入。", 'warn');
                            result = false;
                        }
                    }
                })
                return result;
            }
        }


        //App.GoToCharge = function (obj) {

        //    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
        //    $('#dataGrid').datagrid('selectRow', index);
        //    var data = $('#dataGrid').datagrid('getSelected');
        //    location.href = "Charge.aspx?UserID=" + data.UserID;

        //}


        //App.DeleteUser = function (obj) {

        //    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
        //    $('#dataGrid').datagrid('selectRow', index);
        //    var data = $('#dataGrid').datagrid('getSelected');

        //    $.messager.confirm('确认', '是否真的删除?', function (r) {
        //        if (r == true) {
        //            $.post("../Handler/GasUserManage.ashx?AType=DeleteGasUser",
        //                 data,
        //                  function (data, textStatus) {
        //                      if (textStatus == 'success') {
        //                          if (data.Result) {
        //                              $('#dataGrid').datagrid('deleteRow', parseInt(index));
        //                              $.messager.alert('提示', data.TxtMessage, 'info', function () {
        //                              });
        //                          }
        //                          else
        //                              $.messager.alert('警告', data.TxtMessage, 'warn');

        //                      }
        //                  }, "json");


        //        }
        //    });
        //}


        //$(function () {

        //    App.LoadDataGrid();
        //    App.LoadCompanyComboBox("CompanyBox", false, false);
        //    $("#linka").on("click", function () {
        //        document.getElementById('imgValidateCode').src = 'CheckCode.aspx?' + Math.random();
        //    });
           
        //});







        $(function () {

            //1.初始化Table
            var oTable = new TableInit();
            oTable.Init();

            //2.初始化Button的点击事件
            var oButtonInit = new ButtonInit();
            oButtonInit.Init();


            App.LoadCompanyComboBox("CompanyBox", false, false);

            $("#linka").on("click", function () {
                document.getElementById('imgValidateCode').src = 'CheckCode.aspx?' + Math.random();
            });

        });


        var TableInit = function () {
            var oTableInit = new Object();
            //初始化Table
            oTableInit.Init = function () {
                $('#tb_departments').bootstrapTable({
                    url: '../Handler/GasUserManage.ashx?AType=GetGasUserList',         //请求后台的URL（*）
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
                    height: 500,                        //行高，如果没有设置height属性，表格自动根据记录条数觉得表格高度
                    uniqueId: "ID",                     //每一行的唯一标识，一般为主键列
                    //showToggle: true,                    //是否显示详细视图和列表视图的切换按钮
                    //cardView: false,                    //是否显示详细视图
                    //detailView: false,                   //是否显示父子表
                    columns: [
                    //{
                    //checkbox: true
                    //},
                    {
                        field: 'CompanyID',
                        title: '燃气公司'
                    }, {
                        field: 'UserID',
                        title: '户号'
                    }, {
                        field: 'UserName',
                        title: '户名'
                    }, {
                        field: 'Balance',
                        title: '余额'
                    },
                    {
                        field: 'Address',
                        title: '地址'
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


        var ButtonInit = function () {
            var oInit = new Object();
            var postdata = {};

            oInit.Init = function () {
                //初始化页面上面的按钮事件
            };

            return oInit;
        };




      


    </script>





</asp:Content>



