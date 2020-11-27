using System;
using System.Windows;

namespace MijnenRapportage.Config {
  /// <summary>
  /// Interaction logic for ReportValuesWindow.xaml
  /// </summary>
  public partial class ReportValuesWindow : Window
  {
    private ReportValues reportValues;
    public ReportValuesWindow(ReportValues rapportWaardes)
    {
      InitializeComponent();
      Init(rapportWaardes);
      reportValues = rapportWaardes;
    }

    private void Init(ReportValues rapportWaardes)
    {
      ijzerWaarde.Text = rapportWaardes.ijzerWaarde.ToString();
      steenWaarde.Text = rapportWaardes.steenWaarde.ToString();
      kleiWaarde.Text = rapportWaardes.kleiWaarde.ToString();
      gloon1uur.Text = rapportWaardes.gloon1uur.ToString();
      gloon1uurmob.Text = rapportWaardes.gloon1uurmob.ToString();
      gloon2uur.Text = rapportWaardes.gloon2uur.ToString();
      gloon2uurmob.Text = rapportWaardes.gloon2uurmob.ToString();
      gloon6uur.Text = rapportWaardes.gloon6uur.ToString();
      gloon10uur.Text = rapportWaardes.gloon10uur.ToString();
      gloon22uur.Text = rapportWaardes.gloon22uur.ToString();
      iloon1uur.Text = rapportWaardes.iloon1uur.ToString();
      iloon1uurmob.Text = rapportWaardes.iloon1uurmob.ToString();
      iloon2uur.Text = rapportWaardes.iloon2uur.ToString();
      iloon2uurmob.Text = rapportWaardes.iloon2uurmob.ToString();
      iloon6uur.Text = rapportWaardes.iloon6uur.ToString();
      iloon10uur.Text = rapportWaardes.iloon10uur.ToString();
      iloon22uur.Text = rapportWaardes.iloon22uur.ToString();
      cloon1uur.Text = rapportWaardes.cloon1uur.ToString();
      cloon1uurmob.Text = rapportWaardes.cloon1uurmob.ToString();
      cloon2uur.Text = rapportWaardes.cloon2uur.ToString();
      cloon2uurmob.Text = rapportWaardes.cloon2uurmob.ToString();
      cloon6uur.Text = rapportWaardes.cloon6uur.ToString();
      cloon10uur.Text = rapportWaardes.cloon10uur.ToString();
      cloon22uur.Text = rapportWaardes.cloon22uur.ToString();
      sloon1uur.Text = rapportWaardes.sloon1uur.ToString();
      sloon1uurmob.Text = rapportWaardes.sloon1uurmob.ToString();
      sloon2uur.Text = rapportWaardes.sloon2uur.ToString();
      sloon2uurmob.Text = rapportWaardes.sloon2uurmob.ToString();
      sloon6uur.Text = rapportWaardes.sloon6uur.ToString();
      sloon10uur.Text = rapportWaardes.sloon10uur.ToString();
      sloon22uur.Text = rapportWaardes.sloon22uur.ToString();
    }

    private void opslaan_Click(object sender, RoutedEventArgs e)
    {
      reportValues.ijzerWaarde = Convert.ToDecimal(ijzerWaarde.Text);
      reportValues.steenWaarde = Convert.ToDecimal(steenWaarde.Text);
      reportValues.kleiWaarde = Convert.ToDecimal(kleiWaarde.Text);
      reportValues.gloon1uur = Convert.ToDecimal(gloon1uur.Text);
      reportValues.gloon1uurmob = Convert.ToDecimal(gloon1uurmob.Text);
      reportValues.gloon2uur = Convert.ToDecimal(gloon2uur.Text);
      reportValues.gloon2uurmob = Convert.ToDecimal(gloon2uurmob.Text);
      reportValues.gloon6uur = Convert.ToDecimal(gloon6uur.Text);
      reportValues.gloon10uur = Convert.ToDecimal(gloon10uur.Text);
      reportValues.gloon22uur = Convert.ToDecimal(gloon22uur.Text);
      reportValues.iloon1uur = Convert.ToDecimal(iloon1uur.Text);
      reportValues.iloon1uurmob = Convert.ToDecimal(iloon1uurmob.Text);
      reportValues.iloon2uur = Convert.ToDecimal(iloon2uur.Text);
      reportValues.iloon2uurmob = Convert.ToDecimal(iloon2uurmob.Text);
      reportValues.iloon6uur = Convert.ToDecimal(iloon6uur.Text);
      reportValues.iloon10uur = Convert.ToDecimal(iloon10uur.Text);
      reportValues.iloon22uur = Convert.ToDecimal(iloon22uur.Text);
      reportValues.cloon1uur = Convert.ToDecimal(cloon1uur.Text);
      reportValues.cloon1uurmob = Convert.ToDecimal(cloon1uurmob.Text);
      reportValues.cloon2uur = Convert.ToDecimal(cloon2uur.Text);
      reportValues.cloon2uurmob = Convert.ToDecimal(cloon2uurmob.Text);
      reportValues.cloon6uur = Convert.ToDecimal(cloon6uur.Text);
      reportValues.cloon10uur = Convert.ToDecimal(cloon10uur.Text);
      reportValues.cloon22uur = Convert.ToDecimal(cloon22uur.Text);
      reportValues.sloon1uur = Convert.ToDecimal(sloon1uur.Text);
      reportValues.sloon1uurmob = Convert.ToDecimal(sloon1uurmob.Text);
      reportValues.sloon2uur = Convert.ToDecimal(sloon2uur.Text);
      reportValues.sloon2uurmob = Convert.ToDecimal(sloon2uurmob.Text);
      reportValues.sloon6uur = Convert.ToDecimal(sloon6uur.Text);
      reportValues.sloon10uur = Convert.ToDecimal(sloon10uur.Text);
      reportValues.sloon22uur = Convert.ToDecimal(sloon22uur.Text);
      Close();
    }
  }
}
