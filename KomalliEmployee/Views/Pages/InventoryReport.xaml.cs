﻿using KomalliEmployee.Model.Utilities;
using KomalliEmployee.Resources.DatasetsDB.DataReportsTableAdapters;
using KomalliEmployee.Resources.DatasetsDB;
using log4net.Repository.Hierarchy;
using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace KomalliEmployee.Views.Pages {
    public partial class InventoryReport : Page {
        public InventoryReport() {
            InitializeComponent();
            ShowReport(null);
        }

        private void BindReport(DataSet dataSet) {
            ReportDataSource reportDataSource = new ReportDataSource("DataReports", dataSet.Tables[0]);
            rpv.LocalReport.DataSources.Add(reportDataSource);
            rpv.LocalReport.ReportEmbeddedResource = "KomalliEmployee.Resources.KomalliReports.InventoryReport.rdlc";
            rpv.RefreshReport();
        }

        private void ClicDownloadReport(object sender, RoutedEventArgs e) {
            try {
                LocalReport localReport = rpv.LocalReport;

                byte[] bytes = localReport.Render("PDF");
                ComboBoxItem selectedItem = (ComboBoxItem)cboCategory.SelectedItem;
                string defaultName = "";
                DateTime date = DateTime.Now;
                string dateStr = date.ToString("yyyy-MM-dd");

                if (selectedItem == null) {
                    defaultName = "ReporteInventarioGeneral" + dateStr;
                } else {
                    string category = selectedItem.Content.ToString();
                    defaultName = "ReporteInventario" + category + dateStr;

                }

                SaveFileDialog saveFileDialog = new SaveFileDialog {
                    Filter = "PDF files (.pdf)|.pdf",
                    FilterIndex = 2,
                    RestoreDirectory = true,
                    FileName = defaultName
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

        private void ShowReport(string category) {
            try {
                rpv.Reset();
                DataReports dataReports = new DataReports();
                IngredientTableAdapter ingredientTableAdapter = new IngredientTableAdapter();

                if (string.IsNullOrEmpty(category) || category == "General") {
                    ingredientTableAdapter.FillBy(dataReports.Ingredient);
                } else {
                    ingredientTableAdapter.Fill(dataReports.Ingredient, category);
                }

                BindReport(dataReports);
                rpv.RefreshReport();
            } catch (SqlException ex) {
                App.ShowMessageError("Error al cargar el reporte", "Error al cargar");
                LoggerManager.Instance.LogError("Error al cargar el reporte", ex);
            }
        }

        private void SelectionChangedCategory(object sender, SelectionChangedEventArgs e) {
            if (cboCategory.SelectedItem != null) {
                ComboBoxItem selectedItem = (ComboBoxItem)cboCategory.SelectedItem;

                string category = selectedItem.Content.ToString();

                ShowReport(category);
            }
        }
    }
}