using Terraria;
using Terraria.ModLoader;
using Terraria.ID;
using Terraria.Localization;
using AAModClassic.Items.Boss.Rajah;

namespace AAModClassic.Items.Armor.Hoodlum
{
    [AutoloadEquip(EquipType.Head)]
    public class HoodlumHood : BaseAAItem
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Hopping Hoodlum Hood");
            /* Tooltip.SetDefault(@"18% increased melee & minion Damage
Enemies are more likely to target you
Hopping Mad."); */
        }

        public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 24;
			Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Yellow;
            Item.defense = 13;
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<HoodlumShirt>() && legs.type == ModContent.ItemType<HoodlumPants>();
		}

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.HoodlumHoodBonus");
            if (player.statLife <= player.statLifeMax2 * .5f)
            {
                player.moveSpeed += .5f;
                player.GetDamage(DamageClass.Summon) += .5f;
                player.GetDamage(DamageClass.Melee) += .5f;
            }
        }

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Melee) += .18f;
            player.GetDamage(DamageClass.Summon) += .18f;
            player.aggro += 2;
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ModContent.ItemType<RajahPelt>(), 10);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
        }
    }
}