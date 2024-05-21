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
using Microsoft.Win32;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Views.Windows;

namespace KomalliEmployee.Views.Pages {
    /// <summary>
    /// Lógica de interacción para TicketReport.xaml
    /// </summary>
    public partial class TicketReport : Window {
        public TicketReport() {
            InitializeComponent();
            ShowReport();
        }

        private void BindReport(DataSet dataSetFoodOrder, DataSet dataSetFoodOrderSetMenu) {
            ReportDataSource reportDataSourceFoodOrderSetMenu = new ReportDataSource("DataSetFoodOrderSetMenu", dataSetFoodOrderSetMenu.Tables[3]);
            ReportDataSource reportDataSourceFoodOrder = new ReportDataSource("DataSetFoodOrder", dataSetFoodOrder.Tables[4]);

            rpv.LocalReport.DataSources.Clear();
            rpv.LocalReport.DataSources.Add(reportDataSourceFoodOrderSetMenu);
            rpv.LocalReport.DataSources.Add(reportDataSourceFoodOrder);
            rpv.LocalReport.ReportEmbeddedResource = "KomalliEmployee.Resources.KomalliReports.TicketReport.rdlc";
            rpv.RefreshReport();
        }

        private void ClickDownloadReport(object sender, RoutedEventArgs e) {
            try {
                LocalReport localReport = rpv.LocalReport;

                byte[] bytes = localReport.Render("PDF");

                SaveFileDialog saveFileDialog = new SaveFileDialog {
                    Filter = "PDF files (.pdf)|*.pdf",
                    FilterIndex = 2,
                    RestoreDirectory = true
                };

                bool? result = saveFileDialog.ShowDialog();

                if (result == true) {
                    string fileName = saveFileDialog.FileName;
                    System.IO.File.WriteAllBytes(fileName, bytes);
                    App.ShowMessageInformation("Reporte descargado correctamente", "Descarga de reporte");
                }
            } catch (Exception ex) {
                App.ShowMessageError("Error al descargar el reporte", "Error al descargar");
                LoggerManager.Instance.LogError("Error al descargar el reporte", ex);
            }
        }

        private void ShowReport() {
            try {
                rpv.Reset();
                DataReports dataReportsFoodOrderSetMenu = new DataReports();
                DataReports dataReportsFoodOrder = new DataReports();

                FoodOrderSetMenuTableAdapter foodOrderSetMenuTableAdapter = new FoodOrderSetMenuTableAdapter();
                foodOrderSetMenuTableAdapter.Fill(dataReportsFoodOrderSetMenu.FoodOrderSetMenu, SingletonClass.Instance.IdFoodOrderSelected);

                FoodOrderTableAdapter foodOrderTableAdapter = new FoodOrderTableAdapter();
                foodOrderTableAdapter.Fill(dataReportsFoodOrder.FoodOrder, SingletonClass.Instance.IdFoodOrderSelected);

                BindReport(dataReportsFoodOrder, dataReportsFoodOrderSetMenu);
                rpv.RefreshReport();
            } catch (SqlException ex) {
                App.ShowMessageError("Error al cargar el reporte", "Error al cargar");
                LoggerManager.Instance.LogError("Error al cargar el reporte", ex);
            }
        }

        private void ClickExit(object sender, RoutedEventArgs e) {
            SingletonClass.Instance.SelectedFoods.Clear();
            this.Close();
        }
    }
}
