using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic._Content._Dev._PostMoonlord.Items.Weapons
{
    public class Doragonburedo : BaseAAItem
    {
        public static short customGlowMask = 0;
        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Doragonburedo");
            // Tooltip.SetDefault("'I'm gonna wipe their whole team' \n" + "-Jace");
        }

        public override void SetDefaults()
        {
			Item.CloneDefaults(ItemID.Arkhalis);
            Item.glowMask = customGlowMask;
            Item.damage = 220;            //Sword damage
            Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;            //if it's melee
            Item.width = 56;              //Sword width
            Item.height = 56;             //Sword height
            Item.expert = true;
            Item.useTime = 6;
            Item.useAnimation = 6;
            Item.knockBack = 6;      //Sword knockback
            Item.value = 100000;        
            Item.rare = ItemRarityID.Lime;
            Item.autoReuse = true;   //if it's capable of autoswing.
            Item.useTurn = true;
            Item.shoot = ModContent.ProjectileType<Doragonburedo_Ryugen>();
        }
    }
}
