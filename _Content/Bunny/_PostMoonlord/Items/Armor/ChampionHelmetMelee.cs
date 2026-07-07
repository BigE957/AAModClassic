using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.Items;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ChampionHelmetMelee : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Champion";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Champion Greathelm");
            /* Tooltip.SetDefault(@"35% increased Melee damage & critical strike chance
10% increased non-melee damage
18% increased melee speed
'The armor of a champion feared across the land'"); */
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.defense = 39;
		}

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ChampionChestplate>() && legs.type == ModContent.ItemType<ChampionLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Generic) += .1f;
            damageMap.GetDamage(DamageClass.Melee) += .25f;
            damageMap.GetCritChance(DamageClass.Melee) += 35;
            damageMap.GetAttackSpeed(DamageClass.Melee) += .15f;

            AddSetEffect<ChampionHelmetMeleeSetEffect>();
            AddSetEffect<ChampionHelmetMeleeSetDescEffect>();
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<HoppingHoodlumHelmet>(), 1);
            recipe.AddIngredient(ModContent.ItemType<ChampionPlate>(), 10);
            recipe.AddTile(ModContent.TileType<AnyAncientCraftingStation_Tile>());
            recipe.Register();
        }
    }

    public class ChampionHelmetMeleeSetDescEffect : EquipmentEffectData
    {

    }
}