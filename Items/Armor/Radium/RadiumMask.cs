using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Items.Armor.Darkmatter;
using AAModClassic.Globals;

namespace AAModClassic.Items.Armor.Radium
{
    [AutoloadEquip(EquipType.Head)]
	public class RadiumMask : BaseAAItem
	{
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
            Item.rare = ItemRarityID.Cyan;
            AARarity = 12;
        }

        public override void ModifyTooltips(System.Collections.Generic.List<TooltipLine> list)
        {
            foreach (TooltipLine line2 in list)
            {
                if (line2.Mod == "Terraria" && line2.Name == "ItemName")
                {
                    line2.OverrideColor = AAColor.Rarity12;
                }
            }
        }

        public override void UpdateEquip(Player player)
		{
			player.GetDamage(DamageClass.Magic) += 0.15f;
            player.statManaMax2 += 100;

        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<RadiumPlatemail>() && legs.type == ModContent.ItemType<RadiumCuisses>();
        }

		public override void UpdateArmorSet(Player player)
		{
            player.GetModPlayer<DarkmatterMaskEffects>().setBonus = true;
            player.GetModPlayer<DarkmatterMaskEffects>().sunSiphon = true;
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.RadiumMaskBonus1") + (int)(player.GetDamage(DamageClass.Magic)).ApplyTo(100) + " " + Language.GetTextValue("Mods.AAModClassic.Common.RadiumMaskBonus2") + player.GetCritChance(DamageClass.Magic) + Language.GetTextValue("Mods.AAModClassic.Common.RadiumMaskBonus3");

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
            recipe.AddIngredient(null, "RadiumBar", 25);
            recipe.AddIngredient(null, "Stardust", 10);
            recipe.AddTile(null, "QuantumFusionAccelerator");
            recipe.Register();
        }
	}
}