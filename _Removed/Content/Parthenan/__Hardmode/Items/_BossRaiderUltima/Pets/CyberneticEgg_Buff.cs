using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Removed.Content.Parthenan.__Hardmode.Items._BossRaiderUltima.Pets
{
    public class CyberneticEgg_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Raidmini");
			// Description.SetDefault("Smol bot");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<AAPlayer>().Raidmini = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<CyberneticEgg_Raidmini>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, Mod.Find<ModProjectile>("Raidmini").Type, 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}