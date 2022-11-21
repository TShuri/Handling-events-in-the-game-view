using Event_Handling.Objects;

namespace Event_Handling
{
    public partial class Form1 : Form
    {
        MyRectangle myRect; // Создадим поле под прямоугольник
        public Form1()
        {
            InitializeComponent();
            myRect = new MyRectangle(0, 0, 0); // Создание экземпляра класса
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);
            /*
            g.FillRectangle(new SolidBrush(Color.Yellow), 200, 100, 50, 30);
            g.DrawRectangle(new Pen(Color.Red, 2), 200, 100, 50, 30);
            */
            myRect.Render(g);
        }
    }
}