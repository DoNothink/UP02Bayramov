using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UP02Bayramov.ClassFolder
{
    class MBClass
    {
        public static void InfoMessageBox(string text)
        {
            MessageBox.Show(text, "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ErrorMessageBox(Exception text)
        {
            MessageBox.Show(text.Message, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void ErrorMessageBox(string text)
        {
            MessageBox.Show(text, "Ошибка",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static bool QuestionMessageBox(string text)
        {
            return MessageBoxResult.Yes == MessageBox.Show(text,
                "Вопрос", MessageBoxButton.YesNo,
                MessageBoxImage.Question);
        }

        public static void ExitMessageBox()
        {
            bool resultMB = QuestionMessageBox("Вы действительно " +
                "желаете выйти?");
            if (resultMB == true)
            {
                App.Current.Shutdown();
            }
        }
    }
}
