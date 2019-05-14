using Synker.Application.DataSourceReader;
using Synker.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;

namespace Synker.Application.Tests.DataReaders
{
    public class DefaultDataSourceReaderFactoryTest
    {
        [Fact]
        public void IsM3UDataSourceReader()
        {
            var factory = new DefaultDataSourceReaderFactory(null);
            var dataReader = factory.Create(new M3UPlaylistDataSource());
            dataReader.ShouldBeAssignableTo<M3UDataSourceReader>();
        }

        [Fact]
        public void IsXtreamDataSourceReader()
        {
            var factory = new DefaultDataSourceReaderFactory(null);
            var dataReader = factory.Create(new XtreamPlaylistDataSource());
            dataReader.ShouldBeAssignableTo<XtreamDataSourceReader>();
        }
    }
}
