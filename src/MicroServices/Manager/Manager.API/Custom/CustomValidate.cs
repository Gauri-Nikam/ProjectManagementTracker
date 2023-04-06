using System.ComponentModel.DataAnnotations;

namespace Manager.API.Custom
{
    public class CustomValidate: ValidationAttribute
    {
        /// <summary>
        /// Return false if value is null and viceversa
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
