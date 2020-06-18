using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MijnenRapportage.Models.Mijnen {
  [Serializable]
  public class GoudMijn : Mijn {
    private SortedList<DateTime, Decimal> opbrengsten = new SortedList<DateTime, Decimal>();

    public GoudMijn() {
      mijnType = "goud";
    }

    public virtual SortedList<DateTime, Decimal> OpbrengstenLijst {
      get { return opbrengsten; }
      set { opbrengsten = value; }
    }

    public void Opbrengsten(DateTime werkDag, Decimal aantal) {
      if (GetOpbrengst(werkDag) <= aantal) {
        if (opbrengsten.ContainsKey(werkDag)) {
          opbrengsten.Remove(werkDag);
        }
        opbrengsten.Add(werkDag, aantal);
      }
    }

    public Decimal GetOpbrengst(DateTime werkDag) {
      Decimal opbrengst = 0;
      if (!opbrengsten.TryGetValue(werkDag, out opbrengst)) {
        opbrengst = 0;
      }
      return opbrengst;
    }
  }
}
