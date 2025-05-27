// ReSharper disable InconsistentNaming
namespace Presidio.Models;

/// <summary>
/// Enumeration of Personally Identifiable Information (PII) entity types for detection and classification
/// </summary>
public enum PIIEntityTypes
{
    UNKNOWN,

    // Global Entity Types

    /// <summary>
    /// A credit card number is between 12 and 19 digits.
    /// Detection Method: Pattern match and checksum
    /// Reference: https://en.wikipedia.org/wiki/Payment_card_number
    /// </summary>
    CREDIT_CARD,

    /// <summary>
    /// A Crypto wallet number. Currently only Bitcoin address is supported.
    /// Detection Method: Pattern match, context and checksum
    /// </summary>
    CRYPTO,

    /// <summary>
    /// Absolute or relative dates or periods or times smaller than a day.
    /// Detection Method: Pattern match and context
    /// </summary>
    DATE_TIME,

    /// <summary>
    /// An email address identifies an email box to which email messages are delivered.
    /// Detection Method: Pattern match, context and RFC-822 validation
    /// </summary>
    EMAIL_ADDRESS,

    /// <summary>
    /// The International Bank Account Number (IBAN) is an internationally agreed system of identifying bank accounts across national borders to facilitate the communication and processing of cross border transactions with a reduced risk of transcription errors.
    /// Detection Method: Pattern match, context and checksum
    /// </summary>
    IBAN_CODE,

    /// <summary>
    /// An Internet Protocol (IP) address (either IPv4 or IPv6).
    /// Detection Method: Pattern match, context and checksum
    /// </summary>
    IP_ADDRESS,

    /// <summary>
    /// A person's Nationality, religious or political group.
    /// Detection Method: Custom logic and context
    /// </summary>
    NRP,

    /// <summary>
    /// Name of politically or geographically defined location (cities, provinces, countries, international regions, bodies of water, mountains).
    /// Detection Method: Custom logic and context
    /// </summary>
    LOCATION,

    /// <summary>
    /// A full person name, which can include first names, middle names or initials, and last names.
    /// Detection Method: Custom logic and context
    /// </summary>
    PERSON,

    /// <summary>
    /// A telephone number.
    /// Detection Method: Custom logic, pattern match and context
    /// </summary>
    PHONE_NUMBER,

    /// <summary>
    /// Common medical license numbers.
    /// Detection Method: Pattern match, context and checksum
    /// </summary>
    MEDICAL_LICENSE,

    /// <summary>
    /// A URL (Uniform Resource Locator), unique identifier used to locate a resource on the Internet.
    /// Detection Method: Pattern match, context and top level url validation
    /// </summary>
    URL,

    // USA Entity Types

    /// <summary>
    /// A US bank account number is between 8 and 17 digits.
    /// Detection Method: Pattern match and context
    /// </summary>
    US_BANK_NUMBER,

    /// <summary>
    /// A US driver license according to https://ntsi.com/drivers-license-format/.
    /// Detection Method: Pattern match and context
    /// </summary>
    US_DRIVER_LICENSE,

    /// <summary>
    /// US Individual Taxpayer Identification Number (ITIN). Nine digits that start with a "9" and contain a "7" or "8" as the 4th digit.
    /// Detection Method: Pattern match and context
    /// </summary>
    US_ITIN,

    /// <summary>
    /// A US passport number with 9 digits.
    /// Detection Method: Pattern match and context
    /// </summary>
    US_PASSPORT,

    /// <summary>
    /// A US Social Security Number (SSN) with 9 digits.
    /// Detection Method: Pattern match and context
    /// </summary>
    US_SSN,

    // UK Entity Types

    /// <summary>
    /// A UK NHS number is 10 digits.
    /// Detection Method: Pattern match, context and checksum
    /// </summary>
    UK_NHS,

    /// <summary>
    /// UK National Insurance Number is a unique identifier used in the administration of National Insurance and tax.
    /// Detection Method: Pattern match and context
    /// </summary>
    UK_NINO,

    // Spain Entity Types

    /// <summary>
    /// A spanish NIF number (Personal tax ID).
    /// Detection Method: Pattern match, context and checksum
    /// </summary>
    ES_NIF,

    /// <summary>
    /// A spanish NIE number (Foreigners ID card).
    /// Detection Method: Pattern match, context and checksum
    /// </summary>
    ES_NIE,

    // Italy Entity Types

    /// <summary>
    /// An Italian personal identification code.
    /// Detection Method: Pattern match, context and checksum
    /// Reference: https://en.wikipedia.org/wiki/Italian_fiscal_code
    /// </summary>
    IT_FISCAL_CODE,

    /// <summary>
    /// An Italian driver license number.
    /// Detection Method: Pattern match and context
    /// </summary>
    IT_DRIVER_LICENSE,

    /// <summary>
    /// An Italian VAT code number.
    /// Detection Method: Pattern match, context and checksum
    /// </summary>
    IT_VAT_CODE,

    /// <summary>
    /// An Italian passport number.
    /// Detection Method: Pattern match and context
    /// </summary>
    IT_PASSPORT,

    /// <summary>
    /// An Italian identity card number.
    /// Detection Method: Pattern match and context
    /// Reference: https://en.wikipedia.org/wiki/Italian_electronic_identity_card
    /// </summary>
    IT_IDENTITY_CARD,

    // Poland Entity Types

    /// <summary>
    /// Polish PESEL number.
    /// Detection Method: Pattern match, context and checksum
    /// </summary>
    PL_PESEL,

    // Singapore Entity Types

    /// <summary>
    /// A National Registration Identification Card.
    /// Detection Method: Pattern match and context
    /// </summary>
    SG_NRIC_FIN,

    /// <summary>
    /// A Unique Entity Number (UEN) is a standard identification number for entities registered in Singapore.
    /// Detection Method: Pattern match, context, and checksum
    /// </summary>
    SG_UEN,

    // Australia Entity Types

    /// <summary>
    /// The Australian Business Number (ABN) is a unique 11 digit identifier issued to all entities registered in the Australian Business Register (ABR).
    /// Detection Method: Pattern match, context, and checksum
    /// </summary>
    AU_ABN,

    /// <summary>
    /// An Australian Company Number is a unique nine-digit number issued by the Australian Securities and Investments Commission to every company registered under the Commonwealth Corporations Act 2001 as an identifier.
    /// Detection Method: Pattern match, context, and checksum
    /// </summary>
    AU_ACN,

    /// <summary>
    /// The tax file number (TFN) is a unique identifier issued by the Australian Taxation Office to each taxpaying entity.
    /// Detection Method: Pattern match, context, and checksum
    /// </summary>
    AU_TFN,

    /// <summary>
    /// Medicare number is a unique identifier issued by Australian Government that enables the cardholder to receive a rebates of medical expenses under Australia's Medicare system.
    /// Detection Method: Pattern match, context, and checksum
    /// </summary>
    AU_MEDICARE,

    // India Entity Types

    /// <summary>
    /// The Indian Permanent Account Number (PAN) is a unique 12 character alphanumeric identifier issued to all business and individual entities registered as Tax Payers.
    /// Detection Method: Pattern match, context
    /// </summary>
    IN_PAN,

    /// <summary>
    /// Indian government issued unique 12 digit individual identity number.
    /// Detection Method: Pattern match, context, and checksum
    /// </summary>
    IN_AADHAAR,

    /// <summary>
    /// Indian government issued transport (govt, personal, diplomatic, defence) vehicle registration number.
    /// Detection Method: Pattern match, context, and checksum
    /// </summary>
    IN_VEHICLE_REGISTRATION,

    /// <summary>
    /// Indian Election Commission issued 10 digit alpha numeric voter id for all indian citizens (age 18 or above).
    /// Detection Method: Pattern match, context
    /// </summary>
    IN_VOTER,

    /// <summary>
    /// Indian Passport Number.
    /// Detection Method: Pattern match, Context
    /// </summary>
    IN_PASSPORT,

    // Finland Entity Types

    /// <summary>
    /// The Finnish Personal Identity Code (Henkilötunnus) is a unique 11 character individual identity number.
    /// Detection Method: Pattern match, context and custom logic
    /// </summary>
    FI_PERSONAL_IDENTITY_CODE
}