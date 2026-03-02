using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

using System.IO;
using Terraria.Graphics.Shaders;

namespace AAMod.NPCs.Bosses.Sag
{
    [AutoloadBossHead]
    public class Sag : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Sagittarius");
            Main.npcFrameCount[NPC.type] = 9;
		}

		public override void SetDefaults()
        {
            NPC.lifeMax = 6000;
            NPC.boss = true;
            NPC.defense = 20;
            NPC.damage = 35;
            NPC.width = 124;
            NPC.height = 206;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Sag");
            NPC.value = 80000f;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("SagBag").Type;
        }

        public float[] internalAI = new float[3];
        Vector2 targetPos;
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(targetPos.X);
                writer.Write(targetPos.Y);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                internalAI[0] = reader.ReadFloat();
                internalAI[1] = reader.ReadFloat();
                internalAI[2] = reader.ReadFloat();
                targetPos.X = reader.ReadFloat();
                targetPos.Y = reader.ReadFloat();
            }
        }

        bool lowHealth = false;

        public override void AI()
        {
            if (NPC.target == -1)
            {
                NPC.TargetClosest(true);
            }

            Player player = Main.player[NPC.target];

            #region Direction & Alpha

            if (NPC.ai[0] != 2)
            {
                if (player.Center.X > NPC.Center.X)
                {
                    NPC.direction = 1;
                }
                else
                {
                    NPC.direction = -1;
                }
            }
            else
            {
                if (NPC.velocity.X > 0)
                {
                    NPC.direction = 1;
                }
                else
                {
                    NPC.direction = -1;
                }
            }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            #endregion

            switch ((int)NPC.ai[0])
            {
                case 0:
                    if (!DeathCheck())
                        return;

                    int pos;
                    if (player.Center.X > NPC.Center.X) //If NPC's X position is less than the player's
                    {
                        pos = 300;
                    }
                    else //If NPC's X position is higher than the player's
                    {
                        pos = -300;
                    }

                    Vector2 wantedVelocity = player.Center - new Vector2(pos, 250);

                    MoveToPoint(wantedVelocity);

                    Shooting();

                    if (NPC.ai[1]++ > (Main.expertMode ? 480 : 600))
                    {
                        NPC.ai[0]++;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                    }
                    break;


                case 1:
                    if (!DeathCheck())
                        return;
                    if (++NPC.ai[1] > 30)
                    {
                        targetPos = player.Center;
                        targetPos.X += 1000 * (NPC.Center.X < targetPos.X ? -1 : 1);
                        Movement(targetPos, 0.5f);
                        if (NPC.ai[1] > 120 || Math.Abs(NPC.Center.Y - targetPos.Y) < 16) //initiate dash
                        {
                            NPC.ai[0]++;
                            NPC.rotation += NPC.velocity.X * 0.05f;
                            NPC.ai[1] = 0;
                            NPC.netUpdate = true;
                            int speed = NPC.life < NPC.lifeMax / 3 ? 15 : 18;
                            NPC.velocity.X = -speed * (NPC.Center.X < player.Center.X ? -1 : 1);
                            NPC.velocity.Y *= 0f;
                        }
                    }
                    else
                    {
                        NPC.velocity *= 0.9f; //decelerate briefly
                    }
                    NPC.rotation = 0;
                    break;

                case 2:

                    if (++NPC.ai[1] > 240 || (Math.Sign(NPC.velocity.X) > 0 ? NPC.Center.X > player.Center.X + 900 : NPC.Center.X < player.Center.X - 900))
                    {
                        NPC.ai[0] = 0;
                        NPC.ai[1] = 0;
                        NPC.ai[2] = 0;
                        NPC.ai[3] = Main.rand.Next(5);
                        internalAI[0] = 0;
                        NPC.netUpdate = true;
                    }
                    break;
                default:
                    NPC.ai[0] = 0;
                    goto case 0;
            }

            if (NPC.alpha > 0)
            {
                NPC.alpha -= 10;
            }
            if (NPC.alpha <= 0)
            {
                NPC.alpha = 0;
            }

            if (NPC.life < NPC.lifeMax / 3 && internalAI[1]++ % 90 == 0)
            {
                Vector2 SparkPos = NPC.Center + new Vector2(Main.rand.Next(-48, 48), 0);
                Vector2 SparkSpeed = new Vector2(Main.rand.Next(-4, 4), Main.rand.Next(0, 4));
                Projectile.NewProjectile(SparkPos, SparkSpeed, Mod.Find<ModProjectile>("SagSpark").Type, NPC.damage / 4, 1);

                for (int num242 = 0; num242 < 5; num242++)
                {
                    int num243 = Dust.NewDust(SparkPos, 0, 0, 226, SparkSpeed.X, SparkSpeed.Y, 0, default, 1f);
                    Main.dust[num243].scale = 0.5f;
                    Main.dust[num243].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                }

                if (!lowHealth && Main.netMode != 1)
                {
                    CombatText.NewText(NPC.getRect(), new Color(233, 46, 46), Lang.BossChat("SagChat"), true, true);
                    lowHealth = true;
                }
            }
            if (NPC.ai[0] != 2)
            {
                NPC.rotation = 0;
            }
        }

        public void Shooting()
        {
            Player player = Main.player[NPC.target];

            switch ((int)NPC.ai[3])
            {
                case 0:
                    BaseAI.ShootPeriodic(NPC, player.Center, player.width, player.height, ModContent.ProjectileType<SagShot>(), ref NPC.ai[2], 60, NPC.damage / 4, 9, false, new Vector2(-36 * NPC.direction, -51));
                    break;
                case 1:
                    if (Main.netMode != 1)
                    {
                        internalAI[0]++;
                        if (internalAI[0] > 180)
                        {
                            internalAI[0] = 0;
                            NPC.netUpdate = true;
                        }
                    }
                    if (internalAI[0] > 80)
                    {
                        BaseAI.ShootPeriodic(NPC, player.Center + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), player.width, player.height, ModContent.ProjectileType<SagiStar>(), ref NPC.ai[2], 20, NPC.damage / 4, 9, false, new Vector2(36 * NPC.direction, -51));
                    }
                    break;
                case 2:
                    if (Main.netMode != 1)
                    {
                        internalAI[0]++;
                        if (internalAI[0] > 210)
                        {
                            internalAI[0] = 0;
                            NPC.netUpdate = true;
                        }
                    }
                    if (internalAI[0] > 80 && internalAI[0] % 30 == 0)
                    {
                        Projectile.NewProjectile(NPC.Center, new Vector2(Main.rand.Next(3, 7) * NPC.direction, -6f), Mod.Find<ModProjectile>("SagBomb").Type, NPC.damage / 4, 3);
                    }
                    break;
                case 3:
                    if (Main.netMode != 1)
                    {
                        internalAI[0]++;
                        if (internalAI[0] > 240)
                        {
                            internalAI[0] = 0;
                            NPC.netUpdate = true;
                        }
                    }
                    if (internalAI[0] > 80)
                    {
                        BaseAI.ShootPeriodic(NPC, player.Center + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), player.width, player.height, ModContent.ProjectileType<SagRocket>(), ref NPC.ai[2], 40, NPC.damage / 4, 9, false);
                    }
                    break;
                case 4:
                    if (Main.netMode != 1)
                    {
                        internalAI[0]++;
                    }
                    if (internalAI[0] > 80)
                    {
                        BaseAI.ShootPeriodic(NPC, player.Center + new Vector2(Main.rand.Next(-10, 10), Main.rand.Next(-10, 10)), player.width, player.height, ModContent.ProjectileType<SagBlast>(), ref NPC.ai[2], 50, NPC.damage / 4, 9, false, new Vector2(36 * NPC.direction, -51));
                    }
                    break;
                default:
                    NPC.ai[3] = 0;
                    goto case 0;
            }

        }

        private void Movement(Vector2 targetPos, float speedModifier)
        {
            if (NPC.Center.X < targetPos.X)
            {
                NPC.velocity.X += speedModifier;
                if (NPC.velocity.X < 0)
                    NPC.velocity.X += speedModifier * 2;
            }
            else
            {
                NPC.velocity.X -= speedModifier;
                if (NPC.velocity.X > 0)
                    NPC.velocity.X -= speedModifier * 2;
            }
            if (NPC.Center.Y < targetPos.Y)
            {
                NPC.velocity.Y += speedModifier;
                if (NPC.velocity.Y < 0)
                    NPC.velocity.Y += speedModifier * 2;
            }
            else
            {
                NPC.velocity.Y -= speedModifier;
                if (NPC.velocity.Y > 0)
                    NPC.velocity.Y -= speedModifier * 2;
            }
            if (Math.Abs(NPC.velocity.X) > 30)
                NPC.velocity.X = 30 * Math.Sign(NPC.velocity.X);
            if (Math.Abs(NPC.velocity.Y) > 30)
                NPC.velocity.Y = 30 * Math.Sign(NPC.velocity.Y);
        }

        public override void FindFrame(int frameHeight)
        {
            int frameSpeed;
            if (NPC.ai[0] != 1)
            {
                frameSpeed = NPC.ai[0] == 2 ? 3 : 12 - (int)NPC.velocity.X;
                if (NPC.velocity.X != 0)
                {
                    if (NPC.frameCounter++ > frameSpeed)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y += frameHeight;
                        if (NPC.frame.Y > frameHeight * 3)
                        {
                            NPC.frame.Y = 0;
                        }
                    }
                }
            }
            else
            {
                frameSpeed = 7;
                if (NPC.frame.Y < frameHeight * 4)
                {
                    NPC.frame.Y = frameHeight * 4;
                }
                if (NPC.frameCounter++ > frameSpeed)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y > frameHeight * 8)
                    {
                        NPC.frame.Y = frameHeight * 6;
                    }
                }
            }
        }

        public bool DeathCheck()
        {
            AAPlayer modPlayer = Main.player[NPC.target].GetModPlayer<AAPlayer>();
            if (Main.player[NPC.target].dead || Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 5000 || !modPlayer.ZoneVoid)
            {
                NPC.TargetClosest(true);
                if (Main.player[NPC.target].dead || Vector2.Distance(Main.player[NPC.target].Center, NPC.Center) > 5000 || !modPlayer.ZoneVoid)
                {
                    NPC.velocity *= .7f;
                    NPC.alpha += 5;
                    if (NPC.alpha >= 255)
                    {
                        NPC.active = false;
                    }
                    if (!Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) <= 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) >= 6000f)
                    {
                        NPC.TargetClosest(true);
                    }
                    return false;
                }
            }
            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/SagBodyGore"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/SagHeadGore"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/SagLegGore"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/SagLegGore"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity * 0.2f, Mod.GetGoreSlot("Gores/SagNeckGore"), 1f); 
                Vector2 position = NPC.Center + (Vector2.One * -20f);
                int num84 = 40;
                int height3 = num84;
                for (int num85 = 0; num85 < 3; num85++)
                {
                    int num86 = Dust.NewDust(position, num84, height3, 226, 0f, 0f, 100, default, 1.5f);
                    Main.dust[num86].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                    Main.dust[num86].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                }
                for (int num87 = 0; num87 < 7; num87++)
                {
                    int num88 = Dust.NewDust(position, num84, height3, 226, 0, 0, 100, new Color(), 2f);
                    Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                    Main.dust[num88].position = NPC.Center + (Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f);
                    Main.dust[num88].noGravity = true;
                    Main.dust[num88].noLight = true;
                    Main.dust[num88].velocity *= 3f;
                    Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + (Main.rand.NextFloat() * 4f));
                    num88 = Dust.NewDust(position, num84, height3, 226, 0, 0, 100, new Color(), 2f);
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
                    int num90 = Dust.NewDust(position, num84, height3, 226, 0, 0, 100, new Color(), 2f);
                    Main.dust[num90].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                    Main.dust[num90].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                    Main.dust[num90].noGravity = true;
                    Main.dust[num90].noLight = true;
                    Main.dust[num90].velocity *= 3f;
                    Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
                }
                for (int num91 = 0; num91 < 15; num91++)
                {
                    int num92 = Dust.NewDust(position, num84, height3, 226, 0, 0, 100, new Color(), 2f);
                    Main.dust[num92].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                    Main.dust[num92].position = NPC.Center + (Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f);
                    Main.dust[num92].noGravity = true;
                    Main.dust[num92].velocity *= 3f;
                    Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
                }
            }
            else
            {
                for (int num242 = 0; num242 < 3; num242++)
                {
                    int num243 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 226, -2.5f * hitDirection, -2.5f, 0, default, 1f);
                    Main.dust[num243].scale = 0.5f;
                    Main.dust[num243].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.ai[0] == 2)
            {
                BaseDrawing.DrawAfterimage(sb, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.oldPos, NPC.scale, NPC.rotation, NPC.direction, 9, NPC.frame, 1f, 1f, 7, true, 0, 0, Color.White);
            }
            BaseDrawing.DrawTexture(sb, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 9, NPC.frame, NPC.GetAlpha(dColor), false); ;
            BaseDrawing.DrawTexture(sb, Mod.GetTexture("Glowmasks/Sag_Glow"), 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 9, NPC.frame, NPC.GetAlpha(ColorUtils.COLOR_GLOWPULSE), false);
            return false;
        }

        public override void OnKill()
        {
            AAWorld.downedSag = true;
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("SagTrophy").Type);
            }
            if (!Main.expertMode)
            {
                if (Main.rand.Next(7) == 0)
                {
                    NPC.DropLoot(Mod.Find<ModItem>("SagMask").Type);
                }
                string[] lootTable = { "SagCore", "NeutronStaff", "Legg" };
                int loot = Main.rand.Next(lootTable.Length);
                NPC.DropLoot(Mod.Find<ModItem>(lootTable[loot]).Type);
                Item.NewItem(NPC.Center, ModContent.ItemType<Items.Materials.Doomite>(), Main.rand.Next(20, 30));
            }
            else
            {
                NPC.DropBossBags();
            }
        }

        public void MoveToPoint(Vector2 point)
        {
            float moveSpeed = 9f;
            float velMultiplier = 1f;
            Vector2 dist = point - NPC.Center;
            float length = dist == Vector2.Zero ? 0f : dist.Length();
            if (length < moveSpeed)
            {
                velMultiplier = MathHelper.Lerp(0f, 1f, length / moveSpeed);
            }
            if (length < 200f)
            {
                moveSpeed *= (length / 200f);
            }
            NPC.velocity = length == 0f ? Vector2.Zero : Vector2.Normalize(dist);
            NPC.velocity *= moveSpeed;
            NPC.velocity *= velMultiplier;
        }

    }
}
