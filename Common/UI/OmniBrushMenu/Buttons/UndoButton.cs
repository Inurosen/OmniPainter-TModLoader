using Microsoft.Xna.Framework.Graphics;
using OmniPainter.Common.Systems;
using ReLogic.Content;
using Terraria.UI;

namespace OmniPainter.Common.UI.OmniBrushMenu.Buttons
{
    internal class UndoButton : CircularButton
    {
        public UndoButton(Asset<Texture2D> texture, string hoverText, CircularButtonVariant variant) : base(texture, hoverText, variant)
        {
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            if(!_disabled)
            {
                WorldPaintingSystem.GetInstance().Undo();
                base.LeftClick(evt);
                SetSelected(false);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (WorldPaintingSystem.GetInstance().CanUndo())
            {
                _disabled = false;
            }
            else
            {
                _disabled = true;
            }
            base.Draw(spriteBatch);
        }
    }
}
