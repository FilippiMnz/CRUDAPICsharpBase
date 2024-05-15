using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/saveproduct", (Produto produto)=> {
    RepositorioDeProdutos.Add(produto);
});

app.MapGet("/getproduct/{code}", ([FromRoute] string code)=>{
    var produto = RepositorioDeProdutos.GetBy(code);
    return produto;
});

app.MapPut("/editproduct", (Produto produto)=>{
    var produtoSalvo = RepositorioDeProdutos.GetBy(produto.Code);
    produtoSalvo.Name = produto.Name;
});

app.MapDelete("/deleteproduct/{code}", ([FromRoute] string code)=>{
    var produtoSalvo =  RepositorioDeProdutos.GetBy(code);
    RepositorioDeProdutos.Remover(produtoSalvo);
});

app.MapGet("/ping", ()=>{
    return "Pong";
});

app.Run();

public static class RepositorioDeProdutos{
    public static List<Produto> Produtos { get; set; }

    public static void Add(Produto produto){
        if(Produtos == null)
            Produtos = new List<Produto>();
        Produtos.Add(produto);
    }

    public static Produto GetBy(string code){
        return Produtos.FirstOrDefault(p => p.Code == code);
    }

    public static void Remover(Produto produto){
        Produtos.Remove(produto);
    }
}

public class Produto{
    public string Code { get; set; }
    public string Name { get; set; }
}



//passar parametros Via URL
//api.app.com/users?datastart={date}&dataend={date} --> querry 
/*
app.MapGet("/getproduct", ([FromQuery] string DateStart, [FromQuery] string DateEnd)=>{
    return DateStart + " - " + DateEnd;
});
*/

//api.app.com/user/{code}
/*

*/

/*
app.MapGet("/getproduct", (HttpRequest request)=>{
    return request.Headers["product-code"].ToString();
});
*/