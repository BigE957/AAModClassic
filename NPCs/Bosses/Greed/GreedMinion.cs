
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Bosses.Greed
{
    public class GreedMinion : ModNPC
	{
        public int MinionType = 0;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Ore Construct");
			Main.npcFrameCount[NPC.type] = 15;
		}

		public override void SetDefaults()
		{
            NPC.lifeMax = 200;
            NPC.defense = 20;
            NPC.damage = 50;
            NPC.width = 60;
            NPC.height = 60;
            NPC.aiStyle = -1;
            NPC.HitSound = new LegacySoundStyle(21, 1);
            NPC.DeathSound = new LegacySoundStyle(2, 14);
            NPC.knockBackResist = 0.4f;
            NPC.noGravity = true;
        }

        public float[] internalAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if (Main.netMode == NetmodeID.Server || Main.dedServ)
            {
                writer.Write(internalAI[0]);
                writer.Write(internalAI[1]);
                writer.Write(internalAI[2]);
                writer.Write(internalAI[3]);
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
                internalAI[3] = reader.ReadFloat();
            }
        }
        int idleTimer = 0;
        public override void AI()
        {
            Player player = Main.player[NPC.target];

            MinionType = (int)NPC.ai[0];

            if (NPC.ai[2] == 0)
            {
                if (Main.netMode != 1)
                {
                    NPC.ai[2]++;
                }
                if (NPC.ai[2] > 30)
                {
                    NPC.ai[2] = 1;
                    NPC.netUpdate = true;
                }
            }
            else
            {
                if (MinionType <= 7 ||
                   MinionType == 9 ||
                   MinionType == 10 ||
                   MinionType == 11 ||
                   MinionType == 12 ||
                   MinionType == 13 ||
                   MinionType == 14 ||
                   MinionType == 15 ||
                   MinionType == 17 ||
                   MinionType == 18 ||
                   MinionType == 19 ||
                   MinionType == 24 ||
                   MinionType == 25) //If Earlygame ore, Crimtane, Incinerite, Abyssium, Hellstone, Cobalt, Palladium, Mythril, Adamantite, or Titanium, have Melee AI.
                {
                    float Speed = 3;
                    if (MinionType == 11)
                    {
                        Speed = 5;
                    }
                    if (MinionType == 24)
                    {
                        NPC.ai[3]++;
                        for (int m = NPC.oldPos.Length - 1; m > 0; m--)
                        {
                            NPC.oldPos[m] = NPC.oldPos[m - 1];
                        }
                        NPC.oldPos[0] = NPC.position;

                    }
                    if (MinionType == 24 && NPC.ai[3] > 300)
                    {
                        BaseAI.AITackle(NPC, ref NPC.ai, Main.player[NPC.target].Center, 0.5f, 12f, true, 60);

                        if (NPC.ai[3] > 420)
                        {
                            NPC.ai[3] = 0;
                        }
                    }
                    else
                    {
                        BaseAI.AIElemental(NPC, ref internalAI, ref idleTimer, null, 120, false, true, 400, 200f, 180, Speed);
                    }
                }
                else //if Demonite, Oricalcum, Chlorophite, or Technecium, Ranged AI
                {
                    NPC.noTileCollide = true;
                    BaseAI.AISkull(NPC, ref internalAI, false, 6, 350, 0.14f, .2f);

                    if (Main.netMode != 1)
                    {
                        int p;
                        if (MinionType == 8) //Demonite
                        {
                            p = ProjectileID.CursedFlameHostile;
                            ShootPeriodic(NPC, player.position, player.width, player.height, p, ref NPC.ai[1], 120, NPC.damage / 2, 9, true);
                        }
                        else if (MinionType == 20) //Chlorophyte
                        {
                            p = ProjectileID.CrystalLeafShot;
                            ShootPeriodic(NPC, player.position, player.width, player.height, p, ref NPC.ai[1], 120, NPC.damage / 2, 9, true);
                        }
                        else if (MinionType == 22) //Nebula
                        {
                            p = ModContent.ProjectileType<Nebula>();
                            ShootPeriodic(NPC, player.position, player.width, player.height, p, ref NPC.ai[1], 200, NPC.damage / 2, 9, true);
                        }
                        else if (MinionType == 23) //Vortex
                        {
                            p = 640;
                            ShootPeriodic(NPC, player.position, player.width, player.height, p, ref NPC.ai[1], 200, NPC.damage / 2, 9, true);
                        }
                        else if (MinionType == 16 && NPC.ai[1]++ > 180) //Oricalcum
                        {
                            NPC.ai[1] = 0;
                            int direction = NPC.direction;
                            float num = Main.screenPosition.X;
                            if (direction < 0)
                            {
                                num += Main.screenWidth;
                            }
                            float num2 = Main.screenPosition.Y;
                            num2 += Main.rand.Next(Main.screenHeight);
                            Vector2 vector = new Vector2(num, num2);
                            float num3 = NPC.position.X - vector.X;
                            float num4 = NPC.position.Y - vector.Y;
                            num3 += Main.rand.Next(-50, 51) * 0.1f;
                            num4 += Main.rand.Next(-50, 51) * 0.1f;
                            int num5 = 24;
                            float num6 = (float)Math.Sqrt(num3 * num3 + num4 * num4);
                            num6 = num5 / num6;
                            num3 *= num6;
                            num4 *= num6;
                            int l = Projectile.NewProjectile(num, num2, num3, num4, 221, 36, 0f, Main.myPlayer, 0f, 0f);
                            Main.projectile[l].friendly = false;
                            Main.projectile[l].hostile = true;
                            NPC.netUpdate = true;
                        }
                    }
                }
            }
            if (MinionType == 18) //Titanium Shadow Dodge
            {
                shadowDodge = shadowDodgeTimer > 0;
                if (shadowDodge)
                {
                    if (Main.netMode != 1)
                    {
                        if (!NPC.dontTakeDamage)
                        {
                            NPC.dontTakeDamage = true;
                            NPC.netUpdate = true;
                        }
                        shadowDodgeTimer--;
                    }

                    shadowDodgeCount += 1f;
                    if (shadowDodgeCount > 30f)
                    {
                        shadowDodgeCount = 30f;
                    }
                }
                else
                {
                    if (Main.netMode != 1)
                    {
                        if (NPC.dontTakeDamage)
                        {
                            NPC.dontTakeDamage = false;
                            NPC.netUpdate = true;
                        }
                    }
                    NPC.dontTakeDamage = false;
                    shadowDodgeCount -= 1f;
                    if (shadowDodgeCount < 0f)
                    {
                        shadowDodgeCount = 0f;
                    }
                }
            }
            if (MinionType == 9 || MinionType == 14) //Regen
            {
                NPC.ai[3]++;
                if (NPC.ai[3] >= 100)
                {
                    NPC.life++;
                    if (MinionType == 9)
                    {
                        NPC.life++;
                    }
                    NPC.ai[3] = 0;
                }
            }
            if (MinionType == 21) //Stardust Summon
            {
                NPC.ai[3]++;

                if (NPC.ai[3] > 400)
                {
                    NPC.ai[3] = 0;
                    int Xint = Main.rand.Next(-400, 400);
                    int Yint = Main.rand.Next(-400, 400);
                    NPC.NewNPC((int)NPC.Center.X + Xint, (int)NPC.Center.Y + Yint, ModContent.NPCType<GreedMinion>(), 0, Main.rand.Next(29));
                    for (int i = 0; i < 3; i++)
                    {
                        Dust.NewDust(new Vector2(Xint, Yint), 60, 60, 229, 0f, 0f, 0, Color.White, 1);
                    }
                }
            }
            if (MinionType == 23) //Vortex Stealth
            {
                NPC.ai[3]++;

                if (NPC.ai[3] == 300)
                {
                    NPC.netUpdate = true;
                }
                if (NPC.ai[3] > 300)
                {
                    NPC.alpha += 3;
                    if (NPC.alpha > 200)
                    {
                        NPC.alpha = 200;
                    }
                }
                if (NPC.ai[3] > 460)
                {
                    NPC.ai[3] = 0;
                }
            }
            NPC.rotation = 0;
        }

        bool shadowDodge = false;
        float shadowDodgeCount = 0;
        int shadowDodgeTimer = 0;

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (MinionType == 9 || MinionType == 13)
            {
                NPC.ai[3] = 0;
            }
        }

        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            if (MinionType == 23 && NPC.ai[3] > 300)
            {
                return false;
            }
            return true;
        }

        public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
        {
            if (MinionType == 12)
            {
                target.AddBuff(BuffID.OnFire, 200);
            }
            if (MinionType == 18 && shadowDodgeTimer <= 0)
            {
                shadowDodgeTimer = 180;
            }
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            if (MinionType == 10)
            {
                damage *= .9f;
            }
            return true;
        }

        Color bodyColor = Color.White;
        Color glowColor = Color.White;

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            if (NPC.frameCounter >= 10)
            {
                NPC.frame.Y += frameHeight;
                if (NPC.ai[2] == 0)
                {
                    if (NPC.frame.Y > (frameHeight * 3))
                    {
                        NPC.frame.Y = frameHeight * 3;
                    }
                }
                else
                {
                    if (NPC.frame.Y > (frameHeight * 14) || NPC.frame.Y < (frameHeight * 3))
                    {
                        NPC.frame.Y = frameHeight * 3;
                    }
                }
                NPC.frameCounter = 0;
            }
        }

        public void SetColor()
        {
            switch (MinionType)
            {
                case 0: bodyColor = new Color(150, 67, 22); glowColor = new Color(255, 229, 183); break; //Copper
                case 1: bodyColor = new Color(87, 92, 80); glowColor = new Color(187, 165, 124); break; //Tin
                case 2: bodyColor = new Color(87, 60, 60); glowColor = new Color(189, 159, 139); break; //Iron
                case 3: bodyColor = new Color(47, 62, 87); glowColor = new Color(104, 140, 150); break; //Lead
                case 4: bodyColor = new Color(61, 72, 73); glowColor = new Color(122, 140, 144); break; //Silver
                case 5: bodyColor = new Color(39, 70, 40); glowColor = new Color(154, 190, 155); break; //Tungsten
                case 6: bodyColor = new Color(148, 126, 24); glowColor = new Color(255, 249, 183); break; //Gold
                case 7: bodyColor = new Color(72, 73, 114); glowColor = new Color(190, 222, 222); break; //Platinum
                case 8: bodyColor = new Color(68, 69, 114); glowColor = new Color(120, 117, 179); break; //Demonite
                case 9: bodyColor = new Color(76, 76, 76); glowColor = new Color(216, 59, 63); break; //Crimtane
                case 10: bodyColor = new Color(62, 58, 48); glowColor = new Color(204, 108, 42); break; //Incinerite
                case 11: bodyColor = new Color(52, 36, 88); glowColor = new Color(18, 103, 92); break; //Abyssium
                case 12: bodyColor = new Color(102, 34, 34); glowColor = new Color(238, 102, 70); break; //Hellstone
                case 13: bodyColor = new Color(4, 48, 111); glowColor = new Color(143, 210, 253); break; //Cobalt
                case 14: bodyColor = new Color(160, 36, 11); glowColor = new Color(245, 95, 55); break; //Palladium
                case 15: bodyColor = new Color(22, 119, 125); glowColor = new Color(212, 255, 190); break; //Mythril
                case 16: bodyColor = new Color(151, 0, 127); glowColor = new Color(248, 113, 227); break; //Oricalcum
                case 17: bodyColor = new Color(128, 26, 52); glowColor = new Color(221, 85, 152); break; //Adamantite
                case 18: bodyColor = new Color(91, 90, 119); glowColor = new Color(190, 187, 220); break; //Titanium
                case 19: bodyColor = new Color(128, 26, 52); glowColor = new Color(221, 85, 152); break; //Hallowed
                case 20: bodyColor = new Color(36, 137, 0); glowColor = new Color(234, 254, 126); break; //Chlorophyte
                case 21: bodyColor = new Color(80, 87, 182); glowColor = new Color(255, 180, 0); break; //Stardust
                case 22: bodyColor = new Color(80, 87, 182); glowColor = new Color(153, 108, 227); break; //Nebula
                case 23: bodyColor = new Color(0, 127, 78); glowColor = new Color(0, 160, 170); break; //Vortex
                case 24: bodyColor = new Color(249, 79, 7); glowColor = new Color(255, 231, 66); break; //Solar
                case 25: bodyColor = new Color(73, 123, 119); glowColor = new Color(164, 101, 124); break; //Luminite
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SetColor();
            Texture2D glowTex = Mod.GetTexture("Glowmasks/GreedMinion_Glow");

            if (MinionType == 27 && NPC.ai[3] > 300)
            {
                BaseDrawing.DrawAfterimage(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, 1, 1, 8, true, 0, 0, new Color(bodyColor.R, bodyColor.G, bodyColor.B, 90), NPC.frame, 15);
            }

            if (shadowDodgeCount > 0f && MinionType == 19)
            {
                Vector2 position;
                position.X = NPC.position.X + shadowDodgeCount;
                position.Y = NPC.position.Y + NPC.gfxOffY;
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 15, NPC.frame, new Color(bodyColor.R, bodyColor.G, bodyColor.B, 30), true);
                position.X = NPC.position.X - shadowDodgeCount;
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 15, NPC.frame, new Color(bodyColor.R, bodyColor.G, bodyColor.B, 30), true);
            }
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 15, NPC.frame, bodyColor, true);

            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 15, NPC.frame, glowColor, true);
            return false;
        }

        public void SetStats()
        {
            switch (MinionType)
            {
                case 0:
                    NPC.defense = 6; NPC.damage = 8; break;
                case 1:
                    NPC.defense = 7; NPC.damage = 9; break;
                case 2:
                    NPC.defense = 9; NPC.damage = 10; break;
                case 3:
                    NPC.defense = 11; NPC.damage = 11; break;
                case 4:
                    NPC.defense = 13; NPC.damage = 11; break;
                case 5:
                    NPC.defense = 15; NPC.damage = 12; break;
                case 6:
                    NPC.defense = 16; NPC.damage = 13; break;
                case 7:
                    NPC.defense = 20; NPC.damage = 15; break;
                case 8:
                    NPC.defense = 19; NPC.damage = 17; break;
                case 9:
                    NPC.defense = 19; NPC.damage = 22; break;
                case 10:
                    NPC.defense = 21; NPC.damage = 26; break;
                case 11:
                    NPC.defense = 15; NPC.damage = 14; break;
                case 12:
                    NPC.defense = 25; NPC.damage = 36; break;
                case 13:
                    NPC.defense = 16; NPC.damage = 40; break;
                case 14:
                    NPC.defense = 32; NPC.damage = 36; break;
                case 15:
                    NPC.defense = 27; NPC.damage = 50; break;
                case 16:
                    NPC.defense = 42; NPC.damage = 47; break;
                case 17:
                    NPC.defense = 40; NPC.damage = 60; break;
                case 18:
                    NPC.defense = 49; NPC.damage = 52; break;
                case 19:
                    NPC.defense = 50; NPC.damage = 57; break;
                case 20:
                    NPC.defense = 56; NPC.damage = 75; break;
                case 21:
                    NPC.defense = 38; NPC.damage = 60; break;
                case 22:
                    NPC.defense = 46; NPC.damage = 130; break;
                case 23:
                    NPC.defense = 63; NPC.damage = 50; break;
                case 24:
                    NPC.defense = 78; NPC.damage = 105; break;
                default:
                    NPC.defense = 58; NPC.damage = 88; break;

            }
        }

        public static int ShootPeriodic(Entity codable, Vector2 position, int width, int height, int projType, ref float delayTimer, float delayTimerMax = 100f, int damage = -1, float speed = 10f, bool checkCanHit = true, Vector2 offset = default)
        {
            int pID = -1;
            if (damage == -1) { Projectile proj = new Projectile(); proj.SetDefaults(projType); damage = proj.damage; }
            bool properSide = codable is NPC ? Main.netMode != 1 : codable is Projectile ? ((Projectile)codable).owner == Main.myPlayer : true;
            if (properSide)
            {
                Vector2 targetCenter = position + new Vector2(width * 0.5f, height * 0.5f);
                delayTimer--;
                if (delayTimer <= 0)
                {
                    if (!checkCanHit || Collision.CanHit(codable.position, codable.width, codable.height, position, width, height))
                    {
                        Vector2 fireTarget = codable.Center + offset;
                        float rot = BaseUtility.RotationTo(codable.Center, targetCenter);
                        fireTarget = BaseUtility.RotateVector(codable.Center, fireTarget, rot);
                        pID = BaseAI.FireProjectile(targetCenter, fireTarget, projType, damage, 0f, speed, -1);
                        Main.projectile[pID].friendly = false;
                        Main.projectile[pID].hostile = true;

                        if (Main.projectile[pID].type == ModContent.ProjectileType<Nebula>())
                        {
                            Main.projectile[pID].ai[0] = Main.rand.Next(2);
                        }
                    }
                    delayTimer = delayTimerMax;
                    if (codable is NPC) { ((NPC)codable).netUpdate = true; }
                }
            }
            return pID;
        }
    }
}