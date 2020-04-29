using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia
{
    [Serializable]
    public class DropDownItem : IEquatable<DropDownItem>
    {
        public int? Code { get; set; }
        public string Desc { get; set; }

        public DropDownItem()
        {

        }

        public DropDownItem(int? code, string desc)
        {
            this.Code = code;
            this.Desc = desc;
        }

        public bool Equals(DropDownItem other)
        {
            if (this.Code == other.Code)
            {
                return true;
            }
            else
            {
               return false;
            }
        }
    }
}
