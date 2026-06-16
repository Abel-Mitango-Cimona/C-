namespace EcommerceModa.Models
{
    public class CarrinhoItem
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public decimal PrecoUnitario { get; set; }
        public int Quantidade { get; set; }

        public decimal Subtotal => PrecoUnitario * Quantidade;
    }

    public class Carrinho
    {
        public List<CarrinhoItem> Itens { get; set; } = new();

        public decimal Total => Itens.Sum(i => i.Subtotal);
        public int TotalItens => Itens.Sum(i => i.Quantidade);

        public void AdicionarItem(Produto produto, int quantidade = 1)
        {
            var item = Itens.FirstOrDefault(i => i.ProdutoId == produto.Id);
            if (item != null)
                item.Quantidade += quantidade;
            else
                Itens.Add(new CarrinhoItem
                {
                    ProdutoId = produto.Id,
                    Nome = produto.Nome,
                    Categoria = produto.Categoria,
                    PrecoUnitario = produto.Preco,
                    Quantidade = quantidade
                });
        }

        public void AtualizarQuantidade(int produtoId, int quantidade)
        {
            var item = Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item == null) return;
            if (quantidade <= 0)
                Itens.Remove(item);
            else
                item.Quantidade = quantidade;
        }

        public void RemoverItem(int produtoId)
        {
            var item = Itens.FirstOrDefault(i => i.ProdutoId == produtoId);
            if (item != null) Itens.Remove(item);
        }

        public void Limpar() => Itens.Clear();
    }
}
