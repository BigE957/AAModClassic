using AAModClassic.Projectiles;
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
            if (args.Length <= 0 || args[0] is not string)
                return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD NAME! First param MUST be a method name!");

            string methodName = (string)args[0];

            switch (methodName)
            {
                case "Downed": //returns a Func which will return a downed value based on player and name.
                    if (args.Length <= 1 || args[1] is not string)
                        return new Exception("ANCIENTS AWAKENED CALL ERROR: NO DOWNED NAME! Second param of 'Downed' MUST be a name!");

                    string name = (string)args[1];
                    return name switch
                    {
                        "mushroommonarch" => AAWorld.downedMonarch,
                        "broodmother" => AAWorld.downedBrood,
                        "hydra" => AAWorld.downedHydra,
                        "grips" or "gripsofchaos" => AAWorld.downedGrips,
                        "tode" => AAWorld.downedToad,
                        "daybringer" => AAWorld.downedDB,
                        "nightcrawler" => AAWorld.downedNC,
                        "equinox" => AAWorld.downedEquinox,
                        "ancient" or "ancientany" => AAWorld.downedAncient,
                        "sancient" or "sancientany" => AAWorld.downedSAncient,
                        "gripsS" or "akuma" => AAWorld.downedAkuma,
                        "yamata" => AAWorld.downedYamata,
                        "zero" => AAWorld.downedZero,
                        "shen" or "shendoragon" => AAWorld.downedShen,
                        _ => false,
                    };
                case "InZone": //returns a Func which will return a zone value based on player and name.

                    if (args.Length <= 2 || args[1] is not string || args[2] is not Player)
                        return new Exception("ANCIENTS AWAKENED CALL ERROR:");

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
                default:
                    return new Exception("ANCIENTS AWAKENED CALL ERROR: NO METHOD FOUND: " + methodName);
            }
        }
    }
}
