using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class DeathRateAnalysisServiceByMonth
{
    private readonly ApplicationDbContext _context;

    public DeathRateAnalysisServiceByMonth(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetDeathRateAnalysisByMonthAsync()
    {
        var deathRates = await _context.deaths
        .OrderBy(d => d.months)
        .Select(d => new
        {
            MonthNum = d.months,
            Month = d.months == 1 ? "January":
                    d.months == 2 ? "February":
                    d.months == 3 ? "March":
                    d.months == 4 ? "April":
                    d.months == 5 ? "May":
                    d.months == 6 ? "June":
                    d.months == 7 ? "July":
                    d.months == 8 ? "August":
                    d.months == 9 ? "September":
                    d.months == 10 ? "October":
                    d.months == 11 ? "November" : "December",
            //AllCause = d.allcause,
            //NaturalCause = d.naturalcause,
            Septicemia = d.septicemia,
            MalignantNeoplasm = d.malignantneoplasms,
            DiabetesMellitus = d.diabetesmellitus,
            AlzheimerDisease = d.alzheimerdisease,
            InfluenzaAndPneumonia = d.influenzaandpneumonia,
            ChronicLowerRespiratoryDiseases = d.chroniclowerrespiratorydiseases,
            OtherDiseasesOfRespiratorySystem = d.otherdiseasesofrespiratorysystem,
            NephroticDiseases = d.nephroticdiseases,
            DiseasesOfHeart = d.diseasesofheart,
            CerebrovascularDiseases = d.cerebrovasculardiseases,
            Accidents = d.accidents,
            MotorVehicleAccidents = d.motorvehicleaccidents,
            Suicide = d.suicide,
            AssaultHomicide = d.assaulthomicide,
            DrugOverdose = d.drugoverdose
        })
        .GroupBy(d => new { d.MonthNum, d.Month })
        .Select(g => new 
        {
            MonthNum = g.Key.MonthNum,
            Month = g.Key.Month,
            //AllCause = g.Sum(d => d.AllCause),
            //NaturalCause = g.Sum(d => d.NaturalCause),
            Septicemia = g.Sum(d => d.Septicemia),
            MalignantNeoplasm = g.Sum(d => d.MalignantNeoplasm),
            DiabetesMellitus = g.Sum(d => d.DiabetesMellitus),
            AlzheimerDisease = g.Sum(d => d.AlzheimerDisease),
            InfluenzaAndPneumonia = g.Sum(d => d.InfluenzaAndPneumonia),
            ChronicLowerRespiratoryDiseases = g.Sum(d => d.ChronicLowerRespiratoryDiseases),
            OtherDiseasesOfRespiratorySystem = g.Sum(d => d.OtherDiseasesOfRespiratorySystem),
            NephroticDiseases = g.Sum(d => d.NephroticDiseases),
            DiseasesOfHeart = g.Sum(d => d.DiseasesOfHeart),
            CerebrovascularDiseases = g.Sum(d => d.CerebrovascularDiseases),
            Accidents = g.Sum(d => d.Accidents),
            MotorVehicleAccidents = g.Sum(d => d.MotorVehicleAccidents),
            Suicide = g.Sum(d => d.Suicide),
            AssaultHomicide = g.Sum(d => d.AssaultHomicide),
            DrugOverdose = g.Sum(d => d.DrugOverdose)
        })
        .OrderBy(d => d.MonthNum)
        .ToListAsync();
            

        return new
        {
            labels = deathRates.Select(d => d.Month.ToString()).ToArray(),
            datasets = new[]
            {
                /*new
                {
                    label = "All Cause",
                    data = deathRates.Select(d => d.AllCause).ToArray(),
                    backgroundColor = "rgba(255, 99, 132, 0.2)",
                    borderColor = "rgba(255, 99, 132, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Natural Cause",
                    data = deathRates.Select(d => d.NaturalCause).ToArray(),
                    backgroundColor = "rgba(54, 162, 235, 0.2)",
                    borderColor = "rgba(54, 162, 235, 1)",
                    borderWidth = 1
                },*/
                new
                {
                    label = "Septicemia",
                    data = deathRates.Select(d => d.Septicemia).ToArray(),
                    backgroundColor = "rgba(75, 192, 192, 0.2)",
                    borderColor = "rgba(75, 192, 192, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Malignant Neoplasm",
                    data = deathRates.Select(d => d.MalignantNeoplasm).ToArray(),
                    backgroundColor = "rgba(153, 102, 255, 0.2)",
                    borderColor = "rgba(153, 102, 255, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Diabetes Mellitus",
                    data = deathRates.Select(d => d.DiabetesMellitus).ToArray(),
                    backgroundColor = "rgba(255, 159, 64, 0.2)",
                    borderColor = "rgba(255, 159, 64, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Alzheimer Diseases",
                    data = deathRates.Select(d => d.AlzheimerDisease).ToArray(),
                    backgroundColor = "rgba(255, 205, 86, 0.2)",
                    borderColor = "rgba(255, 205, 86, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Influenza and Pneumonia",
                    data = deathRates.Select(d => d.InfluenzaAndPneumonia).ToArray(),
                    backgroundColor = "rgba(201, 203, 207, 0.2)",
                    borderColor = "rgba(201, 203, 207, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Chronic Respiratory Diseases",
                    data = deathRates.Select(d => d.ChronicLowerRespiratoryDiseases).ToArray(),
                    backgroundColor = "rgba(231, 76, 60, 0.2)",
                    borderColor = "rgba(231, 76, 60, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Other Respiratory Diseases",
                    data = deathRates.Select(d => d.OtherDiseasesOfRespiratorySystem).ToArray(),
                    backgroundColor = "rgba(46, 204, 113, 0.2)",
                    borderColor = "rgba(46, 204, 113, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Nephrotic Diseases",
                    data = deathRates.Select(d => d.NephroticDiseases).ToArray(),
                    backgroundColor = "rgba(52, 152, 219, 0.2)",
                    borderColor = "rgba(52, 152, 219, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Heart Diseases",
                    data = deathRates.Select(d => d.DiseasesOfHeart).ToArray(),
                    backgroundColor = "rgba(155, 89, 182, 0.2)",
                    borderColor = "rgba(155, 89, 182, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Cerebrovascular Diseases",
                    data = deathRates.Select(d => d.CerebrovascularDiseases).ToArray(),
                    backgroundColor = "rgba(41, 128, 185, 0.2)",
                    borderColor = "rgba(41, 128, 185, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Accidents",
                    data = deathRates.Select(d => d.Accidents).ToArray(),
                    backgroundColor = "rgba(255, 99, 132, 0.2)",
                    borderColor = "rgba(255, 99, 132, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Motor Vehicle Accidents",
                    data = deathRates.Select(d => d.MotorVehicleAccidents).ToArray(),
                    backgroundColor = "rgba(39, 174, 96, 0.2)",
                    borderColor = "rgba(39, 174, 96, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Suicide",
                    data = deathRates.Select(d => d.Suicide).ToArray(),
                    backgroundColor = "rgba(189, 195, 199, 0.2)",
                    borderColor = "rgba(189, 195, 199, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Assault (Homicide)",
                    data = deathRates.Select(d => d.AssaultHomicide).ToArray(),
                    backgroundColor = "rgba(44, 62, 80, 0.2)",
                    borderColor = "rgba(44, 62, 80, 1)",
                    borderWidth = 1
                },
                new
                {
                    label = "Drug Overdose",
                    data = deathRates.Select(d => d.DrugOverdose).ToArray(),
                    backgroundColor = "rgba(127, 140, 141, 0.2)",
                    borderColor = "rgba(127, 140, 141, 1)",
                    borderWidth = 1
                },
                
            }
        };

        
    }

}