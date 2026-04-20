using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Buffs;
using AAModClassic.Tiles.Crafters;
using AAModClassic.___Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic.___Content.Void._PostMoonlord.Items.Armor;

namespace AAModClassic.___Content.Chaos._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ChaosSlayerHelmetMage : BaseAAItem
    {
        public override Color GlowmaskDrawColor => AAColor.Shen3;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Slayer Hood");
            /* Tooltip.SetDefault(@"32% increased Magic damage
20% increased Magic critical strike chance
2% increased damage resistance
30% reduced Mana consumption
150 increased maximum mana
The power of discordian rage radiates from this hood"); */
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ItemRarityID.Cyan;
            AARarity = 14;
            Item.defense = 30;
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
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.PerfectChaosHoodBonus");
            player.GetModPlayer<AAPlayer>().perfectChaosMa = true;
            player.AddBuff(ModContent.BuffType<ChaosWrath_Buff>(), 2);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += .32f;
            player.GetCritChance(DamageClass.Magic) += 20;
            player.endurance += .02f;
            player.manaCost *= .7f;
            player.statManaMax2 += 150;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomsdayHelmetMage>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 6);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 6);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}