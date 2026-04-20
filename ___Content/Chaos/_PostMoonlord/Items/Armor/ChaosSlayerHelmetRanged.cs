using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Buffs;
using AAModClassic.Tiles.Crafters;
using AAModClassic.___Content.Mire._PostMoonlord.Items.Armor;
using AAModClassic.___Content.Chaos._PostMoonlord.Items.Materials;

namespace AAModClassic.___Content.Chaos._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ChaosSlayerHelmetRanged : BaseAAItem
    {
        public override Color GlowmaskDrawColor => AAColor.Shen3;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Slayer Visor");
            /* Tooltip.SetDefault(@"45% increased ranged damage
38% increased ranged critical strike chance
3% increased damage resistance
25% reduced ammo consumption
+15 Max Life
The power of discordian rage radiates from this hood"); */
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            AARarity = 14;
            Item.defense = 39;
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
			return body.type == ModContent.ItemType<ChaosSlayerChestplate>() && legs.type == ModContent.ItemType<ChaosSlayerLeggings>();
		}

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.PerfectChaosVisorBonus");
            player.GetModPlayer<AAPlayer>().perfectChaosRa = true;
            player.AddBuff(ModContent.BuffType<ChaosWrath_Buff>(), 2);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += .45f;
            player.GetCritChance(DamageClass.Ranged) += 38;
            player.endurance += .03f;
            player.ammoCost75 = true;
            player.statLifeMax2 += 15;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DreadMoonHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 6);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 6);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}