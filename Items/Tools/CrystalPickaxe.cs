using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Tools
{
    public class CrystalPickaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 12;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 42;
            Item.height = 42;

            Item.useTime = 10;
            Item.useAnimation = 14;
            Item.pick = 110;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = 1000;
            Item.rare = ItemRarityID.Lime;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Unity Pickaxe");
      // Tooltip.SetDefault("Can mine mythril and orichalcum.");
    }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.PixieDust, 12);   
			recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddTile(TileID.Anvils);   
            recipe.Register();
        }
    }
}
