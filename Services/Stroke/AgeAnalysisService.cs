using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class AgeAnalysisService
{
    private readonly ApplicationDbContext _context;

    public AgeAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetAgeAnalysisAsync()
    {
        var groupedData = await _context.stroke
                .Select(d => new
                {
                    Age= d.age < 18 ? "Child (<18)" :
                    d.age >= 18 && d.age <= 25 ? "18-25" : 
                    d.age >= 26 && d.age <= 30 ? "26-30" :
                    d.age >= 31 && d.age <= 35 ? "31-35" :
                    d.age >= 36 && d.age <= 40 ? "36-40" :
                    d.age >= 41 && d.age <= 45 ? "41-45" :
                    d.age >= 46 && d.age <= 50 ? "46-50" :
                    d.age >= 51 && d.age <= 55 ? "51-55" :
                    d.age >= 56 && d.age <= 60 ? "56-60" :
                    d.age >= 61 && d.age <= 65 ? "61-65" :
                    d.age >= 66 && d.age <= 70 ? "66-70" :
                    d.age >= 71 && d.age <= 75 ? "71-75" :
                    
                    
                    
                    "76+",
                    StrokeResult = Convert.ToInt32(d.stroke_result)
                })
                .GroupBy(d => d.Age)
                .Select(g => new
                {
                    Age = g.Key,
                    StrokeRate = g.Average(d => d.StrokeResult)
                })
                .ToListAsync();

        return new
        {
            labels = groupedData.Select(d => d.Age).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Stroke Rate by Age",
                    data = groupedData.Select(d => d.StrokeRate).ToArray(),
                    backgroundColor = new[]
                    {
                        "rgba(255, 99, 132, 0.2)",   // Red
                        "rgba(54, 162, 235, 0.2)",   // Blue
                        "rgba(255, 206, 86, 0.2)",   // Yellow
                        "rgba(75, 192, 192, 0.2)",   // Teal
                        "rgba(153, 102, 255, 0.2)",  // Purple
                        "rgba(255, 159, 64, 0.2)",   // Orange
                        "rgba(201, 203, 207, 0.2)",  // Gray
                        "rgba(105, 180, 132, 0.2)",  // Green
                        "rgba(255, 99, 255, 0.2)",   // Pink
                        "rgba(102, 153, 255, 0.2)",  // Light Blue
                        "rgba(255, 229, 100, 0.2)",  // Light Yellow
                        "rgba(255, 102, 178, 0.2)",  // Light Pink
                        "rgba(64, 159, 255, 0.2)"    // Cyan
                    },

                    borderColor = new[]
                    {
                        "rgba(255, 99, 132, 1)",   // Red
                        "rgba(54, 162, 235, 1)",   // Blue
                        "rgba(255, 206, 86, 1)",   // Yellow
                        "rgba(75, 192, 192, 1)",   // Teal
                        "rgba(153, 102, 255, 1)",  // Purple
                        "rgba(255, 159, 64, 1)",   // Orange
                        "rgba(201, 203, 207, 1)",  // Gray
                        "rgba(105, 180, 132, 1)",  // Green
                        "rgba(255, 99, 255, 1)",   // Pink
                        "rgba(102, 153, 255, 1)",  // Light Blue
                        "rgba(255, 229, 100, 1)",  // Light Yellow
                        "rgba(255, 102, 178, 1)",  // Light Pink
                        "rgba(64, 159, 255, 1)"    // Cyan
                    },
                    borderWidth = 1
                }
            }
        };
    }
}
