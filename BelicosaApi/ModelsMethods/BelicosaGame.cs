using BelicosaApi.Models;

namespace BelicosaApi.Models
{
    public partial class BelicosaGame
    {
        public Color GetRandomAvailableColor()
        {
            Color ChosenColor = AvailableColors.ElementAt(new Random().Next(AvailableColors.Count));
            AvailableColors.Remove(ChosenColor);
            return ChosenColor;
        }
    }
}
