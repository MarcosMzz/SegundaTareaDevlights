using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestionProductos.Common.DTOs;

namespace GestionProductos.BusinessLayer.Services
{
    public interface IProductoService
    {
        Task<List<ProductoDto>> GetAllAsync();
        Task<ProductoDto?> GetByIdAsync(int id);
        Task<ProductoDto> CreateAsync(CreateProductoDto dto);
        Task<bool> UpdateAsync(int id, CreateProductoDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
