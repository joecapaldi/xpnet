using System;
using System.Collections.Generic;
using System.Text;

namespace XPNet.Data
{
    public class Sim_Flightmodel_PositionDatarefs
    {
        private readonly IXPlaneData m_data;

        internal Sim_Flightmodel_PositionDatarefs(IXPlaneData data)
        {
            m_data = data;
        }
    }
}