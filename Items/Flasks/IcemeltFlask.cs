using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Flasks
{
    public class IcemeltFlask : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 22;
            Item.height = 26;
            Item.maxStack = 999;
            Item.consumable = true;
            Item.useTime = 28;
            Item.useAnimation = 28;
            Item.shoot = Terraria.ModLoader.ModContent.ProjectileType<Projectiles.Snowmelt>();
            Item.shootSpeed = 1f;
            Item.useStyle = 1;
            Item.value = Item.sellPrice(0, 0, 1, 0);
            Item.rare = 2;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = false;
            Item.noUseGraphic = false;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Icemelt Flask");
            // Tooltip.SetDefault(@"Clears the snow biome");
        }

        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {

            if (player.altFunctionUse != 2)
            {
                Item.shoot = Mod.Find<ModProjectile>("IcemeltFlask").Type;
                Item.shootSpeed = 9f;
            }
            else
            {
                Item.shoot = Mod.Find<ModProjectile>("Snowmelt").Type;
                Item.shootSpeed = 2f;
            }
            return base.CanUseItem(player);
        }
    }
}