using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Enemies.Void
{
    public class Vortex : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vortex");
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
			BannerItem = ModContent.ItemType<Items.Banners.VortexBanner>();
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
            Texture2D texture2D13 = TextureAssets.Npc[NPC.type].Value;
            Texture2D BladeTex = Mod.GetTexture("NPCs/Enemies/Void/Vortex_Blades");
            Texture2D GlowTex = Mod.GetTexture("Glowmasks/Vortex_Glow");
            Texture2D BladeGlowTex = Mod.GetTexture("Glowmasks/Vortex_Blades_Glow");

            BaseDrawing.DrawTexture(spriteBatch, BladeTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, Rotation, 0, 1, new Rectangle(0, 0, BladeTex.Width, BladeTex.Height), drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, texture2D13, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, new Rectangle(0, 0, texture2D13.Width, texture2D13.Height), drawColor, true);
            BaseDrawing.DrawTexture(spriteBatch, BladeGlowTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, Rotation, 0, 1, new Rectangle(0, 0, BladeGlowTex.Width, BladeGlowTex.Height), AAColor.ZeroShield, true);
            BaseDrawing.DrawTexture(spriteBatch, GlowTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, new Rectangle(0, 0, GlowTex.Width, GlowTex.Height), AAColor.ZeroShield, true);
            return false;
        }
    }
}