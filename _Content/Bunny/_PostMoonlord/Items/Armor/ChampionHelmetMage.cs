using Terraria;
using Terraria.ModLoader;
using Terraria.Localization;
using Terraria.ID;
using AAModClassic.Globals;
using AAModClassic._Content.Bunny.__Hardmode.Items.Armor;
using AAModClassic._Content.Bunny._PostMoonlord.Items.Materials;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using AAModClassic.Rarities;
using AAModClassic._Content.Chaos._PostMoonlord.Items.Tiles.Functional;

namespace AAModClassic._Content.Bunny._PostMoonlord.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class ChampionHelmetMage : BaseAAItem, ILocalizedModType
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

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Equipset.ChampionHoodBonus");

            AAPlayer mplayer = player.GetModPlayer<AAPlayer>();

            mplayer.ChampionMa = true;
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Magic) += .22f;
            player.GetDamage(DamageClass.Generic) += .1f;
            player.GetCritChance(DamageClass.Magic) += 25;
            player.manaCost *= .75f;
            player.statManaMax2 += 150;
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
}