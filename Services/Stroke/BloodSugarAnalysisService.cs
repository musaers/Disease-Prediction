using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class BloodSugarAnalysisService
{
    private readonly ApplicationDbContext _context;

    public BloodSugarAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetBloodSugarAnalysisAsync()
    {
        var groupedData = await _context.stroke
                .Select(d => new
                {
                    BloodSugar= d.avg_glucose_level < 100 ? "Hypoglisemi (<100)" :
                    d.avg_glucose_level >= 100 && d.avg_glucose_level <= 140 ? "Normal (100-140)" : 
                    d.avg_glucose_level >= 140.1 && d.avg_glucose_level <= 199 ? "Diabetes Inspudus (140-199)" :
                    
                    "Diabetic (200+)",
                    StrokeResult = Convert.ToInt32(d.stroke_result)
                })
                .GroupBy(d => d.BloodSugar)
                .Select(g => new
                {
                    BloodSugar = g.Key,
                    StrokeRate = g.Average(d => d.StrokeResult)
                })
                .ToListAsync();

        return new
        {
            labels = groupedData.Select(d => d.BloodSugar).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Stroke Rate by Blood Sugar Level",
                    data = groupedData.Select(d => d.StrokeRate).ToArray(),
                    backgroundColor = new[]
                    {
                        "rgba(255, 99, 132, 0.2)",
                        "rgba(54, 162, 235, 0.2)",
                        
                        
                        "rgba(255, 159, 64, 0.2)",
                        "rgba(153, 102, 255, 0.2)",
                    },
                    borderColor = new[]
                    {
                        "rgba(255, 99, 132, 1)",
                        "rgba(54, 162, 235, 1)",
                        
                        "rgba(255, 159, 64, 1)",
                        "rgba(153, 102, 255, 1)",
                    },
                    
                    borderWidth = 1
                }
            }
        };
    }
}
