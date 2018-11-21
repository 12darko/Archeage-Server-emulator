﻿


using System;
using LocalCommons.Logging;
using LocalCommons.Utilities.Configuration;
using LocalCommons.World;

namespace ArcheAgeLogin.ArcheAge.Utilites.Configuration.Files
{
	/// <summary>
	/// Represents login.conf
	/// </summary>
	public class LoginConfFile : ConfFile
	{
		/// <summary>
		/// Location new characters start at.
		/// </summary>
		public Location StartLocation { get; protected set; }

		public void Load()
		{
			this.Require("system/conf/login.conf");

			this.StartLocation = this.LoadStartLocation();
		}

		/// <summary>
		/// Parses and returns start location from conf option.
		/// </summary>
		/// <returns></returns>
		private Location LoadStartLocation()
		{
			var startLocation = this.GetString("start_location", "w_gweonid_forest_1, -628, 260, -1025");
			var split = startLocation.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
			if (split.Length != 4)
			{
				Log.Warning("login.conf: Malformed start_location, using default location.");
				goto L_Default;
			}

			var mapName = split[0].Trim();
			var map = LoginServer.Instance.Data.MapDb.Find(mapName);
			if (map == null)
			{
				Log.Warning("login.conf: Start map '{0}' not found, using default location.", mapName);
				goto L_Default;
			}

			if (!int.TryParse(split[1], out var x) || !int.TryParse(split[2], out var y) || !int.TryParse(split[3], out var z))
			{
				Log.Warning("login.conf: Invalid coordinates for start_location, using default location.");
				goto L_Default;
			}

			return new Location(map.Id, x, y, z);

		L_Default:
			return new Location(1021, -628, 260, -1025);
		}
	}
}