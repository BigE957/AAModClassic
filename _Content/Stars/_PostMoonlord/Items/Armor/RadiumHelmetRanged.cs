using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class RadiumHelmetRanged : BaseAAItem, ICustomEquipGlow
    {
        public Color Color => AAColor.Glow;

        public bool Condition(Player p) => Main.dayTime && p.GetModPlayer<AAPlayer>().Radium;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Radium Headgear");
            /* Tooltip.SetDefault(@"20% increased Ranged damage
Shines with the light of a starry night sky"); */

        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 14;
            Item.value = 300000;
            Item.defense = 22;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += 0.20f;
            player.AddBuff(BuffID.Shine, 2);
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<RadiumChestplate>() && legs.type == ModContent.ItemType<RadiumLeggings>();
        }

        public override void UpdateArmorSet(Player player)
        {

            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.RadiumHeadgearBonus");


            player.GetModPlayer<StarHelmetRangedPlayer>().setBonus = true;
            player.GetModPlayer<StarHelmetRangedPlayer>().sunPortal = true;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RadiumBar>(), 25);
            recipe.AddIngredient(ModContent.ItemType<RadiantPhoton>(), 10);
            recipe.AddTile(ModContent.TileType<QuantumFusionAccelerator_Tile>());
            recipe.Register();
        }
    }
}