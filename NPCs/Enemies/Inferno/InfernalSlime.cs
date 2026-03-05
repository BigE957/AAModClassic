using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Enemies.Inferno
{
    // Party Zombie is a pretty basic clone of a vanilla NPC. To learn how to further adapt vanilla NPC behaviors, see https://github.com/blushiemagic/tModLoader/wiki/Advanced-Vanilla-Code-Adaption#example-npc-npc-clone-with-modified-projectile-hoplite
    public class InfernalSlime : ModNPC
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Infernal Slime");
			Main.npcFrameCount[NPC.type] = Main.npcFrameCount[NPCID.BlueSlime];
		}

		public override void SetDefaults()
		{
            NPC.aiStyle = NPCAIStyleID.Slime;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.width = 32;
			NPC.height = 26;
			NPC.damage = 14;
			NPC.defense = 2;
			NPC.lifeMax = 20;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.value = 60f;
            NPC.lavaImmune = true;
            NPC.knockBackResist = 0.5f;
            AnimationType = NPCID.CorruptSlime;
            NPC.buffImmune[BuffID.OnFire] = true;
            Banner = NPC.type;
			BannerItem = Mod.Find<ModItem>("InfernalSlimeBanner").Type;
        }

        public override bool PreAI()
        {
            Lighting.AddLight(NPC.Center, Color.DarkOrange.R / 255, Color.DarkOrange.G / 255, Color.DarkOrange.B / 255);
            return true;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            SpriteEffects spriteEffects = SpriteEffects.None;
            if (NPC.spriteDirection == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(Mod.GetTexture("Glowmasks/InfernalSlime_Glow"), new Vector2(NPC.Center.X - Main.screenPosition.X, NPC.Center.Y - Main.screenPosition.Y),
            NPC.frame, Color.White, NPC.rotation,
            new Vector2(NPC.width * 0.5f, NPC.height * 0.5f), 1f, spriteEffects, 0f);
        }

        public override void HitEffect(NPC.HitInfo hit)
		{
			for (int i = 0; i < 10; i++)
			{
				int dustType = Main.rand.Next(139, 143);
				int dustIndex = Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.IncineriteDust>(), 0f, 0f, 200, default, 0.8f);
                Main.dust[dustIndex].velocity *= 0.3f;
			}
		}
		
		public override void OnKill()
		{
			Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("BurningGel").Type, Main.rand.Next(5,15));
        }
	}
}
