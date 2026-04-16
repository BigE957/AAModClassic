using Terraria;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using AAModClassic.___Content.Void.___PreHardmode.Items.Materials;
using AAModClassic.___Content.Snow.___PreHardmode.Items.Materials;

namespace AAModClassic.___Content.Mire.___PreHardmode.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
	public class AbyssalHelmet : BaseAAItem
	{
		public override void SetStaticDefaults()
        {
            base.SetStaticDefaults();
            // DisplayName.SetDefault("Abyssal Fukumen");
            /* Tooltip.SetDefault(@"35% increased movement speed
15% increased ranged damage
Weightless as shadow itself"); */
        }

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.rare = ItemRarityID.LightRed;
			Item.defense = 6;
		}

        public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Ranged) += .15f;
            player.moveSpeed += .35f;
            player.GetModPlayer<AAPlayer>().MaxMovespeedboost += .35f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == ModContent.ItemType<AbyssalChestplate>() && legs.type == ModContent.ItemType<AbyssalLeggings>();
		}

		public override void UpdateArmorSet(Player player)
		{
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.AbyssalBonus");
            player.GetModPlayer<AAPlayer>().depthSet = true;
            player.aggro -= 3;
            player.ammoCost80 = true;
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