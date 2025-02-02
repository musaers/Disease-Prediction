using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;



public class SGenderAnalysisService
{
    private readonly ApplicationDbContext _context;

    public SGenderAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(object ChartData, string AnalysisSummary)> GetSGenderAnalysisWithSummaryAsync()
    {
        var GenderData = await _context.stroke
            .GroupBy(d => d.gender)
            .Select(g => new
            {
                GenderData = g.Key,
                StrokeRate = g.Average(d => Convert.ToInt32(d.stroke_result))
            })
            .ToListAsync();

        var maleRate = GenderData.FirstOrDefault(d => d.GenderData == "Male")?.StrokeRate ?? 0;
        var womenRate = GenderData.FirstOrDefault(d => d.GenderData == "Female")?.StrokeRate ?? 0;

        double increaseRate = 0;
        if (womenRate > 0) 
        {
            increaseRate = ((maleRate - womenRate) / womenRate) * 100;
        }

        string analysisSummary = $"Men have higher risk of stroke by {increaseRate:F2}% compared to women";
    

        var chartData = new
        {
            labels = GenderData.Select(d => d.GenderData).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Stroke Rate by Gender",
                    data = GenderData.Select(d => d.StrokeRate).ToArray(),
                    backgroundColor = new[]
                    {
                        "rgba(255, 99, 132, 0.2)",
                        "rgba(54, 162, 235, 0.2)",
                        
                    },
                    borderColor = new[]
                    {
                        "rgba(255, 99, 132, 1)",
                        "rgba(54, 162, 235, 1)",
                        
                    },
                    borderWidth = 1
                }
            }
        };
        return (chartData, analysisSummary);
    }
}
