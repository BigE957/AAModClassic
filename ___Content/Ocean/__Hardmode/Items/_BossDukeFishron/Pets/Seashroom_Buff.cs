using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.___Content.Ocean.__Hardmode.Items._BossDukeFishron.Pets
{
    public class Seashroom_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Sharkron");
			// Description.SetDefault("It won't bite...much...");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<AAPlayer>().Sharkron = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Seashroom_Sharkron>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<Seashroom_Sharkron>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}