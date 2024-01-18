using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.IO;
using CsvHelper.Configuration;

namespace Cake.CsvHelper
{
    /// <summary>
    /// Contians functionality related to reading and writing CSV files.
    /// </summary>
    [CakeAliasCategory(nameof(CsvHelper))]
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
        [CakeAliasCategory(nameof(ReadCsv))]
        public static IEnumerable<T> ReadCsv<T>(this ICakeContext context, FilePath csvFile)
        {
            var settings = new CsvHelperSettings();
            return ReadCsv<T>(context, csvFile, null, settings);
        }

        /// <summary>
        /// Reads a CSV file into a C# object.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to read.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>
        ///     <![CDATA[var people = ReadCsv<Person>("./people.csv");]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(nameof(ReadCsv))]
        public static IEnumerable<T> ReadCsv<T>(this ICakeContext context, FilePath csvFile, CsvHelperSettings settings)
        {
           return ReadCsv<T>(context, csvFile, null, settings);
        }

        /// <summary>
        /// Reads a CSV file into a C# object.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to read.</param>
        /// <param name="classMap">The CSV class map.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>
        ///     <![CDATA[var people = ReadCsv<Person>("./people.csv", new ClassMap());]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(nameof(ReadCsv))]
        [CakeNamespaceImport("CsvHelper.Configuration.CsvClassMap")]
        public static IEnumerable<T> ReadCsv<T>(this ICakeContext context, FilePath csvFile, ClassMap classMap)
        {
            var settings = new CsvHelperSettings();
            return ReadCsv<T>(context, csvFile, classMap, settings);
        }

        /// <summary>
        /// Reads a CSV file into a C# object.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to read.</param>
        /// <param name="classMap">The CSV class map.</param>
        /// <param name="settings">The settings.</param>
        /// <returns>List objects as defined by type.</returns>
        /// <example>
        /// <code>
        ///     <![CDATA[var people = ReadCsv<Person>("./people.csv", new ClassMap(), new CsvHelperSettings { HasHeaderRecord = true });]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(nameof(ReadCsv))]
        [CakeNamespaceImport("CsvHelper.Configuration.CsvClassMap")]
        public static IEnumerable<T> ReadCsv<T>(this ICakeContext context, FilePath csvFile, ClassMap? classMap, CsvHelperSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var csvHelpers = new CsvHelpers(context.FileSystem, context.Environment);
            return csvHelpers.ReadRecords<T>(csvFile, classMap, settings);
        }

        /// <summary>
        ///  Writes the records to the speficed file using the specified class mapp and settings.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to file to write.</param>
        /// <param name="records">The list objects you want to write to a csv.</param>
        /// <example>
        /// <code>
        ///     <![CDATA[WriteCsv<Person>("./people.csv", new List<Person>());]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(nameof(WriteCsv))]
        public static void WriteCsv<T>(this ICakeContext context, FilePath csvFile, IList<T> records)
        {
            var settings = new CsvHelperSettings();
            WriteCsv(context, csvFile, records, settings);
        }

        /// <summary>
        ///  Writes the records to the speficed file using the specified class mapp and settings.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to file to write.</param>
        /// <param name="records">The list objects you want to write to a csv.</param>
        /// <param name="settings">The settings.</param>
        /// <example>
        /// <code>
        ///     <![CDATA[WriteCsv<Person>("./people.csv", new List<Person>(), new CsvHelperSettings { HasHeaderRecord = true });]]>
        /// </code>
        /// </example>
        [CakeMethodAlias]
        [CakeAliasCategory(nameof(WriteCsv))]
        public static void WriteCsv<T>(this ICakeContext context, FilePath csvFile, IList<T> records, CsvHelperSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var csvHelpers = new CsvHelpers(context.FileSystem, context.Environment);
            csvHelpers.WriteRecords(csvFile, records, settings);
        }

        /// <summary>
        ///  Writes the records to the speficed file using the specified class mapp and settings.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to file to write.</param>
        /// <param name="records">The list objects you want to write to a csv.</param>
        /// <param name="mapping">The property column mapping.</param>
        /// <param name="settings">The settings.</param>
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
        [CakeAliasCategory(nameof(WriteCsv))]
        public static void WriteCsv<T>(this ICakeContext context, FilePath csvFile, IList<T> records, IDictionary<string, string> mapping, CsvHelperSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var csvHelpers = new CsvHelpers(context.FileSystem, context.Environment);
            csvHelpers.WriteRecords(csvFile, records, mapping, settings);
        }

        /// <summary>
        ///  Writes the records to the speficed file using the specified class mapp and settings.
        /// </summary>
        /// <typeparam name="T">The type.</typeparam>
        /// <param name="context">The context.</param>
        /// <param name="csvFile">The CSV to file to write.</param>
        /// <param name="records">The list objects you want to write to a csv.</param>
        /// <param name="classMap">The CSV Helper Class Map.</param>
        /// <param name="settings">The settings.</param>
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
        [CakeAliasCategory(nameof(WriteCsv))]
        [CakeNamespaceImport("CsvHelper.Configuration.CsvClassMap")]
        public static void WriteCsv<T>(this ICakeContext context, FilePath csvFile, IList<T> records, ClassMap classMap, CsvHelperSettings settings)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var csvHelpers = new CsvHelpers(context.FileSystem, context.Environment);
            csvHelpers.WriteRecords(csvFile, records, classMap, settings);
        }
    }
}
