using AAModClassic._Content.Chaos._PostMoonlord.Items._BossSistersOfDiscord.Weapons;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Void._PostMoonlord.NPCs.__BossZero.Awakened
{
    public class ZeroEcho : ModNPC
    {
        public override string Texture => ModContent.GetInstance<ZeroA>().Texture;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("3CH0");
            Main.npcFrameCount[NPC.type] = 7; 
        }
        public override void SetDefaults()
        {
            NPC.lifeMax = 1000;
            NPC.damage = 110;
            NPC.defense = 70;
            NPC.knockBackResist = 0f;
            NPC.width = 146;
            NPC.height = 152;
            NPC.friendly = false;
            NPC.aiStyle = -1;
            NPC.value = Item.sellPrice(0, 0, 0, 0);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = new SoundStyle("AAModClassic/Sounds/Zerohit");
            NPC.DeathSound = new SoundStyle("AAModClassic/Sounds/ZeroDeath");
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override Color? GetAlpha(Color drawColor)
        {
            return AAColor.Oblivion;
        }

        int body = -1;

        public override void AI()
        {
            if(NPC.ai[0] ++ == 5)
            {
                SpawnDust();
            }

            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(NPC.Center, ModContent.NPCType<ZeroA>(), -1, null);
                if (npcID >= 0) body = npcID;
            }

            if (body == -1) return;

            NPC zero = Main.npc[body];
            if (zero == null || zero.life <= 0 || !zero.active || zero.type != ModContent.NPCType<ZeroA>()) { NPC.active = false; return; }

            if (zero.ai[1] == 1f && Main.netMode != NetmodeID.MultiplayerClient)
            {
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + new Vector2(30, 30), new Vector2(10, 10), ModContent.ProjectileType<ZeroA_EchoDeathray>(), 70, 0f, Main.myPlayer, 0, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + new Vector2(-30, 30), new Vector2(-10, 10), ModContent.ProjectileType<ZeroA_EchoDeathray>(), 70, 0f, Main.myPlayer, 0, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + new Vector2(30, -30), new Vector2(10, -10), ModContent.ProjectileType<ZeroA_EchoDeathray>(), 70, 0f, Main.myPlayer, 0, NPC.whoAmI);
                Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center + new Vector2(-30, -30), new Vector2(-10, -10), ModContent.ProjectileType<ZeroA_EchoDeathray>(), 70, 0f, Main.myPlayer, 0, NPC.whoAmI);
            }

            if (zero.ai[0] != 4)
            {
                NPC.life = 0;
                SpawnDust();
            }
        }

        int Frame = 0;
        public override void FindFrame(int frameHeight)
        {
            int frameWidth = TextureAssets.Npc[NPC.type].Value.Width / 2;
            NPC.frame.Width = frameWidth;

            if (NPC.frameCounter++ > 3)
            {
                NPC.frameCounter = 0;
                Frame += 1;
            }

            if (Frame > 6)
            {
                Frame = 0;
            }

            NPC.frame.X = 0;
            NPC.frame.Y = frameHeight * Frame;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - Main.screenPosition, NPC.frame, AAColor.Oblivion, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.SpriteEffectDirection(), 0f);
            return false;
        }

        public void SpawnDust()
        {
            Vector2 position = NPC.Center + Vector2.One * -20f;
            int num84 = 40;
            int height3 = num84;
            for (int num85 = 0; num85 < 3; num85++)
            {
                int num86 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0f, 0f, 100, default, 1.5f);
                //Main.dust[num86].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num86].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
            }
            for (int num87 = 0; num87 < 7; num87++)
            {
                int num88 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0, 0, 100, new Color(), 2f);
                //Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity *= 3f;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * (2f + Main.rand.NextFloat() * 4f);
                num88 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0, 0, 100, new Color(), 2f);
                //Main.dust[num88].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num88].position = NPC.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * num84 / 2f;
                Main.dust[num88].velocity *= 2f;
                Main.dust[num88].noGravity = true;
                Main.dust[num88].fadeIn = 1f;
                Main.dust[num88].color = Color.Black * 0.5f;
                Main.dust[num88].noLight = true;
                Main.dust[num88].velocity += NPC.DirectionTo(Main.dust[num88].position) * 8f;
            }
            for (int num89 = 0; num89 < 5; num89++)
            {
                int num90 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0, 0, 100, new Color(), 2f);
                //Main.dust[num90].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num90].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num90].noGravity = true;
                Main.dust[num90].noLight = true;
                Main.dust[num90].velocity *= 3f;
                Main.dust[num90].velocity += NPC.DirectionTo(Main.dust[num90].position) * 2f;
            }
            for (int num91 = 0; num91 < 15; num91++)
            {
                int num92 = Dust.NewDust(position, num84, height3, DustID.GemRuby, 0, 0, 100, new Color(), 2f);
                //Main.dust[num92].shader = GameShaders.Armor.GetSecondaryShader(59, Main.LocalPlayer);
                Main.dust[num92].position = NPC.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(NPC.velocity.ToRotation(), default) * num84 / 2f;
                Main.dust[num92].noGravity = true;
                Main.dust[num92].velocity *= 3f;
                Main.dust[num92].velocity += NPC.DirectionTo(Main.dust[num92].position) * 3f;
            }
        }
    }
}
