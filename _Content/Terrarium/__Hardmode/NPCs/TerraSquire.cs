using AAModClassic._Content.Terrarium.World.Biomes;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Terrarium.__Hardmode.NPCs
{
    public class TerraSquire : ModNPC, IBannerNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Terra Squire");
            Main.npcFrameCount[NPC.type] = 20;

            NPCID.Sets.NPCBestiaryDrawModifiers value = new()
            {
                Velocity = -2,
                PortraitPositionYOverride = 0,
                Position = new(0, 12)
            };
            NPCID.Sets.NPCBestiaryDrawOffset[Type] = value;
        }
        public override void SetDefaults()
        {
            NPC.width = 36;
            NPC.height = 42;

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
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.TerraSquireBanner>();
            SpawnModBiomes = [ModContent.GetInstance<TerrariumBiome>().Type];
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
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, NPC.frame, NPC.dontTakeDamage ? color : drawColor, NPC.rotation, NPC.frame.Size() * 0.5f, NPC.scale, NPC.direction == 1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None, 0);
            return false;
        }
    }
}