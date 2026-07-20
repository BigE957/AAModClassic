using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Rarities;
using AAModClassic.UI.World;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class DoomsdayHelmetMage : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Doomsday";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Doomsday Assault Visor");
			/* Tooltip.SetDefault(@"'The power to destroy entire planets rests in this armor'"); */
		}

        public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.value = 3000000;
			Item.defense = 32;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DoomsdayChestplate>() && legs.type == ModContent.ItemType<DoomsdayLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Magic) += .25f;
            damageMap.GetCritChance(DamageClass.Magic) += 18;

            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddSetEffect(new ManaCostMultiplierEffect(0.70f));
            else
                AddSetEffect(new ManaCostEffect(-0.80f));
            AddSetEffect<HunterEffect>();
			AddSetEffect<NightOwlEffect>();
			AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Magic, (BuffID.BrokenArmor, 1000)));
            AddSetEffect<DoomsdayHelmetSetDescEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<ApocalyptitePlate>(), 15);
            recipe.AddIngredient(ModContent.ItemType<UnstableSingularity>(), 5);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();
		}
	}

    public class DoomsdayHelmetSetDescEffect : EquipmentEffectData
    {
        
    }
}