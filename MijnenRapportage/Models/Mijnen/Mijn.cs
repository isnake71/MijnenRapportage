using System;
using System.Collections.Generic;
using System.Windows;

namespace MijnenRapportage.Models.Mijnen {
  [Serializable]
  public class Mijn {
    #region Fields

    private SortedList<DateTime, int> werk1uur = new SortedList<DateTime, int>();
    private SortedList<DateTime, int> werk1uurMob = new SortedList<DateTime, int>();
    private SortedList<DateTime, int> werk2uur = new SortedList<DateTime, int>();
    private SortedList<DateTime, int> werk2uurMob = new SortedList<DateTime, int>();
    private SortedList<DateTime, int> werk6uur = new SortedList<DateTime, int>();
    private SortedList<DateTime, int> werk10uur = new SortedList<DateTime, int>();
    private SortedList<DateTime, int> werk22uur = new SortedList<DateTime, int>();

    private SortedList<DateTime, int> steenKosten = new SortedList<DateTime, int>();
    private SortedList<DateTime, int> ijzerKosten = new SortedList<DateTime, int>();

    public String mijnType;
    public Decimal loon1uur;
    public Decimal loon1uurmob;
    public Decimal loon2uur;
    public Decimal loon2uurmob;
    public Decimal loon6uur;
    public Decimal loon10uur;
    public Decimal loon22uur;
    #endregion

    #region Properties

    public virtual SortedList<DateTime, int> Werk1Uur {
      get { return werk1uur; }
      set { werk1uur = value; }
    }
    public virtual SortedList<DateTime, int> Werk1UurMob {
      get { return werk1uurMob; }
      set { werk1uurMob = value; }
    }

    public virtual SortedList<DateTime, int> Werk2Uur {
      get { return werk2uur; }
      set { werk2uur = value; }
    }
    public virtual SortedList<DateTime, int> Werk2UurMob {
      get { return werk2uurMob; }
      set { werk2uurMob = value; }
    }
    public virtual SortedList<DateTime, int> Werk6Uur {
      get { return werk6uur; }
      set { werk6uur = value; }
    }

    public virtual SortedList<DateTime, int> Werk10Uur {
      get { return werk10uur; }
      set { werk10uur = value; }
    }

    public virtual SortedList<DateTime, int> Werk22Uur {
      get { return werk22uur; }
      set { werk22uur = value; }
    }

    public virtual SortedList<DateTime, int> SteenKostenLijst {
      get { return steenKosten; }
      set { steenKosten = value; }
    }

    public virtual SortedList<DateTime, int> IJzerKostenLijst {
      get { return ijzerKosten; }
      set { ijzerKosten = value; }
    }

    #endregion

    #region Methods
    public void reInitMijn() {
      if (werk1uur == null) werk1uur = new SortedList<DateTime, int>();
      if (werk1uurMob == null) werk1uurMob = new SortedList<DateTime, int>();
      if (werk2uur == null) werk2uur = new SortedList<DateTime, int>();
      if (werk2uurMob == null) werk2uurMob = new SortedList<DateTime, int>();
      if (werk6uur == null) werk6uur = new SortedList<DateTime, int>();
      if (werk10uur == null) werk10uur = new SortedList<DateTime, int>();
      if (werk22uur == null) werk22uur = new SortedList<DateTime, int>();

      if (steenKosten == null) steenKosten = new SortedList<DateTime, int>();
      if (ijzerKosten == null) ijzerKosten = new SortedList<DateTime, int>();
    }

    public void addMijnKosten(Mijn lmijn, DateTime skipDag) {
      foreach (var uren in lmijn.Werk1Uur) {
        if (uren.Key != skipDag) {
          if (Werk1Uur.ContainsKey(uren.Key)) {
            Werk1Uur.Remove(uren.Key);
          }
          Werk1Uur.Add(uren.Key, uren.Value);
        }
      }
      foreach (var uren in lmijn.Werk1UurMob) {
        if (uren.Key != skipDag) {
          if (Werk1UurMob != null && Werk1UurMob.ContainsKey(uren.Key)) {
            Werk1UurMob.Remove(uren.Key);
          }
          Werk1UurMob.Add(uren.Key, uren.Value);
        }
      }
      foreach (var uren in lmijn.Werk2Uur) {
        if (uren.Key != skipDag) {
          if (Werk2Uur.ContainsKey(uren.Key)) {
            Werk2Uur.Remove(uren.Key);
          }
          Werk2Uur.Add(uren.Key, uren.Value);
        }
      }
      foreach (var uren in lmijn.Werk2UurMob) {
        if (uren.Key != skipDag) {
          if (Werk2UurMob.ContainsKey(uren.Key)) {
            Werk2UurMob.Remove(uren.Key);
          }
          Werk2UurMob.Add(uren.Key, uren.Value);
        }
      }
      foreach (var uren in lmijn.Werk6Uur) {
        if (uren.Key != skipDag) {
          if (Werk6Uur.ContainsKey(uren.Key)) {
            Werk6Uur.Remove(uren.Key);
          }
          Werk6Uur.Add(uren.Key, uren.Value);
        }
      }
      foreach (var uren in lmijn.Werk10Uur) {
        if (uren.Key != skipDag) {
          if (Werk10Uur.ContainsKey(uren.Key)) {
            Werk10Uur.Remove(uren.Key);
          }
          Werk10Uur.Add(uren.Key, uren.Value);
        }
      }
      foreach (var uren in lmijn.Werk22Uur) {
        if (uren.Key != skipDag) {
          if (Werk22Uur.ContainsKey(uren.Key)) {
            Werk22Uur.Remove(uren.Key);
          }
          Werk22Uur.Add(uren.Key, uren.Value);
        }
      }
      foreach (var kosten in lmijn.SteenKostenLijst) {
        if (kosten.Key != skipDag) {
          if (SteenKostenLijst.ContainsKey(kosten.Key)) {
            SteenKostenLijst.Remove(kosten.Key);
          }
          SteenKostenLijst.Add(kosten.Key, kosten.Value);
        }
      }
      foreach (var kosten in lmijn.IJzerKostenLijst) {
        if (kosten.Key != skipDag) {
          if (IJzerKostenLijst.ContainsKey(kosten.Key)) {
            IJzerKostenLijst.Remove(kosten.Key);
          }
          IJzerKostenLijst.Add(kosten.Key, kosten.Value);
        }
      }
    }

    public void WerkUren(DateTime werkDag, int soort, int aantal) {
      switch (soort) {
        case 1:
          if (Werk1Uur.ContainsKey(werkDag)) {
            aantal += getWerkUren(werkDag, soort);
            Werk1Uur.Remove(werkDag);
          }
          Werk1Uur.Add(werkDag, aantal);
          break;
        case 101:
          if (Werk1UurMob.ContainsKey(werkDag)) {
            aantal += getWerkUren(werkDag, soort);
            Werk1UurMob.Remove(werkDag);
          }
          Werk1UurMob.Add(werkDag, aantal);
          break;
        case 2:
          if (Werk2Uur.ContainsKey(werkDag)) {
            aantal += getWerkUren(werkDag, soort);
            Werk2Uur.Remove(werkDag);
          }
          Werk2Uur.Add(werkDag, aantal);
          break;
        case 102:
          if (Werk2UurMob.ContainsKey(werkDag)) {
            aantal += getWerkUren(werkDag, soort);
            Werk2UurMob.Remove(werkDag);
          }
          Werk2UurMob.Add(werkDag, aantal);
          break;
        case 6:
          if (Werk6Uur.ContainsKey(werkDag)) {
            aantal += getWerkUren(werkDag, soort);
            Werk6Uur.Remove(werkDag);
          }
          Werk6Uur.Add(werkDag, aantal);
          break;
        case 10:
          if (Werk10Uur.ContainsKey(werkDag)) {
            aantal += getWerkUren(werkDag, soort);
            Werk10Uur.Remove(werkDag);
          }
          Werk10Uur.Add(werkDag, aantal);
          break;
        case 22:
          if (Werk22Uur.ContainsKey(werkDag)) {
            aantal += getWerkUren(werkDag, soort);
            Werk22Uur.Remove(werkDag);
          }
          Werk22Uur.Add(werkDag, aantal);
          break;
        default:
          string bericht = String.Concat("Onverwacht aantal werkuren: ", soort.ToString());
          MessageBox.Show(bericht);
          break;
      }
    }

    public int getWerkUren(DateTime werkDag, int soort) {
      var kosten = 0;
      switch (soort) {
        case 1:
          if (!Werk1Uur.TryGetValue(werkDag, out kosten)) {
            kosten = 0;
          }
          break;
        case 101:
          if (!Werk1UurMob.TryGetValue(werkDag, out kosten)) {
            kosten = 0;
          }
          break;
        case 2:
          if (!Werk2Uur.TryGetValue(werkDag, out kosten)) {
            kosten = 0;
          }
          break;
        case 102:
          if (!Werk2UurMob.TryGetValue(werkDag, out kosten)) {
            kosten = 0;
          }
          break;
        case 6:
          if (!Werk6Uur.TryGetValue(werkDag, out kosten)) {
            kosten = 0;
          }
          break;
        case 10:
          if (!Werk10Uur.TryGetValue(werkDag, out kosten)) {
            kosten = 0;
          }
          break;
        case 22:
          if (!Werk22Uur.TryGetValue(werkDag, out kosten)) {
            kosten = 0;
          }
          break;
        default:
          kosten = 0;
          break;
      }
      return kosten;
    }

    public Decimal getLoonKosten(DateTime werkDag, int soort) {
      Decimal kosten = 0;
      Decimal loon = 0;
      int uren = getWerkUren(werkDag, soort);
      switch (soort) {
        case 1:
          loon = loon1uur;
          break;
        case 101:
          loon = loon1uurmob;
          break;
        case 2:
          loon = loon2uur;
          break;
        case 102:
          loon = loon2uurmob;
          break;
        case 6:
          loon = loon6uur;
          break;
        case 10:
          loon = loon10uur;
          break;
        case 22:
          loon = loon22uur;
          break;
        default:
          loon = 0;
          break;
      }
      kosten = loon * uren;
      return kosten;
    }

    public void SteenKosten(DateTime werkDag, int aantal) {
      if (steenKosten.ContainsKey(werkDag)) {
        steenKosten.Remove(werkDag);
      }
      steenKosten.Add(werkDag, aantal);
    }

    public void IjzerKosten(DateTime werkDag, int aantal) {
      if (ijzerKosten.ContainsKey(werkDag)) {
        ijzerKosten.Remove(werkDag);
      }
      ijzerKosten.Add(werkDag, aantal);
    }

    public int GetSteenKosten(DateTime werkDag) {
      var kosten = 0;
      if (!steenKosten.TryGetValue(werkDag, out kosten)) {
        kosten = 0;
      }
      return kosten;
    }

    public int GetIjzerKosten(DateTime werkDag) {
      var kosten = 0;
      if (!ijzerKosten.TryGetValue(werkDag, out kosten)) {
        kosten = 0;
      }
      return kosten;
    }
  }

  #endregion
}
