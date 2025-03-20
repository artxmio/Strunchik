using PdfSharp.Fonts;
using System.IO;

namespace Strunchik.ViewModel.Services.PDFMakerService;

public class FontResolver : IFontResolver
{
    public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
    {
        if (familyName == "Verdana")
        {
            string fontFile = @"Resources/Fonts/Verdana.ttf";
            return new FontResolverInfo(fontFile);
        }
        if (familyName == "Arial")
        {
            string fontFile = @"Resources/Fonts/Arial.ttf";
            return new FontResolverInfo(fontFile);
        }

        return null;
    }

    public byte[] GetFont(string fontName)
    {
        return File.ReadAllBytes(fontName);
    }
}