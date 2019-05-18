using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;

namespace Aspektre.Business
{
    internal class AspektreImageProcessor
    {
        private static readonly string[] ImagePathFilters = {"*.jpg", "*.jpeg", "*.png"};

        private static string _directory;
        private static string _matchCopyDirectory;
        private static double _targetAspectRatio;

        private const double Tolerance = 0.15;
        private const int MinimumWidth = 1100;
        private const int MinimumHeight = 600;
        private const int BatchSize = 100;

        public AspektreImageProcessor(string directory, int aspectRatioWidth, int aspectRatioHeight)
        {
            _directory = directory;
            _matchCopyDirectory = GetMatchCopyDirectory();
            _targetAspectRatio = aspectRatioWidth / (float) aspectRatioHeight;

            Directory.CreateDirectory(_matchCopyDirectory);
        }

        public void Process()
        {
            var imagePaths = GetImagePaths().ToList();
            var batchCount = imagePaths.Count / BatchSize;
            var currentCount = 0;

            foreach (var imagePath in imagePaths)
            {
                CopyImageMatches(imagePath);
                ++currentCount;

                if (currentCount % batchCount == 0)
                {
                    Console.WriteLine($"{(currentCount / (double) imagePaths.Count * 100):##.##}% complete...");
                }
            }
        }

        private static void CopyImageMatches(string imagePath)
        {
            if (!IsImageAspectRatioMatch(imagePath)) return;

            var fileInfo = new FileInfo(imagePath);

            if (File.Exists(Path.Combine(_matchCopyDirectory, fileInfo.Name)))
            {
                return;
            }

            fileInfo.CopyTo(Path.Combine(_matchCopyDirectory, fileInfo.Name));
        }

        private static string GetMatchCopyDirectory()
        {
            return Path.Combine(_directory, "matches", DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss"));
        }

        private static IEnumerable<string> GetImagePaths() => ImagePathFilters.Aggregate(new List<string>(),
            (current, imagePathFilter) =>
                current.Concat(Directory.GetFiles(_directory, imagePathFilter, SearchOption.AllDirectories).ToList())
                    .ToList());

        private static bool IsImageAspectRatioMatch(string imagePath)
        {
            bool match;

            using (var image = Image.FromFile(imagePath))
            {
                match = image.Width > image.Height &&
                        image.Width > MinimumWidth &&
                        image.Height > MinimumHeight &&
                        Math.Abs(image.Width / (float) image.Height - _targetAspectRatio) < Tolerance;
            }

            return match;
        }
    }
}