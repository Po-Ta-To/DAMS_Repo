using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DAMS_03.Models
{
    //public class CustomValidationAttributes
    //{
    //}

    namespace CustomValidationAttributeDemo.ValidationAttributes
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public sealed class ValidBirthDate : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    DateTime _birthJoin = Convert.ToDateTime(value);
                    if (_birthJoin > DateTime.Now)
                    {
                        return new ValidationResult("Birth date can not be greater than current date.");
                    }
                }
                return ValidationResult.Success;
            }
        }
        
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        public class ValidPriceRange : ValidationAttribute
        {
            public string DependentProperty { get; set; }

            public ValidPriceRange(string dependentProperty)
            {
                this.DependentProperty = dependentProperty;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    var containerType = validationContext.ObjectInstance.GetType();
                    var field = containerType.GetProperty(this.DependentProperty);
                    if (field != null)
                    {
                        var dependentvalue = field.GetValue(validationContext.ObjectInstance, null);
                        decimal price = (decimal)value;
                        decimal priceCompare = (decimal)dependentvalue;
                        if (price > priceCompare)
                        {
                            return new ValidationResult("Min price can not be greater than max price.");
                        }
                    }

                }
                return ValidationResult.Success;
            }
        }

        //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        //public class ValidPrice : ValidationAttribute
        //{

        //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //    {
        //        decimal price = (decimal)value;
        //        var model = (AddTreatmentsModel)validationContext.ObjectInstance;
        //        decimal priceCompare = model.PriceHigh;
        //        if (price > priceCompare)
        //        {
        //            return new ValidationResult("Min price can not be greater than max price.");
        //        }
        //        return ValidationResult.Success;
        //    }

        //}

        //[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
        //public class ValidPriceRange : ValidationAttribute, IClientValidatable
        //{
        //    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    private ValidPrice _innerAttribute = new ValidPrice();

        //    public string DependentProperty { get; set; }
        //    public object TargetValue { get; set; }

        //    public ValidPriceRange(string dependentProperty, object targetValue)
        //    {
        //        this.DependentProperty = dependentProperty;
        //        this.TargetValue = targetValue;
        //    }

        //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        //    {
        //        if (value != null)
        //        {
        //            //decimal price = (decimal)value;
        //            //var model = (AddTreatmentsModel)validationContext.ObjectInstance;
        //            var containerType = validationContext.ObjectInstance.GetType();
        //            var field = containerType.GetProperty(this.DependentProperty);
        //            if (field != null)
        //            {
        //                var dependentvalue = field.GetValue(validationContext.ObjectInstance, null);
        //                if ((dependentvalue == null && this.TargetValue == null) ||
        //                                (dependentvalue != null && dependentvalue.Equals(this.TargetValue)))
        //                {
        //                    if (!_innerAttribute.IsValid(value))
        //                        // validation failed - return an error
        //                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
        //                }
        //            }

        //            //decimal priceCompare = model.PriceHigh;
        //            //if (price > priceCompare)
        //            //{
        //            //    return new ValidationResult("Min price can not be greater than max price.");
        //            //}
        //        }
        //        return ValidationResult.Success;
        //    }
        //}



    }


}