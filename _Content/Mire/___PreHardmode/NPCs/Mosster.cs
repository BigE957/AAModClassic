using AAModClassic._Content.Mire.___PreHardmode.Items.Materials;
using AAModClassic._Content.Mire.World.Biomes;
using AAModClassic.Items.Banners;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.NPCs
{

    public class Mosster : ModNPC
    {

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Mosster");

            Main.npcFrameCount[NPC.type] = 8;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 80;   //boss life
            NPC.damage = 30;  //boss damage
            NPC.defense = 8;    //boss defense
            NPC.knockBackResist = 0f;
            NPC.value = Item.buyPrice(0, 0, 6, 45);
            AIType = NPCID.Crawdad;
            AnimationType = NPCID.Crawdad;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            NPC.width = 72;
            NPC.height = 78;
            NPC.lavaImmune = false;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<MossterBanner>();
            SpawnModBiomes = [ModContent.GetInstance<MireBiome>().Type];
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MossterGoreBackArm").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MossterGoreBackLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MossterGoreBody").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MossterGoreFrontArm").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MossterGoreFrontLeg").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MossterGoreHead").Type, 1f);
            }
        }
        
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(ModContent.Request<Texture2D>(Texture + "_Glow").Value, new Vector2(NPC.Center.X - Main.screenPosition.X, NPC.Center.Y - Main.screenPosition.Y),
            NPC.frame, Color.White, NPC.rotation,
            new Vector2(NPC.width * 0.5f, NPC.height * 0.5f), 1f, spriteEffects, 0f);
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<MirePod>()));
        }
    }
}


