using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Pets
{
    public class ScorchedEgg : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Scorched Egg");

			// Tooltip.SetDefault("What will hatch from this egg?");
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.DD2PetGhost);
			Item.shoot = ModContent.ProjectileType<ScorchedEgg_Broodmini>();
            
            Item.buffType = ModContent.BuffType<ScorchedEgg_Buff>();
		}

        public override void UseStyle(Player player, Rectangle heldItemFrame)
		{
			if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
			{
				player.AddBuff(Item.buffType, 3600, true);
			}
		}
	}
}