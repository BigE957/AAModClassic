using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Acropolis.__Hardmode.Items._BossAthena.Accessories
{
    public class SeraphHarp : BaseAAItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Seraph Harp");
			/* Tooltip.SetDefault(@"Summons a seraph to fight for you
Seraph is boosted by minion damage"); */
		}

	    public override void SetDefaults()
	    {
	        Item.width = 20;
	        Item.height = 26;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.Yellow;
	        Item.accessory = true;
            Item.expert = true;
            Item.expertOnly = true;
        }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (modPlayer.Athena)
            {
                return false;
            }
            return true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
			if (player.whoAmI == Main.myPlayer)
			{
                if (!hideVisual)
                {
                    if (player.FindBuffIndex(ModContent.BuffType<SeraphHarp_Buff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<SeraphHarp_Buff>(), 3600, true);
                    }
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<SeraphHarp_Seraph>()] < 1)
                    {
                        Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<SeraphHarp_Seraph>(), (int)player.GetDamage(DamageClass.Summon).ApplyTo(60f), 2f, Main.myPlayer, 0f, 0f);
                    }
                }
			}
		}
	}
}
