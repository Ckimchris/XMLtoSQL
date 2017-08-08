using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DW_ManagedRetail_DataTests
{
    public static class MyGlobals
    {
        /********************************
        ** SQL Server Database Globals **
        *********************************/
        private static string _sqlServer = Environment.GetEnvironmentVariable("A4SQLServer");
        readonly public static string MasterSQLServerConn = "Server=" + _sqlServer + ";Database=Master;Integrated Security=SSPI";
        readonly public static string DW_ManagedRetailSQLServerConn = "Server=" + _sqlServer + ";Database=DW_ManagedRetail;Integrated Security=SSPI";
        readonly public static string ASMCRMProd_MSCRMSQLServerConn = "Server=" + _sqlServer + ";Database=ASMCRMProd_MSCRM;Integrated Security=SSPI";

        /*****************
        ** SSRS Globals **
        *****************/
        readonly public static string SSRSServerUrl = Environment.GetEnvironmentVariable("A4SSRSServer");
        public const string InitialPerformanceSummaryReportPath = @"/Albertson's Companies/Execution Recap";
        public const string PerformanceRecapReportPath = @"/Albertson's Companies/Performance Recap";
        public const string MSSMReportPath = @"/Walmart Retailtainment/Minimum Standard Store Metric Report";
        public const string ESReportPath = @"/Walmart Retailtainment/Event Specialist Disqualification";


        /**********************************
        ** Execution Recap XPath Globals **
        ***********************************/

        // Header
        public const string ExecRecap_Header_ProgramOpportunityList = @"/Report/@Text_ProgramOpportunityListReportHeader";

        // Event Execution Chart
        public const string ExecRecap_EventExecChart_Percentage = @"/Report/SubReport001_EventExecutionChart/Report/@Textbox_ExecutionPct";
        public const string ExecRecap_EventExecChart_CompletedOutOfTxt = @"/Report/SubReport001_EventExecutionChart/Report/@Textbox_EventsCompletedOfTotal";

        // Distribution Summary 
        public const string ExecRecap_DistSummary_Label1 = @"/Report/Table_Distribution4/@Table_Distribution4_Label_1";
        public const string ExecRecap_DistSummary_Value1 = @"/Report/Table_Distribution4/@Table_Distribution4_Value_1";
        public const string ExecRecap_DistSummary_Label2 = @"Report/Table_Distribution4/@Table_Distribution4_Label_2";
        public const string ExecRecap_DistSummary_Value2 = @"/Report/Table_Distribution4/@Table_Distribution4_Value_2";
        public const string ExecRecap_DistSummary_Label3 = @"Report/Table_Distribution4/@Table_Distribution4_Label_3";
        public const string ExecRecap_DistSummary_Value3 = @"/Report/Table_Distribution4/@Table_Distribution4_Value_3";
        public const string ExecRecap_DistSummary_Label4 = @"Report/Table_Distribution4/@Table_Distribution4_Label_4";
        public const string ExecRecap_DistSummary_Value4 = @"/Report/Table_Distribution4/@Table_Distribution4_Value_4";
        public static string[] ExecRecap_DistSummary_Labels = { ExecRecap_DistSummary_Label1
                                                              , ExecRecap_DistSummary_Label2
                                                              , ExecRecap_DistSummary_Label3
                                                              , ExecRecap_DistSummary_Label4 };
        public static string[] ExecRecap_DistSummary_Values = { ExecRecap_DistSummary_Value1
                                                              , ExecRecap_DistSummary_Value2
                                                              , ExecRecap_DistSummary_Value3
                                                              , ExecRecap_DistSummary_Value4 };


        /***********************
               ** MSSM Xpath Globals **
               ***********************/

        // MSSM columns
        public const string MSSM_StoreID = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[1]/@Table_MSSM_StoreID";
        public const string MSSM_TDLinxID = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[1]/Table_MSSM_TDLinxID[1]/@Table_MSSM_TDLinxID";
        public const string MSSM_StoreDetails = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[1]//Table_MSSM_StoreDetails/@Table_MSSM_StoreDetails";
        public const string MSSM_Project = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[1]//Details/@Table_MSSM_Project";
        public const string MSSM_Job = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[1]//Details[1]/@Table_MSSM_Job";
        public const string MSSM_EmployeeID = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[1]//Details[1]/@Table_MSSM_EmployeeID";
        public const string MSSM_EventDate = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[1]//Details[1]/@Table_MSSM_AssignmentDate";
        public const string MSSM_ExecutedDate = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[1]//Details[1]/@Table_MSSM_ExecutedDate";
        public const string MSSM_Target = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[1]//Table_MSSM_StoreDetails[1]/@Table_MSSM_Target";
        public const string MSSM_Metric = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[1]//Table_MSSM_StoreDetails[1]/@Table_MSSM_Metric";
        public const string MSSM_Breach = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[2]//Table_MSSM_StoreDetails[1]/@Table_MSSM_Breach";
        public const string MSSM_RunningWeeksBreach = @"/Report/Table_MSSM/GroupCalendarWeek_Collection//GroupStoreID[2]//Table_MSSM_StoreDetails[1]/@Table_MSSM_RunningWeeksInBreach";
        public const string MSSM_Breaches52Weeks = @"/Report/Table_MSSM/GroupCalendarWeek_Collection/GroupCalendarWeek/GroupStoreID_Collection/GroupStoreID[1]/Table_MSSM_TDLinxID/Table_MSSM_StoreDetails/@Table_MSSM_NumBreachesInRunning52Week";
        public const string MSSM_PlannedEventCount = @"/Report/Table_MSSM/GroupCalendarWeek_Collection/GroupCalendarWeek/GroupStoreID_Collection/GroupStoreID[1]/Table_MSSM_TDLinxID/Table_MSSM_StoreDetails/Details_Collection/Details[1]/@Table_MSSM_Detail_PlannedEventCount";
        public const string MSSM_OnDateExecuted = @"/Report/Table_MSSM/GroupCalendarWeek_Collection/GroupCalendarWeek/GroupStoreID_Collection/GroupStoreID[1]/Table_MSSM_TDLinxID/Table_MSSM_StoreDetails/Details_Collection/Details[1]/@Table_MSSM_Detail_OnDateExecutedCount";
        public const string MSSM_PercentOnDateExecuted = @"/Report/Table_MSSM/GroupCalendarWeek_Collection/GroupCalendarWeek/GroupStoreID_Collection/GroupStoreID[1]/Table_MSSM_TDLinxID/Table_MSSM_StoreDetails/Details_Collection/Details[1]/@Table_MSSM_Detail_OnDateExecutedPercent";
        public const string MSSM_EventStatus = @"";
        public const string MSSM_StatusReason = @"";
        public const string MSSM_Audit = @"";
        public const string MSSM_ExceptionViolation = @"/Report/Table_MSSM/GroupCalendarWeek_Collection/GroupCalendarWeek/GroupStoreID_Collection/GroupStoreID[1]/Table_MSSM_TDLinxID/Table_MSSM_StoreDetails/Details_Collection/Details[1]/@EventExceptionViolationType";
        public const string MSSM_CallReportExecutionResponse = @"/Report/Table_MSSM/GroupCalendarWeek_Collection/GroupCalendarWeek/GroupStoreID_Collection/GroupStoreID[1]/Table_MSSM_TDLinxID/Table_MSSM_StoreDetails/Details_Collection/Details[1]/@CallReportExecutionResponse";
        public const string MSSM_EventType = @"/Report/Table_MSSM/GroupCalendarWeek_Collection/GroupCalendarWeek/GroupStoreID_Collection/GroupStoreID[1]/Table_MSSM_TDLinxID/Table_MSSM_StoreDetails/Details_Collection/Details[1]/@Event";

    }
}
