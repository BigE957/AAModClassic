using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Weapons
{
    public class DarkmatterKunai : BaseAAItem, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Weapons.Ranged";
		public override void SetDefaults()
		{

            Item.damage = 60;            
            Item.DamageType = DamageClass.Ranged;
            Item.width = 20;
            Item.height = 20;
			Item.useTime = 8;
            Item.maxStack = Item.CommonMaxStack;
			Item.useAnimation = 8;
            Item.noUseGraphic = true;
            Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 0;
			Item.shootSpeed = 15f;
			Item.shoot = ModContent.ProjectileType<DarkmatterKunai_Proj>();
			Item.UseSound = SoundID.Item1;
			Item.autoReuse = true;
            Item.consumable = true;
            Item.noMelee = true;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
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
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>());
		    recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
		}
    }
}
