using System;
using System.Windows;
using MijnenRapportage.Models.Overzichten;

namespace MijnenRapportage.Views {
  /// <summary>
  /// Interaction logic for ReportWindow.xaml
  /// </summary>
  public partial class ReportWindow : Window
  {
    private MijnenOverzicht mijnenOverzicht;

    public ReportWindow(MijnenOverzicht mijnGegevens)
    {
      mijnenOverzicht = mijnGegevens;
      InitializeComponent();
    }

    private void Genereren_Click(object sender, RoutedEventArgs e)
    {
      MijnenRapportRequest myRequest = new MijnenRapportRequest();
      DateTime gisteren = DateTime.Today.AddDays(-1);
      myRequest.BeginDag = RapportDatum.SelectedDate ?? gisteren;
      myRequest.EindDag = RapportEindDatum.SelectedDate ?? myRequest.BeginDag;
      if (myRequest.BeginDag > myRequest.EindDag) myRequest.EindDag = myRequest.BeginDag;
      myRequest.IncludeMijn[0] = Mijn1.IsChecked.Equals(true);
      myRequest.IncludeMijn[1] = Mijn2.IsChecked.Equals(true);
      myRequest.IncludeMijn[2] = Mijn3.IsChecked.Equals(true);
      myRequest.IncludeMijn[3] = Mijn4.IsChecked.Equals(true);
      Dagrapport.Text = mijnenOverzicht.GetOtherRapport(myRequest);
    }

    private void Genereren_Csv(object sender, RoutedEventArgs e)
    {
      MijnenRapportRequest myRequest = new MijnenRapportRequest();
      DateTime gisteren = DateTime.Today.AddDays(-1);
      myRequest.BeginDag = RapportDatum.SelectedDate ?? gisteren;
      myRequest.EindDag = RapportEindDatum.SelectedDate ?? myRequest.BeginDag;
      if (myRequest.BeginDag > myRequest.EindDag) myRequest.EindDag = myRequest.BeginDag;
      myRequest.IncludeMijn[0] = Mijn1.IsChecked.Equals(true);
      myRequest.IncludeMijn[1] = Mijn2.IsChecked.Equals(true);
      myRequest.IncludeMijn[2] = Mijn3.IsChecked.Equals(true);
      myRequest.IncludeMijn[3] = Mijn4.IsChecked.Equals(true);
      Dagrapport.Text = mijnenOverzicht.GetCSVRapport(myRequest);
    }
  }
}
