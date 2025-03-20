using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Strunchik.Model.Order;
using Strunchik.Model.OrderItem;
using PdfSharp.Fonts;
using System.IO;

namespace Strunchik.ViewModel.Services.PDFMakerService;

public class PDFMakerService
{
    public static void CreatePDf(OrderModel order, ApplicationContext.ApplicationContext context)
    {
        GlobalFontSettings.FontResolver = new FontResolver();

        // Создаём PDF-документ
        var document = new PdfDocument();
        document.Info.Title = "Чек магазина";
        document.Info.Author = "Strunchic Inc.";

        // Добавляем новую страницу
        PdfPage page = document.AddPage();
        page.Width = XUnit.FromMillimeter(100);
        page.Height = XUnit.FromMillimeter(250);

        // Объект для рисования на странице
        XGraphics gfx = XGraphics.FromPdfPage(page);

        // Шрифты
        XFont titleFont = new("Arial", 22, XFontStyleEx.Bold);
        XFont textFont = new("Arial", 12, XFontStyleEx.Bold);
        XFont dateFont = new("Arial", 10, XFontStyleEx.Bold);

        // Название магазина
        gfx.DrawString("Магазин \"Strunchic\"", titleFont, XBrushes.Black, new XPoint(50, 50));
        gfx.DrawString($"Чек №{new Random().Next(1000, 10000)}", textFont, XBrushes.Black, new XPoint(115, 70));

        // Объект для рисования линий
        var pen = new XPen(XColor.FromArgb(0), 2);
        gfx.DrawLine(pen, 30, 85, 260, 85);

        // Список товаров

        int yPosition = 105;
        int counter = 1;

        List<OrderItemModel> items = [.. order.OrderItems];
        foreach (var item in items)
        {
            var itemString1 = $"{counter}. {context.Items.FirstOrDefault(i => i.ItemId == item.ItemId).Title ?? throw new NullReferenceException()}";
            var itemString2 = $"{item.Quantity} x {item.Price}";
            var itemString3 = $"Итого: ${item.Price * item.Quantity}";

            gfx.DrawString(itemString1, textFont, XBrushes.Black, new XPoint(40, yPosition));
            yPosition += 13;
            gfx.DrawString(itemString2, textFont, XBrushes.Black, new XPoint(55, yPosition));
            yPosition += 13;
            gfx.DrawString(itemString3, textFont, XBrushes.Black, new XPoint(55, yPosition));
            yPosition += 13;

            counter++;
        }

        gfx.DrawLine(pen, 30, yPosition, 260, yPosition);
        yPosition += 20;

        // Итого и Дата
        gfx.DrawString($"{DateTime.Now:dd.MM.yyyy HH:mm}", dateFont, XBrushes.Black, new XPoint(40, yPosition));
        yPosition += 15;
        gfx.DrawString($"Итого: ${order.TotalAmount}", textFont, XBrushes.Black, new XPoint(40, yPosition));
        yPosition += 20;

        gfx.DrawString("Спасибо за покупку!", textFont, XBrushes.Black, new XPoint(40, yPosition));
        yPosition += 20;

        // QR-код
        XImage image = XImage.FromFile(@"Resources/Images/qr.png");
        gfx.DrawImage(image, 90, yPosition, 100, 100);

        // Сохраняем документ
        string filename = "ShopReceipt.pdf";
        Directory.CreateDirectory("temp");
        document.Save($@"temp\{filename}");
        Console.WriteLine($"Чек успешно создан: {filename}");
    }
}
