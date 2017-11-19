// 
//   SubSonic - http://subsonicproject.com
// 
//   The contents of this file are subject to the New BSD
//   License (the "License"); you may not use this file
//   except in compliance with the License. You may obtain a copy of
//   the License at http://www.opensource.org/licenses/bsd-license.php
//  
//   Software distributed under the License is distributed on an 
//   "AS IS" basis, WITHOUT WARRANTY OF ANY KIND, either express or
//   implied. See the License for the specific language governing
//   rights and limitations under the License.
// 
/// <summary>
/// Summary for the RegexPattern class
/// </summary>
namespace CoreExtensions
{
    internal class RegexPattern
    {
        internal const string ALPHABETIC = "[^a-zA-Z]";
        internal const string ALPHABETIC_NUMERIC = "[^a-zA-Z0-9]";
        internal const string ALPHABETIC_NUMERIC_SPACE = @"[^a-zA-Z0-9\s]";
        internal const string CREDIT_CARD_AMERICAN_EXPRESS = @"^(?:(?:[3][4|7])(?:\d{13}))$";
        internal const string CREDIT_CARD_CARTE_BLANCHE = @"^(?:(?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12}))$";
        internal const string CREDIT_CARD_DINERS_CLUB = @"^(?:(?:[3](?:[0][0-5]|[6|8]))(?:\d{11,12}))$";
        internal const string CREDIT_CARD_DISCOVER = @"^(?:(?:6011)(?:\d{12}))$";
        internal const string CREDIT_CARD_EN_ROUTE = @"^(?:(?:[2](?:014|149))(?:\d{11}))$";
        internal const string CREDIT_CARD_JCB = @"^(?:(?:(?:2131|1800)(?:\d{11}))$|^(?:(?:3)(?:\d{15})))$";
        internal const string CREDIT_CARD_MASTER_CARD = @"^(?:(?:[5][1-5])(?:\d{14}))$";
        internal const string CREDIT_CARD_STRIP_NON_NUMERIC = @"(\-|\s|\D)*";
        internal const string CREDIT_CARD_VISA = @"^(?:(?:[4])(?:\d{12}|\d{15}))$";
        internal const string EMAIL = @"^([0-9a-zA-Z]+[-._+&])*[0-9a-zA-Z]+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$";
        internal const string EMBEDDED_CLASS_NAME_MATCH = "(?<=^_).*?(?=_)";
        internal const string EMBEDDED_CLASS_NAME_REPLACE = "^_.*?_";
        internal const string EMBEDDED_CLASS_NAME_UNDERSCORE_MATCH = "(?<=^UNDERSCORE).*?(?=UNDERSCORE)";
        internal const string EMBEDDED_CLASS_NAME_UNDERSCORE_REPLACE = "^UNDERSCORE.*?UNDERSCORE";
        internal const string GUID = "[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}";
        internal const string IP_ADDRESS = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
        internal const string LOWER_CASE = @"^[a-z]+$";
        internal const string NUMERIC = "[^0-9]";
        internal const string SOCIAL_SECURITY = @"^\d{3}[-]?\d{2}[-]?\d{4}$";
        internal const string SQL_EQUAL = @"\=";
        internal const string SQL_GREATER = @"\>";
        internal const string SQL_GREATER_OR_EQUAL = @"\>.*\=";
        internal const string SQL_IS = @"\x20is\x20";
        internal const string SQL_IS_NOT = @"\x20is\x20not\x20";
        internal const string SQL_LESS = @"\<";
        internal const string SQL_LESS_OR_EQUAL = @"\<.*\=";
        internal const string SQL_LIKE = @"\x20like\x20";
        internal const string SQL_NOT_EQUAL = @"\<.*\>";
        internal const string SQL_NOT_LIKE = @"\x20not\x20like\x20";

        internal const string STRONG_PASSWORD =
            @"(?=^.{8,255}$)((?=.*\d)(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[^A-Za-z0-9])(?=.*[a-z])|(?=.*[^A-Za-z0-9])(?=.*[A-Z])(?=.*[a-z])|(?=.*\d)(?=.*[A-Z])(?=.*[^A-Za-z0-9]))^.*";

        internal const string UPPER_CASE = @"^[A-Z]+$";
        internal const string URL = @"^^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_=]*)?$";
        internal const string US_CURRENCY = @"^\$(([1-9]\d*|([1-9]\d{0,2}(\,\d{3})*))(\.\d{1,2})?|(\.\d{1,2}))$|^\$[0](.00)?$";
        internal const string US_TELEPHONE = @"^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$";
        internal const string US_ZIPCODE = @"^\d{5}$";
        internal const string US_ZIPCODE_PLUS_FOUR = @"^\d{5}((-|\s)?\d{4})$";
        internal const string US_ZIPCODE_PLUS_FOUR_OPTIONAL = @"^\d{5}((-|\s)?\d{4})?$";
    }
}