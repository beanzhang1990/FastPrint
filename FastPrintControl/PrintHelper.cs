using System;
using System.Collections.Generic;
using System.Data;
using FastReport;
using FastReport.Data;

namespace FastPrintControl
{
    public class PrintHelper
    {
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="printerName">打印机</param>
        /// <param name="frxPath">模板</param>
        /// <param name="dicParam">字典参数</param>
        /// <param name="dsDataSource">数据源</param>
        /// <param name="printNum">打印数量</param>
        /// <returns></returns>
        public static Tuple<bool, string> Print(string printerName, string frxPath, Dictionary<string, object> dicParam, DataSet dsDataSource, int printNum = 1)
        {
            bool flag = false;
            string msg = "";
            FastReport.Report report = new FastReport.Report();
            try
            {
                report.Load(frxPath);
                report.DoublePass = true;
                if (dicParam != null && dicParam.Count > 0)
                {
                    foreach (var item in dicParam)
                    {
                        report.SetParameterValue(item.Key, item.Value);
                    }
                }
                if (dsDataSource != null && dsDataSource.Tables.Count > 0)
                {
                    report.RegisterData(dsDataSource);
                    foreach (DataSourceBase dataSourceBase in report.Dictionary.DataSources)
                    {
                        dataSourceBase.Enabled = true;
                    }
                }

                report.PrintSettings.ShowDialog = false;
                report.PrintSettings.Printer = printerName;
                report.PrintSettings.PrintMode = PrintMode.Split;
                EnvironmentSettings envSet = new EnvironmentSettings();
                envSet.ReportSettings.ShowProgress = false;
                for (int i = 0; i < printNum; i++)
                {
                    report.Print();
                }
                flag = true;
                msg = "打印成功";
            }
            catch (Exception ex)
            {
                flag = false;
                msg = ex.Message;
            }
            finally
            {
                report.Dispose();
            }
            return new Tuple<bool, string>(flag, msg);
        }

        /// <summary>
        /// 设计
        /// </summary>
        /// <param name="frxPath">模板</param>
        /// <param name="dicParam">字典参数</param>
        /// <param name="dsDataSource">数据源</param>
        /// <returns></returns>
        public static Tuple<bool, string> Design(string frxPath, Dictionary<string, object> dicParam, DataSet dsDataSource)
        {
            bool flag = false;
            string msg = "";
            FastReport.Report report = new FastReport.Report();
            try
            {
                report.Load(frxPath);
                report.DoublePass = true;
                if (dicParam != null && dicParam.Count > 0)
                {
                    foreach (var item in dicParam)
                    {
                        report.SetParameterValue(item.Key, item.Value);
                    }
                }
                if (dsDataSource != null && dsDataSource.Tables.Count > 0)
                {
                    report.RegisterData(dsDataSource);
                    foreach (DataSourceBase dataSourceBase in report.Dictionary.DataSources)
                    {
                        dataSourceBase.Enabled = true;
                    }
                }

                report.Design();
                flag = true;
                msg = "设计器打开成功";
            }
            catch (Exception ex)
            {
                flag = false;
                msg = ex.Message;
            }
            finally
            {
                report.Dispose();
            }
            return new Tuple<bool, string>(flag, msg);
        }
    }
}
