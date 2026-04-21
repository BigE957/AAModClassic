using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using AAModClassic.___Content.Desert.___PreHardmode.Items.Materials;

namespace AAModClassic.___Content.Desert.___PreHardmode.Items.Tools
{
    public class Excavator : BaseAAItem
    {
        public override void SetDefaults()
        {
            Item.damage = 5;
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;
            Item.width = 32;
            Item.height = 32;
            Item.useTime = 19;
            Item.useAnimation = 22;
            Item.pick = 100;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.knockBack = 1;
            Item.value = Item.sellPrice(0, 0, 60, 0);
            Item.rare = ItemRarityID.LightRed;
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.useTurn = true;
        }

        public override void AddRecipes()  
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.DesertFossil, 15);
            recipe.AddIngredient(ItemID.Sandstone, 20);
            recipe.AddIngredient(ModContent.ItemType<DesertMana>(), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}
