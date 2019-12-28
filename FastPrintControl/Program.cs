using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using FastReport;
namespace FastPrintControl
{
    class Program
    {

        static void Main(string[] args)
        {
            try
            {
               
                    CreateDataSet();
                
            }
            catch (Exception ex)
            {
                LogHelper log = new LogHelper();
                log.WriteLog(ex.Message.ToString());
            }
        }

        static void CreateDataSet()
        {
            LogHelper log2 = new LogHelper();

            // create simple dataset with one table
            DataSet FDataSet = new DataSet();
            DataTable table = new DataTable();
            table.TableName = "Employees";
            FDataSet.Tables.Add(table);
            table.Columns.Add("ID", typeof(int));
            table.Columns.Add("Name", typeof(string));
            table.Rows.Add(1, "Andrew Fuller");
            table.Rows.Add(2, "Nancy Davolio");
            table.Rows.Add(3, "Margaret Peacock");
            string frxPath = string.Empty;
 
            frxPath=System.AppDomain.CurrentDomain.BaseDirectory+"\\report\\report.frx";

            log2.WriteLog(string.Format(frxPath));

            var tuple = PrintHelper.Print("Microsoft Print to PDF", frxPath, null, FDataSet);
            if (!tuple.Item1)
            {
                //打印失败
                log2.WriteLog(string.Format("打印失败"));
            }
            else
            {
                log2.WriteLog(string.Format("打印成功"));
            }


        }
    }
}
