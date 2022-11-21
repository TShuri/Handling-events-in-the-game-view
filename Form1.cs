using Event_Handling.Objects;

namespace Event_Handling
{
    public partial class Form1 : Form
    {
        MyRectangle myRect; // Создадим поле под прямоугольник
        List<BaseObject> objects = new(); // Список объектов
        public Form1()
        {
            InitializeComponent();

            objects.Add(new MyRectangle(100, 100, 45));
            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(150, 150, 30));
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.Clear(Color.White);

            foreach(var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }
    }
}