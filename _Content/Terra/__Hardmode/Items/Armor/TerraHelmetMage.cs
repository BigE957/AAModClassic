using AAModClassic._Content.Jungle.___PreHardmode.Items.Armor;
using AAModClassic._Content.Terrarium.__Hardmode.Items.Materials;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Terra.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class TerraHelmetMage : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Terra";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Hood");
            /* Tooltip.SetDefault(@"Increases maximum mana by 100 and 30% reduced mana cost
            17% increased magic damage
            15% increased magic critical strike chance"); */
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 24;
            Item.value = 90000;
            Item.rare = ItemRarityID.Lime;
            Item.defense = 22;
        }

        public override void UpdateEquip(Player player)
        {
            player.statManaMax2 += 100;
            player.manaCost -= 0.3f;
            player.GetDamage(DamageClass.Magic) += 0.17f;
            player.GetCritChance(DamageClass.Magic) += 15;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<TerraChestplate>() && legs.type == ModContent.ItemType<TerraLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = FilePathUtils.SetBonusPath<TerraHelmetMage>();

            player.manaFlower = true;
            player.manaCost *= 0.6f;
            player.GetModPlayer<TerraHelmetMagePlayer>().effect = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<TribalHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<TerraPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}