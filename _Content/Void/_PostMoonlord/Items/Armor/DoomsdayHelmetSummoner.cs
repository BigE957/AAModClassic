using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Void._PostMoonlord.Items.Materials;
using AAModClassic.Rarities;
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
    public class DoomsdayHelmetSummoner : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.Doomsday";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doomsday Tactical Visor");
            /* Tooltip.SetDefault(@"'The power to destroy entire planets rests in this armor'"); */
        }

        public override void SetDefaults()
        {
            Item.width = 18;
            Item.height = 18;
            Item.value = 3000000;
            Item.defense = 28;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DoomsdayChestplate>() && legs.type == ModContent.ItemType<DoomsdayLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Summon) += .50f;

            AddSetEffect(new MaxMinionSlotEffect(5));
            AddSetEffect<HunterEffect>();
            AddSetEffect<NightOwlEffect>();
            AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Summon, (BuffID.BrokenArmor, 1000)));
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
}