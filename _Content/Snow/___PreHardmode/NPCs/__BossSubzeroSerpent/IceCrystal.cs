using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.AbstractsLikeDigitalCircus.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Bestiary;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Snow.___PreHardmode.NPCs.__BossSubzeroSerpent
{
    public class IceCrystal : BiomeConvertableNPC
    {
        public override string Texture => "AAModClassic/_Content/Snow/___PreHardmode/NPCs/__BossSubzeroSerpent/CrystalTextures/IceCrystal";
        public override string AssetPath => "AAModClassic/_Content/Snow/___PreHardmode/NPCs/__BossSubzeroSerpent/CrystalTextures/";

        public override void SetDefaults()
        {
            NPC.lifeMax = 600;
            NPC.defense = 10;
            NPC.damage = 50;
            NPC.width = 30;
            NPC.height = 30;
            NPC.aiStyle = -1;
            if (!NPC.IsABestiaryIconDummy)
                NPC.alpha = 255;
            NPC.value = 0;
            NPC.noGravity = true;
            NPC.knockBackResist = 0f;
            NPC.HitSound = SoundID.Item30;
            NPC.DeathSound = SoundID.Item27;
        }

		public override void SetStaticDefaults()
		{
		    // DisplayName.SetDefault("Ice Crystal");
            Main.npcFrameCount[NPC.type] = 1;
            base.SetStaticDefaults();
        }

        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(
            [
                BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Snow,
                new FlavorTextBestiaryInfoElement("Mods.AAModClassic.Bestiary.IceCrystal")
            ]);
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                NPC.ai[2]++;
            }
            if (NPC.alpha > 50)
            {
                NPC.alpha -= 3;
            }
            else
            {
                //TODO: speed based on distance or something
                int p = BaseAI.ShootPeriodic(NPC, player.position, player.width, player.height, ModContent.ProjectileType<IceCrystal_IceSpike>(), ref NPC.ai[0], 80, NPC.damage / 2, 7, true);
                if (p != -1)
                {
                    ((IceCrystal_IceSpike)Main.projectile[p].ModProjectile).BiomeType = BiomeType;
                    int pieCut = 8;
                    float radians = MathHelper.TwoPi / pieCut;
                    for (int i = 0; i < pieCut; i++)
                    {
                        int dustID = Dust.NewDust(NPC.Center, 2, 2, ModContent.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 0.6f);
                    }
                }

                NPC.alpha = 40;
            }
        }

        public override void OnKill()
        {
            SoundEngine.PlaySound(SoundID.Item50, NPC.position);
            int pieCut = 20;
            float radians = MathHelper.TwoPi / pieCut;
            for (int i = 0; i < pieCut; i++)
            {
                int dustID = Dust.NewDust(NPC.Center, 2, 2, ModContent.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 1.6f);
                Main.dust[dustID].velocity = Vector2.Normalize(new Vector2(6, 0).RotatedBy(radians * i));
            }
            for (int i = 0; i < pieCut; i++)
            {
                int dustID = Dust.NewDust(NPC.Center, 2, 2, ModContent.DustType<Dusts.SnowDust>(), 0f, 0f, 100, Color.White, 2f);
                Main.dust[dustID].velocity = Vector2.Normalize(new Vector2(9, 0).RotatedBy(radians * i));
                Main.dust[dustID].noLight = false;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPC.IsABestiaryIconDummy)
                NPC.alpha = 0;

            spriteBatch.Draw(GetCurrentTexture(), NPC.Center - screenPos, NPC.frame, drawColor * NPC.Opacity, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0f);
            return false;
        }
    }
}
