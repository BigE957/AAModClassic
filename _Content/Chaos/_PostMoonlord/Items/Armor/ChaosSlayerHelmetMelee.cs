using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Materials;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;

namespace AAModClassic._Content.Chaos._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ChaosSlayerHelmetMelee : BaseAAItem
    {
        public override Color GlowmaskDrawColor => AAColor.Shen3;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Chaos Slayer Kabuto");
            /* Tooltip.SetDefault(@"30% increased Melee damage & critical strike chance
5% increased damage resistance
15% increased melee speed
+25 Max Life
The power of discordian rage radiates from this armor"); */
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.defense = 44;
        }

        

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ChaosSlayerChestplate>() && legs.type == ModContent.ItemType<ChaosSlayerLeggings>();
		}
        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.PerfectChaosKabutoBonus");
            player.GetModPlayer<AAPlayer>().perfectChaosMe = true;
            player.AddBuff(ModContent.BuffType<ChaosWrath_Buff>(), 2);
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += .3f;
            player.GetCritChance(DamageClass.Melee) += 30;
            player.endurance += .05f;
            player.GetAttackSpeed(DamageClass.Melee) += .15f;
            player.statLifeMax2 += 25;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DraconianSunHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DiscordiumBar>(), 6);
            recipe.AddIngredient(ModContent.ItemType<ChaosScale>(), 6);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}