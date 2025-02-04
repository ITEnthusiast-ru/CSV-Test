using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSV_test
{
    public partial class MainForm : Form
    {
        private string _filePath = "data.csv"; // Укажите путь к вашему CSV-файлу

        public MainForm()
        {
            InitializeComponent();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchDigits = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchDigits))
            {
                MessageBox.Show("Введите цифры для поиска.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            List<string> results = SearchByDigits(searchDigits);

            listBoxResults.Items.Clear();

            if (results.Count > 0)
            {
                foreach (var result in results)
                {
                    listBoxResults.Items.Add(result);
                }
            }
            else
            {
                MessageBox.Show("Совпадений не найдено.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private List<string> SearchByDigits(string digits)
        {
            List<string> matches = new List<string>();

            try
            {
                using (StreamReader reader = new StreamReader(_filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.StartsWith(digits))
                        {
                            matches.Add(line);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return matches;
        }
    }
}
