using AAModClassic._Content._EX._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons
{
    public class StarburstWand : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Weapons.Magic";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Starburst Wand");
            /* Tooltip.SetDefault(@"Hold to charge the wand
Wand of Sparking EX"); */
        }

        public override void SetDefaults()
        {
            Item.mana = 8;
            Item.width = 74;
            Item.height = 34;
            Item.DamageType = DamageClass.Magic;
            Item.damage = 300;
            Item.shoot = ModContent.ProjectileType<StarburstWand_Holdout>();
            Item.useTime = 10;
            Item.useAnimation = 10;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.channel = true;
            Item.sellPrice(3, 0, 0, 0);
            Item.noMelee = true;
			Item.rare = ItemRarityID.Purple;
			Item.shootSpeed = 12f;
			Item.noUseGraphic = true;
            Item.expert = true; Item.expertOnly = true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.WandofSparking);
                recipe.AddIngredient(ModContent.ItemType<EXSoul>());
                recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
                recipe.Register();
            }
        }
    }
}
