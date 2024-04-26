using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Data;
using Repository;

namespace Service;
    public class TypeService : ITypeService
    {
        private readonly ITypeRepository _typeRepository;
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public TypeService(ITypeRepository typeRepository, ApplicationDbContext context, HttpClient httpClient)
        {
            _typeRepository = typeRepository;
            _context = context;
            _httpClient = httpClient;
        }
        public async Task<List<Models.Type>> MakeTypeDBTable()
        {
            return await _typeRepository.MakeTypeDBTable();
        }
    }