using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Glowmoss_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Glowmoss Ball");
            // Description.SetDefault("Don't ask what makes it glows. Trust me.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.lightPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.GetModPlayer<AAPlayer>().Glowmoss = true;
            player.buffTime[buffIndex] = 18000;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Glowmoss>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<Glowmoss>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}