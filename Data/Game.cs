using BlazorPDF.Report;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace BlazorPDF.Data
{
    public class Game : PageModel
    {
        public int Id { get; set; } = 0;
        public int AppId { get; set; } = 0;
        public string Name { get; set; }

        public async void GeneratePDF(IJSRuntime js)
        {
            List<Game> oGames = new List<Game>();
            string pth = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\")); ;

            using (StreamReader r = new StreamReader(pth + "wwwroot/assets/json/steam_games.json"))
            {
                string json = r.ReadToEnd();
                oGames = JsonConvert.DeserializeObject<List<Game>>(json);
            }

            oGames = oGames.OrderBy(x => x.Name).ToList();

            RptGame oRptGames = new RptGame();
            await js.InvokeAsync<Student>(
                "saveAsFile",
                "GamesList.pdf",
                Convert.ToBase64String(oRptGames.Report(oGames))
            );
        }
    }
}
