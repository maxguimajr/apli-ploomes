using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
var configuration = app.Configuration;
ProdutoRepository.Init(configuration); 





    app.UseSwagger();
    app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});




//Inserir
 app.MapPost("/products", (ProductRequest productRequest, ApplicationDbContext context ) => {
     var category = context.Categories.Where(c => c.Id == productRequest.CategoryId).First();
     var product = new Produto {
         Code = productRequest.Code,
         Name = productRequest.Name,
         Description = productRequest.Description,
         Category = category
     };
     if(productRequest.Tags != null)
     {
         product.Tags = new List<Tag>();
         foreach(var item in productRequest.Tags)
         {
             product.Tags.Add(new Tag{Name = item});
         }
     }
     context.Products.Add(product);
     context.SaveChanges();
     return Results.Created($"/products/{product.Id}" , product.Id);
});

// Obter
app.MapGet("/products/{id}", ([FromRoute] int id, ApplicationDbContext context) =>{
    var product = context.Products
    .Where(p => p.Id == id ).First();
    if(product != null){
          
        return Results.Ok(product);   
    } 
        return Results.NotFound();
    
}); 
//Alterar
app.MapPut("/products/{id}", ([FromRoute] int id, ProductRequest productRequest, ApplicationDbContext context) => {
    var product = context.Products.Where(p => p.Id == id).First();
    
    product.Code = productRequest.Code;
    product.Name = productRequest.Name;
    product.Description = productRequest.Description;

    context.SaveChanges();
    return Results.Ok();
}); 
//Deletar
app.MapDelete("/products/{id}", ([FromRoute] int id, ApplicationDbContext context) =>{
    var product = context.Products.Where(p => p.Id == id).First();
    context.Products.Remove(product);

    context.SaveChanges();
    return Results.Ok();
});

app.MapGet("/products", (ApplicationDbContext context) =>{
    var product = context.Products;
    
    if(product != null){
          
        return Results.Ok(product);   
    } else {
        return Results.NotFound();
        }
});

app.Run();
