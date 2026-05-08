using AAModClassic.Projectiles.Zero;
using Microsoft.Xna.Framework;
using System;
using Terraria;
using Terraria.ModLoader;

namespace AAModClassic._Content._EX._PostMoonlord.Items.Weapons.RuneBook
{
    public class EnergyRune : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Energy Rune");
        }
        public override void SetDefaults()
        {
            Projectile.width = 24;
            Projectile.height = 20;
            Projectile.timeLeft = 18000;
            Projectile.timeLeft *= 5;
            Projectile.minionSlots = 0f;
            Projectile.penetrate = -1;
            Projectile.tileCollide = false;
            Projectile.timeLeft *= 5;
            Projectile.minion = true;
            Projectile.netImportant = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.damage = 100;
        }
        int maxlife = 0;
        int target = -1;
        public override void AI()
        {
            Lighting.AddLight((int)(Projectile.position.X + Projectile.width / 2) / 16, (int)(Projectile.position.Y + Projectile.height / 2) / 16, 1f, 0.95f, 0.8f);
            bool flag64 = Projectile.type == ModContent.ProjectileType<EnergyRune>();
            Player player = Main.player[Projectile.owner];
            AAPlayer modPlayer = player.GetModPlayer<AAPlayer>();
            player.AddBuff(ModContent.BuffType<CCRune_Buff>(), 3600);
            if (!modPlayer.CCBook)
            {
                Projectile.active = false;
                return;
            }
            if (flag64)
            {
                if (player.dead)
                {
                    modPlayer.WeakCCRune = false;
                }
                if (modPlayer.WeakCCRune || modPlayer.CCBook)
                {
                    Projectile.timeLeft = 2;
                }
            }
            
            float num633 = 700f;
            float num634 = 800f;
            float num635 = 1200f;
            float num636 = 150f;
            float num637 = 0.05f;
            for (int num638 = 0; num638 < 1000; num638++)
            {
                bool flag23 = Main.projectile[num638].type == ModContent.ProjectileType<EnergyRune>();
                if (num638 != Projectile.whoAmI && Main.projectile[num638].active && Main.projectile[num638].owner == Projectile.owner && flag23 && Math.Abs(Projectile.position.X - Main.projectile[num638].position.X) + Math.Abs(Projectile.position.Y - Main.projectile[num638].position.Y) < Projectile.width)
                {
                    if (Projectile.position.X < Main.projectile[num638].position.X)
                    {
                        Projectile.velocity.X = Projectile.velocity.X - num637;
                    }
                    else
                    {
                        Projectile.velocity.X = Projectile.velocity.X + num637;
                    }
                    if (Projectile.position.Y < Main.projectile[num638].position.Y)
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y - num637;
                    }
                    else
                    {
                        Projectile.velocity.Y = Projectile.velocity.Y + num637;
                    }
                }
            }
            bool flag24 = false;
            if (Projectile.ai[0] == 2f)
            {
                Projectile.ai[1] += 1f;
                Projectile.extraUpdates = 1;
                Projectile.rotation = Projectile.velocity.ToRotation() + 1.57f;
                
                if (Projectile.ai[1] > 40f)
                {
                    Projectile.ai[1] = 1f;
                    Projectile.ai[0] = 0f;
                    Projectile.extraUpdates = 0;
                    Projectile.numUpdates = 0;
                    Projectile.netUpdate = true;
                }
                else
                {
                    flag24 = true;
                }
            }
            if (flag24)
            {
                return;
            }
            bool flag25 = false;
            Vector2 vector46 = Projectile.position;
            if (Projectile.ai[0] != 1f)
            {
                Projectile.tileCollide = false;
            }
            if (Projectile.tileCollide && WorldGen.SolidTile(Framing.GetTileSafely((int)Projectile.Center.X / 16, (int)Projectile.Center.Y / 16)))
            {
                Projectile.tileCollide = false;
            }
            for (int num645 = 0; num645 < 200; num645++)
            {
                NPC nPC2 = Main.npc[num645];
                if (nPC2.CanBeChasedBy(Projectile, false))
                {
                    float num646 = Vector2.Distance(nPC2.Center, Projectile.Center);
                    if ((num646 < num633 && Main.npc[num645].life > maxlife) && nPC2.active && nPC2.life > 0 && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, nPC2.position, nPC2.width, nPC2.height))
                    {
                        maxlife = Main.npc[num645].life;
                        target = num645;
                    }
                }
            }
            if(target > -1)
            {
                vector46 = Main.npc[target].Center;
                flag25 = true;
            }
            float num647 = num634;
            if (flag25)
            {
                num647 = num635;
            }
            if (Vector2.Distance(player.Center, Projectile.Center) > num647)
            {
                Projectile.ai[0] = 1f;
                Projectile.tileCollide = false;
                Projectile.netUpdate = true;
            }
            if (flag25 && Projectile.ai[0] == 0f)
            {
                Vector2 vector = vector46 - Projectile.Center - new Vector2(0, 50f + Main.npc[target].height/2);
                float num639 = 7f;
                if (vector.Length() > 200f && num639 < 10f)
                {
                    num639 = 10f;
                }
                if (vector.Length() > 200f)
                {
                    vector.Normalize();
                    vector *= num639;
                    Projectile.velocity = (Projectile.velocity * 40f + vector) / 41f;
                }
                else if(vector.Length() < 40f && (Projectile.velocity.X != 0f || Projectile.velocity.Y != 0f))
                {
                    vector.Normalize();
                    vector *= num639;
                    Projectile.velocity = (Projectile.velocity + vector * 40f) / 41f;
                }
                if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                {
                    Projectile.velocity.X = -0.02f;
                    Projectile.velocity.Y = -0.01f;
                }
            }
            else
            {
                bool flag26 = false;
                if (!flag26)
                {
                    flag26 = Projectile.ai[0] == 1f;
                }
                float num650 = 6f;
                if (flag26)
                {
                    num650 = 15f;
                }
                Vector2 center2 = Projectile.Center;
                Vector2 vector48 = player.Center - center2 + new Vector2(0f, -60f);
                float num651 = vector48.Length();
                if (num651 > 200f && num650 < 8f)
                {
                    num650 = 8f;
                }
                if (num651 < num636 && flag26 && !Collision.SolidCollision(Projectile.position, Projectile.width, Projectile.height))
                {
                    Projectile.ai[0] = 0f;
                    Projectile.netUpdate = true;
                }
                if (num651 > 2000f)
                {
                    Projectile.position.X = Main.player[Projectile.owner].Center.X - Projectile.width / 2;
                    Projectile.position.Y = Main.player[Projectile.owner].Center.Y - Projectile.height / 2;
                    Projectile.netUpdate = true;
                }
                if (num651 > 70f)
                {
                    vector48.Normalize();
                    vector48 *= num650;
                    Projectile.velocity = (Projectile.velocity * 40f + vector48) / 41f;
                }
                else if (Projectile.velocity.X == 0f && Projectile.velocity.Y == 0f)
                {
                    Projectile.velocity.X = -0.15f;
                    Projectile.velocity.Y = -0.05f;
                }
            }
            
            if (Projectile.ai[1] > 0f)
            {
                Projectile.ai[1] += Main.rand.Next(1, 4);
            }
            if (Projectile.ai[1] > 40f)
            {
                Projectile.ai[1] = 0f;
                Projectile.netUpdate = true;
            }
            if (Projectile.ai[0] == 0f)
            {
                float scaleFactor3 = 8f;
				int num658 = ModContent.ProjectileType<Neutralizer>();
				if (flag25 && Projectile.ai[1] == 0f)
				{
					Projectile.ai[1] += 1f;
					if (Main.myPlayer == Projectile.owner && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, vector46, 0, 0))
					{
						Vector2 value19 = vector46 - Projectile.Center;
						value19.Normalize();
						value19 *= scaleFactor3;
						int num659 = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center.X, Projectile.Center.Y, value19.X, value19.Y, num658, Projectile.damage, 0f, Main.myPlayer, 0f, 0f);
                        Main.projectile[num659].minion = true;
                        Main.projectile[num659].timeLeft = 300;
						Projectile.netUpdate = true;
					}
				}
            }
        }
    }
}