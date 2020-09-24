using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ClothingStoreFranchise.NetCore.Common.Extensible
{
    public class ExtendedAttributeModel : IEquatable<ExtendedAttributeModel>
    {
        [Required]
        public string AttributeName { get; set; }

        [Required]
        public ValueType ValueType { get; set; }

        public string SerializedEnumValues
        {
            get
            {
                if (ValueType is Enum)
                {
                    return JsonConvert.SerializeObject(EnumValues);
                }
                return null;
            }
            set
            {
                EnumValues = string.IsNullOrWhiteSpace(value)
                    ? null
                    : JsonConvert.DeserializeObject<List<string>>(value);
            }
        }

        [NotMapped]
        public ICollection<string> EnumValues { get; set; }

        #region "Equals"

        public bool Equals(ExtendedAttributeModel other)
        {
            return other != null
                && AttributeName == other.AttributeName
                && ValueType == other.ValueType
                && CheckEnumValuesAreEqual(other);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ExtendedAttributeModel);
        }

        public override int GetHashCode()
        {
            const int constant = 197;
            return AttributeName.GetHashCode() * constant
                + ValueType.GetHashCode()
                + SerializedEnumValues.GetHashCode()
                + EnumValues.GetHashCode();
        }

        private bool CheckEnumValuesAreEqual(ExtendedAttributeModel other)
        {
            return SerializedEnumValues == other.SerializedEnumValues
                && Equals(EnumValues, other.EnumValues);
        }

        #endregion
    }
}
