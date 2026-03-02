using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Mire
{
    public class Newt : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Newt");
            Main.npcFrameCount[NPC.type] = 15;
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
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("NewtBanner").Type;
        }
        
        private bool tongueAttack;
        private int tongueFrame;
        private int tongueCounter;
        private int tongueTimer;

        public override void AI()
        {
            Player player = Main.player[NPC.target]; // makes it so you can reference the player the npc is targetting
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
                if (Main.rand.Next(30) == 0) // so it wont do it repeatedly when the player is near. increase to lower the chance of it doing it
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
                        Projectile.NewProjectile(new Vector2(NPC.position.X + 56f, NPC.Center.Y), new Vector2(3 + Main.rand.Next(0, 3), -4 + Main.rand.Next(-4, 0)), Mod.Find<ModProjectile>("AcidProj").Type, 15, 3);
                    }
                    else
                    {
                        Projectile.NewProjectile(new Vector2(NPC.Center.X - 56f, NPC.Center.Y), new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), Mod.Find<ModProjectile>("AcidProj").Type, 15, 3);
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

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/NewtGoreTail"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/NewtGoreBody"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/NewtGoreLeg"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/NewtGoreHead"), 1f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D tongueAni = Mod.GetTexture("NPCs/Enemies/Mire/Newt_Shoot");
            var effects = NPC.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (tongueAttack == false) // i think this is important for it to not do its usual walking cycle while its also doing those attacks
            {
                spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, effects, 0f);
            }
            if (tongueAttack == true)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                int num214 = tongueAni.Height / 4;
                int y6 = num214 * tongueFrame;
                Main.spriteBatch.Draw(tongueAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, tongueAni.Width, num214)), drawColor, NPC.rotation, new Vector2(tongueAni.Width / 2f, num214 / 2f), NPC.scale, effects, 0f);
            }
            return false;
        }

        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("MirePod").Type);
        }
    }
}