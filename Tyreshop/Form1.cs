using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace Tyreshop
{
    public partial class Form1 : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        Class1 classCon = new Class1();
        MySqlDataAdapter adapter;
        DataTable table;

        string _tyreno, _brand,_sizeone,_sizetwo,_sizethree,_price,_qty,_type,_des;
        public Form1()
        {
            InitializeComponent();
            cn =new MySqlConnection(classCon.dbCon());
            LoadRecord();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2(this);
            frm.Show();
        }
        public void getAll()
        {
            int i = 0;
            
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new MySqlCommand("select * from stock", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[8].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            cn.Close();
        }
        public void LoadRecord()
        {
            getAll();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (colName=="colEdit")
            {
                Form2 frm2 = new Form2(this);
                
                frm2.setValue(_tyreno,_brand,_sizeone,_sizetwo,_sizethree,_type,_price,_qty,_des);
                frm2.ShowDialog();
            }

            else if (colName == "colDelete")
            {
                if (MessageBox.Show("Are You Sure Delete This Data?", "Delete Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new MySqlCommand("delete from stock where tyreNo='" + _tyreno + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadRecord();
                }
                
            }
        }

        public void searchData(string valueToSearch)
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            string query = "select * from stock where concat(brand,sizeOne) like '%"+ valueToSearch + "%' ";
            cm = new MySqlCommand(query, cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[8].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            cn.Close();
            //adapter = new MySqlDataAdapter(cm);
            //table = new DataTable();
            //adapter.Fill(table);
            //dataGridView1.DataSource=table;

        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            
            string valueToSearch = searchTyre.Text.ToString();
            if (valueToSearch == "")
            {
                getAll();
            }
            else
            {
                searchData(valueToSearch);
            }
            
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            _tyreno = dataGridView1[1, i].Value.ToString();
            _brand = dataGridView1[2, i].Value.ToString();
            _sizeone = dataGridView1[3, i].Value.ToString();
            _sizetwo = dataGridView1[4, i].Value.ToString();
            _sizethree = dataGridView1[5, i].Value.ToString();
            _type = dataGridView1[6, i].Value.ToString();
            _price = dataGridView1[7, i].Value.ToString();
            _qty = dataGridView1[8, i].Value.ToString();
            _des = dataGridView1[9, i].Value.ToString();
        }
    }
}
