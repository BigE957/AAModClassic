using AAModClassic.___Content.Terrarium.___PreHardmode.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic.Items.Armor.Biomite
{
    [AutoloadEquip(EquipType.Head)]
	public class BiomiteHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Biomite Helmet");
		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 20;
			Item.value = 7500;
			Item.rare = ItemRarityID.Green;
			Item.defense = 5;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<BiomiteChestplate>() && legs.type == ModContent.ItemType<BiomiteLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor1") + SetBonus(player);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<TerraShard>(), 15);
			recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

		public static string SetBonus(Player player)
		{
			string set = "";
			if (Main.dayTime)
			{
				player.statLifeMax2 += 20;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor2");
			}
			else
			{
				player.statManaMax2 += 20;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor3");
			}
			if (player.GetModPlayer<AAPlayer>().ZoneVoid)
			{
				player.detectCreature = true;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor4");
			}
			if (player.GetModPlayer<AAPlayer>().ZoneInferno)
			{
				player.buffImmune[BuffID.OnFire] = true;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor5");
			}
			if (player.GetModPlayer<AAPlayer>().ZoneMire)
			{
				player.buffImmune[BuffID.Poisoned] = true;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor6");
			}
			if (player.GetModPlayer<AAPlayer>().Terrarium)
			{
				player.statDefense += 5;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor7");
			}
			if (player.ZoneJungle)
			{
				player.manaRegenBonus += 3;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor8");
			}
			if (player.ZoneSnow)
			{
				player.buffImmune[BuffID.Chilled] = true;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor9");
			}
			if (player.ZoneDesert)
			{
				player.buffImmune[BuffID.WindPushed] = true;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor10");
			}
			if (player.ZoneHallow)
			{
				player.buffImmune[BuffID.Slow] = true;
				player.lifeRegen += 3;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor11");
			}
			if (player.ZoneCorrupt)
			{
				player.moveSpeed += .1f;
				player.GetModPlayer<AAPlayer>().MaxMovespeedboost += 0.1f;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor12");
			}
			if (player.ZoneCrimson)
			{
				player.GetArmorPenetration(DamageClass.Generic) += 5;
				set += Language.GetTextValue("Mods.AAModClassic.Items.BiomiteArmor.BiomiteArmor13");
			}
			return set;
		}
	}
}