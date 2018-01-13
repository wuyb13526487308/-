using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;

namespace CY.IotM.Common.Tool
{
    public class WinRar
    {
        static WinRar()
        {
            //判断是否安装了WinRar.exe
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe");
            _existSetupWinRar = !string.IsNullOrEmpty(key.GetValue(string.Empty).ToString());
            //获取WinRar.exe路径
            _winRarPath = key.GetValue(string.Empty).ToString();
        }
        static bool _existSetupWinRar;
        /// <summary>
        /// 获取是否安装了WinRar的标识
        /// </summary>
        public static bool ExistSetupWinRar
        {
            get { return _existSetupWinRar; }
        }
        static string _winRarPath;
        /// <summary>
        /// 获取WinRar.exe路径
        /// </summary>
        public static string WinRarPath
        {
            get { return _winRarPath; }
        }
        /// <summary>
        /// 压缩到.rar
        /// </summary>
        /// <param name="intputPath">输入目录</param>
        /// <param name="outputPath">输出目录</param>
        /// <param name="outputFileName">输出文件名</param>
        public static void CompressRar(string intputPath, string outputPath, string outputFileName)
        {
            //rar 执行时的命令、参数
            string rarCmd;
            //启动进程的参数
            ProcessStartInfo processStartInfo;
            //进程对象
            Process process;
            try
            {
                if (!ExistSetupWinRar)
                {
                    throw new ArgumentException("Not setuping the winRar, you can Compress.make sure setuped winRar.");
                }
                //判断输入目录是否存在
                if (!Directory.Exists(intputPath))
                {
                    throw new ArgumentException("CompressRar'arge : inputPath isn't exsit.");
                }
                //命令参数
                rarCmd = " a -ep " + outputFileName + " " + intputPath + " -r";
                //创建启动进程的参数
                processStartInfo = new ProcessStartInfo();
                //指定启动文件名
                processStartInfo.FileName = WinRarPath;
                //指定启动该文件时的命令、参数
                processStartInfo.Arguments = rarCmd;
                //指定启动窗口模式：隐藏
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                //指定压缩后到达路径
                processStartInfo.WorkingDirectory = outputPath;
                //创建进程对象
                process = new Process();
                //指定进程对象启动信息对象
                process.StartInfo = processStartInfo;
                //启动进程
                process.Start();
                //指定进程自行退行为止
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 解压.rar
        /// </summary>
        /// <param name="inputRarFileName">输入.rar</param>
        /// <param name="outputPath">输出目录</param>
        public static void UnCompressRar(string inputRarFileName, string outputPath)
        {
            //rar 执行时的命令、参数
            string rarCmd;
            //启动进程的参数
            ProcessStartInfo processStartInfo;
            //进程对象
            Process process;
            try
            {
                if (!ExistSetupWinRar)
                {
                    throw new ArgumentException("Not setuping the winRar, you can UnCompress.make sure setuped winRar.");
                }
                //如果压缩到目标路径不存在
                if (!Directory.Exists(outputPath))
                {
                    //创建压缩到目标路径
                    Directory.CreateDirectory(outputPath);
                }
                rarCmd = "x " + inputRarFileName + " " + outputPath + " -y";
                processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = WinRarPath;
                processStartInfo.Arguments = rarCmd;
                processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                processStartInfo.WorkingDirectory = outputPath;
                process = new Process();
                process.StartInfo = processStartInfo;
                process.Start();
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}