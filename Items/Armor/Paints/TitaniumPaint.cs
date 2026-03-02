using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class TitaniumPaint : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Titanium Face Paint");
            /* Tooltip.SetDefault(@"27% increased minion damage
+100 mana"); */
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 50000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.27f;
            player.statManaMax2 += 100;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.TitaniumBreastplate && legs.type == ItemID.TitaniumLeggings;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.TitaniumPaintBonus");
            player.onHitDodge = true;
            player.maxMinions += 4;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.TitaniumBar, 6);
                recipe.AddIngredient(ItemID.BottledWater, 1);
                recipe.AddTile(TileID.BewitchingTable);
                recipe.Register();
            }
        }
    }
}