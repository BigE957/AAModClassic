using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAMod.Items.Armor.Paints
{
    [AutoloadEquip(EquipType.Head)]
    public class MythrilPaint : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mythril Face Paint");
            /* Tooltip.SetDefault(@"26% increased minion damage
+60 mana"); */
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 50000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 2;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.26f;
            player.statManaMax2 += 60;
        }

        public override void DrawHair(ref bool drawHair, ref bool drawAltHair)/* tModPorter Note: Removed. In SetStaticDefaults, use ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true if you had drawHair set to true, and ArmorIDs.Head.Sets.DrawHatHair[Item.headSlot] = true if you had drawAltHair set to true */
        {
            drawHair = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ItemID.MythrilChainmail && legs.type == ItemID.MythrilGreaves;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.MythrilPaintBonus");
            player.maxMinions += 3;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.MythrilBar, 6);
            recipe.AddIngredient(ItemID.BottledWater, 1);
            recipe.AddTile(TileID.BewitchingTable);
            recipe.Register();
        }
    }
}