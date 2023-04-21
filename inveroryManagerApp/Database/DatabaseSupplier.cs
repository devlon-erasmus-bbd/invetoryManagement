using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public partial class DatabaseConnection
{
    public List<SupplierModel> GetListOfSupplier()
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT s.supplier_id, s.supplier_name, s.supplier_contact_number, s.created_date " +
            "FROM [invetory_manager].[dbo].[Supplier] as s";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        List<SupplierModel> supplierModels = new List<SupplierModel>();
        CultureInfo culture = new CultureInfo("en-US");

        while (dataReader.Read())
        {
            SupplierModel supplier = new SupplierModel()
            {
                SupplierId = (int)dataReader.GetValue(0),
                SupplierName = dataReader.GetValue(1).ToString(),
                SupplierContactNumber = dataReader.GetValue(2).ToString(),
                DateAdded = Convert.ToDateTime(dataReader.GetValue(3).ToString(), culture)
            };

            supplierModels.Add(supplier);
        }

        CloseConnectionToDatabase(conn);

        return supplierModels;
    }

    private SupplierModel GetSupplierBySupplierId(int supplier_id)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT s.supplier_id, s.supplier_name, s.supplier_contact_number, s.created_date " +
            "FROM [invetory_manager].[dbo].[Supplier] as s " +
            "WHERE s.supplier_id = " + supplier_id;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        SupplierModel supplier = new SupplierModel();

        while (dataReader.Read())
        {
            supplier.SupplierId = (int)dataReader.GetValue(0);
            supplier.SupplierName = dataReader.GetValue(1).ToString();
            supplier.SupplierContactNumber = dataReader.GetValue(2).ToString();
            supplier.DateAdded = Convert.ToDateTime(dataReader.GetValue(3).ToString(), culture);
        }

        CloseConnectionToDatabase(conn);
        return supplier;
    }

    public void AddSupplier(SupplierModel supplier)
    {
        if (supplier.SupplierName == null)
            return;

        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[Supplier] (supplier_name, supplier_contact_number, created_date)" +
            "VALUES ('" + supplier.SupplierName + "', '" + supplier.SupplierContactNumber + "', GETDATE()); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
    }
}