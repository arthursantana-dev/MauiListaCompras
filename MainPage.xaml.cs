
using System.Collections.ObjectModel;
using MauiListaCompras.Models;

namespace MauiListaCompras
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Produto> listaProdutos = new ObservableCollection<Produto>();

        public MainPage()
        {
            InitializeComponent();
            list_produtos.ItemsSource = listaProdutos;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
        }

        private void ToolbarItem_Clicked_Somar(object sender, EventArgs e)
        {
            double soma = listaProdutos.Sum(i => (i.Preco * i.Quantidade));
            string msg = $"O total é {soma:C}";
            DisplayAlert("Somatória", msg, "Fechar");
        }

        protected async override void OnAppearing()
        {
            if(listaProdutos.Count == 0)
            {
                    List<Produto> tmp = await App.Db.GetAll();
                    foreach (Produto produto in tmp)
                    {
                        listaProdutos.Add(produto);
                    }
            }
        }

        private async void ToolbarItem_Clicked_Adicionar(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.NovoProduto());
        }

        private void text_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue;
            listaProdutos.Clear();
            Task.Run(async() => {
                List<Produto> tmp = await App.Db.Search(q);
                foreach (Produto produto in tmp)
                {
                    listaProdutos.Add(produto);
                }
            });
        }

        private void ref_carregando_Refreshing(object sender, EventArgs e)
        {
            listaProdutos.Clear();
            Task.Run(async() => {
                List<Produto> tmp = await App.Db.GetAll();
                foreach(Produto produto in tmp)
                {
                    listaProdutos.Add(produto);
                }
            });
            ref_carregando.IsRefreshing = false;
        }

        private void list_produtos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Produto? p = e.SelectedItem as Produto;
            Navigation.PushAsync(new Views.EditarProduto { BindingContext = p });
        }

        private async void MenuItem_Clicked_Remover(object sender, EventArgs e)
        {
            try
            {
                MenuItem selecionado = (MenuItem)sender;
                Produto p = selecionado.BindingContext
                    as Produto;
                bool confirm = await DisplayAlert("Tem certeza?", "Remover Produto?", "OK", "Cancelar");
                if(confirm)
                {
                    await App.Db.Delete(p);
                    await DisplayAlert("Sucesso!", "Produto Removido", "OK");
                }
            } catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}
