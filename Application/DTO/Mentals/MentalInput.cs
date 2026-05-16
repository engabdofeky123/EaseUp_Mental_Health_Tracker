using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Mentals
{
    public class MentalInput
    {
        public double DepressionScore { get; set; }
        public double AnxietyScore { get; set; }
        public double StressScore { get; set; }
        public string Diagnosis { get; set; }
    }
}
