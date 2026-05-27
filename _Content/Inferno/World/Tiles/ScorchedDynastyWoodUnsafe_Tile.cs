using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.World.Tiles
{
    public class ScorchedDynastyWoodUnsafe_Tile : ScorchedDynastyWood_Tile
    {
        public override string Texture => ModContent.GetInstance<ScorchedDynastyWood_Tile>().Texture;
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            RegisterItemDrop(ModContent.ItemType<ScorchedDynastyWoodUnsafe>());
        }
    }
}