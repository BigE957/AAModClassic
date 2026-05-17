using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic._Content.Void.__Hardmode.NPCs
{
    public class Vortex : ModNPC
    {
        public static Asset<Texture2D> Glowmask;
        public static Asset<Texture2D> Blades;
        public static Asset<Texture2D> BladesGlowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vortex");

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            Blades = ModContent.Request<Texture2D>(Texture + "_Blades");
            BladesGlowmask = ModContent.Request<Texture2D>(Texture + "_Blades_Glow");
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 100;
            NPC.width = 84;
            NPC.height = 84;
            NPC.aiStyle = -1;
            NPC.damage = 40;
            NPC.defense = 40;
            NPC.lifeMax = 1000;
            NPC.value = Item.sellPrice(0, 0, 50, 0);
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.netAlways = true;
            Banner = NPC.type;
			BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.VortexBanner>();
        }

        public float Rotation = 0;

        /*
        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<VoidEnergy>(), Main.rand.Next(1, 4));
        }
        */
        public override void AI()
        {
            BaseAI.AIElemental(NPC, ref NPC.ai, null, 1, false, false, 800f, 600f, 180, 3f);

            if (NPC.velocity.X > 0)
            {
                Rotation += .03f;
            }
            else if (NPC.velocity.X < 0)
            {
                Rotation -= .03f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            BaseDrawing.DrawTexture(spriteBatch, Blades.Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, Rotation, 0, 1, new Rectangle(0, 0, Blades.Value.Width, Blades.Value.Height), drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, new Rectangle(0, 0, TextureAssets.Npc[NPC.type].Value.Width, TextureAssets.Npc[NPC.type].Value.Height), drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, BladesGlowmask.Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, Rotation, 0, 1, new Rectangle(0, 0, BladesGlowmask.Value.Width, BladesGlowmask.Value.Height), AAColor.ZeroShield, true);
            BaseDrawing.DrawTexture(spriteBatch, Glowmask.Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, new Rectangle(0, 0, Glowmask.Value.Width, Glowmask.Value.Height), AAColor.ZeroShield, true);
            return false;
        }
    }
}