using System;
using System.Collections.Generic;
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
using UP02Bayramov.WindowFolder.AdminFolder;

namespace UP02Bayramov.WindowFolder.UserFolder
{
    /// <summary>
    /// Логика взаимодействия для MenuUserWindow.xaml
    /// </summary>
    public partial class MenuUserWindow : Window
    {
        DGClass dGClass;
        public MenuUserWindow()
        {
            InitializeComponent();
            dGClass = new DGClass(ListCustomerDG);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[Customer]");
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            dGClass.LoadDG("Select * From dbo.[Customer] " +
                $"Where LastNameCustomer Like '%{SearchTb.Text}%' " +
                $"OR FirstNameCustomer Like '%{SearchTb.Text}%'");
        }

        private void BackIn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ExitIn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMessageBox();
        }

        private void AddIn_Click(object sender, RoutedEventArgs e)
        {
            new AddCustmerWindow().Show();
            dGClass.LoadDG("Select * From dbo.[Customer]");
        }

        private void ListCustomerDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListCustomerDG.SelectedItem == null)
            {
                MBClass.ErrorMessageBox("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VariableClass.CustomerId = dGClass.SelectId();
                    new EditCustomerWindow().ShowDialog();
                    dGClass.LoadDG("Select * From dbo.[Customer]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMessageBox(ex);
                }
            }
        }
    }
}
