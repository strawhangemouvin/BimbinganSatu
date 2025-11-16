using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PenjualanLaptop.Controller
{
    internal class LaptopController : Model.Connection
    {
        /// ======================= MENAMPILKAN DATA ====================\\\
        public DataTable tampilkanBarang(MySqlCommand cmd) //mengemvalikan data dlm btk table
        {
            DataTable data = new DataTable();
            try
            {
                string show = "SELECT * FROM Laptop"; //query untuk menampilkan semua data dari tabel barang
                da = new MySqlConnector.MySqlDataAdapter(show, GetConn()); //menghubungkan query dengan koneksi database
                da.Fill(data); //mengisi data dengan hasil query
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return data;
        }

        // Returns true when insert succeeds, false otherwise (e.g. duplicate id)
        public bool AddBarang(string Id, string Merk, string Seri, string Ram, string Rom, string Stock)
        {
            try
            {
                using (var conn = GetConn())
                {
                    // check duplicate id
                    string checkSql = "SELECT COUNT(*) FROM Laptop WHERE id = @id";
                    using (var checkCmd = new MySqlCommand(checkSql, conn))
                    {
                        checkCmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = Id;
                        var result = checkCmd.ExecuteScalar();
                        int count = 0;
                        if (result != null && int.TryParse(result.ToString(), out count) && count > 0)
                        {
                            MessageBox.Show("ID sudah digunakan. Gunakan ID lain.", "Duplicate ID", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return false;
                        }
                    }

                    // insert
                    string insertSql = "INSERT INTO Laptop (id, merk, seri, ram, rom, stock) VALUES (@id, @merk, @seri, @ram, @rom, @stock)";
                    using (var insertCmd = new MySqlCommand(insertSql, conn))
                    {
                        insertCmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = Id;
                        insertCmd.Parameters.Add("@merk", MySqlDbType.VarChar).Value = Merk;
                        insertCmd.Parameters.Add("@seri", MySqlDbType.VarChar).Value = Seri;
                        insertCmd.Parameters.Add("@ram", MySqlDbType.VarChar).Value = Ram;
                        insertCmd.Parameters.Add("@rom", MySqlDbType.VarChar).Value = Rom;
                        insertCmd.Parameters.Add("@stock", MySqlDbType.Int32).Value = string.IsNullOrWhiteSpace(Stock) ? 0 : Convert.ToInt32(Stock);

                        insertCmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tambah Barang gagal : " + ex.Message);
                return false;
            }
        }

        // Update existing laptop by id. Returns true if update succeeded.
        public bool UpdateBarang(string Id, string Merk, string Seri, string Ram, string Rom, string Stock)
        {
            try
            {
                using (var conn = GetConn())
                {
                    string updateSql = "UPDATE Laptop SET merk = @merk, seri = @seri, ram = @ram, rom = @rom, stock = @stock WHERE id = @id";
                    using (var cmd = new MySqlCommand(updateSql, conn))
                    {
                        cmd.Parameters.Add("@merk", MySqlDbType.VarChar).Value = Merk;
                        cmd.Parameters.Add("@seri", MySqlDbType.VarChar).Value = Seri;
                        cmd.Parameters.Add("@ram", MySqlDbType.VarChar).Value = Ram;
                        cmd.Parameters.Add("@rom", MySqlDbType.VarChar).Value = Rom;
                        cmd.Parameters.Add("@stock", MySqlDbType.Int32).Value = string.IsNullOrWhiteSpace(Stock) ? 0 : Convert.ToInt32(Stock);
                        cmd.Parameters.Add("@id", MySqlDbType.VarChar).Value = Id;

                        int rows = cmd.ExecuteNonQuery();
                        return rows > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Update gagal: " + ex.Message);
                return false;
            }
        }

        public void DeleteBarang(string id)
        {
            string delete = "DELETE FROM Laptop WHERE id=@id";
            try
            {
                cmd = new MySqlConnector.MySqlCommand(delete, GetConn());
                cmd.Parameters.Add("@id", MySqlConnector.MySqlDbType.VarChar).Value = id;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Delete Data Barang Gagal" + ex.Message);
            }
        }
    }
}
