using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MijnenRapportage.Models.Mijnen {
  [Serializable]
  public class GrondstofMijn : Mijn {
    private SortedList<DateTime, int> opbrengsten = new SortedList<DateTime, int>();

    public virtual SortedList<DateTime, int> OpbrengstenLijst {
      get { return opbrengsten; }
      set { opbrengsten = value; }
    }

    public void Opbrengsten(DateTime werkDag, int aantal) {
      if (GetOpbrengst(werkDag) <= aantal) {
        if (opbrengsten.ContainsKey(werkDag)) {
          opbrengsten.Remove(werkDag);
        }
        opbrengsten.Add(werkDag, aantal);
      }
    }

    public int GetOpbrengst(DateTime werkDag) {
      int opbrengst = 0;
      if (!opbrengsten.TryGetValue(werkDag, out opbrengst)) {
        opbrengst = 0;
      }
      return opbrengst;
    }
  }
}

