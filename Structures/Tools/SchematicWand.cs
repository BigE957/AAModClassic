#if DEBUG
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace AAModClassic.Structures.Tools
{
    public class SchematicWand : ModItem
    {
        public override string Texture => "Terraria/Images/Item_" + ItemID.MulticolorWrench;

        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.useStyle = ItemUseStyleID.Swing;
            Item.useTime = 15;
            Item.useAnimation = 15;
            Item.autoReuse = false;
            Item.noMelee = true;
            Item.rare = ItemRarityID.Purple;
        }

        public override bool AltFunctionUse(Player player) => true;

        public override bool CanUseItem(Player player) => Main.netMode == NetmodeID.SinglePlayer;

        public override bool? UseItem(Player player)
        {
            if (player.whoAmI != Main.myPlayer)
                return true;

            SchematicEditSession session = SchematicToolSystem.Session;
            if (session == null)
                return true;

            if (player.altFunctionUse == 2)
            {
                if (session.Phase == SchematicToolPhase.AwaitingSecondCorner)
                {
                    session.Cancel();
                    Main.NewText("Schematic selection cancelled.", Color.Orange);
                }
                return true;
            }

            Point tile = Main.MouseWorld.ToTileCoordinates();

            switch (session.Phase)
            {
                case SchematicToolPhase.Idle:
                    session.BeginSelection(tile);
                    Main.NewText($"Corner 1 set at ({session.FirstCorner.X}, {session.FirstCorner.Y}). Click the opposite corner.", Color.LightGreen);
                    break;

                case SchematicToolPhase.AwaitingSecondCorner:
                    session.CompleteSelection(tile);
                    Main.NewText($"Region set: {session.Region.Width}x{session.Region.Height} at ({session.Region.X}, {session.Region.Y}). Use /schem for commands.", Color.LightGreen);
                    break;
            }

            return true;
        }
    }
}
#endif