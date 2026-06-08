using AAModClassic._Content.Stars._PostMoonlord.Items.Materials;
using AAModClassic.Globals;
using AAModClassic.Tiles.Crafters;
using AAModClassic.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace AAModClassic._Content.Stars._PostMoonlord.Items.Armor
{
    public class StarHelmetMeleePlayer : ModPlayer
    {
        public int ShieldTime = 0;
        public int ShieldCoolDown = 0;
        public float yetAnotherTrigCounter = 0;
        public bool badShield = false;

        public override void ResetEffects()
        {
            if (ShieldTime > 0)
            {
                ShieldTime--;
            }
        }
        public override void PreUpdate()
        {
            yetAnotherTrigCounter += (float)Math.PI / 60;

            if (ShieldCoolDown > 0)
            {
                ShieldCoolDown--;
            }
            else
            {
                ShieldCoolDown = 0;
            }
        }
        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (ShieldTime > 0)
            {
                if (badShield)
                {
                    modifiers.IncomingDamageMultiplier *= 1.4f;
                }
                else
                {
                    modifiers.IncomingDamageMultiplier *= 0.6f;
                    ShieldCoolDown = 1800;
                }

            }
        }
    }

    public class StarHelmetMeleePlayer_StarShieldDrawLayer : PlayerDrawLayer// = new PlayerLayer("AAMod", "drawShield", PlayerLayer.MiscEffectsFront, delegate (PlayerDrawSet drawInfo)
    {
        public static Asset<Texture2D> DarkmatterShieldTex;
        public static Asset<Texture2D> RadiumShieldTex;

        public override void SetStaticDefaults()
        {
            DarkmatterShieldTex = ModContent.Request<Texture2D>(FilePathUtils.TexturePath<StarHelmetMeleePlayer>() + "_DarkmatterShield");
            RadiumShieldTex = ModContent.Request<Texture2D>(FilePathUtils.TexturePath<StarHelmetMeleePlayer>() + "_RadiumShield");
        }

        public override Position GetDefaultPosition() => new AfterParent(PlayerDrawLayers.ElectrifiedDebuffFront);

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player drawPlayer = drawInfo.drawPlayer;
            Mod mod = AAMod.instance;
            Texture2D texture = DarkmatterShieldTex.Value;
            if (drawPlayer.GetModPlayer<StarHelmetMeleePlayer>().badShield)
            {
                texture = RadiumShieldTex.Value;
            }
            if (drawPlayer.GetModPlayer<StarHelmetMeleePlayer>().ShieldTime > 0)
            {
                Vector2 Center = drawInfo.Position + new Vector2(drawPlayer.width / 2, 0) + Vector2.UnitY * -30 - Main.screenPosition;

                DrawData data = new DrawData(texture, Center, null, Color.White, 0f, texture.Size() * .5f, 1f + .1f * (float)Math.Sin(drawPlayer.GetModPlayer<StarHelmetMeleePlayer>().yetAnotherTrigCounter), SpriteEffects.None, 0);
                data.shader = drawInfo.cBody;
                drawInfo.DrawDataCache.Add(data);
            }
        }
    }

    public class StarHelmetMeleePlayer_RadiumWeaken : GlobalNPC
    {
        public override bool InstancePerEntity
        {
            get
            {
                return true;
            }
        }
        public int BrokenShield = 0;
        public override void ResetEffects(NPC npc)
        {
            if (BrokenShield > 0)
            {
                BrokenShield--;
            }
        }
        public float yetAnotherTrigCounter = 0;
        public override void AI(NPC npc)
        {
            yetAnotherTrigCounter += (float)Math.PI / 60;
        }
        public override void ModifyHitByItem(NPC npc, Player player, Item item, ref NPC.HitModifiers modifiers)
        {
            if (BrokenShield > 0)
            {
                modifiers.TargetDamageMultiplier *= 1.4f;
            }
        }
        public override void ModifyHitByProjectile(NPC npc, Projectile projectile, ref NPC.HitModifiers modifiers)
        {
            if (BrokenShield > 0)
            {
                modifiers.TargetDamageMultiplier *= 1.4f;
            }
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (BrokenShield > 0)
            {
                Texture2D texture = StarHelmetMeleePlayer_StarShieldDrawLayer.RadiumShieldTex.Value;
                spriteBatch.Draw(texture, npc.Top + Vector2.UnitY * -30 - Main.screenPosition, null, Color.White, 0f, texture.Size() * .5f, 1f + .1f * (float)Math.Sin(yetAnotherTrigCounter), SpriteEffects.None, 0f);
            }

        }
    }
}