using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Generators
{
    public class CodeGenerator : BaseGenerator, IGenerator
    {
        public CodeGenerator()
        {

        }
        public string GenerateKeyCode(string key, BaseRepository repository)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return null;
                }

                Random generator = new();

                _code = generator.Next(100000, 999999).ToString();

                if (repository.Repository.ContainsKey(key))
                {
                    repository.Repository[key].Add(_code);
                }
                else
                {
                    repository.Repository.Add(key, new List<string> { _code });
                }

                return _code;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
        }
    }
}
