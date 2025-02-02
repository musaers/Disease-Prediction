using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace HeartDiseaseAnalysis.Models
{
    public class DeathData
    {
        public int id {get; set;}
        public string? country {get;set;}

        public int years {get;set;}

        public int months {get;set;}
        public int allcause {get;set;}
        public int naturalcause {get;set;}

        public int septicemia {get;set;}
        public int malignantneoplasms {get;set;}

        public int diabetesmellitus {get;set;}
        public int alzheimerdisease {get;set;}
        public int influenzaandpneumonia {get;set;}
        public int chroniclowerrespiratorydiseases {get;set;}
        public int otherdiseasesofrespiratorysystem {get;set;}
        public int nephroticdiseases {get;set;}

        public int notclassified {get;set;}

        public int diseasesofheart {get;set;}

        public int cerebrovasculardiseases {get;set;}
        public int accidents {get;set;}
        public int motorvehicleaccidents {get;set;}
        public int suicide {get;set;}
        public int assaulthomicide {get;set;}
        public int drugoverdose {get;set;}
    }

}