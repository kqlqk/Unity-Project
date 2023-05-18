using System;
using System.Numerics;
using System.IO;
using UnityEngine;
using Random = UnityEngine.Random;

public class NeuralNetwork
{
    private int inputSize;
    private int outputSize;
    private int hiddenSize;
    private float[,] weightsHidden;
    private float[,] weightsOutput;
    private float[] biasesHidden;
    private float[] biasesOutput;

    public NeuralNetwork(int inputSize, int outputSize, int hiddenSize)
    {
        this.inputSize = inputSize;
        this.outputSize = outputSize;
        this.hiddenSize = hiddenSize;

        weightsHidden = new float[inputSize, hiddenSize];
        weightsOutput = new float[hiddenSize, outputSize];
        biasesHidden = new float[hiddenSize];
        biasesOutput = new float[outputSize];

        InitializeWeightsAndBiases();
    }

    private void InitializeWeightsAndBiases()
    {
        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                weightsHidden[i, j] = Random.Range(-1f, 1f);
            }
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                weightsOutput[i, j] = Random.Range(-1f, 1f);
            }
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            biasesHidden[i] = Random.Range(-1f, 1f);
        }

        for (int i = 0; i < outputSize; i++)
        {
            biasesOutput[i] = Random.Range(-1f, 1f);
        }
    }

    public float[] FeedForward(float[] inputs)
    {
        float[] hiddenLayer = new float[hiddenSize];
        for (int i = 0; i < hiddenSize; i++)
        {
            float sum = 0f;
            for (int j = 0; j < inputSize; j++)
            {
                sum += inputs[j] * weightsHidden[j, i];
            }

            hiddenLayer[i] = ActivationFunction(sum + biasesHidden[i]);
        }

        float[] outputs = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            float sum = 0f;
            for (int j = 0; j < hiddenSize; j++)
            {
                sum += hiddenLayer[j] * weightsOutput[j, i];
            }

            outputs[i] = ActivationFunction(sum + biasesOutput[i]);
        }

        return outputs;
    }

    private float ActivationFunction(float x)
    {
        return Mathf.Clamp01(x);
    }

    public void Train(float[] inputs, float[] targets, float learningRate)
    {
        float[] hiddenLayer = new float[hiddenSize];
        float[] outputs = new float[outputSize];

        // Feedforward pass
        for (int i = 0; i < hiddenSize; i++)
        {
            float sum = 0f;
            for (int j = 0; j < inputSize; j++)
            {
                sum += inputs[j] * weightsHidden[j, i];
            }

            hiddenLayer[i] = ActivationFunction(sum + biasesHidden[i]);
        }

        for (int i = 0; i < outputSize; i++)
        {
            float sum = 0f;
            for (int j = 0; j < hiddenSize; j++)
            {
                sum += hiddenLayer[j] * weightsOutput[j, i];
            }

            outputs[i] = ActivationFunction(sum + biasesOutput[i]);
        }

        // Backpropagation pass
        float[] outputErrors = new float[outputSize];
        for (int i = 0; i < outputSize; i++)
        {
            outputErrors[i] = targets[i] - outputs[i];
        }

        float[] hiddenErrors = new float[hiddenSize];

        for (int i = 0; i < hiddenSize; i++)
        {
            float error = 0f;
            for (int j = 0; j < outputSize; j++)
            {
                error += outputErrors[j] * weightsOutput[i, j];
            }

            hiddenErrors[i] = error;
        }

        // Update weights and biases
    for (int i = 0; i < hiddenSize; i++)
        {
            for (int j = 0; j < outputSize; j++)
            {
                weightsOutput[i, j] += learningRate * outputErrors[j] * hiddenLayer[i];
            }
        }

        for (int i = 0; i < inputSize; i++)
        {
            for (int j = 0; j < hiddenSize; j++)
            {
                weightsHidden[i, j] += learningRate * hiddenErrors[j] * inputs[i];
            }
        }

        for (int i = 0; i < outputSize; i++)
        {
            biasesOutput[i] += learningRate * outputErrors[i];
        }

        for (int i = 0; i < hiddenSize; i++)
        {
            biasesHidden[i] += learningRate * hiddenErrors[i];
        }
    }

    
    public void SaveModel(string filename)
{
    using (StreamWriter writer = new StreamWriter(filename))
    {
        int rows1 = weightsHidden.GetLength(0);
        int columns1 = weightsHidden.GetLength(1);
        writer.WriteLine(rows1);
        writer.WriteLine(columns1);

        for (int i = 0; i < rows1; i++)
        {
            for (int j = 0; j < columns1; j++)
            {
                writer.Write(weightsHidden[i, j] + " ");
            }
            writer.WriteLine(); // Add a new line after each row
        }

        int rows2 = weightsOutput.GetLength(0);
        int columns2 = weightsOutput.GetLength(1);
        writer.WriteLine(rows2);
        writer.WriteLine(columns2);

        for (int i = 0; i < rows2; i++)
        {
            for (int j = 0; j < columns2; j++)
            {
                writer.Write(weightsOutput[i, j] + " ");
            }
            writer.WriteLine(); // Add a new line after each row
        }
    }
}

    public void LoadModel(string filename)
    {
        using (StreamReader reader = new StreamReader(filename))
        {
            int rows1 = int.Parse(reader.ReadLine());
            int columns1 = int.Parse(reader.ReadLine());

            weightsHidden = new float[rows1, columns1];
            for (int i = 0; i < rows1; i++)
            {
                string[] weightsHiddenValues = reader.ReadLine().Split(' ');
                for (int j = 0; j < columns1; j++)
                {
                    weightsHidden[i, j] = float.Parse(weightsHiddenValues[j]);
                }
            }

            int rows2 = int.Parse(reader.ReadLine());
            int columns2 = int.Parse(reader.ReadLine());

            weightsOutput = new float[rows2, columns2];
            for (int i = 0; i < rows2; i++)
            {
                string[] weightsOutputValues = reader.ReadLine().Split(' ');
                for (int j = 0; j < columns2; j++)
                {
                    weightsOutput[i, j] = float.Parse(weightsOutputValues[j]);
                }
            }
        }
    }


    
}

