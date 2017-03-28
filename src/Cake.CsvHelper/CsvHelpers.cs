using System;
using System.Collections.Generic;
using System.IO;
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
        /// <param name="settings">The settings.</param>
        /// <returns>List of defined type.</returns>
        public IEnumerable<T> ReadRecords<T>(FilePath csvFile, CsvClassMap classMap, CsvHelperSettings settings) {
            if (csvFile == null) {
                throw new ArgumentNullException(nameof(csvFile));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }
            var file = GetFile(csvFile);
            using (var textReader = new StreamReader(file.OpenRead()))
            using (var csvReader = new CsvReader(textReader)) {
                if (classMap != null) {
                    csvReader.Configuration.RegisterClassMap(classMap);
                }
                return csvReader.GetRecords<T>();
            }
        }

        /// <summary>
        /// Writes the records to the speficed file using the specified class mapp and settings.
        /// </summary>
        /// <typeparam name="T">The record type.</typeparam>
        /// <param name="csvFile">The CSV file to write.</param>
        /// <param name="records">The records to write.</param>
        /// <param name="classMap">The class map.</param>
        /// <param name="settings">The settings.</param>
        public void WriteRecords<T>(FilePath csvFile, List<T> records, CsvClassMap classMap, CsvHelperSettings settings) {
            if (csvFile == null) {
                throw new ArgumentNullException(nameof(csvFile));
            }
            if (records == null) {
                throw new ArgumentNullException(nameof(records));
            }
            if (classMap == null) {
                throw new ArgumentNullException(nameof(classMap));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            var file = GetFile(csvFile);
            using (var stream = file.OpenWrite())
            using (var textWriter = new StreamWriter(stream, settings.Encoding))
            using (var csvWriter = new CsvWriter(textWriter)) {
                csvWriter.Configuration.RegisterClassMap(classMap);
                csvWriter.WriteHeader<T>();
                foreach (var record in records) {
                    csvWriter.WriteRecord(record);
                }
                textWriter.Close();
            }
        }

        /// <summary>
        /// Writes the records to the speficed file using the specified mapping and settings.
        /// </summary>
        /// <typeparam name="T">The record type.</typeparam>
        /// <param name="csvFile">The CSV file to write.</param>
        /// <param name="records">The records to write.</param>
        /// <param name="mapping">The property column mapping.</param>
        /// <param name="settings">The settings.</param>
        public void WriteRecords<T>(FilePath csvFile, List<T> records, Dictionary<string, string> mapping,
            CsvHelperSettings settings) {
            if (mapping == null) {
                throw new ArgumentNullException(nameof(mapping));
            }
            var customMap = new DefaultCsvClassMap<T>();
            foreach (var key in mapping.Keys) {
                var columnName = mapping[key];
                if (string.IsNullOrEmpty(columnName)) continue;
                var propertyInfo = typeof(T).GetProperty(key);
                var newMap = new CsvPropertyMap(propertyInfo);
                newMap.Name(columnName);
                customMap.PropertyMaps.Add(newMap);
            }
            WriteRecords(csvFile, records, customMap, settings);
        }

        /// <summary>
        /// Writes the records to the speficed file using automap and settings.
        /// </summary>
        /// <typeparam name="T">The record type.</typeparam>
        /// <param name="csvFile">The CSV file to write.</param>
        /// <param name="records">The records to write.</param>
        /// <param name="settings">The settings.</param>
        public void WriteRecords<T>(FilePath csvFile, List<T> records, CsvHelperSettings settings) {
            if (csvFile == null) {
                throw new ArgumentNullException(nameof(csvFile));
            }
            if (records == null) {
                throw new ArgumentNullException(nameof(records));
            }
            if (settings == null) {
                throw new ArgumentNullException(nameof(settings));
            }

            // Make the path absolute if necessary.
            var file = GetFile(csvFile);
            using (var stream = file.OpenWrite())
            using (var textWriter = new StreamWriter(stream, settings.Encoding))
            using (var csvWriter = new CsvWriter(textWriter)) {
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
