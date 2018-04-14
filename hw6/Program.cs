using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hw6
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["automate"];
            DbProviderFactory factory = DbProviderFactories.GetFactory(connectionString.ProviderName);

            DbConnection connection = factory.CreateConnection();
            connection.ConnectionString = connectionString.ConnectionString;

            //создаем лок БД
            DataSet shopDB = new DataSet("Shop");

            //---------------------------------------------------------------------
            //создаем табл "Customers"
            DataTable tableCustomers = new DataTable("Customers");

            //столбцы
            DataColumn idColumnCustomers = new DataColumn();
            idColumnCustomers.ColumnName = "customers_id";
            idColumnCustomers.DataType = typeof(int);
            idColumnCustomers.AllowDBNull = false;
            idColumnCustomers.AutoIncrement = true;
            idColumnCustomers.Unique = true;

            DataColumn nameColumn = new DataColumn("name", typeof(string));

            //доб столбец в табл
            tableCustomers.Columns.AddRange(new DataColumn[] { idColumnCustomers, nameColumn });

            //строки
            DataRow rowCustomers = tableCustomers.NewRow();
            //row1["id"] = 1; //можно не встаялять ибо автоинкр
            rowCustomers["name"] = "Василий";

            //доб строку в табл
            tableCustomers.Rows.Add(rowCustomers);

            //добавляем табл в лок БД
            shopDB.Tables.Add(tableCustomers);

            //---------------------------------------------------------------------
            //создаем табл "Employees"
            DataTable tableEmployees = new DataTable("Employees");

            //столбцы
            DataColumn idColumnEmployees = new DataColumn();
            idColumnEmployees.ColumnName = "Employees_id";
            idColumnEmployees.DataType = typeof(int);
            idColumnEmployees.AllowDBNull = false;
            idColumnEmployees.AutoIncrement = true;
            idColumnEmployees.Unique = true;

            DataColumn nameColumnEmployee = new DataColumn("name", typeof(string));

            //доб столбец в табл
            tableEmployees.Columns.AddRange(new DataColumn[] { idColumnEmployees, nameColumnEmployee });

            //строки
            DataRow rowEmployees = tableCustomers.NewRow();
            //row1["id"] = 1; //можно не встаялять ибо автоинкр
            rowEmployees["name"] = "Геннадий";

            //доб строку в табл
            tableEmployees.Rows.Add(rowEmployees);

            //добавляем табл в лок БД
            shopDB.Tables.Add(tableEmployees);

            //---------------------------------------------------------------------
            //создаем табл "Products"
            DataTable tableProducts = new DataTable("Products");

            //столбцы
            DataColumn idColumnProducts = new DataColumn();
            idColumnProducts.ColumnName = "Product_id";
            idColumnProducts.DataType = typeof(int);
            idColumnProducts.AllowDBNull = false;
            idColumnProducts.AutoIncrement = true;
            idColumnProducts.Unique = true;

            DataColumn nameColumnProduct = new DataColumn("name", typeof(string));

            //доб столбец в табл
            tableProducts.Columns.AddRange(new DataColumn[] { idColumnProducts, nameColumnProduct });

            //строки
            DataRow rowProducts = tableProducts.NewRow();
            //row1["id"] = 1; //можно не встаялять ибо автоинкр
            rowProducts["name"] = "телевизор";

            //доб строку в табл
            tableProducts.Rows.Add(rowProducts);

            //добавляем табл в лок БД
            shopDB.Tables.Add(tableProducts);

            //---------------------------------------------------------------------
            //создаем табл "Orders"
            DataTable tableOrders = new DataTable("Orders");

            //столбцы
            DataColumn idColumnOrders = new DataColumn();
            idColumnOrders.ColumnName = "order_id";
            idColumnOrders.DataType = typeof(int);
            idColumnOrders.AllowDBNull = false;
            idColumnOrders.AutoIncrement = true;
            idColumnOrders.Unique = true;

            DataColumn employeeIdColumn = new DataColumn();
            employeeIdColumn.ColumnName = "employee_id";
            employeeIdColumn.DataType = typeof(int);
            employeeIdColumn.AllowDBNull = false;

            DataColumn customersIdColumn = new DataColumn();
            customersIdColumn.ColumnName = "customers_id";
            customersIdColumn.DataType = typeof(int);
            customersIdColumn.AllowDBNull = false;

            DataColumn productIdColumn = new DataColumn();
            productIdColumn.ColumnName = "product_id";
            productIdColumn.DataType = typeof(int);
            productIdColumn.AllowDBNull = false;

            shopDB.Relations.Add(new DataRelation("OrdersEmployee", tableEmployees.Columns["Employees_id"], tableOrders.Columns["employee_id"]));
            shopDB.Relations.Add(new DataRelation("OrdersCustomers", tableCustomers.Columns["customers_id"], tableOrders.Columns["customers_id"]));
            shopDB.Relations.Add(new DataRelation("OrdersProducts", tableProducts.Columns["Product_id"], tableOrders.Columns["product_id"]));

            //доб столбец в табл
            tableOrders.Columns.AddRange(new DataColumn[] { idColumnOrders, employeeIdColumn, customersIdColumn, productIdColumn });

            //строки
            DataRow rowOrders = tableOrders.NewRow();
            //row1["id"] = 1; //можно не встаялять ибо автоинкр
            rowOrders["employee_id"] = 1;
            rowOrders["customers_id"] = 1;
            rowOrders["product_id"] = 1;

            //доб строку в табл
            tableOrders.Rows.Add(rowOrders);

            //добавляем табл в лок БД
            shopDB.Tables.Add(tableOrders);

            shopDB.AcceptChanges();
        }
    }
}
