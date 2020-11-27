using System;
using System.Windows;
using MijnenRapportage.Config;
using MijnenRapportage.Models.Overzichten;
using MijnenRapportage.Tools;
using MijnenRapportage.Views;

namespace MijnenRapportage {
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
        Foutmelding.Content = "Error reading mine values";
      }
      try
      {
        reportValues = BinarySerialization.ReadFromBinaryFile<ReportValues>("reportValues.bin");
      }
      catch (Exception)
      {
        Foutmelding.Content = "Error reading report values, using default";
        reportValues.ijzerWaarde = 19.00m;
        reportValues.steenWaarde = 14.00m;
        reportValues.kleiWaarde = 4.00m;
        reportValues.gloon1uur = 2.04m;
        reportValues.gloon1uurmob = 1.22m;
        reportValues.gloon2uur = 2.72m;
        reportValues.gloon2uurmob = 2.18m;
        reportValues.gloon6uur = 5.52m;
        reportValues.gloon10uur = 7.84m;
        reportValues.gloon22uur = 15.00m;
        reportValues.iloon1uur = 2.04m;
        reportValues.iloon1uurmob = 1.22m;
        reportValues.iloon2uur = 2.72m;
        reportValues.iloon2uurmob = 2.18m;
        reportValues.iloon6uur = 5.52m;
        reportValues.iloon10uur = 7.84m;
        reportValues.iloon22uur = 15.00m;
        reportValues.cloon1uur = 2.04m;
        reportValues.cloon1uurmob = 1.22m;
        reportValues.cloon2uur = 2.72m;
        reportValues.cloon2uurmob = 2.18m;
        reportValues.cloon6uur = 5.52m;
        reportValues.cloon10uur = 7.84m;
        reportValues.cloon22uur = 15.00m;
        reportValues.sloon1uur = 2.04m;
        reportValues.sloon1uurmob = 1.22m;
        reportValues.sloon2uur = 2.72m;
        reportValues.sloon2uurmob = 2.18m;
        reportValues.sloon6uur = 5.52m;
        reportValues.sloon10uur = 7.84m;
        reportValues.sloon22uur = 15.00m;
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
      mijnenOverzicht.mijn1.loon1uur = reportValues.gloon1uur;
      mijnenOverzicht.mijn1.loon1uurmob = reportValues.gloon1uurmob;
      mijnenOverzicht.mijn1.loon2uur = reportValues.gloon2uur;
      mijnenOverzicht.mijn1.loon2uurmob = reportValues.gloon2uurmob;
      mijnenOverzicht.mijn1.loon6uur = reportValues.gloon6uur;
      mijnenOverzicht.mijn1.loon10uur = reportValues.gloon10uur;
      mijnenOverzicht.mijn1.loon22uur = reportValues.gloon22uur;
      mijnenOverzicht.mijn2.loon1uur = reportValues.iloon1uur;
      mijnenOverzicht.mijn2.loon1uurmob = reportValues.iloon1uurmob;
      mijnenOverzicht.mijn2.loon2uur = reportValues.iloon2uur;
      mijnenOverzicht.mijn2.loon2uurmob = reportValues.iloon2uurmob;
      mijnenOverzicht.mijn2.loon6uur = reportValues.iloon6uur;
      mijnenOverzicht.mijn2.loon10uur = reportValues.iloon10uur;
      mijnenOverzicht.mijn2.loon22uur = reportValues.iloon22uur;
      mijnenOverzicht.mijn3.loon1uur = reportValues.cloon1uur;
      mijnenOverzicht.mijn3.loon1uurmob = reportValues.cloon1uurmob;
      mijnenOverzicht.mijn3.loon2uur = reportValues.cloon2uur;
      mijnenOverzicht.mijn3.loon2uurmob = reportValues.cloon2uurmob;
      mijnenOverzicht.mijn3.loon6uur = reportValues.cloon6uur;
      mijnenOverzicht.mijn3.loon10uur = reportValues.cloon10uur;
      mijnenOverzicht.mijn3.loon22uur = reportValues.cloon22uur;
      mijnenOverzicht.mijn4.loon1uur = reportValues.sloon1uur;
      mijnenOverzicht.mijn4.loon1uurmob = reportValues.sloon1uurmob;
      mijnenOverzicht.mijn4.loon2uur = reportValues.sloon2uur;
      mijnenOverzicht.mijn4.loon2uurmob = reportValues.sloon2uurmob;
      mijnenOverzicht.mijn4.loon6uur = reportValues.sloon6uur;
      mijnenOverzicht.mijn4.loon10uur = reportValues.sloon10uur;
      mijnenOverzicht.mijn4.loon22uur = reportValues.sloon22uur;
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
        Foutmelding.Content = "Error saving mine information";
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
        Foutmelding.Content = "Error saving report values";
      }

    }

    private void OverigeRapporten_Click(object sender, RoutedEventArgs e)
    {
      ReportWindow repVal = new ReportWindow(mijnenOverzicht);
      repVal.ShowDialog();
    }
  }
}
