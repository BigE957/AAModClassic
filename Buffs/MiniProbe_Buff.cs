using AAModClassic.Items.Pets;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class MiniProbe_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Mini Probe");
			// Description.SetDefault("Seeks out life and treasure for you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<AAPlayer>().MiniProbe = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<MiniProbe>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<MiniProbe>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}