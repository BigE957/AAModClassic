using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.Items.Pets
{
    public class DragonSoul_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Dragon Soul");
			// Description.SetDefault("Burns with the rage of a dragon");
			Main.buffNoTimeDisplay[Type] = true;
			Main.lightPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.GetModPlayer<ZAAPlayer>().DragonSoul = true;
			player.buffTime[buffIndex] = 18000;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<DragonSoul_DragonSoul>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<DragonSoul_DragonSoul>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}