using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class MentalHealth 
    {
        public int Id { get; private set; }
        public int StudentId  { get; set; }
        public double DepressionScore { get;  set; }
        public double AnxietyScore { get;  set; }
        public double StressScore { get;  set; }
        public string Diagnosis { get;  set; }
        public DateTime CreatedAt { get;  set; }
    }
}
