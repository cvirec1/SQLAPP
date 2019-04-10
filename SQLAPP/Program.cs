using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SQLAPP
{
    class Program
    {
        static void Main(string[] args)
        {

            string connString = @"Server = VALJASEK\SQL2017; Database = AdventureWorks; Trusted_Connection = True;";
            string sqlQuery1 = @" select max(TotalDue)  FROM [AdventureWorks].[Sales].[SalesOrderHeader];";
            string sqlQuery2 = @" select min(TotalDue)  FROM [AdventureWorks].[Sales].[SalesOrderHeader];";
            string sqlQuery3 = @"select MAX(OrderDate) FROM [AdventureWorks].[Purchasing].[PurchaseOrderHeader] 
                                where VendorID in (select VendorID from Purchasing.Vendor where [name] = @company);";
            string sqlQuery4 = " select COUNT(*) FROM [AdventureWorks].[HumanResources].[Employee] as e " +
                               "right join Sales.SalesPerson as p on e.BusinessEntityID=p.BusinessEntityID" +
                               " where e.gender = @gender; ";
            string sqlQuery5 = " select s.[Name] as Shipment,v.[Name] as Vendor,h.OrderDate,h.SubTotal,h.TaxAmt,s.ShipBase,h.TotalDue FROM [AdventureWorks].[Purchasing].[PurchaseOrderHeader] as h join Purchasing.ShipMethod as s on h.ShipMethodID = s.ShipMethodID join Purchasing.Vendor as v on h.VendorID = v.BusinessEntityID where h.PurchaseOrderID = @id;";

            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(connString))
            //    {                    
            //        using (var sqlCmd = new SqlCommand(sqlQuery1, connection))
            //        {
            //            connection.Open();
            //            double max = Convert.ToDouble(sqlCmd.ExecuteScalar());
            //            Console.WriteLine(max);
            //        }                                        
            //    }
            //    Console.WriteLine("Connected");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("No connections");
            //}
            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(connString))
            //    {   
            //        using (var sqlCmd = new SqlCommand(sqlQuery2, connection))
            //        {
            //            connection.Open();
            //            double min = Convert.ToDouble(sqlCmd.ExecuteScalar());
            //            Console.WriteLine(min);
            //        }                    
            //    }
            //    Console.WriteLine("Connected");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("No connections");
            //}
            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(connString))
            //    {
            //        string company = Console.ReadLine();
            //        using (var sqlCmd = new SqlCommand(sqlQuery3, connection))
            //        {
            //            connection.Open();
            //            sqlCmd.Parameters.Add("@company", SqlDbType.NVarChar).Value = company;
            //            DateTime maxdate = Convert.ToDateTime(sqlCmd.ExecuteScalar());
            //            Console.WriteLine(maxdate);
            //        }
                    
            //    }
            //    Console.WriteLine("Connected");
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine("No connections");
            //}
            //try
            //{
            //    using (SqlConnection connection = new SqlConnection(connString))
            //    {
            //        string gender = Console.ReadLine();                    
            //        using (var sqlCmd = new SqlCommand(sqlQuery4, connection))
            //        {
            //            connection.Open();                        
            //            sqlCmd.Parameters.Add("@gender", SqlDbType.NVarChar).Value = gender;
            //            //int countgender = Convert.ToInt32(sqlCmd.ExecuteScalar());
            //            Console.WriteLine(sqlCmd.ExecuteScalar());
            //        }                    
            //    }
            //    Console.WriteLine("Connected");
            //}
            //catch (Exception e)
            //{
            //    e.Message.ToString();
            //    Console.WriteLine("No connections");
            //}

            try
            {
                using (SqlConnection connection = new SqlConnection(connString))
                {
                    int idOrder;
                    int.TryParse(Console.ReadLine(), out idOrder);
                    using (var sqlCmd = new SqlCommand(sqlQuery5, connection))
                    {
                        sqlCmd.Parameters.Add("@id", SqlDbType.Int).Value = idOrder;
                        connection.Open();
                        try
                        {
                            using (SqlDataReader reader = sqlCmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    StringBuilder sb = new StringBuilder();
                                    sb.AppendLine(reader.GetName(0) + " : " + reader.GetString(0));
                                    sb.AppendLine(reader.GetName(1) + " : " + reader.GetString(1));
                                    sb.AppendLine(reader.GetName(2) + " : " + reader.GetDateTime(2));
                                    sb.AppendLine(reader.GetName(3) + " : " + reader.GetSqlMoney(3));
                                    sb.AppendLine(reader.GetName(4) + " : " + reader.GetSqlMoney(4));
                                    sb.AppendLine(reader.GetName(5) + " : " + reader.GetSqlMoney(5));
                                    sb.AppendLine(reader.GetName(6) + " : " + reader.GetSqlMoney(6));
                                    Console.WriteLine(sb.ToString());
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            e.Message.ToString();
                            Console.WriteLine("No connections");
                        }
                        //sqlCmd.Parameters.Add("@id", SqlDbType.Int).Value = idOrder;
                        //int countgender = Convert.ToInt32(sqlCmd.ExecuteScalar());
                        //Console.WriteLine(sqlCmd.ExecuteScalar());
                    }
                }
                Console.WriteLine("Connected");
            }
            catch (Exception e)
            {
                e.Message.ToString();
                Console.WriteLine("No connections");
            }



            Console.ReadKey();
        }
    }
}
