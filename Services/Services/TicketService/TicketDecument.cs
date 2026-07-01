using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entity;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Services.Services.TicketService
{
    public class TicketDecument : IDocument
    {
        private readonly Ticket _ticket;

        // Color palette - teal / dark green boarding pass theme
        private const string AccentColor = "#1D9E75";
        private const string DarkPanelColor = "#04342C";
        private const string LightAccentColor = "#5DCAA5";
        private const string TextMuted = "#6B7280";
        private const string TextPrimary = "#111827";

        public TicketDecument(Ticket ticket)
        {
            _ticket = ticket;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A5.Landscape());
                page.Margin(0);
                page.Background(Colors.White);
                page.Content().Element(ComposeContent);
            });
        }

        private void ComposeContent(IContainer container)
        {
            // Outer wrapper gives the card some breathing room on the page
            container.Padding(16).Element(card =>
            {
                card.Background(Colors.White)
                    .Border(0.75f)
                    .BorderColor(Colors.Grey.Lighten2)
                    .Row(row =>
                    {
                        // ── Left: main info panel ──
                        row.RelativeItem(3).Element(ComposeMainPanel);

                        // ── Middle: dashed perforation divider ──
                        row.ConstantItem(1).Element(ComposePerforation);

                        // ── Right: dark stub panel with QR ──
                        row.ConstantItem(150).Element(ComposeStubPanel);
                    });
            });
        }

        private void ComposeMainPanel(IContainer container)
        {
            container.Padding(20).Column(col =>
            {
                // Brand row
                col.Item().Row(row =>
                {
                    row.AutoItem().Row(brand =>
                    {
                        brand.AutoItem().Width(20).Height(20)
                            .Background(AccentColor)
                            .AlignMiddle().AlignCenter()
                            .Text("R").FontColor(Colors.White).FontSize(11).Bold();

                        brand.AutoItem().PaddingLeft(6).AlignMiddle()
                            .Text("Rayh").FontSize(14).Bold().FontColor(TextPrimary);
                    });

                    row.RelativeItem().AlignRight().AlignMiddle()
                        .Text("boarding pass").FontSize(8).FontColor(TextMuted);
                });

                col.Item().PaddingTop(16);

                // From / To
                col.Item().Row(row =>
                {
                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text("from").FontSize(8).FontColor(TextMuted);
                        c.Item().Text(_ticket.Booking.Trip.Routes.StartStation.Name)
                            .FontSize(15).Bold().FontColor(TextPrimary);
                    });

                    row.ConstantItem(24).AlignCenter().AlignMiddle()
                        .Text("→").FontSize(14).FontColor(AccentColor);

                    row.RelativeItem().Column(c =>
                    {
                        c.Item().AlignRight().Text("to").FontSize(8).FontColor(TextMuted);
                        c.Item().AlignRight().Text(_ticket.Booking.Trip.Routes.EndStation.Name)
                            .FontSize(15).Bold().FontColor(TextPrimary);
                    });
                });

                col.Item().PaddingTop(16);

                // Date / Time / Seat
                col.Item().Row(row =>
                {
                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text("date").FontSize(7).FontColor(TextMuted);
                        c.Item().Text(_ticket.Booking.Trip.DepartualTime.ToString("dd MMM yyyy"))
                            .FontSize(10).Bold().FontColor(TextPrimary);
                    });

                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text("time").FontSize(7).FontColor(TextMuted);
                        c.Item().Text(_ticket.Booking.Trip.DepartualTime.ToString("HH:mm"))
                            .FontSize(10).Bold().FontColor(TextPrimary);
                    });

                    row.RelativeItem().Column(c =>
                    {
                        var seats = string.Join(",", _ticket.Booking.BookingSeats.Select(x => x.Seats.Number));
                        c.Item().Text("seat").FontSize(7).FontColor(TextMuted);
                        c.Item().Text(seats).FontSize(10).Bold().FontColor(TextPrimary);
                    });

                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text("price").FontSize(7).FontColor(TextMuted);
                        c.Item().Text($"L.E {_ticket.Booking.TotalPrice:F2}")
                            .FontSize(10).Bold().FontColor(TextPrimary);
                    });
                });

                col.Item().PaddingTop(14).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten3);
                col.Item().PaddingTop(12);

                // Passenger
                col.Item().Column(c =>
                {
                    c.Item().Text("passenger").FontSize(7).FontColor(TextMuted);
                    c.Item().Text(_ticket.Booking.User.FullName).FontSize(11).Bold().FontColor(TextPrimary);
                });

                col.Item().PaddingTop(10);

                col.Item().Row(row =>
                {
                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text("phone").FontSize(7).FontColor(TextMuted);
                        c.Item().Text(_ticket.Booking.User.PhoneNumber).FontSize(10).FontColor(TextPrimary);
                    });

                    row.RelativeItem().Column(c =>
                    {
                        c.Item().Text("issued").FontSize(7).FontColor(TextMuted);
                        c.Item().Text(_ticket.IssuedAt.ToString("dd MMM, HH:mm")).FontSize(10).FontColor(TextPrimary);
                    });
                });

                col.Item().PaddingTop(14).LineHorizontal(0.5f).LineColor(Colors.Grey.Lighten3);
                col.Item().PaddingTop(12);

                // Ticket number - the hero element
                col.Item().Column(c =>
                {
                    c.Item().Text("ticket no").FontSize(7).FontColor(TextMuted);
                    c.Item().Text(_ticket.Number).FontSize(20).Bold().FontColor(AccentColor);
                });
            });
        }

        private void ComposePerforation(IContainer container)
        {
            container.Background(Colors.Grey.Lighten4);
        }

        private void ComposeStubPanel(IContainer container)
        {
            container.Background(DarkPanelColor).Padding(16).Column(col =>
            {
                // QR Code on white tile
                col.Item().AlignCenter().Background(Colors.White).Padding(8).Element(e =>
                {
                    var qrBytes = Convert.FromBase64String(_ticket.QRCode);
                    e.Width(80).Height(80).Image(qrBytes).FitArea();
                });

                col.Item().PaddingTop(16);

                col.Item().AlignCenter().Column(c =>
                {
                    c.Item().AlignCenter().Text("trip").FontSize(7).FontColor(LightAccentColor);
                    c.Item().AlignCenter().Text(_ticket.Booking.Trip.Id.ToString())
                        .FontSize(11).Bold().FontColor(Colors.White);
                });

                col.Item().PaddingTop(10);

                col.Item().AlignCenter().Column(c =>
                {
                    var seats = string.Join(",", _ticket.Booking.BookingSeats.Select(x => x.Seats.Number));
                    c.Item().AlignCenter().Text("seat").FontSize(7).FontColor(LightAccentColor);
                    c.Item().AlignCenter().Text(seats).FontSize(11).Bold().FontColor(Colors.White);
                });

                col.Item().ExtendVertical().AlignBottom().Column(c =>
                {
                    c.Item().AlignCenter().Text("Rayh").FontSize(11).Bold().FontColor(Colors.White);
                    c.Item().AlignCenter().Text("your best way to travel")
                        .FontSize(6).FontColor(LightAccentColor);
                });
            });
        }
    }
}
