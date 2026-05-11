using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu.BossStandard;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityBrain;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEater;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityEye;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityLeviathan;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeityRose;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._DeitySkull;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Music;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu
{
    public class Cthulhu : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Cthulhu, Cosmic Calamity");

        }
        public override void SetDefaults()
        {
            NPC.width = 222;
            NPC.height = 228;
            NPC.alpha = 255;
            NPC.damage = 0;
            Music = MusicManagementSystem.MusicSlots["Cthulhu"];
            NPC.lifeMax = 1500000;
            NPC.dontTakeDamage = false;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.boss = true;
            NPC.chaseable = false;
            NPC.scale *= 1.2f;
            //bossBag/* tModPorter Note: Removed. Spawn the treasure bag alongside other loot via npcLoot.Add(ItemDropRule.BossBag(type)) */ = Mod.Find<ModItem>("SoCCache").Type;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            NPC.knockBackResist = 0f;
        }


        public float[] shootAI = new float[4];

        public float[] customAI = new float[4];
        public override void SendExtraAI(BinaryWriter writer)
        {
            base.SendExtraAI(writer);
            if ((Main.netMode == NetmodeID.Server || Main.dedServ))
            {
                writer.Write((short)customAI[0]);
                writer.Write((short)customAI[1]);
                writer.Write((short)customAI[2]);
                writer.Write((short)customAI[3]);
            }
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            base.ReceiveExtraAI(reader);
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                customAI[0] = reader.ReadSingle();
                customAI[1] = reader.ReadSingle();
                customAI[2] = reader.ReadSingle();
                customAI[3] = reader.ReadSingle();
            }
        }

        public int BoomTimer = 0;
        public int Speechtimer = 0;

        public float ShieldScale = 0;
        public float ShieldRotation = 0;

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            float EyeSummon = NPC.lifeMax * .85f;
            float BrainSummon = NPC.lifeMax * .70f;
            float EaterSummon = NPC.lifeMax * .55f;
            float SkullSummon = NPC.lifeMax * .40f;
            float RoseSummon = NPC.lifeMax * .25f;
            float LeviathanSummon = NPC.lifeMax * .15f;

            bool BossAlive = NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) || NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) || NPC.AnyNPCs(ModContent.NPCType<DeityBrain>()) || NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) || NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(ModContent.NPCType<DeityRose>());

            Vector2 Explosion = new Vector2(Main.rand.Next((int)NPC.position.X + NPC.width), Main.rand.Next((int)NPC.position.Y - NPC.height));

            ShieldRotation += .05f;

            if (BossAlive)
            {
                NPC.dontTakeDamage = true;
                if (ShieldScale < 1f)
                {
                    ShieldScale += .05f;
                }
                if (ShieldScale >= 1f)
                {
                    ShieldScale = 1f;
                }
            }
            else
            {
                if (ShieldScale > 0)
                {
                    ShieldScale -= .05f;
                }
                if (ShieldScale <= 0)
                {
                    NPC.dontTakeDamage = false;
                    ShieldScale = 0;
                }
            }

            if (NPC.ai[1] == 1f)
            {
                NPC.dontTakeDamage = true;
                BoomTimer++;
                NPC.ai[3]++;
                if (BoomTimer == 60)
                {
                    Projectile.NewProjectile(NPC.GetSource_Death(), Explosion, new Vector2(0, 0), ModContent.ProjectileType<CthulhuDeathBoom>(), 0, 0, Main.myPlayer);
                    BoomTimer = 0;
                }
                if (NPC.ai[3] == 40)
                {
                    Main.NewText("AAAAAAAAAGGHHH….!!", Color.DarkCyan);
                }

                if (NPC.ai[3] == 100)
                {
                    Main.NewText("NO!", Color.DarkCyan);
                }

                if (NPC.ai[3] == 160)
                {
                    Main.NewText("NO!!!", Color.DarkCyan);
                }

                if (NPC.ai[3] == 220)
                {
                    Main.NewText("IMPOSSIBLE", Color.DarkCyan);
                }

                if (NPC.ai[3] == 280)
                {
                    Main.NewText("BY THE NAME OF AZATHOTH!", Color.DarkCyan);
                }

                if (NPC.ai[3] == 340)
                {
                    Main.NewText("I CURSE YOU AND YOUR WORLD!", Color.DarkCyan);
                }

                if (NPC.ai[3] == 400)
                {
                    Main.NewText("MAY YOU AND YOUR KIND ROT IN THE DEPTHS OF THE OCEAN!", Color.DarkCyan);
                }

                if (NPC.ai[3] == 460)
                {
                    Main.NewText("GRAAAAAAAAAAAHHH…!", Color.DarkCyan);
                }

                if (NPC.ai[3] == 520)
                {
                    Projectile.NewProjectile(NPC.GetSource_Death(), NPC.Center, new Vector2(0, 0), ModContent.ProjectileType<CthulhuDeath>(), 0, 0, Main.myPlayer);
                    Gore.NewGore(NPC.GetSource_Death(), NPC.Center, NPC.velocity, Mod.Find<ModGore>("CthulhuGore").Type, 1.2f);
                    NPC.dontTakeDamage = false;
                    NPC.life = 0;
                }
                return;
            }

            BaseAI.AISpaceOctopus(NPC, ref customAI, .1f, 1, 0f, 120f, FireMagic);

            if (NPC.life < EyeSummon && NPC.ai[2] == 0)
            {
                NPC.ai[2] = 1;
                Main.NewText("Cyaegha! Scorch this insignificant mortal with your flames of agony!", Color.DarkCyan);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DeityEye").Type);
                }
            }

            if (NPC.life < EaterSummon && NPC.ai[2] == 1)
            {
                NPC.ai[2] = 2;
                Main.NewText("Cyaegha! Scorch this insignificant mortal with your flames of agony!", Color.DarkCyan);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DeityEater").Type);
                }
            }

            if (NPC.life < BrainSummon && NPC.ai[2] == 2)
            {
                NPC.ai[2] = 3;
                Main.NewText("Lu'Kthu! Send your minions upon this pest to distract them!", Color.DarkCyan);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DeityBrain").Type);
                }
            }

            if (NPC.life < SkullSummon && NPC.ai[2] == 3)
            {
                NPC.ai[2] = 4;
                Main.NewText("Rhan-Tegoth! Crush this annoyance with your claws of pain!", Color.DarkCyan);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DeitySkull").Type);
                }
            }

            if (NPC.life < RoseSummon && NPC.ai[2] == 4)
            {
                NPC.ai[2] = 5;
                Main.NewText("Ei'Lor! Devour my foe for me so I may destroy this world!", Color.DarkCyan);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DeityRose").Type);
                }
            }

            if (NPC.life < LeviathanSummon && NPC.ai[2] == 5)
            {
                NPC.ai[2] = 6;
                Main.NewText("THAT TEARS IT! VILE-OCT! DESTROY THIS INSIGNIFICANT PEST!", Color.DarkCyan);
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    NPC.SpawnOnPlayer(player.whoAmI, Mod.Find<ModNPC>("DeityLeviathan").Type);
                }
            }

            if (NPC.life <= NPC.lifeMax / 10)
            {

                Music = MusicManagementSystem.MusicSlots["Superancients_Pinch"];
                if (NPC.ai[2] == 6)
                {
                    NPC.ai[2] = 7;
                    Main.NewText("I SHALL NOT LOSE TO A MORTAL AGAIN. YOU WILL FEEL MY WRATH UPON YOUR WORLD!", Color.DarkCyan);
                }
            }
        }

        public override void OnKill()
        {
            if (Main.expertMode)
            {
                NPC.DropLoot(ModContent.ItemType<SoulOfCthulhuTreasureBag>());
                AAWorld_Unreleased.downedSoC = true;
            }
            else
            {
                Main.NewText("Cheaters do not deserve the spoils of battle", Color.DarkCyan);
            }
        }

        int ShootThis;
        int Loop;

        public void FireMagic(NPC npc, Vector2 velocity)
        {
            Player player = Main.player[npc.target];
            int num429 = 1;
            if (npc.position.X + (npc.width / 2) < Main.player[npc.target].position.X + Main.player[npc.target].width)
            {
                num429 = -1;
            }
            Vector2 PlayerDistance = new Vector2(npc.position.X + npc.width * 0.5f, npc.position.Y + npc.height * 0.5f);
            float PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) + (num429 * 180) - PlayerDistance.X;
            float PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            float PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
            float num433 = 6f;
            PlayerPosX = Main.player[npc.target].position.X + (Main.player[npc.target].width / 2) - PlayerDistance.X;
            PlayerPosY = Main.player[npc.target].position.Y + (Main.player[npc.target].height / 2) - PlayerDistance.Y;
            PlayerPos = (float)Math.Sqrt((PlayerPosX * PlayerPosX + PlayerPosY * PlayerPosY));
            PlayerPos = num433 / PlayerPos;
            PlayerPosX *= PlayerPos;
            PlayerPosY *= PlayerPos;
            PlayerPosY += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosX += Main.rand.Next(-40, 41) * 0.01f;
            PlayerPosY += npc.velocity.Y * 0.5f;
            PlayerPosX += npc.velocity.X * 0.5f;
            PlayerDistance.X -= PlayerPosX * 1f;
            PlayerDistance.Y -= PlayerPosY * 1f;
            Vector2 spawnAt = npc.Center + new Vector2(0f, npc.height / 2f);
            npc.ai[0] += 1;
            if (npc.ai[0] == 1 || npc.ai[0] == 4 || npc.ai[0] == 6 || npc.ai[0] == 15 || npc.ai[0] == 20)
            {
                ShootThis = ModContent.ProjectileType<CthulhuNuke>();
            }
            if (npc.ai[0] == 2 || npc.ai[0] == 3 || npc.ai[0] == 9 || npc.ai[0] == 17 || npc.ai[0] == 22)
            {
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)spawnAt.X, (int)spawnAt.Y, Mod.Find<ModNPC>("Portal").Type, 0, -npc.velocity.X * 1.2f, -npc.velocity.Y * 1.2f);
                return;
            }
            if (npc.ai[0] == 5 || npc.ai[0] == 12 || npc.ai[0] == 16 || npc.ai[0] == 21 || npc.ai[0] == 25)
            {
                ShootThis = ModContent.ProjectileType<CthulhuShot>();
            }
            if (npc.ai[0] == 7 || npc.ai[0] == 11 || npc.ai[0] == 14 || npc.ai[0] == 18 || npc.ai[0] == 24)
            {
                ShootThis = ModContent.ProjectileType<Watcher>();
                Loop = 5;
            }
            if (npc.ai[0] == 8 || npc.ai[0] == 10 || npc.ai[0] == 13 || npc.ai[0] == 19 || npc.ai[0] == 23)
            {
                ShootThis = ModContent.ProjectileType<Watcher>();
                Loop = 9;
            }
            if (ShootThis == ModContent.ProjectileType<Watcher>())
            {
                float spread = 45f * 0.0174f;
                float baseSpeed = (float)Math.Sqrt((PlayerPosX * PlayerPosX) + (PlayerPosY * PlayerPosY));
                double startAngle = Math.Atan2(PlayerPosX, PlayerPosY) - .1d;
                double deltaAngle = spread / 6f;
                double offsetAngle;
                for (int i = 0; i < Loop; i++)
                {
                    offsetAngle = startAngle + (deltaAngle * i);
                    Projectile.NewProjectile(NPC.GetSource_FromThis(), npc.Center.X, npc.Center.Y, baseSpeed * (float)Math.Sin(offsetAngle), baseSpeed * (float)Math.Cos(offsetAngle), ShootThis, (int)(npc.damage * .5f), 0f, Main.myPlayer);
                }
            }
            else
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), PlayerDistance.X, PlayerDistance.Y, PlayerPosX * 2, PlayerPosY * 2, ShootThis, (int)(npc.damage * .8f), 0f, Main.myPlayer);
            }
            if (npc.ai[0] > 25)
            {
                npc.ai[0] = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && NPC.ai[1] != 1 && Main.expertMode)
            {
                NPC.ai[1] = 1f;
                NPC.life = NPC.lifeMax;
                NPC.netUpdate = true;
                NPC.dontTakeDamage = true;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            bool BossAlive = NPC.AnyNPCs(ModContent.NPCType<DeityEye>()) || NPC.AnyNPCs(ModContent.NPCType<DeityEater>()) || NPC.AnyNPCs(ModContent.NPCType<DeityBrain>()) || NPC.AnyNPCs(ModContent.NPCType<DeitySkull>()) || NPC.AnyNPCs(ModContent.NPCType<DeityLeviathan>()) || NPC.AnyNPCs(ModContent.NPCType<DeityRose>());
            Texture2D currentTex = TextureAssets.Npc[NPC.type].Value;
            Texture2D GlowTex = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D Barrier = ModContent.Request<Texture2D>(Texture + "Barrier").Value;
            Texture2D Shield = ModContent.Request<Texture2D>(Texture + "Shield").Value;
            Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);

            Main.spriteBatch.Draw(currentTex, (drawCenter - Main.screenPosition), new Rectangle?(new Rectangle(0, 0, currentTex.Width, currentTex.Height)), drawColor, NPC.rotation, new Vector2(currentTex.Width / 2f, currentTex.Height / 2f), NPC.scale, SpriteEffects.None, 0f);

            //draw glow/glow afterimage
            BaseDrawing.DrawTexture(Main.spriteBatch, GlowTex, 0, NPC, AAColor.Cthulhu2);
            BaseDrawing.DrawAfterimage(Main.spriteBatch, GlowTex, 0, NPC, 0.8f, 1f, 6, false, 0f, 0f, AAColor.Cthulhu2);

            //Draw Shield
            int shader = GameShaders.Armor.GetShaderIdFromItemId(ItemID.LivingOceanDye);

            if (BossAlive)
            {
                BaseDrawing.DrawTexture(Main.spriteBatch, Shield, shader, NPC.position, NPC.width, NPC.height, ShieldScale, ShieldRotation, 0, 1, new Rectangle(0, 0, Shield.Width, Shield.Height), AAColor.Cthulhu, true);
                BaseDrawing.DrawTexture(Main.spriteBatch, Barrier, 0, NPC.position, NPC.width, NPC.height, ShieldScale, ShieldRotation, 0, 1, new Rectangle(0, 0, Barrier.Width, Barrier.Height), Color.White, true);
            }
            return false;
        }
    }
}