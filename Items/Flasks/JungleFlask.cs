using AAModClassic.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Flasks
{
    public class JungleFlask : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.maxStack = 9999;
            Item.consumable = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.shoot = ModContent.ProjectileType<Projectiles.JungleSolution>();
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
            // DisplayName.SetDefault("Jungle Flask");
            // Tooltip.SetDefault(@"Converts Forest into Jungle");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse != 2)
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.Flasks.JungleFlask>();
                Item.shootSpeed = 9f;
            }
            else
            {
                Item.shoot = ModContent.ProjectileType<Projectiles.JungleSolution>();
                Item.shootSpeed = 2f;
            }
            return base.CanUseItem(player);
        }
    }
}