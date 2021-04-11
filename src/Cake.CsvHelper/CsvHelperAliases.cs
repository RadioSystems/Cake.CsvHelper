using System;
using System.Collections.Generic;
using System.Globalization;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using CsvHelper.Configuration;

namespace Cake.CsvHelper
{
    /// <summary>
    /// Contains functionality related to reading and writing CSV files.
    /// </summary>
    [CakeAliasCategory("CsvHelper")]
    public static class CsvHelperAliases
    {
        /// <summary>
        /// Reads a CSV file into a C# object.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to read.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>
        ///     <![CDATA[var people = ReadCsv<Person>("./people.csv");]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("ReadCsv")]
        public static IEnumerable<T> ReadCsv<T>(this ICakeContext context, FilePath csvFile) {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
            return ReadCsv<T>(context, csvFile, null, configuration);
        }

        /// <summary>
        /// Reads a CSV file into a C# object.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to read.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>
        ///     <![CDATA[var people = ReadCsv<Person>("./people.csv");]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("ReadCsv")]
        public static IEnumerable<T> ReadCsv<T>(this ICakeContext context, FilePath csvFile, CsvConfiguration configuration) {
           return ReadCsv<T>(context, csvFile, null, configuration);
        }

        /// <summary>
        /// Reads a CSV file into a C# object.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="classMap">The CSV class map.</param>
        /// <param name="csvFile">The CSV to read.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>     
        ///     <![CDATA[var people = ReadCsv<Person>("./people.csv", new ClassMap());]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("ReadCsv")]
        [CakeNamespaceImport("CsvHelper.Configuration.CsvClassMap")]
        public static IEnumerable<T> ReadCsv<T>(this ICakeContext context, FilePath csvFile, ClassMap classMap)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
            return ReadCsv<T>(context, csvFile, classMap, configuration);
        }

        /// <summary>
        /// Reads a CSV file into a C# object.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to read.</param>
        /// <param name="classMap">The CSV class map.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>
        ///     <![CDATA[var people = ReadCsv<Person>("./people.csv", new ClassMap(), new CsvHelperSettings { HasHeaderRecord = true });]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("ReadCsv")]
        [CakeNamespaceImport("CsvHelper.Configuration.CsvClassMap")]
        public static IEnumerable<T> ReadCsv<T>(this ICakeContext context, FilePath csvFile, ClassMap classMap, CsvConfiguration configuration)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var csvHelpers = new CsvHelpers(context.FileSystem, context.Environment);
            return csvHelpers.ReadRecords<T>(csvFile, classMap, configuration);
        }

        /// <summary>
        ///  Writes the records to the specified file using the specified class map and settings.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to file to write.</param>
        /// <param name="records">The list objects you want to write to a csv.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>
        ///     <![CDATA[WriteCsv<Person>("./people.csv", new List<Person>());]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("WriteCsv")]
        public static void WriteCsv<T>(this ICakeContext context, FilePath csvFile, List<T> records)
        {
            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture);
            WriteCsv(context, csvFile, records, configuration);
        }

        /// <summary>
        ///  Writes the records to the specified file using the specified class map and settings.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to file to write.</param>
        /// <param name="records">The list objects you want to write to a csv.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>
        ///     <![CDATA[WriteCsv<Person>("./people.csv", new List<Person>(), new CsvHelperSettings { HasHeaderRecord = true });]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("WriteCsv")]
        public static void WriteCsv<T>(this ICakeContext context, FilePath csvFile, List<T> records, CsvConfiguration configuration)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var csvHelpers = new CsvHelpers(context.FileSystem, context.Environment);
            csvHelpers.WriteRecords(csvFile, records, configuration);
        }

        /// <summary>
        ///  Writes the records to the specified file using the specified class map and settings.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to file to write.</param>
        /// <param name="records">The list objects you want to write to a csv.</param>
        /// <param name="mapping">The property column mapping.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>
        ///     <![CDATA[var mapping = new Dictionary<string, string> { 
        ///                                       {"Id", "EmployeeId"},
        ///                                       {"Name", "FirstName"}
        ///                                   };]]>
        ///     <![CDATA[WriteCsv<Person>("./people.csv", new List<Person>(), mapping, new CsvHelperSettings { HasHeaderRecord = true });]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("WriteCsv")]
        public static void WriteCsv<T>(this ICakeContext context, FilePath csvFile, List<T> records,
            Dictionary<string, string> mapping, CsvConfiguration configuration)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var csvHelpers = new CsvHelpers(context.FileSystem, context.Environment);
            csvHelpers.WriteRecords(csvFile, records, mapping, configuration);
        }

        /// <summary>
        ///  Writes the records to the specified file using the specified class map and settings.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to file to write.</param>
        /// <param name="records">The list objects you want to write to a csv.</param>
        /// <param name="classMap">The CSV Helper Class Map.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>     
        ///     public sealed class PersonMap : CsvClassMap&lt;Person&gt;
        ///     {
        ///         public PersonMap()
        ///         {
        ///             Map(m => m.Id);
        ///             Map(m => m.Name);
        ///         }
        ///     }
        ///     <![CDATA[WriteCsv<Person>("./people.csv", new List<Person>(), PersonMap, new CsvHelperSettings { HasHeaderRecord = true });]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory("WriteCsv")]
        [CakeNamespaceImport("CsvHelper.Configuration.CsvClassMap")]
        public static void WriteCsv<T>(this ICakeContext context, FilePath csvFile, List<T> records, ClassMap classMap, CsvConfiguration configuration)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            var csvHelpers = new CsvHelpers(context.FileSystem, context.Environment);
            csvHelpers.WriteRecords(csvFile,records, classMap, configuration);
        }
    }
}
