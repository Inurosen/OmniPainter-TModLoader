using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace OmniPainter.Common.UI.OmniBrushMenu.Buttons
{
    [Autoload(Side = ModSide.Client)]
    internal class CircularButton : UIImageButton
    {
        internal string _hoverText;
        internal bool _selected = false;
        internal bool _disabled = false;
        private Asset<Texture2D> _texture;
        private readonly Texture2D _btnBg = (Texture2D)ModContent.Request<Texture2D>("Terraria/Images/UI/Wires_0", AssetRequestMode.ImmediateLoad);
        private readonly Texture2D _btnBgHover = (Texture2D)ModContent.Request<Texture2D>("Terraria/Images/UI/Wires_1", AssetRequestMode.ImmediateLoad);
        private readonly Texture2D _btnBgAlt = (Texture2D)ModContent.Request<Texture2D>("Terraria/Images/UI/Wires_8", AssetRequestMode.ImmediateLoad);
        private readonly Texture2D _btnBgHoverAlt = (Texture2D)ModContent.Request<Texture2D>("Terraria/Images/UI/Wires_9", AssetRequestMode.ImmediateLoad);
        private readonly Texture2D _btnBgSelected = (Texture2D)ModContent.Request<Texture2D>("OmniPainter/Common/UI/OmniBrushMenu/Buttons/Wires_1_Green", AssetRequestMode.ImmediateLoad);
        private readonly Texture2D _btnBgSelectedAlt = (Texture2D)ModContent.Request<Texture2D>("OmniPainter/Common/UI/OmniBrushMenu/Buttons/Wires_9_Green", AssetRequestMode.ImmediateLoad);
        private readonly Texture2D _btnBgDisabled = (Texture2D)ModContent.Request<Texture2D>("OmniPainter/Common/UI/OmniBrushMenu/Buttons/Wires_0_Inactive", AssetRequestMode.ImmediateLoad);


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
            if(!_disabled)
            {
                SetSelected(true);
                OmniBrushMenuSystem.GetInstance().Hide();
            }
        }

        internal void SetSelected(bool selected)
        {
            _selected = selected;
        }

        internal void SetDisabled(bool disabled)
        {
            _disabled = disabled;
        }

        private void DrawBlue(SpriteBatch spriteBatch, CalculatedStyle dimensions)
        {
            if (_disabled)
            {
                spriteBatch.Draw(_btnBgDisabled, dimensions.ToRectangle(), Color.White);
                return;
            }

            if (IsMouseHovering)
            {
                spriteBatch.Draw(_btnBgHover, dimensions.ToRectangle(), Color.White);
            }
            else
            {
                if (!_selected)
                {
                    spriteBatch.Draw(_btnBg, dimensions.ToRectangle(), Color.White);
                }
                else
                {
                    spriteBatch.Draw(_btnBgSelected, dimensions.ToRectangle(), Color.White);
                }
            }
        }

        private void DrawRed(SpriteBatch spriteBatch, CalculatedStyle dimensions)
        {
            if (_disabled)
            {
                spriteBatch.Draw(_btnBgDisabled, dimensions.ToRectangle(), Color.White);
                return;
            }

            if (IsMouseHovering)
            {
                spriteBatch.Draw(_btnBgHoverAlt, dimensions.ToRectangle(), Color.White);
            }
            else
            {
                if (!_selected)
                {
                    spriteBatch.Draw(_btnBgAlt, dimensions.ToRectangle(), Color.White);
                }
                else
                {
                    spriteBatch.Draw(_btnBgSelectedAlt, dimensions.ToRectangle(), Color.White);
                }
            }
        }
    }

    public enum CircularButtonVariant : int
    {
        Blue,
        Red
    }
}
