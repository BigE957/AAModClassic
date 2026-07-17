using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.__Hardmode.Items.Tools
{
    public class DragonsGrip : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon's Grip");
        }

        public override void SetDefaults()
        {
            Item.CloneDefaults(ItemID.IlluminantHook);
            Item.shoot = ModContent.ProjectileType<DragonsGrip_Hook>();
        }
    }
}
