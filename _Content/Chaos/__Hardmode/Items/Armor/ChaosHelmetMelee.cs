using AAModClassic._Content.Chaos.__Hardmode.Items.Materials;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Accessories;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Armor;
using AAModClassic._Content.Inferno.Buffs;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic._Content.Snow.___PreHardmode.Items.Armor;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Chaos.__Hardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosHelmetMelee : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Chaos";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chaos Kabuto");
        }

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 20;
			Item.value = 100000;
            Item.rare = ItemRarityID.Lime;
            Item.defense = 26;
		}

        public override void RegisterEquipStats()
        {
            damageMap.GetDamage(DamageClass.Melee) += .25f;

            setDamageMap.GetAttackSpeed(DamageClass.Melee) += .10f;
            AddSetEffect(new AggroEffect(5));
            AddSetEffect<DragonsGuardEffect>();
            AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Melee, (ModContent.BuffType<DragonFire_Buff>(), 180), (ModContent.BuffType<HydraToxin_Buff>(), 180)));
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ChaosChestplate>() && legs.type == ModContent.ItemType<ChaosLeggings>();
        }

        public override void AddRecipes()
        {
            Recipe recipe;
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<BlazingHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
            recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RaiderHelmet>());
            recipe.AddIngredient(ModContent.ItemType<ChaosPrism>());
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
	}
}