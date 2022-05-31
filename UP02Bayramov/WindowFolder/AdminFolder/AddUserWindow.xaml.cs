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
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window
    {
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
                                Initial Catalog=UP02Bayramov;
                                Integrated Security=True");
        SqlCommand sqlCommand;
        CBClass cBClass;
        public AddUserWindow()
        {
            InitializeComponent();
            cBClass = new CBClass();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cBClass.RoleCBLoad(RoleCb);
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            string pass = passPB.Password;
            string zagl = "QWERTYUIOPASDFGHJKLZXCVBNM";
            string mal = "qwertyuiopasdfghjklzxcvbnm";
            string cif = "1234567890";
            string znak = "!@#$%^&*()_+=-№";

            if (string.IsNullOrWhiteSpace(EmailTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели логин");
                EmailTB.Focus();
            }
            else if(string.IsNullOrWhiteSpace(nameTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели имя");
                nameTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(lastnameTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели фамилию");
                lastnameTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(passPB.Password))
            {
                MBClass.ErrorMessageBox("Вы не ввели пароль");
                passPB.Focus();
            }
            else if (zagl.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMessageBox("Пароль должен содержать прописную букву");
                passPB.Focus();
            }
            else if (mal.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMessageBox("Пароль должен содержать строчную букву");
                passPB.Focus();
            }
            else if (cif.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMessageBox("Пароль должен содержать цифру");
                passPB.Focus();
            }
            else if (znak.IndexOfAny(pass.ToCharArray()) == -1)
            {
                MBClass.ErrorMessageBox("Пароль должен содержать " +
                    "один из следующих знаков: " + znak);
                passPB.Focus();
            }
            else if(RoleCb.SelectedIndex == -1)
            {
                MBClass.ErrorMessageBox("Выберите роль");
                RoleCb.Focus();
            }
            else
            { 
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert into dbo.[User] " +
                        "(Email, Password, FirstName, LastName, RoleId) " +
                        $"Values ('{EmailTB.Text}' , " +
                        $"'{passPB.Password}', " +
                        $"'{nameTB.Text}', " +
                        $"'{lastnameTB.Text}', " +
                        $"'{RoleCb.SelectedValue.ToString()}')",sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMessageBox($"Пользователь {lastnameTB.Text} " +
                        $"{nameTB.Text} успешно добавлен");
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

        private void addBackBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addExitBtn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMessageBox();
        }
    }
}
