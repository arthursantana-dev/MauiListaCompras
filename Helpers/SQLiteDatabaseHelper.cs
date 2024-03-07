using MauiListaCompras.Models;
using SQLite;

namespace MauiListaCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection _connection;

        //construtor: executa "create table if not exists"
        public SQLiteDatabaseHelper(string path)
        {
            //conexão com o banco de dados (localhost, port, user...)
            _connection = new SQLiteAsyncConnection(path);

            _connection.CreateTableAsync<Produto>().Wait();
        }

        public Task<int> Insert(Produto produto)
        {
            return _connection.InsertAsync(produto);
        }

        public Task<List<Produto>> Update(Produto produto)
        {
            string sql = "UPDATE Produto SET Descricao=?, Preco=?, Quantidade=? WHERE Id=?";

            return _connection.QueryAsync<Produto>(sql, produto.Descricao, produto.Preco, produto.Quantidade, produto.Id);
        }

        public Task<List<Produto>> GetAll()
        {
            return _connection.Table<Produto>().ToListAsync();
        }

        public Task<int> Delete(Produto produto)
        {
            return _connection.Table<Produto>().DeleteAsync(i => i.Id == produto.Id);
        }

        public Task<List<Produto>> Search(string query)
        {
            string sql = "SELECT * FROM Produto WHERE descricao LIKE '%" + query + "%'";
            return _connection.QueryAsync<Produto>(sql);
        }

    }
}
