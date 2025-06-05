// See https://aka.ms/new-console-template for more information
using CompositionOverInheritage.Invoices.Implementations;

Console.WriteLine("Hello, World!");


DigitalInvoice digitalInvoice = new DigitalInvoice();

RailwayInvoice railwayInvoice = new RailwayInvoice();
Console.WriteLine(railwayInvoice.BuyerName);
