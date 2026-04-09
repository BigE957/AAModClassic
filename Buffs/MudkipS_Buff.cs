using AAModClassic.Items.Pets;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class MudkipS_Buff : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Shiny Mudkip");
			// Description.SetDefault("So I heard you like mudkips");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<AAPlayer>().MudkipS = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<MudkipS>()] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.GetSource_FromThis(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<MudkipS>(), 0, 0f, player.whoAmI, 0f, 0f);
			}
        }
	}
}