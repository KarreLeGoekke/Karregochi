using System.Numerics;
using Raylib_cs;

static class Aleph{
    public static void DrawTextCenter(string text, Vector2 pos, int fontSize, Color color){
        Vector2 textSize = Raylib.MeasureTextEx(Raylib.GetFontDefault(), text, fontSize, 2.0f);
        Raylib.DrawTextEx(Raylib.GetFontDefault(), text, new Vector2(pos.X - textSize.X / 2, pos.Y - textSize.Y / 2), fontSize, 2.0f, color);
    }

    public static void DrawStatistic(string text, Vector2 pos, float n){
        Raylib.DrawRectangleRounded(new Rectangle(pos.X - 100, pos.Y - 50, n * 2, 25), 0.25f, 4, Color.RED);
        Raylib.DrawRectangleRoundedLines(new Rectangle(pos.X - 100, pos.Y - 50, 200, 25), 0.25f, 4, 2.0f, Color.BLACK);
        DrawTextCenter(text, new Vector2(pos.X, pos.Y - 10), 24, Color.BLACK);
    }

    public class Button{
        public string text;
        public Vector2 pos;
        public Vector2 size;
        private Action method;
        private bool inZone = false;
        private bool isHolding = false;
        private bool isJustClicked = false;

        public Button(string text, Vector2 pos, Vector2 size, Action method){
            this.text = text;
            this.pos = new Vector2(pos.X - size.X / 2, pos.Y - size.Y / 2);
            this.size = size;
            this.method = method;
        }

        public void CheckIfMouseIsInRectangle(){
            Vector2 m = Raylib.GetMousePosition();

            if (m.X >= pos.X && m.X <= pos.X + size.X && m.Y >= pos.Y && m.Y <= pos.Y + size.Y) inZone = true;
            else inZone = false;

            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT)) isHolding = true;
            else isHolding = false;

            if (Raylib.IsMouseButtonReleased(MouseButton.MOUSE_BUTTON_LEFT) && inZone) isJustClicked = true;
        }

        public void Draw(){
            Color col = Color.WHITE;

            CheckIfMouseIsInRectangle();
            if (inZone){
                col = Color.LIGHTGRAY;
            }

            if (isHolding && inZone){
                col = Color.GRAY;
            }

            if (inZone && isJustClicked){
                method();
                isJustClicked = false;
                Console.WriteLine("Done!");
            }

            Raylib.DrawRectangleRounded(new Rectangle(pos.X, pos.Y, size.X, size.Y), 0.25f, 2, col);
            Raylib.DrawRectangleRoundedLines(new Rectangle(pos.X, pos.Y, size.X, size.Y), 0.25f, 2, 2.0f, Color.BLACK);
            DrawTextCenter(text, pos + new Vector2(size.X / 2, size.Y / 2), 16, Color.BLACK);
        }
    }
}