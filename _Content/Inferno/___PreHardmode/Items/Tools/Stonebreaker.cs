using AAModClassic._Content.Desert.___PreHardmode.Items.Tools;
using AAModClassic._Content.Ocean.___PreHardmode.Items.Tools;
using AAModClassic._Content.Void.___PreHardmode.Items.Tools;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Tools
{
    public class Stonebreaker : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 15;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 40;
            Item.height = 40;
            Item.useAnimation = 30;
            Item.useTime = 10;
            Item.pick = 110;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 1, 8, 0);
            Item.rare = ItemRarityID.Green;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Stonebreaker");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DragonDigger>());
            recipe.AddIngredient(ModContent.ItemType<CoralPickaxe>());
            recipe.AddIngredient(ModContent.ItemType<Excavator>());
            recipe.AddIngredient(ModContent.ItemType<DoomiteMiningLaser>());
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
