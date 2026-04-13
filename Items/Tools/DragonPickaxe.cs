using AAModClassic.___Content.Sky.__Hardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Tools
{
    public class DragonPickaxe : BaseAAItem
    {
        public override void SetDefaults()
        {

            Item.damage = 10;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 54;
            Item.height = 52;

            Item.useTime = 12;
            Item.useAnimation = 24;
            Item.pick = 130;    //pickaxe power
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 0;
            Item.value = 10;
            Item.rare = ItemRarityID.Pink;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

    public override void SetStaticDefaults()
    {
      // DisplayName.SetDefault("Dragon Pickaxe");
      // Tooltip.SetDefault("");
    }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DragonSpirit>(), 18);
            recipe.AddTile(TileID.MythrilAnvil);   
            recipe.Register();
        }
    }
}
