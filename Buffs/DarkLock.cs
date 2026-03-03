using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Buffs
{
    public class DarkLock : ModBuff
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Dark Lock");
            // Description.SetDefault("You're locked in place by darkness!");
            Main.debuff[Type] = true;
            BuffID.Sets.LongerExpertDebuff[Type] = true;
        }
    }
    public class DarkLockEffect : GlobalNPC
    {
        public override bool PreAI(NPC npc)
        {
            if (npc.HasBuff(Mod.Find<ModBuff>("DarkLock").Type))
            {
                npc.velocity = Vector2.Zero;
                return false;
            }
            return base.PreAI(npc);
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (npc.HasBuff(Mod.Find<ModBuff>("DarkLock").Type) && npc.active)
            {
                Texture2D DarkX = new Texture2D(Main.graphics.GraphicsDevice, npc.width, npc.height);
                Color[] dataColors = new Color[npc.width * npc.height];
                float ratio = npc.height / (float)npc.width;
                for (int x = 0; x < npc.width; x++)
                {
                    for (int y = 0; y < npc.height; y++)
                    {
                        int i = x + y * npc.width;
                        if (Math.Abs(x* ratio - y) < 5 || Math.Abs((npc.width - x) * ratio - y) < 5)
                        {
                            dataColors[i] = Main.rand.Next(10) == 0 ? new Color(0, 255, 181) : new Color(9, 0, 44);
                        }
                    }
                }
                DarkX.SetData(0, null, dataColors, 0, npc.width * npc.height);
                spriteBatch.Draw(DarkX, npc.Center - Main.screenPosition, null, Color.White, 0, DarkX.Size() * .5f, npc.scale, SpriteEffects.None, 0f);
            }
            
        }
    }

}
