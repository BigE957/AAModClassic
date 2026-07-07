using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Projectiles;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
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
    [AutoloadEquipGlow(EquipType.Head)]
    public class DarkmatterHelmetRanged : EquipAbstract, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.Darkmatter";
        public Color Color => AAColor.Nightcrawler;

        public bool Condition(Player p) => !Main.dayTime && p.GetModPlayer<ZAAPlayer>().DarkmatterSet;

        public override void SetStaticDefaults()
        {

            // DisplayName.SetDefault("Darkmatter Visor");
            /* Tooltip.SetDefault(@"'Dark, yet still barely visible'"); */
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

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<DarkmatterChestplate>() && legs.type == ModContent.ItemType<DarkmatterLeggings>();
        }

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Ranged) += 0.15f;
            AddEffect<AmmoCost80Effect>();

            AddSetEffect<DarkmatterHelmetRangedSetEffect>();
            AddSetEffect<DrawShadowLokisEffect>();
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