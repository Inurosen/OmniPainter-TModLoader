using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OmniPainter.Common.Systems;
using Terraria;
using Terraria.ModLoader;
using Terraria.UI;

namespace OmniPainter.Common.UI.TileSelectionRectangleOverlay
{
    [Autoload(Side = ModSide.Client)]
    public class TileSelectionRectangleOverlay : UIElement
    {
        Color color = new(60, 60, 60);

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 start = WorldPaintingSystem.GetInstance().GetScreenPositionStart();
            Vector2 end = WorldPaintingSystem.GetInstance().GetScreenPositionEnd();

            int rectX = (int)start.X;
            int rectY = (int)start.Y;
            int rectW = (int)(end.X - start.X);
            int rectH = (int)(end.Y - start.Y);

            Rectangle rect = new Rectangle(
                rectW < 0 ? rectX + rectW : rectX,
                rectH < 0 ? rectY + rectH : rectY,
                rectW < 0 ? -rectW : rectW,
                rectH < 0 ? -rectH : rectH
            );

            spriteBatch.Draw(CreateTexture(), rect, color);
        }

        private static Texture2D CreateTexture()
        {
            Texture2D texture = new Texture2D(Main.graphics.GraphicsDevice, 1, 1);
            texture.SetData<Color>(new Color[] { new Color(255, 255, 200, 50) });

            return texture;
        }
    }
}
