using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.ModLoader;

namespace OmniPainter.Common.UI.OmniBrushMenu
{
    [Autoload(Side = ModSide.Client)]
    public class OmniBrushMenuPanel : UIPanel
    {
        private bool isActive;
        public override void OnActivate()
        {
            base.OnActivate();
            isActive = true;
        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            if (!isActive)
            {
                return;
            }

            Left.Set(Main.mouseX - Width.Pixels / 2f, 0f);
            Top.Set(Main.mouseY - Height.Pixels / 2f, 0f);
            Recalculate();
            isActive = false;
        }

        protected override void DrawChildren(SpriteBatch spriteBatch)
        {
            if(!isActive)
            {
                base.DrawChildren(spriteBatch);
            }
        }
    }
}
