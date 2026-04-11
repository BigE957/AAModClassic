using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;
using AAModClassic.Buffs;
using AAModClassic.Items.Armor.Doomite;
using AAModClassic.Items.Materials;

namespace AAModClassic.Items.Armor.Chaos
{
    [AutoloadEquip(EquipType.Head)]
	public class ChaosVisor : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			base.SetStaticDefaults();
			// DisplayName.SetDefault("Chaos Visor");
            // Tooltip.SetDefault(@"30% increased minion damage");
        }

		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 24;
            Item.value = Item.sellPrice(0, 5, 0, 0);
            Item.rare = ItemRarityID.Lime;
            Item.defense = 15;
        }
		
		public override void UpdateEquip(Player player)
        {
            player.GetDamage(DamageClass.Summon) += 0.3f;
        }

        public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == ModContent.ItemType<ChaosDou>() && legs.type == ModContent.ItemType<ChaosGreaves>();
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAModClassic.Common.ChaosVisorBonus");
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.ChaosSu = true;
            player.maxMinions += 4;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(ModContent.BuffType<DragonSpirit_Buff>()) == -1)
                {
                    player.AddBuff(ModContent.BuffType<DragonSpirit_Buff>(), 3600, true);
                }
                if (player.ownedProjectileCounts[ModContent.ProjectileType<DragonSpirit>()] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<DragonSpirit>(), 55, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ModContent.ItemType<DoomiteVisor>());
			recipe.AddIngredient(ModContent.ItemType<ChaosPrism>(), 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}