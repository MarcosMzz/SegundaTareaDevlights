using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProductos.DataAccessLayer.Models;

namespace GestionProductos.DataAccessLayer.Repositories
{
    public class ProductoRepositoryInMemory : IProductoRepository
    {
        private static readonly List<Producto> _productos = new()
        {
            new Producto { Id = 1, Nombre = "Teclado Mecánico", Precio = 50.0m, Categoria = "Periféricos", Activo = true },
            new Producto { Id = 2, Nombre = "Mouse Gamer", Precio = 30.0m, Categoria = "Periféricos", Activo = true },
            new Producto { Id = 3, Nombre = "Monitor 24 Pulgadas", Precio = 180.0m, Categoria = "Pantallas", Activo = true }
        };

        public Task<List<Producto>> GetAllAsync()
        {
            return Task.FromResult(_productos.Where(p => p.Activo).ToList());
        }

        public Task<Producto?> GetByIdAsync(int id)
        {
            return Task.FromResult(_productos.FirstOrDefault(p => p.Id == id && p.Activo));
        }

        public Task AddAsync(Producto producto)
        {
            producto.Id = _productos.Count == 0 ? 1 : _productos.Max(p => p.Id) + 1;
            _productos.Add(producto);
            return Task.CompletedTask;
        }

        public async Task UpdateAsync(Producto producto)
        {
            var existingProducto = await GetByIdAsync(producto.Id);
            if (existingProducto != null)
            {
                existingProducto.Nombre = producto.Nombre;
                existingProducto.Precio = producto.Precio;
                existingProducto.Categoria = producto.Categoria;
                existingProducto.Activo = producto.Activo;
            }
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await GetByIdAsync(id);
            if (producto != null)
            {
                _productos.Remove(producto);
            }
        }
    }
}