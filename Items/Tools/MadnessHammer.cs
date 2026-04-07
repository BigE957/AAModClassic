using AAModClassic;
using AAModClassic.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Tools
{
    public class MadnessHammer : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.width = 40;
            Item.height = 40;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.useAnimation = 30;
            Item.useTime = 24;
            Item.autoReuse = true;
            Item.damage = 7;
            Item.hammer = 50;
            Item.UseSound = SoundID.Item1;
            Item.knockBack = 3f;
            Item.value = 10000;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Madness Hammer");
        }

        public override void AddRecipes()  //How to craft item item
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<MadnessFragment>(), 6);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}
