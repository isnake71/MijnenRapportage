using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace MijnenRapportage.Models.Overzichten {
  public class MijnenRapportRequest
  {
    public DateTime BeginDag;
    public DateTime EindDag;
    public Boolean[] IncludeMijn = new Boolean[6];
  }
}
