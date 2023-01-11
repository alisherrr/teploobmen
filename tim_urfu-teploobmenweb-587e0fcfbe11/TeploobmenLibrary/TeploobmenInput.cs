using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeploobmenLibrary
{
    public class TeploobmenInput
    {

        public string Name { get; set; }

        public double InitTemperatureMaterial { get; set; }
        public double InitTemperatureGas { get; set; }
        public double SpeedGas { get; set; }
        public double MiddleHeatСapacity { get; set; }
        public double ConsumptionMaterial { get; set; }
        public double HeatСapacityMaterial { get; set; }
        public double VolumetricHeatTransferCoefficient { get; set; }
        public double ApparatusDiameter { get; set; }
        


    }
}
