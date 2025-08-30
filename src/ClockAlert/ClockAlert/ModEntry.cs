using System;
using Microsoft.Xna.Framework;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewModdingAPI.Utilities;
using StardewValley;
using xTile.Dimensions;

namespace ClockAlert
{
    internal sealed class ModEntry : Mod
    {
        public override void Entry(IModHelper helper)
        {
            helper.Events.GameLoop.TimeChanged += ShowAlert;
        }

        private static int GetTime()
        {
            return Game1.timeOfDay;
        }

        private static string GetCurrentLocation()
        {
            return Game1.currentLocation.Name;
        }

        private static string MessageTime(IModHelper helper, int time, string location)
        {
            // if player is away from farm
            if (location != "farm")
            {
                // time messages
                return time switch
                {
                    2200 => helper.Translation.Get("time.2200"),
                    2300 => helper.Translation.Get("time.2300"),
                    2400 => helper.Translation.Get("time.2400"),
                    _ => string.Empty,
                };
            }

            return string.Empty;
        }

        private void ShowAlert(object? sender, TimeChangedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            int time = GetTime();
            string location = GetCurrentLocation();
            string message = MessageTime(Helper, time, location);

            if (string.IsNullOrEmpty(message))
                return;

            // add message to game
            Game1.addHUDMessage(new HUDMessage($"{ message }", 2));
            // and play sound
            Game1.playSound("detector");
        }
    }
}
