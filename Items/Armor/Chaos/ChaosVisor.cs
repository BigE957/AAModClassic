using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

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
            return body.type == Mod.Find<ModItem>("ChaosDou").Type && legs.type == Mod.Find<ModItem>("ChaosGreaves").Type;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = Language.GetTextValue("Mods.AAMod.Common.ChaosVisorBonus");
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            modPlayer.ChaosSu = true;
            player.maxMinions += 4;
            if (player.whoAmI == Main.myPlayer)
            {
                if (player.FindBuffIndex(Mod.Find<ModBuff>("DragonSpirit").Type) == -1)
                {
                    player.AddBuff(Mod.Find<ModBuff>("DragonSpirit").Type, 3600, true);
                }
                if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("DragonSpirit").Type] < 1)
                {
                    Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("DragonSpirit").Type, 55, 0f, Main.myPlayer, 0f, 0f);
                }
            }
        }
		
		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(Mod.Find<ModItem>("DoomiteVisor").Type);
			recipe.AddIngredient(null, "ChaosCrystal", 1);
            recipe.AddTile(TileID.MythrilAnvil);
			recipe.Register();
		}
	}
}