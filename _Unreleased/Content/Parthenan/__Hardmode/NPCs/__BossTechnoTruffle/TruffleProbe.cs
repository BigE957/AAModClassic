using AAModClassic._Unreleased.Content.Parthenan.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.IO;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Unreleased.Content.Parthenan.__Hardmode.NPCs.__BossTechnoTruffle
{
    public class TruffleProbe : ModNPC
    {
        public static Asset<Texture2D> Glowmask1;
        public static Asset<Texture2D> Glowmask2;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Truffle Probe");
            Main.npcFrameCount[NPC.type] = 4;

            Glowmask1 = ModContent.Request<Texture2D>(Texture + "_Glow1");
            Glowmask2 = ModContent.Request<Texture2D>(Texture + "_Glow2");
        }

        public override void SetDefaults()
        {
            NPC.width = 14;
            NPC.height = 14;
            NPC.value = 0;
            NPC.npcSlots = 0;
            NPC.aiStyle = -1;
            NPC.lifeMax = 250;
            NPC.defense = 0;
            NPC.damage = 20;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            SpawnModBiomes = [ModContent.GetInstance<ParthenanBiome>().Type];
        }

        public int body = -1;
        public float rotValue = -1f;
        private int despawnTimer;
        private int syncTimer;
        private float lastSyncedRot = -1f;

        public override void AI()
        {
            //setting life to 0 left the probe active with no health, and the truffle counts
            //every active probe when it decides whether it's invulnerable
            if (!NPC.AnyNPCs(ModContent.NPCType<TechnoTruffle>()))
            {
                BaseAI.KillNPCWithLoot(NPC);
                return;
            }

            NPC.noGravity = true;

            if (NPC.alpha > 0)
            {
                NPC.dontTakeDamage = true;
                NPC.chaseable = false;
            }
            else
            {
                NPC.dontTakeDamage = false;
                NPC.chaseable = true;
            }

            //failsafe, a probe nobody can reach would keep the truffle invulnerable forever
            if (++despawnTimer > 3600) { BaseAI.KillNPCWithLoot(NPC); return; }

            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, ModContent.NPCType<TechnoTruffle>(), 400f, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;
            NPC truffle = Main.npc[body];
            if (truffle == null || truffle.life <= 0 || !truffle.active || truffle.type != ModContent.NPCType<TechnoTruffle>()) { BaseAI.KillNPCWithLoot(NPC); return; }

            //the truffle's target can point at someone who died or left, which would fling the
            //probe off to a spot nobody can follow it to
            int target = truffle.target;
            if (!CanOrbit(target))
            {
                NPC.TargetClosest(false);
                target = NPC.target;
                if (!CanOrbit(target)) return;
            }
            Player player = Main.player[target];

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            int probeNumber = ((TechnoTruffle)truffle.ModNPC).ProbeCount;
            if (rotValue == -1f) rotValue = NPC.ai[0] % probeNumber * ((float)Math.PI * 2f / probeNumber);
            rotValue += 0.04f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;

            //probes teleport instead of moving, so clients can't extrapolate them, and every
            //client starts its own rotValue however late the spawn packet turned up. park the
            //server's angle in ai[1] so they can line back up
            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[1] = rotValue;
                if (++syncTimer >= 12)
                {
                    syncTimer = 0;
                    NPC.netUpdate = true;
                }
            }
            else if (NPC.ai[1] != lastSyncedRot)
            {
                lastSyncedRot = NPC.ai[1];
                rotValue = NPC.ai[1];
            }

            NPC.Center = BaseUtility.RotateVector(player.Center, player.Center + new Vector2(260, 0f), rotValue);
            NPC.netOffset = Vector2.Zero;
        }

        private static bool CanOrbit(int player) =>
            player >= 0 && player < Main.maxPlayers && Main.player[player].active && !Main.player[player].dead;

        public override void FindFrame(int frameHeight)
        {
            if (NPC.frameCounter++ > 8)
            {
                NPC.frameCounter = 0;
                NPC.frame.Y += frameHeight;
                if (NPC.frame.Y > frameHeight * 3)
                {
                    NPC.frame.Y = 0;
                }
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Glowmask1.Value;
            Texture2D glowTex1 = Glowmask2.Value;
            Color color = BaseUtility.MultiLerpColor(((int)(Main.GlobalTimeWrappedHourly * 60)) % 100 / 100f, drawColor, drawColor, Color.Violet, drawColor, Color.Violet, drawColor);

            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, color, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex1, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }
    }
}


