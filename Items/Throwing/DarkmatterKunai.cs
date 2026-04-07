using AAModClassic.Globals;
using AAModClassic.Items.Materials;
using AAModClassic.Tiles.Crafters;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Throwing
{
    public class DarkmatterKunai : BaseAAItem
	{
		public override void SetDefaults()
		{

            Item.damage = 60;            
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 20;
			Item.useTime = 8;
            Item.maxStack = 9999;
			Item.useAnimation = 8;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.shootSpeed = 15f;
			Item.shoot = ModContent.ProjectileType<Projectiles.DMK>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.consumable = true;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Darkmatter Kunai");
            // Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe(50);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DarkMatter>());
		    recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
		}
    }
}
