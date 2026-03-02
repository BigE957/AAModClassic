using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.Items.Accessories
{
    [AutoloadEquip(EquipType.Shoes)]
    public class DragonstrideBoots : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dragonstride Boots");
            /* Tooltip.SetDefault(@"Allows flight, super fast running, and extra mobility on ice
12% increased movement speed
Provides the ability to walk on water and lava
Grants immunity to fire blocks and 10 seconds of immunity to lava
Grants the ability to swim
Allows the ability to climb walls"); */
        }

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 15, 0, 0);
            Item.rare = 8;
            Item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed += .12f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .12f;
            player.waterWalk = true;
            player.fireWalk = true;
            player.lavaMax += 600;
            player.spikedBoots = Math.Max(player.spikedBoots, 1);
            player.rocketBoots = Math.Max(player.rocketBoots, 3);
            player.accRunSpeed = Math.Max(player.accRunSpeed, 9f);
            player.iceSkate = true;
            player.accFlipper = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.FrostsparkBoots, 1);
            recipe.AddIngredient(ItemID.LavaWaders, 1);
            recipe.AddIngredient(ItemID.TigerClimbingGear, 1);
            recipe.AddIngredient(ItemID.Flipper, 1);
            recipe.AddIngredient(null, "ShadowBand", 1);
            recipe.AddIngredient(null, "SoulOfSmite", 10);
            recipe.AddIngredient(null, "SoulOfSpite", 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}