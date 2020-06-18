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
using System.Windows.Shapes;

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
      loon1uur.Text = rapportWaardes.loon1uur.ToString();
      loon1uurmob.Text = rapportWaardes.loon1uurmob.ToString();
      loon2uur.Text = rapportWaardes.loon2uur.ToString();
      loon2uurmob.Text = rapportWaardes.loon2uurmob.ToString();
      loon6uur.Text = rapportWaardes.loon6uur.ToString();
      loon10uur.Text = rapportWaardes.loon10uur.ToString();
      loon22uur.Text = rapportWaardes.loon22uur.ToString();
    }

    private void opslaan_Click(object sender, RoutedEventArgs e)
    {
      reportValues.ijzerWaarde = Convert.ToDecimal(ijzerWaarde.Text);
      reportValues.steenWaarde = Convert.ToDecimal(steenWaarde.Text);
      reportValues.kleiWaarde = Convert.ToDecimal(kleiWaarde.Text);
      reportValues.loon1uur = Convert.ToDecimal(loon1uur.Text);
      reportValues.loon1uurmob = Convert.ToDecimal(loon1uurmob.Text);
      reportValues.loon2uur = Convert.ToDecimal(loon2uur.Text);
      reportValues.loon2uurmob = Convert.ToDecimal(loon2uurmob.Text);
      reportValues.loon6uur = Convert.ToDecimal(loon6uur.Text);
      reportValues.loon10uur = Convert.ToDecimal(loon10uur.Text);
      reportValues.loon22uur = Convert.ToDecimal(loon22uur.Text);
      Close();
    }
  }
}
