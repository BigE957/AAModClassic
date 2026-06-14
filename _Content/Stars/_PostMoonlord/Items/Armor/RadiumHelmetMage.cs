using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic._Content.Stars._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class RadiumHelmetMage : BaseAAItem, ILocalizedModType, ICustomEquipGlow
    {
        public new string LocalizationCategory => "Items.Armor.Radium";
        public Color Color => AAColor.Glow;

        public bool Condition(Player p) => Main.dayTime && p.GetModPlayer<AAPlayer>().Radium;

        public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Radium Mask");
			/* Tooltip.SetDefault(@"15% increased magic damage
Increases maximum mana by 100
Shines with the light of a starry night sky"); */

		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 18;
			Item.value = 300000;
			Item.defense = 18;
            Item.rare = ModContent.RarityType<PostEquinoxRarity>();
        }

        

        public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Magic) += 0.15f;
            player.statManaMax2 += 100;

        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<RadiumChestplate>() && legs.type == ModContent.ItemType<RadiumLeggings>();
        }

		public override void UpdateArmorSet(Player player)
		{
            player.GetModPlayer<StarHelmetMagePlayer>().setBonus = true;
            player.GetModPlayer<StarHelmetMagePlayer>().sunSiphon = true;
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.RadiumMaskBonus1") + (int)player.GetDamage(DamageClass.Magic).ApplyTo(100) + " " + Language.GetTextValue("Mods.AAModClassic.Common.RadiumMaskBonus2") + player.GetCritChance(DamageClass.Magic) + Language.GetTextValue("Mods.AAModClassic.Common.RadiumMaskBonus3");

			for (int i = 0; i < 15; i++)
            {
                Vector2 offset = new Vector2();
                double angle = Main.rand.NextDouble() * 2d * Math.PI;
                offset.X += (float)(Math.Sin(angle) * 300);
                offset.Y += (float)(Math.Cos(angle) * 300);
                Dust dust = Main.dust[Dust.NewDust(player.Center + offset - new Vector2(4, 4), 0, 0,  ModContent.DustType<Dusts.RadiumDust>(), 0, 0, 100, default, 1f)];
                dust.velocity = player.velocity;
                dust.noGravity = true;
            }
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