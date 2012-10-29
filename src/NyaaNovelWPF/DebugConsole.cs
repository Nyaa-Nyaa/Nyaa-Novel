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
    public partial class DebugConsole : Form
    {
        public DebugConsole()
        {
            InitializeComponent();
        }

        public void addToConsole(String line)
        {
            ConsoleText.Text = ConsoleText.Text + line + "\n";
        }
    }
}
