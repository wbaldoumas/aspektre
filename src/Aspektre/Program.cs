using System;
using Aspektre.Engine.Processors;

while (true)
{
    var options = ImageProcessorOptionsPrompt.PromptForImageProcessorOptions();

    Console.WriteLine("\nProcessing images...");
    await new ImageProcessor(options).Process();
    Console.WriteLine($"Processing complete! Check {options.Directory} for matches!\n");
}
