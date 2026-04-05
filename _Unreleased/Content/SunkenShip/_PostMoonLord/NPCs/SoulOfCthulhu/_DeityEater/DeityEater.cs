using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using static System.Net.Mime.MediaTypeNames;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEater
{
    [AutoloadBossHead]
    public class DeityEater : ModNPC
    {
        public override string Texture { get { return "AAModClassic/_Unreleased/NPCs/Bosses/SoC/Bosses/DeityEater"; } }
        
        public int fireTimer = 0;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crom Cruach");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
            Main.npcFrameCount[NPC.type] = 3;
        }

        public override void SetDefaults()
        {
            NPC.width = 38;
            NPC.height = 38;
            NPC.aiStyle = -1;
            NPC.netAlways = true;
            NPC.damage = 90;
            NPC.defense = 100;
            NPC.lifeMax = 150000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0f;
            NPC.behindTiles = true;
            NPC.scale = 1f;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[39] = true;
            Music = Mod.GetSoundSlot(SoundType.Music, "_Unreleased/_Unreleased/Sounds/Music/SoC");
            for (int m = 0; m < NPC.buffImmune.Length; m++) NPC.buffImmune[m] = true;
            NPC.alpha = 255;
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = NPC.lifeMax;
        }

        private bool fireAttack;
        public override bool PreAI()
        {
            Player player = Main.player[NPC.target];
            float dist = NPC.Distance(player.Center);
            fireTimer++;
            if (fireTimer >= 240 && fireAttack == false)
            {
                fireAttack = true;

                fireTimer = 0;
            }
            if (fireAttack == true)
            {
                SoundEngine.PlaySound(SoundID.Item34, NPC.position);
                //TODOSOC this proj has to be ported
                int proj2 = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X + Main.rand.Next(-20, 20), NPC.Center.Y + Main.rand.Next(-20, 20), NPC.velocity.X * 1.6f, NPC.velocity.Y * 1.6f, ModContent.ProjectileType<AFireProjHostile>(), 20, 0, Main.myPlayer);
                Main.projectile[proj2].damage = NPC.damage / 3;
                fireAttack = false;

            }
            if (NPC.alpha != 0)
            {
                for (int spawnDust = 0; spawnDust < 2; spawnDust++)
                {
                    int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 100, default, 2f);
                    Main.dust[num935].noGravity = true;
                    Main.dust[num935].noLight = true;
                }
            }
            NPC.alpha -= 12;
            if (NPC.alpha < 0)
            {
                NPC.alpha = 0;
            }
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 0)
                {
                    NPC.realLife = NPC.whoAmI;
                    int latestNPC = NPC.whoAmI;
                    int segment = 0;
                    int AkumaALength = 30;
                    for (int i = 0; i < AkumaALength; ++i)
                    {
                        latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeityEaterBody>(), NPC.whoAmI, 0, latestNPC);
                        Main.npc[latestNPC].realLife = NPC.whoAmI;
                        Main.npc[latestNPC].ai[3] = NPC.whoAmI;
                        segment += 1;
                    }

                    latestNPC = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<DeityEaterTail>(), NPC.whoAmI, 0, latestNPC);
                    Main.npc[latestNPC].realLife = NPC.whoAmI;
                    Main.npc[latestNPC].ai[3] = NPC.whoAmI;

                    NPC.ai[0] = 1;
                    NPC.netUpdate = true;
                }
            }

            int minTilePosX = (int)(NPC.position.X / 16.0) - 1;
            int maxTilePosX = (int)((NPC.position.X + NPC.width) / 16.0) + 2;
            int minTilePosY = (int)(NPC.position.Y / 16.0) - 1;
            int maxTilePosY = (int)((NPC.position.Y + NPC.height) / 16.0) + 2;
            if (minTilePosX < 0)
                minTilePosX = 0;
            if (maxTilePosX > Main.maxTilesX)
                maxTilePosX = Main.maxTilesX;
            if (minTilePosY < 0)
                minTilePosY = 0;
            if (maxTilePosY > Main.maxTilesY)
                maxTilePosY = Main.maxTilesY;

            bool collision = true;

            for (int i = minTilePosX; i < maxTilePosX; ++i)
            {
                for (int j = minTilePosY; j < maxTilePosY; ++j)
                {
                    if (Main.tile[i, j] != null && (Main.tile[i, j].HasUnactuatedTile && (Main.tileSolid[Main.tile[i, j].TileType] || Main.tileSolidTop[Main.tile[i, j].TileType] && Main.tile[i, j].TileFrameY == 0) || Main.tile[i, j].LiquidAmount > 64))
                    {
                        Vector2 vector2;
                        vector2.X = i * 16;
                        vector2.Y = j * 16;
                        if (NPC.position.X + NPC.width > vector2.X && NPC.position.X < vector2.X + 16.0 && NPC.position.Y + NPC.height > (double)vector2.Y && NPC.position.Y < vector2.Y + 16.0)
                        {
                            collision = true;
                            if (Main.rand.Next(100) == 0 && Main.tile[i, j].HasUnactuatedTile)
                                WorldGen.KillTile(i, j, true, true, false);
                        }
                    }
                }
            }
            float speedval = 0f;
            if (NPC.life > NPC.lifeMax / 3 && NPC.type == ModContent.NPCType<DeityEater>())
            {
                speedval = 9f;
            }
            if (NPC.life <= NPC.lifeMax / 3 && NPC.type == ModContent.NPCType<DeityEater>())
            {
                speedval = 11f;
            }
            float speed = speedval;
            float acceleration = 0.18f;

            Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float targetXPos = Main.player[NPC.target].position.X + Main.player[NPC.target].width / 2;
            float targetYPos = Main.player[NPC.target].position.Y + Main.player[NPC.target].height / 2;

            float targetRoundedPosX = (int)(targetXPos / 16.0) * 16;
            float targetRoundedPosY = (int)(targetYPos / 16.0) * 16;
            npcCenter.X = (int)(npcCenter.X / 16.0) * 16;
            npcCenter.Y = (int)(npcCenter.Y / 16.0) * 16;
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;
            NPC.TargetClosest(true);
            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);

            float absDirX = Math.Abs(dirX);
            float absDirY = Math.Abs(dirY);
            float newSpeed = speed / length;
            dirX = dirX * (newSpeed * 2);
            dirY = dirY * (newSpeed * 2);
            if (NPC.velocity.X > 0.0 && dirX > 0.0 || NPC.velocity.X < 0.0 && dirX < 0.0 || NPC.velocity.Y > 0.0 && dirY > 0.0 || NPC.velocity.Y < 0.0 && dirY < 0.0)
            {
                if (NPC.velocity.X < dirX)
                    NPC.velocity.X = NPC.velocity.X + acceleration;
                else if (NPC.velocity.X > dirX)
                    NPC.velocity.X = NPC.velocity.X - acceleration;
                if (NPC.velocity.Y < dirY)
                    NPC.velocity.Y = NPC.velocity.Y + acceleration;
                else if (NPC.velocity.Y > dirY)
                    NPC.velocity.Y = NPC.velocity.Y - acceleration;
                if (Math.Abs(dirY) < speed * 0.2 && (NPC.velocity.X > 0.0 && dirX < 0.0 || NPC.velocity.X < 0.0 && dirX > 0.0))
                {
                    if (NPC.velocity.Y > 0.0)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration * 2f;
                    else
                        NPC.velocity.Y = NPC.velocity.Y - acceleration * 2f;
                }
                if (Math.Abs(dirX) < speed * 0.2 && (NPC.velocity.Y > 0.0 && dirY < 0.0 || NPC.velocity.Y < 0.0 && dirY > 0.0))
                {
                    if (NPC.velocity.X > 0.0)
                        NPC.velocity.X = NPC.velocity.X + acceleration * 2f;
                    else
                        NPC.velocity.X = NPC.velocity.X - acceleration * 2f;
                }
            }
            else if (absDirX > absDirY)
            {
                if (NPC.velocity.X < dirX)
                    NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
                else if (NPC.velocity.X > dirX)
                    NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;

                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                {
                    if (NPC.velocity.Y > 0.0)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration;
                    else
                        NPC.velocity.Y = NPC.velocity.Y - acceleration;
                }
            }
            else
            {
                if (NPC.velocity.Y < dirY)
                    NPC.velocity.Y = NPC.velocity.Y + acceleration * 1.1f;
                else if (NPC.velocity.Y > dirY)
                    NPC.velocity.Y = NPC.velocity.Y - acceleration * 1.1f;

                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                {
                    if (NPC.velocity.X > 0.0)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                    else
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                }
            }

            if (Main.player[NPC.target].dead || Math.Abs(NPC.position.X - Main.player[NPC.target].position.X) > 6000f || Math.Abs(NPC.position.Y - Main.player[NPC.target].position.Y) > 6000f)
            {
                NPC.velocity.Y = NPC.velocity.Y + 1f;
                if (NPC.position.Y > Main.rockLayer * 16.0)
                {
                    NPC.velocity.Y = NPC.velocity.Y + 1f;
                    speed = 30f;
                }
                if (NPC.position.Y > Main.rockLayer * 16.0)
                {
                    for (int num957 = 0; num957 < 200; num957++)
                    {
                        if (Main.npc[num957].aiStyle == NPC.aiStyle)
                        {
                            Main.npc[num957].active = false;
                        }
                    }
                }
            }

            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;

            if (collision)
            {
                if (NPC.localAI[0] != 1)
                    NPC.netUpdate = true;
                NPC.localAI[0] = 1f;
            }
            if ((NPC.velocity.X > 0.0 && NPC.oldVelocity.X < 0.0 || NPC.velocity.X < 0.0 && NPC.oldVelocity.X > 0.0 || NPC.velocity.Y > 0.0 && NPC.oldVelocity.Y < 0.0 || NPC.velocity.Y < 0.0 && NPC.oldVelocity.Y > 0.0) && !NPC.justHit)
                NPC.netUpdate = true;

            return false;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life > 0)
            {
                SoulOfCthulhu.ComeBack = true;
                int num121 = 0;
                while (num121 < hit.Damage / (double)NPC.lifeMax * 3.0)
                {
                    if (Main.rand.Next(3) == 0)
                    {
                        Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), hit.HitDirection, -1f, 0, Color.Transparent, 0.75f);
                    }
                    if (Main.rand.Next(2) == 0)
                    {
                        Dust dust39 = Main.dust[Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 0, default, 1f)];
                        dust39.noGravity = true;
                    }
                    for (int num122 = 0; num122 < NPC.oldPos.Length; num122++)
                    {
                        if (Main.rand.Next(4) == 0)
                        {
                            if (NPC.oldPos[num122] == Vector2.Zero)
                            {
                                break;
                            }
                            if (Main.rand.Next(3) == 0)
                            {
                                Dust.NewDust(NPC.oldPos[num122], NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), hit.HitDirection, -1f, 0, Color.Transparent, 0.75f);
                            }
                            if (Main.rand.Next(2) == 0)
                            {
                                Dust dust40 = Main.dust[Dust.NewDust(NPC.oldPos[num122], NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 0, default, 1f)];
                                dust40.noGravity = true;
                            }
                        }
                    }
                    num121++;
                }
            }
        }
        
        public override void ModifyHitByProjectile(Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            Player player = Main.player[NPC.target];
            if (player.vortexStealthActive && projectile.CountsAsClass(DamageClass.Ranged))
            {
                modifiers.TargetDamageMultiplier /= 2f;
                modifiers.DisableCrit();
            }
            if (projectile.penetrate == -1 && !projectile.minion)
            {
                projectile.damage *= (int).2;
            }
            else if (projectile.penetrate >= 1)
            {
                projectile.damage *= (int).2;
            }

            //TODOSOC
            /*
            if (AAWorld_Unreleased.Anticheat == true)
            {
                if (damage > NPC.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    modifiers.TargetDamageMultiplier *= 0;
                }
            }
            */
        }

        /*
        public override void ModifyHitByItem(Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (AAWorld_Unreleased.Anticheat == true)
            {
                if (damage > NPC.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    modifiers.TargetDamageMultiplier *= 0;
                }
            }
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (AAWorld_Unreleased.Anticheat == true)
            {
                if (damage > NPC.lifeMax / 8)
                {
                    Main.NewText("YOU CANNOT CHEAT DEATH", Color.DarkCyan);
                    modifiers.TargetDamageMultiplier *= 0;
                }
            }
        }
        */

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D currentTex = TextureAssets.Npc[NPC.type].Value;
            Texture2D GlowTex = Mod.GetTexture("_Unreleased/Glowmasks/DeityEater_Glow");

            BaseDrawing.DrawTexture(spriteBatch, currentTex, 0, NPC, drawColor);

            //draw glow/glow afterimage
            BaseDrawing.DrawTexture(spriteBatch, GlowTex, 0, NPC, AAColor.Cthulhu2);
            BaseDrawing.DrawAfterimage(spriteBatch, GlowTex, 0, NPC, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);

            return false;
        }
    }
    
    public class DeityEaterBody : DeityEater
    {
        public override string Texture { get { return "AAModClassic/_Unreleased/NPCs/Bosses/SoC/Bosses/DeityEaterBody"; } }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crom Cruach");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.dontCountMe = true;
        }
        
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreAI()
        {
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;

            if (Main.npc[(int)NPC.ai[1]].alpha < 128)
            {
                if (NPC.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 100, default, 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                NPC.alpha -= 10;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                }
            }
            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - npcCenter.Y;

                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D currentTex = TextureAssets.Npc[NPC.type].Value;
            Texture2D GlowTex = Mod.GetTexture("_Unreleased/Glowmasks/DeityEaterBody_Glow");

            BaseDrawing.DrawTexture(spriteBatch, currentTex, 0, NPC, drawColor);
            
            BaseDrawing.DrawTexture(spriteBatch, GlowTex, 0, NPC, AAColor.Cthulhu2);
            BaseDrawing.DrawAfterimage(spriteBatch, GlowTex, 0, NPC, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);

            return false;
        }
    }

    public class DeityEaterTail : DeityEater
    {
        public override string Texture { get { return "AAModClassic/_Unreleased/NPCs/Bosses/SoC/Bosses/DeityEaterTail"; } }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Crom Cruach");
        }

        public override void SetDefaults()
        {
            base.SetDefaults();
            NPC.dontCountMe = true;
        }
        
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            return false;
        }

        public override bool PreAI()
        {
            if (NPC.ai[3] > 0)
                NPC.realLife = (int)NPC.ai[3];
            if (NPC.target < 0 || NPC.target == byte.MaxValue || Main.player[NPC.target].dead)
                NPC.TargetClosest(true);
            if (Main.player[NPC.target].dead && NPC.timeLeft > 300)
                NPC.timeLeft = 300;

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (!Main.npc[(int)NPC.ai[1]].active)
                {
                    NPC.life = 0;
                    NPC.HitEffect(0, 10.0);
                    NPC.active = false;
                }
            }

            if (Main.npc[(int)NPC.ai[1]].alpha < 128)
            {
                if (NPC.alpha != 0)
                {
                    for (int num934 = 0; num934 < 2; num934++)
                    {
                        int num935 = Dust.NewDust(new Vector2(NPC.position.X, NPC.position.Y), NPC.width, NPC.height, ModContent.DustType<CthulhuDust>(), 0f, 0f, 100, default, 2f);
                        Main.dust[num935].noGravity = false;
                        Main.dust[num935].noLight = false;
                    }
                }
                NPC.alpha -= 10;
                if (NPC.alpha < 0)
                {
                    NPC.alpha = 0;
                }
            }


            if (NPC.ai[1] < (double)Main.npc.Length)
            {
                Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
                float dirX = Main.npc[(int)NPC.ai[1]].position.X + Main.npc[(int)NPC.ai[1]].width / 2 - npcCenter.X;
                float dirY = Main.npc[(int)NPC.ai[1]].position.Y + Main.npc[(int)NPC.ai[1]].height / 2 - npcCenter.Y;
                NPC.rotation = (float)Math.Atan2(dirY, dirX) + 1.57f;
                float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
                float dist = (length - NPC.width) / length;
                float posX = dirX * dist;
                float posY = dirY * dist;
                NPC.position.X = NPC.position.X + posX;
                NPC.position.Y = NPC.position.Y + posY;
            }
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D currentTex = TextureAssets.Npc[NPC.type].Value;
            Texture2D GlowTex = Mod.GetTexture("_Unreleased/Glowmasks/DeityEaterTail_Glow");

            BaseDrawing.DrawTexture(spriteBatch, currentTex, 0, NPC, drawColor);

            //draw glow/glow afterimage
            BaseDrawing.DrawTexture(spriteBatch, GlowTex, 0, NPC, AAColor.Cthulhu2);
            BaseDrawing.DrawAfterimage(spriteBatch, GlowTex, 0, NPC, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);

            return false;
        }
    }
}