using AAModClassic._Content.Acropolis.__Hardmode.Items._BossAthena.Accessories;
using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;
using AAModClassic.Globals;
using AAModClassic.Rarities;
using AAModClassic.UI.World;
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
    public class ChampionHelmetMage : EquipAbstract, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Armor.Champion";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Champion Hood");
            /* Tooltip.SetDefault(@"32% increased Magic damage
10% increased non-magic damage
25% increased Magic critical strike chance
25% reduced Mana consumption
150 increased maximum mana
The armor of a champion feared across the land"); */
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(3, 0, 0, 0);
            Item.rare = ModContent.RarityType<SuperancientsRarity>();
            Item.defense = 30;
		}

        

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<ChampionChestplate>() && legs.type == ModContent.ItemType<ChampionLeggings>();
		}

        public override void RegisterEquipEffects()
        {
            damageMap.GetDamage(DamageClass.Magic) += .22f;
            damageMap.GetDamage(DamageClass.Generic) += .1f;
            damageMap.GetCritChance(DamageClass.Magic) += 25;
            AddEffect(new MaxManaEffect(150));
            if (!WorldTypeSystem.IsWorldOptionEnabled(AAWorldOption.Unofficial))
                AddEffect(new ManaCostMultiplierEffect(0.75f));
            else 
                AddEffect(new ManaCostEffect(-0.25f));

            AddSetEffect<ChampionHelmetMageSetEffect>();
            AddSetEffect<ChampionHelmetMageSetDescEffect>();
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

    public class ChampionHelmetMageSetDescEffect : EquipmentEffectData
    {

    }
}