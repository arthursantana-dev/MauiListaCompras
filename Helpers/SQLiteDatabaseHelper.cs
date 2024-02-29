using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MauiListaCompras.Models;

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

        public Task<int> Delete(Produto produto)
        {
            return _connection.Table<Produto>().DeleteAsync(i => i.Id == produto.Id);
        }

    }
}
