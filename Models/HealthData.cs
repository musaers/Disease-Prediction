using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace HeartDiseaseAnalysis.Models
{
    public class HealthData
    {
        
        public bool  heartdisease { get; set; }
        
        public float bmi { get; set; }
        public bool smoking { get; set; }
        public bool alcoholdrinking { get; set; }
        public bool stroke { get; set; }
        public int physicalhealth { get; set; }
        public int mentalhealth { get; set; }
        public bool diffwalking { get; set; }
        public string? sex { get; set; }
        public string? agecategory { get; set; }
        
        
        
        public string? race { get; set; }
        public bool diabetic { get; set; }
        public bool physicalactivity { get; set; }
        public int genhealth { get; set; }
        public double sleeptime { get; set; }
        public bool asthma { get; set; }
        public bool kidneydisease { get; set; }
        public bool skincancer { get; set; }
    }
}