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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace NyaaNovelWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            /* 
            * Load a folder containing contents for the story
            * Main file will be NameOfStory.nyaa (my magic extention)
            */

            OpenFileDialog storyFileLocation = new OpenFileDialog();

            storyFileLocation.Filter = "Nyaa Story Files|*.nyaa";
            storyFileLocation.Title = "Open Nyaa Story";
            storyFileLocation.ShowDialog();

            if (storyFileLocation.FileName.CompareTo("") != 0)
            {
                NyaaNovel loadedNovel = new NyaaNovel(storyFileLocation.FileName, DebugCheckBox.IsChecked);
                this.Hide();
            }
        }
    }
}
