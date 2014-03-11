using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using PluginInterface;
namespace UI
{
    public partial class MainForm : Form
    {
    
        public MainForm()
        {
            InitializeComponent();
            DisplayPlugins();
        }

        private void DisplayPlugins() {
            this.listBox1.DataSource = Program.EulerPlugins;
            this.listBox1.DisplayMember = "Name";
            
        }

        private void btnExecutePlugin_Click(object sender, EventArgs e)
        {
            TimeSpan ts;
            DateTime dtStart, dtEnd;
            IEulerPluginContext context = new EulerPluginContext();
            IEulerPlugin plugin =(IEulerPlugin)listBox1.SelectedValue;
            if (plugin.ImplementsGetInput) {
                plugin.PerformGetInput(context);
            }
            dtStart = DateTime.Now;
            context = plugin.PerformAction(context);
            dtEnd = DateTime.Now;
            ts = dtEnd.Subtract(dtStart);
            textBox1.AppendText(String.Format("{0}{1}",context.strResultLongText , System.Environment.NewLine));
            textBox1.AppendText(string.Format("Results Calculated in: {0} ms{1}",ts.TotalMilliseconds,Environment.NewLine));
         
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedItem != null) {
                btnExecutePlugin.Enabled = true;
            }
        }

        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    
    }
}
