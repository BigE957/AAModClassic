using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Projectiles;
using AAModClassic.Rarities;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DarkmatterHelmetRanged : BaseAAItem
    {
        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Darkmatter Visor");
            /* Tooltip.SetDefault(@"15% increased Ranged damage
20% decreased ammo consumption 
Dark, yet still barely visible"); */
            ArmorIDs.Head.Sets.DrawFullHair[Item.headSlot] = true;
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.value = 300000;
            Item.defense = 26;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.15f;
            player.ammoCost80 = true;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DarkmatterChestplate>() && legs.type == ModContent.ItemType<DarkmatterLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.DarkmatterVisorBonus");
            player.GetModPlayer<StarHelmetRangedPlayer>().setBonus = true;
            player.GetModPlayer<StarHelmetRangedPlayer>().sunPortal = false;
            player.armorEffectDrawShadowLokis = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DarkmatterBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<DarkEnergy>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }

    }

}