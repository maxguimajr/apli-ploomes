public static class ProdutoRepository{
    public static List<Produto> Products { get; set;} = Products = new List<Produto>();

    public static void Init(IConfiguration configuration){
        var products = configuration.GetSection("Products").Get<List<Produto>>();
        Products = products;
    }

    public static void Adicionar(Produto product){

            Products.Add(product);
        
    }   

    public static Produto GetBy(string code){
        return Products.FirstOrDefault(p => p.Code == code);
    }

    public static void Remove (Produto product){
        Products.Remove(product);
    }

}   
