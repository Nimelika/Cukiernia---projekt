using BusinessLogic.Models.Printing;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace BusinessLogic.Services.Printing

{
    public class WordTemplateEngine
    {
        public void GenerateInvoice(
            InvoicePrintModel model,
            string templatePath,
            string outputPath)
        {
            File.Copy(templatePath, outputPath, true);

            using var document = WordprocessingDocument.Open(outputPath, true);
            var body = document.MainDocumentPart!.Document.Body!;

            ReplaceParagraphPlaceholders(body, model);
            ReplaceItemTable(body, model.Items);

            document.MainDocumentPart.Document.Save();
        }

        //placeholdery
        private void ReplaceParagraphPlaceholders(Body body, InvoicePrintModel model)
        {
            foreach (var paragraph in body.Descendants<Paragraph>())
            {
                var fullText = paragraph.InnerText;

                var replaced = ReplaceValue(fullText, model);

                if (fullText != replaced)
                {
                    paragraph.RemoveAllChildren<Run>();
                    paragraph.AppendChild(new Run(new Text(replaced)));
                }
            }
        }

        private string ReplaceValue(string text, InvoicePrintModel model)
        {
            return text
                .Replace("{{Header.InvoiceNumber}}", model.Header.InvoiceNumber ?? "")
                .Replace("{{Header.InvoiceDate}}", model.Header.InvoiceDate?.ToString("dd.MM.yyyy") ?? "")
                .Replace("{{Header.DueDate}}", model.Header.DueDate?.ToString("dd.MM.yyyy") ?? "")
                .Replace("{{Header.PaymentMethod}}", model.Header.PaymentMethod ?? "")

                .Replace("{{Buyer.Name}}", model.Buyer.Name ?? "")
                .Replace("{{Buyer.Address}}", model.Buyer.Address ?? "")
                .Replace("{{Buyer.PostalCode}}", model.Buyer.PostalCode ?? "")
                .Replace("{{Buyer.City}}", model.Buyer.City ?? "")
                .Replace("{{Buyer.TaxId}}", model.Buyer.TaxId ?? "")

                .Replace("{{Totals.TotalNet}}", model.Totals.TotalNet.ToString("0.00"))
                .Replace("{{Totals.TotalVat}}", model.Totals.TotalVat.ToString("0.00"))
                .Replace("{{Totals.TotalGross}}", model.Totals.TotalGross.ToString("0.00"))

                .Replace("{{Notes}}", model.Notes ?? "");
        }

        // tabela z pozycjami
        private void ReplaceItemTable(Body body, List<InvoiceItemPrintModel> items)
        {
            var table = body.Descendants<Table>()
                .FirstOrDefault(t => t.InnerText.Contains("{{Item.Index}}"));

            if (table == null)
                return;

            var templateRow = table.Descendants<TableRow>()
                .First(r => r.InnerText.Contains("{{Item.Index}}"));

            foreach (var item in items)
            {
                var newRow = (TableRow)templateRow.CloneNode(true);

                foreach (var cell in newRow.Descendants<TableCell>())
                {
                    var paragraph = cell.Descendants<Paragraph>().FirstOrDefault();
                    if (paragraph == null)
                        continue;

                    var text = paragraph.InnerText;

                    var replaced = text
                        .Replace("{{Item.Index}}", item.Index.ToString())
                        .Replace("{{Item.ProductName}}", item.ProductName ?? "")
                        .Replace("{{Item.Quantity}}", item.Quantity?.ToString() ?? "")
                        .Replace("{{Item.UnitNetPrice}}", item.UnitNetPrice.ToString("0.00"))
                        .Replace("{{Item.VatRate}}", item.VatRate.ToString("0"))
                        .Replace("{{Item.LineNet}}", item.LineNet.ToString("0.00"))
                        .Replace("{{Item.LineVat}}", item.LineVat.ToString("0.00"))
                        .Replace("{{Item.LineGross}}", item.LineGross.ToString("0.00"));

                    paragraph.RemoveAllChildren<Run>();
                    paragraph.AppendChild(new Run(new Text(replaced)));
                }

                table.AppendChild(newRow);
            }

            templateRow.Remove();
        }
    }
}
