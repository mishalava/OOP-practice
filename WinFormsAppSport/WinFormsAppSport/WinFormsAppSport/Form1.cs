using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows.Forms;
using WinFormsAppSport.Data;
using WinFormsAppSport.Models.Activity;
using WinFormsAppSport.Services;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WinFormsAppSport
{
    public partial class Form1 : Form
    {
        private readonly WorkoutService _workoutService;
        public Form1(WorkoutService workoutService)
        {
            InitializeComponent();

            _workoutService = workoutService;

            LoadWorkouts();
        }

        private void LoadWorkouts()
        {
            workoutBindingSource.DataSource = _workoutService.GetAll();
        }

        private void открытьФайлToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Clear();
                listBox2.Items.Clear();
                
                var fileName = openFileDialog1.FileName;

                

                try
                {
                    var content = File.ReadAllText(fileName);

                    var data = JsonSerializer.Deserialize<ActivityModel>(content);

                    if (data == null || data.DeviceLog == null)
                    {
                        MessageBox.Show("Пустой файл");
                    }

                    var header = data.DeviceLog.Header;

                    

                    _workoutService.Add(new Workout
                    {
                        FileName = Path.GetFileName(fileName),
                        Distance = header.Distance,
                        Time = header.DateTime
                    });

                    LoadWorkouts();

                    //listBox1.Items.Add($"Имя файла: {Path.GetFileName(fileName)}");
                    //listBox1.Items.Add($"Дата тренировки: {header.DateTime.ToString("dd.MM.yy HH:mm:ss")}");
                    //listBox1.Items.Add($"Дистанция: {header.Distance}");

                    //dataGridView1.Rows.Clear();
                    //dataGridView1.Columns.Clear();


                    //dataGridView1.Columns.Add("TimeISO8601", "TimeISO8601");
                    //dataGridView1.Columns.Add("Speed", "Speed");

                    var samples = data.DeviceLog.Samples;

                    var validSamples = samples.Where(v => v.TimeISO8601.HasValue && v.Speed.HasValue).ToList();

                    //foreach (var k in validSamples)
                    //{
                    //    dataGridView1.Rows.Add(
                    //        k.TimeISO8601?.ToString("yyyy-MM-dd HH:mm:ss.fff") ?? "",
                    //        k.Speed?.ToString() ?? ""
                    //    );
                    //}

                   

                    var speedSamples = data.DeviceLog.Samples.Where(n => n.Speed.HasValue && n.Speed.Value != 0 && n.TimeISO8601.HasValue).ToList();

                    double avgSpeed = speedSamples.Count > 0 ? speedSamples.Average(n => n.Speed!.Value) : 0;

                    double maxSpeed = speedSamples.Count > 0 ? speedSamples.Max(n => n.Speed!.Value) : 0;

                    double minSpeed = speedSamples.Count > 0 ? speedSamples.Min(n => n.Speed!.Value) : 0;

                    double changesSpeed = maxSpeed - minSpeed;

                    double acceleration = 0;

                    if (speedSamples.Count > 1)
                    {

                        var minTime = speedSamples.Min(n => n.TimeISO8601!.Value);
                        var maxTime = speedSamples.Max(n => n.TimeISO8601!.Value);

                        TimeSpan changesTime = maxTime - minTime;
                        double seconds = changesTime.TotalSeconds;

                        acceleration = seconds > 0 ? changesSpeed / seconds : 0;

                    }

                    //listBox2.Items.Add($"Изменение скорости во времени:{acceleration}");
                    //listBox2.Items.Add($"Средняя скорость:{avgSpeed}");
                    //listBox2.Items.Add($"Максимальная скорость:{maxSpeed}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Некорректный файл ({ex.Message})");
                }


            }
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var workoutId = (int)dataGridView2.Rows[e.RowIndex].Cells[0].Value;

            var workout = _workoutService.GetById(workoutId);

            listBox1.Items.Clear();

            if (workout != null)
            {

                listBox1.Items.Add($"Имя файла: {workout.FileName}");
                listBox1.Items.Add($"Дата тренировки: {workout.Time.ToString("dd.MM.yy HH:mm:ss")}");
                listBox1.Items.Add($"Дистанция: {workout.Distance}");
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
