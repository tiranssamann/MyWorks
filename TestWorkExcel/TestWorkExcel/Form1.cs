using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Excel = Microsoft.Office.Interop.Excel;
using TestWorkExcel.Модель;
namespace TestWorkExcel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnImportExcel_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файл Excel|*.XLSX;*.XLS";
            openFileDialog.ShowDialog();
            Excel.Application exApp = new Excel.Application();
            Excel.Workbook xlWorkbook = exApp.Workbooks.Open(openFileDialog.FileName);
            Excel.Worksheet xlWorksheet = (Excel.Worksheet)xlWorkbook.Sheets.get_Item(1);
            Excel.Range range = (Excel.Range)xlWorksheet.get_Range("A1", "A"+ xlWorksheet.UsedRange.Rows.Count).Cells;
            Excel.Range range1 = (Excel.Range)xlWorksheet.get_Range("B1", "B" + xlWorksheet.UsedRange.Rows.Count).Cells;
            Excel.Range range2 = (Excel.Range)xlWorksheet.get_Range("C1", "C" + xlWorksheet.UsedRange.Rows.Count).Cells;
            for (int i = 2; i < xlWorksheet.UsedRange.Rows.Count +1; i++)
            {
                Products products = new Products();
                products.NameOfProduct = string.Format("{0}",range[i].Cells.Value);
                products.PriceOfProduct = double.Parse(string.Format("{0}", range1[i].Cells.Value));
                products.CountOfProduct = int.Parse(string.Format("{0}", range2[i].Cells.Value));
                products.SumOfProduct = products.CountOfProduct * products.PriceOfProduct;
                dataGridView1.Rows.Add(products.NameOfProduct, products.PriceOfProduct, products.CountOfProduct, products.SumOfProduct);
            }
            exApp.Quit();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                // открытие окна куда загрузить файл
                SaveFileDialog saveFile = new SaveFileDialog();
                // формат файла
                saveFile.DefaultExt = ".xsl";
                // фильтр создания файла
                saveFile.Filter = "Excel files (*.xlsx)|*.xlsx";
                if (saveFile.ShowDialog() == System.Windows.Forms.DialogResult.OK && saveFile.FileName.Length > 0)
                {
                    // создание excel
                    Excel.Application exApp = new Excel.Application();
                    exApp.Workbooks.Add();
                    // создание листа excel
                    Excel.Worksheet workSheet = (Excel.Worksheet)exApp.ActiveSheet;
                    // объединение ячеек
                    Excel.Range _excelCells1 = (Excel.Range)workSheet.get_Range("A1", "B1").Cells;
                    Excel.Range _excelCells2 = (Excel.Range)workSheet.get_Range("A2", "B2").Cells;
                    _excelCells2.Merge(Type.Missing);
                    _excelCells1.Merge(Type.Missing);
                    // что вписать в ячейки 
                    workSheet.Cells[1, 1] = "Остаток товара на складе";
                    workSheet.Cells[2, 1] = DateTime.Now.ToString();
                    workSheet.Cells[4, 1] = "Наименование товара";
                    workSheet.Cells[4, 2] = "Цена";
                    workSheet.Cells[4, 3] = "Количество";
                    workSheet.Cells[4, 4] = "Сумма";

                    // выровнить столбцы под надпись в них
                    Excel.Range _excelCells3 = (Excel.Range)workSheet.get_Range("A1", "D4").Cells;
                    _excelCells3.Columns.EntireColumn.AutoFit();
                    _excelCells3.HorizontalAlignment = Excel.Constants.xlCenter;
                    // с какой строки начать вывод данных таблицы
                    int rowExcel = 5;
                    // вывод данных в excel с datagridview
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        workSheet.Cells[rowExcel, "A"] = dataGridView1.Rows[i].Cells["NameOf"].Value;
                        workSheet.Cells[rowExcel, "B"] = dataGridView1.Rows[i].Cells["Price"].Value;
                        workSheet.Cells[rowExcel, "C"] = dataGridView1.Rows[i].Cells["CoutOf"].Value;
                        workSheet.Cells[rowExcel, "D"] = dataGridView1.Rows[i].Cells["SumOf"].Value;

                        ++rowExcel;
                    }
                    // сделать из ячеек таблицу
                    int a = 3 + dataGridView1.Rows.Count;
                    Excel.Range s = workSheet.Range["A4:D" + a];
                    s.HorizontalAlignment = Excel.Constants.xlCenter;
                    s.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
                    s.Borders.Color = Color.Black;
                    // сохранить файл
                    workSheet.SaveAs(saveFile.FileName);
                    // убрать файл с использования ОЗУ
                    exApp.Quit();
                }
                else if (saveFile.FileName.Length < 0) { throw new Exception("Введите название файла"); }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
