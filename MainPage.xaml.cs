
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

        protected override void OnAppearing()
        {
            if(listaProdutos.Count == 0)
            {
                Task.Run(async () =>
                {
                    List<Produto> tmp = await App.Db.GetAll();
                    foreach (Produto produto in tmp)
                    {
                        listaProdutos.Add(produto);
                    }
                });
            }
        }

        private void ToolbarItem_Clicked_Adicionar(object sender, EventArgs e)
        {

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

        }

        private void MenuItem_Clicked_Remover(object sender, EventArgs e)
        {

        }
    }

}
