using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Web.Services;
using System.Web.Services.Description;
using System.Web.Services.Protocols;
using System.Xml.Serialization;

using CY.IoTM.OlbCommon;
using CY.IoTM.OlbCommon.Tool;
using System.Reflection;

namespace CY.IoTM.OlbService
{
    public class IotMService
    {



        private static IotMService instance = null;
        public static IotMService GetInstance()
        {
            if (instance == null)
            {
                instance = new IotMService();
            }
            return instance;
        }

        private IotMService()
        {

            string serviceUrl = System.Configuration.ConfigurationManager.AppSettings["IotMServiceUrl"];




            //调用方式一：生成客户端代理程序集文件

            //// 1. 使用 WebClient 下载 WSDL 信息。
            //WebClient web = new WebClient();
            //Stream stream = web.OpenRead(serviceUrl);

            //// 2. 创建和格式化 WSDL 文档。
            //ServiceDescription description = ServiceDescription.Read(stream);

            //// 3. 创建客户端代理代理类。
            //ServiceDescriptionImporter importer = new ServiceDescriptionImporter();

            //importer.ProtocolName = "Soap"; // 指定访问协议。
            //importer.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。
            //importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;

            //importer.AddServiceDescription(description, null, null); // 添加 WSDL 文档。

            //// 4. 使用 CodeDom 编译客户端代理类。
            //CodeNamespace nmspace = new CodeNamespace(); // 为代理类添加命名空间，缺省为全局空间。
            //CodeCompileUnit unit = new CodeCompileUnit();
            //unit.Namespaces.Add(nmspace);

            //ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            //CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
        

            //CompilerParameters parameter = new CompilerParameters();
            //parameter.GenerateExecutable = false;
            //parameter.OutputAssembly = "IotMService.dll"; // 可以指定你所需的任何文件名。
            //parameter.ReferencedAssemblies.Add("System.dll");
            //parameter.ReferencedAssemblies.Add("System.XML.dll");
            //parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            //parameter.ReferencedAssemblies.Add("System.Data.dll");
            
          
            //CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);
            //if (result.Errors.HasErrors)
            //{
            //    // 显示编译错误信息
            //}

            //asm = Assembly.LoadFrom("IotMService.dll");
            //t = asm.GetType("OlbService");




            //调用方式二：内存中创建程序集

            // 1. 使用 WebClient 下载 WSDL 信息。
            WebClient web = new WebClient();
            Stream stream = web.OpenRead(serviceUrl);

            // 2. 创建和格式化 WSDL 文档。
            ServiceDescription description = ServiceDescription.Read(stream);

            // 3. 创建客户端代理代理类。
            ServiceDescriptionImporter importer = new ServiceDescriptionImporter();

            importer.ProtocolName = "Soap"; // 指定访问协议。
            importer.Style = ServiceDescriptionImportStyle.Client; // 生成客户端代理。
            importer.CodeGenerationOptions = CodeGenerationOptions.GenerateProperties | CodeGenerationOptions.GenerateNewAsync;

            importer.AddServiceDescription(description, null, null); // 添加 WSDL 文档。

            // 4. 使用 CodeDom 编译客户端代理类。
            CodeNamespace nmspace = new CodeNamespace(); // 为代理类添加命名空间，缺省为全局空间。
            CodeCompileUnit unit = new CodeCompileUnit();
            unit.Namespaces.Add(nmspace);

            ServiceDescriptionImportWarnings warning = importer.Import(nmspace, unit);
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");

            CompilerParameters parameter = new CompilerParameters();
            parameter.GenerateExecutable = false;
            parameter.GenerateInMemory = true;
            parameter.ReferencedAssemblies.Add("System.dll");
            parameter.ReferencedAssemblies.Add("System.XML.dll");
            parameter.ReferencedAssemblies.Add("System.Web.Services.dll");
            parameter.ReferencedAssemblies.Add("System.Data.dll");

            CompilerResults result = provider.CompileAssemblyFromDom(parameter, unit);


            // 5. 使用 Reflection 调用 WebService。
            if (!result.Errors.HasErrors)
            {
                asm = result.CompiledAssembly;
                t = asm.GetType("OlbService"); // 如果在前面为代理类添加了命名空间，此处需要将命名空间添加到类型前面。
            }

        }

        private Assembly asm = null;
        private Type t = null;


        /// <summary>
        /// 获取公司列表
        /// </summary>
        /// <returns></returns>
        public List<Company> GetCompanyList() {


            object o = Activator.CreateInstance(t);
            MethodInfo method = t.GetMethod("GetCompanyList");

            object objStr = method.Invoke(o, null);

            List<Company> list = JsToJson.Deserialize<List<Company>>(objStr.ToString());

            return list;
        
        }



        /// <summary>
        /// 根据户号获取燃气用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public GasUser GetGasUserByUserId(string userId, string companyId)
        {

            object o = Activator.CreateInstance(t);
            MethodInfo method = t.GetMethod("GetGasUserByUserId");

            object [] param={userId,companyId};

            string objStr = method.Invoke(o, param).ToString();

            GasUser user = JsToJson.Deserialize<GasUser>(objStr);

            return user;

        }



        /// <summary>
        /// 充值
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public string Charge(string userId, string companyId,decimal money)
        {

            object o = Activator.CreateInstance(t);
            MethodInfo method = t.GetMethod("Charge");

            object[] param = { userId, companyId, money };

            string objStr = method.Invoke(o, param).ToString();

            return objStr;

        }





        /// <summary>
        /// 根据户号获取燃气用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="companyId"></param>
        /// <returns></returns>
        public GasFeeBill GetGasUserBill(string userId, string companyId, string month)
        {

            object o = Activator.CreateInstance(t);
            MethodInfo method = t.GetMethod("GetGasUserBill");

            object[] param = { userId, companyId };

            string objStr = method.Invoke(o, param).ToString();

            GasFeeBill bill = JsToJson.Deserialize<GasFeeBill>(objStr);

            return bill;

        }

    



    }
}
