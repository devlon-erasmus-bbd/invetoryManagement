using System.Data.SqlClient;
using System.Globalization;

namespace inveroryManagerApp.Models;
public partial class DatabaseConnection
{
    public List<TransactionModel> GetTransactions()
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT t.transaction_id, t.fk_customer_id, t.fk_staff_id, t.transaction_total, t.transaction_date " +
            "FROM [dbo].[Transaction] as t";
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        List<TransactionModel> transactions = new List<TransactionModel>();

        while (dataReader.Read())
        {
            TransactionModel transaction = new TransactionModel()
            {
                TransactionId = (int)dataReader.GetValue(0),
                Customer = GetCustomerByCustomerId((int)dataReader.GetValue(1)),
                Staff = GetStaffByStaffId((int)dataReader.GetValue(2)),
                TransactionTotal = (decimal)dataReader.GetValue(3),
                TransactionDate = Convert.ToDateTime(dataReader.GetValue(4).ToString(), culture)
            };

            transactions.Add(transaction);
        }

        CloseConnectionToDatabase(conn);
        return transactions;
    }

    public TransactionModel GetTransactionByTransactionId(int transactionId)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT t.transaction_id, t.fk_customer_id, t.fk_staff_id, t.transaction_total, t.transaction_date " +
            "FROM [dbo].[Transaction] as t " +
            "WHERE t.transaction_id = " + transactionId;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        TransactionModel transaction = new TransactionModel();

        while (dataReader.Read())
        {
            transaction.TransactionId = (int)dataReader.GetValue(0);
            transaction.Customer = GetCustomerByCustomerId((int)dataReader.GetValue(1));
            transaction.Staff = GetStaffByStaffId((int)dataReader.GetValue(2));
            transaction.TransactionTotal = (decimal)dataReader.GetValue(3);
            transaction.TransactionDate = Convert.ToDateTime(dataReader.GetValue(4).ToString(), culture);
        }

        CloseConnectionToDatabase(conn);
        return transaction;
    }

    public List<TransactionDetailsModel> GetTransactionDetailsByTransactionId(int transaction_id)
    {
        SqlConnection conn = ConnectToDatabase();

        String sql = "SELECT t.transaction_id, t.fk_customer_id, t.fk_staff_id, t.transaction_total, t.transaction_date, " +
            "td.transaction_details_id, td.fk_item_id, td.fk_transaction_id, td.quantity " +
            "FROM [dbo].[Transaction] as t " +
            "INNER JOIN [dbo].[TransactionDetails] as td " +
            "ON t.transaction_id = td.fk_transaction_id " +
            "WHERE t.transaction_id = " + transaction_id;
        SqlCommand command = new SqlCommand(sql, conn);
        SqlDataReader dataReader = command.ExecuteReader();
        CultureInfo culture = new CultureInfo("en-US");

        List<TransactionDetailsModel> transactionDetails = new List<TransactionDetailsModel>();

        while (dataReader.Read())
        {
            TransactionModel transaction = new TransactionModel()
            {
                TransactionId = (int)dataReader.GetValue(0),
                Customer = GetCustomerByCustomerId((int)dataReader.GetValue(1)),
                Staff = GetStaffByStaffId((int)dataReader.GetValue(2)),
                TransactionTotal = (decimal)dataReader.GetValue(3),
                TransactionDate = Convert.ToDateTime(dataReader.GetValue(4).ToString(), culture)
            };

            TransactionDetailsModel transactionDetailsModel = new TransactionDetailsModel()
            {
                TransactionDetailsId = (int)dataReader.GetValue(5),
                Item = GetItemByItemId((int)dataReader.GetValue(6)),
                Transaction = transaction,
                Quantity = (int)dataReader.GetValue(8)
            };

            transactionDetails.Add(transactionDetailsModel);
        }

        CloseConnectionToDatabase(conn);
        return transactionDetails;
    }

    public void AddTransaction(BuyItemModel buyItemModel)
    {
        if (buyItemModel.listStaff == 0 || buyItemModel.listCustomer == 0 || buyItemModel.SelectedItems.Count == 0)
            return;

        SqlConnection conn = ConnectToDatabase();

        decimal transactionTotal = 0;
        foreach (string i in buyItemModel.SelectedItems)
        {
            transactionTotal += GetItemByItemId(int.Parse(i)).SellPrice;
        }

        String sql = "INSERT INTO [invetory_manager].[dbo].[Transaction] (fk_customer_id, fk_staff_id, transaction_total, transaction_date) " +
            "VALUES (" + buyItemModel.listCustomer + ", " + buyItemModel.listStaff + ", " + transactionTotal.ToString().Replace(',', '.') + ", GETDATE()); " +
            "SELECT SCOPE_IDENTITY()";
        SqlCommand command = new SqlCommand(sql, conn);
        int insertedTransactionId = Convert.ToInt32(command.ExecuteScalar());

        foreach (string i in buyItemModel.SelectedItems)
        {
            sql = "INSERT INTO [invetory_manager].[dbo].[TransactionDetails] (fk_item_id, fk_transaction_id, quantity) " +
            "VALUES (" + i + ", " + insertedTransactionId + ", 1); SELECT SCOPE_IDENTITY()";
            command = new SqlCommand(sql, conn);
            command.ExecuteScalar();
        }

        CloseConnectionToDatabase(conn);
    }

}