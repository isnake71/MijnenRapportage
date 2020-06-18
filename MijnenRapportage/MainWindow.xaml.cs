using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MijnenRapportage.Config;
using MijnenRapportage.Models.Overzichten;
using MijnenRapportage.Tools;
using MijnenRapportage.Views;

namespace MijnenRapportage
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public ReportValues reportValues = new ReportValues();

    private MijnenOverzicht mijnenOverzicht = new MijnenOverzicht();

    public MainWindow()
    {
      InitializeComponent();
      Init();
    }

    private void Init()
    {
      try
      {
        mijnenOverzicht = BinarySerialization.ReadFromBinaryFile<MijnenOverzicht>("mijnenoverzicht.bin");
      }
      catch (Exception)
      {
        Foutmelding.Content = "Fout bij lezen oude gegevens";
      }
      try
      {
        reportValues = BinarySerialization.ReadFromBinaryFile<ReportValues>("reportValues.bin");
      }
      catch (Exception)
      {
        Foutmelding.Content = "Fout bij lezen oude configuratie";
        reportValues.ijzerWaarde = 19.00m;
        reportValues.steenWaarde = 14.00m;
        reportValues.kleiWaarde = 4.00m;
        reportValues.loon1uur = 2.04m;
        reportValues.loon1uurmob = 1.22m;
        reportValues.loon2uur = 2.72m;
        reportValues.loon2uurmob = 2.18m;
        reportValues.loon6uur = 5.52m;
        reportValues.loon10uur = 7.84m;
        reportValues.loon22uur = 15.00m;
      }
      InitMijnen();
      RapportDatum.SelectedDate = DateTime.Today.AddDays(-1);
    }

    private void InitMijnen()
    {
      mijnenOverzicht.reInitMijnen();
      mijnenOverzicht.ijzerWaarde = reportValues.ijzerWaarde;
      mijnenOverzicht.steenWaarde = reportValues.steenWaarde;
      mijnenOverzicht.kleiWaarde = reportValues.kleiWaarde;
      mijnenOverzicht.loon1uur = reportValues.loon1uur;
      mijnenOverzicht.loon1uurmob = reportValues.loon1uurmob;
      mijnenOverzicht.loon2uur = reportValues.loon2uur;
      mijnenOverzicht.loon2uurmob = reportValues.loon2uurmob;
      mijnenOverzicht.loon6uur = reportValues.loon6uur;
      mijnenOverzicht.loon10uur = reportValues.loon10uur;
      mijnenOverzicht.loon22uur = reportValues.loon22uur;
    }

    private void Inlezen_Click(object sender, RoutedEventArgs e)
    {
      Foutmelding.Content = "";
      InitMijnen();
      mijnenOverzicht.inlezen(MijnData.Text);
      try
      {
        BinarySerialization.WriteToBinaryFile<MijnenOverzicht>("mijnenoverzicht.bin", mijnenOverzicht);
      }
      catch (Exception)
      {
        Foutmelding.Content = "Fout bij opslaan gegevens";
      }
      Genereren_Click(sender, e);
    }

    private void Genereren_Click(object sender, RoutedEventArgs e)
    {
      DateTime gisteren = DateTime.Today.AddDays(-1);
      DateTime rapportDatum = RapportDatum.SelectedDate ?? gisteren;
      Dagrapport.Text = mijnenOverzicht.GetDagRapport(rapportDatum);
    }

    private void Weekrapport_Click(object sender, RoutedEventArgs e)
    {
      DateTime gisteren = DateTime.Today.AddDays(-1);
      DateTime rapportDatum = RapportDatum.SelectedDate ?? gisteren;
      Dagrapport.Text = mijnenOverzicht.GetWeekRapport(rapportDatum);
    }

    private void Config_Click(object sender, RoutedEventArgs e)
    {
      ReportValuesWindow repVal = new ReportValuesWindow(reportValues);
      repVal.ShowDialog();
      SaveConfig();
      InitMijnen();
    }

    private void SaveConfig()
    {
      Foutmelding.Content = "";
      try
      {
        BinarySerialization.WriteToBinaryFile<ReportValues>("reportValues.bin", reportValues);
      }
      catch (Exception)
      {
        Foutmelding.Content = "Fout bij opslaan gegevens";
      }

    }

    private void OverigeRapporten_Click(object sender, RoutedEventArgs e)
    {
      ReportWindow repVal = new ReportWindow(mijnenOverzicht);
      repVal.ShowDialog();
    }
  }
}
