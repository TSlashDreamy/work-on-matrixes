using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    [Serializable]
    class ViolationException : Exception 
    {
        public ViolationException() { }

        public ViolationException(string copyright)
            : base(String.Format("Rights violation detected!\n{0}", copyright))
        {

        }
    }
}
