using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Ocean.___PreHardmode.Items.Tools
{
    public class CoralPickaxe : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetDefaults()
        {

            Item.damage = 7;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;

            Item.useTime = 12;
            Item.useAnimation = 20;
            Item.pick = 40;    //pickaxe power
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = 10;
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Coral Pickaxe");
            // Tooltip.SetDefault("Because Blue Pickaxe was a boring name");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.Coral, 15);
            recipe.AddTile(TileID.WorkBenches);   
            recipe.Register();
        }
    }
}
