using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Mire.___PreHardmode.Items.Armor;
using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Mire.Buffs;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Mire._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class DreadMoonHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.DreadMoon";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dread Moon Fukumen");
			/* Tooltip.SetDefault(@"24% increased ranged critical chance
20% increased movement speed
+15 Max Life
The abyssal wrath of the Mire rests in this armor"); */

		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 22;
			Item.value = 3000000;
			Item.defense = 36;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DreadMoonChestplate>() && legs.type == ModContent.ItemType<DreadMoonLeggings>();
		}

        public override void RegisterEquipStats()
        {
            damageMap.GetCritChance(DamageClass.Ranged) += 24;
            AddEffect(new MovementSpeedEffect(0.20f));
            AddEffect(new MaxRunSpeedEffect(0.20f));
            AddEffect(new MaxLifeEffect(15));

            AddSetEffect(new BuffImmunityEffect(BuffID.OnFire, BuffID.CursedInferno, BuffID.Frostburn, BuffID.Burning));
            AddSetEffect(new EmitLightFromPlayerEffect(0.8f, 0.95f, 1f)); // shine potion
			AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Ranged, (ModContent.BuffType<Moonraze_Buff>(), 600)));
            AddSetEffect<DreadMoonHelmetSetDescEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<EventideAbyssiumBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<DreadScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<DepthHelmet>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();
		}
	}

    public class DreadMoonHelmetSetDescEffect : EquipmentEffectData
    {

    }
}