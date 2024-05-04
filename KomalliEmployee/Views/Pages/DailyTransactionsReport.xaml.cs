using KomalliEmployee.Resources.DatasetsDB.DataReportsTableAdapters;
using KomalliEmployee.Resources.DatasetsDB;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using KomalliEmployee.Model.Utilities;
using System.Data;
using System.Runtime.InteropServices.ComTypes;

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Lógica de interacción para DailyTransactionsReport.xaml
    /// </summary>
    public partial class DailyTransactionsReport : Page {

        private DateTime? startDate;
        private DateTime? endDate;
        public DailyTransactionsReport() {
            InitializeComponent();
            dtpStartDate.SelectedDateChanged += SelectedDateChangedGetDates;
            dtpEndDate.SelectedDateChanged += SelectedDateChangedGetDates;
            dtpStartDate.SelectedDate = DateTime.Today;
            dtpEndDate.SelectedDate = DateTime.Today;
            ShowReport(DateTime.Today, DateTime.Today);
        }

        private void SelectedDateChangedGetDates(object sender, SelectionChangedEventArgs e) {
            startDate = dtpStartDate.SelectedDate;
            endDate = dtpEndDate.SelectedDate;
            ShowReport(startDate, endDate);
        }

        private void ClicDownloadReport(object sender, RoutedEventArgs e) {
            try {
                LocalReport localReport = rpv.LocalReport;

                byte[] bytes = localReport.Render("PDF");

                SaveFileDialog saveFileDialog = new SaveFileDialog {
                    Filter = "PDF files (.pdf)|.pdf",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };

                if (saveFileDialog.ShowDialog() == DialogResult.OK) {
                    string fileName = saveFileDialog.FileName;
                    System.IO.File.WriteAllBytes(fileName, bytes);
                    App.ShowMessageInformation("Reporte descargado correctamente", "Descarga de reporte");
                }
            } catch (Exception ex) {
                App.ShowMessageError("Error al descargar el reporte", "Error al descargar");
                LoggerManager.Instance.LogError("Error al descargar el reporte", ex);
            }
        }
        private void BindReport(DataSet dataSet) {
            ReportDataSource reportDataSource = new ReportDataSource("DataSetCashCutFoodOrder", dataSet.Tables[2]);
            rpv.LocalReport.DataSources.Add(reportDataSource);
            rpv.LocalReport.ReportEmbeddedResource = "KomalliEmployee.Resources.KomalliReports.DailyTransactionsReport.rdlc";
            rpv.RefreshReport();
        }
        private void ShowReport(DateTime? startDate, DateTime? endDate) {
            try {
                if (startDate != null && endDate != null) {
                    string start = startDate.Value.Date.ToString("yyyy-MM-dd");
                    string end = endDate.Value.Date.ToString("yyyy-MM-dd");

                    rpv.Reset();
                    DataReports dataReports = new DataReports();
                    CashCutFoodOrderTableAdapter cashCutFoodOrderTableAdapter = new CashCutFoodOrderTableAdapter();
                    cashCutFoodOrderTableAdapter.Fill(dataReports.CashCutFoodOrder, start, end);
                    BindReport(dataReports);
                    rpv.RefreshReport();
                }
            } catch (SqlException ex) {
                App.ShowMessageError("Error al cargar el reporte", "Error al cargar");
                LoggerManager.Instance.LogError("Error al cargar el reporte", ex);
            }
        }
    }
}
