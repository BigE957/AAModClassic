using AAModClassic._Content.Ocean.___PreHardmode.Items.Tools;
using AAModClassic._Content.Snow.___PreHardmode.Items._BossSubzeroSerpent.Tools;
using AAModClassic._Content.Void.___PreHardmode.Items.Tools;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Tools
{
    public class ShadowDrill : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
            // DisplayName.SetDefault("Shadow Drill");
            // Tooltip.SetDefault("Mines things with a spinning...green thing I guess?");
		}

		public override void SetDefaults()
		{
			Item.damage = 10;
			Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
			Item.width = 50;
			Item.height = 18;
			Item.useTime = 8;
			Item.useAnimation = 15;
			Item.channel = true;
			Item.noUseGraphic = true;
			Item.noMelee = true;
			Item.pick = 110;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 0;
			Item.value = Item.sellPrice(0, 1, 8, 0);
			Item.rare = ItemRarityID.LightRed;
			Item.UseSound = SoundID.Item23;
			Item.autoReuse = true;
			Item.shoot = ModContent.ProjectileType<ShadowDrill_Holdout>();
			Item.shootSpeed = 40f;
		}

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HydraTuneller>());
            recipe.AddIngredient(ModContent.ItemType<CoralPickaxe>());
            recipe.AddIngredient(ModContent.ItemType<Icepick>());
            recipe.AddIngredient(ModContent.ItemType<DoomiteMiningLaser>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}