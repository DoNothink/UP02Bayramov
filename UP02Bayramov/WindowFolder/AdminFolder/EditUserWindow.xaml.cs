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

namespace UP02Bayramov.WindowFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для EditUserWindow.xaml
    /// </summary>
    public partial class EditUserWindow : Window
    {
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
                                Initial Catalog=UP02Bayramov;
                                Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        CBClass cBClass;
        public EditUserWindow()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void addBackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMessageBox();
        }

        private void EditUserBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                sqlConnection.Open();
                sqlCommand =
                    new SqlCommand("Update " +
                    "dbo.[User] " +
                    $"Set [Email] = '{EmailTB.Text}', " +
                    $"[Password] = '{passPB.Password}', " +
                    $"FirstName = '{nameTB.Text}', " +
                    $"LastName = '{lastnameTB.Text}', " +
                    $"RoleId = '{RoleCb.SelectedValue.ToString()}' " +
                    $"Where UserId = '{VariableClass.UserId}'",sqlConnection);
                sqlCommand.ExecuteNonQuery();
                MBClass.InfoMessageBox($"Данные пользователя " +
                    $"{lastnameTB.Text} {nameTB.Text} " +
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.RoleCBLoad(RoleCb);
            try
            {
                sqlConnection.Open();
                sqlCommand =
                    new SqlCommand("Select * From dbo.[User] " +
                    $"Where UserId= '{VariableClass.UserId}'",sqlConnection);
                dataReader = sqlCommand.ExecuteReader();
                dataReader.Read();
                EmailTB.Text = dataReader[1].ToString();
                passPB.Password = dataReader[2].ToString();
                nameTB.Text = dataReader[3].ToString();
                lastnameTB.Text = dataReader[4].ToString();
                RoleCb.SelectedValue = dataReader[5].ToString();

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
