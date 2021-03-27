using System.IO;

namespace Aspektre.Engine.Processors
{
    public class ImageProcessorOptions
    {
        public string Directory { get; init; }

        public SearchOption SearchOption { get; init; }

        public uint AspectRatioWidth { get; init; }

        public uint AspectRatioHeight { get; init; }

        public float AspectRatio => AspectRatioWidth / (float) AspectRatioHeight;

        public double AspectRatioTolerance { get; init; }

        public uint MinimumHeight { get; init; }

        public uint MinimumWidth { get; init; }
    }
}