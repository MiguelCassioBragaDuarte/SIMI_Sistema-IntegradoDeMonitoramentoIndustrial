using SensorInterface.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows.Input;
using System.Windows;
using Shared; // Certifique-se de que o projeto Shared está referenciado

namespace SensorInterface.ViewModels
{
    internal class MainViewModel : BaseViewModel
    {
        // Alterado para usar explicitamente a classe do Shared
        public ObservableCollection<Shared.SensorData> ListaSensores { get; set; }

        public ICommand CarregarSensoresCommand { get; }

        public MainViewModel()
        {
            ListaSensores = new ObservableCollection<Shared.SensorData>();
            CarregarSensoresCommand = new RelayCommand(CarregarSensores);
            CarregarSensores();
        }

        private async void CarregarSensores()
        {
            try
            {
                using var http = new HttpClient();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                // Buscamos os dados usando a classe do Shared
                var dados = await http.GetFromJsonAsync<List<Shared.SensorData>>(
                    "https://localhost:7179/api/v1/sensores", options);

                if (dados != null)
                {
                    // Dispatcher necessário para atualizar a UI a partir de uma tarefa assíncrona
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        ListaSensores.Clear();
                        foreach (var item in dados)
                        {
                            ListaSensores.Add(item);
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao conectar na API: {ex.Message}");
            }
        }
    }
}