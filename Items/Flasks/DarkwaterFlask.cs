using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Flasks
{
    public class DarkwaterFlask : BaseAAItem
	{
		public override void SetDefaults()
		{
			Item.width = 22;
            Item.height = 26;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.useTime = 28;
			Item.useAnimation = 28;
			Item.shoot = ModContent.ProjectileType<IndigoSolution>();
			Item.shootSpeed = 1f;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.value = Item.sellPrice(0, 0, 1, 0);
			Item.rare = ItemRarityID.Green;
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = false;
            Item.noUseGraphic = false;
        }

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Darkwater Flask");
			// Tooltip.SetDefault(@"Spreads the Mire");
		}

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse == 2)
            {
                Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Flasks.DarkwaterFlask>();
                Item.shootSpeed = 9f;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<IndigoSolution>();
                Item.shootSpeed = 2f;
            }
            return base.CanUseItem(player);
        }

        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
            if (type == ModContent.ProjectileType<DarkwaterFlask>())
            {
                Projectile.NewProjectile(source, position, velocity, type, 0, 0, Main.myPlayer, 4);
                return false;
            }
            return base.Shoot(player, source, position, velocity, type, damage, knockback);
        }
    }
}
