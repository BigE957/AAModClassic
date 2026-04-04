using System;
using System.IO;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Bosses.Zero
{
    [AutoloadBossHead]
    public class VoidStar : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Void Star");
            NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }
        public override void SetDefaults()
        {
            NPC.width = 40;
            NPC.height = 54;
            NPC.damage = 59;
            NPC.defense = 40;
            NPC.lifeMax = 30000;
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCHit4;
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

        public override void SendExtraAI(BinaryWriter writer)
        {
            writer.Write((short)NPC.localAI[0]);
        }

        public override void ReceiveExtraAI(BinaryReader reader)
        {
            NPC.localAI[0] = reader.ReadInt16();
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool flag = NPC.life <= 0 || (!NPC.active && NPC.AnyNPCs(ModContent.NPCType<Zero>()));
            if (flag && Main.netMode != NetmodeID.MultiplayerClient)
            {
                int ind = NPC.NewNPC(NPC.GetSource_Death(), (int)(NPC.position.X + (double)(NPC.width / 2)), (int)NPC.position.Y + (NPC.height / 2), ModContent.NPCType<TeslaHand>(), NPC.whoAmI, NPC.ai[0], NPC.ai[1], NPC.ai[2], NPC.ai[3], NPC.target);
                Main.npc[ind].Center = NPC.Center;
                Main.npc[ind].velocity = new Vector2(MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()), MathHelper.Lerp(-1f, 1f, (float)Main.rand.NextDouble()));
                Main.npc[ind].velocity *= 8f;
                Main.npc[ind].netUpdate2 = true; Main.npc[ind].netUpdate = true;
            }
        }

        public int body = -1;
        public float rotValue = -1f;
        public Vector2 pos;

        public override void AI()
        {
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

            int aiTimerFire = 600;

            if (Main.netMode != NetmodeID.MultiplayerClient) { NPC.ai[2]++; }

            Player player = Main.player[zero.target];

            if (NPC.ai[2] == aiTimerFire)
            {
                NPC.ai[2] = 0;
                if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.Center, player.width, player.height))
                {
                    Vector2 fireTarget = NPC.Center;
                    float rot = BaseUtility.RotationTo(NPC.Center, player.Center);
                    fireTarget = BaseUtility.RotateVector(NPC.Center, fireTarget, rot);
                    BaseAI.FireProjectile(player.Center, fireTarget, Mod.ProjType("VoidStarP"), NPC.damage / 2, 0f, 4f);
                }
            }

            Vector2 vector2 = new Vector2(NPC.position.X + (NPC.width * 0.5f), NPC.position.Y + (NPC.height * 0.5f));
            float num1 = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2) - vector2.X;
            float num2 = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2) - vector2.Y;
            float NewRotation = (float)Math.Atan2(num2, num1) - 1.57f;
            NPC.rotation = MathHelper.Lerp(NPC.rotation, NewRotation, 1f / 30f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D glowTex = Mod.GetTexture("Glowmasks/VoidStarZ");
            BaseDrawing.DrawAfterimage(spriteBatch, tex, 0, NPC, 1, 1, 6, true, 0, 0, Color.DarkRed, NPC.frame);
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, NPC, drawColor);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC, AAColor.COLOR_WHITEFADE1);
            return false;
        }

        public override void BossHeadRotation(ref float rotation)
        {
            rotation = NPC.rotation;
        }

    }
}
