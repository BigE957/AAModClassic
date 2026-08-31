using AAModClassic._Content.Chaos._PostMoonlord.NPCs.__BossSistersOfDiscord.Haruka;
using AAModClassic._Content.Mire.___PreHardmode.NPCs.__BossHydra;
using AAModClassic._CrossMod;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Mire.___PreHardmode.NPCs
{ 
    public class HarukaShadow : ModNPC
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("...");
            Main.npcFrameCount[NPC.type] = 3;
            this.HideFromBestiary();
        }

        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.defense = 1;
            NPC.knockBackResist = 0f;
            NPC.noGravity = false;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.lifeMax = 1;
            NPC.damage = 0;
            NPC.value = 0;
            NPC.alpha = 50;
            NPC.width = 38;
            NPC.height = 58;
            NPC.rarity = 1;
            NPC.immortal = true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            if (!AAWorld.downedSisters && NPCExtensions.BeenKilled<HydraBody>() && spawnInfo.Player.ZoneAnyMire() && !NPCUtils.AnyEvents(spawnInfo.Player) && !NPC.AnyNPCs(ModContent.NPCType<HarukaShadow>()))
                return ContentReplacementSystem.NeedToReplaceContent ? 0.0005f : .00005f;

            return 0f;
        }

        public override void AI()
        {
            if (Collision.CanHit(NPC.position, NPC.width, NPC.height, Main.player[NPC.target].position, Main.player[NPC.target].width, Main.player[NPC.target].height))
            {
                NPC.ai[0] = 1;
            }
            if (NPC.ai[0] == 1)
            {
                NPC.dontTakeDamage = true;
                if (NPC.ai[1] < 255)
                {
                    NPC.alpha += 4;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        NPC.ai[1] += 4;
                    }
                }
                else
                {
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        Main.BestiaryTracker.Sights.RegisterWasNearby(ContentSamples.NpcsByNetId[ModContent.NPCType<Haruka>()]);
                        NPC.active = false;
                        NPC.netUpdate = true;
                    }
                }
            }
        }

        public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
        {
            modifiers.DisableKnockback();
            modifiers.TargetDamageMultiplier *= 0;
            modifiers.DisableCrit();
            if (NPC.ai[0] != 1)
            {
                NPC.ai[0] = 1;
                CombatText.NewText(NPC.Hitbox, new Color(72, 78, 117), Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SistersOfDiscord.Ambient.HarukaShadow"));
            }
        }

        public override void FindFrame(int frameHeight)
        {
            if (NPC.ai[0] == 0)
            {
                NPC.frame.Y = frameHeight;
            }
            else
            {
                NPC.frame.Y = frameHeight * 2;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            Texture2D tex = TextureAssets.Npc[NPC.type].Value;
            Texture2D tex2 = ModContent.Request<Texture2D>(ModContent.GetInstance<HarukaShadowPostHydra>().Texture + "_Glow").Value;
            BaseDrawing.DrawTexture(spriteBatch, tex, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 3, NPC.frame, NPC.GetAlpha(drawColor));
            if (NPC.ai[0] == 0)
            {
                Lighting.AddLight(NPC.Center, Color.MediumVioletRed.R / 180, Color.MediumVioletRed.G / 180, Color.MediumVioletRed.B / 180);
                BaseDrawing.DrawTexture(spriteBatch, tex2, 0, NPC.position, NPC.width, NPC.height, NPC.scale, NPC.rotation, NPC.direction, 3, NPC.frame, Color.White);
            }
            return false;
        }
    }
}