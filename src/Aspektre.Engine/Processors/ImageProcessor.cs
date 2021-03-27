using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Atrea.Extensions;

namespace Aspektre.Engine.Processors
{
    public class ImageProcessor
    {
        private const int BatchSize = 10;
        private const string MatchesFolder = "matches";

        private static readonly object Mutex = new();

        private static readonly IEnumerable<string> ImagePathFilters = new List<string>
        {
            "*.bmp", "*.gif", "*.jpg", "*.jpeg", "*.jpe", "*.jif", "*.jfif", "*.png", "*.tiff", "*.tif"
        };

        private readonly ImageProcessorOptions _options;

        public ImageProcessor(ImageProcessorOptions options) => _options = options;

        public async Task Process()
        {
            var outputDirectory = CreateOutputDirectory();
            var imagePaths = GetImagePaths();

            var copyMatchingImagesTasks = imagePaths
                .BatchBy(BatchSize)
                .Select(imageBatch => Task.Run(() => CopyImagesIfAspectRatioMatch(imageBatch, outputDirectory)));

            await Task.WhenAll(copyMatchingImagesTasks);
        }

        private FileSystemInfo CreateOutputDirectory() =>
            Directory.CreateDirectory(Path.Combine(_options.Directory, MatchesFolder));

        private IEnumerable<string> GetImagePaths() =>
            ImagePathFilters.Aggregate(
                new List<string>(),
                (current, imagePathFilter) => current.Concat(
                    Directory.GetFiles(_options.Directory, imagePathFilter, _options.SearchOption)
                ).ToList()
            );

        private void CopyImagesIfAspectRatioMatch(IEnumerable<string> imagePaths, FileSystemInfo outputDirectory)
        {
            foreach (var imagePath in imagePaths)
            {
                if (!IsAspectRatioMatch(imagePath))
                {
                    continue;
                }

                var fileInfo = new FileInfo(imagePath);
                var outputImagePath = Path.Combine(outputDirectory.FullName, fileInfo.Name);

                lock (Mutex)
                {
                    try
                    {
                        if (File.Exists(outputImagePath))
                        {
                            continue;
                        }

                        fileInfo.CopyTo(outputImagePath);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Failed to copy {imagePath} to output directory: {e.Message}");
                    }
                }
            }
        }

        private bool IsAspectRatioMatch(string imagePath)
        {
            try
            {
                using var image = Image.FromFile(imagePath);

                return image.Width > _options.MinimumWidth &&
                       image.Height > _options.MinimumHeight &&
                       Math.Abs(image.Width / (float) image.Height - _options.AspectRatio) <
                       _options.AspectRatioTolerance;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to handle {imagePath}: {e.Message}");
                return false;
            }
        }
    }
}