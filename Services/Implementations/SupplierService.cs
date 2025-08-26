using AutoMapper;
using AutoMapper.QueryableExtensions;
using InvoiceApp.Data;
using InvoiceApp.DTOs;
using InvoiceApp.Models;
using InvoiceApp.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InvoiceApp.Services.Implementations
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SupplierService(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        public async Task<List<SupplierDto>> GetBySiteIdAsync(int siteId)
        {
            var suppliers = await _context.Suppliers
                        .ProjectTo<SupplierDto>(_mapper.ConfigurationProvider)
                        .OrderBy(s => s.Name)
                        .ToListAsync();

            return suppliers;
        }

        public async Task<(bool isSuccess, object result)> CreateAsync(int siteId, SupplierDto dto)
        {
            var supplier = _mapper.Map<Supplier>(dto);
            supplier.SiteId = siteId;
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();

            var updatedList = await GetBySiteIdAsync(siteId);
            return (true, updatedList);
        }
    }
}