using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class SmokeAnalysisService
{
    private readonly ApplicationDbContext _context;

    public SmokeAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(object ChartData, string AnalysisSummary)> GetSmokeAnalysisWithSummaryAsync()
    {
        var smokedata = await _context.healthdata
            .Select(d => new
            {
                Smoke = Convert.ToInt32(d.smoking) == 0 ? "Non-smoker" : "Smoker",
                HeartDisease = Convert.ToInt32(d.heartdisease)
            })
            .GroupBy(d => d.Smoke)
            .Select(g => new
            {
                Smoke = g.Key,
                HeartDiseaseRate = g.Average(d => d.HeartDisease)
            })
            .OrderBy(d => d.Smoke)
            .ToListAsync();

        // Sigara içenler ve içmeyenler arasındaki oran farkını hesaplama
        var nonSmokerRate = smokedata.FirstOrDefault(d => d.Smoke == "Non-smoker")?.HeartDiseaseRate ?? 0;
        var smokerRate = smokedata.FirstOrDefault(d => d.Smoke == "Smoker")?.HeartDiseaseRate ?? 0;

        double increaseRate = 0;
        if (nonSmokerRate > 0) 
        {
            increaseRate = ((smokerRate - nonSmokerRate) / nonSmokerRate) * 100;
        }

        string analysisSummary = $"Smoking increases the risk of heart disease by {increaseRate:F2}% compared to non-smokers.";

        var chartData = new
        {
            labels = smokedata.Select(d => d.Smoke).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Heart Disease Rate by Smoking Status",
                    data = smokedata.Select(d => d.HeartDiseaseRate).ToArray(),
                    backgroundColor = new[]
                    {
                        "rgba(255, 2, 25, 0.2)",
                        "rgba(10, 10, 10, 0.2)"
                    },
                    borderColor = new[]
                    {
                        "rgba(255, 2, 25, 1)",
                        "rgba(10, 10, 10, 1)"
                    },
                    borderWidth = 1
                }
            }
        };

        return (chartData, analysisSummary);
    }
}

