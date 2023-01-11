
namespace TeploobmenLibrary
{
    public class TeploobmenSolver
    {
        public double InitTemperatureMaterial { get; set; }
        public double InitTemperatureGas { get; set; }
        public double SpeedGas { get; set; }
        public double MiddleHeatСapacity { get; set; }
        public double ConsumptionMaterial { get; set; }
        public double HeatСapacityMaterial { get; set; }
        public double VolumetricHeatTransferCoefficient { get; set; }
        public double ApparatusDiameter { get; set; }
        public TeploobmenSolver(TeploobmenInput input)
        {
            InitTemperatureMaterial = input.InitTemperatureMaterial;
            InitTemperatureGas = input.InitTemperatureGas;
            SpeedGas = input.SpeedGas;
            MiddleHeatСapacity = input.MiddleHeatСapacity;
            ConsumptionMaterial = input.ConsumptionMaterial;
            HeatСapacityMaterial = input.HeatСapacityMaterial;
            VolumetricHeatTransferCoefficient = input.VolumetricHeatTransferCoefficient;
            ApparatusDiameter = input.ApparatusDiameter;
        }

        public TeploobmenOutput Solve()
        {
            double[] ys = new double[7];
            var height = 3;

            for(int i = 0; i <= 6; i=i+1)
            {
                ys[i] = 0.5 * i;
            }
            double m = (HeatСapacityMaterial * ConsumptionMaterial) /(SpeedGas * MiddleHeatСapacity * Math.PI * Math.Pow(ApparatusDiameter, 2) / 4);
            Console.WriteLine(m);
            Console.WriteLine(HeatСapacityMaterial);
            Console.WriteLine(MiddleHeatСapacity);
            Console.WriteLine(SpeedGas);
            Console.WriteLine(ConsumptionMaterial);
            double Y0 = (VolumetricHeatTransferCoefficient * height) / (SpeedGas * MiddleHeatСapacity * 1000);

            var model = new TeploobmenOutput()
            {
                Rows = new List<TeploobmenOutputRow>()
            };
            double _RelativeHeight;
            double _intermediate1;
            double _intermediate2;
            double _V;
            double _RelativeTempGas;
            double _temp;
            double _Temp;
            double _Difference;

            foreach (var y in ys)
            {
                _RelativeHeight = (VolumetricHeatTransferCoefficient * y) / (SpeedGas * MiddleHeatСapacity) / 1000;
                _intermediate1 = 1 - Math.Exp((m - 1) * (_RelativeHeight/ m));
                _intermediate2 = 1 - m*Math.Exp((m - 1) * (_RelativeHeight / m));
                _V = _intermediate1 / (1 - m * Math.Exp((m - 1) * Y0 / m));
                _RelativeTempGas = _intermediate2 / (1 - m * Math.Exp((m - 1) * Y0 / m));
                _temp = InitTemperatureMaterial + (InitTemperatureGas - InitTemperatureMaterial) * _V;
                _Temp = InitTemperatureMaterial + (InitTemperatureGas - InitTemperatureMaterial) * _RelativeTempGas;
                _Difference = _temp - _Temp;
                var row = new TeploobmenOutputRow
                {
                    Y = y,
                    RelativeHeight = _RelativeHeight,
                    Intermediate1 = _intermediate1,
                    Intermediate2 = _intermediate2,
                    V = _V,
                    RelativeTempGas = _RelativeTempGas,
                    temp = _temp,
                    Temp = _Temp,
                    Difference = _Difference
                };
                model.Rows.Add(row);
            }

            return model;
        }
    }
}