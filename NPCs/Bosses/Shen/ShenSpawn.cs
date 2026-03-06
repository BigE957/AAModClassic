using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Dusts;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Shen
{
    public class ShenSpawn : ModNPC
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Shen/ShenSpawn";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discord");
            Terraria.ID.NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.alpha = 255;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenIntro");
            NPC.lifeMax = 1000000000;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10000000;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        int dustx = 50;

        public override void AI()
        {
            if (!NPC.HasNPCTarget)
            {
                NPC.TargetClosest();
            }
            Player player = Main.player[NPC.target];
            NPC.Center = player.Center - new Vector2(0, 300f);
            NPC.ai[0]++;
            if (NPC.ai[0] <= 960)
            {
                for (int LOOP = 0; LOOP < 4; LOOP++)
                {
                    Dust dust1;
                    Dust dust2;
                    Vector2 position1 = new Vector2(NPC.Center.X + dustx, NPC.Center.Y);
                    Vector2 position2 = new Vector2(NPC.Center.X - dustx, NPC.Center.Y);
                    dust1 = Main.dust[Dust.NewDust(position1, 1, 1, ModContent.DustType<AkumaDust>(), 0, 0, 0, default, 1f)];
                    dust1.noGravity = false;
                    dust2 = Main.dust[Dust.NewDust(position2, 1, 1, ModContent.DustType<YamataDust>(), 0, 0, 0, default, 1f)];
                    dust2.noGravity = true;
                    dust2.scale *= 1.3f;
                    dust2.velocity.Y -= 6;
                }
            }
            else if (NPC.ai[0] > 960 && NPC.ai[0] < 1640)
            {
                for (int LOOP = 0; LOOP < 8; LOOP++)
                {
                    Dust dust1;
                    Vector2 position1 = NPC.Center;
                    dust1 = Main.dust[Dust.NewDust(position1, 20, 20, ModContent.DustType<Discord>(), 0, 0, 0, default, 1f)];
                    dust1.noGravity = false;
                    dust1.scale *= 1.3f;
                    dust1.velocity.Y -= 6;
                }
            }

            if (NPC.ai[0] == 180)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn1"), new Color(180, 41, 32));
            }

            if (NPC.ai[0] == 360)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn2"), new Color(45, 46, 70));
            }

            if (NPC.ai[0] == 540)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn3"), new Color(180, 41, 32));
            }

            if (NPC.ai[0] == 720)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn4"), new Color(45, 46, 70));
            }
            if (NPC.ai[0] == 900)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn5"), new Color(180, 41, 32));
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn5"), new Color(45, 46, 70));
            }

            if (dustx > 0 && NPC.ai[0] >= 900)
            {
                dustx -= 1;
                if (dustx < 0)
                {
                    dustx = 0;
                }
            }

            if (NPC.ai[0] == 960)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn6"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (NPC.ai[0] == 1140)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn7"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (NPC.ai[0] == 1320)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn8"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
            }

            if (NPC.ai[0] >= 1500)
            {
                NPC.alpha -= 5;
            }

            if (NPC.ai[0] == 1520)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn9"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            }

            if (NPC.ai[0] == 1700)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn10"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            }

            if (NPC.ai[0] >= 1880)
            {
                if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenSpawn11"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                SummonShen();
                NPC.active = false;
            }
        }

        public override bool PreAI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                NPC.TargetClosest();
                Player player = Main.player[NPC.target];
                NPC.Center = player.Center - new Vector2(0, 300f); ;
                NPC.ai[0]++;

                if (NPC.ai[0] <= 960)
                {
                    for (int LOOP = 0; LOOP < 4; LOOP++)
                    {
                        Dust dust1;
                        Dust dust2;
                        Vector2 position1 = new Vector2(NPC.Center.X + dustx, NPC.Center.Y);
                        Vector2 position2 = new Vector2(NPC.Center.X - dustx, NPC.Center.Y);
                        dust1 = Main.dust[Dust.NewDust(position1, 1, 1, ModContent.DustType<AkumaDust>(), 0, 0, 0, default, 1f)];
                        dust1.noGravity = false;
                        dust2 = Main.dust[Dust.NewDust(position2, 1, 1, ModContent.DustType<YamataDust>(), 0, 0, 0, default, 1f)];
                        dust2.noGravity = true;
                        dust2.scale *= 1.3f;
                        dust2.velocity.Y -= 6;
                    }
                }
                else if (NPC.ai[0] > 960 && NPC.ai[0] < 1640)
                {
                    for (int LOOP = 0; LOOP < 8; LOOP++)
                    {
                        Dust dust1;
                        Vector2 position1 = NPC.Center;
                        dust1 = Main.dust[Dust.NewDust(position1, 20, 20, ModContent.DustType<Discord>(), 0, 0, 0, default, 1f)];
                        dust1.noGravity = false;
                        dust1.scale *= 1.3f;
                        dust1.velocity.Y -= 6;
                    }
                }

                if (NPC.ai[0] >= 400)
                {
                    NPC.alpha -= 5;
                }


                if (dustx > 0 && NPC.ai[0] >= 900)
                {
                    dustx -= 1;
                    if (dustx < 0)
                    {
                        dustx = 0;
                    }
                }

                if (NPC.ai[0] >= 600)
                {
                    SummonShen();
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
                return false;
            }
            return true;
        }

        public void SummonShen()
        {
            AAModGlobalNPC.SpawnBoss(Main.player[NPC.target], Mod.Find<ModNPC>("Shen").Type, false, NPC.Center, "");
        }

        public Color GetColorAlpha()
        {
            return new Color(233, 0, 233) * (Main.mouseTextColor / 255f);
        }
        
        public float auraPercent = 0f;
        public bool auraDirection = true;
        public bool saythelinezero = false;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            if (NPC.alpha <= 0)
            {
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, Color.White);
                BaseDrawing.DrawAura(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, auraPercent, 1f, 0f, 0f, GetColorAlpha());
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, GetColorAlpha());
                return false;
            }
            return true;
        }
    }

    public class ShenDefeat : ModNPC
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Shen/ShenSpawn";
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discord's Defeat");
            Terraria.ID.NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.height = 100;
            NPC.width = 444;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.alpha = 255;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/silence");
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (NPC.ai[1] > 240)
            {
                int i = AAWorld.downedShen ? 1 : 0;
                NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, ModContent.NPCType<ShenDeath>(), 0, i);
                NPC.active = false;
                NPC.netUpdate = true;
            }
            else
            {
                NPC.ai[1]++;
                NPC.ai[0]++;
                if (NPC.ai[0] > 4)
                {
                    NPC.ai[0] = 0;
                    SoundEngine.PlaySound(SoundID.Item124);
                    for (int i = 0; i < 3; i++)
                    {
                        Vector2 Pos = new Vector2(NPC.position.X + Main.rand.Next(0, 444), NPC.position.Y - Main.rand.Next(0, 100));
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), Pos, Vector2.Zero, ModContent.ProjectileType<ShenDeathBoom>(), 0, 0, Main.myPlayer, Main.rand.Next(3));
                    }
                }
            }
        }
    }

    public class ShenDeath : ModNPC
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Shen/ShenSpawn";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discord's Death");
            Terraria.ID.NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }

        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.lifeMax = 1;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.alpha = 255;
            Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ChaosSissy");
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                AAWorld.downedShen = true;
                NPC.active = false;
                NPC.netUpdate = true;
            }
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            NPC.Center = player.Center;

            NPC.ai[1]++;
            if (NPC.ai[0] == 0)
            {
                if (NPC.ai[1] == 180)
                {
                    BaseUtility.Chat(Lang.BossChat("ShenDeath1"), new Color(180, 41, 32), false);
                }

                if (NPC.ai[1] == 360)
                {
                    BaseUtility.Chat(Lang.BossChat("ShenDeath2"), new Color(45, 46, 70), false);
                }

                if (NPC.ai[1] == 540)
                {
                    string Name = Main.netMode != NetmodeID.SinglePlayer ? Lang.BossChat("ShenDeath3") : player.name;
                    BaseUtility.Chat(Name + Lang.BossChat("ShenDeath4"), new Color(180, 41, 32), false);
                }

                if (NPC.ai[1] == 720)
                {
                    BaseUtility.Chat(Lang.BossChat("ShenDeath5"), new Color(45, 46, 70), false);
                }

                if (NPC.ai[1] == 899)
                {
                    BaseUtility.Chat(Lang.BossChat("ShenDeath6"), new Color(45, 46, 70), false);
                    BaseUtility.Chat(Lang.BossChat("ShenDeath6"), new Color(180, 41, 32), false);
                }

                if (NPC.ai[1] >= 900)
                {
                    AAWorld.downedShen = true;
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
                return;
            }
            else
            {
                if (NPC.ai[1] == 180)
                {
                    BaseUtility.Chat(Lang.BossChat("ShenDeath7"), new Color(45, 46, 70), false);
                }

                if (NPC.ai[1] == 360)
                {
                    BaseUtility.Chat(Lang.BossChat("ShenDeath8"), new Color(180, 41, 32), false);
                }

                if (NPC.ai[1] == 540)
                {
                    string Name = Main.netMode != NetmodeID.SinglePlayer ? Lang.BossChat("ShenDeath9") : player.Male ? Lang.BossChat("boy") : Lang.BossChat("girl");
                    BaseUtility.Chat(Lang.BossChat("ShenDeath10") + Name + Lang.BossChat("ShenDeath11"), new Color(45, 46, 70), false);
                }

                if (NPC.ai[1] == 720)
                {
                    BaseUtility.Chat(Lang.BossChat("ShenDeath12"), new Color(180, 41, 32), false);
                }

                if (NPC.ai[1] == 899)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Lang.BossChat("ShenDeath13"), new Color(45, 46, 70), false);
                        BaseUtility.Chat(Lang.BossChat("ShenDeath13"), new Color(180, 41, 32), false);
                    }
                }
                if (NPC.ai[1] >= 900)
                {
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
        }
    }

    public class ShenTransition : ModNPC
    {
        public override string Texture => "AAModClassic/NPCs/Bosses/Shen/ShenTransition";

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Discordian Awakening");
            Terraria.ID.NPCID.Sets.ShouldBeCountedAsBoss[NPC.type] = true;
        }
        public override void SetDefaults()
        {
            NPC.width = 100;
            NPC.height = 100;
            NPC.friendly = false;
            NPC.alpha = 255;
            NPC.lifeMax = 10000000;
            NPC.dontTakeDamage = true;
            NPC.noGravity = true;
            NPC.aiStyle = -1;
            NPC.timeLeft = 10;
            for (int k = 0; k < NPC.buffImmune.Length; k++)
            {
                NPC.buffImmune[k] = true;
            }
        }

        public override void AI()
        {
            NPC.TargetClosest();
            Player player = Main.player[NPC.target];
            NPC.Center = player.Center - new Vector2(0, 300f); ;
            NPC.ai[0]++;
            if (NPC.timeLeft <= 10)
            {
                NPC.timeLeft = 10;
            }
            if (NPC.ai[0] > 350)
            {
                Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA");
                for (int LOOP = 0; LOOP < 8; LOOP++)
                {
                    Dust dust1;
                    Vector2 position1 = NPC.Center;
                    dust1 = Main.dust[Dust.NewDust(position1, 20, 20, ModContent.DustType<Discord>(), 0, 0, 0, default, 1f)];
                    dust1.noGravity = false;
                    dust1.scale *= 1.3f;
                    dust1.velocity.Y -= 6;
                }
            }

            if (Main.netMode != NetmodeID.MultiplayerClient)
            {
                if (NPC.ai[0] == 375)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenTransition1"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] == 475)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenTransition2"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] == 600)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenTransition3"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] == 820)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenTransition4"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] == 960)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenTransition5"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] >= 1100)
                {
                    NPC.alpha -= 5;
                }
                if (NPC.ai[0] == 1100)
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenTransition6"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);
                    NPC.netUpdate = true;
                }
                if (NPC.ai[0] >= 1400)
                {
                    SummonShen();
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
            }
        }

        public override bool PreAI()
        {
            if (AAConfigClient.Instance.NoBossDialogue)
            {
                NPC.TargetClosest();
                Player player = Main.player[NPC.target];
                NPC.Center = player.Center - new Vector2(0, 300f); ;
                NPC.ai[0]++;
                if (NPC.alpha < 255 && NPC.ai[0] > 200)
                {
                    Music = Mod.GetSoundSlot(SoundType.Music, "Sounds/Music/ShenA");
                    for (int LOOP = 0; LOOP < 8; LOOP++)
                    {
                        Dust dust1;
                        Vector2 position1 = NPC.Center;
                        dust1 = Main.dust[Dust.NewDust(position1, 20, 20, ModContent.DustType<Discord>(), 0, 0, 0, default, 1f)];
                        dust1.noGravity = false;
                        dust1.scale *= 1.3f;
                        dust1.velocity.Y -= 6;
                    }
                }

                if (NPC.ai[0] >= 400)
                {
                    NPC.alpha -= 5;
                }

                if (NPC.ai[0] >= 600)
                {
                    SummonShen();
                    NPC.active = false;
                    NPC.netUpdate = true;
                }
                return false;
            }
            return true;
        }

        public void SummonShen()
        {
            Player player = Main.player[NPC.target];
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenTransition7"), Color.Magenta.R, Color.Magenta.G, Color.Magenta.B);
            if (Main.netMode != NetmodeID.MultiplayerClient) BaseUtility.Chat(Lang.BossChat("ShenTransition8"), Color.DarkMagenta.R, Color.DarkMagenta.G, Color.DarkMagenta.B);

            int b = Projectile.NewProjectile(NPC.GetSource_FromThis(), NPC.Center.X, NPC.Center.Y, 0f, 0f, Mod.Find<ModProjectile>("ShockwaveBoom").Type, 0, 1, Main.myPlayer, 0, 0);
            Main.projectile[b].Center = NPC.Center;


            AAModGlobalNPC.SpawnBoss(player, Mod.Find<ModNPC>("ShenA").Type, false, NPC.Center, "Shen Awakened", false);
        }

        public Color GetColorAlpha()
        {
            return new Color(233, 0, 233) * (Main.mouseTextColor / 255f);
        }

        public float auraPercent = 0f;
        public bool auraDirection = true;
        public bool saythelinezero = false;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            if (NPC.alpha <= 0)
            {
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, drawColor);
                BaseDrawing.DrawAura(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, auraPercent, 1f, 0f, 0f, GetColorAlpha());
                BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, GetColorAlpha());
                return false;
            }

            return true;
        }
    }
}