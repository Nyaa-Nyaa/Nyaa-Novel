using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NyaaNovel
{
    public partial class LoadStory : Form
    {
        public LoadStory()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* 
             * Load a folder containing contents for the story
             * Main file will be NameOfStory.nyaa (my magic extention)
             */

            OpenFileDialog storyFileLocation = new OpenFileDialog();
           
            storyFileLocation.Filter = "Nyaa Story Files|*.nyaa";
            storyFileLocation.Title = "Open Nyaa Story";
            storyFileLocation.ShowDialog();

            NyaaNovel loadedNovel = new NyaaNovel(storyFileLocation.FileName);
            loadedNovel.Show();

            this.Hide();
             
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
