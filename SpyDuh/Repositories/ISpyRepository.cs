﻿using SpyDuh.Models;

namespace SpyDuh.Repositories
{
    public interface ISpyRepository
    {
        Spy getEnemies(int id);
        Spy getFriends(int id);
    }
}