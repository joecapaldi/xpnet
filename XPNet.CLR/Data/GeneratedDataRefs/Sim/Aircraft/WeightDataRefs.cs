using System;
using System.Collections.Generic;
using System.Text;

namespace XPNet.Data
{
    public class Sim_Aircraft_WeightDatarefs
    {
        private readonly IXPlaneData m_data;

        internal Sim_Aircraft_WeightDatarefs(IXPlaneData data)
        {
            m_data = data;
        }
    }
}