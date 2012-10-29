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

namespace NyaaNovelWPF
{
    /// <summary>
    /// Interaction logic for DebugConsole.xaml
    /// </summary>
    public partial class DebugConsole : Window
    {
        public DebugConsole()
        {
            InitializeComponent();
        }

        public void addToConsole(String line)
        {
            ConsoleText.AppendText(line+"\n");
        }

        private void Window_KeyDown_1(object sender, KeyEventArgs e)
        {

        }

        private void ConsoleText_KeyDown(object sender, KeyEventArgs e)
        {

        }

    }
}
