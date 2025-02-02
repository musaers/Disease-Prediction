using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class DiabetesAnalysisService
{
    private readonly ApplicationDbContext _context;

    public DiabetesAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(object ChartData, string AnalysisSummary)> GetDiabetesAnalysisWithSummaryAsync()
    {
        
        var diabetesdata = await _context.healthdata
            .Select(d=> new
            {
                diabetes = Convert.ToInt32(d.diabetic) == 0 ? "Healthy":
                "Diabetic",


                HeartDisease = Convert.ToInt32(d.heartdisease)
            })
            .GroupBy(d => d.diabetes)
            .Select(g => new
            {
                diabetes = g.Key,
                HeartDiseaseRate = g.Average(d => d.HeartDisease )

            })
            .OrderBy(d=> d.diabetes)
            
            .ToListAsync();

        // Sigara içenler ve içmeyenler arasındaki oran farkını hesaplama
        var nondiabeticRate = diabetesdata.FirstOrDefault(d => d.diabetes == "Healthy")?.HeartDiseaseRate ?? 0;
        var diabeticRate = diabetesdata.FirstOrDefault(d => d.diabetes == "Diabetic")?.HeartDiseaseRate ?? 0;

        double increaseRate = 0;
        if (nondiabeticRate > 0) 
        {
            increaseRate = ((diabeticRate - nondiabeticRate) / nondiabeticRate) * 100;
        }

        string analysisSummary = $"Diabetes increases the risk of heart disease by {increaseRate:F2}% compared to healthy .";



        var chartData = new
        {
            labels = diabetesdata.Select(d => d.diabetes).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Heart Disease Rate by Diabetes",
                    data = diabetesdata.Select(d => d.HeartDiseaseRate).ToArray(),
                    backgroundColor = new[]
                    {
                          
                        "rgba(255, 99, 132, 0.3)",   // Red
                        "rgba(54, 162, 235, 0.3)",   // Gray
                         
                    },

                    borderColor = new[]
                    {
                        "rgba(255, 99, 132, 0.3)",   // Red
                        "rgba(54, 162, 235, 0.3)",   // Blue
                          
                    },

                    borderWidth = 1
                }
            }
        };
        return (chartData, analysisSummary);

    
    }
    
}

