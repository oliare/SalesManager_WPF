using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SalesManager_WPF
{
    public partial class MainWindow : Window
    {
        private SqlConnection connection;
        private SqlDataAdapter adapter;
        private DataSet set;
        private SqlCommandBuilder cmd;
        private string _table;

        public MainWindow()
        {
            InitializeComponent();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings["connShop"].ConnectionString);
            FillUpComboBox();
        }

        private void FillUpComboBox()
        {
            List<string> tablesList = GetTableNames();
            foreach (string table in tablesList)
            {
                tableComboBox.Items.Add(table);
            }
        }

        private List<string> GetTableNames()
        {
            List<string> tables = new List<string>();

            connection.Open();
            string query = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";
            SqlCommand command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tables.Add(reader.GetString(0));
            }
            reader.Close();
            connection.Close();
            return tables;
        }

        private void Button_Fill(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_table))
            {
                MessageBox.Show("no table is selected");
                return;
            }
            string sql = $"select * from {_table}";
            adapter = new SqlDataAdapter(sql, connection);
            cmd = new SqlCommandBuilder(adapter);
            set = new DataSet();
            adapter.Fill(set, _table);
            dataGrid.ItemsSource = set.Tables[_table].DefaultView;
        }

        private void Button_Update(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_table))
            {
                MessageBox.Show("no table is selected");
                return;
            }
            adapter.Update(set, _table);
        }
        private void Button_Delete(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(_table))
            {
                MessageBox.Show("no item is selected");
                return;
            }
            DataRowView selectedRow = (DataRowView)dataGrid.SelectedItem;
            selectedRow.Delete();
        }

        private void Table_Check(object sender, SelectionChangedEventArgs e)
        {
            if (tableComboBox.SelectedItem != null)
                _table = tableComboBox.SelectedItem.ToString();
            else
                MessageBox.Show("error...");
        }
    }
}

