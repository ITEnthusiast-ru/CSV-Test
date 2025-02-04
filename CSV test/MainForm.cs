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
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged; // Событие при изменении текста
            textBoxSearch.KeyDown += TextBoxSearch_KeyDown; // Событие при нажатии клавиш
            listBoxResults.Visible = false; // Скрываем ListBox по умолчанию
        }

        private void TextBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string searchDigits = textBoxSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchDigits))
            {
                listBoxResults.Visible = false; // Скрываем ListBox, если поле пустое
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
                listBoxResults.Visible = true; // Показываем ListBox с результатами
                listBoxResults.Top = textBoxSearch.Bottom; // Позиционируем ListBox под TextBox
                listBoxResults.Left = textBoxSearch.Left;
                listBoxResults.Width = textBoxSearch.Width;
            }
            else
            {
                listBoxResults.Visible = false; // Скрываем ListBox, если результатов нет
            }
        }

        private void TextBoxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Если нажат Enter и есть выбранный элемент в ListBox
                if (listBoxResults.SelectedIndex != -1)
                {
                    textBoxSearch.Text = listBoxResults.SelectedItem.ToString();
                    listBoxResults.Visible = false; // Скрываем ListBox после выбора
                }
            }
            else if (e.KeyCode == Keys.Down)
            {
                // Перемещаем фокус на ListBox и выбираем первый элемент
                if (listBoxResults.Items.Count > 0)
                {
                    listBoxResults.Focus();
                    listBoxResults.SelectedIndex = 0;
                }
            }
        }

        private void ListBoxResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // Если нажат Enter в ListBox
                if (listBoxResults.SelectedIndex != -1)
                {
                    textBoxSearch.Text = listBoxResults.SelectedItem.ToString();
                    listBoxResults.Visible = false; // Скрываем ListBox после выбора
                }
            }
            else if (e.KeyCode == Keys.Up && listBoxResults.SelectedIndex == 0)
            {
                // Если достигнут верхний элемент, возвращаем фокус в TextBox
                textBoxSearch.Focus();
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

        private void MainForm_Click(object sender, EventArgs e)
        {
            listBoxResults.Visible = false;
        }
    }
}
