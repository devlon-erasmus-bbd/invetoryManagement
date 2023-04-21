using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public partial class DatabaseConnection
{
    public List<StaffModel> GetListOfStaffs()
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT s.staff_id, s.staff_name " +
            "FROM [invetory_manager].[dbo].[Staff] as s";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        List<StaffModel> staffModels = new List<StaffModel>();
        CultureInfo culture = new CultureInfo("en-US");

        while (dataReader.Read())
        {
            StaffModel staff = new StaffModel()
            {
                StaffId = (int)dataReader.GetValue(0),
                StaffName = dataReader.GetValue(1).ToString(),
            };

            staffModels.Add(staff);
        }

        CloseConnectionToDatabase(conn);

        return staffModels;
    }

    public StaffModel GetStaffByStaffId(int staff_id)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT st.staff_id, st.staff_name " +
            "FROM [invetory_manager].[dbo].[Staff] as st " +
            "WHERE st.staff_id = " + staff_id;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        StaffModel staff = new StaffModel();

        while (dataReader.Read())
        {
            staff.StaffId = (int)dataReader.GetValue(0);
            staff.StaffName = dataReader.GetValue(1).ToString();
        }

        CloseConnectionToDatabase(conn);
        return staff;
    }

    public void AddStaff(StaffModel staff)
    {
        if (staff.StaffName == null)
            return;

        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[Staff] (staff_name)" +
            "VALUES ('" + staff.StaffName + "'); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
    }

    public void EditStaff(StaffModel staff)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "UPDATE [invetory_manager].[dbo].[Staff] " +
            "SET staff_name = '" + staff.StaffName + "' " +
            "WHERE staff_id = " + staff.StaffId;
        SqlCommand command = new SqlCommand(sql, conn);
        command.ExecuteNonQuery();

        CloseConnectionToDatabase(conn);
    }
}