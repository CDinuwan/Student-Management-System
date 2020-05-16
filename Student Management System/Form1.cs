using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Student_Management_System
{
    public partial class Form1 : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        public Form1()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyCon());
            LoadRecord();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        public void LoadRecord()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("select * from student",cn);
            dr = cm.ExecuteReader();
            while(dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i,dr["regno"].ToString(),dr["surname"].ToString(),dr["name"].ToString(),dr["phoneno"].ToString(),dr["birthday"].ToString(),dr["extra"].ToString(),dr["specialab"].ToString());
            }
            cn.Close();
        }

        public void Clear()
        {
            txtReg.Clear();
            txtName.Clear();
            txtEA.Clear();
            txtBD.Clear();
            txtSur.Clear();
            txtTP.Clear();
            txtSA.Clear();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this?", "Registration", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO student(regno,surname,name,phoneno,birthday,extra,specialab)values(@regno,@surname,@name,@phoneno,@birthday,@extra,@specialab)", cn);
                    cm.Parameters.AddWithValue("@regno", txtReg.Text);
                    cm.Parameters.AddWithValue("@surname", txtSur.Text);
                    cm.Parameters.AddWithValue("@name", txtName.Text);
                    cm.Parameters.AddWithValue("@phoneno", txtTP.Text);
                    cm.Parameters.AddWithValue("@birthday", txtBD.Text);
                    cm.Parameters.AddWithValue("extra", txtEA.Text);
                    cm.Parameters.AddWithValue("specialab", txtEA.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Information has been saved");
                    Clear();
                    LoadRecord();
                }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void metroTextBox1_FontChanged(object sender, EventArgs e)
        {

        }

        
    }
}
