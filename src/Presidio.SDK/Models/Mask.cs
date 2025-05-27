namespace Presidio.Models;

public class Mask : IAnonymizer
{
    public string Type => "mask";

    public string MaskingChar { get; set; } = "*";

    public int CharsToMask { get; set; }

    public bool FromEnd { get; set; } = false;
}