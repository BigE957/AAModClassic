using AAMod.Items.Armor.Darkmatter;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Head)]
    public class RadiumHeadgear : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radium Headgear");
            /* Tooltip.SetDefault(@"20% increased Ranged damage
Shines with the light of a starry night sky"); */

        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.value = 300000;
            Item.defense = 22;
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.20f;
            player.AddBuff(BuffID.Shine, 2);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("RadiumPlatemail").Type && legs.type == Mod.Find<ModItem>("RadiumCuisses").Type;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.RadiumHeadgearBonus");


            player.GetModPlayer<VisorEffects>().setBonus = true;
            player.GetModPlayer<VisorEffects>().sunPortal = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "RadiumBar", 25);
            recipe.AddIngredient(null, "Stardust", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
    }
}