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
    public partial class Citas : Form
    {
        String IntCitas;

        public Citas(String cites)
        {
            InitializeComponent();
            IntCitas = cites;
        }

        private void Citas_Load(object sender, EventArgs e)
        {
            textBox1.Text = IntCitas;
        }
    }
}
