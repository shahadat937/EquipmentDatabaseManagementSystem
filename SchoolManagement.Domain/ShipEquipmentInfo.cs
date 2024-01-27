﻿using SchoolManagement.Domain.Common;
using System;
using System.Collections.Generic;

namespace SchoolManagement.Domain
{
    public class ShipEquipmentInfo : BaseDomainEntity
    {
        public ShipEquipmentInfo()
        {
            //ShipInformations = new HashSet<ShipInformation>();
        }
        
        public int ShipEquipmentInfoId { get; set; }
        public int? BaseSchoolNameId { get; set; }
        public int? EquipmentCategoryId { get; set; }
        public int? EquipmentTypeId { get; set; }
        public int? EqupmentNameId { get; set; }
        public int? BrandId { get; set; }
        public string? Brand { get; set; }
        public int? AcquisitionMethodId { get; set; }
        public int? StateOfEquipmentId { get; set; }
        public double? Qty { get; set; }
        public string? Model { get; set; }
        public string? Voltage { get; set; }
        public string? Ipvoltage { get; set; }
        public string? Opvoltage { get; set; }
        public string? Type { get; set; }
        public string? Power { get; set; }
        public string? Phase { get; set; }
        public string? Frequency { get; set; }
        public string? MaximumUseableAngle { get; set; }
        public string? MaximumRange { get; set; }
        public string? Pf { get; set; }
        public string? Ah { get; set; }
        public string? Cell { get; set; }
        public string? ManufacturerNameAndAddress { get; set; }
        public string? YearOfInstallation { get; set; }
        public string? Location { get; set; }
        public string? PowerSupply { get; set; }
        public string? TiltScanRange { get; set; }
        public string? Avrbrand { get; set; }
        public string? Avrmodel { get; set; }
        public string? GyroCompassFollowUpRate { get; set; }
        public string? HeadingDisplay { get; set; }
        public string? SettlingTime { get; set; }
        public string? ParallelStatus { get; set; }
        public string? Purpose { get; set; }
        public string? DefectDescription { get; set; }
        public string? Composition { get; set; }
        public string? PositionAccuracy { get; set; }
        public string? Range { get; set; }
        public string? DisplayMinimumSourndingDepth { get; set; }
        public string? TxPulsereqetitionRate { get; set; }
        public string? SpeedRange { get; set; }
        public string? DistanceRunRange { get; set; }
        public string? FrequencyPowerSupplyVoltage { get; set; }
        public string? SpeedAccuracy { get; set; }
        public string? SpeedLimit { get; set; }
        public string? AudioableRange { get; set; }
        public string? WorkingAirPressure { get; set; }
        public string? AirPressureCapacity { get; set; }
        public string? PowerOutput { get; set; }
        public string? InputTakenForm { get; set; }
        public string? InterfaceProtocol { get; set; }
        public string? ReceiverType { get; set; }
        public string? Sensitivity { get; set; }
        public string? TimeToFirstFix { get; set; }
        public string? PositionUpdate { get; set; }
        public string? Dgpsinput { get; set; }
        public string? DataOutput { get; set; }
        public string? Accuracy { get; set; }
        public string? Os { get; set; }
        public string? DisplaySize { get; set; }
        public string? Ebl { get; set; }
        public string? Vrm { get; set; }
        public string? BearingIndication { get; set; }
        public string? PresentationMode { get; set; }
        public string? Updating { get; set; }
        public string? DataCorrection { get; set; }
        public string? RoutPlanningSystem { get; set; }
        public string? RangeOfFrequency { get; set; }
        public string? TypeOfModulation { get; set; }
        public string? DefaultChannel { get; set; }
        public string? DataInterface { get; set; }
        public string? Colour { get; set; }
        public string? VisibleDistance { get; set; }
        public string? LampLuminousFlux { get; set; }
        public string? Efficiency { get; set; }
        public string? WindSpeedMeasuringRange { get; set; }
        public string? AccuracyOfTemperature { get; set; }
        public string? AirVelocityUnit { get; set; }
        public string? AverageWindSpeedMeasuring { get; set; }
        public string? SteeringType { get; set; }
        public string? ServiceLifeOfMainEquipment { get; set; }
        public string? NameOfLifeLimeOfImportantSpares { get; set; }
        public string? PresentRunningHours { get; set; }
        public string? ExpectedLeftOverLifeTime { get; set; }
        public string? UpgradationOrOverhaulinRequired { get; set; }
        public string? TobeStoredInNsdctgLatestBuy { get; set; }
        public string? RateOfFire { get; set; }
        public string? NoOfTube { get; set; }
        public string? WeightOfChaff { get; set; }
        public string? NoOfSensor { get; set; }
        public string? TypeOfSensor { get; set; }
        public string? DisplayType { get; set; }
        public string? OperatingFrequency { get; set; }
        public string? HorizontalDetectionRange { get; set; }
        public string? TitlScanRange { get; set; }
        public string? SonarRange { get; set; }
        public string? TransmittedPulseLength { get; set; }
        public string? Interferencerejection { get; set; }
        public string? MinimumRange { get; set; }
        public string? Mass { get; set; }
        public string? Diameter { get; set; }
        public string? EffectiveFiringRange { get; set; }
        public string? EffectiveRange { get; set; }
        public string? TotalLength { get; set; }
        public string? WeightOfMissile { get; set; }
        public string? WeightOfWarhead { get; set; }
        public string? WeightOfFuse { get; set; }
        public string? InterfaceWith { get; set; }
        public string? TakenDataFrom { get; set; }
        public string? MaximumFiringElevation { get; set; }
        public string? MuzzleVelocity { get; set; }
        public string? AmmonationType { get; set; }
        public string? TypeOfPlate { get; set; }
        public string? RateOfInitalCurrent { get; set; }
        public string? Rpm { get; set; }
        public string? Standard { get; set; }
        public string? Inch { get; set; }
        public string? ReqShipsSpeedForOperate { get; set; }
        public string? ReqSeaStateForOperate { get; set; }
        public string? MaxRollingAngle { get; set; }
        public string? RateOfWaterSupply { get; set; }
        public string? Btu { get; set; }
        public string? Capacity { get; set; }
        public string? RateOfFuelTransfer { get; set; }
        public string? Amps { get; set; }
        public string? NoOfCoil { get; set; }
        public string? WaterCapacity { get; set; }
        public string? Output { get; set; }
        public string? NvrorDvrcapacity { get; set; }
        public string? TechSpecification { get; set; }
        public string? FrequencyRange { get; set; }
        public string? RfoutputImpedance { get; set; }
        public string? Filterandwidth { get; set; }
        public string? OperationRange { get; set; }
        public string? Channel { get; set; }
        public string? Mode { get; set; }
        public string? Remarks { get; set; }
        public int? MenuPosition { get; set; }
        public bool IsActive { get; set; }

        public virtual AcquisitionMethod? AcquisitionMethod { get; set; }
        public virtual BaseSchoolName? BaseSchoolName { get; set; }
        //public virtual Brand? Brand { get; set; }
        public virtual EquipmentCategory? EquipmentCategory { get; set; }
        public virtual EqupmentName? EqupmentName { get; set; }
        public virtual EquipmentType? EquipmentType { get; set; }
        public virtual StateOfEquipment? StateOfEquipment { get; set; }
        //public virtual ICollection<ShipInformation> ShipInformations { get; set; }
    }
}
