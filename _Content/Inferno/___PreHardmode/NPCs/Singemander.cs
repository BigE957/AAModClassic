using AAModClassic._Content.Inferno.___PreHardmode.Items.Materials;
using AAModClassic._Content.Inferno.World.Biomes;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Inferno.___PreHardmode.NPCs
{

    public class Singemander : ModNPC, IBannerNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Singemander");

            Main.npcFrameCount[NPC.type] = 5;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                PortraitPositionXOverride = 0,
                Position = new Vector2(-18, 0),
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 100;   //boss life
            NPC.damage = 14;  //boss damage
            NPC.defense = 14;    //boss defense
            NPC.knockBackResist = 1f;   //this boss will behavior like the DemonEye  //boss frame/animation 
            NPC.value = Item.buyPrice(0, 0, 6, 45);
            NPC.aiStyle = NPCAIStyleID.Fighter;
            AIType = NPCID.GoblinScout;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.width = 104;
            NPC.height = 28;
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.lavaImmune = true;
            NPC.buffImmune[BuffID.OnFire] = true;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<SingemanderBanner>();
            SpawnModBiomes = new int[1] { ModContent.GetInstance<InfernoBiome>().Type };
        }

        private bool biteAttack;
        private int biteFrame;
        private int biteCounter;
        private int biteTimer;

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0 && !Main.dedServ)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ISGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ISGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ISGore4").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ISGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ISGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ISGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("ISGore1").Type, 1f);
            }
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            Lighting.AddLight(NPC.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);
            
            if (NPC.velocity.X > 0) // so it faces the player
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
            float distance = NPC.Distance(Main.player[NPC.target].Center);
            if (distance <= 50) // so it only bites when the player is right next to it
            {
                if (biteAttack == false) // so it doesnt bite while its currently biting, and if its doing the tongue attack
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
            if (biteAttack == false) // so it changes back to aiStyle 3 after the attacks are done
            {
                NPC.aiStyle = NPCAIStyleID.Fighter;
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (biteAttack == false)
            {
                NPC.frameCounter++;
                if (NPC.frameCounter >= 10)
                {
                    NPC.frameCounter = 0;
                    NPC.frame.Y += frameHeight;
                    if (NPC.frame.Y > frameHeight * 4)
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
            Texture2D textureGlow = ModContent.Request<Texture2D>(Texture + "_Glow").Value;
            Texture2D biteAni = ModContent.Request<Texture2D>(Texture + "_Nom").Value;
            Texture2D biteGlow = ModContent.Request<Texture2D>(Texture + "_Nom_Glow").Value;
            if (biteAttack == false) // i think this is important for it to not do its usual walking cycle while its also doing those attacks
            {
                spriteBatch.Draw(texture, NPC.Center - screenPos, NPC.frame, drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.Draw(textureGlow, NPC.Center - screenPos, NPC.frame, Color.White, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            if (biteAttack == true)
            {
                Vector2 drawCenter = new Vector2(NPC.Center.X, NPC.Center.Y);
                Rectangle frame = biteAni.Frame(1, 3, 0, biteFrame);
                spriteBatch.Draw(biteAni, drawCenter - screenPos, frame, drawColor, NPC.rotation, frame.Size() * 0.5f, NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
                spriteBatch.Draw(biteGlow, drawCenter - screenPos, frame, Color.White, NPC.rotation, frame.Size() * 0.5f, NPC.scale, NPC.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            }
            return false;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<DragonScale>()));
        }
    }
}


