
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace AAMod.NPCs.Enemies.Void
{
    public class Searcher : ModNPC
	{
		public int timer = 0;
		public bool start = true;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Searcher");
        }

        public override void SetDefaults()
        {
            NPC.width = 35;
            NPC.height = 35;
            NPC.value = BaseUtility.CalcValue(0, 0, 5, 50);
            NPC.npcSlots = 1;
            NPC.aiStyle = -1;
            NPC.lifeMax = 250;
            NPC.defense = 30;
            NPC.damage = 65;
            NPC.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            NPC.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            NPC.knockBackResist = 0.5f;
            NPC.noGravity = true;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("SearcherBanner").Type;

        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool isDead = NPC.life <= 0;
            for (int m = 0; m < (isDead ? 25 : 5); m++)
            {
                int dustType = ModContent.DustType<Dusts.VoidDust>();
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, isDead ? 2f : 1.1f);
            }

            if (NPC.life <= 0)
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/SearcherGore1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/SearcherGore2"), 1f);
            }
        }

        float shootAI = 0;
        public override void AI()
        {
            BaseAI.AISkull(NPC, ref NPC.ai, true, 6f, 350f, 0.1f, 0.15f);
            Player player = Main.player[NPC.target];
            bool playerActive = player != null && player.active && !player.dead;
            BaseAI.LookAt(playerActive ? player.Center : (NPC.Center + NPC.velocity), NPC, 0);
            if (Main.netMode != 1 && playerActive)
            {
                shootAI++;
                if (shootAI >= 90)
                {
                    shootAI = 0;
                    int projType = Mod.ProjType("DeathLaser");
                    if (Collision.CanHit(NPC.position, NPC.width, NPC.height, player.position, player.width, player.height))
                        BaseAI.FireProjectile(player.Center, NPC, projType, (int)(NPC.damage * 0.25f), 0f, 2f);
                }
            }
        }
        public float auraPercent = 0f;
        public bool auraDirection = true;

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Mod.GetTexture("Glowmasks/Searcher_Glow");
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, NPC, Color.Red);
            return false;
        }

        public override void OnKill()
        {
            Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Apocalyptite").Type);
        }
    }
}