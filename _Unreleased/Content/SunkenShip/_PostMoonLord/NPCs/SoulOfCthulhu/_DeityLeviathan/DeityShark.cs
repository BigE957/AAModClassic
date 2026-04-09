using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityLeviathan
{
    public class DeityShark : ModNPC
	{

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Deity Shark");
            Main.npcFrameCount[NPC.type] = 4;
        }

        public override void SetDefaults()
        {
            NPC.width = 120;
            NPC.height = 24;
            NPC.aiStyle = -1;
            NPC.damage = 100;
            NPC.defense = 100;
            NPC.lifeMax = 100;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.alpha = 255;
        }

        public override void AI()
        {
            NPC.noTileCollide = true;
            int num985 = 90;
            if (NPC.target < 0 || NPC.target == 255 || Main.player[NPC.target].dead)
            {
                NPC.TargetClosest(false);
                NPC.direction = 1;
                NPC.netUpdate = true;
            }
            if (NPC.ai[0] == 0f)
            {
                NPC.ai[1] += 1f;
                int arg_2F288_0 = NPC.type;
                NPC.noGravity = true;
                NPC.dontTakeDamage = true;
                NPC.velocity.Y = NPC.ai[3];
                if (NPC.ai[1] >= num985)
                {
                    NPC.ai[0] = 1f;
                    NPC.ai[1] = 0f;
                    if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        NPC.ai[1] = 1f;
                    }
                    SoundEngine.PlaySound(SoundID.NPCDeath19, NPC.Center);
                    NPC.TargetClosest(true);
                    NPC.spriteDirection = NPC.direction;
                    Vector2 vector123 = Main.player[NPC.target].Center - NPC.Center;
                    vector123.Normalize();
                    NPC.velocity = vector123 * 16f;
                    NPC.rotation = NPC.velocity.ToRotation();
                    if (NPC.direction == -1)
                    {
                        NPC.rotation += 3.14159274f;
                    }
                    NPC.netUpdate = true;
                    return;
                }
            }
            else if (NPC.ai[0] == 1f)
            {
                NPC.noGravity = true;
                if (!Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                {
                    if (NPC.ai[1] < 1f)
                    {
                        NPC.ai[1] = 1f;
                    }
                }
                else
                {
                    NPC.alpha -= 15;
                    if (NPC.alpha < 150)
                    {
                        NPC.alpha = 150;
                    }
                }
                if (NPC.ai[1] >= 1f)
                {
                    NPC.alpha -= 60;
                    if (NPC.alpha < 0)
                    {
                        NPC.alpha = 0;
                    }
                    NPC.dontTakeDamage = false;
                    NPC.ai[1] += 1f;
                    if (Collision.SolidCollision(NPC.position, NPC.width, NPC.height))
                    {
                        if (NPC.DeathSound != null)
                        {
                            SoundEngine.PlaySound(NPC.DeathSound, NPC.position);
                        }
                        NPC.life = 0;
                        NPC.HitEffect(0, 10.0);
                        NPC.active = false;
                        return;
                    }
                }
                if (NPC.ai[1] >= 60f)
                {
                    NPC.noGravity = false;
                }
                NPC.rotation = NPC.velocity.ToRotation();
                if (NPC.direction == -1)
                {
                    NPC.rotation += 3.14159274f;
                    return;
                }
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            SoundEngine.PlaySound(SoundID.NPCDeath3, NPC.position);
            if (NPC.life <= 0)
            {
                Vector2 arg_98DC_0 = NPC.Center;
                for (int num207 = 0; num207 < 60; num207++)
                {
                    int num208 = 25;
                    int num209 = Dust.NewDust(NPC.Center - Vector2.One * num208, num208 * 2, num208 * 2, ModContent.DustType<Dusts.CthulhuDust>(), 0f, 0f, 0, default, 1f);
                    Dust dust47 = Main.dust[num209];
                    Vector2 vector7 = Vector2.Normalize(dust47.position - NPC.Center);
                    dust47.position = NPC.Center + vector7 * 25f * NPC.scale;
                    if (num207 < 30)
                    {
                        dust47.velocity = vector7 * dust47.velocity.Length();
                    }
                    else
                    {
                        dust47.velocity = vector7 * Main.rand.Next(45, 91) / 10f;
                    }
                    dust47.color = Main.hslToRgb((float)(0.40000000596046448 + Main.rand.NextDouble() * 0.20000000298023224), 0.9f, 0.5f);
                    dust47.color = Color.Lerp(dust47.color, Color.White, 0.3f);
                    dust47.noGravity = true;
                    dust47.scale = 0.7f;
                }
            }
        }
    }
}