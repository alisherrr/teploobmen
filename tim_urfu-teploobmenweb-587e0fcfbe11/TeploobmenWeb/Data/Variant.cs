using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeploobmenWeb.Data
{
    public class Variant
    {
        [Key]
        public int Id { get; set; }

        public int? UserId { get; set; }

        public string Name { get; set; }

        public double InitTemperatureMaterial { get; set; }
        public double InitTemperatureGas { get; set; }
        public double SpeedGas { get; set; }
        public double MiddleHeatСapacity { get; set; }
        public double ConsumptionMaterial { get; set; }
        public double HeatСapacityMaterial { get; set; }
        public double VolumetricHeatTransferCoefficient { get; set; }
        public double ApparatusDiameter { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
    }
}
