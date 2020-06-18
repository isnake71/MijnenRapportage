using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MijnenRapportage.Models.Mijnen
{
  [Serializable]
  public class IjzerMijn : GrondstofMijn
  {
    public IjzerMijn()
    {
      mijnType = "ijzer";
    }
  }
}
