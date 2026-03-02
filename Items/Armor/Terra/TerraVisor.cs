using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;


namespace AAMod.Items.Armor.Terra
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraVisor : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Visor");
            /* Tooltip.SetDefault(@"24% Increased ranged damage
25% Reduced Ammo Consumption
Grants hunter & night vision"); */
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 34;
            Item.value = 90000;
            Item.rare = ItemRarityID.Lime;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.24f;
            player.GetDamage(DamageClass.Ranged) += 0.24f;
            player.ammoCost75 = true;
            player.nightVision = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("TerraPlate").Type && legs.type == Mod.Find<ModItem>("TerraGreaves").Type;
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.TerraVisorBonus");

            player.aggro -= 5;
            player.GetCritChance(DamageClass.Ranged) += 20;
            player.GetModPlayer<AAPlayer>().TerraRa = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "DeathlySkull", 1);
            recipe.AddIngredient(null, "TerraCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}