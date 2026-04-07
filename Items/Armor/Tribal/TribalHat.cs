using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Armor.Tribal
{
    [AutoloadEquip(EquipType.Head)]
    public class TribalHat : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Tribal Hat");
            /* Tooltip.SetDefault(@"8% Increased magic critical chance
Increases maximum mana by 20"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 7;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 20;
            player.GetCritChance(DamageClass.Magic) += 8;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TribalCloak>() && legs.type == ModContent.ItemType<TribalKilt>();
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.TribalHatBonus");

            player.manaCost *= 0.7f;
            player.manaFlower = true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.JungleHat, 1);
                recipe.AddIngredient(ItemID.ShadowScale, 8);
                recipe.AddIngredient(ItemID.Bone, 8);
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.JungleHat, 1);
                recipe.AddIngredient(ItemID.TissueSample, 8);
                recipe.AddIngredient(ItemID.Bone, 8);
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 8);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}