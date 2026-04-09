//TODOGENERAL holy shit what do i do with this thing
namespace AAModClassic._Unreleased._Aggregate.inferno.npcs
{
    /*public class Magmalgam : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Magmalgam");
            Main.npcFrameCount[NPC.type] = 4;
        }
        public override void SetDefaults()
        {
            NPC.width = 54;
            NPC.height = 50;
            NPC.friendly = false;
            NPC.damage = 20;
            NPC.defense = 30;
            NPC.lifeMax = 2000;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 100000f;
            NPC.knockBackResist = 0;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            AIType = NPCID.GoblinScout;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.hardMode && spawnInfo.Player.GetModPlayer<AAPlayer>().ZoneInferno)
            {
                return SpawnCondition.OverworldNightMonster.Chance * 0.25f;
            }
            return 0f;
        }

        private bool biteAttack;
        private bool fireballAttack;
        private bool ded;
        private bool live;
        private int fireballFrame;
        private int fireballCounter;
        private int fireballTimer;
        private int biteFrame;
        private int biteCounter;
        private int biteTimer;
        private int dedFrame;
        private int dedCounter;
        private int dedTimer;
        private int liveFrame;
        private int liveCounter;
        private int liveTimer;
        private bool alive;

        public override void OnHitNPC(NPC target, NPC.HitInfo hit)
        {
            for (int Money = 0; Money < 5; Money++)
            {
                if (Main.rand.Next(7) == 0 || Main.rand.Next(7) == 1)
                {
                    Item.NewItem(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.CopperCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
                if (Main.rand.Next(7) == 2 || Main.rand.Next(7) == 3 || Main.rand.Next(7) == 4)
                {
                    Item.NewItem(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.SilverCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
                if (Main.rand.Next(7) == 5 || Main.rand.Next(7) == 6)
                {
                    Item.NewItem(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.GoldCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
                if (Main.rand.Next(7) == 7)
                {
                    Item.NewItem(NPC.GetSource_FromThis(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.PlatinumCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
            }
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            float distance = NPC.Distance(Main.player[NPC.target].Center);
            if (distance <= 9600 && alive == true) // distance until it does the fireball attack
            {
                alive = false;
                ded = true;
                biteAttack = false;
                fireballAttack = false;
                live = false;
                NPC.aiStyle = 0;
            }
            else
            {
                ded = false;
                alive = true;
                NPC.aiStyle = 3;
            }
            if (biteAttack == false && fireballAttack == false && ded == false && live == false && alive == true)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += 50;
                    if (NPC.frame.Y > 150)
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
            if (player.Center.X > NPC.Center.X) // so it faces the player
            {
                NPC.spriteDirection = -1;
            }
            else
            {
                NPC.spriteDirection = 1;
            }
            if (biteAttack == true)
            {
                biteCounter++;
                if (biteCounter > 10)
                {
                    biteFrame++;
                    biteCounter = 0;
                }
                if (biteFrame >= 4)
                {
                    biteFrame = 0;
                }
            }
            if (fireballAttack == true)
            {
                if (fireballFrame < 4)
                {
                    fireballCounter++;
                }
                if (fireballCounter > 5)
                {
                    fireballFrame++;
                    fireballCounter = 0;
                }
                if (fireballFrame >= 4)
                {
                    fireballFrame = 3;
                }
            }
            if (ded == true)
            {
                if (dedFrame < 12)
                {
                    dedCounter++;
                }
                if (dedCounter > 5)
                {
                    dedFrame++;
                    dedCounter = 0;
                }
                if (dedFrame >= 12)
                {
                    dedFrame = 11;
                    alive = false;
                }
            }
            if (live == true)
            {
                if (liveFrame < 20)
                {
                    liveCounter++;
                }
                if (liveCounter > 5)
                {
                    liveFrame++;
                    liveCounter = 0;
                }
                if (liveFrame >= 20)
                {
                    alive = true;
                }
            }
            if (distance <= 50) // so it only bites when the player is right next to it
            {
                if (biteAttack == false && fireballAttack == false && ded == false && live == false && alive == true) // so it doesnt bite while its currently biting, and if its doing the fireball attack
                {
                    biteAttack = true;
                }
            }
            if (biteAttack == true)
            {
                biteTimer++;
                NPC.aiStyle = 0; // so the dude doesnt spaz right and left when not moving
                NPC.velocity.X = 0; // stops the dude from moving right or left
                if (biteTimer >= 30) // when 30 frames have gone by, reset all those values
                {
                    biteAttack = false;
                    biteTimer = 0;
                    biteCounter = 0;
                    biteFrame = 0;
                }
            }
            if (distance <= 150) // distance until it does the fireball attack
            {
                if (Main.rand.Next(60) == 0) // so it wont do it repeatedly when the player is near. increase to lower the chance of it doing it
                {
                    if (fireballAttack == false && biteAttack == false && ded == false && live == false && alive == true)
                    {
                        fireballAttack = true;
                    }
                }
            }
            if (fireballAttack == true)
            {
                fireballTimer++;
                NPC.aiStyle = 0;
                NPC.velocity.X = 0;
                if (fireballTimer == 35)
                {
                    if (NPC.direction == -1)
                    {
                        //Main.PlaySound(SoundID.Item3, (int)npc.position.X, (int)npc.position.Y);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), (new Vector2(NPC.position.X + 17f, NPC.position.Y + 18f)), new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), ModContent.ProjectileType<Magma>(), 15, 3);
                    }
                    else
                    {
                        //Main.PlaySound(SoundID.Item3, (int)npc.position.X, (int)npc.position.Y);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), (new Vector2(NPC.position.X + 57f, NPC.position.Y + 18f)), new Vector2(6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-4, 0)), ModContent.ProjectileType<Magma>(), 15, 3);
                    }
                }
                if (fireballTimer >= 60)
                {
                    fireballAttack = false;
                    fireballTimer = 0;
                    fireballCounter = 0;
                    fireballFrame = 0;
                }
            }
            if (fireballAttack == false && biteAttack == false && ded == false && live == false && alive == true) // so it changes back to aiStyle 3 after the attacks are done
            {
                NPC.aiStyle = 3;
            }
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D biteAni = Mod.GetTexture("_Unreleased/NPCs/Enemies/Inferno/Magmalgam/MagmalgamBite");
            Texture2D shootAni = Mod.GetTexture("_Unreleased/NPCs/Enemies/Inferno/Magmalgam/MagmalgamFireball");
            Texture2D Reanimation = Mod.GetTexture("_Unreleased/NPCs/Enemies/Inferno/Magmalgam/MagmalgamReanimation");
            Texture2D dedAni = Mod.GetTexture("_Unreleased/NPCs/Enemies/Inferno/Magmalgam/MagmalgamDed");
            var effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (biteAttack == false && fireballAttack == false) // i think this is important for it to not do its usual walking cycle while its also doing those attacks
            {
                spriteBatch.Draw(texture, NPC.Center - Main.screenPosition, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (biteAttack == true)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                int num214 = biteAni.Height / 3; // 3 is the number of frames in the sprite sheet
                int y6 = num214 * biteFrame;
                Main.spriteBatch.Draw(biteAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, biteAni.Width, num214)), drawColor, NPC.rotation, new Vector2((float)biteAni.Width / 2f, (float)num214 / 2f), NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (fireballAttack == true)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                int num214 = shootAni.Height / 4;
                int y6 = num214 * fireballFrame;
                Main.spriteBatch.Draw(shootAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, shootAni.Width, num214)), drawColor, NPC.rotation, new Vector2((float)shootAni.Width / 2f, (float)num214 / 2f), NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (live == true)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                int num214 = Reanimation.Height / 20;
                int y6 = num214 * liveFrame;
                Main.spriteBatch.Draw(Reanimation, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, Reanimation.Width, num214)), drawColor, NPC.rotation, new Vector2((float)Reanimation.Width / 2f, (float)num214 / 2f), NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (ded == true)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                int num214 = dedAni.Height / 12;
                int y6 = num214 * dedFrame;
                Main.spriteBatch.Draw(dedAni, drawCenter - Main.screenPosition, new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y6, dedAni.Width, num214)), drawColor, NPC.rotation, new Vector2((float)dedAni.Width / 2f, (float)num214 / 2f), NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            return false;
        }
    }*/
}