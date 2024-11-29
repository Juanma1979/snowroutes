using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;

namespace SnowRoutesApp.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string? Start { get; set; } // Nodo inicial
        [BindProperty]
        public string? End { get; set; } // Nodo final

        public double? Distance { get; set; } // Distancia calculada
        public List<Measurement> Measurements { get; set; } = new(); // Lista de mediciones

        public class Measurement
        {
            public string StartNode { get; set; }
            public string EndNode { get; set; }
            public double StartLat { get; set; }
            public double StartLng { get; set; }
            public double EndLat { get; set; }
            public double EndLng { get; set; }
            public double Distance { get; set; }
        }

        // Método para calcular la distancia entre dos coordenadas (en km)
        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371; // Radio de la Tierra en km
            var dLat = (lat2 - lat1) * Math.PI / 180;
            var dLon = (lon2 - lon1) * Math.PI / 180;
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(lat1 * Math.PI / 180) * Math.Cos(lat2 * Math.PI / 180) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        // Método que maneja el formulario cuando el usuario lo envía
        public void OnPost()
        {
            if (!string.IsNullOrEmpty(Start) && !string.IsNullOrEmpty(End))
            {
                // Generamos una medición ficticia
                var startLat = 40.4168; // Simulamos coordenadas (puedes cambiar esto)
                var startLng = -3.7038;
                var endLat = 40.7128;
                var endLng = -74.0060;

                var distance = CalculateDistance(startLat, startLng, endLat, endLng);

                Measurements.Add(new Measurement
                {
                    StartNode = Start,
                    EndNode = End,
                    StartLat = startLat,
                    StartLng = startLng,
                    EndLat = endLat,
                    EndLng = endLng,
                    Distance = distance
                });
            }
        }

        // Eliminar medición (por índice)
        public IActionResult OnPostDeleteMeasurement(int index)
        {
            if (index >= 0 && index < Measurements.Count)
            {
                Measurements.RemoveAt(index); // Eliminar el elemento
            }
            return RedirectToPage();
        }
    }
}
