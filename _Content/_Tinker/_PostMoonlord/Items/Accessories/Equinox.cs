using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items._BossEquinoxWorms.Accessories;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content._Tinker._PostMoonlord.Items.Accessories
{
    public class Equinox : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Equinox");
            /* Tooltip.SetDefault(
@"Turns the holder into a werewolf at night and a merfolk when entering water
Gives immensely increased stats
'True balance'"); */
            ItemID.Sets.ItemNoGravity[Item.type] = true;
        }
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.value = Item.sellPrice(0, 30, 0, 0);
            Item.rare = ItemRarityID.Purple;
            Item.accessory = true;
            Item.expert = true; Item.expertOnly = true;
        }

		public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 6;
            player.statDefense += 9;
            player.GetAttackSpeed(DamageClass.Melee) += 0.10f;
            player.GetCritChance(DamageClass.Melee) += 5;
            player.GetCritChance(DamageClass.Ranged) += 5;
            player.GetCritChance(DamageClass.Magic) += 5;
            player.pickSpeed -= 0.35f;
            player.GetKnockback(DamageClass.Summon).Base += 0.75f;
            player.GetDamage(DamageClass.Generic) += 0.17f;
            player.GetCritChance(DamageClass.Throwing) += 5;
            player.nightVision = true;
            player.GetModPlayer<AAPlayer>().RStar = true;
            player.accMerman = true;
            player.wolfAcc = true;
            if (hideVisual)
            {
                player.hideMerman = true;
                player.hideWolf = true;
            }
        }
        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.CelestialShell, 1);
            recipe.AddIngredient(ModContent.ItemType<RadiantStar>(), 1);
            recipe.AddIngredient(ModContent.ItemType<DarkVoid>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 20);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 20);
            recipe.AddTile(ModContent.TileType<ACS_Tile>());
            recipe.Register();
        }
    }
}