using Services.Dto;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{

        public interface ICsvService
        {
        public Task<List<ViewCsv>> GetAll();


        public Task<ViewCsv> GetById(int Id);

        public Task<ViewCsv> GetViewById(int Id);

        public Task<ViewCsv> Insert(ViewCsv quizDto);

        public Task<ViewCsv> Insert(IEnumerable<ViewCsv> quizDto);

        public Task<ViewCsv> Update(ViewCsv quizDto);

        public Task Delete(int Id);
    }

}
