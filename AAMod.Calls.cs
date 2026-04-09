using AAModClassic.Music;
using AAModClassic.Projectiles;
using AAModClassic.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic
{
    public partial class AAMod : Mod
    {
        public override object Call(params object[] args)
        {
            try
            {
                if (args.Length <= 0 || args[0] is not string)
                    return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD NAME! First param MUST be a method name!");

                string methodName = (string)args[0];

                switch (methodName)
                {
                    case "Downed": //returns a Func which will return a downed value based on player and name.
                        if (args.Length <= 1)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args.Length} is invalid for {methodName}. Must be at least 1");
                            return null;
                        }
                        if (args[1] is not string)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[1].GetType().Name} is invalid for the first argument of {methodName}. Must be a string");
                            return null;
                        }

                        string name = (string)args[1];
                        if (this.TryFind<ModNPC>(name, out var npc))
                            return npc.BeenKilled();
                        else
                        {
                            Logger.Error($"Ancients Awakened Call Error: An NPC named {name} could not be found.");
                            return null;
                        }
                    case "InZone": //returns a Func which will return a zone value based on player and name.
                        if (args.Length <= 2)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args.Length} is invalid for {methodName}. Must be at least 2");
                            return null;
                        }
                        if (args[1] is not string)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[1].GetType().Name} is invalid for the first argument of {methodName}. Must be a string");
                            return null;
                        }
                        if (args[2] is not Player)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[2].GetType().Name} is invalid for the second argument of {methodName}. Must be a Player");
                            return null;
                        }

                        name = ((string)args[1]).ToLower();
                        AAPlayer aap = ((Player)args[2]).GetModPlayer<AAPlayer>();

                        return name switch
                        {
                            "mire" => aap.ZoneMire,
                            "lake" => aap.ZoneRisingMoonLake,
                            "inferno" => aap.ZoneInferno,
                            "pagoda" => aap.ZoneRisingSunPagoda,
                            "ship" => aap.ZoneShip,
                            "storm" => aap.ZoneStorm,
                            "void" => aap.ZoneVoid,
                            "mush" => aap.ZoneMush,
                            "terrarium" => aap.Terrarium,
                            _ => false,
                        };
                    case "ReplaceTrack":
                        if (args.Length <= 2)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args.Length} is invalid for {methodName}. Must be at least 2");
                            return null;
                        }
                        if(args[1] is not string)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[1].GetType().Name} is invalid for the first argument of {methodName}. Must be a string");
                            return null;
                        }
                        if(args[2] is not int)
                        {
                            Logger.Error($"Ancients Awakened Call Error: {args[2].GetType().Name} is invalid for the second argument of {methodName}. Must be an int");
                            return null;
                        }

                        string key = (string)args[1];
                        int slot = (int)args[2];
                        return MusicManagementSystem.ReplaceTrack(key, slot);
                    default:
                        Logger.Error($"Ancients Awakened Call Error: {methodName} does not exist.");
                        return null;
                }
            }
            catch(Exception e)
            {
                Logger.Error("Ancients Awakened Call Error: " + e.Message + "\n" + e.StackTrace);
                return null;
            }
        }
    }
}
