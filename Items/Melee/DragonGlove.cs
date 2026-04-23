using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Melee
{
    public class DragonGlove : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragon Glove");
        }

        public override void SetDefaults()
        {
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTurn = true;
            Item.autoReuse = true;
            Item.useAnimation = 9;
            Item.useTime = 9;
            Item.width = 28;
            Item.height = 24;
            Item.damage = 21;
            Item.knockBack = 4f;
            Item.UseSound = SoundID.Item1;
            Item.scale = 1.35f;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.rare = ItemRarityID.Orange;
            Item.value = 50000;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<IncineriteBar>(), 10);
            recipe.AddIngredient(ModContent.ItemType<DragonClaw>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}