using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class MudkipBall : BaseAAItem
	{
        
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Mud Fish Ball");

			// Tooltip.SetDefault("It seems to have something in it already");
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.UnluckyYarn);
			Item.shoot = Mod.Find<ModProjectile>("Mudkip").Type;
            
            Item.buffType = Mod.Find<ModBuff>("Mudkip").Type;
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