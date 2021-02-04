using AutoMapper;
using DAL.Interfaces;
using DAL.Repositories;
using Services.Dto;
using Services.Interfaces;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Services.Models;
using AutoMapper.QueryableExtensions;

namespace Services
{
    public class CsvService : ICsvService
    {
        private readonly ICsvRepository _csvRepository;

        private readonly IMapper _mapper;

        public CsvService(IMapper mapper, ICsvRepository csvRepository)
        {
            this._mapper = mapper;
            this._csvRepository = csvRepository;
            
        }

        public async Task<List<ViewCsv>> GetAll()
       {
           return await _csvRepository.GetAll().Select(el => _mapper.Map<Csv, ViewCsv>(el)).ToListAsync();
        }

       public async Task<ViewCsv> GetById(string Id)
        {
            var entity = await _csvRepository.GetAll().Where(user => user.FirstName == Id).FirstOrDefaultAsync().ConfigureAwait(false);
            return _mapper.Map<Csv, ViewCsv>(entity);
        }

        public async Task<ViewCsv> Insert(ViewCsv userDto)
        {
            var entity = new Csv();
            _mapper.Map(userDto, entity);
            await _csvRepository.Insert(entity).ConfigureAwait(false);
            await _csvRepository.SaveAsync().ConfigureAwait(false);
            return _mapper.Map<Csv, ViewCsv>(entity);
        }

        public async Task<ViewCsv> Insert(IEnumerable<ViewCsv> users)
        {
            var entity = new Csv();
            foreach (var e in users)
            {
                _mapper.Map(e, entity);
                await _csvRepository.Insert(entity).ConfigureAwait(false);
            }
            await _csvRepository.SaveAsync().ConfigureAwait(false);
            return _mapper.Map<Csv, ViewCsv>(entity);
        }

        public async Task<ViewCsv> Update(ViewCsv userDto)
        {
            var entity = new Csv();
            _mapper.Map(userDto, entity);
            _csvRepository.Update(entity);
            await _csvRepository.SaveAsync().ConfigureAwait(false);
            return _mapper.Map<Csv, ViewCsv>(entity);
        }

        public async Task Delete(int Id)
        {
            await _csvRepository.Delete(Id).ConfigureAwait(false);
            await _csvRepository.SaveAsync().ConfigureAwait(false);
        }

        public bool IsAny(int id)
        {
            return _csvRepository.IsAny(id);
        }

        public async Task<ViewCsv> GetById(int Id)
        {
            return await _csvRepository.GetAll()
               .Where(q => q.Id == Id)
               .ProjectTo<ViewCsv>(_mapper.ConfigurationProvider)
               .FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public Task<ViewCsv> GetViewById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
