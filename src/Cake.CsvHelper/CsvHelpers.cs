using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Cake.Core;
using Cake.Core.IO;
using CsvHelper;

namespace Cake.CsvHelpers {
    /// <summary>
    /// The CsvHelpers class for working with CSV Helper.
    /// </summary>
    public sealed class CsvHelpers {
        private readonly IFileSystem _fileSystem;
        private readonly ICakeEnvironment _environment;
        private readonly CsvHelperSettings _settings;


        /// <summary>
        /// Initializes new instance of the <see cref="CsvHelpers"/> class.
        /// </summary>
        /// <param name="fileSystem">The filesystem.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="settings">The settings.</param>
        public CsvHelpers(IFileSystem fileSystem, ICakeEnvironment environment, CsvHelperSettings settings) {
            _settings = settings;
            _fileSystem = fileSystem;
            _environment = environment;
        }

        /// <summary>
        /// Reads records from a CSV returning a list of the object type passed.
        /// </summary>
        /// <typeparam name="T">The type to use for mapping.</typeparam>
        /// <param name="csvFile">The CSV file to read.</param>
        /// <returns>List of defined type.</returns>
        public IEnumerable<T> ReadRecords<T>(FilePath csvFile) {
            if (csvFile == null) {
                throw new ArgumentNullException();
            }

            var file = _fileSystem.GetFile(csvFile);
            using(var textReader = new StreamReader(file.OpenRead()))
            using (var csvReader = new CsvReader(textReader)) {
                return csvReader.GetRecords<T>();
            }
        }

        public void WriteRecords() {
            throw new NotImplementedException();
        }
    }
}
