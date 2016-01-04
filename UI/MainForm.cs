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
        private void DisplayPlugins()
        {
            this.dataGridView1.ColumnHeadersVisible = false;
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.DataSource = Program.EulerPlugins.OrderByDescending(plugin => plugin.ID).ToList();
            
        }

        private async void PerformActionAsync(IEulerPlugin plugin,  IEulerPluginContext context) {

            Task<IEulerPluginContext> tContext = plugin.PerformActionAsync(context);
            context = await tContext;
            textBox1.AppendText(string.Format("{0}{1}", plugin.Name, Environment.NewLine));
            textBox1.AppendText(String.Format("  {0}{1}", context.strResultLongText, System.Environment.NewLine));
            textBox1.AppendText(string.Format("  Results Calculated in: {0} ms{1}", context.spnDuration.TotalMilliseconds, Environment.NewLine));
        }

        private  void btnExecutePlugin_Click(object sender, EventArgs e)
        {
 
            DateTime dtStart, dtEnd;
            IEulerPluginContext context = new EulerPluginContext();
            IEulerPlugin plugin = (IEulerPlugin)dataGridView1.SelectedRows[0].DataBoundItem;
            if (plugin.ImplementsGetInput)
            {
                plugin.PerformGetInput(context);
            }
            if (plugin.IsAsync) {
                PerformActionAsync(plugin, context);
            }
            else
            {
                dtStart = DateTime.Now;
                context = plugin.PerformAction(context);
                dtEnd = DateTime.Now;
                context.spnDuration = dtEnd.Subtract(dtStart);
                textBox1.AppendText(string.Format("{0}{1}", plugin.Name, Environment.NewLine));
                textBox1.AppendText(string.Format("  {0}{1}", context.strResultLongText, Environment.NewLine));
                textBox1.AppendText(string.Format("  Results Calculated in: {0} ms{1}", context.spnDuration.TotalMilliseconds, Environment.NewLine));
            }
           
        }

        private void btnClearConsole_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            btnExecutePlugin.Enabled = true;
            if (dgv.SelectedRows.Count > 0)
            {
                IEulerPlugin plugin = (IEulerPlugin)dgv.SelectedRows[0].DataBoundItem;
                this.textBox2.Text = plugin.Description;
            }
        }

        private void dataGridView1_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            DataGridViewColumn dgvc = e.Column;
            switch (e.Column.Name.ToUpper())
            {
                case "ID":
                    dgvc.SortMode = DataGridViewColumnSortMode.Automatic;
                    dgvc.Visible = false;
                    dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                    break;
                case "NAME":
                    dgvc.Visible = true;
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCellsExceptHeader;
                    break;
                default:
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                    dgvc.Visible = false;
                    break;
            }
        }

     
    }
}
