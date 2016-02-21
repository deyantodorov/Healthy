﻿namespace HealthySystem.Services.Data.Contracts
{
    using System.Linq;
    using HealthySystem.Data.Models;

    public interface IRubricService
    {
        IQueryable<Rubric> GetAll();
    }
}