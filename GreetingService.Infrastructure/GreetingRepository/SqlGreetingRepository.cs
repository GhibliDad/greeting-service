﻿using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.GreetingRepository
{
    public class SqlGreetingRepository : IGreetingRepository
    {
        private readonly GreetingDbContext _greetingDbContext;

        public SqlGreetingRepository(GreetingDbContext greetingDbContext)
        {
            _greetingDbContext = greetingDbContext;
        }

        public async Task CreateAsync(Greeting greeting)
        {
            await _greetingDbContext.Greetings.AddAsync(greeting);
            await _greetingDbContext.SaveChangesAsync();
        }
        public async Task<Greeting> GetAsync(Guid id)
        {
            var greeting = await _greetingDbContext.Greetings.FirstOrDefaultAsync(x => x.Id == id);

            return greeting;
        }

        public async Task<IEnumerable<Greeting>> GetAsync()
        {
            return await _greetingDbContext.Greetings.ToListAsync();
        }

        public async Task<IEnumerable<Greeting>> GetAsync(string from, string to)
        {
            //from & to are not null
            if (!string.IsNullOrWhiteSpace(from) && !string.IsNullOrWhiteSpace(to))
            {
                var greetings = _greetingDbContext.Greetings.Where(x => x.From.Equals(from) && x.To.Equals(to));
                return await greetings.ToListAsync();
            }
            //from is not null & to is null
            else if (!string.IsNullOrWhiteSpace(from) && string.IsNullOrWhiteSpace(to))
            {
                var greetings = _greetingDbContext.Greetings.Where(x => x.From.Equals(from));
                return await greetings.ToListAsync();
            }
            //from is null & to is not null
            else if (string.IsNullOrWhiteSpace(from) && !string.IsNullOrWhiteSpace(to))
            {
                var greetings = _greetingDbContext.Greetings.Where(x => x.To.Equals(to));
                return await greetings.ToListAsync();
            }

            //from & to are null, return all greetings
            return await _greetingDbContext.Greetings.ToListAsync();
        }

        public async Task UpdateAsync(Greeting greeting)
        {
            var oldGreeting = await _greetingDbContext.Greetings.FirstOrDefaultAsync(x => x.Id == greeting.Id);
            if (oldGreeting == null)
                throw new Exception("Greeting not found");

            oldGreeting.Message = greeting.Message;
            oldGreeting.To = greeting.To;
            oldGreeting.From = greeting.From;

            await _greetingDbContext.SaveChangesAsync();
        }

        public async Task DeleteAllAsync()
        {
            var greetings = await _greetingDbContext.Greetings.ToListAsync();
            if (!greetings.Any())
                throw new Exception("No Greetings to delete");

            greetings.Clear();

            await _greetingDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var greetings = await _greetingDbContext.Greetings.ToListAsync();
            var greeting = await _greetingDbContext.Greetings.FirstOrDefaultAsync(x => x.Id == id);

            if (greeting == null)
                throw new Exception("Greeting does not exist");

            greetings.Remove(greeting);

            await _greetingDbContext.SaveChangesAsync();
        }
    }
}