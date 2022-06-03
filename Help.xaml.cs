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

namespace graphs
{
    /// <summary>
    /// Логика взаимодействия для Help.xaml
    /// </summary>
    public partial class Help : Window
    {
        public Help()
        {
            InitializeComponent();
        }

        private void Questions_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Прочитай ещё раз)");
        }

        private void OK_Button_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }
    }
}
