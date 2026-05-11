using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossFeudalFungus.Accessories;
using AAModClassic._Content.GlowingMushroom.___PreHardmode.Items._BossTruffleToad.Accessories;
using AAModClassic._Content.RedMushroom.___PreHardmode.Items._BossMushroomMonarch.Accessories;
using AAModClassic._Unreleased.Content.Parthenan.__Hardmode.Items._BossTechnoTruffle.Accessories;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content._Tinker.__Hardmode.Accessories
{
    public class MadnessTruffle : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Madness Truffle");
            /* Tooltip.SetDefault(@"Increased jump speed and allows auto-jump
You are immune to fall damage
Increased jump height
+50 Max Mana
+50 Max Life
You know what? Just don't put it anywhere near your mouth."); */
        }

        public override void SetDefaults()
        {
            Item.width = 26;
            Item.height = 26;
            Item.value = Item.sellPrice(0, 10, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
            Item.defense = 8;
        }

        //TODO: .
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.wingTime > 0)
            {
                player.wingTime += 3;
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.autoJump = true;
            Player.jumpHeight = 25;
            player.jumpSpeedBoost += 3.6f;
            player.noFallDmg = true;
            player.statManaMax2 += 50;
            player.statLifeMax2 += 50;
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HeartyTruffle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<GlowingTruffle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<MetallicTruffle>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TruffleLegs>(), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.Register();
        }
    }
}