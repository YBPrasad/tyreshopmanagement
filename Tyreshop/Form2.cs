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
    public partial class Form2 : Form
    {
        MySqlConnection cn;
        MySqlCommand cm;
        MySqlDataReader dr;
        Class1 classCon = new Class1();
        Form1 f1;
        string ttype;
        public Form2(Form1 frm)
        {
            InitializeComponent();
            cn = new MySqlConnection(classCon.dbCon());

            f1 = frm;
        }

        public void setValue(string tyreno,string tyrebrand,string sizeone,string sizetwo,string sizethree,string type,string tprice,string quantity,string des)
        {
            tyreNo.Enabled = false;
            tyreNo.Text = tyreno;
            tyreBrand.Text = tyrebrand;
            sizeOne.Text = sizeone;
            sizeTwo.Text = sizetwo;
            sizeThree.Text = sizethree;
            tyrePrice.Text = tprice;
            tyreQuantity.Text = quantity;
            tyreDesc.Text = des;
            if (type == "Tube")
            {
                tubeType.Checked = true;
            }
            else if (type =="Tubeless")
            {
                tubelessType.Checked = true;
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void clear()
        {
            tyreBrand.Clear();
            tyreDesc.Clear();
            tyreNo.Clear();
            tyrePrice.Clear();
            tyreQuantity.Clear();
            sizeOne.Clear();
            sizeTwo.Clear();
            sizeThree.Clear();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try {
                cn.Open();
                string type;
                if (tubeType.Checked)
                {
                    type = "Tube";
                }
                else if (tubelessType.Checked)
                {
                    type = "Tubeless";
                }
                else
                {
                    type = null;
                }
                cm = new MySqlCommand("insert into stock values('"+tyreNo.Text+"','"+tyreBrand.Text+ "','"+sizeOne.Text+ "','"+sizeTwo.Text+ "','"+sizeThree.Text+ "','"+tyrePrice.Text+ "','"+tyreQuantity.Text+ "','"+tyreDesc.Text+"','"+type+"' )", cn);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Successfully added new item","Tyre Information",MessageBoxButtons.OK,MessageBoxIcon.Information);
                f1.LoadRecord();
            }
            catch(Exception ex)
            {
                cn.Close();
                MessageBox.Show("Warning"+ex.Message,"Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cn.Open();
            
            if (tubeType.Checked)
            {
                ttype = "Tube";
            }
            else if (tubelessType.Checked)
            {
                ttype = "Tubeless";
            }
            cm = new MySqlCommand("update stock set brand='"+tyreBrand.Text+"',sizeOne='"+sizeOne.Text+"',sizeTwo='"+sizeTwo.Text+ "',sizeThree='"+sizeThree.Text+ "',price='"+tyrePrice.Text+ "',qty='"+tyreQuantity.Text+ "',description='"+tyreDesc.Text+ "',type='"+ttype+"' where tyreNo='"+tyreNo.Text+"'", cn);
            cm.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Successfully Updated!","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
            
            f1.LoadRecord();
        }
    }
}
