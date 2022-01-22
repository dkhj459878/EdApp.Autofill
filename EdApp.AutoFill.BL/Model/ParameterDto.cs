﻿using EdApp.AutoFill.DAL.Contract;

namespace EdApp.AutoFill.BL.Model
{
    /// <summary>
    ///     Contains parameters' information.
    /// </summary>
    public class ParameterDto : IIdentifier
    {
        /// <summary>
        ///     Parameter name. For example: WE.WEK.WINDING_TYPE.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Model type identifier. For example: Request, Response or Common.
        /// </summary>
        public int ModelTypeId { get; set; }

        /// <summary>
        ///     Model type. For example: Request, Response or Common.
        /// </summary>
        public ModelTypeDto ModelType { get; set; }

        /// <summary>
        ///     Calculation type identifier. For example: WindingDesignRoundWire or WindingDesignRoundWire, etc.
        /// </summary>
        public int CalculationTypeId { get; set; }

        /// <summary>
        ///     Calculation type. For example: WindingDesignRoundWire or WindingDesignRoundWire, etc.
        /// </summary>
        public CalculationTypeDto CalculationType { get; set; }

        /// <summary>
        ///     Shows if parameter is required.
        /// </summary>
        public bool MandatoryParameter { get; set; }

        /// <summary>
        ///     Shows if parameter value is required.
        /// </summary>
        public bool MandatoryValue { get; set; }

        /// <summary>
        ///     Variable name. For example: z_leiter_nut_string.
        /// </summary>
        public string VariableName { get; set; }

        /// <summary>
        ///     Parameter description in English languages.
        /// </summary>
        public string DescriptionEn { get; set; }

        /// <summary>
        ///     Unit name. For example: mm, kg, etc.
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        ///     Data type name. For example: int, string, bool, and etc.
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        ///     Field name. For example: Type and etc.
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        ///     Shows if parameter value should be counted in the hash calculation.
        /// </summary>
        public bool RelevantForHash { get; set; }

        /// <summary>
        ///     Name in the UI. For example: Stator winding type.
        /// </summary>
        public string UIName { get; set; }

        /// <summary>
        ///     Identifier.
        /// </summary>
        public int Id { get; set; }
    }
}