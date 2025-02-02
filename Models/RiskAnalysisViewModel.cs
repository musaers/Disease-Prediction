using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace HeartDiseaseAnalysis.Models
{

    public class RiskAnalysisViewModel
    {
        public int id {get;set;}
        public string? age { get; set; }
        public double height { get; set; }
        public double weight { get; set; }
        public double bmi { get; set; }
        public bool diabeticstatus { get; set; }
        public string? gender { get; set; }
        public string? race { get; set; }
        public int sleeptime { get; set; }
        public bool smokingstatus { get; set; }
        public double bloodsugar { get; set; }
        public string? marriagestatus { get; set; }
        public bool hypertension { get; set; }
        public string? workplace { get; set; }
        public double risk { get; set; }

       
    }


}


