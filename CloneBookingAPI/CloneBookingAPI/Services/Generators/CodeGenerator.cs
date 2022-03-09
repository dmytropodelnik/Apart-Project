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
        public string GenerateKeyCode(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                {
                    return null;
                }

                Random generator = new();

                _code = generator.Next(100000, 999999).ToString();

                if (_repository.Repository.ContainsKey(key))
                {
                    _repository.Repository[key].Add(_code);
                }
                else
                {
                    _repository.Repository.Add(key, new List<string> { _code });
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
