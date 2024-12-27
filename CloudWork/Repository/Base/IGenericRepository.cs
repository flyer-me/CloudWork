﻿using CloudWork.Models;

namespace CloudWork.Repository.Base
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(object id);
        Task AddAsync(T entity);
        void Update(T entity);
        Task DeleteAsync(object id);
        Task SaveAsync();
    }
}
