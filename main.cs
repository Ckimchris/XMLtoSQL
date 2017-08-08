using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using NUnit.Framework;
using QAToolsRS;
using Utility;
using System.Xml.XPath;

namespace DW_ManagedRetail_DataTests.ReportTests.MSSM_Tests.ProjectTests
{
    class ES_ProjectType_Tests
    {
        public static SSRSExecutor _MSSMReport;

        MiscUtil miscUtil = new MiscUtil();
        public static void Setup()
        {
            _MSSMReport = new SSRSExecutor(MyGlobals.SSRSServerUrl, MyGlobals.MSSMReportPath);
            _MSSMReport.SetReportParameterValue("ProjectType", _MSSMReport.GetAvaialableParameterValues("ProjectType"));
            _MSSMReport.SetReportParameterValue("ActivityType", _MSSMReport.GetAvaialableParameterValues("ActivityType"));
            _MSSMReport.SetReportParameterValue("Project", _MSSMReport.GetAvaialableParameterValues("Project"));
            _MSSMReport.SetReportParameterValue("EmployeeID", _MSSMReport.GetAvaialableParameterValues("EmployeeID"));
            _MSSMReport.SetReportParameterValue("WalmartDemoWeekStart", "201648");
        }


        public static void test()
        {
            ReportXmlUtil rptXMLUtil = new ReportXmlUtil();

            XmlDocument report = rptXMLUtil.RenderReportXML(_MSSMReport, 1200000);

            int count = Utility.GetCount(report, @"Report/Table_MSSM/GroupStore_Collection/GroupStore");

            int tot = Utility.GetCount(report, @"Report/Table_MSSM/GroupStore_Collection/GroupStore/Table_MSSM_Store_TDLinxID/Table_MSSM_Store_StoreDetails/GroupCalendarWeek_Collection/GroupCalendarWeek/Table_MSSM_Week_Timeframe/GroupEvent_Collection/GroupEvent/Textbox1077/Textbox228");
            Console.WriteLine(tot);

            string[] breachColumn = new string[count];
            string[] breachCalc = new string[count];
            int projectCount = 0;
            int a = 0;
            int[] addedup = new int[count];
            int[] total = new int[count];
            for (int i = 1; i < 2; i++)
            {
                string storeID = Utility.GetNodeText(report, @"Report/Table_MSSM/GroupStore_Collection/GroupStore[" + i + "]/@Table_MSSM_Store_StoreID");
                int wkCount = Utility.GetCount(report, @"Report/Table_MSSM/GroupStore_Collection/GroupStore[" + i + "]/Table_MSSM_Store_TDLinxID/Table_MSSM_Store_StoreDetails/GroupCalendarWeek_Collection/GroupCalendarWeek");


                for (int j = 1; j <= wkCount; j++)
                {
                    int projCount = Utility.GetCount(report, @"/Report/Table_MSSM/GroupStore_Collection/GroupStore[" + i + "]/Table_MSSM_Store_TDLinxID/Table_MSSM_Store_StoreDetails/GroupCalendarWeek_Collection/GroupCalendarWeek[" + j + "]/Table_MSSM_Week_Timeframe/GroupEvent_Collection/GroupEvent");
                    string fourWeeklyOnDateExecutedCount = rptXMLUtil.GetNodeText(report, @"Report/Table_MSSM/GroupStore_Collection/GroupStore[" + i + "]/Table_MSSM_Store_TDLinxID/Table_MSSM_Store_StoreDetails/GroupCalendarWeek_Collection/GroupCalendarWeek[" + j + "]/Table_MSSM_Week_Timeframe/Textbox481/Textbox1077/Textbox228/@Table_MSSM_Week_OnDateExecutedCount");
                    int exp = Int32.Parse(fourWeeklyOnDateExecutedCount);
                    total[i] = exp;
                    Console.WriteLine("J " + j + " : " + total[i]);

                    for (int k = 1; k <= projCount; k++)
                    {
                        string indivWeeklyOnDateExecutedCount = rptXMLUtil.GetNodeText(report, @"/Report/Table_MSSM/GroupStore_Collection/GroupStore[" + i + "]/Table_MSSM_Store_TDLinxID/Table_MSSM_Store_StoreDetails/GroupCalendarWeek_Collection/GroupCalendarWeek[" + j + "]/Table_MSSM_Week_Timeframe/GroupEvent_Collection/GroupEvent[" + k + "]/Textbox1077/Textbox228/@Table_MSSM_Detail_OnDateExecutedCount");
                        a = Int32.Parse(indivWeeklyOnDateExecutedCount);
                        addedup[i] += a;
                        Console.WriteLine("k " + k +  " : " + addedup[i]);
                    }
                    Console.WriteLine(storeID + " : " + total[i] + " , " + addedup[i]);
                }
            }
        }

        static void Main(string[] args)  
        {
            Setup();
            test();
            System.Console.ReadKey();
        }
    }
}
