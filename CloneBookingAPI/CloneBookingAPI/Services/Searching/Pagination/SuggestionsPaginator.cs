using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Database.Models.Suggestions;
using CloneBookingAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Controllers.Search.Pagination
{
    public class SuggestionsPaginator : IPaginator
    {
        private readonly ApartProjectDbContext _context;

        public SuggestionsPaginator(ApartProjectDbContext context)
        {
            _context = context;
        }

        public IQueryable<Suggestion> SelectItems(IQueryable<Suggestion> suggestions, int page = 1, int pageSize = 25)
        {
            try
            {
                if (suggestions is null)
                {
                    return null;
                }

                var resSuggestions = suggestions
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

                return resSuggestions;
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (ArgumentException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (OperationCanceledException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (InvalidOperationException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
