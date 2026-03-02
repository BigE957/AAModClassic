using System;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;


namespace AAModClassic.NPCs.Bosses.Anubis.Forsaken
{
    public class CurseGlyphs : ModProjectile
	{				
		public override void SetStaticDefaults()
		{
            Main.projFrames[Projectile.type] = 9;
		}

        public override void SetDefaults()
        {
            Projectile.tileCollide = false;
            Projectile.width = 16;
            Projectile.height = 16;
            Projectile.aiStyle = -1;
            Projectile.penetrate = -1;
            Projectile.friendly = false;
            Projectile.hostile = true;
            Projectile.ignoreWater = true;
        }

        public int body = -1;
		public float rotValue = -1f;
		public bool spawnedDust = false;

		public override void AI()
        {
            if (body == -1)
            {
                int npcID = BaseAI.GetNPC(Projectile.Center, ModContent.NPCType<ForsakenAnubis>(), 400f, null);
                if (npcID >= 0) body = npcID;
            }
            if (body == -1) return;
            NPC anubis = Main.npc[body];
            if (anubis == null || anubis.life <= 0 || !anubis.active || anubis.type != ModContent.NPCType<ForsakenAnubis>()) { Projectile.active = false; return; }

            Projectile.rotation += .1f;

            int glyph = ((ForsakenAnubis)anubis.ModNPC).RuneCount;

            if (rotValue == -1f) rotValue = Projectile.ai[0] % glyph * ((float)Math.PI * 2f / glyph);
            rotValue += 0.04f;
            while (rotValue > (float)Math.PI * 2f) rotValue -= (float)Math.PI * 2f;

            Projectile.Center = BaseUtility.RotateVector(anubis.Center, anubis.Center + new Vector2(130, 0f), rotValue);

            Projectile.rotation = 0;

            for (int m = Projectile.oldPos.Length - 1; m > 0; m--)
            {
                Projectile.oldPos[m] = Projectile.oldPos[m - 1];
            }
            Projectile.oldPos[0] = Projectile.position;
        }

		public override bool PreDraw(ref Color lightColor)
        {
            Rectangle frame = BaseDrawing.GetFrame(Projectile.frame, TextureAssets.Projectile[Projectile.type].Value.Width, TextureAssets.Projectile[Projectile.type].Value.Height / 9, 0, 0);
            BaseDrawing.DrawAfterimage(sb, TextureAssets.Projectile[Projectile.type].Value, 0, Projectile, 3f, 0.9f, 6, true, 0f, 0f, Color.White, frame, 9);
            return false;
		}		
	}
}