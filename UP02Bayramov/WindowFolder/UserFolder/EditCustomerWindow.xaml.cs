using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using UP02Bayramov.ClassFolder;

namespace UP02Bayramov.WindowFolder.UserFolder
{
    /// <summary>
    /// Логика взаимодействия для EditCustomerWindow.xaml
    /// </summary>
    public partial class EditCustomerWindow : Window
    {
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
                                Initial Catalog=UP02Bayramov;
                                Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public EditCustomerWindow()
        {
            InitializeComponent();
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[Customer] " +
                    $"Set LastNameCustomer = '{lastnameTB.Text}', " +
                    $"FirstNameCustomer = '{firstnameTB.Text}', " +
                    $"MiddleNameCustomer = '{middlenameTB.Text}', " +
                    $"NumberPhoneCustomer = '{phonenumberTB.Text}', " +
                    $"EmailCustomer = '{emailTB.Text}', " +
                    $"DateOfBirthCustomer = '{dateobTB.Text}'" +
                    $"Where IdCustomer = '{VariableClass.CustomerId}'", sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMessageBox($"Данные пользователя " +
                    $"{lastnameTB.Text} {firstnameTB.Text} " +
                    $"Успешно отредактированы");
            }
            catch (Exception ex)
            {
                MBClass.ErrorMessageBox(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void EditBackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EditExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMessageBox();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("Select * From dbo.[Customer] " +
                    $"Where IdCustomer = '{VariableClass.CustomerId}'",
                    sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                lastnameTB.Text = dataReader[1].ToString();
                firstnameTB.Text = dataReader[2].ToString();
                middlenameTB.Text = dataReader[3].ToString();
                phonenumberTB.Text = dataReader[4].ToString();
                emailTB.Text = dataReader[5].ToString();
                dateobTB.Text = dataReader[6].ToString();
            }
            catch (Exception ex)
            {
                MBClass.ErrorMessageBox(ex);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
