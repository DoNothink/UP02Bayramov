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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        SqlConnection sqlConnection =
            new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
                                Initial Catalog=UP02Bayramov;
                                Integrated Security=True");
        SqlCommand sqlCommand;
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = PasswordPB.Password;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "1234567890";
            string znak = "!@#$%^&*()_+=-№";

            if (string.IsNullOrWhiteSpace(LoginTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели логин");
                LoginTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(PasswordPB.Password))
            {
                MBClass.ErrorMessageBox("Вы не ввели пароль");
                PasswordPB.Focus();
            }
            else if(zagl.IndexOfAny(pass.ToCharArray())==-1)
            {
                MBClass.ErrorMessageBox("Пароль должен содержать прописную букву");
                PasswordPB.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMessageBox("Пароль должен содержать строчную букву");
                PasswordPB.Focus();
            }
            else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMessageBox("Пароль должен содержать цифру");
                PasswordPB.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMessageBox("Пароль должен содержать " +
                    "один из следующих знаков: "+znak);
                PasswordPB.Focus();
            }
            else if(string.IsNullOrWhiteSpace(RepeatPB.Password))
            {
                MBClass.ErrorMessageBox("Вы не ввели повторно пароль");
                RepeatPB.Focus();
            }
            else if(PasswordPB.Password != RepeatPB.Password)
            {
                MBClass.ErrorMessageBox("Пароли не совпадают");
                PasswordPB.Focus();
            }
            else
            {
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert Into dbo.[User] " +
                        "(Email,Password,RoleId) Values " +
                        $"('{LoginTB.Text}','{PasswordPB.Password}',2)",sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMessageBox("Вы успешно зарегистрировались");
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

        private void ExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMessageBox();
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
