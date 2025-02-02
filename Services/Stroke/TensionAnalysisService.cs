using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class TensionAnalysisService
{
    private readonly ApplicationDbContext _context;

    public TensionAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(object ChartData, string AnalysisSummary)> GetTensionAnalysisWithSummaryAsync()
    {
        
        var tensiondata = await _context.stroke
            .Select(d=> new
            {
                tension = Convert.ToInt32(d.hypertension) == 0 ? "Healthy":
                "Hypertension",


                StrokeResult = Convert.ToInt32(d.stroke_result)
            })
            .GroupBy(d => d.tension)
            .Select(g => new
            {
                tension = g.Key,
                StrokeRate = g.Average(d => d.StrokeResult )

            })
            .OrderBy(d=> d.tension)
            
            .ToListAsync();

        var healthyrate= tensiondata.FirstOrDefault(d => d.tension == "0")?.StrokeRate ?? 0;
        var hypertension = tensiondata.FirstOrDefault(d => d.tension == "1")?.StrokeRate ?? 0;

        double increaseRate = 0;
        if (healthyrate > 0) 
        {
            increaseRate = ((hypertension - healthyrate) / healthyrate) * 100;
        }

        string analysisSummary = $"Hypertension increases the risk of stroke by {increaseRate:F2}%";



        var chartData = new
        {
            labels = tensiondata.Select(d => d.tension).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Stroke Rate by Hypertension",
                    data = tensiondata.Select(d => d.StrokeRate).ToArray(),
                    backgroundColor = new[]
                    {
                        "rgba(54, 162, 235, 0.2)",
                        "rgba(255, 99, 132, 0.2)",
                        
                        
                    },
                    borderColor = new[]
                    {
                        "rgba(54, 162, 235, 1)",
                        "rgba(255, 99, 132, 1)"
                        
                        
                    },
                    borderWidth = 1
                }
            }
        };
        return (chartData, analysisSummary);
    }
}

