using AAModClassic._Content.Mire.Projectiles;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic._CrossMod;
using AAModClassic.Utilities;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.Utilities;


namespace AAModClassic._Content.Mire.__Hardmode.NPCs._Surface._Night
{
    public class Toxitoad : ModNPC, IBannerNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Toxitoad");
            Main.npcFrameCount[NPC.type] = 7;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionXOverride = 0,
                Position = new(-2, 0)
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 64;
            NPC.height = 32;
            NPC.friendly = false; // its a mean toad! :(
            NPC.damage = 40;
            NPC.defense = 20;
            NPC.lifeMax = 1500;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 100000f;
            NPC.knockBackResist = 0.1f;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            AIType = NPCID.GoblinScout;
            NPC.rarity = 2;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<ToxitoadBanner>();
            SpawnModBiomes = [ModContent.GetInstance<MireBiome>().Type];
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (Main.dayTime && !AAWorld.downedYamata)
                return 0f;

            if (Main.hardMode && spawnInfo.Player.ZoneSurface() && spawnInfo.Player.ZoneAnyMire() && !NPCUtils.AnyEvents(spawnInfo.Player))
                return ContentReplacementSystem.NeedToReplaceContent ? 0.05f : .005f;

            return 0f;
        }

        private bool biteAttack;
        private bool tongueAttack;
        private int tongueFrame;
        private int tongueCounter;
        private int tongueTimer;
        private int biteFrame;
        private int biteCounter;
        private int biteTimer;

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int Money = 0; Money < 5; Money++)
            {
                if (Main.rand.NextBool(7) || Main.rand.NextBool(7) || Main.rand.NextBool(7) || Main.rand.NextBool(7))
                {
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.CopperCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
                if (Main.rand.NextBool(7) || Main.rand.NextBool(7))
                {
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.SilverCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
                if (Main.rand.NextBool(7))
                {
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ItemID.GoldCoin);       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
                }
            }
            if (NPC.life <= 0 && !Main.dedServ)
            {

                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToxitoadGoreHead").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToxitoadGoreFrontLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToxitoadGoreFrontLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToxitoadGoreEye").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToxitoadGoreCoinChain").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToxitoadGoreBody").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToxitoadGoreBackLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ToxitoadGoreBackLeg").Type, 1f);
            }
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target]; // makes it so you can reference the player the npc is targetting
            if (player.Center.X > NPC.Center.X)
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
                if (biteFrame >= 3)
                {
                    biteFrame = 0;
                }
            }
            if (tongueAttack == true)
            {
                if (tongueFrame < 8)
                {
                    tongueCounter++;
                }
                if (tongueCounter > 5)
                {
                    tongueFrame++;
                    tongueCounter = 0;
                }
                if (tongueFrame >= 8)
                {
                    tongueFrame = 7;
                }
            }
            float distance = NPC.Distance(Main.player[NPC.target].Center);
            if (distance <= 50) // so it only bites when the player is right next to it
            {
                if (biteAttack == false && tongueAttack == false) // so it doesnt bite while its currently biting, and if its doing the tongue attack
                {
                    biteAttack = true;
                }
            }
            if (biteAttack == true)
            {
                biteTimer++;
                NPC.aiStyle = NPCAIStyleID.FaceClosestPlayer; // so the dude doesnt spaz right and left when not moving
                NPC.velocity.X = 0; // stops the dude from moving right or left
                if (biteTimer >= 30) // when 30 frames have gone by, reset all those values
                {
                    biteAttack = false;
                    biteTimer = 0;
                    biteCounter = 0;
                    biteFrame = 0;
                }
            }
            if (distance <= 150) // distance until it does the tongue attack
            {
                if (Main.rand.NextBool(60)) // so it wont do it repeatedly when the player is near. increase to lower the chance of it doing it
                {
                    if (tongueAttack == false && biteAttack == false)
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
                if (tongueTimer >= 35)
                {
                    // projectile code, donno how to do it though, so it just throws up dirt ¯\_(ツ)_/¯
                    if (NPC.direction == -1)
                    {
                        //Main.PlaySound(SoundID.Item3, (int)npc.position.X, (int)npc.position.Y);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + 17f, NPC.position.Y + 18f), new Vector2(-6 + Main.rand.Next(-6, 0), -4 + Main.rand.Next(-4, 0)), ModContent.ProjectileType<AcidProj>(), 15, 3);
                    }
                    else
                    {
                        //Main.PlaySound(SoundID.Item3, (int)npc.position.X, (int)npc.position.Y);
                        Projectile.NewProjectile(NPC.GetSource_FromThis(), new Vector2(NPC.position.X + 57f, NPC.position.Y + 18f), new Vector2(6 + Main.rand.Next(0, 6), -4 + Main.rand.Next(-4, 0)), ModContent.ProjectileType<AcidProj>(), 15, 3);
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
            if (tongueAttack == false && biteAttack == false) // so it changes back to aiStyle 3 after the attacks are done
            {
                NPC.aiStyle = NPCAIStyleID.Fighter;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (biteAttack == false && tongueAttack == false)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += 36;
                    if (NPC.frame.Y > 214)
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
        
        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D texture = TextureAssets.Npc[NPC.type].Value;
            Texture2D biteAni = ModContent.Request<Texture2D>(Texture + "Bite").Value;
            Texture2D tongueAni = ModContent.Request<Texture2D>(Texture + "TongueAttack").Value;
            var effects = NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
            if (biteAttack == false && tongueAttack == false) // i think this is important for it to not do its usual walking cycle while its also doing those attacks
            {
                spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() / 2, NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (biteAttack == true)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                int num214 = biteAni.Height / 3; // 3 is the number of frames in the sprite sheet
                int y6 = num214 * biteFrame;
                Main.spriteBatch.Draw(biteAni, drawCenter - screenPos, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, biteAni.Width, num214)), drawColor, NPC.rotation, new Vector2(biteAni.Width / 2f, num214 / 2f), NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (tongueAttack == true)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                int num214 = tongueAni.Height / 8;
                int y6 = num214 * tongueFrame;
                Main.spriteBatch.Draw(tongueAni, drawCenter - screenPos, new Microsoft.Xna.Framework.Rectangle?(new Rectangle(0, y6, tongueAni.Width, num214)), drawColor, NPC.rotation, new Vector2(tongueAni.Width / 2f, num214 / 2f), NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            return false;
        }
    }
}