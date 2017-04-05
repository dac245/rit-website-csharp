using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISTEProj3
{
    public partial class WebMap : Form
    {
        public WebMap()
        {
            InitializeComponent();
            webBrowser1.Navigate("http://ist.rit.edu/api/map");
            //System.Diagnostics.Process.Start("http://ist.rit.edu/api/map");
        }
    }
}
