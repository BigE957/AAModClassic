using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.Projectiles;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.NPCs
{
    public class Newt : ModNPC, IBannerNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Newt");
            Main.npcFrameCount[NPC.type] = 15;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionXOverride = 0,
                Position = new Vector2(-32, 0),
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

        public override void SetDefaults()
        {
            NPC.width = 112;
            NPC.height = 30;
            NPC.damage = 10;
            NPC.defense = 10;
            NPC.damage = 28;
            NPC.defense = 6;
            NPC.lifeMax = 60;
            NPC.knockBackResist = 0.55f;
            NPC.value = 100f;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            AIType = NPCID.Crawdad;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<NewtBanner>();
            SpawnModBiomes = [ModContent.GetInstance<MireBiome>().Type];
        }
        
        private bool tongueAttack;
        private int tongueFrame;
        private int tongueCounter;
        private int tongueTimer;

        public override void AI()
        {
            Player player = Main.player[NPC.target]; // makes it so you can reference the player the npc is targetting
            if (!tongueAttack)
            {
                if (NPC.velocity.X < 0) // so it faces the player
                {
                    NPC.direction = 1;
                }
                else if (NPC.velocity.X > 0)
                {
                    NPC.direction = -1;
                }
            }
            else
            {
                if (player.position.X < NPC.position.X)
                {
                    NPC.direction = 1;
                }
                else
                {
                    NPC.direction = -1;
                }
            }
            if (tongueAttack == true)
            {
                if (tongueFrame < 3)
                {
                    tongueCounter++;
                }
                if (tongueCounter > 5)
                {
                    tongueFrame++;
                    tongueCounter = 0;
                }
                if (tongueFrame >= 3)
                {
                    tongueFrame = 0;
                }
            }
            float distance = NPC.Distance(Main.player[NPC.target].Center);
            if (distance >= 100) // distance until it does the tongue attack
            {
                if (Main.rand.NextBool(30)) // so it wont do it repeatedly when the player is near. increase to lower the chance of it doing it
                {
                    if (tongueAttack == false)
                    {
                        tongueAttack = true;
                    }
                }
            }
            if (tongueAttack == true)
            {
                tongueTimer++;
                NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer;
                NPC.velocity.X = 0;
                if (tongueTimer == 35)
                {
                    if (NPC.direction == -1)
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + 56f, NPC.Center.Y), new Vector2(3 + Main.rand.Next(0, 3), -4 + Main.rand.Next(-4, 0)), ModContent.ProjectileType<AcidProj>(), 15, 3);
                    }
                    else
                    {
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.Center.X - 56f, NPC.Center.Y), new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), ModContent.ProjectileType<AcidProj>(), 15, 3);
                    }
                }
                if (tongueTimer >= 100)
                {
                    tongueAttack = false;
                    tongueTimer = 0;
                    tongueCounter = 0;
                    tongueFrame = 0;
                }
            }
            if (tongueAttack == false) // so it changes back to aiStyle 3 after the attacks are done
            {
                NPC.aiStyle = NPCAIStyleID.Fighter;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (tongueAttack == false)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += 30;
                    if (NPC.frame.Y > 420)
                    {
                        NPC.frameCounter = 0;
                        NPC.frame.Y = 0;
                    }
                }
            }
            else
            {
                NPC.frameCounter = 0;
                NPC.frame.Y = 0;
            }
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("NewtGoreTail").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("NewtGoreBody").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("NewtGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("NewtGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("NewtGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("NewtGoreLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("NewtGoreHead").Type, 1f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D tongueAni = ModContent.Request<Texture2D>(Texture + "_Shoot").Value;
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (tongueAttack == false) // i think this is important for it to not do its usual walking cycle while its also doing those attacks
            {
                spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0f);
            }
            if (tongueAttack == true)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                int num214 = tongueAni.Height / 4;
                int y6 = num214 * tongueFrame;
                spriteBatch.Draw(tongueAni, drawCenter - screenPos, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, tongueAni.Width, num214)), drawColor, NPC.rotation, new Vector2(tongueAni.Width / 2f, num214 / 2f), NPC.scale, effects, 0f);
            }
            return false;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MirePod>()));
        }
    }
}