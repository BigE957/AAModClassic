using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.___Content.Terrarium.__Hardmode.NPCs
{
    public class TerraSquire : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Squire");
            Main.npcFrameCount[NPC.type] = 20;
        }
        public override void SetDefaults()
        {
            NPC.width = 58;
            NPC.height = 70;

            NPC.damage = 40;
            NPC.friendly = false;
            NPC.defense = 18;
            NPC.lifeMax = 300;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 0f;
            NPC.knockBackResist = 0.05f;
            NPC.aiStyle = NPCAIStyleID.Fighter;
            NPC.lavaImmune = true;
            AIType = NPCID.GraniteGolem;  //npc behavior
            AnimationType = NPCID.GraniteGolem;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<Items.Banners.TerraSquireBanner>();
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn this
            {
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Terra, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f);
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Terra, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f);
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Terra, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f);
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Terra, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f);
                Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Terra, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f);
            }
            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Terra, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f);
            Dust.NewDust(NPC.position + NPC.velocity, NPC.width, NPC.height, DustID.Terra, NPC.velocity.X * 0.5f, NPC.velocity.Y * 0.5f);
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Color color = BaseUtility.MultiLerpColor(Main.LocalPlayer.miscCounter % 100 / 100f, BaseDrawing.GetLightColor(NPC.position), BaseDrawing.GetLightColor(NPC.position), Color.LimeGreen, BaseDrawing.GetLightColor(NPC.position), Color.LimeGreen, BaseDrawing.GetLightColor(NPC.position));
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, NPC.dontTakeDamage ? color : drawColor);
            return false;
        }
    }
}