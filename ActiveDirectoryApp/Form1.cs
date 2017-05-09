using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ActiveDirectory;

namespace ActiveDirectoryApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        ADirectory ad = new ADirectory("LDAP://BCBSFL.COM/DC=BCBSFL, DC=COM");
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                dataGridView1.DataSource =  ad.FindUser(textBox1.Text.Trim());
            }
            if (radioButton2.Checked)
            {
                dataGridView1.DataSource = ad.getMemberOf(textBox1.Text.Trim());
            }
            if (radioButton3.Checked)
            {
                dataGridView1.DataSource = ad.getGroupUsers(textBox1.Text.Trim());               
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                       
            this.textBox1.KeyDown += new KeyEventHandler(textBox1_KeyDown);
            radioButton1.Checked = true;
        }

        void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                button1_Click(button1, null);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string value = ad.getUserProperty("b5c5", "manager");
            string value2 = value.Substring(value.IndexOf("CN=")+3, value.IndexOf("OU=")-3);
            value2.Replace("\\\\", "");
        }

        
    }
}
