using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

using Terraria.Audio;
using Terraria.ID;
using AAModClassic.Base.BaseMod.Base;

namespace AAModClassic.NPCs.Bosses.Zero
{
    public class SearcherZero : ModNPC
	{
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
            NPC.lifeMax = 800;
            NPC.defense = 100;
            NPC.damage = 55;
            NPC.HitSound = new LegacySoundStyle(3, 4, Terraria.Audio.SoundType.Sound);
            NPC.DeathSound = new LegacySoundStyle(4, 14, Terraria.Audio.SoundType.Sound);
            NPC.knockBackResist = 0.5f;
			NPC.noGravity = true;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            bool isDead = NPC.life <= 0;
            for (int m = 0; m < (isDead ? 25 : 5); m++)
            {
                int dustType = ModContent.DustType<Dusts.VoidDust>();
                Dust.NewDust(NPC.position, NPC.width, NPC.height, dustType, NPC.velocity.X * 0.2f, NPC.velocity.Y * 0.2f, 100, Color.White, isDead ? 2f : 1.1f);
            }
        }

        float shootAI = 0;
        public override void AI()
        {
            BaseAI.AISkull(NPC, ref NPC.ai, false, 6f, 350f, 0.1f, 0.15f);
            Player player = Main.player[NPC.target];
            bool playerActive = player != null && player.active && !player.dead;
            BaseAI.LookAt(playerActive ? player.Center : (NPC.Center + NPC.velocity), NPC, 0);
            if (Main.netMode != NetmodeID.MultiplayerClient && playerActive)
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
            Texture2D glowTex = Mod.GetTexture("Glowmasks/SearcherZero_Glow");
            if (auraDirection) { auraPercent += 0.1f; auraDirection = auraPercent < 1f; }
            else { auraPercent -= 0.1f; auraDirection = auraPercent <= 0f; }
            BaseDrawing.DrawTexture(spritebatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, dColor);
            BaseDrawing.DrawAura(spritebatch, glowTex, 0, NPC, auraPercent, 1f, 0f, 0f, Color.Red);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, NPC, Color.Red);
            return false;
        }
	}
}