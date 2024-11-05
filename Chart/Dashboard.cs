using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestion_Entrepot.Chart
{

    public struct RevenueByDate
    {
        public string Date { get; set; }
        public int TotalAmount { get; set; }

        public string DateEntree { get; set; }
        public int TotalEntree { get; set; }
    }
    public class Dashboard : DbConnection
    {

        private DateTime startDate;
        private DateTime endDate;
        private int numberDays;

        public int NumEntree { get; private set; }
        public int EntreeTotal { get; private set; }

        public int EntreeReste { get; private set; }

        public int NumReste { get; private set; }

        public int TotalSortie { get; private set; }
        public int TotalDeclarant { get; private set; }
        public int TotalImportateur { get; private set; }
        public int NumSortie { get; private set; }
        public int NumSuppliers { get; private set; }
        public int NumProducts { get; private set; }
        public List<KeyValuePair<string, int>> TopProductsList { get; private set; }
        public List<KeyValuePair<string, int>> UnderstockList { get; private set; }
        public List<RevenueByDate> GrossRevenueList { get; private set; }
        public List<RevenueByDate> GrossEntreeList { get; private set; }
        public int NumOrders { get; set; }
        public int TotalRevenue { get; set; }
        public int TotalProfit { get; set; }

        public int TotalEntree { get; set; }

        //Constructor
        public Dashboard()
        {

        }

        private void GetNumberItems()
        {
            try
            {
                using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    connection.Open();
                    using (var command = new SqlCommand())
                    {
                        command.Connection = connection;
                        //Get Total Number of Customers
                        command.CommandText = "select count(id_Colis) from Colis";
                        NumEntree = (int)command.ExecuteScalar();

                        //Get TotatQuantite Entree
                        command.CommandText = "select  sum(Quantite) from Magasinage";
                        EntreeTotal = (int)command.ExecuteScalar();

                        //Get TotatReste Entree
                        command.CommandText = "select sum(Quantite) - sum(Reste) from Magasinage";
                        TotalSortie = (int)command.ExecuteScalar();

                        //Get Total Nombres des Sortie Egale Zero 
                        command.CommandText = "select count(id_Magasinage) from Magasinage where reste =0";
                        NumSortie = (int)command.ExecuteScalar();

                        //Get Total Des Nombres des packets Restants
                        command.CommandText = "select count(id_Magasinage) from Magasinage where reste <>0";
                        NumReste = (int)command.ExecuteScalar();

                        //Get Total Des Quantites des packets Restants 
                        command.CommandText = "select sum(Reste) from Magasinage";
                        EntreeReste = (int)command.ExecuteScalar();

                        //Get Total Declarants 
                        command.CommandText = "Select count(id_Declarant) from Declarant";
                        TotalDeclarant = (int)command.ExecuteScalar();

                        //Get Total Importateur 
                        command.CommandText = "Select count(id_Importateur) from Importateur";
                        TotalImportateur = (int)command.ExecuteScalar();

                    }
                }
            }
            catch (Exception)
            {

                
            }
        }
        private void GetProductAnalisys()
        {
            TopProductsList = new List<KeyValuePair<string, int>>();
            UnderstockList = new List<KeyValuePair<string, int>>();
            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    SqlDataReader reader;
                    command.Connection = connection;
                    //Get Top 5 products
                    command.CommandText = @"select top 5 CONCAT(Colis.Designation,': ', Retrait.Nature), sum(Retrait.Quantite) as Q 
                                            from Retrait  inner join Colis on Colis.Id_Colis=Retrait.Colis   where Retrait.Date
                                            between @fromDate and @toDate 
                                            group by Retrait.Nature, Colis.Designation order by Q";

                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TopProductsList.Add(
                            new KeyValuePair<string, int>(reader[0].ToString(), (int)reader[1]));
                    }
                    reader.Close();

                    //Get Understock
                    command.CommandText = @"select Colis.Nature as Nature, Magasinage.Reste as Reste
                                            from Magasinage inner join Colis
                                            on Colis.Id_Colis=Magasinage.Colis
                                            where Reste <=20 and Reste <> 0";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        UnderstockList.Add(
                            new KeyValuePair<string, int>(reader[0].ToString(), (int)reader[1]));
                    }
                    reader.Close();
                }
            }
        }


        private void GetOrderAnalisys()
        {
            GrossRevenueList = new List<RevenueByDate>();
            TotalProfit = 0;
            TotalRevenue = 0;

            GrossEntreeList = new List<RevenueByDate>();
            TotalEntree = 0;

            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"Select Max(Circulation.Date) as Date, case when count(type)>=1 then 1 end as Total from Circulation 
                                            inner join Colis on Colis.Id_Colis=Circulation.Colis where circulation.Type='Sortie' 
                                            and Colis.Designation in(Select Colis.Designation from Magasinage inner join Colis on Colis.Id_Colis=Magasinage.Colis where Magasinage.Reste=0)
                                            and Circulation.Reste in (Select Circulation.Reste from Circulation inner join Colis on Colis.Id_Colis=Circulation.Colis where Circulation.Reste=0)
                                            and Colis.Designation in (Select Colis.Designation from Magasinage inner join Colis on colis.Id_Colis =Magasinage.Colis group by Colis.Designation having sum(Magasinage.Reste)=0)
                                            and circulation.Date between @fromDate and @toDate
                                            group by Colis.Designation";


                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;
                    var reader = command.ExecuteReader();
                    var resultTable = new List<KeyValuePair<DateTime, int>>();
                    while (reader.Read())
                    {
                        resultTable.Add(
                            new KeyValuePair<DateTime, int>((DateTime)reader[0], (int)reader[1])
                            );
                        TotalRevenue += (int)reader[1];
                    }
                    TotalProfit = TotalRevenue * 0;//20%
                    reader.Close();

                    //Group by Hours
                    if (numberDays <= 1)
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("hh tt")
                                           into order
                                            select new RevenueByDate
                                            {
                                                Date = order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                    //Group by Days
                    else if (numberDays <= 30)
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("dd MMM")
                                           into order
                                            select new RevenueByDate
                                            {
                                                Date = order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }

                    //Group by Weeks
                    else if (numberDays <= 92)
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                                orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                           into order
                                            select new RevenueByDate
                                            {
                                                Date = "Week " + order.Key.ToString(),
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }

                    //Group by Months
                    else if (numberDays <= (365 * 2))
                    {
                        bool isYear = numberDays <= 365 ? true : false;
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("MMM yyyy")
                                           into order
                                            select new RevenueByDate
                                            {
                                                Date = isYear ? order.Key.Substring(0, order.Key.IndexOf(" ")) : order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }

                    //Group by Years
                    else
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("yyyy")
                                           into order
                                            select new RevenueByDate
                                            {
                                                Date = order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                }

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandText = @"select circulation.Date, case when count(type)>=1 then 1 end as Total  from Circulation 
                                            inner join Colis on Colis.Id_Colis=Circulation.Colis
                                            where circulation.Type='Entree' and
                                            circulation.Date between @fromDate and @toDate
                                            group by Circulation.Date, Circulation.Type, colis.Designation";

                    cmd.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    cmd.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;
                    var reader = cmd.ExecuteReader();
                    var result = new List<KeyValuePair<DateTime, int>>();
                    while (reader.Read())
                    {
                        result.Add(
                            new KeyValuePair<DateTime, int>((DateTime)reader[0], (int)reader[1])
                            );
                        TotalEntree += (int)reader[1];
                    }
                    TotalProfit = TotalEntree * 0;//20%
                    reader.Close();

                    //Group by Hours
                    if (numberDays <= 1)
                    {
                        GrossEntreeList = (from orderList in result
                                           group orderList by orderList.Key.ToString("hh tt")
                                           into order
                                           select new RevenueByDate
                                           {
                                               DateEntree = order.Key,
                                               TotalEntree = order.Sum(amount => amount.Value)
                                           }).ToList();
                    }
                    //Group by Days
                    else if (numberDays <= 30)
                    {
                        GrossEntreeList = (from orderList in result
                                           group orderList by orderList.Key.ToString("dd MMM")
                                           into order
                                           select new RevenueByDate
                                           {
                                               DateEntree = order.Key,
                                               TotalEntree = order.Sum(amount => amount.Value)
                                           }).ToList();
                    }

                    //Group by Weeks
                    else if (numberDays <= 92)
                    {
                        GrossEntreeList = (from orderList in result
                                           group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                               orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                           into order
                                           select new RevenueByDate
                                           {
                                               DateEntree = "Week " + order.Key.ToString(),
                                               TotalEntree = order.Sum(amount => amount.Value)
                                           }).ToList();
                    }

                    //Group by Months
                    else if (numberDays <= (365 * 2))
                    {
                        bool isYear = numberDays <= 365 ? true : false;
                        GrossEntreeList = (from orderList in result
                                           group orderList by orderList.Key.ToString("MMM yyyy")
                                           into order
                                           select new RevenueByDate
                                           {
                                               DateEntree = isYear ? order.Key.Substring(0, order.Key.IndexOf(" ")) : order.Key,
                                               TotalEntree = order.Sum(amount => amount.Value)
                                           }).ToList();
                    }

                    //Group by Years
                    else
                    {
                        GrossEntreeList = (from orderList in result
                                           group orderList by orderList.Key.ToString("yyyy")
                                           into order
                                           select new RevenueByDate
                                           {
                                               DateEntree = order.Key,
                                               TotalEntree = order.Sum(amount => amount.Value)
                                           }).ToList();
                    }
                }
            }
        }

        private void GetEntreeAnalisys()
        {
            GrossEntreeList = new List<RevenueByDate>();
            TotalProfit = 0;
            TotalEntree = 0;

            using (var connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\JEAN MARIE\Documents\GestionEntrepot.mdf;Integrated Security=True;Connect Timeout=30"))
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"select circulation.Date, case when count(type)>=1 then 1 end as Total from Circulation 
                                            inner join Colis on Colis.Id_Colis=Circulation.Colis
                                            where circulation.Type='Entree' and
                                            circulation.Date between @fromDate and @toDate
                                            group by Circulation.Date, Circulation.Type, colis.Designation";

                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;
                    var reader = command.ExecuteReader();
                    var resultTable = new List<KeyValuePair<DateTime, int>>();
                    while (reader.Read())
                    {
                        resultTable.Add(
                            new KeyValuePair<DateTime, int>((DateTime)reader[0], (int)reader[1])
                            );
                        TotalEntree += (int)reader[1];
                    }
                    TotalProfit = TotalEntree * 0;//20%
                    reader.Close();

                    //Group by Hours
                    if (numberDays <= 1)
                    {
                        GrossEntreeList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("hh tt")
                                           into order
                                            select new RevenueByDate
                                            {
                                                DateEntree = order.Key,
                                                TotalEntree = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                    //Group by Days
                    else if (numberDays <= 30)
                    {
                        GrossEntreeList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("dd MMM")
                                           into order
                                            select new RevenueByDate
                                            {
                                                DateEntree = order.Key,
                                                TotalEntree = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }

                    //Group by Weeks
                    else if (numberDays <= 92)
                    {
                        GrossEntreeList = (from orderList in resultTable
                                            group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                                orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                           into order
                                            select new RevenueByDate
                                            {
                                                DateEntree = "Week " + order.Key.ToString(),
                                                TotalEntree = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }

                    //Group by Months
                    else if (numberDays <= (365 * 2))
                    {
                        bool isYear = numberDays <= 365 ? true : false;
                        GrossEntreeList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("MMM yyyy")
                                           into order
                                            select new RevenueByDate
                                            {
                                                DateEntree = isYear ? order.Key.Substring(0, order.Key.IndexOf(" ")) : order.Key,
                                                TotalEntree = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }

                    //Group by Years
                    else
                    {
                        GrossEntreeList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("yyyy")
                                           into order
                                            select new RevenueByDate
                                            {
                                                DateEntree = order.Key,
                                                TotalEntree = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                }
            }
        }
        public bool LoadData(DateTime startDate, DateTime endDate)
        {
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day,
                endDate.Hour, endDate.Minute, 59);
            if (startDate != this.startDate || endDate != this.endDate)
            {
                this.startDate = startDate;
                this.endDate = endDate;
                this.numberDays = (endDate - startDate).Days;

                GetNumberItems();
                GetProductAnalisys();
                GetOrderAnalisys();
                GetEntreeAnalisys();
                Console.WriteLine("Refreshed data: {0} - {1}", startDate.ToString(), endDate.ToString());
                return true;
            }
            else
            {
                Console.WriteLine("Data not refreshed, same query: {0} - {1}", startDate.ToString(), endDate.ToString());
                return false;
            }
        }
        public override string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override string Database => throw new NotImplementedException();

        public override string DataSource => throw new NotImplementedException();

        public override string ServerVersion => throw new NotImplementedException();

        public override ConnectionState State => throw new NotImplementedException();

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }

        public override void Open()
        {
            throw new NotImplementedException();
        }

        protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        protected override DbCommand CreateDbCommand()
        {
            throw new NotImplementedException();
        }
    }
}
