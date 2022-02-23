using CloneBookingAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CloneBookingAPI.Services.Generators
{
    public class CodeGenerator : BaseGenerator, IGenerator
    {
        private CodesRepository _repository;

        public CodeGenerator(CodesRepository repository)
        {
            _repository = repository;
        }
        public string GenerateCode(string key)
        {
            try
            {
                Random generator = new();
                do
                {
                    _code = generator.Next(100000, 999999).ToString();
                } while (_repository.IsValueExists(_code));

                _repository.Repository.Add(KeyValuePair.Create(key, _code));

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
