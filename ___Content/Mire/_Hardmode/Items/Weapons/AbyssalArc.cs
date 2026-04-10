using AAModClassic.___Content.Mire._Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Mire._Hardmode.Items.Weapons
{
    public class AbyssalArc : BaseAAItem
	{
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Abyssal Arc");
            // Tooltip.SetDefault("");
        }

        public override void SetDefaults()
		{

            Item.damage = 60;            
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 30;
            Item.height = 30;
			Item.useTime = 16;
			Item.useAnimation = 16;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.LightPurple;
			Item.shootSpeed = 15f;
			Item.shoot = ModContent.ProjectileType<AbyssalArc_Proj>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.noMelee = true;
		}

        public override bool CanUseItem(Player player)       //this make that you can shoot only 1 boomerang at once
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
                Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ModContent.ItemType<DeepAbyssiumBar>(), 12);
				recipe.AddTile(TileID.MythrilAnvil);
                recipe.Register();
		}
    }
}
