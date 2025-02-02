using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;



public class GenderAnalysisService
{
    private readonly ApplicationDbContext _context;

    public GenderAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(object ChartData, string AnalysisSummary)> GetGenderAnalysisWithSummaryAsync()
    {
        var GenderData = await _context.healthdata
            .GroupBy(d => d.sex)
            .Select(g => new
            {
                GenderData = g.Key,
                HeartDiseaseRate = g.Average(d => Convert.ToInt32(d.heartdisease))
            })
            .ToListAsync();

        var maleRate = GenderData.FirstOrDefault(d => d.GenderData == "Male")?.HeartDiseaseRate ?? 0;
        var womenRate = GenderData.FirstOrDefault(d => d.GenderData == "Female")?.HeartDiseaseRate ?? 0;

        double increaseRate = 0;
        if (womenRate > 0) 
        {
            increaseRate = ((maleRate - womenRate) / womenRate) * 100;
        }

        string analysisSummary = $"Men have higher risk of heart disease by {increaseRate:F2}% compared to women";



        var chartData = new
        {
            labels = GenderData.Select(d => d.GenderData).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Heart Disease Rate by Gender",
                    data = GenderData.Select(d => d.HeartDiseaseRate).ToArray(),
                    backgroundColor = new[]
                    {
                          
                        "rgba(255, 99, 132, 0.4)",   // Red
                        "rgba(54, 162, 235, 0.4)",   // Gray
                         
                    },

                    borderColor = new[]
                    {
                        "rgba(255, 99, 132, 1)",   // Red
                        "rgba(54, 162, 235, 1)",   // Blue
                          
                    },
                    borderWidth = 1
                }
            }
        };
        return (chartData, analysisSummary);
    }
}
