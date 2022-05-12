using Microsoft.AspNetCore.Mvc;

//para realizar o build da api, criar um var para acessar webapplication
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Definindo Resposta
app.MapPost("/products", (Product product) =>
{
   ProductRepository.Add(product); 
   return Results.Created($"/products/{product.Code}" , product.Code);
});


app.MapGet("/products/{code}", ([FromRoute] string code) =>
{
    var product = ProductRepository.GetBy(code);
    if(product != null)
        return Results.Ok(product);
    return Results.NotFound();
});

app.MapPut("/products", (Product product) => 
{
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
    productSaved.Name = product.Name;
});

app.MapDelete("/products/{code}", ([FromRoute] string code) => { 
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
});


//para executar a api
app.Run();

public static class ProductRepository
{
    public static List<Product> Products { get; set; }

    public static void Add(Product product)
    {
        if(Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetBy(string code)
    {
       return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Remove(Product product)
    {
        Products.Remove(product);
    }
}

//método público
public class Product
{
    public string Name { get; set; }

    public string Code { get; set; }
}
