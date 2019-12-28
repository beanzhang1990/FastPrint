using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPrintControl
{
    public class LogHelper
    {
        public void WriteLog(string msg)
        {
            try
            {
                string root = System.AppDomain.CurrentDomain.BaseDirectory;
                string dirPath = "\\Logs\\" + System.DateTime.Now.ToString("yyyyMMdd") + "\\";
                string fileName = System.DateTime.Now.ToString("HHmmssffff") + ".txt";
                string path = root + dirPath + fileName;

                if (!Directory.Exists(root + dirPath))
                {
                    Directory.CreateDirectory(root + dirPath);
                }
                //创建文件
                if (!File.Exists(path))
                {
                    StreamWriter sw = File.CreateText(path);
                    sw.Flush();
                    sw.Close();
                }
                StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8);
                StringBuilder builder = new StringBuilder();
                builder.Append("时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                builder.Append("\r\n==============================begin======================================");
                builder.Append("\r\nLOG信息：");
                builder.Append("\r\n" + msg);
                builder.Append("\r\n============================== end ======================================");
                writer.Write(builder);
                writer.Flush();
                writer.Close();
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }

        }
    }
}
