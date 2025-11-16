using PenjualanLaptop.Controller;
using PenjualanLaptop.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PenjualanLaptop
{
    public partial class AddLaptop : Form
    {
        LaptopController laptopController;
        LaptopView Kembali = new LaptopView();

        public AddLaptop()
        {
            laptopController = new LaptopController();
            InitializeComponent();
        }
        

        private void AddLaptop_Load(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            // textBox1 = id, textBox2 = merk, textBox3 = seri, textBox4 = ram, textBox5 = rom, textBox6 = stock
            string id = textBox1.Text.Trim();
            string merk = textBox2.Text.Trim();
            string seri = textBox3.Text.Trim();
            string ram = textBox4.Text.Trim();
            string rom = textBox5.Text.Trim();
            string stock = textBox6.Text.Trim();

            if (string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(merk))
            {
                MessageBox.Show("ID dan Merk tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int stockInt = 0;
            if (!string.IsNullOrWhiteSpace(stock) && !int.TryParse(stock, out stockInt))
            {
                MessageBox.Show("Stock harus berupa angka.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = laptopController.AddBarang(id, merk, seri, ram, rom, stock);
            if (ok)
            {
                MessageBox.Show("Data berhasil ditambahkan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Kembali.Show();
                this.Close();
            }
            // if AddBarang returned false it already showed message about duplicate id or error
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            //LaptopView Kembali = new LaptopView();
            Kembali.Show();
            this.Hide();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
