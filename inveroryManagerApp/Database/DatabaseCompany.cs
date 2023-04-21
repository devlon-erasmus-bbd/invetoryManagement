using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public partial class DatabaseConnection
{
    public List<CompanyModel> GetListOfCompany()
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT c.company_id, c.company_name, c.company_description, c.created_date " +
            "FROM [invetory_manager].[dbo].[Company] as c";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        List<CompanyModel> companyModels = new List<CompanyModel>();
        CultureInfo culture = new CultureInfo("en-US");

        while (dataReader.Read())
        {
            CompanyModel company = new CompanyModel()
            {
                CompanyId = (int)dataReader.GetValue(0),
                CompanyName = dataReader.GetValue(1).ToString(),
                CompanyDescription = dataReader.GetValue(2).ToString(),
                CreatedDate = Convert.ToDateTime(dataReader.GetValue(3).ToString(), culture)
            };

            companyModels.Add(company);
        }

        CloseConnectionToDatabase(conn);

        return companyModels;
    }

    private CompanyModel GetCompanyByCompanyId(int company_id)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT c.company_id, c.company_name, c.company_description, c.created_date " +
            "FROM [invetory_manager].[dbo].[Company] as c " +
            "WHERE c.company_id = " + company_id;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        CompanyModel company = new CompanyModel();

        while (dataReader.Read())
        {
            company.CompanyId = (int)dataReader.GetValue(0);
            company.CompanyName = dataReader.GetValue(1).ToString();
            company.CompanyDescription = dataReader.GetValue(2).ToString();
            company.CreatedDate = Convert.ToDateTime(dataReader.GetValue(3).ToString(), culture);
        }

        CloseConnectionToDatabase(conn);
        return company;
    }

    public void AddCompany(CompanyModel company)
    {
        if (company.CompanyName == null)
            return;

        SqlConnection conn = ConnectToDatabase();

        String sql = "INSERT INTO [invetory_manager].[dbo].[Company] (company_name, company_description, created_date)" +
            "VALUES ('" + company.CompanyName + "', '" + company.CompanyDescription + "', GETDATE()); SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedID = Convert.ToInt32(command.ExecuteScalar());

        CloseConnectionToDatabase(conn);
    }
}