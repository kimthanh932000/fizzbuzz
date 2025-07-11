﻿namespace Backend.Repositories.Interfaces
{
    public interface IRepoBase<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}