using PenjualanLaptop.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PenjualanLaptop.View
{
    public partial class UpdateBarang : Form
    {
        private LaptopController laptopController;
        private string originalId;

        public UpdateBarang(string id, string merk, string seri, string ram, string rom, string stock)
        {
            InitializeComponent();
            laptopController = new LaptopController();
            originalId = id;

            // Populate textboxes
            textBox1.Text = id;
            textBox2.Text = merk;
            textBox3.Text = seri;
            textBox4.Text = ram;
            textBox5.Text = rom;
            textBox6.Text = stock;

            // Make ID read-only (prevent changing primary key)
            textBox1.ReadOnly = true;

            // wire buttons
            saveButton.Click += saveButton_Click;
            exitButton.Click += exitButton_Click;
        }

        private void UpdateBarang_Load(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text.Trim();
            string merk = textBox2.Text.Trim();
            string seri = textBox3.Text.Trim();
            string ram = textBox4.Text.Trim();
            string rom = textBox5.Text.Trim();
            string stock = textBox6.Text.Trim();

            if (string.IsNullOrWhiteSpace(merk))
            {
                MessageBox.Show("Merk tidak boleh kosong.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int stockInt = 0;
            if (!string.IsNullOrWhiteSpace(stock) && !int.TryParse(stock, out stockInt))
            {
                MessageBox.Show("Stock harus berupa angka.", "Validasi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = laptopController.UpdateBarang(id, merk, seri, ram, rom, stock);
            if (ok)
            {
                MessageBox.Show("Data berhasil diupdate.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
