namespace HeraAI.API.Enums
{

    /// <summary>
    /// Lenght type that a string can contain
    /// </summary>
    public enum StringLengthTypes
    {
        xSmall,
        small,
        medium,
        large,
        xLarge,
        xxLarge,
        fourkLarge,
        xxxLarge
    }


    /// <summary>
    /// Content types that a string can contain
    /// </summary>
    public enum StringContentTypes
    {
        identifier,
        regularText,
        email,
        phone,
        url,
        htmlTag,
        acronym
    }


    /// <summary>
    /// Allowed types of datetime
    /// </summary>
    public enum DateTimeAllowedPeriods
    {
        undefinedPast,      // PAST TO PRESENT WITH UNDEFINED DATE
        pastToPresent,
        nearPastToNearFuture,
        present,
        presentToNearFuture,
        presentToLongFuture,
        all
    }


    /// <summary>
    /// Allowed types of timeonly
    /// </summary>
    public enum TimeOnlyAllowedPeriods
    {
        undefinedPast,
        hours,
        seconds
    }


    /// <summary>
    /// Allowed formats of datetime
    /// </summary>
    public enum DateTimeFormats
    {
        fullDateTime,
        fullDateTime2,
        dateTime,
        date,
        hour
    }


    /// <summary>
    /// Allowed formats of datetime
    /// </summary>
    public enum TimeOnlyFormats
    {
        second,
        hour
    }


    /// <summary>
    /// Allowed intervals of Interger
    /// </summary>
    public enum IntegerAllowedIntervals
    {
        all,
        negativeWithoutZero,
        negativeWithZero,
        positiveWithZero,
        positiveWithoutZero,
        intIdentifier,
        longIdentifier,
        shortPositiveWithoutZero,
        shortPositiveWithZero,
        percentage
    }


    /// <summary>
    /// Allowed intervals of double
    /// </summary>
    public enum DoubleAllowedIntervals
    {
        all,
        negativeWithoutZero,
        negativeWithZero,
        positiveWithZero,
        positiveWithoutZero,
        percentage
    }

}
