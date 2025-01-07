// See https://aka.ms/new-console-template for more information
using BuilderPattern;
using static BuilderPattern.AddressGuidedBuilder;

Console.WriteLine("Hello, World!");


var order = OrderBuilder.Empty()
    .WithNumber(10)
    .WithCreatedDateTime(DateTime.Now)
    .WithShippingAddress(b => b
            .WithStreet("123 Main St")
            .WithDetail("Apt 2")
            .WithProvince("CA")
            .WithZipCode("12345"))
    .Build();

Console.WriteLine(order);

Console.WriteLine("order.Number:" + order.Number);
Console.WriteLine("order.CreatedOn:" + order.CreatedOn);
Console.WriteLine("order.ShippingAddress.Street:" + order.ShippingAddress.Street);
Console.WriteLine("order.ShippingAddress.Detail:" + order.ShippingAddress.Detail);
Console.WriteLine("order.ShippingAddress.Province:" + order.ShippingAddress.Province);
Console.WriteLine("order.ShippingAddress.ZipCode:" + order.ShippingAddress.ZipCode);


var address = AddressGuidedBuilder.Create()
    .WithZipCode("12345")
    .WithProvince("CA")
    .WithStreetAndDetail("123 Main St", "Apt 2")
    .Build();

