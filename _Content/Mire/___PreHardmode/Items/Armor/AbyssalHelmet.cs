using AAModClassic._Content.Snow.___PreHardmode.Items.Materials;
using AAModClassic._Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class AbyssalHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Abyssal";
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Abyssal Fukumen");
            /* Tooltip.SetDefault(@"'Weightless as shadow itself'"); */
        }

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 6;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<AbyssalChestplate>() && legs.type == ModContent.ItemType<AbyssalLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Ranged) += .15f;
            AddEffect(new MovementSpeedEffect(0.35f));
            AddEffect(new MaxRunSpeedEffect(0.35f));

            AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Ranged, (BuffID.Poisoned, 180)));
            if (WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddSetEffect(new AggroEffect(-300));
            else
                AddSetEffect(new AggroEffect(-3));
            AddSetEffect<AmmoCost80Effect>();

        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DepthHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<RelicBar>(), 5);
            recipe.AddIngredient(ItemID.Coral, 5);
            recipe.AddIngredient(ModContent.ItemType<DoomiteBar>(), 5);
            recipe.AddTile(TileID.DemonAltar);
            recipe.Register();
        }
    }
}