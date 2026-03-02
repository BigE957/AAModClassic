using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Terra
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraHood : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Hood");
            /* Tooltip.SetDefault(@"Increases maximum mana by 100
Increases magic damage by 17%
Increases magic crit by 15%"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.value = 90000;
            Item.rare = 7;
            Item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.manaCost -= 0.3f;
            player.GetDamage(DamageClass.Magic) += 0.17f;
            player.GetCritChance(DamageClass.Magic) += 15;
            player.statManaMax2 += 100;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("TerraPlate").Type && legs.type == Mod.Find<ModItem>("TerraGreaves").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.TerraHoodBonus");

            player.manaCost *= 0.6f;
            player.manaFlower = true;
            player.GetModPlayer<AAPlayer>().TerraMa = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "TribalHat", 1);
            recipe.AddIngredient(null, "TerraCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}