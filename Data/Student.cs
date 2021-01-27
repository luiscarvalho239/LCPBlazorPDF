using BlazorPDF.Report;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;

namespace BlazorPDF.Data
{
    public class Student : PageModel
    {
        public int StudentId { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Roll { get; set; } = "";

        public async void GeneratePDF(IJSRuntime js)
        {
            List<Student> oStudents = new List<Student>();
            for(int i = 1; i <= 9; i++)
            {
                oStudents.Add(new Student()
                {
                    StudentId = i,
                    Name = "Stu" + i,
                    Roll = "100" + i
                });
            }
            RptStudent oRptStudent = new RptStudent();
            await js.InvokeAsync<Student>(
                "saveAsFile",
                "StudentList.pdf",
                Convert.ToBase64String(oRptStudent.Report(oStudents))
            );
        }
    }
}
