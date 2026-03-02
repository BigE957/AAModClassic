using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class LunaminiJar : BaseAAItem
	{
        public override void SetStaticDefaults()
		{
			// DisplayName and Tooltip are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Moon Bee in a Jar");
			// Tooltip.SetDefault("Summons a Lunamini");

            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(2, 2));
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }

		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.ZephyrFish);
			Item.shoot = Mod.Find<ModProjectile>("Lunamini").Type;
            Item.buffType = Mod.Find<ModBuff>("Lunamini").Type;
            Item.noUseGraphic = true;
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