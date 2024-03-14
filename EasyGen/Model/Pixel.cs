namespace EBMPG.Model;

public sealed record Pixel(byte R = 0, byte G = 0, byte B = 0)
{
    public  byte B { get; init; } = B;
    public  byte G { get; init; } = G;
    public  byte R { get; init; } = R;
}