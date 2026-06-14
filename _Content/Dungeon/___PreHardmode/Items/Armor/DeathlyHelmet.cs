using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic._Content.Hell.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;


namespace AAModClassic._Content.Dungeon.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DeathlyHelmet : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Deathly";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deathly Skull");
            // Tooltip.SetDefault("9% Increased ranged damage");
        }

        public override void SetDefaults()
        {
            Item.width = 38;
            Item.height = 34;
            Item.value = 90000;
            Item.rare = ItemRarityID.LightRed;
            Item.defense = 6;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.09f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DeathlyChestplate>() && legs.type == ModContent.ItemType<DeathlyLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DeathlySkullBonus");

            player.aggro -= 5;
            player.ammoCost80 = true;
        }

        public override void AddRecipes()
        {
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.NecroHelmet, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 5);
                recipe.AddIngredient(ItemID.ShadowScale, 5);
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 5);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
            {
                Recipe recipe = CreateRecipe();
                recipe.AddIngredient(ItemID.NecroHelmet, 1);
                recipe.AddIngredient(ItemID.JungleSpores, 5);
                recipe.AddIngredient(ItemID.TissueSample, 5);
                recipe.AddIngredient(ModContent.ItemType<DevilSilk>(), 5);
                recipe.AddTile(TileID.DemonAltar);
                recipe.Register();
            }
        }
    }
}