using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader.Utilities;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities;

namespace AAModClassic._Content.GlowingMushroom.___PreHardmode.NPCs.__BossFeudalFungus
{
    public class FeudalFungusAsleep : ModNPC
    {
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Very Large Glowing Mushroom");
            Main.npcFrameCount[NPC.type] = 1;
        }

        public override void SetDefaults()
        {
            NPC.lifeMax = 200;
            NPC.defense = 0;
            NPC.damage = 0;
            NPC.width = 74;
            NPC.height = 70;
            NPC.aiStyle = -1;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0f;
            NPC.noTileCollide = false;
            NPC.noGravity = false;
            NPC.value = 0;
            NPC.rarity = 1;
        }

        public override bool PreAI()
        {
            NPC.velocity.Y += .1f;
            return false;
        }
        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (spawnInfo.PlayerSafe || NPC.AnyNPCs(ModContent.NPCType<FeudalFungusAsleep>()) || NPC.AnyNPCs(ModContent.NPCType<FeudalFungusWakeUp>()) || NPC.AnyNPCs(ModContent.NPCType<FeudalFungus>()) && !spawnInfo.Player.ZoneGlowshroom)
            {
                return 0f;
            }
            if (spawnInfo.Player.ZoneSurface())
            {
                return SpawnCondition.OverworldMushroom.Chance * 0.001f;
            }
            if (spawnInfo.Player.ZoneDirtLayerHeight)
            {
                return SpawnCondition.UndergroundMushroom.Chance * 0.001f;
            }
            return 0f;
        }

        public override void HitEffect(NPC.HitInfo hit)
        {
            for (int k = 0; k < 3; k++)
            {
                Dust.NewDust(NPC.position, NPC.width, NPC.height, ModContent.DustType<Dusts.MushDust>(), hit.HitDirection, -1f, 0, default, 1f);
            }
            if (Main.netMode != NetmodeID.MultiplayerClient && NPC.CountNPCS(ModContent.NPCType<FeudalFungusWakeUp>()) + NPC.CountNPCS(ModContent.NPCType<FeudalFungus>()) < 1)
            {
                int id = NPC.NewNPC(NPC.GetSource_FromThis(), (int)NPC.Center.X, (int)NPC.Center.Y, ModContent.NPCType<FeudalFungusWakeUp>());
                Main.npc[id].position = NPC.position;
            }
            NPC.active = false;
            NPC.life = 0;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D glowTex = Mod.GetTexture("Glowmasks/FungusSlep_Glow");
            BaseDrawing.DrawTexture(spriteBatch, TextureAssets.Npc[NPC.type].Value, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, NPC.frame, NPC.GetAlpha(drawColor), true);
            BaseDrawing.DrawTexture(spriteBatch, glowTex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, 0, 1, NPC.frame, AAColor.Glow, true);
            return false;
        }
    }
}
