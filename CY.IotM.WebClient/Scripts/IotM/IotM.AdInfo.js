jQuery.fn.extend({
    uploadPreview: function (opts) {
        var _self = this,
            _this = $(this);
        //opts = jQuery.extend({
        //    Img: "ImgPr",
        //    Width: 100,
        //    Height: 100,
        //    ImgType: ["gif", "jpeg", "jpg", "bmp", "png"],
        //    Callback: function () { }
        //}, opts || {});
        _self.getObjectURL = function (file) {
            var url = null;
            if (window.createObjectURL != undefined) {
                url = window.createObjectURL(file)
            } else if (window.URL != undefined) {
                url = window.URL.createObjectURL(file)
            } else if (window.webkitURL != undefined) {
                url = window.webkitURL.createObjectURL(file)
            }
            return url
        };
        var getFileName = function (o) {
            var pos = o.lastIndexOf("\\");
            return o.substring(pos + 1);
        };
        _this.change(function () {
            if (this.value) {

                if (getFileName(this.value).length > 10) {
                    $.messager.alert('警告', "文件名长度不能超过10位", 'warn');
                    this.value = "";
                    return false;
                }
                $("#FileName").val(getFileName(this.value));
                if (!RegExp("\.(" + opts.ImgType.join("|") + ")$", "i").test(this.value.toLowerCase())) {
                    $.messager.alert("警告", "选择文件错误,图片类型必须是" + opts.ImgType.join("，") + "中的一种", "warn");
                    this.value = "";
                    return false
                }
                if (this.files[0].size > 16777216) {
                    $.messager.alert("警告", "文件大小不能超过16M", "warn");
                    this.value = "";
                    return false
                }
                if ($.browser.msie) {
                    try {
                        $("#" + opts.Img).attr('src', _self.getObjectURL(this.files[0]))
                    } catch (e) {
                        var src = "";
                        var obj = $("#" + opts.Img);
                        var div = obj.parent("div")[0];
                        _self.select();
                        if (top != self) {
                            window.parent.document.body.focus()
                        } else {
                            _self.blur()
                        }
                        src = document.selection.createRange().text;
                        document.selection.empty();
                        obj.hide();
                        obj.parent("div").css({
                            'filter': 'progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod=scale)',
                            'width': opts.Width + 'px',
                            'height': opts.Height + 'px'
                        });
                        div.filters.item("DXImageTransform.Microsoft.AlphaImageLoader").src = src
                    }
                } else {
                    $("#" + opts.Img).attr('src', _self.getObjectURL(this.files[0]))
                }
                opts.Callback()
            }
        })
    }
});


IotM.namespace('IotM.AdInfo');
//加载列表控件
IotM.AdInfo.LoadDataGrid = function () {
    var url = "../Handler/AdInfoHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "ID",
        order: "ASC",
        columns: [
                    [
                       
                        { field: 'FileIndex', title: '序号', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true },
                        { field: 'FileName', title: '文件名称', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true },
                        { field: 'StartDate', title: '开始时间', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true },
                        { field: 'EndDate', title: '停止时间', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true },
                        { field: 'CycleTime', title: '轮循时长（秒）', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true },
                        {
                            field: 'PublishStatus', title: '发布状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true,
                            formatter: function (value, rec, index) {
                                if (value == "0") { return "未发布"; }
                                else if (value == "1") { return "已发布"; }
                                else if (value == "2") { return "发布中"; }
                                else if (value == "3") { return "重新发布"; }
                                else { return "未知"; }
                            }
                        },
                        {
                            field: 'ShowStatus', title: '显示状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true,
                            formatter: function (value, rec, index) {
                                if (value == "0") { return "不显示"; }
                                else { return "显示"; }
                            }

                        },
                        {
                            field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true,
                            formatter: function (value, rec, index) {
                                var e = '<a href="#" mce_href="#"  onclick="IotM.AdInfo.OpenformEdit(this)"><span style="color:blue">编辑</span></a> ';
                                var f = '';
                                var g = '<a href="#" mce_href="#"  onclick="IotM.AdInfo.PreView(this)"><span style="color:blue">预览</span></a> ';
                                var h = '<a href="#" mce_href="#"  onclick="IotM.AdInfo.RemoveClick(this)"><span style="color:blue">删除</span></a> ';

                                if (rec.PublishStatus == "0" || rec.PublishStatus == "3")//未发布或已撤销
                                {
                                    f += '<a href="#" mce_href="#"  onclick="IotM.AdInfo.Publish(this)"><span style="color:blue">发布</span></a> ';
                                }
                                if (rec.PublishStatus == "2") {
                                    e = ''; h = '';
                                }
                                return e + f + g+ h;
                            }
                        }
                      
                    ]
                   
        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid').datagrid('getPager');
            $(p).pagination({
                pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数
                pageList: IotM.pageList, //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                onBeforeRefresh: function () {
                    $(this).pagination('loading');
                    $(this).pagination('loaded');
                }
            });
        }
    });
};
IotM.AdInfo.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.AdInfo.AddClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
   
    $("#uploadTr").show();
    $("#preview").hide();
    $("#UploadFile").val("");

    $("#StartDate").datebox("setValue", IotM.MyDateformatter(new Date()));
    $("#EndDate").datebox("setValue", IotM.MyDateformatter(new Date().dateAdd('m', 1)));

    $("#FileName").val("");
    $("#FileIndex").val("");
    $("#CycleTime").numberbox("setValue", 30);
    $("#FileName").attr("disabled", true);
    $("#FileIndex").attr("disabled", false);
   
  
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '添加广告',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 100,
        left:100,
        onOpen: function () {
            IotM.checkisValid('formAdd');
          
        }
    });
};

IotM.AdInfo.AddClick = function () {
    if (IotM.checkisValid('formAdd'))
    {

        var cycleSecond = $("#CycleTime").numberbox("getValue");
        if (cycleSecond == 0) {
            $.messager.alert('警告', "循环时长必须大于0", 'warn');
            return false;
        }
        var fileText = $("#UploadFile").val();
        if (!fileText) {
            $.messager.alert('警告', "未选择文件", 'warn');
            return false;
        }


        var options = {
            data: {
                'FileName': $("#FileName").val()
            },
            dataType: "json",
            beforeSubmit: function () {
            },
            success: function (result) {
                if (result.Result) {
                    $.messager.alert('提示', '操作成功！', 'info', function () {
                        $('#wAdd').window('close');
                        $('#dataGrid').datagrid("reload");
                    });
                } else {
                    $.messager.alert('警告', result.TxtMessage, 'warn');
                }
            },
            error: function (result) {
                $.messager.alert('警告',"提交失败", 'warn');
            }
        };
        $("#formAdd").ajaxSubmit(options);
    }
};



IotM.AdInfo.OpenformEdit = function (obj) {
   
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
  
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    $('#btnOk').unbind('click').bind('click', function () { IotM.AdInfo.EditClick(data); });
 

    $("#preview").hide();
    $("#uploadTr").hide();

    $("#StartDate").datebox("setValue", data.StartDate);
    $("#EndDate").datebox("setValue", data.EndDate);


    $("#FileName").val(data.FileName);
    $("#FileIndex").val(data.FileIndex);
    $("#FileName").val(data.FileName);
    $("#CompanyID").val(data.CompanyID);
    $("#ID").val(data.ID);
    $("#PublishStatus").val(data.PublishStatus);
    $("#ShowStatus").val(data.ShowStatus);
    $("#CycleTime").numberbox("setValue", data.CycleTime);

    $("#FileName").attr("disabled", "disabled");
    $("#FileIndex").attr("disabled", "disabled");

   
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '编辑广告',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () {
            IotM.EditDisabled('formAdd');
            IotM.checkisValid('formAdd');
         
        }
    });
};



IotM.AdInfo.Edit = function (data) {

    var data = $("#formAdd").serialize();
    data += "&FileIndex=" + $("#FileIndex").val();

    $.post("../Handler/AdInfoHandler.ashx?AType=Edit",
             data,
              function (data, textStatus) {
                  if (textStatus == 'success') {
                      if (data.Result) {
                          $('#dataGrid').datagrid('updateRow',
                              {
                                  index: $('#dataGrid').datagrid('getRowIndex', $('#dataGrid').datagrid('getSelected')),
                                  row: eval('(' + data.TxtMessage + ')')
                              });
                          $.messager.alert('提示', '操作成功！', 'info', function () {
                              $('#wAdd').window('close');
                          });
                      }
                      else
                          $.messager.alert('警告', data.TxtMessage, 'warn');

                  }
              }, "json");
};


IotM.AdInfo.EditAndSend = function (data) {

    var data = $("#formAdd").serialize();
    data += "&FileName=" + $("#FileName").val();
    data += "&FileIndex=" + $("#FileIndex").val();
  
    $.post("../Handler/AdInfoHandler.ashx?AType=EditAdInfo",
             data,
              function (data, textStatus) {
                  if (textStatus == 'success') {
                      if (data.Result) {
                          
                          $.messager.alert('提示', '操作成功,任务已下发！', 'info', function () {
                              $('#wAdd').window('close');
                          });
                      }
                      else
                          $.messager.alert('警告', data.TxtMessage, 'warn');
                  }
              }, "json");
};


IotM.AdInfo.EditClick = function (data) {
    if (IotM.checkisValid('formAdd')) {
      
        //文件发布状态为未发布
        if ($("#PublishStatus").val() == 0) {
            IotM.AdInfo.Edit(data);
        }
        //文件发布状态为已发布
        if ($("#PublishStatus").val() == 1) {
            $.messager.confirm('确认', '文件已发布，是否同步编辑?', function (r) {
                if (r == true) {
                    IotM.AdInfo.EditAndSend(data);
                } 
            });
        }
    }
};


IotM.AdInfo.Remove = function (data) {

   
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r == true) {
            $.post("../Handler/AdInfoHandler.ashx?AType=Delete",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('deleteRow', parseInt(index));
                              $.messager.alert('提示', data.TxtMessage, 'info', function () {
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");


        }
    }
   );

}


IotM.AdInfo.RemoveClick = function (obj) {

    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');


    //文件发布状态为未发布
    if (data.PublishStatus == 0) {
        IotM.AdInfo.Remove(data);
    }
    //文件发布状态为已发布
    if (data.PublishStatus  == 1) {
        $.messager.confirm('确认', '文件已发布，是否同步删除?', function (r) {
            if (r == true) {

                $.post("../Handler/AdInfoHandler.ashx?AType=DeleteAdInfo",
                         data,
                          function (data, textStatus) {
                              if (textStatus == 'success') {
                                  if (data.Result) {

                                      $.messager.alert('提示', '操作成功,任务已下发！', 'info', function () {
                                          $('#wAdd').window('close');
                                      });
                                  }
                                  else
                                      $.messager.alert('警告', data.TxtMessage, 'warn');
                              }
                          }, "json");

            }
        });
    }
};


IotM.AdInfo.SerachClick = function () {
  
    var Where = "";
    if ($("#select_FileName").val() != "") {
        Where += '  AND  FileName  like \'%' + $("#select_FileName").val() + '%\'';
    }
    if ($("#select_Publish").combobox("getValue") != "") {
        Where += '  AND  PublishStatus =' + $("#select_Publish").combobox("getValue")+ ' ';
    }
    if ($("#select_Show").combobox("getValue") != "") {
        Where += '  AND  ShowStatus   =' + $("#select_Show").combobox("getValue") + ' ';
    }


        $('#dataGrid').datagrid('options').queryParams = {
            TWhere: Where ,
            Date1: $('#txtDate1').datebox('getValue'),
            Date2: $('#txtDate2').datebox('getValue')
        
        };
    $('#dataGrid').datagrid('load');
};





//预览
IotM.AdInfo.PreView = function (obj) {


    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    $('#wPreView').window({
        resizable: false,
        width: 600,
        height: 400,
        title: '预览广告',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 100,
        left:250,
        onOpen: function () {

            var winPreView = document.getElementById("wPreView");

            var iframe = document.createElement("iframe");
            iframe.src = "../IotM/PreViewImage.aspx?FileId="+data.ID;
            //iframe.style.display = "none";

            iframe.frameBorder = 0;
            iframe.scrolling = "no";

            iframe.marginheight=0;
            iframe.marginwidth=0;

            iframe.height = 340;
            iframe.width = 560;

            winPreView.innerHTML = "";
            winPreView.appendChild(iframe);
          
        }
    });

};


//发布
IotM.AdInfo.Publish = function (obj) {

    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '是否真的发布?', function (r) {
        if (r == true) {
            $.post("../Handler/AdInfoHandler.ashx?AType=Publish",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                            
                              $.messager.alert('提示', data.TxtMessage, 'info', function () {

                              });
                              $('#dataGrid').datagrid('load');
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");


        }
    }
   );
}



IotM.AdInfo.LoadControlGrid = function () {


    var url = "../Handler/AdInfoHandler.ashx?AType=QueryControl";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "ID",
        order: "ASC",
        columns: [
                    [

                        { field: 'FileIndex', title: '序号', align: 'center', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true },
                        { field: 'FileName', title: '文件名称', align: 'center', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true },

                        {
                            field: 'SetType', title: '发布类型', align: 'center', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true,
                            formatter: function (value, rec, index) {
                                if (value == "0") { return "发布"; }
                                else if (value == "1") { return "编辑"; }
                                else if (value == "2") { return "删除"; }
                                else { return "未知"; }
                            }
                        },
                        { title: '发布时间', field: 'SendTime', title: '发送时间', align: 'center', rowspan: 2, width: IotM.MainGridWidth * 0.12, sortable: true },
                        { title: '发布参数', colspan: 4 },

                       
                        {
                            field: 'PublishStatus', title: '发布状态', align: 'center', rowspan: 2, width: IotM.MainGridWidth * 0.1, sortable: true,
                            formatter: function (value, rec, index) {
                                if (value == "0") { return "未发布"; }
                                else if (value == "1") { return "已发布"; }
                                else if (value == "2") { return "发布中"; }
                                else if (value == "3") { return "已撤销"; }
                                else { return "未知"; }
                            }
                        },
                        
                        {
                            field: 'opt', title: '操作', rowspan: 2, align: 'center', width: IotM.MainGridWidth * 0.1, sortable: true,
                            formatter: function (value, rec, index) {
                               
                                var f = '';
                                var h = '<a href="#" mce_href="#"  onclick="IotM.AdInfo.SelectAdMeter(this)"><span style="color:blue">查看详细</span></a> ';

                                if (rec.PublishStatus == "2")//发布中
                                {
                                    f += '<a href="#" mce_href="#"  onclick="IotM.AdInfo.UnPublish(this)"><span style="color:blue">撤销发布</span></a> ';
                                }
                                return  h+f;
                            }
                        }

                    ],
                    [
                     { field: 'StartDate', title: '开始时间', width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'EndDate', title: '停止时间', width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'CycleTime', title: '轮循时长（秒）', width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     {
                         field: 'ShowStatus', title: '显示状态', width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                            formatter: function (value, rec, index) {
                                if (value == "0") { return "不显示"; }
                                else { return "显示"; }
                            }

                        }
                    
                    ]

        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function (){ IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid').datagrid('getPager');
            $(p).pagination({
                pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数
                pageList: IotM.pageList, //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                onBeforeRefresh: function () {
                    $(this).pagination('loading');
                    $(this).pagination('loaded');
                }
            });
        }
    });


}



IotM.AdInfo.SelectAdMeter = function (obj) {

    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');


    IotM.AdInfo.LoadDataGridUser(data.ID);

    $('#GetUserDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '查看用户明细',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 150,
        top: 150,
    });


}



//加载用户详细视图
IotM.AdInfo.LoadDataGridUser = function (id) {

    var url = "../Handler/AdInfoHandler.ashx?AType=QueryMeterView";
    $('#dataGrid_getUser').datagrid({
        title: '',
        toolbar: '',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        sort: "UserID",
        order: "ASC",
        columns: [
                    [

                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "0") { return "申请"; }
                             else if (value == "1") { return "撤销"; }
                             else if (value == "2") { return "完成"; }
                             else if (value == "3") { return "失败"; }
                             else { return "未知"; }
                         }
                     },
                     {
                         field: 'FileName', title: '文件名称', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true

                     },
                     //{ field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.4, align: 'center', sortable: true },
                     { field: 'FinishedDate', title: '完成时间', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     {
                         field: 'opt', title: '日志', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,

                         formatter: function (value, rec, index) {

                             var f = '<a href="#" mce_href="#"  onclick="IotM.Pricing.GetMeterLog(this)"><span style="color:blue">日志</span></a> ';
                             return f;
                         }
                     }
                    ]

        ],
        queryParams: { TWhere: '', ID: id },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_getUser').datagrid('getPager');
            $(p).pagination({
                //pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
                pageList: IotM.pageList, //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                onBeforeRefresh: function () {
                    $(this).pagination('loading');
                    $(this).pagination('loaded');
                }
            });
        }
    });


}



IotM.AdInfo.UnPublish = function (obj) {

    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '是否真的撤销?', function (r) {
        if (r == true) {
            $.post("../Handler/AdInfoHandler.ashx?AType=UnPublish",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {

                              $.messager.alert('提示', data.TxtMessage, 'info', function () {
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");

        }
    }
   );

}




IotM.AdInfo.SerachHistoryClick = function () {

    var Where = "";
    if ($("#select_FileName").val() != "") {
        Where += '  AND  FileName  like \'%' + $("#select_FileName").val() + '%\'';
    }
 
    $('#dataGrid').datagrid('options').queryParams = {
        TWhere: Where
       
    };

    $('#dataGrid').datagrid('load');
};



