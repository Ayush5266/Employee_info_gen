using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EF_Employee_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //.Model.My_staffEntities dc = new Model.My_staffEntities();
        EF_Employee_App.Model.My_staffEntities dc;

        private void button1_Click(object sender, EventArgs e)
        {
            dc.Dispose();
            dc = new Model.My_staffEntities(); //if we comment this then we should dispose dc and again initialize the instance we do this in reload button 
            //------
            dc.Emp_Info.Load();
            this.emp_InfoBindingSource.DataSource = dc.Emp_Info.Local.ToBindingList();
            //-------------
            this.label1.Text="of "+"{"+this.emp_InfoBindingSource.Count.ToString()+"}";
            record_pos();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            dc.Dispose(); //to save memory we should destroy the db context before closing the app.
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.emp_InfoBindingSource.EndEdit();
            int r = dc.SaveChanges();
            if (r>0)
            {
                this.emp_InfoDataGridView.Refresh();
                MessageBox.Show("Data Saved!!! Count:"+r.ToString());

            }
            else
            {
                MessageBox.Show("Not Saved!! Try Again");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.emp_InfoBindingSource.MoveFirst();
            record_pos();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.emp_InfoBindingSource.MovePrevious();
            record_pos();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.emp_InfoBindingSource.MoveNext();
            record_pos();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.emp_InfoBindingSource.MoveLast();
            record_pos();
         }

        void record_pos()
        {
            this.textBox1.Text = (this.emp_InfoBindingSource.Position+1).ToString();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            save_cancel_butt();
            //-----------------
            dc = new Model.My_staffEntities();
            dc.Emp_Info.Load();
            this.emp_InfoBindingSource.DataSource = dc.Emp_Info.Local.ToBindingList();
            //-------------
        }

        void save_cancel_butt()
        {
            this.save_butt.Enabled = false;
            this.cancel_butt.Enabled = false;
            //-------
            this.add_butt.Enabled = true;
            this.edit_butt.Enabled = true;
            this.del_butt.Enabled = true;
            //------
            this.groupBox1.Enabled = false;
        }
        void add_edit_del()
        {
            this.save_butt.Enabled = true;
            this.cancel_butt.Enabled = true;
            //-------
            this.add_butt.Enabled = false;
            this.edit_butt.Enabled = false;
            this.del_butt.Enabled = false;
            //------------
            this.groupBox1.Enabled = true;

        }

        private void save_butt_Click(object sender, EventArgs e)
        {
            this.emp_InfoBindingSource.EndEdit();
            int r = dc.SaveChanges();
            if (r > 0)
            {
                this.emp_InfoDataGridView.Refresh();
                this.emp_InfoBindingSource.ResetCurrentItem();  //used to show last saved id
                //-------------
                MessageBox.Show("Data Saved!!! Count:" + r.ToString());

            }
            else
            {
                MessageBox.Show("Not Saved!! Try Again");
            }
            //-------------------
            save_cancel_butt();

        }

        private void cancel_butt_Click(object sender, EventArgs e)
        {
            dc.Dispose();
            dc = new Model.My_staffEntities();
            dc.Emp_Info.Load();
            this.emp_InfoBindingSource.DataSource = dc.Emp_Info.Local.ToBindingList();
            //---------------
            save_cancel_butt();

        }

        private void add_butt_Click(object sender, EventArgs e)
        {
            add_edit_del();
            //------------------
            this.emp_InfoBindingSource.AddNew();
        }

        private void edit_butt_Click(object sender, EventArgs e)
        {
          int rc =  this.emp_InfoBindingSource.Count;
            if (rc==0)
            {
                MessageBox.Show("No records to edit!!");
                return;
            }
            //-------------
            add_edit_del();
            
            
        }

        private void del_butt_Click(object sender, EventArgs e)
        {
            int rc = this.emp_InfoBindingSource.Count;
            if (rc == 0)
            {
                MessageBox.Show("No records to delete!!");
                return;
            }
            //-------------
            DialogResult dr = MessageBox.Show("Are you sure?","Warning",MessageBoxButtons.OKCancel);
            if (dr==DialogResult.Cancel)
            {
                return;
            }
            this.emp_InfoBindingSource.RemoveCurrent();
            //----------------
            add_edit_del();
            //------
            this.groupBox1.Enabled = false;


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            dc.Dispose();
            dc = new Model.My_staffEntities();
            dc.Emp_Info.Load();
            this.emp_InfoBindingSource.DataSource = dc.Emp_Info.Local.ToBindingList();
        }
    }
}
