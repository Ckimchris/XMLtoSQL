using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
//using QAToolsRS;

namespace DW_ManagedRetail_DataTests
{
    public static class Utility
    {
        /// <summary>
        /// Converts a List of strings into a comma separated string with each values single quoted
        /// For use in a SQL "IN" statment
        /// Output: "'Value1', 'Value2', 'Value3'"
        /// </summary>
        /// <param name="stringList">ex. ["Value1", "Value2"]</param>
        public static string ConvertListToQuotedCommaString(List<string> stringList)
        {
            string result = "";
            List<string> newList = new List<string>();

            if (stringList.Count > 0)
            {
                foreach (string item in stringList)
                {
                    newList.Add("'" + item + "'");
                }
                result = string.Join(",", newList);
            }
            return result;
        }

        /// <summary>
        /// Uses xPath to find value in an XmlDocument
        /// Returns: string value"
        /// </summary>
        /// <param name="xdoc">Takes an XmlDocument</param>
        /// <param name="xPath">xPath to the single node</param>
        public static string GetNodeText(XmlDocument xdoc, string xPath)
        {
            string nodeText = "";

            try
            {
                var node = xdoc.SelectSingleNode(xPath);
                nodeText = node.InnerText;
            }
            catch (Exception e)
            {
                return "";
            }

            return nodeText;
        }

        public static int GetCount(XmlDocument xdoc, string xPath)
        {
            int count = 0;

            try
            {
                XmlNodeList node = xdoc.SelectNodes(xPath);
                count = node.Count;
            }
            catch (Exception e)
            {
                return 0;
            }

            return count;
        }

        public static int GetNodeTextAndConvertToInt(XmlDocument xdoc, string xPath)
        {
            string nodeText = "";
            int number;

            try
            {
                var node = xdoc.SelectSingleNode(xPath);
                nodeText = node.InnerText;
                number = Int32.Parse(nodeText);
            }
            catch (Exception e)
            {
                return 0;
            }

            return number;
        }

        /// <summary>
        /// Renders report and cleans up xml
        /// Returns: XmlDocument"
        /// </summary>
        /// <param name="rsExecutor">SSRSExecutor object used to render report</param>
        //public static XmlDocument RenderReportXML(SSRSExecutor rsExecutor)
        //{
        //    string tmpReportOutputPath;
        //    XmlDocument xdoc = new XmlDocument();
        //    string filter = @"xmlns(:\w+)?=""([^""]+)""|xsi(:\w+)?=""([^""]+)""";

        //    try
        //    {
        //        string tempfile = Path.GetTempFileName();
        //        tmpReportOutputPath = tempfile.Replace(".tmp", ".xml");
        //        rsExecutor.RenderReport("XML", tmpReportOutputPath);
        //        string xmlString = System.IO.File.ReadAllText(tmpReportOutputPath);
        //        xmlString = Regex.Replace(xmlString, filter, "");

        //        xdoc.LoadXml(xmlString);
        //        File.Delete(tempfile);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Failed to read xml file");
        //        Console.WriteLine("Error Message: " + ex.ToString());
        //        throw ex;
        //    }

        //    File.Delete(tmpReportOutputPath);

        //    return xdoc;
        //}

        /// <summary>
        /// Renders report and cleans up xml
        /// Returns: XmlDocument"
        /// </summary>
        /// <param name="rsExecutor">SSRSExecutor object used to render report</param>
        /// <param name="timeout">Set render timeout in milliseconds</param>
        //public static XmlDocument RenderReportXML(SSRSExecutor rsExecutor, int timeout)
        //{
        //    string tmpReportOutputPath;

        //    XmlDocument xdoc = new XmlDocument();
        //    string filter = @"xmlns(:\w+)?=""([^""]+)""|xsi(:\w+)?=""([^""]+)""";

        //    try
        //    {
        //        string tempFile = Path.GetTempFileName();
        //        tmpReportOutputPath = tempFile.Replace(".tmp", ".xml");
        //        rsExecutor.RenderReport("XML", tmpReportOutputPath, timeout);
        //        string xmlString = System.IO.File.ReadAllText(tmpReportOutputPath);
        //        xmlString = Regex.Replace(xmlString, filter, "");

        //        xdoc.LoadXml(xmlString);
        //        File.Delete(tempFile);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Failed to read xml file");
        //        Console.WriteLine("Error Message: " + ex.ToString());
        //        throw ex;
        //    }

        //    File.Delete(tmpReportOutputPath);
        //    return xdoc;
        //}

        /// <summary>
        /// Queries DW_ManagedRetail.DW.Work for the ProgramNumber give an Program_SK
        /// Returns: ProgramNumber string"
        /// </summary>
        /// <param name="programSK">program_SK from DW_ManagedRetail.DW.Work</param>
        public static string GetProgramNumber(string programSK)
        {
            string programNumber = null;

            using (var conn = new SqlConnection(MyGlobals.DW_ManagedRetailSQLServerConn))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string query = @"SELECT DISTINCT ProgramNumber
                                 FROM DW_ManagedRetail.DW.Work
                                 WHERE Program_SK = {0}";

                cmd.CommandText = string.Format(query, programSK);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    programNumber = (string)reader["ProgramNumber"];
                }
                reader.Close();
            }
            return programNumber;
        }

        /// <summary>
        /// Queries DW_ManagedRetail.DW.Work for the Program_SK give an ProgramNumber
        /// Returns: Program_SK string"
        /// </summary>
        /// <param name="programNumber">programNumber from DW_ManagedRetail.DW.Work</param>
        public static string GetProgramSK(string programNumber)
        {
            string programSK = null;

            using (var conn = new SqlConnection(MyGlobals.DW_ManagedRetailSQLServerConn))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string query = @"SELECT DISTINCT Program_SK
                                 FROM DW_ManagedRetail.DW.Work
                                 WHERE ProgramNumber = '{0}'";

                cmd.CommandText = string.Format(query, programNumber);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    programSK = (string)reader["Program_SK"].ToString();
                }
                reader.Close();
            }
            return programSK;
        }

        /// <summary>
        /// Queries DW_ManagedRetail.DW.Opportunity for the Opportunity given a Opportunity_SK
        /// Returns: Opportunity string"
        /// </summary>
        /// <param name="opportunitySK">Opportunity_SK from DW_ManagedRetail.DW.Work</param>
        public static string GetOpportunity(string opportunitySK)
        {
            string opportunity = null;

            using (var conn = new SqlConnection(MyGlobals.DW_ManagedRetailSQLServerConn))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string query = @"SELECT Opportunity
                                    FROM [DW_ManagedRetail].[DW].[Opportunity]
                                    WHERE Opportunity_SK = {0}";

                cmd.CommandText = string.Format(query, opportunitySK);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    opportunity = (string)reader["Opportunity"];
                }
                reader.Close();
            }
            return opportunity;
        }

        /// <summary>
        /// Queries DW_ManagedRetail.DW.Opportunity for the Opportunity_BK given a Opportunity_SK
        /// Returns: Opportunity_BK string"
        /// </summary>
        /// <param name="opportunitySK">Opportunity_SK from DW_ManagedRetail.DW.Work</param>
        public static string GetOpportunityBK(string opportunitySK)
        {
            string opportunityBK = null;

            using (var conn = new SqlConnection(MyGlobals.DW_ManagedRetailSQLServerConn))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string query = @"SELECT Opportunity_BK
                                    FROM [DW_ManagedRetail].[DW].[Opportunity]
                                    WHERE Opportunity_SK = {0}";

                cmd.CommandText = string.Format(query, opportunitySK);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    opportunityBK = (string)reader["Opportunity_BK"];
                }
                reader.Close();
            }
            return opportunityBK;
        }

        /// <summary>
        /// Queries DW_ManagedRetail.DW.Platform for the Platform given a Platform_SK
        /// Returns: Platform string"
        /// </summary>
        /// <param name="platformSK">Platform_SK from DW_ManagedRetail.DW.Platform</param>
        public static string GetPlatform(string platformSK)
        {
            string platform = null;

            using (var conn = new SqlConnection(MyGlobals.DW_ManagedRetailSQLServerConn))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string query = @"SELECT DISTINCT Platform
                                 FROM DW_ManagedRetail.DW.Platform
                                 WHERE Platform_SK = {0}";

                cmd.CommandText = string.Format(query, platformSK);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    platform = (string)reader["Platform"];
                }
                reader.Close();
            }
            return platform;
        }

        /// <summary>
        /// Queries DW_ManagedRetail.DW.Platform for the Platform_SK given a Platform
        /// Returns: Platform_SK string"
        /// </summary>
        /// <param name="platform">Platform from DW_ManagedRetail.DW.Platform</param>
        public static string GetPlatformSK(string platform)
        {
            string platformSK = null;

            using (var conn = new SqlConnection(MyGlobals.DW_ManagedRetailSQLServerConn))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string query = @"SELECT DISTINCT Platform_SK
                                 FROM DW_ManagedRetail.DW.Platform
                                 WHERE Platform = '{0}'";

                cmd.CommandText = string.Format(query, platform);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    platformSK = (string)reader["Platform_SK"].ToString();
                }
                reader.Close();
            }
            return platformSK;
        }

        /// <summary>
        /// Queries DW_ManagedRetail.DW.CalendarWeek for the CalendarWeek_SK given a CalendarWeek
        /// Returns: CalendarWeek_SK string"
        /// </summary>
        /// <param name="calendarWeek">CalendarWeek from DW_ManagedRetail.DW.CalendarWeek</param>
        public static string GetCalendarWeekSK(string calendarWeek)
        {
            string calendarWeekSK = null;

            using (var conn = new SqlConnection(MyGlobals.DW_ManagedRetailSQLServerConn))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string query = @"SELECT CalendarWeek_SK
                                FROM [DW_ManagedRetail].[DW].[CalendarWeek]
                                WHERE	CalendarWeek = '{0}'";

                cmd.CommandText = string.Format(query, calendarWeek);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    calendarWeekSK = (string)reader["CalendarWeek_SK"].ToString();
                }
                reader.Close();
            }
            return calendarWeekSK;
        }

        /// <summary>
        /// Queries DW_ManagedRetail.DW.CalendarWeek for the CalendarWeek given a CalendarWeek_SK
        /// Returns: CalendarWeek string"
        /// </summary>
        /// <param name="calendarWeekSK">CalendarWeek_SK from DW_ManagedRetail.DW.CalendarWeek</param>
        public static string GetCalendarWeek(string calendarWeekSK)
        {
            string calendarWeek = null;

            using (var conn = new SqlConnection(MyGlobals.DW_ManagedRetailSQLServerConn))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string query = @"SELECT CalendarWeek
                                FROM [DW_ManagedRetail].[DW].[CalendarWeek]
                                WHERE	CalendarWeek_SK = '{0}'";

                cmd.CommandText = string.Format(query, calendarWeekSK);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    calendarWeek = (string)reader["CalendarWeek"].ToString();
                }
                reader.Close();
            }
            return calendarWeek;
        }

        /// <summary>
        /// Queries DW_ManagedRetail.DW.AssignmentProduct for the ProductUse given a ProductUse_SK
        /// Returns: ProductUse string"
        /// </summary>
        /// <param name="productUseSK">ProductUse_SK from DW_ManagedRetail.DW.CalendarWeek</param>
        public static string GetProductUse(string productUseSK)
        {
            string productUse = null;

            using (var conn = new SqlConnection(MyGlobals.DW_ManagedRetailSQLServerConn))
            using (var cmd = new SqlCommand())
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                string query = @"SELECT DISTINCT ProductUse_SK
				                                    ,ProductUse
                                    FROM [DW_ManagedRetail].[DW].[AssignmentProduct]
                                    WHERE	ProductUse_SK = '{0}'";

                cmd.CommandText = string.Format(query, productUseSK);

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    productUse = (string)reader["ProductUse"].ToString();
                }
                reader.Close();
            }
            return productUse;
        }
    }
}
