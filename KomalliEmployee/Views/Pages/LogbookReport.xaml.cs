using KomalliEmployee.Model.Utilities;
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
using System.Data;

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Lógica de interacción para LogbookReport.xaml
    /// </summary>
    public partial class LogbookReport : Page {
        public LogbookReport() {
            InitializeComponent();
            cboSection.SelectionChanged += SelectedDateChangedGetDate;
        }

        private void SelectedDateChangedGetDate(object sender, SelectionChangedEventArgs e) {
            ShowReport();
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
            ReportDataSource reportDataSource = new ReportDataSource("DataSetLogbook", dataSet.Tables[1]);
            rpv.LocalReport.DataSources.Add(reportDataSource);
            rpv.LocalReport.ReportEmbeddedResource = "KomalliEmployee.Resources.KomalliReports.LogbookReport.rdlc";
            rpv.RefreshReport();
        }
        private void ShowReport() {
            try {
                string date = dtpDate.SelectedDate.HasValue ? dtpDate.SelectedDate.Value.Date.ToString("yyyy-MM-dd") : null;
                string section = null;
                if (cboSection.SelectedItem != null) {
                    ComboBoxItem selectedItem = (ComboBoxItem)cboSection.SelectedItem;
                    section = selectedItem.Content.ToString();
                }
          
                rpv.Reset();
                DataReports dataReports = new DataReports();
                LogbookEmployeeTableAdapter logbookEmployeeTableAdapter = new LogbookEmployeeTableAdapter();
                logbookEmployeeTableAdapter.Fill(dataReports.LogbookEmployee, date, section);
                BindReport(dataReports);
                rpv.RefreshReport();
            } catch (SqlException ex) {
                App.ShowMessageError("Error al cargar el reporte", "Error al cargar");
                LoggerManager.Instance.LogError("Error al cargar el reporte", ex);
            }
        }
    }
}
