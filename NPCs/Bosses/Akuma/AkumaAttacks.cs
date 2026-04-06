using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using System;
using Terraria;
using AAModClassic.NPCs.Bosses.Akuma.Awakened;

namespace AAModClassic.NPCs.Bosses.Akuma
{
    public class AkumaAttacks
	{
        public static void Dragonfire(NPC npc, Mod mod, bool Awakened = false)
        {
            Player player = Main.player[npc.target];
            float num72 = 10;
            Vector2 vector2 = new Vector2(player.position.X + (player.width * 0.5f) + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
            vector2.X = ((vector2.X + player.Center.X) / 2f) + Main.rand.Next(-700, 700);
            vector2.Y -= 100 * Main.rand.Next(3);
            float num78 = player.Center.X + Main.screenPosition.X - vector2.X + (Main.rand.Next(-40, 41) * 0.03f);
            float num79 = player.Center.Y + Main.screenPosition.Y - vector2.Y;
            if (num79 < 0f)
            {
                num79 *= -1f;
            }
            if (num79 < 20f)
            {
                num79 = 20f;
            }
            float num80 = (float)Math.Sqrt((num78 * num78) + (num79 * num79));
            num80 = num72 / num80;
            num79 *= num80;
            float num115 = num79 + (Main.rand.Next(41) * 0.02f);
            int projType = Awakened ? ModContent.ProjectileType<AkumaAMeteor>() : ModContent.ProjectileType<AkumaMeteor>();
            Projectile.NewProjectile(npc.GetSource_FromThis(), vector2.X, vector2.Y, 0, num115 * 1.5f, projType, npc.damage / 4, 0, player.whoAmI, 0f, 0.5f + ((float)Main.rand.NextDouble() * 0.3f));
        }

        public static void Eruption(NPC npc, Mod mod)
        {
            Player player = Main.player[npc.target];
            float num72 = 15;
            Vector2 vector2 = new Vector2(player.position.X + (player.width * 0.5f) + Main.rand.Next(201) * -(float)player.direction + (Main.mouseX + Main.screenPosition.X - player.position.X), player.MountedCenter.Y - 600f);
            vector2.X = ((vector2.X + player.Center.X) / 2f) + Main.rand.Next(-700, 700);
            vector2.Y -= 100;
            float num78 = player.Center.X + Main.screenPosition.X - vector2.X + (Main.rand.Next(-40, 41) * 0.03f);
            float num79 = player.Center.Y + Main.screenPosition.Y - vector2.Y;
            if (num79 < 0f)
            {
                num79 *= -1f;
            }
            if (num79 < 20f)
            {
                num79 = 20f;
            }
            float num80 = (float)Math.Sqrt((num78 * num78) + (num79 * num79));
            num80 = num72 / num80;
            num79 *= num80;
            float num115 = num79 + (Main.rand.Next(41) * 0.02f);
            Projectile.NewProjectile(npc.GetSource_FromThis(), vector2.X, vector2.Y, 0, num115 * 2f, ModContent.ProjectileType<AkumaRock>(), npc.damage / 4, 0, player.whoAmI, 0f, 0.5f + ((float)Main.rand.NextDouble() * 0.3f));
        }
    }
}