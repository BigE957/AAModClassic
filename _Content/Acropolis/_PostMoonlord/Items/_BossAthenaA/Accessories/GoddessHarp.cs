using AAModClassic.Utilities.AbstractsLikeDigitalCircus;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Acropolis._PostMoonlord.Items._BossAthenaA.Accessories
{
    public class GoddessHarp : BaseAAItem, ILocalizedModType
    {
        public new string LocalizationCategory => "Items.Accessories";
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Goddess Harp");
			/* Tooltip.SetDefault(@"Summons the seraph queen herself to fight with you
Athena is boosted by minion damage"); */
        }

	    public override void SetDefaults()
	    {
	        Item.width = 20;
	        Item.height = 26;
            Item.value = Item.buyPrice(0, 15, 0, 0);
            Item.rare = ItemRarityID.Purple;
	        Item.accessory = true;
            Item.expert = true;
            Item.expertOnly = true;
	    }

        public override bool CanEquipAccessory(Player player, int slot, bool modded)/* tModPorter Suggestion: Consider using new hook CanAccessoryBeEquippedWith */
        {
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            if (modPlayer.Seraph)
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
                    if (player.FindBuffIndex(ModContent.BuffType<GoddessHarp_Buff>()) == -1)
                    {
                        player.AddBuff(ModContent.BuffType<GoddessHarp_Buff>(), 3600, true);
                    }
                    if (player.ownedProjectileCounts[ModContent.ProjectileType<GoddessHarp_Athena>()] < 1)
                    {
                        Projectile.NewProjectile(Item.GetSource_ReleaseEntity(), player.Center.X, player.Center.Y, 0f, -1f, ModContent.ProjectileType<GoddessHarp_Athena>(), (int)player.GetDamage(DamageClass.Summon).ApplyTo(100f), 2f, Main.myPlayer, 0f, 0f);
                    }
                }
			}
		}
	}
}
