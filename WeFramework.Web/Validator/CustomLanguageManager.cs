using FluentValidation.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Validator
{
    public class CustomLanguageManager: LanguageManager
    {
        public CustomLanguageManager()
        {
            AddTranslation("zh-CN", "EmailValidator", "{PropertyName}不是有效的电子邮件地址。");
            AddTranslation("zh-CN", "GreaterThanOrEqualValidator", "{PropertyName}必须大于或等于{ComparisonValue}。");
            AddTranslation("zh-CN", "GreaterThanValidator", "{PropertyName}必须大于{ComparisonValue}。");
            AddTranslation("zh-CN", "LengthValidator", "{PropertyName}的长度必须在{MinLength}到{MaxLength}字符。");
            AddTranslation("zh-CN", "MinimumLengthValidator", "{PropertyName}必须大于或等于{MinLength}个字符。");
            AddTranslation("zh-CN", "MaximumLengthValidator", "{PropertyName}必须小于或等于{MaxLength}个字符。");
            AddTranslation("zh-CN", "LessThanOrEqualValidator", "{PropertyName}必须小于或等于{ComparisonValue}。");
            AddTranslation("zh-CN", "LessThanValidator", "{PropertyName}必须小于{ComparisonValue}。");
            AddTranslation("zh-CN", "NotEmptyValidator", "{PropertyName}不能为空。");
            AddTranslation("zh-CN", "NotEqualValidator", "{PropertyName}不能和{ComparisonValue}相等。");
            AddTranslation("zh-CN", "NotNullValidator", "{PropertyName}不能为空。");
            AddTranslation("zh-CN", "PredicateValidator", "{PropertyName}不符合指定的条件。");
            AddTranslation("zh-CN", "AsyncPredicateValidator", "{PropertyName}不符合指定的条件。");
            AddTranslation("zh-CN", "RegularExpressionValidator", "{PropertyName}的格式不正确。");
            AddTranslation("zh-CN", "EqualValidator", "{PropertyName}应该和{ComparisonValue}相等。");
            AddTranslation("zh-CN", "ExactLengthValidator", "{PropertyName}必须是{MaxLength}个字符。");
            AddTranslation("zh-CN", "InclusiveBetweenValidator", "{PropertyName}必须在{From}(包含)和{To}(包含)之间，您输入了{Value}");
            AddTranslation("zh-CN", "ExclusiveBetweenValidator", "{PropertyName}必须在{From}(不包含)和{To}(不包含)之间，您输入了{Value}。");
            AddTranslation("zh-CN", "CreditCardValidator", "{PropertyName}不是有效的信用卡号。");
            AddTranslation("zh-CN", "ScalePrecisionValidator", "{PropertyName}总位数不能超过{ExpectedPrecision}位，其中小数部分{ExpectedScale}位。您共计输入了{Digits}位数字，其中小数部分{ActualScale}位。");
            AddTranslation("zh-CN", "EmptyValidator", "{PropertyName}必须为空。");
            AddTranslation("zh-CN", "NullValidator", "{PropertyName}必须为空。");
            AddTranslation("zh-CN", "EnumValidator", "{PropertyName}的值范围不包含{PropertyValue}。");
        }
    }
}