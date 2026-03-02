using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace AAModClassic.Items.Pets
{
    public class DragonsSoul : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dragon Soul");
			/* Tooltip.SetDefault(@"Summons a Dragon Soul
It feels hot, but comforting..."); */
            Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 4));
        }

		public override void SetDefaults()
		{
			Item.damage = 0;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.shoot = Mod.Find<ModProjectile>("DragonSoul").Type;
			Item.width = 16;
			Item.height = 30;
			Item.UseSound = SoundID.Item2;
			Item.useAnimation = 20;
			Item.useTime = 20;
			Item.rare = ItemRarityID.Yellow;
			Item.noMelee = true;
			Item.value = Item.sellPrice(0, 5, 50, 0);
			Item.buffType = Mod.Find<ModBuff>("DragonSoul").Type;
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