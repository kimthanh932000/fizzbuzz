﻿namespace Backend.Repositories.Interfaces
{
    public interface IGameRepo : IRepoBase<Game>
    {
        Task<IEnumerable<Game?>> GetAllAsync();
    }
}
