using AAModClassic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Items.Boss.Athena
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
                    if (player.FindBuffIndex(Mod.Find<ModBuff>("Seraph").Type) == -1)
                    {
                        player.AddBuff(Mod.Find<ModBuff>("Seraph").Type, 3600, true);
                    }
                    if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("Seraph").Type] < 1)
                    {
                        Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("Seraph").Type, (int)(60f * player.GetDamage(DamageClass.Summon)), 2f, Main.myPlayer, 0f, 0f);
                    }
                }
			}
		}
	}
}
