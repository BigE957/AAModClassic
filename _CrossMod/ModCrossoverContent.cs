using AAModClassic._CrossMod.Thorium;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._CrossMod
{
    public abstract class CrossoverItem : ModItem
    {
        public abstract string CrossoverModName { get; }

        public override bool IsLoadingEnabled(Mod mod) => ModLoader.HasMod(CrossoverModName);
    }

    public class ModSupportPlayer : ModPlayer
    {
        public static Mod Redeption = null;

        public override void Load()
        {
            if (!ModLoader.TryGetMod("Redemption", out Redeption))
                Redeption = null;
        }

        #region Thorium
        public float Thorium_radiantBoost
        {
            get
            {
                if (ThoriumMod.IsEnabled)
                {
                    float? boost = (float?)ThoriumMod.Call("GetRadiantBoost", Player.whoAmI);
                    if (boost != null) return (float)boost;
                }
                return 1f;
            }
            set
            {
                if (ThoriumMod.IsEnabled)
                {
                    ThoriumMod.Call("SetRadiantBoost", Player.whoAmI, value);
                }
            }
        }
        public int Thorium_radiantCrit
        {
            get
            {
                if (ThoriumMod.IsEnabled)
                {
                    int? boost = (int?)ThoriumMod.Call("GetRadiantCrit", Player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ThoriumMod.IsEnabled)
                {
                    ThoriumMod.Call("SetRadiantCrit", Player.whoAmI, value);
                }
            }
        }
        public int Thorium_healBonus
        {
            get
            {
                if (ThoriumMod.IsEnabled)
                {
                    int? boost = (int?)ThoriumMod.Call("GetHealBonus", Player.whoAmI);
                    if (boost != null) return (int)boost;
                }
                return 0;
            }
            set
            {
                if (ThoriumMod.IsEnabled)
                {
                    ThoriumMod.Call("SetHealBonus", Player.whoAmI, value);
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