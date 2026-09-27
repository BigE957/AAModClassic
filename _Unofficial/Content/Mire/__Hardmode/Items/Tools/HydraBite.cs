using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unofficial.Content.Mire.__Hardmode.Items.Tools
{
    public class HydraBite : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon's Grip");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.IlluminantHook);
            Item.shoot = ModContent.ProjectileType<HydraBite_Hook>();
        }
    }
}
