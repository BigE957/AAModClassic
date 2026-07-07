using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items._BossBroodmother.Pets
{
    public class ScorchedEgg_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Broodmini");
			// Description.SetDefault("Smol bab");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<ZAAPlayer>().Broodmini = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<ScorchedEgg_Broodmini>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<ScorchedEgg_Broodmini>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}