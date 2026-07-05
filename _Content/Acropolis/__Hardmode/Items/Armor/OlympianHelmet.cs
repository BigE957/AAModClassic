using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Acropolis.__Hardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class OlympianHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Olympian";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Olympian Helmet");
		}

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 5, 0, 0);
			Item.rare = ItemRarityID.LightPurple;
            Item.defense = 8;
        }
		
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<OlympianChestplate>() && legs.type == ModContent.ItemType<OlympianLeggings>();
        }

        public override void RegisterEquipStats()
        {
			setDamageMap.GetCritChance(DamageClass.Generic) += 60;
			AddSetEffect<OlympianHelmetSetDescEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.GladiatorHelmet);
            recipe.AddIngredient(ModContent.ItemType<GoddessFeather>(), 6);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}

    public class OlympianHelmetSetDescEffect : EquipmentEffectData
    {

    }
}