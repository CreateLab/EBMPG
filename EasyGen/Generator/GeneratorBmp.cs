using EBMPG.Model;

namespace EBMPG.Generator;

public sealed class GeneratorBmp
{
    public byte[] Generate(Pixel[,] pixels)
    {
        var height = pixels.GetLength(0);
        var width = pixels.GetLength(1);
        var imageSize = CalculateImageSize(width, height);
        var header = GenerateHeader(width, height, imageSize);
        var pixelsData = GeneratePixelsData(pixels, imageSize, width, height);

        var bmp = new byte[header.Length + pixelsData.Length];
        Array.Copy(header, bmp, header.Length);
        Array.Copy(pixelsData, 0, bmp, header.Length, pixelsData.Length);
        return bmp;
    }

    private byte[] GeneratePixelsData(Pixel[,] pixels, int imageSize, int width, int height)
    {
        var padding = (4 - (width * 3) % 4) % 4; //  padding

        var pixelsData = new byte[imageSize];

        var index = 0;
        for (var h = height - 1; h >= 0; h--) // start from bottom
        {
            for (var w = 0; w < width; w++)
            {
                var p = pixels[h, w];
                pixelsData[index] = p.B;
                pixelsData[index + 1] = p.G;
                pixelsData[index + 2] = p.R;
                index += 3;
            }

            // fill padding with empty pixels
            for (var i = 0; i < padding; i++)
            {
                pixelsData[index] = 0;
                index++;
            }
        }

        return pixelsData;
    }

    private byte[] GenerateHeader(int width, int height, int imageSize)
    {
        return new byte[]
        {
            0x42, 0x4D, // Signature ("BM")
            (byte)(imageSize + 54), 0x00, 0x00, 0x00, // File size
            0x00, 0x00, 0x00, 0x00, // Reserved
            0x36, 0x00, 0x00, 0x00, // Shift, to start of pixel
            0x28, 0x00, 0x00, 0x00, // Size of header (image info)
            (byte)(width & 0xFF), (byte)((width >> 8) & 0xFF), (byte)((width >> 16) & 0xFF),
            (byte)((width >> 24) & 0xFF), // width
            (byte)(height & 0xFF), (byte)((height >> 8) & 0xFF), (byte)((height >> 16) & 0xFF),
            (byte)((height >> 24) & 0xFF), // height
            0x01, 0x00, //  count of color planes (1) 
            0x18, 0x00, // Bit per pixel (8 = 256 colors)
            0x00, 0x00, 0x00, 0x00, // Compression (0 = no compression)
            0x00, 0x00, 0x00, 0x00, // Size of data (0 = no compression)
            0x00, 0x00, 0x00, 0x00, // horizontal resolution (ignored)
            0x00, 0x00, 0x00, 0x00, // vertical resolution (ignored)
            0x00, 0x00, 0x00, 0x00, // Number of colors in palette (0 = all) (ignored for 24 bit)
            0x00, 0x00, 0x00, 0x00 // Number of important colors (0 = all) (ignored for 24 bit)
        };
    }


    private int CalculateImageSize(int width, int height)
    {
        // Here padding represents the number of extra padding bytes
        // that are added to each line of the image to make the line length a multiple of 4.
        // This is done to ensure line alignment in BMP files.
        var padding = (4 - (width * 3) % 4) % 4;
        return width * height * 3 + padding * height;
    }
}