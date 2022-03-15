using CloneBookingAPI.Interfaces;
using CloneBookingAPI.Services.Database;
using CloneBookingAPI.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Text;

namespace CloneBookingAPI.Services.Generators
{
    public class PromoCodeGenerator : BaseGenerator, IPromoGenerator
    {
        private readonly ApartProjectDbContext _context;
        private const string _alphabet = "QWERTYUIOPASDFGHJKLZXCVBNM1234567890";
        private readonly StringBuilder _builder = new();
        private readonly Random _random = new();

        public PromoCodeGenerator(ApartProjectDbContext context)
        {
            _context = context;
        }

        public string GenerateCode(int widthRange, int amountRange)
        {
            try
            {
                for (int i = 0; i < amountRange; i++)
                {
                    for (int j = 0; j < widthRange; j++)
                    {
                        int index;
                        do
                        {
                            index = _random.Next(0, _alphabet.Length - 1);
                        } while (CalculateCountChars(index) > 2);
                        _builder.Append(_alphabet[index]);
                    }

                    if (i < amountRange - 1)
                    {
                        _builder.Append("-");
                    }
                }

                return _builder.ToString();
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);

                return null;
            }
            catch (OperationCanceledException ex)
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

        public int CalculateCountChars(int index)
        {
            int counter = 0;

            for (int i = 0; i < _builder.Length; i++)
            {
                if (_builder[i] == _alphabet[index])
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
