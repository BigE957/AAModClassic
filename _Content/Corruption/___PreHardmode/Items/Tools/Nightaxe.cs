using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using AAModClassic._Content.Jungle.___PreHardmode.Items.Tools;
using AAModClassic._Content.Dungeon.___PreHardmode.Items.Tools;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Corruption.___PreHardmode.Items.Tools
{
    public class Nightaxe : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Tools";
        public override void SetDefaults()
        {

            Item.damage = 15;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 64;
            Item.height = 64;
            Item.useAnimation = 23;
            Item.useTime = 10;
            Item.pick = 110;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 1, 8, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nightaxe");
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.NightmarePickaxe);
            recipe.AddIngredient(ModContent.ItemType<Grasscutter>());
            recipe.AddIngredient(ModContent.ItemType<Toothpick>());
            recipe.AddIngredient(ItemID.MoltenPickaxe);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}
