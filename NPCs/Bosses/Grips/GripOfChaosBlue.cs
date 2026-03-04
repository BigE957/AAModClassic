using AAModClassic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.NPCs.Bosses.Grips
{
    [AutoloadBossHead]
    public class GripOfChaosBlue : BaseGripOfChaos
    {
        public override void SetDefaults()
        {
			base.SetDefaults();
			NPC.lifeMax = 1400;
            NPC.damage = 30;
            NPC.defense = 10;		
            NPC.buffImmune[BuffID.Poisoned] = true;	

			offsetBasePoint = new Vector2(240f, 0f);
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            if (NPC.life <= 0) //this make so when the npc has 0 life(dead) he will spawn this
            {
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore1").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore2").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore3").Type, 1f);
                Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, Mod.Find<ModGore>("MireGripGore4").Type, 1f);
            }
        }

        public override void OnKill()
        {
            int redGripExists = NPC.CountNPCS(Mod.Find<ModNPC>("GripOfChaosRed").Type);
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("GripTrophyBlue").Type);
            }
            if (redGripExists == 0)
            {
                if (Main.rand.Next(4) == 0 && !Main.expertMode)
                {
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("ClawBaton").Type);
                }

                AAWorld.downedGrips = true;
                if (Main.expertMode)
                {
                    NPC.DropLoot(Mod.Find<ModItem>("GripBag").Type);
                }
            }
            else
            {
                if (Main.rand.Next(10) == 0)
                {
                    Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("GripMaskBlue").Type);
                }
            }
            if (!Main.expertMode)
            {
                Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, Mod.Find<ModItem>("Abyssium").Type, Main.rand.Next(30, 44));
            }
        }

        public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
        {
            if (Main.rand.Next(2) == 0 || (Main.expertMode && Main.rand.Next(0) == 0))       //Chances for it to inflict the debuff
            {
                target.AddBuff(BuffID.Poisoned, Main.rand.Next(180, 250));       //Main.rand.Next part is the length of the buff, so 8.3 seconds to 16.6 seconds
            }
        }		
    }
}
