﻿using CloudWork.Service.Interface;
using CloudWork.Repository.Base;
using CloudWork.Common;
namespace CloudWork.Service
{
    [Service]
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private IBaseRepository<TEntity> Repository { get; }

        public GenericService(IBaseRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await Repository.GetByIdAsync(id);
        }
        /// <summary>
        /// 增加实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task</returns>
        public async Task AddAsync(TEntity entity)
        {
            await Repository.AddAsync(entity);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Task 影响行数</returns>
        public void Update(TEntity entity)
        {
            Repository.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await Repository.DeleteAsync(id);
        }

        public virtual async Task<int> SaveAsync()
        {
            return await Repository.SaveAsync();
        }
    }
}
