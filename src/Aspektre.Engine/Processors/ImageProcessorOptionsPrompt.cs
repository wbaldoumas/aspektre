using System;
using System.IO;

namespace Aspektre.Engine.Processors
{
    public static class ImageProcessorOptionsPrompt
    {
        public static ImageProcessorOptions PromptForImageProcessorOptions()
        {
            var directory = PromptForDirectory();
            var searchOption = PromptForSearchOption();
            var aspectRatioWidth = PromptForAspectRatioWidth();
            var aspectRatioHeight = PromptForAspectRatioHeight();
            var aspectRatioTolerance = PromptForAspectRatioTolerance();
            var minimumWidth = PromptForMinimumWidth();
            var minimumHeight = PromptForMinimumHeight();

            return new ImageProcessorOptions
            {
                Directory = directory,
                SearchOption = searchOption,
                AspectRatioWidth = aspectRatioWidth,
                AspectRatioHeight = aspectRatioHeight,
                AspectRatioTolerance = aspectRatioTolerance,
                MinimumWidth = minimumWidth,
                MinimumHeight = minimumHeight
            };
        }

        private static string PromptForDirectory()
        {
            Console.Write("Enter a directory to search: ");
            var directory = Console.ReadLine();

            while (string.IsNullOrEmpty(directory) || !Directory.Exists(directory))
            {
                Console.Write("Invalid input. Enter a valid directory to search: ");
                directory = Console.ReadLine();
            }

            return directory;
        }

        private static SearchOption PromptForSearchOption()
        {
            Console.Write("Include sub-directories? [y/n]:");
            var searchOptionChoice = Console.ReadLine();

            switch (searchOptionChoice)
            {
                case "n" or "N":
                    return SearchOption.TopDirectoryOnly;
                case "y" or "Y":
                    return SearchOption.AllDirectories;
            }

            while (true)
            {
                Console.Write("Invalid input. Include sub-directories? [y/n]:");
                searchOptionChoice = Console.ReadLine();

                switch (searchOptionChoice)
                {
                    case "" or "n" or "N":
                        return SearchOption.TopDirectoryOnly;
                    case "y" or "Y":
                        return SearchOption.AllDirectories;
                }
            }
        }

        private static uint PromptForAspectRatioWidth()
        {
            Console.Write("Enter desired aspect ratio width: ");
            var desiredAspectRatioWidth = Console.ReadLine();

            if (uint.TryParse(desiredAspectRatioWidth, out var aspectRatioWidth))
            {
                return aspectRatioWidth;
            }

            while (true)
            {
                Console.Write("Invalid input. Enter desired aspect ratio width: ");
                desiredAspectRatioWidth = Console.ReadLine();

                if (uint.TryParse(desiredAspectRatioWidth, out var revisedAspectRatioWidth))
                {
                    return revisedAspectRatioWidth;
                }
            }
        }

        private static uint PromptForAspectRatioHeight()
        {
            Console.Write("Enter desired aspect ratio height: ");
            var desiredAspectRatioHeight = Console.ReadLine();

            if (uint.TryParse(desiredAspectRatioHeight, out var aspectRatioHeight))
            {
                return aspectRatioHeight;
            }

            while (true)
            {
                Console.Write("Invalid input. Enter desired aspect ratio height: ");
                desiredAspectRatioHeight = Console.ReadLine();

                if (uint.TryParse(desiredAspectRatioHeight, out var revisedAspectRatioHeight))
                {
                    return revisedAspectRatioHeight;
                }
            }
        }

        private static double PromptForAspectRatioTolerance()
        {
            Console.Write("Enter desired aspect ratio tolerance: ");
            var desiredAspectRatioTolerance = Console.ReadLine();

            if (double.TryParse(desiredAspectRatioTolerance, out var aspectRatioTolerance) && aspectRatioTolerance > 0)
            {
                return aspectRatioTolerance;
            }

            while (true)
            {
                Console.Write("Invalid input. Enter desired aspect ratio tolerance: ");
                desiredAspectRatioTolerance = Console.ReadLine();

                if (double.TryParse(desiredAspectRatioTolerance, out var revisedAspectRatioTolerance) &&
                    revisedAspectRatioTolerance > 0)
                {
                    return revisedAspectRatioTolerance;
                }
            }
        }

        private static uint PromptForMinimumWidth()
        {
            Console.Write("Enter desired minimum width (in pixels): ");
            var desiredMinimumWidth = Console.ReadLine();

            if (uint.TryParse(desiredMinimumWidth, out var minimumWidth))
            {
                return minimumWidth;
            }

            while (true)
            {
                Console.Write("Invalid input. Enter desired minimum width (in pixels): ");
                desiredMinimumWidth = Console.ReadLine();

                if (uint.TryParse(desiredMinimumWidth, out var revisedMinimumWidth))
                {
                    return revisedMinimumWidth;
                }
            }
        }

        private static uint PromptForMinimumHeight()
        {
            Console.Write("Enter desired minimum height (in pixels): ");
            var desiredMinimumHeight = Console.ReadLine();

            if (uint.TryParse(desiredMinimumHeight, out var minimumHeight))
            {
                return minimumHeight;
            }

            while (true)
            {
                Console.Write("Invalid input. Enter desired minimum height (in pixels): ");
                desiredMinimumHeight = Console.ReadLine();

                if (uint.TryParse(desiredMinimumHeight, out var revisedMinimumHeight))
                {
                    return revisedMinimumHeight;
                }
            }
        }
    }
}