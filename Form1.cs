using Event_Handling.Objects;

namespace Event_Handling
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new(); // Список объектов
        Player player; // Создание игрока
        Marker marker; // Создание маркера
        public Form1()
        {
            InitializeComponent();

            player = new Player(pbMain.Width / 2, pbMain.Height / 2, 0);
            objects.Add(player);

            player.OnOverlap += (p, obj) =>
            {
                txtLog.Text = $"[{DateTime.Now:HH:mm:ss:ff}] Игрок пересекся с {obj}\n" + txtLog.Text;
            };

            player.OnMarkerOverlap += (m) =>
            {
                objects.Remove(m);
                marker = null;
            };


            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            objects.Add(marker);

            objects.Add(new MyRectangle(100, 100, 45));
            objects.Add(new MyRectangle(50, 50, 0));
            objects.Add(new MyRectangle(150, 150, 30));
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            g.Clear(Color.White);

            foreach(var obj in objects.ToList())
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }
            }
            foreach(var obj in objects)
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (marker != null)
            {
                float dx = marker.X - player.X; // Расчет вектора между игроком и маркером
                float dy = marker.Y - player.Y;

                float lenght = MathF.Sqrt(dx * dx + dy * dy); // Находим длину
                dx /= lenght; // Нормализация координат
                dy /= lenght;

                player.X += dx * 2; // Пересчет координат игрока
                player.Y += dy * 2;
            }

            pbMain.Invalidate(); // Запрос метод pbMain_Paint по новой
        }

        private void pbMain_MouseClick(object sender, MouseEventArgs e)
        {
            if (marker == null)
            {
                marker = new Marker(0, 0, 0);
                objects.Add(marker);
            }
            marker.X = e.X;
            marker.Y = e.Y;
        }
    }
}