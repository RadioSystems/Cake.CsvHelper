using System;
using System.IO;
using Cake.CsvHelper.Tests.Fixtures;
using Should;
using Xunit;

namespace Cake.CsvHelper.Tests {
    public sealed class CsvHelperTests {
        public sealed class TheReadCsvMethod {
            [Fact]
            public void Should_Throw_If_CsvFile_Is_Null() {
                // Given
                var fixture = new CsvHelpersFixture(false);
                
                // When 
                var result = Record.Exception(() => fixture.Read());
                
                // Then
                result.ShouldBeType<ArgumentNullException>().ParamName.ShouldEqual("csvFile");
            }

            [Fact]
            public void Should_Throw_If_File_Does_Not_Exist() {
                // Given
                var fixture = new CsvHelpersFixture(false);
                fixture.CsvFilePath = "/Working/test.csv";

                // When 
                var result = Record.Exception(() => fixture.Read());

                // Then
                result.ShouldBeType<FileNotFoundException>();
            }
        }

        public sealed class TheWriteCsvMethod {
            
        }
    }
}
