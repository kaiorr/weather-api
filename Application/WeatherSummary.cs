namespace weather_api.Application
{
  public class WeatherSumary : IWeatherSummary
  {
      //DICA: Ao informar a interface, utilizar hotkey control ponto = (ctrl + .), para IDE 
      //apresentar as formar de importação da interface.
    public string[] getSummaries()
    {
        return new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
    }
  }
}