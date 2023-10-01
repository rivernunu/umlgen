using System;
using System.IO;

class Program
{
    static void Main()
    {
        // See https://aka.ms/new-console-template for more information
        Console.WriteLine("Hello, World!");
        // Specify the directory to be manipulated.
        string path = "./input";

        // Get the files in the directory and print out the ones with .cs extension.
        foreach (string file in Directory.GetFiles(path, "*.cs"))
        {
            string fileName = Path.GetFileName(file);
            Console.WriteLine($"File Name: {fileName}");

            // Read and print the content of each .cs file
            string contents = File.ReadAllText(file);
            Console.WriteLine($"Contents: {contents}");
        }
        Console.ReadLine();
    }

    static void Sub()
    {
        Console.WriteLine("整数を入力してください:");
        string userInput = Console.ReadLine();

        try
        {
            int number = int.Parse(userInput);
            Console.WriteLine("入力された整数は: " + number);
        }
        catch (FormatException)
        {
            Console.WriteLine("無効な入力です。整数を入力してください。");
        }

        Console.WriteLine("プログラムが終了しました。");
    }

    static int AddNumbers(int a, int b)
    {
        int sum = a + b;
        return sum;
    }
}

public sealed class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;

    public Worker(ILogger<Worker> logger) =>
        _logger = logger;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
            await Task.Delay(1_000, stoppingToken);
        }
    }
}

