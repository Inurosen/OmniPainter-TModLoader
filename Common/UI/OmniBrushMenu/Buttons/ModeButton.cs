using Microsoft.Xna.Framework.Graphics;
using OmniPainter.Common.Systems;
using ReLogic.Content;
using Terraria.UI;

namespace OmniPainter.Common.UI.OmniBrushMenu.Buttons
{
    internal class ModeButton : CircularButton
    {
        private readonly Mode _mode;

        public ModeButton(Asset<Texture2D> texture, string hoverText, CircularButtonVariant variant, Mode mode) : base(texture, hoverText, variant)
        {
            _mode = mode;
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            WorldPaintingSystem.GetInstance().SelectMode(_mode);
            OmniBrushMenuSystem.GetInstance().OmniBrushMenuState.DeselectAllToolButtons<ModeButton>();
            base.LeftClick(evt);
        }
    }
}
