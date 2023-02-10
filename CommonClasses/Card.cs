using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonClasses
{
    public class Card
    {
        public int Value { get; set; }

        public string Suit { get; set; }

        public string Name => GetName();

        private string GetName()
        {
           switch (Value)
            {
                case 6:  
                case 7:  
                case 8:  
                case 9:  
                case 10: return $"{Value} {Suit}";
                case 11: return $"Валет {Suit}";
                case 12: return $"Дама {Suit}";
                case 13: return $"Король {Suit}";
                case 14:
                default:
                    return $"Туз {Suit}";
            }
        }
    }
}
