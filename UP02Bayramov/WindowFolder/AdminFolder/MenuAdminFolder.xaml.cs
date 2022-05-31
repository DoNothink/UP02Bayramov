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

namespace UP02Bayramov.WindowFolder.AdminFolder
{
    /// <summary>
    /// Логика взаимодействия для MenuAdminFolder.xaml
    /// </summary>
    public partial class MenuAdminFolder : Window
    {
        DGClass DGClass;
        public MenuAdminFolder()
        {
            InitializeComponent();
            DGClass = new DGClass(ListUserDG);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DGClass.LoadDG("Select * From dbo.[User]");
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            DGClass.LoadDG("Select * From dbo.[User] " +
                $"Where LastName Like '%{SearchTb.Text}%' " +
                $"OR Email Like '%{SearchTb.Text}%'");
        }

        private void AddIn_Click(object sender, RoutedEventArgs e)
        {
            new AddUserWindow().Show();
            DGClass.LoadDG("Select * From dbo.[User]");
        }

        private void BackIn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ExitIn_Click(object sender, RoutedEventArgs e)
        {
            MBClass.ExitMessageBox();
        }

        private void ListUserDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(ListUserDG.SelectedItem == null)
            {
                MBClass.ErrorMessageBox("Вы не выбрали строку");
            }
            else
            {
                try
                {
                    VariableClass.UserId = DGClass.SelectId();
                    new EditUserWindow().ShowDialog();
                    DGClass.LoadDG("Select * From dbo.[User]");
                }
                catch (Exception ex)
                {
                    MBClass.ErrorMessageBox(ex);
                }
            }
        }
    }
}
