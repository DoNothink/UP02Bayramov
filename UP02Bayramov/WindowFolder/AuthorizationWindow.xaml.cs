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


namespace UP02Bayramov.WindowFolder
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
                                Initial Catalog=UP02Bayramov;
                                Integrated Security=True");
        SqlCommand sqlCommand;
        SqlDataReader dataReader;
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(LoginTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели логин");
                LoginTB.Focus();
            }
            else if(string.IsNullOrWhiteSpace(PasswordPB.Password))
            {
                MBClass.ErrorMessageBox("Вы не ввели пароль");
                PasswordPB.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Select * FROM " +
                        "dbo.[User] " +
                        $"Where Email = '{LoginTB.Text}'",
                        sqlConnection);
                    dataReader = sqlCommand.ExecuteReader();
                    dataReader.Read();

                    if(dataReader[2].ToString() != PasswordPB.Password)
                    {
                        MBClass.ErrorMessageBox("Вы ввели не верный пароль");
                        PasswordPB.Focus();
                    }
                    else
                    {
                        switch(dataReader[5].ToString())
                        {
                            case "1":
                                new AdminFolder.MenuAdminFolder().ShowDialog();
                                break;
                            case "2":
                                new UserFolder.MenuUserWindow().ShowDialog();
                                break;
                            case "3":
                                break;
                        }
                    }
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

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationWindow().Show();
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMessageBox();
        }
    }
}
