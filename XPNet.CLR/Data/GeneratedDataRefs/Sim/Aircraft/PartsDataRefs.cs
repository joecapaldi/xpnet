using System;
using System.Collections.Generic;
using System.Text;

namespace XPNet.Data
{
    public class Sim_Aircraft_PartsDatarefs
    {
        private readonly IXPlaneData m_data;

        internal Sim_Aircraft_PartsDatarefs(IXPlaneData data)
        {
            m_data = data;
        }
    }
}