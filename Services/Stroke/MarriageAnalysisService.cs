using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class MarriageAnalysisService
{
    private readonly ApplicationDbContext _context;

    public MarriageAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(object ChartData, string AnalysisSummary)> GetMarriageAnalysisWithSummaryAsync()
    {
        
        var marriagedata = await _context.stroke
            
            
            .GroupBy(d => d.ever_married)
            .Select(g => new
            {
                marriage= g.Key,
                StrokeRate = g.Average(d => Convert.ToInt32(d.stroke_result))
            })
            .ToListAsync();


        var marriedrate = marriagedata.FirstOrDefault(d => d.marriage == "Yes")?.StrokeRate ?? 0;
        var unmarriedrate= marriagedata.FirstOrDefault(d => d.marriage== "No")?.StrokeRate ?? 0;

        double increaseRate = 0;
        if (unmarriedrate > 0) 
        {
            increaseRate = (( marriedrate- unmarriedrate) / unmarriedrate) * 100;
        }

        string analysisSummary = $"Being married increses stroke posibility by {increaseRate:F2}% compared to unmarried people.";
        var chartData =new
        {
            labels = marriagedata.Select(d => d.marriage).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Stroke Rate by Marriage Status",
                    data = marriagedata.Select(d => d.StrokeRate).ToArray(),
                    backgroundColor = new[]
                    {
                        "rgba(54, 162, 235, 0.3)",
                        "rgba(255, 99, 132, 0.3)",
                        
                        
                    },
                    borderColor = new[]
                    {
                        "rgba(54, 162, 235, 0.8)",
                        "rgba(255, 99, 132, 0.8)",
                        
                    },
                    borderWidth = 1
                }
            }
        };
        return (chartData, analysisSummary);

    }
}

