using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Pets
{
    public class RoyalStar_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Royal Kitten");
            // Description.SetDefault("'She's so pretty!'");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<AAPlayer>().RoyalKitten = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<RoyalStar_RoyalKitten>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<RoyalStar_RoyalKitten>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}