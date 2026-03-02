using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Imp
{
    [AutoloadEquip(EquipType.Head)]
    public class ImpHood : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Imp Hood");
            // Tooltip.SetDefault("7% Increased Minion damage \n" + "+1 Minion slot");
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 7000;
            Item.rare = ItemRarityID.Green;
            Item.defense = 4;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.07f;
            player.maxMinions += 1;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("ImpGarb").Type && legs.type == Mod.Find<ModItem>("ImpBoots").Type;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.ImpHoodBonus");

            player.GetModPlayer<AAPlayer>().impSet = true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(null, "DevilSilk", 6);
                recipe.AddTile(TileID.Loom);
                recipe.Register();
            }
        }
    }
}