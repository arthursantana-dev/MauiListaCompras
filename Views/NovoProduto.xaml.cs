using MauiListaCompras.Models;

namespace MauiListaCompras.Views;

public partial class NovoProduto : ContentPage
{
	public NovoProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
			Produto p = new Produto
			{
				Descricao = text_descricao.Text,
				Quantidade = Convert.ToDouble(text_quantidade.Text),
				Preco = Convert.ToDouble(text_preco.Text),

			};

			await App.Db.Insert(p);
			await DisplayAlert("Sucesso", "Produto Inserido", "OK");
		} catch (Exception ex) {
			await DisplayAlert("Ops", ex.Message, "OK");
		}
    }
}