using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Mire
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
            NPC.value = Item.sellPrice(0, 0, 6, 45);
            AIType = NPCID.Crawdad;
            AnimationType = NPCID.Crawdad;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.aiStyle = 3;
            NPC.width = 72;
            NPC.height = 78;
            NPC.lavaImmune = false;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("MossterBanner").Type;

        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/MossterGoreBackArm"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/MossterGoreBackLeg"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/MossterGoreBody"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/MossterGoreFrontArm"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/MossterGoreFrontLeg"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/MossterGoreHead"), 1f);
            }
        }
        
        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(Mod.GetTexture("Glowmasks/Mosster_Glow"), new Vector2(NPC.Center.X - Main.screenPosition.X, NPC.Center.Y - Main.screenPosition.Y),
            NPC.frame, Color.White, NPC.rotation,
            new Vector2(NPC.width * 0.5f, NPC.height * 0.5f), 1f, spriteEffects, 0f);
        }

        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("MirePod").Type);
        }
    }
}


