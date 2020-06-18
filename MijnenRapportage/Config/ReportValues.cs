using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MijnenRapportage.Config {
  [Serializable]
  public class ReportValues {
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
  }
}
