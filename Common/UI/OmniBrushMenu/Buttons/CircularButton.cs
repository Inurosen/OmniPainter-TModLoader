using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OmniPainter.Common.Systems;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace OmniPainter.Common.UI.OmniBrushMenu.Buttons
{
    internal class CircularButton : UIImageButton
    {
        internal string _hoverText;
        private Asset<Texture2D> _texture;
        private readonly Texture2D _btnBg = (Texture2D)ModContent.Request<Texture2D>("Terraria/Images/UI/Wires_0");
        private readonly Texture2D _btnBgHover = (Texture2D)ModContent.Request<Texture2D>("Terraria/Images/UI/Wires_1");
        private readonly Texture2D _btnBgAlt = (Texture2D)ModContent.Request<Texture2D>("Terraria/Images/UI/Wires_8");
        private readonly Texture2D _btnBgHoverAlt = (Texture2D)ModContent.Request<Texture2D>("Terraria/Images/UI/Wires_9");

        private readonly CircularButtonVariant _variant = CircularButtonVariant.Blue;

        public CircularButton(Asset<Texture2D> texture, string hoverText) : base(texture)
        {
            _texture = texture;
            _hoverText = hoverText;
        }

        public CircularButton(Asset<Texture2D> texture, string hoverText, CircularButtonVariant variant) : base(texture)
        {
            _texture = texture;
            _hoverText = hoverText;
            _variant = variant;
        }

        protected override void DrawSelf(SpriteBatch spriteBatch)
        {
            CalculatedStyle dimensions = GetDimensions();
            if (_variant == CircularButtonVariant.Blue)
            {
                DrawBlue(spriteBatch, dimensions);
            }
            else
            {
                DrawRed(spriteBatch, dimensions);
            }
            Rectangle iconRect = new((int)(dimensions.X + dimensions.Width / 8f), (int)(dimensions.Y + dimensions.Height / 8f), (int)(dimensions.Width - dimensions.Width / 4f), (int)(dimensions.Height - dimensions.Height / 4f));
            spriteBatch.Draw(_texture.Value, iconRect, Color.White);

            if (IsMouseHovering)
            {
                Main.hoverItemName = Language.GetTextValue(_hoverText);
            }
        }

        public override void LeftClick(UIMouseEvent evt)
        {
            OmniBrushMenuSystem.GetInstance().Hide();
        }

        private void DrawBlue(SpriteBatch spriteBatch, CalculatedStyle dimensions)
        {
            if (IsMouseHovering)
            {
                spriteBatch.Draw(_btnBgHover, dimensions.ToRectangle(), Color.White);
            }
            else
            {
                spriteBatch.Draw(_btnBg, dimensions.ToRectangle(), Color.White);
            }
        }

        private void DrawRed(SpriteBatch spriteBatch, CalculatedStyle dimensions)
        {
            if (IsMouseHovering)
            {
                spriteBatch.Draw(_btnBgHoverAlt, dimensions.ToRectangle(), Color.White);
            }
            else
            {
                spriteBatch.Draw(_btnBgAlt, dimensions.ToRectangle(), Color.White);
            }
        }
    }

    public enum CircularButtonVariant : int
    {
        Blue,
        Red
    }
}
