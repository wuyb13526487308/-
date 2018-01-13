
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

                if (getFileName(this.value).length > 100) {
                    $.messager.alert('警告', "文件名长度不能超过100位", 'warn');
                    this.value = "";
                    return false;
                }
                $("#FileName").val(getFileName(this.value));
                if (!RegExp("\.(" + opts.ImgType.join("|") + ")$", "i").test(this.value.toLowerCase())) {
                    $.messager.alert("警告", "选择文件错误,图片类型必须是" + opts.ImgType.join("，") + "中的一种", "warn");
                    this.value = "";
                    return false
                }
                if (this.files[0].size > 2097152) {
                    $.messager.alert("警告", "文件大小不能超过2M", "warn");
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

IotM.namespace('AdM.ContentList');
//加载主题列表
AdM.ContentList.LoadDataGridView = function () {
    var url = "../Handler/AdmListHandler.ashx?AType=QUERYVIEW&AC_ID=" + $("#AC_ID").val();
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        //rownumbers: true,
        singleSelect: true,
        sort: "OrderID",
        order: "asc",
        columns: [
                    [
                     { field: 'OrderID', title: '排序号', width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'AI_ID', title: '编号',  hidden:true },
                     { field: 'FileName', title: '文件名称',  width: IotM.MainGridWidth * 0.15, align: 'left', sortable: true },
                     { field: 'BDate', title: '开始时间',  width: IotM.MainGridWidth * 0.1, align: 'center',sortable: true },
                     { field: 'EDate', title: '停止时间',  width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Length', title: '显示时长',  width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },                     
                     {
                         field: 'IsDisplay', title: '显示状态',  hidden:true, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {

                             if (value == "1") { return "显示"; }
                             else if (value == "0" ) { return "不显示"; }
                             else { return "未知"; }
                         }
                     },
                     { field: 'StorePath', title: '文件地址', hidden: true },
                     { field: 'StoreName', title: '文件地址', hidden: true },
                     { field: 'FileLength', title: '文件地址', hidden: true },
                      {
                          field: 'opt', title: '操作',  width: IotM.MainGridWidth * 0.08, align: 'center',
                          formatter: function (value, rec, index) {
                              var a = '<a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentList.OpenformEdit(this)"><span style="color:blue">编辑</span></a> ';
                              var b = '<a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentList.RemoveClick(this)"><span style="color:blue">删除</span></a> ';
                              var c = '<a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentList.UpOrder(this)"><span style="color:blue">上移</span></a> ';
                              var d = '<a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentList.DownOrder(this)"><span style="color:blue">下移</span></a> ';
                              return a+b+c+d;
                          }
                      }
                     
                    ]  
        ],
        queryParams: { TWhere: '  AND  AC_ID= ' + $("#AC_ID").val() },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid').datagrid('getPager');
            
            $(p).pagination({
                pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
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



AdM.ContentList.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { AdM.ContentList.AddClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });

    $("#uploadShow").hide();
    $("#uploadTr").show();
    $("#preview").hide();
    $("#UploadFile").val("");

    $("#BDate").datebox("setValue", IotM.MyDateformatter(new Date()));
    $("#EDate").datebox("setValue", IotM.MyDateformatter(new Date().dateAdd('m', 1)));

    $("#FileName").val("");
    $("#FileName").attr("disabled", true);



    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.43,
        title: '添加信息',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 10,
        left: 200,
        onOpen: function () {
            IotM.checkisValid('formAdd');

        }
    });
};

AdM.ContentList.AddClick = function () {
    if (IotM.checkisValid('formAdd')) {

        var cycleSecond = $("#Length").numberbox("getValue");
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
                        window.location.reload();
                        //$('#dataGrid').datagrid("reload");
                    });
                } else {
                    $.messager.alert('警告', result.TxtMessage, 'warn');
                }
            },
            error: function (result) {
                $.messager.alert('警告', "提交失败", 'warn');
            }
        };
        $("#formAdd").ajaxSubmit(options);
    }
};




//打开修改
AdM.ContentList.OpenformEdit = function (obj) {

    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    $('#btnOk').unbind('click').bind('click', function () { AdM.ContentList.Edit(data); });
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    
    //显示图片
    var companyID = data.StoreName.substring(0, data.StoreName.length - 15);
    $("#ImgPr").attr('src', 'ADMPlayShow.aspx?fileLen=' + data.FileLength + '&StoreName=' + data.StoreName + '&CompanyID=' + companyID);
    $("#preview").show();
    //$("#preview").hide();
    //$("#uploadTr").hide();

    $("#BDate").datebox("setValue", data.BDate);
    $("#EDate").datebox("setValue", data.EDate);
    $("#uploadShow").show();
    $("#StorePath").val(data.StorePath);
    $("#StoreName").val(data.StoreName);
    $("#FileLength").val(data.FileLength);

    $("#FileName").val(data.FileName);
    $("#OrderID").val(data.OrderID);
    $("#AC_ID").val(data.AC_ID);
    $("#AI_ID").val(data.AI_ID);
    $("#IsDisplay").val(data.IsDisplay);
    $("#ShowStatus").val(data.ShowStatus);
    $("#Length").numberbox("setValue", data.Length);

    //$("#FileName").attr("disabled", "disabled");
    //$("#OrderID").attr("disabled", "disabled");


    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.43,
        title: '编辑广告',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top:10,

        onOpen: function () {
            IotM.EditDisabled('formAdd');
            IotM.checkisValid('formAdd');

        }
    });
};


AdM.ContentList.Edit = function () {
    if (IotM.checkisValid('formAdd')) {

        var cycleSecond = $("#Length").numberbox("getValue");
        if (cycleSecond == 0) {
            $.messager.alert('警告', "循环时长必须大于0", 'warn');
            return false;
        }
        //var fileText = $("#UploadFile").val();
        //if (!fileText) {
        //    $.messager.alert('警告', "未选择文件", 'warn');
        //    return false;
        //}


        var options = {
            data: {
                //'FileName': $("#FileName").val()
            },
            dataType: "json",
            beforeSubmit: function () {
            },
            success: function (result) {
                if (result.Result) {
                    $.messager.alert('提示', '操作成功！', 'info', function () {
                        $('#wAdd').window('close');
                        window.location.reload();
                        //$('#dataGrid').datagrid("reload");
                    });
                } else {
                    $.messager.alert('警告', result.TxtMessage, 'warn');
                }
            },
            error: function (result) {
                $.messager.alert('警告', "提交失败", 'warn');
            }
        };
        $("#formAdd").attr("action", "../Handler/AdmListHandler.ashx?AType=Edit")
        $("#formAdd").ajaxSubmit(options);
    }
};

AdM.ContentList.Edit_nofile = function (data) {

    var data = $("#formAdd").serialize();
    //data += "&OrderID=" + $("#OrderID").val();

    $.post("../Handler/AdmListHandler.ashx?AType=Edit",
             data,
              function (data, textStatus) {
                  if (textStatus == 'success') {
                      if (data.Result) {
                          //$('#dataGrid').datagrid('updateRow',
                          //    {
                          //        index: $('#dataGrid').datagrid('getRowIndex', $('#dataGrid').datagrid('getSelected')),
                          //        row: eval('(' + data.TxtMessage + ')')
                          //    });
                          $('#dataGrid').datagrid('reload')
                          $.messager.alert('提示', '操作成功！', 'info', function () {
                              $('#wAdd').window('close');
                          });
                      }
                      else
                          $.messager.alert('警告', data.TxtMessage, 'warn');

                  }
              }, "json");
};



AdM.ContentList.RemoveClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    if ($("#State").val() == "2") {
        alert('主题已经发布，内容不可删除！');
        return;
    }
    $.messager.confirm('确认', '是否真的删除？', function (r) {
        if (r == true) {
            $.post("../Handler/AdmListHandler.ashx?AType=DELFILE",
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
};



AdM.ContentList.UpOrder = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    $.post("../Handler/AdmListHandler.ashx?AType=UPORDER",
            data,
            function (data, textStatus) {
                if (textStatus == 'success') {
                    if (data.Result) {
                        $('#dataGrid').datagrid("reload");
                        $.messager.alert('提示', data.TxtMessage, 'info', function () {
                        });
                    }
                    else
                        $.messager.alert('警告', data.TxtMessage, 'warn');

                }
            }, "json");

};

AdM.ContentList.DownOrder = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    $.post("../Handler/AdmListHandler.ashx?AType=DOWNORDER",
            data,
            function (data, textStatus) {
                if (textStatus == 'success') {
                    if (data.Result) {
                        $('#dataGrid').datagrid("reload");
                        $.messager.alert('提示', data.TxtMessage, 'info', function () {
                        });
                    }
                    else
                        $.messager.alert('警告', data.TxtMessage, 'warn');

                }
            }, "json");

};



//预览
AdM.ContentList.Preview = function (CompanyID,AC_ID) {
    window.open('AdMPlay.aspx?CompanyID=' + CompanyID + '&AC_ID=' + AC_ID, '预览广告', 'height=500 width=730, top=150, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no')
};


Date.prototype.format = function (format) {
    var o = {
        "M+": this.getMonth() + 1, // month  
        "d+": this.getDate(), // day  
        "h+": this.getHours(), // hour  
        "m+": this.getMinutes(), // minute  
        "s+": this.getSeconds(), // second  
        "q+": Math.floor((this.getMonth() + 3) / 3), // quarter  
        "S": this.getMilliseconds()
        // millisecond  
    }
    if (/(y+)/.test(format))
        format = format.replace(RegExp.$1, (this.getFullYear() + "")
            .substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(format))
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
    return format;
}
function formatDatebox(value) {
    if (value == null || value == '') {
        return '';
    }
    var dt;
    if (value instanceof Date) {
        dt = value;
    } else {
        dt = new Date(value);
    }

    return dt.format("yyyy-MM-dd"); //扩展的Date的format方法(上述插件实现)  
}

