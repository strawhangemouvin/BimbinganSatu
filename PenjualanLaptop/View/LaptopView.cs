using MySqlConnector;
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

namespace PenjualanLaptop.View
{
    public partial class LaptopView : Form
    {
        LaptopController laptopController;
        public LaptopView()
        {
            laptopController = new LaptopController();
            InitializeComponent();
        }
        bool showTable()
        {
            dataGridView1.DataSource = laptopController.tampilkanBarang(new MySqlCommand("SELECT * FROM Laptop"));
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            return true;
        }

        private void LaptopView_Load(object sender, EventArgs e)
        {
            showTable();

        }

        private void ADD_Click(object sender, EventArgs e)
        {
            AddLaptop addForm = new AddLaptop();
            addForm.Show();
            this.Hide();

        }

        private void UPDATE_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Pilih baris yang ingin diupdate.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dataGridView1.CurrentRow;

            // assume columns: id, merk, seri, ram, rom, stock
            string id = row.Cells[0].Value?.ToString();
            string merk = row.Cells[1].Value?.ToString();
            string seri = row.Cells[2].Value?.ToString();
            string ram = row.Cells[3].Value?.ToString();
            string rom = row.Cells[4].Value?.ToString();
            string stock = row.Cells[5].Value?.ToString();

            UpdateBarang updateForm = new UpdateBarang(id, merk, seri, ram, rom, stock);
            updateForm.FormClosed += (s, ev) => { this.Show(); showTable(); };
            updateForm.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Delete = this.dataGridView1.CurrentRow.Cells[0].Value.ToString();
            laptopController.DeleteBarang(Delete);
            showTable();

            MessageBox.Show("Data berhasil dihapus");
        }
    }
}
