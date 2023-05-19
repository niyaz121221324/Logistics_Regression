using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsRegression.csproj1
{
    public class LogisticsRegression
    {
        private double[,] _inputs;
        private double[] _outputs;
        private double[] _weights;

        public LogisticsRegression(double[,] inputs, double[] outputs)
        {
            _inputs = inputs;
            _outputs = outputs;
            _weights = new double[inputs.GetLength(1)];
        }

        public double Predict(double[,] inputs)
        {
            double weightedSum = 0;

            for (var i = 0; i < _weights.Length; i++)
                weightedSum += inputs[0, i] * _weights[i];

            return Sigmoid(weightedSum);
        }

        public double Sigmoid(double z)
        {
            return 1.0 / (1.0 + Math.Exp(-z));
        }

        public double[] Train(double learningRate, int iterations)
        {
            for (var iteration = 0; iteration < iterations; iteration++)
            {
                double sumError = 0;

                for (var j = 0; j < _inputs.GetLength(0); j++)
                {
                    var prediction = Predict(new double[,] { { _inputs[j, 0], _inputs[j, 1] } });
                    var error = _outputs[j] - prediction;
                    sumError += error;

                    for (var i = 0; i < _weights.Length; i++)
                        _weights[i] += learningRate * error * _inputs[j, i];
                }
            }
            return _weights;
        }
    }
}
