using System;
using System.Collections.Generic;
using System.Text;

namespace XPNet.Data
{
    public class Sim_Cockpit2_TemperatureDatarefs
    {
        private readonly IXPlaneData m_data;

        internal Sim_Cockpit2_TemperatureDatarefs(IXPlaneData data)
        {
            m_data = data;
        }
    }
}