using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.IO;
using Cake.CsvHelper.Tests.Fixtures;
using NSubstitute;
using Shouldly;
using Xunit;

namespace Cake.CsvHelper.Tests {
    public sealed class CsvHelperAliasesTests {
        public sealed class TheReadCsvMethod {
            [Fact]
            public void Should_Throw_If_Context_Is_Null() {
                // Given
                var file = Substitute.For<IFile>();

                // When
                var result = Record.Exception(() => CsvHelperAliases.ReadCsv<Person>(null, file.Path));

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("context");
            }

            [Fact]
            public void Should_Throw_If_CSV_File_Is_Null() {
                // Given
                var environment = Substitute.For<ICakeEnvironment>();
                var context = Substitute.For<ICakeContext>();
                context.Environment.Returns(environment);

                // When
                var result = Record.Exception(() => CsvHelperAliases.ReadCsv<Person>(context, null));

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("csvFile");
            }
        }

        public sealed class TheWriteCsvMethod {
            [Fact]
            public void Should_Throw_If_Context_Is_Null() {
                // Given
                var file = Substitute.For<IFile>();

                // When
                var result = Record.Exception(() => CsvHelperAliases.WriteCsv(null, file.Path, new List<Person>()));

                // Then
                result.ShouldBeOfType<ArgumentNullException>().ParamName.ShouldBe("context");
            }
        }
    }

    
}
