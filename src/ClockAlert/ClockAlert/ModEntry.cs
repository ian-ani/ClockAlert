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
            helper.Events.GameLoop.TimeChanged += this.ShowAlert;
        }

        private static int GetTime()
        {
            // get current time
            return Game1.timeOfDay;
        }

        private static string MessageTime(int time, string location)
        {
            // if player is away from farm
            if (location != "farm")
            {
                // time messages
                switch (time)
                {
                    case 2200:
                        return "Ya son las 10 de la noche.";
                    case 2300:
                        return "Son las 11 de la noche.";
                    case 2400:
                        return "Cuidado. Son las 12 de la noche, vete a casa.";
                    default:
                        break;
                }
            }

            return "";
        }

        private void ShowAlert(object? sender, TimeChangedEventArgs e)
        {
            // ignore if player hasn't loaded a save yet
            if (!Context.IsWorldReady)
                return;

            int time = GetTime();
            string location = Game1.currentLocation.Name;
            string message = MessageTime(time, location);

            if (message != "")
            {
                // add message to game
                Game1.addHUDMessage(new HUDMessage($"{message}", 2));
                // and play sound
                Game1.playSound("detector");
            }
        }
    }
}
