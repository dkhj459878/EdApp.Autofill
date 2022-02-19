﻿using System;
using System.Collections.Generic;
using System.Text.Json;
using EdApp.AutoFill.BL.Contract.Services;
using EdApp.AutoFill.BL.Enums;
using EdApp.AutoFill.BL.Model;

namespace EdApp.AutoFill.BL.Service
{
    public class JsonDataLoaderService : IJsonDataLoaderService
    {
        private JsonSerializerOptions SerializeOptions => new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public ICollection<AttributeDto> GetJsonDataConvertedToObject(JsonKind jsonKind)
        {
            string json = jsonKind switch
            {
                JsonKind.MyJson => GetMyJson(),
                JsonKind.SiemensJson => GetFlatFromSiemens(),
                _ => throw new InvalidOperationException()
            };
            MappingDtoCollection mappingDtoCollection = JsonSerializer.Deserialize<MappingDtoCollection>(json, SerializeOptions);
            return mappingDtoCollection.attributes;
        }

        private class MappingDtoCollection
        {
            public List<AttributeDto> attributes { get; set; }
        }

        private string GetMyJson()
        {
            return @"{""attributes"":[{""name"":""K.BGK.NORM"",""value"":"""",""unit"":"""",""description"":null},{""name"":""K.BGK.ZUSATZVERLUSTE_GEMESSEN"",""value"":""0"",""unit"":""W"",""description"":null},{""name"":""K.BGK.AUSNUTZUNGNACHWAERMEKLASSE"",""value"":""F*"",""unit"":"""",""description"":null},{""name"":""Steuerung.Betriebsart"",""value"":""M"",""unit"":"""",""description"":null},{""name"":""Steuerung.Leistungsstufe"",""value"":""A"",""unit"":"""",""description"":null},{""name"":""K.BGK.U_NENN"",""value"":""6600"",""unit"":""V"",""description"":null},{""name"":""K.BGK.SCHALTUNG"",""value"":""Y"",""unit"":"""",""description"":null},{""name"":""K.BGK.P2_MECH_ENT"",""value"":""560"",""unit"":""kW"",""description"":null},{""name"":""K.BGK.F_BETRIEB"",""value"":""60"",""unit"":""Hz"",""description"":null},{""name"":""K.BGK.VERHAELTNISMOMENTERWARTET_LISTE"",""value"":""2"",""unit"":"""",""description"":null},{""name"":""K.BGK.K_ANLAUF_LISTE"",""value"":""6.8"",""unit"":"""",""description"":null},{""name"":""K.BGK.VERHAELTNISKIPPMOMENTERWART_LIST"",""value"":""2.8"",""unit"":"""",""description"":null},{""name"":""BGZ.CHECK_LISTENWERTE"",""value"":""False"",""unit"":"""",""description"":null},{""name"":""BGZ.KFE_RED"",""value"":""1.1"",""unit"":"""",""description"":null},{""name"":""BGZ.ALFA_PAKET"",""value"":""54"",""unit"":"""",""description"":null},{""name"":""BGZ.ALFA2_PAKET"",""value"":""90"",""unit"":"""",""description"":null},{""name"":""BGZ.K_WIRBEL"",""value"":""1.05"",""unit"":"""",""description"":null},{""name"":""BGZ.K_HYSTERESE"",""value"":""1.25"",""unit"":"""",""description"":null},{""name"":""BGZ.KSA"",""value"":""1"",""unit"":"""",""description"":null},{""name"":""BGZ.KSUK"",""value"":""1"",""unit"":"""",""description"":null},{""name"":""BGZ.STIRNSTREUFAKTOR"",""value"":""1"",""unit"":"""",""description"":null},{""name"":""BGZ.K_VA_EINGABE"",""value"":""1"",""unit"":"""",""description"":null},{""name"":""BGZ.STROMVERDRAENGUNGSFAKTORIMRING"",""value"":""0"",""unit"":"""",""description"":null},{""name"":""BGZ.TH_DELTA"",""value"":null,""unit"":""K"",""description"":null},{""name"":""BGZ.TH2_DELTA"",""value"":""100"",""unit"":""K"",""description"":null},{""name"":""BGZ.K_VAE_1"",""value"":""0.42"",""unit"":"""",""description"":null},{""name"":""BGZ.K_VAE_2"",""value"":""0.35"",""unit"":"""",""description"":null},{""name"":""BGZ.K_VAE_3"",""value"":""0.3"",""unit"":"""",""description"":null},{""name"":""BGZ.K_VAE_4"",""value"":""0.5"",""unit"":"""",""description"":null},{""name"":""BGZ.KSUK_D"",""value"":null,""unit"":"""",""description"":null},{""name"":""BGC.F2_JOCHENT"",""value"":""5"",""unit"":""%"",""description"":null},{""name"":""K.BGK.TRANummer"",""value"":""171419"",""unit"":"""",""description"":null},{""name"":""K.BGK.TRANUMMERNACHTRAG"",""value"":""2"",""unit"":"""",""description"":null},{""name"":""Steuerung.MaschinenArt"",""value"":""K"",""unit"":"""",""description"":null},{""name"":""K.BGK.MASCHINE"",""value"":""1NB1"",""unit"":"""",""description"":null},{""name"":""K.BGK.BAUGROESSE"",""value"":""458"",""unit"":"""",""description"":null},{""name"":""K.BGK.Polzahl"",""value"":""4"",""unit"":"""",""description"":null},{""name"":""K.BGK.Datum"",""value"":""10/17/201812:00:00AM"",""unit"":"""",""description"":null},{""name"":""K.BGK.REIHE"",""value"":""SIMOTICSHVC"",""unit"":"""",""description"":null},{""name"":""BGC.Z2_NUTEN"",""value"":""38"",""unit"":"""",""description"":null},{""name"":""BGC.BLECHSORTE"",""value"":""M470P65A"",""unit"":"""",""description"":null},{""name"":""BGC.H2_STEG"",""value"":""4"",""unit"":""mm"",""description"":null},{""name"":""BGC.B2_SCHRAEG_AX"",""value"":""0"",""unit"":""mm"",""description"":null},{""name"":""BGC.ALFA2_SCHRAEG_AX"",""value"":""00"",""unit"":""\u00B0min"",""description"":null},{""name"":""BGC.ENDBLECHVERSTAERKUNG"",""value"":""6"",""unit"":""mm"",""description"":null},{""name"":""BGC.A_LUFTSPALT"",""value"":""2"",""unit"":""mm"",""description"":null},{""name"":""BGC.Z2_KANAL"",""value"":""36"",""unit"":"""",""description"":null},{""name"":""BGC.DAISI"",""value"":""True"",""unit"":"""",""description"":null},{""name"":""Rotoroutsidediameter"",""value"":""366"",""unit"":""mm"",""description"":null},{""name"":""BGC.D2_INNEN"",""value"":""140"",""unit"":""mm"",""description"":null},{""name"":""BGC.L2_PAKET"",""value"":""708"",""unit"":""mm"",""description"":null},{""name"":""BGC.L2_TOTAL"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGC.Z2_KANAL_RAD"",""value"":null,""unit"":"""",""description"":null},{""name"":""BGC.D2_KANAL1"",""value"":""241"",""unit"":""mm"",""description"":null},{""name"":""BGC.K2_EISEN"",""value"":""0.96"",""unit"":"""",""description"":null},{""name"":""BGC.BEMERKUNG1"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGC.Stegwelle"",""value"":""False"",""unit"":"""",""description"":null},{""name"":""BGC.NUTENFORM"",""value"":""NRA"",""unit"":"""",""description"":null},{""name"":""BGC.NUTENNUMMER"",""value"":""1262"",""unit"":"""",""description"":null},{""name"":""BGC.B2_STEG"",""value"":""5"",""unit"":""mm"",""description"":null},{""name"":""BGC.B2_STEG2"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGC.H2_STEG2"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGC.B2_STEG3"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGC.H2_STEG3"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGC.D2_NUT"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGC.H2_NUT2"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGC.B2_NUT"",""value"":""11.7"",""unit"":""mm"",""description"":null},{""name"":""BGC.B2_NUT2"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGC.H2_NUT"",""value"":""32.6"",""unit"":""mm"",""description"":null},{""name"":""BGC.H2_KEIL"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGC.H2_NUT4"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGC.ALFA2_SCHRAEG2"",""value"":null,""unit"":""\u00B0"",""description"":null},{""name"":""BGC.KUEHLNUTENFORM"",""value"":""NAT"",""unit"":"""",""description"":null},{""name"":""BGC.KUEHLNUTENNUMMER"",""value"":""1009"",""unit"":"""",""description"":null},{""name"":""BGC.B2_KANAL"",""value"":""8"",""unit"":""mm"",""description"":null},{""name"":""BGC.B2_KANAL2"",""value"":""12.5"",""unit"":""mm"",""description"":null},{""name"":""BGC.H2_KANAL"",""value"":""32"",""unit"":""mm"",""description"":null},{""name"":""BGB.Z_NUTEN"",""value"":""48"",""unit"":"""",""description"":null},{""name"":""BGB.BLECHSORTE"",""value"":""M470P65A"",""unit"":"""",""description"":null},{""name"":""BGB.H_STEG"",""value"":""4.5"",""unit"":""mm"",""description"":null},{""name"":""BGB.ENDBLECHVERSTAERKUNG"",""value"":""6"",""unit"":""mm"",""description"":null},{""name"":""BGB.Z_KANAL"",""value"":null,""unit"":"""",""description"":null},{""name"":""BGB.DAISI"",""value"":""True"",""unit"":"""",""description"":null},{""name"":""BGB.D_AUSSEN"",""value"":""620"",""unit"":""mm"",""description"":null},{""name"":""BGB.D_INNEN"",""value"":""370"",""unit"":""mm"",""description"":null},{""name"":""BGB.L_PAKET"",""value"":""708"",""unit"":""mm"",""description"":null},{""name"":""BGB.L_TOTAL"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGB.Z_KANAL_RAD"",""value"":null,""unit"":"""",""description"":null},{""name"":""BGB.D_KANAL1"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGB.K_EISEN"",""value"":""0.96"",""unit"":"""",""description"":null},{""name"":""BGB.BEMERKUNG1"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGB.NUTENFORM"",""value"":""NJA"",""unit"":"""",""description"":null},{""name"":""BGB.NUTENNUMMER"",""value"":""1012"",""unit"":"""",""description"":null},{""name"":""BGB.B_NUT2"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""BGB.B_NUT"",""value"":""13.5"",""unit"":""mm"",""description"":null},{""name"":""BGB.H_NUT"",""value"":""62"",""unit"":""mm"",""description"":null},{""name"":""BGB.H_KEIL"",""value"":""2.4"",""unit"":""mm"",""description"":null},{""name"":""BGB.B_STEG"",""value"":""13.9"",""unit"":""mm"",""description"":null},{""name"":""BGB.B_STEG2"",""value"":""14.3"",""unit"":""mm"",""description"":null},{""name"":""BGB.B_NUT3"",""value"":null,""unit"":""mm"",""description"":null},{""name"":""WE.WEK.WINDING_TYPE"",""value"":""K2"",""unit"":null,""description"":null},{""name"":""WE.WEK.NO_OF_CONDUCTORS_LOWER_LAYER"",""value"":""3"",""unit"":"""",""description"":null},{""name"":""WE.WEK.NO_OF_CONDUCTORS_UPPER_LAYER"",""value"":""3"",""unit"":"""",""description"":null},{""name"":""BGW.Z_DRAHT"",""value"":""8"",""unit"":null,""description"":null},{""name"":""BGW.Z_DRAHT2"",""value"":""30"",""unit"":null,""description"":null},{""name"":""BGW.D_DRAHT"",""value"":""1.25"",""unit"":""mm"",""description"":null},{""name"":""BGW.D_DRAHT_ISO"",""value"":""1.316"",""unit"":""mm"",""description"":null},{""name"":""BGW.L_WIKOPF"",""value"":""555"",""unit"":""mm"",""description"":null},{""name"":""BGW.D_DRAHT2"",""value"":""1.4"",""unit"":""mm"",""description"":null},{""name"":""BGW.D_DRAHT_ISO2"",""value"":""1.468"",""unit"":""mm"",""description"":null},{""name"":""BGW.O_WICKELRAUM"",""value"":""581"",""unit"":""mm\u00B2"",""description"":null},{""name"":""BGW.K_FUELL"",""value"":""0.811"",""unit"":null,""description"":null},{""name"":""BGW.S_NUTKASTEN"",""value"":""0.63"",""unit"":""mm"",""description"":null},{""name"":""WE.WEK.WIRE_INSULATION_TYPE"",""value"":""1L-OC"",""unit"":null,""description"":null},{""name"":""BGW.ISOLATIONSTYP"",""value"":""2.35.70"",""unit"":null,""description"":null},{""name"":""BGW.WAERMEKLASSE"",""value"":""F"",""unit"":null,""description"":null},{""name"":""BGW.BEMERKUNG1"",""value"":"""",""unit"":null,""description"":null},{""name"":""BGW.Z_SPULEN_NEBEN"",""value"":""0"",""unit"":null,""description"":null},{""name"":""BGW.Z_PARALLEL"",""value"":""4"",""unit"":null,""description"":null},{""name"":""BGW.Y_NUTSCHRITT"",""value"":""16"",""unit"":null,""description"":null},{""name"":""WE.WEK.DOUBLE_LAYER_WINDING"",""value"":""True"",""unit"":null,""description"":null},{""name"":""WE.WEK.WIRE_CONFIGURATION"",""value"":""0"",""unit"":null,""description"":null},{""name"":""WE.WEK.INSULATION_DESIGN_MODE"",""value"":""2"",""unit"":null,""description"":null},{""name"":""WE.WEK.THICKNESS_SLOT_OPENING_INSULATION"",""value"":""0"",""unit"":""m"",""description"":null},{""name"":""WE.WEK.THICKNESS_SEPARATOR"",""value"":""0"",""unit"":""m"",""description"":null},{""name"":""WE.WEK.GROUND_WALL_INSULATION"",""value"":""False"",""unit"":null,""description"":null},{""name"":""WE.WEK.AVERAGE_COIL_OVERHANG_LENGTH"",""value"":""False"",""unit"":null,""description"":null},{""name"":""WE.WEK.WIRE_DIMENSIONS_PRESET"",""value"":""False"",""unit"":null,""description"":null},{""name"":""BGZ.m2_gegen_IstQuadratisch"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.m2_gegen_istKonstant"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.m2_gegen_istBeliebig"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.FREMDTRAEGHEITSMOMENT"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.MOTORTRAEGHEITSMOMENT"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.m2_gegen_relativ"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.CHECK_YD"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.URED"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.MKORR"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.X_NETZ"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN_BEZUGSWERT"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN_HOCH"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN_STILLSTAND"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN_KONSTANT"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.0"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.1"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.2"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.3"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.4"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.5"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.6"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.7"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.8"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.9"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.N2_GEGEN.10"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.0"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.1"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.2"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.3"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.4"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.5"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.6"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.7"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.8"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.9"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGZ.M2_GEGEN.10"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGC.D2_KANAL2"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGC.D2_KANAL3"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGC.B2_KANAL_RAD"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGC.H2_KANAL1"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGC.H2_KANAL2"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGC.H2_KANAL3"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGB.B_KANAL_RAD"",""value"":"""",""unit"":"""",""description"":null},{""name"":""GA.BGW.Z_LEITER_NUT_STRING"",""value"":"""",""unit"":"""",""description"":null},{""name"":""GA.BGW.Z_PARALLEL"",""value"":"""",""unit"":"""",""description"":null},{""name"":""Steuerung.Rechnungsart"",""value"":""WE"",""unit"":"""",""description"":null},{""name"":""BGZ.REIBUNGSVERLUSTE"",""value"":""1500"",""unit"":"""",""description"":null},{""name"":""DEFAULT.LOCALE"",""value"":""DE"",""unit"":"""",""description"":null},{""name"":""K.BGK.LAEUFERART"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGW.BETA_STRING"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGW.ANZSPULENSTAEBE"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGW.POLZAHL_A"",""value"":"""",""unit"":"""",""description"":null},{""name"":""K.BGZ.Z_PHASEN"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGW.Z_LEITER_NUT_STRING"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGW.B_DRAHT"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.H_DRAHT"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.B_DRAHT_ISO"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.H_DRAHT_ISO"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.Z_WINDUNGEN"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGW.Z_LEITER_NEBEN"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGW.Z_DRAHT_NEBEN"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGW.B_SPULE_O"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.B_SPULE_M"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.VERBLEITERMITUMPRESSUEB1"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.VERBLEITERMITUMPRESSUEB2"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.NUTVERSCHLUSS"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGW.L_SPULE_AXIAL"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.A_ISO_STIRN"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.NUTAUFRECHNUNG"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGW.FUELLSTREIFENKOPF"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.FUELLSTREIFENMITTE"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGW.L_SCHENKEL1"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGX.STABMATERIALAUSSEN"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGX.STABMATERIALINNEN"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGX.LAEUFERAUSFUEHRUNG"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGX.L2_STAB"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGX.L2_STAB2"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGX.STABFORM"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGX.D2_STAB"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGX.H2_STAB"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGX.D2_STAB2"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGX.H2_STAB2"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGX.RINGMATERIALAUSSEN"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGX.RINGMATERIALINNEN"",""value"":"""",""unit"":"""",""description"":null},{""name"":""BGX.B2_RING"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGX.H2_RING"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGX.B2_RING2"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""BGX.H2_RING2"",""value"":"""",""unit"":""mm"",""description"":null},{""name"":""K.BGK.COSPHI"",""value"":null,""unit"":"""",""description"":null},{""name"":""K.BGK.ETAPROZENT"",""value"":null,""unit"":""%"",""description"":null},{""name"":""K.BGK.GRANUMMER"",""value"":null,""unit"":"""",""description"":null}]}";
        }

        private string GetFlatFromSiemens()
    {
        return @"{""attributes"":[{""name"":""WE.WEK.WIRE_INSULATION_TYPE"",""value"":""3L-OC"",""unit"":"""",""description"":""""},{""name"":""WE.WEK.WINDING_TYPE"",""value"":""K2"",""unit"":"""",""description"":""""},{""name"":""WE.WEK.DOUBLE_LAYER_WINDING"",""value"":""true"",""unit"":"""",""description"":""""},{""name"":""WE.WEK.NO_OF_CONDUCTORS_UPPER_LAYER"",""value"":""3"",""unit"":"""",""description"":""""},{""name"":""WE.WEK.NO_OF_CONDUCTORS_LOWER_LAYER"",""value"":""3"",""unit"":"""",""description"":""""},{""name"":""WE.WEK.WIRE_CONFIGURATION"",""value"":""1"",""unit"":"""",""description"":""""},{""name"":""WE.WEK.INSULATION_DESIGN_MODE"",""value"":""3"",""unit"":"""",""description"":""""},{""name"":""BGW.O_WICKELRAUM"",""value"":""624"",""unit"":""mm^2"",""description"":""""},{""name"":""WE.WEK.THICKNESS_SLOT_OPENING_INSULATION"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""WE.WEK.THICKNESS_SEPARATOR"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""WE.WEK.GROUND_WALL_INSULATION"",""value"":""false"",""unit"":"""",""description"":""""},{""name"":""BGW.S_NUTKASTEN"",""value"":""0.6"",""unit"":""mm"",""description"":""""},{""name"":""WE.WEK.AVERAGE_COIL_OVERHANG_LENGTH"",""value"":""false"",""unit"":"""",""description"":""""},{""name"":""BGW.L_WIKOPF"",""value"":""420"",""unit"":""mm"",""description"":""""},{""name"":""WE.WEK.WIRE_DIMENSIONS_PRESET"",""value"":""N"",""unit"":"""",""description"":""""},{""name"":""BGW.K_FUELL"",""value"":""0.82"",""unit"":"""",""description"":""""},{""name"":""BGW.Z_DRAHT"",""value"":""8"",""unit"":"""",""description"":""""},{""name"":""BGW.Z_DRAHT2"",""value"":""24"",""unit"":"""",""description"":""""},{""name"":""BGW.D_DRAHT"",""value"":""1.25"",""unit"":""mm"",""description"":""""},{""name"":""BGW.D_DRAHT_ISO"",""value"":""1.402"",""unit"":""mm"",""description"":""""},{""name"":""BGW.D_DRAHT2"",""value"":""1.4"",""unit"":""mm"",""description"":""""},{""name"":""BGW.D_DRAHT_ISO2"",""value"":""1.542"",""unit"":""mm"",""description"":""""},{""name"":""BGW.WAERMEKLASSE"",""value"":""F"",""unit"":"""",""description"":""""},{""name"":""BGW.Y_NUTSCHRITT"",""value"":""10"",""unit"":"""",""description"":""""},{""name"":""BGW.BEMERKUNG1"",""value"":""W07:*STROMVERDR.OPT.;"",""unit"":"""",""description"":""""},{""name"":""BGW.BEMERKUNG2"",""value"":""W35:*2-FACHTEILSPULEN;"",""unit"":"""",""description"":""""},{""name"":""K.BGK.EX_SCHUTZ"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""K.BGK.MASCHINE"",""value"":""1NA1"",""unit"":"""",""description"":""""},{""name"":""K.BGK.BAUGROESSE"",""value"":""454"",""unit"":"""",""description"":""""},{""name"":""K.BGK.I_BETRIEB"",""value"":"""",""unit"":""A"",""description"":""""},{""name"":""K.BGK.N2_MECH"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""DEFAULT.LOCALE"",""value"":""DE"",""unit"":"""",""description"":""""},{""name"":""Steuerung.Rechnungsart"",""value"":""WE"",""unit"":"""",""description"":""""},{""name"":""Steuerung.Leistungsstufe"",""value"":""A"",""unit"":"""",""description"":""""},{""name"":""Steuerung.Betriebsart"",""value"":""MOT."",""unit"":"""",""description"":""""},{""name"":""K.BGK.AUSNUTZUNGNACHWAERMEKLASSE"",""value"":""F"",""unit"":"""",""description"":""""},{""name"":""K.BGK.U_NENN"",""value"":""690"",""unit"":""V"",""description"":""""},{""name"":""K.BGK.SCHALTUNG"",""value"":""d"",""unit"":"""",""description"":""""},{""name"":""K.BGK.P2_MECH_ENT"",""value"":""1000.0"",""unit"":""kW"",""description"":""""},{""name"":""K.BGK.COSPHI"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""K.BGK.ETAPROZENT"",""value"":"""",""unit"":""%"",""description"":""""},{""name"":""K.BGK.F_BETRIEB"",""value"":""60.0"",""unit"":""Hz"",""description"":""""},{""name"":""K.BGK.VERHAELTNISMOMENTERWARTET_LISTE"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""K.BGK.K_ANLAUF_LISTE"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""K.BGK.VERHAELTNISKIPPMOMENTERWART_LIST"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""K.BGK.NORM"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""K.BGK.ZUSATZVERLUSTE_GEMESSEN"",""value"":"""",""unit"":""W"",""description"":""""},{""name"":""BGC.F2_JOCHENT"",""value"":""0.0"",""unit"":""%"",""description"":""""},{""name"":""BGZ.ALFA_PAKET"",""value"":""93.0"",""unit"":"""",""description"":""""},{""name"":""BGZ.ALFA2_PAKET"",""value"":""102.0"",""unit"":"""",""description"":""""},{""name"":""BGZ.TH_DELTA"",""value"":"""",""unit"":""K"",""description"":""""},{""name"":""BGZ.STIRNSTREUFAKTOR"",""value"":""1"",""unit"":"""",""description"":""""},{""name"":""BGZ.KFE_RED"",""value"":""1"",""unit"":"""",""description"":""""},{""name"":""BGZ.K_VA_EINGABE"",""value"":""1"",""unit"":"""",""description"":""""},{""name"":""BGZ.REIBUNGSVERLUSTE"",""value"":""2300.0"",""unit"":""W"",""description"":""""},{""name"":""BGZ.STROMVERDRAENGUNGSFAKTORIMRING"",""value"":""0.0"",""unit"":"""",""description"":""""},{""name"":""BGZ.K_VAE_1"",""value"":""0.42"",""unit"":"""",""description"":""""},{""name"":""BGZ.K_VAE_2"",""value"":""0.35"",""unit"":"""",""description"":""""},{""name"":""BGZ.K_VAE_3"",""value"":""0.3"",""unit"":"""",""description"":""""},{""name"":""BGZ.K_VAE_4"",""value"":""0.5"",""unit"":"""",""description"":""""},{""name"":""BGZ.TH2_DELTA"",""value"":"""",""unit"":""K"",""description"":""""},{""name"":""BGZ.K_WIRBEL"",""value"":""0.86"",""unit"":"""",""description"":""""},{""name"":""BGZ.K_HYSTERESE"",""value"":""1.49"",""unit"":"""",""description"":""""},{""name"":""BGZ.KSA"",""value"":""1"",""unit"":"""",""description"":""""},{""name"":""BGZ.KSUK"",""value"":""1"",""unit"":"""",""description"":""""},{""name"":""BGZ.KSUK_D"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""ICCoolingType"",""value"":""IC71W"",""unit"":"""",""description"":""""},{""name"":""Outerfantype"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""Outerfandiameter"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""Innerfantype"",""value"":""RG4"",""unit"":"""",""description"":""""},{""name"":""Innerfandiameter"",""value"":""630.0"",""unit"":"""",""description"":""""},{""name"":""Variant"",""value"":""AA"",""unit"":"""",""description"":""""},{""name"":""K.BGK.TRANummer"",""value"":""175724"",""unit"":"""",""description"":""""},{""name"":""K.BGK.TRANUMMERNACHTRAG"",""value"":""0"",""unit"":"""",""description"":""""},{""name"":""K.BGK.GRANUMMER"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""K.BGK.LAEUFERART"",""value"":""KL"",""unit"":"""",""description"":""""},{""name"":""K.BGZ.Z_PHASEN"",""value"":""3"",""unit"":"""",""description"":""""},{""name"":""BGW.WICKLUNGSART"",""value"":""K2"",""unit"":"""",""description"":""""},{""name"":""BGW.BETA_STRING"",""value"":""4"",""unit"":"""",""description"":""""},{""name"":""BGW.ANZSPULENSTAEBE"",""value"":""72"",""unit"":"""",""description"":""""},{""name"":""BGW.Z_LEITER_NUT_STRING"",""value"":""3+3"",""unit"":"""",""description"":""""},{""name"":""BGW.B_DRAHT"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGW.H_DRAHT"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGW.B_DRAHT_ISO"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGW.H_DRAHT_ISO"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGW.POLZAHL_A"",""value"":""6"",""unit"":"""",""description"":""""},{""name"":""BGW.Z_SPULEN_NEBEN"",""value"":""2"",""unit"":"""",""description"":""""},{""name"":""BGW.Z_PARALLEL"",""value"":""3"",""unit"":"""",""description"":""""},{""name"":""BGW.Z_WINDUNGEN"",""value"":""24"",""unit"":"""",""description"":""""},{""name"":""BGW.Z_LEITER_NEBEN"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGW.Z_DRAHT_NEBEN"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGW.B_SPULE_O"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGW.B_SPULE_M"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGW.VERBLEITERMITUMPRESSUEB1"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGW.VERBLEITERMITUMPRESSUEB2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGW.NUTVERSCHLUSS"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGW.DRAHTISOLIERUNG"",""value"":""3L-OC"",""unit"":"""",""description"":""""},{""name"":""BGW.ISOLATIONSTYP"",""value"":""2.15.72"",""unit"":"""",""description"":""""},{""name"":""BGW.WAERMEKLASSE"",""value"":""F"",""unit"":"""",""description"":""""},{""name"":""BGW.SCHALTBILDNUMMER"",""value"":""9134"",""unit"":"""",""description"":""""},{""name"":""BGW.L_SPULE_AXIAL"",""value"":""141"",""unit"":""mm"",""description"":""""},{""name"":""BGW.A_ISO_STIRN"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGW.L_SCHENKEL1"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGW.FUELLSTREIFENKOPF"",""value"":""0"",""unit"":""mm"",""description"":""""},{""name"":""BGW.FUELLSTREIFENMITTE"",""value"":""0"",""unit"":""mm"",""description"":""""},{""name"":""BGW.SPANNUNGSSTUFE"",""value"":""690"",""unit"":""V"",""description"":""""},{""name"":""BGW.NUTAUFRECHNUNG"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGW.STD_DRAHTTAB"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGX.STABMATERIALAUSSEN"",""value"":""E-CU"",""unit"":"""",""description"":""""},{""name"":""BGX.STABMATERIALINNEN"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGX.LAEUFERAUSFUEHRUNG"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGX.L2_STAB"",""value"":""775"",""unit"":""mm"",""description"":""""},{""name"":""BGX.L2_STAB2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGX.STABFORM"",""value"":""3"",""unit"":"""",""description"":""""},{""name"":""BGX.D2_STAB"",""value"":""9"",""unit"":""mm"",""description"":""""},{""name"":""BGX.H2_STAB"",""value"":""45"",""unit"":""mm"",""description"":""""},{""name"":""BGX.D2_STAB2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGX.H2_STAB2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGX.RINGMATERIALAUSSEN"",""value"":""SE-CU"",""unit"":"""",""description"":""""},{""name"":""BGX.RINGMATERIALINNEN"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGX.B2_RING"",""value"":""18"",""unit"":""mm"",""description"":""""},{""name"":""BGX.H2_RING"",""value"":""60"",""unit"":""mm"",""description"":""""},{""name"":""BGX.B2_RING2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGX.H2_RING2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGB.D_AUSSEN"",""value"":""780"",""unit"":""mm"",""description"":""""},{""name"":""BGB.D_INNEN"",""value"":""550"",""unit"":""mm"",""description"":""""},{""name"":""BGB.L_PAKET"",""value"":""684"",""unit"":""mm"",""description"":""""},{""name"":""BGB.L_TOTAL"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGB.Z_KANAL_RAD"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGB.Z_NUTEN"",""value"":""72"",""unit"":"""",""description"":""""},{""name"":""BGB.NUTENFORM"",""value"":""NTF"",""unit"":"""",""description"":""""},{""name"":""BGB.BLECHSORTE"",""value"":""M470P65A"",""unit"":"""",""description"":""""},{""name"":""BGB.B_KANAL_RAD"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGB.B_NUT2"",""value"":""15.30"",""unit"":""mm"",""description"":""""},{""name"":""BGB.B_NUT"",""value"":""11.10"",""unit"":""mm"",""description"":""""},{""name"":""BGB.H_NUT"",""value"":""56.00"",""unit"":""mm"",""description"":""""},{""name"":""BGB.H_KEIL"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGB.H_STEG"",""value"":""2"",""unit"":""mm"",""description"":""""},{""name"":""BGB.B_STEG"",""value"":""4.30"",""unit"":""mm"",""description"":""""},{""name"":""BGB.B_STEG2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGB.B2_SCHRAEG_AX"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGB.ENDBLECHVERSTAERKUNG"",""value"":""0"",""unit"":""mm"",""description"":""""},{""name"":""BGB.D_KANAL1"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGB.Z_KANAL"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGB.KUEHLNUTENFORM"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGB.B_KANAL"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGB.B_KANAL2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGB.H_KANAL"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGB.K_EISEN"",""value"":""0.96"",""unit"":"""",""description"":""""},{""name"":""BGB.B_NUT3"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.D2_INNEN"",""value"":""170"",""unit"":""mm"",""description"":""""},{""name"":""BGC.L2_PAKET"",""value"":""684"",""unit"":""mm"",""description"":""""},{""name"":""BGC.L2_TOTAL"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.Z2_KANAL_RAD"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGC.Z2_NUTEN"",""value"":""54"",""unit"":"""",""description"":""""},{""name"":""BGC.NUTENFORM"",""value"":""NRA"",""unit"":"""",""description"":""""},{""name"":""BGC.BLECHSORTE"",""value"":""M470P65A"",""unit"":"""",""description"":""""},{""name"":""BGC.B2_KANAL_RAD"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.B2_STEG"",""value"":""4.0"",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_STEG"",""value"":""2"",""unit"":""mm"",""description"":""""},{""name"":""BGC.B2_STEG2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_STEG2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.B2_STEG3"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_STEG3"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.D2_NUT"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_NUT2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.B2_NUT"",""value"":""9.7"",""unit"":""mm"",""description"":""""},{""name"":""BGC.B2_NUT2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_NUT"",""value"":""45.7"",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_KEIL"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_NUT4"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.ALFA2_SCHRAEG2"",""value"":"""",""unit"":""\u00b0"",""description"":""""},{""name"":""BGC.B2_SCHRAEG_AX"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.ALFA2_SCHRAEG_AX"",""value"":"""",""unit"":""\u00b0"",""description"":""""},{""name"":""BGC.ENDBLECHVERSTAERKUNG"",""value"":""8"",""unit"":""mm"",""description"":""""},{""name"":""BGC.A_LUFTSPALT"",""value"":""2"",""unit"":""mm"",""description"":""""},{""name"":""BGC.D2_KANAL1"",""value"":""317"",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_KANAL1"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.D2_KANAL2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_KANAL2"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.D2_KANAL3"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_KANAL3"",""value"":"""",""unit"":""mm"",""description"":""""},{""name"":""BGC.Z2_KANAL"",""value"":""36"",""unit"":"""",""description"":""""},{""name"":""BGC.KUEHLNUTENFORM"",""value"":""NAT"",""unit"":"""",""description"":""""},{""name"":""BGC.B2_KANAL"",""value"":""10"",""unit"":""mm"",""description"":""""},{""name"":""BGC.B2_KANAL2"",""value"":""16.7"",""unit"":""mm"",""description"":""""},{""name"":""BGC.H2_KANAL"",""value"":""50"",""unit"":""mm"",""description"":""""},{""name"":""BGC.K2_EISEN"",""value"":""0.96"",""unit"":"""",""description"":""""},{""name"":""GA.BGW.Z_LEITER_NUT_STRING"",""value"":""3+3"",""unit"":"""",""description"":""""},{""name"":""GA.BGW.Z_PARALLEL"",""value"":""3"",""unit"":"""",""description"":""""},{""name"":""BGZ.m2_gegen_IstQuadratisch"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGZ.m2_gegen_istKonstant"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGZ.m2_gegen_istBeliebig"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGZ.FREMDTRAEGHEITSMOMENT"",""value"":"""",""unit"":""kgm^2"",""description"":""""},{""name"":""BGZ.MOTORTRAEGHEITSMOMENT"",""value"":"""",""unit"":""kgm^2"",""description"":""""},{""name"":""BGZ.m2_gegen_relativ"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.CHECK_YD"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGZ.CHECK_LISTENWERTE"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGZ.URED"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGZ.MKORR"",""value"":"""",""unit"":"""",""description"":""""},{""name"":""BGZ.X_NETZ"",""value"":"""",""unit"":""Ohm"",""description"":""""},{""name"":""BGZ.M2_GEGEN_BEZUGSWERT"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN_HOCH"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN_STILLSTAND"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN_KONSTANT"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.N2_GEGEN.0"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.N2_GEGEN.1"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.N2_GEGEN.2"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.N2_GEGEN.3"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.N2_GEGEN.4"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.N2_GEGEN.5"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.N2_GEGEN.6"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.N2_GEGEN.7"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.N2_GEGEN.8"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.N2_GEGEN.9"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.N2_GEGEN.10"",""value"":"""",""unit"":""1/min"",""description"":""""},{""name"":""BGZ.M2_GEGEN.0"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN.1"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN.2"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN.3"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN.4"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN.5"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN.6"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN.7"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN.8"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN.9"",""value"":"""",""unit"":""Nm"",""description"":""""},{""name"":""BGZ.M2_GEGEN.10"",""value"":"""",""unit"":""Nm"",""description"":""""}]}";
    }
    }
}