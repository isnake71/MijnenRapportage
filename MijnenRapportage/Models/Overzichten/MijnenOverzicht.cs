using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;
using MijnenRapportage.Models.Mijnen;

namespace MijnenRapportage.Models.Overzichten {
  [Serializable]
  public class MijnenOverzicht {
    private GoudMijn mijn1 = new GoudMijn();
    private IjzerMijn mijn2 = new IjzerMijn();
    private GoudMijn mijn3 = new GoudMijn();
    private SteenMijn mijn4 = new SteenMijn();
    private SteenMijn mijn5 = new SteenMijn();
    private KleiMijn mijn6 = new KleiMijn();

    private CultureInfo provider = CultureInfo.CreateSpecificCulture("nl-NL");
    private string datumFormat = "yyyy-MM-dd";

    public Decimal ijzerWaarde;
    public Decimal steenWaarde;
    public Decimal kleiWaarde;
    public Decimal loon1uur;
    public Decimal loon1uurmob;
    public Decimal loon2uur;
    public Decimal loon2uurmob;
    public Decimal loon6uur;
    public Decimal loon10uur;
    public Decimal loon22uur;

    public void reInitMijnen() {
      mijn1.reInitMijn();
      mijn2.reInitMijn();
      mijn3.reInitMijn();
      mijn4.reInitMijn();
      mijn5.reInitMijn();
      mijn6.reInitMijn();
    }

    public void inlezen(string invoer) {
      if (invoer != null) {
        ParseInvoer(invoer);
      }
    }

    private void ParseInvoer(string invoer) {
      GoudMijn lmijn1 = new GoudMijn();
      IjzerMijn lmijn2 = new IjzerMijn();
      GoudMijn lmijn3 = new GoudMijn();
      SteenMijn lmijn4 = new SteenMijn();
      SteenMijn lmijn5 = new SteenMijn();
      KleiMijn lmijn6 = new KleiMijn();
      DateTime skipDag = DateTime.Today;
      DateTime parseSkipDag = DateTime.Today;

      lmijn1.OpbrengstenLijst = mijn1.OpbrengstenLijst;
      lmijn2.OpbrengstenLijst = mijn2.OpbrengstenLijst;
      lmijn3.OpbrengstenLijst = mijn3.OpbrengstenLijst;
      lmijn4.OpbrengstenLijst = mijn4.OpbrengstenLijst;
      lmijn5.OpbrengstenLijst = mijn5.OpbrengstenLijst;
      lmijn6.OpbrengstenLijst = mijn6.OpbrengstenLijst;
      parseSkipDag = ParseMijn(lmijn1, invoer, "Mijn 1", "Mijn 2");
      if (parseSkipDag < skipDag) {
        skipDag = parseSkipDag;
      }
      parseSkipDag = ParseMijn(lmijn2, invoer, "Mijn 2", "Mijn 3");
      if (parseSkipDag < skipDag) {
        skipDag = parseSkipDag;
      }
      parseSkipDag = ParseMijn(lmijn3, invoer, "Mijn 3", "Mijn 4");
      if (parseSkipDag < skipDag) {
        skipDag = parseSkipDag;
      }
      parseSkipDag = ParseMijn(lmijn4, invoer, "Mijn 4", "Mijn 5");
      if (parseSkipDag < skipDag) {
        skipDag = parseSkipDag;
      }
      parseSkipDag = ParseMijn(lmijn5, invoer, "Mijn 5", "Mijn 6");
      if (parseSkipDag < skipDag) {
        skipDag = parseSkipDag;
      }
      parseSkipDag = ParseMijn(lmijn6, invoer, "Mijn 6", null);
      if (parseSkipDag < skipDag) {
        skipDag = parseSkipDag;
      }
      mijn1.addMijnKosten(lmijn1, skipDag);
      mijn2.addMijnKosten(lmijn2, skipDag);
      mijn3.addMijnKosten(lmijn3, skipDag);
      mijn4.addMijnKosten(lmijn4, skipDag);
      mijn5.addMijnKosten(lmijn5, skipDag);
      mijn6.addMijnKosten(lmijn6, skipDag);
      mijn1.OpbrengstenLijst = lmijn1.OpbrengstenLijst;
      mijn2.OpbrengstenLijst = lmijn2.OpbrengstenLijst;
      mijn3.OpbrengstenLijst = lmijn3.OpbrengstenLijst;
      mijn4.OpbrengstenLijst = lmijn4.OpbrengstenLijst;
      mijn5.OpbrengstenLijst = lmijn5.OpbrengstenLijst;
      mijn6.OpbrengstenLijst = lmijn6.OpbrengstenLijst;
    }

    private DateTime ParseMijn(Mijn mijnMijn, string invoer, string startString, string endString) {
      String mijnInvoer = invoer;
      DateTime skipDag = DateTime.Today;
      if (!String.IsNullOrEmpty(mijnInvoer)) {
        int index = mijnInvoer.IndexOf(startString, 0);
        if (index >= 0) {
          int tempIndex = index;
          while (tempIndex >= 0) {
            int startPoint = tempIndex + 5;
            index = tempIndex;
            tempIndex = mijnInvoer.IndexOf(startString, startPoint);
          }
          int endPoint = mijnInvoer.Length;
          if (!String.IsNullOrEmpty(endString)) {
            endPoint = mijnInvoer.IndexOf(endString, (index + 5));
          }
          if (endPoint > index) {
            int mijnGegevensLength = endPoint - index;
            String mijnGegevens = mijnInvoer.Substring(index, mijnGegevensLength);
            index = mijnGegevens.IndexOf("Aantal mijnwerkers", 0);
            if (index > 0) {
              endPoint = mijnGegevens.IndexOf("Productie", index);
              if (endPoint > index) {
                mijnGegevensLength = endPoint - index;
                String mijnWerkGegevens = mijnGegevens.Substring(index, mijnGegevensLength);
                skipDag = ParseWerkUren(mijnMijn, mijnWerkGegevens);

                index = endPoint;
                endPoint = mijnGegevens.IndexOf("Grondstoffen", index);
                if (endPoint > index) {
                  mijnGegevensLength = endPoint - index;
                  String mijnProductieGegevens = mijnGegevens.Substring(index, mijnGegevensLength);
                  ParseProductie(mijnMijn, mijnProductieGegevens);

                  String mijnVerbruikGegevens = mijnGegevens.Substring(endPoint);
                  ParseVerbruik(mijnMijn, mijnVerbruikGegevens);
                }
              }
            }
          }
        }
      }
      return skipDag;
    }

    private DateTime ParseWerkUren(Mijn werkMijn, String gegevens) {
      String mijnGegevens = gegevens;
      DateTime skipDag = DateTime.Today;
      if (!String.IsNullOrEmpty(mijnGegevens)) {
        int index = mijnGegevens.IndexOf("20", 0);
        while (index > 0) {
          int startsearch = index + 1;
          int startpoint = index;
          int endpoint = mijnGegevens.IndexOf(Environment.NewLine, index);
          if (endpoint > startpoint) {
            int lineLength = endpoint - startpoint;
            String workline = mijnGegevens.Substring(startpoint, lineLength);
            String[] workitems = workline.Split('\t');
            if (workitems.Length == 1) {
              String[] workitemsTemp = workline.Split(' ');
              if (workitemsTemp.Length == 4) {
                workitems = new String[3];
                workitems[0] = workitemsTemp[0];
                workitems[1] = workitemsTemp[1] + ' ' + workitemsTemp[2];
                workitems[2] = workitemsTemp[3];
              }
            }
            if (workitems.Length == 3) {
              if (!String.IsNullOrEmpty(workitems[0])) {
                try {
                  DateTime werkDag = DateTime.ParseExact(workitems[0], datumFormat, provider);
                  if (skipDag == DateTime.Today) {
                    skipDag = werkDag;
                  }
                  string[] werkDuurElements = workitems[1].Split(' ');
                  if (werkDuurElements.Length == 2 && !String.IsNullOrEmpty(werkDuurElements[0])) {
                    int werkDuur = Int32.Parse(werkDuurElements[0]);
                    if (werkDuur == 1 && !String.IsNullOrEmpty(werkDuurElements[1]) && werkDuurElements[1] == "(×1.8)") {
                      werkDuur = werkDuur + 100;
                    }
                    if (werkDuur == 2 && !String.IsNullOrEmpty(werkDuurElements[1]) && werkDuurElements[1] == "(×1.6)") {
                      werkDuur = werkDuur + 100;
                    }
                    int aantalPersonen = Int32.Parse(workitems[2]);
                    werkMijn.WerkUren(werkDag, werkDuur, aantalPersonen);
                  }
                }
                catch (Exception) {
                }
              }
            }
          }
          index = mijnGegevens.IndexOf("20", startsearch);
        }
      }
      return skipDag;
    }

    private void ParseProductie(Mijn werkMijn, String gegevens) {
      String mijnGegevens = gegevens;
      if (!String.IsNullOrEmpty(mijnGegevens)) {
        int index = mijnGegevens.IndexOf("20", 0);
        while (index > 0) {
          int startsearch = index + 1;
          int startpoint = index;
          int endpoint = mijnGegevens.IndexOf(Environment.NewLine, index);
          if (endpoint > startpoint) {
            int lineLength = endpoint - startpoint;
            String workline = mijnGegevens.Substring(startpoint, lineLength);
            String[] workitems = workline.Split('\t');
            if (workitems.Length == 1) {
              workitems = workline.Split(' ');
            }
            if (workitems.Length == 2) {
              if (!String.IsNullOrEmpty(workitems[0])) {
                try {
                  DateTime werkDag = DateTime.ParseExact(workitems[0], datumFormat, provider);
                  if (werkMijn.mijnType == "goud") {
                    workitems[1].Replace(',', '.');
                    Decimal opbrengst = Decimal.Parse(workitems[1]);
                    (werkMijn as GoudMijn).Opbrengsten(werkDag, opbrengst);
                  }
                  else {
                    int opbrengst = Int32.Parse(workitems[1]);
                    (werkMijn as GrondstofMijn).Opbrengsten(werkDag, opbrengst);
                  }
                }
                catch (Exception) {
                }
              }
            }
          }
          index = mijnGegevens.IndexOf("20", startsearch);
        }
      }
    }

    private void ParseVerbruik(Mijn werkMijn, String gegevens) {
      String mijnGegevens = gegevens;
      if (!String.IsNullOrEmpty(mijnGegevens)) {
        int index = mijnGegevens.IndexOf("20", 0);
        while (index > 0) {
          int startsearch = index + 1;
          int startpoint = index;
          int endpoint = mijnGegevens.IndexOf(Environment.NewLine, index);
          if (endpoint > startpoint) {
            int lineLength = endpoint - startpoint;
            String workline = mijnGegevens.Substring(startpoint, lineLength);
            String[] workitems = workline.Split('\t');
            if (workitems.Length == 1) {
              workitems = workline.Split(' ');
            }

            if (workitems.Length == 3) {
              if (!String.IsNullOrEmpty(workitems[0])) {
                try {
                  DateTime werkDag = DateTime.ParseExact(workitems[0], datumFormat, provider);
                  int steenKosten = Int32.Parse(workitems[1]);
                  int ijzerKosten = Int32.Parse(workitems[2]);
                  werkMijn.SteenKosten(werkDag, steenKosten);
                  werkMijn.IjzerKosten(werkDag, ijzerKosten);
                }
                catch (Exception) {
                }
              }
            }
          }
          index = mijnGegevens.IndexOf("20", startsearch);
        }
      }
    }

    public String GetDagRapport(DateTime dag) {
      int steenopbrengst = Steenopbrengst(dag);
      int ijzeropbrengst = Ijzeropbrengst(dag);
      Decimal goudopbrengst = Goudopbrengst(dag);
      int kleiopbrengst = Kleiopbrengst(dag);

      int steenkosten = Steenkosten(dag);
      int ijzerkosten = Ijzerkosten(dag);

      int werk1uren = Werk1uren(dag);
      int werk1urenMob = Werk1urenMob(dag);
      int werk2uren = Werk2uren(dag);
      int werk2urenMob = Werk2urenMob(dag);
      int werk6uren = Werk6uren(dag);
      int werk10uren = Werk10uren(dag);
      int werk22uren = Werk22uren(dag);

      Decimal loonkosten = (werk1uren * loon1uur) + (werk1urenMob * loon1uurmob) + (werk2uren * loon2uur) + (werk2urenMob * loon2uurmob)
                         + (werk6uren * loon6uur) + (werk10uren * loon10uur) + (werk22uren * loon22uur);

      int steentotaal = steenopbrengst - steenkosten;
      int ijzertotaal = ijzeropbrengst - ijzerkosten;
      Decimal goudtotaal = goudopbrengst - loonkosten;
      int kleitotaal = kleiopbrengst;

      Decimal opbrengstenWaarde = (steentotaal * steenWaarde) + (ijzertotaal * ijzerWaarde) + (kleitotaal * kleiWaarde) +
                                  goudtotaal;

      String dagRapport = "[quote=\"";
      dagRapport += dag.ToString("m", provider);
      dagRapport += "\"]";
      dagRapport += Environment.NewLine;

      dagRapport += "[b]Opbrengsten[/b]";
      dagRapport += Environment.NewLine;

      dagRapport += "[list] [*]Steen: ";
      dagRapport += steenopbrengst;
      dagRapport += Environment.NewLine;

      dagRapport += "[*]IJzer: ";
      dagRapport += ijzeropbrengst;
      dagRapport += Environment.NewLine;

      dagRapport += "[*]Goud: fl. ";
      dagRapport += goudopbrengst.ToString("#.00").Replace(".", ",");
      dagRapport += Environment.NewLine;

      dagRapport += "[*]Klei: ";
      dagRapport += kleiopbrengst;
      dagRapport += Environment.NewLine;

      dagRapport += "[/list]";
      dagRapport += Environment.NewLine;

      dagRapport += "[b]Kosten[/b]";
      dagRapport += Environment.NewLine;

      dagRapport += "[list] [*]Steen: ";
      dagRapport += steenkosten;
      dagRapport += Environment.NewLine;

      dagRapport += "[*]IJzer: ";
      dagRapport += ijzerkosten;
      dagRapport += Environment.NewLine;

      dagRapport += "[*]Loonkost: fl ";
      dagRapport += loonkosten.ToString("#.00").Replace(".", ",");
      dagRapport += Environment.NewLine;

      dagRapport += "[/list]";
      dagRapport += Environment.NewLine;

      dagRapport += "[b]Netto opbrengst = ";
      if (opbrengstenWaarde > 0) {
        dagRapport += "[color=green]";
      }
      if (opbrengstenWaarde < 0) {
        dagRapport += "[color=red]";
      }
      dagRapport += " fl ";
      dagRapport += opbrengstenWaarde.ToString("#.00").Replace(".", ",");
      if (opbrengstenWaarde != 0) {
        dagRapport += "[/color]";
      }
      dagRapport += "[/b]";
      dagRapport += Environment.NewLine;

      dagRapport += "[list] [*]Steen: ";
      if (steentotaal > 0) {
        dagRapport += "[color=green]";
      }
      if (steentotaal < 0) {
        dagRapport += "[color=red]";
      }
      dagRapport += steentotaal;
      if (steentotaal != 0) {
        dagRapport += "[/color]";
      }
      dagRapport += Environment.NewLine;

      dagRapport += "[*]IJzer: ";
      if (ijzertotaal > 0) {
        dagRapport += "[color=green]";
      }
      if (ijzertotaal < 0) {
        dagRapport += "[color=red]";
      }
      dagRapport += ijzertotaal;
      if (ijzertotaal != 0) {
        dagRapport += "[/color]";
      }
      dagRapport += Environment.NewLine;

      dagRapport += "[*]Geld: ";
      if (goudtotaal > 0) {
        dagRapport += "[color=green]";
      }
      if (goudtotaal < 0) {
        dagRapport += "[color=red]";
      }
      dagRapport += " fl ";
      dagRapport += goudtotaal.ToString("#.00").Replace(".", ",");
      if (goudtotaal != 0) {
        dagRapport += "[/color]";
      }
      dagRapport += Environment.NewLine;

      dagRapport += "[*]Klei: ";
      if (kleitotaal > 0) {
        dagRapport += "[color=green]";
      }
      if (kleitotaal < 0) {
        dagRapport += "[color=red]";
      }
      dagRapport += kleitotaal;
      if (kleitotaal != 0) {
        dagRapport += "[/color]";
      }
      dagRapport += "[/list]";
      dagRapport += Environment.NewLine;

      dagRapport += "[b]Gewerkte uren[/b]";
      dagRapport += Environment.NewLine;

      dagRapport += "[list] [*]1 uur: ";
      dagRapport += werk1uren;
      dagRapport += " website";
      dagRapport += Environment.NewLine;

      dagRapport += "[*]1 uur: ";
      dagRapport += werk1urenMob;
      dagRapport += " mobiel";
      dagRapport += Environment.NewLine;

      dagRapport += "[*]2 uur: ";
      dagRapport += werk2uren;
      dagRapport += " website";
      dagRapport += Environment.NewLine;

      dagRapport += "[*]2 uur: ";
      dagRapport += werk2urenMob;
      dagRapport += " mobiel";
      dagRapport += Environment.NewLine;

      dagRapport += "[*]6 uur: ";
      dagRapport += werk6uren;
      dagRapport += Environment.NewLine;

      dagRapport += "[*]10 uur: ";
      dagRapport += werk10uren;
      dagRapport += Environment.NewLine;

      dagRapport += "[*]22 uur: ";
      dagRapport += werk22uren;
      dagRapport += Environment.NewLine;

      dagRapport += "[/list][/quote]";
      dagRapport += Environment.NewLine;

      return dagRapport;
    }

    public String GetWeekRapport(DateTime dag) {
      int steenopbrengst = Steenopbrengst(dag) + Steenopbrengst(dag.AddDays(1)) + Steenopbrengst(dag.AddDays(2)) +
                           Steenopbrengst(dag.AddDays(3)) + Steenopbrengst(dag.AddDays(4)) +
                           Steenopbrengst(dag.AddDays(5)) + Steenopbrengst(dag.AddDays(6));
      int ijzeropbrengst = Ijzeropbrengst(dag) + Ijzeropbrengst(dag.AddDays(1)) + Ijzeropbrengst(dag.AddDays(2)) +
                           Ijzeropbrengst(dag.AddDays(3)) + Ijzeropbrengst(dag.AddDays(4)) +
                           Ijzeropbrengst(dag.AddDays(5)) + Ijzeropbrengst(dag.AddDays(6));
      Decimal goudopbrengst = Goudopbrengst(dag) + Goudopbrengst(dag.AddDays(1)) + Goudopbrengst(dag.AddDays(2)) +
                              Goudopbrengst(dag.AddDays(3)) + Goudopbrengst(dag.AddDays(4)) +
                              Goudopbrengst(dag.AddDays(5)) + Goudopbrengst(dag.AddDays(6));
      int kleiopbrengst = Kleiopbrengst(dag) + Kleiopbrengst(dag.AddDays(1)) + Kleiopbrengst(dag.AddDays(2)) +
                          Kleiopbrengst(dag.AddDays(3)) + Kleiopbrengst(dag.AddDays(4)) + Kleiopbrengst(dag.AddDays(5)) +
                          Kleiopbrengst(dag.AddDays(6));

      int steenkosten = Steenkosten(dag) + Steenkosten(dag.AddDays(1)) + Steenkosten(dag.AddDays(2)) +
                        Steenkosten(dag.AddDays(3)) + Steenkosten(dag.AddDays(4)) + Steenkosten(dag.AddDays(5)) +
                        Steenkosten(dag.AddDays(6));
      int ijzerkosten = Ijzerkosten(dag) + Ijzerkosten(dag.AddDays(1)) + Ijzerkosten(dag.AddDays(2)) +
                        Ijzerkosten(dag.AddDays(3)) + Ijzerkosten(dag.AddDays(4)) + Ijzerkosten(dag.AddDays(5)) +
                        Ijzerkosten(dag.AddDays(6));

      int werk1uren = Werk1uren(dag) + Werk1uren(dag.AddDays(1)) + Werk1uren(dag.AddDays(2)) + Werk1uren(dag.AddDays(3)) +
                      Werk1uren(dag.AddDays(4)) + Werk1uren(dag.AddDays(5)) + Werk1uren(dag.AddDays(6));
      int werk1urenmob = Werk1urenMob(dag) + Werk1urenMob(dag.AddDays(1)) + Werk1urenMob(dag.AddDays(2)) + Werk1urenMob(dag.AddDays(3)) +
                         Werk1urenMob(dag.AddDays(4)) + Werk1urenMob(dag.AddDays(5)) + Werk1urenMob(dag.AddDays(6));
      int werk2uren = Werk2uren(dag) + Werk2uren(dag.AddDays(1)) + Werk2uren(dag.AddDays(2)) + Werk2uren(dag.AddDays(3)) +
                      Werk2uren(dag.AddDays(4)) + Werk2uren(dag.AddDays(5)) + Werk2uren(dag.AddDays(6));
      int werk2urenmob = Werk2urenMob(dag) + Werk2urenMob(dag.AddDays(1)) + Werk2urenMob(dag.AddDays(2)) + Werk2urenMob(dag.AddDays(3)) +
                         Werk2urenMob(dag.AddDays(4)) + Werk2urenMob(dag.AddDays(5)) + Werk2urenMob(dag.AddDays(6));
      int werk6uren = Werk6uren(dag) + Werk6uren(dag.AddDays(1)) + Werk6uren(dag.AddDays(2)) + Werk6uren(dag.AddDays(3)) +
                      Werk6uren(dag.AddDays(4)) + Werk6uren(dag.AddDays(5)) + Werk6uren(dag.AddDays(6));
      int werk10uren = Werk10uren(dag) + Werk10uren(dag.AddDays(1)) + Werk10uren(dag.AddDays(2)) +
                       Werk10uren(dag.AddDays(3)) + Werk10uren(dag.AddDays(4)) + Werk10uren(dag.AddDays(5)) +
                       Werk10uren(dag.AddDays(6));
      int werk22uren = Werk22uren(dag) + Werk22uren(dag.AddDays(1)) + Werk22uren(dag.AddDays(2)) +
                       Werk22uren(dag.AddDays(3)) + Werk22uren(dag.AddDays(4)) + Werk22uren(dag.AddDays(5)) +
                       Werk22uren(dag.AddDays(6));

      int oudwerk1uren = Werk1uren(dag.AddDays(-1)) + Werk1uren(dag.AddDays(-2)) + Werk1uren(dag.AddDays(-3)) +
                         Werk1uren(dag.AddDays(-4)) + Werk1uren(dag.AddDays(-5)) + Werk1uren(dag.AddDays(-6)) +
                         Werk1uren(dag.AddDays(-7));
      int oudwerk1urenmob = Werk1urenMob(dag.AddDays(-1)) + Werk1urenMob(dag.AddDays(-2)) + Werk1urenMob(dag.AddDays(-3)) +
                            Werk1urenMob(dag.AddDays(-4)) + Werk1urenMob(dag.AddDays(-5)) + Werk1urenMob(dag.AddDays(-6)) +
                            Werk1urenMob(dag.AddDays(-7));
      int oudwerk2uren = Werk2uren(dag.AddDays(-1)) + Werk2uren(dag.AddDays(-2)) + Werk2uren(dag.AddDays(-3)) +
                         Werk2uren(dag.AddDays(-4)) + Werk2uren(dag.AddDays(-5)) + Werk2uren(dag.AddDays(-6)) +
                         Werk2uren(dag.AddDays(-7));
      int oudwerk2urenmob = Werk2urenMob(dag.AddDays(-1)) + Werk2urenMob(dag.AddDays(-2)) + Werk2urenMob(dag.AddDays(-3)) +
                            Werk2urenMob(dag.AddDays(-4)) + Werk2urenMob(dag.AddDays(-5)) + Werk2urenMob(dag.AddDays(-6)) +
                            Werk2urenMob(dag.AddDays(-7));
      int oudwerk6uren = Werk6uren(dag.AddDays(-1)) + Werk6uren(dag.AddDays(-2)) + Werk6uren(dag.AddDays(-3)) +
                         Werk6uren(dag.AddDays(-4)) + Werk6uren(dag.AddDays(-5)) + Werk6uren(dag.AddDays(-6)) +
                         Werk6uren(dag.AddDays(-7));
      int oudwerk10uren = Werk10uren(dag.AddDays(-1)) + Werk10uren(dag.AddDays(-2)) + Werk10uren(dag.AddDays(-3)) +
                          Werk10uren(dag.AddDays(-4)) + Werk10uren(dag.AddDays(-5)) + Werk10uren(dag.AddDays(-6)) +
                          Werk10uren(dag.AddDays(-7));
      int oudwerk22uren = Werk22uren(dag.AddDays(-1)) + Werk22uren(dag.AddDays(-2)) + Werk22uren(dag.AddDays(-3)) +
                          Werk22uren(dag.AddDays(-4)) + Werk22uren(dag.AddDays(-5)) + Werk22uren(dag.AddDays(-6)) +
                          Werk22uren(dag.AddDays(-7));

      int uren1dif = werk1uren - oudwerk1uren;
      int uren1difmob = werk1urenmob - oudwerk1urenmob;
      int uren2dif = werk2uren - oudwerk2uren;
      int uren2difmob = werk2urenmob - oudwerk2urenmob;
      int uren6dif = werk6uren - oudwerk6uren;
      int uren10dif = werk10uren - oudwerk10uren;
      int uren22dif = werk22uren - oudwerk22uren;

      Decimal loonkosten = (werk1uren * loon1uur) + (werk1urenmob * loon1uurmob) + (werk2uren * loon2uur) + (werk2urenmob * loon2uurmob)
                         + (werk6uren * loon6uur) + (werk10uren * loon10uur) + (werk22uren * loon22uur);

      int steentotaal = steenopbrengst - steenkosten;
      int ijzertotaal = ijzeropbrengst - ijzerkosten;
      Decimal goudtotaal = goudopbrengst - loonkosten;
      int kleitotaal = kleiopbrengst;

      Decimal opbrengstenWaarde = (steentotaal * steenWaarde) + (ijzertotaal * ijzerWaarde) + (kleitotaal * kleiWaarde) +
                                  goudtotaal;

      String weekRapport = "[quote][rp]";
      weekRapport += Environment.NewLine;

      weekRapport += "[b][u][size=18]Mijnopbrengst van ";
      weekRapport += dag.ToString("m", provider);
      weekRapport += " t/m ";
      weekRapport += dag.AddDays(6).ToString("m", provider);
      weekRapport += "[/size][/u][/b]";
      weekRapport += Environment.NewLine;
      weekRapport += Environment.NewLine;
      weekRapport += Environment.NewLine;

      weekRapport += "[b]Opbrengsten[/b]";
      weekRapport += Environment.NewLine;

      weekRapport += "[list] [*]Steen: ";
      weekRapport += steenopbrengst;
      weekRapport += Environment.NewLine;

      weekRapport += "[*]IJzer: ";
      weekRapport += ijzeropbrengst;
      weekRapport += Environment.NewLine;

      weekRapport += "[*]Goud: fl. ";
      weekRapport += goudopbrengst.ToString("#.00").Replace(".", ",");
      weekRapport += Environment.NewLine;

      weekRapport += "[*]Klei: ";
      weekRapport += kleiopbrengst;
      weekRapport += Environment.NewLine;

      weekRapport += "[/list]";
      weekRapport += Environment.NewLine;

      weekRapport += "[b]Kosten[/b]";
      weekRapport += Environment.NewLine;

      weekRapport += "[list] [*]Steen: ";
      weekRapport += steenkosten;
      weekRapport += Environment.NewLine;

      weekRapport += "[*]IJzer: ";
      weekRapport += ijzerkosten;
      weekRapport += Environment.NewLine;

      weekRapport += "[*]Loonkost: fl ";
      weekRapport += loonkosten.ToString("#.00").Replace(".", ",");
      weekRapport += Environment.NewLine;

      weekRapport += "[/list]";
      weekRapport += Environment.NewLine;

      weekRapport += "[b]Netto opbrengst = ";
      if (opbrengstenWaarde > 0) {
        weekRapport += "[color=green]";
      }
      if (opbrengstenWaarde < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += " fl ";
      weekRapport += opbrengstenWaarde.ToString("#.00").Replace(".", ",");
      if (opbrengstenWaarde != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += "[/b]";
      weekRapport += Environment.NewLine;

      weekRapport += "[list] [*]Steen: ";
      if (steentotaal > 0) {
        weekRapport += "[color=green]";
      }
      if (steentotaal < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += steentotaal;
      if (steentotaal != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += Environment.NewLine;

      weekRapport += "[*]IJzer: ";
      if (ijzertotaal > 0) {
        weekRapport += "[color=green]";
      }
      if (ijzertotaal < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += ijzertotaal;
      if (ijzertotaal != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += Environment.NewLine;

      weekRapport += "[*]Geld: ";
      if (goudtotaal > 0) {
        weekRapport += "[color=green]";
      }
      if (goudtotaal < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += " fl ";
      weekRapport += goudtotaal.ToString("#.00").Replace(".", ",");
      if (goudtotaal != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += Environment.NewLine;

      weekRapport += "[*]Klei: ";
      if (kleitotaal > 0) {
        weekRapport += "[color=green]";
      }
      if (kleitotaal < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += kleitotaal;
      if (kleitotaal != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += "[/list]";
      weekRapport += Environment.NewLine;

      weekRapport += "[b]Gewerkte uren[/b]";
      weekRapport += Environment.NewLine;

      weekRapport += "[list] [*]1 uur: ";
      weekRapport += werk1uren;
      weekRapport += " (";
      if (uren1dif > 0) {
        weekRapport += "[color=green]";
      }
      if (uren1dif < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += uren1dif;
      if (uren1dif != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += ") website";
      weekRapport += Environment.NewLine;

      weekRapport += "[*]1 uur: ";
      weekRapport += werk1urenmob;
      weekRapport += " (";
      if (uren1difmob > 0) {
        weekRapport += "[color=green]";
      }
      if (uren1difmob < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += uren1difmob;
      if (uren1difmob != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += ") mobiel";
      weekRapport += Environment.NewLine;

      weekRapport += "[*]2 uur: ";
      weekRapport += werk2uren;
      weekRapport += " (";
      if (uren2dif > 0) {
        weekRapport += "[color=green]";
      }
      if (uren2dif < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += uren2dif;
      if (uren2dif != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += ") website";
      weekRapport += Environment.NewLine;

      weekRapport += "[*]2 uur: ";
      weekRapport += werk2urenmob;
      weekRapport += " (";
      if (uren2difmob > 0) {
        weekRapport += "[color=green]";
      }
      if (uren2difmob < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += uren2difmob;
      if (uren2difmob != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += ") mobiel";
      weekRapport += Environment.NewLine;

      weekRapport += "[*]6 uur: ";
      weekRapport += werk6uren;
      weekRapport += " (";
      if (uren6dif > 0) {
        weekRapport += "[color=green]";
      }
      if (uren6dif < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += uren6dif;
      if (uren6dif != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += ")";
      weekRapport += Environment.NewLine;

      weekRapport += "[*]10 uur: ";
      weekRapport += werk10uren;
      weekRapport += " (";
      if (uren10dif > 0) {
        weekRapport += "[color=green]";
      }
      if (uren10dif < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += uren10dif;
      if (uren10dif != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += ")";
      weekRapport += Environment.NewLine;

      weekRapport += "[*]22 uur: ";
      weekRapport += werk22uren;
      weekRapport += " (";
      if (uren22dif > 0) {
        weekRapport += "[color=green]";
      }
      if (uren22dif < 0) {
        weekRapport += "[color=red]";
      }
      weekRapport += uren22dif;
      if (uren22dif != 0) {
        weekRapport += "[/color]";
      }
      weekRapport += ")";
      weekRapport += Environment.NewLine;

      weekRapport += "[/list][/rp][/quote]";
      weekRapport += Environment.NewLine;

      return weekRapport;
    }

    public String GetOtherRapport(MijnenRapportRequest requestInfo) {
      String otherRapport = "[quote][rp]";
      DateTime calcDag = requestInfo.BeginDag;
      if (requestInfo.BeginDag > requestInfo.EindDag) {
        return "Einddatum mag niet voor de begindatum liggen.";
      }
      if (!requestInfo.IncludeMijn[0] && !requestInfo.IncludeMijn[1] && !requestInfo.IncludeMijn[2] &&
          !requestInfo.IncludeMijn[3] && !requestInfo.IncludeMijn[4] && !requestInfo.IncludeMijn[5]) {
        return "Geen mijnen geselecteerd, kan geen rapport maken.";
      }

      int steenopbrengst = 0;
      int ijzeropbrengst = 0;
      Decimal goudopbrengst = 0;
      int kleiopbrengst = 0;

      int steenkosten = 0;
      int ijzerkosten = 0;
      int werk1uren = 0;
      int werk1urenmob = 0;
      int werk2uren = 0;
      int werk2urenmob = 0;
      int werk6uren = 0;
      int werk10uren = 0;
      int werk22uren = 0;

      while (calcDag <= requestInfo.EindDag) {
        for (int mijnNummer = 0; mijnNummer < 6; mijnNummer++) {
          if (requestInfo.IncludeMijn[mijnNummer]) {
            Mijn dezeMijn = BepaalMijn(mijnNummer);
            if (dezeMijn.mijnType == "steen") steenopbrengst += ((SteenMijn)dezeMijn).GetOpbrengst(calcDag);
            if (dezeMijn.mijnType == "ijzer") ijzeropbrengst += ((IjzerMijn)dezeMijn).GetOpbrengst(calcDag);
            if (dezeMijn.mijnType == "klei") kleiopbrengst += ((KleiMijn)dezeMijn).GetOpbrengst(calcDag);
            if (dezeMijn.mijnType == "goud") goudopbrengst += ((GoudMijn)dezeMijn).GetOpbrengst(calcDag);
            steenkosten += dezeMijn.GetSteenKosten(calcDag);
            ijzerkosten += dezeMijn.GetIjzerKosten(calcDag);
            werk1uren += dezeMijn.getWerkUren(calcDag, 1);
            werk1urenmob += dezeMijn.getWerkUren(calcDag, 101);
            werk2uren += dezeMijn.getWerkUren(calcDag, 2);
            werk2urenmob += dezeMijn.getWerkUren(calcDag, 102);
            werk6uren += dezeMijn.getWerkUren(calcDag, 6);
            werk10uren += dezeMijn.getWerkUren(calcDag, 10);
            werk22uren += dezeMijn.getWerkUren(calcDag, 22);
          }
        }
        calcDag = calcDag.AddDays(1);
      }

      Decimal loonkosten = (werk1uren * loon1uur) + (werk1urenmob * loon1uurmob) + (werk2uren * loon2uur) + (werk2urenmob * loon2uurmob)
                         + (werk6uren * loon6uur) + (werk10uren * loon10uur) + (werk22uren * loon22uur);

      int steentotaal = steenopbrengst - steenkosten;
      int ijzertotaal = ijzeropbrengst - ijzerkosten;
      Decimal goudtotaal = goudopbrengst - loonkosten;
      int kleitotaal = kleiopbrengst;

      Decimal opbrengstenWaarde = (steentotaal * steenWaarde) + (ijzertotaal * ijzerWaarde) + (kleitotaal * kleiWaarde) +
                                  goudtotaal;

      otherRapport += "[b][u][size=18]Mijnopbrengst van ";
      otherRapport += requestInfo.BeginDag.ToString("m", provider);
      if (requestInfo.EindDag > requestInfo.BeginDag) {
        otherRapport += " t/m ";
        otherRapport += requestInfo.EindDag.ToString("m", provider);
      }
      otherRapport += "[/size][/u][/b]";
      otherRapport += Environment.NewLine;
      otherRapport += Environment.NewLine;
      otherRapport += Environment.NewLine;

      otherRapport += "[b]Opbrengsten[/b]";
      otherRapport += Environment.NewLine;

      otherRapport += "[list] [*]Steen: ";
      otherRapport += steenopbrengst;
      otherRapport += Environment.NewLine;

      otherRapport += "[*]IJzer: ";
      otherRapport += ijzeropbrengst;
      otherRapport += Environment.NewLine;

      otherRapport += "[*]Goud: fl. ";
      otherRapport += goudopbrengst.ToString("#.00").Replace(".", ",");
      otherRapport += Environment.NewLine;

      otherRapport += "[*]Klei: ";
      otherRapport += kleiopbrengst;
      otherRapport += Environment.NewLine;

      otherRapport += "[/list]";
      otherRapport += Environment.NewLine;

      otherRapport += "[b]Kosten[/b]";
      otherRapport += Environment.NewLine;

      otherRapport += "[list] [*]Steen: ";
      otherRapport += steenkosten;
      otherRapport += Environment.NewLine;

      otherRapport += "[*]IJzer: ";
      otherRapport += ijzerkosten;
      otherRapport += Environment.NewLine;

      otherRapport += "[*]Loonkost: fl ";
      otherRapport += loonkosten.ToString("#.00").Replace(".", ",");
      otherRapport += Environment.NewLine;

      otherRapport += "[/list]";
      otherRapport += Environment.NewLine;

      otherRapport += "[b]Netto opbrengst = ";
      if (opbrengstenWaarde > 0) {
        otherRapport += "[color=green]";
      }
      if (opbrengstenWaarde < 0) {
        otherRapport += "[color=red]";
      }
      otherRapport += " fl ";
      otherRapport += opbrengstenWaarde.ToString("#.00").Replace(".", ",");
      if (opbrengstenWaarde != 0) {
        otherRapport += "[/color]";
      }
      otherRapport += "[/b]";
      otherRapport += Environment.NewLine;

      otherRapport += "[list] [*]Steen: ";
      if (steentotaal > 0) {
        otherRapport += "[color=green]";
      }
      if (steentotaal < 0) {
        otherRapport += "[color=red]";
      }
      otherRapport += steentotaal;
      if (steentotaal != 0) {
        otherRapport += "[/color]";
      }
      otherRapport += Environment.NewLine;

      otherRapport += "[*]IJzer: ";
      if (ijzertotaal > 0) {
        otherRapport += "[color=green]";
      }
      if (ijzertotaal < 0) {
        otherRapport += "[color=red]";
      }
      otherRapport += ijzertotaal;
      if (ijzertotaal != 0) {
        otherRapport += "[/color]";
      }
      otherRapport += Environment.NewLine;

      otherRapport += "[*]Geld: ";
      if (goudtotaal > 0) {
        otherRapport += "[color=green]";
      }
      if (goudtotaal < 0) {
        otherRapport += "[color=red]";
      }
      otherRapport += " fl ";
      otherRapport += goudtotaal.ToString("#.00").Replace(".", ",");
      if (goudtotaal != 0) {
        otherRapport += "[/color]";
      }
      otherRapport += Environment.NewLine;

      otherRapport += "[*]Klei: ";
      if (kleitotaal > 0) {
        otherRapport += "[color=green]";
      }
      if (kleitotaal < 0) {
        otherRapport += "[color=red]";
      }
      otherRapport += kleitotaal;
      if (kleitotaal != 0) {
        otherRapport += "[/color]";
      }
      otherRapport += "[/list]";
      otherRapport += Environment.NewLine;

      otherRapport += "[b]Gewerkte uren[/b]";
      otherRapport += Environment.NewLine;

      otherRapport += "[list] [*]1 uur: ";
      otherRapport += werk1uren;
      otherRapport += " website";
      otherRapport += Environment.NewLine;

      otherRapport += "[*]1 uur: ";
      otherRapport += werk1urenmob;
      otherRapport += " mobiel";
      otherRapport += Environment.NewLine;

      otherRapport += "[*]2 uur: ";
      otherRapport += werk2uren;
      otherRapport += " website";
      otherRapport += Environment.NewLine;

      otherRapport += "[*]2 uur: ";
      otherRapport += werk2urenmob;
      otherRapport += " mobiel";
      otherRapport += Environment.NewLine;

      otherRapport += "[*]6 uur: ";
      otherRapport += werk6uren;
      otherRapport += Environment.NewLine;

      otherRapport += "[*]10 uur: ";
      otherRapport += werk10uren;
      otherRapport += Environment.NewLine;

      otherRapport += "[*]22 uur: ";
      otherRapport += werk22uren;
      otherRapport += Environment.NewLine;

      otherRapport += "[/list]";

      otherRapport += Environment.NewLine;
      otherRapport += Environment.NewLine;
      otherRapport += "[b]Mijnen in dit rapport meegenomen: [/b]";
      otherRapport += Environment.NewLine;
      if (requestInfo.IncludeMijn[0]) {
        otherRapport += "- Mijn 1: Goudmijn";
        otherRapport += Environment.NewLine;
      }
      if (requestInfo.IncludeMijn[1]) {
        otherRapport += "- Mijn 2: IJzermijn";
        otherRapport += Environment.NewLine;
      }
      if (requestInfo.IncludeMijn[2]) {
        otherRapport += "- Mijn 3: Goudmijn";
        otherRapport += Environment.NewLine;
      }
      if (requestInfo.IncludeMijn[3]) {
        otherRapport += "- Mijn 4: Steenmijn";
        otherRapport += Environment.NewLine;
      }
      if (requestInfo.IncludeMijn[4]) {
        otherRapport += "- Mijn 5: Steenmijn";
        otherRapport += Environment.NewLine;
      }
      if (requestInfo.IncludeMijn[5]) {
        otherRapport += "- Mijn 6: Kleimijn";
        otherRapport += Environment.NewLine;
      }
      otherRapport += Environment.NewLine;
      otherRapport += "[b]Rekenwaardes in dit rapport: [/b]";

      otherRapport += Environment.NewLine;
      otherRapport += "Steenwaarde: fl ";
      otherRapport += steenWaarde.ToString("#.00").Replace(".", ",");

      otherRapport += Environment.NewLine;
      otherRapport += "IJzerwaarde: fl ";
      otherRapport += ijzerWaarde.ToString("#.00").Replace(".", ",");

      otherRapport += Environment.NewLine;
      otherRapport += "Kleiwaarde: fl ";
      otherRapport += kleiWaarde.ToString("#.00").Replace(".", ",");

      otherRapport += Environment.NewLine;
      otherRapport += "[/rp][/quote]";
      otherRapport += Environment.NewLine;
      return otherRapport;
    }

    public String GetCSVRapport(MijnenRapportRequest requestInfo) {
      String otherRapport = "";

      DateTime calcDag = requestInfo.BeginDag;
      if (requestInfo.BeginDag > requestInfo.EindDag) {
        return "Einddatum mag niet voor de begindatum liggen.";
      }
      if (!requestInfo.IncludeMijn[0] && !requestInfo.IncludeMijn[1] && !requestInfo.IncludeMijn[2] &&
          !requestInfo.IncludeMijn[3] && !requestInfo.IncludeMijn[4] && !requestInfo.IncludeMijn[5]) {
        return "Geen mijnen geselecteerd, kan geen rapport maken.";
      }

      otherRapport += "Datum";
      for (int mijnNummer = 0; mijnNummer < 6; mijnNummer++) {
        if (requestInfo.IncludeMijn[mijnNummer]) {
          Mijn dezeMijn = BepaalMijn(mijnNummer);
          if (dezeMijn.mijnType == "steen") otherRapport += ";Steenopbrengst mijn " + (mijnNummer + 1);
          if (dezeMijn.mijnType == "ijzer") otherRapport += ";IJzeropbrengst mijn " + (mijnNummer + 1);
          if (dezeMijn.mijnType == "klei") otherRapport += ";Kleiopbrengst mijn " + (mijnNummer + 1);
          if (dezeMijn.mijnType == "goud") otherRapport += ";Goudopbrengst mijn " + (mijnNummer + 1);
          otherRapport += ";Steenkosten";
          otherRapport += ";IJzerkosten";
          otherRapport += ";Werk1uur";
          otherRapport += ";Werk2uur";
          otherRapport += ";Werk6uur";
          otherRapport += ";Werk10uur";
          otherRapport += ";Werk22uur";
        }
      }
      otherRapport += Environment.NewLine;


      while (calcDag <= requestInfo.EindDag) {
        otherRapport += calcDag.ToString("d", provider);
        for (int mijnNummer = 0; mijnNummer < 6; mijnNummer++) {
          if (requestInfo.IncludeMijn[mijnNummer]) {
            Mijn dezeMijn = BepaalMijn(mijnNummer);
            if (dezeMijn.mijnType == "steen") otherRapport += ";" + ((SteenMijn)dezeMijn).GetOpbrengst(calcDag);
            if (dezeMijn.mijnType == "ijzer") otherRapport += ";" + ((IjzerMijn)dezeMijn).GetOpbrengst(calcDag);
            if (dezeMijn.mijnType == "klei") otherRapport += ";" + ((KleiMijn)dezeMijn).GetOpbrengst(calcDag);
            if (dezeMijn.mijnType == "goud") otherRapport += ";" + ((GoudMijn)dezeMijn).GetOpbrengst(calcDag);
            otherRapport += ";" + dezeMijn.GetSteenKosten(calcDag);
            otherRapport += ";" + dezeMijn.GetIjzerKosten(calcDag);
            otherRapport += ";" + dezeMijn.getWerkUren(calcDag, 1);
            otherRapport += ";" + dezeMijn.getWerkUren(calcDag, 2);
            otherRapport += ";" + dezeMijn.getWerkUren(calcDag, 6);
            otherRapport += ";" + dezeMijn.getWerkUren(calcDag, 10);
            otherRapport += ";" + dezeMijn.getWerkUren(calcDag, 22);
          }
        }
        otherRapport += Environment.NewLine;
        calcDag = calcDag.AddDays(1);
      }

      return otherRapport;
    }

    private int Steenopbrengst(DateTime dag) {
      return mijn4.GetOpbrengst(dag) + mijn5.GetOpbrengst(dag);
    }

    private int Ijzeropbrengst(DateTime dag) {
      return mijn2.GetOpbrengst(dag);
    }

    private Decimal Goudopbrengst(DateTime dag) {
      return mijn1.GetOpbrengst(dag) + mijn3.GetOpbrengst(dag);
    }

    private int Kleiopbrengst(DateTime dag) {
      return mijn6.GetOpbrengst(dag);
    }

    private int Steenkosten(DateTime dag) {
      return mijn1.GetSteenKosten(dag) + mijn2.GetSteenKosten(dag) + mijn3.GetSteenKosten(dag) +
             mijn4.GetSteenKosten(dag) + mijn5.GetSteenKosten(dag) + mijn6.GetSteenKosten(dag);
    }

    private int Ijzerkosten(DateTime dag) {
      return mijn1.GetIjzerKosten(dag) + mijn2.GetIjzerKosten(dag) + mijn3.GetIjzerKosten(dag) +
             mijn4.GetIjzerKosten(dag) + mijn5.GetIjzerKosten(dag) + mijn6.GetIjzerKosten(dag);
    }

    private int Werk1uren(DateTime dag) {
      return mijn1.getWerkUren(dag, 1) + mijn2.getWerkUren(dag, 1) + mijn3.getWerkUren(dag, 1) +
             mijn4.getWerkUren(dag, 1) + mijn5.getWerkUren(dag, 1) + mijn6.getWerkUren(dag, 1);
    }

    private int Werk1urenMob(DateTime dag) {
      return mijn1.getWerkUren(dag, 101) + mijn2.getWerkUren(dag, 101) + mijn3.getWerkUren(dag, 101) +
             mijn4.getWerkUren(dag, 101) + mijn5.getWerkUren(dag, 101) + mijn6.getWerkUren(dag, 101);
    }

    private int Werk2uren(DateTime dag) {
      return mijn1.getWerkUren(dag, 2) + mijn2.getWerkUren(dag, 2) + mijn3.getWerkUren(dag, 2) +
             mijn4.getWerkUren(dag, 2) + mijn5.getWerkUren(dag, 2) + mijn6.getWerkUren(dag, 2);
    }

    private int Werk2urenMob(DateTime dag) {
      return mijn1.getWerkUren(dag, 102) + mijn2.getWerkUren(dag, 102) + mijn3.getWerkUren(dag, 102) +
             mijn4.getWerkUren(dag, 102) + mijn5.getWerkUren(dag, 102) + mijn6.getWerkUren(dag, 102);
    }

    private int Werk6uren(DateTime dag) {
      return mijn1.getWerkUren(dag, 6) + mijn2.getWerkUren(dag, 6) + mijn3.getWerkUren(dag, 6) +
             mijn4.getWerkUren(dag, 6) + mijn5.getWerkUren(dag, 6) + mijn6.getWerkUren(dag, 6);
    }

    private int Werk10uren(DateTime dag) {
      return mijn1.getWerkUren(dag, 10) + mijn2.getWerkUren(dag, 10) + mijn3.getWerkUren(dag, 10) +
             mijn4.getWerkUren(dag, 10) + mijn5.getWerkUren(dag, 10) + mijn6.getWerkUren(dag, 10);
    }

    private int Werk22uren(DateTime dag) {
      return mijn1.getWerkUren(dag, 22) + mijn2.getWerkUren(dag, 22) + mijn3.getWerkUren(dag, 22) +
             mijn4.getWerkUren(dag, 22) + mijn5.getWerkUren(dag, 22) + mijn6.getWerkUren(dag, 22);
    }

    private Mijn BepaalMijn(int mijnNummer) {
      if (mijnNummer == 0) return mijn1;
      if (mijnNummer == 1) return mijn2;
      if (mijnNummer == 2) return mijn3;
      if (mijnNummer == 3) return mijn4;
      if (mijnNummer == 4) return mijn5;
      return mijn6;
    }
  }
}
