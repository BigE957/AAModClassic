using Terraria;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace AAModClassic.___Content.Inferno.___PreHardmode.Items.Materials
{
    public class ScorchedScale : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.width = 22;
            Item.height = 24;
            Item.maxStack = 99;
            Item.rare = ItemRarityID.Blue;
			
        }

        
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Scorched Scale");
            // Tooltip.SetDefault("The scale of a formidable foe");
        }
    }
}
