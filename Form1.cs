using Event_Handling.Objects;
using System.Security.Cryptography.X509Certificates;

namespace Event_Handling
{
    public partial class Form1 : Form
    {
        List<BaseObject> objects = new(); // Список объектов
        Player player; // Создание игрока
        Marker marker; // Создание маркера
        Danger danger; // Создание опасного круга
        Black_area black_area; // Создание черной зоны
        int points;
        public Form1()
        {
            InitializeComponent();
            points = 0;

            black_area = new Black_area(0, 0, 0, pbMain.Height, pbMain.Width);
            objects.Add(black_area);

            danger = new Danger(300, 300, 0);
            objects.Add(danger);

            objects.Add(new Target(800, 50, 0, 200));
            objects.Add(new Target(300, 500, 0, 100));

            marker = new Marker(pbMain.Width / 2 + 50, pbMain.Height / 2 + 50, 0);
            objects.Add(marker);

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

            player.OnTargetOverlap += (t) =>
            {
                updateCoordinate(t);
                t.updateTime(player.X, player.Y);
                points++;
            };

            player.OnDangerOverlap += (d) =>
            {
                updateCoordinate(d);
                d.updateSize();
                points--;
            };
        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            counter.Text = "Points: " + points.ToString();

            var g = e.Graphics;
            g.Clear(Color.White);

            updatePlayer();

            foreach(var obj in objects.ToList()) // Цикл проверки пересечения объектов
            {
                if (obj != player && player.Overlaps(obj, g))
                {
                    player.Overlap(obj);
                    obj.Overlap(player);
                }

                if (obj != black_area && black_area.Overlaps(obj, g))
                {
                    black_area.ChangeFill(obj);
                }
            }
            foreach(var obj in objects) // Цикл рендеринга объектов
            {
                g.Transform = obj.GetTransform();
                obj.Render(g);
            }
        }
        
        private void updatePlayer()
        {
            if (marker != null)
            {
                float dx = marker.X - player.X; // Расчет вектора между игроком и маркером
                float dy = marker.Y - player.Y;
                float lenght = MathF.Sqrt(dx * dx + dy * dy); // Находим длину
                dx /= lenght; // Нормализация координат
                dy /= lenght;

                player.vX += dx * 0.5f; // Пересчет координат игрока
                player.vY += dy * 0.5f;
                player.Angle = 90 - MathF.Atan2(player.vX, player.vY) * 180 / MathF.PI;
            }

            player.vX += -player.vX * 0.1f;
            player.vY += -player.vY * 0.1f;

            // пересчет позиции игрока с помощью вектора скорости
            player.X += player.vX;
            player.Y += player.vY;

        }

        private void updateCoordinate(BaseObject obj)
        {
            Random random = new Random();

            obj.X = random.Next(10, pbMain.Width); // Обновляем координаты цели
            obj.Y = random.Next(10, pbMain.Height);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
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