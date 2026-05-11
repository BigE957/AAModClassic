using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.Items.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu;
using AAModClassic._Unreleased.Content.SunkenShip._PostMoonLord.NPCs.SoulOfCthulhu._Cthulhu;
using AAModClassic.Base.BaseMod.Base;
using Microsoft.Xna.Framework;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace AAModClassic._Unreleased
{
    public class AAPlayer_Unreleased : ModPlayer
    {
        public bool ZoneStorm = false;
        public bool ZoneShip = false;
        public int CthulhuCountdown = 10800;
        public bool Leave = false;
        public bool Compass = false;

        public override void Initialize()
        {
            ZoneStorm = false;
            ZoneShip = false;
        }

        public bool CustomBiomesMatch(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>();
            return ZoneStorm == modOther.ZoneStorm && ZoneShip == modOther.ZoneShip;
        }

        public void CopyCustomBiomesTo(Player other)
        {
            AAPlayer modOther = other.GetModPlayer<AAPlayer>();
            modOther.ZoneStorm = ZoneStorm;
            modOther.ZoneShip = ZoneShip;
        }

        public void SendCustomBiomes(BinaryWriter bb)
        {
            bb.WriteFlags(ZoneStorm, ZoneShip);
        }

        public void ReceiveCustomBiomes(BinaryReader bb)
        {
            bb.ReadFlags(out ZoneStorm, out ZoneShip);
        }

        public override void ResetEffects()
        {
            Compass = false;
        }

        public override void PostUpdate()
        {
            if (!AAWorld_Unreleased.Compass && !Compass)
            {
                if (Player.inventory.Any(i => i.type == ModContent.ItemType<CursedCompass>() && i.stack > 0))
                {
                    AAWorld_Unreleased.Compass = true;
                    Compass = true;
                    Leave = false;
                    if (ZoneShip)
                    {
                        Vector2 spawnPos = Player.Center + (Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2)) * 800);
                        int n = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<UDUNFUKED>());
                        Main.npc[n].target = Player.whoAmI;
                        if (Main.netMode != NetmodeID.MultiplayerClient)
                        {            
                            BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.CompassSteal"), Color.Cyan);
                        }
                    }
                }
            }
            if (ZoneShip && !Leave)
            {
                CthulhuCountdown--;
                if (CthulhuCountdown == 9500 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.1"), Color.Blue);
                }
                if (CthulhuCountdown == 7050 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.2"), Color.DarkCyan);
                }
                if (CthulhuCountdown == 5050 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.3"), Color.Cyan);
                }
                if (CthulhuCountdown == 3000 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.4"), Color.Cyan);
                }
                if (CthulhuCountdown == 1200 && Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.5"), Color.DarkCyan);
                }
                if (CthulhuCountdown == 0)
                {
                    Leave = false;
                    Vector2 spawnPos = Player.Center + (Vector2.UnitY.RotatedBy(Main.rand.NextFloat(-MathHelper.PiOver2, MathHelper.PiOver2)) * 800);
                    int n = NPC.NewNPC(NPC.GetSource_NaturalSpawn(), (int)spawnPos.X, (int)spawnPos.Y, ModContent.NPCType<UDUNFUKED>());
                    Main.npc[n].target = Player.whoAmI;
                    if (Main.netMode != NetmodeID.MultiplayerClient)
                    {
                        BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.6"), Color.Cyan);
                    }
                }
            }
            if (!ZoneShip || NPC.AnyNPCs(ModContent.NPCType<UDUNFUKED>()) || NPC.AnyNPCs(ModContent.NPCType<SoulOfCthulhu>()) || NPC.AnyNPCs(ModContent.NPCType<CthulhuPortal>()) || NPC.AnyNPCs(ModContent.NPCType<Cthulhu>()))
            {
                CthulhuCountdown = 10800;
            }
            if (!ZoneShip && Leave == true)
            {
                Leave = false;
                if (Main.netMode != NetmodeID.MultiplayerClient)
                {
                    BaseUtility.Chat(Language.GetTextValue("Mods.AAModClassic.NPCs.BossDialogue.SoulOfCthulhu.PreFight.SunkenShipWarning.Escape"), Color.DarkCyan);
                }
            }
        }
    }
}
