IotM.namespace('IotM.Menu');
IotM.Menu.LoadMenu = function () {
    $.ajax({
        url: "../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=LoadLeftMenu",
        async: true,
        type: "POST",
        data: {},
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                var treeJson = eval('(' + decodeURI(data) + ')');
                //绑定菜单列表
                $('#treeMenu').tree({ data: eval('(' + treeJson.TxtMessage + ')') });
                $('#treeMenu').tree('collapseAll');
                //绑定事件列表
                $(".easyui-tree .tree-title").each(
                            function () {
                                var nodeID = $(this).parent().first().attr("node-id");
                                var node = $('#treeMenu').tree('find', nodeID);
                                if (node) {
                                    if (node.text) {
                                        if (node.text == '常用查询' || node.text == '常用报表') {
                                            $('#treeMenu').tree('expand', node.target);
                                        }
                                    }
                                    if (node.attributes) {
                                        var url = node.attributes.url;
                                        if (url) {
                                            $(this).bind("click", function () {
                                                //如果在frame中打开的页面
                                                try {
                                                    var index = window.parent.frames["mainFrame"];
                                                    index.window.MenuToMain(node.text, node.attributes.url);

                                                } catch (e) {
                                                    alert(e.Message);

                                                }

                                            });
                                        }
                                    }
                                }
                            }
                            );
                $.ajax({
                    url: "../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=LoadButtonMenuCode",
                    async: true,
                    type: "POST",
                    data: {},
                    success: function (data, textStatus) {
                        if (textStatus == 'success') {
                            $("#hidMenuCode").val(eval('(' + data + ')').TxtMessage);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        $.messager.alert('警告', "访问数据中心失败。", 'warn');
                    }
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "访问数据中心失败。", 'warn');
        }
    });
}