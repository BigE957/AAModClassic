using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev.__Hardmode.Items.Pets
{
    public class K9Collar_Buff : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("K9 Unit");
            // Description.SetDefault("Bork.");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<AAPlayer>().K9 = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<K9Collar_K9Unit>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<K9Collar_K9Unit>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}