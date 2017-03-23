using System.Globalization;
using System.Reflection;
using System.Text;
using CsvHelper;

namespace Cake.CsvHelper {

    /// <summary>
    /// Contains settings used by <see cref="CsvHelpers"/>.
    /// </summary>
    public class CsvHelperSettings {
        /// <summary>
		/// Gets or sets a value indicating if comments are allowed.
		/// </summary>
		/// <value>
		/// <c>true</c> to allow commented out lines; otherwise, <c>false</c>.
		/// </value>
        public bool AllowComments { get; set; }

        /// <summary>
		/// Gets or sets the size of the buffer used for reading and writing CSV files.
		/// </summary>
		/// <value>
		/// Default is 2048.
		/// </value>
        public int BufferSize { get; set; }

        /// <summary>
		/// Gets or sets the character used to denote a line that is commented out.
		/// </summary>
		/// <value>
		/// Default is '#'.
		/// </value>
        public string Comment { get; set; }

        /// <summary>
		/// Gets or sets a value indicating whether the number of bytes should be counted while parsing. Default is false. This will slow down parsing
		/// because it needs to get the byte count of every char for the given encoding.
		/// The <see cref="Encoding"/> needs to be set correctly for this to be accurate.
		/// </summary>
        public bool CountBytes { get; set; }

        /// <summary>
		/// Gets or sets the culture info used to read an write CSV files.
		/// </summary>
        public CultureInfo CultureInfo { get; set; }

        /// <summary>
		/// Gets or sets the value used to separate the fields in a CSV row.
		/// </summary>
		/// <value>
		///  Defaults to <c>Encoding.UTF8</c>.
		/// </value>
        public string Delimiter { get; set; }

        /// <summary>
		/// Gets or sets a value indicating whether changes in the column count should be detected. If true, a <see cref="CsvBadDataException"/> will be thrown if a different column count is detected.
		/// </summary>
		/// <value>
		///  <c>true</c> if [detect column count changes]; otherwise, <c>false</c>.
		/// </value>
        public bool DetectColumnCountChanges { get; set; }

        /// <summary>
		/// Gets or sets the encoding used when counting bytes.
		/// </summary>
		/// <value>
		///  Defaults to <c>Encoding.UTF8</c>.
		/// </value>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the CSV file has a header record.
        /// </summary>
        public bool HasHeaderRecord { get; set; }

        /// <summary>
        /// Gets or sets a value indicating to ignore white space in the headers when matching the columns to the properties by name.
        /// </summary>
        public bool IgnoreHeaderWhiteSpace { get; set; }

        /// <summary>
        /// Gets or sets a value indicating to ignore private accessors when reading and writing. By default you can't read from a private getter or 
        /// write to a private setter. Turn this on will allow that. Properties that can't be read from or written to are silently ignored.
        /// </summary>
        public bool IgnorePrivateAccessor { get; set; }

        /// <summary>
		/// Gets or sets a value indicating whether exceptions that occur duruing reading should be ignored.
		/// </summary>
		/// <value>
        ///  <c>true</c> if to ignore exceptions; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreReadingExceptions { get; set; }

        /// <summary>
		/// Gets or sets a value indicating if quotes should be ingored when parsing and treated like any other character.
		/// </summary>
		/// <value>
        ///  <c>true</c> if to ignore qoutes; otherwise, <c>false</c>.
        /// </value>
        public bool IgnoreQoutes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether matching CSV header names will be case sensitive.
        /// </summary>
        public bool IsHeaderCaseSensitive { get; set; }

        /// <summary>
        /// Gets or sets the property/field binding flags. This determines what properties/fields on the custom class are used. Default is Public | Instance.
        /// </summary>
        /// <value>
        ///  Defaults to <c>Public | Instance</c>.
        /// </value>
        public BindingFlags PropertyBindingFlags { get; set; }

        /// <summary>
        /// Gets or sets a value used to escape fields that contain a delimiter, quote, or line ending.
        /// </summary>
        public string Qoute { get; set; }

        /// <summary>
		/// Gets or sets a value indicating whether all fields are quoted when writing, or just ones that have to be. <see cref="QouteAllFields"/> and
		/// <see cref="QouteNoFields"/> cannot be true at the same time. Turning one on will turn the other off.
		/// </summary>
		/// <value>
		///   <c>true</c> if all fields should be quoted; otherwise, <c>false</c>.
		/// </value>
        public bool QouteAllFields { get; set; }

        /// <summary>
		/// Gets or sets a value indicating whether no fields are quoted when writing. <see cref="QouteAllFields"/> and <see cref="QouteNoFields"/> cannot be true 
		/// at the same time. Turning one on will turn the other off.
		/// </summary>
		/// <value>
		///   <c>true</c> if [quote no fields]; otherwise, <c>false</c>.
		/// </value>
        public bool QouteNoFields { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether empty rows should be skipped when reading. A record is considered empty if all fields are empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [skip empty rows]; otherwise, <c>false</c>.
        /// </value>
        public bool SkipEmptyRecords { get; set; }

        /// <summary>
        /// Gets or sets a value indicating if the reader to trim whitespace from the beginning and ending of the field value when reading.
        /// </summary>
        public bool TrimFields { get; set; }

        /// <summary>
        /// Gets or sets a value indicating ifthe reader to ignore white space from the beginning and ending of the headers 
        /// when matching the columns to the properties by name.
        /// </summary>
        public bool TrimHeaders { get; set; }

        /// <summary>
		/// Gets or sets a value indicating if an exception will be thrown if a field defined in a mapping is missing. True to throw an exception, otherwise false. Default is true.
		/// </summary>
		/// <value>
        ///  <c>true</c> if an exception is to be thrown; otherwise, <c>false</c>. Defaults to <c>true</c>.
        /// </value>
        public bool WillThrowOnMissingField { get; set; }

        /// <summary>
        /// Initializes the <see cref="CsvHelperSettings"/> class.
        /// </summary>
        public CsvHelperSettings() {
            Encoding = Encoding.UTF8;
        }
    }
}
