using AAModClassic;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class Cerberus : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cerberus");
            // Description.SetDefault("'Aww, that isn't so scary!'");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 1800000;
            player.GetModPlayer<AAPlayer>().Cerberus = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Items.Pets.Cerberus>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.GetSource_FromThis(), player.position.X + player.width / 2, player.position.Y + player.height / 2, 0f, 0f, ModContent.ProjectileType<Items.Pets.Cerberus>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}