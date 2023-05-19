using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LogisticsRegression.csproj1
{
    public partial class LogisticsRefressionForm : Form
    {
        private double[,] inputs = new double[,]
        {
            { 2.7810836, 2.550537003 },
            { 1.465489372, 2.362125076 },
            { 3.396561688, 4.400293529 },
            { 1.38807019, 1.850220317 },
            { 3.06407232, 3.005305973 },
            { 7.627531214, 2.759262235 },
            { 5.332441248, 2.088626775 },
            { 6.922596716, 1.77106367 },
            { 8.675418651, -0.242068655 },
            { 7.673756466, 3.508563011 }
        };

        private double[] outputs = new double[]
        {
            0, 0, 0, 0, 0, 1, 1, 1, 1, 1
        };

        private double[] weights;
        private LogisticsRegression logisticRegression;

        public LogisticsRefressionForm()
        {
            InitializeComponent();
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            // Создаем экземпляр класса LogisticRegression
            logisticRegression = new LogisticsRegression(inputs, outputs);

            // Обучаем модель с помощью метода Train с заданными параметрами
            double learningRate = 0.1;
            int iterations = 1000;
            weights = logisticRegression.Train(learningRate, iterations);

            // Создаем Series для отрисовки функции Logistic Regression
            var series = new Series();
            series.ChartType = SeriesChartType.Line;
            series.Color = Color.Red;
            series.BorderWidth = 2;

            // Добавляем координаты точек на Series
            var points = new List<double[]>();
            for (double i = -10; i <= 10; i += 0.1)
            {
                double y = logisticRegression.Sigmoid(weights[0] + weights[1] * i);
                points.Add(new double[] { i, y });
            }

            foreach (var p in points)
            {
                series.Points.AddXY(p[0], p[1]);
            }

            // Добавляем Series на Chart
            chart.Series.Add(series);
            chart.ChartAreas[0].AxisX.Minimum = -10;
            chart.ChartAreas[0].AxisX.Maximum = 10;
            chart.ChartAreas[0].AxisX.Interval = 2;
            chart.ChartAreas[0].AxisY.Minimum = 0;
            chart.ChartAreas[0].AxisY.Maximum = 1;
            chart.ChartAreas[0].AxisY.Interval = 0.2;
            // Вывод результатов обучения на экран
            label1.Text = $"Model weights: {string.Join(" , ", weights)}";
            label2.Text = $"Predict(5, 5) = {logisticRegression.Predict(new double[,] { { 5, 5 } })}";
            label3.Text = $"Predict(2, 1) = {logisticRegression.Predict(new double[,] { { 2, 1 } })}";
            chart.Show();
        }
    }
}
