import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ShipEquipmentInfoService } from '../../service/ShipEquipmentInfo.service';
import { SelectedModel } from 'src/app/core/models/selectedModel';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ConfirmService } from '../../../core/service/confirm.service';
import { AuthService } from 'src/app/core/service/auth.service';
import { MasterData } from 'src/assets/data/master-data';
import { Role } from 'src/app/core/models/role';

@Component({
  selector: 'app-new-shipequipmentinfo',
  templateUrl: './new-shipequipmentinfo.component.html',
  styleUrls: ['./new-shipequipmentinfo.component.sass']
})
export class NewShipEquipmentInfoComponent implements OnInit {
  masterData = MasterData;
  userRole = Role;
  pageTitle: string;
  destination:string;
  btnText:string;
  ShipEquipmentInfoForm: FormGroup;
  validationErrors: string[] = [];
  selectedModel:SelectedModel[]; 
  selectedBaseName:SelectedModel[];
  selectedBranchLevel:SelectedModel[];
  selectedBaseSchoolName:SelectedModel[];
  selectSchoolName:SelectedModel[];
  selectedEquipmentCategory:SelectedModel[];
  selectEquipmentCategory:SelectedModel[];
  selectedEqupmentName:SelectedModel[];
  selectEquipmentName:SelectedModel[];
  selectedBrand:SelectedModel[];
  selectedStateOfEquipment:SelectedModel[];
  selectedAcquisitionMethod:SelectedModel[];
  selectAcuisitionMethod:SelectedModel[];
  selectedEquipmentType:SelectedModel[];
  selectEquipmentType:SelectedModel[];
  traineeId:any;
  role:any;
  branchId:any;

  equpmentNameId:any;

  common:boolean=false;
  PMS:boolean=false;
  Generator:boolean=false;
  Battery:boolean=false;
  GyroCompass:boolean=false;
  EWSystem:boolean=false;
  Sonar:boolean=false;
  ControlSystem:boolean=false;
  Portable:boolean=false;
  SREBroadcastSystem:boolean=false;
  ICS:boolean=false;
  TelephoneExchange:boolean=false;
  SoundPoweredTelephone:boolean=false;
  UnderWaterTelephoneSet:boolean=false;
  Torpedo:boolean=false;
  ESM:boolean=false;
  TrueLyingRuder:boolean=false;
  EWSystemChaff:boolean=false;
  GUN:boolean=false;
  RDC:boolean=false;
  CombatSystem:boolean=false;
  NavigationRadar:boolean=false;
  ECDIS:boolean=false;
  AIS:boolean=false;
  GPS:boolean=false;
  SpeedLog:boolean=false;
  EchoSounder:boolean=false;
  Transformer:boolean=false;
  DCPowerSystem:boolean=false;
  MagneticCompass:boolean=false;
  AirWhistle:boolean=false;
  ElectricHorn:boolean=false;
  DegaussingSystem:boolean=false;
  TrackingRadar:boolean=false;
  SearchRadar:boolean=false;
  HelControlRadar:boolean=false;
  SurveillanceRadar:boolean=false;
  SearchLight:boolean=false;
  Animometer:boolean=false;
  AutoPilotSystem:boolean=false;
  SteeringSystem:boolean=false;
  ECM:boolean=false;
  OFC3:boolean=false;
  Chaff:boolean=false;
  TDL:boolean=false;
  IFF:boolean=false;
  NBC:boolean=false;
  SSM:boolean=false;
  SAM:boolean=false;
  FCU:boolean=false;
  TDS:boolean=false;
  PCE : boolean = false;
  HighFrequencySet:boolean=false;
  VeryHighFrequencySet:boolean=false;
  UltraHighFrequencySet:boolean=false;
  WalkieTalkieSet:boolean=false;
  CombatManagementSystem:boolean=false;
  WeaponGunSystem:boolean=false;
  PropulsionMonitoringControlSystem:boolean=false;
  ADCMS:boolean=false;
  ICCPSystem:boolean=false;
  AntiFoulingSystem:boolean=false;
  FirePump:boolean=false;
  CPPSystem:boolean=false;
  GearBox:boolean=false;
  FireAlarmSystem:boolean=false;
  Telivision:boolean=false;
  AllMotor:boolean=false;
  HPAirCompressor:boolean=false;
  HelicopterPowerSupplySystem:boolean=false;
  FINStabilizer:boolean=false;
  ROPlant:boolean=false;
  ASGSI:boolean=false;
  UVSterilizer:boolean=false;
  ACPlant:boolean=false;
  DomesticFridge:boolean=false;
  PackageAC:boolean=false;
  HelicopterStaticLandingAIDLightSystem:boolean=false;
  FridgePlant:boolean=false;
  SHRS:boolean=false;
  LandingLights:boolean=false;
  SubmersiblePump:boolean=false;
  CapstonMotor:boolean=false;
  HotPlate:boolean=false;
  RiceCooker:boolean=false;
  AviationFoulingSystem:boolean=false;
  CargoLift:boolean=false;
  MarineCombaiOven:boolean=false;
  BoatDavit:boolean=false;
  DeepFatFryer:boolean=false;
  ShoreConnectionBox:boolean=false;
  StreamBox:boolean=false;
  SeaWaterHydrophorePumpMotor:boolean=false;
  DishWashingMachine:boolean=false;
  UniversalKitchenMachine:boolean=false;
  FreshWaterHydrophorePumpMotor:boolean=false;          
  WaterBoiler:boolean=false;
  HotWaterCalorifier:boolean=false;
  GeneralBroadcast:boolean=false;
  STP:boolean=false;
  WashingMachine:boolean=false;
  IntercomSystem:boolean=false;
  DyerMachine:boolean=false;
  CCTVSystem:boolean=false;
  WeldingMachine:boolean=false;
  AllExhaustFanMotor:boolean=false;
  AllSupplyFanMotor:boolean=false;
  WasteDisposerMachine:boolean=false;

  constructor(private snackBar: MatSnackBar,private authService: AuthService,private confirmService: ConfirmService,private ShipEquipmentInfoService: ShipEquipmentInfoService,private fb: FormBuilder, private router: Router,  private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.role = this.authService.currentUserValue.role.trim();
    this.traineeId =  this.authService.currentUserValue.traineeId.trim();
    this.branchId =  this.authService.currentUserValue.branchId.trim();

    const id = this.route.snapshot.paramMap.get('shipEquipmentInfoId'); 
    if (id) {
      this.pageTitle = 'Edit Ship Equipment Info';
      this.destination = "Edit";
      this.btnText = 'Update';
      this.ShipEquipmentInfoService.find(+id).subscribe(
        res => {
          console.log(res);
          this.ShipEquipmentInfoForm.patchValue({          

            shipEquipmentInfoId: res.shipEquipmentInfoId,
            baseSchoolNameId: res.baseSchoolNameId,
            equipmentCategoryId: res.equipmentCategoryId,
            equpmentNameId: res.equpmentNameId,
            equipmentTypeId:res.equipmentTypeId,
            brandId: res.brandId,
            brand:res.brand,
            acquisitionMethodId: res.acquisitionMethodId,
            stateOfEquipmentId: res.stateOfEquipmentId,
            qty: res.qty,
            model: res.model,
            voltage: res.voltage,
            ipvoltage: res.ipvoltage,
            opvoltage: res.opvoltage,
            type: res.type,
            power: res.power,
            phase: res.phase,
            frequency: res.frequency,
            kva: res.kva,
            maximumUseableAngle: res.maximumUseableAngle,
            maximumRange: res.maximumRange,
            pf: res.pf,
            ah: res.ah,
            cell: res.cell,
            manufacturerNameAndAddress: res.manufacturerNameAndAddress,
            yearOfInstallation: res.yearOfInstallation,
            location: res.location,
            powerSupply: res.powerSupply,
            tiltScanRange: res.tiltScanRange,
            avrbrand: res.avrbrand,
            avrmodel: res.avrmodel,
            gyroCompassFollowUpRate: res.gyroCompassFollowUpRate,
            headingDisplay: res.headingDisplay,
            settlingTime: res.settlingTime,
            parallelStatus: res.parallelStatus,
            purpose: res.purpose,
            defectDescription: res.defectDescription,
            composition: res.composition,
            positionAccuracy: res.positionAccuracy,
            range: res.range,
            displayMinimumSourndingDepth: res.displayMinimumSourndingDepth,
            txPulsereqetitionRate: res.txPulsereqetitionRate,
            speedRange: res.speedRange,
            distanceRunRange: res.distanceRunRange,
            frequencyPowerSupplyVoltage: res.frequencyPowerSupplyVoltage,
            speedAccuracy: res.speedAccuracy,
            speedLimit: res.speedLimit,
            audioableRange: res.audioableRange,
            workingAirPressure: res.workingAirPressure,
            airPressureCapacity: res.airPressureCapacity,
            powerOutput: res.powerOutput,
            inputTakenForm: res.inputTakenForm,
            interfaceProtocol: res.interfaceProtocol,
            receiverType: res.receiverType,
            sensitivity: res.sensitivity,
            timeToFirstFix: res.timeToFirstFix,
            positionUpdate: res.positionUpdate,
            dgpsinput: res.dgpsinput,
            dataOutput: res.dataOutput,
            accuracy: res.accuracy,
            os: res.os,
            displaySize: res.displaySize,
            ebl: res.ebl,
            vrm: res.vrm,
            bearingIndication: res.bearingIndication,
            presentationMode: res.presentationMode,
            updating: res.updating,
            dataCorrection: res.dataCorrection,
            routPlanningSystem: res.routPlanningSystem,
            rangeOfFrequency: res.rangeOfFrequency,
            typeOfModulation: res.typeOfModulation,
            defaultChannel: res.defaultChannel,
            dataInterface: res.dataInterface,
            colour: res.colour,
            visibleDistance: res.visibleDistance,
            lampLuminousFlux: res.lampLuminousFlux,
            efficiency: res.efficiency,
            windSpeedMeasuringRange: res.windSpeedMeasuringRange,
            accuracyOfTemperature: res.accuracyOfTemperature,
            airVelocityUnit: res.airVelocityUnit,
            averageWindSpeedMeasuring: res.averageWindSpeedMeasuring,
            steeringType: res.steeringType,
            serviceLifeOfMainEquipment: res.serviceLifeOfMainEquipment,
            stanameOfLifeLimeOfImportantSparestus: res.nameOfLifeLimeOfImportantSpares,
            presentRunningHours: res.presentRunningHours,
            expectedLeftOverLifeTime: res.expectedLeftOverLifeTime,
            upgradationOrOverhaulinRequired: res.upgradationOrOverhaulinRequired,
            tobeStoredInNsdctgLatestBuy: res.tobeStoredInNsdctgLatestBuy,
            rateOfFire: res.rateOfFire,
            noOfTube: res.noOfTube,
            weightOfChaff: res.weightOfChaff,
            noOfSensor: res.noOfSensor,
            typeOfSensor: res.typeOfSensor,
            displayType: res.displayType,
            operatingFrequency: res.operatingFrequency,
            horizontalDetectionRange: res.horizontalDetectionRange,
            sonarRange: res.sonarRange,
            transmittedPulseLength: res.transmittedPulseLength,
            titlScanRange: res.titlScanRange,
            interferencerejection: res.interferencerejection,
            minimumRange: res.minimumRange,
            mass: res.mass,
            diameter: res.diameter,
            effectiveFiringRange: res.effectiveFiringRange,
            effectiveRange: res.effectiveRange,
            totalLength: res.totalLength,
            weightOfMissile: res.weightOfMissile,
            weightOfWarhead: res.weightOfWarhead,
            weightOfFuse: res.weightOfFuse,
            interfaceWith: res.interfaceWith,
            takenDataFrom: res.takenDataFrom,
            maximumFiringElevation: res.maximumFiringElevation,
            muzzleVelocity: res.muzzleVelocity,
            ammonationType: res.ammonationType,
            typeOfPlate: res.typeOfPlate,
            rateOfInitalCurrent: res.rateOfInitalCurrent,
            rpm: res.rpm,
            standard: res.standard,
            inch: res.inch,
            reqShipsSpeedForOperate: res.reqShipsSpeedForOperate,
            reqSeaStateForOperate: res.reqSeaStateForOperate,
            maxRollingAngle: res.maxRollingAngle,
            rateOfWaterSupply: res.rateOfWaterSupply,
            btu: res.btu,
            capacity: res.capacity,
            rateOfFuelTransfer: res.rateOfFuelTransfer,
            amps: res.amps,
            noOfCoil: res.noOfCoil,
            waterCapacity: res.waterCapacity,
            output: res.output,
            nvrorDvrcapacity: res.nvrorDvrcapacity,
            techSpecification: res.techSpecification,
            frequencyRange: res.frequencyRange,
            rfoutputImpedance: res.rfoutputImpedance,
            filterandwidth: res.filterandwidth,
            operationRange: res.operationRange,
            channel: res.channel,
            mode:res.mode,
            remarks: res.remarks,
            menuPosition: res.menuPosition,
            isActive: res.isActive,
            
          });   
          this.onEquipmentCategorySelectionChangeGetequipmentName();       
          this.getequipmentName();       
        }
      );
    } else {
      this.pageTitle = 'Create Ship Equipment Info';
      this.destination = "Add";
      this.btnText = 'Save';
    }
    this.intitializeForm();

    if(this.role == this.userRole.ShipStaff || this.role == this.userRole.LOEO){
      this.ShipEquipmentInfoForm.get('baseSchoolNameId').setValue(this.branchId);
      // this.baseSchoolNameService.find(this.branchId).subscribe(res=>{
      //   this.MonthlyReturnForm.get('baseNameId').setValue(res.thirdLevel);
      //   this.MonthlyReturnForm.get('authorityId').setValue(res.secondLevel);
      // });
    }


    // this.getSelectedBaseName(this.branchId);
    this.getSelectedSchoolByBranchLevelAndThirdLevel();
    this.getSelectedStateOfEquipment();
    this.getSelectedEquipmentCategory();
    // this.getSelectedEqupmentName();
    this.getSelectedEquipmentType();
    this.getSelectedBrand();
    this.getSelectedAcquisitionMethod();
  }
  intitializeForm() {
    this.ShipEquipmentInfoForm = this.fb.group({
      shipEquipmentInfoId: [0],
      baseSchoolNameId: [],
      equipmentCategoryId: [],
      equpmentNameId: [],
      equipmentTypeId:[],
      brandId: [],
      brand:[],
      acquisitionMethodId:[],
      stateOfEquipmentId: [''],
      qty: [''],
      model:  [''],
      voltage:  [''],
      power: [''],
      phase: [''],
      frequency: [''],
      kva : [''],
      pf: [''],
      manufacturerNameAndAddress: [''],
      yearOfInstallation: [''],
      location: [''],
      avrmodel: [''],
      avrbrand: [''],
      parallelStatus: [''],
      purpose: [''],
      defectDescription: [''],
      ipvoltage: [''],
      opvoltage: [''],
      type:  [''],
      maximumUseableAngle: [''],
      maximumRange: [''],
      ah: [''],
      cell: [''],
      powerSupply: [''],
      tiltScanRange: [''],
      gyroCompassFollowUpRate: [''],
      headingDisplay: [''],
      settlingTime: [''],
      composition: [''],
      positionAccuracy: [''],
      range: [''],
      displayMinimumSourndingDepth: [''],
      txPulsereqetitionRate: [''],
      speedRange: [''],
      distanceRunRange: [''],
      frequencyPowerSupplyVoltage: [''],
      speedAccuracy: [''],
      speedLimit: [''],
      audioableRange: [''],
      workingAirPressure: [''],
      airPressureCapacity: [''],
      powerOutput: [''],
      inputTakenForm: [''],
      interfaceProtocol: [''],
      receiverType: [''],
      sensitivity: [''],
      timeToFirstFix: [''],
      positionUpdate: [''],
      dgpsinput: [''],
      dataOutput: [''],
      accuracy: [''],
      os: [''],
      displaySize: [''],
      ebl: [''],
      vrm: [''],
      bearingIndication: [''],
      presentationMode: [''],
      updating: [''],
      dataCorrection: [''],
      routPlanningSystem: [''],
      rangeOfFrequency: [''],
      typeOfModulation: [''],
      defaultChannel: [''],
      dataInterface: [''],
      colour: [''],
      visibleDistance: [''],
      lampLuminousFlux: [''],
      efficiency: [''],
      windSpeedMeasuringRange: [''],
      accuracyOfTemperature: [''],
      airVelocityUnit: [''],
      averageWindSpeedMeasuring: [''],
      steeringType: [''],
      serviceLifeOfMainEquipment: [''],
      nameOfLifeLimeOfImportantSpares: [''],
      presentRunningHours: [''],
      expectedLeftOverLifeTime: [''],
      upgradationOrOverhaulinRequired: [''],
      tobeStoredInNsdctgLatestBuy: [''],
      rateOfFire: [''],
      noOfTube: [''],
      weightOfChaff: [''],
      noOfSensor: [''],
      typeOfSensor: [''],
      displayType: [''],
      operatingFrequency: [''],
      horizontalDetectionRange: [''],
      titlScanRange: [''],
      sonarRange: [''],
      transmittedPulseLength: [''],
      interferencerejection: [''],
      mass: [''],
      minimumRange: [''],
      diameter: [''],
      effectiveFiringRange: [''],
      effectiveRange: [''],
      totalLength: [''],
      weightOfMissile: [''],
      weightOfWarhead: [''],
      weightOfFuse: [''],
      interfaceWith: [''],
      takenDataFrom: [''],
      maximumFiringElevation: [''],
      muzzleVelocity: [''],
      ammonationType: [''],
      typeOfPlate: [''],
      rateOfInitalCurrent: [''],
      rpm: [''],
      standard: [''],
      inch: [''],
      reqShipsSpeedForOperate: [''],
      reqSeaStateForOperate: [''],
      maxRollingAngle: [''],
      rateOfWaterSupply: [''],
      btu: [''],
      capacity: [''],
      rateOfFuelTransfer: [''],
      amps: [''],
      noOfCoil: [''],
      waterCapacity: [''],
      output: [''],
      nvrorDvrcapacity: [''],
      techSpecification: [''],
      frequencyRange: [''],
      rfoutputImpedance: [''],
      filterandwidth: [''],
      operationRange: [''],
      channel: [''],
      mode:[''],
      remarks: [''],
      menuPosition: [''],
      isActive: [true],
     
    })
  }

  onEquipmentCategorySelectionChangeGetequipmentName(){
    var equipmentCategoryId = this.ShipEquipmentInfoForm.get('equipmentCategoryId').value;     
    this.ShipEquipmentInfoService.getSelectedEqupmentNameByEquepmentCategory(equipmentCategoryId).subscribe(res=>{

      this.selectedEqupmentName=res;
      this.selectEquipmentName=res;

    });
  }

  filterByEquipmentName(value:any){
    this.selectedEqupmentName=this.selectEquipmentName.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getequipmentName(){
    var equpmentNameId = this.ShipEquipmentInfoForm.get('equpmentNameId').value;  
    this.equpmentNameId = equpmentNameId;
    this.masterData.equepmentName.ACPlant;
    this.common = true;

    if(this.equpmentNameId == this.masterData.equepmentName.Generator){
      this.allFormGroupFalse();
      this.Generator=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.PMS){
      this.allFormGroupFalse();
      this.PMS=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.Battery){
      this.allFormGroupFalse();
      this.Battery=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.Transformer){
      this.allFormGroupFalse();
      this.Transformer=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.GyroCompass){
      this.allFormGroupFalse();
      this.GyroCompass=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.NavigationRadar){
      this.allFormGroupFalse();
      this.NavigationRadar=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.MagneticCompass){
      this.allFormGroupFalse();
      this.MagneticCompass=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.EchoSounder){
      this.allFormGroupFalse();
      this.EchoSounder=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.SpeedLog){
      this.allFormGroupFalse();
      this.SpeedLog=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.AirWhistle){
      this.allFormGroupFalse();
      this.AirWhistle=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.ElectricHorn){
      this.allFormGroupFalse();
      this.ElectricHorn=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.DegaussingSystem){
      this.allFormGroupFalse();
      this.DegaussingSystem=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.TrackingRadar){
      this.allFormGroupFalse();
      this.TrackingRadar=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.SearchRadar){
      this.allFormGroupFalse();
      this.SearchRadar=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.HelControlRadar){
      this.allFormGroupFalse();
      this.HelControlRadar=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.SurveillanceRadar){
      this.allFormGroupFalse();
      this.SurveillanceRadar=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.GPS){
      this.allFormGroupFalse();
      this.GPS=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.ECDIS){
      this.allFormGroupFalse();
      this.ECDIS=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.AIS){
      this.allFormGroupFalse();
      this.AIS=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.SearchLight){
      this.allFormGroupFalse();
      this.SearchLight=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.Animometer){
      this.allFormGroupFalse();
      this.Animometer=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.AutoPilotSystem){
      this.allFormGroupFalse();
      this.AutoPilotSystem=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.SteeringSystem){
      this.allFormGroupFalse();
      this.SteeringSystem=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.EWSystem){
      this.allFormGroupFalse();
      this.EWSystem=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.ESM){
      this.allFormGroupFalse();
      this.ESM=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.ECM){
      this.allFormGroupFalse();
      this.ECM=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.OFC3){
      this.allFormGroupFalse();
      this.OFC3=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.Chaff){
      this.allFormGroupFalse();
      this.Chaff=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.RDC){
      this.allFormGroupFalse();
      this.RDC=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.TDL){
      this.allFormGroupFalse();
      this.TDL=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.IFF){
      this.allFormGroupFalse();
      this.IFF=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.NBC){
      this.allFormGroupFalse();
      this.NBC=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.Sonar){
      this.allFormGroupFalse();
      this.Sonar=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.UnderWaterTelephoneSet){
      this.allFormGroupFalse();
      this.UnderWaterTelephoneSet=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.Torpedo){
      this.allFormGroupFalse();
      this.Torpedo=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.SSM){
      this.allFormGroupFalse();
      this.SSM=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.SAM){
      this.allFormGroupFalse();
      this.SAM=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.FCU){
      this.allFormGroupFalse();
      this.FCU=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.TDS){
      this.allFormGroupFalse();
      this.TDS=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.CombatManagementSystem){
      this.allFormGroupFalse();
      this.CombatManagementSystem=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.WeaponGunSystem){
      this.allFormGroupFalse();
      this.WeaponGunSystem=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.PropulsionMonitoringControlSystem){
      this.allFormGroupFalse();
      this.PropulsionMonitoringControlSystem=true;
    }else if(this.equpmentNameId == this.masterData.equepmentName.ADCMS){
      this.allFormGroupFalse();
      this.ADCMS=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.ICCPSystem){
      this.allFormGroupFalse();
      this.ICCPSystem=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.AntiFoulingSystem){
      this.allFormGroupFalse();
      this.AntiFoulingSystem=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.FirePump){
      this.allFormGroupFalse();
      this.FirePump=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.CPPSystem){
      this.allFormGroupFalse();
      this.CPPSystem=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.GearBox){
      this.allFormGroupFalse();
      this.GearBox=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.FireAlarmSystem){
      this.allFormGroupFalse();
      this.FireAlarmSystem=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.Telivision){
      this.allFormGroupFalse();
      this.Telivision=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.AllMotor){
      this.allFormGroupFalse();
      this.AllMotor=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.HPAirCompressor){
      this.allFormGroupFalse();
      this.HPAirCompressor=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.FINStabilizer){
      this.allFormGroupFalse();
      this.FINStabilizer=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.ROPlant){
      this.allFormGroupFalse();
      this.ROPlant=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.UVSterilizer){
      this.allFormGroupFalse();
      this.UVSterilizer=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.ACPlant){
      this.allFormGroupFalse();
      this.ACPlant=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.DomesticFridge){
      this.allFormGroupFalse();
      this.DomesticFridge=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.PackageAC){
      this.allFormGroupFalse();
      this.PackageAC=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.FridgePlant){
      this.allFormGroupFalse();
      this.FridgePlant=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.SubmersiblePump){
      this.allFormGroupFalse();
      this.SubmersiblePump=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.CapstonMotor){
      this.allFormGroupFalse();
      this.CapstonMotor=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.AviationFoulingSystem){
      this.allFormGroupFalse();
      this.AviationFoulingSystem=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.CargoLift){
      this.allFormGroupFalse();
      this.CargoLift=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.BoatDavit){
      this.allFormGroupFalse();
      this.BoatDavit=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.ShoreConnectionBox){
      this.allFormGroupFalse();
      this.ShoreConnectionBox=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.SeaWaterHydrophorePumpMotor){
      this.allFormGroupFalse();
      this.SeaWaterHydrophorePumpMotor=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.FreshWaterHydrophorePumpMotor){
      this.allFormGroupFalse();
      this.FreshWaterHydrophorePumpMotor=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.HotWaterCalorifier){
      this.allFormGroupFalse();
      this.HotWaterCalorifier=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.STP){
      this.allFormGroupFalse();
      this.STP=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.WashingMachine){
      this.allFormGroupFalse();
      this.WashingMachine=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.DyerMachine){
      this.allFormGroupFalse();
      this.DyerMachine=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.WeldingMachine){
      this.allFormGroupFalse();
      this.WeldingMachine=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.AllExhaustFanMotor){
      this.allFormGroupFalse();
      this.AllExhaustFanMotor=true;
    } 
    else if(this.equpmentNameId == this.masterData.equepmentName.AllSupplyFanMotor){
      this.allFormGroupFalse();
      this.AllSupplyFanMotor=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.HotPlate){
      this.allFormGroupFalse();
      this.HotPlate=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.RiceCooker){
      this.allFormGroupFalse();
      this.RiceCooker=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.MarineCombaiOven){
      this.allFormGroupFalse();
      this.MarineCombaiOven=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.DeepFatFryer){
      this.allFormGroupFalse();
      this.DeepFatFryer=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.WasteDisposerMachine){
      this.allFormGroupFalse();
      this.WasteDisposerMachine=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.StreamBox){
      this.allFormGroupFalse();
      this.StreamBox=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.DishWashingMachine){
      this.allFormGroupFalse();
      this.DishWashingMachine=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.UniversalKitchenMachine){
      this.allFormGroupFalse();
      this.UniversalKitchenMachine=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.WaterBoiler){
      this.allFormGroupFalse();
      this.WaterBoiler=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.GeneralBroadcast){
      this.allFormGroupFalse();
      this.GeneralBroadcast=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.IntercomSystem){
      this.allFormGroupFalse();
      this.IntercomSystem=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.CCTVSystem){
      this.allFormGroupFalse();
      this.CCTVSystem=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.HelicopterPowerSupplySystem){
      this.allFormGroupFalse();
      this.HelicopterPowerSupplySystem=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.ASGSI){
      this.allFormGroupFalse();
      this.ASGSI=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.HelicopterStaticLandingAIDLightSystem){
      this.allFormGroupFalse();
      this.HelicopterStaticLandingAIDLightSystem=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.SHRS){
      this.allFormGroupFalse();
      this.SHRS=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.LandingLights){
      this.allFormGroupFalse();
      this.LandingLights=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.HighFrequencySet){
      this.allFormGroupFalse();
      this.HighFrequencySet=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.VeryHighFrequencySet){
      this.allFormGroupFalse();
      this.VeryHighFrequencySet=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.UltraHighFrequencySet){
      this.allFormGroupFalse();
      this.UltraHighFrequencySet=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.WalkieTalkieSet){
      this.allFormGroupFalse();
      this.WalkieTalkieSet=true;
    }
    else if(this.equpmentNameId == this.masterData.equepmentName.GUN){
      this.allFormGroupFalse();
      this.GUN=true;
    }
    else if(this.equpmentNameId ==  this.masterData.equepmentName.PowerConversionEquation){
      this.allFormGroupFalse();
      this.PCE=true;
    }
    else{
      this.allFormGroupFalse();
    }
    console.log(this.equpmentNameId);


  }

  allFormGroupFalse(){
    this.PMS=false;
    this.Generator=false;
    this.Battery=false;
    this.GyroCompass=false;
    this.EWSystem=false;
    this.Sonar=false;
    this.ControlSystem=false;
    this.Portable=false;
    this.SREBroadcastSystem=false;
    this.ICS=false;
    this.TelephoneExchange=false;
    this.SoundPoweredTelephone=false;
    this.UnderWaterTelephoneSet=false;
    this.Torpedo=false;
    this.ESM=false;
    this.TrueLyingRuder=false;
    this.EWSystemChaff=false;
    this.GUN=false;
    this.RDC=false;
    this.CombatSystem=false;
    this.NavigationRadar=false;
    this.ECDIS=false;
    this.AIS=false;
    this.GPS=false;
    this.SpeedLog=false;
    this.EchoSounder=false;
    this.Transformer=false;
    this.DCPowerSystem=false;
    this.MagneticCompass=false;
    this.AirWhistle=false;
    this.ElectricHorn=false;
    this.DegaussingSystem=false;
    this.TrackingRadar=false;
    this.SearchRadar=false;
    this.HelControlRadar=false;
    this.SurveillanceRadar=false;
    this.SearchLight=false;
    this.Animometer=false;
    this.AutoPilotSystem=false;
    this.SteeringSystem=false;
    this.ECM=false;
    this.OFC3=false;
    this.Chaff=false;
    this.TDL=false;
    this.IFF=false;
    this.NBC=false;
    this.SSM=false;
    this.SAM=false;
    this.FCU=false;
    this.TDS=false;
    this.HighFrequencySet=false;
    this.VeryHighFrequencySet=false;
    this.UltraHighFrequencySet=false;
    this.WalkieTalkieSet=false;
    this.CombatManagementSystem=false;
    this.WeaponGunSystem=false;
    this.PropulsionMonitoringControlSystem=false;
    this.ADCMS=false;
    this.ICCPSystem=false;
    this.AntiFoulingSystem=false;
    this.FirePump=false;
    this.CPPSystem=false;
    this.GearBox=false;
    this.FireAlarmSystem=false;
    this.Telivision=false;
    this.AllMotor=false;
    this.HPAirCompressor=false;
    this.HelicopterPowerSupplySystem=false;
    this.FINStabilizer=false;
    this.ROPlant=false;
    this.ASGSI=false;
    this.UVSterilizer=false;
    this.ACPlant=false;
    this.DomesticFridge=false;
    this.PackageAC=false;
    this.HelicopterStaticLandingAIDLightSystem=false;
    this.FridgePlant=false;
    this.SHRS=false;
    this.LandingLights=false;
    this.SubmersiblePump=false;
    this.CapstonMotor=false;
    this.HotPlate=false;
    this.RiceCooker=false;
    this.AviationFoulingSystem=false;
    this.CargoLift=false;
    this.MarineCombaiOven=false;
    this.BoatDavit=false;
    this.DeepFatFryer=false;
    this.ShoreConnectionBox=false;
    this.StreamBox=false;
    this.SeaWaterHydrophorePumpMotor=false;
    this.DishWashingMachine=false;
    this.UniversalKitchenMachine=false;
    this.FreshWaterHydrophorePumpMotor=false;        
    this.WaterBoiler=false;
    this.HotWaterCalorifier=false;
    this.GeneralBroadcast=false;
    this.STP=false;
    this.WashingMachine=false;
    this.IntercomSystem=false;
    this.DyerMachine=false;
    this.CCTVSystem=false;
    this.WeldingMachine=false;
    this.AllExhaustFanMotor=false;
    this.AllSupplyFanMotor=false;
  }
  getSelectedBaseName(value){
  
    this.ShipEquipmentInfoService.getSelectedSchoolName(value).subscribe(res=>{
      this.selectedBaseName=res
    }); 
  }
  getSelectedSchoolByBranchLevelAndThirdLevel(){
    //var baseNameId = this.ShipEquipmentInfoForm.value['baseNameId'];
    this.ShipEquipmentInfoService.getSelectedSchoolByBranchLevelAndThirdLevel().subscribe(res=>{
      this.selectedBaseSchoolName=res;
      this.selectSchoolName = res;

    }); 
  }
  filterBySchool(value:any){
    this.selectedBaseSchoolName=this.selectSchoolName.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }

  getSelectedEquipmentCategory(){
    this.ShipEquipmentInfoService.getSelectedEquipmentCategory().subscribe(res=>{
      this.selectedEquipmentCategory=res

      this.selectEquipmentCategory = res

    }); 
  }
  filterByEquipmentCategory(value:any){
    this.selectedEquipmentCategory=this.selectEquipmentCategory.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  getSelectedEquipmentType(){
    this.ShipEquipmentInfoService.getSelectedEquipmentType().subscribe(res=>{

      this.selectedEquipmentType=res;
      this.selectEquipmentType=res;
    }); 
  }

  filterByEquipmentType(value:any){
    this.selectedEquipmentType=this.selectEquipmentType.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  
  getSelectedBrand(){
    this.ShipEquipmentInfoService.getSelectedBrand().subscribe(res=>{
      this.selectedBrand=res
    }); 
  }
  getSelectedStateOfEquipment(){
    this.ShipEquipmentInfoService.getSelectedStateOfEquipment().subscribe(res=>{
      this.selectedStateOfEquipment=res;
    }); 
  }
  getSelectedAcquisitionMethod(){
    this.ShipEquipmentInfoService.getSelectedAcquisitionMethod().subscribe(res=>{
      this.selectedAcquisitionMethod=res
      this.selectAcuisitionMethod=res

    }); 
  }
  filterByAquisitionMethod(value:any){
    this.selectedAcquisitionMethod=this.selectAcuisitionMethod.filter(x=>x.text.toLowerCase().includes(value.toLowerCase()))
  }
  // onFileChanged(event){
  //   if (event.target.files.length > 0) {
  //     const file = event.target.files[0];
  //     this.ShipEquipmentInfoForm.patchValue({
  //       doc: file,
  //     });
  //   }
  // }

 


  getSelectedOrganizationByBranch(){
    this.ShipEquipmentInfoService.getSelectedOrganizationByBranchLevel().subscribe(res=>{
      this.selectedBranchLevel=res
    }); 
  }

  getShipEquipmentByCategoryId(){
    
  }

  onSubmit() {
    const id = this.ShipEquipmentInfoForm.get('shipEquipmentInfoId').value;   

    //this.ShipEquipmentInfoForm.get('dateOfCommission').setValue((new Date(this.ShipEquipmentInfoForm.get('dateOfCommission').value)).toUTCString());

    // const formData = new FormData();
    // for (const key of Object.keys(this.ShipEquipmentInfoForm.value)) {
    //   const value = this.ShipEquipmentInfoForm.value[key];
    //   formData.append(key, value);
    // }

    if (id) {
      this.confirmService.confirm('Confirm Update message', 'Are You Sure Update This  Item').subscribe(result => {
        
        if (result) {
          this.ShipEquipmentInfoService.update(+id,this.ShipEquipmentInfoForm.value).subscribe(response => {
            this.router.navigateByUrl('/ship-management/shipequipmentinfo-list');
            this.snackBar.open('Information Updated Successfully ', '', {
              duration: 2000,
              verticalPosition: 'bottom',
              horizontalPosition: 'right',
              panelClass: 'snackbar-success'
            });
          }, error => {
            this.validationErrors = error;
          })
        }
      })
    } else {
      this.ShipEquipmentInfoService.submit(this.ShipEquipmentInfoForm.value).subscribe(response => {
        this.router.navigateByUrl('/ship-management/shipequipmentinfo-list');
        this.snackBar.open('Information Inserted Successfully ', '', {
          duration: 2000,
          verticalPosition: 'bottom',
          horizontalPosition: 'right',
          panelClass: 'snackbar-success'
        });
      }, error => {
        this.validationErrors = error;
      })
    }
 
  }
}
