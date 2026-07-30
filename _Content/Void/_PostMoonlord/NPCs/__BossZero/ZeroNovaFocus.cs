using AAModClassic._Content.Void.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.UI.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero
{
    [AutoloadBossHead]
    public class ZeroNovaFocus : ModNPC
    {
        public static Asset<Texture2D> Glowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Nova Focus");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            NPCID.Sets.BossBestiaryPriority.Add(Type);
        }

        public override void SetDefaults()
        {
            NPC.width = 62;
            NPC.height = 42;
            NPC.damage = 57;
            NPC.defense = 90;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCHit4;
            NPC.lifeMax = 30000;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.knockBackResist = 0.0f;
            NPC.buffImmune[20] = true;
            NPC.buffImmune[24] = true;
            NPC.buffImmune[39] = true;
            NPC.lavaImmune = true;
            NPC.netAlways = true;
            NPC.knockBackResist = 0;
            NPC.noGravity = true;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                new ColoredFlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.ZeroNovaFocus", AAColor.OblivionDialogue)
            ]);
        }

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.damage = (int)(NPC.damage * .7f);
            NPC.lifeMax = (int)(NPC.lifeMax * 0.6f * balance);
        }

        public override bool CheckActive()
        {
            if (NPC.AnyNPCs(ModContent.NPCType<Zero>()))
            {
                return false;
            }
            return true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool flag = NPC.life <= 0 || !NPC.active && NPC.AnyNPCs(ModContent.NPCType<Zero>());
            if (flag && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int ind = NPC.NewNPC(NPC.GetSource_Death(), (int)(NPC.position.X + (double)(NPC.width / 2)), (int)NPC.position.Y + NPC.height / 2, ModContent.NPCType<ZeroBrokenWeapon>(), NPC.whoAmI, NPC.ai[0], NPC.ai[1], NPC.ai[2], NPC.ai[3], NPC.target);
                Main.npc[ind].Center = NPC.Center;
                Main.npc[ind].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                Main.npc[ind].netUpdate2 = true; Main.npc[ind].netUpdate = true;
            }
        }

        public int body = -1;
        public float rotValue = -1f;
        public Vector2 pos;
        Projectile laser;

        public override void AI()
        {
            NPC.TargetClosest();
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, ModContent.NPCType<Zero>(), 1000, null);
                if (npcID >= 0) body = npcID;
            }

            if (body == -1) return;

            NPC zero = Main.npc[body];
            if (zero == null || zero.life <= 0 || !zero.active || zero.type != ModContent.NPCType<Zero>()) { NPC.active = false; return; }

            for (int m = NPC.oldPos.Length - 1; m > 0; m--)
            {
                NPC.oldPos[m] = NPC.oldPos[m - 1];
            }
            NPC.oldPos[0] = NPC.position;

            int probeNumber = ((Zero)zero.ModNPC).WeaponCount;
            if (rotValue == -1f) rotValue = NPC.ai[0] % probeNumber * ((float)Math.PI * 2f / probeNumber);
            rotValue += Main.expertMode ? .05f : 0f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;
            NPC.Center = BaseUtility.RotateVector(zero.Center, zero.Center + new Vector2(((Zero)zero.ModNPC).Distance, 0f), rotValue);
            NPC.netOffset = Vector2.Zero;

            if (Main.netMode != NetmodeID.MultiplayerClient) { NPC.ai[2]++; }

            NPC.TargetClosest();
            Player player = Main.player[zero.target];

            int aiTimerFire = Main.expertMode ? 230 : 280;

            float NewRotation = NPC.AngleTo(Main.player[NPC.target].Center);
            NPC.rotation = NPC.rotation.AngleLerp(NewRotation, 1f / 25f);

            if (NPC.ai[2] >= aiTimerFire)
            {
                NPC.ai[3]++;
                if (NPC.ai[3] >= 210)
                {
                    NPC.ai[2] = 0;
                    NPC.ai[3] = 0;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                        laser?.Kill();
                }
                else if (!AAGlobalProjectile.AnyProjectiles(ModContent.ProjectileType<ZeroNovaFocus_NovaRay>()) && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    laser = Main.projectile[Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, ModContent.ProjectileType<ZeroNovaFocus_NovaRay>(), 42, 3f, -1, NPC.whoAmI, 420)];
                    laser.velocity = BaseUtility.RotateVector(default, new Vector2(14f, 0f), laser.rotation);
                }
            }
            NPC.direction = 1;
            NPC.spriteDirection = 1;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D glowTex = Glowmask.Value;
            BaseDrawing.DrawAfterimage(spriteBatch, tex, 0, NPC, 1, 1, 6, true, 0, 0, Color.DarkRed, NPC.frame);
            spriteBatch.Draw(tex, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            spriteBatch.Draw(glowTex, NPC.Center - screenPos, NPC.frame, AAColor.COLOR_WHITEFADE1, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }
    }
}
