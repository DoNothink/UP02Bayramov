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
    /// Логика взаимодействия для AddCustmerWindow.xaml
    /// </summary>
    public partial class AddCustmerWindow : Window
    {
        SqlConnection sqlConnection =
           new SqlConnection(@"Data Source=K218PC\SQLEXPRESS;
                                Initial Catalog=UP02Bayramov;
                                Integrated Security=True");
        SqlCommand sqlCommand;
        public AddCustmerWindow()
        {
            InitializeComponent();
        }
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(lastnameTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели фамилию");
                lastnameTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(firstnameTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели имя");
                firstnameTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(middlenameTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели отчество");
                middlenameTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(phonenumberTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели номер телефона");
                phonenumberTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(emailTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели электронную почту");
                emailTB.Focus();
            }
            else if (string.IsNullOrWhiteSpace(dateobTB.Text))
            {
                MBClass.ErrorMessageBox("Вы не ввели дату рождения");
                dateobTB.Focus();
            }
            else
            { 
                try
                {
                    sqlConnection.Open();
                    sqlCommand = new SqlCommand("Insert into dbo.[Customer] " +
                        "(LastNameCustomer, FirstNameCustomer, MiddleNameCustomer, NumberPhoneCustomer, EmailCustomer, DateOfBirthCustomer) " +
                        $"Values ('{lastnameTB.Text}', " +
                        $"'{firstnameTB.Text}', " +
                        $"'{middlenameTB.Text}', " +
                        $"'{phonenumberTB.Text}', " +
                        $"'{emailTB.Text}', " +
                        $"'{dateobTB.Text}')", sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    MBClass.InfoMessageBox($"Заказчик {lastnameTB.Text} " +
                        $"{firstnameTB.Text} успешно добавлен");
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
