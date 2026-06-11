using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Mire.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Mire.__Hardmode.Items.Materials;
using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;

namespace AAModClassic._Content.Mire.__Hardmode.Items.Accessories
{
    public class BlackLotusEmblem : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Black Lotus Emblem");
            /* Tooltip.SetDefault(
@"Increases pickup range for mana stars
Automatically use mana potions when needed
Greatly reduce manasick time
Your magic attacks inflicts moonraze
15% increased movement speed
12% reduced mana usage
18% increased magic damage"); */
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 50, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.GetModPlayer<AAPlayer>().BlackLotusEmblem = true;
            player.manaMagnet = true;
			player.manaCost -= 0.12f;
			player.GetDamage(DamageClass.Magic) += 0.18f;
            player.moveSpeed += 0.15f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += 0.15f;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.ManaFlower, 1);
            recipe.AddIngredient(ItemID.CelestialEmblem, 1);
            recipe.AddIngredient(ModContent.ItemType<BlackLotus>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ShadowBand>(), 1);
            recipe.AddIngredient(ModContent.ItemType<SoulOfSpite>(), 10);
            recipe.AddTile(ModContent.TileType<HallowedAnvil_Tile>());
            recipe.Register();
        }

    }
}