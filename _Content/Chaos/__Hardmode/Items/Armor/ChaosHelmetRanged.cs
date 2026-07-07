using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic._Content.Desert.___PreHardmode.Items.Armor;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.___PreHardmode.Items.Armor;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosHelmetRanged : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Chaos";
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Chaos Fukumen");
        }

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.value = 50000;
			Item.rare = ItemRarityID.Lime;
			Item.defense = 15;
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetCritChance(DamageClass.Ranged) += 24;

            setDamageMap.GetDamage(DamageClass.Ranged) += .25f;
            AddSetEffect(new AggroEffect(-7));
            AddSetEffect<AmmoCost75Effect>();
            AddSetEffect<NightOwlEffect>();
            AddSetEffect<HunterEffect>();
            AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Ranged, (ModContent.BuffType<DragonFire_Buff>(), 180), (ModContent.BuffType<HydraToxin_Buff>(), 180)));
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ChaosChestplate>() && legs.type == ModContent.ItemType<ChaosLeggings>();
        }

        public override void AddRecipes()
		{
            Recipe recipe;
            recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<AbyssalHelmet>());
			recipe.AddIngredient(ModContent.ItemType<ChaosPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DynaskullHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}