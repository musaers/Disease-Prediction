using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;


namespace HeartDiseaseAnalysis.Models
{
    public class CancerData
    {
        public int id {get;set;}
        public string? gender {get;set;}
        public string? country {get;set;}
        public string? date1 {get;set;}
        public string? cancerstage {get;set;}
        public string? date2 {get;set;}
        public string? familyhistory{get;set;}
        public string? smokingstatus{get;set;}
        public double bmi {get;set;}
        public int cholesterollevel{get;set;}
        public bool hypertension{get;set;}
        public bool asthma {get;set;}
        public bool cirrhosis {get;set;}
        public bool othercancer{get;set;}
        public string? treatmenttype{get;set;}
        
        public string? date3{get;set;}
        public bool survived{get;set;}

    }
}