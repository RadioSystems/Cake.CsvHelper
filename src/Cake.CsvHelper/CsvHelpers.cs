using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace Cake.CsvHelper {
    /// <summary>
    /// The CsvHelpers class for working with CSV Helper.
    /// </summary>
    public sealed class CsvHelpers {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;

        /// <summary>
        /// Initializes new instance of the <see cref="CsvHelpers"/> class.
        /// </summary>
        /// <param name="fileSystem">The filesystem.</param>
        /// <param name="environment">The environment.</param>
        public CsvHelpers(IFileSystem fileSystem, ICakeEnvironment environment) {
            _fileSystem = fileSystem;
            _environment = environment;
        }

        /// <summary>
        /// Reads records from a CSV returning a list of the object type passed. Automapping is used if a Map is configured in settings.
        /// </summary>
        /// <typeparam name="T">The record type.</typeparam>
        /// <param name="csvFile">The CSV file to read.</param>
        /// <param name="classMap">The class map to use, null if not needed.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns>List of defined type.</returns>
        public IEnumerable<T> ReadRecords<T>(FilePath csvFile, ClassMap classMap, CsvConfiguration configuration) {
            if (csvFile == null) {
                throw new ArgumentNullException(nameof(csvFile));
            }
            if (configuration == null) {
                throw new ArgumentNullException(nameof(configuration));
            }
            var file = GetFile(csvFile);
            using (var textReader = new StreamReader(file.OpenRead()))
            using (var csvReader = new CsvReader(textReader, configuration)) {
                if (classMap != null) {
                    csvReader.Context.RegisterClassMap(classMap);
                }
                return csvReader.GetRecords<T>().ToList();
            }
        }

        /// <summary>
        /// Writes the records to the specified file using the specified class map and settings.
        /// </summary>
        /// <typeparam name="T">The record type.</typeparam>
        /// <param name="csvFile">The CSV file to write.</param>
        /// <param name="records">The records to write.</param>
        /// <param name="classMap">The class map.</param>
        /// <param name="configuration">The configuration.</param>
        public void WriteRecords<T>(FilePath csvFile, List<T> records, ClassMap classMap, CsvConfiguration configuration) {
            if (csvFile == null) {
                throw new ArgumentNullException(nameof(csvFile));
            }
            if (records == null) {
                throw new ArgumentNullException(nameof(records));
            }
            if (classMap == null) {
                throw new ArgumentNullException(nameof(classMap));
            }
            if (configuration == null) {
                throw new ArgumentNullException(nameof(configuration));
            }

            var file = GetFile(csvFile);
            using (var stream = file.OpenWrite())
            using (var textWriter = new StreamWriter(stream, configuration.Encoding))
            using (var csvWriter = new CsvWriter(textWriter, configuration)) {
                csvWriter.Context.RegisterClassMap(classMap);
                csvWriter.WriteHeader<T>();
                foreach (var record in records) {
                    csvWriter.WriteRecord(record);
                }
                textWriter.Close();
            }
        }

        /// <summary>
        /// Writes the records to the specified file using the specified mapping and settings.
        /// </summary>
        /// <typeparam name="T">The record type.</typeparam>
        /// <param name="csvFile">The CSV file to write.</param>
        /// <param name="records">The records to write.</param>
        /// <param name="mapping">The property column mapping.</param>
        /// <param name="configuration">The configuration.</param>
        public void WriteRecords<T>(FilePath csvFile, List<T> records, Dictionary<string, string> mapping,
            CsvConfiguration configuration) {
            if (mapping == null) {
                throw new ArgumentNullException(nameof(mapping));
            }
            var customMap = new DefaultClassMap<T>();
            customMap.AutoMap(configuration);
            WriteRecords(csvFile, records, customMap, configuration);
        }

        /// <summary>
        /// Writes the records to the specified file using automap and settings.
        /// </summary>
        /// <typeparam name="T">The record type.</typeparam>
        /// <param name="csvFile">The CSV file to write.</param>
        /// <param name="records">The records to write.</param>
        /// <param name="configuration">The configuration.</param>
        public void WriteRecords<T>(FilePath csvFile, List<T> records, CsvConfiguration configuration) {
            if (csvFile == null) {
                throw new ArgumentNullException(nameof(csvFile));
            }
            if (records == null) {
                throw new ArgumentNullException(nameof(records));
            }
            if (configuration == null) {
                throw new ArgumentNullException(nameof(configuration));
            }

            // Make the path absolute if necessary.
            var file = GetFile(csvFile);
            using (var stream = file.OpenWrite())
            using (var textWriter = new StreamWriter(stream, configuration.Encoding))
            using (var csvWriter = new CsvWriter(textWriter, configuration)) {
                csvWriter.WriteHeader<T>();
                foreach (var record in records) {
                    csvWriter.WriteRecord(record);
                }
                textWriter.Close();
            }
        }

        private IFile GetFile(FilePath path) {
            var file = path.IsRelative ? path.MakeAbsolute(_environment) : path;
            return _fileSystem.GetFile(file);
        }
    }
}
