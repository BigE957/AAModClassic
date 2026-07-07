using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Void.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class DarkDoomiteHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.DarkDoomite";
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Dark Doomite Helmet");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 0, 5, 0);
            Item.rare = ItemRarityID.Orange;
            Item.defense = 3;
        }
		
		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DarkDoomiteChestplate>() && legs.type == ModContent.ItemType<DarkDoomiteLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Summon) += 0.05f;

			AddSetEffect(new MaxMinionSlotEffect(2));
			setDamageMap.GetKnockback(DamageClass.Summon).Base += 1f;
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DoomiteScrap>(), 6);
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}
	}
}