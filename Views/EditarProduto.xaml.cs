using MauiListaCompras.Models;

namespace MauiListaCompras.Views;

public partial class EditarProduto : ContentPage
{
	public EditarProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
            Produto produto_anexado = BindingContext as Produto;

			Produto p = new Produto
			{
				Id = produto_anexado.Id,
				Descricao = text_descricao.Text,
				Quantidade = Convert.ToDouble(text_quantidade.Text),
				Preco = Convert.ToDouble(text_preco.Text),
			
			};

			await App.Db.Update(p);

        } catch
		{

		}
    }
}