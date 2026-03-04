using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;

using Terraria.Graphics.Shaders;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;

namespace AAModClassic.NPCs.Bosses.Zero.Protocol
{
    public class ZeroMini : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("ZER0 SELF 0RGANIZATI0N");
            Main.npcFrameCount[NPC.type] = 12; 
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 3500;
            NPC.damage = 100;
            NPC.defense = 50;
            NPC.knockBackResist = 0f;
            NPC.width = 52;
            NPC.height = 52;
            NPC.friendly = false;
            NPC.aiStyle = -1;
            NPC.value = Item.sellPrice(0, 0, 0, 0);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Sounds/Zerohit");
            NPC.DeathSound = Mod.GetLegacySoundSlot(SoundType.Sound, "Sounds/Sounds/ZeroDeath");
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = (int)(NPC.lifeMax * 0.8f);
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return AAColor.Oblivion;
        }

        public Vector2 point = new Vector2(0f,0f);

        int body = -1;
        public override void AI()
        {
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, Mod.Find<ModNPC>("ZeroProtocol").Type, -1, null);
                if (npcID >= 0) body = npcID;
            }

            if (body == -1) return;

            NPC zero = Main.npc[body];

            NPC.TargetClosest(true);

            Player player = Main.player[NPC.target];

            if(NPC.ai[0] == 0)
            {
                NPC.velocity *= 0;
            }
            else if(NPC.ai[0] == 1)
            {
                NPC.ai[1] ++;
                if(NPC.ai[1] % 180 == 60)
                {
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0f, -14f), Mod.Find<ModProjectile>("ProtoStar").Type, NPC.damage/2, 3);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(0f, 14f), Mod.Find<ModProjectile>("ProtoStar").Type, NPC.damage/2, 3);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(14f, 0f), Mod.Find<ModProjectile>("ProtoStar").Type, NPC.damage/2, 3);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-14f, 0f), Mod.Find<ModProjectile>("ProtoStar").Type, NPC.damage/2, 3);
                    }
                }
                if(NPC.ai[1] % 180 == 120)
                {
                    if(Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(10f, -10f), Mod.Find<ModProjectile>("ProtoStar").Type, NPC.damage/2, 3);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-10f, -10f), Mod.Find<ModProjectile>("ProtoStar").Type, NPC.damage/2, 3);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(-10f, 10f), Mod.Find<ModProjectile>("ProtoStar").Type, NPC.damage/2, 3);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X, NPC.Center.Y), new Vector2(10f, 10f), Mod.Find<ModProjectile>("ProtoStar").Type, NPC.damage/2, 3);
                    }
                }
            }
            else if(NPC.ai[0] == 2)
            {
                NPC.velocity *= 0;
                if(Main.netMode != NetmodeID.MultiplayerClient)
                {
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + new Vector2(30, 30), new Vector2(10, 10), ModContent.ProjectileType<EchoRay>(), NPC.damage / 3, 0f, Main.myPlayer, 6.2831855f / 750f, NPC.whoAmI);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + new Vector2(-30, 30), new Vector2(-10, 10), ModContent.ProjectileType<EchoRay>(), NPC.damage / 3, 0f, Main.myPlayer, 6.2831855f / 750f, NPC.whoAmI);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + new Vector2(30, -30), new Vector2(10, -10), ModContent.ProjectileType<EchoRay>(), NPC.damage / 3, 0f, Main.myPlayer, 6.2831855f / 750f, NPC.whoAmI);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + new Vector2(-30, -30), new Vector2(-10, -10), ModContent.ProjectileType<EchoRay>(), NPC.damage / 3, 0f, Main.myPlayer, 6.2831855f / 750f, NPC.whoAmI);
                }
                NPC.ai[0] = 3;
                NPC.ai[1] = 0;
                NPC.netUpdate = true;
            }
            else if(NPC.ai[0] == 3)
            {
                NPC.velocity *= 0;
                NPC.ai[1]++;
                if(NPC.ai[1] >= 90)
                {
                    NPC.ai[1] = 0;
                    NPC.ai[0] = 1;
                    NPC.netUpdate = true;
                }
                return;
            }
            else
            {
                if (Main.rand.Next(2) == 0)
                {
                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<GlitchBomb>(), ref NPC.ai[3], 50, NPC.damage / 3, 10, true);
                }
                else
                {
                    BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<GlitchRocket>(), ref NPC.ai[3], 50, NPC.damage / 3, 10, true);
                }
            }

            NPC.ai[2] ++;

            if(NPC.ai[2] > 360 && NPC.ai[0] != 2)
            {
                NPC.ai[0] = Main.rand.Next(2) == 0? 1:3;
                NPC.netUpdate = true;
            }

            if(zero.ai[0] == 5 && zero.ai[3] == 1f)
            {
                NPC.ai[0] = 2;
                NPC.netUpdate = true;
            }
            else if((NPC.ai[2] > 360 && NPC.ai[0] != 2) || NPC.ai[0] == 2)
            {
                NPC.ai[0] = Main.rand.Next(2) == 0? 1:3;
                NPC.ai[1] = 0;
                NPC.netUpdate = true;
                NPC.ai[2] = 0;
            }

            if(NPC.ai[0] != 3 && NPC.ai[0] != 2 && NPC.ai[0] != 0)
            {
                if((NPC.Center - player.Center).Length() > 400f)
                {
                    MoveToPoint(player.Center);
                }
                else
                {
                    BaseAI.AISkull(NPC, ref Move, true, 14, 350, .04f, .05f);
                }
            }
        }

        public float[] Move = new float[4];

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 12f;

            if (Vector2.Distance(NPC.Center, point) > 500)
            {
                moveSpeed = 18f;
            }

            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 100f)
            {
                moveSpeed *= 0.5f;
            }
            if (length < 50f)
            {
                moveSpeed *= 0.5f;
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

        int Frame = 0;
        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 10)
            {
                NPC.frameCounter = 0;
                Frame += 1;
            }

            if (Frame < 0)
            {
                Frame = 0;
            }
            else if (Frame < 6)
            {
                NPC.ai[0] = 0;
            }
            else if (Frame > 11)
            {
                Frame = 6;
            }

            NPC.frame.Y = frameHeight * Frame;
        }

        public override void OnKill()
        {
            DeathDust();
        }

        public void DeathDust()
        {
            Vector2 position = NPC.Center + (Vector2.One * -20f);
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.Electric, 0f, 0f, 100, default, 1.5f);
                Main.dust[num86].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num86].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.Electric, 0, 0, 100, new Color(), 2f);
                Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                num88 = Dust.NewDust(position, num84, height3, DustID.Electric, 0, 0, 100, new Color(), 2f);
                Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 5; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, DustID.Electric, 0, 0, 100, new Color(), 2f);
                Main.dust[num90].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 15; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.Electric, 0, 0, 100, new Color(), 2f);
                Main.dust[num92].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num92].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }

    }
}
