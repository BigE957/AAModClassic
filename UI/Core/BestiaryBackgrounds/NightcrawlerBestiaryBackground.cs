using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.GameContent.Bestiary;
using Terraria.ModLoader;
using Terraria.UI;

namespace AAModClassic.UI.Core.BestiaryBackgrounds
{
    public class NightcrawlerBestiaryBackground : IBestiaryInfoElement, IBestiaryBackgroundImagePathAndColorProvider
    {
        Color? IBestiaryBackgroundImagePathAndColorProvider.GetBackgroundColor() => Color.White;

        Asset<Texture2D> IBestiaryBackgroundImagePathAndColorProvider.GetBackgroundImage() => ModContent.Request<Texture2D>("AAModClassic/UI/Core/BestiaryBackgrounds/NightcrawlerBestiaryBG", AssetRequestMode.ImmediateLoad);

        UIElement IBestiaryInfoElement.ProvideUIElement(BestiaryUICollectionInfo info) => null;
    }
}
