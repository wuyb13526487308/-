<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemParManage.aspx.cs" Inherits="CY.IotM.WebClient.SystemParManage" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>服务器参数管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.SystemPar.js" type="text/javascript"></script>


 
    <script type="text/javascript">



        $(function () {
            IotM.CheckLogin();
            IotM.SystemPar.OpenformSystemPar();
           
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
               
                
            });
        });
    </script>
  
</head>
<body>
    <div id="wAddSystemPar" class="easyui-window" title="服务器参数" data-options="modal:true,closed:true,iconCls:'icon-edit'"
        style="padding: 10px">
        <form id='formAdd'>
        

            <table>

                <tr>

                    <td>

                          <table>
            <tr>
                <td>
                    服务器类型:
                </td>
                <td>
                    <input type="hidden" name="CNCompanyID" />
                
                    手机<input type="radio"  value='0' name='CNServerType' />
                    IP<input type="radio"  value='1' name='CNServerType' checked="checked"/>
                    域名<input type="radio"  value='2' name='CNServerType'/>


                </td>
            </tr>
            <tr>
                <td>
                    服务器地址:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNNetAddr" required="true"  missingmessage="请输入服务器地址"
                        style="width: 80%" />
                </td>
            </tr>


            <tr>
                <td>
                    服务器端口号:
                </td>
                <td>
                    <input name="CNNetPort" type="text" class="easyui-numberbox"  id="CNNetPort" required="true"  missingmessage="请输入服务器端口号" style="width: 80%"/>
                </td>
            </tr>
            <tr>
                <td>
                    短信中心号码:
                </td>
                <td>
                    <input name="CNGSM"  type="text" class="easyui-numberbox" id="CNGSM"  style="width: 80%"/>
                </td>
            </tr>
            <tr>
                <td>
                    APN接入点:
                </td>
                <td>
                    <input  name="CNAPN" type="text" class="easyui-validatebox" style="width: 80%"/>
                </td>
            </tr>
              <tr>
                <td>
                    APN用户名:
                </td>
                <td>
                    <input name="CNUID" type="text" class="easyui-validatebox"  style="width: 80%"/>
                </td>
            </tr>
              <tr>
                <td>
                    APN用户密码:
                </td>
                <td>
                    <input name="CNPWD" type="text" class="easyui-validatebox"  style="width: 80%"/>  
                </td>
            </tr>
              <tr>
                <td>
                    通信密钥:
                </td>
                <td>
                    <input name="CNMKey" type="text" class="easyui-validatebox"  /> 
                    <input type="checkbox" name="CNAutoKey" id="AutoKey" />随机生成
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                        id="btnOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                            id="btnCancel">取消</a>
                </td>
            </tr>
        </table>


                    </td>


                    <td>
                        
                        <div class="demo-info">
                            <div class="demo-tip icon-tip">
                            </div>
                            <div> 用于设置物联网表<span style="color:red">初始化</span>通信参数</div>
                        </div>
                        <div>
                            服务器地址：填写为IP地址或域名  <br /> 
                            例：<span style="color:red">192.168.1.1</span> or  <span style="color:red">server.com</span>  <br /><br />
                            
                            服务器端口号：表示服务器端TCP端口，值为整数，例如：<span style="color:red">67810</span> <br /><br />
 
                            短信中心号码：用于物联网表通过短信上数据中上报数据，由<span style="color:red">0~9</span> 之间的15个字符组成<br /><br />

                            APN接入点：GPRS网络的接入点，例如移动为：CMNET <br /><br />
 
                            APN用户名：APN接入点的对应账号，如无用户名，可以为空<br /><br />


                            APN用户密码：APN账户的对应密码，如无密码，可以为空 <br /><br />
  
 
                            通信密钥：用于加密物联网表和后台的通信数据，由<span style="color:red">0~9和A~F</span> 之间的16个字符组成，可<br />以选择随机生成
                            <br /><br />
                  
                        </div>

                    </td>

                </tr>

            </table>

      
        </form>
    </div>
</body>
</html>
