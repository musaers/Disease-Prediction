using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using HeartDiseaseAnalysis.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;


public class WorkAnalysisService
{
    private readonly ApplicationDbContext _context;

    public WorkAnalysisService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<object> GetWorkAnalysisAsync()
    {
        var workData = await _context.stroke
            .GroupBy(d => d.work_type)
            .Select(g => new
            {
                workData= g.Key,
                StrokeRate = g.Average(d => Convert.ToInt32(d.stroke_result))
            })
            .ToListAsync();

        return new
        {
            labels = workData.Select(d => d.workData).ToArray(),
            datasets = new[]
            {
                new
                {
                    label = "Stroke Rate by Employement type",
                    data = workData.Select(d => d.StrokeRate).ToArray(),
                    backgroundColor = new[]
                    {
                        
                        "rgba(54, 162, 235, 0.2)",
                        "rgba(255, 206, 86, 0.2)",
                        "rgba(75, 192, 192, 0.2)",
                        "rgba(255, 99, 132, 0.2)",
                        "rgba(153, 102, 255, 0.2)",
                        
                    },
                    borderColor = new[]
                    {
                        
                        "rgba(54, 162, 235, 1)",
                        "rgba(255, 206, 86, 1)",
                        "rgba(75, 192, 192, 1)",
                        "rgba(255, 99, 132, 0.2)",
                        "rgba(153, 102, 255, 1)",
                        
                    },
                    borderWidth = 1
                }
            }
        };
    }
}
