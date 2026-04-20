using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.___Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic.___Content.Bunny._PostMoonlord.Items.Materials;

namespace AAModClassic.___Content.Bunny._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ChampionHelmetRanged : BaseAAItem
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Champion Mask");
            /* Tooltip.SetDefault(@"55% increased ranged damage
44% increased ranged critical strike chance
10% increased non-ranged damage
25% reduced ammo consumption
The armor of a champion feared across the land"); */
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            AARarity = 14;
            Item.defense = 34;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity14;
                }
            }
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ChampionChestplate>() && legs.type == ModContent.ItemType<ChampionLeggings>();
		}

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.ChampionMaskBonus");
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.ChampionRa = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += .45f;
            player.GetCritChance(DamageClass.Ranged) += 44;
            player.GetDamage(DamageClass.Generic) += .1f;
            player.ammoCost75 = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HoppingHoodlumHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChampionPlate>(), 10);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}