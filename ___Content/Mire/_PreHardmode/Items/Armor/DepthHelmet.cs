using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.___Content.Mire._PreHardmode.Items.Materials;

namespace AAModClassic.___Content.Mire._PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DepthHelmet : BaseAAItem
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
            return body.type == ModContent.ItemType<DepthChestplate>() && legs.type == ModContent.ItemType<DepthLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DepthFukumenBonus");
            player.aggro -= 3;
            player.ammoCost80 = true;
            player.nightVision = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<AbyssiumBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<HydraHide>(), 10);
            recipe.AddTile(TileID.Anvils);
            recipe.Register();
        }
    }
}