using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Tools
{
    class NiunDiv
    {
        private long value;

        public NiunDiv(long vl)
        {
            this.value = vl;
        }

        public long Quotient()
        {
            return value / 256;
        }

        public long Remainder()
        {
            return value % 256;
        }
    }
}
