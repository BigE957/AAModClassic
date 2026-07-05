using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic._Content.Inferno.___PreHardmode.Items.Armor;
using AAModClassic._Content.Inferno._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using AAModClassic.Utilities.Attributes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;


namespace AAModClassic._Content.Inferno._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    [AutoloadEquipGlow(EquipType.Head)]
    public class DraconianSunHelmet : EquipAbstract, ILocalizedModType
	{
        public new string LocalizationCategory => "Items.Armor.DraconianSun";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Draconian Sun Kabuto");
			/* Tooltip.SetDefault(@"'The blazing fury of the Inferno rests in this armor'"); */

		}

		public override void SetDefaults()
		{
			Item.width = 24;
			Item.height = 22;
			Item.value = 3000000;
			Item.defense = 38;
            Item.rare = ModContent.RarityType<AncientsRarity>();
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<DraconianSunChestplate>() && legs.type == ModContent.ItemType<DraconianSunLeggings>();
		}

        public override void RegisterEquipStats()
        {
            damageMap.GetCritChance(DamageClass.Melee) += 20;
			AddEffect(new EnduranceEffect(0.03f));
			AddEffect(new MaxLifeEffect(25));

			AddSetEffect(new BuffImmunityEffect(BuffID.Chilled, BuffID.Frozen));
			AddSetEffect(new EmitLightFromPlayerEffect(0.8f, 0.95f, 1f)); // shine potion
			AddSetEffect(new AttacksInflictBuffEffect(DamageClass.Melee, (BuffID.Daybreak, 600)));
			AddSetEffect<DraconianSunHelmetSetDescEffect>();
        }

        public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<DaybreakIncineriteBar>(), 15);
            recipe.AddIngredient(ModContent.ItemType<CrucibleScale>(), 5);
            recipe.AddIngredient(ModContent.ItemType<KindledHelmet>(), 1);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
			recipe.Register();
		}
	}

    public class DraconianSunHelmetSetDescEffect : EquipmentEffectData
    {
        
    }
}