using Microsoft.Xna.Framework.Graphics;
using OmniPainter.Common.Systems;
using ReLogic.Content;
using Terraria.ModLoader;
using Terraria.UI;

namespace OmniPainter.Common.UI.OmniBrushMenu.Buttons
{
    [Autoload(Side = ModSide.Client)]
    internal class PaintButton : CircularButton
    {
        private readonly PaintBucket _paint;

        public PaintButton(Asset<Texture2D> texture, string hoverText, CircularButtonVariant variant, PaintBucket paint) : base(texture, hoverText, variant)
        {
            _paint = paint;
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            WorldPaintingSystem.GetInstance().SelectPaintBucket(_paint);
            OmniBrushMenuSystem.GetInstance().OmniBrushMenuState.DeselectAllToolButtons<PaintButton>();
            base.LeftClick(evt);
        }
    }
}
