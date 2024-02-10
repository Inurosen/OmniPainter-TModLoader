using Microsoft.Xna.Framework.Graphics;
using OmniPainter.Common.Systems;
using ReLogic.Content;
using Terraria.UI;

namespace OmniPainter.Common.UI.OmniBrushMenu.Buttons
{
    internal class PaintingModeBtn : CircularButton
    {
        private readonly BrushMode _mode;

        public PaintingModeBtn(Asset<Texture2D> texture, string hoverText, CircularButtonVariant variant, BrushMode mode) : base(texture, hoverText, variant)
        {
            _mode = mode;
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            WorldPaintingSystem.GetInstance().SelectMode(_mode);
            base.LeftClick(evt);
        }
    }
}
