using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProductos.Common.DTOs;
using GestionProductos.DataAccessLayer.Models;
using GestionProductos.DataAccessLayer.Repositories;

namespace GestionProductos.BusinessLayer.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductoDto>> GetAllAsync()
        {
            var productos = await _repository.GetAllAsync();
            return productos.Select(p => MapToDto(p)).ToList();
        }

        public async Task<ProductoDto?> GetByIdAsync(int id)
        {
            var producto = await _repository.GetByIdAsync(id);
            if (producto == null) return null;

            return MapToDto(producto);
        }

        public async Task<ProductoDto> CreateAsync(CreateProductoDto dto)
        {
            var nuevoProducto = new Producto
            {
                Nombre = dto.Nombre,
                Precio = dto.Precio,
                Categoria = dto.Categoria,
                Activo = true
            };

            await _repository.AddAsync(nuevoProducto);

            return MapToDto(nuevoProducto);
        }

        public async Task<bool> UpdateAsync(int id, CreateProductoDto dto)
        {
            var productoExistente = await _repository.GetByIdAsync(id);
            if (productoExistente == null) return false;

            productoExistente.Nombre = dto.Nombre;
            productoExistente.Precio = dto.Precio;
            productoExistente.Categoria = dto.Categoria;

            await _repository.UpdateAsync(productoExistente);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productoExistente = await _repository.GetByIdAsync(id);
            if (productoExistente == null) return false;

            await _repository.DeleteAsync(id);
            return true;
        }

        private static ProductoDto MapToDto(Producto producto)
        {
            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Precio = producto.Precio,
                Categoria = producto.Categoria,
                Activo = producto.Activo
            };
        }
    }
}