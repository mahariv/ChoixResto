using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace ChoixResto.Models
{
    public class AuMoinsUnDesDeuxAttribute : ValidationAttribute, IClientValidatable
    {

        public String Parametre1 { get; set; }
        public String Parametre2 { get; set; }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo[] properties = validationContext.ObjectType.GetProperties();
            PropertyInfo info1 = properties.FirstOrDefault(elt => elt.Name == Parametre1);
            PropertyInfo info2 = properties.FirstOrDefault(elt => elt.Name == Parametre2);

            string valeur1 = info1.GetValue(validationContext.ObjectInstance) as string;
            string valeur2 = info2.GetValue(validationContext.ObjectInstance) as string;

            if (String.IsNullOrWhiteSpace(valeur1) && String.IsNullOrWhiteSpace(valeur2))
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            ModelClientValidationRule regle = new ModelClientValidationRule();
            regle.ValidationType = "verifcontact";
            regle.ErrorMessage = ErrorMessage;
            regle.ValidationParameters.Add("parametre1", Parametre1);
            regle.ValidationParameters.Add("parametre2", Parametre2);
            return new List<ModelClientValidationRule> { regle };
        }
    }
}