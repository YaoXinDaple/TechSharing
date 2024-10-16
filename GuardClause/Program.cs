// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


/*
 卫语句 （Guard Clause），是一种为了避免代码的嵌套，提高代码的可读性，减少代码的复杂度，提高代码的可维护性。
假设有这样一个用例
有使用一个账户下单并发货的场景
    1.下单的时候需要判断账户是否被禁用，
    2.商品列表是否为空，
    3.订单是否已付款，
    4.收货地址是否为空，
    要求在哪一步出现异常要提示异常信息
    以上条件都满足，才允许下单。
 */



var account = new Account { IsEnable = true };
var items = new List<Item>
{
    new Item
    {
        Name = "Item1",
        Price = 10,
        Quantity = 1
    },
    new Item
    {
        Name = "Item2",
        Price = 20,
        Quantity = 2
    },
    new Item
    {
        Name = "Item3",
        Price = 30,
        Quantity = 3
    }
};
PlaceAndShipOrderNesting(account,items, "111");
PlaceAndShipOrderGuardClause(account,items, "222");

void PlaceAndShipOrderNesting(Account account,List<Item> items, string shippingAddress)
{
    if (account.IsEnable)
    {
        if (items.Count > 0)
        {
            var order = Order.PlaceOrder(items, "收货地址");
            if (order.IsPaid)
            {
                if (!string.IsNullOrWhiteSpace(order.ShippingAddress))
                {
                    order.ShipOrder();
                }
                else
                {
                    Console.WriteLine("收货地址不能为空");
                }
            }
            else
            {
                Console.WriteLine("订单未付款");
            }
        }
        else
        {
            Console.WriteLine("商品列表为空，不允许下单");
        }
    }
    else
    {
        Console.WriteLine("账户被禁用，不允许下单");
    }
}

void PlaceAndShipOrderGuardClause(Account account, List<Item> items, string shippingAddress)
{
    if (items.Count == 0)
    {
        Console.WriteLine("商品列表为空，不允许下单");
        return;
    }

    var order1 = Order.PlaceOrder(items, "收货地址");

    if (!order1.IsPaid)
    {
        Console.WriteLine("订单未付款");
        return;
    }
    if (string.IsNullOrWhiteSpace(order1.ShippingAddress))
    {
        Console.WriteLine("收货地址不能为空");
        return;
    }

    order1.ShipOrder();
}

public class Order
{
    public Order(List<Item> items, string shippingAddress)
    {
        Items = items;
        ShippingAddress = shippingAddress;
    }

    public bool IsPaid { get; set; }
    public List<Item> Items { get; set; }
    public string ShippingAddress { get; set; }

    public static Order PlaceOrder(List<Item> items, string shippingAddress)
    {
        return new Order(items, shippingAddress);
    }

    public void ShipOrder()
    {
        Console.WriteLine("订单已发货");
    }
}

public class Item
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

public class Account
{ 
    public bool IsEnable { get; set; }
}
