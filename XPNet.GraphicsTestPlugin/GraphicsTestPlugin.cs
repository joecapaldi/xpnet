﻿using System;
using System.Linq;

namespace XPNet
{
	/// <summary>
	/// This is a testbed for getting the graphics going.
	/// </summary>
	[XPlanePlugin(
		name: "GraphicsTestPlugin",
		signature: "xpnet.plugins.graphics",
		description: "This should test the graphics."
	)]
	public class GraphicsTestPlugin : IXPlanePlugin
	{
		private const string TUGPATH = "lib/airport/vehicles/pushback/tug.obj";
		//private const string TUGPATH = "lib/airport/vehicles/pushback/*.obj";
		private readonly IXPlaneApi m_api;
		private readonly IXPProbe m_probe;
		private readonly IXPDrawingLoopHook m_drawingLoopHook;
		private IXPFlightLoopHook m_flightLoopHook;
		private IXPSceneryObject testTug;

		public GraphicsTestPlugin(IXPlaneApi api)
		{
			m_api = api ?? throw new ArgumentNullException(nameof(api));

			m_api.Log.Log("GraphicsTestPlugin: Displaytest started");
			m_drawingLoopHook = m_api.Display.RegisterDrawHook(DrawingHook, XPLMDrawingPhase.xplm_Phase_Airplanes, 0);
			m_flightLoopHook = m_api.Processing.RegisterFlightLoopHook(FlightLoopTime.FromCycles(1), SimLoaded);
			m_api.Log.Log("GraphicsTestPlugin: And now create a probe");
			m_probe = m_api.Scenery.CreateProbe();
			m_api.Log.Log("GraphicsTestPlugin: Probe created");
		}

		public void Dispose()
		{
			// Clean up whatever we attached / registered for / etc.

			m_drawingLoopHook.Dispose();
			testTug.Dispose();
			m_probe.Dispose();

            if (m_flightLoopHook != null)
                m_flightLoopHook.Dispose();
		}

		/// <summary>
		/// This is a hook that is called when the flight look starts to run,
		/// that is, when the simulator is fully loaded. Only then will <see cref="IXPlaneScenery.LookupObjects(string, float, float)"/>
		/// reasonable results, because the scenery indexes are loaded only after the 
		/// plugins are started.  After the work is done the flight loop hook is
		/// disposed because we no longer need it.
		/// </summary>
		private FlightLoopTime SimLoaded(TimeSpan elapsedTimeSinceLastCall, TimeSpan elapsedTimeSinceLastFlightLoop, int counter)
		{
			m_api.Log.Log($"GraphicsTestPlugin: Loading objects with path {TUGPATH}");

			var tugs = m_api.Scenery.LookupObjects(TUGPATH, 0, 0);
			foreach (var p in tugs)
				m_api.Log.Log($"GraphicsTestPlugin: Filename: {p}");

			testTug = m_api.Scenery.LoadObject(tugs.First());
			m_api.Log.Log($"GraphicsTestPlugin: Loaded and still living, reference is {testTug}");

			m_flightLoopHook.Dispose();
            m_flightLoopHook = null;

			return FlightLoopTime.Unscheduled;
		}

        private int DrawingHook(XPLMDrawingPhase inPhase, int inIsBefore)
		{
			m_api.Log.Log("GraphicsTestPlugin: Entering drawing hook");

			var (x, y, z) = m_api.Graphics.WorldToLocal(47.439444, 19.261944, 0);
			var res = m_probe.ProbeTerrainXYZ((float)x, 0, (float)z);
			m_api.Log.Log($"Probed terrain, got result {res.LocationY} with code {res.Result}");

			var (lat, lon, alt) = m_api.Graphics.LocalToWorld(res.LocationX, res.LocationY, res.LocationZ);

			testTug.Draw(0, 0, new XPLMDrawInfo_t[] { new XPLMDrawInfo_t(res.LocationX, res.LocationY, res.LocationZ, 0.0f, 0.0f, 0.0f) });
			return 1;
		}

		public void Enable()
		{
			// Nothing to do b/c we don't have any interesting work going on.
		}

		public void Disable()
		{
			// Nothing to do b/c we don't have any interesting work going on.
		}
	}
}