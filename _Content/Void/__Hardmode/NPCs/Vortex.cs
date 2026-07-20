using AAModClassic._Content.Mire._PostMoonlord.Items.Materials;
using AAModClassic._Content.Void.World.Biomes;
using AAModClassic._Unofficial.Content.Void.__Hardmode.Items.Tools;
using AAModClassic.Base.BaseMod.Base;
using AAModClassic.Globals;
using AAModClassic.Utilities.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;
using static AAModClassic.Utilities.ItemDropRuleConditionUtils;


namespace AAModClassic._Content.Void.__Hardmode.NPCs
{
    public class Vortex : ModNPC, IBannerNPC
    {
        public static Asset<Texture2D> Glowmask;
        public static Asset<Texture2D> Blades;
        public static Asset<Texture2D> BladesGlowmask;

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Vortex");

            Glowmask = ModContent.Request<Texture2D>(Texture + "_Glow");
            Blades = ModContent.Request<Texture2D>(Texture + "_Blades");
            BladesGlowmask = ModContent.Request<Texture2D>(Texture + "_Blades_Glow");
        }

        public override void SetDefaults()
        {
            NPC.npcSlots = 100;
            NPC.width = 84;
            NPC.height = 84;
            NPC.aiStyle = -1;
            NPC.damage = 40;
            NPC.defense = 40;
            NPC.lifeMax = 1000;
            NPC.value = Item.buyPrice(0, 0, 50, 0);
            NPC.HitSound = SoundID.NPCHit4;
            NPC.DeathSound = SoundID.NPCDeath14;
            NPC.knockBackResist = 0f;
            NPC.noGravity = true;
            NPC.netAlways = true;
            //Banner = NPC.type;
			//BannerItem = ModContent.ItemType<AAModClassic.Items.Banners.VortexBanner>();
            SpawnModBiomes = [ModContent.GetInstance<VoidBiome>().Type];
        }

        public float Rotation = 0;

        /*
        public override void OnKill()
        {
            Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<VoidEnergy>(), Main.rand.Next(1, 4));
        }
        */
        public override void AI()
        {
            BaseAI.AIElemental(NPC, ref NPC.ai, null, 1, false, false, 800f, 600f, 180, 3f);

            if (NPC.velocity.X > 0)
            {
                Rotation += .03f;
            }
            else if (NPC.velocity.X < 0)
            {
                Rotation -= .03f;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            spriteBatch.Draw(Blades.Value, NPC.Center - screenPos, Blades.Frame(), drawColor, Rotation, Blades.Value.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(TextureAssets.Npc[NPC.type].Value, NPC.Center - screenPos, TextureAssets.Npc[NPC.type].Frame(), drawColor, NPC.rotation, TextureAssets.Npc[NPC.type].Value.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(BladesGlowmask.Value, NPC.Center - screenPos, BladesGlowmask.Frame(), AAColor.ZeroShield, Rotation, BladesGlowmask.Value.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            spriteBatch.Draw(Glowmask.Value, NPC.Center - screenPos, Glowmask.Frame(), AAColor.ZeroShield, NPC.rotation, Glowmask.Value.Size() * 0.5f, NPC.scale, SpriteEffects.None, 0);
            return false;
        }

        public override void ModifyNPCLoot(NPCLoot npcLoot)
        {
            LeadingConditionRule unofficialRule = new(new Unofficial());

            unofficialRule.OnSuccess(ItemDropRule.Common(ModContent.ItemType<GravitronCentrifuge>(), 10));

            npcLoot.Add(unofficialRule);
        }
    }
}