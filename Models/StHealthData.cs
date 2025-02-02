using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace HeartDiseaseAnalysis.Models
{
    public class StHealthData
    {
        [Key] // This annotation makes Id the primary key
        public int id { get; set; }
        public string? gender { get; set; } 
        public int age { get; set; }
        public bool hypertension { get; set; }
        public bool heart_disease { get; set; }
        public string? ever_married { get; set; } 
        public string? work_type { get; set; }
        public string? residence_type { get; set; }
        public double avg_glucose_level { get; set; }
        public string? smoking_status { get; set; } 
        public bool stroke_result { get; set; }
    }
}