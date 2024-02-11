using Microsoft.Xna.Framework.Graphics;
using OmniPainter.Common.Systems;
using ReLogic.Content;
using Terraria.UI;

namespace OmniPainter.Common.UI.OmniBrushMenu.Buttons
{
    internal class PaintingToolBtn : CircularButton
    {
        private readonly BrushTool _tool;

        public PaintingToolBtn(Asset<Texture2D> texture, string hoverText, CircularButtonVariant variant, BrushTool tool) : base(texture, hoverText, variant)
        {
            _tool = tool;
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            WorldPaintingSystem.GetInstance().SelectTool(_tool);
            OmniBrushMenuSystem.GetInstance().OmniBrushMenuState.DeselectAllToolButtons<PaintingToolBtn>();
            base.LeftClick(evt);
        }
    }
}
