using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using System;
using System.Reflection;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic.CrossMod
{
    public abstract class CrossoverItem : BaseAAItem
    {
        public string crossoverModName = "(N/A)";

        public override void ModifyTooltips(List<TooltipLine> list)
        {
            if (!ModLoader.TryGetMod(crossoverModName, out _))
            {
                TooltipLine error = new TooltipLine(Mod, "Error", "WARNING: ITEM WILL NOT FUNCTION WITHOUT " + crossoverModName.ToUpper() + " ENABLED!")
                {
                    OverrideColor = new Color(255, 50, 50)
                };
                list.Add(error);
            }
        }
    }

    public class ModSupportPlayer : ModPlayer
    {
        private static Mod Redeption = null;
        private static Mod Thorium = null;

        public override void Load()
        {
            if (!ModLoader.TryGetMod("Redemption", out Redeption))
                Redeption = null;
            if (!ModLoader.TryGetMod("ThoriumMod", out Thorium))
                Thorium = null;
        }

        #region Thorium
        public float Thorium_radiantBoost
        {
            get
            {
                if (Thorium != null)
                {
                    float? boost = (float?)Thorium.Call("GetRadiantBoost", Player.whoAmI);
                    if (boost != null) return (float)boost;
                }
                return 1f;
            }
            set
            {
                if (Thorium != null)
                {
                    Thorium.Call("SetRadiantBoost", Player.whoAmI, value);
                }
            }
        }
        public int Thorium_radiantCrit
        {
            get
            {
                if (Thorium != null)
                {
                    int? boost = (int?)Thorium.Call("GetRadiantCrit", Player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (Thorium != null)
                {
                    Thorium.Call("SetRadiantCrit", Player.whoAmI, value);
                }
            }
        }
        public int Thorium_healBonus
        {
            get
            {
                if (Thorium != null)
                {
                    int? boost = (int?)Thorium.Call("GetHealBonus", Player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (Thorium != null)
                {
                    Thorium.Call("SetHealBonus", Player.whoAmI, value);
                }
            }
        }
        #endregion

        #region Redemption

        public float Redemption_druidicBoost
        {
            get
            {
                if (Redeption != null)
                {
                    float? boost = (float?)Redeption.Call("GetDruidicBoost", Player.whoAmI);
                    if (boost != null) return (float)boost;
                }
                return 1f;
            }
            set
            {
                if (Redeption != null)
                {
                    Redeption.Call("SetDruidicBoost", Player.whoAmI, value);
                }
            }
        }
        public int Redemption_druidicCrit
        {
            get
            {
                if (Redeption != null)
                {
                    int? boost = (int?)Redeption.Call("GetDruidicCrit", Player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (Redeption != null)
                {
                    Redeption.Call("SetDruidicCrit", Player.whoAmI, value);
                }
            }
        }

        #endregion
    }
}