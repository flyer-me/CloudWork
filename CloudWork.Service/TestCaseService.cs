﻿using CloudWork.Model;
using CloudWork.Repository.Base;
using CloudWork.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudWork.Service
{
    public class TestCaseService : BaseService<TestCase>, ITestCaseService
    {
        private readonly IBaseRepository<TestCase> _repository;
        public TestCaseService(IBaseRepository<TestCase> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TestCase>> GetTestCasesWithQuestionAsync()
        {
            return await _repository.Get(t => t, w => true, null, nameof(Question));
        }

        public async Task<TestCase?> GetTestCaseWithQuestionAsync(int id)
        {
            var result =  await _repository.Get(t => t, w => w.Id == id, null, nameof(Question));
            
            return result.FirstOrDefault();
        }
    }
}
