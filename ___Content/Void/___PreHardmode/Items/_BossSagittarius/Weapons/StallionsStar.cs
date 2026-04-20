using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AAModClassic.Projectiles.Sag;
using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;

namespace AAModClassic.Items.Boss.Sagittarius
{
    public class StallionsStar : BaseAAItem
    {
        public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Stalion's Star");
            // Tooltip.SetDefault("A spinning blade of doom");
        }
		public override void SetDefaults()
		{
	        Item.damage = 25;
	        Item.width = 46;
	        Item.height = 46;
	        Item.useTime = 30;
	        Item.useAnimation = 30;
	        Item.useStyle = ItemUseStyleID.Swing;
	        Item.knockBack = 6;
	        Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.UseSound = SoundID.Item1;
	        Item.autoReuse = true;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.shoot = ModContent.ProjectileType<StallionsStar_Proj>();
            Item.shootSpeed = 10f;
            Item.noMelee = true;
            Item.noUseGraphic = true;
            Item.rare = ItemRarityID.LightRed;
        }

        public override bool CanUseItem(Player player)
        {
            for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
                {
                    return false;
                }
            }
            return true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 25);
                recipe.AddIngredient(ModContent.ItemType<DoomiteScrap>(), 15);
                recipe.AddTile(TileID.Anvils);
                recipe.Register();
            }
        }
    }
}