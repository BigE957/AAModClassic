using Terraria;
using Terraria.ModLoader;

namespace AAMod.Buffs
{
    public class Lunamini : ModBuff
	{
		public override void SetStaticDefaults()
		{
			// DisplayName and Description are automatically set from the .lang files, but below is how it is done normally.
			// DisplayName.SetDefault("Lunamini");
			// Description.SetDefault("A small lunar bee follows you");
			Main.buffNoTimeDisplay[Type] = true;
			Main.vanityPet[Type] = true;
		}

		public override void Update(Player player, ref int buffIndex)
		{
			player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<AAPlayer>().Lunamini = true;
			bool petProjectileNotSpawned = player.ownedProjectileCounts[Mod.Find<ModProjectile>("Lunamini").Type] <= 0;
			if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
			{
				Projectile.NewProjectile(player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, Mod.Find<ModProjectile>("Lunamini").Type, 0, 0f, player.whoAmI, 0f, 0f);
			}
		}
	}
}