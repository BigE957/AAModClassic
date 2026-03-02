using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace AAModClassic.Items.Armor.Depth
{
    [AutoloadEquip(EquipType.Head)]
    public class DepthFukumen : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Depth Fukumen");
            /* Tooltip.SetDefault(@"25% increased movement speed
8% increased ranged damage
Weightless as shadow itself"); */
        }

        public override void SetDefaults()
        {
            Item.width = 20;
            Item.height = 20;
            Item.value = 7500;
            Item.rare = ItemRarityID.Green;
            Item.defense = 5;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += .08f;
            player.moveSpeed += .25f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == Mod.Find<ModItem>("DepthGi").Type && legs.type == Mod.Find<ModItem>("DepthHakama").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.DepthFukumenBonus");
            player.aggro -= 3;
            player.ammoCost80 = true;
            player.nightVision = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(null, "AbyssiumBar", 15);
            recipe.AddIngredient(null, "HydraHide", 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}