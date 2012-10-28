using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InstructionEditor
{
    public partial class InputBox : Form
    {
        public InputBox()
        {
            InitializeComponent();
        }

        public static String Show( String Caption, String Title, String DefaultValue = "")
        {
            InputBox instance = new InputBox();
            instance.Text = Title;
            instance.label.Text = Caption;
            instance.textBox.Text = DefaultValue;
            instance.AcceptButton = instance.button;

            instance.ShowDialog();

            return instance.textBox.Text;
        }

        private void button_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
