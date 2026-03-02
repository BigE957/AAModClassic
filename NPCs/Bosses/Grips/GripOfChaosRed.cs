using AAModClassic;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Grips
{
    [AutoloadBossHead]
    public class GripOfChaosRed : BaseGripOfChaos
    {
        public override void SetDefaults()
        {
			base.SetDefaults();
			NPC.lifeMax = 1600;
            NPC.damage = 32;
            NPC.defense = 15;	
            NPC.buffImmune[BuffID.OnFire] = true;			

			offsetBasePoint = new Vector2(-240f, 0f);			
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0) //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/InfernoGripGore1"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/InfernoGripGore2"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/InfernoGripGore3"), 1f);
                Gore.NewGore(NPC.position, NPC.velocity, Mod.GetGoreSlot("Gores/InfernoGripGore4"), 1f);
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Mod.GetTexture("Glowmasks/GripOfChaosRed_Glow");
            BaseDrawing.DrawTexture(spritebatch, TextureAssets.Npc[NPC.type].Value, 0, NPC, dColor);
            BaseDrawing.DrawTexture(spritebatch, glowTex, 0, NPC, Color.White);
            return false;
        }

        public override void OnKill()
        {
            int blueGripExists = NPC.CountNPCS(Mod.Find<ModNPC>("GripOfChaosBlue").Type);
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("GripTrophyRed").Type);
            }
            if (blueGripExists == 0)
            {
                if (Main.rand.Next(4) == 0 && !Main.expertMode)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("ClawBaton").Type);
                }
                AAWorld.downedGrips = true;
                if (Main.expertMode)
                {
                    NPC.DropBossBags();
                }
            }
            else
            {
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("GripMaskRed").Type);
                }
            }
            if (!Main.expertMode)
            {
                Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Incinerite").Type, Main.rand.Next(30, 44));
            }
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))
            {
                target.AddBuff(BuffID.OnFire, Main.rand.Next(180, 250));
            }
        }
    }
}
